using System.Globalization;
using UnityEngine;

public class BottomMenuController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private UILabel contractLabel;

	private UILabel blueprintLabel;

	private UILabel shopLabel;

	private UILabel inventoryLabel;

	private UILabel collectionLabel;

	private UILabel settingsLabel;

	private UILabel worldMapLabel;

	private UILabel journalLabel;

	private UISprite researchAlert;

	private UISprite researchTimerBg;

	private UISprite researchWpnIcon;

	private UISprite researchProgress;

	private UILabel researchTimerLabel;

	private GameObject tutorialMask_popup;

	private void Awake()
	{
		tutorialMask_popup = GameObject.Find("tutorialMask_popup");
	}

	public void loadButtons()
	{
		setVariables();
		refreshBottomButtons();
	}

	public void processClick(string gameObjectName)
	{
		setVariables();
		if (base.gameObject.GetComponent<TweenPosition>().enabled)
		{
			return;
		}
		switch (gameObjectName)
		{
		case "forge_button":
			showForgeMenu();
			break;
		case "button_contract":
			audioController.playButtonAudio();
			audioController.playMenuOpenAudio();
			viewController.showContract();
			break;
		case "button_blueprint":
			audioController.playButtonAudio();
			audioController.playMenuOpenAudio();
			viewController.showResearch();
			break;
		case "button_shop":
			audioController.playButtonAudio();
			audioController.playMenuOpenAudio();
			viewController.showUpgradeShopMenu();
			break;
		case "button_inventory":
			audioController.playButtonAudio();
			audioController.playMenuOpenAudio();
			viewController.showInventoryPopup();
			break;
		case "button_collection":
			audioController.playButtonAudio();
			audioController.playMenuOpenAudio();
			viewController.showCollectionPopup();
			break;
		case "button_settings":
			audioController.playButtonAudio();
			audioController.playMenuOpenAudio();
			viewController.showSettingsMenu();
			break;
		case "button_journal":
			audioController.playButtonAudio();
			audioController.playMenuOpenAudio();
			viewController.showJournal();
			break;
		case "button_worldMap":
		{
			audioController.playButtonAudio();
			GameObject gameObject = GameObject.Find("Panel_Tutorial");
			bool flag = false;
			if (gameObject != null)
			{
				GUITutorialController component = gameObject.GetComponent<GUITutorialController>();
				if (component.checkCurrentTutorial("30001"))
				{
					component.nextTutorial();
				}
			}
			viewController.showWorldMap();
			break;
		}
		}
	}

	public void processHover(bool isOver, string hoverName)
	{
		setVariables();
		if (isOver)
		{
			showLabel(hoverName);
		}
		else
		{
			showLabel(string.Empty);
		}
	}

	private void Update()
	{
		if (viewController != null && viewController.getGameStarted() && GameObject.Find("Panel_Tutorial") == null && !tutorialMask_popup.GetComponent<BoxCollider>().enabled)
		{
			handleInput();
		}
	}

	private void handleInput()
	{
		if (Input.GetKeyDown(gameData.getKeyCodeByRefID("200001")) || Input.GetMouseButtonDown(1))
		{
			if (!viewController.getHasPopup())
			{
				processClick("forge_button");
			}
		}
		else if (Input.GetKeyDown(gameData.getKeyCodeByRefID("200002")) && blueprintLabel.parent.GetComponent<UIButton>().isEnabled)
		{
			if (!viewController.getHasPopup())
			{
				processClick("button_blueprint");
			}
		}
		else if (Input.GetKeyDown(gameData.getKeyCodeByRefID("200003")) && contractLabel.parent.GetComponent<UIButton>().isEnabled)
		{
			if (!viewController.getHasPopup())
			{
				processClick("button_contract");
			}
		}
		else if (Input.GetKeyDown(gameData.getKeyCodeByRefID("200004")) && worldMapLabel.parent.GetComponent<UIButton>().isEnabled)
		{
			if (!viewController.getHasPopup())
			{
				processClick("button_worldMap");
			}
		}
		else if (Input.GetKeyDown(gameData.getKeyCodeByRefID("200005")) && collectionLabel.parent.GetComponent<UIButton>().isEnabled)
		{
			if (!viewController.getHasPopup())
			{
				processClick("button_collection");
			}
		}
		else if (Input.GetKeyDown(gameData.getKeyCodeByRefID("200006")) && inventoryLabel.parent.GetComponent<UIButton>().isEnabled)
		{
			if (!viewController.getHasPopup())
			{
				processClick("button_inventory");
			}
		}
		else if (Input.GetKeyDown(gameData.getKeyCodeByRefID("200007")) && settingsLabel.parent.GetComponent<UIButton>().isEnabled)
		{
			if (!viewController.getHasPopup())
			{
				processClick("button_settings");
			}
		}
		else if (Input.GetKeyDown(gameData.getKeyCodeByRefID("200008")) && shopLabel.parent.GetComponent<UIButton>().isEnabled && !viewController.getHasPopup())
		{
			processClick("button_shop");
		}
	}

	public void clearTooltips()
	{
		showLabel(string.Empty);
	}

	public void refreshBottomButtons()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		if (player.checkCanResearch() && gameData.checkHasNewResearch(itemLockSet) && gameData.checkFeatureIsUnlocked(gameLockSet, "RESEARCH", completedTutorialIndex))
		{
			researchAlert.alpha = 1f;
		}
		else
		{
			researchAlert.alpha = 0f;
		}
	}

	private void showLabel(string show)
	{
		if (show == "button_contract")
		{
			contractLabel.alpha = 1f;
		}
		else
		{
			contractLabel.alpha = 0f;
		}
		if (show == "button_blueprint")
		{
			blueprintLabel.alpha = 1f;
		}
		else
		{
			blueprintLabel.alpha = 0f;
		}
		if (show == "button_shop")
		{
			shopLabel.alpha = 1f;
		}
		else
		{
			shopLabel.alpha = 0f;
		}
		if (show == "button_inventory")
		{
			inventoryLabel.alpha = 1f;
		}
		else
		{
			inventoryLabel.alpha = 0f;
		}
		if (show == "button_collection")
		{
			collectionLabel.alpha = 1f;
		}
		else
		{
			collectionLabel.alpha = 0f;
		}
		if (show == "button_settings")
		{
			settingsLabel.alpha = 1f;
		}
		else
		{
			settingsLabel.alpha = 0f;
		}
		if (show == "button_worldMap")
		{
			worldMapLabel.alpha = 1f;
		}
		else
		{
			worldMapLabel.alpha = 0f;
		}
		if (show == "button_journal")
		{
			journalLabel.alpha = 1f;
		}
		else
		{
			journalLabel.alpha = 0f;
		}
	}

	private void setVariables()
	{
		if (game == null)
		{
			game = GameObject.Find("Game").GetComponent<Game>();
		}
		if (gameData == null)
		{
			gameData = game.getGameData();
		}
		if (commonScreenObject == null)
		{
			commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		}
		if (viewController == null)
		{
			viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		}
		if (shopMenuController == null)
		{
			shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		}
		if (audioController == null)
		{
			audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		}
		if (tooltipScript == null)
		{
			tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		}
		if (contractLabel == null)
		{
			contractLabel = commonScreenObject.findChild(base.gameObject, "button_contract/contractLabel_label").GetComponent<UILabel>();
		}
		if (blueprintLabel == null)
		{
			blueprintLabel = commonScreenObject.findChild(base.gameObject, "button_blueprint/researchLabel_label").GetComponent<UILabel>();
			researchAlert = commonScreenObject.findChild(base.gameObject, "button_blueprint/ResearchAlert").GetComponent<UISprite>();
			researchTimerBg = commonScreenObject.findChild(base.gameObject, "button_blueprint/researchTimer_bg").GetComponent<UISprite>();
			researchWpnIcon = commonScreenObject.findChild(researchTimerBg.gameObject, "researchTimer_icon").GetComponent<UISprite>();
			researchProgress = commonScreenObject.findChild(researchTimerBg.gameObject, "researchCircle_fg").GetComponent<UISprite>();
			researchTimerLabel = commonScreenObject.findChild(researchTimerBg.gameObject, "researchTimer_label").GetComponent<UILabel>();
		}
		if (shopLabel == null)
		{
			shopLabel = commonScreenObject.findChild(base.gameObject, "button_shop/shopsLabel_label").GetComponent<UILabel>();
		}
		if (inventoryLabel == null)
		{
			inventoryLabel = commonScreenObject.findChild(base.gameObject, "button_inventory/inventoryLabel_label").GetComponent<UILabel>();
		}
		if (collectionLabel == null)
		{
			collectionLabel = commonScreenObject.findChild(base.gameObject, "button_collection/collectionLabel_label").GetComponent<UILabel>();
		}
		if (settingsLabel == null)
		{
			settingsLabel = commonScreenObject.findChild(base.gameObject, "button_settings/settingsLabel_label").GetComponent<UILabel>();
		}
		if (worldMapLabel == null)
		{
			worldMapLabel = commonScreenObject.findChild(base.gameObject, "button_worldMap/worldMapLabel_label").GetComponent<UILabel>();
		}
		if (journalLabel == null)
		{
			journalLabel = commonScreenObject.findChild(base.gameObject, "button_journal/journalLabel_label").GetComponent<UILabel>();
		}
		if (game != null)
		{
			commonScreenObject.findChild(base.gameObject, "bottomMenuForge_bg/forge_button").GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("forgeBottomMenu01").ToUpper(CultureInfo.InvariantCulture);
			contractLabel.text = gameData.getTextByRefId("forgeBottomMenu02").ToUpper(CultureInfo.InvariantCulture);
			blueprintLabel.text = gameData.getTextByRefId("forgeBottomMenu03").ToUpper(CultureInfo.InvariantCulture);
			inventoryLabel.text = gameData.getTextByRefId("forgeBottomMenu04").ToUpper(CultureInfo.InvariantCulture);
			shopLabel.text = gameData.getTextByRefId("forgeBottomMenu05").ToUpper(CultureInfo.InvariantCulture);
			collectionLabel.text = gameData.getTextByRefId("forgeBottomMenu06").ToUpper(CultureInfo.InvariantCulture);
			settingsLabel.text = gameData.getTextByRefId("forgeBottomMenu07").ToUpper(CultureInfo.InvariantCulture);
			worldMapLabel.text = gameData.getTextByRefId("mapText33").ToUpper(CultureInfo.InvariantCulture);
			journalLabel.text = gameData.getTextByRefId("forgeBottomMenu08").ToUpper(CultureInfo.InvariantCulture);
		}
	}

	public void updateResearchProgress()
	{
		Player player = game.getPlayer();
		bool flag = false;
		foreach (Smith smith in player.getSmithList())
		{
			if (smith.getSmithAction().getRefId() == "904")
			{
				float fillAmount = smith.calculateSmithActionProgressPercentage();
				int num = smith.calculateSmithActionTimeLeft();
				Weapon currentResearchWeapon = player.getCurrentResearchWeapon();
				researchTimerBg.alpha = 1f;
				researchWpnIcon.spriteName = "icon_" + currentResearchWeapon.getWeaponType().getImage();
				researchProgress.fillAmount = fillAmount;
				researchTimerLabel.text = CommonAPI.convertHalfHoursToTimeString(num, showHalfHours: false);
				flag = true;
			}
		}
		if (!flag)
		{
			researchTimerBg.alpha = 0f;
		}
	}

	private void showForgeMenu()
	{
		viewController.showForgeMenuNewPopup();
	}

	public void setTutorialState(string currentState)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		if (currentState == string.Empty)
		{
			UIButton[] componentsInChildren = base.gameObject.GetComponentsInChildren<UIButton>();
			foreach (UIButton uIButton in componentsInChildren)
			{
				switch (uIButton.name)
				{
				case "button_blueprint":
					if (gameData.checkFeatureIsUnlocked(gameLockSet, "RESEARCH", completedTutorialIndex))
					{
						uIButton.isEnabled = true;
					}
					else
					{
						uIButton.isEnabled = false;
					}
					break;
				case "button_collection":
					if (gameData.checkFeatureIsUnlocked(gameLockSet, "CHEST", completedTutorialIndex))
					{
						uIButton.isEnabled = true;
					}
					else
					{
						uIButton.isEnabled = false;
					}
					break;
				case "button_contract":
					if (gameData.checkFeatureIsUnlocked(gameLockSet, "CONTRACT", completedTutorialIndex))
					{
						uIButton.isEnabled = true;
					}
					else
					{
						uIButton.isEnabled = false;
					}
					break;
				case "button_inventory":
					if (gameData.checkFeatureIsUnlocked(gameLockSet, "INVENTORY", completedTutorialIndex))
					{
						uIButton.isEnabled = true;
					}
					else
					{
						uIButton.isEnabled = false;
					}
					break;
				case "button_shop":
					if (gameData.checkFeatureIsUnlocked(gameLockSet, "UPGRADES", completedTutorialIndex))
					{
						uIButton.isEnabled = true;
					}
					else
					{
						uIButton.isEnabled = false;
					}
					break;
				case "button_worldMap":
					if (gameData.checkFeatureIsUnlocked(gameLockSet, "MAP", completedTutorialIndex))
					{
						uIButton.isEnabled = true;
					}
					else
					{
						uIButton.isEnabled = false;
					}
					break;
				case "button_journal":
					if (gameData.checkFeatureIsUnlocked(gameLockSet, "JOURNAL", completedTutorialIndex))
					{
						uIButton.isEnabled = true;
					}
					else
					{
						uIButton.isEnabled = false;
					}
					break;
				default:
					uIButton.isEnabled = true;
					break;
				}
			}
			return;
		}
		switch (currentState)
		{
		case "INTRO_FORGE":
		{
			UIButton[] componentsInChildren3 = base.gameObject.GetComponentsInChildren<UIButton>();
			foreach (UIButton uIButton3 in componentsInChildren3)
			{
				if (uIButton3.gameObject.name != "forge_button")
				{
					uIButton3.isEnabled = false;
				}
				else
				{
					uIButton3.isEnabled = true;
				}
			}
			break;
		}
		case "INTRO_MAP":
		{
			UIButton[] componentsInChildren2 = base.gameObject.GetComponentsInChildren<UIButton>();
			foreach (UIButton uIButton2 in componentsInChildren2)
			{
				if (uIButton2.gameObject.name != "button_worldMap")
				{
					uIButton2.isEnabled = false;
				}
				else
				{
					uIButton2.isEnabled = true;
				}
			}
			break;
		}
		}
	}
}
