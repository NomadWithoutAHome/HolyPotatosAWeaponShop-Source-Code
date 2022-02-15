using System.Collections.Generic;
using UnityEngine;

public class GUISmithListController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private TooltipTextScript tooltipScript;

	private Smith smith;

	private UISprite smithIconFrame;

	private UITexture smithImg;

	private UISprite smithActionProgress;

	private UISprite smithActionIcon;

	private UISprite smithAlertIcon;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		smithIconFrame = commonScreenObject.findChild(base.gameObject, "SmithIconFrame").GetComponent<UISprite>();
		smithImg = commonScreenObject.findChild(smithIconFrame.gameObject, "SmithImg").GetComponent<UITexture>();
		smithActionProgress = commonScreenObject.findChild(smithIconFrame.gameObject, "SmithProgress").GetComponent<UISprite>();
		smithActionIcon = commonScreenObject.findChild(smithIconFrame.gameObject, "SmithAction_icon").GetComponent<UISprite>();
		smithAlertIcon = commonScreenObject.findChild(smithIconFrame.gameObject, "SmithAlert_icon").GetComponent<UISprite>();
	}

	public void processClick(string gameObjectName)
	{
		switch (smith.getExploreState())
		{
		case SmithExploreState.SmithExploreStateExploreReturned:
			finishExploration(smith);
			break;
		case SmithExploreState.SmithExploreStateBuyMaterialReturned:
			finishBuyMaterial(smith);
			break;
		case SmithExploreState.SmithExploreStateOffersWaiting:
		{
			Area exploreArea = smith.getExploreArea();
			smith.nextExploreState();
			viewController.showOfferWeapon(smith);
			break;
		}
		case SmithExploreState.SmithExploreStateSellWeaponReturned:
			finishSellWeapon(smith);
			break;
		case SmithExploreState.SmithExploreStateVacationReturned:
			finishVacation(smith);
			break;
		case SmithExploreState.SmithExploreStateTrainingReturned:
			finishTraining(smith);
			break;
		default:
			viewController.showAssignSmithMenu(smith);
			break;
		}
	}

	public void processHover(bool isOver, string hoverName)
	{
		if (isOver)
		{
			if (hoverName != null)
			{
			}
			tooltipScript.showText(smith.getSmithStandardInfoString(showFullJobDetails: false));
			smithImg.depth = 8;
		}
		else
		{
			tooltipScript.setInactive();
			smithImg.depth = 4;
		}
	}

	public void setReference(Smith aSmithInfo)
	{
		smith = aSmithInfo;
		smithImg.mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/" + smith.getImage());
		refreshSmithActionProgress();
	}

	public void setReference()
	{
		smithIconFrame.spriteName = "button_hire";
		smithActionProgress.gameObject.SetActive(value: false);
	}

	private void setSmithActionIconSprite()
	{
		switch (smith.getSmithAction().getRefId())
		{
		case "901":
			smithActionIcon.spriteName = "smallicon_explore";
			smithActionIcon.color = Color.white;
			break;
		case "902":
			smithActionIcon.spriteName = "smallicon_buy";
			smithActionIcon.color = Color.white;
			break;
		case "903":
			smithActionIcon.spriteName = "smallicon_sell";
			smithActionIcon.color = Color.white;
			break;
		case "906":
			smithActionIcon.spriteName = "small_vacation";
			smithActionIcon.color = Color.white;
			break;
		case "905":
			smithActionIcon.spriteName = "smallicon_idle";
			smithActionIcon.color = Color.white;
			break;
		case "904":
			smithActionIcon.spriteName = "smallicon_research";
			smithActionIcon.color = Color.white;
			break;
		case "907":
			smithActionIcon.spriteName = "small_training";
			smithActionIcon.color = Color.white;
			break;
		default:
			smithActionIcon.spriteName = "star_2";
			smithActionIcon.color = Color.yellow;
			break;
		}
	}

	public void refreshSmithActionProgress()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		if (smith != null)
		{
			smith = player.getSmithByRefID(smith.getSmithRefId());
			if (smith.getExploreArea().getAreaRefId() != string.Empty)
			{
				smithActionProgress.color = new Color(40f / 51f, 0.172549024f, 0.2784314f);
				smithActionProgress.alpha = 0.75f;
				smithActionProgress.fillAmount = smith.calculateExploreProgressPercentage();
				setSmithActionIconSprite();
				smithActionIcon.alpha = 1f;
			}
			else if (smith.getSmithAction().getRefId() == "904")
			{
				float fillAmount = smith.calculateSmithActionProgressPercentage();
				smithActionProgress.color = new Color(0.168627456f, 0.239215687f, 66f / 85f);
				smithActionProgress.alpha = 0.75f;
				smithActionProgress.fillAmount = fillAmount;
				setSmithActionIconSprite();
				smithActionIcon.alpha = 1f;
			}
			else if (smith.getSmithAction().getRefId() == "905")
			{
				smithActionProgress.fillAmount = 0f;
				setSmithActionIconSprite();
				smithActionIcon.alpha = 1f;
			}
			else
			{
				smithActionProgress.fillAmount = 0f;
				smithActionIcon.alpha = 0f;
			}
		}
		SmithExploreState exploreState = smith.getExploreState();
		if (exploreState == SmithExploreState.SmithExploreStateBuyMaterialReturned || exploreState == SmithExploreState.SmithExploreStateExploreReturned || exploreState == SmithExploreState.SmithExploreStateSellWeaponReturned || exploreState == SmithExploreState.SmithExploreStateTrainingReturned || exploreState == SmithExploreState.SmithExploreStateVacationReturned || exploreState == SmithExploreState.SmithExploreStateOffersWaiting)
		{
			switch (exploreState)
			{
			case SmithExploreState.SmithExploreStateOffersWaiting:
				shopMenuController.tryStartTutorial("OFFER");
				break;
			case SmithExploreState.SmithExploreStateSellWeaponReturned:
				shopMenuController.tryStartTutorial("SELL_RESULT");
				break;
			}
			smithAlertIcon.alpha = 1f;
		}
		else
		{
			smithAlertIcon.alpha = 0f;
		}
	}

	public Smith getSmith()
	{
		return smith;
	}

	public bool checkIsSmith(Smith compareSmith)
	{
		if (compareSmith.getSmithRefId() == smith.getSmithRefId())
		{
			return true;
		}
		return false;
	}

	private void finishSellWeapon(Smith aSmith)
	{
		smith = aSmith;
		Area exploreArea = aSmith.getExploreArea();
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		List<string> exploreTask = aSmith.getExploreTask();
		string empty = string.Empty;
		bool flag = false;
		for (int i = 0; i < exploreTask.Count; i++)
		{
			Project completedProjectById = player.getCompletedProjectById(exploreTask[i]);
			Hero buyer = completedProjectById.getBuyer();
			if (buyer.getHeroRefId() != string.Empty)
			{
				flag = true;
			}
			else
			{
				completedProjectById.setProjectSaleState(ProjectSaleState.ProjectSaleStateInStock);
			}
		}
		if (flag)
		{
			viewController.showSellResult(aSmith);
		}
		else
		{
			string text = aSmith.tryAddStatusEffect();
			if (text != string.Empty)
			{
				gameData.addNewWhetsappMsg(aSmith.getSmithName(), text, "Image/Smith/Portraits/" + aSmith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
			}
			bool hasMoodLimit = false;
			if (!gameData.checkFeatureIsUnlocked(gameLockSet, "MOODLIMIT", completedTutorialIndex))
			{
				hasMoodLimit = true;
			}
			float aReduce = aSmith.getExploreArea().getMoodFactor() * 3f;
			SmithMood moodState = aSmith.getMoodState();
			aSmith.reduceSmithMood(aReduce, hasMoodLimit);
			SmithMood moodState2 = aSmith.getMoodState();
			if (moodState != moodState2)
			{
				string whetsappMoodString = CommonAPI.getWhetsappMoodString(moodState2);
				if (whetsappMoodString != string.Empty)
				{
					gameData.addNewWhetsappMsg(aSmith.getSmithName(), whetsappMoodString, "Image/Smith/Portraits/" + aSmith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
				}
			}
			aSmith.returnToShopStandby();
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: true, gameData.getTextByRefId("smithStatusPopup01"), gameData.getTextByRefIdWithDynText("smithStatusPopup02", "[smithName]", aSmith.getSmithName()) + text, PopupType.PopuptypeFinishActivity, null, colorTag: false, smith, map: false, string.Empty);
		}
		exploreArea.removeAreaSmithRefID(aSmith.getSmithRefId());
		GameObject.Find("Panel_ActivitySelect").GetComponent<GUIActivitySelectController>().refreshButtons();
	}

	private void finishBuyMaterial(Smith aSmith)
	{
		smith = aSmith;
		viewController.showBuyResult(aSmith);
	}

	private void finishVacation(Smith aSmith)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		smith = aSmith;
		Area exploreArea = aSmith.getExploreArea();
		Vacation vacation = aSmith.getVacation();
		string text = aSmith.tryAddStatusEffect();
		if (text != string.Empty)
		{
			gameData.addNewWhetsappMsg(aSmith.getSmithName(), text, "Image/Smith/Portraits/" + aSmith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
		}
		float vacationMoodAdd = vacation.getVacationMoodAdd();
		SmithMood moodState = aSmith.getMoodState();
		aSmith.addSmithMood(vacationMoodAdd);
		SmithMood moodState2 = aSmith.getMoodState();
		string whetsappMoodString = CommonAPI.getWhetsappMoodString(moodState2);
		if (whetsappMoodString != string.Empty)
		{
			gameData.addNewWhetsappMsg(aSmith.getSmithName(), whetsappMoodString, "Image/Smith/Portraits/" + aSmith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
		}
		string text2 = gameData.getTextByRefIdWithDynText("vacationText03", "[SmithName]", aSmith.getSmithName()) + "\n" + text;
		viewController.showVacationResult(aSmith, whetsappMoodString);
	}

	private void finishTraining(Smith aSmith)
	{
		smith = aSmith;
		viewController.showTrainingResult(aSmith);
	}

	private void finishExploration(Smith aSmith)
	{
		smith = aSmith;
		viewController.showExploreResult(aSmith);
	}
}
