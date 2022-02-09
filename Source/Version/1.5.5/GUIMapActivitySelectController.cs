using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIMapActivitySelectController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private GUISceneCameraController mapCamera;

	private ViewController viewController;

	private GUIMapController mapController;

	private UILabel regionName;

	private UILabel activitySubtitle;

	private UILabel nextRegionLabel;

	private UILabel nextRegionNameLabel;

	private UILabel activityTitle;

	private UILabel activityDesc;

	private UIButton buyMatsButton;

	private UILabel buyMatsLabel;

	private UIButton exploreButton;

	private UILabel exploreLabel;

	private UIButton sellWeaponButton;

	private UILabel sellWeaponLabel;

	private UIButton trainingButton;

	private UILabel trainingLabel;

	private UIButton unlockButton;

	private UILabel unlockLabel;

	private UIButton vacationButton;

	private UILabel vacationLabel;

	private BoxCollider nextRegionButton;

	private UIButton backToWorkshopButton;

	private Vector3 enlargedScale;

	private bool tutorial;

	private Color32 enabledColor;

	private Color32 disabledColor;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		mapCamera = GameObject.Find("NGUICameraMap").GetComponent<GUISceneCameraController>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		mapController = GameObject.Find("GUIMapController").GetComponent<GUIMapController>();
		regionName = commonScreenObject.findChild(base.gameObject, "RegionName").GetComponent<UILabel>();
		activitySubtitle = commonScreenObject.findChild(base.gameObject, "ActivitySubtitle").GetComponent<UILabel>();
		nextRegionLabel = commonScreenObject.findChild(base.gameObject, "NextRegionButton/NextRegionLabel").GetComponent<UILabel>();
		nextRegionNameLabel = commonScreenObject.findChild(base.gameObject, "NextRegionButton/NextRegionNameLabel").GetComponent<UILabel>();
		activityTitle = commonScreenObject.findChild(base.gameObject, "Frame/ActivityTitle").GetComponent<UILabel>();
		activityDesc = commonScreenObject.findChild(base.gameObject, "Frame/ActivityDesc").GetComponent<UILabel>();
		buyMatsButton = commonScreenObject.findChild(base.gameObject, "BuyMatsButton").GetComponent<UIButton>();
		buyMatsLabel = commonScreenObject.findChild(buyMatsButton.gameObject, "BuyMatsLabel").GetComponent<UILabel>();
		exploreButton = commonScreenObject.findChild(base.gameObject, "ExploreButton").GetComponent<UIButton>();
		exploreLabel = commonScreenObject.findChild(exploreButton.gameObject, "ExploreLabel").GetComponent<UILabel>();
		sellWeaponButton = commonScreenObject.findChild(base.gameObject, "SellWeaponButton").GetComponent<UIButton>();
		sellWeaponLabel = commonScreenObject.findChild(sellWeaponButton.gameObject, "SellWeaponLabel").GetComponent<UILabel>();
		trainingButton = commonScreenObject.findChild(base.gameObject, "TrainingButton").GetComponent<UIButton>();
		trainingLabel = commonScreenObject.findChild(trainingButton.gameObject, "TrainingLabel").GetComponent<UILabel>();
		unlockButton = commonScreenObject.findChild(base.gameObject, "UnlockButton").GetComponent<UIButton>();
		unlockLabel = commonScreenObject.findChild(unlockButton.gameObject, "UnlockLabel").GetComponent<UILabel>();
		vacationButton = commonScreenObject.findChild(base.gameObject, "VacationButton").GetComponent<UIButton>();
		vacationLabel = commonScreenObject.findChild(vacationButton.gameObject, "VacationLabel").GetComponent<UILabel>();
		nextRegionButton = commonScreenObject.findChild(base.gameObject, "NextRegionButton").GetComponent<BoxCollider>();
		backToWorkshopButton = commonScreenObject.findChild(base.gameObject, "BackToWorkshopButton").GetComponent<UIButton>();
		enlargedScale = new Vector3(1.2f, 1.2f, 1.2f);
		tutorial = false;
		enabledColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		disabledColor = new Color32(59, 109, 130, byte.MaxValue);
		mapCamera.setCameraEnabled(aCameraEnabled: false);
	}

	public void processClick(string gameObjectName)
	{
		GameObject gameObject = GameObject.Find("Panel_Tutorial");
		switch (gameObjectName)
		{
		case "BuyMatsButton":
			if (gameObject == null)
			{
				showActivity(ActivityType.ActivityTypeBuyMats);
			}
			break;
		case "ExploreButton":
			if (gameObject == null)
			{
				showActivity(ActivityType.ActivityTypeExplore);
			}
			break;
		case "SellWeaponButton":
		{
			bool flag = false;
			if (gameObject != null)
			{
				GUITutorialController component = gameObject.GetComponent<GUITutorialController>();
				flag = component.checkCurrentTutorial("30002");
				if (flag)
				{
					component.nextTutorial();
				}
			}
			if (flag || gameObject == null)
			{
				game.getPlayer().clearLastSelectProject();
				showActivity(ActivityType.ActivityTypeSellWeapon);
			}
			break;
		}
		case "TrainingButton":
			if (gameObject == null)
			{
				showActivity(ActivityType.ActivityTypeTraining);
			}
			break;
		case "VacationButton":
			if (gameObject == null)
			{
				showActivity(ActivityType.ActivityTypeVacation);
			}
			break;
		case "UnlockButton":
			if (gameObject == null)
			{
				showActivity(ActivityType.ActivityTypeUnlock);
			}
			break;
		case "NextRegionButton":
			if (gameObject == null)
			{
				upgradeWorkshop();
			}
			break;
		case "BackToWorkshopButton":
			if (gameObject == null)
			{
				mapController.processClick("Button_BackWorkshop");
			}
			break;
		}
	}

	public void processHover(bool isOver, string gameObjectName)
	{
		if (isOver)
		{
			GameData gameData = game.getGameData();
			switch (gameObjectName)
			{
			case "BuyMatsButton":
				buyMatsButton.transform.localScale = enlargedScale;
				activityTitle.text = gameData.getTextByRefId("mapText05").ToUpper(CultureInfo.InvariantCulture);
				activityDesc.text = gameData.getTextByRefId("mapText61");
				break;
			case "ExploreButton":
				exploreButton.transform.localScale = enlargedScale;
				activityTitle.text = gameData.getTextByRefId("mapText03").ToUpper(CultureInfo.InvariantCulture);
				activityDesc.text = gameData.getTextByRefId("mapText62");
				break;
			case "SellWeaponButton":
				sellWeaponButton.transform.localScale = enlargedScale;
				activityTitle.text = gameData.getTextByRefId("mapText04").ToUpper(CultureInfo.InvariantCulture);
				activityDesc.text = gameData.getTextByRefId("mapText63");
				break;
			case "TrainingButton":
				trainingButton.transform.localScale = enlargedScale;
				activityTitle.text = gameData.getTextByRefId("mapText08").ToUpper(CultureInfo.InvariantCulture);
				activityDesc.text = gameData.getTextByRefId("mapText64");
				break;
			case "VacationButton":
				vacationButton.transform.localScale = enlargedScale;
				activityTitle.text = gameData.getTextByRefId("mapText28").ToUpper(CultureInfo.InvariantCulture);
				activityDesc.text = gameData.getTextByRefId("mapText65");
				break;
			case "UnlockButton":
				unlockButton.transform.localScale = enlargedScale;
				activityTitle.text = gameData.getTextByRefId("mapText47").ToUpper(CultureInfo.InvariantCulture);
				activityDesc.text = gameData.getTextByRefId("mapText66");
				break;
			}
		}
		else
		{
			activityTitle.text = string.Empty;
			activityDesc.text = string.Empty;
			buyMatsButton.transform.localScale = Vector3.one;
			exploreButton.transform.localScale = Vector3.one;
			sellWeaponButton.transform.localScale = Vector3.one;
			trainingButton.transform.localScale = Vector3.one;
			vacationButton.transform.localScale = Vector3.one;
			unlockButton.transform.localScale = Vector3.one;
		}
	}

	private void Update()
	{
		if (viewController != null && viewController.getIsPaused() && viewController.getGameStarted() && backToWorkshopButton.isEnabled)
		{
			handleInput();
		}
	}

	private void handleInput()
	{
		if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(gameData.getKeyCodeByRefID("100007"))) && GameObject.Find("Panel_Tutorial") == null)
		{
			processClick("BackToWorkshopButton");
		}
	}

	public void setTutorialState(string aTutorial)
	{
		if (aTutorial == null)
		{
			return;
		}
		if (!(aTutorial == "SELL"))
		{
			if (aTutorial == string.Empty)
			{
				GameData gameData = game.getGameData();
				Player player = game.getPlayer();
				GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
				string gameLockSet = gameScenarioByRefId.getGameLockSet();
				int completedTutorialIndex = player.getCompletedTutorialIndex();
				string aFeature = "BUY";
				if (game.getPlayer().checkSkipTutorials())
				{
					aFeature = "BUY_SKIP";
				}
				if (gameData.checkFeatureIsUnlocked(gameLockSet, aFeature, completedTutorialIndex))
				{
					buyMatsButton.isEnabled = true;
				}
				else
				{
					buyMatsButton.isEnabled = false;
				}
				if (gameData.checkFeatureIsUnlocked(gameLockSet, "REGION", completedTutorialIndex))
				{
					unlockButton.isEnabled = true;
				}
				else
				{
					unlockButton.isEnabled = false;
				}
				if (gameData.checkFeatureIsUnlocked(gameLockSet, "EXPLORE", completedTutorialIndex))
				{
					exploreButton.isEnabled = true;
				}
				else
				{
					exploreButton.isEnabled = false;
				}
				if (gameData.checkFeatureIsUnlocked(gameLockSet, "VACATION", completedTutorialIndex))
				{
					vacationButton.isEnabled = true;
				}
				else
				{
					vacationButton.isEnabled = false;
				}
				if (gameData.checkFeatureIsUnlocked(gameLockSet, "TRAINING", completedTutorialIndex))
				{
					trainingButton.isEnabled = true;
				}
				else
				{
					trainingButton.isEnabled = false;
				}
				if (gameData.checkFeatureIsUnlocked(gameLockSet, "SELL", completedTutorialIndex))
				{
					sellWeaponButton.isEnabled = true;
				}
				else
				{
					sellWeaponButton.isEnabled = false;
				}
				activityTitle.text = string.Empty;
				activityDesc.text = string.Empty;
			}
		}
		else
		{
			buyMatsButton.isEnabled = false;
			exploreButton.isEnabled = false;
			trainingButton.isEnabled = false;
			unlockButton.isEnabled = false;
			vacationButton.isEnabled = false;
			nextRegionButton.enabled = false;
			CommonAPI.debug("setTutorialState nextRegionButton DISABLE");
			backToWorkshopButton.isEnabled = false;
			tutorial = true;
			activityTitle.text = string.Empty;
			activityDesc.text = string.Empty;
		}
	}

	public void setReference()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		int areaRegion = player.getAreaRegion();
		regionName.text = gameData.getAreaRegionByRefID(areaRegion).getRegionName();
		activitySubtitle.text = gameData.getTextByRefId("mapText60");
		AreaRegion areaRegionByRefID = gameData.getAreaRegionByRefID(areaRegion + 1);
		UISprite component = nextRegionButton.GetComponent<UISprite>();
		if (areaRegionByRefID.getAreaRegionRefID() == 0)
		{
			nextRegionButton.enabled = false;
			component.alpha = 0f;
		}
		else if (nextRegionUnlocked())
		{
			nextRegionButton.enabled = true;
			component.spriteName = "bt_unlocklocation-active";
			component.alpha = 1f;
			nextRegionLabel.color = enabledColor;
			nextRegionNameLabel.color = enabledColor;
		}
		else
		{
			nextRegionButton.enabled = false;
			component.alpha = 1f;
		}
		nextRegionLabel.text = gameData.getTextByRefId("mapText49");
		nextRegionNameLabel.text = areaRegionByRefID.getRegionName();
		backToWorkshopButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("mapText48");
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		if (completedTutorialIndex >= gameData.getTutorialSetOrderIndex("SELL1"))
		{
			GameObject gameObject = GameObject.Find("ShopMenuController");
			if (gameObject != null)
			{
				gameObject.GetComponent<ShopMenuController>().tryStartTutorial("SELL2", isMap: true);
			}
		}
		if (gameData.getObjectiveByRefId("1023").checkObjectiveEnded() && completedTutorialIndex >= gameData.getTutorialSetOrderIndex("JOB_CLASS"))
		{
			GameObject gameObject2 = GameObject.Find("ShopMenuController");
			if (gameObject2 != null)
			{
				gameObject2.GetComponent<ShopMenuController>().tryStartTutorial("TRAINING", isMap: true);
			}
		}
		GameObject gameObject3 = GameObject.Find("Panel_Tutorial");
		bool flag = false;
		if (gameObject3 != null)
		{
			flag = true;
		}
		if (!flag)
		{
			setTutorialState(string.Empty);
		}
	}

	private bool nextRegionUnlocked()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		int areaRegion = player.getAreaRegion();
		if ((areaRegion == 1 && gameData.checkFeatureIsUnlocked(gameLockSet, "REGION2UNLOCK", completedTutorialIndex)) || (areaRegion == 2 && gameData.checkFeatureIsUnlocked(gameLockSet, "REGION3UNLOCK", completedTutorialIndex)) || (areaRegion == 3 && gameData.checkFeatureIsUnlocked(gameLockSet, "REGION4UNLOCK", completedTutorialIndex)) || (areaRegion == 4 && gameData.checkFeatureIsUnlocked(gameLockSet, "REGION5UNLOCK", completedTutorialIndex)))
		{
			return true;
		}
		return false;
	}

	private void showActivity(ActivityType aType)
	{
		mapController.destroyArea();
		if (tutorial)
		{
			mapController.createArea(aType, tutorial);
		}
		else
		{
			mapController.createArea(aType);
		}
		mapCamera.setCameraEnabled(aCameraEnabled: true);
		viewController.destroyMapActivitySelect();
	}

	private void upgradeWorkshop()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		AreaRegion areaRegionByRefID = gameData.getAreaRegionByRefID(player.getAreaRegion() + 1);
		if (nextRegionUnlocked())
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			dictionary.Add(gameData.getTextByRefId("upgradeMenu05"), player.getHighestPlayerFurnitureByType("601").getFurnitureLevel());
			dictionary.Add(gameData.getTextByRefId("upgradeMenu06"), player.getHighestPlayerFurnitureByType("701").getFurnitureLevel());
			dictionary.Add(gameData.getTextByRefId("upgradeMenu07"), player.getHighestPlayerFurnitureByType("801").getFurnitureLevel());
			dictionary.Add(gameData.getTextByRefId("upgradeMenu08"), player.getHighestPlayerFurnitureByType("901").getFurnitureLevel());
			bool flag = true;
			string text = string.Empty;
			foreach (KeyValuePair<string, int> item in dictionary)
			{
				if (item.Value < areaRegionByRefID.getWorkstationLvl())
				{
					flag = false;
					text = ((!(text == string.Empty)) ? (text + ", " + item.Key) : (text + item.Key));
				}
			}
			if (!flag)
			{
				List<string> list = new List<string>();
				list.Add("[StationPhase]");
				list.Add("[StationLvl]");
				List<string> list2 = new List<string>();
				list2.Add(text);
				list2.Add(areaRegionByRefID.getWorkstationLvl().ToString());
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, string.Empty, gameData.getTextByRefIdWithDynTextList("mapText50", list, list2), PopupType.PopupTypeNothing, null, colorTag: false, null, map: true, string.Empty);
			}
			else
			{
				List<Smith> inShopOrStandbySmithList = player.getInShopOrStandbySmithList();
				List<Smith> smithList = player.getSmithList();
				if (inShopOrStandbySmithList.Count != smithList.Count)
				{
					viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, string.Empty, gameData.getTextByRefId("mapText92"), PopupType.PopupTypeNothing, null, colorTag: false, null, map: true, string.Empty);
				}
				else
				{
					viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeYesAndNo, resume: false, string.Empty, gameData.getTextByRefId("mapText51"), PopupType.PopupTypeUpgradeShop, null, colorTag: false, null, map: true, string.Empty);
				}
			}
		}
		else
		{
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, string.Empty, gameData.getTextByRefId("mapText52"), PopupType.PopupTypeNothing, null, colorTag: false, null, map: true, string.Empty);
		}
	}

	public void upgradeShop()
	{
		StartCoroutine("startUpgradeShop");
	}

	private IEnumerator startUpgradeShop()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		ShopLevel currShopLevel = player.getShopLevel();
		ShopLevel nextShopLevel = gameData.getShopLevel(currShopLevel.getNextShopRefId());
		player.upgradeShop(nextShopLevel);
		mapController.destroyArea(setCameraVariable: true);
		GameObject.Find("GUIGridController").GetComponent<GUIGridController>().createWorld(refresh: true);
		yield return new WaitForSeconds(0.1f);
		viewController.destroyMapActivitySelect();
		viewController.closeGeneralPopup(toResume: false, hide: true, resumeFromPlayerPause: true);
		GameObject.Find("LoadingMaskMap").GetComponent<LoadingScript>().startLoadingToBlack("BACKWORKSHOP");
	}
}
