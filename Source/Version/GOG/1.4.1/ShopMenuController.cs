using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class ShopMenuController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private AudioController audioController;

	private SteamStatsAndAchievements steamStatsController;

	private GameCenterAchievement gameCenterAchievement;

	public MenuState menuState;

	public PopEventType popEventType;

	public PopupType popupType;

	public Dictionary<string, int> storedInputDict;

	private string longInputText;

	private Hashtable questDisplayList;

	private bool questIsFinish;

	private List<WeekendActivity> weekendList;

	private List<SpecialEvent> dayStartEventList;

	private int lastFeedDay;

	private SpriteRenderer sceneBackground;

	private bool doUnlockCheck;

	private bool isGameOver;

	private int isDemoOver;

	private GameObject nguiCameraGUI;

	private GUICharacterAnimationController charAnimCtr;

	private GUISmithListMenuController smithListMenuController;

	private void Start()
	{
		if (game == null)
		{
			game = GameObject.Find("Game").GetComponent<Game>();
		}
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		GameObject gameObject = GameObject.Find("SteamStatsAndAchievements");
		if (gameObject != null)
		{
			steamStatsController = gameObject.GetComponent<SteamStatsAndAchievements>();
		}
		GameObject gameObject2 = GameObject.Find("GameCenterAchievement");
		if (gameObject2 != null)
		{
			gameCenterAchievement = gameObject2.GetComponent<GameCenterAchievement>();
		}
		menuState = MenuState.MenuStateClosed;
		popEventType = PopEventType.PopEventTypeStartMenu;
		popupType = PopupType.PopupTypeNothing;
		storedInputDict = new Dictionary<string, int>();
		longInputText = string.Empty;
		questDisplayList = new Hashtable();
		questIsFinish = false;
		weekendList = new List<WeekendActivity>();
		dayStartEventList = new List<SpecialEvent>();
		lastFeedDay = 0;
		sceneBackground = GameObject.Find("SceneBackground").GetComponent<SpriteRenderer>();
		doUnlockCheck = true;
		isGameOver = false;
		isDemoOver = 0;
		nguiCameraGUI = GameObject.Find("NGUICameraGUI");
		if (nguiCameraGUI != null)
		{
			charAnimCtr = GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>();
		}
	}

	public void showSaveMenu()
	{
		menuState = MenuState.MenuStateSaveMenu;
	}

	public void showForgeJobClassMenu(int showDetails, int input)
	{
		menuState = MenuState.MenuStateForgeJobClass;
		Player player = game.getPlayer();
		player.setLastSelectProjectType(ProjectType.ProjectTypeWeapon);
		if (nguiCameraGUI != null)
		{
			viewController.showForgeMenuNewPopup();
		}
	}

	public void showContractMenu()
	{
		menuState = MenuState.MenuStateForgeContract;
		Player player = game.getPlayer();
		player.setLastSelectProjectType(ProjectType.ProjectTypeContract);
		if (nguiCameraGUI != null)
		{
			viewController.closeMainMenu(resume: false);
			viewController.showContract();
		}
	}

	public void showContractConfirmation(int input)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		List<Contract> playerContractList = player.getPlayerContractList(gameData.getContractList(player.getPlayerMonths()));
		Contract contract = playerContractList[input - 1];
	}

	public void startContract()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		Contract lastSelectContract = player.getLastSelectContract();
		Project project = new Project(player.getNextProjectId().ToString(), lastSelectContract.getContractRefId(), lastSelectContract.getContractName(), lastSelectContract.getContractDesc(), lastSelectContract.getGold(), 0, 0, ProjectType.ProjectTypeContract, lastSelectContract.getTimeLimit(), lastSelectContract.getAtkReq(), lastSelectContract.getSpdReq(), lastSelectContract.getAccReq(), lastSelectContract.getMagReq(), 0, 0);
		if (tryStartProject(project, 0))
		{
			player.clearPlayerContractList();
			project.setProjectContract(lastSelectContract);
			lastSelectContract.addTimesStarted(1);
			menuState = MenuState.MenuStateClosed;
			commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().setGameState(GameState.GameStateIdle);
			string text = CommonAPI.convertHalfHoursToTimeDisplay(project.getContractEndTime(), isMenu: false);
			SmithAction smithActionByRefId = gameData.getSmithActionByRefId("102");
			player.resetSmithsWorkState(smithActionByRefId, -1);
			player.clearPlayerContractList();
			audioController.playForgingAudio();
			if (nguiCameraGUI != null)
			{
				viewController.showProjectProgress();
				viewController.closeContract(hide: true, resumeFromPlayerPause: true);
			}
		}
	}

	public void showResearchMainMenu(int input)
	{
		menuState = MenuState.MenuStateResearchMain;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		player.setLastSelectProjectType(ProjectType.ProjectTypeResearch);
		if (nguiCameraGUI != null)
		{
			viewController.closeMainMenu(resume: false);
		}
	}

	public int getCombiScore(Weapon selectedWeapon, Hero selectedJobClass)
	{
		Player player = game.getPlayer();
		int num = 2;
		if (player.getCurrentProjectType() == ProjectType.ProjectTypeUnique)
		{
			return 3;
		}
		return selectedJobClass.getAffinity(selectedWeapon.getWeaponTypeRefId());
	}

	public void showFurnitureShop(int input)
	{
		menuState = MenuState.MenuStateFurnitureShop;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		List<Furniture> furnitureListFiltered = gameData.getFurnitureListFiltered(onlyNotOwned: true, onlyLowestLevel: true, -1, player.checkHasDog(), isShopList: true);
		if (furnitureListFiltered.Count > 0)
		{
			if (nguiCameraGUI != null)
			{
				viewController.closeTier2Menu(hide: false);
				viewController.closeMainMenu(resume: false);
				viewController.showShopPopup(PopupType.PopupTypeBuyFurniture);
			}
		}
		else if (nguiCameraGUI != null)
		{
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, string.Empty, gameData.getTextByRefId("menuShop10"), PopupType.PopupTypeBuyFurnitureNoItem, null, colorTag: false, null, map: false, string.Empty);
		}
	}

	public void buyFurniture(bool onlyNotOwned = true)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int num = referStoredInput("furniture");
		List<Furniture> furnitureListFiltered = gameData.getFurnitureListFiltered(onlyNotOwned, onlyLowestLevel: true, -1, player.checkHasDog(), isShopList: true);
		if (num > furnitureListFiltered.Count)
		{
			return;
		}
		Furniture furniture = furnitureListFiltered[num - 1];
		int furnitureCost = furniture.getFurnitureCost();
		if (furniture.checkShopLevelReq(player.getShopLevelInt()) && tryActionWithGold(furnitureCost, allowNegative: false, useGold: true))
		{
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseBuyItem, string.Empty, -1 * furnitureCost);
			furniture.setPlayerOwned(aOwned: true);
			commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().showTimeStatus();
			if (nguiCameraGUI != null)
			{
				GameObject.Find("GUIObstacleController").GetComponent<GUIObstacleController>().createObstacles();
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, string.Empty, gameData.getTextByRefId("menuShop04"), PopupType.PopupTypeBuyFurniture, null, colorTag: false, null, map: false, string.Empty);
				viewController.closeShopPopup(hide: false);
				audioController.playShopPurchaseAudio();
			}
		}
	}

	public void showEnchantmentShop(int input)
	{
		menuState = MenuState.MenuStateEnchantmentShop;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		List<Item> shopItemListByType = gameData.getShopItemListByType(ItemType.ItemTypeEnhancement);
		if (shopItemListByType.Count > 0)
		{
			if (nguiCameraGUI != null)
			{
				viewController.closeMainMenu(resume: false);
				viewController.closeTier2Menu(hide: false);
				viewController.showShopPopup(PopupType.PopupTypeBuyEnchantment);
			}
		}
		else if (nguiCameraGUI != null)
		{
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, string.Empty, gameData.getTextByRefId("menuShop10"), PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
		}
	}

	public void buyEnchantment()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int num = referStoredInput("enchantment");
		List<Item> shopItemListByType = gameData.getShopItemListByType(ItemType.ItemTypeEnhancement);
		if (num > shopItemListByType.Count)
		{
			return;
		}
		Item item = shopItemListByType[num - 1];
		int itemCost = item.getItemCost();
		if (tryActionWithGold(itemCost, allowNegative: false, useGold: true))
		{
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseBuyItem, string.Empty, -1 * itemCost);
			item.addItem(1);
			commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().showTimeStatus();
			if (nguiCameraGUI != null)
			{
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, string.Empty, gameData.getTextByRefId("menuShop04"), PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
				GameObject.Find("Panel_Shop").GetComponent<GUIShopPopupController>().buyItem();
				audioController.playShopPurchaseAudio();
			}
		}
	}

	public void showDecorationShop(int input)
	{
		menuState = MenuState.MenuStateDecorationShop;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		List<Decoration> decorationList = gameData.getDecorationList(isShopList: true, itemLockSet);
		if (decorationList.Count <= 0)
		{
		}
	}

	public void buyDecoration()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		int num = referStoredInput("scroll");
		List<Decoration> decorationList = gameData.getDecorationList(isShopList: true, itemLockSet);
		if (num <= decorationList.Count)
		{
			Decoration decoration = decorationList[num - 1];
			int decorationShopCost = decoration.getDecorationShopCost();
			if (tryActionWithGold(decorationShopCost, allowNegative: false, useGold: true))
			{
				player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseBuyItem, string.Empty, -1 * decorationShopCost);
				decoration.setIsPlayerOwned(aPlayerOwned: true);
				player.addOwnedDecoration(decoration);
				commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().showTimeStatus();
			}
		}
	}

	public void showSellMenu(int input, Item prevItem = null)
	{
		menuState = MenuState.MenuStateShopSellMenu;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		List<Item> sellItemList = gameData.getSellItemList();
		if (sellItemList.Count > 0)
		{
			if (nguiCameraGUI != null)
			{
				viewController.closeMainMenu(resume: false);
				viewController.closeTier2Menu(hide: false);
				viewController.showShopPopup(PopupType.PopupTypeSellItem, prevItem);
			}
		}
		else if (nguiCameraGUI != null)
		{
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: true, string.Empty, gameData.getTextByRefId("menuShop11"), PopupType.PopupTypeSellItemError, null, colorTag: false, null, map: false, string.Empty);
		}
	}

	public void sellItem()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int num = referStoredInput("sell");
		List<Item> sellItemList = gameData.getSellItemList();
		if (num > sellItemList.Count)
		{
			return;
		}
		Item item = sellItemList[num - 1];
		if (item.tryUseItem(1, isUse: false))
		{
			int itemSellPrice = item.getItemSellPrice();
			player.addGold(itemSellPrice);
			audioController.playGoldGainAudio();
			commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().showTimeStatus();
			if (nguiCameraGUI != null)
			{
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, "ITEM SOLD", gameData.getTextByRefId("menuShop15"), PopupType.PopupTypeSellItem, item, colorTag: false, null, map: false, string.Empty);
				audioController.playShopPurchaseAudio();
			}
		}
	}

	public void showManageDeco(int input)
	{
		menuState = MenuState.MenuStateDecorationsMenu;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		List<Decoration> ownedDecorationList = player.getOwnedDecorationList();
		if (ownedDecorationList.Count <= 0)
		{
		}
	}

	public void showDecoChangeConfirmation(int selectedIndex)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		selectedIndex += retrieveStoredInput("scroll");
		List<Decoration> ownedDecorationList = player.getOwnedDecorationList();
		if (selectedIndex <= ownedDecorationList.Count)
		{
			Decoration decoration = ownedDecorationList[selectedIndex - 1];
			Decoration displayDecorationByType = player.getDisplayDecorationByType(decoration.getDecorationType());
			insertStoredInput("decoChange", selectedIndex);
			if (displayDecorationByType.getDecorationRefId() == string.Empty)
			{
				string text = decoration.getDecorationName() + "\n   " + generateDecoEffect(decoration);
			}
			else if (!(displayDecorationByType.getDecorationRefId() == decoration.getDecorationRefId()))
			{
				string text2 = "Replaced decoration" + displayDecorationByType.getDecorationName() + "\n   " + generateDecoEffect(displayDecorationByType);
				string text3 = text2;
				text2 = text3 + "\nSelected decoration" + decoration.getDecorationName() + "\n   " + generateDecoEffect(decoration);
			}
		}
	}

	public void changeShopDeco()
	{
		Player player = game.getPlayer();
		int num = retrieveStoredInput("decoChange");
		List<Decoration> ownedDecorationList = player.getOwnedDecorationList();
		if (num <= ownedDecorationList.Count)
		{
			Decoration selectDeco = ownedDecorationList[num - 1];
			doShopDecoChange(selectDeco);
		}
	}

	public void doShopDecoChange(Decoration selectDeco)
	{
		Player player = game.getPlayer();
		selectDeco.setIsCurrentDisplay(aDisplay: true);
		Decoration displayDecorationByType = player.getDisplayDecorationByType(selectDeco.getDecorationType());
		if (displayDecorationByType.getDecorationRefId() == string.Empty)
		{
			displayDecorationByType.setIsCurrentDisplay(aDisplay: false);
		}
		player.replaceDisplayDecoration(selectDeco);
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().showTimeStatus();
	}

	private void showSmithTrainTypeMenu(int input)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		List<Smith> smithList = player.getSmithList();
		if (input <= smithList.Count)
		{
			menuState = MenuState.MenuStateSmithTrainType;
		}
	}

	public void doSmithTraining(Smith training, SmithTraining trainingType)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		if (!(training.getSmithId() != string.Empty))
		{
			return;
		}
		menuState = MenuState.MenuStateSmithTrain;
		if (training.checkIsSmithMaxLevel())
		{
			return;
		}
		int smithTrainingStamina = trainingType.getSmithTrainingStamina();
		int num = trainingType.getSmithTrainingCost();
		float num2 = player.checkDecoEffect("TRAIN_COST", string.Empty);
		if (num2 != 1f)
		{
			num = (int)((float)num * num2);
		}
		if (!tryActionWithGold(num, allowNegative: false, useGold: true))
		{
			return;
		}
		player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseTraining, string.Empty, -1 * num);
		int num3 = trainingType.getSmithTrainingExp();
		string empty = string.Empty;
		float num4 = player.checkDecoEffect("TRAIN_EXP", string.Empty);
		if (num4 != 1f)
		{
			int num5 = num3;
			num3 = (int)(num4 * (float)num3);
			if (num4 > 1f)
			{
				int num6 = num3 - num5;
				if (num6 == 0)
				{
					num6 = 1;
					num3++;
				}
				empty = "<color=green>(+" + num6 + ")</color>";
			}
			else if (num4 < 1f)
			{
				int num7 = num5 - num3;
				if (num7 == 0)
				{
					num7 = 1;
					num3--;
				}
				empty = "<color=red>(-" + num7 + ")</color>";
			}
		}
		switch (training.getMoodState())
		{
		case SmithMood.SmithMoodHappy:
			num3 = (int)((float)num3 * Random.Range(1.1f, 1.3f));
			break;
		case SmithMood.SmithMoodSad:
			num3 = (int)((float)num3 * Random.Range(0.7f, 0.9f));
			break;
		}
		Dictionary<string, int> dictionary = training.addSmithExp(num3);
		bool hasMoodLimit = false;
		if (!gameData.checkFeatureIsUnlocked(gameLockSet, "MOODLIMIT", completedTutorialIndex))
		{
			hasMoodLimit = true;
		}
		training.reduceSmithMood(smithTrainingStamina, hasMoodLimit);
		trainingType.addUseCount(1);
	}

	public void changeSmithJob()
	{
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().setGameState(GameState.GameStateIdle);
		menuState = MenuState.MenuStateClosed;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int num = referStoredInput("jobChangeSmith");
		int num2 = referStoredInput("jobChangeJob");
		Smith smithByIndex = player.getSmithByIndex(num - 1);
		SmithJobClass newJob = gameData.getJobChangeList(smithByIndex.getExperienceList(), smithByIndex.getSmithJob().getSmithJobRefId())[num2 - 1];
		doSmithJobChange(smithByIndex, newJob);
	}

	public void doSmithJobChange(Smith changeSmith, SmithJobClass newJob)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		int smithJobChangeCost = newJob.getSmithJobChangeCost();
		if (tryActionWithGold(smithJobChangeCost, allowNegative: false, useGold: true))
		{
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseJobChange, string.Empty, -1 * smithJobChangeCost);
			retrieveStoredInput("jobChangeSmith");
			retrieveStoredInput("jobChangeJob");
			changeSmith.setSmithJob(newJob);
			changeSmith.resetExp();
			List<string> list = new List<string>();
			list.Add("[smithName]");
			list.Add("[newJob]");
			List<string> list2 = new List<string>();
			list2.Add(changeSmith.getSmithName());
			list2.Add(newJob.getSmithJobName());
			string textByRefIdWithDynTextList = gameData.getTextByRefIdWithDynTextList("menuSmithManagement27", list, list2);
			string textByRefId = gameData.getTextByRefId("smithJobChangeComment" + newJob.getSmithJobRefId());
			if (nguiCameraGUI != null)
			{
				viewController.showGeneralDialoguePopup(GeneralPopupType.GeneralPopupTypeDialogueGeneral, resume: false, gameData.getTextByRefId("menuChangeJob08").ToUpper(CultureInfo.InvariantCulture), textByRefIdWithDynTextList, textByRefId, "Image/Smith/Portraits/" + changeSmith.getImage());
			}
			gameData.addNewWhetsappMsg(changeSmith.getSmithName(), textByRefId, "Image/Smith/Portraits/" + changeSmith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
		}
	}

	public void showSmithRoleAssignMenu(int input)
	{
		menuState = MenuState.MenuStateSmithAssignRoleSmith;
	}

	public int checkSpaceAvailable(SmithStation station)
	{
		int result = 0;
		Player player = game.getPlayer();
		switch (station)
		{
		case SmithStation.SmithStationAuto:
			result = 1;
			break;
		case SmithStation.SmithStationDesign:
		{
			Furniture highestPlayerFurnitureByType3 = player.getHighestPlayerFurnitureByType("601");
			int count3 = player.getAssignedSmithArray(SmithStation.SmithStationDesign).Count;
			if (highestPlayerFurnitureByType3.getFurnitureCapacity() > count3)
			{
				result = highestPlayerFurnitureByType3.getFurnitureCapacity() - count3;
			}
			break;
		}
		case SmithStation.SmithStationCraft:
		{
			Furniture highestPlayerFurnitureByType4 = player.getHighestPlayerFurnitureByType("701");
			int count4 = player.getAssignedSmithArray(SmithStation.SmithStationCraft).Count;
			if (highestPlayerFurnitureByType4.getFurnitureCapacity() > count4)
			{
				result = highestPlayerFurnitureByType4.getFurnitureCapacity() - count4;
			}
			break;
		}
		case SmithStation.SmithStationPolish:
		{
			Furniture highestPlayerFurnitureByType2 = player.getHighestPlayerFurnitureByType("801");
			int count2 = player.getAssignedSmithArray(SmithStation.SmithStationPolish).Count;
			if (highestPlayerFurnitureByType2.getFurnitureCapacity() > count2)
			{
				result = highestPlayerFurnitureByType2.getFurnitureCapacity() - count2;
			}
			break;
		}
		case SmithStation.SmithStationEnchant:
		{
			Furniture highestPlayerFurnitureByType = player.getHighestPlayerFurnitureByType("901");
			int count = player.getAssignedSmithArray(SmithStation.SmithStationEnchant).Count;
			if (highestPlayerFurnitureByType.getFurnitureCapacity() > count)
			{
				result = highestPlayerFurnitureByType.getFurnitureCapacity() - count;
			}
			break;
		}
		}
		return result;
	}

	public void reassignSmith()
	{
		menuState = MenuState.MenuStateSmithAssignRoleSmith;
		Player player = game.getPlayer();
		int num = retrieveStoredInput("assignSmith");
		int num2 = retrieveStoredInput("assignSmithRole");
		SmithStation role = SmithStation.SmithStationBlank;
		switch (num2)
		{
		case 1:
			role = SmithStation.SmithStationAuto;
			break;
		case 2:
			role = SmithStation.SmithStationDesign;
			break;
		case 3:
			role = SmithStation.SmithStationCraft;
			break;
		case 4:
			role = SmithStation.SmithStationPolish;
			break;
		case 5:
			role = SmithStation.SmithStationEnchant;
			break;
		}
		Smith smithByIndex = player.getSmithByIndex(num - 1);
		doSmithReassign(smithByIndex, role);
	}

	public void doSmithReassign(Smith assignSmith, SmithStation role, int aIndex = -1, Smith occupyingSmith = null)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		SmithStation currentStation = assignSmith.getCurrentStation();
		int currentStationIndex = assignSmith.getCurrentStationIndex();
		int shopLevelInt = player.getShopLevelInt();
		if (occupyingSmith != null)
		{
			switch (role)
			{
			case SmithStation.SmithStationDesign:
			{
				int furnitureLevel4 = player.getHighestPlayerFurnitureByType("601").getFurnitureLevel();
				Station phaseStation4 = gameData.getPhaseStation(SmithStation.SmithStationDesign, shopLevelInt, furnitureLevel4);
				phaseStation4.swapASmith(occupyingSmith, assignSmith);
				break;
			}
			case SmithStation.SmithStationCraft:
			{
				int furnitureLevel3 = player.getHighestPlayerFurnitureByType("701").getFurnitureLevel();
				Station phaseStation3 = gameData.getPhaseStation(SmithStation.SmithStationCraft, shopLevelInt, furnitureLevel3);
				phaseStation3.swapASmith(occupyingSmith, assignSmith);
				break;
			}
			case SmithStation.SmithStationPolish:
			{
				int furnitureLevel2 = player.getHighestPlayerFurnitureByType("801").getFurnitureLevel();
				Station phaseStation2 = gameData.getPhaseStation(SmithStation.SmithStationPolish, shopLevelInt, furnitureLevel2);
				phaseStation2.swapASmith(occupyingSmith, assignSmith);
				break;
			}
			case SmithStation.SmithStationEnchant:
			{
				int furnitureLevel = player.getHighestPlayerFurnitureByType("901").getFurnitureLevel();
				Station phaseStation = gameData.getPhaseStation(SmithStation.SmithStationEnchant, shopLevelInt, furnitureLevel);
				phaseStation.swapASmith(occupyingSmith, assignSmith);
				break;
			}
			}
			switch (currentStation)
			{
			case SmithStation.SmithStationDesign:
			{
				int furnitureLevel8 = player.getHighestPlayerFurnitureByType("601").getFurnitureLevel();
				Station phaseStation8 = gameData.getPhaseStation(SmithStation.SmithStationDesign, shopLevelInt, furnitureLevel8);
				phaseStation8.swapASmith(assignSmith, occupyingSmith);
				break;
			}
			case SmithStation.SmithStationCraft:
			{
				int furnitureLevel7 = player.getHighestPlayerFurnitureByType("701").getFurnitureLevel();
				Station phaseStation7 = gameData.getPhaseStation(SmithStation.SmithStationCraft, shopLevelInt, furnitureLevel7);
				phaseStation7.swapASmith(assignSmith, occupyingSmith);
				break;
			}
			case SmithStation.SmithStationPolish:
			{
				int furnitureLevel6 = player.getHighestPlayerFurnitureByType("801").getFurnitureLevel();
				Station phaseStation6 = gameData.getPhaseStation(SmithStation.SmithStationPolish, shopLevelInt, furnitureLevel6);
				phaseStation6.swapASmith(assignSmith, occupyingSmith);
				break;
			}
			case SmithStation.SmithStationEnchant:
			{
				int furnitureLevel5 = player.getHighestPlayerFurnitureByType("901").getFurnitureLevel();
				Station phaseStation5 = gameData.getPhaseStation(SmithStation.SmithStationEnchant, shopLevelInt, furnitureLevel5);
				phaseStation5.swapASmith(assignSmith, occupyingSmith);
				break;
			}
			}
			if (aIndex != -1)
			{
				assignSmith.setCurrentStationIndex(aIndex);
				occupyingSmith.setCurrentStationIndex(currentStationIndex);
			}
			assignSmith.setAssignedRole(role);
			occupyingSmith.setAssignedRole(currentStation);
		}
		else
		{
			switch (currentStation)
			{
			case SmithStation.SmithStationDesign:
			{
				int furnitureLevel12 = player.getHighestPlayerFurnitureByType("601").getFurnitureLevel();
				Station phaseStation12 = gameData.getPhaseStation(SmithStation.SmithStationDesign, shopLevelInt, furnitureLevel12);
				phaseStation12.unassignASmith(assignSmith);
				break;
			}
			case SmithStation.SmithStationCraft:
			{
				int furnitureLevel11 = player.getHighestPlayerFurnitureByType("701").getFurnitureLevel();
				Station phaseStation11 = gameData.getPhaseStation(SmithStation.SmithStationCraft, shopLevelInt, furnitureLevel11);
				phaseStation11.unassignASmith(assignSmith);
				break;
			}
			case SmithStation.SmithStationPolish:
			{
				int furnitureLevel10 = player.getHighestPlayerFurnitureByType("801").getFurnitureLevel();
				Station phaseStation10 = gameData.getPhaseStation(SmithStation.SmithStationPolish, shopLevelInt, furnitureLevel10);
				phaseStation10.unassignASmith(assignSmith);
				break;
			}
			case SmithStation.SmithStationEnchant:
			{
				int furnitureLevel9 = player.getHighestPlayerFurnitureByType("901").getFurnitureLevel();
				Station phaseStation9 = gameData.getPhaseStation(SmithStation.SmithStationEnchant, shopLevelInt, furnitureLevel9);
				phaseStation9.unassignASmith(assignSmith);
				break;
			}
			}
			if (aIndex != -1)
			{
				assignSmith.setCurrentStationIndex(aIndex);
			}
			switch (role)
			{
			case SmithStation.SmithStationDesign:
			{
				int furnitureLevel16 = player.getHighestPlayerFurnitureByType("601").getFurnitureLevel();
				Station phaseStation16 = gameData.getPhaseStation(SmithStation.SmithStationDesign, shopLevelInt, furnitureLevel16);
				phaseStation16.assignASmith(assignSmith);
				break;
			}
			case SmithStation.SmithStationCraft:
			{
				int furnitureLevel15 = player.getHighestPlayerFurnitureByType("701").getFurnitureLevel();
				Station phaseStation15 = gameData.getPhaseStation(SmithStation.SmithStationCraft, shopLevelInt, furnitureLevel15);
				phaseStation15.assignASmith(assignSmith);
				break;
			}
			case SmithStation.SmithStationPolish:
			{
				int furnitureLevel14 = player.getHighestPlayerFurnitureByType("801").getFurnitureLevel();
				Station phaseStation14 = gameData.getPhaseStation(SmithStation.SmithStationPolish, shopLevelInt, furnitureLevel14);
				phaseStation14.assignASmith(assignSmith);
				break;
			}
			case SmithStation.SmithStationEnchant:
			{
				int furnitureLevel13 = player.getHighestPlayerFurnitureByType("901").getFurnitureLevel();
				Station phaseStation13 = gameData.getPhaseStation(SmithStation.SmithStationEnchant, shopLevelInt, furnitureLevel13);
				phaseStation13.assignASmith(assignSmith);
				break;
			}
			}
			assignSmith.setAssignedRole(role);
		}
	}

	public void upgradeShop()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int shopUpgradeCost = player.getShopUpgradeCost();
		if (tryActionWithGold(shopUpgradeCost, allowNegative: false, useGold: true))
		{
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseShopUpgrades, string.Empty, -1 * shopUpgradeCost);
			ShopLevel shopLevel = gameData.getShopLevel(player.getShopNextLevelRefId());
			if (player.upgradeShop(shopLevel))
			{
				commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().setGameState(GameState.GameStateIdle);
				menuState = MenuState.MenuStateClosed;
			}
		}
	}

	public Weapon doUnlockWeapon(string weaponRefId)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		Weapon weaponByRefId = gameData.getWeaponByRefId(weaponRefId);
		player.unlockWeapon(weaponByRefId);
		return weaponByRefId;
	}

	public WeaponType doUnlockWeaponType(string weaponTypeRefId, bool unlockFirstWeapon)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		WeaponType weaponTypeByRefId = gameData.getWeaponTypeByRefId(weaponTypeRefId);
		player.unlockWeaponType(weaponTypeByRefId);
		if (unlockFirstWeapon)
		{
			doUnlockWeapon(weaponTypeByRefId.getFirstWeaponRefId());
		}
		return weaponTypeByRefId;
	}

	public void unlockJobClass(Hero tryUnlock)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		tryUnlock.setUnlocked(aUnlock: true);
		player.addJobClassByObject(tryUnlock);
	}

	public Furniture giveShopFurniture(string furnitureRefId)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		Furniture furniture = gameData.getOwnedFurnitureByRefID(furnitureRefId);
		if (furniture.getFurnitureRefId() == string.Empty)
		{
			furniture = gameData.getFurnitureByRefId(furnitureRefId);
			player.unlockFurniture(furniture);
		}
		return furniture;
	}

	public Color32 getQuestDifficultyColorUI(ProjectType projType, int atkReq, int spdReq, int accReq, int magReq, int timeLimit)
	{
		Player player = game.getPlayer();
		return player.calculateProjectDifficulty(projType, atkReq, spdReq, accReq, magReq, timeLimit) switch
		{
			5 => new Color32(237, 28, 36, byte.MaxValue), 
			4 => new Color32(183, 66, 58, byte.MaxValue), 
			3 => new Color32(129, 105, 80, byte.MaxValue), 
			2 => new Color32(75, 143, 101, byte.MaxValue), 
			1 => new Color32(37, 188, 15, byte.MaxValue), 
			_ => new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue), 
		};
	}

	public Vector3 getQuestDifficultyPosUI(QuestNEW quest)
	{
		Player player = game.getPlayer();
		return player.calculateProjectDifficulty(ProjectType.ProjectTypeWeapon, quest.getAtkReq(), quest.getSpdReq(), quest.getAccReq(), quest.getMagReq(), quest.getForgeTimeLimit()) switch
		{
			5 => new Vector3(32.5f, -3f, 0f), 
			4 => new Vector3(22.5f, 8.5f, 0f), 
			3 => new Vector3(0f, 14f, 0f), 
			2 => new Vector3(-19.5f, 8.5f, 0f), 
			1 => new Vector3(-32.5f, -3f, 0f), 
			_ => Vector3.zero, 
		};
	}

	public string getDifficultyString(ProjectType projType, int atkReq, int spdReq, int accReq, int magReq, int timeLimit)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		return player.calculateProjectDifficulty(projType, atkReq, spdReq, accReq, magReq, timeLimit) switch
		{
			5 => gameData.getTextByRefId("difficulty05"), 
			4 => gameData.getTextByRefId("difficulty04"), 
			3 => gameData.getTextByRefId("difficulty03"), 
			2 => gameData.getTextByRefId("difficulty02"), 
			1 => gameData.getTextByRefId("difficulty01"), 
			_ => string.Empty, 
		};
	}

	public string generateDecoEffect(Decoration deco)
	{
		GameData gameData = game.getGameData();
		string text = string.Empty;
		List<DecorationEffect> decorationEffectList = deco.getDecorationEffectList();
		foreach (DecorationEffect item in decorationEffectList)
		{
			if (text != string.Empty)
			{
				text += ", ";
			}
			float decorationBoostMult = item.getDecorationBoostMult();
			string decorationBoostRefId = item.getDecorationBoostRefId();
			switch (item.getDecorationBoostType())
			{
			case "FORGE_EXP":
				text = "Smith Forging Exp Gain x" + decorationBoostMult;
				break;
			case "TRAIN_EXP":
				text = "Smith Training Exp Gain x" + decorationBoostMult;
				break;
			case "MILESTONE_EXP":
				text = "Smith Milestone Boost Exp Gain x" + decorationBoostMult;
				break;
			case "SMITH_STA":
				text = "Smith Stamina x" + decorationBoostMult;
				break;
			case "SMITH_POW":
				text = "Smith Power x" + decorationBoostMult;
				break;
			case "SMITH_INT":
				text = "Smith Intelligence x" + decorationBoostMult;
				break;
			case "SMITH_TEC":
				text = "Smith Technique x" + decorationBoostMult;
				break;
			case "SMITH_LUC":
				text = "Smith Luck x" + decorationBoostMult;
				break;
			case "SMITH_ALL":
				text = "Smith Stats x" + decorationBoostMult;
				break;
			case "HERO_GOLD":
				text = "Quest Enemy Gold Drop x" + decorationBoostMult;
				break;
			case "QUEST_GOLD":
				text = "Quest Reward Gold x" + decorationBoostMult;
				break;
			case "FORGE_COST":
				text = "Forging Cost x" + decorationBoostMult;
				break;
			case "RECRUIT_COST":
				text = "Smith Recruitment Cost x" + decorationBoostMult;
				break;
			case "SALARY":
				text = "Smith Salary x" + decorationBoostMult;
				break;
			case "HIRE_COST":
				text = "Smith Hire Cost x" + decorationBoostMult;
				break;
			case "TRAIN_COST":
				text = "Smith Training Cost x" + decorationBoostMult;
				break;
			case "SMITH_ACTION_CHANCE":
			{
				SmithAction smithActionByRefId = gameData.getSmithActionByRefId(decorationBoostRefId);
				text = "Smith " + smithActionByRefId.getText() + " Chance x" + decorationBoostMult;
				break;
			}
			case "DOG_LOVE":
				text = "Dog Love Gain x" + decorationBoostMult;
				break;
			}
		}
		return text;
	}

	public string generateItemEffect(Item item)
	{
		GameData gameData = game.getGameData();
		string text = string.Empty;
		switch (item.getItemEffectStat())
		{
		case WeaponStat.WeaponStatAttack:
			text += gameData.getTextByRefIdWithDynText("projectStats02", "[atk]", "+" + item.getItemEffectValue());
			break;
		case WeaponStat.WeaponStatSpeed:
			text += gameData.getTextByRefIdWithDynText("projectStats03", "[spd]", "+" + item.getItemEffectValue());
			break;
		case WeaponStat.WeaponStatAccuracy:
			text += gameData.getTextByRefIdWithDynText("projectStats04", "[acc]", "+" + item.getItemEffectValue());
			break;
		case WeaponStat.WeaponStatMagic:
			text += gameData.getTextByRefIdWithDynText("projectStats05", "[mag]", "+" + item.getItemEffectValue());
			break;
		}
		if (item.getItemElement() != Element.ElementNone)
		{
			if (text != string.Empty)
			{
				text += " ";
			}
			string replaceValue = CommonAPI.convertElementToString(item.getItemElement());
			text = text + "+ " + gameData.getTextByRefIdWithDynText("menuInventory01", "[element]", replaceValue);
		}
		return text;
	}

	public void showMenuByState()
	{
		switch (menuState)
		{
		case MenuState.MenuStateForgeContract:
			showContractMenu();
			break;
		case MenuState.MenuStateSaveMenu:
			showSaveMenu();
			break;
		case MenuState.MenuStateSmithTrainType:
			showSmithTrainTypeMenu(100);
			break;
		case MenuState.MenuStateSmithAssignRoleSmith:
			showSmithRoleAssignMenu(100);
			break;
		case MenuState.MenuStateSmithAssignRoleRole:
		{
			int num = referStoredInput("assignSmith");
			break;
		}
		case MenuState.MenuStateFurnitureShop:
			showFurnitureShop(100);
			break;
		case MenuState.MenuStateEnchantmentShop:
			showEnchantmentShop(100);
			break;
		case MenuState.MenuStateDecorationShop:
			showDecorationShop(100);
			break;
		case MenuState.MenuStateShopSellMenu:
			showSellMenu(100);
			break;
		case MenuState.MenuStateDecorationsMenu:
			showManageDeco(100);
			break;
		case MenuState.MenuStateForgeJobClass:
			showForgeJobClassMenu(-1, 100);
			break;
		case MenuState.MenuStateResearchMain:
			showResearchMainMenu(100);
			break;
		}
	}

	public void showSmithLevelUpWhetsapp(Smith selectedSmith, int levelGain)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		if (selectedSmith.checkIsSmithMaxLevel())
		{
			string randomTextBySetRefIdWithDynText = gameData.getRandomTextBySetRefIdWithDynText("whetsappSmithLevelMax", "[jobClass]", selectedSmith.getSmithJob().getSmithJobName());
			gameData.addNewWhetsappMsg(selectedSmith.getSmithName(), randomTextBySetRefIdWithDynText, "Image/Smith/Portraits/" + selectedSmith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
			return;
		}
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		list.Add("[level]");
		list.Add("[jobClass]");
		list2.Add(levelGain.ToString());
		list2.Add(selectedSmith.getSmithJob().getSmithJobName());
		string randomTextBySetRefIdWithDynTextList = gameData.getRandomTextBySetRefIdWithDynTextList("whetsappSmithLevelUp", list, list2);
		gameData.addNewWhetsappMsg(selectedSmith.getSmithName(), randomTextBySetRefIdWithDynTextList, "Image/Smith/Portraits/" + selectedSmith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
	}

	public void showSmithRecruitmentMenu(int input)
	{
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().setGameState(GameState.GameStatePopEvent);
		popEventType = PopEventType.PopEventTypeRecruitment;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		List<Smith> recruitList = player.getRecruitList();
		if (recruitList.Count > 0)
		{
			if (nguiCameraGUI != null)
			{
				viewController.showHireResult();
			}
			return;
		}
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().returnToShopView();
		popEventType = PopEventType.PopEventTypeNothing;
		if (nguiCameraGUI != null)
		{
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: true, gameData.getTextByRefId("menuSmithManagement52"), gameData.getTextByRefId("menuSmithManagement12"), PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
		}
	}

	public void doStartRecruitment(RecruitmentType recruitmentType)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		int num = recruitmentType.getRecruitmentCost();
		float num2 = player.checkDecoEffect("RECRUIT_COST", string.Empty);
		if (num2 != 1f)
		{
			num = (int)((float)num * num2);
		}
		if (tryActionWithGold(num, allowNegative: false, useGold: true))
		{
			List<Smith> list = new List<Smith>();
			list.AddRange(gameData.getRecruitList(recruitmentType.getRecruitmentMaxNum(), recruitmentType.getRecruitmentSmithList(), gameScenarioByRefId.getItemLockSet()));
			player.setRecruitList(list);
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseRecruitHire, string.Empty, -1 * num);
			player.addTimedAction(TimedAction.TimedActionRecruit, recruitmentType.getRecruitmentDuration());
			if (nguiCameraGUI != null)
			{
				viewController.closeMainMenu(resume: false);
				viewController.closeTier2Menu(hide: true);
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: true, gameData.getTextByRefId("menuSmithManagement52"), recruitmentType.getRecruitmentDesc() + "\n" + gameData.getTextByRefId("menuSmithManagement24"), PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
			}
		}
	}

	public void endRecruitment()
	{
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().setGameState(GameState.GameStatePopEvent);
		popEventType = PopEventType.PopEventTypeRecruitment;
		GameData gameData = game.getGameData();
		if (nguiCameraGUI != null)
		{
			viewController.closeMainMenu(resume: false);
			viewController.closeTier2Menu(hide: false);
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, gameData.getTextByRefId("menuSmithManagement52"), gameData.getTextByRefId("menuSmithManagement25"), PopupType.PopupTypeHire, null, colorTag: false, null, map: false, string.Empty);
		}
	}

	public void hireSmith()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int num = retrieveStoredInput("hire");
		Smith recruitByIndex = player.getRecruitByIndex(num - 1);
		doHireSmithDisplay(recruitByIndex);
	}

	public void doHireSmithDisplay(Smith hired)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		List<Smith> recruitList = player.getRecruitList();
		if (hired == null || !(hired.getSmithId() != string.Empty))
		{
			return;
		}
		int num = hired.getSmithHireCost();
		float num2 = player.checkDecoEffect("HIRE_COST", string.Empty);
		if (num2 != 1f)
		{
			num = (int)((float)num * num2);
		}
		if (tryActionWithGold(num, allowNegative: false, useGold: true))
		{
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseRecruitHire, string.Empty, -1 * num);
			doHireSmith(hired);
			removeRecruit(hired);
			if (nguiCameraGUI != null)
			{
				player.updateSmithStations();
				GameObject.Find("StationController").GetComponent<StationController>().assignSmithStations();
				charAnimCtr.hireSmith(hired);
				viewController.closeSmithManagePopup(hide: true);
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, string.Empty, gameData.getTextByRefIdWithDynText("menuSmithManagement32", "[smithName]", hired.getSmithName()), PopupType.PopupTypeHire, null, colorTag: false, null, map: false, string.Empty);
			}
		}
	}

	public void doHireSmith(Smith hired)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		SmithAction smithActionByRefId = gameData.getSmithActionByRefId("101");
		player.hireSmithByObject(hired);
		hired.setSmithAction(smithActionByRefId, -1);
		setDoUnlockCheck();
		if (nguiCameraGUI != null)
		{
			List<Smith> smithList = player.getSmithList();
			GameObject.Find("Panel_SmithList").GetComponent<GUISmithListMenuController>().loadSmithList(smithList);
		}
	}

	public void removeRecruit(Smith hired)
	{
		Player player = game.getPlayer();
		List<Smith> recruitList = player.getRecruitList();
		recruitList.Remove(hired);
		player.setRecruitList(recruitList);
	}

	public void doHireFireSmith(Smith hired, Smith fired)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		if (!(fired.getSmithId() != string.Empty) || hired == null || !(hired.getSmithId() != string.Empty))
		{
			return;
		}
		List<Smith> recruitList = player.getRecruitList();
		int num = hired.getSmithHireCost();
		float num2 = player.checkDecoEffect("HIRE_COST", string.Empty);
		if (num2 != 1f)
		{
			num = (int)((float)num * num2);
		}
		if (tryActionWithGold(num, allowNegative: false, useGold: true))
		{
			player.fireSmithByObject(fired);
			fired.clearSmithEffects();
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseRecruitHire, string.Empty, -1 * num);
			doHireSmith(hired);
			recruitList.Remove(hired);
			player.setRecruitList(recruitList);
			popEventType = PopEventType.PopEventTypeRecruitment;
			if (nguiCameraGUI != null)
			{
				viewController.closeFireHire(hide: true);
				charAnimCtr.hireSmith(hired);
				charAnimCtr.fireSmith(fired);
				string displayText = gameData.getTextByRefIdWithDynText("menuSmithManagement34", "[smithName]", fired.getSmithName()) + "\n" + gameData.getTextByRefIdWithDynText("menuSmithManagement32", "[smithName]", hired.getSmithName());
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, string.Empty, displayText, PopupType.PopupTypeHire, null, colorTag: false, null, map: false, string.Empty);
			}
		}
	}

	public void doFireSmith(Smith fired)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		if (fired.getSmithId() != string.Empty)
		{
			player.fireSmithByObject(fired);
			fired.clearSmithEffects();
		}
	}

	public void startForging()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		Weapon lastSelectWeapon = player.getLastSelectWeapon();
		ProjectType aProjectType = ProjectType.ProjectTypeWeapon;
		if (lastSelectWeapon.getWeaponTypeRefId() == "901")
		{
			aProjectType = ProjectType.ProjectTypeUnique;
		}
		int aMaxBoost = 1;
		string weaponRefId = lastSelectWeapon.getWeaponRefId();
		if (weaponRefId != null && weaponRefId == "9011003")
		{
			aMaxBoost = 2;
		}
		Project project = new Project(player.getNextProjectId().ToString(), "1", string.Empty, lastSelectWeapon.getWeaponDesc(), 0, 0, 0, aProjectType, -1, 0, 0, 0, 0, aMaxBoost, 0);
		project.setProjectWeapon(lastSelectWeapon);
		project.setProjectName(lastSelectWeapon.getWeaponName());
		if (tryStartProject(project, 0))
		{
			GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
			string gameLockSet = gameScenarioByRefId.getGameLockSet();
			int completedTutorialIndex = player.getCompletedTutorialIndex();
			if (gameData.checkFeatureIsUnlocked(gameLockSet, "FORGEINCIDENT", completedTutorialIndex))
			{
				player.addTimedAction(TimedAction.TimedActionForgeIncident, Random.Range(10, 20));
			}
			lastSelectWeapon.addTimesUsed(1);
			SmithAction smithActionByRefId = gameData.getSmithActionByRefId("102");
			player.resetSmithsWorkState(smithActionByRefId, -1);
			foreach (Smith inShopSmith in player.getInShopSmithList())
			{
				charAnimCtr.showStartForgeBubble(inShopSmith);
			}
			menuState = MenuState.MenuStateClosed;
			commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().setGameState(GameState.GameStateIdle);
			audioController.playForgingAudio();
			if (nguiCameraGUI != null)
			{
				viewController.showProjectProgress();
				viewController.closeForgeMenuNewPopup(resumeFromPlayerPause: true);
			}
		}
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().showTimeStatus();
	}

	public bool tryStartProject(Project nextProject, int returnGoldAmt)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		Project currentProject = player.getCurrentProject();
		if (currentProject.getProjectType() == ProjectType.ProjectTypeNothing)
		{
			ProjectType projectType = nextProject.getProjectType();
			Dictionary<string, int> materialList = nextProject.getProjectWeapon().getMaterialList();
			foreach (string key in materialList.Keys)
			{
				int useNum = materialList[key];
				gameData.getItemByRefId(key).tryUseItem(useNum, isUse: true);
			}
			int count = player.getSmithList().Count;
			nextProject.setProgressMax(CommonAPI.getProjectGrowthNum(count));
			if (projectType == ProjectType.ProjectTypeContract || projectType == ProjectType.ProjectTypeResearch)
			{
				nextProject.setEndTime(player.getPlayerTimeLong() + nextProject.getTimeLimit());
			}
			player.startCurrentProject(nextProject);
			commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().resetPreviousProjectStatsList();
			GameObject gameObject = GameObject.Find("Panel_BottomMenu");
			if (gameObject != null)
			{
				gameObject.GetComponent<BottomMenuController>().refreshBottomButtons();
			}
			return true;
		}
		menuState = MenuState.MenuStateClosed;
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().setGameState(GameState.GameStateIdle);
		if (nguiCameraGUI != null)
		{
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, gameData.getTextByRefId("errorTitle01"), gameData.getTextByRefId("menuForgeConfirm05"), PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
		}
		return false;
	}

	public void showForgeBoost1Design()
	{
		Player player = game.getPlayer();
		List<Smith> list = new List<Smith>();
		List<Smith> designSmithArray = player.getDesignSmithArray(includeAway: false);
		GameData gameData = game.getGameData();
		List<Smith> outsourceSmithList = gameData.getOutsourceSmithList("DESIGN", player.getShopLevelInt(), player.getPlayerGold());
		list.AddRange(designSmithArray);
		list.AddRange(outsourceSmithList);
		if (list.Count > 0)
		{
			commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().setGameState(GameState.GameStatePopEvent);
			popEventType = PopEventType.PopEventTypeBoost1Design;
			if (nguiCameraGUI != null)
			{
				GameObject.Find("Grid_topRight").GetComponent<GUIProgressHUDController>().refreshStats();
				viewController.showSelectSmithNewPopup(SmithSelectPopupType.SmithSelectPopupTypeBoostDesign);
			}
		}
		else
		{
			endPopEvent();
			if (nguiCameraGUI != null)
			{
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: true, gameData.getTextByRefId("errorTitle02"), gameData.getTextByRefId("menuForgeBoost03"), PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
			}
		}
	}

	private int calculateBoostNum(SmithMood aMood)
	{
		int a = CommonAPI.convertMoodStateToInt(aMood) + (int)(0.9f + Random.Range(0f, 1f)) + (int)(0.54f + Random.Range(0f, 1f)) + (int)(0.162f + Random.Range(0f, 1f));
		a = Mathf.Max(a, 1);
		return Mathf.Min(a, 7);
	}

	public void startDesignBoost(bool sorted = false)
	{
		endPopEvent();
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		List<Smith> list = new List<Smith>();
		List<Smith> list2 = player.getDesignSmithArray(includeAway: false);
		if (sorted)
		{
			list2 = list2.OrderByDescending((Smith x) => x.getSmithPower()).ToList();
		}
		List<Smith> outsourceSmithList = gameData.getOutsourceSmithList("DESIGN", player.getShopLevelInt(), player.getPlayerGold());
		list.AddRange(list2);
		list.AddRange(outsourceSmithList);
		Smith lastSelectSmith = player.getLastSelectSmith();
		player.clearLastSelectSmith();
		SmithMood moodState = lastSelectSmith.getMoodState();
		if (lastSelectSmith.checkOutsource())
		{
			lastSelectSmith.addTimesHired(1);
			player.reduceGold(lastSelectSmith.getSmithHireCost(), allowNegative: true);
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseOutsource, string.Empty, -1 * lastSelectSmith.getSmithHireCost());
			audioController.playPurchaseAudio();
		}
		Project currentProject = player.getCurrentProject();
		Weapon currentProjectWeapon = player.getCurrentProjectWeapon();
		Furniture highestPlayerFurnitureByType = player.getHighestPlayerFurnitureByType("601");
		int furnitureLevel = highestPlayerFurnitureByType.getFurnitureLevel();
		currentProject.addUsedSmith(lastSelectSmith);
		string empty = string.Empty;
		string empty2 = string.Empty;
		empty2 = lastSelectSmith.getSmithName() + ": \"" + CommonAPI.generateBeforeBoostText(furnitureLevel) + "\"\n\n";
		empty = empty2;
		int num = calculateBoostNum(moodState);
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		float num6 = 1f;
		if (currentProject.checkBoostPenalty(WeaponStat.WeaponStatAttack) > 0)
		{
			num6 = 0.6f;
		}
		int num7 = 1 + 2 * (furnitureLevel - 1);
		List<string> list3 = new List<string>();
		List<Boost> list4 = new List<Boost>();
		for (int i = 0; i < num; i++)
		{
			float num8 = (0.8f + 0.1f * (float)i) * num6;
			int num9 = (int)(num8 * (float)lastSelectSmith.getSmithPower() * Random.Range(0.08f, 0.12f) * 1.2f * currentProjectWeapon.getAtkMult() * (float)num7 * 0.7f);
			int num10 = (int)(num8 * (float)lastSelectSmith.getSmithIntelligence() * Random.Range(0.08f, 0.12f) * 1.5f * currentProjectWeapon.getSpdMult() * (float)num7 * 0.7f);
			int num11 = (int)(num8 * (float)lastSelectSmith.getSmithTechnique() * Random.Range(0.08f, 0.12f) * 0.2f * currentProjectWeapon.getAccMult() * (float)num7 * 0.7f);
			int num12 = (int)(num8 * (float)lastSelectSmith.getSmithLuck() * Random.Range(0.08f, 0.12f) * 0.1f * currentProjectWeapon.getMagMult() * (float)num7 * 0.7f);
			if (!gameData.checkFeatureIsUnlocked(gameLockSet, "ENCHANT", completedTutorialIndex))
			{
				num12 = 0;
			}
			num9 = (int)((float)num9 * getMoodForgingStatModifier(lastSelectSmith, isMilestone: true));
			num10 = (int)((float)num10 * getMoodForgingStatModifier(lastSelectSmith, isMilestone: true));
			num11 = (int)((float)num11 * getMoodForgingStatModifier(lastSelectSmith, isMilestone: true));
			num12 = (int)((float)num12 * getMoodForgingStatModifier(lastSelectSmith, isMilestone: true));
			num2 += num9;
			num3 += num10;
			num4 += num11;
			num5 += num12;
			empty = empty + gameData.getTextByRefIdWithDynText("menuForgeBoost08", "[boostNum]", (i + 1).ToString()) + "\n";
			empty = empty + CommonAPI.generateBoostString(num9, num10, num11, num12) + "\n\n";
			string text = gameData.getTextByRefIdWithDynText("menuForgeBoost08", "[boostNum]", (i + 1).ToString()) + "\n";
			list4.Add(new Boost(num9, num11, num10, num12));
			text = CommonAPI.generateBoostString(num9, num10, num11, num12) + "\n";
			list3.Add(text);
		}
		List<int> currentProjectStats = player.getCurrentProjectStats();
		int num13 = num2 + num3 + num4 + num5;
		string empty3 = string.Empty;
		empty3 = ((!(nguiCameraGUI != null)) ? (lastSelectSmith.getSmithName() + ": \"" + CommonAPI.generateAfterBoostText(num) + "\"") : ("\"" + CommonAPI.generateAfterBoostText(num) + "\""));
		empty += empty3;
		list3.Add(empty3);
		string text2 = string.Empty;
		bool hasLevelUp = false;
		if (num13 > 0)
		{
			if (!lastSelectSmith.checkOutsource() && !lastSelectSmith.checkIsSmithMaxLevel())
			{
				int num14 = (int)((float)num13 * 0.5f + 0.5f);
				string text3 = string.Empty;
				float num15 = player.checkDecoEffect("MILESTONE_EXP", string.Empty);
				if (num15 != 1f)
				{
					int num16 = num14;
					num14 = (int)(num15 * (float)num14);
					if (num15 > 1f)
					{
						int num17 = num14 - num16;
						if (num17 == 0)
						{
							num17 = 1;
							num14++;
						}
						text3 = "<color=green>(+" + num17 + ")</color>";
					}
					else if (num15 < 1f)
					{
						int num18 = num16 - num14;
						if (num18 == 0)
						{
							num18 = 1;
							num14--;
						}
						text3 = "<color=red>(-" + num18 + ")</color>";
					}
				}
				Dictionary<string, int> dictionary = lastSelectSmith.addSmithExp(num14);
				text2 = "\n" + lastSelectSmith.getSmithName() + " gained " + num14 + text3 + " Exp!";
				if (dictionary["lvlGain"] > 0)
				{
					string text4 = text2;
					text2 = text4 + " Smith Level +" + dictionary["lvlGain"] + "!";
					hasLevelUp = true;
					showSmithLevelUpWhetsapp(lastSelectSmith, dictionary["lvlGain"]);
				}
			}
			empty += text2;
		}
		currentProject.addBoost(WeaponStat.WeaponStatAttack);
		if (nguiCameraGUI != null)
		{
			viewController.showBoostPopup(ProcessPopupType.ProcessPopupTypeDesign, lastSelectSmith, empty3, currentProjectStats, player.getCurrentProjectStats(), list4, hasLevelUp);
		}
	}

	public void showForgeBoost2Craft()
	{
		Player player = game.getPlayer();
		List<Smith> list = new List<Smith>();
		List<Smith> craftSmithArray = player.getCraftSmithArray(includeAway: false);
		GameData gameData = game.getGameData();
		List<Smith> outsourceSmithList = gameData.getOutsourceSmithList("CRAFT", player.getShopLevelInt(), player.getPlayerGold());
		list.AddRange(craftSmithArray);
		list.AddRange(outsourceSmithList);
		if (list.Count > 0)
		{
			commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().setGameState(GameState.GameStatePopEvent);
			popEventType = PopEventType.PopEventTypeBoost2Craft;
			if (nguiCameraGUI != null)
			{
				GameObject.Find("Grid_topRight").GetComponent<GUIProgressHUDController>().refreshStats();
				viewController.showSelectSmithNewPopup(SmithSelectPopupType.SmithSelectPopupTypeBoostCraft);
			}
		}
		else
		{
			endPopEvent();
			if (nguiCameraGUI != null)
			{
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: true, gameData.getTextByRefId("errorTitle02"), gameData.getTextByRefId("menuForgeBoost13"), PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
			}
		}
	}

	public void startCraftBoost(bool sorted = false)
	{
		endPopEvent();
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		List<Smith> list = new List<Smith>();
		List<Smith> list2 = player.getCraftSmithArray(includeAway: false);
		if (sorted)
		{
			list2 = list2.OrderByDescending((Smith x) => x.getSmithIntelligence()).ToList();
		}
		List<Smith> outsourceSmithList = gameData.getOutsourceSmithList("CRAFT", player.getShopLevelInt(), player.getPlayerGold());
		list.AddRange(list2);
		list.AddRange(outsourceSmithList);
		Smith lastSelectSmith = player.getLastSelectSmith();
		player.clearLastSelectSmith();
		SmithMood moodState = lastSelectSmith.getMoodState();
		if (lastSelectSmith.checkOutsource())
		{
			lastSelectSmith.addTimesHired(1);
			player.reduceGold(lastSelectSmith.getSmithHireCost(), allowNegative: true);
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseOutsource, string.Empty, -1 * lastSelectSmith.getSmithHireCost());
			audioController.playPurchaseAudio();
		}
		Project currentProject = player.getCurrentProject();
		Weapon currentProjectWeapon = player.getCurrentProjectWeapon();
		Furniture highestPlayerFurnitureByType = player.getHighestPlayerFurnitureByType("701");
		int furnitureLevel = highestPlayerFurnitureByType.getFurnitureLevel();
		currentProject.addUsedSmith(lastSelectSmith);
		string empty = string.Empty;
		string empty2 = string.Empty;
		empty2 = lastSelectSmith.getSmithName() + ": \"" + CommonAPI.generateBeforeBoostText(furnitureLevel) + "\"\n\n";
		empty = empty2;
		int num = calculateBoostNum(moodState);
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		float num6 = 1f;
		if (currentProject.checkBoostPenalty(WeaponStat.WeaponStatSpeed) > 0)
		{
			num6 = 0.6f;
		}
		int num7 = 1 + 2 * (furnitureLevel - 1);
		List<string> list3 = new List<string>();
		List<Boost> list4 = new List<Boost>();
		for (int i = 0; i < num; i++)
		{
			float num8 = (0.8f + 0.1f * (float)i) * num6;
			int num9 = (int)(num8 * (float)lastSelectSmith.getSmithPower() * Random.Range(0.08f, 0.12f) * 0.5f * currentProjectWeapon.getAtkMult() * (float)num7 * 0.7f);
			int num10 = (int)(num8 * (float)lastSelectSmith.getSmithIntelligence() * Random.Range(0.08f, 0.12f) * 1.2f * currentProjectWeapon.getSpdMult() * (float)num7 * 0.7f);
			int num11 = (int)(num8 * (float)lastSelectSmith.getSmithTechnique() * Random.Range(0.08f, 0.12f) * 0.1f * currentProjectWeapon.getAccMult() * (float)num7 * 0.7f);
			int num12 = (int)(num8 * (float)lastSelectSmith.getSmithLuck() * Random.Range(0.08f, 0.12f) * 0.2f * currentProjectWeapon.getMagMult() * (float)num7 * 0.7f);
			if (!gameData.checkFeatureIsUnlocked(gameLockSet, "ENCHANT", completedTutorialIndex))
			{
				num12 = 0;
			}
			num9 = (int)((float)num9 * getMoodForgingStatModifier(lastSelectSmith, isMilestone: true));
			num10 = (int)((float)num10 * getMoodForgingStatModifier(lastSelectSmith, isMilestone: true));
			num11 = (int)((float)num11 * getMoodForgingStatModifier(lastSelectSmith, isMilestone: true));
			num12 = (int)((float)num12 * getMoodForgingStatModifier(lastSelectSmith, isMilestone: true));
			num2 += num9;
			num3 += num10;
			num4 += num11;
			num5 += num12;
			empty = empty + gameData.getTextByRefIdWithDynText("menuForgeBoost08", "[boostNum]", (i + 1).ToString()) + "\n";
			empty = empty + CommonAPI.generateBoostString(num9, num10, num11, num12) + "\n\n";
			string text = gameData.getTextByRefIdWithDynText("menuForgeBoost08", "[boostNum]", (i + 1).ToString()) + "\n";
			list4.Add(new Boost(num9, num11, num10, num12));
			text = CommonAPI.generateBoostString(num9, num10, num11, num12) + "\n";
			list3.Add(text);
		}
		List<int> currentProjectStats = player.getCurrentProjectStats();
		int num13 = num2 + num3 + num4 + num5;
		string empty3 = string.Empty;
		empty3 = ((!(nguiCameraGUI != null)) ? (lastSelectSmith.getSmithName() + ": \"" + CommonAPI.generateAfterBoostText(num) + "\"") : ("\"" + CommonAPI.generateAfterBoostText(num) + "\""));
		empty += empty3;
		list3.Add(empty3);
		string text2 = string.Empty;
		bool hasLevelUp = false;
		if (num13 > 0)
		{
			if (!lastSelectSmith.checkOutsource() && !lastSelectSmith.checkIsSmithMaxLevel())
			{
				int num14 = (int)((float)num13 * 0.5f + 0.5f);
				string text3 = string.Empty;
				float num15 = player.checkDecoEffect("MILESTONE_EXP", string.Empty);
				if (num15 != 1f)
				{
					int num16 = num14;
					num14 = (int)(num15 * (float)num14);
					if (num15 > 1f)
					{
						int num17 = num14 - num16;
						if (num17 == 0)
						{
							num17 = 1;
							num14++;
						}
						text3 = "<color=green>(+" + num17 + ")</color>";
					}
					else if (num15 < 1f)
					{
						int num18 = num16 - num14;
						if (num18 == 0)
						{
							num18 = 1;
							num14--;
						}
						text3 = "<color=red>(-" + num18 + ")</color>";
					}
				}
				Dictionary<string, int> dictionary = lastSelectSmith.addSmithExp(num14);
				text2 = "\n" + lastSelectSmith.getSmithName() + " gained " + num14 + text3 + " Exp!";
				if (dictionary["lvlGain"] > 0)
				{
					string text4 = text2;
					text2 = text4 + " Smith Level +" + dictionary["lvlGain"] + "!";
					hasLevelUp = true;
					showSmithLevelUpWhetsapp(lastSelectSmith, dictionary["lvlGain"]);
				}
			}
			empty += text2;
		}
		currentProject.addBoost(WeaponStat.WeaponStatSpeed);
		if (nguiCameraGUI != null)
		{
			GameObject.Find("Grid_topRight").GetComponent<GUIProgressHUDController>().refreshStats();
			viewController.showBoostPopup(ProcessPopupType.ProcessPopupTypeCraft, lastSelectSmith, empty3, currentProjectStats, player.getCurrentProjectStats(), list4, hasLevelUp);
		}
	}

	public void showForgeBoost3Polish()
	{
		Player player = game.getPlayer();
		List<Smith> list = new List<Smith>();
		List<Smith> polishSmithArray = player.getPolishSmithArray(includeAway: false);
		GameData gameData = game.getGameData();
		List<Smith> outsourceSmithList = gameData.getOutsourceSmithList("POLISH", player.getShopLevelInt(), player.getPlayerGold());
		list.AddRange(polishSmithArray);
		list.AddRange(outsourceSmithList);
		if (list.Count > 0)
		{
			commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().setGameState(GameState.GameStatePopEvent);
			popEventType = PopEventType.PopEventTypeBoost3Polish;
			if (nguiCameraGUI != null)
			{
				GameObject.Find("Grid_topRight").GetComponent<GUIProgressHUDController>().refreshStats();
				viewController.showSelectSmithNewPopup(SmithSelectPopupType.SmithSelectPopupTypeBoostPolish);
			}
		}
		else
		{
			endPopEvent();
			if (nguiCameraGUI != null)
			{
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: true, gameData.getTextByRefId("errorTitle02"), gameData.getTextByRefId("menuForgeBoost19"), PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
			}
		}
	}

	public void startPolishBoost(bool sorted = false)
	{
		endPopEvent();
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		List<Smith> list = new List<Smith>();
		List<Smith> list2 = player.getPolishSmithArray(includeAway: false);
		if (sorted)
		{
			list2 = list2.OrderByDescending((Smith x) => x.getSmithTechnique()).ToList();
		}
		List<Smith> outsourceSmithList = gameData.getOutsourceSmithList("POLISH", player.getShopLevelInt(), player.getPlayerGold());
		list.AddRange(list2);
		list.AddRange(outsourceSmithList);
		Smith lastSelectSmith = player.getLastSelectSmith();
		player.clearLastSelectSmith();
		SmithMood moodState = lastSelectSmith.getMoodState();
		if (lastSelectSmith.checkOutsource())
		{
			lastSelectSmith.addTimesHired(1);
			player.reduceGold(lastSelectSmith.getSmithHireCost(), allowNegative: true);
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseOutsource, string.Empty, -1 * lastSelectSmith.getSmithHireCost());
			audioController.playPurchaseAudio();
		}
		Project currentProject = player.getCurrentProject();
		Weapon currentProjectWeapon = player.getCurrentProjectWeapon();
		Furniture highestPlayerFurnitureByType = player.getHighestPlayerFurnitureByType("801");
		int furnitureLevel = highestPlayerFurnitureByType.getFurnitureLevel();
		currentProject.addUsedSmith(lastSelectSmith);
		string empty = string.Empty;
		string empty2 = string.Empty;
		empty2 = lastSelectSmith.getSmithName() + ": \"" + CommonAPI.generateBeforeBoostText(furnitureLevel) + "\"\n\n";
		empty = empty2;
		int num = calculateBoostNum(moodState);
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		float num6 = 1f;
		if (currentProject.checkBoostPenalty(WeaponStat.WeaponStatAccuracy) > 0)
		{
			num6 = 0.6f;
		}
		int num7 = 1 + 2 * (furnitureLevel - 1);
		List<string> list3 = new List<string>();
		List<Boost> list4 = new List<Boost>();
		for (int i = 0; i < num; i++)
		{
			float num8 = (0.8f + 0.1f * (float)i) * num6;
			int num9 = (int)(num8 * (float)lastSelectSmith.getSmithPower() * Random.Range(0.08f, 0.12f) * 0.2f * currentProjectWeapon.getAtkMult() * (float)num7 * 0.7f);
			int num10 = (int)(num8 * (float)lastSelectSmith.getSmithIntelligence() * Random.Range(0.08f, 0.12f) * 0.1f * currentProjectWeapon.getSpdMult() * (float)num7 * 0.7f);
			int num11 = (int)(num8 * (float)lastSelectSmith.getSmithTechnique() * Random.Range(0.08f, 0.12f) * 1.2f * currentProjectWeapon.getAccMult() * (float)num7 * 0.7f);
			int num12 = (int)(num8 * (float)lastSelectSmith.getSmithLuck() * Random.Range(0.08f, 0.12f) * 0.5f * currentProjectWeapon.getMagMult() * (float)num7 * 0.7f);
			if (!gameData.checkFeatureIsUnlocked(gameLockSet, "ENCHANT", completedTutorialIndex))
			{
				num12 = 0;
			}
			num9 = (int)((float)num9 * getMoodForgingStatModifier(lastSelectSmith, isMilestone: true));
			num10 = (int)((float)num10 * getMoodForgingStatModifier(lastSelectSmith, isMilestone: true));
			num11 = (int)((float)num11 * getMoodForgingStatModifier(lastSelectSmith, isMilestone: true));
			num12 = (int)((float)num12 * getMoodForgingStatModifier(lastSelectSmith, isMilestone: true));
			num2 += num9;
			num3 += num10;
			num4 += num11;
			num5 += num12;
			empty = empty + gameData.getTextByRefIdWithDynText("menuForgeBoost08", "[boostNum]", (i + 1).ToString()) + "\n";
			empty = empty + CommonAPI.generateBoostString(num9, num10, num11, num12) + "\n\n";
			string text = gameData.getTextByRefIdWithDynText("menuForgeBoost08", "[boostNum]", (i + 1).ToString()) + "\n";
			list4.Add(new Boost(num9, num11, num10, num12));
			text = CommonAPI.generateBoostString(num9, num10, num11, num12) + "\n";
			list3.Add(text);
		}
		List<int> currentProjectStats = player.getCurrentProjectStats();
		int num13 = num2 + num3 + num4 + num5;
		string empty3 = string.Empty;
		empty3 = ((!(nguiCameraGUI != null)) ? (lastSelectSmith.getSmithName() + ": \"" + CommonAPI.generateAfterBoostText(num) + "\"") : ("\"" + CommonAPI.generateAfterBoostText(num) + "\""));
		empty += empty3;
		list3.Add(empty3);
		string text2 = string.Empty;
		bool hasLevelUp = false;
		if (num13 > 0)
		{
			if (!lastSelectSmith.checkOutsource() && !lastSelectSmith.checkIsSmithMaxLevel())
			{
				int num14 = (int)((float)num13 * 0.5f + 0.5f);
				string text3 = string.Empty;
				float num15 = player.checkDecoEffect("MILESTONE_EXP", string.Empty);
				if (num15 != 1f)
				{
					int num16 = num14;
					num14 = (int)(num15 * (float)num14);
					if (num15 > 1f)
					{
						int num17 = num14 - num16;
						if (num17 == 0)
						{
							num17 = 1;
							num14++;
						}
						text3 = "<color=green>(+" + num17 + ")</color>";
					}
					else if (num15 < 1f)
					{
						int num18 = num16 - num14;
						if (num18 == 0)
						{
							num18 = 1;
							num14--;
						}
						text3 = "<color=red>(-" + num18 + ")</color>";
					}
				}
				Dictionary<string, int> dictionary = lastSelectSmith.addSmithExp(num14);
				text2 = "\n" + lastSelectSmith.getSmithName() + " gained " + num14 + text3 + " Exp!";
				if (dictionary["lvlGain"] > 0)
				{
					string text4 = text2;
					text2 = text4 + " Smith Level +" + dictionary["lvlGain"] + "!";
					hasLevelUp = true;
					showSmithLevelUpWhetsapp(lastSelectSmith, dictionary["lvlGain"]);
				}
			}
			empty += text2;
		}
		currentProject.addBoost(WeaponStat.WeaponStatAccuracy);
		if (nguiCameraGUI != null)
		{
			GameObject.Find("Grid_topRight").GetComponent<GUIProgressHUDController>().refreshStats();
			viewController.showBoostPopup(ProcessPopupType.ProcessPopupTypePolish, lastSelectSmith, empty3, currentProjectStats, player.getCurrentProjectStats(), list4, hasLevelUp);
		}
	}

	public void showForgeBoost4Enchant()
	{
		Player player = game.getPlayer();
		List<Smith> list = new List<Smith>();
		List<Smith> enchantSmithArray = player.getEnchantSmithArray(includeAway: false);
		GameData gameData = game.getGameData();
		List<Smith> outsourceSmithList = gameData.getOutsourceSmithList("ENCHANT", player.getShopLevelInt(), player.getPlayerGold());
		list.AddRange(enchantSmithArray);
		list.AddRange(outsourceSmithList);
		if (list.Count > 0)
		{
			commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().setGameState(GameState.GameStatePopEvent);
			popEventType = PopEventType.PopEventTypeBoost4Enchant;
			if (nguiCameraGUI != null)
			{
				GameObject.Find("Grid_topRight").GetComponent<GUIProgressHUDController>().refreshStats();
				viewController.showSelectSmithNewPopup(SmithSelectPopupType.SmithSelectPopupTypeBoostEnchant);
			}
		}
		else
		{
			endPopEvent();
			if (nguiCameraGUI != null)
			{
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: true, gameData.getTextByRefId("errorTitle02"), gameData.getTextByRefId("menuForgeBoost25"), PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
			}
		}
	}

	public void showForgeBoost4EnchantItemSelect(int selectedIndex, int input, bool noScroll = false)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		List<Smith> list = new List<Smith>();
		List<Smith> enchantSmithArray = player.getEnchantSmithArray(includeAway: false);
		List<Smith> outsourceSmithList = gameData.getOutsourceSmithList("ENCHANT", player.getShopLevelInt(), player.getPlayerGold());
		list.AddRange(enchantSmithArray);
		list.AddRange(outsourceSmithList);
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().setGameState(GameState.GameStatePopEvent);
		popEventType = PopEventType.PopEventTypeBoost4EnchantItem;
		List<Item> itemListByType = gameData.getItemListByType(ItemType.ItemTypeEnhancement, ownedOnly: true, includeSpecial: true, string.Empty);
		if (itemListByType.Count > 0)
		{
			if (nguiCameraGUI != null)
			{
				viewController.showEnchantmentSelectNew();
			}
		}
		else if (nguiCameraGUI != null)
		{
			showPopEvent(PopEventType.PopEventTypeNaming);
		}
	}

	public void startEnchantBoost(bool sorted = false)
	{
		endPopEvent();
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		List<Smith> list = new List<Smith>();
		List<Smith> list2 = player.getEnchantSmithArray(includeAway: false);
		if (sorted)
		{
			list2 = list2.OrderByDescending((Smith x) => x.getSmithLuck()).ToList();
		}
		List<Smith> outsourceSmithList = gameData.getOutsourceSmithList("ENCHANT", player.getShopLevelInt(), player.getPlayerGold());
		list.AddRange(list2);
		list.AddRange(outsourceSmithList);
		Smith lastSelectSmith = player.getLastSelectSmith();
		player.clearLastSelectSmith();
		SmithMood moodState = lastSelectSmith.getMoodState();
		if (lastSelectSmith.checkOutsource())
		{
			lastSelectSmith.addTimesHired(1);
			player.reduceGold(lastSelectSmith.getSmithHireCost(), allowNegative: true);
			player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseOutsource, string.Empty, -1 * lastSelectSmith.getSmithHireCost());
			audioController.playPurchaseAudio();
		}
		Project currentProject = player.getCurrentProject();
		Weapon currentProjectWeapon = player.getCurrentProjectWeapon();
		Furniture highestPlayerFurnitureByType = player.getHighestPlayerFurnitureByType("901");
		int furnitureLevel = highestPlayerFurnitureByType.getFurnitureLevel();
		currentProject.addUsedSmith(lastSelectSmith);
		string empty = string.Empty;
		string empty2 = string.Empty;
		empty2 = lastSelectSmith.getSmithName() + ": \"" + CommonAPI.generateBeforeBoostText(3) + "\"\n\n";
		empty = empty2;
		int num = calculateBoostNum(moodState);
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		float num6 = 1f;
		if (currentProject.checkBoostPenalty(WeaponStat.WeaponStatMagic) > 0)
		{
			num6 = 0.6f;
		}
		int num7 = 1 + 2 * (furnitureLevel - 1);
		List<string> list3 = new List<string>();
		List<Boost> list4 = new List<Boost>();
		for (int i = 0; i < num; i++)
		{
			float num8 = (0.8f + 0.1f * (float)i) * num6;
			int num9 = (int)(num8 * (float)lastSelectSmith.getSmithPower() * Random.Range(0.08f, 0.12f) * 0.1f * currentProjectWeapon.getAtkMult() * (float)num7 * 0.7f);
			int num10 = (int)(num8 * (float)lastSelectSmith.getSmithIntelligence() * Random.Range(0.08f, 0.12f) * 0.2f * currentProjectWeapon.getSpdMult() * (float)num7 * 0.7f);
			int num11 = (int)(num8 * (float)lastSelectSmith.getSmithTechnique() * Random.Range(0.08f, 0.12f) * 0.5f * currentProjectWeapon.getAccMult() * (float)num7 * 0.7f);
			int num12 = (int)(num8 * (float)lastSelectSmith.getSmithLuck() * Random.Range(0.08f, 0.12f) * 1.2f * currentProjectWeapon.getMagMult() * (float)num7 * 0.7f);
			if (!gameData.checkFeatureIsUnlocked(gameLockSet, "ENCHANT", completedTutorialIndex))
			{
				num12 = 0;
			}
			num9 = (int)((float)num9 * getMoodForgingStatModifier(lastSelectSmith, isMilestone: true));
			num10 = (int)((float)num10 * getMoodForgingStatModifier(lastSelectSmith, isMilestone: true));
			num11 = (int)((float)num11 * getMoodForgingStatModifier(lastSelectSmith, isMilestone: true));
			num12 = (int)((float)num12 * getMoodForgingStatModifier(lastSelectSmith, isMilestone: true));
			num2 += num9;
			num3 += num10;
			num4 += num11;
			num5 += num12;
			empty = empty + gameData.getTextByRefIdWithDynText("menuForgeBoost08", "[boostNum]", (i + 1).ToString()) + "\n";
			empty = empty + CommonAPI.generateBoostString(num9, num10, num11, num12) + "\n\n";
			string text = gameData.getTextByRefIdWithDynText("menuForgeBoost08", "[boostNum]", (i + 1).ToString()) + "\n";
			list4.Add(new Boost(num9, num11, num10, num12));
			text = CommonAPI.generateBoostString(num9, num10, num11, num12) + "\n";
			list3.Add(text);
		}
		List<int> currentProjectStats = player.getCurrentProjectStats();
		int num13 = num2 + num3 + num4 + num5;
		int num14 = -1;
		Item item = null;
		string empty3 = string.Empty;
		Boost enchantBoost = null;
		if (num14 > 0)
		{
			List<Item> itemListByType = gameData.getItemListByType(ItemType.ItemTypeEnhancement, ownedOnly: true, includeSpecial: true, string.Empty);
			Item item2 = itemListByType[num14 - 1];
			item = itemListByType[num14 - 1];
			enchantBoost = addEnchantItem(item);
			if (nguiCameraGUI == null)
			{
				list3.Add(empty3);
			}
		}
		string empty4 = string.Empty;
		empty4 = ((!(nguiCameraGUI != null)) ? (lastSelectSmith.getSmithName() + ": \"" + CommonAPI.generateAfterBoostText(num) + "\"") : ("\"" + CommonAPI.generateAfterBoostText(num) + "\""));
		if (empty3 != string.Empty)
		{
			empty = empty + empty3 + "\n\n";
		}
		empty += empty4;
		list3.Add(empty4);
		string text2 = string.Empty;
		bool hasLevelUp = false;
		if (num13 > 0)
		{
			if (!lastSelectSmith.checkOutsource() && !lastSelectSmith.checkIsSmithMaxLevel())
			{
				int num15 = (int)((float)num13 * 0.5f + 0.5f);
				string text3 = string.Empty;
				float num16 = player.checkDecoEffect("MILESTONE_EXP", string.Empty);
				if (num16 != 1f)
				{
					int num17 = num15;
					num15 = (int)(num16 * (float)num15);
					if (num16 > 1f)
					{
						int num18 = num15 - num17;
						if (num18 == 0)
						{
							num18 = 1;
							num15++;
						}
						text3 = "<color=green>(+" + num18 + ")</color>";
					}
					else if (num16 < 1f)
					{
						int num19 = num17 - num15;
						if (num19 == 0)
						{
							num19 = 1;
							num15--;
						}
						text3 = "<color=red>(-" + num19 + ")</color>";
					}
				}
				Dictionary<string, int> dictionary = lastSelectSmith.addSmithExp(num15);
				text2 = "\n" + lastSelectSmith.getSmithName() + " gained " + num15 + text3 + " Exp!";
				if (dictionary["lvlGain"] > 0)
				{
					string text4 = text2;
					text2 = text4 + " Smith Level +" + dictionary["lvlGain"] + "!";
					hasLevelUp = true;
					showSmithLevelUpWhetsapp(lastSelectSmith, dictionary["lvlGain"]);
				}
			}
			empty += text2;
		}
		GameObject gameObject = GameObject.Find("Panel_BottomMenu");
		if (gameObject != null)
		{
			gameObject.GetComponent<BottomMenuController>().refreshBottomButtons();
		}
		currentProject.addBoost(WeaponStat.WeaponStatMagic);
		if (nguiCameraGUI != null)
		{
			GameObject.Find("Grid_topRight").GetComponent<GUIProgressHUDController>().refreshStats();
			viewController.showBoostPopup(ProcessPopupType.ProcessPopupTypeEnchant, lastSelectSmith, empty4, currentProjectStats, player.getCurrentProjectStats(), list4, hasLevelUp, enchantBoost, item);
		}
	}

	public Boost addEnchantItem(Item selectedItem)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		string empty = string.Empty;
		Boost boost = null;
		List<int> currentProjectStats = player.getCurrentProjectStats();
		bool flag = player.enchantWeapon(selectedItem);
		List<int> currentProjectStats2 = player.getCurrentProjectStats();
		if (flag)
		{
			empty += gameData.getTextByRefIdWithDynText("menuForgeBoost31", "[itemName]", selectedItem.getItemName());
			switch (selectedItem.getItemEffectStat())
			{
			case WeaponStat.WeaponStatAttack:
				boost = new Boost(selectedItem.getItemEffectValue(), 0, 0, 0);
				empty = empty + "\n" + gameData.getTextByRefIdWithDynText("projectStats02", "[atk]", "+" + selectedItem.getItemEffectValue());
				break;
			case WeaponStat.WeaponStatSpeed:
				boost = new Boost(0, 0, selectedItem.getItemEffectValue(), 0);
				empty = empty + "\n" + gameData.getTextByRefIdWithDynText("projectStats03", "[spd]", "+" + selectedItem.getItemEffectValue());
				break;
			case WeaponStat.WeaponStatAccuracy:
				boost = new Boost(0, selectedItem.getItemEffectValue(), 0, 0);
				empty = empty + "\n" + gameData.getTextByRefIdWithDynText("projectStats04", "[acc]", "+" + selectedItem.getItemEffectValue());
				break;
			case WeaponStat.WeaponStatMagic:
				boost = new Boost(0, 0, 0, selectedItem.getItemEffectValue());
				empty = empty + "\n" + gameData.getTextByRefIdWithDynText("projectStats05", "[mag]", "+" + selectedItem.getItemEffectValue());
				break;
			}
			if (selectedItem.getItemElement() != Element.ElementNone)
			{
				string replaceValue = CommonAPI.convertElementToString(selectedItem.getItemElement());
				empty = empty + "\n" + gameData.getTextByRefIdWithDynText("menuForgeBoost32", "[element]", replaceValue);
			}
			viewController.showAddEnchantmentPopup(currentProjectStats, currentProjectStats2, player.getCurrentProjectWeapon(), boost, selectedItem);
		}
		return boost;
	}

	public void skipBoost(ProjectPhase nextPhase)
	{
		Player player = game.getPlayer();
		endPopEvent();
	}

	public void renameWeapon(string nameInput)
	{
		Player player = game.getPlayer();
		player.setProjectName(nameInput);
		showWeaponComplete();
	}

	public void showWeaponComplete()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		Project currentProject = player.getCurrentProject();
		if (nguiCameraGUI != null)
		{
			viewController.closeProjectProgress();
			if (player.getCurrentProject().getProjectType() == ProjectType.ProjectTypeUnique)
			{
				viewController.showLegendaryComplete();
				return;
			}
			currentProject.setQualifyGoldenHammer(player.getNextGoldenHammerYear());
			viewController.showRenamePopup(PopupType.PopupTypeForgeWeaponComplete);
		}
	}

	public bool checkProjectStatus(bool hasTimePass = true, bool checkDog = true)
	{
		Player player = game.getPlayer();
		Project currentProject = player.getCurrentProject();
		player.updateSmithStations();
		doDogShopActions(checkDog);
		if (nguiCameraGUI != null)
		{
			GameObject.Find("StationController").GetComponent<StationController>().assignSmithStations();
		}
		if (hasTimePass)
		{
			foreach (Smith smith in player.getSmithList())
			{
				smith.passTimeOnEffects(2);
			}
			bool flag = false;
			switch (currentProject.getProjectType())
			{
			case ProjectType.ProjectTypeWeapon:
				flag = timePassOnWeaponProject();
				break;
			case ProjectType.ProjectTypeUnique:
				flag = timePassOnWeaponProject();
				break;
			case ProjectType.ProjectTypeContract:
				flag = timePassOnContract();
				break;
			case ProjectType.ProjectTypeNothing:
				flag = timePassOnIdle();
				break;
			}
			if (!flag)
			{
				GameObject.Find("ExploreController").GetComponent<ExploreController>().timePassOnExplore(2);
			}
		}
		return false;
	}

	public void doDogShopActions(bool checkDog)
	{
		Player player = game.getPlayer();
		bool flag = false;
		if (!player.checkHasDog() || !checkDog)
		{
			return;
		}
		if (player.checkRandomDog())
		{
			player.randomizeDogStation();
		}
		SmithStation dogStation = player.getDogStation();
		int num = 0;
		Furniture highestPlayerFurnitureByType = player.getHighestPlayerFurnitureByType("301");
		if (highestPlayerFurnitureByType != null && highestPlayerFurnitureByType.getFurnitureRefId() != string.Empty)
		{
			num = highestPlayerFurnitureByType.getFurnitureLevel();
		}
		if (dogStation != SmithStation.SmithStationDogHome)
		{
			int dogBarkChance = player.getDogBarkChance();
			if (Random.Range(0, dogBarkChance) != 0 || charAnimCtr.getDogObject().GetComponent<GUIDogPathController>().getIsMoving())
			{
				return;
			}
			flag = true;
			float aValue = 5f - 0.75f * Mathf.Pow(num, 1.16f);
			player.reduceDogEnergy(aValue);
			if (nguiCameraGUI != null)
			{
				charAnimCtr.makeDogBark();
			}
			{
				foreach (Smith item in player.getStationSmithArray(dogStation))
				{
					float num2 = 0f;
					float p = 0.545f;
					float num3 = 0.038f;
					float num4 = (num2 + num3 * Mathf.Pow(player.getDogLove(), p)) * (0.6f + (float)(player.getDogMoodState() - 1) + 0.2f) / 100f;
					int num5 = (int)(num4 * item.getSmithMaxMood());
					if (item.checkSmithInShop())
					{
						item.addSmithMood(num5);
					}
				}
				return;
			}
		}
		float aValue2 = 1.25f + 0.3f * Mathf.Pow(num - 1, 1.7f);
		player.addDogEnergy(aValue2);
	}

	public bool checkQuestExpiry(QuestNEW quest)
	{
		Player player = game.getPlayer();
		if (quest.getExpired() || (quest.getExpiryDay() > 0 && quest.getExpiryDay() <= player.getPlayerDays()))
		{
			quest.setExpired(aExpired: true);
			return true;
		}
		return false;
	}

	public bool timePassOnWeaponProject()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		Project currentProject = player.getCurrentProject();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		if (currentProject.getProjectState() == ProjectState.ProjectStateCurrent)
		{
			int num = currentProject.addTimeElapsed(2);
			int projectProgressPercent = currentProject.getProjectProgressPercent();
			if (projectProgressPercent >= 100)
			{
				if (currentProject.getProjectType() == ProjectType.ProjectTypeUnique)
				{
					SmithAction smithActionByRefId = gameData.getSmithActionByRefId("101");
					player.resetSmithsWorkState(smithActionByRefId, -1);
					showForgeBoost4EnchantItemSelect(0, 100, noScroll: true);
				}
				else
				{
					SmithAction smithActionByRefId2 = gameData.getSmithActionByRefId("101");
					player.resetSmithsWorkState(smithActionByRefId2, -1);
					if (gameData.checkFeatureIsUnlocked(gameLockSet, "ENCHANT", completedTutorialIndex))
					{
						showForgeBoost4EnchantItemSelect(0, 100, noScroll: true);
					}
					else
					{
						showPopEvent(PopEventType.PopEventTypeNaming);
					}
				}
				return true;
			}
			List<string> list = doNormalGrowthOnProject();
			GameObject gameObject = GameObject.Find("Panel_ForgeProgressNew");
			if (gameObject == null)
			{
				viewController.showProjectProgress();
			}
			GameObject.Find("Panel_ForgeProgressNew").GetComponent<GUIForgeProgressNewController>().updateWeaponDisplay();
		}
		return false;
	}

	public bool timePassOnContract()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		Project currentProject = player.getCurrentProject();
		if (currentProject.getProjectState() == ProjectState.ProjectStateCurrent)
		{
			int num = currentProject.addTimeElapsed(2);
			if (currentProject.checkContractFinish())
			{
				Contract projectContract = currentProject.getProjectContract();
				int gold = projectContract.getGold();
				projectContract.addTimesCompleted(1);
				string text = CommonAPI.convertHalfHoursToTimeDisplay(currentProject.getContractEndTime(), isMenu: false);
				player.addGold(gold);
				audioController.playGoldGainAudio();
				player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeEarningContract, string.Empty, gold);
				SmithAction smithActionByRefId = gameData.getSmithActionByRefId("101");
				player.resetSmithsWorkState(smithActionByRefId, -1);
				player.endCurrentProject(ProjectState.ProjectStateCompleted);
				if (nguiCameraGUI != null)
				{
					viewController.closeProjectProgress();
					viewController.showContractComplete(projectContract);
				}
				return true;
			}
			if (currentProject.checkContractTimeLeft(player.getPlayerTimeLong()))
			{
				SmithAction smithActionByRefId2 = gameData.getSmithActionByRefId("101");
				player.resetSmithsWorkState(smithActionByRefId2, -1);
				player.endCurrentProject(ProjectState.ProjectStateTimedOut);
				if (nguiCameraGUI != null)
				{
					viewController.closeProjectProgress();
					viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: true, string.Empty, gameData.getTextByRefId("menuForgeComplete07"), PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
				}
				return true;
			}
			List<string> list = doNormalGrowthOnProject();
			GameObject gameObject = GameObject.Find("Panel_ForgeProgressNew");
			if (gameObject == null)
			{
				viewController.showProjectProgress();
			}
			GameObject.Find("Panel_ForgeProgressNew").GetComponent<GUIForgeProgressNewController>().updateContractDisplay();
		}
		return false;
	}

	public void forceEndForging()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		SmithAction smithActionByRefId = gameData.getSmithActionByRefId("101");
		player.resetSmithsWorkState(smithActionByRefId, -1);
		Project currentProject = player.getCurrentProject();
		int projectProgressPercent = currentProject.getProjectProgressPercent();
		bool flag = false;
		if (projectProgressPercent > 50 && currentProject.getProjectType() != ProjectType.ProjectTypeUnique)
		{
			int randomInt = CommonAPI.getRandomInt(0, projectProgressPercent);
			if (randomInt > 40)
			{
				player.endCurrentProject(ProjectState.ProjectStateAbandoned);
				flag = true;
				currentProject.setQualifyGoldenHammer(player.getNextGoldenHammerYear());
			}
			else
			{
				player.destroyCurrentProject();
			}
		}
		else
		{
			player.destroyCurrentProject();
		}
		if (nguiCameraGUI != null)
		{
			string textByRefId = gameData.getTextByRefId("projectTerminate01");
			textByRefId = ((!flag) ? (textByRefId + "\n[E54242]" + gameData.getTextByRefId("projectTerminate10") + "[-]") : (textByRefId + "\n" + gameData.getTextByRefId("projectTerminate09")));
			viewController.closeProjectProgress();
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: true, string.Empty, textByRefId, PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
		}
	}

	public void forceEndContract()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		SmithAction smithActionByRefId = gameData.getSmithActionByRefId("101");
		player.resetSmithsWorkState(smithActionByRefId, -1);
		player.endCurrentProject(ProjectState.ProjectStateAbandoned);
		if (nguiCameraGUI != null)
		{
			viewController.closeProjectProgress();
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: true, string.Empty, gameData.getTextByRefId("projectTerminate02"), PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
		}
	}

	public bool timePassOnIdle()
	{
		Player player = game.getPlayer();
		Project currentProject = player.getCurrentProject();
		List<Smith> smithList = player.getSmithList();
		foreach (Smith item in smithList)
		{
			string empty = string.Empty;
			SmithAction smithAction = updateSmithAction(item, 2);
		}
		return false;
	}

	public void showSmithMoodWhetsapp(Smith selectedSmith)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		string whetsappMoodString = CommonAPI.getWhetsappMoodString(selectedSmith.getMoodState());
		if (whetsappMoodString != string.Empty)
		{
			gameData.addNewWhetsappMsg(selectedSmith.getSmithName(), whetsappMoodString, "Image/Smith/Portraits/" + selectedSmith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
		}
	}

	public SmithAction updateSmithAction(Smith smith, int durationUnitTime)
	{
		GameData gameData = game.getGameData();
		SmithAction smithAction = smith.getSmithAction();
		float remainingMood = smith.getRemainingMood();
		Player player = game.getPlayer();
		bool flag = player.checkHasProject();
		SmithAction smithActionByRefId = gameData.getSmithActionByRefId("101");
		float aReduce = 0f;
		if (flag)
		{
			smithActionByRefId = gameData.getSmithActionByRefId("102");
			aReduce = player.getShopLevel().getShopMoodReduceRate();
		}
		if (smith.checkSmithInShop())
		{
			GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
			string gameLockSet = gameScenarioByRefId.getGameLockSet();
			int completedTutorialIndex = player.getCompletedTutorialIndex();
			bool hasMoodLimit = false;
			if (!gameData.checkFeatureIsUnlocked(gameLockSet, "MOODLIMIT", completedTutorialIndex))
			{
				hasMoodLimit = true;
			}
			SmithMood moodState = smith.getMoodState();
			remainingMood = smith.reduceSmithMood(aReduce, hasMoodLimit);
			SmithMood moodState2 = smith.getMoodState();
			if (moodState != moodState2)
			{
				charAnimCtr.showMoodBubble(smith);
				showSmithMoodWhetsapp(smith);
			}
			if (smithAction.getActionState() == SmithActionState.SmithActionStateDefault && flag)
			{
				smith.increaseWorkProgress(2);
			}
			if (smith.decreaseActionDuration(durationUnitTime))
			{
				switch (smithAction.getStatEffect())
				{
				case StatEffect.StatEffectAbsStamina:
					remainingMood = smith.addSmithMood((int)smithAction.getEffectValue());
					break;
				case StatEffect.StatEffectMultStamina:
					remainingMood = smith.addSmithMood((int)(smith.getSmithMaxMood() * smithAction.getEffectValue()));
					break;
				case StatEffect.StatEffectWorkProgressMax:
					if (flag)
					{
						smith.setWorkProgress(3);
					}
					break;
				case StatEffect.StatEffectForgeBoostNext:
					if (flag)
					{
						smith.addSmithEffect(StatEffect.StatEffectForgeBoostNext, smithAction.getEffectValue(), 2, string.Empty, string.Empty);
					}
					break;
				}
				SmithAction smithAction2 = smithActionByRefId;
				int num = 2;
				if (smithAction.getActionState() == SmithActionState.SmithActionStateDefault)
				{
					smithAction2 = getNextAction(smith, flag);
					num = smithAction2.getDuration();
				}
				else
				{
					num = 8;
				}
				smith.setSmithAction(smithAction2, num);
				if (nguiCameraGUI != null)
				{
					string actionImage = smithAction2.getImage();
					if (smithAction2.checkWorking())
					{
						switch (smith.getCurrentStation())
						{
						case SmithStation.SmithStationDesign:
							actionImage = "Design-icon";
							break;
						case SmithStation.SmithStationCraft:
							actionImage = "crafting-icon";
							break;
						case SmithStation.SmithStationPolish:
							actionImage = "polish-icon";
							break;
						case SmithStation.SmithStationEnchant:
							actionImage = "enchanting-icon";
							break;
						}
					}
					charAnimCtr.showActionBubble(smith, actionImage);
				}
			}
			else if (Random.Range(0, 10) == 0)
			{
				charAnimCtr.showRandomTextBubble(smith);
			}
		}
		else if (smithAction.getRefId() == "904" && smith.decreaseActionDuration(durationUnitTime))
		{
			Weapon currentResearchWeapon = player.getCurrentResearchWeapon();
			bool flag2 = player.unlockWeapon(currentResearchWeapon);
			GameScenario gameScenarioByRefId2 = gameData.getGameScenarioByRefId(player.getGameScenario());
			string gameLockSet2 = gameScenarioByRefId2.getGameLockSet();
			int completedTutorialIndex2 = player.getCompletedTutorialIndex();
			bool hasMoodLimit2 = false;
			if (!gameData.checkFeatureIsUnlocked(gameLockSet2, "MOODLIMIT", completedTutorialIndex2))
			{
				hasMoodLimit2 = true;
			}
			SmithMood moodState3 = smith.getMoodState();
			smith.reduceSmithMood(currentResearchWeapon.getResearchMood(), hasMoodLimit2);
			SmithMood moodState4 = smith.getMoodState();
			if (moodState3 != moodState4)
			{
				string whetsappMoodString = CommonAPI.getWhetsappMoodString(moodState4);
				if (whetsappMoodString != string.Empty)
				{
					gameData.addNewWhetsappMsg(smith.getSmithName(), whetsappMoodString, "Image/Smith/Portraits/" + smith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
				}
			}
			SmithAction smithAction3 = smithActionByRefId;
			int num2 = 2;
			if (smithAction.getActionState() == SmithActionState.SmithActionStateDefault)
			{
				smithAction3 = getNextAction(smith, flag);
				num2 = smithAction3.getDuration();
			}
			else
			{
				num2 = 8;
			}
			smith.setSmithAction(smithAction3, num2);
			string aRefID = string.Empty;
			switch (player.getShopLevelInt())
			{
			case 1:
				aRefID = "10015";
				break;
			case 2:
				aRefID = "20017";
				break;
			case 3:
				aRefID = "30020";
				break;
			case 4:
				aRefID = "40026";
				break;
			}
			GameObject.Find("GUIObstacleController").GetComponent<GUIObstacleController>().stopObstacleAnim(aRefID, enableSprite: true);
			viewController.showResearchComplete(smith);
			if (flag2)
			{
				List<string> list = new List<string>();
				list.Add("[weaponName]");
				list.Add("[weaponType]");
				List<string> list2 = new List<string>();
				list2.Add(currentResearchWeapon.getWeaponName());
				list2.Add(currentResearchWeapon.getWeaponType().getWeaponTypeName());
				string textByRefIdWithDynTextList = gameData.getTextByRefIdWithDynTextList("whetsappWeaponTypeUnlock01", list, list2);
				gameData.addNewWhetsappMsg(smith.getSmithName(), textByRefIdWithDynTextList, "Image/Smith/Portraits/" + smith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
			}
			else
			{
				string randomTextBySetRefIdWithDynText = gameData.getRandomTextBySetRefIdWithDynText("whetsappResearchComplete", "[weaponName]", currentResearchWeapon.getWeaponName());
				gameData.addNewWhetsappMsg(smith.getSmithName(), randomTextBySetRefIdWithDynText, "Image/Smith/Portraits/" + smith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
			}
			player.endResearch(addResearchCount: true);
		}
		return smithAction;
	}

	public SmithAction getNextAction(Smith smith, bool hasProject)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		float remainingMood = smith.getRemainingMood();
		float smithMaxMood = smith.getSmithMaxMood();
		List<SmithAction> smithActionList = gameData.getSmithActionList(!hasProject, player.getWeather());
		List<int> list = new List<int>();
		string smithPreferredAction = smith.getSmithPreferredAction();
		foreach (SmithAction item in smithActionList)
		{
			int num = 0;
			num = ((!(smithPreferredAction == item.getRefId())) ? item.getChance() : smith.getSmithPreferredActionChance());
			float num2 = player.checkDecoEffect("SMITH_ACTION_CHANCE", item.getRefId());
			if (num2 != 1f)
			{
				num = (int)((float)num * num2);
			}
			list.Add(num);
		}
		int weightedRandomIndex = CommonAPI.getWeightedRandomIndex(list);
		return smithActionList[weightedRandomIndex];
	}

	public List<string> doNormalGrowthOnProject()
	{
		Player player = game.getPlayer();
		List<Smith> smithList = player.getSmithList();
		List<string> result = new List<string>();
		foreach (Smith item in smithList)
		{
			SmithAction smithAction = updateSmithAction(item, 2);
			switch (smithAction.getActionState())
			{
			case SmithActionState.SmithActionStateDefault:
				if (item.getWorkProgress() < 3)
				{
					break;
				}
				if (nguiCameraGUI != null)
				{
					if (charAnimCtr.getSmithObject(item) != null && !charAnimCtr.getSmithObject(item).GetComponent<GUIPathController>().getIsMoving())
					{
						List<string> list = doSmithGrowthOnProject(item);
						item.useWorkProgress();
						break;
					}
					item.addSmithMood(2f);
					if (item.getWorkProgress() > 3)
					{
						item.decreaseWorkProgress(2);
					}
				}
				else
				{
					List<string> list2 = doSmithGrowthOnProject(item);
					item.useWorkProgress();
				}
				break;
			}
		}
		return result;
	}

	public float getMoodForgingStatModifier(Smith aSmith, bool isMilestone)
	{
		float num = 1f;
		if (isMilestone)
		{
			return CommonAPI.getMoodEffect(aSmith.getMoodState(), 0.4f, 1f, 0.2f);
		}
		return CommonAPI.getMoodEffect(aSmith.getMoodState(), 0.4f, 1f, 0.2f);
	}

	public List<string> doSmithGrowthOnProject(Smith smith)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		Project currentProject = player.getCurrentProject();
		Weapon projectWeapon = currentProject.getProjectWeapon();
		List<int> list = new List<int>();
		bool flag = false;
		if (currentProject.getProjectType() == ProjectType.ProjectTypeContract || currentProject.getProjectType() == ProjectType.ProjectTypeResearch)
		{
			flag = true;
			int num = 1;
			foreach (int item2 in player.getCurrentProjectStatReqsLeft())
			{
				if (item2 > 0)
				{
					list.Add(num);
				}
				num++;
			}
		}
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		string text = "white";
		string text2 = string.Empty;
		string empty = string.Empty;
		if (!flag || list.Count > 0)
		{
			SmithStation currentStation = smith.getCurrentStation();
			float num6 = 1f;
			if (currentStation == player.getPlayerStation() && !charAnimCtr.getAvatarObject().GetComponent<GUIAvatarPathController>().getIsMoving())
			{
				num6 = 1.05f;
			}
			switch (currentStation)
			{
			case SmithStation.SmithStationDesign:
			{
				float num10 = 1f;
				if (projectWeapon.checkWeaponValid())
				{
					num10 = projectWeapon.getAtkMult();
				}
				float moodForgingStatModifier2 = getMoodForgingStatModifier(smith, isMilestone: false);
				Furniture highestPlayerFurnitureByType2 = player.getHighestPlayerFurnitureByType("601");
				int furnitureLevel2 = highestPlayerFurnitureByType2.getFurnitureLevel();
				int num11 = 1 + 2 * (furnitureLevel2 - 1);
				num2 = (int)((float)smith.getSmithPower() * Random.Range(0.03f, 0.06f) * num10 * 0.6f * moodForgingStatModifier2 * (float)num11);
				num2 = Mathf.Max(1, num2);
				float num12 = smith.useForgeBoost();
				string text5 = string.Empty;
				if (num12 != 1f)
				{
					text = "orange";
					num2 = (int)((float)num2 * num12);
					text5 = " (x" + num12 + ")";
				}
				num2 = (int)((float)num2 * num6);
				if (flag)
				{
					num2 = Mathf.Min(num2, currentProject.getAtkReqLeft());
				}
				if (num2 > 0)
				{
					if (text == "white")
					{
						text = "green";
					}
					string text4 = text2;
					text2 = text4 + " Atk +" + num2 + text5;
				}
				break;
			}
			case SmithStation.SmithStationCraft:
			{
				float num13 = 1f;
				if (projectWeapon.checkWeaponValid())
				{
					num13 = projectWeapon.getSpdMult();
				}
				float moodForgingStatModifier3 = getMoodForgingStatModifier(smith, isMilestone: false);
				Furniture highestPlayerFurnitureByType3 = player.getHighestPlayerFurnitureByType("701");
				int furnitureLevel3 = highestPlayerFurnitureByType3.getFurnitureLevel();
				int num14 = 1 + 2 * (furnitureLevel3 - 1);
				num3 = (int)((float)smith.getSmithIntelligence() * Random.Range(0.03f, 0.06f) * num13 * 0.6f * moodForgingStatModifier3 * (float)num14);
				num3 = Mathf.Max(1, num3);
				float num15 = smith.useForgeBoost();
				string text6 = string.Empty;
				if (num15 != 1f)
				{
					text = "orange";
					num3 = (int)((float)num3 * num15);
					text6 = " (x" + num15 + ")";
				}
				num3 = (int)((float)num3 * num6);
				if (flag)
				{
					num3 = Mathf.Min(num3, currentProject.getSpdReqLeft());
				}
				if (num3 > 0)
				{
					if (text == "white")
					{
						text = "green";
					}
					string text4 = text2;
					text2 = text4 + " Spd +" + num3 + text6;
				}
				break;
			}
			case SmithStation.SmithStationPolish:
			{
				float num16 = 1f;
				if (projectWeapon.checkWeaponValid())
				{
					num16 = projectWeapon.getAccMult();
				}
				float moodForgingStatModifier4 = getMoodForgingStatModifier(smith, isMilestone: false);
				Furniture highestPlayerFurnitureByType4 = player.getHighestPlayerFurnitureByType("801");
				int furnitureLevel4 = highestPlayerFurnitureByType4.getFurnitureLevel();
				int num17 = 1 + 2 * (furnitureLevel4 - 1);
				num4 = (int)((float)smith.getSmithTechnique() * Random.Range(0.03f, 0.06f) * num16 * 0.6f * moodForgingStatModifier4 * (float)num17);
				num4 = Mathf.Max(1, num4);
				float num18 = smith.useForgeBoost();
				string text7 = string.Empty;
				if (num18 != 1f)
				{
					text = "orange";
					num4 = (int)((float)num4 * num18);
					text7 = " (x" + num18 + ")";
				}
				num4 = (int)((float)num4 * num6);
				if (flag)
				{
					num4 = Mathf.Min(num4, currentProject.getAccReqLeft());
				}
				if (num4 > 0)
				{
					if (text == "white")
					{
						text = "green";
					}
					string text4 = text2;
					text2 = text4 + " Acc +" + num4 + text7;
				}
				break;
			}
			case SmithStation.SmithStationEnchant:
			{
				float num7 = 1f;
				if (projectWeapon.checkWeaponValid())
				{
					num7 = projectWeapon.getMagMult();
				}
				float moodForgingStatModifier = getMoodForgingStatModifier(smith, isMilestone: false);
				Furniture highestPlayerFurnitureByType = player.getHighestPlayerFurnitureByType("901");
				int furnitureLevel = highestPlayerFurnitureByType.getFurnitureLevel();
				int num8 = 1 + 2 * (furnitureLevel - 1);
				num5 = (int)((float)smith.getSmithLuck() * Random.Range(0.03f, 0.06f) * num7 * 0.6f * moodForgingStatModifier * (float)num8);
				num5 = Mathf.Max(1, num5);
				float num9 = smith.useForgeBoost();
				string text3 = string.Empty;
				if (num9 != 1f)
				{
					text = "orange";
					num5 = (int)((float)num5 * num9);
					text3 = " (x" + num9 + ")";
				}
				num5 = (int)((float)num5 * num6);
				if (flag)
				{
					num5 = Mathf.Min(num5, currentProject.getMagReqLeft());
				}
				if (num5 > 0)
				{
					if (text == "white")
					{
						text = "green";
					}
					string text4 = text2;
					text2 = text4 + " Mag +" + num5 + text3;
				}
				break;
			}
			}
		}
		currentProject.addProgress(1);
		player.addCurrentProjectStats(num2, num3, num4, num5);
		if (nguiCameraGUI != null)
		{
			int value = 0;
			string type = string.Empty;
			if (num2 > 0)
			{
				value = num2;
				type = gameData.getTextByRefId("smithStatsShort02");
			}
			if (num4 > 0)
			{
				value = num4;
				type = gameData.getTextByRefId("smithStatsShort04");
			}
			if (num3 > 0)
			{
				value = num3;
				type = gameData.getTextByRefId("smithStatsShort03");
			}
			if (num5 > 0)
			{
				value = num5;
				type = gameData.getTextByRefId("smithStatsShort05");
			}
			charAnimCtr.showProjectStats(smith, value, type);
		}
		string item = string.Empty;
		if (!smith.checkIsSmithMaxLevel())
		{
			int num19 = (int)((float)(num2 + num3 + num4 + num5) * 0.5f + 0.5f);
			string text8 = string.Empty;
			float num20 = player.checkDecoEffect("FORGE_EXP", string.Empty);
			if (num20 != 1f)
			{
				int num21 = num19;
				num19 = (int)(num20 * (float)num19);
				if (num20 > 1f)
				{
					int num22 = num19 - num21;
					if (num22 == 0)
					{
						num22 = 1;
						num19++;
					}
					text8 = "<color=green>(+" + num22 + ")</color>";
				}
				else if (num20 < 1f)
				{
					int num23 = num21 - num19;
					if (num23 == 0)
					{
						num23 = 1;
						num19--;
					}
					text8 = "<color=red>(-" + num23 + ")</color>";
				}
			}
			Dictionary<string, int> dictionary = smith.addSmithExp(num19);
			item = " (Exp +" + num19 + text8;
			if (dictionary["lvlGain"] > 0)
			{
				string text4 = item;
				item = text4 + " Smith Level +" + dictionary["lvlGain"] + "!";
				if (nguiCameraGUI != null)
				{
					charAnimCtr.showLevelUpBubble(smith);
				}
				showSmithLevelUpWhetsapp(smith, dictionary["lvlGain"]);
			}
			item += ")";
		}
		List<string> list2 = new List<string>();
		list2.Add(text2);
		list2.Add(text);
		list2.Add(item);
		return list2;
	}

	public void showAppraisalPopup()
	{
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().setGameState(GameState.GameStatePopEvent);
		popEventType = PopEventType.PopEventTypeQuestProgress;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		Project currentProject = player.getCurrentProject();
		Weapon projectWeapon = currentProject.getProjectWeapon();
		player.endCurrentProject(ProjectState.ProjectStateCompleted);
		GameObject.Find("Panel_ActivitySelect").GetComponent<GUIActivitySelectController>().refreshButtons();
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().setGameState(GameState.GameStateIdle);
		popEventType = PopEventType.PopEventTypeNothing;
		if (nguiCameraGUI != null)
		{
			viewController.showForgeAppraisal();
		}
	}

	public void setDoUnlockCheck()
	{
		doUnlockCheck = true;
	}

	public bool tryStartTutorial(string aTryTutorial, bool isMap = false)
	{
		if (game == null)
		{
			game = GameObject.Find("Game").GetComponent<Game>();
		}
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		bool result = false;
		int tutorialSetOrderIndex = gameData.getTutorialSetOrderIndex(aTryTutorial);
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		if (completedTutorialIndex >= 7 && PlayerPrefs.GetInt("lastSeenTutorialIndex", -1) < completedTutorialIndex)
		{
			PlayerPrefs.SetInt("lastSeenTutorialIndex", completedTutorialIndex);
			CommonAPI.debug("SET lastSeenTutorialIndex " + completedTutorialIndex);
		}
		if (tutorialSetOrderIndex == completedTutorialIndex + 1 && player.getTutorialIndex() != tutorialSetOrderIndex)
		{
			string tutorialSetRefIdByOrderIndex = gameData.getTutorialSetRefIdByOrderIndex(tutorialSetOrderIndex);
			player.setTutorialIndex(tutorialSetOrderIndex);
			bool flag = false;
			if (tutorialSetOrderIndex >= 7 && PlayerPrefs.GetInt("lastSeenTutorialIndex", -1) < tutorialSetOrderIndex)
			{
				PlayerPrefs.SetInt("lastSeenTutorialIndex", tutorialSetOrderIndex);
				CommonAPI.debug("SET lastSeenTutorialIndex " + tutorialSetOrderIndex);
				flag = true;
			}
			if (!player.checkSkipTutorials() || flag)
			{
				result = true;
				viewController.showTutorialPopup(tutorialSetRefIdByOrderIndex, doPause: true, doTutorialMask: true, isMap);
			}
			if (player.checkSkipTutorials())
			{
				player.setCompletedTutorialIndex(tutorialSetOrderIndex);
			}
		}
		return result;
	}

	public bool checkTutorial()
	{
		return false;
	}

	public bool checkEventTimers()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		bool flag = false;
		steamStatsController.checkAchievementStatus();
		checkAreaEventExpiry();
		if (!flag)
		{
			flag = checkObjectiveStatus();
		}
		if (!flag)
		{
			flag = checkRequestExpiry();
		}
		return flag;
	}

	public void checkDogBowl()
	{
		Player player = game.getPlayer();
		if (player.checkHasDog())
		{
			if (player.getDogBowlState() == 0)
			{
				player.reduceDogLove(1);
			}
			else
			{
				player.addDogLove(1);
			}
		}
	}

	public bool checkUnlocks()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		bool flag = false;
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		if (gameData.checkFeatureIsUnlocked(gameLockSet, "UPGRADES", completedTutorialIndex))
		{
			checkDecorationUnlock();
		}
		if (!flag)
		{
			flag = checkPlayerTickets();
		}
		if (!flag)
		{
			flag = checkLegendarySmith();
		}
		if (!flag)
		{
			flag = checkLegendaryHero();
		}
		if (!flag && gameData.checkFeatureIsUnlocked(gameLockSet, "AREAEVENT", completedTutorialIndex))
		{
			flag = checkAreaEvent();
		}
		if (!flag && gameData.checkFeatureIsUnlocked(gameLockSet, "REQUEST", completedTutorialIndex))
		{
			flag = checkRequest();
		}
		return flag;
	}

	public bool checkDemoGoldReach()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		if (isDemoOver == 1)
		{
			viewController.showDialoguePopup("90101", gameData.getDialogueBySetId("90101"));
			isDemoOver = 2;
			return true;
		}
		if (isDemoOver == 2)
		{
			restartGame();
		}
		if (player.getPlayerGold() >= 100000)
		{
			List<Hashtable> list = new List<Hashtable>();
			Hashtable hashtable = new Hashtable();
			hashtable["texture"] = "Image/TextureSequence/Agent-46-Tutorial-end";
			hashtable["anchor"] = "CENTER";
			hashtable["posX"] = 0;
			hashtable["posY"] = -50;
			hashtable["sizeX"] = 668;
			hashtable["sizeY"] = 304;
			list.Add(hashtable);
			viewController.showTextureSequencePopup(list);
			isDemoOver = 1;
			return true;
		}
		return false;
	}

	public bool checkLegendarySmith()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		Smith smith = null;
		bool flag = false;
		List<Smith> lockedSmithList = gameData.getLockedSmithList(checkDLC: true, gameScenarioByRefId.getItemLockSet());
		foreach (Smith item in lockedSmithList)
		{
			if (checkUnlockCondition(item.getUnlockCondition(), item.getUnlockConditionValue(), item.getCheckString(), item.getCheckNum(), 0))
			{
				smith = item;
				flag = true;
				break;
			}
		}
		if (flag && smith != null)
		{
			smith.doUnlock();
			insertStoredInput("visitorRefId", CommonAPI.parseInt(smith.getSmithRefId()));
			string unlockDialogueSetId = smith.getUnlockDialogueSetId();
			Dictionary<string, DialogueNEW> dialogueBySetId = gameData.getDialogueBySetId(unlockDialogueSetId);
			if (dialogueBySetId.Count > 0)
			{
				viewController.showDialoguePopup(unlockDialogueSetId, dialogueBySetId, PopupType.PopupTypeSmithVisit);
			}
			viewController.queueItemGetPopup(gameData.getTextByRefId("charaUnlock01"), "Image/Smith/" + smith.getImage() + "_manage", gameData.getTextByRefIdWithDynText("charaUnlock03", "[smithName]", smith.getSmithName()));
		}
		return flag;
	}

	public bool checkLegendaryHero()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		if (player.checkDisplayLegendaryHeroList())
		{
			foreach (LegendaryHero legendaryHero in gameData.getLegendaryHeroList(checkDLC: true, gameScenarioByRefId.getItemLockSet()))
			{
				if (legendaryHero.getRequestState() == RequestState.RequestStateBlank && checkUnlockCondition(legendaryHero.getUnlockCondition(), legendaryHero.getUnlockConditionValue(), legendaryHero.getCheckString(), legendaryHero.getCheckNum(), 0))
				{
					legendaryHero.setRequestState(RequestState.RequestStatePending);
					player.addLegendaryHeroRequest(legendaryHero);
					doShowLegendaryNotification();
					return true;
				}
			}
		}
		else if (player.getLastLegendaryHeroRequest().getRequestState() == RequestState.RequestStatePending)
		{
			doShowLegendaryNotification();
		}
		else
		{
			doHideLegendaryNotification();
		}
		return false;
	}

	public void doShowLegendaryNotification()
	{
		if (smithListMenuController == null)
		{
			GameObject gameObject = GameObject.Find("Panel_SmithList");
			if (gameObject != null)
			{
				smithListMenuController = gameObject.GetComponent<GUISmithListMenuController>();
			}
		}
		if (smithListMenuController != null && !smithListMenuController.checkLegendaryNotificationDisplay())
		{
			smithListMenuController.showLegendaryNotification();
		}
	}

	public void doHideLegendaryNotification()
	{
		if (smithListMenuController == null)
		{
			GameObject gameObject = GameObject.Find("Panel_SmithList");
			if (gameObject != null)
			{
				smithListMenuController = gameObject.GetComponent<GUISmithListMenuController>();
			}
		}
		if (smithListMenuController != null && smithListMenuController.checkLegendaryNotificationDisplay())
		{
			smithListMenuController.dismissLegendaryNotification();
		}
	}

	public bool checkObjectiveStatus(bool forceObjectiveSuccess = false)
	{
		GameObject gameObject = GameObject.Find("Objective");
		if (gameObject != null && gameObject.GetComponent<GUIObjectiveController>().checkObjectiveStatus(forceObjectiveSuccess))
		{
			return true;
		}
		return false;
	}

	public void checkAreaEventExpiry()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		List<Area> areaList = gameData.getAreaList(string.Empty);
		long playerTimeLong = player.getPlayerTimeLong();
		foreach (Area item in areaList)
		{
			item.checkEventExpiry(playerTimeLong);
		}
	}

	public bool checkAreaEvent()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		bool flag = false;
		if (player.tryAreaEvent())
		{
			int areaRegion = player.getAreaRegion();
			List<AreaEvent> areaEventListByRegion = gameData.getAreaEventListByRegion(areaRegion);
			List<Area> areaListWithEvents = gameData.getAreaListWithEvents(areaRegion);
			AreaRegion areaRegionByRefID = gameData.getAreaRegionByRefID(areaRegion);
			if (areaListWithEvents.Count < areaRegionByRefID.getMaxEventCount())
			{
				List<int> list = new List<int>();
				int num = 0;
				foreach (AreaEvent item in areaEventListByRegion)
				{
					int probability = item.getProbability();
					list.Add(probability);
					num += probability;
				}
				if (num < 1000)
				{
					list.Add(1000 - num);
				}
				int weightedRandomIndex = CommonAPI.getWeightedRandomIndex(list);
				if (weightedRandomIndex < areaEventListByRegion.Count)
				{
					AreaEvent aEvent = areaEventListByRegion[weightedRandomIndex];
					List<Area> areaListByRegionWithSell = gameData.getAreaListByRegionWithSell(areaRegion);
					if (areaListByRegionWithSell.Count > 0)
					{
						Area area = areaListByRegionWithSell[Random.Range(0, areaListByRegionWithSell.Count)];
						flag = area.trySetCurrentEvent(aEvent, player.getPlayerTimeLong(), itemLockSet);
						if (flag)
						{
							viewController.showAreaEventPopup(area, aEvent);
						}
					}
				}
			}
		}
		return flag;
	}

	public bool checkRequestExpiry()
	{
		GameObject gameObject = GameObject.Find("Panel_RequestList");
		if (gameObject != null && gameObject.GetComponent<GUIRequestListController>().checkRequestExpiry())
		{
			return true;
		}
		return false;
	}

	public bool checkRequest()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		if (player.tryGiveRequest())
		{
			int num = Random.Range(0, 100);
			if (num >= 7)
			{
				return false;
			}
			Hero requestHero = gameData.getRequestHero(player.getShopLevelInt(), itemLockSet);
			CommonAPI.debug("requestHero " + requestHero.getHeroRefId());
			Request requestByLevelRange = gameData.getRequestByLevelRange(requestHero.getRequestLevelMin(), requestHero.getRequestLevelMax());
			CommonAPI.debug("request " + requestByLevelRange.getRequestRefId());
			if (requestHero.getHeroRefId() == string.Empty || requestByLevelRange.getRequestRefId() == string.Empty)
			{
				return false;
			}
			string aWeaponTypeReq = string.Empty;
			string aWeaponReq = string.Empty;
			int aAtkReq = 0;
			int aSpdReq = 0;
			int aAccReq = 0;
			int aMagReq = 0;
			string aEnchantmentReq = string.Empty;
			bool flag = false;
			float num2 = player.calculateENSB(player.getSmithList().Count, 0);
			float num3 = player.calculateEMSB(hasMilestoneBoost: true);
			List<string> requestReqList = requestByLevelRange.getRequestReqList();
			using (List<string>.Enumerator enumerator = requestReqList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					switch (enumerator.Current)
					{
					case "WEAPON_TYPE":
					{
						List<string> requestWeaponTypeList = requestHero.getRequestWeaponTypeList();
						List<WeaponType> requestWeaponTypeList2 = gameData.getRequestWeaponTypeList(requestWeaponTypeList);
						if (requestWeaponTypeList2.Count > 0)
						{
							WeaponType weaponType = requestWeaponTypeList2[Random.Range(0, requestWeaponTypeList2.Count)];
							aWeaponTypeReq = weaponType.getWeaponTypeRefId();
							flag = true;
						}
						break;
					}
					case "WEAPON":
					{
						List<string> requestWeaponTypeList3 = requestHero.getRequestWeaponTypeList();
						List<Weapon> requestWeaponList = gameData.getRequestWeaponList(requestWeaponTypeList3, itemLockSet);
						if (requestWeaponList.Count > 0)
						{
							Weapon weapon = requestWeaponList[Random.Range(0, requestWeaponList.Count)];
							aWeaponReq = weapon.getWeaponRefId();
							flag = true;
						}
						break;
					}
					case "ONE_STAT":
					{
						int num5 = (int)((num2 / 2f + num3) * Random.Range(0.8f, 1.2f));
						switch (Random.Range(1, 4))
						{
						case 1:
							aAtkReq = num5;
							break;
						case 2:
							aSpdReq = num5;
							break;
						case 3:
							aAccReq = num5;
							break;
						case 4:
							aMagReq = num5;
							break;
						}
						flag = true;
						break;
					}
					case "TWO_STAT":
					{
						List<int> randomIntList = CommonAPI.getRandomIntList(4, 2);
						foreach (int item2 in randomIntList)
						{
							int num4 = (int)((num2 / 2.5f + num3) * Random.Range(0.8f, 1.2f));
							switch (item2)
							{
							case 1:
								aAtkReq = num4;
								break;
							case 2:
								aSpdReq = num4;
								break;
							case 3:
								aAccReq = num4;
								break;
							default:
								aMagReq = num4;
								break;
							}
						}
						flag = true;
						break;
					}
					case "ENCHANTMENT":
					{
						List<Item> itemListByType = gameData.getItemListByType(ItemType.ItemTypeEnhancement, ownedOnly: true, includeSpecial: true, string.Empty);
						if (itemListByType.Count > 0)
						{
							Item item = itemListByType[Random.Range(0, itemListByType.Count)];
							aEnchantmentReq = item.getItemRefId();
							flag = true;
						}
						break;
					}
					}
				}
			}
			if (flag)
			{
				string randomTextRefIdBySetRefId = gameData.getRandomTextRefIdBySetRefId("requestText");
				string aRefId = randomTextRefIdBySetRefId.Replace("_", "Title");
				string aRefId2 = randomTextRefIdBySetRefId.Replace("_", "Desc");
				string textByRefId = gameData.getTextByRefId(aRefId);
				string textByRefId2 = gameData.getTextByRefId(aRefId2);
				int num6 = (int)(Mathf.Pow((num2 + num3) / 150f, 0.5f) * (float)requestByLevelRange.getRequestBaseGold());
				int aLoyalty = 0;
				int aFame = (int)(Mathf.Pow((num2 + num3) / 150f, 0.5f) * (float)requestByLevelRange.getRequestBaseFame());
				RewardChest rewardChestByRefId = gameData.getRewardChestByRefId(requestByLevelRange.getRequestRewardSet());
				int requestRewardQty = requestByLevelRange.getRequestRewardQty();
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				for (int i = 0; i < requestRewardQty; i++)
				{
					RewardChestItem randomRewardFromChest = rewardChestByRefId.getRandomRewardFromChest();
					string itemRefId = randomRewardFromChest.getItemRefId();
					int itemNum = randomRewardFromChest.getItemNum();
					if (itemRefId == "-1")
					{
						num6 += itemNum;
					}
					else if (dictionary.ContainsKey(itemRefId))
					{
						dictionary[itemRefId] += itemNum;
					}
					else
					{
						dictionary.Add(itemRefId, itemNum);
					}
				}
				string aId = (10000 + player.getTotalRequestCount()).ToString();
				HeroRequest heroRequest = new HeroRequest(aId, requestHero.getHeroRefId(), requestByLevelRange.getRequestDuration(), num6, aLoyalty, aFame, dictionary, textByRefId, textByRefId2, aWeaponTypeReq, aWeaponReq, aAtkReq, aSpdReq, aAccReq, aMagReq, aEnchantmentReq, player.getPlayerTimeLong());
				viewController.showWeaponRequest(heroRequest);
				return true;
			}
		}
		return false;
	}

	public bool checkPlayerTickets()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int num = player.tryGiveTicket();
		if (num > 0)
		{
			string randomTextBySetRefIdWithDynText = gameData.getRandomTextBySetRefIdWithDynText("whetsappFameUp", "[shopName]", player.getShopName());
			string textByRefIdWithDynText = gameData.getTextByRefIdWithDynText("fameUp02", "[numTickets]", num.ToString());
			viewController.queueItemGetPopup(gameData.getTextByRefId("fameUp01"), "Image/unlock/4-ticket", textByRefIdWithDynText);
			gameData.addNewWhetsappMsg(gameData.getTextByRefId("whetsappTitle02"), randomTextBySetRefIdWithDynText + " " + textByRefIdWithDynText, "Image/whetsapp/news2", player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeNotice);
			return true;
		}
		return false;
	}

	public void checkDecorationUnlock()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		List<Decoration> checkDecorationList = gameData.getCheckDecorationList(itemLockSet);
		foreach (Decoration item in checkDecorationList)
		{
			if (checkUnlockCondition(item.getDecorationShopUnlockCondition(), item.getDecorationShopUnlockValue(), item.getDecorationCheckString(), item.getDecorationCheckNum(), 0))
			{
				item.setIsVisibleInShop(aVisible: true);
				viewController.queueItemGetPopup(gameData.getTextByRefId("featureUnlock16"), "Image/Decoration/" + item.getDecorationImage(), gameData.getTextByRefId("featureUnlock17"));
			}
		}
	}

	public bool checkUnlockCondition(UnlockCondition condition, int reqCount, string checkString, int checkInt, int initCount)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		switch (condition)
		{
		case UnlockCondition.UnlockConditionTime:
		{
			int playerDays = player.getPlayerDays();
			if (playerDays - initCount >= reqCount)
			{
				return true;
			}
			break;
		}
		case UnlockCondition.UnlockConditionShopLevel:
		{
			int shopLevelInt = player.getShopLevelInt();
			if (shopLevelInt - initCount >= reqCount)
			{
				return true;
			}
			break;
		}
		case UnlockCondition.UnlockConditionFame:
		{
			int fame = player.getFame();
			if (fame - initCount >= reqCount)
			{
				return true;
			}
			break;
		}
		case UnlockCondition.UnlockConditionStarch:
			if (checkString == "EARNINGS")
			{
				int totalEarnings = player.getTotalEarnings();
				if (totalEarnings - initCount >= reqCount)
				{
					return true;
				}
			}
			if (checkString == "WEAPON")
			{
				int weaponEarnings = player.getWeaponEarnings();
				if (weaponEarnings - initCount >= reqCount)
				{
					return true;
				}
			}
			else
			{
				int playerGold = player.getPlayerGold();
				if (playerGold - initCount >= reqCount)
				{
					return true;
				}
			}
			break;
		case UnlockCondition.UnlockConditionForgeCount:
			if (checkString != string.Empty)
			{
				int completedWeaponCountByWeaponRefId = player.getCompletedWeaponCountByWeaponRefId(checkString);
				if (completedWeaponCountByWeaponRefId - initCount >= reqCount)
				{
					return true;
				}
			}
			else
			{
				int completedWeaponCount = player.getCompletedWeaponCount();
				if (completedWeaponCount - initCount >= reqCount)
				{
					return true;
				}
			}
			break;
		case UnlockCondition.UnlockConditionForgeTypeCount:
			if (checkString != string.Empty)
			{
				int completedWeaponCountByWeaponTypeRefId = player.getCompletedWeaponCountByWeaponTypeRefId(checkString);
				if (completedWeaponCountByWeaponTypeRefId - initCount >= reqCount)
				{
					return true;
				}
			}
			break;
		case UnlockCondition.UnlockConditionContractCount:
		{
			int totalContractCompleteCount = gameData.getTotalContractCompleteCount();
			if (totalContractCompleteCount - initCount >= reqCount)
			{
				return true;
			}
			break;
		}
		case UnlockCondition.UnlockConditionHeroExp:
			if (checkString != string.Empty)
			{
				int expPoints = gameData.getHeroByHeroRefID(checkString).getExpPoints();
				if (expPoints - initCount >= reqCount)
				{
					return true;
				}
			}
			else if (checkInt != -1)
			{
				int totalExpByRegion = gameData.getTotalExpByRegion(checkInt, itemLockSet);
				if (totalExpByRegion - initCount >= reqCount)
				{
					return true;
				}
			}
			else
			{
				int totalHeroExpGain = player.getTotalHeroExpGain();
				if (totalHeroExpGain - initCount >= reqCount)
				{
					return true;
				}
			}
			break;
		case UnlockCondition.UnlockConditionHeroMax:
			if (checkString != string.Empty)
			{
				int num12 = gameData.countMaxLevelHeroInArea(checkString);
				if (num12 - initCount >= reqCount)
				{
					return true;
				}
			}
			else if (checkInt != -1)
			{
				int totalMaxLevelHeroesByRegion = gameData.getTotalMaxLevelHeroesByRegion(checkInt);
				if (totalMaxLevelHeroesByRegion - initCount >= reqCount)
				{
					return true;
				}
			}
			else
			{
				int count10 = gameData.getMaxLevelHeroList().Count;
				if (count10 - initCount >= reqCount)
				{
					return true;
				}
			}
			break;
		case UnlockCondition.UnlockConditionHeroLevel:
			if (checkString != string.Empty && gameData.getHeroByHeroRefID(checkString).getHeroLevel() - initCount >= reqCount)
			{
				return true;
			}
			break;
		case UnlockCondition.UnlockConditionHeroLevelCount:
			if (checkInt != -1 && checkString != string.Empty)
			{
				int count3 = gameData.getMinLevelHeroListByTier(CommonAPI.parseInt(checkString), checkInt).Count;
				if (count3 - initCount >= reqCount)
				{
					return true;
				}
			}
			break;
		case UnlockCondition.UnlockConditionResearchWeapon:
		{
			if (checkString != string.Empty)
			{
				if (player.getWeaponByRefId(checkString).getWeaponRefId() == checkString)
				{
					return true;
				}
				break;
			}
			int researchCount = player.getResearchCount();
			if (researchCount - initCount < reqCount)
			{
				break;
			}
			return true;
		}
		case UnlockCondition.UnlockConditionResearchType:
			if (checkString != string.Empty)
			{
				int count5 = player.getUnlockedWeaponListByType(checkString).Count;
				if (count5 - initCount >= reqCount)
				{
					return true;
				}
			}
			break;
		case UnlockCondition.UnlockConditionUpgradeTotal:
		{
			int num7 = player.countWorkstationUpgrades();
			if (num7 - initCount >= reqCount)
			{
				return true;
			}
			break;
		}
		case UnlockCondition.UnlockConditionWorkstationLevel:
		{
			if (checkString != string.Empty)
			{
				int furnitureLevel = player.getHighestPlayerFurnitureByType(checkString).getFurnitureLevel();
				if (furnitureLevel - initCount >= reqCount)
				{
					return true;
				}
				break;
			}
			List<Furniture> currentShopWorkstationList = player.getCurrentShopWorkstationList();
			foreach (Furniture item in currentShopWorkstationList)
			{
				if (item.getFurnitureLevel() < reqCount)
				{
					return false;
				}
			}
			return true;
		}
		case UnlockCondition.UnlockConditionDecoCount:
		{
			if (checkString != string.Empty)
			{
				if (player.getOwnedDecorationByRefId(checkString).getDecorationRefId() == checkString)
				{
					return true;
				}
				break;
			}
			int count = player.getOwnedDecorationList().Count;
			if (count - initCount < reqCount)
			{
				break;
			}
			return true;
		}
		case UnlockCondition.UnlockConditionDecoEquip:
		{
			if (checkString != string.Empty)
			{
				if (player.getOwnedDecorationByRefId(checkString).checkIsCurrentDisplay())
				{
					return true;
				}
				break;
			}
			int count6 = player.getDisplayDecorationList().Count;
			if (count6 - initCount <= reqCount)
			{
				break;
			}
			return true;
		}
		case UnlockCondition.UnlockConditionFurnitureEquip:
		{
			if (checkString != string.Empty)
			{
				if (gameData.getFurnitureByRefId(checkString).checkShowInShop())
				{
					return true;
				}
				break;
			}
			int count4 = player.getCurrentShopFurnitureList().Count;
			if (count4 - initCount <= reqCount)
			{
				break;
			}
			return true;
		}
		case UnlockCondition.UnlockConditionRequestComplete:
		{
			int totalContractCompleteCount2 = gameData.getTotalContractCompleteCount();
			if (totalContractCompleteCount2 - initCount >= reqCount)
			{
				return true;
			}
			break;
		}
		case UnlockCondition.UnlockConditionLegendaryComplete:
		{
			if (checkString != string.Empty)
			{
				return player.checkLegendaryCompleted(checkString);
			}
			int count2 = player.getCompletedLegendaryList().Count;
			if (count2 - initCount >= reqCount)
			{
				return true;
			}
			break;
		}
		case UnlockCondition.UnlockConditionArea:
		{
			if (checkString != string.Empty)
			{
				if (gameData.getAreaByRefID(checkString).checkIsUnlock())
				{
					return true;
				}
				break;
			}
			List<Area> unlockedAreaList = gameData.getUnlockedAreaList(itemLockSet);
			if (unlockedAreaList.Count - initCount < reqCount)
			{
				break;
			}
			return true;
		}
		case UnlockCondition.UnlockConditionRegion:
		{
			int areaRegion = player.getAreaRegion();
			if (areaRegion - initCount >= reqCount)
			{
				return true;
			}
			break;
		}
		case UnlockCondition.UnlockConditionExploreItem:
		{
			if (checkString != string.Empty)
			{
				Item itemByRefId3 = gameData.getItemByRefId(checkString);
				if (itemByRefId3.getItemFromExplore() - initCount >= reqCount)
				{
					return true;
				}
				break;
			}
			List<Item> itemList3 = gameData.getItemList(ownedOnly: false);
			int num8 = 0;
			foreach (Item item2 in itemList3)
			{
				num8 += item2.getItemFromExplore();
			}
			if (num8 - initCount < reqCount)
			{
				break;
			}
			return true;
		}
		case UnlockCondition.UnlockConditionBuyItem:
		{
			if (checkString != string.Empty)
			{
				Item itemByRefId2 = gameData.getItemByRefId(checkString);
				if (itemByRefId2.getItemFromBuy() - initCount >= reqCount)
				{
					return true;
				}
				break;
			}
			List<Item> itemList2 = gameData.getItemList(ownedOnly: false);
			int num5 = 0;
			foreach (Item item3 in itemList2)
			{
				num5 += item3.getItemFromBuy();
			}
			if (num5 - initCount < reqCount)
			{
				break;
			}
			return true;
		}
		case UnlockCondition.UnlockConditionOwnItem:
		{
			if (checkString != string.Empty)
			{
				Item itemByRefId = gameData.getItemByRefId(checkString);
				if (itemByRefId.getItemNum() - initCount >= reqCount)
				{
					return true;
				}
				break;
			}
			List<Item> itemList = gameData.getItemList(ownedOnly: false);
			int num2 = 0;
			foreach (Item item4 in itemList)
			{
				num2 += item4.getItemNum();
			}
			if (num2 - initCount < reqCount)
			{
				break;
			}
			return true;
		}
		case UnlockCondition.UnlockConditionWeaponsSold:
			if (checkString != string.Empty)
			{
				int count8 = player.getCompletedProjectListByFilterType(ProjectType.ProjectTypeWeapon, CollectionFilterState.CollectionFilterStateSold, "00000").Count;
				if (count8 - initCount >= reqCount)
				{
					return true;
				}
			}
			else
			{
				int count9 = player.getCompletedProjectListByFilterType(ProjectType.ProjectTypeWeapon, CollectionFilterState.CollectionFilterStateSold, checkString).Count;
				if (count9 - initCount >= reqCount)
				{
					return true;
				}
			}
			break;
		case UnlockCondition.UnlockConditionExplore:
		{
			if (checkString != string.Empty)
			{
				string[] array2 = checkString.Split('_');
				if (!(array2[0] == "SCENARIO"))
				{
					Area areaByRefID4 = gameData.getAreaByRefID(checkString);
					if (areaByRefID4.checkTimesExplored() - initCount >= reqCount)
					{
						return true;
					}
				}
				break;
			}
			List<Area> areaList4 = gameData.getAreaList(string.Empty);
			int num10 = 0;
			foreach (Area item5 in areaList4)
			{
				num10 += item5.checkTimesExplored();
			}
			if (num10 - initCount < reqCount)
			{
				break;
			}
			return true;
		}
		case UnlockCondition.UnlockConditionBuy:
		{
			if (checkString != string.Empty)
			{
				Area areaByRefID3 = gameData.getAreaByRefID(checkString);
				if (areaByRefID3.checkTimesBuyItems() - initCount >= reqCount)
				{
					return true;
				}
				break;
			}
			List<Area> areaList3 = gameData.getAreaList(string.Empty);
			int num6 = 0;
			foreach (Area item6 in areaList3)
			{
				num6 += item6.checkTimesBuyItems();
			}
			if (num6 - initCount < reqCount)
			{
				break;
			}
			return true;
		}
		case UnlockCondition.UnlockConditionSell:
		{
			if (checkString != string.Empty)
			{
				Area areaByRefID2 = gameData.getAreaByRefID(checkString);
				if (areaByRefID2.checkTimesSell() - initCount >= reqCount)
				{
					return true;
				}
				break;
			}
			List<Area> areaList2 = gameData.getAreaList(string.Empty);
			int num4 = 0;
			foreach (Area item7 in areaList2)
			{
				num4 += item7.checkTimesSell();
			}
			if (num4 - initCount < reqCount)
			{
				break;
			}
			return true;
		}
		case UnlockCondition.UnlockConditionTraining:
		{
			if (checkString != string.Empty)
			{
				Area areaByRefID = gameData.getAreaByRefID(checkString);
				if (areaByRefID.checkTimesTrain() - initCount >= reqCount)
				{
					return true;
				}
				break;
			}
			List<Area> areaList = gameData.getAreaList(string.Empty);
			int num = 0;
			foreach (Area item8 in areaList)
			{
				num += item8.checkTimesTrain();
			}
			if (num - initCount < reqCount)
			{
				break;
			}
			return true;
		}
		case UnlockCondition.UnlockConditionVacation:
		{
			if (checkString != string.Empty)
			{
				Area areaByRefID5 = gameData.getAreaByRefID(checkString);
				if (areaByRefID5.checkTimesVacation() - initCount >= reqCount)
				{
					return true;
				}
				break;
			}
			List<Area> areaList5 = gameData.getAreaList(string.Empty);
			int num11 = 0;
			foreach (Area item9 in areaList5)
			{
				num11 += item9.checkTimesVacation();
			}
			if (num11 - initCount < reqCount)
			{
				break;
			}
			return true;
		}
		case UnlockCondition.UnlockConditionSmith:
			if (checkString != string.Empty)
			{
				Smith smithByRefId = gameData.getSmithByRefId(checkString);
				if (smithByRefId.checkPlayerOwned())
				{
					return true;
				}
			}
			else
			{
				int count7 = player.getSmithList().Count;
				if (count7 - initCount >= reqCount)
				{
					return true;
				}
			}
			break;
		case UnlockCondition.UnlockConditionSmithHire:
		{
			if (checkString != string.Empty)
			{
				if (gameData.getSmithByRefId(checkString).getTimesHired() > 0)
				{
					return true;
				}
				break;
			}
			List<Smith> smithList2 = gameData.getSmithList(checkDLC: false, includeNormal: true, includeLegendary: true, string.Empty);
			int num9 = 0;
			foreach (Smith item10 in smithList2)
			{
				if (item10.getTimesHired() > 0)
				{
					num9++;
				}
			}
			if (num9 - initCount < reqCount)
			{
				break;
			}
			return true;
		}
		case UnlockCondition.UnlockConditionSmithStat:
		{
			SmithStat stat = SmithStat.SmithStatBlank;
			switch (checkString)
			{
			case "ATK":
				stat = SmithStat.SmithStatPower;
				break;
			case "SPD":
				stat = SmithStat.SmithStatIntelligence;
				break;
			case "ACC":
				stat = SmithStat.SmithStatTechnique;
				break;
			case "MAG":
				stat = SmithStat.SmithStatLuck;
				break;
			case "ALL":
				stat = SmithStat.SmithStatAll;
				break;
			}
			if (player.getSmithTotalStat(stat) - initCount >= reqCount)
			{
				return true;
			}
			break;
		}
		case UnlockCondition.UnlockConditionSmithJobClass:
		{
			string[] array = checkString.Split('@');
			if (array.Length <= 0)
			{
				break;
			}
			string text = array[0];
			if (array.Length > 1)
			{
				string text2 = array[1];
				Smith smithByRefID = player.getSmithByRefID(text2);
				if (smithByRefID.getSmithRefId() == text2 && smithByRefID.getSmithJob().getSmithJobRefId() == text && (checkInt == -1 || smithByRefID.getSmithLevel() >= checkInt))
				{
					return true;
				}
				break;
			}
			List<Smith> smithList = player.getSmithList();
			int num3 = 0;
			foreach (Smith item11 in smithList)
			{
				if (item11.getSmithJob().getSmithJobRefId() == text && (checkInt == -1 || item11.getSmithLevel() >= checkInt))
				{
					num3++;
				}
			}
			if (num3 - initCount < reqCount)
			{
				break;
			}
			return true;
		}
		case UnlockCondition.UnlockConditionObjective:
			if (checkString != string.Empty)
			{
				Objective objectiveByRefId = gameData.getObjectiveByRefId(checkString);
				if (objectiveByRefId.checkObjectiveSuccess())
				{
					return true;
				}
			}
			else
			{
				List<Objective> succeededObjectiveList = gameData.getSucceededObjectiveList();
				if (succeededObjectiveList.Count - initCount >= reqCount)
				{
					return true;
				}
			}
			break;
		case UnlockCondition.UnlockConditionCode:
			if (checkString != string.Empty)
			{
				Code codeByRefId = gameData.getCodeByRefId(checkString);
				if (codeByRefId.checkUsed())
				{
					return true;
				}
			}
			else
			{
				List<Code> usedCodesList = gameData.getUsedCodesList();
				if (usedCodesList.Count - initCount >= reqCount)
				{
					return true;
				}
			}
			break;
		}
		return false;
	}

	public void showHeroVisitResult()
	{
		GameData gameData = game.getGameData();
		string aRefId = retrieveStoredInput("visitorRefId").ToString();
		Hero jobClassByRefId = gameData.getJobClassByRefId(aRefId);
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().setGameState(GameState.GameStateIdle);
		popEventType = PopEventType.PopEventTypeNothing;
		if (nguiCameraGUI != null)
		{
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: true, string.Empty, gameData.getTextByRefIdWithDynText("charaUnlock01", "[charaName]", jobClassByRefId.getHeroName()) + "\n" + gameData.getTextByRefIdWithDynText("charaUnlock02", "[blueprintName]", "[blueprintName]"), PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
		}
	}

	public void startNewDay(bool checkDog = true)
	{
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().setGameState(GameState.GameStatePopEvent);
		popEventType = PopEventType.PopEventTypeDayStart;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int num = player.skipToNextDay(7, 0);
		bool flag = player.checkHasProject();
		SmithAction smithActionByRefId = gameData.getSmithActionByRefId("101");
		if (flag)
		{
			smithActionByRefId = gameData.getSmithActionByRefId("102");
		}
		foreach (Smith smith in player.getSmithList())
		{
			smith.startNewDay(smithActionByRefId);
		}
		if (!checkEndOfDayActionList())
		{
			showDayStart(checkDog);
		}
	}

	public bool checkEndOfDayActionList()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		if (player.checkWeekend())
		{
			player.clearPlayerContractList();
			showPopEvent(PopEventType.PopEventTypeWeekend);
			return true;
		}
		return false;
	}

	public void showDayStart(bool checkDog = true)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		setDoUnlockCheck();
		string empty = string.Empty;
		checkStartOfDayActionList();
	}

	public void doAutosave()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		string empty = string.Empty;
		if (gameData.checkFeatureIsUnlocked(gameLockSet, "SAVELOAD", completedTutorialIndex))
		{
			string text = string.Empty;
			if (game.getPlayer().getGameScenario() != "10001")
			{
				text = game.getPlayer().getGameScenario();
			}
			empty = saveGame("autosave" + text, isPlayerSave: false);
		}
	}

	public bool checkStartOfDayActionList(bool isAutosaveLoad = false)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		bool result = false;
		if (player.tryDailyAction())
		{
			string text = generateWeather();
			commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().showTimeStatus();
			checkDogBowl();
		}
		if (player.getPlayerDays() < 1)
		{
			tryStartTutorial("INTRO");
		}
		else
		{
			if (player.checkPaySalary())
			{
				viewController.showPayDayPopup();
				return true;
			}
			if (!isAutosaveLoad)
			{
				doAutosave();
			}
			result = checkSpecialEvents();
		}
		return result;
	}

	public bool checkSpecialEvents()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		List<int> aDateList = CommonAPI.convertHalfHoursToPlayerTimeIntList(player.getLastEventDate());
		if (!player.checkDate(aDateList))
		{
			long playerTimeLong = player.getPlayerTimeLong();
			player.setLastEventDate(playerTimeLong);
			aDateList = CommonAPI.convertHalfHoursToPlayerTimeIntList(playerTimeLong);
			dayStartEventList = gameData.getSpecialEventByDate(aDateList, player.getPlayerMonths(), itemLockSet);
		}
		if (dayStartEventList.Count == 0)
		{
			if (nguiCameraGUI == null || (nguiCameraGUI != null && !viewController.getIsPaused()))
			{
				endPopEvent();
			}
			return false;
		}
		SpecialEvent specialEvent = dayStartEventList[0];
		dayStartEventList.RemoveAt(0);
		switch (specialEvent.getEventType())
		{
		case SpecialEventType.SpecialEventTypeSeason:
			doSeasonEvent(specialEvent);
			break;
		case SpecialEventType.SpecialEventTypeFestival:
			doFestivalEvent(specialEvent);
			break;
		case SpecialEventType.SpecialEventTypeWarning:
			doWarningEvent(specialEvent);
			break;
		}
		return true;
	}

	public void doDogEvent(SpecialEvent specialEvent)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		switch (specialEvent.getSpecialEventRefId())
		{
		case "1001":
			break;
		case "1002":
			break;
		case "1003":
			break;
		}
	}

	public void doFestivalEvent(SpecialEvent specialEvent)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		switch (specialEvent.getSpecialEventRefId())
		{
		case "2001":
			viewController.showGoldenHammerPreEvent();
			break;
		case "2002":
			viewController.showGoldenHammerInvite();
			break;
		}
	}

	public void doSeasonEvent(SpecialEvent specialEvent)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		switch (specialEvent.getSpecialEventRefId())
		{
		case "3001":
			audioController.changeBGM(CommonAPI.getSeasonBGM(Season.SeasonSpring));
			gameData.addNewWhetsappMsg(gameData.getTextByRefId("whetsappTitle01"), gameData.getRandomTextBySetRefId("whetsappSeasonSpring"), "Image/whetsapp/news2", player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeNotice);
			break;
		case "3002":
			audioController.changeBGM(CommonAPI.getSeasonBGM(Season.SeasonSummer));
			gameData.addNewWhetsappMsg(gameData.getTextByRefId("whetsappTitle01"), gameData.getRandomTextBySetRefId("whetsappSeasonSummer"), "Image/whetsapp/news2", player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeNotice);
			break;
		case "3003":
			audioController.changeBGM(CommonAPI.getSeasonBGM(Season.SeasonAutumn));
			gameData.addNewWhetsappMsg(gameData.getTextByRefId("whetsappTitle01"), gameData.getRandomTextBySetRefId("whetsappSeasonAutumn"), "Image/whetsapp/news2", player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeNotice);
			break;
		case "3004":
			audioController.changeBGM(CommonAPI.getSeasonBGM(Season.SeasonWinter));
			gameData.addNewWhetsappMsg(gameData.getTextByRefId("whetsappTitle01"), gameData.getRandomTextBySetRefId("whetsappSeasonWinter"), "Image/whetsapp/news2", player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeNotice);
			break;
		}
	}

	public void doStoryEvent(SpecialEvent specialEvent)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		switch (specialEvent.getSpecialEventRefId())
		{
		case "4001":
			break;
		case "4002":
			break;
		case "4003":
			break;
		}
	}

	public void doWarningEvent(SpecialEvent specialEvent)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		string specialEventRefId = specialEvent.getSpecialEventRefId();
		if (specialEventRefId != null && specialEventRefId == "5001")
		{
			int smithTotalSalary = player.getSmithTotalSalary();
			string text = gameData.getTextByRefIdWithDynText("salaryEvent10", "[Gold]", smithTotalSalary.ToString());
			if (smithTotalSalary >= player.getPlayerGold())
			{
				text = text + "\n\n" + gameData.getTextByRefId("salaryEvent11");
			}
			if (nguiCameraGUI != null)
			{
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: true, gameData.getTextByRefId("salaryEvent16"), gameData.getTextByRefId("salaryEvent09") + "\n\n" + text, PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
			}
		}
	}

	public void renameDogConfirmation(string dogName)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		if (dogName != string.Empty)
		{
			player.nameDog(dogName);
		}
		if (nguiCameraGUI != null)
		{
			viewController.showRenamePopup(PopupType.PopupTypeDogNaming);
		}
	}

	public void showDogNamePopup(string dogName)
	{
		GameData gameData = game.getGameData();
		string text = (longInputText = gameData.getTextByRefId("dogEvent06") + "\n\n>") + dogName;
	}

	public void giveDogToPlayer()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		popEventType = PopEventType.PopEventTypeDayEvent;
		player.giveDogToPlayer();
		string dogName = player.getDogName();
		charAnimCtr.spawnDog();
	}

	public string generateWeather()
	{
		if (game == null)
		{
			game = GameObject.Find("Game").GetComponent<Game>();
		}
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		Season seasonByMonth = CommonAPI.getSeasonByMonth(player.getSeasonIndex());
		string empty = string.Empty;
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		showSeasonBg(seasonByMonth);
		List<Weather> weatherList = game.getGameData().getWeatherList(seasonByMonth, player.getPlayerMonths(), itemLockSet);
		List<int> list = new List<int>();
		foreach (Weather item in weatherList)
		{
			list.Add(item.getWeatherChance());
		}
		Weather weather = weatherList[CommonAPI.getWeightedRandomIndex(list)];
		empty = weather.getWeatherText();
		player.setWeather(weather);
		if (viewController == null)
		{
			viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		}
		viewController.showWeatherParticles(weather);
		if (weather.getShowWhetsapp())
		{
			gameData.addNewWhetsappMsg(gameData.getTextByRefId("whetsappTitle01"), empty, "Image/whetsapp/news2", player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeNotice);
		}
		return empty;
	}

	public void showSeasonBg(Season season)
	{
		if (sceneBackground == null)
		{
			sceneBackground = GameObject.Find("SceneBackground").GetComponent<SpriteRenderer>();
		}
		switch (season)
		{
		case Season.SeasonSpring:
			sceneBackground.sprite = commonScreenObject.loadSprite("Image/Shop/background_spring");
			break;
		case Season.SeasonSummer:
			sceneBackground.sprite = commonScreenObject.loadSprite("Image/Shop/background_summer");
			break;
		case Season.SeasonAutumn:
			sceneBackground.sprite = commonScreenObject.loadSprite("Image/Shop/background_autumn");
			break;
		case Season.SeasonWinter:
			sceneBackground.sprite = commonScreenObject.loadSprite("Image/Shop/background_winter");
			break;
		}
	}

	public void showWeekend()
	{
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().setGameState(GameState.GameStatePopEvent);
		popEventType = PopEventType.PopEventTypeWeekend;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		List<WeekendActivity> list = new List<WeekendActivity>();
		List<WeekendActivity> weekendActivityListByType = gameData.getWeekendActivityListByType(WeekendActivityType.WeekendActivityTypeNormal, player);
		List<WeekendActivity> weekendActivityListByType2 = gameData.getWeekendActivityListByType(WeekendActivityType.WeekendActivityTypeDog, player);
		List<WeekendActivity> weekendActivityListByType3 = gameData.getWeekendActivityListByType(WeekendActivityType.WeekendActivityTypeAdventure, player);
		List<WeekendActivityType> weekendActivityTypeListByShopLevel = CommonAPI.getWeekendActivityTypeListByShopLevel(player.getShopLevelInt());
		using (List<WeekendActivityType>.Enumerator enumerator = weekendActivityTypeListByShopLevel.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				switch (enumerator.Current)
				{
				case WeekendActivityType.WeekendActivityTypeNormal:
				{
					if (weekendActivityListByType.Count <= 0)
					{
						break;
					}
					List<int> list4 = new List<int>();
					foreach (WeekendActivity item6 in weekendActivityListByType)
					{
						list4.Add(item6.getChance());
					}
					int weightedRandomIndex5 = CommonAPI.getWeightedRandomIndex(list4);
					WeekendActivity item5 = weekendActivityListByType[weightedRandomIndex5];
					list.Add(item5);
					weekendActivityListByType.RemoveAt(weightedRandomIndex5);
					break;
				}
				case WeekendActivityType.WeekendActivityTypeDog:
				{
					List<int> list3 = new List<int>();
					if (weekendActivityListByType2.Count > 0)
					{
						foreach (WeekendActivity item7 in weekendActivityListByType2)
						{
							list3.Add(item7.getChance());
						}
						int weightedRandomIndex3 = CommonAPI.getWeightedRandomIndex(list3);
						WeekendActivity item3 = weekendActivityListByType2[weightedRandomIndex3];
						list.Add(item3);
						weekendActivityListByType2.RemoveAt(weightedRandomIndex3);
					}
					else
					{
						if (weekendActivityListByType.Count <= 0)
						{
							break;
						}
						foreach (WeekendActivity item8 in weekendActivityListByType)
						{
							list3.Add(item8.getChance());
						}
						int weightedRandomIndex4 = CommonAPI.getWeightedRandomIndex(list3);
						WeekendActivity item4 = weekendActivityListByType[weightedRandomIndex4];
						list.Add(item4);
						weekendActivityListByType.RemoveAt(weightedRandomIndex4);
					}
					break;
				}
				case WeekendActivityType.WeekendActivityTypeAdventure:
				{
					List<int> list2 = new List<int>();
					if (weekendActivityListByType3.Count > 0)
					{
						foreach (WeekendActivity item9 in weekendActivityListByType3)
						{
							list2.Add(item9.getChance());
						}
						int weightedRandomIndex = CommonAPI.getWeightedRandomIndex(list2);
						WeekendActivity item = weekendActivityListByType3[weightedRandomIndex];
						list.Add(item);
						weekendActivityListByType3.RemoveAt(weightedRandomIndex);
					}
					else
					{
						if (weekendActivityListByType.Count <= 0)
						{
							break;
						}
						foreach (WeekendActivity item10 in weekendActivityListByType)
						{
							list2.Add(item10.getChance());
						}
						int weightedRandomIndex2 = CommonAPI.getWeightedRandomIndex(list2);
						WeekendActivity item2 = weekendActivityListByType[weightedRandomIndex2];
						list.Add(item2);
						weekendActivityListByType.RemoveAt(weightedRandomIndex2);
					}
					break;
				}
				}
			}
		}
		CommonAPI.debug("showActivityList " + list.Count);
		weekendList = list;
		List<string> list5 = new List<string>();
		foreach (WeekendActivity item11 in list)
		{
			string text = " (" + gameData.getTextByRefId("reward") + " ";
			switch (item11.getWeekendActivityType())
			{
			case WeekendActivityType.WeekendActivityTypeNormal:
				text = text + gameData.getTextByRefId("gold") + ")";
				break;
			case WeekendActivityType.WeekendActivityTypeDog:
				text = text + gameData.getTextByRefId("item") + ")";
				break;
			case WeekendActivityType.WeekendActivityTypeAdventure:
				text += "???)";
				break;
			}
			list5.Add(item11.getWeekendActivityName() + text);
		}
		if (nguiCameraGUI != null)
		{
			viewController.closeMainMenu(resume: false);
			viewController.closeTier2Menu(hide: false);
			viewController.showEventSelectPopup(EventSelectType.EventSelectTypeWeekend, string.Empty, list5);
		}
	}

	public void doWeekendActivity(int input)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		bool flag = false;
		if (input <= 0 || input > weekendList.Count)
		{
			return;
		}
		popEventType = PopEventType.PopEventTypeWeekendResult;
		WeekendActivity weekendActivity = weekendList[input - 1];
		weekendActivity.addDoneCount(1);
		string weekendActivityResultText = weekendActivity.getWeekendActivityResultText();
		string text = string.Empty;
		if (nguiCameraGUI == null)
		{
			text += "<b>";
		}
		if (weekendActivity.getWeekendActivityType() == WeekendActivityType.WeekendActivityTypeDog)
		{
			player.addDogActivity();
			player.addDogLove(weekendActivity.getDogLove());
			text = text + "\n\n" + gameData.getTextByRefIdWithDynText("dogEvent09", "[dogName]", player.getDogName());
		}
		switch (weekendActivity.getRewardType())
		{
		case "GOLD":
		{
			int rewardQty4 = weekendActivity.getRewardQty();
			player.addGold(rewardQty4);
			audioController.playGoldGainAudio();
			string text2 = text;
			text = text2 + "\n\n" + gameData.getTextByRefId("gold") + " +" + rewardQty4 + "!";
			break;
		}
		case "ITEM":
		{
			Item itemByRefId = gameData.getItemByRefId(weekendActivity.getRewardRefId());
			int rewardQty3 = weekendActivity.getRewardQty();
			itemByRefId.addItem(rewardQty3);
			List<string> list = new List<string>();
			list.Add("[itemName]");
			list.Add("[itemNum]");
			List<string> list2 = new List<string>();
			list2.Add(itemByRefId.getItemName());
			list2.Add(rewardQty3.ToString());
			text = text + "\n\n" + gameData.getTextByRefIdWithDynTextList("weekendEvent03", list, list2);
			break;
		}
		case "FURNITURE":
		{
			Furniture furnitureByRefId = gameData.getFurnitureByRefId(weekendActivity.getRewardRefId());
			furnitureByRefId.setPlayerOwned(aOwned: true);
			text = text + "\n\n" + gameData.getTextByRefIdWithDynText("weekendEvent04", "[furnitureName]", furnitureByRefId.getFurnitureName());
			break;
		}
		case "STAT_INDIV":
		{
			List<Smith> smithList2 = player.getSmithList();
			Smith selectSmith = smithList2[Random.Range(0, smithList2.Count)];
			SmithStat stat2 = CommonAPI.convertStringToSmithStat(weekendActivity.getRewardRefId());
			int rewardQty2 = weekendActivity.getRewardQty();
			int rewardMagnitude2 = weekendActivity.getRewardMagnitude();
			text = text + "\n\n" + addWeekendActivitySmithStatEffect(selectSmith, stat2, rewardMagnitude2, rewardQty2);
			break;
		}
		case "STAT_TEAM":
		{
			List<Smith> smithList = player.getSmithList();
			SmithStat stat = CommonAPI.convertStringToSmithStat(weekendActivity.getRewardRefId());
			int rewardQty = weekendActivity.getRewardQty();
			int rewardMagnitude = weekendActivity.getRewardMagnitude();
			text += "\n";
			foreach (Smith item in smithList)
			{
				text = text + "\n" + addWeekendActivitySmithStatEffect(item, stat, rewardMagnitude, rewardQty);
			}
			break;
		}
		}
		if (nguiCameraGUI == null)
		{
			text += "</b>";
		}
		weekendActivityResultText += text;
		if (nguiCameraGUI != null)
		{
			viewController.showEventSelectPopup(EventSelectType.EventSelectTypeWeekendResult, weekendActivityResultText, null);
		}
	}

	public string addWeekendActivitySmithStatEffect(Smith selectSmith, SmithStat stat, int statAdd, int effectDays)
	{
		int num = (int)((float)statAdd * Random.Range(0.9f, 1.1f));
		string empty = string.Empty;
		GameData gameData = game.getGameData();
		List<string> list = new List<string>();
		list.Add("[smithName]");
		list.Add("[stat]");
		list.Add("[amt]");
		list.Add("[numDays]");
		List<string> list2 = new List<string>();
		list2.Add(selectSmith.getSmithName());
		list2.Add(string.Empty);
		list2.Add(num.ToString());
		list2.Add(effectDays.ToString());
		switch (stat)
		{
		case SmithStat.SmithStatPower:
			selectSmith.addSmithEffect(StatEffect.StatEffectAbsPower, num, effectDays, string.Empty, string.Empty);
			list2[1] = gameData.getTextByRefId("smithStatsShort02");
			break;
		case SmithStat.SmithStatIntelligence:
			selectSmith.addSmithEffect(StatEffect.StatEffectAbsIntelligence, num, effectDays, string.Empty, string.Empty);
			list2[1] = gameData.getTextByRefId("smithStatsShort03");
			break;
		case SmithStat.SmithStatTechnique:
			selectSmith.addSmithEffect(StatEffect.StatEffectAbsTechnique, num, effectDays, string.Empty, string.Empty);
			list2[1] = gameData.getTextByRefId("smithStatsShort04");
			break;
		case SmithStat.SmithStatLuck:
			selectSmith.addSmithEffect(StatEffect.StatEffectAbsLuck, num, effectDays, string.Empty, string.Empty);
			list2[1] = gameData.getTextByRefId("smithStatsShort05");
			break;
		case SmithStat.SmithStatStamina:
			selectSmith.addSmithEffect(StatEffect.StatEffectAbsStamina, num, effectDays, string.Empty, string.Empty);
			list2[1] = gameData.getTextByRefId("smithStatsShort06");
			break;
		case SmithStat.SmithStatAll:
			selectSmith.addSmithEffect(StatEffect.StatEffectAbsPower, num, effectDays, string.Empty, string.Empty);
			selectSmith.addSmithEffect(StatEffect.StatEffectAbsIntelligence, num, effectDays, string.Empty, string.Empty);
			selectSmith.addSmithEffect(StatEffect.StatEffectAbsTechnique, num, effectDays, string.Empty, string.Empty);
			selectSmith.addSmithEffect(StatEffect.StatEffectAbsLuck, num, effectDays, string.Empty, string.Empty);
			list2[1] = gameData.getTextByRefId("smithStatsShort02") + ", " + gameData.getTextByRefId("smithStatsShort03") + ", " + gameData.getTextByRefId("smithStatsShort04") + ", " + gameData.getTextByRefId("smithStatsShort05");
			break;
		}
		return empty + gameData.getTextByRefIdWithDynTextList("weekendEvent08", list, list2);
	}

	public bool tryActionWithGold(int goldReq, bool allowNegative, bool useGold, bool showError = true)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		if (!allowNegative && player.getPlayerGold() < goldReq)
		{
			if (nguiCameraGUI != null && showError)
			{
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, gameData.getTextByRefId("errorTitle02"), gameData.getTextByRefId("errorCommon01"), PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
			}
			return false;
		}
		if (useGold)
		{
			player.reduceGold(goldReq, allowNegative: true);
			audioController.playPurchaseAudio();
			commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().showTimeStatus();
		}
		return true;
	}

	public void showGoldenHammerAwards()
	{
		viewController.showGoldenHammerAwards();
	}

	public bool checkTimedActionList()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		bool flag = false;
		List<int> list = new List<int>();
		if (gameData.checkFeatureIsUnlocked(gameLockSet, "RANDOMSCENARIO", completedTutorialIndex) && !player.checkTimedAction(TimedAction.TimedActionRandomScenario))
		{
			player.addTimedAction(TimedAction.TimedActionRandomScenario, Random.Range(10, 20));
		}
		if (player.checkHasDog() && !player.checkTimedAction(TimedAction.TimedActionDogAction))
		{
			player.addTimedAction(TimedAction.TimedActionDogAction, Random.Range(10, 20));
		}
		switch (player.passTimeOnTimedActions(2))
		{
		case TimedAction.TimedActionRecruit:
			endRecruitment();
			CommonAPI.debug("TimedActionRecruit");
			flag = true;
			break;
		case TimedAction.TimedActionForgeIncident:
		{
			ProjectType projectType = game.getPlayer().getCurrentProject().getProjectType();
			if (projectType == ProjectType.ProjectTypeWeapon || projectType == ProjectType.ProjectTypeUnique)
			{
				flag = tryForgeIncident();
			}
			if (flag)
			{
				CommonAPI.debug("TimedActionForgeIncident");
			}
			break;
		}
		case TimedAction.TimedActionRandomScenario:
			flag = tryRandomScenario();
			if (flag)
			{
				CommonAPI.debug("TimedActionRandomScenario");
			}
			break;
		case TimedAction.TimedActionDogAction:
			flag = checkDogAction();
			if (flag)
			{
				CommonAPI.debug("TimedActionDogAction");
			}
			break;
		}
		return flag;
	}

	public bool checkDogAction()
	{
		bool result = false;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int dogLove = player.getDogLove();
		if (player.checkHasDog())
		{
			int num = Random.Range(0, 20);
			if (num == 1)
			{
				List<Item> dogItemList = gameData.getDogItemList(player.getShopLevelInt(), player.getPlayerMonths(), player.getDogLove());
				if (dogItemList.Count > 0)
				{
					List<int> list = new List<int>();
					foreach (Item item2 in dogItemList)
					{
						list.Add(item2.getDogItemChance());
					}
					int weightedRandomIndex = CommonAPI.getWeightedRandomIndex(list);
					Item item = dogItemList[weightedRandomIndex];
					item.addItem(1);
					string aImage = CommonAPI.getItemImagePath(item.getItemType()) + item.getImage();
					viewController.queueDogItemGetPopup(gameData.getTextByRefId("dayStartEvent03").ToUpper(CultureInfo.InvariantCulture), aImage, item.getItemName());
				}
				result = true;
			}
			player.addTimedAction(TimedAction.TimedActionDogAction, Random.Range(10, 20));
		}
		return result;
	}

	public List<DayEndScenario> getAllowedRandomScenarioList()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		List<DayEndScenario> dayEndScenarioList = gameData.getDayEndScenarioList();
		List<DayEndScenario> list = new List<DayEndScenario>();
		foreach (DayEndScenario item in dayEndScenarioList)
		{
			if (!item.checkNeedProject() || player.checkHasForgingProject())
			{
				bool flag = checkUnlockCondition(item.getUnlockCondition(), item.getReqCount(), item.getCheckString(), item.getCheckNum(), 0);
				bool flag2 = false;
				int endCount = item.getEndCount();
				if (endCount != -1)
				{
					flag2 = checkUnlockCondition(item.getUnlockCondition(), endCount, item.getCheckString(), item.getCheckNum(), 0);
				}
				if (flag && !flag2)
				{
					list.Add(item);
				}
			}
		}
		return list;
	}

	public bool tryRandomScenario()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		bool result = false;
		List<DayEndScenario> dayEndScenarioList = gameData.getDayEndScenarioList();
		int num = Random.Range(0, 2500);
		if (num < dayEndScenarioList.Count)
		{
			DayEndScenario dayEndScenario = dayEndScenarioList[num];
			if (!dayEndScenario.checkNeedProject() || player.checkHasForgingProject())
			{
				bool flag = checkUnlockCondition(dayEndScenario.getUnlockCondition(), dayEndScenario.getReqCount(), dayEndScenario.getCheckString(), dayEndScenario.getCheckNum(), 0);
				bool flag2 = false;
				int endCount = dayEndScenario.getEndCount();
				if (endCount != -1)
				{
					flag2 = checkUnlockCondition(dayEndScenario.getUnlockCondition(), endCount, dayEndScenario.getCheckString(), dayEndScenario.getCheckNum(), 0);
				}
				if (flag && !flag2)
				{
					viewController.showRandomScenarioPopup(dayEndScenario);
					result = true;
				}
			}
		}
		player.addTimedAction(TimedAction.TimedActionRandomScenario, Random.Range(10, 20));
		return result;
	}

	public bool tryForgeIncident()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		List<ForgeIncident> forgeIncidentList = gameData.getForgeIncidentList(player.getPlayerMonths());
		bool result = false;
		int num = Random.Range(0, 350);
		if (forgeIncidentList.Count > 0 && num < forgeIncidentList.Count)
		{
			ForgeIncident aIncident = forgeIncidentList[num];
			viewController.showForgeIncidentPopup(aIncident);
			result = true;
		}
		int timer = Random.Range(10, 20);
		player.addTimedAction(TimedAction.TimedActionForgeIncident, timer);
		return result;
	}

	public void showDemoEnd(int input)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		List<string> list = new List<string>();
		int num = 0;
		int num2 = (int)((float)player.getPlayerGold() * 0.05f);
		num += num2;
		list.Add("Gold Score: " + num2);
		int num3 = gameData.getHiredSmithCount() * 100;
		num += num3;
		list.Add("Total Smiths Hired Score: " + num3);
		int num4 = player.getSmithTotalLevel() * 15;
		num += num4;
		list.Add("Final Smiths Total Level Score: " + num4);
		int num5 = player.getSmithMaxLevel() * 80;
		num += num5;
		list.Add("Final Smiths Max Level Score: " + num5);
		int num6 = (int)((float)player.getDogLove() * 0.5f);
		num += num6;
		list.Add("Dog Love Score: " + num6);
		int num7 = player.getShopLevelInt() * 1000;
		num += num7;
		list.Add("Shop Level Score: " + num7);
		int num8 = gameData.getPlayerOwnedFurniture(highestLevelOnly: false).Count * 300;
		num += num8;
		list.Add("Furniture Owned Score: " + num8);
		int num9 = (player.getLawAlignment() + player.getChaosAlignment()) * 2;
		num += num9;
		list.Add("Total War Effort Score: " + num9);
		int num10 = gameData.countPlayerCompletedQuestByType(QuestNEWType.QuestNEWTypeNormal) * 40;
		num += num10;
		list.Add("Quests Completed Score: " + num10);
		int num11 = player.getUnlockedJobClassList().Count * 100;
		num += num11;
		list.Add("Job Classes Unlocked Score: " + num11);
		int num12 = player.getCompletedLegendaryList().Count * 2000;
		num += num12;
		list.Add("Legendary Weapons Score: " + num12);
		int num13 = player.getCompletedNormalWeaponCount() * 50;
		num += num13;
		list.Add("Forged Weapons Score: " + num13);
		int num14 = player.getUnlockedWeaponList().Count * 50;
		num += num14;
		list.Add("Weapons Unlocked Score: " + num14);
		List<int> scoreWeaponStats = player.getScoreWeaponStats();
		int num15 = scoreWeaponStats[0] * 3;
		num += num15;
		list.Add("Highest Weapon Attack Score: " + num15);
		int num16 = scoreWeaponStats[1] * 3;
		num += num16;
		list.Add("Highest Weapon Speed Score: " + num16);
		int num17 = scoreWeaponStats[2] * 3;
		num += num17;
		list.Add("Highest Weapon Accuracy Score: " + num17);
		int num18 = scoreWeaponStats[3] * 3;
		num += num18;
		list.Add("Highest Weapon Magic Score: " + num18);
		int num19 = (int)((float)scoreWeaponStats[4] * 0.1f);
		num += num19;
		list.Add("Total Weapon Stats Score: " + num19);
		int num20 = scoreWeaponStats[5] * 25;
		num += num20;
		list.Add("Total Enchantments Used Score: " + num20);
		player.setFinalScore(num);
	}

	public void restartGame()
	{
		CommonAPI.clearNewsFeedGUIItems();
		popEventType = PopEventType.PopEventTypeStartMenu;
		Application.LoadLevel("ALLNGUIMENU");
	}

	public void showStartMenu()
	{
		CommonAPI.clearNewsFeedGUIItems();
		popEventType = PopEventType.PopEventTypeStartMenu;
		if (game.getPlayer().getPlayerName() == string.Empty)
		{
			showIntro(isLoad: false);
		}
	}

	public void showLoadMenu()
	{
		popEventType = PopEventType.PopEventTypeLoadMenu;
		GameData gameData = game.getGameData();
		string text = SC.ToString(PlayerPrefs.GetString("autosaveLoad", string.Empty));
		List<string> list = new List<string>();
		for (int i = 1; i <= 5; i++)
		{
			string text2 = SC.ToString(PlayerPrefs.GetString("save" + i + "Load", string.Empty));
			if (text2 != string.Empty)
			{
				list.Add(text2);
			}
		}
		if (text != string.Empty)
		{
		}
		int num = 2;
		foreach (string item in list)
		{
			if (item != string.Empty)
			{
				num++;
			}
		}
	}

	public void showDemoIntro()
	{
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().setGameState(GameState.GameStatePopEvent);
		popEventType = PopEventType.PopEventTypeDemoIntro;
		game.resetPlayer();
	}

	public void showIntro(bool isLoad)
	{
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().setGameState(GameState.GameStatePopEvent);
		if (game == null)
		{
			game = GameObject.Find("Game").GetComponent<Game>();
		}
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string initialConditionSet = gameScenarioByRefId.getInitialConditionSet();
		bool flag = false;
		if (isLoad)
		{
			popEventType = PopEventType.PopEventTypeLoad;
		}
		else
		{
			CommonAPI.debug("showIntro INITIAL");
			List<InitialValue> initialValueBySetAndType = gameData.getInitialValueBySetAndType(initialConditionSet, "GAME_START_TIME");
			long timeLong = 14L;
			if (initialValueBySetAndType.Count > 0)
			{
				timeLong = long.Parse(initialValueBySetAndType[0].getInitialValue());
			}
			player.setTimeLong(timeLong);
			int playerMonths = player.getPlayerMonths();
			player.setLastPaidMonth(playerMonths);
			Season seasonByMonth = CommonAPI.getSeasonByMonth(player.getSeasonIndex());
			showSeasonBg(seasonByMonth);
			popEventType = PopEventType.PopEventTypeIntro;
			LoadingScript component = GameObject.Find("LoadingMask").GetComponent<LoadingScript>();
			component.startLoadingFromBlack(string.Empty);
			string gameLockSet = gameScenarioByRefId.getGameLockSet();
			int completedTutorialIndex = player.getCompletedTutorialIndex();
			if (!gameData.checkFeatureIsUnlocked(gameLockSet, "ENCHANT", completedTutorialIndex))
			{
				player.setEnchantLocked(locked: true);
			}
			List<InitialValue> initialValueBySetAndType2 = gameData.getInitialValueBySetAndType(initialConditionSet, "SHOP_LEVEL");
			string refId = "101";
			if (initialValueBySetAndType2.Count > 0)
			{
				refId = initialValueBySetAndType2[0].getInitialValue();
			}
			player.setShopLevel(gameData.getShopLevel(refId));
			List<InitialValue> initialValueBySetAndType3 = gameData.getInitialValueBySetAndType(initialConditionSet, "FURNITURE");
			foreach (InitialValue item in initialValueBySetAndType3)
			{
				CommonAPI.debug("initFurniture " + item.getInitialValue());
				giveShopFurniture(item.getInitialValue());
			}
			player.setShowAwardsFromYear(player.getNextGoldenHammerYear());
			GUIGridController component2 = GameObject.Find("GUIGridController").GetComponent<GUIGridController>();
			component2.createWorld();
			string text = string.Empty;
			List<InitialValue> initialValueBySetAndType4 = gameData.getInitialValueBySetAndType(initialConditionSet, "INTRO_CUTSCENE");
			if (initialValueBySetAndType4.Count > 0)
			{
				text = initialValueBySetAndType4[0].getInitialValue();
			}
			CommonAPI.debug("initialConditionsSet " + initialConditionSet + " cutsceneSet " + text);
			if (text != string.Empty)
			{
				viewController.showDialoguePopup(text, gameData.getDialogueBySetId(text));
				flag = true;
			}
			else
			{
				flag = false;
			}
		}
		if (flag)
		{
			return;
		}
		if (player.getPlayerName() == string.Empty || player.getShopName() == string.Empty)
		{
			getPlayerDetails();
			return;
		}
		player.setNamed();
		if (isLoad && nguiCameraGUI != null)
		{
			CommonAPI.debug("RUN");
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: true, gameData.getTextByRefId("intro10"), gameData.getTextByRefIdWithDynText("intro07", "[playerName]", player.getPlayerName()) + "\n" + gameData.getTextByRefIdWithDynText("intro08", "[shopName]", player.getShopName()), PopupType.PopupTypeLoadSuccess, null, colorTag: false, null, map: false, string.Empty);
		}
	}

	public void unlockFeature(string feature)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		switch (feature)
		{
		case "DOG":
			giveDogToPlayer();
			GameObject.Find("Panel_TopLeftMenu").GetComponent<GUITopMenuNewController>().updateDogBowl();
			break;
		case "DOGCONTROL":
			player.setRandomDog(aBool: false);
			break;
		case "PATATA":
			if (!player.checkHasAvatar())
			{
				GUICharacterAnimationController component2 = GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>();
				player.giveAvatarToPlayer();
				component2.spawnAvatar();
			}
			break;
		case "ENCHANT":
			player.setEnchantLocked(locked: false);
			GameObject.Find("GUIObstacleController").GetComponent<GUIObstacleController>().createObstacles();
			break;
		case "SMITHHIRE":
		{
			GameObject gameObject = GameObject.Find("Panel_SmithList");
			if (gameObject != null)
			{
				GUISmithListMenuController component = gameObject.GetComponent<GUISmithListMenuController>();
				component.enableSmithHire();
				component.closeHirePanel(isDismissSmithList: true);
			}
			break;
		}
		}
	}

	public void getPlayerDetails()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		if (player.getPlayerName() == string.Empty)
		{
			if (nguiCameraGUI != null)
			{
				viewController.showRenamePopup(PopupType.PopupTypeIntroName);
			}
		}
		else if (player.getShopName() == string.Empty)
		{
			if (nguiCameraGUI != null)
			{
				viewController.showRenamePopup(PopupType.PopupTypeIntroShopName);
			}
		}
		else if (player.getGameScenario() == "30001" && nguiCameraGUI != null)
		{
			viewController.showRenamePopup(PopupType.PopupTypeDogNaming);
		}
	}

	public void enterPlayerName(string inputName)
	{
		if (inputName != string.Empty)
		{
			Player player = game.getPlayer();
			player.setPlayerName(inputName);
			getPlayerDetails();
		}
	}

	public void enterShopName(string inputName)
	{
		if (inputName != string.Empty)
		{
			Player player = game.getPlayer();
			player.setShopName(inputName);
			getPlayerDetails();
		}
	}

	public void startNewPlayer()
	{
		CommonAPI.debug("startNewPlayer INITIAL");
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		bool flag = player.checkSkipTutorials();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		CommonAPI.debug("currentScenario " + gameScenarioByRefId.getGameScenarioRefId());
		string initialConditionSet = gameScenarioByRefId.getInitialConditionSet();
		Avatar avatarByRefId = gameData.getAvatarByRefId("101");
		player.chooseAvatar(avatarByRefId.getAvatarRefId(), avatarByRefId.getAvatarName(), avatarByRefId.getAvatarDesc(), avatarByRefId.getAvatarImage());
		int num = 500;
		List<InitialValue> initialValueBySetAndType = gameData.getInitialValueBySetAndType(initialConditionSet, "GOLD");
		if (initialValueBySetAndType.Count > 0)
		{
			num = initialValueBySetAndType[0].getInitialQty();
		}
		player.setPlayerGold(num);
		player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeEarningStartingCapital, string.Empty, num);
		List<InitialValue> list = new List<InitialValue>();
		string aType = "SMITH";
		if (flag)
		{
			aType = "SMITHSKIP";
		}
		list = gameData.getInitialValueBySetAndType(initialConditionSet, aType);
		foreach (InitialValue item in list)
		{
			Smith smithByRefId = gameData.getSmithByRefId(item.getInitialValue());
			SmithStation assignedRole = SmithStation.SmithStationDesign;
			doHireSmith(smithByRefId);
			switch (item.getInitialQty())
			{
			case 1:
				assignedRole = SmithStation.SmithStationDesign;
				break;
			case 2:
				assignedRole = SmithStation.SmithStationCraft;
				break;
			case 3:
				assignedRole = SmithStation.SmithStationPolish;
				break;
			case 4:
				assignedRole = SmithStation.SmithStationEnchant;
				break;
			}
			smithByRefId.setAssignedRole(assignedRole);
		}
		List<InitialValue> initialValueBySetAndType2 = gameData.getInitialValueBySetAndType(initialConditionSet, "WEAPON_TYPE");
		foreach (InitialValue item2 in initialValueBySetAndType2)
		{
			doUnlockWeaponType(item2.getInitialValue(), unlockFirstWeapon: false);
		}
		List<InitialValue> initialValueBySetAndType3 = gameData.getInitialValueBySetAndType(initialConditionSet, "WEAPON");
		foreach (InitialValue item3 in initialValueBySetAndType3)
		{
			doUnlockWeapon(item3.getInitialValue());
		}
		List<InitialValue> initialValueBySetAndType4 = gameData.getInitialValueBySetAndType(initialConditionSet, "ITEM");
		List<string> list2 = new List<string>();
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		foreach (InitialValue item4 in initialValueBySetAndType4)
		{
			list2.Add(item4.getInitialValue());
			dictionary.Add(item4.getInitialValue(), item4.getInitialQty());
		}
		List<Item> itemListByRefId = gameData.getItemListByRefId(list2);
		foreach (Item item5 in itemListByRefId)
		{
			item5.addItem(dictionary[item5.getItemRefId()]);
		}
		int areaRegion = 1;
		List<InitialValue> initialValueBySetAndType5 = gameData.getInitialValueBySetAndType(initialConditionSet, "REGION");
		if (initialValueBySetAndType5.Count > 0)
		{
			areaRegion = initialValueBySetAndType5[0].getInitialQty();
		}
		player.setAreaRegion(areaRegion);
		List<InitialValue> initialValueBySetAndType6 = gameData.getInitialValueBySetAndType(initialConditionSet, "AREA");
		foreach (InitialValue item6 in initialValueBySetAndType6)
		{
			Area areaByRefID = gameData.getAreaByRefID(item6.getInitialValue());
			if (areaByRefID != null && areaByRefID.getAreaRefId() != string.Empty)
			{
				areaByRefID.setUnlock(aUnlock: true);
			}
		}
		List<InitialValue> initialValueBySetAndType7 = gameData.getInitialValueBySetAndType(initialConditionSet, "VARIABLE_SET");
		foreach (InitialValue item7 in initialValueBySetAndType7)
		{
			makeScenarioVariableSet(initialConditionSet, item7.getInitialValue());
		}
		if (flag)
		{
			player.setCompletedTutorialIndex(7);
			player.updateSmithStations();
			StationController component = GameObject.Find("StationController").GetComponent<StationController>();
			component.assignSmithStations();
			GUICharacterAnimationController component2 = GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>();
			foreach (InitialValue item8 in list)
			{
				string initialValue = item8.getInitialValue();
				component2.spawnCharacterFrmheaven(initialValue);
			}
			GameObject gameObject = GameObject.Find("Panel_TopLeftMenu");
			if (gameObject != null)
			{
				audioController.playSlideEnterAudio();
				viewController.moveHUD(gameObject, MoveDirection.Right, moveIn: true, 0.75f, null, string.Empty);
			}
			viewController.closeAssignSmithMenu(null, hide: true, resume: true, resumeFromPlayerPause: true);
		}
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		if (gameData.checkFeatureIsUnlocked(gameLockSet, "DOG", completedTutorialIndex))
		{
			unlockFeature("DOG");
		}
		if (gameData.checkFeatureIsUnlocked(gameLockSet, "DOGCONTROL", completedTutorialIndex))
		{
			unlockFeature("DOGCONTROL");
		}
		if (gameData.checkFeatureIsUnlocked(gameLockSet, "PATATA", completedTutorialIndex))
		{
			unlockFeature("PATATA");
		}
		setDoUnlockCheck();
		if (nguiCameraGUI == null)
		{
			checkUnlocks();
		}
		if (nguiCameraGUI != null)
		{
			if (smithListMenuController == null)
			{
				smithListMenuController = GameObject.Find("Panel_SmithList").GetComponent<GUISmithListMenuController>();
			}
			smithListMenuController.setReference();
		}
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().startGameTimer();
		GameObject gameObject2 = GameObject.Find("Panel_BottomMenu");
		if (gameObject2 != null)
		{
			gameObject2.GetComponent<BottomMenuController>().refreshBottomButtons();
		}
		string textByRefIdWithDynText = gameData.getTextByRefIdWithDynText("intro03", "[playerName]", "[D484F5]" + player.getPlayerName() + "[-]");
		gameData.addNewWhetsappMsg(player.getShopName(), textByRefIdWithDynText, "Image/whetsapp/news2", player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeNotice);
	}

	public void makeScenarioVariableSet(string aScenario, string aSetName)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string formulaConstantsSet = gameScenarioByRefId.getFormulaConstantsSet();
		List<ScenarioVariable> scenarioVariableListByListName = gameData.getScenarioVariableListByListName(aScenario, aSetName);
		foreach (ScenarioVariable item in scenarioVariableListByListName)
		{
			string[] array = item.getVariableInitValue().Split('@');
			int randomInt = CommonAPI.getRandomInt(array.Length);
			item.setVariableValueString(array[randomInt].ToString());
		}
	}

	public string saveGame(string saveKey, bool isPlayerSave)
	{
		CommonAPI.debug("saveGame");
		return commonScreenObject.getController("DynamicDataController").GetComponent<DynamicDataController>().saveData(saveKey, isPlayerSave);
	}

	public string loadGame(string saveKey)
	{
		CommonAPI.debug("loadGame");
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		return commonScreenObject.getController("DynamicDataController").GetComponent<DynamicDataController>().getDynDataFromFile(saveKey);
	}

	public void showPopEvent(PopEventType aShow)
	{
		CommonAPI.debug("showPopEvent " + aShow);
		if (aShow != PopEventType.PopEventTypeNothing)
		{
			popEventType = aShow;
		}
		if (popEventType != PopEventType.PopEventTypeNothing)
		{
			commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().setGameState(GameState.GameStatePopEvent);
		}
		switch (popEventType)
		{
		case PopEventType.PopEventTypeRecruitment:
			showSmithRecruitmentMenu(100);
			break;
		case PopEventType.PopEventTypeRecruitmentFire:
			break;
		case PopEventType.PopEventTypeBoost1Design:
			showForgeBoost1Design();
			break;
		case PopEventType.PopEventTypeBoost2Craft:
			showForgeBoost2Craft();
			break;
		case PopEventType.PopEventTypeBoost3Polish:
			showForgeBoost3Polish();
			break;
		case PopEventType.PopEventTypeBoost4Enchant:
			showForgeBoost4Enchant();
			break;
		case PopEventType.PopEventTypeBoost4EnchantItem:
			showForgeBoost4Enchant();
			break;
		case PopEventType.PopEventTypeNaming:
			showWeaponComplete();
			break;
		case PopEventType.PopEventTypeLegendaryComplete:
			showAppraisalPopup();
			break;
		case PopEventType.PopEventTypeHeroVisit:
			showHeroVisitResult();
			break;
		case PopEventType.PopEventTypeWeekend:
		{
			for (int i = 0; i < 10; i++)
			{
				showWeekend();
			}
			break;
		}
		case PopEventType.PopEventTypeDayStart:
			showDayStart();
			break;
		case PopEventType.PopEventTypeDayEvent:
			checkStartOfDayActionList();
			break;
		case PopEventType.PopEventTypeDogName:
			renameDogConfirmation(string.Empty);
			break;
		case PopEventType.PopEventTypeWeekendResult:
			startNewDay();
			break;
		case PopEventType.PopEventTypeGoldenHammer:
			showGoldenHammerAwards();
			break;
		case PopEventType.PopEventTypeStartMenu:
			showStartMenu();
			break;
		case PopEventType.PopEventTypeLoadMenu:
			showLoadMenu();
			break;
		case PopEventType.PopEventTypeGameOver:
			break;
		case PopEventType.PopEventTypeBoost1DesignTag:
		case PopEventType.PopEventTypeBoost2CraftTag:
		case PopEventType.PopEventTypeBoost3PolishTag:
		case PopEventType.PopEventTypeBoost4EnchantTag:
		case PopEventType.PopEventTypeAuction:
		case PopEventType.PopEventTypeLegendaryHeroGet:
		case PopEventType.PopEventTypeChooseQuest:
		case PopEventType.PopEventTypeQuestProgress:
		case PopEventType.PopEventTypeAfterQuestCutscene:
		case PopEventType.PopEventTypeDogGet:
		case PopEventType.PopEventTypeDayEnd:
		case PopEventType.PopEventTypeStoryPopup:
		case PopEventType.PopEventTypePostGoldenHammer:
		case PopEventType.PopEventTypeSmithVisit:
		case PopEventType.PopEventTypeTagReplace:
		case PopEventType.PopEventTypeDemoIntro:
		case PopEventType.PopEventTypeIntro:
		case PopEventType.PopEventTypeLoad:
			break;
		}
	}

	public void endPopEvent()
	{
		CommonAPI.debug("endPopEvent");
		popEventType = PopEventType.PopEventTypeNothing;
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().returnToShopView();
	}

	public void actionPopEvent(int input)
	{
		CommonAPI.debug("actionPopEvent");
		if (input < 0)
		{
			return;
		}
		switch (popEventType)
		{
		case PopEventType.PopEventTypeRecruitment:
			if (input == 0)
			{
				endPopEvent();
			}
			break;
		case PopEventType.PopEventTypeRecruitmentFire:
			if (input == 0)
			{
				showSmithRecruitmentMenu(100);
			}
			break;
		case PopEventType.PopEventTypeWeekend:
			doWeekendActivity(input);
			break;
		case PopEventType.PopEventTypeDayStart:
			if (input == 0)
			{
				showDayStart();
			}
			break;
		case PopEventType.PopEventTypeDayEvent:
			if (input == 0)
			{
				checkStartOfDayActionList();
			}
			break;
		case PopEventType.PopEventTypeStartMenu:
			switch (input)
			{
			case 1:
				showDemoIntro();
				break;
			case 2:
				showLoadMenu();
				break;
			}
			break;
		case PopEventType.PopEventTypeLoadMenu:
			switch (input)
			{
			case 1:
			{
				string text2 = SC.ToString(PlayerPrefs.GetString("autosaveLoad", string.Empty));
				if (text2 != string.Empty)
				{
					loadGame("autosave");
				}
				break;
			}
			case 0:
				showStartMenu();
				break;
			default:
			{
				int num = input - 1;
				string text = SC.ToString(PlayerPrefs.GetString("save" + num + "Load", string.Empty));
				if (text != string.Empty)
				{
					loadGame("save" + num);
				}
				break;
			}
			}
			break;
		case PopEventType.PopEventTypeIntro:
			if (input == 0)
			{
				showDayStart();
			}
			break;
		case PopEventType.PopEventTypeLoad:
			if (input == 0)
			{
				endPopEvent();
			}
			break;
		case PopEventType.PopEventTypeStoryPopup:
			if (input == 0)
			{
				checkStartOfDayActionList();
			}
			break;
		case PopEventType.PopEventTypeGameOver:
			if (input == 0)
			{
				restartGame();
			}
			break;
		case PopEventType.PopEventTypeTheEnd:
			if (input == 0)
			{
				restartGame();
			}
			break;
		}
	}

	public void startGame()
	{
		StartCoroutine("startANewGame");
	}

	private IEnumerator startANewGame()
	{
		yield return StartCoroutine("setupWorld");
		GameObject.Find("DemoNotice_label").GetComponent<UILabel>().text = game.getGameData().getTextByRefIdWithDynText("demoText01", "[demoGold]", CommonAPI.formatNumber(100000));
	}

	private IEnumerator setupWorld()
	{
		startNewPlayer();
		LoadingScript component = GameObject.Find("LoadingMask").GetComponent<LoadingScript>();
		if (component.checkIsLoadingMaskVisible())
		{
			component.startLoadingFromBlack(string.Empty);
		}
		GameObject.Find("GUIGridController").GetComponent<GUIGridController>().createWorld();
		showDayStart();
		return null;
	}

	public void insertStoredInput(string key, int value)
	{
		if (storedInputDict.ContainsKey(key))
		{
			storedInputDict.Remove(key);
		}
		storedInputDict.Add(key, value);
	}

	public int referStoredInput(string key)
	{
		int value = -1;
		storedInputDict.TryGetValue(key, out value);
		return value;
	}

	public int retrieveStoredInput(string key)
	{
		int value = -1;
		storedInputDict.TryGetValue(key, out value);
		storedInputDict.Remove(key);
		return value;
	}
}
