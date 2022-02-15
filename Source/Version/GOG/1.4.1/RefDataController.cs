using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class RefDataController : MonoBehaviour
{
	private Game game;

	private JsonFileController jsonFileController;

	private int coroutineCheckDone;

	private int coroutineCheckStart;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		jsonFileController = GameObject.Find("JsonFileController").GetComponent<JsonFileController>();
		coroutineCheckDone = 0;
		coroutineCheckStart = 0;
	}

	public void getRefDataFromFile()
	{
		GameObject.Find("ViewController").GetComponent<ViewController>().showLoadRef();
		try
		{
			string empty = string.Empty;
			string fileNameByLanguage = getFileNameByLanguage(Constants.LANGUAGE);
			TextAsset textAsset = (TextAsset)Resources.Load("Data/" + fileNameByLanguage, typeof(TextAsset));
			empty = textAsset.ToString();
			empty = empty.Remove(0, empty.Length % 4);
			empty = CommonAPI.Decrypt(empty, "e7nc3r2e6f8k2e0y");
			ServerResponse serverResponse = JsonMapper.ToObject<ServerResponse>(empty);
			ServerValue value = serverResponse.value;
			coroutineCheckDone = 0;
			coroutineCheckStart = 0;
			processAllRefData(value);
			jsonFileController.loadSaveFileDir(game);
		}
		catch (Exception ex)
		{
			CommonAPI.debug(ex.ToString());
		}
	}

	private void processAllRefData(ServerValue aValue)
	{
		StartCoroutine(processRefArea(aValue.RefArea, aValue.RefAreaHero, aValue.RefAreaExploreItem, aValue.RefAreaShopItem));
		StartCoroutine(processRefAreaEvent(aValue.RefAreaEvent));
		StartCoroutine(processRefAreaRegion(aValue.RefAreaRegion));
		StartCoroutine(processRefAreaStatus(aValue.RefAreaStatus));
		StartCoroutine(processRefAreaPath(aValue.RefAreaPath));
		StartCoroutine(processRefAvatar(aValue.RefAvatar));
		StartCoroutine(processRefFurniture(aValue.RefFurniture));
		StartCoroutine(processRefDecoration(aValue.RefDecoration, aValue.RefDecorationEffect, aValue.RefDecorationType));
		StartCoroutine(processRefDecorationPosition(aValue.RefDecorationPosition));
		StartCoroutine(processRefHeroLevel(aValue.RefHeroLevel));
		StartCoroutine(processRefHero(aValue.RefHero, aValue.RefAffinity, aValue.RefJobClassTag));
		StartCoroutine(processRefLegendaryHero(aValue.RefLegendaryHero));
		StartCoroutine(processRefMaxLoyaltyReward(aValue.RefMaxLoyaltyReward));
		StartCoroutine(processRefRequest(aValue.RefRequest));
		StartCoroutine(processRefJobUnlockNEW(aValue.RefJobUnlockNEW));
		StartCoroutine(processRefItem(aValue.RefItem, aValue.RefDogItem));
		StartCoroutine(processRefSmithJobClass(aValue.RefSmithJobClass));
		StartCoroutine(processRefSmithTraining(aValue.RefSmithTraining));
		StartCoroutine(processRefTrainingPackage(aValue.RefTrainingPackage));
		StartCoroutine(processRefWeaponType(aValue.RefWeaponType));
		StartCoroutine(processRefWeapon(aValue.RefWeapon, aValue.RefWeaponMaterial, aValue.RefWeaponRelic));
		StartCoroutine(processRefShopLevel(aValue.RefShopLevel));
		StartCoroutine(processRefRecruitmentType(aValue.RefRecruitmentType, aValue.RefRecruitmentSmith));
		StartCoroutine(processRefForgeIncident(aValue.RefForgeIncident));
		StartCoroutine(processRefQuestNEW(aValue.RefQuestNEW, aValue.RefQuestTag));
		StartCoroutine(processRefContract(aValue.RefContract));
		StartCoroutine(processRefObjective(aValue.RefObjective));
		StartCoroutine(processRefTutorial(aValue.RefTutorial));
		StartCoroutine(processRefWeather(aValue.RefWeather));
		StartCoroutine(processRefDayEndScenario(aValue.RefDayEndScenario));
		StartCoroutine(processRefSmithAction(aValue.RefSmithAction));
		StartCoroutine(processRefSmithStatusEffect(aValue.RefSmithStatusEffect));
		StartCoroutine(processRefSmith(aValue.RefSmith, aValue.RefSmithExperience, aValue.RefSmithTag));
		StartCoroutine(processRefDialogue(aValue.RefDialogueNEW));
		StartCoroutine(processRefDialogueSetNoSkip(aValue.RefDialogueSetNoSkip));
		StartCoroutine(processRefSpecialEvent(aValue.RefSpecialEvent));
		StartCoroutine(processRefSeasonObjective(aValue.RefSeasonObjective));
		StartCoroutine(processRefTag(aValue.RefTag));
		StartCoroutine(processRefRewardChest(aValue.RefRewardChest, aValue.RefRewardChestItem));
		StartCoroutine(processRefWeekendActivity(aValue.RefWeekendActivity));
		StartCoroutine(processRefStation(aValue.RefStation));
		StartCoroutine(processRefPath(aValue.RefPath));
		StartCoroutine(processRefObstacles(aValue.RefObstacles));
		StartCoroutine(processRefEnemy(aValue.RefEnemy));
		StartCoroutine(processRefVacation(aValue.RefVacation));
		StartCoroutine(processRefVacationPackage(aValue.RefVacationPackage));
		StartCoroutine(processRefCutscenePath(aValue.RefCutscenePath));
		StartCoroutine(processRefCutsceneDialogue(aValue.RefCutsceneDialogue));
		StartCoroutine(processRefCutsceneObstacle(aValue.RefCutsceneObstacles));
		StartCoroutine(processRefKeyShortcut(aValue.RefKeyShortcut));
		StartCoroutine(processRefCode(aValue.RefCode));
		StartCoroutine(processRefAchievement(aValue.RefAchievement));
		StartCoroutine(processRefInitialValue(aValue.RefInitialValue));
		StartCoroutine(processRefGameScenario(aValue.RefGameScenario));
		StartCoroutine(processRefScenarioVariable(aValue.RefScenarioVariable));
		StartCoroutine(processRefGameLock(aValue.RefGameLock));
		StartCoroutine(processRefRandomText(aValue.RefRandomText));
		StartCoroutine(processRefText(aValue.RefText));
		StartCoroutine(processRefGameConstant(aValue.RefGameConstant));
	}

	public void processRefData(ServerResponse aResponse, LanguageType aLang = LanguageType.kLanguageTypeBlank)
	{
		if (aLang == LanguageType.kLanguageTypeBlank)
		{
			aLang = Constants.LANGUAGE;
		}
		if (aLang == Constants.LANGUAGE)
		{
			game.clearRefData();
			ServerValue value = aResponse.value;
			processAllRefData(value);
		}
	}

	private IEnumerator processRefWeekendActivity(List<RefWeekendActivity> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearWeekendActivity();
		int i = 0;
		foreach (RefWeekendActivity aWeekendActivity in aArray)
		{
			string weekendActivityRefId = aWeekendActivity.weekendActivityRefId;
			string activityName = aWeekendActivity.activityName;
			string activityResultText = aWeekendActivity.activityResultText;
			WeekendActivityType activityType = CommonAPI.convertStringToWeekendActivity(aWeekendActivity.activityType);
			int limit = CommonAPI.parseInt(aWeekendActivity.limit);
			int chance = CommonAPI.parseInt(aWeekendActivity.chance);
			int reqShopLevel = CommonAPI.parseInt(aWeekendActivity.reqShopLevel);
			Season reqSeason = CommonAPI.convertStringToSeason(aWeekendActivity.reqSeason);
			int reqPlayerMonths = CommonAPI.parseInt(aWeekendActivity.reqPlayerMonths);
			string reqFurniture = aWeekendActivity.reqFurniture;
			int reqAlignGood = CommonAPI.parseInt(aWeekendActivity.reqAlignGood);
			int reqAlignEvil = CommonAPI.parseInt(aWeekendActivity.reqAlignEvil);
			string rewardType = aWeekendActivity.rewardType;
			string rewardRefId = aWeekendActivity.rewardRefId;
			int rewardMagnitude = CommonAPI.parseInt(aWeekendActivity.rewardMagnitude);
			int rewardQty = CommonAPI.parseInt(aWeekendActivity.rewardQty);
			int dogLove = CommonAPI.parseInt(aWeekendActivity.dogLove);
			aData.addWeekendActivity(weekendActivityRefId, activityName, activityResultText, activityType, limit, chance, reqShopLevel, reqSeason, reqPlayerMonths, reqFurniture, reqAlignGood, reqAlignEvil, rewardType, rewardRefId, rewardMagnitude, rewardQty, dogLove);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefWeekendActivity COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefSpecialEvent(List<RefSpecialEvent> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearSpecialEvent();
		int i = 0;
		foreach (RefSpecialEvent aSpecialEvent in aArray)
		{
			string specialEventRefId = aSpecialEvent.specialEventRefId;
			int specialEventMaxOccurrence = CommonAPI.parseInt(aSpecialEvent.specialEventMaxOccurrence);
			string scenarioLock = aSpecialEvent.scenarioLock;
			int dateYear = CommonAPI.parseInt(aSpecialEvent.dateYear);
			int dateMonth = CommonAPI.parseInt(aSpecialEvent.dateMonth);
			int dateWeek = CommonAPI.parseInt(aSpecialEvent.dateWeek);
			int dateDay = CommonAPI.parseInt(aSpecialEvent.dateDay);
			SpecialEventType eventType = CommonAPI.convertStringToSpecialEventType(aSpecialEvent.eventType);
			int startAfterMonths = CommonAPI.parseInt(aSpecialEvent.startAfterMonths);
			aData.addSpecialEvent(specialEventRefId, specialEventMaxOccurrence, scenarioLock, dateYear, dateMonth, dateWeek, dateDay, eventType, startAfterMonths);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefSpecialEvent COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefSeasonObjective(List<RefSeasonObjective> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearSeasonObjective();
		int i = 0;
		foreach (RefSeasonObjective aSeasonObjective in aArray)
		{
			string objectiveRefId = aSeasonObjective.objectiveRefId;
			string objectiveTitle = aSeasonObjective.objectiveTitle;
			string objectiveDescription = aSeasonObjective.objectiveDescription;
			int objectiveSeasonIndex = CommonAPI.parseInt(aSeasonObjective.objectiveSeasonIndex);
			string alignment = aSeasonObjective.alignment;
			int range = CommonAPI.parseInt(aSeasonObjective.range);
			int targetPoints = CommonAPI.parseInt(aSeasonObjective.targetPoints);
			string startDialogueRefId = aSeasonObjective.startDialogueRefId;
			string endDialogueRefId = aSeasonObjective.endDialogueRefId;
			aData.addSeasonObjective(objectiveRefId, objectiveTitle, objectiveDescription, objectiveSeasonIndex, alignment, range, targetPoints, startDialogueRefId, endDialogueRefId);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefSeasonObjective COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefTag(List<RefTag> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearTag();
		int i = 0;
		foreach (RefTag aTag in aArray)
		{
			string tagRefId = aTag.tagRefId;
			string tagName = aTag.tagName;
			string tagDesc = aTag.tagDesc;
			aData.addTag(tagRefId, tagName, tagDesc);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefTag COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefRewardChest(List<RefRewardChest> aChestArray, List<RefRewardChestItem> aChestItemArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearRewardChest();
		int i = 0;
		foreach (RefRewardChest aChest in aChestArray)
		{
			string rewardChestRefId2 = aChest.rewardChestRefId;
			string chestName = aChest.chestName;
			string chestDescription = aChest.chestDescription;
			string image = aChest.image;
			aData.addRewardChest(rewardChestRefId2, chestName, chestDescription, image);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		i = 0;
		foreach (RefRewardChestItem aChestItem in aChestItemArray)
		{
			RewardChest chest = aData.getRewardChestByRefId(aChestItem.rewardChestRefId);
			string rewardChestItemRefId = aChestItem.rewardChestItemRefId;
			string rewardChestRefId = aChestItem.rewardChestRefId;
			string itemRefId = aChestItem.itemRefId;
			int itemNum = CommonAPI.parseInt(aChestItem.itemNum);
			int probability = CommonAPI.parseInt(aChestItem.probability);
			chest.addRewardChestItem(rewardChestItemRefId, rewardChestRefId, itemRefId, itemNum, probability);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefRewardChest COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefDialogue(List<RefDialogueNEW> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearDialogue();
		int i = 0;
		foreach (RefDialogueNEW aDialogue in aArray)
		{
			string dialogueRefId = aDialogue.dialogueRefId;
			string dialogueSetId = aDialogue.dialogueSetId;
			string dialogueName = aDialogue.dialogueName;
			string dialogueText = aDialogue.dialogueText;
			string dialogueNextRefId = aDialogue.dialogueNextRefId;
			string dialogueChoice1 = aDialogue.dialogueChoice1;
			string dialogueChoice1NextRefId = aDialogue.dialogueChoice1NextRefId;
			string dialogueChoice2 = aDialogue.dialogueChoice2;
			string dialogueChoice2NextRefId = aDialogue.dialogueChoice2NextRefId;
			string dialoguePosition = aDialogue.dialoguePosition;
			string backgroundTexture = aDialogue.backgroundTexture;
			string soundEffect = aDialogue.soundEffect;
			string backgroundMusic = aDialogue.backgroundMusic;
			string dialogueImage = aDialogue.dialogueImage;
			bool dialogueFlip = bool.Parse(aDialogue.dialogueFlip);
			aData.addDialogue(dialogueRefId, dialogueSetId, dialogueName, dialogueText, dialogueNextRefId, dialogueChoice1, dialogueChoice1NextRefId, dialogueChoice2, dialogueChoice2NextRefId, dialoguePosition, backgroundTexture, soundEffect, backgroundMusic, dialogueImage, dialogueFlip);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefDialogue COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefDialogueSetNoSkip(List<RefDialogueSetNoSkip> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearDialogueNoSkip();
		int i = 0;
		foreach (RefDialogueSetNoSkip aNoSkip in aArray)
		{
			aData.addDialogueNoSkip(aNoSkip.dialogueSetId);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefDialogueSetNoSkip COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefArea(List<RefArea> aAreaArray, List<RefAreaHero> aHeroArray, List<RefAreaExploreItem> aExploreItemArray, List<RefAreaShopItem> aShopItemArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearArea();
		int i = 0;
		foreach (RefArea aArea in aAreaArray)
		{
			string areaRefId = aArea.areaRefId;
			string areaName = aArea.areaName;
			string areaDesc = aArea.areaDesc;
			string coordinates = aArea.coordinates;
			string position = aArea.position;
			float scale = CommonAPI.parseFloat(aArea.scale);
			string image = aArea.image;
			Dictionary<string, int> heroList = new Dictionary<string, int>();
			Dictionary<string, int> rareHeroList = new Dictionary<string, int>();
			foreach (RefAreaHero item2 in aHeroArray)
			{
				if (!(item2.areaRefId == areaRefId))
				{
					continue;
				}
				if (bool.Parse(item2.isRare))
				{
					string heroRefId = item2.heroRefId;
					int value = CommonAPI.parseInt(item2.probability);
					if (!rareHeroList.ContainsKey(heroRefId))
					{
						rareHeroList.Add(heroRefId, value);
					}
				}
				else
				{
					string heroRefId2 = item2.heroRefId;
					int value2 = CommonAPI.parseInt(item2.probability);
					if (!heroList.ContainsKey(heroRefId2))
					{
						heroList.Add(heroRefId2, value2);
					}
				}
			}
			Dictionary<string, ExploreItem> exploreList = new Dictionary<string, ExploreItem>();
			foreach (RefAreaExploreItem item3 in aExploreItemArray)
			{
				if (item3.areaRefId == areaRefId)
				{
					string areaExploreItemRefId = item3.areaExploreItemRefId;
					_ = item3.areaRefId;
					string itemRefId = item3.itemRefId;
					int aProbability = CommonAPI.parseInt(item3.probability);
					if (!exploreList.ContainsKey(itemRefId))
					{
						exploreList.Add(itemRefId, new ExploreItem(areaExploreItemRefId, areaRefId, itemRefId, aProbability));
					}
				}
			}
			Dictionary<string, ShopItem> shopItemList = new Dictionary<string, ShopItem>();
			foreach (RefAreaShopItem item4 in aShopItemArray)
			{
				if (item4.areaRefId == areaRefId)
				{
					string areaShopItemRefId = item4.areaShopItemRefId;
					_ = item4.areaRefId;
					string itemRefId2 = item4.itemRefId;
					int aCost = CommonAPI.parseInt(item4.cost);
					int aMaxQty = CommonAPI.parseInt(item4.maxQty);
					if (!shopItemList.ContainsKey(itemRefId2))
					{
						shopItemList.Add(itemRefId2, new ShopItem(areaShopItemRefId, areaRefId, itemRefId2, aCost, aMaxQty));
					}
				}
			}
			AreaType areaType = CommonAPI.convertStringToAreaType(aArea.areaType);
			bool canSell = bool.Parse(aArea.canSell);
			bool canBuy = bool.Parse(aArea.canBuy);
			bool canExplore = bool.Parse(aArea.canExplore);
			string trainingPackageRefId = aArea.trainingPackageRefId;
			string vacationPackageRefId = aArea.vacationPackageRefId;
			int travelTime = CommonAPI.parseInt(aArea.travelTime);
			float moodFactor = CommonAPI.parseFloat(aArea.moodFactor);
			int refreshPrice = CommonAPI.parseInt(aArea.refreshPrice);
			string statusEffectRefId = aArea.statusEffectRefId;
			int expGrowth = CommonAPI.parseInt(aArea.expGrowth);
			string[] unlockAreaListSplit = aArea.unlockArea.Split('@');
			List<string> unlockAreaList = new List<string>();
			string[] array = unlockAreaListSplit;
			foreach (string item in array)
			{
				unlockAreaList.Add(item);
			}
			int unlockTickets = CommonAPI.parseInt(aArea.unlockTickets);
			int region = CommonAPI.parseInt(aArea.region);
			aData.addArea(areaRefId, areaName, areaDesc, coordinates, position, scale, image, heroList, rareHeroList, exploreList, shopItemList, areaType, canSell, canBuy, canExplore, trainingPackageRefId, vacationPackageRefId, travelTime, moodFactor, refreshPrice, statusEffectRefId, expGrowth, unlockAreaList, unlockTickets, region);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefArea COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefAreaEvent(List<RefAreaEvent> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearAreaEvent();
		int i = 0;
		foreach (RefAreaEvent aRegion in aArray)
		{
			string areaEventRefId = aRegion.areaEventRefId;
			string areaEventName = aRegion.areaEventName;
			string areaEventDescription = aRegion.areaEventDescription;
			int areaStartRegion = CommonAPI.parseInt(aRegion.areaStartRegion);
			int areaEndRegion = CommonAPI.parseInt(aRegion.areaEndRegion);
			string effectType = aRegion.effectType;
			float starchMult = CommonAPI.parseFloat(aRegion.starchMult);
			float expMult = CommonAPI.parseFloat(aRegion.expMult);
			int duration = CommonAPI.parseInt(aRegion.duration);
			int probability = CommonAPI.parseInt(aRegion.probability);
			aData.addAreaEvent(areaEventRefId, areaEventName, areaEventDescription, areaStartRegion, areaEndRegion, effectType, starchMult, expMult, duration, probability);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefAreaEvent COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefAreaRegion(List<RefAreaRegion> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearAreaRegion();
		int i = 0;
		foreach (RefAreaRegion aRegion in aArray)
		{
			int areaRegionRefID = CommonAPI.parseInt(aRegion.areaRegionRefID);
			string regionName = aRegion.regionName;
			string regionDesc = aRegion.regionDesc;
			string scenarioLock = aRegion.scenarioLock;
			int fameRequired = CommonAPI.parseInt(aRegion.fameRequired);
			int workstationLvl = CommonAPI.parseInt(aRegion.workstationLvl);
			int regionTicketInterval = CommonAPI.parseInt(aRegion.regionTicketInterval);
			int maxEventCount = CommonAPI.parseInt(aRegion.maxEventCount);
			int grantAmount = CommonAPI.parseInt(aRegion.grantAmount);
			string cameraCentre = aRegion.cameraCentre;
			float zoomDefault = CommonAPI.parseFloat(aRegion.zoomDefault);
			float xPosLimitUpper = CommonAPI.parseFloat(aRegion.xPosLimitUpper);
			float xPosLimitLower = CommonAPI.parseFloat(aRegion.xPosLimitLower);
			float yPosLimitUpper = CommonAPI.parseFloat(aRegion.yPosLimitUpper);
			float yPosLimitLower = CommonAPI.parseFloat(aRegion.yPosLimitLower);
			float zoomLimitUpper = CommonAPI.parseFloat(aRegion.zoomLimitUpper);
			float zoomLimitLower = CommonAPI.parseFloat(aRegion.zoomLimitLower);
			float targetZoom = CommonAPI.parseFloat(aRegion.targetZoom);
			string bgImg = aRegion.bgImg;
			aData.addAreaRegion(areaRegionRefID, regionName, regionDesc, scenarioLock, fameRequired, workstationLvl, regionTicketInterval, maxEventCount, grantAmount, cameraCentre, zoomDefault, xPosLimitUpper, xPosLimitLower, yPosLimitUpper, yPosLimitLower, zoomLimitUpper, zoomLimitLower, targetZoom, bgImg);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefAreaRegion COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefAreaStatus(List<RefAreaStatus> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearAreaStatus();
		int i = 0;
		foreach (RefAreaStatus aAreaStatus in aArray)
		{
			string areaStatusRefID = aAreaStatus.areaStatusRefID;
			string areaRefID = aAreaStatus.areaRefID;
			string smithEffectRefID = aAreaStatus.smithEffectRefID;
			string season = aAreaStatus.season;
			int probability = CommonAPI.parseInt(aAreaStatus.probability);
			aData.addAreaStatus(areaStatusRefID, areaRefID, smithEffectRefID, season, probability);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefAreaStatus COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefAreaPath(List<RefAreaPath> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearAreaPath();
		int i = 0;
		foreach (RefAreaPath aAreaPath in aArray)
		{
			string areaPathRefID = aAreaPath.areaPathRefId;
			string startAreaRefID = aAreaPath.startAreaRefID;
			string endAreaRefID = aAreaPath.endAreaRefID;
			string path = aAreaPath.path;
			aData.addAreaPath(areaPathRefID, startAreaRefID, endAreaRefID, path);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefAreaPath COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefAvatar(List<RefAvatar> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearAvatar();
		int i = 0;
		foreach (RefAvatar aAvatar in aArray)
		{
			string avatarRefId = aAvatar.avatarRefId;
			string avatarName = aAvatar.avatarName;
			string avatarDescription = aAvatar.avatarDescription;
			string avatarImage = aAvatar.avatarImage;
			bool initUnlock = bool.Parse(aAvatar.initUnlock);
			int unlockPlayRequirement = CommonAPI.parseInt(aAvatar.unlockPlayRequirement);
			aData.addAvatar(avatarRefId, avatarName, avatarDescription, avatarImage, initUnlock, unlockPlayRequirement);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefAvatar COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefFurniture(List<RefFurniture> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearFurniture();
		int i = 0;
		foreach (RefFurniture aFurniture in aArray)
		{
			string furnitureRefId = aFurniture.furnitureRefId;
			string furnitureName = aFurniture.furnitureName;
			string furnitureDesc = aFurniture.furnitureDesc;
			bool showInShop = bool.Parse(aFurniture.showInShop);
			int furnitureCost = CommonAPI.parseInt(aFurniture.furnitureCost);
			string furnitureType = aFurniture.furnitureType;
			int furnitureLevel = CommonAPI.parseInt(aFurniture.furnitureLevel);
			int furnitureCapacity = CommonAPI.parseInt(aFurniture.furnitureCapacity);
			int shopLevelReq = CommonAPI.parseInt(aFurniture.shopLevelReq);
			int dogLove = CommonAPI.parseInt(aFurniture.dogLove);
			string image = aFurniture.image;
			aData.addFurniture(furnitureRefId, furnitureName, furnitureDesc, showInShop, furnitureCost, furnitureType, furnitureLevel, furnitureCapacity, shopLevelReq, dogLove, image);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefFurniture COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefDecoration(List<RefDecoration> aDecorationArray, List<RefDecorationEffect> aDecorationEffectArray, List<RefDecorationType> aDecorationTypeArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearDecoration();
		int i = 0;
		foreach (RefDecoration aDecoration in aDecorationArray)
		{
			string decorationRefId = aDecoration.decorationRefId;
			string decorationName = aDecoration.decorationName;
			string decorationDesc = aDecoration.decorationDesc;
			string decorationImage = aDecoration.decorationImage;
			int shopLevelReq = CommonAPI.parseInt(aDecoration.shopLevelReq);
			string decorationType = aDecoration.decorationType;
			int decorationShopCost = CommonAPI.parseInt(aDecoration.decorationShopCost);
			UnlockCondition decorationUnlockCondition = CommonAPI.convertStringToUnlockCondition(aDecoration.decorationUnlockCondition);
			int decoReqCount = CommonAPI.parseInt(aDecoration.decoReqCount);
			string decoCheckString = aDecoration.decoCheckString;
			int decoCheckNum = CommonAPI.parseInt(aDecoration.decoCheckNum);
			List<DecorationEffect> effectList = new List<DecorationEffect>();
			foreach (RefDecorationEffect item in aDecorationEffectArray)
			{
				if (item.decorationRefId == decorationRefId)
				{
					string decorationEffectRefId = item.decorationEffectRefId;
					string decorationBoostType = item.decorationBoostType;
					string decorationBoostRefId = item.decorationBoostRefId;
					float aBoostMult = CommonAPI.parseFloat(item.decorationBoostMult);
					effectList.Add(new DecorationEffect(decorationEffectRefId, decorationRefId, decorationBoostType, decorationBoostRefId, aBoostMult));
				}
			}
			bool isSpecial = bool.Parse(aDecoration.isSpecial);
			string scenarioLock = aDecoration.scenarioLock;
			int dlc = CommonAPI.parseInt(aDecoration.dlc);
			string decorationTypeName = string.Empty;
			foreach (RefDecorationType item2 in aDecorationTypeArray)
			{
				if (item2.decorationTypeRefId == decorationType)
				{
					decorationTypeName = item2.decorationTypeName;
					break;
				}
			}
			aData.setDecorationType(aDecorationTypeArray);
			Decoration addDeco = new Decoration(decorationRefId, decorationName, decorationDesc, decorationImage, shopLevelReq, decorationType, decorationTypeName, decorationShopCost, decorationUnlockCondition, decoReqCount, decoCheckString, decoCheckNum, isSpecial, scenarioLock, dlc);
			addDeco.setDecorationEffectList(effectList);
			aData.addDecorationByObj(addDeco);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefDecoration COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefDecorationPosition(List<RefDecorationPosition> aDecorationPosition)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearDecorationPosition();
		int i = 0;
		foreach (RefDecorationPosition aDecoPos in aDecorationPosition)
		{
			string decorationPositionRefId = aDecoPos.decorationPositionRefId;
			string decorationRefId = aDecoPos.decorationRefId;
			string decorationPosition = aDecoPos.decorationPosition;
			float yValue = CommonAPI.parseFloat(aDecoPos.yValue);
			int sortOrder = CommonAPI.parseInt(aDecoPos.sortOrder);
			string flip = aDecoPos.flip;
			string sortLayer = aDecoPos.sortLayer;
			string decorationImage = aDecoPos.decorationImage;
			int shopLevel = CommonAPI.parseInt(aDecoPos.shopLevel);
			aData.addDecorationPosition(decorationPositionRefId, decorationRefId, decorationPosition, yValue, sortOrder, flip, sortLayer, decorationImage, shopLevel);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefDecorationPosition COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefHero(List<RefHero> aHeroArray, List<RefAffinity> aAffinityArray, List<RefJobClassTag> aTagArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearHero();
		int i = 0;
		foreach (RefHero aHero in aHeroArray)
		{
			string heroRefId = aHero.heroRefId;
			string heroName = aHero.heroName;
			string heroDesc = aHero.heroDescription;
			string jobClassName = aHero.jobClassName;
			string jobClassDesc = aHero.jobClassDesc;
			int heroTier = CommonAPI.parseInt(aHero.heroTier);
			string image = aHero.image;
			float baseAtk = CommonAPI.parseFloat(aHero.baseAtk);
			float baseSpd = CommonAPI.parseFloat(aHero.baseSpd);
			float baseAcc = CommonAPI.parseFloat(aHero.baseAcc);
			float baseMag = CommonAPI.parseFloat(aHero.baseMag);
			WeaponStat priStat = CommonAPI.convertStringToWeaponStat(aHero.priStat);
			WeaponStat secStat = CommonAPI.convertStringToWeaponStat(aHero.secStat);
			int wealth = CommonAPI.parseInt(aHero.wealth);
			int sellExp = CommonAPI.parseInt(aHero.sellExp);
			int requestLevelMin = CommonAPI.parseInt(aHero.requestLevelMin);
			int requestLevelMax = CommonAPI.parseInt(aHero.requestLevelMax);
			string requestText = aHero.requestText;
			string rewardSetId = aHero.rewardSetId;
			int initExpPoints = CommonAPI.parseInt(aHero.initExpPoints);
			string scenarioLock = aHero.scenarioLock;
			int dlc = CommonAPI.parseInt(aHero.dlc);
			Dictionary<string, int> typeAffinityList = new Dictionary<string, int>();
			foreach (RefAffinity item in aAffinityArray)
			{
				if (heroRefId == item.heroRefId && !typeAffinityList.ContainsKey(item.weaponTypeRefId))
				{
					typeAffinityList.Add(item.weaponTypeRefId, CommonAPI.parseInt(item.affinity));
				}
			}
			Hero jobClass = new Hero(heroRefId, heroName, heroDesc, jobClassName, jobClassDesc, heroTier, image, baseAtk, baseSpd, baseAcc, baseMag, priStat, secStat, wealth, sellExp, requestLevelMin, requestLevelMax, requestText, typeAffinityList, rewardSetId, initExpPoints, scenarioLock, dlc);
			aData.addHeroByObj(jobClass);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefHero COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefHeroLevel(List<RefHeroLevel> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearHeroLevel();
		int i = 0;
		foreach (RefHeroLevel aLevel in aArray)
		{
			string heroLevelRefId = aLevel.heroLevelRefId;
			int heroLevel = CommonAPI.parseInt(aLevel.heroLevel);
			int levelExp = CommonAPI.parseInt(aLevel.levelExp);
			int levelUpFame = CommonAPI.parseInt(aLevel.levelUpFame);
			aData.addHeroLevel(heroLevelRefId, heroLevel, levelExp, levelUpFame);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefHeroLevel COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefLegendaryHero(List<RefLegendaryHero> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearLegendaryHero();
		int i = 0;
		foreach (RefLegendaryHero aLegend in aArray)
		{
			string legendaryHeroRefId = aLegend.legendaryHeroRefId;
			string legendaryHeroName = aLegend.legendaryHeroName;
			string legendaryHeroDescription = aLegend.legendaryHeroDescription;
			string legendaryQuestName = aLegend.legendaryQuestName;
			string legendaryQuestDescription = aLegend.legendaryQuestDescription;
			string image = aLegend.image;
			string weaponRefId = aLegend.weaponRefId;
			int reqAtk = CommonAPI.parseInt(aLegend.reqAtk);
			int reqSpd = CommonAPI.parseInt(aLegend.reqSpd);
			int reqAcc = CommonAPI.parseInt(aLegend.reqAcc);
			int reqMag = CommonAPI.parseInt(aLegend.reqMag);
			string rewardItemType = aLegend.rewardItemType;
			string rewardItemRefId = aLegend.rewardItemRefId;
			int rewardItemQty = CommonAPI.parseInt(aLegend.rewardItemQty);
			int rewardGold = CommonAPI.parseInt(aLegend.rewardGold);
			int rewardFame = CommonAPI.parseInt(aLegend.rewardFame);
			UnlockCondition unlockCondition = CommonAPI.convertStringToUnlockCondition(aLegend.unlockCondition);
			int unlockConditionValue = CommonAPI.parseInt(aLegend.unlockConditionValue);
			string checkString = aLegend.checkString;
			int checkNum = CommonAPI.parseInt(aLegend.checkNum);
			string successComment = aLegend.successComment;
			string failComment = aLegend.failComment;
			string heroVisitDialogueSetId = aLegend.heroVisitDialogueSetId;
			string forgeFailDialogueSetId = aLegend.forgeFailDialogueSetId;
			string forgeSuccessDialogueRefId = aLegend.forgeSuccessDialogueRefId;
			string scenarioLock = aLegend.scenarioLock;
			int dlc = CommonAPI.parseInt(aLegend.dlc);
			aData.addLegendaryHero(legendaryHeroRefId, legendaryHeroName, legendaryHeroDescription, legendaryQuestName, legendaryQuestDescription, image, weaponRefId, reqAtk, reqSpd, reqAcc, reqMag, rewardItemType, rewardItemRefId, rewardItemQty, rewardGold, rewardFame, unlockCondition, unlockConditionValue, checkString, checkNum, successComment, failComment, heroVisitDialogueSetId, forgeFailDialogueSetId, forgeSuccessDialogueRefId, scenarioLock, dlc);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefLegendaryHero COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefMaxLoyaltyReward(List<RefMaxLoyaltyReward> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearMaxLoyaltyReward();
		int i = 0;
		foreach (RefMaxLoyaltyReward aReward in aArray)
		{
			string maxLoyaltyRewardRefId = aReward.maxLoyaltyRewardRefId;
			int heroTier = CommonAPI.parseInt(aReward.heroTier);
			int heroCount = CommonAPI.parseInt(aReward.heroCount);
			bool isSpecial = bool.Parse(aReward.isSpecial);
			string rewardType = aReward.rewardType;
			string rewardRefId = aReward.rewardRefId;
			int rewardNum = CommonAPI.parseInt(aReward.rewardNum);
			aData.addMaxLoyaltyReward(maxLoyaltyRewardRefId, heroTier, heroCount, isSpecial, rewardType, rewardRefId, rewardNum);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefMaxLoyaltyReward COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefRequest(List<RefRequest> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearRequest();
		int i = 0;
		foreach (RefRequest aRequest in aArray)
		{
			string requestRefId = aRequest.requestRefId;
			int requestLevel = CommonAPI.parseInt(aRequest.requestLevel);
			string requestReq1 = aRequest.requestReq1;
			string requestReq2 = aRequest.requestReq2;
			int requestDuration = CommonAPI.parseInt(aRequest.requestDuration);
			int requestBaseGold = CommonAPI.parseInt(aRequest.requestBaseGold);
			int requestBaseLoyalty = CommonAPI.parseInt(aRequest.requestBaseLoyalty);
			int requestBaseFame = CommonAPI.parseInt(aRequest.requestBaseFame);
			string requestRewardSet = aRequest.requestRewardSet;
			int requestRewardQty = CommonAPI.parseInt(aRequest.requestRewardQty);
			aData.addRequest(requestRefId, requestLevel, requestReq1, requestReq2, requestDuration, requestBaseGold, requestBaseLoyalty, requestBaseFame, requestRewardSet, requestRewardQty);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefMaxLoyaltyReward COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefJobUnlockNEW(List<RefJobUnlockNEW> aUnlockArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearJobUnlock();
		int i = 0;
		foreach (RefJobUnlockNEW aUnlock in aUnlockArray)
		{
			string jobUnlockRefId = aUnlock.jobUnlockRefId;
			string requiredJobClassRefId = aUnlock.requiredJobClassRefId;
			string requiredWeaponRefId = aUnlock.requiredWeaponRefId;
			string key = requiredJobClassRefId + "@" + requiredWeaponRefId;
			string unlockJobClassRefId = aUnlock.unlockJobClassRefId;
			aData.addJobUnlock(key, jobUnlockRefId, requiredJobClassRefId, requiredWeaponRefId, unlockJobClassRefId);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefJobUnlockNEW COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefItem(List<RefItem> aItemList, List<RefDogItem> aDogItemList)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearItem();
		int i = 0;
		foreach (RefItem aItem in aItemList)
		{
			string itemRefId = aItem.itemRefId;
			string itemName = aItem.itemName;
			string itemDesc = aItem.itemDesc;
			int itemCost = CommonAPI.parseInt(aItem.itemCost);
			int itemBuyExp = CommonAPI.parseInt(aItem.itemBuyExp);
			int itemSellPrice = CommonAPI.parseInt(aItem.itemSellPrice);
			ItemType itemType = CommonAPI.convertStringToItemType(aItem.itemType);
			bool isSpecial = bool.Parse(aItem.isSpecial);
			WeaponStat effectStat = CommonAPI.convertStringToWeaponStat(aItem.effectStat);
			Element element = CommonAPI.convertStringToElement(aItem.element);
			string scenarioLock = aItem.scenarioLock;
			int effectValue = CommonAPI.parseInt(aItem.effectValue);
			string effectString = aItem.effectString;
			string image = aItem.image;
			int reqShopLevel = 0;
			int reqMonths = 0;
			int reqDogLove = 0;
			int chance = 0;
			foreach (RefDogItem aDogItem in aDogItemList)
			{
				if (itemRefId == aDogItem.itemRefId)
				{
					reqShopLevel = CommonAPI.parseInt(aDogItem.reqShopLevel);
					reqMonths = CommonAPI.parseInt(aDogItem.reqMonths);
					reqDogLove = CommonAPI.parseInt(aDogItem.reqDogLove);
					chance = CommonAPI.parseInt(aDogItem.chance);
				}
			}
			aData.addItem(itemRefId, itemRefId, itemName, itemDesc, itemCost, itemBuyExp, itemSellPrice, itemType, isSpecial, effectStat, element, scenarioLock, effectValue, effectString, image, 0, reqShopLevel, reqMonths, reqDogLove, chance);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefItem COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefSmithJobClass(List<RefSmithJobClass> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearSmithJobClass();
		int i = 0;
		foreach (RefSmithJobClass aSmithJobClass in aArray)
		{
			string smithJobClassRefId = aSmithJobClass.smithJobClassRefId;
			string smithJobName = aSmithJobClass.smithJobName;
			string smithJobDesc = aSmithJobClass.smithJobDesc;
			int smithJobChangeCost = CommonAPI.parseInt(aSmithJobClass.smithJobChangeCost);
			int maxLevel = CommonAPI.parseInt(aSmithJobClass.maxLevel);
			int salaryMult = CommonAPI.parseInt(aSmithJobClass.salaryMult);
			float powMult = CommonAPI.parseFloat(aSmithJobClass.powMult);
			float intMult = CommonAPI.parseFloat(aSmithJobClass.intMult);
			float tecMult = CommonAPI.parseFloat(aSmithJobClass.tecMult);
			float lucMult = CommonAPI.parseFloat(aSmithJobClass.lucMult);
			bool canDesign = bool.Parse(aSmithJobClass.canDesign);
			bool canCraft = bool.Parse(aSmithJobClass.canCraft);
			bool canPolish = bool.Parse(aSmithJobClass.canPolish);
			bool canEnchant = bool.Parse(aSmithJobClass.canEnchant);
			string[] unlockRequirementList = aSmithJobClass.unlockRequirement.Split('@');
			Dictionary<string, int> unlockJobClassList = new Dictionary<string, int>();
			string[] array = unlockRequirementList;
			foreach (string text in array)
			{
				string[] array2 = text.Split('#');
				if (array2.Length > 1 && !unlockJobClassList.ContainsKey(array2[0]))
				{
					unlockJobClassList.Add(array2[0], CommonAPI.parseInt(array2[1]));
				}
			}
			int basePermPow = CommonAPI.parseInt(aSmithJobClass.basePermPow);
			float growthPermPow = CommonAPI.parseFloat(aSmithJobClass.growthPermPow);
			int basePermInt = CommonAPI.parseInt(aSmithJobClass.basePermInt);
			float growthPermInt = CommonAPI.parseFloat(aSmithJobClass.growthPermInt);
			int basePermTec = CommonAPI.parseInt(aSmithJobClass.basePermTec);
			float growthPermTec = CommonAPI.parseFloat(aSmithJobClass.growthPermTec);
			int basePermLuc = CommonAPI.parseInt(aSmithJobClass.basePermLuc);
			float growthPermLuc = CommonAPI.parseFloat(aSmithJobClass.growthPermLuc);
			int basePermSta = CommonAPI.parseInt(aSmithJobClass.basePermSta);
			float growthPermSta = CommonAPI.parseFloat(aSmithJobClass.growthPermSta);
			float expGrowthType = CommonAPI.parseFloat(aSmithJobClass.expGrowthType);
			int baseExp = CommonAPI.parseInt(aSmithJobClass.baseExp);
			float growthExp = CommonAPI.parseFloat(aSmithJobClass.growthExp);
			aData.addSmithJobClass(smithJobClassRefId, smithJobName, smithJobDesc, smithJobChangeCost, maxLevel, salaryMult, powMult, intMult, tecMult, lucMult, canDesign, canCraft, canPolish, canEnchant, unlockJobClassList, basePermPow, growthPermPow, basePermInt, growthPermInt, basePermTec, growthPermTec, basePermLuc, growthPermLuc, basePermSta, growthPermSta, expGrowthType, baseExp, growthExp);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefSmithJobClass COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefSmithTraining(List<RefSmithTraining> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearSmithTraining();
		int i = 0;
		foreach (RefSmithTraining aSmithTraining in aArray)
		{
			string smithJobClassRefId = aSmithTraining.smithTrainingRefId;
			string smithTrainingName = aSmithTraining.smithTrainingName;
			string smithTrainingDescription = aSmithTraining.smithTrainingDescription;
			int smithTrainingExp = CommonAPI.parseInt(aSmithTraining.smithTrainingExp);
			int smithTrainingCost = CommonAPI.parseInt(aSmithTraining.smithTrainingCost);
			int smithTrainingStamina = CommonAPI.parseInt(aSmithTraining.smithTrainingStamina);
			int unlockTime = CommonAPI.parseInt(aSmithTraining.unlockTime);
			int unlockShopLevel = CommonAPI.parseInt(aSmithTraining.unlockShopLevel);
			aData.addSmithTraining(smithJobClassRefId, smithTrainingName, smithTrainingDescription, smithTrainingExp, smithTrainingCost, smithTrainingStamina, unlockTime, unlockShopLevel);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefSmithTraining COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefTrainingPackage(List<RefTrainingPackage> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearTrainingPackage();
		int i = 0;
		foreach (RefTrainingPackage aPackage in aArray)
		{
			string trainingPackageRefID = aPackage.trainingPackageRefID;
			string summer = aPackage.summer;
			string autumn = aPackage.autumn;
			string spring = aPackage.spring;
			string winter = aPackage.winter;
			aData.addTrainingPackage(trainingPackageRefID, summer, autumn, spring, winter);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefTrainingPackage COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefWeapon(List<RefWeapon> aArray, List<RefWeaponMaterial> aMatArray, List<RefWeaponRelic> aRelicArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearWeapon();
		int i = 0;
		foreach (RefWeapon aWeapon in aArray)
		{
			string weaponRefId = aWeapon.weaponRefId;
			string weaponName = aWeapon.weaponName;
			string weaponDesc = aWeapon.weaponDesc;
			string image = aWeapon.image;
			string weaponType = aWeapon.weaponType;
			float atkMult = CommonAPI.parseFloat(aWeapon.atkMult);
			float spdMult = CommonAPI.parseFloat(aWeapon.spdMult);
			float accMult = CommonAPI.parseFloat(aWeapon.accMult);
			float magMult = CommonAPI.parseFloat(aWeapon.magMult);
			WeaponStat researchType = CommonAPI.convertStringToWeaponStat(aWeapon.researchType);
			int researchDuration = CommonAPI.parseInt(aWeapon.researchDuration);
			int researchCost = CommonAPI.parseInt(aWeapon.researchCost);
			float researchMood = CommonAPI.parseFloat(aWeapon.researchMood);
			Dictionary<string, int> matList = new Dictionary<string, int>();
			foreach (RefWeaponMaterial item in aMatArray)
			{
				if (item.weaponRefId == weaponRefId && !matList.ContainsKey(item.matRefId))
				{
					matList.Add(item.matRefId, CommonAPI.parseInt(item.matNum));
				}
			}
			List<string> relicList = new List<string>();
			foreach (RefWeaponRelic item2 in aRelicArray)
			{
				if (item2.weaponRefId == weaponRefId && !relicList.Contains(item2.relicRefId))
				{
					relicList.Add(item2.relicRefId);
				}
			}
			int dlc = CommonAPI.parseInt(aWeapon.dlc);
			string scenarioLock = aWeapon.scenarioLock;
			Weapon weapon = new Weapon(weaponRefId, weaponName, weaponDesc, image, weaponType, atkMult, spdMult, accMult, magMult, matList, relicList, researchType, researchDuration, researchCost, researchMood, dlc, scenarioLock);
			aData.addWeaponByObj(weapon);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefWeapon COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefWeaponType(List<RefWeaponType> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearWeaponType();
		int i = 0;
		foreach (RefWeaponType aWeaponType in aArray)
		{
			string weaponTypeRefId = aWeaponType.weaponTypeRefId;
			string weaponTypeName = aWeaponType.weaponTypeName;
			string skillName = aWeaponType.skillName;
			string firstWeaponRefId = aWeaponType.firstWeaponRefId;
			float scoreMultAtk = CommonAPI.parseFloat(aWeaponType.scoreMultAtk);
			float scoreMultSpd = CommonAPI.parseFloat(aWeaponType.scoreMultSpd);
			float scoreMultAcc = CommonAPI.parseFloat(aWeaponType.scoreMultAcc);
			float scoreMultMag = CommonAPI.parseFloat(aWeaponType.scoreMultMag);
			string image = aWeaponType.image;
			aData.addWeaponType(weaponTypeRefId, weaponTypeName, skillName, firstWeaponRefId, scoreMultAtk, scoreMultSpd, scoreMultAcc, scoreMultMag, image);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefWeaponType COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefShopLevel(List<RefShopLevel> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearShopLevel();
		int i = 0;
		foreach (RefShopLevel aShopLevel in aArray)
		{
			string shopLevelRefId = aShopLevel.shopLevelRefId;
			string shopLevelName = aShopLevel.shopLevelName;
			int shopCapacity = CommonAPI.parseInt(aShopLevel.shopCapacity);
			int shopUpgradeCost = CommonAPI.parseInt(aShopLevel.shopUpgradeCost);
			int shopMonthRequired = CommonAPI.parseInt(aShopLevel.shopMonthRequired);
			float shopMoodReduceRate = CommonAPI.parseFloat(aShopLevel.shopMoodReduceRate);
			string nextShopLevelRefId = aShopLevel.nextShopLevelRefId;
			int width = CommonAPI.parseInt(aShopLevel.width);
			int height = CommonAPI.parseInt(aShopLevel.height);
			string startingCoor = aShopLevel.startingCoor;
			string coffeeCoor = aShopLevel.coffeeCoor;
			string researchCoor = aShopLevel.researchCoor;
			string portalCoor = aShopLevel.portalCoor;
			string spotIndicatorImage = aShopLevel.spotIndicatorImage;
			string floor = aShopLevel.floor;
			string wallLeft = aShopLevel.wallLeft;
			string wallRight = aShopLevel.wallRight;
			string floorPosition = aShopLevel.floorPosition;
			string wallLeftPosition = aShopLevel.wallLeftPosition;
			string wallRightPosition = aShopLevel.wallRightPosition;
			string bgPosition = aShopLevel.bgPosition;
			aData.addShopLevel(shopLevelRefId, shopLevelName, shopCapacity, shopUpgradeCost, shopMonthRequired, shopMoodReduceRate, nextShopLevelRefId, width, height, startingCoor, coffeeCoor, researchCoor, portalCoor, spotIndicatorImage, floor, wallLeft, wallRight, floorPosition, wallLeftPosition, wallRightPosition, bgPosition);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefShopLevel COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefRecruitmentType(List<RefRecruitmentType> aRecruitmentTypeArray, List<RefRecruitmentSmith> aRecruitmentSmithArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearRecruitmentType();
		int i = 0;
		foreach (RefRecruitmentType aRecType in aRecruitmentTypeArray)
		{
			string recruitmentRefId = aRecType.recruitmentRefId;
			string recruitmentName = aRecType.recruitmentName;
			string recruitmentDesc = aRecType.recruitmentDesc;
			int recruitmentCost = CommonAPI.parseInt(aRecType.recruitmentCost);
			int recruitmentMaxNum = CommonAPI.parseInt(aRecType.recruitmentMaxNum);
			int recruitmentDuration = CommonAPI.parseInt(aRecType.recruitmentDuration);
			int shopLevelReq = CommonAPI.parseInt(aRecType.shopLevelReq);
			int monthReq = CommonAPI.parseInt(aRecType.monthReq);
			List<string> recruitSmithList = new List<string>();
			foreach (RefRecruitmentSmith item in aRecruitmentSmithArray)
			{
				if (item.recruitmentTypeRefId == recruitmentRefId)
				{
					recruitSmithList.Add(item.recruitmentSmithRefId);
				}
			}
			aData.addRecruitmentType(recruitmentRefId, recruitmentName, recruitmentDesc, recruitmentCost, recruitmentMaxNum, recruitmentDuration, shopLevelReq, monthReq, recruitSmithList);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefRecruitmentType COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefForgeIncident(List<RefForgeIncident> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearForgeIncident();
		int i = 0;
		foreach (RefForgeIncident aIncident in aArray)
		{
			string incidentRefId = aIncident.incidentRefId;
			string incidentName = aIncident.incidentName;
			string incidentDesc = aIncident.incidentDesc;
			IncidentType incidentType = CommonAPI.convertStringToIncidentType(aIncident.incidentType);
			float incidentMagnitude = CommonAPI.parseFloat(aIncident.incidentMagnitude);
			string incidentImage = aIncident.incidentImage;
			int monthReq = CommonAPI.parseInt(aIncident.monthReq);
			aData.addForgeIncident(incidentRefId, incidentName, incidentDesc, incidentType, incidentMagnitude, incidentImage, monthReq);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefForgeIncident COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefQuestNEW(List<RefQuestNEW> aQuestArray, List<RefQuestTag> aTagArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearQuestNEW();
		int i = 0;
		foreach (RefQuestNEW aQuestNEW in aQuestArray)
		{
			string questRefId = aQuestNEW.questRefId;
			string questName = aQuestNEW.questName;
			string questDesc = aQuestNEW.questDesc;
			string questEndText = aQuestNEW.questEndText;
			string questGiverEndText = aQuestNEW.questGiverEndText;
			string questUnlockCutscene = aQuestNEW.questUnlockCutscene;
			string questEndCutscene = aQuestNEW.questEndCutscene;
			QuestNEWType questType = CommonAPI.convertDataStringToQuestNEWType(aQuestNEW.questType);
			string objectiveRefId = aQuestNEW.objectiveRefId;
			bool clearWithObjective = bool.Parse(aQuestNEW.clearWithObjective);
			UnlockCondition unlockCondition = CommonAPI.convertStringToUnlockCondition(aQuestNEW.unlockCondition);
			int unlockValue = CommonAPI.parseInt(aQuestNEW.unlockValue);
			string[] lockQuests = aQuestNEW.lockQuests.Split('@');
			int questTimeLimit = CommonAPI.parseInt(aQuestNEW.questTimeLimit);
			int forgeTimeLimit = CommonAPI.parseInt(aQuestNEW.forgeTimeLimit);
			string questJobClass = aQuestNEW.questJobClass;
			string questWeapon = aQuestNEW.questWeapon;
			int atkReq = CommonAPI.parseInt(aQuestNEW.atkReq);
			int spdReq = CommonAPI.parseInt(aQuestNEW.spdReq);
			int accReq = CommonAPI.parseInt(aQuestNEW.accReq);
			int magReq = CommonAPI.parseInt(aQuestNEW.magReq);
			int rewardPoint = CommonAPI.parseInt(aQuestNEW.rewardPoint);
			bool pointIsVariable = bool.Parse(aQuestNEW.pointIsVariable);
			int rewardGold = CommonAPI.parseInt(aQuestNEW.rewardGold);
			int alignLaw = CommonAPI.parseInt(aQuestNEW.alignLaw);
			int alignChaos = CommonAPI.parseInt(aQuestNEW.alignChaos);
			string questGiverName = aQuestNEW.questGiverName;
			string questGiverImage = aQuestNEW.questGiverImage;
			int completeCountLimit = CommonAPI.parseInt(aQuestNEW.completeCountLimit);
			string terrainRefId = aQuestNEW.terrainRefId;
			int milestoneNum = CommonAPI.parseInt(aQuestNEW.milestoneNum);
			int questTime = CommonAPI.parseInt(aQuestNEW.questTime);
			int minQuestGold = CommonAPI.parseInt(aQuestNEW.minQuestGold);
			QuestNEW quest = new QuestNEW(questRefId, questName, questDesc, questEndText, questGiverEndText, questUnlockCutscene, questEndCutscene, questType, objectiveRefId, clearWithObjective, unlockCondition, unlockValue, lockQuests, questTimeLimit, forgeTimeLimit, questJobClass, questWeapon, atkReq, spdReq, accReq, magReq, rewardPoint, pointIsVariable, rewardGold, alignLaw, alignChaos, questGiverName, questGiverImage, completeCountLimit, terrainRefId, milestoneNum, questTime, minQuestGold);
			List<QuestTag> questTagList = new List<QuestTag>();
			foreach (RefQuestTag item in aTagArray)
			{
				if (item.questRefId == questRefId)
				{
					string questTagRefId = item.questTagRefId;
					string tagRefId = item.tagRefId;
					string rewardChestRefId = item.rewardChestRefId;
					int aSetNum = CommonAPI.parseInt(item.setNum);
					bool aVisible = bool.Parse(item.isVisible);
					questTagList.Add(new QuestTag(questTagRefId, tagRefId, questRefId, rewardChestRefId, aSetNum, aVisible));
				}
			}
			quest.setQuestTagList(questTagList);
			aData.addQuestNEWObj(quest);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefQuestNEW COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefContract(List<RefContract> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearContracts();
		int i = 0;
		foreach (RefContract aContract in aArray)
		{
			string contractRefId = aContract.contractRefId;
			string contractName = aContract.contractName;
			string contractDesc = aContract.contractDesc;
			int rewardGold = CommonAPI.parseInt(aContract.rewardGold);
			int timeLimit = CommonAPI.parseInt(aContract.timeLimit);
			int atkReq = CommonAPI.parseInt(aContract.atkReq);
			int spdReq = CommonAPI.parseInt(aContract.spdReq);
			int accReq = CommonAPI.parseInt(aContract.accReq);
			int magReq = CommonAPI.parseInt(aContract.magReq);
			int monthStart = CommonAPI.parseInt(aContract.monthStart);
			int monthEnd = CommonAPI.parseInt(aContract.monthEnd);
			aData.addContract(contractRefId, contractName, contractDesc, rewardGold, timeLimit, atkReq, spdReq, accReq, magReq, monthStart, monthEnd);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefContract COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefObjective(List<RefObjective> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearObjective();
		int i = 0;
		foreach (RefObjective aObjective in aArray)
		{
			string objectiveRefId = aObjective.objectiveRefId;
			string objectiveName = aObjective.objectiveName;
			string objectiveDesc = aObjective.objectiveDesc;
			int timeLimit = CommonAPI.parseInt(aObjective.timeLimit);
			string startDialogueRefId = aObjective.startDialogueRefId;
			string successDialogueRefId = aObjective.successDialogueRefId;
			string successNextObjective = aObjective.successNextObjective;
			string failDialogueRefId = aObjective.failDialogueRefId;
			string failNextObjective = aObjective.failNextObjective;
			UnlockCondition successCondition = CommonAPI.convertStringToUnlockCondition(aObjective.successCondition);
			int reqCount = CommonAPI.parseInt(aObjective.reqCount);
			string checkString = aObjective.checkString;
			int checkNum = CommonAPI.parseInt(aObjective.checkNum);
			bool countFromObjectiveStart = bool.Parse(aObjective.countFromObjectiveStart);
			bool countAsObjective = bool.Parse(aObjective.countAsObjective);
			string objectiveSet = aObjective.objectiveSet;
			aData.addObjective(objectiveRefId, objectiveName, objectiveDesc, timeLimit, startDialogueRefId, successDialogueRefId, successNextObjective, failDialogueRefId, failNextObjective, successCondition, reqCount, checkString, checkNum, countFromObjectiveStart, countAsObjective, objectiveSet);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefObjective COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefTutorial(List<RefTutorial> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearTutorial();
		int i = 0;
		foreach (RefTutorial aTutorial in aArray)
		{
			string tutorialRefId = aTutorial.tutorialRefId;
			string tutorialSetRefId = aTutorial.tutorialSetRefId;
			int tutorialOrderIndex = CommonAPI.parseInt(aTutorial.tutorialOrderIndex);
			string tutorialTitle = aTutorial.tutorialTitle;
			string tutorialText = aTutorial.tutorialText;
			string tutorialTexturePath = aTutorial.tutorialTexturePath;
			float tutorialXPos = CommonAPI.parseFloat(aTutorial.tutorialXPos);
			float tutorialYPos = CommonAPI.parseFloat(aTutorial.tutorialYPos);
			aData.addTutorial(tutorialRefId, tutorialSetRefId, tutorialOrderIndex, tutorialTitle, tutorialText, tutorialTexturePath, tutorialXPos, tutorialYPos);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefTutorial COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefWeather(List<RefWeather> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearWeather();
		int i = 0;
		foreach (RefWeather aWeather in aArray)
		{
			string weatherRefId = aWeather.weatherRefId;
			string weatherName = aWeather.weatherName;
			string weatherText = aWeather.weatherText;
			bool showWhetsapp = bool.Parse(aWeather.showWhetsapp);
			Season season = CommonAPI.convertStringToSeason(aWeather.season);
			int weatherChance = CommonAPI.parseInt(aWeather.weatherChance);
			string scenarioLock = aWeather.scenarioLock;
			int monthReq = CommonAPI.parseInt(aWeather.monthReq);
			string image = aWeather.image;
			aData.addWeather(weatherRefId, weatherName, weatherText, showWhetsapp, season, weatherChance, scenarioLock, monthReq, image);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefWeather COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefDayEndScenario(List<RefDayEndScenario> aScenarioArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearScenario();
		int i = 0;
		foreach (RefDayEndScenario aScenario in aScenarioArray)
		{
			string scenarioRefId = aScenario.scenarioRefId;
			string scenarioText = aScenario.scenarioText;
			string scenarioChoice1 = aScenario.scenarioChoice1;
			string scenarioChoice2 = aScenario.scenarioChoice2;
			bool hasProject = bool.Parse(aScenario.hasProject);
			UnlockCondition unlockCondition = CommonAPI.convertStringToUnlockCondition(aScenario.unlockCondition);
			int reqCount = CommonAPI.parseInt(aScenario.reqCount);
			string checkString = aScenario.checkString;
			int checkNum = CommonAPI.parseInt(aScenario.checkNum);
			int endCount = CommonAPI.parseInt(aScenario.endCount);
			string scenarioChoice1Success = aScenario.scenarioChoice1Success;
			int choice1SuccessChance = CommonAPI.parseInt(aScenario.choice1SuccessChance);
			ScenarioEffect choice1SuccessEffect = CommonAPI.convertStringToScenarioEffect(aScenario.choice1SuccessEffect);
			float choice1SuccessValue = CommonAPI.parseFloat(aScenario.choice1SuccessValue);
			string scenarioChoice1Failure = aScenario.scenarioChoice1Failure;
			int choice1FailureChance = CommonAPI.parseInt(aScenario.choice1FailureChance);
			ScenarioEffect choice1FailureEffect = CommonAPI.convertStringToScenarioEffect(aScenario.choice1FailureEffect);
			float choice1FailureValue = CommonAPI.parseFloat(aScenario.choice1FailureValue);
			string scenarioChoice2Success = aScenario.scenarioChoice2Success;
			int choice2SuccessChance = CommonAPI.parseInt(aScenario.choice2SuccessChance);
			ScenarioEffect choice2SuccessEffect = CommonAPI.convertStringToScenarioEffect(aScenario.choice2SuccessEffect);
			float choice2SuccessValue = CommonAPI.parseFloat(aScenario.choice2SuccessValue);
			string scenarioChoice2Failure = aScenario.scenarioChoice2Failure;
			int choice2FailureChance = CommonAPI.parseInt(aScenario.choice2FailureChance);
			ScenarioEffect choice2FailureEffect = CommonAPI.convertStringToScenarioEffect(aScenario.choice2FailureEffect);
			float choice2FailureValue = CommonAPI.parseFloat(aScenario.choice2FailureValue);
			aData.addScenario(scenarioRefId, scenarioText, scenarioChoice1, scenarioChoice2, hasProject, unlockCondition, reqCount, checkString, checkNum, endCount, scenarioChoice1Success, choice1SuccessChance, choice1SuccessEffect, choice1SuccessValue, scenarioChoice1Failure, choice1FailureChance, choice1FailureEffect, choice1FailureValue, scenarioChoice2Success, choice2SuccessChance, choice2SuccessEffect, choice2SuccessValue, scenarioChoice2Failure, choice2FailureChance, choice2FailureEffect, choice2FailureValue);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefDayEndScenario COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefSmithAction(List<RefSmithAction> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearSmithAction();
		int i = 0;
		foreach (RefSmithAction aSmithAction in aArray)
		{
			string smithActionRefId = aSmithAction.smithActionRefId;
			string smithActionText = aSmithAction.smithActionText;
			SmithActionState smithActionState = CommonAPI.convertStringToSmithActionState(aSmithAction.smithActionState);
			bool isAllowedWhenWorking = bool.Parse(aSmithAction.isAllowedWhenWorking);
			bool isAllowedWhenIdle = bool.Parse(aSmithAction.isAllowedWhenIdle);
			string requiredWeather = aSmithAction.requiredWeather;
			float hpBelow = CommonAPI.parseFloat(aSmithAction.hpBelow);
			int actionChance = CommonAPI.parseInt(aSmithAction.actionChance);
			int durationMin = CommonAPI.parseInt(aSmithAction.durationMin);
			int durationMax = CommonAPI.parseInt(aSmithAction.durationMax);
			StatEffect statEffect = CommonAPI.convertStringToStatEffect(aSmithAction.statEffect);
			float effectMin = CommonAPI.parseFloat(aSmithAction.effectMin);
			float effectMax = CommonAPI.parseFloat(aSmithAction.effectMax);
			string itemRequired = aSmithAction.itemRequired;
			string itemPrevent = aSmithAction.itemPrevent;
			int itemMinLevel = CommonAPI.parseInt(aSmithAction.itemMinLevel);
			string image = aSmithAction.image;
			aData.addSmithAction(smithActionRefId, smithActionText, smithActionState, isAllowedWhenWorking, isAllowedWhenIdle, requiredWeather, hpBelow, actionChance, durationMin, durationMax, statEffect, effectMin, effectMax, itemRequired, itemPrevent, itemMinLevel, image);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefSmithAction COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefSmithStatusEffect(List<RefSmithStatusEffect> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearSmithStatusEffect();
		int i = 0;
		foreach (RefSmithStatusEffect aStatus in aArray)
		{
			string smithEffectRefId = aStatus.smithEffectRefId;
			string effectName = aStatus.effectName;
			string effectComment = aStatus.effectComment;
			string effectDescription = aStatus.effectDescription;
			StatEffect effect1Type = CommonAPI.convertStringToStatEffect(aStatus.effect1Type);
			float effect1Value = CommonAPI.parseFloat(aStatus.effect1Value);
			StatEffect effect2Type = CommonAPI.convertStringToStatEffect(aStatus.effect2Type);
			float effect2Value = CommonAPI.parseFloat(aStatus.effect2Value);
			int effectDuration = CommonAPI.parseInt(aStatus.effectDuration);
			aData.addSmithStatusEffect(smithEffectRefId, effectName, effectComment, effectDescription, effect1Type, effect1Value, effect2Type, effect2Value, effectDuration);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefSmithStatusEffect COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefSmith(List<RefSmith> aSmithArray, List<RefSmithExperience> aExpArray, List<RefSmithTag> aTagArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearSmith();
		aData.clearOutsourceSmith();
		int i = 0;
		foreach (RefSmith aSmith in aSmithArray)
		{
			string smithRefId = aSmith.smithRefId;
			string smithName = aSmith.smithName;
			string smithDesc = aSmith.smithDesc;
			SmithGender smithGender = CommonAPI.convertStringToGender(aSmith.smithGender);
			bool isOutsource = bool.Parse(aSmith.isOutsource);
			SmithJobClass initJob = aData.getSmithJobClass(aSmith.initJob);
			bool initUnlock = bool.Parse(aSmith.initUnlock);
			UnlockCondition unlockCondition = CommonAPI.convertStringToUnlockCondition(aSmith.unlockCondition);
			int unlockConditionValue = CommonAPI.parseInt(aSmith.unlockConditionValue);
			string checkString = aSmith.checkString;
			int checkNum = CommonAPI.parseInt(aSmith.checkNum);
			string unlockDialogueSetId = aSmith.unlockDialogueSetId;
			float growthType = CommonAPI.parseFloat(aSmith.growthType);
			int basePower = CommonAPI.parseInt(aSmith.basePower);
			int baseIntelligence = CommonAPI.parseInt(aSmith.baseIntelligence);
			int baseTechnique = CommonAPI.parseInt(aSmith.baseTechnique);
			int baseLuck = CommonAPI.parseInt(aSmith.baseLuck);
			int baseStamina = CommonAPI.parseInt(aSmith.baseStamina);
			int growthPower = CommonAPI.parseInt(aSmith.growthPower);
			int growthIntelligence = CommonAPI.parseInt(aSmith.growthIntelligence);
			int growthTechnique = CommonAPI.parseInt(aSmith.growthTechnique);
			int growthLuck = CommonAPI.parseInt(aSmith.growthLuck);
			float growthStamina = CommonAPI.parseFloat(aSmith.growthStamina);
			int hireCost = CommonAPI.parseInt(aSmith.hireCost);
			float salaryGrowthType = CommonAPI.parseFloat(aSmith.salaryGrowthType);
			int baseSalary = CommonAPI.parseInt(aSmith.baseSalary);
			int growthSalary = CommonAPI.parseInt(aSmith.growthSalary);
			string preferredAction = aSmith.preferredAction;
			int preferredActionChance = CommonAPI.parseInt(aSmith.preferredActionChance);
			string image = aSmith.image;
			string scenarioLock = aSmith.scenarioLock;
			int dlc = CommonAPI.parseInt(aSmith.dlc);
			List<SmithExperience> smithExpList = new List<SmithExperience>();
			foreach (RefSmithExperience item in aExpArray)
			{
				if (smithRefId == item.smithRefId)
				{
					string smithExperienceRefId = item.smithExperienceRefId;
					string smithJobClassRefId = item.smithJobClassRefId;
					string maxLevelTagRefId = item.maxLevelTagRefId;
					int aInitLevel = CommonAPI.parseInt(item.initLevel);
					smithExpList.Add(new SmithExperience(smithExperienceRefId, smithRefId, smithJobClassRefId, maxLevelTagRefId, aInitLevel));
				}
			}
			List<SmithTag> smithTagList = new List<SmithTag>();
			foreach (RefSmithTag item2 in aTagArray)
			{
				if (smithRefId == item2.smithRefId)
				{
					string smithTagRefId = item2.smithTagRefId;
					string tagRefId = item2.tagRefId;
					bool aReplaceable = bool.Parse(item2.replaceable);
					smithTagList.Add(new SmithTag(smithTagRefId, tagRefId, smithRefId, aReplaceable));
				}
			}
			Smith addSmith = new Smith(smithRefId, smithRefId, smithName, smithDesc, smithGender, isOutsource, initJob, initUnlock, unlockCondition, unlockConditionValue, checkString, checkNum, unlockDialogueSetId, hireCost, salaryGrowthType, baseSalary, growthSalary, preferredAction, preferredActionChance, image, growthType, basePower, baseIntelligence, baseTechnique, baseLuck, growthPower, growthIntelligence, growthTechnique, growthLuck, baseStamina, growthStamina, scenarioLock, dlc);
			addSmith.setExperienceList(smithExpList);
			addSmith.setSmithTagList(smithTagList);
			if (!isOutsource)
			{
				aData.addSmithByObject(addSmith);
			}
			else
			{
				aData.addOutsourceSmithByObject(addSmith);
			}
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefSmith COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefStation(List<RefStation> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearStation();
		int i = 0;
		foreach (RefStation aStation in aArray)
		{
			string refStationID = aStation.refStationID;
			string stationPoint = aStation.stationPoint;
			string endPoint = aStation.endPoint;
			string phase = aStation.phase;
			int shopLevel = CommonAPI.parseInt(aStation.shopLevel);
			int furnitureLevel = CommonAPI.parseInt(aStation.furnitureLevel);
			string obstacleRefID = aStation.obstacleRefID;
			string dogStationPoint = aStation.dogStationPoint;
			aData.addStation(refStationID, stationPoint, endPoint, phase, shopLevel, furnitureLevel, obstacleRefID, dogStationPoint);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefStation COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefPath(List<RefPath> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearCharacterPath();
		int i = 0;
		foreach (RefPath aPath in aArray)
		{
			string refPathID = aPath.refPathID;
			string startingPoint = aPath.startingPoint;
			string endPoint = aPath.endPoint;
			string smithActionRefID = aPath.smithActionRefID;
			string dogActionRefID = aPath.dogActionRefID;
			string path = aPath.path;
			float yValue = CommonAPI.parseFloat(aPath.yValue);
			string endView = aPath.endView;
			int shopLevel = CommonAPI.parseInt(aPath.shopLevel);
			aData.addCharacterPath(refPathID, startingPoint, endPoint, smithActionRefID, dogActionRefID, path, yValue, endView, shopLevel);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefPath COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefObstacles(List<RefObstacles> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearObstacle();
		int i = 0;
		foreach (RefObstacles aObstacle in aArray)
		{
			string refObstacleID = aObstacle.refObstaclesID;
			string obstaclePoint = aObstacle.obstaclePoint;
			float yValue = CommonAPI.parseFloat(aObstacle.yValue);
			int width = CommonAPI.parseInt(aObstacle.width);
			int height = CommonAPI.parseInt(aObstacle.height);
			string endView = aObstacle.endView;
			int sortOrder = CommonAPI.parseInt(aObstacle.sortOrder);
			string imageLocked = aObstacle.imageLocked;
			string imageUnlocked = aObstacle.imageUnlocked;
			int shopLevel = CommonAPI.parseInt(aObstacle.shopLevel);
			int furnitureLevel = CommonAPI.parseInt(aObstacle.furnitureLevel);
			string furnitureRefID = aObstacle.furnitureRefID;
			aData.addObstacle(refObstacleID, obstaclePoint, yValue, width, height, endView, sortOrder, imageLocked, imageUnlocked, shopLevel, furnitureLevel, furnitureRefID);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefObstacles COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefEnemy(List<RefEnemy> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearEnemy();
		int i = 0;
		foreach (RefEnemy aEnemy in aArray)
		{
			string enemyRefId = aEnemy.enemyRefId;
			string terrainRefId = aEnemy.terrainRefId;
			string enemyName = aEnemy.enemyName;
			string enemyImage = aEnemy.enemyImage;
			int enemyGoldMin = CommonAPI.parseInt(aEnemy.enemyGoldMin);
			aData.addEnemy(enemyRefId, terrainRefId, enemyName, enemyImage, enemyGoldMin);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefEnemy COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefVacation(List<RefVacation> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearVacation();
		int i = 0;
		foreach (RefVacation aVacation in aArray)
		{
			string vacationRefId = aVacation.vacationRefId;
			string vacationName = aVacation.vacationName;
			string vacationDescription = aVacation.vacationDescription;
			int vacationCost = CommonAPI.parseInt(aVacation.vacationCost);
			int vacationDuration = CommonAPI.parseInt(aVacation.vacationDuration);
			float vacationMoodAdd = CommonAPI.parseFloat(aVacation.vacationMoodAdd);
			aData.addVacation(vacationRefId, vacationName, vacationDescription, vacationCost, vacationDuration, vacationMoodAdd);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefVacation COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefVacationPackage(List<RefVacationPackage> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearVacationPackage();
		int i = 0;
		foreach (RefVacationPackage aPackage in aArray)
		{
			string vacationPackageRefID = aPackage.vacationPackageRefID;
			string summer = aPackage.summer;
			string autumn = aPackage.autumn;
			string spring = aPackage.spring;
			string winter = aPackage.winter;
			aData.addVacationPackage(vacationPackageRefID, summer, autumn, spring, winter);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefVacationPackage COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefInitialValue(List<RefInitialValue> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearInitialValue();
		int i = 0;
		foreach (RefInitialValue aInitValue in aArray)
		{
			string vacationPackageRefID = aInitValue.initialValueRefId;
			string initialValueType = aInitValue.initialValueType;
			string initialValue = aInitValue.initialValue;
			int initialQty = CommonAPI.parseInt(aInitValue.initialQty);
			string initialConditionSet = aInitValue.initialConditionSet;
			aData.addInitialValue(vacationPackageRefID, initialValueType, initialValue, initialQty, initialConditionSet);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefInitialValue COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefGameScenario(List<RefGameScenario> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearGameScenario();
		int i = 0;
		foreach (RefGameScenario aGameScenario in aArray)
		{
			string gameScenarioRefId = aGameScenario.gameScenarioRefId;
			string gameScenarioName = aGameScenario.gameScenarioName;
			string gameScenarioDescription = aGameScenario.gameScenarioDescription;
			int difficulty = CommonAPI.parseInt(aGameScenario.difficulty);
			string bgImage = aGameScenario.bgImage;
			string image = aGameScenario.image;
			string completeImg = aGameScenario.completeImg;
			bool isScenario = bool.Parse(aGameScenario.isScenario);
			string initialConditionSet = aGameScenario.initialConditionSet;
			string gameLockSet = aGameScenario.gameLockSet;
			string itemLockSet = aGameScenario.itemLockSet;
			string objectiveSet = aGameScenario.objectiveSet;
			string firstObjectiveRefId = aGameScenario.firstObjectiveRefId;
			string formulaConstantsSet = aGameScenario.formulaConstantsSet;
			int dlc = CommonAPI.parseInt(aGameScenario.dlc);
			aData.addGameScenario(gameScenarioRefId, gameScenarioName, gameScenarioDescription, difficulty, bgImage, image, completeImg, isScenario, initialConditionSet, gameLockSet, itemLockSet, objectiveSet, firstObjectiveRefId, formulaConstantsSet, dlc);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefGameScenario COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefScenarioVariable(List<RefScenarioVariable> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearScenarioVariable();
		int i = 0;
		foreach (RefScenarioVariable aScenarioVariable in aArray)
		{
			string scenarioVariableRefId = aScenarioVariable.scenarioVariableRefId;
			string variableName = aScenarioVariable.variableName;
			string variableInitValue = aScenarioVariable.variableInitValue;
			string scenarioSet = aScenarioVariable.scenarioSet;
			aData.addScenarioVariable(scenarioVariableRefId, variableName, variableInitValue, scenarioSet);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefScenarioVariable COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefGameLock(List<RefGameLock> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearGameLock();
		int i = 0;
		foreach (RefGameLock aGameLock in aArray)
		{
			string gameLockRefId = aGameLock.gameLockRefId;
			string lockFeature = aGameLock.lockFeature;
			string unlockType = aGameLock.unlockType;
			string unlockRefId = aGameLock.unlockRefId;
			string gameLockSet = aGameLock.gameLockSet;
			aData.addGameLock(gameLockRefId, lockFeature, unlockType, unlockRefId, gameLockSet);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefGameLock COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefCutscenePath(List<RefCutscenePath> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearCutscenePath();
		int i = 0;
		foreach (RefCutscenePath aPath in aArray)
		{
			string cutscenePathRefID = aPath.cutscenePathRefID;
			string startingPoint = aPath.startingPoint;
			string endPoint = aPath.endPoint;
			string path = aPath.path;
			float yValue = CommonAPI.parseFloat(aPath.yValue);
			string endView = aPath.endView;
			aData.addCutscenePath(cutscenePathRefID, startingPoint, endPoint, path, yValue, endView);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefCutscenePath COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefCutsceneDialogue(List<RefCutsceneDialogue> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearCutsceneDialogue();
		int i = 0;
		foreach (RefCutsceneDialogue aDialogue in aArray)
		{
			string dialogueRefID = aDialogue.dialogueRefID;
			string dialogueSetID = aDialogue.dialogueSetID;
			int displayOrder = CommonAPI.parseInt(aDialogue.displayOrder);
			string type = aDialogue.type;
			string cutscenePathRefID = aDialogue.cutscenePathRefID;
			string cutsceneObstacleRefID = aDialogue.cutsceneObstacleRefID;
			string dialogueName = aDialogue.dialogueName;
			string dialogueText = aDialogue.dialogueText;
			string characterImage = aDialogue.characterImage;
			string action = aDialogue.action;
			string spawnPoint = aDialogue.spawnPoint;
			float yValue = CommonAPI.parseFloat(aDialogue.yValue);
			string startingView = aDialogue.startingView;
			string nextDialogueRefID = aDialogue.dialogueRefID;
			aData.addCutsceneDialogue(dialogueRefID, dialogueSetID, displayOrder, type, cutscenePathRefID, cutsceneObstacleRefID, dialogueName, dialogueText, characterImage, action, spawnPoint, yValue, startingView, nextDialogueRefID);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefCutsceneDialogue COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefCutsceneObstacle(List<RefCutsceneObstacles> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearCutsceneObstacle();
		int i = 0;
		foreach (RefCutsceneObstacles aObstacle in aArray)
		{
			string refObstacleID = aObstacle.refObstaclesID;
			string obstaclePoint = aObstacle.obstaclePoint;
			float yValue = CommonAPI.parseFloat(aObstacle.yValue);
			int width = CommonAPI.parseInt(aObstacle.width);
			int height = CommonAPI.parseInt(aObstacle.height);
			string endView = aObstacle.endView;
			int sortOrder = CommonAPI.parseInt(aObstacle.sortOrder);
			string image = aObstacle.image;
			aData.addCutsceneObstacle(refObstacleID, obstaclePoint, yValue, width, height, endView, sortOrder, image);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefCutsceneObstacle COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefKeyShortcut(List<RefKeyShortcut> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearKeyShortcut();
		int i = 0;
		foreach (RefKeyShortcut aKeyShortcut in aArray)
		{
			string keyShortcutRefID = aKeyShortcut.keyShortcutRefID;
			string function = aKeyShortcut.function;
			string button = aKeyShortcut.button;
			string category = aKeyShortcut.category;
			bool canBeChanged = bool.Parse(aKeyShortcut.canBeChanged);
			aData.addKeyShortcut(keyShortcutRefID, function, button, category, canBeChanged);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefKeyShortcut COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefCode(List<RefCode> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearCode();
		int i = 0;
		foreach (RefCode aCode in aArray)
		{
			string codeRefID = aCode.codeRefID;
			string code = aCode.code;
			string type = aCode.type;
			string refID = aCode.refID;
			aData.addCode(codeRefID, code, type, refID);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefCode COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefAchievement(List<RefAchievement> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearAchievement();
		int i = 0;
		foreach (RefAchievement aAchievement in aArray)
		{
			string achievementRefID = aAchievement.achievementRefID;
			string steamID = aAchievement.steamID;
			UnlockCondition successCondition = CommonAPI.convertStringToUnlockCondition(aAchievement.successCondition);
			int reqCount = CommonAPI.parseInt(aAchievement.reqCount);
			string checkString = aAchievement.checkString;
			int checkNum = CommonAPI.parseInt(aAchievement.checkNum);
			aData.addAchievement(achievementRefID, steamID, successCondition, reqCount, checkString, checkNum);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefAchievement COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefRandomText(List<RefRandomText> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearRandomText();
		int i = 0;
		foreach (RefRandomText aText in aArray)
		{
			string randomTextRefId = aText.randomTextRefId;
			string textRefId = aText.textRefId;
			string setRefId = aText.setRefId;
			aData.addRandomText(randomTextRefId, textRefId, setRefId);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefRandomText COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefText(List<RefText> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearText();
		int i = 0;
		foreach (RefText aText in aArray)
		{
			string textRefId = aText.textRefId;
			string reference = aText.reference;
			string text = aText.text;
			if (textRefId == "charaUnlock03")
			{
				CommonAPI.debug("text: " + text);
			}
			aData.addText(textRefId, reference, text);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefText COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private IEnumerator processRefGameConstant(List<RefGameConstant> aArray)
	{
		coroutineCheckStart++;
		GameData aData = game.getGameData();
		aData.clearConstant();
		int i = 0;
		foreach (RefGameConstant aConstant in aArray)
		{
			string lookUpKey = aConstant.lookUpKey;
			string value = aConstant.value;
			aData.addConstant(lookUpKey, value);
			i++;
			if (i % 20 == 0 && i != 0)
			{
				yield return null;
			}
		}
		CommonAPI.debug("processRefGameConstant COMPLETED");
		coroutineCheckDone++;
		checkCoroutine();
	}

	private string getFileNameByLanguage(LanguageType aLang)
	{
		return aLang switch
		{
			LanguageType.kLanguageTypeEnglish => "WSREFDATA", 
			LanguageType.kLanguageTypeGermany => "WSREFDATA_GERMANY", 
			LanguageType.kLanguageTypeRussia => "WSREFDATA_RUSSIA", 
			LanguageType.kLanguageTypeJap => "WSREFDATA_JAP", 
			LanguageType.kLanguageTypeChinese => "WSREFDATA_CHINESE", 
			LanguageType.kLanguageTypeFrench => "WSREFDATA_FRENCH", 
			LanguageType.kLanguageTypeItalian => "WSREFDATA_ITALIAN", 
			LanguageType.kLanguageTypeSpanish => "WSREFDATA_SPANISH", 
			_ => "WSREFDATA", 
		};
	}

	private void checkCoroutine()
	{
		if (GameObject.Find("Panel_LoadingLanguage") != null && coroutineCheckDone >= coroutineCheckStart)
		{
			CommonAPI.debug("Coroutine started: " + coroutineCheckStart);
			CommonAPI.debug("Coroutine Done: " + coroutineCheckDone);
			GameObject.Find("Panel_LoadingLanguage").GetComponent<GUILoadingController>().finishLoading();
		}
	}

	public bool checkShowLoadingScreen()
	{
		if (coroutineCheckStart > 0 && coroutineCheckDone < coroutineCheckStart)
		{
			return true;
		}
		return false;
	}
}
