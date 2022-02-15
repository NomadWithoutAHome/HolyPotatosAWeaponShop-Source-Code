using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class GUIMapController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private GUISceneCameraController mapCamera;

	private GUICameraController cameraController;

	private GUIMapGridController gridController;

	private GUIAnimationClickController animClickController;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private TooltipTextScript heroTooltipScript;

	private TooltipGridScript islandTooltipScript;

	private SpriteRenderer mapBgFront;

	private Smith preselectedSmith;

	private bool fromAssign;

	private GameObject mapPlayerHUD;

	private GameObject mapHUDButtons;

	private UIButton buttonBackWorkshop;

	private UIButton buttonBackWorldMap;

	private UIButton buttonChangeActivity;

	private UILabel backWorkshopLabel;

	private UILabel backWorldMapLabel;

	private UILabel changeActivityLabel;

	private UILabel playerName;

	private UILabel shopName;

	private UILabel lvlLabel;

	private UILabel shopLvlLabel;

	private UILabel mapMoneyLabel;

	private UILabel mapFameLabel;

	private UILabel mapTicketLabel;

	private UILabel mapSmithLabel;

	private GUIAreaEventHUDController areaEventHUDCtr;

	private List<Area> areaList;

	private ActivityType selectedActivity;

	private int selectedAreaIndex;

	private Dictionary<string, GameObject> areaGameobjectList;

	private List<GameObject> pathGameObjectList;

	private GameObject Panel_Area;

	private GameObject Panel_MapSelectGrid;

	private string areaPrefix;

	private string lockedAreaPrefix;

	private string pathPrefix;

	private string itemPrefix;

	private GameObject panel_MapAreaInfo;

	private List<GameObject> areaInfoObjList;

	private Dictionary<string, ExploreItem> itemExploreList;

	private UILabel areaTitle;

	private GameObject areaEventIcon;

	private UILabel areaInfo;

	private UILabel smithEffectTitle;

	private UILabel smithEffectLabel;

	private UILabel travelTimeTitle;

	private UILabel travelTimeLabel;

	private GameObject subtitle;

	private UILabel subtitleLabel;

	private UIGrid heroGrid;

	private UIGrid itemGrid;

	private UIGrid exploreEnchantmentGrid;

	private UILabel exploreEnchantmentLabel;

	private UIGrid exploreMatGrid;

	private UILabel exploreMatLabel;

	private UIGrid exploreRelicGrid;

	private UILabel exploreRelicLabel;

	private GameObject seasonPackageFrame;

	private UIGrid seasonPackageGrid;

	private GameObject otherPackageFrame;

	private UIGrid otherPackageGrid;

	private int exploreItemIndex;

	private Dictionary<string, ShopItem> buyItemList;

	private Dictionary<string, GameObject> areaInfoBuyObjList;

	private Dictionary<string, int> areaBuyQtyList;

	private GUIMapSelectSmithController selectSmithCtr;

	private GUIMapSellWeaponController sellWeaponCtr;

	private GUIMapBuyMatController buyMatCtr;

	private GameObject button_confirm;

	private Vector3 leftPanelOut;

	private Vector3 rightPanelOut;

	private Vector3 bottomPanelOut;

	private bool focusedMap;

	private List<Vector3> areaSmithPos1;

	private List<Vector3> areaSmithPos2;

	private List<Vector3> areaSmithPos3;

	private Color32 trainVacationColor;

	private bool canGo;

	private GameObject unlockPanel;

	private bool tutorial;

	private void Start()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		mapCamera = GameObject.Find("NGUICameraMap").GetComponent<GUISceneCameraController>();
		cameraController = GameObject.Find("GUICameraController").GetComponent<GUICameraController>();
		gridController = GameObject.Find("GUIMapGridController").GetComponent<GUIMapGridController>();
		animClickController = GameObject.Find("GUIAnimationClickController").GetComponent<GUIAnimationClickController>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		tooltipScript = GameObject.Find("MapTooltipInfo").GetComponent<TooltipTextScript>();
		heroTooltipScript = GameObject.Find("HeroTooltipInfo").GetComponent<TooltipTextScript>();
		islandTooltipScript = GameObject.Find("IslandTooltipInfo").GetComponent<TooltipGridScript>();
		mapBgFront = GameObject.Find("mapBgFront").GetComponent<SpriteRenderer>();
		preselectedSmith = null;
		fromAssign = false;
		mapPlayerHUD = GameObject.Find("MapPlayerHUD");
		mapHUDButtons = GameObject.Find("MapHUDButtons");
		buttonBackWorkshop = commonScreenObject.findChild(mapHUDButtons, "Button_BackWorkshop").GetComponent<UIButton>();
		buttonBackWorldMap = commonScreenObject.findChild(mapHUDButtons, "Button_BackWorldMap").GetComponent<UIButton>();
		buttonChangeActivity = commonScreenObject.findChild(mapHUDButtons, "Button_ChangeActivity").GetComponent<UIButton>();
		backWorkshopLabel = commonScreenObject.findChild(mapHUDButtons, "Button_BackWorkshop/BackWorkshopLabel").GetComponent<UILabel>();
		backWorldMapLabel = commonScreenObject.findChild(mapHUDButtons, "Button_BackWorldMap/BackWorldMapLabel").GetComponent<UILabel>();
		changeActivityLabel = commonScreenObject.findChild(mapHUDButtons, "Button_ChangeActivity/ChangeActivityLabel").GetComponent<UILabel>();
		playerName = commonScreenObject.findChild(mapPlayerHUD, "ShopFrame/MapPlayerName").GetComponent<UILabel>();
		shopName = commonScreenObject.findChild(mapPlayerHUD, "ShopFrame/MapShopName").GetComponent<UILabel>();
		lvlLabel = commonScreenObject.findChild(mapPlayerHUD, "ShopFrame/MapLvlLabel").GetComponent<UILabel>();
		shopLvlLabel = commonScreenObject.findChild(mapPlayerHUD, "ShopFrame/MapShopLvl").GetComponent<UILabel>();
		mapMoneyLabel = commonScreenObject.findChild(mapPlayerHUD, "MoneyFrame/MapMoneyLabel").GetComponent<UILabel>();
		mapFameLabel = commonScreenObject.findChild(mapPlayerHUD, "FameFrame/MapFameLabel").GetComponent<UILabel>();
		mapTicketLabel = commonScreenObject.findChild(mapPlayerHUD, "TicketFrame/MapTicketLabel").GetComponent<UILabel>();
		mapSmithLabel = commonScreenObject.findChild(mapPlayerHUD, "SmithFrame/MapSmithLabel").GetComponent<UILabel>();
		areaEventHUDCtr = GameObject.Find("AreaEventMapHUD").GetComponent<GUIAreaEventHUDController>();
		areaList = new List<Area>();
		selectedActivity = ActivityType.ActivityTypeBlank;
		selectedAreaIndex = -1;
		areaGameobjectList = new Dictionary<string, GameObject>();
		pathGameObjectList = new List<GameObject>();
		Panel_Area = GameObject.Find("Panel_Area");
		Panel_MapSelectGrid = GameObject.Find("Panel_MapSelectGrid");
		areaPrefix = "Area_";
		lockedAreaPrefix = "LockedArea_";
		pathPrefix = "Path_";
		itemPrefix = "Item_";
		panel_MapAreaInfo = GameObject.Find("Panel_MapAreaInfo");
		areaInfoObjList = new List<GameObject>();
		itemExploreList = new Dictionary<string, ExploreItem>();
		buyItemList = new Dictionary<string, ShopItem>();
		areaInfoBuyObjList = new Dictionary<string, GameObject>();
		areaBuyQtyList = new Dictionary<string, int>();
		selectSmithCtr = null;
		sellWeaponCtr = null;
		buyMatCtr = null;
		button_confirm = null;
		leftPanelOut = new Vector3(-400f, 0f, 0f);
		rightPanelOut = new Vector3(400f, 0f, 0f);
		bottomPanelOut = new Vector3(0f, -100f, 0f);
		focusedMap = false;
		areaSmithPos1 = new List<Vector3>();
		areaSmithPos1.Add(new Vector3(0.2f, -2.1f, 0f));
		areaSmithPos2 = new List<Vector3>();
		areaSmithPos2.Add(new Vector3(-0.35f, -2.1f, 0f));
		areaSmithPos2.Add(new Vector3(0.85f, -2.1f, 0f));
		areaSmithPos3 = new List<Vector3>();
		areaSmithPos3.Add(new Vector3(-0.85f, -2.1f, 0f));
		areaSmithPos3.Add(new Vector3(0.2f, -2.1f, 0f));
		areaSmithPos3.Add(new Vector3(1.3f, -2.1f, 0f));
		trainVacationColor = new Color32(248, 160, 160, byte.MaxValue);
		canGo = true;
		unlockPanel = null;
		tutorial = false;
		setReference();
	}

	public void processClick(string gameObjectName, bool doubleClick = false)
	{
		GameData gameData = game.getGameData();
		CommonAPI.debug("gameobjectName: " + gameObjectName);
		switch (gameObjectName)
		{
		case "UnlockButton":
			unlockArea();
			return;
		case "Button_Confirm":
			startActivity();
			return;
		case "Button_ChangeActivity":
			viewController.showActivitySelect();
			return;
		case "Button_BackWorkshop":
			viewController.destroyMapActivitySelect();
			if (fromAssign)
			{
				viewController.closeWorldMap(hide: true, resume: false, resumeFromPlayerPause: false);
				animClickController.closeSmithActionMenu();
			}
			else
			{
				viewController.closeWorldMap(hide: true, resume: true, resumeFromPlayerPause: false);
			}
			fromAssign = false;
			return;
		case "ActivityGoButton":
			audioController.playMapConfirmAudio();
			if (selectedAreaIndex != -1 && checkArea())
			{
				focusOnSelectedArea();
				slideInPanel();
			}
			return;
		case "Button_BackWorldMap":
			if (!focusedMap)
			{
				return;
			}
			foreach (GameObject areaInfoObj in areaInfoObjList)
			{
				commonScreenObject.destroyPrefabImmediate(areaInfoObj);
			}
			areaInfoObjList.Clear();
			areaInfoBuyObjList.Clear();
			areaBuyQtyList.Clear();
			itemExploreList = null;
			resetFocusCamera(transition: true);
			slideOutAreaInfoPanel();
			slideOutPanel();
			areaEventHUDCtr.slideInPanel(transition: true);
			buttonBackWorldMap.gameObject.SetActive(value: false);
			buttonChangeActivity.gameObject.SetActive(value: true);
			return;
		}
		string[] array = gameObjectName.Split('_');
		if (focusedMap || !mapCamera.getPanLimit())
		{
			return;
		}
		switch (array[0])
		{
		case "Area":
		{
			int aIndex = CommonAPI.parseInt(array[1]);
			selectArea(aIndex);
			tooltipScript.setInactive();
			heroTooltipScript.setInactive();
			islandTooltipScript.setInactive();
			break;
		}
		case "LockedArea":
		{
			Player player = game.getPlayer();
			GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
			string gameLockSet = gameScenarioByRefId.getGameLockSet();
			int completedTutorialIndex = player.getCompletedTutorialIndex();
			if (gameData.checkFeatureIsUnlocked(gameLockSet, "AREAUNLOCK", completedTutorialIndex))
			{
				Area aArea = areaList[CommonAPI.parseInt(array[1])];
				viewController.showLockedIslandPopup(gameData.getTextByRefId("mapText47").ToUpper(CultureInfo.InvariantCulture), aArea);
			}
			break;
		}
		}
	}

	public void processHover(bool isOver, string gameObjectName, string islandInfo = "")
	{
		if (isOver)
		{
			string[] array = gameObjectName.Split('_');
			GameData gameData = game.getGameData();
			switch (array[0])
			{
			case "Mat":
			{
				Item itemByRefId = gameData.getItemByRefId(array[2]);
				string text = itemByRefId.getItemName() + "\n" + itemByRefId.getItemDesc() + "\n" + gameData.getTextByRefId("mapText13") + " " + itemByRefId.getItemNum();
				if (itemExploreList.ContainsKey(array[2]) && itemExploreList[array[2]].getFound())
				{
					if (selectedActivity == ActivityType.ActivityTypeExplore)
					{
						tooltipScript.showText(itemByRefId.getItemStandardTooltipString(areaList[selectedAreaIndex]));
					}
					else
					{
						tooltipScript.showText(itemByRefId.getItemStandardTooltipString());
					}
				}
				break;
			}
			case "AreaSmith":
			{
				Smith smithByRefId = gameData.getSmithByRefId(array[1]);
				tooltipScript.showText(smithByRefId.getSmithStandardInfoString(showFullJobDetails: false));
				break;
			}
			case "Hero":
			{
				Hero heroByHeroRefID = gameData.getHeroByHeroRefID(array[2]);
				heroTooltipScript.showText(heroByHeroRefID.getHeroStandardInfoString());
				break;
			}
			case "SmithEffectCollider":
			{
				string smithEffectString = getSmithEffectString(areaList[selectedAreaIndex]);
				if (smithEffectString != string.Empty)
				{
					tooltipScript.showText(smithEffectString);
				}
				break;
			}
			case "Area":
				if (!focusedMap)
				{
					commonScreenObject.findChild(GameObject.Find(gameObjectName), "AreaHighlights").GetComponent<SpriteRenderer>().enabled = true;
				}
				if (islandInfo != null && islandInfo != string.Empty && !focusedMap)
				{
					tooltipScript.showText(islandInfo);
				}
				break;
			case "AreaEventObj":
			{
				Area area = areaList[selectedAreaIndex];
				tooltipScript.showText(area.getCurrentEventTooltipInfo(game.getPlayer().getPlayerTimeLong()));
				break;
			}
			}
			return;
		}
		string[] array2 = gameObjectName.Split('_');
		string text2 = array2[0];
		if (text2 != null && text2 == "Area")
		{
			if (!focusedMap)
			{
				commonScreenObject.findChild(GameObject.Find(gameObjectName), "AreaHighlights").GetComponent<SpriteRenderer>().enabled = false;
			}
			tooltipScript.setInactive();
			heroTooltipScript.setInactive();
			islandTooltipScript.setInactive();
		}
		else
		{
			tooltipScript.setInactive();
			heroTooltipScript.setInactive();
			islandTooltipScript.setInactive();
		}
	}

	private void Update()
	{
		if (gameData != null && !tutorial && GameObject.Find("Panel_Tutorial") == null)
		{
			handleInput();
		}
	}

	private void handleInput()
	{
		if (mapCamera.getCameraEnabled() && buttonChangeActivity.gameObject.activeSelf && buttonChangeActivity.isEnabled && (Input.GetMouseButtonDown(1) || Input.GetKeyDown(gameData.getKeyCodeByRefID("100007"))))
		{
			processClick("Button_ChangeActivity");
		}
		else if (focusedMap && buttonBackWorldMap.gameObject.activeSelf && buttonBackWorldMap.isEnabled && (Input.GetMouseButton(1) || Input.GetKey(gameData.getKeyCodeByRefID("100007"))))
		{
			CommonAPI.debug("here");
			processClick("Button_BackWorldMap");
		}
		else if (focusedMap && button_confirm != null && Input.GetKeyDown(gameData.getKeyCodeByRefID("100008")))
		{
			processClick("Button_Confirm");
		}
		else if (mapCamera.getCameraEnabled() && unlockPanel != null && commonScreenObject.findChild(unlockPanel, "UnlockButton").gameObject.GetComponent<BoxCollider>() != null && Input.GetKeyDown(gameData.getKeyCodeByRefID("100008")))
		{
			processClick("UnlockButton");
		}
	}

	public void setReference()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		backWorkshopLabel.text = gameData.getTextByRefId("mapText48");
		backWorldMapLabel.text = gameData.getTextByRefId("mapText01");
		changeActivityLabel.text = gameData.getTextByRefId("mapText55");
		buttonBackWorldMap.gameObject.SetActive(value: false);
		buttonChangeActivity.gameObject.SetActive(value: true);
		buttonChangeActivity.isEnabled = false;
	}

	public void setPlayerDetails()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		playerName.text = player.getPlayerName();
		shopName.text = player.getShopName();
		lvlLabel.text = gameData.getTextByRefId("smithStatsShort01");
		shopLvlLabel.text = player.getShopLevelInt().ToString();
		mapMoneyLabel.text = CommonAPI.formatNumber(player.getPlayerGold()).ToString();
		mapFameLabel.text = CommonAPI.formatNumber(player.getFame());
		mapTicketLabel.text = player.getUnusedTickets().ToString();
		mapSmithLabel.text = player.getSmithList().Count + "/" + player.getShopMaxSmith();
	}

	public void createArea(ActivityType activityType, bool aTutorial = false)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		setPlayerDetails();
		areaEventHUDCtr.slideInPanel(transition: false);
		areaEventHUDCtr.refreshAreaEventHUD();
		mapCamera.setCameraEnabled(aCameraEnabled: false);
		mapCamera.setCameraVariables(gameData.getAreaRegionByRefID(player.getAreaRegion()));
		mapCamera.focusMapCameraOut(transition: false);
		mapCamera.zoomMapCameraOut(transition: false);
		mapBgFront.sprite = commonScreenObject.loadSprite("Image/Area/" + gameData.getAreaRegionByRefID(player.getAreaRegion()).getBgImg());
		Panel_MapSelectGrid.transform.localPosition = rightPanelOut;
		selectedActivity = activityType;
		areaList = gameData.getAreaListByRegion(player.getAreaRegion());
		tutorial = aTutorial;
		if (tutorial)
		{
			buttonBackWorkshop.isEnabled = false;
			buttonChangeActivity.isEnabled = false;
			buttonBackWorldMap.isEnabled = false;
		}
		else
		{
			buttonBackWorkshop.isEnabled = true;
			buttonChangeActivity.isEnabled = true;
			buttonBackWorldMap.isEnabled = true;
		}
		if (panel_MapAreaInfo == null)
		{
			panel_MapAreaInfo = commonScreenObject.createPrefab(GameObject.Find("Map_topLeft"), "Panel_MapAreaInfo", "Prefab/Area/NEW/Panel_MapAreaInfo", leftPanelOut, Vector3.one, Vector3.zero);
			GameObject aObject = GameObject.Find("MapAreaInfoNEW");
			areaTitle = commonScreenObject.findChild(aObject, "AreaTitle/AreaTitleLabel").GetComponent<UILabel>();
			areaEventIcon = commonScreenObject.findChild(aObject, "AreaTitle/AreaEvent").gameObject;
			areaInfo = commonScreenObject.findChild(aObject, "AreaInfo").GetComponent<UILabel>();
			smithEffectTitle = commonScreenObject.findChild(aObject, "SmithEffectTitle").GetComponent<UILabel>();
			smithEffectLabel = commonScreenObject.findChild(aObject, "SmithEffectLabel").GetComponent<UILabel>();
			travelTimeTitle = commonScreenObject.findChild(aObject, "TravelTimeTitle").GetComponent<UILabel>();
			travelTimeLabel = commonScreenObject.findChild(aObject, "TravelTimeLabel").GetComponent<UILabel>();
			subtitle = commonScreenObject.findChild(aObject, "Subtitle").gameObject;
			subtitleLabel = commonScreenObject.findChild(subtitle, "SubtitleLabel").GetComponent<UILabel>();
			heroGrid = commonScreenObject.findChild(aObject, "HeroGrid").GetComponent<UIGrid>();
			itemGrid = commonScreenObject.findChild(aObject, "ItemGrid").GetComponent<UIGrid>();
			exploreEnchantmentGrid = commonScreenObject.findChild(aObject, "Panel_ExploreEnchantment/ExploreEnchantmentGrid").GetComponent<UIGrid>();
			exploreEnchantmentLabel = commonScreenObject.findChild(aObject, "ExploreEnchantmentLabel").GetComponent<UILabel>();
			exploreMatGrid = commonScreenObject.findChild(aObject, "Panel_ExploreMat/ExploreMatGrid").GetComponent<UIGrid>();
			exploreMatLabel = commonScreenObject.findChild(aObject, "ExploreMatLabel").GetComponent<UILabel>();
			exploreRelicGrid = commonScreenObject.findChild(aObject, "Panel_ExploreRelic/ExploreRelicGrid").GetComponent<UIGrid>();
			exploreRelicLabel = commonScreenObject.findChild(aObject, "ExploreRelicLabel").GetComponent<UILabel>();
			seasonPackageFrame = commonScreenObject.findChild(aObject, "SeasonPackageFrame").gameObject;
			seasonPackageGrid = commonScreenObject.findChild(aObject, "SeasonPackageGrid").GetComponent<UIGrid>();
			otherPackageFrame = commonScreenObject.findChild(aObject, "OtherPackageFrame").gameObject;
			otherPackageGrid = commonScreenObject.findChild(aObject, "OtherPackageGrid").GetComponent<UIGrid>();
			seasonPackageFrame.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("mapText96").ToUpper(CultureInfo.InvariantCulture);
			otherPackageFrame.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("mapText97").ToUpper(CultureInfo.InvariantCulture);
			exploreItemIndex = 0;
			seasonPackageFrame.SetActive(value: false);
			otherPackageFrame.SetActive(value: false);
		}
		for (int i = 0; i < areaList.Count; i++)
		{
			spawnArea(areaList[i], activityType, i);
		}
	}

	private void spawnArea(Area oneArea, ActivityType activityType, int index)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		Vector2 areaCoordinate = oneArea.getAreaCoordinate();
		Vector3 aPosition = ((!(areaCoordinate == new Vector2(-1f, -1f))) ? gridController.getPosition((int)areaCoordinate.x, (int)areaCoordinate.y) : new Vector3(oneArea.getPosition().x, 0f, oneArea.getPosition().y));
		GameObject gameObject = commonScreenObject.createPrefab(Panel_Area, areaPrefix + index, "Prefab/Area/Area", aPosition, Vector3.one, Vector3.zero);
		gameObject.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Area/" + oneArea.getAreaImage());
		gameObject.transform.localRotation = Quaternion.Euler(35.264f, 45f, 0f);
		float scale = oneArea.getScale();
		gameObject.transform.localScale = new Vector3(scale, scale, scale);
		gameObject.AddComponent<BoxCollider>();
		gameObject.GetComponent<BoxCollider>().center = new Vector3(0f, 0f, 0f);
		if (!oneArea.checkIsUnlock())
		{
			if (activityType == ActivityType.ActivityTypeUnlock)
			{
				gameObject.GetComponent<IslandHoverScript>().setIslandLockedInfo(getUnlockAreaInfoString(oneArea));
				commonScreenObject.findChild(gameObject, "LockedBg").GetComponent<SpriteRenderer>().enabled = true;
				commonScreenObject.findChild(gameObject, "LockedBg/LockedIcon").GetComponent<SpriteRenderer>().enabled = true;
				commonScreenObject.findChild(gameObject, "TicketRequired").GetComponent<SpriteRenderer>().enabled = true;
				TextMesh component = commonScreenObject.findChild(gameObject, "TicketRequired/TicketRequiredLabel").GetComponent<TextMesh>();
				component.text = gameData.getTextByRefIdWithDynText("mapText71", "[TicketAmount]", oneArea.getUnlockTickets().ToString());
				component.GetComponent<Renderer>().sortingLayerName = "Character";
				component.GetComponent<Renderer>().sortingOrder = 3;
				if (Constants.LANGUAGE == LanguageType.kLanguageTypeRussia)
				{
					component.fontSize = 14;
				}
			}
			else
			{
				gameObject.name = lockedAreaPrefix + index;
				commonScreenObject.findChild(gameObject, "LockedBg").GetComponent<SpriteRenderer>().enabled = true;
				commonScreenObject.findChild(gameObject, "LockedBg/LockedIcon").GetComponent<SpriteRenderer>().enabled = true;
				if (tutorial)
				{
					Object.DestroyImmediate(gameObject.GetComponent<AreaClickScript>());
					gameObject.GetComponent<IslandHoverScript>().setIslandLockedInfo(gameData.getTextByRefId("mapText38"));
					gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
				}
			}
		}
		else if (oneArea.getAreaType() == AreaType.AreaTypeHome)
		{
			Object.DestroyImmediate(gameObject.GetComponent<AreaClickScript>());
			gameObject.GetComponent<IslandHoverScript>().setIslandLockedInfo(getWorkshopInfoString());
			gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
		}
		else
		{
			switch (activityType)
			{
			case ActivityType.ActivityTypeExplore:
				if (!oneArea.checkCanExplore())
				{
					Object.DestroyImmediate(gameObject.GetComponent<AreaClickScript>());
					gameObject.GetComponent<IslandHoverScript>().setIslandLockedInfo(gameData.getTextByRefId("mapText37"));
					gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
				}
				else
				{
					gameObject.GetComponent<IslandHoverScript>().setIslandLockedInfo(getExploreInfoString(oneArea));
				}
				break;
			case ActivityType.ActivityTypeSellWeapon:
				if (!oneArea.checkCanSell())
				{
					Object.DestroyImmediate(gameObject.GetComponent<AreaClickScript>());
					gameObject.GetComponent<IslandHoverScript>().setIslandLockedInfo(gameData.getTextByRefId("mapText38"));
					gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
				}
				else
				{
					gameObject.GetComponent<IslandHoverScript>().setIslandLockedInfo(getSellInfoString(oneArea));
				}
				break;
			case ActivityType.ActivityTypeBuyMats:
				if (!oneArea.checkCanBuy())
				{
					Object.DestroyImmediate(gameObject.GetComponent<AreaClickScript>());
					gameObject.GetComponent<IslandHoverScript>().setIslandLockedInfo(gameData.getTextByRefId("mapText39"));
					gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
				}
				else
				{
					gameObject.GetComponent<IslandHoverScript>().setIslandLockedInfo(getBuyInfoString(oneArea));
				}
				break;
			case ActivityType.ActivityTypeVacation:
			{
				Season seasonByMonth2 = CommonAPI.getSeasonByMonth(player.getSeasonIndex());
				VacationPackage vacationPackageByRefID = gameData.getVacationPackageByRefID(oneArea.getVacationPackageRefId());
				Vacation vacationByRefId = gameData.getVacationByRefId(vacationPackageByRefID.getVacationRefIDBySeason(seasonByMonth2));
				if (vacationByRefId.getVacationRefId() == string.Empty)
				{
					Object.DestroyImmediate(gameObject.GetComponent<AreaClickScript>());
					gameObject.GetComponent<IslandHoverScript>().setIslandLockedInfo(gameData.getTextByRefId("mapText40"));
					gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
				}
				else
				{
					gameObject.GetComponent<IslandHoverScript>().setIslandLockedInfo(getVacationInfoString(oneArea, vacationByRefId));
				}
				break;
			}
			case ActivityType.ActivityTypeTraining:
			{
				Season seasonByMonth = CommonAPI.getSeasonByMonth(player.getSeasonIndex());
				TrainingPackage trainingPackageByRefID = gameData.getTrainingPackageByRefID(oneArea.getTrainingPackageRefId());
				SmithTraining smithTrainingByRefId = gameData.getSmithTrainingByRefId(trainingPackageByRefID.getTrainingRefIDBySeason(seasonByMonth));
				if (smithTrainingByRefId.getSmithTrainingRefId() == string.Empty)
				{
					Object.DestroyImmediate(gameObject.GetComponent<AreaClickScript>());
					gameObject.GetComponent<IslandHoverScript>().setIslandLockedInfo(gameData.getTextByRefId("mapText41"));
					gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
				}
				break;
			}
			case ActivityType.ActivityTypeUnlock:
				gameObject.GetComponent<IslandHoverScript>().setIslandLockedInfo(getUnlockAreaInfoString(oneArea));
				break;
			}
		}
		List<string> areaSmithRefID = oneArea.getAreaSmithRefID(string.Empty);
		List<Vector3> list = new List<Vector3>();
		switch (areaSmithRefID.Count)
		{
		case 1:
			list = areaSmithPos1;
			break;
		case 2:
			list = areaSmithPos2;
			break;
		case 3:
			list = areaSmithPos3;
			break;
		}
		for (int i = 0; i < areaSmithRefID.Count; i++)
		{
			string text = areaSmithRefID[i];
			Smith smithByRefId = gameData.getSmithByRefId(text);
			GameObject aObject = commonScreenObject.createPrefab(gameObject, "AreaSmith_" + text, "Prefab/Area/SmithAreaBg", list[i], Vector3.one, Vector3.zero);
			commonScreenObject.findChild(aObject, "SmithImg").GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Smith/Portraits/" + smithByRefId.getImage());
		}
		gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
		areaGameobjectList.Add(oneArea.getAreaRefId(), gameObject);
		List<string> unlockAreaList = oneArea.getUnlockAreaList();
		if (unlockAreaList.Count <= 0)
		{
			return;
		}
		foreach (string item in unlockAreaList)
		{
			if (item != string.Empty)
			{
				AreaPath areaPathByStartEndAreaRefID = gameData.getAreaPathByStartEndAreaRefID(item, oneArea.getAreaRefId());
				spawnPath(areaPathByStartEndAreaRefID, oneArea);
			}
		}
	}

	private void spawnPath(AreaPath aPath, Area oneArea)
	{
		GameData gameData = game.getGameData();
		List<Vector2> pathList = aPath.getPathList();
		for (int i = 0; i < pathList.Count - 1; i++)
		{
			Vector3 centrePosition = gridController.getCentrePosition(pathList[i], pathList[i + 1]);
			Vector3 aRotation = new Vector3(35.264f, 45f, 0f);
			GameObject gameObject = commonScreenObject.createPrefab(Panel_Area, pathPrefix + aPath.getAreaPathRefID() + "_" + i + "_" + (i + 1), "Prefab/Area/AreaPathPanel", centrePosition, Vector3.one, aRotation);
			commonScreenObject.findChild(gameObject, "AreaPath").localRotation = Quaternion.Euler(0f, 0f, gridController.getRotation(pathList[i], pathList[i + 1]));
			commonScreenObject.findChild(gameObject, "AreaPathGreyed").localRotation = Quaternion.Euler(0f, 0f, gridController.getRotation(pathList[i], pathList[i + 1]));
			if (!oneArea.checkIsUnlock())
			{
				commonScreenObject.findChild(gameObject, "AreaPath").GetComponent<SpriteRenderer>().enabled = false;
			}
			pathGameObjectList.Add(gameObject);
		}
	}

	private void unlockPathByAreaRefID(string aAreaRefID)
	{
		GameData gameData = game.getGameData();
		Area areaByRefID = gameData.getAreaByRefID(aAreaRefID);
		List<string> unlockAreaList = areaByRefID.getUnlockAreaList();
		foreach (string item in unlockAreaList)
		{
			AreaPath areaPathByStartEndAreaRefID = gameData.getAreaPathByStartEndAreaRefID(item, aAreaRefID);
			List<GameObject> pathGameObjectByRefID = getPathGameObjectByRefID(areaPathByStartEndAreaRefID.getAreaPathRefID());
			foreach (GameObject item2 in pathGameObjectByRefID)
			{
				commonScreenObject.tweenAlpha(commonScreenObject.findChild(item2, "AreaPath").GetComponent<TweenAlpha>(), 0f, 1f, 4f, null, string.Empty);
			}
		}
	}

	public void spawnUI(ActivityType selectedActivity, int smithsInArea)
	{
		switch (selectedActivity)
		{
		case ActivityType.ActivityTypeSellWeapon:
		{
			GameObject gameObject6 = commonScreenObject.createPrefab(Panel_MapSelectGrid, "MapSelectSmithHeader", "Prefab/Area/NEW/MapSelectSmithNEW", Vector3.zero, Vector3.one, Vector3.zero);
			selectSmithCtr = gameObject6.GetComponent<GUIMapSelectSmithController>();
			selectSmithCtr.setReference(1, smithsInArea, preselectedSmith);
			GameObject gameObject7 = commonScreenObject.createPrefab(Panel_MapSelectGrid, "MapSellWeaponHeader", "Prefab/Area/NEW/MapSellWeaponHeaderNEW", Vector3.zero, Vector3.one, Vector3.zero);
			sellWeaponCtr = gameObject7.GetComponent<GUIMapSellWeaponController>();
			sellWeaponCtr.setReference();
			break;
		}
		case ActivityType.ActivityTypeBuyMats:
		{
			GameObject gameObject4 = commonScreenObject.createPrefab(Panel_MapSelectGrid, "MapSelectSmithHeader", "Prefab/Area/NEW/MapSelectSmithNEW", Vector3.zero, Vector3.one, Vector3.zero);
			selectSmithCtr = gameObject4.GetComponent<GUIMapSelectSmithController>();
			selectSmithCtr.setReference(1, smithsInArea, preselectedSmith);
			GameObject gameObject5 = commonScreenObject.createPrefab(Panel_MapSelectGrid, "MapBuyHeader", "Prefab/Area/NEW/MapBuyMatsHeaderNew", Vector3.zero, Vector3.one, Vector3.zero);
			buyMatCtr = gameObject5.GetComponent<GUIMapBuyMatController>();
			buyMatCtr.setReference(areaList[selectedAreaIndex]);
			break;
		}
		case ActivityType.ActivityTypeExplore:
		{
			GameObject gameObject3 = commonScreenObject.createPrefab(Panel_MapSelectGrid, "MapSelectSmithHeader", "Prefab/Area/NEW/MapSelectSmithTop", Vector3.zero, Vector3.one, Vector3.zero);
			selectSmithCtr = gameObject3.GetComponent<GUIMapSelectSmithController>();
			selectSmithCtr.setReference(3, smithsInArea, preselectedSmith);
			break;
		}
		case ActivityType.ActivityTypeVacation:
		{
			GameObject gameObject2 = commonScreenObject.createPrefab(Panel_MapSelectGrid, "MapSelectSmithHeader", "Prefab/Area/NEW/MapSelectSmithTop", Vector3.zero, Vector3.one, Vector3.zero);
			selectSmithCtr = gameObject2.GetComponent<GUIMapSelectSmithController>();
			selectSmithCtr.setReference(3, smithsInArea, preselectedSmith);
			break;
		}
		case ActivityType.ActivityTypeTraining:
		{
			GameObject gameObject = commonScreenObject.createPrefab(Panel_MapSelectGrid, "MapSelectSmithHeader", "Prefab/Area/NEW/MapSelectSmithTop", Vector3.zero, Vector3.one, Vector3.zero);
			selectSmithCtr = gameObject.GetComponent<GUIMapSelectSmithController>();
			selectSmithCtr.setReference(3, smithsInArea, preselectedSmith);
			break;
		}
		}
		slideOutPanel(transition: false);
	}

	public void destroyArea(bool setCameraVariable = false)
	{
		if (setCameraVariable)
		{
			mapCamera.setCameraVariables(game.getGameData().getAreaRegionByRefID(game.getPlayer().getAreaRegion()));
		}
		resetFocusCamera(transition: false);
		mapBgFront.sprite = null;
		selectedActivity = ActivityType.ActivityTypeBlank;
		foreach (KeyValuePair<string, GameObject> areaGameobject in areaGameobjectList)
		{
			commonScreenObject.destroyPrefabImmediate(areaGameobject.Value);
		}
		areaGameobjectList.Clear();
		foreach (GameObject pathGameObject in pathGameObjectList)
		{
			commonScreenObject.destroyPrefabImmediate(pathGameObject);
		}
		pathGameObjectList.Clear();
		foreach (GameObject areaInfoObj in areaInfoObjList)
		{
			commonScreenObject.destroyPrefabImmediate(areaInfoObj);
		}
		areaInfoObjList.Clear();
		areaInfoBuyObjList.Clear();
		areaBuyQtyList.Clear();
		itemExploreList = null;
		selectedAreaIndex = -1;
		if (panel_MapAreaInfo != null)
		{
			commonScreenObject.destroyPrefabImmediate(panel_MapAreaInfo);
		}
		if (selectSmithCtr != null)
		{
			commonScreenObject.destroyPrefabImmediate(selectSmithCtr.gameObject);
			selectSmithCtr = null;
		}
		if (sellWeaponCtr != null)
		{
			commonScreenObject.destroyPrefabImmediate(sellWeaponCtr.gameObject);
			sellWeaponCtr = null;
		}
		if (buyMatCtr != null)
		{
			commonScreenObject.destroyPrefabImmediate(buyMatCtr.gameObject);
			buyMatCtr = null;
		}
		if (button_confirm != null)
		{
			commonScreenObject.destroyPrefabImmediate(button_confirm);
			button_confirm = null;
		}
		areaEventHUDCtr.refreshAreaEventHUD();
	}

	public void selectArea(int aIndex)
	{
		CommonAPI.debug("selectArea: " + aIndex);
		if (aIndex == selectedAreaIndex)
		{
			return;
		}
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		if (selectedActivity == ActivityType.ActivityTypeUnlock)
		{
			CommonAPI.debug("area unlocked: " + areaList[aIndex].checkIsUnlock());
			if (!areaList[aIndex].checkIsUnlock())
			{
				selectedAreaIndex = aIndex;
				setUnlockPanel(areaList[selectedAreaIndex], areaGameobjectList[areaList[selectedAreaIndex].getAreaRefId()]);
				return;
			}
			if (unlockPanel != null)
			{
				commonScreenObject.destroyPrefabImmediate(unlockPanel);
			}
			setUnlockedPanel(areaList[aIndex], areaGameobjectList[areaList[aIndex].getAreaRefId()]);
			return;
		}
		if (selectedAreaIndex != -1)
		{
			GameObject aObject = areaGameobjectList[areaList[selectedAreaIndex].getAreaRefId()];
			commonScreenObject.findChild(aObject, "AreaHighlights").GetComponent<SpriteRenderer>().enabled = false;
		}
		selectedAreaIndex = aIndex;
		smithEffectTitle.text = gameData.getTextByRefId("mapText59");
		List<AreaStatus> areaStatusListByAreaAndSeason = gameData.getAreaStatusListByAreaAndSeason(areaList[selectedAreaIndex].getAreaRefId(), CommonAPI.getSeasonByMonth(player.getSeasonIndex()));
		string text = string.Empty;
		if (areaStatusListByAreaAndSeason.Count > 0)
		{
			bool flag = true;
			foreach (AreaStatus item in areaStatusListByAreaAndSeason)
			{
				if (item.getSmithEffectRefID() != "-1")
				{
					SmithStatusEffect smithStatusEffectByRefId = gameData.getSmithStatusEffectByRefId(item.getSmithEffectRefID());
					if (!flag)
					{
						text += ", ";
					}
					text += smithStatusEffectByRefId.getEffectName();
					if (flag)
					{
						flag = false;
					}
				}
			}
		}
		else
		{
			text = gameData.getTextByRefId("mapText67").ToUpper(CultureInfo.InvariantCulture);
		}
		smithEffectLabel.text = text;
		spawnUI(selectedActivity, areaList[selectedAreaIndex].getAreaSmithRefID(string.Empty).Count);
		GameObject aObject2 = areaGameobjectList[areaList[selectedAreaIndex].getAreaRefId()];
		commonScreenObject.findChild(aObject2, "AreaHighlights").GetComponent<SpriteRenderer>().enabled = true;
		VacationPackage vacationPackage = null;
		Vacation vacation = null;
		TrainingPackage trainingPackage = null;
		SmithTraining smithTraining = null;
		if (selectedActivity == ActivityType.ActivityTypeVacation)
		{
			Season seasonByMonth = CommonAPI.getSeasonByMonth(player.getSeasonIndex());
			vacationPackage = gameData.getVacationPackageByRefID(areaList[selectedAreaIndex].getVacationPackageRefId());
			vacation = gameData.getVacationByRefId(vacationPackage.getVacationRefIDBySeason(seasonByMonth));
			areaTitle.text = areaList[selectedAreaIndex].getAreaName();
			if (player.getPlayerGold() < vacation.getVacationCost())
			{
				canGo = false;
			}
		}
		else if (selectedActivity == ActivityType.ActivityTypeTraining)
		{
			Season seasonByMonth2 = CommonAPI.getSeasonByMonth(player.getSeasonIndex());
			trainingPackage = gameData.getTrainingPackageByRefID(areaList[selectedAreaIndex].getTrainingPackageRefId());
			smithTraining = gameData.getSmithTrainingByRefId(trainingPackage.getTrainingRefIDBySeason(seasonByMonth2));
			areaTitle.text = smithTraining.getSmithTrainingName();
			if (player.getPlayerGold() < smithTraining.getSmithTrainingCost())
			{
				canGo = false;
			}
		}
		else
		{
			areaTitle.text = areaList[selectedAreaIndex].getAreaName();
		}
		if (areaList[selectedAreaIndex].getCurrentEvent().getAreaEventRefId() == string.Empty)
		{
			areaEventIcon.SetActive(value: false);
		}
		else
		{
			areaEventIcon.SetActive(value: true);
		}
		player.setLastSelectArea(areaList[selectedAreaIndex]);
		if (areaInfoObjList.Count > 0)
		{
			foreach (GameObject areaInfoObj in areaInfoObjList)
			{
				commonScreenObject.destroyPrefabImmediate(areaInfoObj);
			}
			areaInfoObjList.Clear();
		}
		areaInfo.text = areaList[selectedAreaIndex].getAreaDesc();
		travelTimeTitle.text = gameData.getTextByRefId("mapText09").ToUpper(CultureInfo.InvariantCulture) + ": ";
		travelTimeLabel.text = getTravelTimeString();
		ActivityType activityType = selectedActivity;
		switch (activityType)
		{
		case ActivityType.ActivityTypeSellWeapon:
		{
			Area aArea = areaList[selectedAreaIndex];
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			Dictionary<string, int> heroList = aArea.getHeroList();
			Dictionary<string, int> rareHeroList = aArea.getRareHeroList();
			bool flag2 = false;
			if (aArea.getAreaRefId() == "101001")
			{
				GameObject gameObject5 = GameObject.Find("Panel_Tutorial");
				if (gameObject5 != null)
				{
					GUITutorialController component = gameObject5.GetComponent<GUITutorialController>();
					flag2 = component.checkCurrentTutorial("30003");
					if (flag2)
					{
						component.nextTutorial();
					}
				}
			}
			foreach (KeyValuePair<string, int> item2 in heroList)
			{
				dictionary.Add(item2.Key, item2.Value);
			}
			foreach (KeyValuePair<string, int> item3 in rareHeroList)
			{
				dictionary.Add(item3.Key, item3.Value);
			}
			List<Hero> list = new List<Hero>();
			foreach (KeyValuePair<string, int> item4 in dictionary)
			{
				Hero heroByHeroRefID = gameData.getHeroByHeroRefID(item4.Key);
				if (!flag2 || item4.Key == "10011" || item4.Key == "10022")
				{
					list.Add(heroByHeroRefID);
				}
			}
			int num3 = 0;
			foreach (Hero item5 in list.OrderByDescending((Hero i) => i.getAppearancePercentInArea(aArea)))
			{
				GameObject gameObject6 = commonScreenObject.createPrefab(heroGrid.gameObject, "Hero_" + num3 + "_" + item5.getHeroRefId(), "Prefab/Area/NEW/MapHeroObjectNEW", Vector3.zero, Vector3.one, Vector3.zero);
				if (item5.getHeroLevel() == item5.getHeroMaxLevel())
				{
					commonScreenObject.findChild(gameObject6, "HeroExp").GetComponent<UIProgressBar>().value = 1f;
				}
				else
				{
					commonScreenObject.findChild(gameObject6, "HeroExp").GetComponent<UIProgressBar>().value = gameData.getExpPercent(item5.getExpPoints());
				}
				commonScreenObject.findChild(gameObject6, "HeroExp/HeroLvlLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("smithStatsShort01") + ". " + item5.getHeroLevel();
				if (item5.getHeroLevel() == item5.getHeroMaxLevel())
				{
					UILabel component2 = commonScreenObject.findChild(gameObject6, "HeroExp/HeroLvlLabel").GetComponent<UILabel>();
					component2.text = component2.text + " (" + gameData.getTextByRefId("mapText18").ToUpper(CultureInfo.InvariantCulture) + ")";
				}
				commonScreenObject.findChild(gameObject6, "HeroImg").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Hero/" + item5.getImage());
				commonScreenObject.findChild(gameObject6, "HeroName").GetComponent<UILabel>().text = item5.getHeroName();
				commonScreenObject.findChild(gameObject6, "HeroClass").GetComponent<UILabel>().text = item5.getJobClassName();
				areaInfoObjList.Add(gameObject6);
				num3++;
			}
			subtitleLabel.text = gameData.getTextByRefId("mapText30") + ": ";
			heroGrid.Reposition();
			break;
		}
		case ActivityType.ActivityTypeBuyMats:
			subtitleLabel.text = string.Empty;
			subtitle.SetActive(value: false);
			break;
		case ActivityType.ActivityTypeExplore:
		{
			itemExploreList = areaList[selectedAreaIndex].getExploreItemList();
			Dictionary<string, ExploreItem> dictionary2 = new Dictionary<string, ExploreItem>();
			Dictionary<string, ExploreItem> dictionary3 = new Dictionary<string, ExploreItem>();
			Dictionary<string, ExploreItem> dictionary4 = new Dictionary<string, ExploreItem>();
			foreach (KeyValuePair<string, ExploreItem> itemExplore in itemExploreList)
			{
				Item itemByRefId = gameData.getItemByRefId(itemExplore.Key);
				if (checkScenarioAllow(itemByRefId))
				{
					switch (itemByRefId.getItemType())
					{
					case ItemType.ItemTypeEnhancement:
						dictionary3.Add(itemExplore.Key, itemExplore.Value);
						break;
					case ItemType.ItemTypeMaterial:
						dictionary2.Add(itemExplore.Key, itemExplore.Value);
						break;
					case ItemType.ItemTypeRelic:
						dictionary4.Add(itemExplore.Key, itemExplore.Value);
						break;
					}
				}
			}
			if (dictionary2.Count > 0)
			{
				foreach (KeyValuePair<string, ExploreItem> item6 in dictionary2.OrderByDescending((KeyValuePair<string, ExploreItem> i) => i.Value.getProbability()))
				{
					processExploreItem(item6);
					exploreItemIndex++;
				}
				exploreMatLabel.text = gameData.getTextByRefId("mapText11");
				exploreMatGrid.GetComponent<UIGrid>().Reposition();
				exploreMatGrid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
				exploreMatGrid.transform.parent.GetComponent<UIScrollView>().verticalScrollBar.value = 0f;
			}
			else
			{
				exploreMatLabel.text = string.Empty;
			}
			if (dictionary3.Count > 0)
			{
				foreach (KeyValuePair<string, ExploreItem> item7 in dictionary3.OrderByDescending((KeyValuePair<string, ExploreItem> i) => i.Value.getProbability()))
				{
					processExploreItem(item7);
					exploreItemIndex++;
				}
				exploreEnchantmentLabel.text = gameData.getTextByRefId("mapText10");
				exploreEnchantmentGrid.GetComponent<UIGrid>().Reposition();
				exploreEnchantmentGrid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
				exploreEnchantmentGrid.transform.parent.GetComponent<UIScrollView>().verticalScrollBar.value = 0f;
			}
			else
			{
				exploreEnchantmentLabel.text = string.Empty;
			}
			if (dictionary4.Count > 0)
			{
				foreach (KeyValuePair<string, ExploreItem> item8 in dictionary4.OrderByDescending((KeyValuePair<string, ExploreItem> i) => i.Value.getProbability()))
				{
					processExploreItem(item8);
					exploreItemIndex++;
				}
				exploreRelicLabel.text = gameData.getTextByRefId("mapText12");
				exploreRelicGrid.GetComponent<UIGrid>().Reposition();
				exploreRelicGrid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
				exploreRelicGrid.transform.parent.GetComponent<UIScrollView>().verticalScrollBar.value = 0f;
			}
			else
			{
				exploreRelicLabel.text = string.Empty;
			}
			subtitleLabel.text = gameData.getTextByRefId("mapText14").ToUpper(CultureInfo.InvariantCulture) + ": ";
			break;
		}
		case ActivityType.ActivityTypeVacation:
		{
			seasonPackageFrame.SetActive(value: true);
			otherPackageFrame.SetActive(value: true);
			subtitle.SetActive(value: false);
			List<string> allRefID2 = vacationPackage.getAllRefID();
			GameObject gameObject3 = null;
			int num2 = 0;
			foreach (string item9 in allRefID2)
			{
				if (!(item9 != "-1"))
				{
					continue;
				}
				Vacation vacationByRefId = gameData.getVacationByRefId(item9);
				gameObject3 = ((!(item9 == vacation.getVacationRefId())) ? otherPackageGrid.gameObject : seasonPackageGrid.gameObject);
				num2 = getSeasonOrder(vacationPackage.checkSeason(item9));
				GameObject gameObject4 = commonScreenObject.createPrefab(gameObject3, "Vacation" + num2 + "_" + vacationByRefId.getVacationRefId(), "Prefab/Area/NEW/MapVacationObject", Vector3.zero, Vector3.one, Vector3.zero);
				commonScreenObject.findChild(gameObject4, "CostLabel").GetComponent<UILabel>().text = CommonAPI.formatNumber(vacationByRefId.getVacationCost()).ToString();
				commonScreenObject.findChild(gameObject4, "VacationTitle").GetComponent<UILabel>().text = vacationByRefId.getVacationName();
				commonScreenObject.findChild(gameObject4, "VacationDesc").GetComponent<UILabel>().text = vacationByRefId.getVacationDesc();
				if (item9 == vacation.getVacationRefId())
				{
					commonScreenObject.findChild(gameObject4, "SeasonVacationFrame").GetComponent<UISprite>().enabled = true;
					commonScreenObject.findChild(gameObject4, "greyOverlay").GetComponent<UISprite>().enabled = false;
					commonScreenObject.findChild(gameObject4, "SeasonBg").GetComponent<UISprite>().color = trainVacationColor;
				}
				else
				{
					commonScreenObject.findChild(gameObject4, "SeasonVacationFrame").GetComponent<UISprite>().enabled = false;
					commonScreenObject.findChild(gameObject4, "greyOverlay").GetComponent<UISprite>().enabled = true;
				}
				GameObject aObject3 = commonScreenObject.findChild(gameObject4, "Panel_Rating").gameObject;
				int vacationPackageRating = CommonAPI.getVacationPackageRating((int)vacationByRefId.getVacationMoodAdd(), player.getShopLevelInt());
				for (int j = 1; j <= 5; j++)
				{
					if (j <= vacationPackageRating)
					{
						commonScreenObject.findChild(aObject3, "Rating" + j).GetComponent<UISprite>().spriteName = "smile_filled";
					}
					else
					{
						commonScreenObject.findChild(aObject3, "Rating" + j).GetComponent<UISprite>().spriteName = "smile_empty";
					}
				}
				commonScreenObject.findChild(gameObject4, "VacationDesc").GetComponent<UILabel>().text = vacationByRefId.getVacationDesc();
				commonScreenObject.findChild(gameObject4, "SeasonBg/SeasonName").GetComponent<UILabel>().text = CommonAPI.convertSeasonToString(vacationPackage.checkSeason(item9)).ToUpper(CultureInfo.InvariantCulture);
				areaInfoObjList.Add(gameObject4);
			}
			otherPackageGrid.Reposition();
			break;
		}
		case ActivityType.ActivityTypeTraining:
		{
			seasonPackageFrame.SetActive(value: true);
			otherPackageFrame.SetActive(value: true);
			subtitle.SetActive(value: false);
			List<string> allRefID = trainingPackage.getAllRefID();
			GameObject gameObject = null;
			int num = 0;
			foreach (string item10 in allRefID)
			{
				if (item10 != "-1")
				{
					SmithTraining smithTrainingByRefId = gameData.getSmithTrainingByRefId(item10);
					gameObject = ((!(item10 == smithTraining.getSmithTrainingRefId())) ? otherPackageGrid.gameObject : seasonPackageGrid.gameObject);
					num = getSeasonOrder(trainingPackage.checkSeason(item10));
					GameObject gameObject2 = commonScreenObject.createPrefab(gameObject, "Training" + num + "_" + smithTrainingByRefId.getSmithTrainingRefId(), "Prefab/Area/NEW/MapTrainingObject", Vector3.zero, Vector3.one, Vector3.zero);
					commonScreenObject.findChild(gameObject2, "CostLabel").GetComponent<UILabel>().text = CommonAPI.formatNumber(smithTrainingByRefId.getSmithTrainingCost()).ToString();
					commonScreenObject.findChild(gameObject2, "TrainingTitle").GetComponent<UILabel>().text = smithTrainingByRefId.getSmithTrainingName();
					commonScreenObject.findChild(gameObject2, "TrainingDesc").GetComponent<UILabel>().text = smithTrainingByRefId.getSmithTrainingDesc();
					if (item10 == smithTraining.getSmithTrainingRefId())
					{
						commonScreenObject.findChild(gameObject2, "SeasonVacationFrame").GetComponent<UISprite>().enabled = true;
						commonScreenObject.findChild(gameObject2, "SeasonBg").GetComponent<UISprite>().color = trainVacationColor;
					}
					else
					{
						commonScreenObject.findChild(gameObject2, "SeasonVacationFrame").GetComponent<UISprite>().enabled = false;
					}
					commonScreenObject.findChild(gameObject2, "SeasonBg/SeasonName").GetComponent<UILabel>().text = CommonAPI.convertSeasonToString(trainingPackage.checkSeason(item10)).ToUpper(CultureInfo.InvariantCulture);
					commonScreenObject.findChild(gameObject2, "Panel_Exp/ExpGainAmt").GetComponent<UILabel>().text = CommonAPI.formatNumber(smithTrainingByRefId.getSmithTrainingExp()) + " " + gameData.getTextByRefId("smithStatsShort08").ToUpper(CultureInfo.InvariantCulture);
					areaInfoObjList.Add(gameObject2);
				}
			}
			otherPackageGrid.Reposition();
			break;
		}
		}
		buttonBackWorldMap.gameObject.SetActive(value: true);
		buttonChangeActivity.gameObject.SetActive(value: false);
		focusOnSelectedArea();
		areaEventHUDCtr.slideOutPanel();
		slideInPanel();
	}

	private void processExploreItem(KeyValuePair<string, ExploreItem> aExploreItem)
	{
		GameData gameData = game.getGameData();
		Item itemByRefId = gameData.getItemByRefId(aExploreItem.Key);
		string text = "Image/";
		UIGrid uIGrid = null;
		switch (itemByRefId.getItemType())
		{
		case ItemType.ItemTypeEnhancement:
			text += "Enchantment/";
			uIGrid = exploreEnchantmentGrid;
			break;
		case ItemType.ItemTypeMaterial:
			text += "materials/";
			uIGrid = exploreMatGrid;
			break;
		case ItemType.ItemTypeRelic:
			text += "relics/";
			uIGrid = exploreRelicGrid;
			break;
		}
		GameObject gameObject = commonScreenObject.createPrefab(uIGrid.gameObject, "Mat_" + exploreItemIndex + "_" + itemByRefId.getItemRefId(), "Prefab/Area/NEW/MapExploreObject", Vector3.zero, Vector3.one, Vector3.zero);
		commonScreenObject.findChild(gameObject, "ItemFrame/RarityFrame").GetComponent<UISprite>().color = CommonAPI.getRarityColor(aExploreItem.Value, itemByRefId.getItemType());
		if (aExploreItem.Value.getFound())
		{
			commonScreenObject.findChild(gameObject, "ItemFrame/ItemImg").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture(text + itemByRefId.getImage());
		}
		else
		{
			commonScreenObject.findChild(gameObject, "ItemFrame/ItemImg").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Area/question_box");
		}
		areaInfoObjList.Add(gameObject);
	}

	private bool checkArea()
	{
		if (areaList[selectedAreaIndex].checkAreaSmithExceed())
		{
			return false;
		}
		return true;
	}

	public void unlockArea()
	{
		Player player = game.getPlayer();
		GameObject gameObject = areaGameobjectList[areaList[selectedAreaIndex].getAreaRefId()];
		Area area = areaList[selectedAreaIndex];
		player.tryUseTicket(area.getUnlockTickets());
		GameObject.Find("MapTicketLabel").GetComponent<UILabel>().text = player.getUnusedTickets().ToString();
		area.setUnlock(aUnlock: true);
		gameObject.GetComponent<IslandHoverScript>().setIslandLockedInfo(getUnlockAreaInfoString(area));
		GameObject gameObject2 = commonScreenObject.findChild(gameObject, "LockedBg").gameObject;
		commonScreenObject.findChild(gameObject2, "LockedIcon").GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Area/lock_Unlocked");
		commonScreenObject.tweenScale(gameObject2.GetComponent<TweenScale>(), new Vector3(3f, 3f, 3f), new Vector3(6f, 6f, 6f), 1f, null, string.Empty);
		TweenAlpha[] componentsInChildren = gameObject2.GetComponentsInChildren<TweenAlpha>();
		TweenAlpha[] array = componentsInChildren;
		foreach (TweenAlpha aTween in array)
		{
			commonScreenObject.tweenAlpha(aTween, 1f, 0f, 1f, null, string.Empty);
		}
		commonScreenObject.findChild(gameObject, "TicketRequired").GetComponent<SpriteRenderer>().enabled = false;
		commonScreenObject.findChild(gameObject, "TicketRequired/TicketRequiredLabel").GetComponent<TextMesh>().text = string.Empty;
		if (unlockPanel != null)
		{
			commonScreenObject.destroyPrefabImmediate(unlockPanel);
		}
		gameObject.name = areaPrefix + selectedAreaIndex;
		unlockPathByAreaRefID(area.getAreaRefId());
		selectedAreaIndex = -1;
	}

	public void focusOnSelectedArea()
	{
		focusedMap = true;
		mapCamera.setCameraEnabled(aCameraEnabled: false);
		GameObject gameObject = areaGameobjectList[areaList[selectedAreaIndex].getAreaRefId()];
		Vector3 localPosition = gameObject.transform.localPosition;
		Vector3 aEndPosition = localPosition;
		aEndPosition.y -= 0.2f;
		commonScreenObject.tweenPosition(gameObject.GetComponent<TweenPosition>(), localPosition, aEndPosition, 2f, null, string.Empty);
		mapCamera.focusMapCameraOn(gameObject);
		mapCamera.zoomMapCameraIn();
	}

	private void resetFocusCamera(bool transition)
	{
		focusedMap = false;
		mapCamera.setCameraEnabled(aCameraEnabled: true);
		if (selectedAreaIndex != -1)
		{
			GameObject gameObject = areaGameobjectList[areaList[selectedAreaIndex].getAreaRefId()];
			gameObject.GetComponent<TweenPosition>().enabled = false;
			gameObject.GetComponent<TweenPosition>().ResetToBeginning();
			GameObject aObject = areaGameobjectList[areaList[selectedAreaIndex].getAreaRefId()];
			commonScreenObject.findChild(aObject, "AreaHighlights").GetComponent<SpriteRenderer>().enabled = false;
			selectedAreaIndex = -1;
		}
		mapCamera.focusMapCameraOut(transition);
		mapCamera.zoomMapCameraOut(transition);
	}

	private void slideOutAreaInfoPanel()
	{
		exploreEnchantmentLabel.text = string.Empty;
		exploreMatLabel.text = string.Empty;
		exploreRelicLabel.text = string.Empty;
		commonScreenObject.tweenPosition(panel_MapAreaInfo.GetComponent<TweenPosition>(), Vector3.zero, rightPanelOut, 0.5f, null, string.Empty);
	}

	public void slideInPanel()
	{
		commonScreenObject.tweenPosition(Panel_MapSelectGrid.GetComponent<TweenPosition>(), rightPanelOut, Vector3.zero, 0.5f, null, string.Empty);
		commonScreenObject.tweenPosition(panel_MapAreaInfo.GetComponent<TweenPosition>(), leftPanelOut, Vector3.zero, 0.5f, null, string.Empty);
		setConfirmButton();
	}

	private void slideOutPanel(bool transition = true)
	{
		if (transition)
		{
			commonScreenObject.tweenPosition(Panel_MapSelectGrid.GetComponent<TweenPosition>(), Vector3.zero, rightPanelOut, 0.5f, null, string.Empty);
			commonScreenObject.tweenPosition(panel_MapAreaInfo.GetComponent<TweenPosition>(), Vector3.zero, leftPanelOut, 0.5f, null, string.Empty);
		}
		else
		{
			Panel_MapSelectGrid.transform.localPosition = leftPanelOut;
		}
	}

	private bool checkSmithInShop(List<Smith> aSmithList)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		List<Smith> inShopSmithList = player.getInShopSmithList();
		int num = inShopSmithList.Count;
		foreach (Smith aSmith in aSmithList)
		{
			SmithAction smithAction = aSmith.getSmithAction();
			if (smithAction.getRefId() != "905" && num <= 1)
			{
				string textByRefId = gameData.getTextByRefId("menuSmithManagement05");
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, string.Empty, textByRefId, PopupType.PopupTypeNothing, null, colorTag: false, null, map: true, string.Empty);
				return false;
			}
			if (smithAction.getRefId() != "905")
			{
				num--;
			}
		}
		return true;
	}

	private bool checkSmithArea(int addCount)
	{
		Area lastSelectArea = game.getPlayer().getLastSelectArea();
		if (lastSelectArea.getAreaSmithRefID(string.Empty).Count + addCount <= 3)
		{
			return true;
		}
		string textByRefId = game.getGameData().getTextByRefId("menuSmithManagement53");
		viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, string.Empty, textByRefId, PopupType.PopupTypeNothing, null, colorTag: false, null, map: true, string.Empty);
		return false;
	}

	public void startBuyMats()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		Dictionary<string, ShopItem> dictionary = buyMatCtr.getBuyItemList();
		Dictionary<string, int> qtyList = buyMatCtr.getQtyList();
		List<string> list = new List<string>();
		int num = 0;
		foreach (KeyValuePair<string, ShopItem> item in dictionary)
		{
			list.Add(item.Key + "@" + qtyList[item.Key]);
			num += item.Value.getCost() * qtyList[item.Key];
		}
		player.reduceGold(num, allowNegative: true);
		player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseBuyItem, string.Empty, -1 * num);
		audioController.playPurchaseAudio();
		List<Smith> selectedSmith = selectSmithCtr.getSelectedSmith();
		if (selectedSmith.Count > 0)
		{
			Smith smith = selectedSmith[0];
			SmithAction smithActionByRefId = gameData.getSmithActionByRefId("902");
			Area lastSelectArea = player.getLastSelectArea();
			lastSelectArea.addTimesBuyItems(1);
			Season seasonByMonth = CommonAPI.getSeasonByMonth(player.getSeasonIndex());
			List<AreaStatus> areaStatusListByAreaAndSeason = gameData.getAreaStatusListByAreaAndSeason(lastSelectArea.getAreaRefId(), seasonByMonth);
			List<int> list2 = new List<int>();
			list2.Add(lastSelectArea.getTravelTime());
			list2.Add(3);
			list2.Add(lastSelectArea.getTravelTime());
			list2.Add(-1);
			List<SmithExploreState> list3 = new List<SmithExploreState>();
			list3.Add(SmithExploreState.SmithExploreStateTravelToBuyMaterial);
			list3.Add(SmithExploreState.SmithExploreStateBuyMaterial);
			list3.Add(SmithExploreState.SmithExploreStateBuyMaterialTravelHome);
			list3.Add(SmithExploreState.SmithExploreStateBuyMaterialReturned);
			smith.setSmithAction(smithActionByRefId, lastSelectArea.getTravelTime() * 2 + 3);
			smith.setExploreStateList(list3, list2);
			smith.setExploreArea(lastSelectArea);
			smith.setExploreTask(list);
			smith.setAreaStatusList(areaStatusListByAreaAndSeason);
			lastSelectArea.addAreaSmithRefID(smith.getSmithRefId());
		}
		player.clearLastSelectArea();
		if (fromAssign)
		{
			viewController.closeWorldMap(hide: true, resume: false, resumeFromPlayerPause: true);
			animClickController.closeSmithActionMenu();
		}
		else
		{
			viewController.closeWorldMap(hide: true, resume: true, resumeFromPlayerPause: true);
		}
		fromAssign = false;
		GameObject.Find("Panel_ActivitySelect").GetComponent<GUIActivitySelectController>().refreshButtons();
	}

	public void startVacation()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		Area lastSelectArea = player.getLastSelectArea();
		List<Smith> selectedSmith = selectSmithCtr.getSelectedSmith();
		foreach (Smith item in selectedSmith)
		{
			lastSelectArea.addTimesVacation(1);
			Season seasonByMonth = CommonAPI.getSeasonByMonth(player.getSeasonIndex());
			VacationPackage vacationPackageByRefID = gameData.getVacationPackageByRefID(lastSelectArea.getVacationPackageRefId());
			Vacation vacationByRefId = gameData.getVacationByRefId(vacationPackageByRefID.getVacationRefIDBySeason(seasonByMonth));
			int vacationCost = vacationByRefId.getVacationCost();
			player.reduceGold(vacationCost, allowNegative: true);
			audioController.playPurchaseAudio();
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseVacation, string.Empty, -1 * vacationCost);
			SmithAction smithActionByRefId = gameData.getSmithActionByRefId("906");
			List<int> list = new List<int>();
			list.Add(lastSelectArea.getTravelTime());
			list.Add(24);
			list.Add(lastSelectArea.getTravelTime());
			list.Add(-1);
			List<SmithExploreState> list2 = new List<SmithExploreState>();
			list2.Add(SmithExploreState.SmithExploreStateTravelToVacation);
			list2.Add(SmithExploreState.SmithExploreStateVacation);
			list2.Add(SmithExploreState.SmithExploreStateVacationTravelHome);
			list2.Add(SmithExploreState.SmithExploreStateVacationReturned);
			List<AreaStatus> areaStatusListByAreaAndSeason = gameData.getAreaStatusListByAreaAndSeason(lastSelectArea.getAreaRefId(), seasonByMonth);
			lastSelectArea.addAreaSmithRefID(item.getSmithRefId());
			item.setSmithAction(smithActionByRefId, lastSelectArea.getTravelTime() * 2 + 24);
			item.setExploreStateList(list2, list);
			item.setExploreArea(lastSelectArea);
			item.setVacation(vacationByRefId);
			item.setAreaStatusList(areaStatusListByAreaAndSeason);
		}
		player.clearLastSelectSmith();
		player.clearLastSelectArea();
		if (fromAssign)
		{
			viewController.closeWorldMap(hide: true, resume: false, resumeFromPlayerPause: true);
			animClickController.closeSmithActionMenu();
		}
		else
		{
			viewController.closeWorldMap(hide: true, resume: true, resumeFromPlayerPause: true);
		}
		fromAssign = false;
	}

	public void startTraining()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		Area area = areaList[selectedAreaIndex];
		List<Smith> selectedSmith = selectSmithCtr.getSelectedSmith();
		foreach (Smith item in selectedSmith)
		{
			area.addTimesTrain(1);
			player.clearLastSelectSmith();
			Season seasonByMonth = CommonAPI.getSeasonByMonth(player.getSeasonIndex());
			TrainingPackage trainingPackageByRefID = gameData.getTrainingPackageByRefID(area.getTrainingPackageRefId());
			SmithTraining smithTrainingByRefId = gameData.getSmithTrainingByRefId(trainingPackageByRefID.getTrainingRefIDBySeason(seasonByMonth));
			int smithTrainingCost = smithTrainingByRefId.getSmithTrainingCost();
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
			area.addAreaSmithRefID(item.getSmithRefId());
			item.setSmithAction(smithActionByRefId, area.getTravelTime() * 2 + 10);
			item.setExploreStateList(list2, list);
			item.setExploreArea(area);
			item.setTraining(smithTrainingByRefId);
			item.setAreaStatusList(areaStatusListByAreaAndSeason);
		}
		if (fromAssign)
		{
			viewController.closeWorldMap(hide: true, resume: false, resumeFromPlayerPause: true);
			animClickController.closeSmithActionMenu();
		}
		else
		{
			viewController.closeWorldMap(hide: true, resume: true, resumeFromPlayerPause: true);
		}
		fromAssign = false;
	}

	private void startActivity()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		Smith smith = null;
		Area area = null;
		SmithAction smithAction = null;
		audioController.playMapConfirmAudio();
		switch (selectedActivity)
		{
		case ActivityType.ActivityTypeSellWeapon:
		{
			List<Smith> list5 = selectSmithCtr.getSelectedSmith();
			if (list5.Count > 0)
			{
				smith = list5[0];
				if (list5.Count > 1)
				{
					list5 = new List<Smith>();
					list5.Add(smith);
				}
			}
			if (smith == null || !checkSmithInShop(list5) || !checkSmithArea(list5.Count))
			{
				break;
			}
			List<Project> selectedProjectList = sellWeaponCtr.getSelectedProjectList();
			List<string> list6 = new List<string>();
			for (int i = 0; i < selectedProjectList.Count; i++)
			{
				list6.Add(selectedProjectList[i].getProjectId());
				selectedProjectList[i].setProjectSaleState(ProjectSaleState.ProjectSaleStateSelling);
			}
			smithAction = gameData.getSmithActionByRefId("903");
			area = player.getLastSelectArea();
			area.addTimesSell(1);
			List<int> list7 = new List<int>();
			list7.Add(area.getTravelTime());
			list7.Add(5);
			list7.Add(-1);
			list7.Add(area.getTravelTime());
			list7.Add(-1);
			List<SmithExploreState> list8 = new List<SmithExploreState>();
			list8.Add(SmithExploreState.SmithExploreStateTravelToSellWeapon);
			list8.Add(SmithExploreState.SmithExploreStateSellWeapon);
			list8.Add(SmithExploreState.SmithExploreStateOffersWaiting);
			list8.Add(SmithExploreState.SmithExploreStateSellWeaponTravelHome);
			list8.Add(SmithExploreState.SmithExploreStateSellWeaponReturned);
			Season seasonByMonth4 = CommonAPI.getSeasonByMonth(player.getSeasonIndex());
			List<AreaStatus> areaStatusListByAreaAndSeason2 = gameData.getAreaStatusListByAreaAndSeason(area.getAreaRefId(), seasonByMonth4);
			smith.setExploreArea(area);
			smith.setSmithAction(smithAction, area.getTravelTime() * 2 + 5);
			smith.setExploreStateList(list8, list7);
			smith.setExploreTask(list6);
			smith.setAreaStatusList(areaStatusListByAreaAndSeason2);
			player.clearLastSelectArea();
			area.addAreaSmithRefID(smith.getSmithRefId());
			bool flag = false;
			GameObject gameObject = GameObject.Find("Panel_Tutorial");
			if (gameObject != null)
			{
				GUITutorialController component = gameObject.GetComponent<GUITutorialController>();
				if (component.checkCurrentTutorial("30004"))
				{
					component.nextTutorial();
				}
			}
			if (fromAssign)
			{
				viewController.closeWorldMap(hide: true, resume: false, resumeFromPlayerPause: true);
				animClickController.closeSmithActionMenu();
			}
			else
			{
				viewController.closeWorldMap(hide: true, resume: true, resumeFromPlayerPause: true);
			}
			fromAssign = false;
			GameObject.Find("Panel_ActivitySelect").GetComponent<GUIActivitySelectController>().refreshButtons();
			break;
		}
		case ActivityType.ActivityTypeBuyMats:
		{
			Dictionary<string, ShopItem> dictionary = buyMatCtr.getBuyItemList();
			Dictionary<string, int> qtyList = buyMatCtr.getQtyList();
			List<string> list = new List<string>();
			int num3 = 0;
			foreach (KeyValuePair<string, ShopItem> item in dictionary)
			{
				list.Add(item.Key + "@" + qtyList[item.Key]);
				num3 += item.Value.getCost() * qtyList[item.Key];
			}
			if (player.getPlayerGold() < num3)
			{
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeYesAndNo, resume: false, string.Empty, gameData.getTextByRefId("mapText45"), PopupType.PopupTypeMapBuyMats, null, colorTag: false, null, map: true, string.Empty);
				break;
			}
			List<Smith> list2 = selectSmithCtr.getSelectedSmith();
			if (list2.Count > 0)
			{
				smith = list2[0];
				if (list2.Count > 1)
				{
					list2 = new List<Smith>();
					list2.Add(smith);
				}
			}
			if (smith != null && checkSmithInShop(list2) && checkSmithArea(list2.Count))
			{
				startBuyMats();
			}
			break;
		}
		case ActivityType.ActivityTypeExplore:
		{
			List<Smith> selectedSmith3 = selectSmithCtr.getSelectedSmith();
			if (!checkSmithInShop(selectedSmith3) || !checkSmithArea(selectedSmith3.Count))
			{
				break;
			}
			area = player.getLastSelectArea();
			smithAction = gameData.getSmithActionByRefId("901");
			foreach (Smith item2 in selectedSmith3)
			{
				area.addTimesExplored(1);
				List<int> list3 = new List<int>();
				list3.Add(area.getTravelTime());
				list3.Add(10);
				list3.Add(area.getTravelTime());
				list3.Add(-1);
				List<SmithExploreState> list4 = new List<SmithExploreState>();
				list4.Add(SmithExploreState.SmithExploreStateTravelToExplore);
				list4.Add(SmithExploreState.SmithExploreStateExplore);
				list4.Add(SmithExploreState.SmithExploreStateExploreTravelHome);
				list4.Add(SmithExploreState.SmithExploreStateExploreReturned);
				Season seasonByMonth3 = CommonAPI.getSeasonByMonth(player.getSeasonIndex());
				List<AreaStatus> areaStatusListByAreaAndSeason = gameData.getAreaStatusListByAreaAndSeason(area.getAreaRefId(), seasonByMonth3);
				item2.setSmithAction(smithAction, area.getTravelTime() * 2 + 10);
				item2.setExploreStateList(list4, list3);
				item2.setExploreArea(area);
				item2.setAreaStatusList(areaStatusListByAreaAndSeason);
				player.clearLastSelectArea();
				area.addAreaSmithRefID(item2.getSmithRefId());
			}
			if (fromAssign)
			{
				viewController.closeWorldMap(hide: true, resume: false, resumeFromPlayerPause: true);
				animClickController.closeSmithActionMenu();
			}
			else
			{
				viewController.closeWorldMap(hide: true, resume: true, resumeFromPlayerPause: true);
			}
			fromAssign = false;
			GameObject.Find("Panel_ActivitySelect").GetComponent<GUIActivitySelectController>().refreshButtons();
			break;
		}
		case ActivityType.ActivityTypeVacation:
		{
			List<Smith> selectedSmith2 = selectSmithCtr.getSelectedSmith();
			if (checkSmithInShop(selectedSmith2) && checkSmithArea(selectedSmith2.Count))
			{
				area = player.getLastSelectArea();
				Season seasonByMonth2 = CommonAPI.getSeasonByMonth(player.getSeasonIndex());
				VacationPackage vacationPackageByRefID = gameData.getVacationPackageByRefID(area.getVacationPackageRefId());
				Vacation vacationByRefId = gameData.getVacationByRefId(vacationPackageByRefID.getVacationRefIDBySeason(seasonByMonth2));
				int num2 = vacationByRefId.getVacationCost() * selectedSmith2.Count;
				if (player.getPlayerGold() < num2)
				{
					viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeYesAndNo, resume: false, string.Empty, gameData.getTextByRefId("mapText45"), PopupType.PopupTypeMapVacation, null, colorTag: false, null, map: true, string.Empty);
				}
				else
				{
					startVacation();
				}
			}
			break;
		}
		case ActivityType.ActivityTypeTraining:
		{
			List<Smith> selectedSmith = selectSmithCtr.getSelectedSmith();
			if (checkSmithInShop(selectedSmith) && checkSmithArea(selectedSmith.Count))
			{
				area = player.getLastSelectArea();
				Season seasonByMonth = CommonAPI.getSeasonByMonth(player.getSeasonIndex());
				TrainingPackage trainingPackageByRefID = gameData.getTrainingPackageByRefID(area.getTrainingPackageRefId());
				SmithTraining smithTrainingByRefId = gameData.getSmithTrainingByRefId(trainingPackageByRefID.getTrainingRefIDBySeason(seasonByMonth));
				int num = smithTrainingByRefId.getSmithTrainingCost() * selectedSmith.Count;
				if (player.getPlayerGold() < num)
				{
					viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeYesAndNo, resume: false, string.Empty, gameData.getTextByRefId("mapText45"), PopupType.PopupTypeMapTraining, null, colorTag: false, null, map: true, string.Empty);
				}
				else
				{
					startTraining();
				}
			}
			break;
		}
		case ActivityType.ActivityTypeResearch:
			break;
		}
	}

	private List<GameObject> getPathGameObjectByRefID(string aRefID)
	{
		List<GameObject> list = new List<GameObject>();
		string[] array = null;
		foreach (GameObject pathGameObject in pathGameObjectList)
		{
			array = pathGameObject.name.Split('_');
			if (aRefID == array[1])
			{
				list.Add(pathGameObject);
			}
		}
		return list;
	}

	public ActivityType getSelectedActivity()
	{
		return selectedActivity;
	}

	public void setSelectedAreaIndex(int aIndex)
	{
		selectedAreaIndex = aIndex;
	}

	public void setSelectedActivity(ActivityType aType)
	{
		selectedActivity = aType;
	}

	public Smith getSmithTrainingVacation()
	{
		return preselectedSmith;
	}

	public void setPreselectedSmith(Smith aSmith)
	{
		preselectedSmith = aSmith;
	}

	public void clearPreselectedSmith()
	{
		preselectedSmith = null;
	}

	public void setIsFromAssign()
	{
		fromAssign = true;
	}

	public void setConfirmButton()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		switch (selectedActivity)
		{
		case ActivityType.ActivityTypeSellWeapon:
			if ((button_confirm != null && sellWeaponCtr != null && !sellWeaponCtr.checkSelectedProjectList()) || (selectSmithCtr != null && !selectSmithCtr.checkSelectedSmith()))
			{
				commonScreenObject.destroyPrefabImmediate(button_confirm);
			}
			if (button_confirm == null && sellWeaponCtr != null && sellWeaponCtr.checkSelectedProjectList() && selectSmithCtr != null && selectSmithCtr.checkSelectedSmith())
			{
				button_confirm = commonScreenObject.createPrefab(Panel_MapSelectGrid, "MapButtonConfirm", "Prefab/Area/NEW/MapButtonConfirm", rightPanelOut, Vector3.one, Vector3.zero);
				commonScreenObject.findChild(button_confirm, "ConfirmNumber").GetComponent<UISprite>().spriteName = "step_3";
				commonScreenObject.findChild(button_confirm, "ConfirmLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("mapText26").ToUpper(CultureInfo.InvariantCulture);
			}
			break;
		case ActivityType.ActivityTypeBuyMats:
			if ((button_confirm != null && buyMatCtr != null && !buyMatCtr.checkSelectedItemList()) || (selectSmithCtr != null && !selectSmithCtr.checkSelectedSmith()))
			{
				commonScreenObject.destroyPrefabImmediate(button_confirm);
			}
			if (button_confirm == null && buyMatCtr != null && buyMatCtr.checkSelectedItemList() && selectSmithCtr != null && selectSmithCtr.checkSelectedSmith())
			{
				button_confirm = commonScreenObject.createPrefab(Panel_MapSelectGrid, "MapButtonConfirm", "Prefab/Area/NEW/MapButtonConfirm", rightPanelOut, Vector3.one, Vector3.zero);
				commonScreenObject.findChild(button_confirm, "ConfirmNumber").GetComponent<UISprite>().spriteName = "step_3";
				commonScreenObject.findChild(button_confirm, "ConfirmLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("mapText27").ToUpper(CultureInfo.InvariantCulture);
			}
			break;
		case ActivityType.ActivityTypeExplore:
			if (button_confirm != null && selectSmithCtr != null && !selectSmithCtr.checkSelectedSmith())
			{
				commonScreenObject.destroyPrefabImmediate(button_confirm);
			}
			if (button_confirm == null && selectSmithCtr != null && selectSmithCtr.checkSelectedSmith())
			{
				button_confirm = commonScreenObject.createPrefab(Panel_MapSelectGrid, "MapButtonConfirm", "Prefab/Area/NEW/MapButtonConfirm", rightPanelOut, Vector3.one, Vector3.zero);
				commonScreenObject.findChild(button_confirm, "ConfirmNumber").GetComponent<UISprite>().spriteName = "step_2yellow";
				commonScreenObject.findChild(button_confirm, "ConfirmLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("mapText25").ToUpper(CultureInfo.InvariantCulture);
			}
			break;
		case ActivityType.ActivityTypeTraining:
			if (button_confirm != null && selectSmithCtr != null && !selectSmithCtr.checkSelectedSmith())
			{
				commonScreenObject.destroyPrefabImmediate(button_confirm);
			}
			if (button_confirm == null && selectSmithCtr != null && selectSmithCtr.checkSelectedSmith())
			{
				button_confirm = commonScreenObject.createPrefab(Panel_MapSelectGrid, "MapButtonConfirm", "Prefab/Area/NEW/MapButtonConfirm", rightPanelOut, Vector3.one, Vector3.zero);
				commonScreenObject.findChild(button_confirm, "ConfirmNumber").GetComponent<UISprite>().spriteName = "step_2yellow";
				commonScreenObject.findChild(button_confirm, "ConfirmLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("mapText29").ToUpper(CultureInfo.InvariantCulture);
			}
			break;
		case ActivityType.ActivityTypeVacation:
			if (button_confirm != null && selectSmithCtr != null && !selectSmithCtr.checkSelectedSmith())
			{
				commonScreenObject.destroyPrefabImmediate(button_confirm);
			}
			if (button_confirm == null && selectSmithCtr != null && selectSmithCtr.checkSelectedSmith())
			{
				button_confirm = commonScreenObject.createPrefab(Panel_MapSelectGrid, "MapButtonConfirm", "Prefab/Area/NEW/MapButtonConfirm", rightPanelOut, Vector3.one, Vector3.zero);
				commonScreenObject.findChild(button_confirm, "ConfirmNumber").GetComponent<UISprite>().spriteName = "step_2yellow";
				commonScreenObject.findChild(button_confirm, "ConfirmLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("mapText28").ToUpper(CultureInfo.InvariantCulture);
			}
			break;
		case ActivityType.ActivityTypeResearch:
			break;
		}
	}

	private string getUnlockAreaInfoString(Area area)
	{
		GameData gameData = game.getGameData();
		string empty = string.Empty;
		empty += area.getAreaName();
		if (!area.checkIsUnlock())
		{
			empty = empty + "[808080]" + gameData.getTextByRefId("mapText73") + "[-]";
		}
		empty += "\n[s]                         [/s]\n";
		if (area.checkCanBuy())
		{
			empty = empty + "[FF9000]" + gameData.getTextByRefId("mapText05") + "[-]\n";
		}
		if (area.checkCanSell())
		{
			empty = empty + "[FF9000]" + gameData.getTextByRefId("mapText04") + "[-]\n";
		}
		if (area.checkCanExplore())
		{
			empty = empty + "[FF9000]" + gameData.getTextByRefId("mapText03") + "[-]\n";
		}
		if (area.getVacationPackageRefId() != "-1")
		{
			empty = empty + "[FF9000]" + gameData.getTextByRefId("mapText06") + "[-]\n";
		}
		if (area.getTrainingPackageRefId() != "-1")
		{
			empty = empty + "[FF9000]" + gameData.getTextByRefId("mapText08") + "[-]\n";
		}
		empty += "[s]                         [/s]\n";
		return empty + "[808080][i]" + area.getAreaDesc() + "[/i][-]";
	}

	private string getExploreInfoString(Area area)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		string empty = string.Empty;
		empty = empty + area.getAreaName() + "\n";
		empty += "[s]                         [/s]\n";
		string text = empty;
		empty = text + gameData.getTextByRefId("mapText09") + ": [FF9000]" + getTravelTimeString(area) + "[-]\n";
		text = empty;
		empty = text + gameData.getTextByRefId("mapText81") + ": [FF9000]" + area.getExploreItemList().Count + "[-]\n\n";
		text = empty;
		empty = text + gameData.getTextByRefId("mapText14") + " (" + area.getItemFoundAmount() + "): \n";
		string itemFoundString = area.getItemFoundString();
		empty = ((!(itemFoundString == string.Empty)) ? (empty + "[FF9000][i]" + itemFoundString + "[-][/i]\n") : (empty + "[FF9000][i]" + gameData.getTextByRefId("mapText84") + "[-][/i]\n"));
		empty += "[s]                         [/s]\n";
		return empty + "[808080][i]" + area.getAreaDesc() + "[/i][-]";
	}

	private string getSellInfoString(Area area)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		string empty = string.Empty;
		empty = empty + area.getAreaName() + "\n";
		empty += "[s]                         [/s]\n";
		string text = empty;
		empty = text + gameData.getTextByRefId("mapText09") + ": [FF9000]" + getTravelTimeString(area) + "[-]\n\n";
		empty = empty + gameData.getTextByRefId("mapText30") + ":\n";
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		Dictionary<string, int> heroList = area.getHeroList();
		Dictionary<string, int> rareHeroList = area.getRareHeroList();
		foreach (KeyValuePair<string, int> item in heroList)
		{
			dictionary.Add(item.Key, item.Value);
		}
		foreach (KeyValuePair<string, int> item2 in rareHeroList)
		{
			dictionary.Add(item2.Key, item2.Value);
		}
		List<Hero> list = new List<Hero>();
		foreach (KeyValuePair<string, int> item3 in dictionary)
		{
			Hero heroByHeroRefID = gameData.getHeroByHeroRefID(item3.Key);
			text = empty;
			empty = text + heroByHeroRefID.getHeroName() + " (" + heroByHeroRefID.getJobClassName() + ") " + gameData.getTextByRefId("smithStatsShort01") + ". " + heroByHeroRefID.getHeroLevel() + "\n";
		}
		empty += "[s]                         [/s]\n";
		return empty + "[808080][i]" + area.getAreaDesc() + "[/i][-]";
	}

	private string getBuyInfoString(Area area)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		string empty = string.Empty;
		empty = empty + area.getAreaName() + "\n";
		empty += "[s]                         [/s]\n";
		string text = empty;
		empty = text + gameData.getTextByRefId("mapText09") + ": [FF9000]" + getTravelTimeString(area) + "[-]\n\n";
		empty = empty + gameData.getTextByRefId("mapText31") + ":\n";
		Dictionary<string, ShopItem> shopItemList = area.getShopItemList();
		empty += "[FF9000][i]";
		bool flag = true;
		foreach (KeyValuePair<string, ShopItem> item in shopItemList.OrderBy((KeyValuePair<string, ShopItem> i) => i.Value.getCost()))
		{
			string key = item.Key;
			Item itemByRefId = gameData.getItemByRefId(key);
			if (checkScenarioAllow(itemByRefId))
			{
				if (!flag)
				{
					empty += ", ";
				}
				empty += itemByRefId.getItemName();
				if (flag)
				{
					flag = false;
				}
			}
		}
		empty += "[/i][-]\n";
		empty += "[s]                         [/s]\n";
		return empty + "[808080][i]" + area.getAreaDesc() + "[/i][-]";
	}

	private string getVacationInfoString(Area area, Vacation aVacation)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		string empty = string.Empty;
		empty = empty + area.getAreaName() + "\n";
		empty += "[s]                         [/s]\n";
		string text = empty;
		empty = text + gameData.getTextByRefId("mapText09") + ": [FF9000]" + getTravelTimeString(area) + "[-]\n\n";
		empty = empty + "[FF9000]" + aVacation.getVacationName() + "[-]\n";
		text = empty;
		empty = text + gameData.getTextByRefId("mapText82") + ": [FF9000]" + CommonAPI.formatNumber(aVacation.getVacationCost()) + "[-]\n";
		int vacationPackageRating = CommonAPI.getVacationPackageRating((int)aVacation.getVacationMoodAdd(), player.getShopLevelInt());
		text = empty;
		empty = text + gameData.getTextByRefId("mapText83") + ": [FF9000]" + vacationPackageRating + "/5[-]\n";
		empty += "[s]                         [/s]\n";
		return empty + "[808080][i]" + area.getAreaDesc() + "[/i][-]";
	}

	private string getSmithEffectString(Area area)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		string text = string.Empty;
		List<AreaStatus> areaStatusListByAreaAndSeason = gameData.getAreaStatusListByAreaAndSeason(area.getAreaRefId(), CommonAPI.getSeasonByMonth(player.getSeasonIndex()));
		if (areaStatusListByAreaAndSeason.Count > 0)
		{
			for (int i = 0; i < areaStatusListByAreaAndSeason.Count; i++)
			{
				if (areaStatusListByAreaAndSeason[i].getSmithEffectRefID() != "-1")
				{
					SmithStatusEffect smithStatusEffectByRefId = gameData.getSmithStatusEffectByRefId(areaStatusListByAreaAndSeason[i].getSmithEffectRefID());
					string text2 = text;
					text = text2 + smithStatusEffectByRefId.getEffectName() + "\n" + smithStatusEffectByRefId.getEffectDesc() + "\n";
					if (i != areaStatusListByAreaAndSeason.Count - 1)
					{
						text += "[s]                         [/s]\n";
					}
				}
			}
		}
		else
		{
			text = string.Empty;
		}
		return text;
	}

	private string getWorkshopInfoString()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		string text = player.getShopName() + " " + gameData.getTextByRefId("smithStatsShort01") + ". " + player.getShopLevelInt() + "\n";
		text += "[s]                         [/s]\n";
		return text + "[808080][i]" + gameData.getTextByRefId("mapText43") + "[/i][-]";
	}

	private string getTravelTimeString(Area aArea = null)
	{
		int num = 0;
		switch (selectedActivity)
		{
		case ActivityType.ActivityTypeExplore:
			num = 10;
			break;
		case ActivityType.ActivityTypeBuyMats:
			num = 3;
			break;
		case ActivityType.ActivityTypeSellWeapon:
			num = 5;
			break;
		case ActivityType.ActivityTypeTraining:
			num = 10;
			break;
		case ActivityType.ActivityTypeVacation:
			num = 24;
			break;
		}
		int num2 = 0;
		num2 = ((aArea != null) ? (aArea.getTravelTime() * 2 + num) : (areaList[selectedAreaIndex].getTravelTime() * 2 + num));
		return CommonAPI.convertHalfHoursToTimeString(num2, showHalfHours: false);
	}

	private void setUnlockPanel(Area area, GameObject parent)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		if (unlockPanel != null)
		{
			commonScreenObject.destroyPrefabImmediate(unlockPanel);
		}
		unlockPanel = commonScreenObject.createPrefab(parent, "UnlockPanel", "Prefab/Area/UnlockPanel", Vector3.zero, Vector3.one, Vector3.zero);
		List<TextMesh> list = new List<TextMesh>();
		TextMesh component = commonScreenObject.findChild(unlockPanel, "UnlockButton/UnlockLabel").GetComponent<TextMesh>();
		component.text = gameData.getTextByRefIdWithDynText("mapText74", "[TicketAmount]", area.getUnlockTickets().ToString());
		list.Add(component);
		TextMesh component2 = commonScreenObject.findChild(unlockPanel, "UnlockIslandName").GetComponent<TextMesh>();
		component2.text = area.getAreaName() + "?";
		list.Add(component2);
		TextMesh component3 = commonScreenObject.findChild(unlockPanel, "UnlockQnLabel").GetComponent<TextMesh>();
		component3.text = gameData.getTextByRefId("mapText75");
		list.Add(component3);
		TextMesh component4 = commonScreenObject.findChild(unlockPanel, "UnlockTitle").GetComponent<TextMesh>();
		component4.text = gameData.getTextByRefId("mapText76");
		list.Add(component4);
		foreach (TextMesh item in list)
		{
			item.GetComponent<Renderer>().sortingLayerName = "Character";
			item.GetComponent<Renderer>().sortingOrder = 6;
		}
		List<string> unlockAreaList = area.getUnlockAreaList();
		string text = string.Empty;
		bool flag = true;
		foreach (string item2 in unlockAreaList)
		{
			Area areaByRefID = gameData.getAreaByRefID(item2);
			if (!areaByRefID.checkIsUnlock())
			{
				if (flag)
				{
					text += areaByRefID.getAreaName();
					flag = false;
				}
				else
				{
					text = text + ", " + areaByRefID.getAreaName();
				}
			}
		}
		if (text != string.Empty)
		{
			component3.text = gameData.getTextByRefId("mapText77");
			component2.text = gameData.getTextByRefIdWithDynText("mapText78", "[IslandName]", text);
			commonScreenObject.findChild(unlockPanel, "UnlockButton/UnlockLabel").GetComponent<TextMesh>().color = Color.grey;
			commonScreenObject.findChild(unlockPanel, "UnlockButton").GetComponent<SpriteRenderer>().color = Color.grey;
		}
		else if (player.getUnusedTickets() < area.getUnlockTickets())
		{
			component3.text = gameData.getTextByRefId("mapText79");
			component2.text = gameData.getTextByRefId("mapText80");
			commonScreenObject.findChild(unlockPanel, "UnlockButton/UnlockLabel").GetComponent<TextMesh>().color = Color.grey;
			commonScreenObject.findChild(unlockPanel, "UnlockButton").GetComponent<SpriteRenderer>().color = Color.grey;
		}
		else
		{
			commonScreenObject.findChild(unlockPanel, "UnlockButton").gameObject.AddComponent<BoxCollider>();
		}
	}

	private void setUnlockedPanel(Area area, GameObject parent)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameObject gameObject = GameObject.Find("UnlockedPanel_" + area.getAreaRefId());
		if (gameObject == null)
		{
			gameObject = commonScreenObject.createPrefab(parent, "UnlockedPanel_" + area.getAreaRefId(), "Prefab/Area/UnlockedPanel", Vector3.zero, Vector3.one, Vector3.zero);
			TextMesh component = commonScreenObject.findChild(gameObject, "UnlockIslandName").GetComponent<TextMesh>();
			component.text = gameData.getTextByRefId("mapText86");
			component.GetComponent<Renderer>().sortingLayerName = "Character";
			component.GetComponent<Renderer>().sortingOrder = 6;
			commonScreenObject.tweenScale(gameObject.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 1f, gameObject, "destroySelf");
		}
	}

	private int getSeasonOrder(Season aSeason)
	{
		return aSeason switch
		{
			Season.SeasonSpring => 1, 
			Season.SeasonSummer => 2, 
			Season.SeasonAutumn => 3, 
			Season.SeasonWinter => 4, 
			_ => 0, 
		};
	}

	private bool checkScenarioAllow(Item aItem)
	{
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		return aItem.checkScenarioAllow(itemLockSet);
	}
}
