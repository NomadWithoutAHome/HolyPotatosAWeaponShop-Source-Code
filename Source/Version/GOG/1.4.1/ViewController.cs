using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ShopViewController shopViewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private GUIObstacleController obstacleController;

	private StationController stationController;

	private GUICharacterAnimationController characterAnimationController;

	private GUISceneCameraController workshopCamController;

	private GUICameraController cameraController;

	private GUIMapController mapController;

	private GUISpeedController speedController;

	private GUIStyle style;

	private GUIStyle menuStyle;

	private GUIStyle feedStyle;

	private Texture blackBox;

	private GameObject nguiCameraGUI;

	private bool isPaused;

	private bool isPlayerPaused;

	private bool gameStarted;

	private GameObject blackmask;

	private GameObject blackmask_popup;

	private GameObject tutorialMask_popup;

	private GameObject mapBlackmask_popup;

	private GameObject mapBlackmask;

	private GameObject blackmaskScene;

	private GUIProgressHUDController progressHUDController;

	private List<GameObject> prevParticlesList;

	private void Awake()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
	}

	private void StartRedShell()
	{
	}

	private void Start()
	{
		StartRedShell();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		checkRescale();
		style = new GUIStyle();
		style.wordWrap = true;
		style.richText = true;
		style.clipping = TextClipping.Clip;
		style.fontSize = 14;
		menuStyle = new GUIStyle();
		menuStyle.wordWrap = true;
		menuStyle.richText = true;
		menuStyle.clipping = TextClipping.Clip;
		menuStyle.fontSize = 14;
		feedStyle = new GUIStyle();
		feedStyle.wordWrap = true;
		feedStyle.richText = true;
		feedStyle.clipping = TextClipping.Clip;
		feedStyle.fontSize = 13;
		blackBox = Resources.Load("Image/blackpixel") as Texture;
		showSplashScreen();
		nguiCameraGUI = GameObject.Find("NGUICameraGUI");
		isPaused = false;
		isPlayerPaused = false;
		gameStarted = false;
		obstacleController = GameObject.Find("GUIObstacleController").GetComponent<GUIObstacleController>();
		stationController = GameObject.Find("StationController").GetComponent<StationController>();
		characterAnimationController = GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>();
		workshopCamController = GameObject.Find("NGUICameraScene").GetComponent<GUISceneCameraController>();
		cameraController = GameObject.Find("GUICameraController").GetComponent<GUICameraController>();
		mapController = GameObject.Find("GUIMapController").GetComponent<GUIMapController>();
		setAssignSmithMenu();
		if (nguiCameraGUI != null)
		{
			progressHUDController = GameObject.Find("Grid_topRight").GetComponent<GUIProgressHUDController>();
		}
	}

	private void checkRescale()
	{
	}

	private void OnGUI()
	{
		int width = Screen.width;
		int height = Screen.height;
		int num = (int)((float)width * 0.7f);
		int num2 = width - num;
		int num3 = (int)((float)height * 0.9f);
		int num4 = height - num3;
		if (nguiCameraGUI == null)
		{
			GUI.DrawTexture(new Rect(0f, 0f, width, height), blackBox);
		}
		GUI.Label(new Rect(10f, num4 + 5, num - 15, num3 - 15), string.Empty, style);
		GUI.Label(new Rect(num + 5, num4 + 5, num2 - 15, num3 - 15), string.Empty, menuStyle);
		GUI.Label(new Rect(10f, 10f, width - 20, num4 - 15), CommonAPI.getNewsFeedTextOnGui(), feedStyle);
	}

	public void startLoading()
	{
		commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_LoadingProgress", "Prefab/Loading/Panel_LoadingProgress", Vector3.zero, Vector3.one, Vector3.zero);
		commonScreenObject.getController("GUILoadingController");
	}

	public void closeLoading()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_LoadingProgress"));
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("GUILoadingController"));
	}

	public void startGame()
	{
		GameObject.Find("RefDataController").GetComponent<RefDataController>().getRefDataFromFile();
		game = GameObject.Find("Game").GetComponent<Game>();
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
	}

	public void showSplashScreen()
	{
		commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_SplashScreen", "Prefab/Splash/Panel_SplashScreen", Vector3.zero, Vector3.one, Vector3.zero);
	}

	public void closeSplashScreen()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_SplashScreen"));
	}

	public void showLoadRef()
	{
		commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_LoadingLanguage", "Prefab/Loading/Panel_LoadingLanguage", Vector3.zero, Vector3.one, Vector3.zero);
	}

	public void closeLoadRef()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_LoadingLanguage"));
	}

	public void showStartScreen(bool firstTime)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_topRight"), "Panel_Version", "Prefab/StartScreen/Panel_Version", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponentInChildren<UILabel>().text = "V 1.1.4.1";
		GameObject gameObject2 = commonScreenObject.createPrefab(GameObject.Find("Anchor_topRight"), "Panel_LanguageButton", "Prefab/StartScreen/Panel_LanguageButton", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject2.GetComponent<GUILanguageButtonController>().setReference();
		GameObject gameObject3 = commonScreenObject.createPrefab(GameObject.Find("Anchor_topLeft"), "Panel_ScenarioSelect", "Prefab/StartScreen/Panel_ScenarioSelect", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject3.GetComponent<GUIScenarioSelectController>().setReference(firstTime);
		commonScreenObject.createPrefab(GameObject.Find("Anchor_bottom"), "Panel_StartScreenButtons", "Prefab/StartScreen/Panel_StartScreenButtons", Vector3.zero, Vector3.one, Vector3.zero);
		commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_StartScreen", "Prefab/StartScreen/Panel_StartScreen", Vector3.zero, Vector3.one, Vector3.zero);
		GameObject controller = commonScreenObject.getController("GUIStartScreenController");
		controller.GetComponent<GUIStartScreenController>().setReference();
		GameObject gameObject4 = commonScreenObject.createPrefab(GameObject.Find("Anchor_bottomRight"), "Panel_HPWIS", "Prefab/StartScreen/Panel_HPWIS", Vector3.zero, Vector3.one, Vector3.zero);
		GameObject.Find("HPWTHLogo/ButtonText").GetComponent<UILabel>().text = game.getGameData().getTextByRefId("hpwisbuttontext");
		if (firstTime)
		{
			controller.GetComponent<GUIStartScreenController>().disableButtons();
			showLanguageMenu();
		}
	}

	public void closeStartScreen()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_Version"));
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_LanguageButton"));
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_ScenarioSelect"));
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_StartScreenButtons"));
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_StartScreen"));
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_HPWIS"));
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("GUIStartScreenController"));
	}

	public void showShop()
	{
		commonScreenObject.getController("ShopMenuController");
		commonScreenObject.getController("ShopViewController");
		shopViewController = GameObject.Find("ShopViewController").GetComponent<ShopViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		GameObject.Find("Panel_BottomMenu").GetComponent<BottomMenuController>().loadButtons();
		cameraController.changeSceneCamera();
		GameObject gameObject = GameObject.Find("Panel_Whetsapp");
		if (gameObject != null)
		{
			gameObject.GetComponent<GUIWhetsappController>().setReference();
		}
	}

	public void showMainMenu(MenuState aMenuState)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_left"), "Panel_MainMenu", "Prefab/Menu/Panel_MainMenu", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIMainMenuController>().setReference(aMenuState);
		showBlackMask();
		pauseEverything(GameState.GameStateMenu);
	}

	public void closeMainMenu(bool resume)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_MainMenu"));
		if (resume)
		{
			hideBlackMask();
			resumeEverything();
		}
		else
		{
			reenableCameraForPlayerPause();
		}
	}

	public void showTier2Menu(MenuState aMenuState)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_left"), "Panel_Tier2Menu", "Prefab/Menu/Panel_Tier2Menu", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUITier2MenuController>().setReference(aMenuState);
		showBlackMask();
		pauseEverything(GameState.GameStateMenu);
	}

	public void closeTier2Menu(bool hide)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_Tier2Menu"));
		if (hide)
		{
			hideBlackMask();
		}
	}

	public void showSettingsMenu(bool start = false)
	{
		string aPath = "Prefab/Settings/Panel_SettingsNEW";
		if (start)
		{
			aPath = "Prefab/Settings/Panel_SettingsStart";
		}
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_Settings", aPath, Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUISettingsController>().setReference(start);
		if (!start)
		{
			showBlackMask();
			pauseEverything(GameState.GameStateMenu);
		}
	}

	public void closeSettingsMenu(bool hide = true, bool resume = true)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_Settings"));
		if (hide)
		{
			hideBlackMask();
		}
		if (!isPlayerPaused && resume)
		{
			resumeEverything();
		}
		else
		{
			reenableCameraForPlayerPause();
		}
	}

	public void showSettingsConfirm(bool start = false)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_SettingConfirm", "Prefab/Settings/Panel_SettingConfirm", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUISettingsConfirmController>().setReference(start);
		if (!start)
		{
			showBlackMaskPopUp();
			pauseEverything(GameState.GameStateMenu);
		}
		else
		{
			showBlackMaskStart();
		}
	}

	public void closeSettingsConfirm(bool start)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_SettingConfirm"));
		if (!start)
		{
			hideBlackMaskPopUp();
		}
		else
		{
			hideBlackMaskStart();
		}
	}

	public void showLanguageMenu(bool firstTime = false)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_LanguageSelect", "Prefab/Settings/Panel_LanguageSelect", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUILanguageController>().setReference(firstTime);
	}

	public void closeLanguageMenu()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_LanguageSelect"));
	}

	public void showForgeMenuNewPopup()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_ForgeWeaponMenu", "Prefab/ForgeMenu/ForgeMenuNEW DEC2014/Panel_ForgeWeaponMenu", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIForgeWeaponMenuController>().setReference();
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeForgeMenuNewPopup(bool resumeFromPlayerPause)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_ForgeWeaponMenu"));
		hideBlackMask();
		if (!isPlayerPaused || resumeFromPlayerPause)
		{
			resumeEverything();
		}
		else
		{
			reenableCameraForPlayerPause();
		}
	}

	public void showMenuForgeDirection()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_MenuForgeDirection", "Prefab/ForgeMenu/Panel_MenuForgeDirection", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIDirectionForgeController>().checkStats();
		showBlackMaskPopUp();
		pauseEverything(GameState.GameStateMenu);
	}

	public void closeMenuForgeDirection()
	{
		hideBlackMaskPopUp();
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_MenuForgeDirection"));
	}

	public void showMenuBlueprint(QuestNEW aBlueprint)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_MenuBlueprint", "Prefab/ForgeMenu/Panel_MenuBlueprint", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIMenuBlueprintController>().SetReference(aBlueprint);
		showBlackMask();
		pauseEverything(GameState.GameStateMenu);
	}

	public void closeMenuBlueprint(bool hide)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_MenuBlueprint"));
		if (hide)
		{
			hideBlackMask();
		}
	}

	public void showContract()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_Contract", "Prefab/Contract/Panel_Contract", Vector3.zero, new Vector3(0.7f, 0.7f, 0.7f), Vector3.zero);
		gameObject.GetComponent<GUIContractController>().setReference();
		showBlackMask();
		pauseEverything(GameState.GameStateMenu);
	}

	public void closeContract(bool hide, bool resumeFromPlayerPause)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_Contract"));
		if (hide)
		{
			hideBlackMask();
		}
		if (!isPlayerPaused || resumeFromPlayerPause)
		{
			resumeEverything();
		}
		else
		{
			reenableCameraForPlayerPause();
		}
	}

	public void showContractComplete(Contract contract)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_ContractComplete", "Prefab/Contract/Panel_ContractComplete", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIContractCompleteController>().setReference(contract);
		showBlackMask();
		pauseEverything(GameState.GameStateMenu);
	}

	public void closeContractComplete()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_ContractComplete"));
		hideBlackMask();
		resumeEverything();
	}

	public void showWeaponRequest(HeroRequest heroRequest)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_WeaponRequest", "Prefab/Request/Panel_WeaponRequest", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIWeaponRequestController>().setReference(heroRequest);
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeWeaponRequest()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_WeaponRequest"));
		hideBlackMask();
		resumeEverything();
	}

	public void showWeaponRequestSubmit(HeroRequest heroRequest)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_WeaponRequestSubmit", "Prefab/Request/Panel_WeaponRequestSubmit", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIRequestDeliveryController>().setReference(heroRequest);
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeWeaponRequestSubmit(bool resume)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_WeaponRequestSubmit"));
		if (resume)
		{
			hideBlackMask();
			if (!isPlayerPaused)
			{
				resumeEverything();
			}
			else
			{
				reenableCameraForPlayerPause();
			}
		}
	}

	public void showRequestResult(HeroRequest heroRequest)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_RequestResult", "Prefab/Request/Panel_RequestResult", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIRequestResultController>().setReference(heroRequest);
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeRequestResult()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_RequestResult"));
		hideBlackMask();
		if (!isPlayerPaused)
		{
			resumeEverything();
		}
		else
		{
			reenableCameraForPlayerPause();
		}
	}

	public void showLegendaryRequest(LegendaryHero aLegendary)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_LegendaryWeaponRequest", "Prefab/Legendary/Panel_LegendaryWeaponRequest", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUILegendaryRequestController>().setReference(aLegendary);
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeLegendaryRequest(bool resume)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_LegendaryWeaponRequest"));
		if (resume)
		{
			hideBlackMask();
			if (!isPlayerPaused)
			{
				resumeEverything();
			}
			else
			{
				reenableCameraForPlayerPause();
			}
		}
	}

	public void showShopPopup(PopupType aPopupType, Item prevItem = null)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_Shop", "Prefab/Shop/Panel_Shop", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIShopPopupController>().setReference(aPopupType, prevItem);
		showBlackMask();
		pauseEverything(GameState.GameStateMenu);
	}

	public void closeShopPopup(bool hide)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_Shop"));
		if (hide)
		{
			hideBlackMask();
		}
	}

	public void showQuestProgressPopup(Project project, Quest quest)
	{
		GameObject gameObject = GameObject.Find("Panel_QuestProgress");
		if (gameObject == null)
		{
			switch (quest.getQuestType())
			{
			case QuestType.QuestTypeNormal:
				gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_QuestProgress", "Prefab/Quest/Panel_QuestProgress", Vector3.zero, Vector3.one, Vector3.zero);
				break;
			case QuestType.QuestTypeInstant:
				gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_QuestProgress", "Prefab/Quest/Panel_QuestProgressInstant", Vector3.zero, Vector3.one, Vector3.zero);
				break;
			}
			gameObject.GetComponent<GUIQuestProgressController>().sendHeroOnQuest(project, quest);
		}
		showBlackMask();
		pauseEverything(GameState.GameStateMenu);
	}

	public void closeQuestProgressPopup()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_QuestProgress"));
		hideBlackMask();
	}

	public void showQuestResultPopup(QuestResultType aPopupType, List<Subquest> choiceList = null, Quest quest = null, Subquest subquest = null)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_QuestResult", "Prefab/Quest/Panel_QuestResult", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIQuestResultController>().setReference(aPopupType, choiceList, quest, subquest);
		showBlackMask();
		pauseEverything(GameState.GameStateMenu);
	}

	public void closeQuestResultPopup(bool hide, bool resume)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_QuestResult"));
		if (hide)
		{
			hideBlackMask();
		}
		if (resume)
		{
			resumeEverything();
		}
	}

	public void showResearch()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_ResearchNEW", "Prefab/Research/ResearchNEW/Panel_ResearchNEW2", Vector3.zero, new Vector3(0.7f, 0.7f, 0.7f), Vector3.zero);
		gameObject.GetComponent<GUIResearchNEW2Controller>().setReference();
		showBlackMask();
		pauseEverything(GameState.GameStateMenu);
	}

	public void closeResearch(bool hide, bool resume, bool resumeFromPlayerPause)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_ResearchNEW"));
		if (hide)
		{
			hideBlackMask();
		}
		if (resume && (!isPlayerPaused || resumeFromPlayerPause))
		{
			resumeEverything();
		}
		else
		{
			reenableCameraForPlayerPause();
		}
	}

	public void showResearchComplete(Smith aSmith)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_ResearchCompleteNEW", "Prefab/Research/ResearchNEW/Panel_ResearchCompleteNEW", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIResearchCompleteController>().setStats(aSmith);
		showBlackMask();
		pauseEverything(GameState.GameStateMenu);
	}

	public void closeResearchComplete(bool hide, bool resume)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_ResearchCompleteNEW"));
		if (hide)
		{
			hideBlackMask();
		}
		if (resume)
		{
			resumeEverything();
		}
	}

	public void showCollectionPopup()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_CollectionNEW", "Prefab/Collection/Panel_CollectionNEW", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIWeaponCollectionController>().setReference();
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeCollectionPopup()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_CollectionNEW"));
		hideBlackMask();
		if (!isPlayerPaused)
		{
			resumeEverything();
		}
		else
		{
			reenableCameraForPlayerPause();
		}
	}

	public void showShopRecordPopup()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_ShopStarchRecord", "Prefab/Shop/Panel_ShopStarchRecordNEW", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIShopStarchRecordController>().setReference();
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeShopRecordPopup()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_ShopStarchRecord"));
		hideBlackMask();
		if (!isPlayerPaused)
		{
			resumeEverything();
		}
		else
		{
			reenableCameraForPlayerPause();
		}
	}

	public void showJournal()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_Journal", "Prefab/Journal/Panel_Journal", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIJournalController>().setReference();
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeJournal()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_Journal"));
		hideBlackMask();
		if (!isPlayerPaused)
		{
			resumeEverything();
		}
		else
		{
			reenableCameraForPlayerPause();
		}
	}

	public void showDogTraining()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_DogTraining", "Prefab/Minigame/Panel_DogTraining", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIDogTrainingController>().setReference();
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeDogTraining()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_DogTraining"));
		hideBlackMask();
		if (!isPlayerPaused)
		{
			resumeEverything();
		}
		else
		{
			reenableCameraForPlayerPause();
		}
	}

	public void showGeneralPopup(GeneralPopupType type, bool resume = false, string displayTitle = null, string displayText = null, PopupType popupToClose = PopupType.PopupTypeNothing, Item aItem = null, bool colorTag = false, Smith aSmith = null, bool map = false, string yesLabel = "")
	{
		GameObject aParent = GameObject.Find("Anchor_centre");
		if (map)
		{
			aParent = GameObject.Find("Map_centre");
		}
		GameObject gameObject = commonScreenObject.createPrefab(aParent, "Panel_GeneralPopup", "Prefab/Popup/Panel_GeneralPopup", Vector3.zero, Vector3.one, Vector3.zero);
		if (popupToClose != PopupType.PopupTypeLoadFail)
		{
			pauseEverything(GameState.GameStatePopEvent);
			if (map)
			{
				showMapBlackMaskPopUp();
			}
			else
			{
				showBlackMaskPopUp();
			}
		}
		else
		{
			showBlackMaskPopUp();
			GameObject gameObject2 = GameObject.Find("panel_blackmask_popup");
			gameObject2.GetComponent<UIPanel>().depth = 200;
		}
		gameObject.GetComponent<GUIGeneralPopupController>().showPopup(type, resume, displayTitle, displayText, popupToClose, aItem, colorTag, aSmith);
	}

	public void showUnlockAreaPopup(Area aArea)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Map_centre"), "Panel_UnlockAreaPopup", "Prefab/Popup/Panel_UnlockAreaPopup", Vector3.zero, Vector3.one, Vector3.zero);
		pauseEverything(GameState.GameStatePopEvent);
		gameObject.GetComponent<GUIMapUnlockAreaPopupController>().showUnlockAreaPopup(aArea);
		showMapBlackMask();
	}

	public void closeUnlockAreaPopup(bool resume)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_UnlockAreaPopup"));
		hideMapBlackMask();
		if (resume)
		{
			resumeEverything();
		}
	}

	public void showGeneralDialoguePopup(GeneralPopupType type, bool resume = false, string displayTitle = "", string displayText = "", string displayDialogue = "", string displayImagePath = "", PopupType popupToClose = PopupType.PopupTypeNothing, bool colorTag = false)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_GeneralPopup", "Prefab/Popup/Panel_GeneralDialoguePopup", Vector3.zero, Vector3.one, Vector3.zero);
		pauseEverything(GameState.GameStatePopEvent);
		showBlackMaskPopUp();
		gameObject.GetComponent<GUIGeneralPopupController>().showDialoguePopup(type, resume, displayTitle, displayText, displayDialogue, displayImagePath, popupToClose, colorTag);
	}

	public void closeGeneralPopup(bool toResume, bool hide, bool resumeFromPlayerPause)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_GeneralPopup"));
		if (hide)
		{
			hideBlackMaskPopUp();
			hideMapBlackMaskPopUp();
		}
		if (toResume && (!isPlayerPaused || resumeFromPlayerPause))
		{
			resumeEverything();
		}
		else
		{
			reenableCameraForPlayerPause();
		}
	}

	public void queueItemGetPopup(string aTitle, string aImage, string aText)
	{
		GameObject gameObject = GameObject.Find("Panel_GeneralItemGet");
		if (gameObject != null)
		{
			gameObject.GetComponent<GUIItemGetPopupController>().addItemGet(aTitle, aImage, aText);
		}
	}

	public void queueDogItemGetPopup(string aTitle, string aImage, string aText)
	{
		GameObject gameObject = GameObject.Find("Panel_GeneralItemGet");
		if (gameObject != null)
		{
			gameObject.GetComponent<GUIItemGetPopupController>().addDogItemGet(aTitle, aImage, aText);
		}
	}

	public void clearItemGetQueue()
	{
		GameObject gameObject = GameObject.Find("Panel_GeneralItemGet");
		if (gameObject != null)
		{
			gameObject.GetComponent<GUIItemGetPopupController>().clearItemGetQueue();
		}
	}

	public void queueObjectiveCompletePopup(string aText)
	{
		GameObject gameObject = GameObject.Find("Panel_ObjectiveComplete");
		if (gameObject != null)
		{
			gameObject.GetComponent<GUIObjectiveCompletePopupController>().addObjectiveComplete(aText);
		}
	}

	public void clearObjectiveCompleteQueue()
	{
		GameObject gameObject = GameObject.Find("Panel_ObjectiveComplete");
		if (gameObject != null)
		{
			gameObject.GetComponent<GUIObjectiveCompletePopupController>().clearObjectiveCompleteQueue();
		}
	}

	public void showTutorialPopup(string aSetRefId, bool doPause, bool doTutorialMask, bool isMap)
	{
		string text = "Anchor_centre";
		if (isMap)
		{
			text = "Map_centre";
		}
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find(text), "Panel_Tutorial", "Prefab/Tutorial/Panel_Tutorial", Vector3.zero, Vector3.one, Vector3.zero);
		if (doTutorialMask)
		{
			showTutorialMaskPopUp();
		}
		if (doPause)
		{
			pauseEverything(GameState.GameStatePopEvent);
		}
		gameObject.GetComponent<GUITutorialController>().setReference(aSetRefId);
	}

	public void closeTutorialPopup(bool toResume, bool hideTutorialMask = true)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_Tutorial"));
		if (hideTutorialMask)
		{
			hideTutorialMaskPopUp();
			hideMapBlackMask();
		}
		if (toResume)
		{
			resumeEverything();
		}
	}

	public void showSaveLoadPopup(bool save, string aScenarioRefID, bool fromStart = true)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_SaveLoadPopup", "Prefab/SaveLoad/Panel_SaveLoadPopup", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUISaveLoadPopupController>().setReference(save, aScenarioRefID, fromStart);
	}

	public void closeSaveLoadPopup()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_SaveLoadPopup"));
	}

	public void showKeyShortcutPopup(bool start)
	{
		string text = "Panel_KeyboardShortcut";
		if (start)
		{
			text = "Panel_KeyboardShortcutStart";
		}
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_KeyboardShortcut", "Prefab/KeyboardShortcut/" + text, Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIKeyShortcutController>().setReference(start);
	}

	public void closeKeyShortcutPopup()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_KeyboardShortcut"));
	}

	public void showKeyPopup(bool start, string aText, bool confirmReset)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_KeyboardPopup", "Prefab/KeyboardShortcut/Panel_KeyboardPopup", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIKeyPopup>().setReference(start, aText, confirmReset);
	}

	public void closeKeyPopup()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_KeyboardPopup"));
	}

	public void showTextureSequencePopup(List<Hashtable> aTextureList, bool isPureBlackMask = false, float blackMaskFadeTime = 0.1f)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_TextureSequencePopup", "Prefab/Popup/Panel_TextureSequencePopup", Vector3.zero, Vector3.one, Vector3.zero);
		pauseEverything(GameState.GameStatePopEvent);
		if (isPureBlackMask)
		{
			showPureBlackMask(blackMaskFadeTime);
		}
		else
		{
			showBlackMaskPopUp();
		}
		gameObject.GetComponent<GUITextureSequencePopupController>().setTextureList(aTextureList);
	}

	public void closeTextureSequencePopup(bool hideBlackMask, bool doResume)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_TextureSequencePopup"));
		if (hideBlackMask)
		{
			hideBlackMaskPopUp();
			hidePureBlackMask();
		}
		if (doResume)
		{
			resumeEverything();
		}
	}

	public void showGoldenHammerAwards()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_GoldenHammerAwards", "Prefab/GoldenHammer/Panel_GoldenHammerAwards", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIGoldenHammerAwardsController>().setReference();
		showPureBlackMask(0.1f);
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeGoldenHammerAwards(bool resumeGame)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_GoldenHammerAwards"));
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().showTimeStatus();
		if (resumeGame)
		{
			hidePureBlackMask();
			resumeEverything();
		}
	}

	public void showGoldenHammerInvite()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_GoldenHammerInvite", "Prefab/GoldenHammer/Panel_GoldenHammerInvite", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIGoldenHammerInviteController>().setReference();
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeGoldenHammerInvite()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_GoldenHammerInvite"));
		hideBlackMask();
		resumeEverything();
	}

	public void showGoldenHammerPreEvent()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_GoldenHammerPreEvent", "Prefab/GoldenHammer/Panel_GoldenHammerPreEvent", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIPreGoldenHammerController>().setReference();
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeGoldenHammerPreEvent(bool resumeGame)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_GoldenHammerPreEvent"));
		if (resumeGame)
		{
			hideBlackMask();
			resumeEverything();
		}
	}

	public void showGoldenHammerResults(List<ProjectAchievement> aWinList, List<int> aWinPrizeList)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_GoldenHammerResults", "Prefab/GoldenHammer/Panel_GoldenHammerResults", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIGoldenHammerResultsController>().setReference(aWinList, aWinPrizeList);
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeGoldenHammerResults(bool resumeGame)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_GoldenHammerResults"));
		if (resumeGame)
		{
			hideBlackMask();
			resumeEverything();
		}
	}

	public void showEventPopup(List<Dialogue> dialogueList, List<string> keyList, List<string> valueList, PopupType aPopupType)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_EventPopup", "Prefab/Event/Panel_EventPopup", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIEventPopupController>().setReference(dialogueList, keyList, valueList, aPopupType);
		showBlackMask();
		pauseEverything(GameState.GameStateCutscene);
	}

	public void closeEventPopup()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_EventPopup"));
		hideBlackMask();
		resumeEverything();
	}

	public void showEventSelectPopup(EventSelectType aPopupType, string resultString, List<string> scenarioText, List<Subquest> subquestList = null)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_EventSelect", "Prefab/Event/Panel_EventSelect", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIEventSelectController>().setReference(aPopupType, resultString, scenarioText, subquestList);
		showBlackMask();
		pauseEverything(GameState.GameStateCutscene);
	}

	public void closeEventSelectPopup()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_EventSelect"));
		hideBlackMask();
	}

	public void showForgeIncidentPopup(ForgeIncident aIncident)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_ForgeIncident", "Prefab/ForgeIncident/Panel_ForgeIncident", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIForgeIncidentController>().setIncident(aIncident);
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeForgeIncidentPopup()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_ForgeIncident"));
		hideBlackMask();
		resumeEverything();
	}

	public void showRandomScenarioPopup(DayEndScenario aScenario)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_RandomScenario", "Prefab/RandomScenario/Panel_RandomScenario", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIRandomScenarioController>().setScenario(aScenario);
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeRandomScenarioPopup()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_RandomScenario"));
		hideBlackMask();
		resumeEverything();
	}

	public void showPayDayPopup()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_PayDay", "Prefab/PayDay/Panel_PayDay", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIPayDayController>().setReference();
		showBlackMask();
		pauseEverything(GameState.GameStateCutscene);
	}

	public void closePayDayPopup(bool resume = true)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_PayDay"));
		if (resume)
		{
			hideBlackMask();
			resumeEverything();
			shopMenuController.checkStartOfDayActionList();
		}
	}

	public void showBetaComplete()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_BetaComplete", "Prefab/BetaComplete/Panel_BetaComplete", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIBetaCompleteController>().setReference();
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeBetaComplete()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_BetaComplete"));
	}

	public void showAreaEventPopup(Area aArea, AreaEvent aEvent)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_AreaEvent", "Prefab/AreaEvent/Panel_AreaEvent", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIAreaEventController>().setReference(aArea, aEvent);
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeAreaEventPopup()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_AreaEvent"));
		hideBlackMask();
		resumeEverything();
	}

	public void showGameOver(string imagePath, string labelText)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_GameOver", "Prefab/GameOver/Panel_GameOver", Vector3.zero, Vector3.one, Vector3.zero);
		showPureBlackMask(1f);
		gameObject.GetComponent<GUIGameOverController>().setReference(imagePath, labelText);
	}

	public void closeGameOver()
	{
		hidePureBlackMask();
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_GameOver"));
	}

	public void showProjectProgress()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_topRight"), "Panel_ForgeProgressNew", "Prefab/ForgeMenu/ForgeMenuNEW DEC2014/Panel_ForgeProgressNew", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIForgeProgressNewController>().setReference();
	}

	public void closeProjectProgress()
	{
		commonScreenObject.clearTooltips();
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_ForgeProgressNew"));
	}

	public void showForgeProgress()
	{
		progressHUDController.setForgeProgressObject();
	}

	public void closeForgeProgress()
	{
		progressHUDController.destroyForgeProgressObject();
	}

	public void showQuestProgress()
	{
		progressHUDController.setQuestProgressObject();
	}

	public void closeQuestProgress(Project aProject)
	{
		progressHUDController.destroyQuestProgressObject(aProject);
	}

	public void showSelectSmithNewPopup(SmithSelectPopupType aPopupType)
	{
		Vector3 one = Vector3.one;
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_SelectSmith", "Prefab/SmithSelect/Panel_SelectSmith", Vector3.zero, one, Vector3.zero);
		gameObject.GetComponent<GUISelectSmithController>().setReference(aPopupType);
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeSelectSmithNewPopup(bool doResume)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_SelectSmith"));
		if (doResume)
		{
			hideBlackMask();
			if (!isPlayerPaused)
			{
				resumeEverything();
			}
			else
			{
				reenableCameraForPlayerPause();
			}
		}
	}

	public void showRenamePopup(PopupType aPopupType)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_RenamePopup", "Prefab/Popup/Panel_RenamePopup", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIRenamePopupController>().setReference(aPopupType);
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeRenamePopup(bool hide)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_RenamePopup"));
		if (hide)
		{
			hideBlackMask();
		}
	}

	public void showForgeComplete()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_ForgeComplete", "Prefab/ForgeMenu/Panel_ForgeComplete", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIForgeCompleteController>().setStats();
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeForgeComplete(bool hide, bool resume)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_ForgeComplete"));
		if (hide)
		{
			hideBlackMask();
		}
		if (resume)
		{
			resumeEverything();
		}
	}

	public void showWeaponComplete()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_WeaponCompleteNEW", "Prefab/ForgeMenu/ForgeMenuNEW/Panel_WeaponCompleteNEW", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIWeaponCompleteController>().setStats();
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeWeaponComplete(bool hide, bool resume)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_WeaponCompleteNEW"));
		if (hide)
		{
			hideBlackMask();
		}
		if (resume)
		{
			resumeEverything();
		}
	}

	public void showLegendaryComplete()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_LegendaryComplete", "Prefab/Legendary/Panel_LegendaryComplete", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUILegendaryCompleteController>().setStats();
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeLegendaryComplete(bool hide, bool resume)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_LegendaryComplete"));
		if (hide)
		{
			hideBlackMask();
		}
		if (resume)
		{
			resumeEverything();
		}
	}

	public void showLegendarySuccess(LegendaryHero aLegendary)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_LegendarySuccess", "Prefab/Legendary/Panel_LegendarySuccess", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUILegendarySuccessController>().setReference(aLegendary);
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeLegendarySuccess(bool hide, bool resume)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_LegendarySuccess"));
		if (hide)
		{
			hideBlackMask();
		}
		if (resume)
		{
			resumeEverything();
		}
	}

	public void showQuestComplete(Project aProject, int questGold, Dictionary<string, RewardChestItem> chestItemList)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_QuestComplete", "Prefab/Quest/QuestNEW/Panel_QuestComplete", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIQuestCompleteController>().setReference(aProject, questGold, chestItemList);
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeQuestComplete()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_QuestComplete"));
	}

	public void showOpenTreasure(Dictionary<string, RewardChestItem> chestItemList)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_OpenTreasure", "Prefab/Quest/Treasure/Panel_OpenTreasure", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIOpenTreasureController>().setReference(chestItemList);
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeOpenTreasure()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_OpenTreasure"));
		hideBlackMask();
		resumeEverything();
	}

	public void showForgeAppraisal()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_ForgeAppraisal", "Prefab/ForgeMenu/Panel_ForgeAppraisal", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIForgeAppraisalController>().setStats();
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeForgeAppraisal()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_ForgeAppraisal"));
	}

	public void showSelectQuestPopup()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_QuestSelect", "Prefab/Quest/Panel_QuestSelect", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIQuestSelectController>().setStats();
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeSelectQuestPopup()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_QuestSelect"));
	}

	public void showBoostPopup(ProcessPopupType aType, Smith selectedSmith, string afterBoostText, List<int> projectBefore, List<int> projectAfter, List<Boost> boostList, bool hasLevelUp, Boost enchantBoost = null, Item enchantItem = null)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_Boost", "Prefab/ForgeMenu/ForgeMenuNEW DEC2014/Panel_Boost", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIBoostController>().showPopup(aType, selectedSmith, afterBoostText, projectBefore, projectAfter, boostList, hasLevelUp, enchantBoost, enchantItem);
		pauseEverything(GameState.GameStatePopEvent);
		showBlackMask();
		GameObject.Find("Panel_ForgeProgressNew").GetComponent<GUIForgeProgressNewController>().moveFront();
	}

	public void closeBoostPopup()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_Boost"));
		hideBlackMask();
		GameObject.Find("Panel_ForgeProgressNew").GetComponent<GUIForgeProgressNewController>().moveBack();
	}

	public void showEnchantmentSelectNew()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_EnchantmentSelectNEW", "Prefab/ForgeMenu/ForgeMenuNEW/Panel_EnchantmentSelectNEW", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIEnchantmentSelectNewController>().setReference();
		pauseEverything(GameState.GameStatePopEvent);
		showBlackMask();
	}

	public void closeEnchantmentSelectNew()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_EnchantmentSelectNEW"));
	}

	public void showAddEnchantmentPopup(List<int> projectBefore, List<int> projectAfter, Weapon displayWeapon, Boost enchantBoost, Item enchantItem)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_EnchantItem", "Prefab/ForgeMenu/ForgeMenuNEW DEC2014/Panel_EnchantItem", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIAddEnchantmentController>().showPopup(projectBefore, projectAfter, displayWeapon, enchantBoost, enchantItem);
		pauseEverything(GameState.GameStatePopEvent);
		showBlackMask();
		GameObject.Find("Panel_ForgeProgressNew").GetComponent<GUIForgeProgressNewController>().moveFront();
	}

	public void closeAddEnchantmentPopup()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_EnchantItem"));
		hideBlackMask();
		GameObject.Find("Panel_ForgeProgressNew").GetComponent<GUIForgeProgressNewController>().moveBack();
	}

	public void showSmithManage(PopupType aPopupType, int currentSmithIndex = -1)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_SmithManage", "Prefab/SmithManage/Panel_SmithManage", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUISmithManageController>().setReference(aPopupType, currentSmithIndex);
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeSmithManagePopup(bool hide)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_SmithManage"));
		if (hide)
		{
			hideBlackMask();
		}
	}

	public void showSmithManagePopup(Smith aSmith)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_SmithManagePopup", "Prefab/SmithManage/Panel_SmithManagePopup", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUISmithManageNewController>().setReference(aSmith);
		showTransparentMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeSmithManagePopupNew(bool resume = true)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_SmithManagePopup"));
		if (resume)
		{
			resumeEverything();
		}
		hideTransparentMask();
	}

	public void showHireResult()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_SmithHireResult", "Prefab/SmithManage/Panel_SmithHireResultNEW", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUISmithHireResultController>().setReference();
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeHireResult(bool hide)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_SmithHireResult"));
		if (hide)
		{
			hideBlackMask();
		}
	}

	public void showHireFire()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_FireToHire", "Prefab/SmithManage/Panel_FireToHire", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIFireToHireController>().setReference();
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeFireHire(bool hide)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_FireToHire"));
		if (hide)
		{
			hideBlackMask();
		}
	}

	public void showTrainCompletePopup(string displayText, int aIndex)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_TrainComplete", "Prefab/SmithManage/Panel_TrainComplete", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUITrainCompletePopupController>().showPopup(displayText, aIndex);
		showBlackMaskPopUp();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeTrainCompletePopup()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_TrainComplete"));
		hideBlackMaskPopUp();
	}

	public void showSmithTrainingPopup(Smith trainSmith)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_Training", "Prefab/SmithManage/Panel_Training", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUITrainingController>().setReference(trainSmith);
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeSmithTrainingPopup(bool hide)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_Training"));
		if (hide)
		{
			hideBlackMask();
		}
	}

	public void showSmithJobChange(Smith aSmith)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_SmithJobChange", "Prefab/ChangeJob/Panel_SmithJobChange", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUISmithJobChangeController>().setReference(aSmith);
		GameObject gameObject2 = GameObject.Find("Panel_AssignSmithHUD");
		if (gameObject2 != null)
		{
			GUIAssignSmithHUDController component = gameObject2.GetComponent<GUIAssignSmithHUDController>();
			component.changeDepth(11);
			component.setButtonStatesToJobChangeState();
		}
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeSmithJobChange()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_SmithJobChange"));
		GameObject gameObject = GameObject.Find("Panel_AssignSmithHUD");
		if (gameObject != null)
		{
			GUIAssignSmithHUDController component = gameObject.GetComponent<GUIAssignSmithHUDController>();
			component.revertDepth();
			component.revertButtonStates();
		}
		hideBlackMask();
	}

	public void showInventoryPopup()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_Inventory", "Prefab/Inventory/Panel_Inventory", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIInventoryController>().setReference();
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeInventoryPopup(bool doResume)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_Inventory"));
		if (doResume)
		{
			hideBlackMask();
			if (!isPlayerPaused)
			{
				resumeEverything();
			}
			else
			{
				reenableCameraForPlayerPause();
			}
		}
	}

	public void showInventoryDragDropPopup()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_Inventory", "Prefab/InventoryDragDrop/Panel_Inventory", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIInventoryController>().setReference();
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeInventoryDragDropPopup(bool doResume)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_Inventory"));
		if (doResume)
		{
			hideBlackMask();
			if (!isPlayerPaused)
			{
				resumeEverything();
			}
			else
			{
				reenableCameraForPlayerPause();
			}
		}
	}

	public void showDialoguePopup(string aSetId, Dictionary<string, DialogueNEW> aDialogueList, PopupType aType = PopupType.PopupTypeNothing)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_Dialogue", "Prefab/Dialogue/Panel_Dialogue", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIDialogueController>().setReference(aSetId, aDialogueList, aType);
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeDialoguePopup(bool hideMask, bool doResume)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_Dialogue"));
		if (hideMask)
		{
			hideBlackMask();
		}
		if (doResume)
		{
			resumeEverything();
		}
	}

	public void showCutsceneDialog(CutsceneDialogue aDialogue, bool isNextDialoguePresent)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Cutscene_bottom"), "Panel_CutsceneDialogue", "Prefab/Cutscene/Panel_CutsceneDialogue", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUICutsceneDialogController>().setDialogue(aDialogue, isNextDialoguePresent);
	}

	public void closeCutsceneDialog()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_CutsceneDialogue"));
	}

	private void switchBGM(string bgmToSwitch)
	{
		if (audioController == null)
		{
			GameObject gameObject = GameObject.Find("AudioController");
			if (gameObject != null)
			{
				audioController = gameObject.GetComponent<AudioController>();
			}
		}
		if (audioController != null)
		{
			audioController.switchBGM(bgmToSwitch);
		}
	}

	public void showActivitySelect()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Map_centre"), "Panel_MapActivitySelect", "Prefab/Area/Panel_MapActivitySelect", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIMapActivitySelectController>().setReference();
		showMapBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void showWorldMap(ActivityType aActType = ActivityType.ActivityTypeBlank, Smith aSmith = null, bool tutorial = false)
	{
		commonScreenObject.clearTooltips();
		switchBGM("worldmap");
		cameraController.changeMapCamera();
		audioController.stopWeatherAudio();
		mapController.setReference();
		mapController.setPlayerDetails();
		switch (aActType)
		{
		case ActivityType.ActivityTypeBlank:
		{
			GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Map_centre"), "Panel_MapActivitySelect", "Prefab/Area/Panel_MapActivitySelect", Vector3.zero, Vector3.one, Vector3.zero);
			gameObject.GetComponent<GUIMapActivitySelectController>().setReference();
			if (aSmith != null)
			{
				mapController.setPreselectedSmith(aSmith);
				mapController.setIsFromAssign();
			}
			showMapBlackMask();
			break;
		}
		case ActivityType.ActivityTypeSellWeapon:
			mapController.createArea(ActivityType.ActivityTypeSellWeapon);
			if (aSmith != null)
			{
				mapController.setPreselectedSmith(aSmith);
				mapController.setIsFromAssign();
			}
			break;
		}
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeWorldMap(bool hide, bool resume, bool resumeFromPlayerPause)
	{
		commonScreenObject.clearTooltips();
		Season seasonByMonth = CommonAPI.getSeasonByMonth(game.getPlayer().getSeasonIndex());
		switchBGM(CommonAPI.getSeasonBGM(seasonByMonth));
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_MapActivitySelect"));
		mapController.destroyArea();
		mapController.clearPreselectedSmith();
		cameraController.changeSceneCamera();
		audioController.playWeatherAudio(game.getPlayer().getWeather().getWeatherRefId());
		if (shopMenuController == null)
		{
			shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		}
		shopMenuController.tryStartTutorial("SELL3");
		if (hide)
		{
			hideMapBlackMask();
		}
		if (resume && (!isPlayerPaused || resumeFromPlayerPause))
		{
			resumeEverything();
		}
		else
		{
			reenableCameraForPlayerPause();
		}
	}

	public void destroyMapActivitySelect()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_MapActivitySelect"));
		hideMapBlackMask();
	}

	public void showLockedIslandPopup(string title, Area aArea)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Map_centre"), "Panel_LockedIslandPopup", "Prefab/Popup/Panel_LockedIslandPopup", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUILockedIslandPopupController>().showPopup(title, aArea);
		showMapBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void destroyLockedIslandPopup()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_LockedIslandPopup"));
		hideMapBlackMask();
	}

	public void showOfferWeapon(Smith aSmith)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_OfferWeapon", "Prefab/OfferWeapon/Panel_OfferWeapon", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIOfferWeaponController>().setOffer(aSmith);
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeOfferWeapon(bool hide, bool resume)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_OfferWeapon"));
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().showTimeStatus();
		if (hide)
		{
			hideBlackMask();
		}
		if (resume)
		{
			resumeEverything();
		}
	}

	public void showSellResult(Smith aSmith)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_SellResult", "Prefab/SellResult/Panel_SellResult", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUISellResultController>().setReference(aSmith);
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeSellResult(Smith aSmith)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_SellResult"));
		hideBlackMask();
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().showTimeStatus();
		bool flag = false;
		GameObject gameObject = GameObject.Find("Panel_Tutorial");
		if (gameObject != null)
		{
			GUITutorialController component = gameObject.GetComponent<GUITutorialController>();
			flag = component.checkCurrentTutorial("50002");
			if (flag)
			{
				component.nextTutorial();
			}
		}
		showAssignSmithMenu(aSmith, exchangeable: false, flag);
	}

	public void showBuyResult(Smith aSmith)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_BuyResult", "Prefab/BuyMaterial/Panel_BuyResult", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIBuyResultController>().setReference(aSmith);
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeBuyResult(Smith aSmith)
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_BuyResult"));
		hideBlackMask();
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().showTimeStatus();
		showAssignSmithMenu(aSmith);
	}

	public void showExploreResult(Smith aSmith)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_ExploreResult", "Prefab/Explore/Panel_ExploreResult", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIExploreResultController>().setReference(aSmith);
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeExploreResult(Smith aSmith = null)
	{
		commonScreenObject.clearTooltips();
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_ExploreResult"));
		hideBlackMask();
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().showTimeStatus();
		if (aSmith != null && aSmith.getSmithRefId() != string.Empty)
		{
			showAssignSmithMenu(aSmith);
		}
		else
		{
			resumeEverything();
		}
	}

	public void showVacationResult(Smith aSmith, string aMoodText)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_VacationResult", "Prefab/Vacation/Panel_VacationResult", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIVacationResultController>().setReference(aSmith, aMoodText);
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeVacationResult(Smith aSmith = null)
	{
		commonScreenObject.clearTooltips();
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_VacationResult"));
		hideBlackMask();
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().showTimeStatus();
		if (aSmith != null && aSmith.getSmithRefId() != string.Empty)
		{
			showAssignSmithMenu(aSmith);
		}
		else
		{
			resumeEverything();
		}
	}

	public void showTrainingResult(Smith aSmith)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_TrainingResult", "Prefab/Training/Panel_TrainingResult", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUITrainingResultController>().setReference(aSmith);
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeTrainingResult(Smith aSmith = null)
	{
		commonScreenObject.clearTooltips();
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_TrainingResult"));
		hideBlackMask();
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().showTimeStatus();
		if (aSmith != null && aSmith.getSmithRefId() != string.Empty)
		{
			showAssignSmithMenu(aSmith);
		}
		else
		{
			resumeEverything();
		}
	}

	public void showScenarioTimer(string aTimerName)
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_top"), "Panel_ScenarioTimer", "Prefab/ScenarioTimer/Panel_ScenarioTimer", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUIScenarioTimerController>().setReference(aTimerName);
	}

	public void closeScenarioTimer()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_ScenarioTimer"));
	}

	public void showPanelsAtStart()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		Season seasonByMonth = CommonAPI.getSeasonByMonth(player.getSeasonIndex());
		switchBGM(CommonAPI.getSeasonBGM(seasonByMonth));
		showWeatherParticles(player.getWeather());
		GameObject gameObject = GameObject.Find("Panel_TopLeftMenu");
		if (gameObject != null)
		{
			if (completedTutorialIndex >= gameData.getTutorialSetOrderIndex("INTRO"))
			{
				gameObject.transform.localPosition = Vector3.zero;
			}
			else
			{
				gameObject.transform.localPosition = new Vector3(-400f, 0f, 0f);
			}
		}
		GameObject gameObject2 = GameObject.Find("Panel_Forge");
		if (gameObject2 != null)
		{
			gameObject2.transform.localPosition = new Vector3(0f, 400f, 0f);
		}
		GameObject gameObject3 = GameObject.Find("Panel_Speed");
		if (gameObject3 != null)
		{
			if (completedTutorialIndex >= gameData.getTutorialSetOrderIndex("SELL3"))
			{
				gameObject3.transform.localPosition = Vector3.zero;
			}
			else
			{
				gameObject3.transform.localPosition = new Vector3(0f, 400f, 0f);
			}
		}
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex2 = player.getCompletedTutorialIndex();
		GameObject gameObject4 = GameObject.Find("Calendar");
		if (gameObject4 != null)
		{
			if (completedTutorialIndex >= gameData.getTutorialSetOrderIndex("INTRO") && gameData.checkFeatureIsUnlocked(gameLockSet, "CALENDAR", completedTutorialIndex2))
			{
				gameObject4.transform.localPosition = Vector3.zero;
			}
			else
			{
				gameObject4.transform.localPosition = new Vector3(400f, 0f, 0f);
			}
		}
		GameObject gameObject5 = GameObject.Find("Objective");
		if (gameObject5 != null)
		{
			if (completedTutorialIndex >= gameData.getTutorialSetOrderIndex("INTRO"))
			{
				gameObject5.transform.localPosition = Vector3.zero;
			}
			else
			{
				gameObject5.transform.localPosition = new Vector3(400f, 0f, 0f);
			}
			gameObject5.GetComponent<GUIObjectiveController>().resetObjectiveDisplay();
		}
		GameObject gameObject6 = GameObject.Find("Panel_SmithList");
		if (gameObject6 != null)
		{
			if (completedTutorialIndex >= gameData.getTutorialSetOrderIndex("INTRO"))
			{
				gameObject6.transform.localPosition = Vector3.zero;
			}
			else
			{
				gameObject6.transform.localPosition = new Vector3(-400f, 0f, 0f);
			}
		}
		GameObject gameObject7 = GameObject.Find("Panel_BottomMenu");
		if (gameObject7 != null)
		{
			if (completedTutorialIndex >= gameData.getTutorialSetOrderIndex("INTRO"))
			{
				gameObject7.transform.localPosition = Vector3.zero;
			}
			else
			{
				gameObject7.transform.localPosition = new Vector3(0f, -400f, 0f);
			}
		}
		GameObject gameObject8 = GameObject.Find("Panel_Whetsapp");
		if (gameObject8 != null)
		{
			if (completedTutorialIndex >= gameData.getTutorialSetOrderIndex("SELL3"))
			{
				gameObject8.transform.localPosition = Vector3.zero;
			}
			else
			{
				gameObject8.transform.localPosition = new Vector3(0f, -400f, 0f);
			}
		}
		GameObject gameObject9 = GameObject.Find("Panel_ActivitySelect");
		if (gameObject9 != null)
		{
			gameObject9.transform.localPosition = new Vector3(0f, -400f, 0f);
		}
		GameObject gameObject10 = GameObject.Find("Panel_ForgeProgressNew");
		if (gameObject10 != null)
		{
			Object.DestroyImmediate(gameObject10);
		}
		GameObject gameObject11 = GameObject.Find("Panel_RequestList");
		if (gameObject11 != null)
		{
			gameObject11.GetComponent<GUIRequestListController>().cleanupRequestDisplay();
		}
		GameObject gameObject12 = GameObject.Find("AreaEventHUD");
		if (gameObject12 != null)
		{
			gameObject12.GetComponent<GUIAreaEventHUDController>().cleanupAreaEventHUD();
		}
	}

	private void setAssignSmithMenu()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("AssignHUDLeft");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			gameObject.transform.localPosition = new Vector3(-400f, 0f, 0f);
		}
	}

	public void showPatataDogAssignMenu(bool patata, SmithStation exceptStation = SmithStation.SmithStationBlank)
	{
		pauseEverything(GameState.GameStateMenu);
		GameObject[] array = GameObject.FindGameObjectsWithTag("MainHUDTop");
		GameObject[] array2 = array;
		foreach (GameObject aObject in array2)
		{
			moveHUD(aObject, MoveDirection.Up, moveIn: false, 0.75f, null, string.Empty);
		}
		GameObject[] array3 = GameObject.FindGameObjectsWithTag("MainHUDRight");
		GameObject[] array4 = array3;
		foreach (GameObject aObject2 in array4)
		{
			moveHUD(aObject2, MoveDirection.Right, moveIn: false, 0.75f, null, string.Empty);
		}
		GameObject[] array5 = GameObject.FindGameObjectsWithTag("MainHUDBottom");
		GameObject[] array6 = array5;
		foreach (GameObject gameObject in array6)
		{
			moveHUD(gameObject, MoveDirection.Down, moveIn: false, 0.75f, null, string.Empty);
			if (gameObject.name == "Panel_SmithList")
			{
				gameObject.GetComponent<GUISmithListMenuController>().closeHirePanel(isDismissSmithList: true);
			}
		}
		GameObject[] array7 = GameObject.FindGameObjectsWithTag("MainHUDLeft");
		GameObject[] array8 = array7;
		foreach (GameObject aObject3 in array8)
		{
			moveHUD(aObject3, MoveDirection.Left, moveIn: false, 0.75f, null, string.Empty);
		}
		GameObject gameObject2 = GameObject.Find("Panel_TopLeftMenu");
		if (gameObject2 != null)
		{
			gameObject2.GetComponent<GUITopMenuNewController>().disableShopProfile();
		}
		GameObject gameObject3 = commonScreenObject.createPrefab(GameObject.Find("Anchor_topRight"), "Panel_AssignPatataDogHUD", "Prefab/AssignStation/Panel_AssignPatataDogHUD", Vector3.zero, Vector3.one, Vector3.zero);
		if (patata)
		{
			gameObject3.GetComponent<GUIAssignPatataDogHUDController>().setPatataReference();
		}
		else
		{
			gameObject3.GetComponent<GUIAssignPatataDogHUDController>().setDogReference();
		}
		moveHUD(gameObject3, MoveDirection.Left, moveIn: true, 0.75f, null, string.Empty);
		characterAnimationController.changeAvatarDogLayer(bringUp: true, !patata);
		stationController.changeBoostLayer(bringUp: true, exceptStation);
		if (!patata)
		{
			obstacleController.disableAllCollider(exceptDogBed: true);
			obstacleController.changeDogBedLayer(bringUp: true);
		}
		else
		{
			obstacleController.disableAllCollider();
		}
		characterAnimationController.disableCollider();
		stationController.activateBoostCollider(exceptStation);
		showSceneBlackMask();
		workshopCamController.setCameraEnabled(aCameraEnabled: true);
	}

	public void closePatataDogAssignMenu(bool resumeFromPlayerPause)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("MainHUDTop");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if (checkShowPanel(gameObject.name))
			{
				moveHUD(gameObject, MoveDirection.Down, moveIn: true, 0.75f, null, string.Empty);
			}
		}
		GameObject[] array3 = GameObject.FindGameObjectsWithTag("MainHUDRight");
		GameObject[] array4 = array3;
		foreach (GameObject gameObject2 in array4)
		{
			if (checkShowPanel(gameObject2.name))
			{
				moveHUD(gameObject2, MoveDirection.Left, moveIn: true, 0.75f, null, string.Empty);
			}
		}
		GameObject[] array5 = GameObject.FindGameObjectsWithTag("MainHUDBottom");
		GameObject[] array6 = array5;
		foreach (GameObject gameObject3 in array6)
		{
			if (checkShowPanel(gameObject3.name))
			{
				moveHUD(gameObject3, MoveDirection.Up, moveIn: true, 0.75f, null, string.Empty);
			}
		}
		GameObject[] array7 = GameObject.FindGameObjectsWithTag("MainHUDLeft");
		GameObject[] array8 = array7;
		foreach (GameObject gameObject4 in array8)
		{
			if (checkShowPanel(gameObject4.name))
			{
				moveHUD(gameObject4, MoveDirection.Right, moveIn: true, 0.75f, null, string.Empty);
			}
		}
		GameObject gameObject5 = GameObject.Find("Panel_TopLeftMenu");
		if (gameObject5 != null)
		{
			gameObject5.GetComponent<GUITopMenuNewController>().enableShopProfile();
		}
		moveHUD(GameObject.Find("Panel_AssignPatataDogHUD"), MoveDirection.Right, moveIn: false, 0.75f, base.gameObject, "closePatataDogAssignHUD");
		characterAnimationController.changeAvatarDogLayer(bringUp: false, dog: true);
		stationController.changeBoostLayer(bringUp: false);
		stationController.disableBoostCollider();
		characterAnimationController.enableCollider();
		obstacleController.enableAllCollider();
		obstacleController.changeDogBedLayer(bringUp: false);
		if (resumeFromPlayerPause || !isPlayerPaused)
		{
			resumeEverything();
		}
		else
		{
			reenableCameraForPlayerPause();
		}
		hideSceneBlackMask();
	}

	public void closePatataDogAssignHUD()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_AssignPatataDogHUD"));
	}

	public void showAssignSmithMenu(Smith aSmith, bool exchangeable = false, bool tutorial = false)
	{
		pauseEverything(GameState.GameStateMenu);
		GameObject[] array = GameObject.FindGameObjectsWithTag("MainHUDTop");
		GameObject[] array2 = array;
		foreach (GameObject aObject in array2)
		{
			moveHUD(aObject, MoveDirection.Up, moveIn: false, 0.75f, null, string.Empty);
		}
		GameObject[] array3 = GameObject.FindGameObjectsWithTag("MainHUDRight");
		GameObject[] array4 = array3;
		foreach (GameObject aObject2 in array4)
		{
			moveHUD(aObject2, MoveDirection.Right, moveIn: false, 0.75f, null, string.Empty);
		}
		GameObject[] array5 = GameObject.FindGameObjectsWithTag("MainHUDBottom");
		GameObject[] array6 = array5;
		foreach (GameObject gameObject in array6)
		{
			moveHUD(gameObject, MoveDirection.Down, moveIn: false, 0.75f, null, string.Empty);
			if (gameObject.name == "Panel_SmithList")
			{
				gameObject.GetComponent<GUISmithListMenuController>().closeHirePanel(isDismissSmithList: true);
			}
		}
		GameObject[] array7 = GameObject.FindGameObjectsWithTag("MainHUDLeft");
		GameObject[] array8 = array7;
		foreach (GameObject aObject3 in array8)
		{
			moveHUD(aObject3, MoveDirection.Left, moveIn: false, 0.75f, null, string.Empty);
		}
		GameObject gameObject2 = GameObject.Find("Panel_TopLeftMenu");
		if (gameObject2 != null)
		{
			gameObject2.GetComponent<GUITopMenuNewController>().disableShopProfile();
		}
		GameObject gameObject3 = commonScreenObject.createPrefab(GameObject.Find("Anchor_topRight"), "Panel_AssignSmithHUD", "Prefab/AssignStation/Panel_AssignSmithHUD", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject3.GetComponent<GUIAssignSmithHUDController>().setReference(aSmith, tutorial);
		moveHUD(gameObject3, MoveDirection.Left, moveIn: true, 0.75f, null, string.Empty);
		GameObject aObject4 = GameObject.Find("Panel_AssignNote");
		moveHUD(aObject4, MoveDirection.Down, moveIn: true, 0.75f, null, string.Empty);
		GameObject.Find("GUIAnimationClickController").GetComponent<GUIAnimationClickController>().setSelectedSmith(aSmith);
		characterAnimationController.changeLayer(aSmith, bringUp: true);
		if (!tutorial)
		{
			stationController.changeLayer(bringUp: true);
		}
		characterAnimationController.disableCollider();
		if (!tutorial)
		{
			stationController.activateCollider(exchangeable);
		}
		obstacleController.disableAllCollider();
		showSceneBlackMask();
		workshopCamController.setCameraEnabled(aCameraEnabled: true);
	}

	public void closeAssignSmithMenu(Smith aSmith, bool hide, bool resume, bool resumeFromPlayerPause, bool tutorial = false)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		GameObject gameObject = GameObject.Find("Panel_TopLeftMenu");
		if (!tutorial)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("MainHUDTop");
			GameObject[] array2 = array;
			foreach (GameObject gameObject2 in array2)
			{
				if (checkShowPanel(gameObject2.name))
				{
					moveHUD(gameObject2, MoveDirection.Down, moveIn: true, 0.75f, null, string.Empty);
				}
			}
			GameObject[] array3 = GameObject.FindGameObjectsWithTag("MainHUDRight");
			GameObject[] array4 = array3;
			foreach (GameObject gameObject3 in array4)
			{
				if (!checkShowPanel(gameObject3.name))
				{
					continue;
				}
				if (gameObject3.name == "Calendar")
				{
					if (gameData.checkFeatureIsUnlocked(gameLockSet, "CALENDAR", completedTutorialIndex))
					{
						moveHUD(gameObject3, MoveDirection.Left, moveIn: true, 0.75f, null, string.Empty);
					}
				}
				else
				{
					moveHUD(gameObject3, MoveDirection.Left, moveIn: true, 0.75f, null, string.Empty);
				}
			}
			GameObject[] array5 = GameObject.FindGameObjectsWithTag("MainHUDBottom");
			GameObject[] array6 = array5;
			foreach (GameObject gameObject4 in array6)
			{
				if (checkShowPanel(gameObject4.name))
				{
					moveHUD(gameObject4, MoveDirection.Up, moveIn: true, 0.75f, null, string.Empty);
				}
			}
			GameObject[] array7 = GameObject.FindGameObjectsWithTag("MainHUDLeft");
			GameObject[] array8 = array7;
			foreach (GameObject gameObject5 in array8)
			{
				if (checkShowPanel(gameObject5.name))
				{
					moveHUD(gameObject5, MoveDirection.Right, moveIn: true, 0.75f, null, string.Empty);
				}
			}
		}
		else
		{
			if (gameObject != null)
			{
				moveHUD(gameObject, MoveDirection.Right, moveIn: true, 0.75f, null, string.Empty);
			}
			GameObject gameObject6 = GameObject.Find("Calendar");
			if (gameObject6 != null && gameData.checkFeatureIsUnlocked(gameLockSet, "CALENDAR", completedTutorialIndex))
			{
				moveHUD(gameObject6, MoveDirection.Left, moveIn: true, 0.75f, null, string.Empty);
			}
		}
		if (gameObject != null)
		{
			gameObject.GetComponent<GUITopMenuNewController>().enableShopProfile();
		}
		GameObject gameObject7 = GameObject.Find("Panel_AssignSmithHUD");
		if (gameObject7 != null)
		{
			moveHUD(gameObject7, MoveDirection.Right, moveIn: false, 0.75f, base.gameObject, "closeAssignSmithHUD");
		}
		GameObject gameObject8 = GameObject.Find("Panel_AssignNote");
		if (gameObject8 != null)
		{
			moveHUD(gameObject8, MoveDirection.Up, moveIn: false, 0.75f, null, string.Empty);
		}
		stationController.changeLayer(bringUp: false);
		stationController.disableAllCollider();
		characterAnimationController.enableCollider();
		obstacleController.enableAllCollider();
		if (resume && (resumeFromPlayerPause || !isPlayerPaused))
		{
			characterAnimationController.changeLayer(aSmith, bringUp: false);
			resumeEverything();
		}
		else
		{
			characterAnimationController.changeLayer(aSmith, bringUp: false, animatorEnabled: false);
			reenableCameraForPlayerPause();
		}
		hideSceneBlackMask();
	}

	public void closeAssignSmithHUD()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_AssignSmithHUD"));
	}

	public void moveHUD(GameObject aObject, MoveDirection aDirection, bool moveIn, float duration = 0.75f, GameObject receiver = null, string aMethod = "")
	{
		Vector3 zero = Vector3.zero;
		Vector3 localPosition = aObject.transform.localPosition;
		switch (aDirection)
		{
		case MoveDirection.Down:
			if (moveIn)
			{
				zero.y += 400f;
			}
			else
			{
				zero.y -= 400f;
			}
			break;
		case MoveDirection.Left:
			if (moveIn)
			{
				zero.x += 400f;
			}
			else
			{
				zero.x -= 400f;
			}
			break;
		case MoveDirection.Right:
			if (moveIn)
			{
				zero.x -= 400f;
			}
			else
			{
				zero.x += 400f;
			}
			break;
		case MoveDirection.Up:
			if (moveIn)
			{
				zero.y -= 400f;
			}
			else
			{
				zero.y += 400f;
			}
			break;
		}
		if (moveIn)
		{
			switch (aObject.name)
			{
			case "Panel_BottomMenu":
				aObject.GetComponent<BottomMenuController>().setTutorialState(string.Empty);
				break;
			case "Panel_SmithList":
			{
				GameData gameData = game.getGameData();
				Player player = game.getPlayer();
				GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
				string gameLockSet = gameScenarioByRefId.getGameLockSet();
				int completedTutorialIndex = player.getCompletedTutorialIndex();
				if (!gameData.checkFeatureIsUnlocked(gameLockSet, "SMITHHIRE", completedTutorialIndex))
				{
					aObject.GetComponent<GUISmithListMenuController>().disableSmithHire();
				}
				else
				{
					aObject.GetComponent<GUISmithListMenuController>().enableSmithHire();
				}
				break;
			}
			}
			commonScreenObject.tweenPosition(aObject.GetComponent<TweenPosition>(), zero, Vector3.zero, duration, receiver, aMethod);
		}
		else
		{
			commonScreenObject.tweenPosition(aObject.GetComponent<TweenPosition>(), localPosition, zero, duration, receiver, aMethod);
		}
	}

	public void showCodePopup()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_CodePopup", "Prefab/Code/Panel_CodePopup", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<GUICodePopupController>().setReference();
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeCodePopup()
	{
		hideBlackMask();
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_CodePopup"));
	}

	public void showUpgradeShopMenu()
	{
		GameObject gameObject = GameObject.Find("Panel_TopLeftMenu");
		if (gameObject != null)
		{
			gameObject.GetComponent<GUITopMenuNewController>().disableShopProfile();
		}
		pauseEverything(GameState.GameStateMenu);
		GameObject[] array = GameObject.FindGameObjectsWithTag("MainHUDTop");
		GameObject[] array2 = array;
		foreach (GameObject aObject in array2)
		{
			moveHUD(aObject, MoveDirection.Up, moveIn: false, 0.75f, null, string.Empty);
		}
		GameObject[] array3 = GameObject.FindGameObjectsWithTag("MainHUDRight");
		GameObject[] array4 = array3;
		foreach (GameObject aObject2 in array4)
		{
			moveHUD(aObject2, MoveDirection.Right, moveIn: false, 0.75f, null, string.Empty);
		}
		GameObject[] array5 = GameObject.FindGameObjectsWithTag("MainHUDBottom");
		GameObject[] array6 = array5;
		foreach (GameObject gameObject2 in array6)
		{
			moveHUD(gameObject2, MoveDirection.Down, moveIn: false, 0.75f, null, string.Empty);
			if (gameObject2.name == "Panel_SmithList")
			{
				gameObject2.GetComponent<GUISmithListMenuController>().closeHirePanel(isDismissSmithList: true);
			}
		}
		GameObject[] array7 = GameObject.FindGameObjectsWithTag("MainHUDLeft");
		GameObject[] array8 = array7;
		foreach (GameObject aObject3 in array8)
		{
			moveHUD(aObject3, MoveDirection.Left, moveIn: false, 0.75f, null, string.Empty);
		}
		GameObject gameObject3 = commonScreenObject.createPrefab(GameObject.Find("Anchor_bottom"), "Panel_ShopUpgrade", "Prefab/ShopUpgrade/Panel_ShopUpgrade", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject3.GetComponent<GUIShopUpgradeHUDController>().setReference();
		moveHUD(gameObject3, MoveDirection.Up, moveIn: true, 0.75f, null, string.Empty);
		GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>().disableCollider();
		obstacleController.disableAllCollider();
		workshopCamController.setCameraEnabled(aCameraEnabled: true);
	}

	public void closeShopUpgradeMenu(bool resume, bool resumeFromPlayerPause)
	{
		GameObject gameObject = GameObject.Find("Panel_TopLeftMenu");
		if (gameObject != null)
		{
			gameObject.GetComponent<GUITopMenuNewController>().enableShopProfile();
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("MainHUDTop");
		GameObject[] array2 = array;
		foreach (GameObject gameObject2 in array2)
		{
			if (checkShowPanel(gameObject2.name))
			{
				moveHUD(gameObject2, MoveDirection.Down, moveIn: true, 0.75f, null, string.Empty);
			}
		}
		GameObject[] array3 = GameObject.FindGameObjectsWithTag("MainHUDRight");
		GameObject[] array4 = array3;
		foreach (GameObject gameObject3 in array4)
		{
			if (checkShowPanel(gameObject3.name))
			{
				moveHUD(gameObject3, MoveDirection.Left, moveIn: true, 0.75f, null, string.Empty);
			}
		}
		GameObject[] array5 = GameObject.FindGameObjectsWithTag("MainHUDBottom");
		GameObject[] array6 = array5;
		foreach (GameObject gameObject4 in array6)
		{
			if (checkShowPanel(gameObject4.name))
			{
				moveHUD(gameObject4, MoveDirection.Up, moveIn: true, 0.75f, null, string.Empty);
			}
		}
		GameObject[] array7 = GameObject.FindGameObjectsWithTag("MainHUDLeft");
		GameObject[] array8 = array7;
		foreach (GameObject gameObject5 in array8)
		{
			if (checkShowPanel(gameObject5.name))
			{
				moveHUD(gameObject5, MoveDirection.Right, moveIn: true, 0.75f, null, string.Empty);
			}
		}
		moveHUD(GameObject.Find("Panel_ShopUpgrade"), MoveDirection.Down, moveIn: false, 0.75f, base.gameObject, "closeShopUpgradeHUD");
		if (resume && (resumeFromPlayerPause || !isPlayerPaused))
		{
			resumeEverything();
		}
		else
		{
			reenableCameraForPlayerPause();
		}
		GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>().enableCollider();
		obstacleController.enableAllCollider();
	}

	public void closeShopUpgradeHUD()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_ShopUpgrade"));
	}

	public void clearWeatherParticles()
	{
		if (prevParticlesList != null)
		{
			foreach (GameObject prevParticles in prevParticlesList)
			{
				commonScreenObject.destroyPrefabImmediate(prevParticles);
			}
		}
		prevParticlesList = new List<GameObject>();
		GameObject gameObject = GameObject.Find("WeatherParticles_position");
		if (gameObject != null && gameObject.transform.childCount > 0)
		{
			for (int num = gameObject.transform.childCount - 1; num >= 0; num--)
			{
				GameObject aObj = gameObject.transform.GetChild(num).gameObject;
				commonScreenObject.destroyPrefabImmediate(aObj);
			}
		}
	}

	public void showWeatherParticles(Weather currWeather)
	{
		string text = "Prefab/WeatherParticles/";
		string text2 = "WeatherParticles_" + currWeather.getImage();
		GameObject gameObject = GameObject.Find("WeatherParticles_position");
		if (prevParticlesList != null)
		{
			foreach (GameObject prevParticles in prevParticlesList)
			{
				commonScreenObject.destroyPrefabImmediate(prevParticles);
			}
		}
		prevParticlesList = new List<GameObject>();
		bool flag = false;
		if (gameObject.transform.childCount > 0)
		{
			for (int num = gameObject.transform.childCount - 1; num >= 0; num--)
			{
				GameObject gameObject2 = gameObject.transform.GetChild(num).gameObject;
				if (gameObject2.name != text2)
				{
					ParticleSystem[] componentsInChildren = gameObject2.GetComponentsInChildren<ParticleSystem>();
					foreach (ParticleSystem particleSystem in componentsInChildren)
					{
						particleSystem.Stop();
					}
					prevParticlesList.Add(gameObject2);
				}
				else
				{
					flag = true;
				}
			}
		}
		if (!flag && text2 != "WeatherParticles_")
		{
			GameObject gameObject3 = commonScreenObject.createPrefab(gameObject, text2, text + text2, Vector3.zero, Vector3.one, Vector3.zero);
		}
	}

	public bool checkShowPanel(string panelName)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		bool result = true;
		switch (panelName)
		{
		case "Panel_TopLeftMenu":
			if (completedTutorialIndex < gameData.getTutorialSetOrderIndex("INTRO"))
			{
				result = false;
			}
			break;
		case "Calendar":
			if (completedTutorialIndex < gameData.getTutorialSetOrderIndex("INTRO"))
			{
				result = false;
			}
			break;
		case "Objective":
			if (completedTutorialIndex < gameData.getTutorialSetOrderIndex("INTRO"))
			{
				result = false;
			}
			break;
		case "Panel_SmithList":
			if (completedTutorialIndex < gameData.getTutorialSetOrderIndex("INTRO"))
			{
				result = false;
			}
			break;
		case "Panel_BottomMenu":
			if (completedTutorialIndex < gameData.getTutorialSetOrderIndex("INTRO"))
			{
				result = false;
			}
			break;
		case "Panel_Speed":
		case "Panel_Whetsapp":
			if (completedTutorialIndex < gameData.getTutorialSetOrderIndex("SELL3"))
			{
				result = false;
			}
			break;
		case "Panel_ActivitySelect":
			result = false;
			break;
		case "Panel_Forge":
			result = false;
			break;
		}
		return result;
	}

	public void doPlayerPause()
	{
		isPlayerPaused = true;
		pauseEverything(GameState.GameStatePopEvent, aHasPopup: false);
		workshopCamController.setCameraEnabled(aCameraEnabled: true);
	}

	public void doPlayerUnpause()
	{
		resumeEverything();
	}

	public void pauseEverything(GameState aGameState, bool aHasPopup = true)
	{
		if (shopViewController != null)
		{
			shopViewController.setGameState(aGameState);
		}
		characterAnimationController.pauseAllCharacters();
		workshopCamController.setCameraEnabled(aCameraEnabled: false);
		obstacleController.pauseOtherObstacleAnim();
		isPaused = true;
	}

	public void reenableCameraForPlayerPause()
	{
		if (isPlayerPaused && !getHasPopup())
		{
			workshopCamController.setCameraEnabled(aCameraEnabled: true);
		}
	}

	public void resumeEverything()
	{
		if (shopMenuController == null)
		{
			shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		}
		shopMenuController.setDoUnlockCheck();
		shopViewController.setGameState(GameState.GameStateIdle);
		characterAnimationController.resumeAllCharacters();
		workshopCamController.setCameraEnabled(aCameraEnabled: true);
		obstacleController.resumeOtherObstacleAnim();
		if (speedController == null)
		{
			GameObject gameObject = GameObject.Find("Panel_Speed");
			if (gameObject != null)
			{
				speedController = gameObject.GetComponent<GUISpeedController>();
			}
		}
		if (speedController != null)
		{
			speedController.revertLastButtonState();
		}
		isPlayerPaused = false;
		isPaused = false;
	}

	public bool getIsPaused()
	{
		return isPaused;
	}

	public bool getIsPlayerPaused()
	{
		return isPlayerPaused;
	}

	public bool getGameStarted()
	{
		return gameStarted;
	}

	public bool getHasPopup()
	{
		findMasks();
		if (checkMaskEnabled() || GameObject.Find("Panel_ShopUpgrade") != null)
		{
			return true;
		}
		return false;
	}

	public bool checkMaskEnabled()
	{
		if (blackmask != null && blackmask.activeSelf && blackmask.GetComponent<BoxCollider>().enabled)
		{
			return true;
		}
		if (blackmask_popup != null && blackmask_popup.activeSelf && blackmask_popup.GetComponent<BoxCollider>().enabled)
		{
			return true;
		}
		if (tutorialMask_popup != null && tutorialMask_popup.activeSelf && tutorialMask_popup.GetComponent<BoxCollider>().enabled)
		{
			return true;
		}
		if (mapBlackmask_popup != null && mapBlackmask_popup.activeSelf && mapBlackmask_popup.GetComponent<BoxCollider>().enabled)
		{
			return true;
		}
		if (mapBlackmask != null && mapBlackmask.activeSelf && mapBlackmask.GetComponent<BoxCollider>().enabled)
		{
			return true;
		}
		if (blackmaskScene != null && blackmaskScene.activeSelf && blackmaskScene.GetComponent<SpriteRenderer>().color.a > 0f)
		{
			return true;
		}
		return false;
	}

	private void findMasks()
	{
		if (blackmask == null)
		{
			blackmask = GameObject.Find("blackmask");
		}
		if (blackmask_popup == null)
		{
			blackmask_popup = GameObject.Find("blackmask_popup");
		}
		if (tutorialMask_popup == null)
		{
			tutorialMask_popup = GameObject.Find("tutorialMask_popup");
		}
		if (mapBlackmask_popup == null)
		{
			mapBlackmask_popup = GameObject.Find("mapBlackmask_popup");
		}
		if (mapBlackmask == null)
		{
			mapBlackmask = GameObject.Find("mapBlackmask");
		}
		if (blackmaskScene == null)
		{
			blackmaskScene = GameObject.Find("blackmaskScene");
		}
	}

	public void setGameStarted(bool started)
	{
		gameStarted = started;
	}

	public void showBlackMask()
	{
		findMasks();
		if (blackmask != null)
		{
			float alpha = blackmask.GetComponent<UISprite>().alpha;
			if (alpha != 0.7f || !blackmask.GetComponent<BoxCollider>().enabled)
			{
				TweenAlpha component = blackmask.GetComponent<TweenAlpha>();
				component.enabled = true;
				commonScreenObject.tweenAlpha(component, alpha, 0.7f, 0.1f, null, string.Empty);
				blackmask.GetComponent<BoxCollider>().enabled = true;
			}
		}
	}

	public void hideBlackMask()
	{
		findMasks();
		if (blackmask != null)
		{
			float alpha = blackmask.GetComponent<UISprite>().alpha;
			if (alpha != 0f || blackmask.GetComponent<BoxCollider>().enabled)
			{
				TweenAlpha component = blackmask.GetComponent<TweenAlpha>();
				component.enabled = true;
				commonScreenObject.tweenAlpha(component, alpha, 0f, 0.1f, null, string.Empty);
				blackmask.GetComponent<BoxCollider>().enabled = false;
			}
		}
	}

	public void showPureBlackMask(float tweenSpeed)
	{
		findMasks();
		if (blackmask != null)
		{
			float alpha = blackmask.GetComponent<UISprite>().alpha;
			if (alpha != 1f || !blackmask.GetComponent<BoxCollider>().enabled)
			{
				TweenAlpha component = blackmask.GetComponent<TweenAlpha>();
				component.enabled = true;
				commonScreenObject.tweenAlpha(component, alpha, 1f, tweenSpeed, null, string.Empty);
				blackmask.GetComponent<BoxCollider>().enabled = true;
			}
		}
	}

	public void hidePureBlackMask()
	{
		findMasks();
		if (blackmask != null)
		{
			float alpha = blackmask.GetComponent<UISprite>().alpha;
			if (alpha != 0f || blackmask.GetComponent<BoxCollider>().enabled)
			{
				TweenAlpha component = blackmask.GetComponent<TweenAlpha>();
				component.enabled = true;
				commonScreenObject.tweenAlpha(component, alpha, 0f, 0.1f, null, string.Empty);
				blackmask.GetComponent<BoxCollider>().enabled = false;
			}
		}
	}

	public void showTransparentMask()
	{
		findMasks();
		if (blackmask != null && !blackmask.GetComponent<BoxCollider>().enabled)
		{
			blackmask.GetComponent<UISprite>().color = Color.white;
			TweenAlpha component = blackmask.GetComponent<TweenAlpha>();
			component.enabled = true;
			commonScreenObject.tweenAlpha(component, 0f, 0.05f, 0.05f, null, string.Empty);
			blackmask.GetComponent<BoxCollider>().enabled = true;
		}
	}

	public void hideTransparentMask()
	{
		findMasks();
		if (blackmask != null && blackmask.GetComponent<BoxCollider>().enabled)
		{
			blackmask.GetComponent<UISprite>().color = Color.black;
			TweenAlpha component = blackmask.GetComponent<TweenAlpha>();
			component.enabled = true;
			commonScreenObject.tweenAlpha(component, 0f, 0f, 0.05f, null, string.Empty);
			blackmask.GetComponent<BoxCollider>().enabled = false;
		}
	}

	public void showBlackMaskPopUp()
	{
		findMasks();
		if (blackmask_popup != null)
		{
			float alpha = blackmask_popup.GetComponent<UISprite>().alpha;
			if (alpha != 0.7f || !blackmask_popup.GetComponent<BoxCollider>().enabled)
			{
				TweenAlpha component = blackmask_popup.GetComponent<TweenAlpha>();
				component.enabled = true;
				commonScreenObject.tweenAlpha(component, alpha, 0.7f, 0.1f, null, string.Empty);
				blackmask_popup.GetComponent<BoxCollider>().enabled = true;
			}
		}
	}

	public void hideBlackMaskPopUp()
	{
		findMasks();
		if (blackmask_popup != null)
		{
			float alpha = blackmask_popup.GetComponent<UISprite>().alpha;
			if (alpha != 0f || blackmask_popup.GetComponent<BoxCollider>().enabled)
			{
				TweenAlpha component = blackmask_popup.GetComponent<TweenAlpha>();
				component.enabled = true;
				commonScreenObject.tweenAlpha(component, alpha, 0f, 0.1f, null, string.Empty);
				blackmask_popup.GetComponent<BoxCollider>().enabled = false;
			}
		}
	}

	public void showTutorialMaskPopUp()
	{
		findMasks();
		if (tutorialMask_popup != null && !tutorialMask_popup.GetComponent<BoxCollider>().enabled)
		{
			tutorialMask_popup.GetComponent<UISprite>().color = Color.white;
			TweenAlpha component = tutorialMask_popup.GetComponent<TweenAlpha>();
			component.enabled = true;
			commonScreenObject.tweenAlpha(component, 0f, 0.01f, 0.2f, null, string.Empty);
			tutorialMask_popup.GetComponent<BoxCollider>().enabled = true;
		}
	}

	public void hideTutorialMaskPopUp()
	{
		findMasks();
		if (tutorialMask_popup != null && tutorialMask_popup.GetComponent<BoxCollider>().enabled)
		{
			tutorialMask_popup.GetComponent<UISprite>().color = Color.white;
			TweenAlpha component = tutorialMask_popup.GetComponent<TweenAlpha>();
			component.enabled = true;
			commonScreenObject.tweenAlpha(component, 0.01f, 0f, 0.2f, null, string.Empty);
			tutorialMask_popup.GetComponent<BoxCollider>().enabled = false;
		}
	}

	public void showMapBlackMaskPopUp()
	{
		findMasks();
		if (mapBlackmask_popup != null)
		{
			float alpha = mapBlackmask_popup.GetComponent<UISprite>().alpha;
			if (alpha != 0.7f || !mapBlackmask_popup.GetComponent<BoxCollider>().enabled)
			{
				TweenAlpha component = mapBlackmask_popup.GetComponent<TweenAlpha>();
				component.enabled = true;
				commonScreenObject.tweenAlpha(component, alpha, 0.7f, 0.1f, null, string.Empty);
				mapBlackmask_popup.GetComponent<BoxCollider>().enabled = true;
			}
		}
	}

	public void hideMapBlackMaskPopUp()
	{
		findMasks();
		if (mapBlackmask_popup != null)
		{
			float alpha = mapBlackmask_popup.GetComponent<UISprite>().alpha;
			if (alpha != 0f || mapBlackmask_popup.GetComponent<BoxCollider>().enabled)
			{
				TweenAlpha component = mapBlackmask_popup.GetComponent<TweenAlpha>();
				component.enabled = true;
				commonScreenObject.tweenAlpha(component, alpha, 0f, 0.1f, null, string.Empty);
				mapBlackmask_popup.GetComponent<BoxCollider>().enabled = false;
			}
		}
	}

	public void showMapBlackMask()
	{
		findMasks();
		if (mapBlackmask != null)
		{
			float alpha = mapBlackmask.GetComponent<UISprite>().alpha;
			if (alpha != 0.7f || !mapBlackmask.GetComponent<BoxCollider>().enabled)
			{
				TweenAlpha component = mapBlackmask.GetComponent<TweenAlpha>();
				component.enabled = true;
				commonScreenObject.tweenAlpha(component, alpha, 0.7f, 0.1f, null, string.Empty);
				mapBlackmask.GetComponent<BoxCollider>().enabled = true;
			}
		}
	}

	public void hideMapBlackMask()
	{
		findMasks();
		if (mapBlackmask != null)
		{
			float alpha = mapBlackmask.GetComponent<UISprite>().alpha;
			if (alpha != 0f || mapBlackmask.GetComponent<BoxCollider>().enabled)
			{
				TweenAlpha component = mapBlackmask.GetComponent<TweenAlpha>();
				component.enabled = true;
				commonScreenObject.tweenAlpha(component, alpha, 0f, 0.1f, null, string.Empty);
				mapBlackmask.GetComponent<BoxCollider>().enabled = false;
			}
		}
	}

	public void showMapTransparentMask()
	{
		findMasks();
		if (blackmask_popup != null && !blackmask_popup.GetComponent<BoxCollider>().enabled)
		{
			blackmask_popup.GetComponent<UISprite>().color = Color.white;
			TweenAlpha component = blackmask_popup.GetComponent<TweenAlpha>();
			component.enabled = true;
			commonScreenObject.tweenAlpha(component, 0f, 0.05f, 0.05f, null, string.Empty);
			blackmask_popup.GetComponent<BoxCollider>().enabled = true;
		}
	}

	public void hideMapTransparentMask()
	{
		findMasks();
		if (mapBlackmask != null && mapBlackmask.GetComponent<BoxCollider>().enabled)
		{
			mapBlackmask.GetComponent<UISprite>().color = Color.black;
			TweenAlpha component = mapBlackmask.GetComponent<TweenAlpha>();
			component.enabled = true;
			commonScreenObject.tweenAlpha(component, 0f, 0f, 0.05f, null, string.Empty);
			mapBlackmask.GetComponent<BoxCollider>().enabled = false;
		}
	}

	public void showSceneBlackMask()
	{
		findMasks();
		if (blackmaskScene != null && blackmaskScene.GetComponent<SpriteRenderer>().color.a < 0.7f)
		{
			blackmaskScene.GetComponent<BlackmaskSceneScript>().startLoadingToBlack();
		}
	}

	public void hideSceneBlackMask()
	{
		findMasks();
		if (blackmaskScene != null && blackmaskScene.GetComponent<SpriteRenderer>().color.a > 0f)
		{
			blackmaskScene.GetComponent<BlackmaskSceneScript>().startLoadingFromBlack();
		}
	}

	public void showBlackMaskStart()
	{
		GameObject gameObject = GameObject.Find("blackmask_start");
		if (gameObject != null)
		{
			float alpha = gameObject.GetComponent<UISprite>().alpha;
			if (alpha != 0.7f || !gameObject.GetComponent<BoxCollider>().enabled)
			{
				TweenAlpha component = gameObject.GetComponent<TweenAlpha>();
				component.enabled = true;
				commonScreenObject.tweenAlpha(component, alpha, 0.7f, 0.1f, null, string.Empty);
				gameObject.GetComponent<BoxCollider>().enabled = true;
			}
		}
	}

	public void hideBlackMaskStart()
	{
		GameObject gameObject = GameObject.Find("blackmask_start");
		if (gameObject != null)
		{
			float alpha = gameObject.GetComponent<UISprite>().alpha;
			if (alpha != 0f || gameObject.GetComponent<BoxCollider>().enabled)
			{
				TweenAlpha component = gameObject.GetComponent<TweenAlpha>();
				component.enabled = true;
				commonScreenObject.tweenAlpha(component, alpha, 0f, 0.1f, null, string.Empty);
				gameObject.GetComponent<BoxCollider>().enabled = false;
			}
		}
	}

	public void showHeroAnimTest()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_TestHeroAnims", "Prefab/Testing/Panel_TestHeroAnims", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<TestHeroAnimsController>().setReference();
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeHeroAnimTest()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_TestHeroAnims"));
		hideBlackMask();
		if (!isPlayerPaused)
		{
			resumeEverything();
		}
		else
		{
			reenableCameraForPlayerPause();
		}
	}

	public void showSmithImagesTest()
	{
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("Anchor_centre"), "Panel_TestSmithImages", "Prefab/Testing/Panel_TestSmithImages", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<TestSmithImagesController>().setReference();
		showBlackMask();
		pauseEverything(GameState.GameStatePopEvent);
	}

	public void closeSmithImagesTest()
	{
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("Panel_TestSmithImages"));
		hideBlackMask();
		if (!isPlayerPaused)
		{
			resumeEverything();
		}
		else
		{
			reenableCameraForPlayerPause();
		}
	}
}
