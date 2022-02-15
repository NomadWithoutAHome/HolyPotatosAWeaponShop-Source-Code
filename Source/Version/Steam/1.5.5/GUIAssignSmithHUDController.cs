using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIAssignSmithHUDController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private GUIAnimationClickController animClickController;

	private TooltipTextScript smithInfoScript;

	private AudioController audioController;

	private Smith smithInfo;

	private List<SmithJobClass> jcList;

	private List<string> jobClassStringList;

	private SmithJobClass selectedJobClass;

	private UILabel assignNoteLabel;

	private UILabel activityLabel;

	private UIButton fireButton;

	private UILabel fireLabel;

	private UILabel salaryLabel;

	private UITexture smithImg;

	private UITexture smithMood;

	private UILabel smithLevelLabel;

	private UISprite smithLevelBg;

	private UISprite smithMaxLevelBg;

	private UILabel smithNameLabel;

	private UIButton standbyButton;

	private UILabel standbyLabel;

	private UILabel accValue;

	private UILabel atkValue;

	private UILabel spdValue;

	private UILabel magValue;

	private UIButton worldMapButton;

	private UILabel worldMapLabel;

	private UILabel explorerLabel;

	private UILabel explorerLvLabel;

	private UILabel merchantLabel;

	private UILabel merchantLvLabel;

	private UIButton closeButton;

	private UIButton jobTreeButton;

	private UILabel jobTreeLabel;

	private UISprite newClassBg;

	private bool isJobChange;

	private UILabel effectTitle;

	private UILabel effectName;

	private UILabel effectDuration;

	private UIButton smithScrollLeft;

	private UIButton smithScrollRight;

	private Smith smithLeft;

	private Smith smithRight;

	private bool tutorial;

	private Color32 greenCol;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		animClickController = GameObject.Find("GUIAnimationClickController").GetComponent<GUIAnimationClickController>();
		smithInfoScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		smithInfo = null;
		jcList = new List<SmithJobClass>();
		jobClassStringList = new List<string>();
		selectedJobClass = null;
		assignNoteLabel = GameObject.Find("AssignNoteLabel").GetComponent<UILabel>();
		activityLabel = commonScreenObject.findChild(base.gameObject, "ActivityFrame/ActivityLabel").GetComponent<UILabel>();
		fireButton = commonScreenObject.findChild(base.gameObject, "FireButton").GetComponent<UIButton>();
		fireLabel = commonScreenObject.findChild(fireButton.gameObject, "FireLabel").GetComponent<UILabel>();
		salaryLabel = commonScreenObject.findChild(base.gameObject, "SalaryFrame/SalaryLabel").GetComponent<UILabel>();
		smithImg = commonScreenObject.findChild(base.gameObject, "SmithBg/SmithImg").GetComponent<UITexture>();
		smithMood = commonScreenObject.findChild(base.gameObject, "SmithBg/SmithMood").GetComponent<UITexture>();
		smithLevelLabel = commonScreenObject.findChild(base.gameObject, "SmithLevelFrame/SmithLevelLabel").GetComponent<UILabel>();
		smithLevelBg = commonScreenObject.findChild(base.gameObject, "SmithLevelFrame/SmithLevelBg").GetComponent<UISprite>();
		smithMaxLevelBg = commonScreenObject.findChild(base.gameObject, "SmithLevelFrame/SmithMaxLevelBg").GetComponent<UISprite>();
		smithNameLabel = commonScreenObject.findChild(base.gameObject, "SmithNameBg/SmithNameLabel").GetComponent<UILabel>();
		standbyButton = commonScreenObject.findChild(base.gameObject, "StandbyButton").GetComponent<UIButton>();
		standbyLabel = commonScreenObject.findChild(standbyButton.gameObject, "StandbyLabel").GetComponent<UILabel>();
		GameObject aObject = commonScreenObject.findChild(base.gameObject, "StatsBg").gameObject;
		accValue = commonScreenObject.findChild(aObject, "Acc/AccValue").GetComponent<UILabel>();
		atkValue = commonScreenObject.findChild(aObject, "Atk/AtkValue").GetComponent<UILabel>();
		spdValue = commonScreenObject.findChild(aObject, "Spd/SpdValue").GetComponent<UILabel>();
		magValue = commonScreenObject.findChild(aObject, "Mag/MagValue").GetComponent<UILabel>();
		worldMapButton = commonScreenObject.findChild(base.gameObject, "WorldMapButton").GetComponent<UIButton>();
		worldMapLabel = commonScreenObject.findChild(worldMapButton.gameObject, "WorldMapLabel").GetComponent<UILabel>();
		explorerLabel = commonScreenObject.findChild(base.gameObject, "ExplorerFrame/ExplorerLabel").GetComponent<UILabel>();
		explorerLvLabel = commonScreenObject.findChild(base.gameObject, "ExplorerFrame/ExplorerLvLabel").GetComponent<UILabel>();
		merchantLabel = commonScreenObject.findChild(base.gameObject, "MerchantFrame/MerchantLabel").GetComponent<UILabel>();
		merchantLvLabel = commonScreenObject.findChild(base.gameObject, "MerchantFrame/MerchantLvLabel").GetComponent<UILabel>();
		closeButton = commonScreenObject.findChild(base.gameObject, "CloseButton").GetComponent<UIButton>();
		jobTreeButton = commonScreenObject.findChild(base.gameObject, "JobTreeButton").GetComponent<UIButton>();
		jobTreeLabel = commonScreenObject.findChild(base.gameObject, "JobTreeButton/JobTreeLabel").GetComponent<UILabel>();
		newClassBg = commonScreenObject.findChild(base.gameObject, "NewClass_bg").GetComponent<UISprite>();
		isJobChange = false;
		GameObject aObject2 = commonScreenObject.findChild(base.gameObject, "EffectBg").gameObject;
		effectTitle = commonScreenObject.findChild(aObject2, "EffectTitle").GetComponent<UILabel>();
		effectName = commonScreenObject.findChild(aObject2, "EffectName").GetComponent<UILabel>();
		effectDuration = commonScreenObject.findChild(aObject2, "EffectDuration").GetComponent<UILabel>();
		smithScrollLeft = commonScreenObject.findChild(base.gameObject, "SmithBg/SmithScroll_left").GetComponent<UIButton>();
		smithScrollRight = commonScreenObject.findChild(base.gameObject, "SmithBg/SmithScroll_right").GetComponent<UIButton>();
		greenCol = new Color32(86, 174, 89, byte.MaxValue);
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "CloseButton":
			assignNoteLabel.text = string.Empty;
			animClickController.closeSmithActionMenu(resume: true, resumeFromPlayerPause: false);
			break;
		case "WorldMapButton":
			if (checkSmithActivity())
			{
				animClickController.closeSmithActionMenu(resume: true, resumeFromPlayerPause: false);
				viewController.showWorldMap(ActivityType.ActivityTypeBlank, smithInfo);
			}
			break;
		case "StandbyButton":
			if (checkSmithInShop(standby: true))
			{
				SmithAction smithActionByRefId = game.getGameData().getSmithActionByRefId("905");
				smithInfo.setSmithAction(smithActionByRefId, -1);
				assignNoteLabel.text = string.Empty;
				animClickController.closeSmithActionMenu();
			}
			break;
		case "FireButton":
			if (checkSmithActivity())
			{
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeYesAndNo, resume: false, "FIRE SMITH", game.getGameData().getTextByRefIdWithDynText("menuSmithManagement03", "[smithName]", smithInfo.getSmithName()), PopupType.PopupTypeFireAssignSmith, null, colorTag: false, null, map: false, string.Empty);
			}
			break;
		case "ConfirmButton":
			shopMenuController.doSmithJobChange(smithInfo, selectedJobClass);
			setReference(smithInfo);
			break;
		case "JobTreeButton":
			viewController.showSmithJobChange(smithInfo);
			break;
		case "SmithScroll_left":
			selectArrow(isLeft: true);
			break;
		case "SmithScroll_right":
			selectArrow(isLeft: false);
			break;
		}
	}

	public void processHover(bool isOver, string hoverName)
	{
		if (isOver)
		{
			GameData gameData = game.getGameData();
			switch (hoverName)
			{
			case "SmithMood":
				if (smithInfo != null && smithInfo.getSmithRefId() != string.Empty)
				{
					smithInfoScript.showText(CommonAPI.getMoodString(smithInfo.getMoodState(), showDesc: true));
				}
				break;
			case "SmithLevelFrame":
				if (smithInfo.getSmithRefId() != string.Empty)
				{
					smithInfoScript.showText(getJobclassTooltipString());
				}
				break;
			case "ExplorerFrame":
				if (smithInfo.getSmithRefId() != string.Empty)
				{
					smithInfoScript.showText(getExplorerTooltipString());
				}
				break;
			case "MerchantFrame":
				if (smithInfo.getSmithRefId() != string.Empty)
				{
					smithInfoScript.showText(getMerchantTooltipString());
				}
				break;
			case "SalaryFrame":
				if (smithInfo.getSmithRefId() != string.Empty)
				{
					smithInfoScript.showText(getSalaryTooltipString());
				}
				break;
			case "EffectBg":
				if (smithInfo.getSmithRefId() != string.Empty)
				{
					string smithEffectTooltipString = getSmithEffectTooltipString();
					if (smithEffectTooltipString != string.Empty)
					{
						smithInfoScript.showText(smithEffectTooltipString);
					}
				}
				break;
			case "WorldMapButton":
				smithInfoScript.showText(gameData.getTextByRefId("assignSmith12"));
				break;
			case "StandbyButton":
				smithInfoScript.showText(gameData.getTextByRefId("assignSmith13"));
				break;
			case "FireButton":
				smithInfoScript.showText(gameData.getTextByRefId("assignSmith14"));
				break;
			case "StatsBg":
				showSmithStats(isSeparate: true);
				break;
			}
		}
		else
		{
			smithInfoScript.setInactive();
			showSmithStats(isSeparate: false);
		}
	}

	private void Update()
	{
		if (viewController != null && viewController.getIsPaused() && viewController.getGameStarted() && GameObject.Find("Panel_SmithJobChange") == null)
		{
			handleInput();
		}
	}

	private void handleInput()
	{
		if ((Input.GetMouseButtonDown(1) || (Input.GetKeyDown(gameData.getKeyCodeByRefID("100007")) && closeButton.isEnabled)) && GameObject.Find("Panel_Tutorial") == null)
		{
			processClick("CloseButton");
		}
	}

	public void setReference(Smith aSmith, bool aTutorial = false)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		tutorial = aTutorial;
		player.setLastSelectSmith(aSmith);
		smithInfo = aSmith;
		jobTreeLabel.text = gameData.getTextByRefId("assignSmith03");
		fireLabel.text = gameData.getTextByRefId("assignSmith02");
		standbyLabel.text = gameData.getTextByRefId("assignSmith04");
		activityLabel.text = smithInfo.getSmithActionText();
		worldMapLabel.text = gameData.getTextByRefId("assignSmith09");
		newClassBg.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("assignSmith21");
		effectTitle.text = gameData.getTextByRefId("assignSmith11").ToUpper(CultureInfo.InvariantCulture);
		string effectRefID = string.Empty;
		string text = string.Empty;
		string empty = string.Empty;
		List<string> smithStatusEffectListNoRepeat = smithInfo.getSmithStatusEffectListNoRepeat();
		if (smithStatusEffectListNoRepeat.Count > 0)
		{
			foreach (string item in smithStatusEffectListNoRepeat)
			{
				effectRefID = item;
				SmithStatusEffect smithStatusEffectByRefId = gameData.getSmithStatusEffectByRefId(item);
				text += smithStatusEffectByRefId.getEffectName();
			}
			effectName.text = text;
			effectDuration.text = CommonAPI.convertHalfHoursToTimeString(smithInfo.getSmithStatusEffectDurationByEffectRefID(effectRefID)) + " " + gameData.getTextByRefId("assignSmith20");
		}
		else
		{
			effectName.text = gameData.getTextByRefId("menuGeneral06");
			effectDuration.text = string.Empty;
		}
		assignNoteLabel.text = gameData.getTextByRefId("assignSmith07");
		SmithAction smithAction = smithInfo.getSmithAction();
		switch (smithAction.getRefId())
		{
		case "902":
		case "901":
		case "904":
		case "903":
		case "907":
		case "906":
			fireButton.isEnabled = false;
			worldMapButton.isEnabled = false;
			assignNoteLabel.text = gameData.getTextByRefId("assignSmith10");
			break;
		}
		smithNameLabel.text = smithInfo.getSmithName();
		salaryLabel.text = CommonAPI.formatNumber(smithInfo.getSmithSalary()) + gameData.getTextByRefId("smithStatsShort07");
		smithImg.mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/" + smithInfo.getImage());
		smithMood.mainTexture = commonScreenObject.loadTexture("Image/Mood/" + CommonAPI.getMoodIcon(smithInfo.getMoodState()));
		audioController.playSmithMoodSfx(smithInfo.getMoodState());
		smithLevelLabel.text = smithInfo.getCurrentJobClassLevelString();
		smithLevelBg.fillAmount = (float)smithInfo.getSmithExp() / (float)smithInfo.getMaxExp();
		if (smithInfo.checkIsSmithMaxLevel())
		{
			smithMaxLevelBg.alpha = 1f;
		}
		else
		{
			smithMaxLevelBg.alpha = 0f;
		}
		showSmithStats(isSeparate: false);
		explorerLabel.text = gameData.getTextByRefId("SmithExplorerText");
		merchantLabel.text = gameData.getTextByRefId("SmithMerchantText");
		explorerLvLabel.text = gameData.getTextByRefIdWithDynText("smithStats24", "[level]", smithInfo.getExploreLevel().ToString());
		merchantLvLabel.text = gameData.getTextByRefIdWithDynText("smithStats24", "[level]", smithInfo.getMerchantLevel().ToString());
		checkNewClass();
		if (!smithInfo.checkSmithInShop())
		{
			standbyButton.isEnabled = false;
			standbyLabel.color = Color.gray;
		}
		if (tutorial)
		{
			worldMapButton.isEnabled = false;
			standbyButton.isEnabled = false;
			fireButton.isEnabled = false;
			closeButton.isEnabled = false;
			jobTreeButton.isEnabled = false;
		}
		else
		{
			setButtonStates();
		}
		setArrows(tutorial);
	}

	public void refreshSmithStats()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		string effectRefID = string.Empty;
		string text = string.Empty;
		string empty = string.Empty;
		List<string> smithStatusEffectListNoRepeat = smithInfo.getSmithStatusEffectListNoRepeat();
		if (smithStatusEffectListNoRepeat.Count > 0)
		{
			foreach (string item in smithStatusEffectListNoRepeat)
			{
				effectRefID = item;
				SmithStatusEffect smithStatusEffectByRefId = gameData.getSmithStatusEffectByRefId(item);
				text += smithStatusEffectByRefId.getEffectName();
			}
			effectName.text = text;
			effectDuration.text = CommonAPI.convertHalfHoursToTimeString(smithInfo.getSmithStatusEffectDurationByEffectRefID(effectRefID)) + " " + gameData.getTextByRefId("assignSmith20");
		}
		else
		{
			effectName.text = gameData.getTextByRefId("menuGeneral06");
			effectDuration.text = string.Empty;
		}
		assignNoteLabel.text = gameData.getTextByRefId("assignSmith07");
		SmithAction smithAction = smithInfo.getSmithAction();
		switch (smithAction.getRefId())
		{
		case "902":
		case "901":
		case "904":
		case "903":
		case "907":
		case "906":
			fireButton.isEnabled = false;
			worldMapButton.isEnabled = false;
			assignNoteLabel.text = gameData.getTextByRefId("assignSmith10");
			break;
		}
		smithNameLabel.text = smithInfo.getSmithName();
		salaryLabel.text = CommonAPI.formatNumber(smithInfo.getSmithSalary()) + gameData.getTextByRefId("smithStatsShort07");
		smithImg.mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/" + smithInfo.getImage());
		smithMood.mainTexture = commonScreenObject.loadTexture("Image/Mood/" + CommonAPI.getMoodIcon(smithInfo.getMoodState()));
		smithLevelLabel.text = smithInfo.getCurrentJobClassLevelString();
		smithLevelBg.fillAmount = (float)smithInfo.getSmithExp() / (float)smithInfo.getMaxExp();
		if (smithInfo.checkIsSmithMaxLevel())
		{
			smithMaxLevelBg.alpha = 1f;
		}
		else
		{
			smithMaxLevelBg.alpha = 0f;
		}
		showSmithStats(isSeparate: false);
		explorerLvLabel.text = gameData.getTextByRefIdWithDynText("smithStats24", "[level]", smithInfo.getExploreLevel().ToString());
		merchantLvLabel.text = gameData.getTextByRefIdWithDynText("smithStats24", "[level]", smithInfo.getMerchantLevel().ToString());
		checkNewClass();
		setButtonStates();
	}

	public void showSmithStats(bool isSeparate)
	{
		if (isSeparate)
		{
			int smithAddedTechnique = smithInfo.getSmithAddedTechnique();
			int smithTechnique = smithInfo.getSmithTechnique();
			int smithAddedPower = smithInfo.getSmithAddedPower();
			int smithPower = smithInfo.getSmithPower();
			int smithAddedIntelligence = smithInfo.getSmithAddedIntelligence();
			int smithIntelligence = smithInfo.getSmithIntelligence();
			int smithAddedLuck = smithInfo.getSmithAddedLuck();
			int smithLuck = smithInfo.getSmithLuck();
			if (smithAddedTechnique > 0)
			{
				accValue.color = greenCol;
				accValue.text = CommonAPI.formatNumber(smithTechnique) + "\n(+" + CommonAPI.formatNumber(smithAddedTechnique) + ")";
			}
			else if (smithAddedTechnique < 0)
			{
				accValue.color = Color.red;
				accValue.text = CommonAPI.formatNumber(smithTechnique) + "\n(" + CommonAPI.formatNumber(smithAddedTechnique) + ")";
			}
			else
			{
				accValue.color = Color.black;
				accValue.text = CommonAPI.formatNumber(smithTechnique);
			}
			if (smithAddedPower > 0)
			{
				atkValue.color = greenCol;
				atkValue.text = CommonAPI.formatNumber(smithPower) + "\n(+" + CommonAPI.formatNumber(smithAddedPower) + ")";
			}
			else if (smithAddedPower < 0)
			{
				atkValue.color = Color.red;
				atkValue.text = CommonAPI.formatNumber(smithPower) + "\n(" + CommonAPI.formatNumber(smithAddedPower) + ")";
			}
			else
			{
				atkValue.color = Color.black;
				atkValue.text = CommonAPI.formatNumber(smithPower);
			}
			if (smithAddedIntelligence > 0)
			{
				spdValue.color = greenCol;
				spdValue.text = CommonAPI.formatNumber(smithIntelligence) + "\n(+" + CommonAPI.formatNumber(smithAddedIntelligence) + ")";
			}
			else if (smithAddedIntelligence < 0)
			{
				spdValue.color = Color.red;
				spdValue.text = CommonAPI.formatNumber(smithIntelligence) + "\n(" + CommonAPI.formatNumber(smithAddedIntelligence) + ")";
			}
			else
			{
				spdValue.color = Color.black;
				spdValue.text = CommonAPI.formatNumber(smithIntelligence);
			}
			if (smithAddedLuck > 0)
			{
				magValue.color = greenCol;
				magValue.text = CommonAPI.formatNumber(smithLuck) + "\n(+" + CommonAPI.formatNumber(smithAddedLuck) + ")";
			}
			else if (smithAddedLuck < 0)
			{
				magValue.color = Color.red;
				magValue.text = CommonAPI.formatNumber(smithLuck) + "\n(" + CommonAPI.formatNumber(smithAddedLuck) + ")";
			}
			else
			{
				magValue.color = Color.black;
				magValue.text = CommonAPI.formatNumber(smithLuck);
			}
		}
		else
		{
			accValue.color = Color.black;
			accValue.text = CommonAPI.formatNumber(smithInfo.getSmithTechnique());
			atkValue.color = Color.black;
			atkValue.text = CommonAPI.formatNumber(smithInfo.getSmithPower());
			spdValue.color = Color.black;
			spdValue.text = CommonAPI.formatNumber(smithInfo.getSmithIntelligence());
			magValue.color = Color.black;
			magValue.text = CommonAPI.formatNumber(smithInfo.getSmithLuck());
		}
	}

	public void changeDepth(int depth)
	{
		base.gameObject.GetComponent<UIPanel>().depth = depth;
	}

	public void revertDepth()
	{
		base.gameObject.GetComponent<UIPanel>().depth = 1;
	}

	private void setArrows(bool tutorial)
	{
		List<Smith> smithList = game.getPlayer().getSmithList();
		if (tutorial || smithList.Count < 2)
		{
			smithScrollLeft.isEnabled = false;
			smithLeft = null;
			smithScrollRight.isEnabled = false;
			smithRight = null;
			return;
		}
		for (int i = 0; i < smithList.Count; i++)
		{
			if (smithList[i].getSmithRefId() == smithInfo.getSmithRefId())
			{
				if (i == 0)
				{
					smithScrollLeft.isEnabled = true;
					smithLeft = smithList[smithList.Count - 1];
				}
				else
				{
					smithScrollLeft.isEnabled = true;
					smithLeft = smithList[i - 1];
				}
				if (i == smithList.Count - 1)
				{
					smithScrollRight.isEnabled = true;
					smithRight = smithList[0];
				}
				else
				{
					smithScrollRight.isEnabled = true;
					smithRight = smithList[i + 1];
				}
			}
		}
	}

	private void selectArrow(bool isLeft)
	{
		GameObject gameObject = GameObject.Find("Panel_SmithJobChange");
		if (isLeft)
		{
			if (smithLeft != null)
			{
				if (gameObject != null)
				{
					gameObject.GetComponent<GUISmithJobChangeController>().setReference(smithLeft);
				}
				animClickController.setSelectedSmith(smithLeft);
				GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>().changeLayer(smithLeft, bringUp: true, animatorEnabled: false);
				setReference(smithLeft);
			}
		}
		else if (smithRight != null)
		{
			if (gameObject != null)
			{
				gameObject.GetComponent<GUISmithJobChangeController>().setReference(smithRight);
			}
			animClickController.setSelectedSmith(smithRight);
			GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>().changeLayer(smithRight, bringUp: true, animatorEnabled: false);
			setReference(smithRight);
		}
	}

	private void fitSmithJob(SmithJobClass fitJob)
	{
		if (smithInfo.getSmithPower() < smithInfo.fitSmithPower(fitJob))
		{
			atkValue.color = Color.green;
		}
		else if (smithInfo.getSmithPower() > smithInfo.fitSmithPower(fitJob))
		{
			atkValue.color = Color.red;
		}
		else
		{
			atkValue.color = Color.black;
		}
		atkValue.text = smithInfo.fitSmithPower(fitJob).ToString();
		if (smithInfo.getSmithIntelligence() < smithInfo.fitSmithIntelligence(fitJob))
		{
			spdValue.color = Color.green;
		}
		else if (smithInfo.getSmithIntelligence() > smithInfo.fitSmithIntelligence(fitJob))
		{
			spdValue.color = Color.red;
		}
		else
		{
			spdValue.color = Color.black;
		}
		spdValue.text = smithInfo.fitSmithIntelligence(fitJob).ToString();
		if (smithInfo.getSmithTechnique() < smithInfo.fitSmithTechnique(fitJob))
		{
			accValue.color = Color.green;
		}
		else if (smithInfo.getSmithTechnique() > smithInfo.fitSmithTechnique(fitJob))
		{
			accValue.color = Color.red;
		}
		else
		{
			accValue.color = Color.black;
		}
		accValue.text = smithInfo.fitSmithTechnique(fitJob).ToString();
		if (smithInfo.getSmithLuck() < smithInfo.fitSmithLuck(fitJob))
		{
			magValue.color = Color.green;
		}
		else if (smithInfo.getSmithLuck() > smithInfo.fitSmithLuck(fitJob))
		{
			magValue.color = Color.red;
		}
		else
		{
			magValue.color = Color.black;
		}
		magValue.text = smithInfo.fitSmithLuck(fitJob).ToString();
	}

	private bool checkSmithActivity()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		List<Smith> inShopSmithList = player.getInShopSmithList();
		SmithAction smithAction = smithInfo.getSmithAction();
		if (smithAction.getRefId() == "905")
		{
			return true;
		}
		if (inShopSmithList.Count <= 1)
		{
			string textByRefId = gameData.getTextByRefId("menuSmithManagement05");
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, string.Empty, textByRefId, PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
			return false;
		}
		return true;
	}

	private bool checkSmithInShop(bool standby)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		List<Smith> inShopSmithList = player.getInShopSmithList();
		SmithAction smithAction = smithInfo.getSmithAction();
		if (inShopSmithList.Count <= 1)
		{
			string textByRefId = gameData.getTextByRefId("menuSmithManagement05");
			if (standby)
			{
				textByRefId = gameData.getTextByRefId("errorCommon04");
			}
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, string.Empty, textByRefId, PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
			return false;
		}
		return true;
	}

	public void fireSmith()
	{
		shopMenuController.doFireSmith(smithInfo);
		List<Smith> list = new List<Smith>();
		list.Add(smithInfo);
		List<Smith> smithList = game.getPlayer().getSmithList();
		GameObject.Find("Panel_SmithList").GetComponent<GUISmithListMenuController>().loadSmithList(smithList);
		GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>().fireSmiths(list);
		assignNoteLabel.text = string.Empty;
		animClickController.closeSmithActionMenu();
	}

	private void setButtonStates()
	{
		if (!isJobChange)
		{
			GameData gameData = game.getGameData();
			Player player = game.getPlayer();
			GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
			string gameLockSet = gameScenarioByRefId.getGameLockSet();
			int completedTutorialIndex = player.getCompletedTutorialIndex();
			bool flag = smithInfo.checkSmithInShopOrStandby();
			if (flag && gameData.checkFeatureIsUnlocked(gameLockSet, "MAP", completedTutorialIndex))
			{
				worldMapButton.isEnabled = true;
			}
			else
			{
				worldMapButton.isEnabled = false;
			}
			if (flag && gameData.checkFeatureIsUnlocked(gameLockSet, "SMITHHIRE", completedTutorialIndex))
			{
				fireButton.isEnabled = true;
			}
			else
			{
				fireButton.isEnabled = false;
			}
			if (gameData.checkFeatureIsUnlocked(gameLockSet, "JOBCHANGE", completedTutorialIndex))
			{
				shopMenuController.tryStartTutorial("JOB_CLASS");
				jobTreeButton.isEnabled = true;
			}
			else
			{
				jobTreeButton.isEnabled = false;
			}
			if (!smithInfo.checkSmithInShop())
			{
				standbyButton.isEnabled = false;
				standbyLabel.color = Color.gray;
			}
			else
			{
				standbyButton.isEnabled = true;
			}
			closeButton.isEnabled = true;
		}
	}

	public void revertButtonStates()
	{
		isJobChange = false;
		if (tutorial)
		{
			worldMapButton.isEnabled = false;
			standbyButton.isEnabled = false;
			fireButton.isEnabled = false;
			closeButton.isEnabled = false;
			jobTreeButton.isEnabled = false;
		}
		else
		{
			setButtonStates();
		}
	}

	public void setButtonStatesToJobChangeState()
	{
		isJobChange = true;
		worldMapButton.isEnabled = false;
		standbyButton.isEnabled = false;
		fireButton.isEnabled = false;
		closeButton.isEnabled = false;
		jobTreeButton.isEnabled = false;
	}

	private string getJobclassTooltipString()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		string empty = string.Empty;
		string text = empty;
		empty = text + smithInfo.getSmithJob().getSmithJobName() + " " + gameData.getTextByRefId("smithStatsShort01") + ". " + smithInfo.getSmithLevel() + " " + gameData.getTextByRefId("smithStatsShort08") + " " + smithInfo.getSmithExp() + "/" + smithInfo.getMaxExp() + "\n";
		empty += "[s]                         [/s]\n";
		empty = empty + gameData.getTextByRefId("assignSmith19") + "\n";
		SmithJobClass smithJob = smithInfo.getSmithJob();
		bool flag = true;
		if (smithJob.checkDesign())
		{
			if (flag)
			{
				flag = false;
			}
			else
			{
				empty += ", ";
			}
			empty = empty + "[FF4842]" + gameData.getTextByRefId("weaponStats06") + "[-]";
		}
		if (smithJob.checkCraft())
		{
			if (flag)
			{
				flag = false;
			}
			else
			{
				empty += ", ";
			}
			empty = empty + "[56AE59]" + gameData.getTextByRefId("weaponStats07") + "[-]";
		}
		if (smithJob.checkPolish())
		{
			if (flag)
			{
				flag = false;
			}
			else
			{
				empty += ", ";
			}
			empty = empty + "[00AAC7]" + gameData.getTextByRefId("weaponStats08") + "[-]";
		}
		if (smithJob.checkEnchant())
		{
			if (flag)
			{
				flag = false;
			}
			else
			{
				empty += ", ";
			}
			empty = empty + "[FFD84A]" + gameData.getTextByRefId("weaponStats09") + "[-]";
		}
		empty += "\n[s]                         [/s]\n";
		return empty + "[808080][i]" + smithJob.getSmithJobDesc() + "[/i][-]";
	}

	private string getExplorerTooltipString()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		string empty = string.Empty;
		string text = empty;
		empty = text + gameData.getTextByRefId("smithStats18") + " " + smithInfo.getExploreLevel() + "\n";
		empty += "[s]                         [/s]\n";
		return empty + gameData.getTextByRefId("assignSmith18");
	}

	private string getMerchantTooltipString()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		string empty = string.Empty;
		string text = empty;
		empty = text + gameData.getTextByRefId("smithStats17") + " " + smithInfo.getMerchantLevel() + "\n";
		empty += "[s]                         [/s]\n";
		return empty + gameData.getTextByRefId("assignSmith17");
	}

	private string getSalaryTooltipString()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		string empty = string.Empty;
		string text = empty;
		return text + gameData.getTextByRefId("assignSmith15") + ": [FF9000]" + CommonAPI.formatNumber(smithInfo.getCurrentSmithSalary()) + "[-]\n" + gameData.getTextByRefId("assignSmith16") + ": [FF9000]" + CommonAPI.formatNumber(smithInfo.getNextMonthSalary()) + "[-]";
	}

	private string getSmithEffectTooltipString()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		string text = string.Empty;
		List<string> smithStatusEffectListNoRepeat = smithInfo.getSmithStatusEffectListNoRepeat();
		if (smithStatusEffectListNoRepeat.Count > 0)
		{
			foreach (string item in smithStatusEffectListNoRepeat)
			{
				SmithStatusEffect smithStatusEffectByRefId = gameData.getSmithStatusEffectByRefId(item);
				text = text + smithStatusEffectByRefId.getEffectName() + "\n";
				text += smithStatusEffectByRefId.getEffectDesc();
			}
			return text;
		}
		return text;
	}

	private void checkNewClass()
	{
		int smithJobClassTier = CommonAPI.getSmithJobClassTier(smithInfo.getSmithJob().getSmithJobRefId());
		List<SmithJobClass> jobChangeList = game.getGameData().getJobChangeList(smithInfo.getExperienceList(), smithInfo.getSmithJob().getSmithJobRefId());
		foreach (SmithJobClass item in jobChangeList)
		{
			if (CommonAPI.getSmithJobClassTier(item.getSmithJobRefId()) > smithJobClassTier)
			{
				newClassBg.alpha = 1f;
				return;
			}
		}
		newClassBg.alpha = 0f;
	}
}
