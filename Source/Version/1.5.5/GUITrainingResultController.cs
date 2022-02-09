using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUITrainingResultController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private UILabel trainingTitle;

	private UILabel trainingPackageTitle;

	private UILabel trainingPackageName;

	private UILabel trainingAreaTitle;

	private UILabel trainingAreaName;

	private UILabel expTitle;

	private UILabel baseExpTitle;

	private UILabel baseExpValue;

	private UILabel bonusExpTitle;

	private UILabel bonusExpValue;

	private UILabel totalExpTitle;

	private UILabel totalExpValue;

	private UITexture smithImage;

	private UILabel smithComment;

	private UITexture moodIcon;

	private UILabel smithName;

	private UISprite expBar;

	private UILabel jobLevelLabel;

	private UILabel levelUpLabel;

	private UISprite levelMaxSprite;

	private UIButton closeButton;

	private UIButton repeatButton;

	private bool allowRepeat;

	private Smith smith;

	private SmithTraining completedTraining;

	private Area area;

	private SmithTraining repeatTraining;

	private int smithLevelBefore;

	private int smithExpBefore;

	private int smithLevelAfter;

	private int smithExpAfter;

	private float expPercentStart;

	private float expPercentEnd;

	private int expDispPercent;

	private int expDispLevel;

	private bool isAnimating;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		trainingTitle = commonScreenObject.findChild(base.gameObject, "TrainingResult_bg/TrainingResultTitle_bg/TrainingResultTitle_label").GetComponent<UILabel>();
		trainingPackageTitle = commonScreenObject.findChild(base.gameObject, "TrainingResult_bg/TrainingPackage_bg/TrainingPackage_title").GetComponent<UILabel>();
		trainingPackageName = commonScreenObject.findChild(base.gameObject, "TrainingResult_bg/TrainingPackage_bg/TrainingPackage_label").GetComponent<UILabel>();
		trainingAreaTitle = commonScreenObject.findChild(base.gameObject, "TrainingResult_bg/Area_bg/Area_title").GetComponent<UILabel>();
		trainingAreaName = commonScreenObject.findChild(base.gameObject, "TrainingResult_bg/Area_bg/Area_label").GetComponent<UILabel>();
		expTitle = commonScreenObject.findChild(base.gameObject, "TrainingResult_bg/Exp_bg/ExpTitle_bg/ExpTitle_label").GetComponent<UILabel>();
		baseExpTitle = commonScreenObject.findChild(base.gameObject, "TrainingResult_bg/Exp_bg/BaseExp_title").GetComponent<UILabel>();
		baseExpValue = commonScreenObject.findChild(baseExpTitle.gameObject, "BaseExp_label").GetComponent<UILabel>();
		bonusExpTitle = commonScreenObject.findChild(base.gameObject, "TrainingResult_bg/Exp_bg/BonusExp_title").GetComponent<UILabel>();
		bonusExpValue = commonScreenObject.findChild(bonusExpTitle.gameObject, "BonusExp_label").GetComponent<UILabel>();
		totalExpTitle = commonScreenObject.findChild(base.gameObject, "TrainingResult_bg/Exp_bg/TotalExp_title").GetComponent<UILabel>();
		totalExpValue = commonScreenObject.findChild(totalExpTitle.gameObject, "TotalExp_label").GetComponent<UILabel>();
		smithImage = commonScreenObject.findChild(base.gameObject, "TrainingResult_bg/Smith_bg/Smith_texture").GetComponent<UITexture>();
		smithComment = commonScreenObject.findChild(base.gameObject, "TrainingResult_bg/Smith_bg/TrainingSmithText_label").GetComponent<UILabel>();
		moodIcon = commonScreenObject.findChild(base.gameObject, "TrainingResult_bg/Smith_bg/SmithMood_texture").GetComponent<UITexture>();
		smithName = commonScreenObject.findChild(base.gameObject, "TrainingResult_bg/SmithName_bg/SmithName_label").GetComponent<UILabel>();
		expBar = commonScreenObject.findChild(base.gameObject, "TrainingResult_bg/SmithName_bg/SmithLevel_bg/SmithLevel_bar").GetComponent<UISprite>();
		jobLevelLabel = commonScreenObject.findChild(base.gameObject, "TrainingResult_bg/SmithName_bg/SmithLevel_bg/SmithLevel_label").GetComponent<UILabel>();
		levelUpLabel = commonScreenObject.findChild(base.gameObject, "TrainingResult_bg/SmithName_bg/LevelUp_label").GetComponent<UILabel>();
		levelMaxSprite = commonScreenObject.findChild(base.gameObject, "TrainingResult_bg/SmithName_bg/SmithLevel_bg/SmithMax_sprite").GetComponent<UISprite>();
		closeButton = commonScreenObject.findChild(base.gameObject, "TrainingResult_bg/Close_button").GetComponent<UIButton>();
		repeatButton = commonScreenObject.findChild(base.gameObject, "TrainingResult_bg/Repeat_button").GetComponent<UIButton>();
		isAnimating = true;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "Close_button":
			if (!isAnimating)
			{
				smith.returnToShopStandby();
				viewController.closeTrainingResult(smith);
			}
			break;
		case "Repeat_button":
			if (!isAnimating)
			{
				smith.returnToShopStandby();
				if (sendSmithRepeatTraining())
				{
					viewController.closeTrainingResult();
				}
				else
				{
					viewController.closeTrainingResult(smith);
				}
			}
			break;
		}
	}

	public void processHover(bool isOver, string hoverName)
	{
		if (isOver)
		{
			switch (hoverName)
			{
			case "SmithMood_texture":
				tooltipScript.showText(CommonAPI.getMoodString(smith.getMoodState(), showDesc: true));
				break;
			case "Repeat_button":
				if (repeatTraining != null)
				{
					string textByRefIdWithDynText = gameData.getTextByRefIdWithDynText("trainingResult10", "[trainingName]", repeatTraining.getSmithTrainingName());
					tooltipScript.showText(textByRefIdWithDynText);
				}
				break;
			}
		}
		else
		{
			tooltipScript.setInactive();
		}
	}

	private void Update()
	{
		if (viewController != null && viewController.getIsPaused() && viewController.getGameStarted())
		{
			handleInput();
		}
	}

	private void handleInput()
	{
		if (Input.GetKeyDown(gameData.getKeyCodeByRefID("100008")))
		{
			processClick("Close_button");
		}
	}

	public void setReference(Smith aSmith)
	{
		smith = aSmith;
		completedTraining = smith.getTraining();
		area = smith.getExploreArea();
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		Season seasonByMonth = CommonAPI.getSeasonByMonth(player.getSeasonIndex());
		TrainingPackage trainingPackageByRefID = gameData.getTrainingPackageByRefID(area.getTrainingPackageRefId());
		if (trainingPackageByRefID != null && trainingPackageByRefID.getTrainingPackageRefID() != string.Empty)
		{
			repeatTraining = gameData.getSmithTrainingByRefId(trainingPackageByRefID.getTrainingRefIDBySeason(seasonByMonth));
		}
		trainingTitle.text = gameData.getTextByRefId("trainingResult01").ToUpper(CultureInfo.InvariantCulture);
		trainingPackageTitle.text = gameData.getTextByRefId("trainingResult02").ToUpper(CultureInfo.InvariantCulture);
		trainingPackageName.text = completedTraining.getSmithTrainingName();
		trainingAreaTitle.text = gameData.getTextByRefId("trainingResult03").ToUpper(CultureInfo.InvariantCulture);
		trainingAreaName.text = area.getAreaName();
		expTitle.text = gameData.getTextByRefId("trainingResult04").ToUpper(CultureInfo.InvariantCulture);
		baseExpTitle.text = gameData.getTextByRefId("trainingResult05").ToUpper(CultureInfo.InvariantCulture);
		totalExpTitle.text = gameData.getTextByRefId("trainingResult07").ToUpper(CultureInfo.InvariantCulture);
		area.removeAreaSmithRefID(smith.getSmithRefId());
		commonScreenObject.findChild(closeButton.gameObject, "Close_label").GetComponent<UILabel>().text = gameData.getTextByRefId("menuGeneral04");
		closeButton.isEnabled = false;
		repeatButton.isEnabled = false;
		string text = smith.tryAddStatusEffect();
		if (text != string.Empty)
		{
			gameData.addNewWhetsappMsg(smith.getSmithName(), text, "Image/Smith/Portraits/" + smith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
		}
		SmithMood moodState = smith.getMoodState();
		float moodEffect = CommonAPI.getMoodEffect(moodState, -0.05f, 1f, 0.05f);
		smithLevelBefore = smith.getSmithLevel();
		smithExpBefore = smith.getSmithExp();
		int smithTrainingExp = completedTraining.getSmithTrainingExp();
		int num = (int)((float)smithTrainingExp * moodEffect);
		int num2 = smithTrainingExp + num;
		smith.addSmithExp(num2);
		smithLevelAfter = smith.getSmithLevel();
		smithExpAfter = smith.getSmithExp();
		calculateExpStartEnd();
		bool hasMoodLimit = false;
		if (!gameData.checkFeatureIsUnlocked(gameLockSet, "MOODLIMIT", completedTutorialIndex))
		{
			hasMoodLimit = true;
		}
		float aReduce = smith.getExploreArea().getMoodFactor() * 7f;
		smith.reduceSmithMood(aReduce, hasMoodLimit);
		SmithMood moodState2 = smith.getMoodState();
		string whetsappMoodString = CommonAPI.getWhetsappMoodString(moodState2);
		if (whetsappMoodString != string.Empty)
		{
			gameData.addNewWhetsappMsg(smith.getSmithName(), whetsappMoodString, "Image/Smith/Portraits/" + smith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
		}
		baseExpValue.text = CommonAPI.formatNumber(smithTrainingExp);
		float num3 = CommonAPI.convertRatioToPercent2DP((float)num / (float)smithTrainingExp);
		bonusExpTitle.text = gameData.getTextByRefId("trainingResult06").ToUpper(CultureInfo.InvariantCulture) + " (" + num3 + "%)";
		bonusExpValue.text = CommonAPI.formatNumber(num);
		totalExpValue.text = CommonAPI.formatNumber(num2);
		smithImage.mainTexture = commonScreenObject.loadTexture("Image/Smith/" + smith.getImage() + "_manage");
		smithComment.text = whetsappMoodString;
		moodIcon.mainTexture = commonScreenObject.loadTexture("Image/Mood/" + CommonAPI.getMoodIcon(moodState2));
		audioController.playSmithMoodSfx(moodState2);
		smithName.text = smith.getSmithName();
		levelUpLabel.text = "Lv. +" + (smithLevelAfter - smithLevelBefore) + "!";
		commonScreenObject.findChild(levelMaxSprite.gameObject, "SmithMax_labelLeft").GetComponent<UILabel>().text = gameData.getTextByRefId("smithStats22").ToUpper(CultureInfo.InvariantCulture);
		commonScreenObject.findChild(levelMaxSprite.gameObject, "SmithMax_labelRight").GetComponent<UILabel>().text = gameData.getTextByRefId("smithStats23").ToUpper(CultureInfo.InvariantCulture);
		jobLevelLabel.text = smith.getSmithJob().getSmithJobName() + " " + gameData.getTextByRefId("smithStatsShort01") + " " + smithLevelBefore;
		levelMaxSprite.alpha = 0f;
		isAnimating = true;
		expDispPercent = 0;
		expDispLevel = 0;
		UILabel component = commonScreenObject.findChild(repeatButton.gameObject, "Repeat_label").GetComponent<UILabel>();
		UILabel component2 = commonScreenObject.findChild(repeatButton.gameObject, "RepeatCost_bg/RepeatCost_label").GetComponent<UILabel>();
		switch (checkRepeatTrainingAvailable(area, showPopup: false))
		{
		case "SUCCESS":
		{
			string text2 = (component.text = gameData.getTextByRefId("trainingResult08"));
			component2.text = CommonAPI.formatNumber(repeatTraining.getSmithTrainingCost());
			allowRepeat = true;
			break;
		}
		case "NO_STARCH":
			component.text = gameData.getTextByRefId("errorCommon05");
			component2.text = CommonAPI.formatNumber(repeatTraining.getSmithTrainingCost());
			allowRepeat = false;
			break;
		case "SMITH_MAX_LEVEL":
			component.text = gameData.getTextByRefId("trainingResult09");
			component2.text = "-";
			allowRepeat = false;
			break;
		case "NO_TRAINING":
		case "REGION_CHANGE":
		case "AREA_FULL":
			component.text = gameData.getTextByRefId("exploreResult03");
			component2.text = "-";
			allowRepeat = false;
			break;
		}
		StartCoroutine(showSmithExpBar());
	}

	private void calculateExpStartEnd()
	{
		SmithJobClass smithJob = smith.getSmithJob();
		int expToLevelUp = smithJob.getExpToLevelUp(smithLevelBefore);
		expPercentStart = (float)smithExpBefore / (float)expToLevelUp;
		int expToLevelUp2 = smithJob.getExpToLevelUp(smithLevelAfter);
		expPercentEnd = (float)smithExpAfter / (float)expToLevelUp2;
		for (int i = 0; i < smithLevelAfter - smithLevelBefore; i++)
		{
			expPercentEnd += 1f;
		}
		CommonAPI.debug("calculateExpStartEnd " + expPercentStart + " -> " + expPercentEnd);
		CommonAPI.debug("Level " + smithLevelBefore + " -> " + smithLevelAfter);
		CommonAPI.debug("EXP " + smithExpBefore + " -> " + smithExpAfter);
	}

	public bool sendSmithRepeatTraining()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		if (checkRepeatTrainingAvailable(area, showPopup: true) == "SUCCESS")
		{
			Season seasonByMonth = CommonAPI.getSeasonByMonth(player.getSeasonIndex());
			area.addTimesTrain(1);
			int smithTrainingCost = repeatTraining.getSmithTrainingCost();
			player.reduceGold(smithTrainingCost, allowNegative: true);
			audioController.playPurchaseAudio();
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseTraining, string.Empty, -1 * smithTrainingCost);
			SmithAction smithActionByRefId = gameData.getSmithActionByRefId("907");
			List<int> list = new List<int>();
			list.Add(area.getTravelTime());
			list.Add(10);
			list.Add(area.getTravelTime());
			list.Add(-1);
			List<SmithExploreState> list2 = new List<SmithExploreState>();
			list2.Add(SmithExploreState.SmithExploreStateTravelToTraining);
			list2.Add(SmithExploreState.SmithExploreStateTraining);
			list2.Add(SmithExploreState.SmithExploreStateTrainingTravelHome);
			list2.Add(SmithExploreState.SmithExploreStateTrainingReturned);
			List<AreaStatus> areaStatusListByAreaAndSeason = gameData.getAreaStatusListByAreaAndSeason(area.getAreaRefId(), seasonByMonth);
			area.addAreaSmithRefID(smith.getSmithRefId());
			smith.setSmithAction(smithActionByRefId, area.getTravelTime() * 2 + 10);
			smith.setExploreStateList(list2, list);
			smith.setExploreArea(area);
			smith.setTraining(repeatTraining);
			smith.setAreaStatusList(areaStatusListByAreaAndSeason);
			return true;
		}
		return false;
	}

	private string checkRepeatTrainingAvailable(Area aArea, bool showPopup)
	{
		Player player = game.getPlayer();
		if (player.getAreaRegion() != aArea.getRegion())
		{
			return "REGION_CHANGE";
		}
		if (repeatTraining == null || repeatTraining.getSmithTrainingRefId() == string.Empty)
		{
			return "NO_TRAINING";
		}
		if (repeatTraining.getSmithTrainingCost() > player.getPlayerGold())
		{
			return "NO_STARCH";
		}
		if (smith.checkIsSmithMaxLevel())
		{
			return "SMITH_MAX_LEVEL";
		}
		if (aArea.getAreaSmithRefID(smith.getSmithRefId()).Count > 2)
		{
			if (showPopup)
			{
				string textByRefId = game.getGameData().getTextByRefId("menuSmithManagement53");
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, string.Empty, textByRefId, PopupType.PopupTypeNothing, null, colorTag: false, null, map: true, string.Empty);
			}
			return "AREA_FULL";
		}
		return "SUCCESS";
	}

	private IEnumerator showSmithExpBar()
	{
		while (isAnimating)
		{
			float displayExp = expPercentStart + (expPercentEnd - expPercentStart) * (float)expDispPercent / 100f;
			int levelCount = 0;
			while (displayExp > 1f)
			{
				displayExp -= 1f;
				levelCount++;
			}
			expBar.fillAmount = displayExp;
			if (levelCount > expDispLevel)
			{
				int num = smithLevelBefore + levelCount;
				jobLevelLabel.text = smith.getSmithJob().getSmithJobName() + " " + CommonAPI.getGameData().getTextByRefId("smithStatsShort01") + " " + num;
			}
			expDispPercent++;
			if (expDispPercent >= 100)
			{
				isAnimating = false;
				if (levelCount > 0)
				{
					commonScreenObject.tweenScale(levelUpLabel.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.4f, null, string.Empty);
				}
				jobLevelLabel.text = smith.getCurrentJobClassLevelString();
				if (smith.checkIsSmithMaxLevel())
				{
					levelMaxSprite.alpha = 1f;
					expBar.fillAmount = 0f;
				}
				closeButton.isEnabled = true;
				if (allowRepeat)
				{
					repeatButton.isEnabled = true;
				}
			}
			yield return new WaitForSeconds(0.01f);
		}
	}
}
