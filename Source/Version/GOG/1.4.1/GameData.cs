using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class GameData
{
	private List<Smith> gameSmithList;

	private List<Smith> outsourceSmithList;

	private List<Hero> heroList;

	private List<HeroLevel> heroLevelList;

	private List<LegendaryHero> legendaryHeroList;

	private List<MaxLoyaltyReward> loyaltyRewardList;

	private List<Request> requestList;

	private List<Weapon> weaponList;

	private Dictionary<string, JobUnlock> jobUnlockList;

	private List<Item> itemList;

	private List<Area> areaList;

	private List<AreaEvent> areaEventList;

	private List<AreaRegion> areaRegionList;

	private List<AreaStatus> areaStatusList;

	private List<AreaPath> areaPathList;

	private List<Vacation> vacationList;

	private List<VacationPackage> vacationPackageList;

	private List<QuestNEW> questNEWList;

	private List<Contract> gameContractList;

	private List<Objective> objectiveList;

	private List<Tutorial> tutorialList;

	private List<string> tutorialSetList;

	private List<Enemy> enemyList;

	private List<Avatar> avatarList;

	private List<Furniture> furnitureList;

	private List<Decoration> decorationList;

	private List<DecorationPosition> decorationPosList;

	private List<RefDecorationType> decorationTypeList;

	private List<DialogueNEW> dialogueList;

	private List<string> dialogueNoSkipList;

	private List<WeaponType> weaponTypeList;

	private List<SmithAction> smithActionList;

	private List<DayEndScenario> dayEndScenarioList;

	private List<ForgeIncident> forgeIncidentList;

	private List<RecruitmentType> recruitmentTypeList;

	private List<SmithJobClass> smithJobClassList;

	private List<SmithStatusEffect> smithStatusEffectList;

	private List<SmithTraining> trainingList;

	private List<TrainingPackage> trainingPackageList;

	private List<Weather> weatherList;

	private List<Whetsapp> whetsappList;

	private List<ShopLevel> shopLevelList;

	private List<SpecialEvent> specialEventList;

	private List<WeekendActivity> weekendActivityList;

	private List<Ending> endingList;

	private List<SeasonObjective> seasonObjectiveList;

	private List<Tag> tagList;

	private List<RewardChest> rewardChestList;

	private List<Station> stationList;

	private List<CharacterPath> pathList;

	private List<Obstacle> obstacleList;

	private List<CutscenePath> cutscenePathList;

	private List<CutsceneDialogue> cutsceneDialogueList;

	private List<CutsceneObstacle> cutsceneObstacleList;

	private List<KeyShortcut> keyShortcutList;

	private List<Code> codeList;

	private List<Achievement> achievementList;

	private List<RandomText> randomTextList;

	private List<InitialValue> initialValueList;

	private List<GameScenario> gameScenarioList;

	private List<ScenarioVariable> scenarioVariableList;

	private List<GameLock> gameLockList;

	private Dictionary<string, Text> textList;

	private Dictionary<string, GameConstant> constantList;

	public GameData()
	{
		gameSmithList = new List<Smith>();
		outsourceSmithList = new List<Smith>();
		heroList = new List<Hero>();
		heroLevelList = new List<HeroLevel>();
		legendaryHeroList = new List<LegendaryHero>();
		loyaltyRewardList = new List<MaxLoyaltyReward>();
		requestList = new List<Request>();
		jobUnlockList = new Dictionary<string, JobUnlock>();
		weaponList = new List<Weapon>();
		itemList = new List<Item>();
		areaList = new List<Area>();
		areaEventList = new List<AreaEvent>();
		areaRegionList = new List<AreaRegion>();
		areaStatusList = new List<AreaStatus>();
		areaPathList = new List<AreaPath>();
		vacationList = new List<Vacation>();
		vacationPackageList = new List<VacationPackage>();
		questNEWList = new List<QuestNEW>();
		gameContractList = new List<Contract>();
		objectiveList = new List<Objective>();
		tutorialList = new List<Tutorial>();
		enemyList = new List<Enemy>();
		furnitureList = new List<Furniture>();
		decorationList = new List<Decoration>();
		decorationPosList = new List<DecorationPosition>();
		decorationTypeList = new List<RefDecorationType>();
		avatarList = new List<Avatar>();
		dialogueList = new List<DialogueNEW>();
		dialogueNoSkipList = new List<string>();
		weaponTypeList = new List<WeaponType>();
		smithActionList = new List<SmithAction>();
		dayEndScenarioList = new List<DayEndScenario>();
		forgeIncidentList = new List<ForgeIncident>();
		recruitmentTypeList = new List<RecruitmentType>();
		smithJobClassList = new List<SmithJobClass>();
		smithStatusEffectList = new List<SmithStatusEffect>();
		trainingList = new List<SmithTraining>();
		trainingPackageList = new List<TrainingPackage>();
		weatherList = new List<Weather>();
		whetsappList = new List<Whetsapp>();
		shopLevelList = new List<ShopLevel>();
		specialEventList = new List<SpecialEvent>();
		weekendActivityList = new List<WeekendActivity>();
		endingList = new List<Ending>();
		seasonObjectiveList = new List<SeasonObjective>();
		tagList = new List<Tag>();
		rewardChestList = new List<RewardChest>();
		stationList = new List<Station>();
		pathList = new List<CharacterPath>();
		obstacleList = new List<Obstacle>();
		cutscenePathList = new List<CutscenePath>();
		cutsceneDialogueList = new List<CutsceneDialogue>();
		cutsceneObstacleList = new List<CutsceneObstacle>();
		keyShortcutList = new List<KeyShortcut>();
		codeList = new List<Code>();
		achievementList = new List<Achievement>();
		initialValueList = new List<InitialValue>();
		gameScenarioList = new List<GameScenario>();
		scenarioVariableList = new List<ScenarioVariable>();
		gameLockList = new List<GameLock>();
		randomTextList = new List<RandomText>();
		textList = new Dictionary<string, Text>();
		constantList = new Dictionary<string, GameConstant>();
	}

	public void addSmith(string aSmithId, string aSmithRefId, string aSmithName, string aSmithDesc, SmithGender aGender, bool aOutsource, SmithJobClass aJob, bool aUnlock, UnlockCondition aCondition, int aConditionValue, string aCheckString, int aCheckInt, string aSetId, int aHireCost, float aSalaryGrowthType, int aBaseSalary, int aGrowthSalary, string aAction, int aModChance, string aImage, float aGrowthType, int aBasePower, int aBaseIntelligence, int aBaseTechnique, int aBaseLuck, int aGrowthPower, int aGrowthIntelligence, int aGrowthTechnique, int aGrowthLuck, int aBaseStamina, float aGrowthStamina, string aScenarioLock, int dlc)
	{
		gameSmithList.Add(new Smith(aSmithId, aSmithRefId, aSmithName, aSmithDesc, aGender, aOutsource, aJob, aUnlock, aCondition, aConditionValue, aCheckString, aCheckInt, aSetId, aHireCost, aSalaryGrowthType, aBaseSalary, aGrowthSalary, aAction, aModChance, aImage, aGrowthType, aBasePower, aBaseIntelligence, aBaseTechnique, aBaseLuck, aGrowthPower, aGrowthIntelligence, aGrowthTechnique, aGrowthLuck, aBaseStamina, aGrowthStamina, aScenarioLock, dlc));
	}

	public void addSmithByObject(Smith toAdd)
	{
		gameSmithList.Add(toAdd);
	}

	public List<Smith> getSmithList(bool checkDLC, bool includeNormal, bool includeLegendary, string aScenario)
	{
		if (includeNormal && includeLegendary)
		{
			return gameSmithList;
		}
		List<Smith> list = new List<Smith>();
		foreach (Smith gameSmith in gameSmithList)
		{
			if (((gameSmith.checkLegendary() && includeLegendary) || (!gameSmith.checkLegendary() && includeNormal)) && (!checkDLC || checkDLCOwned(gameSmith.getDlc())) && gameSmith.checkScenarioAllow(aScenario))
			{
				list.Add(gameSmith);
			}
		}
		return list;
	}

	public List<Smith> getUniqueHiredSmithList(bool includeNormal = true, bool includeLegendary = true)
	{
		List<Smith> list = new List<Smith>();
		foreach (Smith gameSmith in gameSmithList)
		{
			if (gameSmith.getTimesHired() > 0 && ((gameSmith.checkLegendary() && includeLegendary) || (!gameSmith.checkLegendary() && includeNormal)))
			{
				list.Add(gameSmith);
			}
		}
		return list;
	}

	public List<Smith> getLockedSmithList(bool checkDLC, string aScenario)
	{
		List<Smith> list = new List<Smith>();
		foreach (Smith gameSmith in gameSmithList)
		{
			if (!gameSmith.checkUnlock() && (!checkDLC || checkDLCOwned(gameSmith.getDlc())) && gameSmith.checkScenarioAllow(aScenario))
			{
				list.Add(gameSmith);
			}
		}
		return list;
	}

	public List<Smith> getRecruitList(int maxNum, List<string> smithRefIdList, string aScenario)
	{
		List<Smith> list = new List<Smith>();
		List<Smith> list2 = new List<Smith>();
		List<Smith> list3 = new List<Smith>();
		foreach (string smithRefId in smithRefIdList)
		{
			Smith smithByRefId = getSmithByRefId(smithRefId);
			if (smithByRefId.getSmithRefId() != string.Empty && smithByRefId.checkUnlock() && !smithByRefId.checkPlayerOwned() && checkDLCOwned(smithByRefId.getDlc()) && smithByRefId.checkScenarioAllow(aScenario))
			{
				if (smithByRefId.getUnlockCondition() != UnlockCondition.UnlockConditionNone)
				{
					list3.Add(smithByRefId);
				}
				else
				{
					list2.Add(smithByRefId);
				}
			}
		}
		list.AddRange(list3);
		int num = maxNum - list3.Count;
		if (num <= 0)
		{
			return list;
		}
		List<int> randomIntList = CommonAPI.getRandomIntList(list2.Count, num);
		randomIntList.Sort();
		foreach (int item in randomIntList)
		{
			list.Add(list2[item]);
		}
		return list;
	}

	public Smith getSmithByRefId(string refId)
	{
		foreach (Smith gameSmith in gameSmithList)
		{
			if (gameSmith.getSmithRefId() == refId)
			{
				return gameSmith;
			}
		}
		return new Smith();
	}

	public List<Smith> getSmithListByRefId(List<string> refIdList)
	{
		List<Smith> list = new List<Smith>();
		foreach (Smith gameSmith in gameSmithList)
		{
			if (refIdList.Contains(gameSmith.getSmithRefId()))
			{
				list.Add(gameSmith);
			}
		}
		return list;
	}

	public Smith getSmithByIndex(int index)
	{
		if (index < gameSmithList.Count)
		{
			return gameSmithList[index];
		}
		return new Smith();
	}

	public int getHiredSmithCount()
	{
		int num = 0;
		foreach (Smith gameSmith in gameSmithList)
		{
			if (gameSmith.getTimesHired() > 0)
			{
				num++;
			}
		}
		return num;
	}

	public void clearSmith()
	{
		gameSmithList.Clear();
	}

	public void addOutsourceSmith(string aSmithId, string aSmithRefId, string aSmithName, string aSmithDesc, SmithGender aGender, bool aOutsource, SmithJobClass aJob, bool aUnlock, UnlockCondition aCondition, int aConditionValue, string aCheckString, int aCheckNum, string aSetId, int aHireCost, float aSalaryGrowthType, int aBaseSalary, int aGrowthSalary, string aAction, int aModChance, string aImage, float aGrowthType, int aBasePower, int aBaseIntelligence, int aBaseTechnique, int aBaseLuck, int aGrowthPower, int aGrowthIntelligence, int aGrowthTechnique, int aGrowthLuck, int aBaseStamina, float aGrowthStamina, string aScenarioLock, int aDlc)
	{
		outsourceSmithList.Add(new Smith(aSmithId, aSmithRefId, aSmithName, aSmithDesc, aGender, aOutsource, aJob, aUnlock, aCondition, aConditionValue, aCheckString, aCheckNum, aSetId, aHireCost, aSalaryGrowthType, aBaseSalary, aGrowthSalary, aAction, aModChance, aImage, aGrowthType, aBasePower, aBaseIntelligence, aBaseTechnique, aBaseLuck, aGrowthPower, aGrowthIntelligence, aGrowthTechnique, aGrowthLuck, aBaseStamina, aGrowthStamina, aScenarioLock, aDlc));
	}

	public void addOutsourceSmithByObject(Smith toAdd)
	{
		outsourceSmithList.Add(toAdd);
	}

	public Smith getOutsourceSmithByRefId(string refId)
	{
		foreach (Smith outsourceSmith in outsourceSmithList)
		{
			if (outsourceSmith.getSmithRefId() == refId)
			{
				return outsourceSmith;
			}
		}
		return new Smith();
	}

	public List<Smith> getAllOutsourceSmithList(bool checkDlc, string aScenario)
	{
		List<Smith> list = new List<Smith>();
		foreach (Smith outsourceSmith in outsourceSmithList)
		{
			if (checkDLCOwned(outsourceSmith.getDlc()) && outsourceSmith.checkScenarioAllow(aScenario))
			{
				list.Add(outsourceSmith);
			}
		}
		return list;
	}

	public List<Smith> getOutsourceSmithList(string process, int shopLevel, int playerGold)
	{
		List<Smith> list = new List<Smith>();
		foreach (Smith outsourceSmith in outsourceSmithList)
		{
			switch (process)
			{
			case "DESIGN":
				if (outsourceSmith.getSmithJob().checkDesign() && outsourceSmith.getUnlockConditionValue() <= shopLevel)
				{
					list.Add(outsourceSmith);
				}
				break;
			case "CRAFT":
				if (outsourceSmith.getSmithJob().checkCraft() && outsourceSmith.getUnlockConditionValue() <= shopLevel)
				{
					list.Add(outsourceSmith);
				}
				break;
			case "POLISH":
				if (outsourceSmith.getSmithJob().checkPolish() && outsourceSmith.getUnlockConditionValue() <= shopLevel)
				{
					list.Add(outsourceSmith);
				}
				break;
			case "ENCHANT":
				if (outsourceSmith.getSmithJob().checkEnchant() && outsourceSmith.getUnlockConditionValue() <= shopLevel)
				{
					list.Add(outsourceSmith);
				}
				break;
			}
		}
		return list;
	}

	public void clearOutsourceSmith()
	{
		outsourceSmithList.Clear();
	}

	public void addSmithStatusEffect(string aRefId, string aName, string aComment, string aDesc, StatEffect aEffect1Type, float aEffect1Value, StatEffect aEffect2Type, float aEffect2Value, int aEffectDuration)
	{
		smithStatusEffectList.Add(new SmithStatusEffect(aRefId, aName, aComment, aDesc, aEffect1Type, aEffect1Value, aEffect2Type, aEffect2Value, aEffectDuration));
	}

	public void clearSmithStatusEffect()
	{
		smithStatusEffectList.Clear();
	}

	public List<SmithStatusEffect> getSmithStatusEffectList()
	{
		return smithStatusEffectList;
	}

	public SmithStatusEffect getSmithStatusEffectByRefId(string aRefId)
	{
		foreach (SmithStatusEffect smithStatusEffect in smithStatusEffectList)
		{
			if (smithStatusEffect.getEffectRefId() == aRefId)
			{
				return smithStatusEffect;
			}
		}
		return new SmithStatusEffect();
	}

	public void addSmithTraining(string aRefId, string aName, string aDesc, int aExp, int aCost, int aStamina, int aTime, int aShopLevel)
	{
		trainingList.Add(new SmithTraining(aRefId, aName, aDesc, aExp, aCost, aStamina, aTime, aShopLevel));
	}

	public SmithTraining getSmithTrainingByRefId(string aRefId)
	{
		foreach (SmithTraining training in trainingList)
		{
			if (training.getSmithTrainingRefId() == aRefId)
			{
				return training;
			}
		}
		return new SmithTraining();
	}

	public List<SmithTraining> getSmithTrainingList()
	{
		return trainingList;
	}

	public List<SmithTraining> getAllowedTrainingList(int playerDays, int shopLevel)
	{
		List<SmithTraining> list = new List<SmithTraining>();
		foreach (SmithTraining training in trainingList)
		{
			if (training.checkUnlock(playerDays, shopLevel))
			{
				list.Add(training);
			}
		}
		return list;
	}

	public void clearSmithTraining()
	{
		trainingList.Clear();
	}

	public void addTrainingPackage(string aRefId, string aSummer, string aSpring, string aAutumn, string aWinter)
	{
		trainingPackageList.Add(new TrainingPackage(aRefId, aSummer, aSpring, aAutumn, aWinter));
	}

	public void clearTrainingPackage()
	{
		trainingPackageList.Clear();
	}

	public TrainingPackage getTrainingPackageByRefID(string aRefID)
	{
		foreach (TrainingPackage trainingPackage in trainingPackageList)
		{
			if (trainingPackage.getTrainingPackageRefID() == aRefID)
			{
				return trainingPackage;
			}
		}
		return new TrainingPackage();
	}

	public void addItem(string aID, string aRefId, string aName, string aDesc, int aCost, int aBuyExp, int aSellPrice, ItemType aType, bool aSpecial, WeaponStat aStat, Element aElement, string aScenarioLock, int aValue, string aString, string aImage, int aNum, int aReqShopLevel, int aReqMonths, int aReqDogLove, int aChance)
	{
		itemList.Add(new Item(aID, aRefId, aName, aDesc, aCost, aBuyExp, aSellPrice, aType, aSpecial, aStat, aElement, aScenarioLock, aValue, aString, aImage, aNum, aReqShopLevel, aReqMonths, aReqDogLove, aChance));
	}

	public void clearItem()
	{
		itemList.Clear();
	}

	public List<Item> getShopItemListByType(ItemType aType, bool includeSpecial = true)
	{
		List<Item> list = new List<Item>();
		foreach (Item item in itemList)
		{
			if ((aType == ItemType.ItemTypeBlank || aType == item.getItemType()) && item.getItemCost() > 0 && (includeSpecial || !item.checkSpecial()))
			{
				list.Add(item);
			}
		}
		return list;
	}

	public List<Item> getSellItemList()
	{
		List<Item> list = new List<Item>();
		foreach (Item item in itemList)
		{
			if (item.getItemNum() > 0 && item.getItemSellPrice() > 0)
			{
				list.Add(item);
			}
		}
		return list;
	}

	public List<Item> getItemList(bool ownedOnly)
	{
		if (!ownedOnly)
		{
			return itemList;
		}
		List<Item> list = new List<Item>();
		foreach (Item item in itemList)
		{
			if (item.getItemNum() > 0)
			{
				list.Add(item);
			}
		}
		return list;
	}

	public List<Item> getItemListByType(ItemType aType, bool ownedOnly, bool includeSpecial, string aScenario)
	{
		List<Item> list = new List<Item>();
		foreach (Item item in itemList)
		{
			if ((aType == ItemType.ItemTypeBlank || aType == item.getItemType()) && (!ownedOnly || item.getItemNum() > 0) && (includeSpecial || !item.checkSpecial()) && item.checkScenarioAllow(aScenario))
			{
				list.Add(item);
			}
		}
		return list;
	}

	public List<Item> getSpecialItemListByType(ItemType aType, bool ownedOnly, string aScenario)
	{
		List<Item> list = new List<Item>();
		foreach (Item item in itemList)
		{
			if ((aType == ItemType.ItemTypeBlank || aType == item.getItemType()) && (!ownedOnly || item.getItemNum() > 0) && item.checkSpecial() && item.checkScenarioAllow(aScenario))
			{
				list.Add(item);
			}
		}
		return list;
	}

	public Item getItemByRefId(string aRefId)
	{
		foreach (Item item in itemList)
		{
			if (aRefId == item.getItemRefId())
			{
				return item;
			}
		}
		return new Item();
	}

	public List<Item> getItemListByRefId(List<string> refIdList)
	{
		List<Item> list = new List<Item>();
		foreach (Item item in itemList)
		{
			if (refIdList.Contains(item.getItemRefId()))
			{
				list.Add(item);
			}
		}
		return list;
	}

	public List<Item> getOwnedItemListByType(ItemType aType, bool includeSpecial, bool includeNormal)
	{
		List<Item> list = new List<Item>();
		foreach (Item item in itemList)
		{
			if (item.getItemType() == aType && item.getTotalNum() > 0 && ((includeSpecial && item.checkSpecial()) || (includeNormal && !item.checkSpecial())))
			{
				list.Add(item);
			}
		}
		return list;
	}

	public List<Item> getDogItemList(int playerShopLevel, int playerMonths, int playerDogLove)
	{
		List<Item> list = new List<Item>();
		foreach (Item item in itemList)
		{
			if (item.checkDogItemRequirements(playerShopLevel, playerMonths, playerDogLove))
			{
				list.Add(item);
			}
		}
		return list;
	}

	public List<Item> getSortedItemList(bool ownedOnly, bool includeSpecial, ItemType filterType, string aScenario)
	{
		List<Item> list = new List<Item>();
		List<Item> list2 = new List<Item>();
		if (filterType == ItemType.ItemTypeBlank)
		{
			list = getItemListByType(ItemType.ItemTypeEnhancement, ownedOnly, includeSpecial, aScenario);
			list2.AddRange(sortEnchantmentList(list));
			list2.AddRange(getItemListByType(ItemType.ItemTypeMaterial, ownedOnly, includeSpecial, aScenario));
			list2.AddRange(getItemListByType(ItemType.ItemTypeRelic, ownedOnly, includeSpecial, aScenario));
		}
		else
		{
			list = getItemListByType(filterType, ownedOnly, includeSpecial, aScenario);
			if (filterType == ItemType.ItemTypeEnhancement)
			{
				list2 = sortEnchantmentList(list);
			}
			else
			{
				list2.AddRange(list);
			}
		}
		return list2;
	}

	public List<Item> sortEnchantmentList(List<Item> unsortedList)
	{
		List<Item> list = new List<Item>();
		List<Item> list2 = new List<Item>();
		List<Item> list3 = new List<Item>();
		List<Item> list4 = new List<Item>();
		List<int> list5 = new List<int>();
		List<int> list6 = new List<int>();
		List<int> list7 = new List<int>();
		List<int> list8 = new List<int>();
		foreach (Item unsorted in unsortedList)
		{
			switch (unsorted.getItemEffectStat())
			{
			case WeaponStat.WeaponStatAttack:
				list.Add(unsorted);
				list5.Add(unsorted.getItemEffectValue());
				break;
			case WeaponStat.WeaponStatSpeed:
				list2.Add(unsorted);
				list6.Add(unsorted.getItemEffectValue());
				break;
			case WeaponStat.WeaponStatAccuracy:
				list3.Add(unsorted);
				list7.Add(unsorted.getItemEffectValue());
				break;
			case WeaponStat.WeaponStatMagic:
				list4.Add(unsorted);
				list8.Add(unsorted.getItemEffectValue());
				break;
			}
		}
		List<int> list9 = CommonAPI.sortIndices(list5, isAscending: false);
		List<int> list10 = CommonAPI.sortIndices(list6, isAscending: false);
		List<int> list11 = CommonAPI.sortIndices(list7, isAscending: false);
		List<int> list12 = CommonAPI.sortIndices(list8, isAscending: false);
		List<Item> list13 = new List<Item>();
		foreach (int item in list9)
		{
			list13.Add(list[item]);
		}
		foreach (int item2 in list10)
		{
			list13.Add(list2[item2]);
		}
		foreach (int item3 in list11)
		{
			list13.Add(list3[item3]);
		}
		foreach (int item4 in list12)
		{
			list13.Add(list4[item4]);
		}
		return list13;
	}

	public void addArea(string aRefId, string aName, string aDesc, string aCoordinate, string aPosition, float aScale, string aImage, Dictionary<string, int> aHeroList, Dictionary<string, int> aRareHeroList, Dictionary<string, ExploreItem> aExploreList, Dictionary<string, ShopItem> aShopItemList, AreaType aAreaType, bool aCanSell, bool aCanBuy, bool aCanExplore, string aTraining, string aVacation, int aTravelTime, float aMoodFactor, int aRefreshPrice, string aStatus, int aExpGrowth, List<string> aUnlockAreaList, int aUnlockTickets, int aRegion)
	{
		areaList.Add(new Area(aRefId, aName, aDesc, aCoordinate, aPosition, aScale, aImage, aHeroList, aRareHeroList, aExploreList, aShopItemList, aAreaType, aCanSell, aCanBuy, aCanExplore, aTraining, aVacation, aTravelTime, aMoodFactor, aRefreshPrice, aStatus, aExpGrowth, aUnlockAreaList, aUnlockTickets, aRegion));
	}

	public void clearArea()
	{
		areaList.Clear();
	}

	public List<Area> getAreaList(string aScenario)
	{
		if (aScenario == string.Empty)
		{
			return areaList;
		}
		List<Area> list = new List<Area>();
		foreach (Area area in areaList)
		{
			AreaRegion areaRegionByRefID = getAreaRegionByRefID(area.getRegion());
			if (areaRegionByRefID.checkScenarioAllow(aScenario))
			{
				list.Add(area);
			}
		}
		return list;
	}

	public List<Area> getAreaListByRegion(int aRegion)
	{
		List<Area> list = new List<Area>();
		foreach (Area area in areaList)
		{
			if (area.getRegion() == aRegion)
			{
				list.Add(area);
			}
		}
		return list;
	}

	public List<Area> getAreaListByRegionWithSell(int aRegion)
	{
		List<Area> list = new List<Area>();
		foreach (Area area in areaList)
		{
			if (area.getRegion() == aRegion && area.checkCanSell())
			{
				list.Add(area);
			}
		}
		return list;
	}

	public List<Area> getTicketUnlockableAreaList()
	{
		List<Area> list = new List<Area>();
		foreach (Area area in areaList)
		{
			if (!area.checkIsUnlock() && checkAreaPrereq(area))
			{
				list.Add(area);
			}
		}
		return list;
	}

	public Area getAreaByRefID(string aRefID)
	{
		foreach (Area area in areaList)
		{
			if (area.getAreaRefId() == aRefID)
			{
				return area;
			}
		}
		return new Area();
	}

	public bool checkAreaPrereq(Area checkArea)
	{
		List<Area> unlockedAreaList = getUnlockedAreaList(string.Empty);
		List<string> unlockAreaList = checkArea.getUnlockAreaList();
		foreach (Area item in unlockedAreaList)
		{
			if (unlockAreaList.Contains(item.getAreaRefId()))
			{
				return true;
			}
		}
		return false;
	}

	public List<Area> getUnlockedAreaList(string aScenario)
	{
		List<Area> list = new List<Area>();
		List<Area> list2 = getAreaList(aScenario);
		foreach (Area item in list2)
		{
			if (item.checkIsUnlock())
			{
				list.Add(item);
			}
		}
		return list;
	}

	public List<Area> getExploreAreaList(bool includeLocked)
	{
		List<Area> list = new List<Area>();
		foreach (Area area in areaList)
		{
			if (area.checkCanExplore() && (includeLocked || area.checkIsUnlock()))
			{
				list.Add(area);
			}
		}
		return list;
	}

	public List<Area> getSellAreaList(bool includeLocked)
	{
		List<Area> list = new List<Area>();
		foreach (Area area in areaList)
		{
			if (area.checkCanSell() && (includeLocked || area.checkIsUnlock()))
			{
				list.Add(area);
			}
		}
		return list;
	}

	public List<Area> getBuyAreaList(bool includeLocked)
	{
		List<Area> list = new List<Area>();
		foreach (Area area in areaList)
		{
			if (area.checkCanBuy() && (includeLocked || area.checkIsUnlock()))
			{
				list.Add(area);
			}
		}
		return list;
	}

	public List<string> getAreaHeroList(string aAreaRefId)
	{
		Area areaByRefID = getAreaByRefID(aAreaRefId);
		List<string> list = new List<string>();
		Dictionary<string, int> dictionary = areaByRefID.getHeroList();
		Dictionary<string, int> rareHeroList = areaByRefID.getRareHeroList();
		foreach (KeyValuePair<string, int> item in dictionary)
		{
			list.Add(item.Key);
		}
		foreach (KeyValuePair<string, int> item2 in rareHeroList)
		{
			list.Add(item2.Key);
		}
		return list;
	}

	public int countMaxLevelHeroInArea(string aAreaRefId)
	{
		List<Hero> heroListByRefIdList = getHeroListByRefIdList(getAreaHeroList(aAreaRefId), string.Empty);
		int num = 0;
		foreach (Hero item in heroListByRefIdList)
		{
			if (item.checkHeroMaxLevel())
			{
				num++;
			}
		}
		return num;
	}

	public void addAreaEvent(string aRefId, string aName, string aDescription, int aStartRegion, int aEndRegion, string aEffectType, float aStarchMult, float aExpMult, int aDuration, int aProbability)
	{
		areaEventList.Add(new AreaEvent(aRefId, aName, aDescription, aStartRegion, aEndRegion, aEffectType, aStarchMult, aExpMult, aDuration, aProbability));
	}

	public void clearAreaEvent()
	{
		areaEventList.Clear();
	}

	public List<AreaEvent> getAreaEventList()
	{
		return areaEventList;
	}

	public AreaEvent getAreaEventByRefId(string aRefId)
	{
		foreach (AreaEvent areaEvent in areaEventList)
		{
			if (areaEvent.getAreaEventRefId() == aRefId)
			{
				return areaEvent;
			}
		}
		return new AreaEvent();
	}

	public List<AreaEvent> getAreaEventListByRegion(int aRegion)
	{
		List<AreaEvent> list = new List<AreaEvent>();
		foreach (AreaEvent areaEvent in areaEventList)
		{
			if (areaEvent.checkRegion(aRegion))
			{
				list.Add(areaEvent);
			}
		}
		return list;
	}

	public List<Area> getAreaListWithEvents(int aRegion)
	{
		List<Area> list = new List<Area>();
		foreach (Area item in getAreaListByRegion(aRegion))
		{
			if (item.getCurrentEventRefId() != string.Empty)
			{
				list.Add(item);
			}
		}
		return list;
	}

	public void addAreaRegion(int aRefId, string aName, string aDesc, string aScenarioLock, int aFameRequired, int aWorkstationLvl, int aInterval, int aEventMax, int aGrantAmount, string aCameraCentre, float aZoomDefault, float aXPosLimitUpper, float aXPosLimitLower, float aYPosLimitUpper, float aYPosLimitLower, float aZoomLimitUpper, float aZoomLimitLower, float aTargetZoom, string aBgImg)
	{
		areaRegionList.Add(new AreaRegion(aRefId, aName, aDesc, aScenarioLock, aFameRequired, aWorkstationLvl, aInterval, aEventMax, aGrantAmount, aCameraCentre, aZoomDefault, aXPosLimitUpper, aXPosLimitLower, aYPosLimitUpper, aYPosLimitLower, aZoomLimitUpper, aZoomLimitLower, aTargetZoom, aBgImg));
	}

	public void clearAreaRegion()
	{
		areaRegionList.Clear();
	}

	public List<AreaRegion> getAreaRegionList(string aScenario)
	{
		if (aScenario == string.Empty)
		{
			return areaRegionList;
		}
		List<AreaRegion> list = new List<AreaRegion>();
		foreach (AreaRegion areaRegion in areaRegionList)
		{
			if (areaRegion.checkScenarioAllow(aScenario))
			{
				list.Add(areaRegion);
			}
		}
		return list;
	}

	public AreaRegion getAreaRegionByRefID(int aRegionRefID)
	{
		foreach (AreaRegion areaRegion in areaRegionList)
		{
			if (areaRegion.getAreaRegionRefID() == aRegionRefID)
			{
				return areaRegion;
			}
		}
		return new AreaRegion();
	}

	public List<string> getPreferredWeaponTypesInArea(string areaRefId, string aScenario)
	{
		List<string> list = new List<string>();
		Area areaByRefID = getAreaByRefID(areaRefId);
		List<string> heroRefIdList = areaByRefID.getHeroRefIdList();
		List<Hero> heroListByRefIdList = getHeroListByRefIdList(heroRefIdList, aScenario);
		foreach (Hero item in heroListByRefIdList)
		{
			foreach (string preferredWeaponType in item.getPreferredWeaponTypeList())
			{
				if (!list.Contains(preferredWeaponType))
				{
					list.Add(preferredWeaponType);
				}
			}
		}
		return list;
	}

	public int getMaxTicketsInRegion(int regionNum)
	{
		List<Area> areaListByRegion = getAreaListByRegion(regionNum);
		int num = 0;
		foreach (Area item in areaListByRegion)
		{
			num += item.getUnlockTickets();
		}
		return num;
	}

	public void addAreaStatus(string aRefId, string aAreaRefID, string aSmithEffectRefID, string aSeason, int aProbability)
	{
		areaStatusList.Add(new AreaStatus(aRefId, aAreaRefID, aSmithEffectRefID, aSeason, aProbability));
	}

	public void clearAreaStatus()
	{
		areaStatusList.Clear();
	}

	public AreaStatus getAreaStatusByRefID(string aAreaStatusRefID)
	{
		foreach (AreaStatus areaStatus in areaStatusList)
		{
			if (areaStatus.getAreaStatusRefID() == aAreaStatusRefID)
			{
				return areaStatus;
			}
		}
		return new AreaStatus();
	}

	public List<AreaStatus> getAreaStatusList()
	{
		return areaStatusList;
	}

	public List<AreaStatus> getAreaStatusListByAreaAndSeason(string aAreaRefID, Season aSeason)
	{
		List<AreaStatus> list = new List<AreaStatus>();
		foreach (AreaStatus areaStatus in areaStatusList)
		{
			if (areaStatus.getAreaRefID() == aAreaRefID && areaStatus.getSeason() == aSeason)
			{
				list.Add(areaStatus);
			}
		}
		return list;
	}

	public void addAreaPath(string aRefID, string aStartAreaRefID, string aEndAreaRefID, string aPath)
	{
		areaPathList.Add(new AreaPath(aRefID, aStartAreaRefID, aEndAreaRefID, aPath));
	}

	public void clearAreaPath()
	{
		areaPathList.Clear();
	}

	public List<AreaPath> getAreaPathList()
	{
		return areaPathList;
	}

	public AreaPath getAreaPathByRefID(string aRefID)
	{
		foreach (AreaPath areaPath in areaPathList)
		{
			if (areaPath.getAreaPathRefID() == aRefID)
			{
				return areaPath;
			}
		}
		return new AreaPath();
	}

	public AreaPath getAreaPathByStartEndAreaRefID(string aStartAreaRefID, string aEndAreaRefID)
	{
		foreach (AreaPath areaPath in areaPathList)
		{
			if (areaPath.getStartAreaRefID() == aStartAreaRefID && areaPath.getEndAreaRefID() == aEndAreaRefID)
			{
				return areaPath;
			}
		}
		return new AreaPath();
	}

	public void addVacation(string aRefId, string aName, string aDesc, int aCost, int aDuration, float aMood)
	{
		vacationList.Add(new Vacation(aRefId, aName, aDesc, aCost, aDuration, aMood));
	}

	public void clearVacation()
	{
		vacationList.Clear();
	}

	public List<Vacation> getVacationList()
	{
		return vacationList;
	}

	public Vacation getVacationByRefId(string aRefId)
	{
		foreach (Vacation vacation in vacationList)
		{
			if (vacation.getVacationRefId() == aRefId)
			{
				return vacation;
			}
		}
		return new Vacation();
	}

	public void addVacationPackage(string aRefId, string aSummer, string aSpring, string aAutumn, string aWinter)
	{
		vacationPackageList.Add(new VacationPackage(aRefId, aSummer, aSpring, aAutumn, aWinter));
	}

	public void clearVacationPackage()
	{
		vacationPackageList.Clear();
	}

	public VacationPackage getVacationPackageByRefID(string aRefID)
	{
		foreach (VacationPackage vacationPackage in vacationPackageList)
		{
			if (vacationPackage.getVacationPackageRefID() == aRefID)
			{
				return vacationPackage;
			}
		}
		return new VacationPackage(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
	}

	public void addContract(string aContractRefId, string aName, string aDesc, int aGold, int aLimit, int aAtkReq, int aSpdReq, int aAccReq, int aMagReq, int aMonthStart, int aMonthEnd)
	{
		gameContractList.Add(new Contract(aContractRefId, aName, aDesc, aGold, aLimit, aAtkReq, aSpdReq, aAccReq, aMagReq, aMonthStart, aMonthEnd));
	}

	public List<Contract> getFullContractList()
	{
		return gameContractList;
	}

	public List<Contract> getContractList(int month)
	{
		List<Contract> list = new List<Contract>();
		foreach (Contract gameContract in gameContractList)
		{
			if (gameContract.checkMonthRequirement(month))
			{
				list.Add(gameContract);
			}
		}
		return list;
	}

	public Contract getContractByRefID(string contractRefID)
	{
		foreach (Contract gameContract in gameContractList)
		{
			if (gameContract.getContractRefId() == contractRefID)
			{
				return gameContract;
			}
		}
		return new Contract();
	}

	public int getTotalContractAttempts()
	{
		int num = 0;
		foreach (Contract gameContract in gameContractList)
		{
			num += gameContract.getTimesStarted();
		}
		return num;
	}

	public int getTotalContractCompleteCount()
	{
		int num = 0;
		foreach (Contract gameContract in gameContractList)
		{
			num += gameContract.getTimesCompleted();
		}
		return num;
	}

	public void clearContracts()
	{
		gameContractList.Clear();
	}

	public void addObjective(string aRefId, string aName, string aDesc, int aLimit, string aStartDialogue, string aSuccessDialogue, string aSuccessNextRefId, string aFailDialogue, string aFailNextRefId, UnlockCondition aCondition, int aReqCount, string aCheckString, int aCheckNum, bool aFromObjectiveStart, bool aDoCount, string aObjectiveSet)
	{
		objectiveList.Add(new Objective(aRefId, aName, aDesc, aLimit, aStartDialogue, aSuccessDialogue, aSuccessNextRefId, aFailDialogue, aFailNextRefId, aCondition, aReqCount, aCheckString, aCheckNum, aFromObjectiveStart, aDoCount, aObjectiveSet));
	}

	public void clearObjective()
	{
		objectiveList.Clear();
	}

	public List<Objective> getObjectiveList(string aObjectiveSet, bool includeNotCounted)
	{
		List<Objective> list = new List<Objective>();
		foreach (Objective objective in objectiveList)
		{
			if ((includeNotCounted || objective.checkCountAsObjective()) && (aObjectiveSet == string.Empty || objective.getObjectiveSet() == aObjectiveSet))
			{
				list.Add(objective);
			}
		}
		return list;
	}

	public Objective getObjectiveByRefId(string aRefId)
	{
		foreach (Objective objective in objectiveList)
		{
			if (objective.getObjectiveRefId() == aRefId)
			{
				return objective;
			}
		}
		return new Objective();
	}

	public List<Objective> getSucceededObjectiveList()
	{
		List<Objective> list = new List<Objective>();
		foreach (Objective objective in objectiveList)
		{
			if (objective.checkObjectiveSuccess())
			{
				list.Add(objective);
			}
		}
		return list;
	}

	public void addTutorial(string aRefId, string aSetRefId, int aOrderIndex, string aTitle, string aText, string aTexturePath, float aXPos, float aYPos)
	{
		tutorialList.Add(new Tutorial(aRefId, aSetRefId, aOrderIndex, aTitle, aText, aTexturePath, aXPos, aYPos));
	}

	public void clearTutorial()
	{
		tutorialList.Clear();
	}

	public List<Tutorial> getTutorialList()
	{
		return tutorialList;
	}

	public Dictionary<int, Tutorial> getTutorialListBySetRefId(string aSetRefId)
	{
		Dictionary<int, Tutorial> dictionary = new Dictionary<int, Tutorial>();
		foreach (Tutorial tutorial in tutorialList)
		{
			if (tutorial.getTutorialSetRefId() == aSetRefId)
			{
				dictionary.Add(tutorial.getTutorialOrderIndex(), tutorial);
			}
		}
		return dictionary;
	}

	public List<string> getTutorialSetList()
	{
		if (tutorialSetList == null)
		{
			tutorialSetList = new List<string>();
			tutorialSetList.Add("INTRO");
			tutorialSetList.Add("FORGE");
			tutorialSetList.Add("SELL1");
			tutorialSetList.Add("SELL2");
			tutorialSetList.Add("SELL3");
			tutorialSetList.Add("OFFER");
			tutorialSetList.Add("OFFER_SELECTED");
			tutorialSetList.Add("SELL_RESULT");
			tutorialSetList.Add("BOOST");
			tutorialSetList.Add("JOB_CLASS");
			tutorialSetList.Add("TRAINING");
			tutorialSetList.Add("REGION_2");
			tutorialSetList.Add("HIRE_FIRE");
		}
		return tutorialSetList;
	}

	public int getTutorialSetOrderIndex(string aSetRefId)
	{
		List<string> list = getTutorialSetList();
		int result = -1;
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] == aSetRefId)
			{
				result = i;
				break;
			}
		}
		return result;
	}

	public string getTutorialSetRefIdByOrderIndex(int index)
	{
		List<string> list = getTutorialSetList();
		if (list.Count > index)
		{
			return list[index];
		}
		return string.Empty;
	}

	public void addQuestNEW(string aRefId, string aName, string aDesc, string aEndText, string aGiverEndText, string aUnlockCutscene, string aEndCutscene, QuestNEWType aType, string aObjective, bool aObjectiveClear, UnlockCondition aUnlockCondition, int aUnlockValue, string[] aLock, int aQuestTimeLimit, int aForgeTimeLimit, string aJobClass, string aWeapon, int aAtkReq, int aSpdReq, int aAccReq, int aMagReq, int aPoint, bool aPointVar, int aGold, int aLaw, int aChaos, string aGiverName, string aGiverImage, int aCountLimit, string aTerrainRefId, int aMilestoneNum, int aQuestTime, int aMinQuestGold)
	{
		questNEWList.Add(new QuestNEW(aRefId, aName, aDesc, aEndText, aGiverEndText, aUnlockCutscene, aEndCutscene, aType, aObjective, aObjectiveClear, aUnlockCondition, aUnlockValue, aLock, aQuestTimeLimit, aForgeTimeLimit, aJobClass, aWeapon, aAtkReq, aSpdReq, aAccReq, aMagReq, aPoint, aPointVar, aGold, aLaw, aChaos, aGiverName, aGiverImage, aCountLimit, aTerrainRefId, aMilestoneNum, aQuestTime, aMinQuestGold));
	}

	public void addQuestNEWObj(QuestNEW aQuest)
	{
		questNEWList.Add(aQuest);
	}

	public void clearQuestNEW()
	{
		questNEWList.Clear();
	}

	public List<QuestNEW> getQuestNEWList()
	{
		return questNEWList;
	}

	public List<QuestNEW> getQuestNEWListByType(QuestNEWType aType, bool ignoreLock, bool addChallengeQuest = false)
	{
		List<QuestNEW> list = new List<QuestNEW>();
		List<QuestNEW> list2 = new List<QuestNEW>();
		foreach (QuestNEW questNEW in questNEWList)
		{
			if (aType != QuestNEWType.QuestNEWTypeChallenge && questNEW.getQuestType() == QuestNEWType.QuestNEWTypeChallenge)
			{
				if (questNEW.getUnlocked() && !questNEW.getLocked() && !questNEW.getExpired() && questNEW.checkCompleteCount() && !questNEW.getOngoing() && !hasOngoingLockQuest(questNEW))
				{
					list2.Add(questNEW);
				}
			}
			else if (questNEW.getQuestType() == aType)
			{
				if (ignoreLock)
				{
					list.Add(questNEW);
				}
				else if (questNEW.getUnlocked() && !questNEW.getLocked() && !questNEW.getExpired() && questNEW.checkCompleteCount() && !questNEW.getOngoing() && !hasOngoingLockQuest(questNEW))
				{
					list.Add(questNEW);
				}
			}
		}
		if (addChallengeQuest)
		{
			foreach (QuestNEW item in list2)
			{
				if (!item.getOngoing())
				{
					list.Add(item);
				}
			}
			return list;
		}
		return list;
	}

	public Dictionary<string, List<QuestNEW>> sortQuestListByQuestGiver(List<QuestNEW> toSort)
	{
		Dictionary<string, List<QuestNEW>> dictionary = new Dictionary<string, List<QuestNEW>>();
		foreach (QuestNEW item in toSort)
		{
			string questGiverName = item.getQuestGiverName();
			if (dictionary.ContainsKey(questGiverName))
			{
				dictionary[questGiverName].Add(item);
				continue;
			}
			List<QuestNEW> list = new List<QuestNEW>();
			list.Add(item);
			dictionary.Add(questGiverName, list);
		}
		return dictionary;
	}

	private bool hasOngoingLockQuest(QuestNEW quest)
	{
		string[] lockQuests = quest.getLockQuests();
		string[] array = lockQuests;
		foreach (string refId in array)
		{
			if (getQuestNEWByRefId(refId).getOngoing())
			{
				return true;
			}
		}
		return false;
	}

	public QuestNEW getQuestNEWByRefId(string refId)
	{
		foreach (QuestNEW questNEW in questNEWList)
		{
			if (questNEW.getQuestRefId() == refId)
			{
				return questNEW;
			}
		}
		return new QuestNEW();
	}

	public int countPlayerCompletedQuestByType(QuestNEWType aType)
	{
		int num = 0;
		List<QuestNEW> questNEWListByType = getQuestNEWListByType(aType, ignoreLock: false);
		foreach (QuestNEW item in questNEWListByType)
		{
			if (item.getCompleteCount() > 0)
			{
				num++;
			}
		}
		return num;
	}

	public void addEnemy(string aEnemyRefId, string aTerrainRefId, string aEnemyName, string aEnemyImage, int aEnemyGoldMin)
	{
		enemyList.Add(new Enemy(aEnemyRefId, aTerrainRefId, aEnemyName, aEnemyImage, aEnemyGoldMin));
	}

	public Enemy getEnemyByTerrainGold(string aTerrainRefId, int aGoldGet)
	{
		Enemy result = new Enemy();
		foreach (Enemy enemy in enemyList)
		{
			if (enemy.getTerrainRefId() == aTerrainRefId)
			{
				if (!enemy.checkEncounter(aTerrainRefId, aGoldGet))
				{
					return result;
				}
				result = enemy;
			}
		}
		return result;
	}

	public void clearEnemy()
	{
		enemyList.Clear();
	}

	public void addHero(string aHeroRefId, string aHeroName, string aHeroDesc, string aJobClassName, string aJobClassDesc, int aHeroTier, string aImage, float aBaseAtk, float aBaseSpd, float aBaseAcc, float aBaseMag, WeaponStat aPriStat, WeaponStat aSecStat, int aWealth, int aSellExp, int aRequestLevelMin, int aRequestLevelMax, string aRequestText, Dictionary<string, int> aWeaponTypeAffinity, string aRewardSetId, int aInitExpPoints, string aScenarioLock, int aDlc)
	{
		heroList.Add(new Hero(aHeroRefId, aHeroName, aHeroDesc, aJobClassName, aJobClassDesc, aHeroTier, aImage, aBaseAtk, aBaseSpd, aBaseAcc, aBaseMag, aPriStat, aSecStat, aWealth, aSellExp, aRequestLevelMin, aRequestLevelMax, aRequestText, aWeaponTypeAffinity, aRewardSetId, aInitExpPoints, aScenarioLock, aDlc));
	}

	public void addHeroByObj(Hero aHero)
	{
		heroList.Add(aHero);
	}

	public void clearHero()
	{
		heroList.Clear();
	}

	public List<Hero> getDisplayHeroList(string aScenario)
	{
		List<Hero> list = new List<Hero>();
		foreach (Hero hero in heroList)
		{
			if (hero.checkUnlocked() && hero.checkScenarioAllow(aScenario))
			{
				list.Add(hero);
			}
		}
		return list;
	}

	public List<Hero> getHeroListByRefIdList(List<string> refIdList, string aScenario)
	{
		List<Hero> list = new List<Hero>();
		foreach (Hero hero in heroList)
		{
			if (refIdList.Contains(hero.getHeroRefId()) && hero.checkScenarioAllow(aScenario))
			{
				list.Add(hero);
			}
		}
		return list;
	}

	public List<Hero> getHeroList(string aScenario)
	{
		if (aScenario == string.Empty)
		{
			return heroList;
		}
		List<Hero> list = new List<Hero>();
		foreach (Hero hero in heroList)
		{
			if (hero.checkScenarioAllow(aScenario))
			{
				list.Add(hero);
			}
		}
		return list;
	}

	public List<Hero> getHeroCustomerList()
	{
		List<Hero> list = new List<Hero>();
		foreach (Hero hero in heroList)
		{
			if (hero.getTimesBought() > 0)
			{
				list.Add(hero);
			}
		}
		return list;
	}

	public Hero getHeroByHeroRefID(string aRefID)
	{
		foreach (Hero hero in heroList)
		{
			if (hero.getHeroRefId() == aRefID)
			{
				return hero;
			}
		}
		return new Hero();
	}

	public List<Hero> getHeroListByTier(int aTier, bool onlyMaxLoyalty)
	{
		List<Hero> list = new List<Hero>();
		foreach (Hero hero in heroList)
		{
			if (hero.getHeroTier() == aTier && (!onlyMaxLoyalty || hero.getHeroLevel() == 5))
			{
				list.Add(hero);
			}
		}
		return list;
	}

	public List<Hero> getMinLevelHeroListByTier(int aTier, int minLevel)
	{
		List<Hero> list = new List<Hero>();
		foreach (Hero hero in heroList)
		{
			if (hero.getHeroTier() == aTier && hero.getHeroLevel() >= minLevel)
			{
				list.Add(hero);
			}
		}
		return list;
	}

	public Hero getRequestHero(int shopLevel, string aScenario)
	{
		List<Hero> list = new List<Hero>();
		Hero result = new Hero();
		foreach (Hero item in getHeroListByRegion(shopLevel - 1, aScenario))
		{
			if (item.getRequestLevelMax() > shopLevel)
			{
				list.Add(item);
			}
		}
		if (list.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, list.Count);
			result = list[index];
		}
		return result;
	}

	public Hero getJobClassByRefId(string aRefId)
	{
		foreach (Hero hero in heroList)
		{
			if (hero.getHeroRefId() == aRefId)
			{
				return hero;
			}
		}
		return new Hero();
	}

	public List<Hero> getMaxLevelHeroList()
	{
		List<Hero> list = new List<Hero>();
		foreach (Hero hero in heroList)
		{
			if (hero.checkHeroMaxLevel())
			{
				list.Add(hero);
			}
		}
		return list;
	}

	public List<Hero> getTier1JobClass()
	{
		List<Hero> list = new List<Hero>();
		foreach (Hero hero in heroList)
		{
			if (hero.getHeroRefId().Substring(3, 2) == "11")
			{
				list.Add(hero);
			}
		}
		return list;
	}

	public int getJobClassListByCategoryCount(string categoryRefID)
	{
		int num = 0;
		foreach (Hero hero in heroList)
		{
			if (hero.getHeroRefId().Substring(0, 3) == categoryRefID)
			{
				num++;
			}
		}
		return num;
	}

	public Area getAreaByHeroRefId(string heroRefId)
	{
		foreach (Area area in areaList)
		{
			if (area.getHeroRefIdList().Contains(heroRefId))
			{
				return area;
			}
		}
		return new Area();
	}

	public List<string> getForgeWeaponMenuHeroList(string weaponTypeRefId, int currentRegion, string aScenario)
	{
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		List<string> list3 = new List<string>();
		List<string> list4 = new List<string>();
		foreach (Area area in areaList)
		{
			if (area.getRegion() != currentRegion)
			{
				continue;
			}
			if (area.checkIsUnlock())
			{
				foreach (string heroRefId in area.getHeroRefIdList())
				{
					if (!list3.Contains(heroRefId))
					{
						list3.Add(heroRefId);
					}
				}
				continue;
			}
			foreach (string heroRefId2 in area.getHeroRefIdList())
			{
				if (!list4.Contains(heroRefId2))
				{
					list4.Add(heroRefId2);
				}
			}
		}
		foreach (Hero hero in heroList)
		{
			if (hero.getAffinity(weaponTypeRefId) == 3 && hero.checkScenarioAllow(aScenario))
			{
				if (list3.Contains(hero.getHeroRefId()))
				{
					list.Add(hero.getHeroRefId());
				}
				else if (list4.Contains(hero.getHeroRefId()))
				{
					list2.Add(hero.getHeroRefId());
				}
			}
		}
		int count = list.Count;
		list.AddRange(list2);
		list.Add(count.ToString());
		return list;
	}

	public List<Hero> getHeroListByRegion(int aRegion, string aScenario)
	{
		List<string> list = new List<string>();
		foreach (Area area in areaList)
		{
			if (area.getRegion() != aRegion)
			{
				continue;
			}
			foreach (string key in area.getHeroList().Keys)
			{
				if (!list.Contains(key))
				{
					list.Add(key);
				}
			}
		}
		return getHeroListByRefIdList(list, aScenario);
	}

	public int getTotalMaxLevelHeroesByRegion(int aRegion)
	{
		List<Hero> heroListByRegion = getHeroListByRegion(aRegion, string.Empty);
		int num = 0;
		for (int i = 0; i < heroListByRegion.Count; i++)
		{
			if (heroListByRegion[i].checkHeroMaxLevel())
			{
				num++;
			}
		}
		return num;
	}

	public int getTotalExpByRegion(int aRegion, string aScenario)
	{
		List<Hero> heroListByRegion = getHeroListByRegion(aRegion, aScenario);
		int num = 0;
		foreach (Hero item in heroListByRegion)
		{
			num += item.getExpPoints();
		}
		return num;
	}

	public void addHeroLevel(string aHeroLevelRefId, int aHeroLevel, int aLevelExp, int aLevelUpFame)
	{
		heroLevelList.Add(new HeroLevel(aHeroLevelRefId, aHeroLevel, aLevelExp, aLevelUpFame));
	}

	public void clearHeroLevel()
	{
		heroLevelList.Clear();
	}

	public List<HeroLevel> getHeroLevelList()
	{
		return heroLevelList;
	}

	public int getHeroLevelByExp(int aExp)
	{
		for (int i = 0; i < heroLevelList.Count; i++)
		{
			if (heroLevelList[i].getLevelExp() > aExp)
			{
				return heroLevelList[i].getHeroLevel() - 1;
			}
		}
		return heroLevelList[heroLevelList.Count - 1].getHeroLevel();
	}

	public float getExpPercent(int currentExp)
	{
		int heroLevelByExp = getHeroLevelByExp(currentExp);
		int totalExpByHeroLevel = getTotalExpByHeroLevel(heroLevelByExp);
		int totalExpByHeroLevel2 = getTotalExpByHeroLevel(heroLevelByExp + 1);
		return (float)(currentExp - totalExpByHeroLevel) / (float)(totalExpByHeroLevel2 - totalExpByHeroLevel);
	}

	public int getTotalExpByHeroLevel(int aLevel)
	{
		foreach (HeroLevel heroLevel in heroLevelList)
		{
			if (heroLevel.getHeroLevel() == aLevel)
			{
				return heroLevel.getLevelExp();
			}
		}
		return 0;
	}

	public int getFameByLevelReached(int aLevel)
	{
		foreach (HeroLevel heroLevel in heroLevelList)
		{
			if (heroLevel.getHeroLevel() == aLevel)
			{
				return heroLevel.getLevelUpFame();
			}
		}
		return 0;
	}

	public void addLegendaryHero(string aRefId, string aName, string aDesc, string aQuestName, string aQuestDesc, string aImage, string aWeaponRefId, int aAtk, int aSpd, int aAcc, int aMag, string aRewardItemType, string aRewardItemRefId, int aRewardItemQty, int aGold, int aFame, UnlockCondition aCondition, int aConditionValue, string aCheckString, int aCheckNum, string aSuccessComment, string aFailComment, string aVisitDialogue, string aFailDialogue, string aSuccessDialogue, string aScenarioLock, int aDlc)
	{
		legendaryHeroList.Add(new LegendaryHero(aRefId, aName, aDesc, aQuestName, aQuestDesc, aImage, aWeaponRefId, aAtk, aSpd, aAcc, aMag, aRewardItemType, aRewardItemRefId, aRewardItemQty, aGold, aFame, aCondition, aConditionValue, aCheckString, aCheckNum, aSuccessComment, aFailComment, aVisitDialogue, aFailDialogue, aSuccessDialogue, aScenarioLock, aDlc));
	}

	public void clearLegendaryHero()
	{
		legendaryHeroList.Clear();
	}

	public List<LegendaryHero> getLegendaryHeroList(bool checkDLC, string aScenario)
	{
		List<LegendaryHero> list = new List<LegendaryHero>();
		foreach (LegendaryHero legendaryHero in legendaryHeroList)
		{
			if ((!checkDLC || checkDLCOwned(legendaryHero.getDlc())) && legendaryHero.checkScenarioAllow(aScenario))
			{
				list.Add(legendaryHero);
			}
		}
		return list;
	}

	public LegendaryHero getLegendaryHeroByWeaponRefId(string weaponRefId)
	{
		foreach (LegendaryHero legendaryHero in legendaryHeroList)
		{
			if (legendaryHero.getWeaponRefId() == weaponRefId)
			{
				return legendaryHero;
			}
		}
		return new LegendaryHero();
	}

	public LegendaryHero getLegendaryHeroByHeroRefId(string heroRefId)
	{
		foreach (LegendaryHero legendaryHero in legendaryHeroList)
		{
			if (legendaryHero.getLegendaryHeroRefId() == heroRefId)
			{
				return legendaryHero;
			}
		}
		return new LegendaryHero();
	}

	public List<LegendaryHero> getLegendaryHeroCustomerList()
	{
		List<LegendaryHero> list = new List<LegendaryHero>();
		foreach (LegendaryHero legendaryHero in legendaryHeroList)
		{
			if (legendaryHero.getRequestState() == RequestState.RequestStateCompleted)
			{
				list.Add(legendaryHero);
			}
		}
		return list;
	}

	public void addMaxLoyaltyReward(string aRefId, int aTier, int aCount, bool aSpecial, string aRewardType, string aRewardRefId, int aRewardNum)
	{
		loyaltyRewardList.Add(new MaxLoyaltyReward(aRefId, aTier, aCount, aSpecial, aRewardType, aRewardRefId, aRewardNum));
	}

	public void clearMaxLoyaltyReward()
	{
		loyaltyRewardList.Clear();
	}

	public List<MaxLoyaltyReward> getMaxLoyaltyRewardList()
	{
		return loyaltyRewardList;
	}

	public MaxLoyaltyReward tryMaxLoyaltyReward()
	{
		foreach (MaxLoyaltyReward loyaltyReward in loyaltyRewardList)
		{
			if (!loyaltyReward.checkIsGiven())
			{
				List<Hero> heroListByTier = getHeroListByTier(loyaltyReward.getHeroTier(), onlyMaxLoyalty: true);
				if (heroListByTier.Count >= loyaltyReward.getHeroCount())
				{
					return loyaltyReward;
				}
			}
		}
		return new MaxLoyaltyReward();
	}

	public void addRequest(string aRefId, int aLevel, string aReq1, string aReq2, int aDuration, int aBaseGold, int aBaseLoyalty, int aBaseFame, string aRewardSet, int aQty)
	{
		requestList.Add(new Request(aRefId, aLevel, aReq1, aReq2, aDuration, aBaseGold, aBaseLoyalty, aBaseFame, aRewardSet, aQty));
	}

	public void clearRequest()
	{
		requestList.Clear();
	}

	public Request getRequestByRefId(string aRefId)
	{
		foreach (Request request in requestList)
		{
			if (request.getRequestRefId() == aRefId)
			{
				return request;
			}
		}
		return new Request();
	}

	public Request getRequestByLevelRange(int aMin, int aMax)
	{
		List<Request> list = new List<Request>();
		foreach (Request request in requestList)
		{
			if (request.checkLevelRange(aMin, aMax))
			{
				list.Add(request);
			}
		}
		if (list.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, list.Count);
			return list[index];
		}
		return new Request();
	}

	public void addJobUnlock(string key, string aJobUnlockRefId, string aRequiredJobClassRefId, string aRequiredWeaponRefId, string aUnlockJobClassRefId)
	{
		jobUnlockList.Add(key, new JobUnlock(aJobUnlockRefId, aRequiredJobClassRefId, aRequiredWeaponRefId, aUnlockJobClassRefId));
	}

	public void clearJobUnlock()
	{
		jobUnlockList.Clear();
	}

	public string checkJobUnlock(string aJobClassRefId, string aWeaponRefId)
	{
		string key = aJobClassRefId + "@" + aWeaponRefId;
		JobUnlock value = new JobUnlock();
		if (jobUnlockList.ContainsKey(key))
		{
			jobUnlockList.TryGetValue(key, out value);
		}
		return value.getUnlockJobClassRefId();
	}

	public void addWeaponType(string aRefId, string aName, string aSkill, string aFirstRefId, float aAtkMult, float aSpdMult, float aAccMult, float aMagMult, string aImage)
	{
		weaponTypeList.Add(new WeaponType(aRefId, aName, aSkill, aFirstRefId, aAtkMult, aSpdMult, aAccMult, aMagMult, aImage));
	}

	public void clearWeaponType()
	{
		weaponTypeList.Clear();
	}

	public WeaponType getWeaponTypeByRefId(string refId)
	{
		foreach (WeaponType weaponType in weaponTypeList)
		{
			if (weaponType.getWeaponTypeRefId() == refId)
			{
				return weaponType;
			}
		}
		return new WeaponType();
	}

	public List<WeaponType> getWeaponTypeList()
	{
		return weaponTypeList;
	}

	public List<WeaponType> getUnlockedWeaponTypeList()
	{
		List<WeaponType> list = new List<WeaponType>();
		foreach (WeaponType weaponType in weaponTypeList)
		{
			if (weaponType.checkUnlocked())
			{
				list.Add(weaponType);
			}
		}
		return list;
	}

	public List<WeaponType> getRequestWeaponTypeList(List<string> heroPreferredList)
	{
		List<WeaponType> list = new List<WeaponType>();
		foreach (WeaponType weaponType in weaponTypeList)
		{
			if (heroPreferredList.Contains(weaponType.getWeaponTypeRefId()) && weaponType.checkUnlocked())
			{
				list.Add(weaponType);
			}
		}
		return list;
	}

	public void addWeapon(string aRefId, string aName, string aDesc, string aImage, string aTypeRefId, float aAtkMult, float aSpdMult, float aAccMult, float aMagMult, Dictionary<string, int> aMaterialList, List<string> aRelicList, WeaponStat aResearchType, int aResearchDuration, int aResearchCost, int aResearchMood, int aDlc, string aScenarioLock)
	{
		Weapon weapon = new Weapon(aRefId, aName, aDesc, aImage, aTypeRefId, aAtkMult, aSpdMult, aAccMult, aMagMult, aMaterialList, aRelicList, aResearchType, aResearchDuration, aResearchCost, aResearchMood, aDlc, aScenarioLock);
		weapon.setWeaponType(getWeaponTypeByRefId(aTypeRefId));
		weaponList.Add(weapon);
	}

	public void addWeaponByObj(Weapon aWeapon)
	{
		aWeapon.setWeaponType(getWeaponTypeByRefId(aWeapon.getWeaponTypeRefId()));
		weaponList.Add(aWeapon);
	}

	public void clearWeapon()
	{
		weaponList.Clear();
	}

	public Weapon getWeaponByRefId(string aRefId)
	{
		foreach (Weapon weapon in weaponList)
		{
			if (weapon.getWeaponRefId() == aRefId)
			{
				return weapon;
			}
		}
		return new Weapon();
	}

	public List<Weapon> getWeaponList(bool checkDLC, string aScenario)
	{
		List<Weapon> list = new List<Weapon>();
		foreach (Weapon weapon in weaponList)
		{
			if ((!checkDLC || checkDLCOwned(weapon.getDlc())) && weapon.checkScenarioAllow(aScenario))
			{
				list.Add(weapon);
			}
		}
		return list;
	}

	public List<Weapon> getWeaponListByType(string typeRefId, bool checkDLC, string aScenario)
	{
		List<Weapon> list = new List<Weapon>();
		foreach (Weapon weapon in weaponList)
		{
			if (weapon.getWeaponTypeRefId() == typeRefId && (!checkDLC || checkDLCOwned(weapon.getDlc())) && weapon.checkScenarioAllow(aScenario))
			{
				list.Add(weapon);
			}
		}
		return list;
	}

	public Weapon getRandomWeaponFromType(string typeRefId, string aScenario)
	{
		List<Weapon> weaponListByType = getWeaponListByType(typeRefId, checkDLC: true, aScenario);
		int index = UnityEngine.Random.Range(0, weaponListByType.Count);
		return weaponListByType[index];
	}

	public Dictionary<string, string> getWeaponRelicListByRelicRefId(string relic1, string aScenario)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		foreach (Weapon weapon in getWeaponList(checkDLC: true, aScenario))
		{
			List<string> relicList = weapon.getRelicList();
			if (!relicList.Contains(relic1))
			{
				continue;
			}
			foreach (string item in relicList)
			{
				if (relic1 != item && !dictionary.ContainsKey(item))
				{
					dictionary.Add(item, weapon.getWeaponRefId());
					break;
				}
			}
		}
		return dictionary;
	}

	public Weapon getWeaponByRelicCombi(string relic1, string relic2, string aScenario)
	{
		foreach (Weapon weapon in getWeaponList(checkDLC: true, aScenario))
		{
			List<string> relicList = weapon.getRelicList();
			if (relicList.Contains(relic1) && relicList.Contains(relic2))
			{
				return weapon;
			}
		}
		return new Weapon();
	}

	public List<Weapon> getWeaponListByRelicCombi(bool checkDLC, string relic1, string relic2, string aScenario)
	{
		List<Weapon> list = new List<Weapon>();
		foreach (Weapon weapon in getWeaponList(checkDLC, aScenario))
		{
			List<string> relicList = weapon.getRelicList();
			if (relicList.Contains(relic1) && relicList.Contains(relic2))
			{
				list.Add(weapon);
			}
		}
		return list;
	}

	public List<Weapon> getRequestWeaponList(List<string> heroPreferredList, string aScenario)
	{
		List<Weapon> list = new List<Weapon>();
		foreach (Weapon weapon in getWeaponList(checkDLC: true, aScenario))
		{
			if (heroPreferredList.Contains(weapon.getWeaponTypeRefId()) && weapon.checkScenarioAllow(aScenario))
			{
				list.Add(weapon);
			}
		}
		return list;
	}

	public bool checkHasUnforgedWeapon(string weaponTypeRefId, string aScenario)
	{
		foreach (Weapon weapon in getWeaponList(checkDLC: true, aScenario))
		{
			if (weapon.getWeaponTypeRefId() == weaponTypeRefId && weapon.getWeaponUnlocked() && weapon.getTimesUsed() < 1)
			{
				return true;
			}
		}
		return false;
	}

	public bool checkHasNewResearch(string aScenario)
	{
		foreach (Weapon weapon in getWeaponList(checkDLC: true, aScenario))
		{
			if (!weapon.getWeaponUnlocked())
			{
				List<string> relicList = weapon.getRelicList();
				List<Item> itemListByRefId = getItemListByRefId(relicList);
				if (itemListByRefId.Count == 2 && itemListByRefId[0].getItemNum() > 0 && itemListByRefId[1].getItemNum() > 0)
				{
					return true;
				}
			}
		}
		return false;
	}

	public void addDialogue(string aRefId, string aSetId, string aName, string aText, string aNextRefId, string aChoice1, string aChoice1Next, string aChoice2, string aChoice2Next, string aPosition, string aBackgroundTexture, string aSoundEffect, string aBgm, string aImage, bool aFlip)
	{
		dialogueList.Add(new DialogueNEW(aRefId, aSetId, aName, aText, aNextRefId, aChoice1, aChoice1Next, aChoice2, aChoice2Next, aPosition, aBackgroundTexture, aSoundEffect, aBgm, aImage, aFlip));
	}

	public Dictionary<string, DialogueNEW> getDialogueBySetId(string aSetId)
	{
		Dictionary<string, DialogueNEW> dictionary = new Dictionary<string, DialogueNEW>();
		foreach (DialogueNEW dialogue in dialogueList)
		{
			if (dialogue.getDialogueSetId() == aSetId)
			{
				dictionary.Add(dialogue.getDialogueRefId(), dialogue);
			}
		}
		return dictionary;
	}

	public List<string> getDialogueSetIdList()
	{
		List<string> list = new List<string>();
		foreach (DialogueNEW dialogue in dialogueList)
		{
			if (!list.Contains(dialogue.getDialogueSetId()))
			{
				list.Add(dialogue.getDialogueSetId());
			}
		}
		return list;
	}

	public void clearDialogue()
	{
		dialogueList.Clear();
	}

	public void addDialogueNoSkip(string aSetId)
	{
		dialogueNoSkipList.Add(aSetId);
	}

	public bool checkDialogueCanSkip(string checkSetId)
	{
		if (dialogueNoSkipList.Contains(checkSetId))
		{
			return false;
		}
		return true;
	}

	public void clearDialogueNoSkip()
	{
		dialogueNoSkipList.Clear();
	}

	public void addForgeIncident(string aRefId, string aName, string aDesc, IncidentType aType, float aMagnitude, string aImage, int aMonth)
	{
		forgeIncidentList.Add(new ForgeIncident(aRefId, aName, aDesc, aType, aMagnitude, aImage, aMonth));
	}

	public void clearForgeIncident()
	{
		forgeIncidentList.Clear();
	}

	public List<ForgeIncident> getForgeIncidentList(int month)
	{
		List<ForgeIncident> list = new List<ForgeIncident>();
		foreach (ForgeIncident forgeIncident in forgeIncidentList)
		{
			if (forgeIncident.checkMonthReq(month))
			{
				list.Add(forgeIncident);
			}
		}
		return list;
	}

	public void addWeather(string aRefId, string aName, string aText, bool aShow, Season aSeason, int aChance, string aLock, int aMonth, string aImage)
	{
		weatherList.Add(new Weather(aRefId, aName, aText, aShow, aSeason, aChance, aLock, aMonth, aImage));
	}

	public void clearWeather()
	{
		weatherList.Clear();
	}

	public Weather getWeatherByRefId(string aRefId)
	{
		foreach (Weather weather in weatherList)
		{
			if (weather.getWeatherRefId() == aRefId)
			{
				return weather;
			}
		}
		return new Weather();
	}

	public List<Weather> getWeatherList(Season aSeason, int aMonth, string aLock)
	{
		List<Weather> list = new List<Weather>();
		foreach (Weather weather in weatherList)
		{
			if (weather.checkConditions(aSeason, aMonth, aLock))
			{
				list.Add(weather);
			}
		}
		return list;
	}

	public void addDynamicWhetsapp(string aId, string aName, string aText, string aImage, long aTime, WhetsappFilterType aFilter, bool isRead)
	{
		Whetsapp whetsapp = new Whetsapp(aId, aName, aText, aImage, aTime, aFilter);
		whetsapp.setRead(isRead);
		whetsappList.Add(whetsapp);
	}

	public void addWhetsapp(string aId, string aName, string aText, string aImage, long aTime, WhetsappFilterType aFilter)
	{
		whetsappList.Add(new Whetsapp(aId, aName, aText, aImage, aTime, aFilter));
	}

	public void addNewWhetsappMsg(string aName, string aText, string aImage, long aTime, WhetsappFilterType aFilter)
	{
		whetsappList.Add(new Whetsapp(whetsappList.Count.ToString(), aName, aText, aImage, aTime, aFilter));
	}

	public void clearWhetsapp()
	{
		whetsappList.Clear();
	}

	public Whetsapp getWhetsappByRefId(string aId)
	{
		foreach (Whetsapp whetsapp in whetsappList)
		{
			if (whetsapp.getWhetsappId() == aId)
			{
				return whetsapp;
			}
		}
		return new Whetsapp();
	}

	public List<Whetsapp> getWhetsappList()
	{
		return whetsappList;
	}

	public List<Whetsapp> getWhetsappDisplayList(long currentTime, WhetsappFilterType aFilter, bool onlyUnread = false)
	{
		List<Whetsapp> list = new List<Whetsapp>();
		foreach (Whetsapp whetsapp in whetsappList)
		{
			if (whetsapp.getTime() > currentTime - 2688 && (aFilter == WhetsappFilterType.WhetsappFilterTypeAll || aFilter == whetsapp.getFilterType()))
			{
				list.Add(whetsapp);
			}
		}
		list.Reverse();
		if (list.Count > 20)
		{
			int count = list.Count - 20;
			list.RemoveRange(20, count);
		}
		if (onlyUnread)
		{
			List<Whetsapp> list2 = new List<Whetsapp>();
			{
				foreach (Whetsapp item in list)
				{
					if (!item.checkRead())
					{
						list2.Add(item);
					}
				}
				return list2;
			}
		}
		return list;
	}

	public int countUnreadWhetsapp(long currentTime)
	{
		List<Whetsapp> whetsappDisplayList = getWhetsappDisplayList(currentTime, WhetsappFilterType.WhetsappFilterTypeHero, onlyUnread: true);
		List<Whetsapp> whetsappDisplayList2 = getWhetsappDisplayList(currentTime, WhetsappFilterType.WhetsappFilterTypeSmith, onlyUnread: true);
		List<Whetsapp> whetsappDisplayList3 = getWhetsappDisplayList(currentTime, WhetsappFilterType.WhetsappFilterTypeNotice, onlyUnread: true);
		return whetsappDisplayList.Count + whetsappDisplayList2.Count + whetsappDisplayList3.Count;
	}

	public void addSmithAction(string aRefId, string aText, SmithActionState aState, bool aWorkAllow, bool aIdleAllow, string aWeather, float aHpReq, int aChance, int aDurationMin, int aDurationMax, StatEffect aEffect, float aEffectMin, float aEffectMax, string aRequired, string aPrevent, int aMinLevel, string aImage)
	{
		smithActionList.Add(new SmithAction(aRefId, aText, aState, aWorkAllow, aIdleAllow, aWeather, aHpReq, aChance, aDurationMin, aDurationMax, aEffect, aEffectMin, aEffectMax, aRequired, aPrevent, aMinLevel, aImage));
	}

	public void clearSmithAction()
	{
		smithActionList.Clear();
	}

	public SmithAction getSmithActionByRefId(string refId)
	{
		foreach (SmithAction smithAction in smithActionList)
		{
			if (smithAction.getRefId() == refId)
			{
				return smithAction;
			}
		}
		return new SmithAction();
	}

	public List<SmithAction> getSmithActionList(bool isIdle, Weather weather)
	{
		bool flag = true;
		List<SmithAction> list = new List<SmithAction>();
		foreach (SmithAction smithAction in smithActionList)
		{
			if (((isIdle && smithAction.checkIdleAllow()) || (!isIdle && smithAction.checkWorkAllow())) && smithAction.checkWeatherAllow(weather.getWeatherRefId()))
			{
				flag = true;
				string requiredItem = smithAction.getRequiredItem();
				if (requiredItem != "-1" && requiredItem != string.Empty && getHighestPlayerFurnitureByType(requiredItem).getFurnitureLevel() < smithAction.getItemMinLevel())
				{
					flag = false;
				}
				string preventItem = smithAction.getPreventItem();
				if (preventItem != "-1" && preventItem != string.Empty && getHighestPlayerFurnitureByType(preventItem).getFurnitureLevel() >= smithAction.getItemMinLevel())
				{
					flag = false;
				}
				if (flag)
				{
					list.Add(smithAction);
				}
			}
		}
		return list;
	}

	public void addScenario(string aID, string aText, string aChoice1, string aChoice2, bool aProject, UnlockCondition aCondition, int aReqCount, string aCheckString, int aCheckNum, int aEndCount, string a1Success, int a1SuccessChance, ScenarioEffect a1SuccessEffect, float a1SuccessValue, string a1Failure, int a1FailureChance, ScenarioEffect a1FailureEffect, float a1FailureValue, string a2Success, int a2SuccessChance, ScenarioEffect a2SuccessEffect, float a2SuccessValue, string a2Failure, int a2FailureChance, ScenarioEffect a2FailureEffect, float a2FailureValue)
	{
		dayEndScenarioList.Add(new DayEndScenario(aID, aText, aChoice1, aChoice2, aProject, aCondition, aReqCount, aCheckString, aCheckNum, aEndCount, a1Success, a1SuccessChance, a1SuccessEffect, a1SuccessValue, a1Failure, a1FailureChance, a1FailureEffect, a1FailureValue, a2Success, a2SuccessChance, a2SuccessEffect, a2SuccessValue, a2Failure, a2FailureChance, a2FailureEffect, a2FailureValue));
	}

	public void clearScenario()
	{
		dayEndScenarioList.Clear();
	}

	public List<DayEndScenario> getDayEndScenarioList()
	{
		return dayEndScenarioList;
	}

	public DayEndScenario getDayEndScenarioByRefId(string aRefId)
	{
		foreach (DayEndScenario dayEndScenario in dayEndScenarioList)
		{
			if (dayEndScenario.getScenarioRefId() == aRefId)
			{
				return dayEndScenario;
			}
		}
		return new DayEndScenario();
	}

	public void addRecruitmentType(string aRefId, string aName, string aDesc, int aCost, int aMax, int aDuration, int aLevel, int aMonth, List<string> aList)
	{
		recruitmentTypeList.Add(new RecruitmentType(aRefId, aName, aDesc, aCost, aMax, aDuration, aLevel, aMonth, aList));
	}

	public void clearRecruitmentType()
	{
		recruitmentTypeList.Clear();
	}

	public List<RecruitmentType> getRecruitmentTypeList(int aLevel, int aMonth)
	{
		List<RecruitmentType> list = new List<RecruitmentType>();
		foreach (RecruitmentType recruitmentType in recruitmentTypeList)
		{
			if (recruitmentType.checkRequirements(aLevel, aMonth))
			{
				list.Add(recruitmentType);
			}
		}
		return list;
	}

	public void addSmithJobClass(string aRefId, string aName, string aDesc, int aCost, int aMaxLevel, int aSalaryMult, float aPowMult, float aIntMult, float aTecMult, float aLucMult, bool aDesign, bool aCraft, bool aPolish, bool aEnchant, Dictionary<string, int> aReq, int aBasePermPow, float aGrowthPermPow, int aBasePermInt, float aGrowthPermInt, int aBasePermTec, float aGrowthPermTec, int aBasePermLuc, float aGrowthPermLuc, int aBasePermSta, float aGrowthPermSta, float aExpGrowthType, int aBaseExp, float aGrowthExp)
	{
		smithJobClassList.Add(new SmithJobClass(aRefId, aName, aDesc, aCost, aMaxLevel, aSalaryMult, aPowMult, aIntMult, aTecMult, aLucMult, aDesign, aCraft, aPolish, aEnchant, aReq, aBasePermPow, aGrowthPermPow, aBasePermInt, aGrowthPermInt, aBasePermTec, aGrowthPermTec, aBasePermLuc, aGrowthPermLuc, aBasePermSta, aGrowthPermSta, aExpGrowthType, aBaseExp, aGrowthExp));
	}

	public void clearSmithJobClass()
	{
		smithJobClassList.Clear();
	}

	public List<SmithJobClass> getAllSmithJobClassList()
	{
		return smithJobClassList;
	}

	public SmithJobClass getSmithJobClass(string aRefId)
	{
		foreach (SmithJobClass smithJobClass in smithJobClassList)
		{
			if (smithJobClass.getSmithJobRefId() == aRefId)
			{
				return smithJobClass;
			}
		}
		return new SmithJobClass();
	}

	public List<SmithJobClass> getSmithJobClassList(List<SmithExperience> experience)
	{
		List<SmithJobClass> list = new List<SmithJobClass>();
		foreach (SmithJobClass smithJobClass in smithJobClassList)
		{
			if (smithJobClass.checkLevelReq(experience))
			{
				list.Add(smithJobClass);
			}
		}
		return list;
	}

	public List<SmithJobClass> getJobChangeList(List<SmithExperience> experience, string fromJobRefId)
	{
		List<SmithJobClass> list = getSmithJobClassList(experience);
		List<SmithJobClass> list2 = new List<SmithJobClass>();
		foreach (SmithJobClass item in list)
		{
			if (fromJobRefId != item.getSmithJobRefId())
			{
				list2.Add(item);
			}
		}
		return list2;
	}

	public void addAvatar(string aRefId, string aName, string aDesc, string aImage, bool aUnlock, int aPlayReq)
	{
		avatarList.Add(new Avatar(aRefId, aName, aDesc, aImage, aUnlock, aPlayReq));
	}

	public void clearAvatar()
	{
		avatarList.Clear();
	}

	public Avatar getAvatarByRefId(string aRefId)
	{
		List<Avatar> list = new List<Avatar>();
		foreach (Avatar avatar in avatarList)
		{
			if (avatar.getAvatarRefId() == aRefId)
			{
				return avatar;
			}
		}
		return new Avatar();
	}

	public List<Avatar> getUnlockedAvatarList()
	{
		List<Avatar> list = new List<Avatar>();
		foreach (Avatar avatar in avatarList)
		{
			if (avatar.checkIsUnlock())
			{
				list.Add(avatar);
			}
		}
		return list;
	}

	public void addFurniture(string aRefId, string aName, string aDesc, bool aShowInShop, int aCost, string aType, int aFurnitureLevel, int aFurnitureCapacity, int aLevelReq, int aDogLove, string aImage)
	{
		furnitureList.Add(new Furniture(aRefId, aName, aDesc, aShowInShop, aCost, aType, aFurnitureLevel, aFurnitureCapacity, aLevelReq, aDogLove, aImage));
	}

	public void clearFurniture()
	{
		furnitureList.Clear();
	}

	public Furniture getOwnedFurnitureByRefID(string aFurnitureRefID)
	{
		foreach (Furniture furniture in furnitureList)
		{
			if (furniture.checkPlayerOwned() && furniture.getFurnitureRefId() == aFurnitureRefID)
			{
				return furniture;
			}
		}
		return new Furniture();
	}

	public List<Furniture> getPlayerOwnedFurniture(bool highestLevelOnly)
	{
		List<Furniture> list = new List<Furniture>();
		List<string> list2 = new List<string>();
		foreach (Furniture furniture in furnitureList)
		{
			if (!furniture.checkPlayerOwned())
			{
				continue;
			}
			if (highestLevelOnly)
			{
				if (!list2.Contains(furniture.getFurnitureType()))
				{
					Furniture highestPlayerFurnitureByType = getHighestPlayerFurnitureByType(furniture.getFurnitureType());
					if (highestPlayerFurnitureByType.getFurnitureRefId() != string.Empty)
					{
						list.Add(highestPlayerFurnitureByType);
					}
				}
			}
			else
			{
				list.Add(furniture);
			}
		}
		return list;
	}

	public Furniture getLowestFurnitureByType(string aType)
	{
		Furniture furniture = new Furniture();
		foreach (Furniture furniture2 in furnitureList)
		{
			if (furniture2.getFurnitureType() == aType && furniture2.getFurnitureLevel() < furniture.getFurnitureLevel())
			{
				furniture = furniture2;
			}
		}
		return furniture;
	}

	public Furniture getHighestPlayerFurnitureByType(string aType)
	{
		Furniture furniture = new Furniture();
		foreach (Furniture furniture2 in furnitureList)
		{
			if (furniture2.checkPlayerOwned() && furniture2.getFurnitureType() == aType && furniture2.getFurnitureLevel() > furniture.getFurnitureLevel())
			{
				furniture = furniture2;
			}
		}
		return furniture;
	}

	public List<Furniture> getFurnitureListByType(string aType)
	{
		List<Furniture> list = new List<Furniture>();
		foreach (Furniture furniture in furnitureList)
		{
			if (furniture.getFurnitureType() == aType)
			{
				list.Add(furniture);
			}
		}
		return list;
	}

	public List<Furniture> getAllWorkstationsFurniture()
	{
		List<Furniture> list = new List<Furniture>();
		foreach (Furniture furniture in furnitureList)
		{
			if (furniture.getFurnitureType() == "601" || furniture.getFurnitureType() == "701" || furniture.getFurnitureType() == "801" || furniture.getFurnitureType() == "901")
			{
				list.Add(furniture);
			}
		}
		return list;
	}

	public List<Furniture> getAllEnhancementsFurniture()
	{
		List<Furniture> list = new List<Furniture>();
		foreach (Furniture furniture in furnitureList)
		{
			if (furniture.getFurnitureType() == "201" || furniture.getFurnitureType() == "301" || furniture.getFurnitureType() == "401" || furniture.getFurnitureType() == "501")
			{
				list.Add(furniture);
			}
		}
		return list;
	}

	public List<Furniture> getEnvironmentFurniture()
	{
		List<Furniture> list = new List<Furniture>();
		foreach (Furniture furniture in furnitureList)
		{
			if (furniture.getFurnitureType() == "201" || furniture.getFurnitureType() == "401")
			{
				list.Add(furniture);
			}
		}
		return list;
	}

	public List<Furniture> getFurnitureListFiltered(bool onlyNotOwned, bool onlyLowestLevel, int currentLevel, bool includeDogItems, bool isShopList)
	{
		List<Furniture> list = new List<Furniture>();
		if (onlyLowestLevel)
		{
			List<string> list2 = new List<string>();
			foreach (Furniture furniture in furnitureList)
			{
				if (!list2.Contains(furniture.getFurnitureType()))
				{
					Furniture lowestFurnitureByType = getLowestFurnitureByType(furniture.getFurnitureType());
					if (lowestFurnitureByType.getFurnitureRefId() != string.Empty)
					{
						list.Add(lowestFurnitureByType);
					}
				}
			}
		}
		else
		{
			list.AddRange(furnitureList);
		}
		List<Furniture> list3 = new List<Furniture>();
		foreach (Furniture item in list)
		{
			if (includeDogItems || item.getFurnitureType() != "301")
			{
				list3.Add(item);
			}
		}
		List<Furniture> list4 = new List<Furniture>();
		if (currentLevel == -1)
		{
			foreach (Furniture item2 in list3)
			{
				if (!onlyNotOwned || !item2.checkPlayerOwned())
				{
					list4.Add(item2);
				}
			}
		}
		else
		{
			foreach (Furniture item3 in list3)
			{
				if (item3.checkShopLevelReq(currentLevel) && (!onlyNotOwned || !item3.checkPlayerOwned()))
				{
					list4.Add(item3);
				}
			}
		}
		List<Furniture> list5 = new List<Furniture>();
		foreach (Furniture item4 in list4)
		{
			if (!isShopList || item4.checkShowInShop())
			{
				list5.Add(item4);
			}
		}
		return list5;
	}

	public Furniture getFurnitureByRefId(string refId)
	{
		foreach (Furniture furniture in furnitureList)
		{
			if (furniture.getFurnitureRefId() == refId)
			{
				return furniture;
			}
		}
		return new Furniture();
	}

	public List<Furniture> getFurnitureList()
	{
		return furnitureList;
	}

	public void addDecoration(string aRefId, string aName, string aDesc, string aImage, int aShopLevelReq, string aType, string aTypeName, int aShopCost, UnlockCondition aUnlockCondition, int aUnlockValue, string aCheckString, int aCheckNum, bool aSpecial, string aScenarioLock, int aDlc)
	{
		decorationList.Add(new Decoration(aRefId, aName, aDesc, aImage, aShopLevelReq, aType, aTypeName, aShopCost, aUnlockCondition, aUnlockValue, aCheckString, aCheckNum, aSpecial, aScenarioLock, aDlc));
	}

	public void addDecorationByObj(Decoration aDecoration)
	{
		decorationList.Add(aDecoration);
	}

	public void clearDecoration()
	{
		decorationList.Clear();
	}

	public void setDecorationType(List<RefDecorationType> aList)
	{
		decorationTypeList = aList;
	}

	public List<RefDecorationType> getDecorationTypeList()
	{
		return decorationTypeList;
	}

	public List<Decoration> getDecorationList(bool isShopList, string aScenario)
	{
		if (isShopList)
		{
			List<Decoration> list = new List<Decoration>();
			{
				foreach (Decoration decoration in decorationList)
				{
					if ((!decoration.checkSpecial() || (decoration.checkSpecial() && decoration.checkIsPlayerOwned())) && checkDLCOwned(decoration.getDlc()) && decoration.checkScenarioAllow(aScenario))
					{
						list.Add(decoration);
					}
				}
				return list;
			}
		}
		return decorationList;
	}

	public List<Decoration> getSpecialDecorationList(bool ownedOnly, string aScenario)
	{
		List<Decoration> list = new List<Decoration>();
		foreach (Decoration decoration in decorationList)
		{
			if (decoration.checkSpecial() && (!ownedOnly || decoration.checkIsPlayerOwned()) && checkDLCOwned(decoration.getDlc()) && decoration.checkScenarioAllow(aScenario))
			{
				list.Add(decoration);
			}
		}
		return list;
	}

	public List<Decoration> getNormalDecorationList(bool ownedOnly, string aScenario)
	{
		List<Decoration> list = new List<Decoration>();
		foreach (Decoration decoration in decorationList)
		{
			if (!decoration.checkSpecial() && (!ownedOnly || decoration.checkIsPlayerOwned()) && checkDLCOwned(decoration.getDlc()) && decoration.checkScenarioAllow(aScenario))
			{
				list.Add(decoration);
			}
		}
		return list;
	}

	public List<Decoration> getCheckDecorationList(string aScenario)
	{
		List<Decoration> list = new List<Decoration>();
		foreach (Decoration decoration in decorationList)
		{
			if (!decoration.checkIsVisibleInShop() && !decoration.checkIsPlayerOwned() && checkDLCOwned(decoration.getDlc()) && decoration.checkScenarioAllow(aScenario))
			{
				list.Add(decoration);
			}
		}
		return list;
	}

	public List<Decoration> getDecorationCurrentDisplay()
	{
		List<Decoration> list = new List<Decoration>();
		foreach (Decoration decoration in decorationList)
		{
			if (decoration.checkIsCurrentDisplay() && decoration.checkIsPlayerOwned())
			{
				list.Add(decoration);
			}
		}
		return list;
	}

	public Decoration getDecorationCurrentDisplayByType(string aType)
	{
		foreach (Decoration decoration in decorationList)
		{
			if (decoration.getDecorationType() == aType && decoration.checkIsCurrentDisplay() && decoration.checkIsPlayerOwned())
			{
				return decoration;
			}
		}
		return new Decoration();
	}

	public Decoration getDecorationByRefId(string aRefId)
	{
		foreach (Decoration decoration in decorationList)
		{
			if (decoration.getDecorationRefId() == aRefId)
			{
				return decoration;
			}
		}
		return new Decoration();
	}

	public List<Decoration> getDecorationListByType(string aType, string aScenario)
	{
		List<Decoration> list = new List<Decoration>();
		foreach (Decoration decoration in decorationList)
		{
			if (decoration.getDecorationType() == aType && (!decoration.checkSpecial() || (decoration.checkSpecial() && decoration.checkIsPlayerOwned())) && checkDLCOwned(decoration.getDlc()) && decoration.checkScenarioAllow(aScenario))
			{
				list.Add(decoration);
			}
		}
		return list;
	}

	public List<Decoration> getDecorationListNoWallCarpet(string aScenario)
	{
		List<Decoration> list = new List<Decoration>();
		foreach (Decoration decoration in decorationList)
		{
			if (decoration.getDecorationType() != "1" && decoration.getDecorationType() != "2" && (!decoration.checkSpecial() || (decoration.checkSpecial() && decoration.checkIsPlayerOwned())) && checkDLCOwned(decoration.getDlc()) && decoration.checkScenarioAllow(aScenario))
			{
				list.Add(decoration);
			}
		}
		return list;
	}

	public void addDecorationPosition(string aPosRefId, string aRefId, string aPos, float aYValue, int aSortOrder, string aFlip, string aSortLayer, string aImage, int aShopLevel)
	{
		decorationPosList.Add(new DecorationPosition(aPosRefId, aRefId, aPos, aYValue, aSortOrder, aFlip, aSortLayer, aImage, aShopLevel));
	}

	public void clearDecorationPosition()
	{
		decorationPosList.Clear();
	}

	public List<DecorationPosition> getDecoPosByRefIdAndShopLevel(string aDecoRefId, int aShopLevel)
	{
		List<DecorationPosition> list = new List<DecorationPosition>();
		foreach (DecorationPosition decorationPos in decorationPosList)
		{
			if (decorationPos.getDecorationRefId() == aDecoRefId && decorationPos.getShopLevel() == aShopLevel)
			{
				list.Add(decorationPos);
			}
		}
		return list;
	}

	public void addWeekendActivity(string aRefId, string aName, string aText, WeekendActivityType aType, int aLimit, int aChance, int aReqShopLevel, Season aReqSeason, int aReqMonths, string aReqFurniture, int aReqAlignLaw, int aReqAlignChaos, string aRewardType, string aRewardRefId, int aRewardMagnitude, int aRewardQty, int aDogLove)
	{
		weekendActivityList.Add(new WeekendActivity(aRefId, aName, aText, aType, aLimit, aChance, aReqShopLevel, aReqSeason, aReqMonths, aReqFurniture, aReqAlignLaw, aReqAlignChaos, aRewardType, aRewardRefId, aRewardMagnitude, aRewardQty, aDogLove));
	}

	public void clearWeekendActivity()
	{
		weekendActivityList.Clear();
	}

	public WeekendActivity getWeekendActivityByRefId(string aRefId)
	{
		foreach (WeekendActivity weekendActivity in weekendActivityList)
		{
			if (weekendActivity.getWeekendActivityRefId() == aRefId)
			{
				return weekendActivity;
			}
		}
		return new WeekendActivity();
	}

	public List<WeekendActivity> getWeekendActivityListByType(WeekendActivityType type, Player player)
	{
		List<WeekendActivity> list = new List<WeekendActivity>();
		foreach (WeekendActivity weekendActivity in weekendActivityList)
		{
			bool flag = false;
			if (weekendActivity.getWeekendActivityType() == type)
			{
				flag = weekendActivity.checkReq(player.checkHasDog(), player.getShopLevelInt(), CommonAPI.getSeasonByMonth(player.getPlayerDateTimeList()[1]), player.getPlayerMonths(), player.getLawAlignment(), player.getChaosAlignment());
				string requiredFurnitureRefId = weekendActivity.getRequiredFurnitureRefId();
				if (requiredFurnitureRefId != "-1" && requiredFurnitureRefId != string.Empty && !getFurnitureByRefId(requiredFurnitureRefId).checkPlayerOwned())
				{
					flag = false;
				}
			}
			if (flag)
			{
				list.Add(weekendActivity);
			}
		}
		return list;
	}

	public List<WeekendActivity> getWeekendActivityList()
	{
		return weekendActivityList;
	}

	public void addShopLevel(string aRefId, string aName, int aCapacity, int aCost, int aMonth, float aMoodReduceRate, string aNextRefId, int aWidth, int aHeight, string aStartingCoor, string aCoffeeCoor, string aResearchCoor, string aPortalCoor, string aSpotIndicatorImage, string aFloorImg, string aWallLeftImg, string aWallRightImg, string aFloorPosition, string aWallLeftPosition, string aWallRightPosition, string aBgPosition)
	{
		shopLevelList.Add(new ShopLevel(aRefId, aName, aCapacity, aCost, aMonth, aMoodReduceRate, aNextRefId, aWidth, aHeight, aStartingCoor, aCoffeeCoor, aResearchCoor, aPortalCoor, aSpotIndicatorImage, aFloorImg, aWallLeftImg, aWallRightImg, aFloorPosition, aWallLeftPosition, aWallRightPosition, aBgPosition));
	}

	public void clearShopLevel()
	{
		shopLevelList.Clear();
	}

	public ShopLevel getShopLevel(string refId)
	{
		foreach (ShopLevel shopLevel in shopLevelList)
		{
			if (shopLevel.getShopRefId() == refId)
			{
				return shopLevel;
			}
		}
		return new ShopLevel();
	}

	public void addSeasonObjective(string aRefId, string aTitle, string aDesc, int aSeason, string aAlign, int aRange, int aTargetPoints, string aStartDialogueRefId, string aEndDialogueRefId)
	{
		seasonObjectiveList.Add(new SeasonObjective(aRefId, aTitle, aDesc, aSeason, aAlign, aRange, aTargetPoints, aStartDialogueRefId, aEndDialogueRefId));
	}

	public void clearSeasonObjective()
	{
		seasonObjectiveList.Clear();
	}

	public SeasonObjective getSeasonObjectiveByRefId(string aRefId)
	{
		foreach (SeasonObjective seasonObjective in seasonObjectiveList)
		{
			if (seasonObjective.getObjectiveRefId() == aRefId)
			{
				return seasonObjective;
			}
		}
		return new SeasonObjective();
	}

	public List<SeasonObjective> getSeasonObjectiveBySeasonIndex(int aIndex)
	{
		List<SeasonObjective> list = new List<SeasonObjective>();
		foreach (SeasonObjective seasonObjective in seasonObjectiveList)
		{
			if (seasonObjective.getSeasonIndex() == aIndex)
			{
				list.Add(seasonObjective);
			}
		}
		return list;
	}

	public List<SeasonObjective> getSeasonObjectiveList()
	{
		return seasonObjectiveList;
	}

	public void addTag(string aRefId, string aName, string aDesc)
	{
		tagList.Add(new Tag(aRefId, aName, aDesc));
	}

	public void clearTag()
	{
		tagList.Clear();
	}

	public Tag getTagByRefId(string aRefId)
	{
		foreach (Tag tag in tagList)
		{
			if (tag.getTagRefId() == aRefId)
			{
				return tag;
			}
		}
		return new Tag();
	}

	public List<Tag> getTagList()
	{
		return tagList;
	}

	public List<Tag> getTagListFromRefId(List<string> aList)
	{
		List<Tag> list = new List<Tag>();
		for (int i = 0; i < aList.Count; i++)
		{
			list.Add(new Tag(string.Empty, string.Empty, string.Empty));
		}
		foreach (Tag tag in tagList)
		{
			string tagRefId = tag.getTagRefId();
			if (aList.Contains(tagRefId))
			{
				list[aList.IndexOf(tagRefId)] = tag;
			}
		}
		return list;
	}

	public void addRewardChest(string aRefId, string aName, string aDesc, string aImage)
	{
		rewardChestList.Add(new RewardChest(aRefId, aName, aDesc, aImage));
	}

	public void clearRewardChest()
	{
		rewardChestList.Clear();
	}

	public RewardChest getRewardChestByRefId(string aRefId)
	{
		foreach (RewardChest rewardChest in rewardChestList)
		{
			if (rewardChest.getRewardChestRefId() == aRefId)
			{
				return rewardChest;
			}
		}
		return new RewardChest();
	}

	public void addSpecialEvent(string aRefId, int aMax, string aLock, int aDateYear, int aDateMonth, int aDateWeek, int aDateDay, SpecialEventType aEventType, int aStartMonths)
	{
		specialEventList.Add(new SpecialEvent(aRefId, aMax, aLock, aDateYear, aDateMonth, aDateWeek, aDateDay, aEventType, aStartMonths));
	}

	public void clearSpecialEvent()
	{
		specialEventList.Clear();
	}

	public SpecialEvent getSpecialEventByRefId(string aRefId)
	{
		foreach (SpecialEvent specialEvent in specialEventList)
		{
			if (specialEvent.getSpecialEventRefId() == aRefId)
			{
				return specialEvent;
			}
		}
		return new SpecialEvent();
	}

	public List<SpecialEvent> getSpecialEventByDate(List<int> date, int playerMonths, string aScenario)
	{
		List<SpecialEvent> list = new List<SpecialEvent>();
		foreach (SpecialEvent specialEvent in specialEventList)
		{
			if (specialEvent.checkEvent(date, playerMonths, aScenario))
			{
				list.Add(specialEvent);
			}
		}
		return list;
	}

	public List<SpecialEvent> getSpecialEventList()
	{
		return specialEventList;
	}

	public void addStation(string aRefStationID, string aStationPoint, string endPoint, string aPhase, int aShopLevel, int aFurnitureLevel, string aObstacleRefID, string aDogStationCoor)
	{
		stationList.Add(new Station(aRefStationID, aStationPoint, endPoint, aPhase, aShopLevel, aFurnitureLevel, aObstacleRefID, aDogStationCoor));
	}

	public void clearStation()
	{
		stationList.Clear();
	}

	public List<Station> getAllStation()
	{
		return stationList;
	}

	public List<Station> getAllPhaseStation(SmithStation aSmithStation)
	{
		List<Station> list = new List<Station>();
		foreach (Station station in stationList)
		{
			if (station.getSmithStation() == aSmithStation)
			{
				list.Add(station);
			}
		}
		return list;
	}

	public Station getPhaseStation(SmithStation aSmithStation, int aShopLevel, int aFurnitureLevel)
	{
		foreach (Station station in stationList)
		{
			if (station.getSmithStation() == aSmithStation && station.getShopLevel() == aShopLevel && station.getFurnitureLevel() == aFurnitureLevel)
			{
				return station;
			}
		}
		return new Station();
	}

	public Station getStationByRefID(string aRefID)
	{
		foreach (Station station in stationList)
		{
			if (station.getRefStationID() == aRefID)
			{
				return station;
			}
		}
		return new Station();
	}

	public void addCharacterPath(string aRefPathID, string aStartingPoint, string aEndPoint, string aSmithActionRefID, string aDogActionRefID, string aPath, float aYValue, string aEndView, int aShopLevel)
	{
		pathList.Add(new CharacterPath(aRefPathID, aStartingPoint, aEndPoint, aSmithActionRefID, aDogActionRefID, aPath, aYValue, aEndView, aShopLevel));
	}

	public void clearCharacterPath()
	{
		pathList.Clear();
	}

	public CharacterPath getPathByRefIDAndStartPoint(string aSmithActionRefID, Vector2 startPoint)
	{
		foreach (CharacterPath path in pathList)
		{
			if (path.getSmithActionRefID() == aSmithActionRefID && Vector2.Distance(startPoint, path.getStartingPoint()) == 0f)
			{
				return path;
			}
		}
		return new CharacterPath();
	}

	public CharacterPath getPathByRefIDStartEndShopLevel(string aSmithActionRefID, Vector2 startPoint, Vector2 endPoint, int shopLevel)
	{
		foreach (CharacterPath path in pathList)
		{
			if (path.getSmithActionRefID() == aSmithActionRefID && Vector2.Distance(startPoint, path.getStartingPoint()) == 0f && Vector2.Distance(endPoint, path.getEndPoint()) == 0f && path.getShopLevel() == shopLevel)
			{
				return path;
			}
		}
		return new CharacterPath();
	}

	public CharacterPath getPathByRefIDStartShopLevel(string aSmithActionRefID, Vector2 startPoint, int shopLevel)
	{
		foreach (CharacterPath path in pathList)
		{
			if (path.getSmithActionRefID() == aSmithActionRefID && Vector2.Distance(startPoint, path.getStartingPoint()) == 0f && path.getShopLevel() == shopLevel)
			{
				return path;
			}
		}
		return new CharacterPath();
	}

	public void addObstacle(string aRefObstaclesID, string aObstaclePoint, float aYValue, int aWidth, int aHeight, string aEndView, int aSortOrder, string aImageLocked, string aImageUnlocked, int aShopLevel, int aFurnitureLevel, string aFurnitureRefID)
	{
		obstacleList.Add(new Obstacle(aRefObstaclesID, aObstaclePoint, aYValue, aWidth, aHeight, aEndView, aSortOrder, aImageLocked, aImageUnlocked, aShopLevel, aFurnitureLevel, aFurnitureRefID));
	}

	public void clearObstacle()
	{
		obstacleList.Clear();
	}

	public List<Obstacle> getObstacleList()
	{
		return obstacleList;
	}

	public List<Obstacle> getObstacleByShopLevelNoFurniture(int aShopLevel)
	{
		List<Obstacle> list = new List<Obstacle>();
		foreach (Obstacle obstacle in obstacleList)
		{
			if (obstacle.getShopLevel() == aShopLevel && obstacle.getFurnitureRefID() == "-1")
			{
				list.Add(obstacle);
			}
		}
		return list;
	}

	public Obstacle getObstacleByRefID(string aRefID)
	{
		foreach (Obstacle obstacle in obstacleList)
		{
			if (obstacle.getRefObstacleID() == aRefID)
			{
				return obstacle;
			}
		}
		return new Obstacle();
	}

	public List<Obstacle> getObstacleByFurnitureRefID(string aRefID)
	{
		List<Obstacle> list = new List<Obstacle>();
		foreach (Obstacle obstacle in obstacleList)
		{
			if (obstacle.getFurnitureRefID() == aRefID)
			{
				list.Add(obstacle);
			}
		}
		return list;
	}

	public List<Obstacle> getObstacleByFurnitureRefIDAndLevel(string aRefID, int aFurnLevel, int aShopLevel)
	{
		List<Obstacle> list = new List<Obstacle>();
		foreach (Obstacle obstacle in obstacleList)
		{
			if (obstacle.getFurnitureRefID() == aRefID && aFurnLevel == obstacle.getFurnitureLevel() && aShopLevel == obstacle.getShopLevel())
			{
				list.Add(obstacle);
			}
		}
		return list;
	}

	public void addCutscenePath(string aCutscenePathRefID, string aStartingPoint, string aEndPoint, string aPath, float aYValue, string aEndView)
	{
		cutscenePathList.Add(new CutscenePath(aCutscenePathRefID, aStartingPoint, aEndPoint, aPath, aYValue, aEndView));
	}

	public void clearCutscenePath()
	{
		cutscenePathList.Clear();
	}

	public List<CutscenePath> getCutscenePathList()
	{
		return cutscenePathList;
	}

	public CutscenePath getCutscenePathByRefID(string aCutscenePathRefID)
	{
		foreach (CutscenePath cutscenePath in cutscenePathList)
		{
			if (cutscenePath.getCutscenePathRefID() == aCutscenePathRefID)
			{
				return cutscenePath;
			}
		}
		return new CutscenePath();
	}

	public void addCutsceneDialogue(string aRefId, string aSetId, int aDisplayOrder, string aType, string aCutscenePathRefID, string aCutsceneObstacleRefID, string aName, string aText, string aCharacterImage, string aActionName, string aSpawnPoint, float yValue, string startingView, string aNextDialogueRefID)
	{
		cutsceneDialogueList.Add(new CutsceneDialogue(aRefId, aSetId, aDisplayOrder, aType, aCutscenePathRefID, aCutsceneObstacleRefID, aName, aText, aCharacterImage, aActionName, aSpawnPoint, yValue, startingView, aNextDialogueRefID));
	}

	public void clearCutsceneDialogue()
	{
		cutsceneDialogueList.Clear();
	}

	public List<CutsceneDialogue> getCutsceneDialogueList()
	{
		return cutsceneDialogueList;
	}

	public List<CutsceneDialogue> getCutsceneDialogueBySetIDAscending(string aSetID)
	{
		List<CutsceneDialogue> list = new List<CutsceneDialogue>();
		foreach (CutsceneDialogue cutsceneDialogue in cutsceneDialogueList)
		{
			if (cutsceneDialogue.getDialogueSetId() == aSetID)
			{
				list.Add(cutsceneDialogue);
			}
		}
		list.Sort((CutsceneDialogue aDialogue, CutsceneDialogue bDialogue) => aDialogue.getDisplayOrder().CompareTo(bDialogue.getDisplayOrder()));
		return list;
	}

	public void addCutsceneObstacle(string aRefObstaclesID, string aObstaclePoint, float aYValue, int aWidth, int aHeight, string aEndView, int aSortOrder, string aImage)
	{
		cutsceneObstacleList.Add(new CutsceneObstacle(aRefObstaclesID, aObstaclePoint, aYValue, aWidth, aHeight, aEndView, aSortOrder, aImage));
	}

	public void clearCutsceneObstacle()
	{
		cutsceneObstacleList.Clear();
	}

	public List<CutsceneObstacle> getCutsceneObstacleList()
	{
		return cutsceneObstacleList;
	}

	public CutsceneObstacle getCutsceneObstacleByRefID(string aRefID)
	{
		foreach (CutsceneObstacle cutsceneObstacle in cutsceneObstacleList)
		{
			if (cutsceneObstacle.getRefObstacleID() == aRefID)
			{
				return cutsceneObstacle;
			}
		}
		return new CutsceneObstacle();
	}

	public void addKeyShortcut(string aKeyShortcutRefID, string aFunction, string aButton, string aCategory, bool canBeChanged)
	{
		keyShortcutList.Add(new KeyShortcut(aKeyShortcutRefID, aFunction, aButton, aCategory, canBeChanged));
	}

	public void clearKeyShortcut()
	{
		keyShortcutList.Clear();
	}

	public List<KeyShortcut> getKeyShortcutList()
	{
		return keyShortcutList;
	}

	public Dictionary<string, KeyShortcut> getKeyShortcutByCategory(string category)
	{
		Dictionary<string, KeyShortcut> dictionary = new Dictionary<string, KeyShortcut>();
		foreach (KeyShortcut keyShortcut in keyShortcutList)
		{
			if (keyShortcut.getCategory() == category)
			{
				dictionary.Add(keyShortcut.getKeyShortcutRefID(), keyShortcut);
			}
		}
		return dictionary;
	}

	public KeyShortcut getKeyShortcutByRefID(string aRefID)
	{
		foreach (KeyShortcut keyShortcut in keyShortcutList)
		{
			if (keyShortcut.getKeyShortcutRefID() == aRefID)
			{
				return keyShortcut;
			}
		}
		return new KeyShortcut();
	}

	public KeyCode getKeyCodeByRefID(string aRefID)
	{
		foreach (KeyShortcut keyShortcut in keyShortcutList)
		{
			if (keyShortcut.getKeyShortcutRefID() == aRefID)
			{
				return keyShortcut.getAssignedKey();
			}
		}
		return KeyCode.None;
	}

	public void addCode(string aCodeRefID, string aCode, string aUnlockType, string aRefID)
	{
		codeList.Add(new Code(aCodeRefID, aCode, aUnlockType, aRefID));
	}

	public void clearCode()
	{
		codeList.Clear();
	}

	public List<Code> getCodeList()
	{
		return codeList;
	}

	public Code getCodeByRefId(string aRefId)
	{
		foreach (Code code in codeList)
		{
			if (code.getCodeRefID() == aRefId)
			{
				return code;
			}
		}
		return new Code();
	}

	public List<Code> getUsedCodesList()
	{
		List<Code> list = new List<Code>();
		foreach (Code code in codeList)
		{
			if (code.checkUsed())
			{
				list.Add(code);
			}
		}
		return list;
	}

	public List<Code> getCodeByCode(string code)
	{
		List<Code> list = new List<Code>();
		foreach (Code code2 in codeList)
		{
			CommonAPI.debug("acode: " + code2.getCode() + "  " + code);
			if (code2.getCode() == code)
			{
				list.Add(code2);
			}
		}
		return list;
	}

	public void addAchievement(string aAchievementRefID, string aSteamID, UnlockCondition aCondition, int aReqCount, string aCheckString, int aCheckNum)
	{
		achievementList.Add(new Achievement(aAchievementRefID, aSteamID, aCondition, aReqCount, aCheckString, aCheckNum));
	}

	public void clearAchievement()
	{
		achievementList.Clear();
	}

	public List<Achievement> getAchievementList()
	{
		return achievementList;
	}

	public Achievement getAchievementBySteamID(string steamID)
	{
		foreach (Achievement achievement in achievementList)
		{
			if (achievement.getSteamID() == steamID)
			{
				return achievement;
			}
		}
		return new Achievement();
	}

	public void addText(string aTextRefId, string aReference, string aText)
	{
		textList.Add(aTextRefId, new Text(aTextRefId, aReference, aText));
	}

	public void clearText()
	{
		textList.Clear();
	}

	public string getTextByRefId(string aRefId)
	{
		int @int = PlayerPrefs.GetInt("showText", 0);
		if (textList.ContainsKey(aRefId))
		{
			if ((@int != 0) ? true : false)
			{
				return aRefId;
			}
			return textList[aRefId].getText();
		}
		return aRefId;
	}

	public string getTextByRefIdWithDynText(string aRefId, string replaceKey, string replaceValue)
	{
		int @int = PlayerPrefs.GetInt("showText", 0);
		if (textList.ContainsKey(aRefId))
		{
			string text = textList[aRefId].getText();
			text = text.Replace(replaceKey, replaceValue);
			if ((@int != 0) ? true : false)
			{
				return aRefId;
			}
			return text;
		}
		return aRefId;
	}

	public string getTextByRefIdWithDynTextList(string aRefId, List<string> replaceKey, List<string> replaceValue)
	{
		int @int = PlayerPrefs.GetInt("showText", 0);
		if (textList.ContainsKey(aRefId))
		{
			string text = textList[aRefId].getText();
			for (int i = 0; i < replaceKey.Count; i++)
			{
				text = text.Replace(replaceKey[i], replaceValue[i]);
			}
			if ((@int != 0) ? true : false)
			{
				return aRefId;
			}
			return text;
		}
		return aRefId;
	}

	public string getTextByRefIdWithDynTextListColorTag(string aRefId, List<string> replaceKey, List<string> replaceValue, string colorTag)
	{
		int @int = PlayerPrefs.GetInt("showText", 0);
		if (textList.ContainsKey(aRefId))
		{
			string text = textList[aRefId].getText();
			for (int i = 0; i < replaceKey.Count; i++)
			{
				text = text.Replace(replaceKey[i], replaceValue[i]);
			}
			if ((@int != 0) ? true : false)
			{
				return colorTag + aRefId + "[-]";
			}
			return colorTag + text + "[-]";
		}
		return aRefId;
	}

	public string replaceTextInTextString(string textString, List<string> replaceKey, List<string> replaceValue)
	{
		for (int i = 0; i < replaceKey.Count; i++)
		{
			textString = textString.Replace(replaceKey[i], replaceValue[i]);
		}
		return textString;
	}

	public void addConstant(string aLookUpKey, string aValue)
	{
		constantList.Add(aLookUpKey, new GameConstant(aLookUpKey, aValue));
	}

	public void clearConstant()
	{
		constantList.Clear();
	}

	public string getConstantByRefID(string aRefID)
	{
		if (constantList.ContainsKey(aRefID))
		{
			return constantList[aRefID].getValue();
		}
		return aRefID;
	}

	public int getIntConstantByRefID(string aRefID)
	{
		if (constantList.ContainsKey(aRefID))
		{
			return CommonAPI.parseInt(constantList[aRefID].getValue());
		}
		return -1;
	}

	public float getFloatConstantByRefID(string aRefID)
	{
		if (constantList.ContainsKey(aRefID))
		{
			return CommonAPI.parseFloat(constantList[aRefID].getValue());
		}
		return -1f;
	}

	public void addInitialValue(string aRefId, string aType, string aValue, int aQty, string aSet)
	{
		initialValueList.Add(new InitialValue(aRefId, aType, aValue, aQty, aSet));
	}

	public void clearInitialValue()
	{
		initialValueList.Clear();
	}

	public List<InitialValue> getInitialValueList()
	{
		return initialValueList;
	}

	public InitialValue getInitialValueByRefId(string aRefId)
	{
		foreach (InitialValue initialValue in initialValueList)
		{
			if (initialValue.getInitialValueRefId() == aRefId)
			{
				return initialValue;
			}
		}
		return new InitialValue();
	}

	public List<InitialValue> getInitialValueBySetAndType(string aSet, string aType)
	{
		List<InitialValue> list = new List<InitialValue>();
		foreach (InitialValue initialValue in initialValueList)
		{
			if (initialValue.getInitialConditionSet() == aSet && initialValue.getInitialValueType() == aType)
			{
				list.Add(initialValue);
			}
		}
		return list;
	}

	public void addGameScenario(string aRefId, string aName, string aDesc, int difficulty, string bgImage, string image, string completeImg, bool aScenario, string aCondition, string aLockSet, string aItemLockSet, string aObjectiveSet, string aObjective, string aConstants, int aDlc)
	{
		gameScenarioList.Add(new GameScenario(aRefId, aName, aDesc, difficulty, bgImage, image, completeImg, aScenario, aCondition, aLockSet, aItemLockSet, aObjectiveSet, aObjective, aConstants, aDlc));
	}

	public void clearGameScenario()
	{
		gameScenarioList.Clear();
	}

	public List<GameScenario> getGameScenarioList()
	{
		List<GameScenario> list = new List<GameScenario>();
		foreach (GameScenario gameScenario in gameScenarioList)
		{
			if (checkDLCOwned(gameScenario.getDlc()))
			{
				list.Add(gameScenario);
			}
		}
		return list;
	}

	public GameScenario getGameScenarioByRefId(string aRefId)
	{
		foreach (GameScenario gameScenario in gameScenarioList)
		{
			if (gameScenario.getGameScenarioRefId() == aRefId && checkDLCOwned(gameScenario.getDlc()))
			{
				return gameScenario;
			}
		}
		return new GameScenario();
	}

	public List<GameScenario> getOtherGameScenario()
	{
		List<GameScenario> list = new List<GameScenario>();
		foreach (GameScenario gameScenario in gameScenarioList)
		{
			if (gameScenario.getGameScenarioRefId() != "10001" && checkDLCOwned(gameScenario.getDlc()))
			{
				list.Add(gameScenario);
			}
		}
		return list;
	}

	public List<GameScenario> getOtherGameScenarioNoCheck()
	{
		List<GameScenario> list = new List<GameScenario>();
		foreach (GameScenario gameScenario in gameScenarioList)
		{
			CommonAPI.debug("scenario: " + gameScenario.getGameScenarioRefId());
			if (gameScenario.getGameScenarioRefId() != "10001")
			{
				list.Add(gameScenario);
			}
		}
		return list;
	}

	public void addScenarioVariable(string aRefId, string aName, string aValue, string aSet)
	{
		scenarioVariableList.Add(new ScenarioVariable(aRefId, aName, aValue, aSet));
	}

	public void clearScenarioVariable()
	{
		scenarioVariableList.Clear();
	}

	public List<ScenarioVariable> getScenarioVariableList()
	{
		return scenarioVariableList;
	}

	public ScenarioVariable getScenarioVariableByRefId(string aRefId)
	{
		foreach (ScenarioVariable scenarioVariable in scenarioVariableList)
		{
			if (scenarioVariable.getScenarioVariableRefId() == aRefId)
			{
				return scenarioVariable;
			}
		}
		return new ScenarioVariable();
	}

	public List<ScenarioVariable> getScenarioVariableSet(string aSet)
	{
		List<ScenarioVariable> list = new List<ScenarioVariable>();
		foreach (ScenarioVariable scenarioVariable in scenarioVariableList)
		{
			if (scenarioVariable.getScenarioSet() == aSet)
			{
				list.Add(scenarioVariable);
			}
		}
		return list;
	}

	public ScenarioVariable getScenarioVariableValue(string aSet, string aName)
	{
		foreach (ScenarioVariable scenarioVariable in scenarioVariableList)
		{
			if (scenarioVariable.getScenarioSet() == aSet && scenarioVariable.getVariableName() == aName)
			{
				return scenarioVariable;
			}
		}
		return new ScenarioVariable();
	}

	public List<ScenarioVariable> getScenarioVariableListByListName(string aSet, string aListName)
	{
		List<ScenarioVariable> list = new List<ScenarioVariable>();
		foreach (ScenarioVariable scenarioVariable in scenarioVariableList)
		{
			if (scenarioVariable.getScenarioSet() == aSet && scenarioVariable.getVariableName().Split('_')[0] == aListName)
			{
				list.Add(scenarioVariable);
			}
		}
		return list;
	}

	public void addGameLock(string aRefId, string aFeature, string aUnlock, string aUnlockRefId, string aSet)
	{
		gameLockList.Add(new GameLock(aRefId, aFeature, aUnlock, aUnlockRefId, aSet));
	}

	public void clearGameLock()
	{
		gameLockList.Clear();
	}

	public List<GameLock> getGameLockList()
	{
		return gameLockList;
	}

	public GameLock getGameLockByRefId(string aRefId)
	{
		foreach (GameLock gameLock in gameLockList)
		{
			if (gameLock.getGameLockRefId() == aRefId)
			{
				return gameLock;
			}
		}
		return new GameLock();
	}

	public GameLock getGameLockBySetAndFeature(string aSet, string aFeature)
	{
		foreach (GameLock gameLock in gameLockList)
		{
			if (gameLock.getGameLockSet() == aSet && gameLock.getLockFeature() == aFeature)
			{
				return gameLock;
			}
		}
		return new GameLock();
	}

	public List<GameLock> getGameLockListByTypeAndRefId(string aType, string aRefId)
	{
		List<GameLock> list = new List<GameLock>();
		foreach (GameLock gameLock in gameLockList)
		{
			if (gameLock.getUnlockType() == aType && gameLock.getUnlockRefId() == aRefId)
			{
				list.Add(gameLock);
			}
		}
		return list;
	}

	public bool checkFeatureIsUnlocked(string aSet, string aFeature, int completedTutorialIndex)
	{
		GameLock gameLockBySetAndFeature = getGameLockBySetAndFeature(aSet, aFeature);
		if ((gameLockBySetAndFeature.getGameLockRefId() == string.Empty) ? true : false)
		{
			return true;
		}
		switch (gameLockBySetAndFeature.getUnlockType())
		{
		case "OBJECTIVE":
			if (getObjectiveByRefId(gameLockBySetAndFeature.getUnlockRefId()).checkObjectiveEnded())
			{
				return true;
			}
			break;
		case "TUTORIAL":
			if (completedTutorialIndex >= getTutorialSetOrderIndex(gameLockBySetAndFeature.getUnlockRefId()))
			{
				return true;
			}
			break;
		}
		return false;
	}

	public void addRandomText(string aRandomTextRefId, string aTextRefId, string aSetRefId)
	{
		randomTextList.Add(new RandomText(aRandomTextRefId, aTextRefId, aSetRefId));
	}

	public void clearRandomText()
	{
		randomTextList.Clear();
	}

	public string getRandomTextRefIdBySetRefId(string aSetId)
	{
		List<string> list = new List<string>();
		foreach (RandomText randomText in randomTextList)
		{
			if (randomText.getSetRefId() == aSetId)
			{
				list.Add(randomText.getTextRefId());
			}
		}
		if (list.Count > 0)
		{
			return list[UnityEngine.Random.Range(0, list.Count)];
		}
		return aSetId;
	}

	public string getRandomTextBySetRefId(string aSetId)
	{
		string randomTextRefIdBySetRefId = getRandomTextRefIdBySetRefId(aSetId);
		return getTextByRefId(randomTextRefIdBySetRefId);
	}

	public string getRandomTextBySetRefIdWithDynText(string aSetId, string replaceKey, string replaceValue)
	{
		string randomTextRefIdBySetRefId = getRandomTextRefIdBySetRefId(aSetId);
		if (textList.ContainsKey(randomTextRefIdBySetRefId))
		{
			string text = textList[randomTextRefIdBySetRefId].getText();
			return text.Replace(replaceKey, replaceValue);
		}
		return randomTextRefIdBySetRefId;
	}

	public string getRandomTextBySetRefIdWithDynTextList(string aSetId, List<string> replaceKey, List<string> replaceValue)
	{
		string randomTextRefIdBySetRefId = getRandomTextRefIdBySetRefId(aSetId);
		if (textList.ContainsKey(randomTextRefIdBySetRefId))
		{
			string text = textList[randomTextRefIdBySetRefId].getText();
			for (int i = 0; i < replaceKey.Count; i++)
			{
				text = text.Replace(replaceKey[i], replaceValue[i]);
			}
			return text;
		}
		return randomTextRefIdBySetRefId;
	}

	private bool checkDLCOwned(int aDlc)
	{
		switch (aDlc)
		{
		case 0:
			return true;
		case 1:
		{
			string text = Application.streamingAssetsPath + "/dummy_gog_file.txt";
			CommonAPI.debug("readDlc: " + text);
			CommonAPI.debug("aPath: " + File.Exists(text));
			if (File.Exists(text))
			{
				return true;
			}
			return false;
		}
		default:
			return false;
		}
	}
}
