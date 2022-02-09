using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIVacationResultController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private UILabel vacationTitle;

	private UITexture smithImage;

	private UILabel smithName;

	private UILabel smithComment;

	private UILabel moodTitle;

	private UILabel moodName;

	private UITexture moodIcon;

	private Smith smith;

	private Area taskArea;

	private Vacation repeatVacation;

	private UIButton repeatButton;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		vacationTitle = commonScreenObject.findChild(base.gameObject, "VacationResult_bg/VacationTitle_label").GetComponent<UILabel>();
		smithImage = commonScreenObject.findChild(base.gameObject, "VacationResult_bg/VacationPhoto_bg/VacationPhoto_texture").GetComponent<UITexture>();
		smithName = commonScreenObject.findChild(base.gameObject, "VacationResult_bg/VacationPhoto_bg/VacationSmith_label").GetComponent<UILabel>();
		smithComment = commonScreenObject.findChild(base.gameObject, "VacationResult_bg/VacationPhoto_bg/VacationSmithText_label").GetComponent<UILabel>();
		moodTitle = commonScreenObject.findChild(base.gameObject, "VacationResult_bg/Mood_bg/MoodTitle_label").GetComponent<UILabel>();
		moodName = commonScreenObject.findChild(base.gameObject, "VacationResult_bg/Mood_bg/Mood_label").GetComponent<UILabel>();
		moodIcon = commonScreenObject.findChild(base.gameObject, "VacationResult_bg/Mood_bg/MoodIcon_texture").GetComponent<UITexture>();
		repeatButton = commonScreenObject.findChild(base.gameObject, "VacationResult_bg/Repeat_button").GetComponent<UIButton>();
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "Close_button":
			smith.returnToShopStandby();
			viewController.closeVacationResult(smith);
			break;
		case "Repeat_button":
			smith.returnToShopStandby();
			if (sendSmithRepeatVacation())
			{
				viewController.closeVacationResult();
			}
			else
			{
				viewController.closeVacationResult(smith);
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
			case "MoodIcon_texture":
				tooltipScript.showText(CommonAPI.getMoodString(smith.getMoodState(), showDesc: true));
				break;
			case "Repeat_button":
				if (repeatVacation != null)
				{
					string textByRefIdWithDynText = gameData.getTextByRefIdWithDynText("vacationResult04", "[vacationName]", repeatVacation.getVacationName());
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

	public void setReference(Smith aSmith, string moodText)
	{
		smith = aSmith;
		taskArea = aSmith.getExploreArea();
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		Season seasonByMonth = CommonAPI.getSeasonByMonth(player.getSeasonIndex());
		VacationPackage vacationPackageByRefID = gameData.getVacationPackageByRefID(taskArea.getVacationPackageRefId());
		if (vacationPackageByRefID != null && vacationPackageByRefID.getVacationPackageRefID() != string.Empty)
		{
			repeatVacation = gameData.getVacationByRefId(vacationPackageByRefID.getVacationRefIDBySeason(seasonByMonth));
		}
		vacationTitle.text = gameData.getTextByRefId("vacationResult01").ToUpper(CultureInfo.InvariantCulture);
		moodTitle.text = gameData.getTextByRefId("vacationResult02");
		UILabel component = commonScreenObject.findChild(base.gameObject, "VacationResult_bg/Close_button/Close_label").GetComponent<UILabel>();
		component.text = gameData.getTextByRefId("menuGeneral04").ToUpper(CultureInfo.InvariantCulture);
		smithImage.mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/" + smith.getImage());
		smithName.text = smith.getSmithName();
		smithComment.text = moodText;
		taskArea.removeAreaSmithRefID(smith.getSmithRefId());
		SmithMood moodState = smith.getMoodState();
		moodName.text = CommonAPI.getMoodString(moodState, showDesc: false);
		moodIcon.mainTexture = commonScreenObject.loadTexture("Image/Mood/" + CommonAPI.getMoodIcon(moodState));
		audioController.playSmithMoodSfx(moodState);
		UILabel component2 = commonScreenObject.findChild(repeatButton.gameObject, "Repeat_label").GetComponent<UILabel>();
		UILabel component3 = commonScreenObject.findChild(repeatButton.gameObject, "RepeatCost_bg/RepeatCost_label").GetComponent<UILabel>();
		switch (checkRepeatVacationAvailable(taskArea, showPopup: false))
		{
		case "SUCCESS":
		{
			string text = (component2.text = gameData.getTextByRefId("vacationResult03"));
			component3.text = CommonAPI.formatNumber(repeatVacation.getVacationCost());
			repeatButton.isEnabled = true;
			break;
		}
		case "NO_STARCH":
			component2.text = gameData.getTextByRefId("errorCommon05");
			component3.text = CommonAPI.formatNumber(repeatVacation.getVacationCost());
			repeatButton.isEnabled = false;
			break;
		case "NO_VACATION":
		case "REGION_CHANGE":
		case "AREA_FULL":
			component2.text = gameData.getTextByRefId("exploreResult03");
			component3.text = "-";
			repeatButton.isEnabled = false;
			break;
		}
	}

	public bool sendSmithRepeatVacation()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		if (checkRepeatVacationAvailable(taskArea, showPopup: true) == "SUCCESS")
		{
			Season seasonByMonth = CommonAPI.getSeasonByMonth(player.getSeasonIndex());
			taskArea.addTimesVacation(1);
			int vacationCost = repeatVacation.getVacationCost();
			player.reduceGold(vacationCost, allowNegative: true);
			audioController.playPurchaseAudio();
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseVacation, string.Empty, -1 * vacationCost);
			SmithAction smithActionByRefId = gameData.getSmithActionByRefId("906");
			List<int> list = new List<int>();
			list.Add(taskArea.getTravelTime());
			list.Add(24);
			list.Add(taskArea.getTravelTime());
			list.Add(-1);
			List<SmithExploreState> list2 = new List<SmithExploreState>();
			list2.Add(SmithExploreState.SmithExploreStateTravelToVacation);
			list2.Add(SmithExploreState.SmithExploreStateVacation);
			list2.Add(SmithExploreState.SmithExploreStateVacationTravelHome);
			list2.Add(SmithExploreState.SmithExploreStateVacationReturned);
			List<AreaStatus> areaStatusListByAreaAndSeason = gameData.getAreaStatusListByAreaAndSeason(taskArea.getAreaRefId(), seasonByMonth);
			taskArea.addAreaSmithRefID(smith.getSmithRefId());
			smith.setSmithAction(smithActionByRefId, taskArea.getTravelTime() * 2 + 24);
			smith.setExploreStateList(list2, list);
			smith.setExploreArea(taskArea);
			smith.setVacation(repeatVacation);
			smith.setAreaStatusList(areaStatusListByAreaAndSeason);
			return true;
		}
		return false;
	}

	private string checkRepeatVacationAvailable(Area aArea, bool showPopup)
	{
		Player player = game.getPlayer();
		if (player.getAreaRegion() != aArea.getRegion())
		{
			return "REGION_CHANGE";
		}
		if (repeatVacation == null || repeatVacation.getVacationRefId() == string.Empty)
		{
			return "NO_VACATION";
		}
		if (repeatVacation.getVacationCost() > player.getPlayerGold())
		{
			return "NO_STARCH";
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
}
