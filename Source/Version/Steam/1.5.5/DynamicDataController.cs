using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class DynamicDataController : MonoBehaviour
{
	private Game game;

	private JsonFileController jsonFileController;

	private string errorCode;

	private int coroutineCheckDone;

	private int coroutineCheckStart;

	private void Start()
	{
		setUpObjects();
		coroutineCheckDone = 0;
		coroutineCheckStart = 0;
	}

	private void setUpObjects()
	{
		if (game == null)
		{
			game = GameObject.Find("Game").GetComponent<Game>();
		}
		if (jsonFileController == null)
		{
			jsonFileController = GameObject.Find("JsonFileController").GetComponent<JsonFileController>();
		}
		errorCode = string.Empty;
	}

	public string saveData(string saveName, bool isPlayerSave)
	{
		string text = string.Empty;
		int num = 0;
		errorCode = string.Empty;
		while (text == string.Empty && num < 10)
		{
			try
			{
				num++;
				ServerDynValue serverDynValue = new ServerDynValue();
				ServerDynResponse serverDynResponse = new ServerDynResponse();
				Player player = game.getPlayer();
				GameData gameData = game.getGameData();
				serverDynValue.DynPlayer = makeDynPlayer(player);
				serverDynValue.DynCode = makeDynCode(player, gameData);
				serverDynValue.DynActivity = makeDynActivityList(player, gameData);
				serverDynValue.DynFurniture = makeDynFurnitureList(player, gameData);
				serverDynValue.DynDecoration = makeDynDecorationList(player, gameData);
				serverDynValue.DynItem = makeDynItemList(player, gameData);
				serverDynValue.DynExploreItem = makeDynExploreItemList(player, gameData);
				serverDynValue.DynHero = makeDynHeroList(player, gameData);
				serverDynValue.DynArea = makeDynAreaList(player, gameData);
				serverDynValue.DynShopMonthlyStarch = makeDynShopMonthlyStarchList(player, gameData);
				serverDynValue.DynDisplayHeroRequest = makeDynHeroRequestList(player, gameData, completed: false);
				serverDynValue.DynCompletedHeroRequest = makeDynHeroRequestList(player, gameData, completed: true);
				serverDynValue.DynObjective = makeDynObjective(player, gameData);
				serverDynValue.DynLegendaryhero = makeDynLegendaryHero(player, gameData);
				serverDynValue.DynProject = makeDynProjectList(player, gameData);
				serverDynValue.DynProjectOfferList = makeDynProjectOfferList(player, gameData);
				serverDynValue.DynProjectSelectedOffer = makeDynProjectSelectedOffer(player, gameData);
				serverDynValue.DynQuestNEW = makeDynQuestNEWList(player, gameData);
				serverDynValue.DynSmith = makeDynSmithList(player, gameData);
				serverDynValue.DynContract = makeDynContract(player, gameData);
				serverDynValue.DynSmithExperience = makeDynSmithExperienceList(player, gameData);
				serverDynValue.DynSmithEffect = makeDynSmithEffectList(player, gameData);
				serverDynValue.DynSmithTraining = makeDynSmithTrainingList(player, gameData);
				serverDynValue.DynSpecialEvent = makeDynSpecialEventList(player, gameData);
				serverDynValue.DynSeasonObjective = makeDynSeasonObjectiveList(player, gameData);
				serverDynValue.DynTag = makeDynTagList(player, gameData);
				serverDynValue.DynQuestTag = makeDynQuestTagList(player, gameData);
				serverDynValue.DynSmithTag = makeDynSmithTagList(player, gameData);
				serverDynValue.DynProjectTag = makeDynProjectTagList(player, gameData);
				serverDynValue.DynWeapon = makeDynWeaponList(player, gameData);
				serverDynValue.DynWeaponType = makeDynWeaponTypeList(player, gameData);
				serverDynValue.DynWeekendActivity = makeDynWeekendActivityList(player, gameData);
				serverDynValue.DynStation = makeDynStation(player, gameData);
				serverDynValue.DynWhetsapp = makeDynWhetsapp(player, gameData);
				serverDynValue.DynAchievement = makeDynAchievement(player, gameData);
				serverDynValue.DynScenarioVariable = makeDynScenarioVariableList(player, gameData);
				serverDynResponse.result = 1;
				serverDynResponse.errorCode = 0;
				serverDynResponse.errorMsg = "NONE";
				serverDynResponse.value = serverDynValue;
				string empty = string.Empty;
				string toEncrypt = JsonMapper.ToJson(serverDynResponse);
				empty = CommonAPI.Encrypt(toEncrypt, "e1n6c3dy4n9k2ey5");
				jsonFileController.saveContent("WS_" + saveName + ".txt", empty);
				saveFileDir(saveName + "Load", player.getSaveTimeString());
				string text2 = SC.FromString(player.getSaveTimeString());
				jsonFileController.saveContentPlayerPrefs(saveName + "Load", text2);
				if (saveName == "final" || (player.checkSaveLogTime() && isPlayerSave))
				{
					player.setSaveLogTime();
					if (!(saveName == "final"))
					{
					}
				}
				text = "SUCCESS";
			}
			catch (Exception ex)
			{
				CommonAPI.debug(ex.ToString());
				text = "0000";
			}
		}
		if (text == "SUCCESS")
		{
			GameObject gameObject = GameObject.Find("Panel_TopLeftMenu");
			if (gameObject != null)
			{
				GUITopMenuNewController component = gameObject.GetComponent<GUITopMenuNewController>();
				component.showSaveIcon();
			}
			return text + "_" + errorCode;
		}
		return text + errorCode;
	}

	private void saveFileDir(string id, string content)
	{
		Dictionary<string, string> dictionary = jsonFileController.loadSaveFileDir(game);
		if (dictionary.ContainsKey(id))
		{
			dictionary[id] = content;
		}
		else
		{
			dictionary.Add(id, content);
		}
		jsonFileController.saveSaveFileDir(dictionary);
	}

	public void startSendData(Player player, ServerDynValue dynDataValue, bool isEndGame, string saveName)
	{
		ServerDynLog serverDynLog = new ServerDynLog();
		ServerDynLogData serverDynLogData = new ServerDynLogData();
		serverDynLogData.playerID = player.getPlayerId();
		serverDynLogData.playerName = player.getPlayerName();
		string text = JsonMapper.ToJson(dynDataValue);
		text = (serverDynLogData.data = text.Replace("\"", "#"));
		serverDynLogData.email = player.getEmail();
		serverDynLogData.highScore = player.getFinalScore().ToString();
		serverDynLog.from = SystemInfo.deviceUniqueIdentifier;
		serverDynLog.gameID = "WeaponStory";
		serverDynLog.version = "0.1";
		serverDynLog.msgID = "7fce40aaf4e92e2fedc99a6c112f1bdc";
		if (isEndGame)
		{
			serverDynLog.requestID = "be075ffa720d74413d14df1964a9fe5c";
		}
		else
		{
			serverDynLog.requestID = "82963a666691ccbd70eee42aa93c1382";
		}
		serverDynLog.timestamp = DateTime.Now.ToString();
		serverDynLog.platform = saveName;
		serverDynLog.data = serverDynLogData;
		string empty = string.Empty;
		empty = JsonMapper.ToJson(serverDynLog);
		jsonFileController.saveContent("uploadData", empty);
		StartCoroutine(sendPlayerData(empty));
	}

	public IEnumerator sendPlayerData(string uploadData)
	{
		WWWForm form = new WWWForm();
		form.AddField("json", uploadData);
		CommonAPI.debug("sendPlayerData " + uploadData);
		WWW www = new WWW("http://54.251.97.227/WeaponStory/Development/DarkEngineConnection.php", form);
		while (!www.isDone)
		{
			yield return null;
		}
		CommonAPI.debug("***data:" + www.text);
		CommonAPI.debug("***error:" + www.error);
	}

	public DynPlayer makeDynPlayer(Player player)
	{
		DynPlayer dynPlayer = new DynPlayer();
		try
		{
			dynPlayer.playerId = player.getPlayerId();
			dynPlayer.gameScenario = player.getGameScenario();
			dynPlayer.playerName = player.getPlayerName();
			dynPlayer.shopName = player.getShopName();
			dynPlayer.shopLevelRefId = player.getShopLevelRefId();
			dynPlayer.areaRegion = player.getAreaRegion().ToString();
			dynPlayer.playerFame = player.getFame().ToString();
			dynPlayer.playerTickets = player.getTickets().ToString();
			dynPlayer.playerUsedTickets = player.getUsedTickets().ToString();
			dynPlayer.prevRegionFame = player.getPrevRegionFame().ToString();
			dynPlayer.prevRegionTickets = player.getPrevRegionTickets().ToString();
			dynPlayer.hasAvatar = player.checkHasAvatar().ToString();
			dynPlayer.avatarRefId = player.getAvatarRefId();
			dynPlayer.playerGold = player.getPlayerGold().ToString();
			dynPlayer.playerGood = player.getPlayerLaw().ToString();
			dynPlayer.playerEvil = player.getPlayerChaos().ToString();
			dynPlayer.usedEmblems = player.getUsedEmblems().ToString();
			dynPlayer.tofuKnown = player.getTofuKnown().ToString();
			dynPlayer.randomDog = player.checkRandomDog().ToString();
			dynPlayer.hasDog = player.checkHasDog().ToString();
			dynPlayer.dogBedRefID = player.getDogBedRefID();
			dynPlayer.dogName = player.getDogName();
			dynPlayer.dogLove = player.getDogLove().ToString();
			dynPlayer.dogEnergy = player.getDogEnergy().ToString();
			dynPlayer.dogLastFed = player.getDogLastFed().ToString();
			dynPlayer.dogActivityCountMonth = player.getDogActivityCountMonth().ToString();
			dynPlayer.dogActivityCountTotal = player.getDogActivityCountTotal().ToString();
			dynPlayer.loanUsed = player.getUseLoan().ToString();
			dynPlayer.salaryChancesUsed = player.getSalaryChancesUsed().ToString();
			dynPlayer.lastPaidMonth = player.getLastPaidMonth().ToString();
			dynPlayer.currentObjective = player.getCurrentObjective().getObjectiveRefId();
			dynPlayer.completedObjectiveList = string.Empty;
			dynPlayer.currentProjectId = player.getCurrentProject().getProjectId();
			dynPlayer.lastDoneQuest = player.getLastDoneQuest().getQuestRefId();
			dynPlayer.displayProjectList = player.getDisplayProjectListIdString();
			dynPlayer.playerContractList = player.makeContractListIdString();
			dynPlayer.recruitRefIdList = player.makeRecruitListIdString();
			dynPlayer.lastDailyAction = player.getLastDailyAction().ToString();
			dynPlayer.playerTimeLong = player.getPlayerTimeLong().ToString();
			dynPlayer.actionList = player.getTimedActionListString();
			dynPlayer.timerList = player.getTimedActionTimerListString();
			dynPlayer.weatherRefId = player.getWeather().getWeatherRefId();
			dynPlayer.forgeEffect = player.getForgeEffect().ToString();
			dynPlayer.nextGoldenHammerAwardList = player.getNextGoldenHammerAwardListString();
			dynPlayer.nextGoldenHammerYear = player.getNextGoldenHammerYear().ToString();
			dynPlayer.pastGoldenHammerAwardList = player.getPastGoldenHammerAwardListString();
			dynPlayer.showAwardsFromYear = player.getShowAwardsFromYear().ToString();
			dynPlayer.isNamed = player.checkNamed().ToString();
			dynPlayer.email = player.getEmail();
			dynPlayer.finalScore = player.getFinalScore().ToString();
			dynPlayer.playerStation = player.getPlayerStation().ToString();
			dynPlayer.dogStation = player.getDogStation().ToString();
			dynPlayer.designStationLv = player.getDesignStationLv().ToString();
			dynPlayer.craftStationLv = player.getCraftStationLv().ToString();
			dynPlayer.polishStationLv = player.getPolishStationLv().ToString();
			dynPlayer.enchantStationLv = player.getEnchantStationLv().ToString();
			dynPlayer.enchantLocked = player.getEnchantLocked().ToString();
			Weapon currentResearchWeapon = player.getCurrentResearchWeapon();
			if (currentResearchWeapon != null)
			{
				dynPlayer.researchWeaponRefID = player.getCurrentResearchWeapon().getWeaponRefId();
			}
			else
			{
				dynPlayer.researchWeaponRefID = string.Empty;
			}
			Smith currentResearchSmith = player.getCurrentResearchSmith();
			if (currentResearchSmith != null)
			{
				dynPlayer.researchSmithRefID = currentResearchSmith.getSmithRefId();
			}
			else
			{
				dynPlayer.researchSmithRefID = string.Empty;
			}
			dynPlayer.researchLength = player.getResearchLength().ToString();
			dynPlayer.researchTimeLeft = player.getResearchTimeLeft().ToString();
			dynPlayer.researchCount = player.getResearchCount().ToString();
			dynPlayer.lastEventDate = player.getLastEventDate().ToString();
			dynPlayer.lastAreaEvent = player.getLastAreaEvent().ToString();
			dynPlayer.lastCheckRequest = player.getLastCheckRequest().ToString();
			dynPlayer.displayLegendaryList = player.getDisplayLegendaryHeroRefIDString();
			dynPlayer.completedLegendaryList = player.getCompletedLegendaryHeroRefIDString();
			dynPlayer.displayRequestList = player.getDisplayRequestRefIDString();
			dynPlayer.completedRequestList = player.getCompletedRequestRefIDString();
			dynPlayer.requestCount = player.getTotalRequestCount().ToString();
			dynPlayer.skipTutorials = player.checkSkipTutorials().ToString();
			dynPlayer.tutorialState = player.getTutorialState().ToString();
			dynPlayer.tutorialIndex = player.getTutorialIndex().ToString();
			dynPlayer.completedTutorialIndex = player.getCompletedTutorialIndex().ToString();
			dynPlayer.totalHeroExpGain = player.getTotalHeroExpGain().ToString();
			return dynPlayer;
		}
		catch (Exception)
		{
			errorCode += "1001";
			return dynPlayer;
		}
	}

	public List<DynCode> makeDynCode(Player player, GameData gameData)
	{
		List<DynCode> list = new List<DynCode>();
		try
		{
			List<Code> codeList = gameData.getCodeList();
			foreach (Code item in codeList)
			{
				DynCode dynCode = new DynCode();
				dynCode.codeRefId = item.getRefID();
				dynCode.isUsed = item.checkUsed().ToString();
				list.Add(dynCode);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "1002";
			return list;
		}
	}

	public List<DynActivity> makeDynActivityList(Player player, GameData gameData)
	{
		List<DynActivity> list = new List<DynActivity>();
		try
		{
			Dictionary<string, Activity> activityList = player.getActivityList();
			foreach (Activity value in activityList.Values)
			{
				DynActivity dynActivity = new DynActivity();
				dynActivity.activityID = value.getActivityID();
				dynActivity.activityType = value.getActivityType().ToString();
				dynActivity.activityState = value.getActivityState().ToString();
				dynActivity.areaRefID = value.getAreaRefID();
				dynActivity.smithRefID = value.getSmithRefID();
				dynActivity.travelTime = value.getTravelTime().ToString();
				dynActivity.progress = value.getProgress().ToString();
				list.Add(dynActivity);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "2001";
			return list;
		}
	}

	public List<DynFurniture> makeDynFurnitureList(Player player, GameData gameData)
	{
		List<DynFurniture> list = new List<DynFurniture>();
		try
		{
			List<Furniture> furnitureList = gameData.getFurnitureList();
			foreach (Furniture item in furnitureList)
			{
				DynFurniture dynFurniture = new DynFurniture();
				dynFurniture.furnitureRefId = item.getFurnitureRefId();
				dynFurniture.isPlayerOwned = item.checkPlayerOwned().ToString();
				list.Add(dynFurniture);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "3001";
			return list;
		}
	}

	public List<DynDecoration> makeDynDecorationList(Player player, GameData gameData)
	{
		List<DynDecoration> list = new List<DynDecoration>();
		try
		{
			List<Decoration> decorationList = gameData.getDecorationList(isShopList: false, string.Empty);
			foreach (Decoration item in decorationList)
			{
				DynDecoration dynDecoration = new DynDecoration();
				dynDecoration.decorationRefId = item.getDecorationRefId();
				dynDecoration.isVisibleInShop = item.checkIsVisibleInShop().ToString();
				dynDecoration.isPlayerOwned = item.checkIsPlayerOwned().ToString();
				dynDecoration.isCurrentDisplay = item.checkIsCurrentDisplay().ToString();
				list.Add(dynDecoration);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "3002";
			return list;
		}
	}

	public List<DynItem> makeDynItemList(Player player, GameData gameData)
	{
		List<DynItem> list = new List<DynItem>();
		try
		{
			List<Item> itemList = gameData.getItemList(ownedOnly: false);
			foreach (Item item in itemList)
			{
				DynItem dynItem = new DynItem();
				dynItem.itemId = item.getItemId();
				dynItem.itemRefId = item.getItemRefId();
				dynItem.itemNum = item.getItemNum().ToString();
				dynItem.itemUsed = item.getItemUsed().ToString();
				dynItem.itemFromExplore = item.getItemFromExplore().ToString();
				dynItem.itemFromBuy = item.getItemFromBuy().ToString();
				list.Add(dynItem);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "3003";
			return list;
		}
	}

	public List<DynExploreItem> makeDynExploreItemList(Player player, GameData gameData)
	{
		List<DynExploreItem> list = new List<DynExploreItem>();
		try
		{
			List<Area> areaList = gameData.getAreaList(string.Empty);
			foreach (Area item in areaList)
			{
				foreach (ExploreItem value in item.getExploreItemList().Values)
				{
					DynExploreItem dynExploreItem = new DynExploreItem();
					dynExploreItem.areaExploreItemRefID = value.getAreaExploreItemRefID();
					dynExploreItem.areaRefID = value.getAreaRefID();
					dynExploreItem.itemRefID = value.getItemRefID();
					dynExploreItem.found = value.getFound().ToString();
					list.Add(dynExploreItem);
				}
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "2002";
			return list;
		}
	}

	public List<DynHero> makeDynHeroList(Player player, GameData gameData)
	{
		List<DynHero> list = new List<DynHero>();
		try
		{
			List<Hero> heroList = gameData.getHeroList(string.Empty);
			foreach (Hero item in heroList)
			{
				DynHero dynHero = new DynHero();
				dynHero.heroRefId = item.getHeroRefId();
				dynHero.expPoints = item.getExpPoints().ToString();
				dynHero.isUnlocked = item.checkUnlocked().ToString();
				dynHero.timesBought = item.getTimesBought().ToString();
				dynHero.isLoyaltyRewardGiven = item.checkLoyaltyRewardGiven().ToString();
				list.Add(dynHero);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "4001";
			return list;
		}
	}

	public List<DynArea> makeDynAreaList(Player player, GameData gameData)
	{
		List<DynArea> list = new List<DynArea>();
		try
		{
			List<Area> areaList = gameData.getAreaList(string.Empty);
			foreach (Area item in areaList)
			{
				DynArea dynArea = new DynArea();
				dynArea.areaRefId = item.getAreaRefId();
				dynArea.areaSmithRefId = CommonAPI.ConvertStringListToString(item.getAreaSmithRefID(string.Empty));
				dynArea.isUnlocked = item.checkIsUnlock().ToString();
				dynArea.timesExplored = item.checkTimesExplored().ToString();
				dynArea.timesBuyItems = item.checkTimesBuyItems().ToString();
				dynArea.timesSell = item.checkTimesSell().ToString();
				dynArea.timesTrain = item.checkTimesTrain().ToString();
				dynArea.timesVacation = item.checkTimesVacation().ToString();
				dynArea.currentEventRefId = item.getCurrentEventRefId().ToString();
				dynArea.startTime = item.getStartTime().ToString();
				dynArea.eventHeroRefId = item.getEventHeroRefId().ToString();
				dynArea.eventTypeRefId = item.getEventTypeRefId().ToString();
				list.Add(dynArea);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "5001";
			return list;
		}
	}

	public List<DynShopMonthlyStarch> makeDynShopMonthlyStarchList(Player player, GameData gameData)
	{
		List<DynShopMonthlyStarch> list = new List<DynShopMonthlyStarch>();
		try
		{
			List<ShopMonthlyStarch> shopMonthlyStarchList = player.getShopMonthlyStarchList();
			foreach (ShopMonthlyStarch item in shopMonthlyStarchList)
			{
				DynShopMonthlyStarch dynShopMonthlyStarch = new DynShopMonthlyStarch();
				dynShopMonthlyStarch.shopMonthlyStarchId = item.getShopMonthlyStarchId();
				dynShopMonthlyStarch.month = item.getMonth().ToString();
				dynShopMonthlyStarch.recordType = item.getRecordType().ToString();
				dynShopMonthlyStarch.recordName = item.getRecordName().ToString();
				dynShopMonthlyStarch.amount = item.getAmount().ToString();
				list.Add(dynShopMonthlyStarch);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "1003";
			return list;
		}
	}

	public List<DynHeroRequest> makeDynHeroRequestList(Player player, GameData gameData, bool completed)
	{
		List<DynHeroRequest> list = new List<DynHeroRequest>();
		List<HeroRequest> list2 = new List<HeroRequest>();
		try
		{
			list2 = ((!completed) ? player.getDisplayRequestList() : player.getCompletedRequestList());
			foreach (HeroRequest item in list2)
			{
				DynHeroRequest dynHeroRequest = new DynHeroRequest();
				dynHeroRequest.requestId = item.getRequestId();
				dynHeroRequest.requestHero = item.getRequestHero();
				dynHeroRequest.requestDuration = item.getRequestDuration().ToString();
				dynHeroRequest.requestRewardGold = item.getRequestRewardGold().ToString();
				dynHeroRequest.requestRewardLoyalty = item.getRequestRewardLoyalty().ToString();
				dynHeroRequest.requestRewardFame = item.getRequestRewardFame().ToString();
				dynHeroRequest.rewardItemList = CommonAPI.ConvertDictToString(item.getRequestRewardItemList());
				dynHeroRequest.requestName = item.getRequestName();
				dynHeroRequest.requestDesc = item.getRequestDesc();
				dynHeroRequest.weaponTypeRefIdReq = item.getRequestWeaponTypeRefIdReq();
				dynHeroRequest.weaponRefIdReq = item.getRequestWeaponRefIdReq();
				dynHeroRequest.atkReq = item.getRequestAtkReq().ToString();
				dynHeroRequest.spdReq = item.getRequestSpdReq().ToString();
				dynHeroRequest.accReq = item.getRequestAccReq().ToString();
				dynHeroRequest.magReq = item.getRequestMagReq().ToString();
				dynHeroRequest.enchantmentReq = item.getRequestEnchantmentReq();
				dynHeroRequest.requestStartTime = item.getRequestStartTime().ToString();
				dynHeroRequest.requestState = item.getRequestState().ToString();
				dynHeroRequest.deliveredProjectId = item.getDeliveredProjectId();
				list.Add(dynHeroRequest);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "4002";
			return list;
		}
	}

	public List<DynObjective> makeDynObjective(Player player, GameData gameData)
	{
		List<DynObjective> list = new List<DynObjective>();
		try
		{
			List<Objective> objectiveList = gameData.getObjectiveList(string.Empty, includeNotCounted: true);
			foreach (Objective item in objectiveList)
			{
				DynObjective dynObjective = new DynObjective();
				dynObjective.objectiveRefId = item.getObjectiveRefId();
				dynObjective.startTime = item.getStartTime().ToString();
				dynObjective.initCount = item.getInitCount().ToString();
				dynObjective.isStarted = item.checkObjectiveStarted().ToString();
				dynObjective.isEnded = item.checkObjectiveEnded().ToString();
				dynObjective.isSuccess = item.checkObjectiveSuccess().ToString();
				list.Add(dynObjective);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "1004";
			return list;
		}
	}

	public List<DynLegendaryHero> makeDynLegendaryHero(Player player, GameData gameData)
	{
		List<DynLegendaryHero> list = new List<DynLegendaryHero>();
		try
		{
			List<LegendaryHero> legendaryHeroList = gameData.getLegendaryHeroList(checkDLC: false, string.Empty);
			foreach (LegendaryHero item in legendaryHeroList)
			{
				DynLegendaryHero dynLegendaryHero = new DynLegendaryHero();
				dynLegendaryHero.legendaryHeroRefId = item.getLegendaryHeroRefId();
				dynLegendaryHero.requestState = item.getRequestState().ToString();
				list.Add(dynLegendaryHero);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "4003";
			return list;
		}
	}

	public List<DynProject> makeDynProjectList(Player player, GameData gameData)
	{
		List<DynProject> list = new List<DynProject>();
		try
		{
			List<Project> completedProjectList = player.getCompletedProjectList();
			List<Project> list2 = new List<Project>();
			list2.AddRange(completedProjectList);
			Project currentProject = player.getCurrentProject();
			if (currentProject.getProjectType() != ProjectType.ProjectTypeNothing)
			{
				list2.Add(player.getCurrentProject());
			}
			foreach (Project item in list2)
			{
				DynProject dynProject = new DynProject();
				dynProject.projectId = item.getProjectId();
				dynProject.projectRefId = string.Empty;
				dynProject.projectName = item.getProjectName(includePrefix: false);
				dynProject.projectDesc = item.getProjectDesc();
				dynProject.isPlayerNamed = item.getPlayerNamed().ToString();
				dynProject.projectWeapon = item.getProjectWeapon().getWeaponRefId();
				dynProject.projectContract = item.getProjectContract().getContractRefId();
				dynProject.projectType = item.getProjectType().ToString();
				dynProject.projectState = item.getProjectState().ToString();
				dynProject.projectSaleState = item.getProjectSaleState().ToString();
				dynProject.projectPhase = item.getProjectPhase().ToString();
				dynProject.timeLimit = item.getTimeLimit().ToString();
				dynProject.timeElapsed = item.getTimeElapsed().ToString();
				dynProject.endTime = item.getContractEndTime().ToString();
				dynProject.progressMax = item.getProgressMax().ToString();
				dynProject.progress = item.getProgress().ToString();
				dynProject.atk = item.getAtk().ToString();
				dynProject.spd = item.getSpd().ToString();
				dynProject.acc = item.getAcc().ToString();
				dynProject.mag = item.getMag().ToString();
				dynProject.atkReq = item.getAtkReq().ToString();
				dynProject.spdReq = item.getSpdReq().ToString();
				dynProject.accReq = item.getAccReq().ToString();
				dynProject.magReq = item.getMagReq().ToString();
				dynProject.enchantItem = item.getEnchantItem().getItemRefId();
				dynProject.numBoost = item.getNumBoost().ToString();
				dynProject.maxBoost = item.getMaxBoost().ToString();
				dynProject.prevBoost = CommonAPI.ConvertWeaponStatListToString(item.getPrevBoost());
				dynProject.buyerHeroRefId = item.getBuyer().getHeroRefId();
				dynProject.finalPrice = item.getFinalPrice().ToString();
				dynProject.finalScore = item.getFinalScore().ToString();
				dynProject.qualifyGoldenHammer = item.getQualifyGoldenHammer().ToString();
				dynProject.projectAchievementList = item.getProjectAchievementListString();
				dynProject.usedSmithList = CommonAPI.ConvertSmithListToString(item.getUsedSmithList());
				dynProject.forgeCost = item.getForgeCost().ToString();
				list.Add(dynProject);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "6001";
			return list;
		}
	}

	public List<DynOffer> makeDynProjectOfferList(Player player, GameData gameData)
	{
		List<DynOffer> list = new List<DynOffer>();
		try
		{
			List<Project> completedProjectList = player.getCompletedProjectList();
			List<Project> list2 = new List<Project>();
			list2.AddRange(completedProjectList);
			Project currentProject = player.getCurrentProject();
			if (currentProject.getProjectType() != ProjectType.ProjectTypeNothing)
			{
				list2.Add(player.getCurrentProject());
			}
			foreach (Project item in list2)
			{
				foreach (Offer offer in item.getOfferList())
				{
					DynOffer dynOffer = new DynOffer();
					dynOffer.offerId = offer.getOfferId();
					dynOffer.projectId = offer.getProjectId();
					dynOffer.heroRefId = offer.getHeroRefId();
					dynOffer.offerPrice = offer.getPrice().ToString();
					dynOffer.weaponScore = offer.getWeaponScore().ToString();
					dynOffer.expGrowth = offer.getExpGrowth().ToString();
					dynOffer.starchMult = offer.getStarchMult().ToString();
					dynOffer.expMult = offer.getExpMult().ToString();
					list.Add(dynOffer);
				}
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "6002";
			return list;
		}
	}

	public List<DynOffer> makeDynProjectSelectedOffer(Player player, GameData gameData)
	{
		List<DynOffer> list = new List<DynOffer>();
		try
		{
			List<Project> completedProjectList = player.getCompletedProjectList();
			List<Project> list2 = new List<Project>();
			list2.AddRange(completedProjectList);
			Project currentProject = player.getCurrentProject();
			if (currentProject.getProjectType() != ProjectType.ProjectTypeNothing)
			{
				list2.Add(player.getCurrentProject());
			}
			foreach (Project item in list2)
			{
				Offer selectedOffer = item.getSelectedOffer();
				DynOffer dynOffer = new DynOffer();
				dynOffer.offerId = selectedOffer.getOfferId();
				dynOffer.projectId = selectedOffer.getProjectId();
				dynOffer.heroRefId = selectedOffer.getHeroRefId();
				dynOffer.offerPrice = selectedOffer.getPrice().ToString();
				dynOffer.weaponScore = selectedOffer.getWeaponScore().ToString();
				dynOffer.expGrowth = selectedOffer.getExpGrowth().ToString();
				dynOffer.starchMult = selectedOffer.getStarchMult().ToString();
				dynOffer.expMult = selectedOffer.getExpMult().ToString();
				list.Add(dynOffer);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "6003";
			return list;
		}
	}

	public List<DynQuestNEW> makeDynQuestNEWList(Player player, GameData gameData)
	{
		List<DynQuestNEW> list = new List<DynQuestNEW>();
		try
		{
			List<QuestNEW> questNEWList = gameData.getQuestNEWList();
			foreach (QuestNEW item in questNEWList)
			{
				DynQuestNEW dynQuestNEW = new DynQuestNEW();
				dynQuestNEW.questRefId = item.getQuestRefId();
				dynQuestNEW.isUnlocked = item.getUnlocked().ToString();
				dynQuestNEW.isLocked = item.getLocked().ToString();
				dynQuestNEW.expiryDay = item.getExpiryDay().ToString();
				dynQuestNEW.isExpired = item.getExpired().ToString();
				dynQuestNEW.completeCount = item.getCompleteCount().ToString();
				dynQuestNEW.isOngoing = item.getOngoing().ToString();
				dynQuestNEW.atkReq = item.getAtkReq().ToString();
				dynQuestNEW.spdReq = item.getSpdReq().ToString();
				dynQuestNEW.accReq = item.getAccReq().ToString();
				dynQuestNEW.magReq = item.getMagReq().ToString();
				dynQuestNEW.milestoneNum = item.getMilestoneNum().ToString();
				dynQuestNEW.questTime = item.getQuestTime().ToString();
				dynQuestNEW.minQuestGold = item.getMinQuestGold().ToString();
				dynQuestNEW.challengeLastSet = item.getChallengeLastSet().ToString();
				dynQuestNEW.questGoldTotal = item.getQuestGoldTotal().ToString();
				List<int> questGoldDivide = item.getQuestGoldDivide();
				string text = string.Empty;
				foreach (int item2 in questGoldDivide)
				{
					if (text != string.Empty)
					{
						text += "@";
					}
					text += item2;
				}
				dynQuestNEW.questGoldDivide = text;
				dynQuestNEW.questProgress = item.getQuestProgress().ToString();
				dynQuestNEW.milestonePassed = item.getMilestonePassed().ToString();
				list.Add(dynQuestNEW);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "9999";
			return list;
		}
	}

	public List<DynSmith> makeDynSmithList(Player player, GameData gameData)
	{
		List<DynSmith> list = new List<DynSmith>();
		try
		{
			List<Smith> list2 = new List<Smith>();
			list2.AddRange(gameData.getSmithList(checkDLC: false, includeNormal: true, includeLegendary: true, string.Empty));
			list2.AddRange(gameData.getAllOutsourceSmithList(checkDlc: false, string.Empty));
			foreach (Smith item in list2)
			{
				DynSmith dynSmith = new DynSmith();
				dynSmith.smithId = item.getSmithId();
				dynSmith.smithRefId = item.getSmithRefId();
				dynSmith.smithExp = item.getSmithExp().ToString();
				dynSmith.smithJob = item.getSmithJob().getSmithJobRefId();
				dynSmith.isPlayerOwned = item.checkPlayerOwned().ToString();
				dynSmith.isUnlocked = item.checkUnlock().ToString();
				dynSmith.timesHired = item.getTimesHired().ToString();
				dynSmith.buffPower = item.getPowBuff().ToString();
				dynSmith.buffIntelligence = item.getIntBuff().ToString();
				dynSmith.buffTechnique = item.getTecBuff().ToString();
				dynSmith.buffLuck = item.getLucBuff().ToString();
				dynSmith.buffStamina = item.getStaBuff().ToString();
				dynSmith.smithAction = item.getSmithAction().getRefId();
				dynSmith.smithExploreState = item.getExploreState().ToString();
				dynSmith.smithActionDuration = item.getSmithActionDuration().ToString();
				dynSmith.smithActionElapsed = item.getSmithActionElapsed().ToString();
				dynSmith.smithEffectList = CommonAPI.ConvertStatEffectListToString(item.getSmithEffectList());
				dynSmith.smithEffectValueList = CommonAPI.ConvertFloatListToString(item.getSmithEffectValueList());
				dynSmith.smithEffectDurationList = CommonAPI.ConvertIntListToString(item.getSmithEffectDurationList());
				dynSmith.smithEffectDecoList = CommonAPI.ConvertStringListToString(item.getSmithEffectDecoList());
				dynSmith.smithStatusEffectList = CommonAPI.ConvertStringListToString(item.getSmithEffectStatusList());
				dynSmith.remainingMood = item.getRemainingMood().ToString();
				dynSmith.workProgress = item.getWorkProgress().ToString();
				dynSmith.assignedRole = item.getAssignedRole().ToString();
				dynSmith.currentStation = item.getCurrentStation().ToString();
				dynSmith.stationIndex = item.getCurrentStationIndex().ToString();
				dynSmith.exploreExp = item.getExploreExp().ToString();
				dynSmith.merchantExp = item.getMerchantExp().ToString();
				dynSmith.exploreArea = item.getExploreArea().getAreaRefId();
				dynSmith.actionStateList = CommonAPI.ConvertSmithExploreStateListToString(item.getActionStateList());
				dynSmith.actionDurationList = CommonAPI.ConvertIntListToString(item.getActionDurationList());
				dynSmith.actionProgressIndex = item.getActionProgressIndex().ToString();
				dynSmith.exploreTaskList = CommonAPI.ConvertStringListToString(item.getExploreTask());
				dynSmith.vacation = item.getVacation().getVacationRefId();
				dynSmith.training = item.getTraining().getSmithTrainingRefId();
				dynSmith.areaStatusList = CommonAPI.ConvertAreaStatusListToString(item.getAreaStatusList());
				dynSmith.currentSalary = item.getCurrentSmithSalary().ToString();
				list.Add(dynSmith);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "2003";
			return list;
		}
	}

	public List<DynSmithExperience> makeDynSmithExperienceList(Player player, GameData gameData)
	{
		List<DynSmithExperience> list = new List<DynSmithExperience>();
		try
		{
			List<Smith> smithList = gameData.getSmithList(checkDLC: false, includeNormal: true, includeLegendary: true, string.Empty);
			foreach (Smith item in smithList)
			{
				List<SmithExperience> experienceList = item.getExperienceList();
				foreach (SmithExperience item2 in experienceList)
				{
					DynSmithExperience dynSmithExperience = new DynSmithExperience();
					dynSmithExperience.smithExperienceRefId = item2.getSmithExperienceRefId();
					dynSmithExperience.smithRefId = item2.getSmithRefId();
					dynSmithExperience.level = item2.getSmithJobClassLevel().ToString();
					dynSmithExperience.tagGiven = item2.checkTagGiven().ToString();
					list.Add(dynSmithExperience);
				}
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "2004";
			return list;
		}
	}

	public List<DynSmithEffect> makeDynSmithEffectList(Player player, GameData gameData)
	{
		List<DynSmithEffect> list = new List<DynSmithEffect>();
		try
		{
			List<Smith> smithList = gameData.getSmithList(checkDLC: false, includeNormal: true, includeLegendary: true, string.Empty);
			foreach (Smith item in smithList)
			{
				DynSmithEffect dynSmithEffect = new DynSmithEffect();
				List<StatEffect> smithEffectList = item.getSmithEffectList();
				List<float> smithEffectValueList = item.getSmithEffectValueList();
				List<int> smithEffectDurationList = item.getSmithEffectDurationList();
				dynSmithEffect.smithRefId = item.getSmithRefId();
				for (int i = 0; i < smithEffectList.Count; i++)
				{
					dynSmithEffect.smithEffect = CommonAPI.convertStatEffectToString(smithEffectList[i]);
					dynSmithEffect.smithEffectValue = smithEffectValueList[i].ToString();
					dynSmithEffect.smithEffectDuration = smithEffectDurationList[i].ToString();
					list.Add(dynSmithEffect);
				}
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "2005";
			return list;
		}
	}

	public List<DynSmithTraining> makeDynSmithTrainingList(Player player, GameData gameData)
	{
		List<DynSmithTraining> list = new List<DynSmithTraining>();
		try
		{
			List<SmithTraining> smithTrainingList = gameData.getSmithTrainingList();
			foreach (SmithTraining item in smithTrainingList)
			{
				DynSmithTraining dynSmithTraining = new DynSmithTraining();
				dynSmithTraining.smithTrainingRefId = item.getSmithTrainingRefId();
				dynSmithTraining.useCount = item.getUseCount().ToString();
				list.Add(dynSmithTraining);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "5002";
			return list;
		}
	}

	public List<DynSpecialEvent> makeDynSpecialEventList(Player player, GameData gameData)
	{
		List<DynSpecialEvent> list = new List<DynSpecialEvent>();
		try
		{
			List<SpecialEvent> specialEventList = gameData.getSpecialEventList();
			foreach (SpecialEvent item in specialEventList)
			{
				DynSpecialEvent dynSpecialEvent = new DynSpecialEvent();
				dynSpecialEvent.specialEventRefId = item.getSpecialEventRefId();
				dynSpecialEvent.occurrenceCount = item.getOccurrenceCount().ToString();
				list.Add(dynSpecialEvent);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "1005";
			return list;
		}
	}

	public List<DynSeasonObjective> makeDynSeasonObjectiveList(Player player, GameData gameData)
	{
		List<DynSeasonObjective> list = new List<DynSeasonObjective>();
		try
		{
			List<SeasonObjective> seasonObjectiveList = gameData.getSeasonObjectiveList();
			foreach (SeasonObjective item in seasonObjectiveList)
			{
				DynSeasonObjective dynSeasonObjective = new DynSeasonObjective();
				dynSeasonObjective.objectiveRefId = item.getObjectiveRefId();
				dynSeasonObjective.points = item.getPoints().ToString();
				dynSeasonObjective.isCompleted = item.checkIsCompleted().ToString();
				list.Add(dynSeasonObjective);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "9999";
			return list;
		}
	}

	public List<DynTag> makeDynTagList(Player player, GameData gameData)
	{
		List<DynTag> list = new List<DynTag>();
		try
		{
			List<Tag> tagList = gameData.getTagList();
			foreach (Tag item in tagList)
			{
				DynTag dynTag = new DynTag();
				dynTag.tagRefId = item.getTagRefId();
				dynTag.seenTag = item.checkSeenTag().ToString();
				dynTag.tagUseCount = item.getUseCount().ToString();
				list.Add(dynTag);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "9999";
			return list;
		}
	}

	public List<DynQuestTag> makeDynQuestTagList(Player player, GameData gameData)
	{
		List<DynQuestTag> list = new List<DynQuestTag>();
		try
		{
			List<QuestNEW> questNEWList = gameData.getQuestNEWList();
			foreach (QuestNEW item in questNEWList)
			{
				List<QuestTag> questTagList = item.getQuestTagList();
				foreach (QuestTag item2 in questTagList)
				{
					DynQuestTag dynQuestTag = new DynQuestTag();
					dynQuestTag.questTagRefId = item2.getQuestTagRefId();
					dynQuestTag.questRefId = item2.getQuestRefId();
					dynQuestTag.isUnlocked = item2.checkTagUnlocked().ToString();
					list.Add(dynQuestTag);
				}
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "9999";
			return list;
		}
	}

	public List<DynSmithTag> makeDynSmithTagList(Player player, GameData gameData)
	{
		List<DynSmithTag> list = new List<DynSmithTag>();
		try
		{
			List<Smith> smithList = gameData.getSmithList(checkDLC: false, includeNormal: true, includeLegendary: true, string.Empty);
			foreach (Smith item in smithList)
			{
				List<SmithTag> smithTagList = item.getSmithTagList();
				foreach (SmithTag item2 in smithTagList)
				{
					DynSmithTag dynSmithTag = new DynSmithTag();
					dynSmithTag.smithTagRefId = item2.getSmithTagId();
					dynSmithTag.smithRefId = item2.getSmithRefId();
					dynSmithTag.useCount = item2.getUseCount().ToString();
					dynSmithTag.displayOrder = item2.getDisplayOrder().ToString();
					list.Add(dynSmithTag);
				}
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "9999";
			return list;
		}
	}

	public List<DynProjectTag> makeDynProjectTagList(Player player, GameData gameData)
	{
		return new List<DynProjectTag>();
	}

	public List<DynWeapon> makeDynWeaponList(Player player, GameData gameData)
	{
		List<DynWeapon> list = new List<DynWeapon>();
		try
		{
			List<Weapon> weaponList = gameData.getWeaponList(checkDLC: false, string.Empty);
			foreach (Weapon item in weaponList)
			{
				DynWeapon dynWeapon = new DynWeapon();
				dynWeapon.weaponRefId = item.getWeaponRefId();
				dynWeapon.isUnlocked = item.getWeaponUnlocked().ToString();
				dynWeapon.timesUsed = item.getTimesUsed().ToString();
				list.Add(dynWeapon);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "7001";
			return list;
		}
	}

	public List<DynWeaponType> makeDynWeaponTypeList(Player player, GameData gameData)
	{
		List<DynWeaponType> list = new List<DynWeaponType>();
		try
		{
			List<WeaponType> weaponTypeList = gameData.getWeaponTypeList();
			foreach (WeaponType item in weaponTypeList)
			{
				DynWeaponType dynWeaponType = new DynWeaponType();
				dynWeaponType.weaponTypeRefId = item.getWeaponTypeRefId();
				dynWeaponType.isUnlocked = item.checkUnlocked().ToString();
				list.Add(dynWeaponType);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "7002";
			return list;
		}
	}

	public List<DynWeekendActivity> makeDynWeekendActivityList(Player player, GameData gameData)
	{
		List<DynWeekendActivity> list = new List<DynWeekendActivity>();
		try
		{
			List<WeekendActivity> weekendActivityList = gameData.getWeekendActivityList();
			foreach (WeekendActivity item in weekendActivityList)
			{
				DynWeekendActivity dynWeekendActivity = new DynWeekendActivity();
				dynWeekendActivity.weekendActivityRefId = item.getWeekendActivityRefId();
				dynWeekendActivity.doneCount = item.getDoneCount().ToString();
				list.Add(dynWeekendActivity);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "9999";
			return list;
		}
	}

	public List<DynStation> makeDynStation(Player player, GameData gameData)
	{
		List<DynStation> list = new List<DynStation>();
		try
		{
			List<Station> allStation = gameData.getAllStation();
			foreach (Station item in allStation)
			{
				DynStation dynStation = new DynStation();
				dynStation.refStationID = item.getRefStationID();
				dynStation.smithList = CommonAPI.ConvertSmithListToString(item.getSmithList());
				list.Add(dynStation);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "8001";
			return list;
		}
	}

	public List<DynWhetsapp> makeDynWhetsapp(Player player, GameData gameData)
	{
		List<DynWhetsapp> list = new List<DynWhetsapp>();
		try
		{
			List<Whetsapp> whetsappList = gameData.getWhetsappList();
			foreach (Whetsapp item in whetsappList)
			{
				DynWhetsapp dynWhetsapp = new DynWhetsapp();
				dynWhetsapp.whetsappId = item.getWhetsappId();
				dynWhetsapp.senderName = item.getSenderName();
				dynWhetsapp.messageTextRefId = item.getMessageText();
				dynWhetsapp.imagePath = item.getImage();
				dynWhetsapp.time = item.getTime().ToString();
				dynWhetsapp.filterType = item.getFilterType().ToString();
				dynWhetsapp.isRead = item.checkRead().ToString();
				list.Add(dynWhetsapp);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "1006";
			return list;
		}
	}

	public List<DynAchievement> makeDynAchievement(Player player, GameData gameData)
	{
		List<DynAchievement> list = new List<DynAchievement>();
		try
		{
			List<Achievement> achievementList = gameData.getAchievementList();
			foreach (Achievement item in achievementList)
			{
				DynAchievement dynAchievement = new DynAchievement();
				dynAchievement.achievementRefID = item.getAchivementRefID();
				dynAchievement.achieved = item.getAchieved().ToString();
				list.Add(dynAchievement);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "1007";
			return list;
		}
	}

	public List<DynScenarioVariable> makeDynScenarioVariableList(Player player, GameData gameData)
	{
		List<DynScenarioVariable> list = new List<DynScenarioVariable>();
		try
		{
			List<ScenarioVariable> scenarioVariableList = gameData.getScenarioVariableList();
			foreach (ScenarioVariable item in scenarioVariableList)
			{
				DynScenarioVariable dynScenarioVariable = new DynScenarioVariable();
				dynScenarioVariable.scenarioVariableRefId = item.getScenarioVariableRefId();
				dynScenarioVariable.variableValue = item.getVariableValueString();
				list.Add(dynScenarioVariable);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "1008";
			return list;
		}
	}

	public List<DynContract> makeDynContract(Player player, GameData gameData)
	{
		List<DynContract> list = new List<DynContract>();
		try
		{
			List<Contract> fullContractList = gameData.getFullContractList();
			foreach (Contract item in fullContractList)
			{
				DynContract dynContract = new DynContract();
				dynContract.contractRefId = item.getContractRefId();
				dynContract.timesCompleted = item.getTimesCompleted().ToString();
				dynContract.timesStarted = item.getTimesStarted().ToString();
				list.Add(dynContract);
			}
			return list;
		}
		catch (Exception)
		{
			errorCode += "6004";
			return list;
		}
	}

	public string getDynDataFromFile(string saveName)
	{
		setUpObjects();
		string empty = string.Empty;
		errorCode = string.Empty;
		try
		{
			bool flag = jsonFileController.checkContent("WS_" + saveName + ".txt");
			bool flag2 = jsonFileController.checkMyDocContent("WS_" + saveName + ".txt");
			string empty2 = string.Empty;
			if (flag || flag2)
			{
				empty2 = jsonFileController.readContent("WS_" + saveName + ".txt");
			}
			else
			{
				empty2 = jsonFileController.readContentPlayerPrefs(saveName);
				CommonAPI.debug("atext: " + empty2);
				jsonFileController.removeContentFromPlayerPrefs(saveName);
				if (empty2 != null && empty2 != string.Empty)
				{
					jsonFileController.saveContent("WS_" + saveName + ".txt", empty2);
				}
			}
			empty2 = CommonAPI.Decrypt(empty2, "e1n6c3dy4n9k2ey5");
			CommonAPI.debug("atext encrypted: " + empty2);
			CommonAPI.debug(empty2.Length + " " + empty2);
			if (empty2.Length > 0)
			{
				GameObject.Find("ViewController").GetComponent<ViewController>().showLoadRef();
				ServerDynResponse serverDynResponse = JsonMapper.ToObject<ServerDynResponse>(empty2);
				ServerDynValue value = serverDynResponse.value;
				StartCoroutine(processDynamicObjective(value.DynObjective));
				StartCoroutine(processDynamicPlayer(value.DynPlayer));
				Player player = game.getPlayer();
				StartCoroutine(processDynamicCode(value.DynCode));
				StartCoroutine(processDynamicWeaponType(value.DynWeaponType));
				StartCoroutine(processDynamicWeapon(value.DynWeapon));
				StartCoroutine(processDynamicHero(value.DynHero));
				StartCoroutine(processDynamicArea(value.DynArea));
				StartCoroutine(processDynShopMonthlyStarch(value.DynShopMonthlyStarch));
				StartCoroutine(processDynamicHeroRequest(value.DynDisplayHeroRequest, completed: false));
				StartCoroutine(processDynamicHeroRequest(value.DynCompletedHeroRequest, completed: true));
				if (value.DynContract != null)
				{
					StartCoroutine(processDynamicContract(value.DynContract));
				}
				else
				{
					loadEmptyDynamicContract();
				}
				StartCoroutine(processDynamicLegendaryHero(value.DynLegendaryhero));
				StartCoroutine(processDynamicActivityList(value.DynActivity));
				StartCoroutine(processDynamicFurniture(value.DynFurniture));
				StartCoroutine(processDynamicDecoration(value.DynDecoration));
				StartCoroutine(processDynamicItem(value.DynItem));
				StartCoroutine(processDynamicExploreItem(value.DynExploreItem));
				StartCoroutine(processDynamicSpecialEvent(value.DynSpecialEvent));
				StartCoroutine(processDynamicSeasonObjective(value.DynSeasonObjective));
				StartCoroutine(processDynamicTag(value.DynTag));
				StartCoroutine(processDynamicWeekendActivity(value.DynWeekendActivity));
				StartCoroutine(processDynamicSmith(value.DynSmith, value.DynSmithEffect, value.DynSmithExperience, value.DynSmithTag));
				StartCoroutine(processDynamicSmithTraining(value.DynSmithTraining));
				StartCoroutine(processDynamicQuestNEW(value.DynQuestNEW, value.DynQuestTag));
				StartCoroutine(processDynamicProject(value.DynProject, value.DynProjectTag));
				StartCoroutine(processDynamicProjectOfferList(value.DynProjectOfferList, selected: false));
				StartCoroutine(processDynamicProjectOfferList(value.DynProjectSelectedOffer, selected: true));
				StartCoroutine(processDynamicWhetsapp(value.DynWhetsapp));
				if (value.DynAchievement != null)
				{
					StartCoroutine(processDynamicAchievement(value.DynAchievement));
				}
				if (value.DynScenarioVariable != null)
				{
					StartCoroutine(processDynamicScenarioVariable(value.DynScenarioVariable));
				}
				CommonAPI.debug("dynamic data loaded");
			}
			else
			{
				game.setPlayer(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, 0, 0, 0, 0, 0, 0);
			}
			empty = "SUCCESS";
		}
		catch (Exception ex)
		{
			empty = "0000";
			CommonAPI.debug(ex.ToString());
		}
		if (empty != "SUCCESS")
		{
			return empty + errorCode;
		}
		return empty + "_" + errorCode;
	}

	public IEnumerator processDynamicPlayer(DynPlayer aPlayer)
	{
		coroutineCheckStart++;
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		string playerId = aPlayer.playerId;
		string gameScenario = "10001";
		if (aPlayer.gameScenario != null)
		{
			gameScenario = aPlayer.gameScenario;
		}
		string playerName = aPlayer.playerName;
		string shopName = aPlayer.shopName;
		string shopLevelRefId = aPlayer.shopLevelRefId;
		int areaRegion = CommonAPI.parseInt(aPlayer.areaRegion);
		int playerFame = CommonAPI.parseInt(aPlayer.playerFame);
		int playerTickets = CommonAPI.parseInt(aPlayer.playerTickets);
		int playerUsedTickets = CommonAPI.parseInt(aPlayer.playerUsedTickets);
		int prevRegionFame = CommonAPI.parseInt(aPlayer.prevRegionFame);
		int prevRegionTickets = CommonAPI.parseInt(aPlayer.prevRegionTickets);
		bool hasAvatar = bool.Parse(aPlayer.hasAvatar);
		string avatarRefId = aPlayer.avatarRefId;
		Avatar avatar = gameData.getAvatarByRefId(avatarRefId);
		int gold = CommonAPI.parseInt(aPlayer.playerGold);
		int good = CommonAPI.parseInt(aPlayer.playerGood);
		int evil = CommonAPI.parseInt(aPlayer.playerEvil);
		int usedEmblems = CommonAPI.parseInt(aPlayer.usedEmblems);
		bool tofuKnown = bool.Parse(aPlayer.tofuKnown);
		bool randomDog = bool.Parse(aPlayer.randomDog);
		bool hasDog = bool.Parse(aPlayer.hasDog);
		string dogBedRefID = aPlayer.dogBedRefID;
		string dogName = aPlayer.dogName;
		int dogLove = CommonAPI.parseInt(aPlayer.dogLove);
		float dogEnergy = CommonAPI.parseFloat(aPlayer.dogEnergy);
		int dogLastFed = CommonAPI.parseInt(aPlayer.dogLastFed);
		int dogActivityCountMonth = CommonAPI.parseInt(aPlayer.dogActivityCountMonth);
		int dogActivityCountTotal = CommonAPI.parseInt(aPlayer.dogActivityCountTotal);
		bool loanUsed = bool.Parse(aPlayer.loanUsed);
		int salaryChancesUsed = CommonAPI.parseInt(aPlayer.salaryChancesUsed);
		int lastPaidMonth = CommonAPI.parseInt(aPlayer.lastPaidMonth);
		string currentObjective = aPlayer.currentObjective;
		Objective objective = gameData.getObjectiveByRefId(currentObjective);
		string completedObjectiveList = aPlayer.completedObjectiveList;
		string currentProjectId = aPlayer.currentProjectId;
		string lastDoneQuestRefId = aPlayer.lastDoneQuest;
		string displayProjectList = aPlayer.displayProjectList;
		string playerContractList = aPlayer.playerContractList;
		string recruitRefIdList = aPlayer.recruitRefIdList;
		int lastDailyAction = CommonAPI.parseInt(aPlayer.lastDailyAction);
		long playerTimeLong = long.Parse(aPlayer.playerTimeLong);
		string actionList = aPlayer.actionList;
		string timerList = aPlayer.timerList;
		string weatherRefId = aPlayer.weatherRefId;
		Weather weather = game.getGameData().getWeatherByRefId(weatherRefId);
		ForgeSeasonalEffect forgeEffect = CommonAPI.convertDataStringToForgeSeasonalEffect(aPlayer.forgeEffect);
		List<ProjectAchievement> nextGoldenHammerAwardList = new List<ProjectAchievement>();
		if (aPlayer.nextGoldenHammerAwardList != string.Empty)
		{
			string[] array = aPlayer.nextGoldenHammerAwardList.Split('@');
			foreach (string typeString in array)
			{
				nextGoldenHammerAwardList.Add(CommonAPI.convertStringToProjectAchievement(typeString));
			}
		}
		int nextGoldenHammerYear = CommonAPI.parseInt(aPlayer.nextGoldenHammerYear);
		Dictionary<int, string> pastGoldenHammerAwardList = new Dictionary<int, string>();
		if (aPlayer.pastGoldenHammerAwardList != null && aPlayer.pastGoldenHammerAwardList != string.Empty)
		{
			string[] array2 = aPlayer.pastGoldenHammerAwardList.Split('_');
			foreach (string text in array2)
			{
				string[] array3 = text.Split('%');
				pastGoldenHammerAwardList.Add(CommonAPI.parseInt(array3[0]), array3[1]);
			}
		}
		int showAwardsFromYear = -1;
		if (aPlayer.showAwardsFromYear != null)
		{
			showAwardsFromYear = CommonAPI.parseInt(aPlayer.showAwardsFromYear);
		}
		bool isNamed = bool.Parse(aPlayer.isNamed);
		string email = aPlayer.email;
		int finalScore = CommonAPI.parseInt(aPlayer.finalScore);
		SmithStation playerStation = CommonAPI.convertStringToSmithStation(aPlayer.playerStation);
		SmithStation dogStation = CommonAPI.convertStringToSmithStation(aPlayer.dogStation);
		int designStationLv = CommonAPI.parseInt(aPlayer.designStationLv);
		int craftStationLv = CommonAPI.parseInt(aPlayer.craftStationLv);
		int polishStationLv = CommonAPI.parseInt(aPlayer.polishStationLv);
		int enchantStationLv = CommonAPI.parseInt(aPlayer.enchantStationLv);
		bool enchantLocked = bool.Parse(aPlayer.enchantLocked);
		Weapon researchWeapon = null;
		string researchWeaponRefID = aPlayer.researchWeaponRefID;
		if (researchWeaponRefID != string.Empty)
		{
			researchWeapon = gameData.getWeaponByRefId(researchWeaponRefID);
		}
		Smith researchSmith = null;
		string researchSmithRefID = aPlayer.researchSmithRefID;
		if (researchSmithRefID != string.Empty)
		{
			researchSmith = gameData.getSmithByRefId(researchSmithRefID);
		}
		int researchLength = CommonAPI.parseInt(aPlayer.researchLength);
		int researchTimeLeft = CommonAPI.parseInt(aPlayer.researchTimeLeft);
		int researchCount = CommonAPI.parseInt(aPlayer.researchCount);
		long lastEventDate = -1L;
		if (aPlayer.lastEventDate != null)
		{
			lastEventDate = long.Parse(aPlayer.lastEventDate);
		}
		int lastAreaEvent = CommonAPI.parseInt(aPlayer.lastAreaEvent);
		int lastCheckRequest = CommonAPI.parseInt(aPlayer.lastCheckRequest);
		string displayLegendaryList = aPlayer.displayLegendaryList;
		string[] displayLegendSplit = displayLegendaryList.Split('@');
		string completedLegendaryList = aPlayer.completedLegendaryList;
		string[] completedLegendSplit = completedLegendaryList.Split('@');
		int requestCount = CommonAPI.parseInt(aPlayer.requestCount);
		bool skipTutorials = false;
		if (aPlayer.skipTutorials != null)
		{
			skipTutorials = bool.Parse(aPlayer.skipTutorials);
		}
		TutorialState tutorialState = CommonAPI.convertDataStringToTutorialState(aPlayer.tutorialState);
		int tutorialIndex = CommonAPI.parseInt(aPlayer.tutorialIndex);
		int completedTutorialIndex = CommonAPI.parseInt(aPlayer.completedTutorialIndex);
		int totalHeroExpGain = CommonAPI.parseInt(aPlayer.totalHeroExpGain);
		game.setPlayer(playerId, gameScenario, playerName, shopName, shopLevelRefId, areaRegion, playerFame, playerTickets, playerUsedTickets, gold, good, evil, usedEmblems);
		player = game.getPlayer();
		player.setHasAvatar(hasAvatar);
		player.setRandomDog(randomDog);
		player.chooseAvatar(avatarRefId, avatar.getAvatarName(), avatar.getAvatarDesc(), avatar.getAvatarImage());
		player.setShopLevel(gameData.getShopLevel(shopLevelRefId));
		player.setTofuKnown(tofuKnown);
		player.setDog(hasDog, dogName, dogLove, dogEnergy, dogActivityCountMonth, dogActivityCountTotal);
		player.setDogBedRefID(dogBedRefID);
		player.setDogLastFed(dogLastFed);
		player.setUseLoan(loanUsed);
		player.setSalaryChancesUsed(salaryChancesUsed);
		player.setLastPaidMonth(lastPaidMonth);
		player.setCurrentObjectiveString(currentObjective);
		player.setCurrentObjective(objective);
		player.setCurrentProjectId(currentProjectId);
		player.setLastDoneQuestRefId(lastDoneQuestRefId);
		player.setDisplayProjectListString(displayProjectList);
		player.setContractListString(playerContractList);
		player.setRecruitListIdString(recruitRefIdList);
		player.setLastDailyAction(lastDailyAction);
		player.setTimeLong(playerTimeLong);
		player.setTimedActionLists(actionList, timerList);
		player.setWeather(weather);
		player.setForgeEffect(forgeEffect);
		player.setNextGoldenHammerAwardList(nextGoldenHammerAwardList);
		player.setNextGoldenHammerYear(nextGoldenHammerYear);
		player.setPastGoldenHammerAwards(pastGoldenHammerAwardList);
		player.setShowAwardsFromYear(showAwardsFromYear);
		if (player.getShowAwardsFromYear() == -1)
		{
			player.setShowAwardsFromYear(player.getNextGoldenHammerYear());
		}
		if (isNamed)
		{
			player.setNamed();
		}
		player.setEmail(email);
		player.setFinalScore(finalScore);
		player.setPlayerStation(playerStation);
		player.setDogStation(dogStation);
		player.setDesignStationLv(designStationLv);
		player.setCraftStationLv(craftStationLv);
		player.setPolishStationLv(polishStationLv);
		player.setEnchantStationLv(enchantStationLv);
		player.setEnchantLocked(enchantLocked);
		player.setCurrentResearchWeapon(researchWeapon);
		player.setCurrentResearchSmith(researchSmith);
		player.setResearchLength(researchLength);
		player.setResearchTimeLeft(researchTimeLeft);
		player.setResearchCount(researchCount);
		player.setLastEventDate(lastEventDate);
		player.setLastAreaEvent(lastAreaEvent);
		player.setTotalRequestCount(requestCount);
		player.setLastCheckRequest(lastCheckRequest);
		player.setPrevRegionFame(prevRegionFame);
		player.setPrevRegionTickets(prevRegionTickets);
		int i = 0;
		string[] array4 = displayLegendSplit;
		foreach (string refId in array4)
		{
			LegendaryHero aHero = gameData.getLegendaryHeroByHeroRefId(refId);
			if (aHero.getLegendaryHeroRefId() != string.Empty)
			{
				player.addDisplayLegendaryHero(aHero);
			}
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		int j = 0;
		string[] array5 = completedLegendSplit;
		foreach (string refId2 in array5)
		{
			LegendaryHero aHero2 = gameData.getLegendaryHeroByHeroRefId(refId2);
			if (aHero2.getLegendaryHeroRefId() != string.Empty)
			{
				player.addCompletedLegendaryHero(aHero2);
			}
			j++;
			if (j % 20 == 0 && j != 0)
			{
				yield return null;
			}
		}
		player.setSkipTutorials(skipTutorials);
		player.setTutorialState(tutorialState);
		player.setTutorialIndex(tutorialIndex);
		player.setCompletedTutorialIndex(completedTutorialIndex);
		player.setTotalHeroExpGain(totalHeroExpGain);
		CommonAPI.debug("processDynamicPlayer COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicContract(List<DynContract> aDynContractList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		List<Contract> playerContractList = new List<Contract>();
		foreach (DynContract aDynContract in aDynContractList)
		{
			foreach (Contract fullContract in gameData.getFullContractList())
			{
				if (aDynContract.contractRefId == fullContract.getContractRefId())
				{
					int timesStarted = CommonAPI.parseInt(aDynContract.timesStarted);
					int timesCompleted = CommonAPI.parseInt(aDynContract.timesCompleted);
					fullContract.setTimesStarted(timesStarted);
					fullContract.setTimesCompleted(timesCompleted);
					if (player.checkContractInStringList(aDynContract.contractRefId))
					{
						playerContractList.Add(fullContract);
					}
					break;
				}
			}
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		player.setContractList(playerContractList);
		CommonAPI.debug("processDynamicContract COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public void loadEmptyDynamicContract()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		try
		{
			List<Contract> list = new List<Contract>();
			foreach (Contract fullContract in gameData.getFullContractList())
			{
				fullContract.setTimesStarted(0);
				fullContract.setTimesCompleted(0);
				if (player.checkContractInStringList(fullContract.getContractRefId()))
				{
					list.Add(fullContract);
				}
			}
			player.setContractList(list);
		}
		catch (Exception)
		{
			errorCode += "6004";
		}
	}

	public IEnumerator processDynamicActivityList(List<DynActivity> aDynActivityList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		Dictionary<string, Activity> activityList = new Dictionary<string, Activity>();
		foreach (DynActivity aDynActivity in aDynActivityList)
		{
			string activityID = aDynActivity.activityID;
			ActivityType activityType = CommonAPI.convertDataStringToActivityType(aDynActivity.activityType);
			ActivityState activityState = CommonAPI.convertDataStringToActivityState(aDynActivity.activityState);
			string areaRefID = aDynActivity.areaRefID;
			string smithRefID = aDynActivity.smithRefID;
			int travelTime = CommonAPI.parseInt(aDynActivity.travelTime);
			int progress = CommonAPI.parseInt(aDynActivity.progress);
			Activity loadedActivity = new Activity(activityID, activityType, areaRefID, smithRefID, travelTime);
			loadedActivity.setActivityState(activityState);
			loadedActivity.setProgress(progress);
			activityList.Add(activityID, loadedActivity);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		player.setActivityList(activityList);
		CommonAPI.debug("processDynamicActivityList COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicFurniture(List<DynFurniture> aDynFurnitureList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		List<Furniture> unlockedList = new List<Furniture>();
		foreach (DynFurniture aDynFurniture in aDynFurnitureList)
		{
			foreach (Furniture furniture in gameData.getFurnitureList())
			{
				if (aDynFurniture.furnitureRefId == furniture.getFurnitureRefId())
				{
					bool flag = bool.Parse(aDynFurniture.isPlayerOwned);
					furniture.setPlayerOwned(flag);
					if (flag)
					{
						unlockedList.Add(furniture);
					}
					break;
				}
			}
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		player.setShopFurnitureList(unlockedList);
		CommonAPI.debug("processDynamicFurniture COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicDecoration(List<DynDecoration> aDynDecorationList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		List<Decoration> ownedDecoration = new List<Decoration>();
		Dictionary<string, Decoration> displayDecoration = new Dictionary<string, Decoration>();
		foreach (DynDecoration aDynDecoration in aDynDecorationList)
		{
			Decoration aDecoration = gameData.getDecorationByRefId(aDynDecoration.decorationRefId);
			bool isVisibleInShop = bool.Parse(aDynDecoration.isVisibleInShop);
			bool isPlayerOwned = bool.Parse(aDynDecoration.isPlayerOwned);
			bool isCurrentDisplay = bool.Parse(aDynDecoration.isCurrentDisplay);
			aDecoration.setIsVisibleInShop(isVisibleInShop);
			aDecoration.setIsPlayerOwned(isPlayerOwned);
			aDecoration.setIsCurrentDisplay(isCurrentDisplay);
			if (isPlayerOwned)
			{
				ownedDecoration.Add(aDecoration);
			}
			if (isCurrentDisplay)
			{
				displayDecoration.Add(aDecoration.getDecorationType(), aDecoration);
			}
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		player.setOwnedDecorationList(ownedDecoration);
		player.setDisplayDecorationList(displayDecoration);
		CommonAPI.debug("processDynamicDecoration COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicItem(List<DynItem> aDynItemList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		foreach (DynItem aDynItem in aDynItemList)
		{
			foreach (Item item in gameData.getItemList(ownedOnly: false))
			{
				if (aDynItem.itemRefId == item.getItemRefId())
				{
					int itemNum = CommonAPI.parseInt(aDynItem.itemNum);
					int itemUsed = CommonAPI.parseInt(aDynItem.itemUsed);
					int itemFromExplore = CommonAPI.parseInt(aDynItem.itemFromExplore);
					int itemFromBuy = CommonAPI.parseInt(aDynItem.itemFromBuy);
					item.setItemNum(itemNum);
					item.setItemUsed(itemUsed);
					item.setItemFromExplore(itemFromExplore);
					item.setItemFromBuy(itemFromBuy);
					break;
				}
			}
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processDynamicItem COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicExploreItem(List<DynExploreItem> aDynExploreItemList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		foreach (DynExploreItem aDynExploreItem in aDynExploreItemList)
		{
			bool found = bool.Parse(aDynExploreItem.found);
			Area area = gameData.getAreaByRefID(aDynExploreItem.areaRefID);
			area.getExploreItemList()[aDynExploreItem.itemRefID].setFound(found);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processDynamicExploreItem COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicHero(List<DynHero> aDynHeroList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		List<Hero> unlockedList = new List<Hero>();
		int i = 0;
		foreach (DynHero aDynHero in aDynHeroList)
		{
			foreach (Hero hero in gameData.getHeroList(string.Empty))
			{
				if (aDynHero.heroRefId == hero.getHeroRefId())
				{
					int expPoints = CommonAPI.parseInt(aDynHero.expPoints);
					bool flag = bool.Parse(aDynHero.isUnlocked);
					int timesBought = CommonAPI.parseInt(aDynHero.timesBought);
					bool loyaltyRewardGiven = bool.Parse(aDynHero.isLoyaltyRewardGiven);
					hero.setExpPoints(expPoints);
					hero.setUnlocked(flag);
					hero.setTimesBought(timesBought);
					hero.setLoyaltyRewardGiven(loyaltyRewardGiven);
					if (flag)
					{
						unlockedList.Add(hero);
					}
					break;
				}
			}
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processDynamicHero COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicArea(List<DynArea> aDynAreaList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		foreach (DynArea aDynArea in aDynAreaList)
		{
			Area aArea = gameData.getAreaByRefID(aDynArea.areaRefId);
			List<string> areaSmith = CommonAPI.ConvertStringToStringList(aDynArea.areaSmithRefId);
			bool isUnlocked = bool.Parse(aDynArea.isUnlocked);
			int timesExplored = CommonAPI.parseInt(aDynArea.timesExplored);
			int timesBuyItems = CommonAPI.parseInt(aDynArea.timesBuyItems);
			int timesSell = CommonAPI.parseInt(aDynArea.timesSell);
			int timesTrain = CommonAPI.parseInt(aDynArea.timesTrain);
			int timesVacation = CommonAPI.parseInt(aDynArea.timesVacation);
			string currentEventRefId = aDynArea.currentEventRefId;
			long startTime = long.Parse(aDynArea.startTime);
			string eventHeroRefId = aDynArea.eventHeroRefId;
			string eventTypeRefId = aDynArea.eventTypeRefId;
			aArea.setAreaSmtihRefIDList(areaSmith);
			aArea.setUnlock(isUnlocked);
			aArea.setTimesExplored(timesExplored);
			aArea.setTimesBuyItems(timesBuyItems);
			aArea.setTimesSell(timesSell);
			aArea.setTimesTrain(timesTrain);
			aArea.setTimesVacation(timesVacation);
			AreaEvent currentEvent = gameData.getAreaEventByRefId(currentEventRefId);
			aArea.setCurrentEvent(currentEvent);
			aArea.setStartTime(startTime);
			aArea.setEventHeroRefId(eventHeroRefId);
			aArea.setEventTypeRefId(eventTypeRefId);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processDynamicArea COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynShopMonthlyStarch(List<DynShopMonthlyStarch> aShopMonthlyStarchList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		foreach (DynShopMonthlyStarch aDynReport in aShopMonthlyStarchList)
		{
			string shopMonthlyStarchId = aDynReport.shopMonthlyStarchId;
			int month = CommonAPI.parseInt(aDynReport.month);
			RecordType recordType = CommonAPI.convertDataStringToShopStarchRecordType(aDynReport.recordType);
			string recordName = aDynReport.recordName;
			int amount = CommonAPI.parseInt(aDynReport.amount);
			player.addShopMonthlyStarch(shopMonthlyStarchId, month, recordType, recordName, amount);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processDynShopMonthlyStarch COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicHeroRequest(List<DynHeroRequest> aDynHeroRequest, bool completed)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		List<HeroRequest> heroReqList = new List<HeroRequest>();
		foreach (DynHeroRequest aDynReq in aDynHeroRequest)
		{
			string requestId = aDynReq.requestId;
			string requestHero = aDynReq.requestHero;
			int requestDuration = CommonAPI.parseInt(aDynReq.requestDuration);
			int requestRewardGold = CommonAPI.parseInt(aDynReq.requestRewardGold);
			int requestRewardLoyalty = CommonAPI.parseInt(aDynReq.requestRewardLoyalty);
			int requestRewardFame = CommonAPI.parseInt(aDynReq.requestRewardFame);
			Dictionary<string, int> rewardItemList = CommonAPI.ConvertStringToDict(aDynReq.rewardItemList);
			string requestName = aDynReq.requestName;
			string requestDesc = aDynReq.requestDesc;
			string weaponTypeRefIdReq = aDynReq.weaponTypeRefIdReq;
			string weaponRefIdReq = aDynReq.weaponRefIdReq;
			int atkReq = CommonAPI.parseInt(aDynReq.atkReq);
			int spdReq = CommonAPI.parseInt(aDynReq.spdReq);
			int accReq = CommonAPI.parseInt(aDynReq.accReq);
			int magReq = CommonAPI.parseInt(aDynReq.magReq);
			string enchantmentReq = aDynReq.enchantmentReq;
			long requestStartTime = long.Parse(aDynReq.requestStartTime);
			RequestState requestState = CommonAPI.convertDataStringToRequestState(aDynReq.requestState);
			string deliveredProjectId = aDynReq.deliveredProjectId;
			CommonAPI.debug("requestHeroId: " + requestHero);
			HeroRequest aHeroReq = new HeroRequest(requestId, requestHero, requestDuration, requestRewardGold, requestRewardLoyalty, requestRewardFame, rewardItemList, requestName, requestDesc, weaponTypeRefIdReq, weaponRefIdReq, atkReq, spdReq, accReq, magReq, enchantmentReq, requestStartTime);
			aHeroReq.setRequestState(requestState);
			aHeroReq.setDeliveredProjectId(deliveredProjectId);
			heroReqList.Add(aHeroReq);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		if (completed)
		{
			player.setCompletedRequestList(heroReqList);
		}
		else
		{
			player.setDisplayRequestList(heroReqList);
		}
		CommonAPI.debug("processDynamicHeroRequest COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicObjective(List<DynObjective> aDynObjective)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		foreach (DynObjective aObj in aDynObjective)
		{
			Objective objective = gameData.getObjectiveByRefId(aObj.objectiveRefId);
			if (objective.getObjectiveRefId() != string.Empty)
			{
				objective.setStartTime(long.Parse(aObj.startTime));
				objective.setInitCount(CommonAPI.parseInt(aObj.initCount));
				objective.setObjectiveStarted(bool.Parse(aObj.isStarted));
				objective.setObjectiveEnded(bool.Parse(aObj.isEnded));
				objective.setObjectiveSuccess(bool.Parse(aObj.isSuccess));
			}
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processDynamicObjective COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicLegendaryHero(List<DynLegendaryHero> aDynLegendHero)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		foreach (DynLegendaryHero aHero in aDynLegendHero)
		{
			LegendaryHero legendaryHero = gameData.getLegendaryHeroByHeroRefId(aHero.legendaryHeroRefId);
			if (legendaryHero.getLegendaryHeroRefId() != string.Empty)
			{
				legendaryHero.setRequestState(CommonAPI.convertDataStringToRequestState(aHero.requestState));
			}
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processDynamicLegendaryHero COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicProject(List<DynProject> aDynProjectList, List<DynProjectTag> aDynProjectTagList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		foreach (DynProject aDynProject in aDynProjectList)
		{
			string projectId = aDynProject.projectId;
			string projectRefId = aDynProject.projectRefId;
			string projectName = aDynProject.projectName;
			string projectDesc = aDynProject.projectDesc;
			bool isPlayerNamed = bool.Parse(aDynProject.isPlayerNamed);
			Weapon projectWeapon = gameData.getWeaponByRefId(aDynProject.projectWeapon);
			Contract projectContract = gameData.getContractByRefID(aDynProject.projectContract);
			ProjectType projectType = CommonAPI.convertDataStringToProjectType(aDynProject.projectType);
			ProjectState projectState = CommonAPI.convertDataStringToProjectState(aDynProject.projectState);
			ProjectSaleState projectSaleState = CommonAPI.convertDataStringToProjectSellState(aDynProject.projectSaleState);
			ProjectPhase projectPhase = CommonAPI.convertDataStringToProjectPhase(aDynProject.projectPhase);
			int timeLimit = CommonAPI.parseInt(aDynProject.timeLimit);
			int timeElapsed = CommonAPI.parseInt(aDynProject.timeElapsed);
			long endTime = long.Parse(aDynProject.endTime);
			int progress = CommonAPI.parseInt(aDynProject.progress);
			int progressMax = CommonAPI.parseInt(aDynProject.progressMax);
			int atk = CommonAPI.parseInt(aDynProject.atk);
			int spd = CommonAPI.parseInt(aDynProject.spd);
			int acc = CommonAPI.parseInt(aDynProject.acc);
			int mag = CommonAPI.parseInt(aDynProject.mag);
			int atkReq = CommonAPI.parseInt(aDynProject.atkReq);
			int spdReq = CommonAPI.parseInt(aDynProject.spdReq);
			int accReq = CommonAPI.parseInt(aDynProject.accReq);
			int magReq = CommonAPI.parseInt(aDynProject.magReq);
			Item enchantItem = gameData.getItemByRefId(aDynProject.enchantItem);
			int numBoost = CommonAPI.parseInt(aDynProject.numBoost);
			int maxBoost = CommonAPI.parseInt(aDynProject.maxBoost);
			List<WeaponStat> prevBoost = CommonAPI.ConvertStringToWeaponStatList(aDynProject.prevBoost);
			Hero buyer = gameData.getHeroByHeroRefID(aDynProject.buyerHeroRefId);
			int finalPrice = CommonAPI.parseInt(aDynProject.finalPrice);
			int finalScore = CommonAPI.parseInt(aDynProject.finalScore);
			int qualifyGoldenHammer = CommonAPI.parseInt(aDynProject.qualifyGoldenHammer);
			string projectAchievementListString = aDynProject.projectAchievementList;
			List<ProjectAchievement> projectAchievementList = new List<ProjectAchievement>();
			if (projectAchievementListString != string.Empty)
			{
				string[] array = projectAchievementListString.Split('@');
				foreach (string typeString in array)
				{
					projectAchievementList.Add(CommonAPI.convertStringToProjectAchievement(typeString));
				}
			}
			List<Smith> usedSmithList = CommonAPI.ConvertStringToSmithList(aDynProject.usedSmithList);
			int forgeCost = CommonAPI.parseInt(aDynProject.forgeCost);
			Project project = new Project(projectId, projectRefId, projectName, projectDesc, 0, 0, 0, projectType, timeLimit, atkReq, spdReq, accReq, magReq, maxBoost, 0);
			project.setPlayerNamed(isPlayerNamed);
			project.setProjectWeapon(projectWeapon);
			project.setProjectContract(projectContract);
			project.setProjectState(projectState);
			project.setProjectSaleState(projectSaleState);
			project.setProjectPhase(projectPhase);
			project.setTimeElapsed(timeElapsed);
			project.setEndTime(endTime);
			project.setProgress(progress);
			project.setProgressMax(progressMax);
			project.setAtk(atk);
			project.setSpd(spd);
			project.setAcc(acc);
			project.setMag(mag);
			project.setEnchantItem(enchantItem);
			project.setNumBoost(numBoost);
			project.setMaxBoost(maxBoost);
			project.setPrevBoost(prevBoost);
			project.setBuyer(buyer);
			project.setFinalPrice(finalPrice);
			project.setFinalScore(finalScore);
			project.setQualifyGoldenHammer(qualifyGoldenHammer);
			project.setProjectAchievementList(projectAchievementList);
			project.setUsedSmithList(usedSmithList);
			project.setForgeCost(forgeCost);
			if (project.getProjectId() == player.getCurrentProjectId())
			{
				player.setCurrentProject(project);
				player.setCurrentProjectId(project.getProjectId());
			}
			else
			{
				player.addCompletedProject(project);
				if (player.checkDisplayProjectList(project.getProjectId()))
				{
					player.addDisplayProject(project);
				}
			}
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		fixCompletedProjectListId();
		CommonAPI.debug("processDynamicProject COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public void fixCompletedProjectListId()
	{
		Player player = game.getPlayer();
		List<Project> completedProjectList = player.getCompletedProjectList();
		Dictionary<int, Project> dictionary = new Dictionary<int, Project>();
		List<Project> list = new List<Project>();
		List<int> list2 = new List<int>();
		int num = 0;
		bool flag = false;
		foreach (Project item in completedProjectList)
		{
			int num2 = CommonAPI.parseInt(item.getProjectId());
			if (dictionary.ContainsKey(num2))
			{
				list.Add(item);
				flag = true;
			}
			else
			{
				dictionary.Add(num2, item);
			}
			if (num2 > num)
			{
				num = num2;
			}
		}
		if (flag)
		{
			for (int i = 1; i <= num; i++)
			{
				if (!dictionary.ContainsKey(i))
				{
					list2.Add(i);
				}
			}
			{
				foreach (Project item2 in list)
				{
					string projectId = item2.getProjectId();
					string empty = string.Empty;
					if (list2.Count > 0)
					{
						empty = list2[0].ToString();
						list2.RemoveAt(0);
					}
					else
					{
						empty = (num + 1).ToString();
					}
					item2.setProjectId(empty);
					CommonAPI.debug("SET DUPLICATE " + projectId + " TO " + empty);
				}
				return;
			}
		}
		CommonAPI.debug("NO DUPLICATE");
	}

	public IEnumerator processDynamicProjectOfferList(List<DynOffer> aDynOfferList, bool selected)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		foreach (DynOffer aDynOffer in aDynOfferList)
		{
			string offerId = aDynOffer.offerId;
			string projectId = aDynOffer.projectId;
			string heroRefId = aDynOffer.heroRefId;
			int offerPrice = CommonAPI.parseInt(aDynOffer.offerPrice);
			int weaponScore = CommonAPI.parseInt(aDynOffer.weaponScore);
			int expGrowth = CommonAPI.parseInt(aDynOffer.expGrowth);
			float starchMult = CommonAPI.parseFloat(aDynOffer.starchMult);
			float expMult = CommonAPI.parseFloat(aDynOffer.expMult);
			Offer offer = new Offer(offerId, projectId, heroRefId, offerPrice, weaponScore, expGrowth, starchMult, expMult);
			Project project = player.getProjectById(projectId);
			if (selected)
			{
				project.setSelectedOffer(offer);
			}
			else
			{
				project.addOfferList(offer);
			}
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processDynamicProjectOfferList COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicSmith(List<DynSmith> aDynSmithList, List<DynSmithEffect> aDynSmithEffectList, List<DynSmithExperience> aDynSmithExperienceList, List<DynSmithTag> aDynSmithTagList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		List<Smith> recruitList = new List<Smith>();
		List<Smith> allSmithList = new List<Smith>();
		allSmithList.AddRange(gameData.getSmithList(checkDLC: false, includeNormal: true, includeLegendary: true, string.Empty));
		allSmithList.AddRange(gameData.getAllOutsourceSmithList(checkDlc: false, string.Empty));
		foreach (DynSmith aDynSmith in aDynSmithList)
		{
			foreach (Smith item in allSmithList)
			{
				if (!(aDynSmith.smithRefId == item.getSmithRefId()))
				{
					continue;
				}
				string smithId = aDynSmith.smithId;
				int smithExp = CommonAPI.parseInt(aDynSmith.smithExp);
				SmithJobClass smithJobClass = gameData.getSmithJobClass(aDynSmith.smithJob);
				bool flag = bool.Parse(aDynSmith.isPlayerOwned);
				bool unlock = bool.Parse(aDynSmith.isUnlocked);
				int timesHired = CommonAPI.parseInt(aDynSmith.timesHired);
				int aPow = CommonAPI.parseInt(aDynSmith.buffPower);
				int aInt = CommonAPI.parseInt(aDynSmith.buffIntelligence);
				int aTec = CommonAPI.parseInt(aDynSmith.buffTechnique);
				int aLuc = CommonAPI.parseInt(aDynSmith.buffLuck);
				int num = CommonAPI.parseInt(aDynSmith.buffStamina);
				SmithAction smithActionByRefId = gameData.getSmithActionByRefId(aDynSmith.smithAction);
				SmithExploreState exploreState = CommonAPI.convertStringToSmithExploreState(aDynSmith.smithExploreState);
				int fixDuration = CommonAPI.parseInt(aDynSmith.smithActionDuration);
				int smithActionElapsed = CommonAPI.parseInt(aDynSmith.smithActionElapsed);
				List<StatEffect> smithEffectList = CommonAPI.ConvertStringToStatEffectList(aDynSmith.smithEffectList);
				List<float> smithEffectValueList = CommonAPI.ConvertStringToFloatList(aDynSmith.smithEffectValueList);
				List<int> smithEffectDurationList = CommonAPI.ConvertStringToIntList(aDynSmith.smithEffectDurationList);
				List<string> smithEffectDecoList = CommonAPI.ConvertStringToStringList(aDynSmith.smithEffectDecoList);
				List<string> smithEffectStatusList = CommonAPI.ConvertStringToStringList(aDynSmith.smithStatusEffectList);
				float remainingMood = CommonAPI.parseFloat(aDynSmith.remainingMood);
				int workProgress = CommonAPI.parseInt(aDynSmith.workProgress);
				SmithStation assignedRole = CommonAPI.convertStringToSmithStation(aDynSmith.assignedRole);
				SmithStation currentStation = CommonAPI.convertStringToSmithStation(aDynSmith.currentStation);
				int currentStationIndex = CommonAPI.parseInt(aDynSmith.stationIndex);
				int exploreExp = CommonAPI.parseInt(aDynSmith.exploreExp);
				int merchantExp = CommonAPI.parseInt(aDynSmith.merchantExp);
				Area areaByRefID = gameData.getAreaByRefID(aDynSmith.exploreArea);
				List<SmithExploreState> actionStateList = CommonAPI.ConvertStringToSmithExploreStateList(aDynSmith.actionStateList);
				List<int> actionDurationList = CommonAPI.ConvertStringToIntList(aDynSmith.actionDurationList);
				int actionProgressIndex = CommonAPI.parseInt(aDynSmith.actionProgressIndex);
				List<string> exploreTask = CommonAPI.ConvertStringToStringList(aDynSmith.exploreTaskList);
				Vacation vacationByRefId = gameData.getVacationByRefId(aDynSmith.vacation);
				SmithTraining smithTrainingByRefId = gameData.getSmithTrainingByRefId(aDynSmith.training);
				List<AreaStatus> areaStatusList = CommonAPI.ConvertStringToAreaStatusList(aDynSmith.areaStatusList);
				int currentSmithSalary = CommonAPI.parseInt(aDynSmith.currentSalary);
				item.setSmithId(smithId);
				item.setSmithExp(smithExp);
				item.setSmithJob(smithJobClass);
				item.setPlayerOwned(flag, isHire: false);
				item.setUnlock(unlock);
				item.setTimesHired(timesHired);
				item.setStatBuffs(aPow, aInt, aTec, aLuc, num);
				item.setSmithAction(smithActionByRefId, fixDuration);
				item.setExploreState(exploreState);
				item.setSmithActionElapsed(smithActionElapsed);
				item.setSmithEffectList(smithEffectList);
				item.setSmithEffectValueList(smithEffectValueList);
				item.setSmithEffectDurationList(smithEffectDurationList);
				item.setSmithEffectDecoList(smithEffectDecoList);
				item.setSmithEffectStatusList(smithEffectStatusList);
				item.setRemainingMood(remainingMood);
				item.setWorkProgress(workProgress);
				item.setAssignedRole(assignedRole);
				item.setCurrentStation(currentStation);
				item.setCurrentStationIndex(currentStationIndex);
				item.setExploreExp(exploreExp);
				item.setMerchantExp(merchantExp);
				item.setExploreArea(areaByRefID);
				item.setActionStateList(actionStateList);
				item.setActionDurationList(actionDurationList);
				item.setActionProgressIndex(actionProgressIndex);
				item.setExploreTask(exploreTask);
				item.setVacation(vacationByRefId);
				item.setTraining(smithTrainingByRefId);
				item.setAreaStatusList(areaStatusList);
				item.setCurrentSmithSalary(currentSmithSalary);
				foreach (DynSmithExperience aDynSmithExperience in aDynSmithExperienceList)
				{
					if (aDynSmithExperience.smithRefId == item.getSmithRefId())
					{
						int smithJobClassLevel = CommonAPI.parseInt(aDynSmithExperience.level);
						bool tagGiven = bool.Parse(aDynSmithExperience.tagGiven);
						SmithExperience experienceByRefId = item.getExperienceByRefId(aDynSmithExperience.smithExperienceRefId);
						experienceByRefId.setSmithJobClassLevel(smithJobClassLevel);
						experienceByRefId.setTagGiven(tagGiven);
					}
				}
				foreach (DynSmithTag aDynSmithTag in aDynSmithTagList)
				{
					if (aDynSmithTag.smithRefId == item.getSmithRefId())
					{
						int useCount = CommonAPI.parseInt(aDynSmithTag.useCount);
						int displayOrder = CommonAPI.parseInt(aDynSmithTag.displayOrder);
						SmithTag smithTagBySmithTagRefId = item.getSmithTagBySmithTagRefId(aDynSmithTag.smithTagRefId);
						smithTagBySmithTagRefId.setUseCount(useCount);
						smithTagBySmithTagRefId.setDisplayOrder(displayOrder);
					}
				}
				if (flag)
				{
					player.addSmith(item);
				}
				if (player.checkRecruitInStringList(aDynSmith.smithRefId))
				{
					recruitList.Add(item);
				}
				break;
			}
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		player.setRecruitList(recruitList);
		CommonAPI.debug("processDynamicSmith COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicQuestNEW(List<DynQuestNEW> aDynQuestList, List<DynQuestTag> aDynQuestTagList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		foreach (DynQuestNEW aDynQuest in aDynQuestList)
		{
			foreach (QuestNEW questNEW in gameData.getQuestNEWList())
			{
				if (!(aDynQuest.questRefId == questNEW.getQuestRefId()))
				{
					continue;
				}
				bool unlocked = bool.Parse(aDynQuest.isUnlocked);
				bool locked = bool.Parse(aDynQuest.isLocked);
				int expiryDay = CommonAPI.parseInt(aDynQuest.expiryDay);
				bool expired = bool.Parse(aDynQuest.isExpired);
				int completeCount = CommonAPI.parseInt(aDynQuest.completeCount);
				bool ongoing = bool.Parse(aDynQuest.isOngoing);
				int aAtkReq = CommonAPI.parseInt(aDynQuest.atkReq);
				int aSpdReq = CommonAPI.parseInt(aDynQuest.spdReq);
				int aAccReq = CommonAPI.parseInt(aDynQuest.accReq);
				int aMagReq = CommonAPI.parseInt(aDynQuest.magReq);
				int aMilestoneNum = CommonAPI.parseInt(aDynQuest.milestoneNum);
				int aQuestTime = CommonAPI.parseInt(aDynQuest.questTime);
				int aMinQuestGold = CommonAPI.parseInt(aDynQuest.minQuestGold);
				int aChallengeLastSet = CommonAPI.parseInt(aDynQuest.challengeLastSet);
				int questGoldTotal = CommonAPI.parseInt(aDynQuest.questGoldTotal);
				List<int> list = new List<int>();
				if (aDynQuest.questGoldDivide.Length > 0)
				{
					string[] array = aDynQuest.questGoldDivide.Split('@');
					string[] array2 = array;
					foreach (string aText in array2)
					{
						list.Add(CommonAPI.parseInt(aText));
					}
				}
				int questProgress = CommonAPI.parseInt(aDynQuest.questProgress);
				int milestonePassed = CommonAPI.parseInt(aDynQuest.milestonePassed);
				questNEW.setUnlocked(unlocked);
				questNEW.setLocked(locked);
				questNEW.setExpiryDay(expiryDay);
				questNEW.setExpired(expired);
				questNEW.setCompleteCount(completeCount);
				questNEW.setOngoing(ongoing);
				questNEW.setChallenge(aAtkReq, aSpdReq, aAccReq, aMagReq, aMilestoneNum, aQuestTime, aMinQuestGold, aChallengeLastSet);
				questNEW.setQuestGoldTotal(questGoldTotal);
				questNEW.setQuestGoldDivide(list);
				questNEW.setQuestProgress(questProgress);
				questNEW.setMilestonePassed(milestonePassed);
				foreach (DynQuestTag aDynQuestTag in aDynQuestTagList)
				{
					if (aDynQuestTag.questRefId == questNEW.getQuestRefId())
					{
						questNEW.getQuestTagByQuestTagRefId(aDynQuestTag.questTagRefId).setTagUnlock(bool.Parse(aDynQuestTag.isUnlocked));
					}
				}
				break;
			}
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processDynamicQuestNEW COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicSmithTraining(List<DynSmithTraining> aDynTrainingList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		List<SmithTraining> smithTrainingList = new List<SmithTraining>();
		foreach (DynSmithTraining aDynTraining in aDynTrainingList)
		{
			SmithTraining aTraining = gameData.getSmithTrainingByRefId(aDynTraining.smithTrainingRefId);
			if (aTraining.getSmithTrainingRefId() != string.Empty)
			{
				aTraining.setUseCount(CommonAPI.parseInt(aDynTraining.useCount));
			}
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processDynamicSmithTraining COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicSpecialEvent(List<DynSpecialEvent> aDynSpecialEventList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		foreach (DynSpecialEvent aDynSpecialEvent in aDynSpecialEventList)
		{
			foreach (SpecialEvent specialEvent in gameData.getSpecialEventList())
			{
				if (aDynSpecialEvent.specialEventRefId == specialEvent.getSpecialEventRefId())
				{
					int occurrenceCount = CommonAPI.parseInt(aDynSpecialEvent.occurrenceCount);
					specialEvent.setOccurrenceCount(occurrenceCount);
					break;
				}
			}
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processDynamicSpecialEvent COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicSeasonObjective(List<DynSeasonObjective> aDynSeasonObjectiveList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		foreach (DynSeasonObjective aDynSeasonObjective in aDynSeasonObjectiveList)
		{
			foreach (SeasonObjective seasonObjective in gameData.getSeasonObjectiveList())
			{
				if (aDynSeasonObjective.objectiveRefId == seasonObjective.getObjectiveRefId())
				{
					int points = CommonAPI.parseInt(aDynSeasonObjective.points);
					bool completion = bool.Parse(aDynSeasonObjective.isCompleted);
					seasonObjective.setPoints(points);
					seasonObjective.setCompletion(completion);
					break;
				}
			}
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processDynamicSeasonObjective COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicTag(List<DynTag> aDynTagList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		foreach (DynTag aDynTag in aDynTagList)
		{
			foreach (Tag tag in gameData.getTagList())
			{
				if (aDynTag.tagRefId == tag.getTagRefId())
				{
					bool seenTag = bool.Parse(aDynTag.seenTag);
					int useCount = CommonAPI.parseInt(aDynTag.tagUseCount);
					tag.setSeenTag(seenTag);
					tag.setUseCount(useCount);
					break;
				}
			}
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processDynamicTag COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicWeapon(List<DynWeapon> aDynWeaponList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		List<Weapon> unlockedList = new List<Weapon>();
		foreach (DynWeapon aDynWeapon in aDynWeaponList)
		{
			foreach (Weapon weapon in gameData.getWeaponList(checkDLC: false, string.Empty))
			{
				if (aDynWeapon.weaponRefId == weapon.getWeaponRefId())
				{
					bool flag = bool.Parse(aDynWeapon.isUnlocked);
					int timesUsed = CommonAPI.parseInt(aDynWeapon.timesUsed);
					weapon.setWeaponUnlocked(flag);
					weapon.setTimesUsed(timesUsed);
					if (flag)
					{
						unlockedList.Add(weapon);
					}
					break;
				}
			}
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		player.setUnlockedWeaponList(unlockedList);
		CommonAPI.debug("processDynamicWeapoon COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicCode(List<DynCode> aDynCodeList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		List<Code> codeList = gameData.getCodeList();
		foreach (DynCode aDynCode in aDynCodeList)
		{
			foreach (Code item in codeList)
			{
				if (item.getRefID() == aDynCode.codeRefId)
				{
					item.setUsed(bool.Parse(aDynCode.isUsed));
					break;
				}
			}
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processDynamicCode COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicWeaponType(List<DynWeaponType> aDynWeaponTypeList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		List<WeaponType> unlockedList = new List<WeaponType>();
		int i = 0;
		foreach (DynWeaponType aDynWeaponType in aDynWeaponTypeList)
		{
			foreach (WeaponType weaponType in gameData.getWeaponTypeList())
			{
				if (aDynWeaponType.weaponTypeRefId == weaponType.getWeaponTypeRefId())
				{
					bool flag = bool.Parse(aDynWeaponType.isUnlocked);
					weaponType.setUnlock(flag);
					if (flag)
					{
						unlockedList.Add(weaponType);
					}
					break;
				}
			}
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		player.setUnlockedWeaponTypeList(unlockedList);
		CommonAPI.debug("processDynamicWeaponType COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicWeekendActivity(List<DynWeekendActivity> aDynWeekendActivityList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		foreach (DynWeekendActivity aDynWeekendActivity in aDynWeekendActivityList)
		{
			foreach (WeekendActivity weekendActivity in gameData.getWeekendActivityList())
			{
				if (aDynWeekendActivity.weekendActivityRefId == weekendActivity.getWeekendActivityRefId())
				{
					int doneCount = CommonAPI.parseInt(aDynWeekendActivity.doneCount);
					weekendActivity.setDoneCount(doneCount);
					break;
				}
			}
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processDynamicWeekendActivity COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicStation(List<DynStation> aDynStationList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		foreach (DynStation aDynStation in aDynStationList)
		{
			Station station = gameData.getStationByRefID(aDynStation.refStationID);
			List<Smith> stationSmithList = CommonAPI.ConvertStringToSmithList(aDynStation.smithList);
			station.setSmithList(stationSmithList);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processDynamicStation COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicWhetsapp(List<DynWhetsapp> aDynWhetsappList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		gameData.clearWhetsapp();
		foreach (DynWhetsapp aDynWhetsapp in aDynWhetsappList)
		{
			string whetsappId = aDynWhetsapp.whetsappId;
			string senderName = aDynWhetsapp.senderName;
			string messageTextRefId = aDynWhetsapp.messageTextRefId;
			string imagePath = aDynWhetsapp.imagePath;
			long time = long.Parse(aDynWhetsapp.time);
			WhetsappFilterType filterType = CommonAPI.convertDataStringToWhetsappFilterType(aDynWhetsapp.filterType);
			bool isRead = bool.Parse(aDynWhetsapp.isRead);
			gameData.addDynamicWhetsapp(whetsappId, senderName, messageTextRefId, imagePath, time, filterType, isRead);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processDynamicWhetsapp COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicAchievement(List<DynAchievement> aDynAchievementList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		List<Achievement> achList = gameData.getAchievementList();
		foreach (DynAchievement aDynAch in aDynAchievementList)
		{
			foreach (Achievement item in achList)
			{
				if (item.getAchivementRefID() == aDynAch.achievementRefID)
				{
					item.setAchieved(bool.Parse(aDynAch.achieved));
					break;
				}
			}
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processDynamicAchievement COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	public IEnumerator processDynamicScenarioVariable(List<DynScenarioVariable> aDynScenarioVariableList)
	{
		coroutineCheckStart++;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int i = 0;
		List<ScenarioVariable> varList = gameData.getScenarioVariableList();
		foreach (DynScenarioVariable aDynVar in aDynScenarioVariableList)
		{
			foreach (ScenarioVariable item in varList)
			{
				if (item.getScenarioVariableRefId() == aDynVar.scenarioVariableRefId)
				{
					item.setVariableValueString(aDynVar.variableValue);
					break;
				}
			}
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processDynamicScenarioVariable COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private void checkCoroutine()
	{
		if (GameObject.Find("Panel_LoadingLanguage") != null && coroutineCheckDone >= coroutineCheckStart)
		{
			CommonAPI.debug("Loading Coroutine started: " + coroutineCheckStart);
			CommonAPI.debug("Loading Coroutine Done: " + coroutineCheckDone);
			GameObject.Find("Panel_SaveLoadPopup").GetComponent<GUISaveLoadPopupController>().loadAllUI();
			GameObject.Find("ViewController").GetComponent<ViewController>().closeLoadRef();
		}
	}
}
