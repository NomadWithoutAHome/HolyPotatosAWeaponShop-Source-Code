using System;
using System.Collections.Generic;

[Serializable]
public class Smith
{
	private string smithId;

	private string smithRefId;

	private string smithName;

	private string smithDesc;

	private SmithGender smithGender;

	private bool isOutsource;

	private int expAmt;

	private SmithJobClass job;

	private List<SmithExperience> experienceList;

	private bool isPlayerOwned;

	private bool isUnlocked;

	private bool isLegendary;

	private UnlockCondition unlockCondition;

	private int unlockConditionValue;

	private string checkString;

	private int checkNum;

	private string unlockDialogueSetId;

	private int timesHired;

	private int basePower;

	private int baseIntelligence;

	private int baseTechnique;

	private int baseLuck;

	private float baseMood;

	private int buffPower;

	private int buffIntelligence;

	private int buffTechnique;

	private int buffLuck;

	private float buffMood;

	private float growthType;

	private int growthPower;

	private int growthIntelligence;

	private int growthTechnique;

	private int growthLuck;

	private float growthMood;

	private int hireCost;

	private float salaryGrowthType;

	private int baseSalary;

	private int growthSalary;

	private string preferredAction;

	private int preferredActionChance;

	private string image;

	private SmithAction smithAction;

	private SmithExploreState smithExploreState;

	private int smithActionDuration;

	private int smithActionElapsed;

	private List<StatEffect> smithEffectList;

	private List<float> smithEffectValueList;

	private List<int> smithEffectDurationList;

	private List<string> smithEffectDecoList;

	private List<string> smithStatusEffectList;

	private float mood;

	private int workProgress;

	private SmithStation assignedRole;

	private SmithStation currentStation;

	private int stationIndex;

	private List<SmithTag> smithTagList;

	private List<string> tagRefIdList;

	private int exploreExp;

	private int merchantExp;

	private Area exploreArea;

	private List<SmithExploreState> actionStateList;

	private List<int> actionDurationList;

	private int actionProgressIndex;

	private List<string> exploreTaskList;

	private Vacation vacation;

	private SmithTraining training;

	private List<AreaStatus> areaStatusList;

	private int currentSalary;

	private string scenarioLock;

	private int dlc;

	public Smith()
	{
		smithId = string.Empty;
		smithRefId = string.Empty;
		smithName = string.Empty;
		smithDesc = string.Empty;
		smithGender = SmithGender.SmithGenderNil;
		isOutsource = false;
		expAmt = 0;
		job = new SmithJobClass();
		experienceList = new List<SmithExperience>();
		isPlayerOwned = false;
		isUnlocked = false;
		isLegendary = false;
		unlockCondition = UnlockCondition.UnlockConditionNone;
		unlockConditionValue = 0;
		checkString = string.Empty;
		checkNum = 0;
		unlockDialogueSetId = string.Empty;
		timesHired = 0;
		hireCost = 0;
		salaryGrowthType = 0f;
		baseSalary = 0;
		growthSalary = 0;
		preferredAction = string.Empty;
		preferredActionChance = 0;
		image = string.Empty;
		smithAction = new SmithAction();
		smithActionDuration = 0;
		smithActionElapsed = 0;
		growthType = 0f;
		basePower = 0;
		baseIntelligence = 0;
		baseTechnique = 0;
		baseLuck = 0;
		buffPower = 0;
		buffIntelligence = 0;
		buffTechnique = 0;
		buffLuck = 0;
		growthPower = 0;
		growthIntelligence = 0;
		growthTechnique = 0;
		growthLuck = 0;
		baseMood = 0f;
		buffMood = 0f;
		growthMood = 0f;
		smithEffectList = new List<StatEffect>();
		smithEffectValueList = new List<float>();
		smithEffectDurationList = new List<int>();
		smithEffectDecoList = new List<string>();
		smithStatusEffectList = new List<string>();
		mood = 0f;
		workProgress = 0;
		assignedRole = SmithStation.SmithStationAuto;
		currentStation = SmithStation.SmithStationBlank;
		stationIndex = 0;
		smithTagList = new List<SmithTag>();
		tagRefIdList = new List<string>();
		exploreExp = 0;
		merchantExp = 0;
		exploreArea = new Area();
		actionStateList = new List<SmithExploreState>();
		actionDurationList = new List<int>();
		actionProgressIndex = 0;
		exploreTaskList = new List<string>();
		vacation = new Vacation();
		training = new SmithTraining();
		areaStatusList = new List<AreaStatus>();
		currentSalary = -1;
		scenarioLock = string.Empty;
		dlc = -1;
	}

	public Smith(string aSmithId, string aSmithRefId, string aName, string aDesc, SmithGender aGender, bool aOutsource, SmithJobClass aJob, bool aUnlock, UnlockCondition aUnlockCondition, int aUnlockConditionValue, string aCheckString, int aCheckNum, string aSetId, int aHireCost, float aSalaryGrowthType, int aBaseSalary, int aGrowthSalary, string aAction, int aModChance, string aImage, float aGrowthType, int aBasePower, int aBaseIntelligence, int aBaseTechnique, int aBaseLuck, int aGrowthPower, int aGrowthIntelligence, int aGrowthTechnique, int aGrowthLuck, int aBaseMood, float aGrowthMood, string aScenarioLock, int aDlc)
	{
		smithId = aSmithId;
		smithRefId = aSmithRefId;
		smithName = aName;
		smithDesc = aDesc;
		smithGender = aGender;
		isOutsource = aOutsource;
		expAmt = 0;
		job = aJob;
		experienceList = new List<SmithExperience>();
		isPlayerOwned = false;
		isUnlocked = aUnlock;
		if (isUnlocked)
		{
			isLegendary = false;
		}
		else
		{
			isLegendary = true;
		}
		unlockCondition = aUnlockCondition;
		unlockConditionValue = aUnlockConditionValue;
		checkString = aCheckString;
		checkNum = aCheckNum;
		unlockDialogueSetId = aSetId;
		timesHired = 0;
		hireCost = aHireCost;
		salaryGrowthType = aSalaryGrowthType;
		baseSalary = aBaseSalary;
		growthSalary = aGrowthSalary;
		preferredAction = aAction;
		preferredActionChance = aModChance;
		image = aImage;
		smithAction = new SmithAction();
		smithActionDuration = 0;
		smithActionElapsed = 0;
		growthType = aGrowthType;
		basePower = aBasePower;
		baseIntelligence = aBaseIntelligence;
		baseTechnique = aBaseTechnique;
		baseLuck = aBaseLuck;
		buffPower = 0;
		buffIntelligence = 0;
		buffTechnique = 0;
		buffLuck = 0;
		growthPower = aGrowthPower;
		growthIntelligence = aGrowthIntelligence;
		growthTechnique = aGrowthTechnique;
		growthLuck = aGrowthLuck;
		baseMood = aBaseMood;
		buffMood = 0f;
		growthMood = aGrowthMood;
		smithEffectList = new List<StatEffect>();
		smithEffectValueList = new List<float>();
		smithEffectDurationList = new List<int>();
		smithEffectDecoList = new List<string>();
		smithStatusEffectList = new List<string>();
		mood = 0f;
		workProgress = 0;
		assignedRole = SmithStation.SmithStationAuto;
		currentStation = SmithStation.SmithStationBlank;
		stationIndex = 0;
		smithTagList = new List<SmithTag>();
		tagRefIdList = new List<string>();
		exploreExp = 0;
		merchantExp = 0;
		exploreArea = new Area();
		actionStateList = new List<SmithExploreState>();
		actionDurationList = new List<int>();
		actionProgressIndex = 0;
		exploreTaskList = new List<string>();
		vacation = new Vacation();
		training = new SmithTraining();
		areaStatusList = new List<AreaStatus>();
		currentSalary = -1;
		scenarioLock = aScenarioLock;
		dlc = aDlc;
	}

	public int fitSmithPower(SmithJobClass sjc)
	{
		SmithExperience experienceByJobClass = getExperienceByJobClass(sjc.getSmithJobRefId());
		return (int)(((double)((float)basePower + (float)buffPower) + (double)(float)growthPower * Math.Pow(experienceByJobClass.getSmithJobClassLevel() - 1, growthType)) * (double)sjc.getPowMult());
	}

	public int fitSmithIntelligence(SmithJobClass sjc)
	{
		SmithExperience experienceByJobClass = getExperienceByJobClass(sjc.getSmithJobRefId());
		return (int)(((double)((float)baseIntelligence + (float)buffIntelligence) + (double)(float)growthIntelligence * Math.Pow(experienceByJobClass.getSmithJobClassLevel() - 1, growthType)) * (double)sjc.getIntMult());
	}

	public int fitSmithTechnique(SmithJobClass sjc)
	{
		SmithExperience experienceByJobClass = getExperienceByJobClass(sjc.getSmithJobRefId());
		return (int)(((double)((float)baseTechnique + (float)buffTechnique) + (double)(float)growthTechnique * Math.Pow(experienceByJobClass.getSmithJobClassLevel() - 1, growthType)) * (double)sjc.getTecMult());
	}

	public int fitSmithLuck(SmithJobClass sjc)
	{
		SmithExperience experienceByJobClass = getExperienceByJobClass(sjc.getSmithJobRefId());
		return (int)(((double)((float)baseLuck + (float)buffLuck) + (double)(float)growthLuck * Math.Pow(experienceByJobClass.getSmithJobClassLevel() - 1, growthType)) * (double)sjc.getLucMult());
	}

	public void resetWorkState(SmithAction action, int fixDuration)
	{
		string refId = smithAction.getRefId();
		if (refId != "103" && refId != "104")
		{
			setSmithAction(action, fixDuration);
			workProgress = 0;
			smithExploreState = SmithExploreState.SmithExploreStateBlank;
			actionProgressIndex = 0;
		}
	}

	public SmithExploreState getExploreState()
	{
		return smithExploreState;
	}

	public void setExploreState(SmithExploreState aState)
	{
		smithExploreState = aState;
	}

	public void returnToShopStandby()
	{
		SmithAction smithActionByRefId = CommonAPI.getGameData().getSmithActionByRefId("905");
		setSmithAction(smithActionByRefId, -1);
		clearExploreArea();
		clearExploreTask();
		clearExploreState();
		clearTraining();
		clearVacation();
	}

	public void clearExploreState()
	{
		smithExploreState = SmithExploreState.SmithExploreStateBlank;
		actionStateList = new List<SmithExploreState>();
		actionDurationList = new List<int>();
		actionProgressIndex = 0;
		workProgress = 0;
	}

	public void setExploreStateList(List<SmithExploreState> aStateList, List<int> aDurationList)
	{
		actionStateList = aStateList;
		actionDurationList = aDurationList;
		actionProgressIndex = 0;
		smithExploreState = aStateList[actionProgressIndex];
		smithActionDuration = aDurationList[actionProgressIndex];
		smithActionElapsed = 0;
		workProgress = 0;
	}

	public void nextExploreState()
	{
		actionProgressIndex++;
		smithExploreState = actionStateList[actionProgressIndex];
		smithActionDuration = actionDurationList[actionProgressIndex];
		smithActionElapsed = 0;
	}

	public List<SmithExploreState> getActionStateList()
	{
		return actionStateList;
	}

	public void setActionStateList(List<SmithExploreState> smithExploreStateList)
	{
		actionStateList = smithExploreStateList;
	}

	public List<int> getActionDurationList()
	{
		return actionDurationList;
	}

	public void setActionDurationList(List<int> aList)
	{
		actionDurationList = aList;
	}

	public int getActionProgressIndex()
	{
		return actionProgressIndex;
	}

	public void setActionProgressIndex(int aIndex)
	{
		actionProgressIndex = aIndex;
	}

	public List<AreaStatus> getAreaStatusList()
	{
		return areaStatusList;
	}

	public Area getExploreArea()
	{
		return exploreArea;
	}

	public void setExploreArea(Area aArea)
	{
		exploreArea = aArea;
	}

	public void clearExploreArea()
	{
		exploreArea = new Area();
	}

	public int getSmithExploreDuration()
	{
		return actionProgressIndex;
	}

	public void clearSmithExploreDuration()
	{
		actionProgressIndex = 0;
	}

	public List<string> getExploreTask()
	{
		return exploreTaskList;
	}

	public void setExploreTask(List<string> aTaskList)
	{
		exploreTaskList = aTaskList;
	}

	public void clearExploreTask()
	{
		exploreTaskList = new List<string>();
	}

	public Vacation getVacation()
	{
		return vacation;
	}

	public void setVacation(Vacation aVacation)
	{
		vacation = aVacation;
	}

	public void clearVacation()
	{
		vacation = new Vacation();
	}

	public SmithTraining getTraining()
	{
		return training;
	}

	public void setTraining(SmithTraining aTraining)
	{
		training = aTraining;
	}

	public void clearTraining()
	{
		training = new SmithTraining();
	}

	public AreaStatus getRandomAreaStatus(bool forceStatusEffect = false)
	{
		if (areaStatusList.Count < 1)
		{
			return new AreaStatus();
		}
		List<int> list = new List<int>();
		foreach (AreaStatus areaStatus in areaStatusList)
		{
			if (forceStatusEffect && areaStatus.getSmithEffectRefID() == "10001")
			{
				return areaStatus;
			}
			list.Add(areaStatus.getProbability());
		}
		int weightedRandomIndex = CommonAPI.getWeightedRandomIndex(list);
		return areaStatusList[weightedRandomIndex];
	}

	public void setAreaStatusList(List<AreaStatus> aStatusList)
	{
		areaStatusList = aStatusList;
	}

	public void clearAreaStatusList()
	{
		areaStatusList.Clear();
	}

	public void startNewDay(SmithAction defaultAction)
	{
		string refId = smithAction.getRefId();
		passTimeOnEffects(1);
		if (refId == "104" && smithActionDuration > smithActionElapsed)
		{
			smithActionElapsed++;
		}
		else
		{
			setSmithAction(defaultAction, -1);
		}
	}

	public string getCurrentJobClassLevelString()
	{
		return job.getSmithJobName() + " " + CommonAPI.getGameData().getTextByRefId("smithStatsShort01") + " " + getSmithLevel();
	}

	public string getSmithStandardInfoString(bool showFullJobDetails, bool showCurrentState = true)
	{
		GameData gameData = CommonAPI.getGameData();
		string empty = string.Empty;
		empty += smithName;
		if (showCurrentState)
		{
			empty = empty + " (" + CommonAPI.getMoodString(getMoodState(), showDesc: false) + ")";
		}
		empty += "\n";
		if (!showFullJobDetails)
		{
			empty = empty + getCurrentJobClassLevelString() + "\n";
		}
		empty += "[s]                         [/s]\n";
		if (showCurrentState)
		{
			empty = empty + "[FF9000][i]" + getSmithActionText() + "[/i][-]\n\n";
		}
		empty = empty + gameData.getTextByRefId("smithStats17") + ": ";
		empty += "[FF9000]";
		if (getMerchantLevel() < 15)
		{
			string text = empty;
			empty = text + getMerchantLevel() + "/" + 15;
		}
		else
		{
			empty += gameData.getTextByRefId("playerStats08");
		}
		empty += "[-]\n";
		empty = empty + gameData.getTextByRefId("smithStats18") + ": ";
		empty += "[FF9000]";
		if (getExploreLevel() < 15)
		{
			string text = empty;
			empty = text + getExploreLevel() + "/" + 15;
		}
		else
		{
			empty += gameData.getTextByRefId("playerStats08");
		}
		empty += "[-]\n";
		if (showCurrentState)
		{
			empty += gameData.getTextByRefIdWithDynText("smithStats07", "[stat]", "[FF9000]$" + CommonAPI.formatNumber(getSmithSalary()) + "[-]\n");
		}
		empty += "[s]                         [/s]\n";
		empty = empty + gameData.getTextByRefId("smithStatsShort02") + ": ";
		if (!showCurrentState)
		{
			int num = getSmithPower() - getSmithAddedPower();
			empty = empty + num + " | ";
		}
		else if (getSmithAddedPower() > 0)
		{
			string text = empty;
			empty = text + "[56AE59]" + getSmithPower() + " (+" + getSmithAddedPower() + ")[-] | ";
		}
		else if (getSmithAddedPower() < 0)
		{
			string text = empty;
			empty = text + "[FF4842]" + getSmithPower() + " (" + getSmithAddedPower() + ")[-] | ";
		}
		else
		{
			empty = empty + getSmithPower() + " | ";
		}
		empty = empty + gameData.getTextByRefId("smithStatsShort03") + ": ";
		if (!showCurrentState)
		{
			int num2 = getSmithIntelligence() - getSmithAddedIntelligence();
			empty = empty + num2 + "\n";
		}
		else if (getSmithAddedIntelligence() > 0)
		{
			string text = empty;
			empty = text + "[56AE59]" + getSmithIntelligence() + " (+" + getSmithAddedIntelligence() + ")[-]\n";
		}
		else if (getSmithAddedIntelligence() < 0)
		{
			string text = empty;
			empty = text + "[FF4842]" + getSmithIntelligence() + " (" + getSmithAddedIntelligence() + ")[-]\n";
		}
		else
		{
			empty = empty + getSmithIntelligence() + "\n";
		}
		empty = empty + gameData.getTextByRefId("smithStatsShort04") + ": ";
		if (!showCurrentState)
		{
			int num3 = getSmithTechnique() - getSmithAddedTechnique();
			empty = empty + num3 + " | ";
		}
		else if (getSmithAddedTechnique() > 0)
		{
			string text = empty;
			empty = text + "[56AE59]" + getSmithTechnique() + " (+" + getSmithAddedTechnique() + ")[-] | ";
		}
		else if (getSmithAddedTechnique() < 0)
		{
			string text = empty;
			empty = text + "[FF4842]" + getSmithTechnique() + " (" + getSmithAddedTechnique() + ")[-] | ";
		}
		else
		{
			empty = empty + getSmithTechnique() + " | ";
		}
		empty = empty + gameData.getTextByRefId("smithStatsShort05") + ": ";
		if (!showCurrentState)
		{
			int num4 = getSmithLuck() - getSmithAddedLuck();
			empty = empty + num4 + "\n";
		}
		else if (getSmithAddedLuck() > 0)
		{
			string text = empty;
			empty = text + "[56AE59]" + getSmithLuck() + " (+" + getSmithAddedLuck() + ")[-]\n";
		}
		else if (getSmithAddedLuck() < 0)
		{
			string text = empty;
			empty = text + "[FF4842]" + getSmithLuck() + " (" + getSmithAddedLuck() + ")[-]\n";
		}
		else
		{
			empty = empty + getSmithLuck() + "\n";
		}
		if (showCurrentState)
		{
			empty = empty + gameData.getTextByRefId("smithStats21") + ": ";
			List<string> smithStatusEffectListNoRepeat = getSmithStatusEffectListNoRepeat();
			if (smithStatusEffectListNoRepeat.Count > 0)
			{
				string text2 = string.Empty;
				foreach (string item in smithStatusEffectListNoRepeat)
				{
					if (text2 != string.Empty)
					{
						text2 += ", ";
					}
					SmithStatusEffect smithStatusEffectByRefId = gameData.getSmithStatusEffectByRefId(item);
					string text = text2;
					text2 = text + smithStatusEffectByRefId.getEffectName() + " (" + smithStatusEffectByRefId.getEffectDesc() + ") ";
					text2 += gameData.getTextByRefIdWithDynText("projectStats07", "[timeLeft]", CommonAPI.convertHalfHoursToTimeString(smithStatusEffectByRefId.getEffectDuration()));
				}
				empty = empty + "[FF9000]" + text2 + "[-]\n";
			}
			else
			{
				empty = empty + "[808080]" + gameData.getTextByRefId("menuGeneral06") + "[-]\n";
			}
		}
		empty += "[s]                         [/s]\n";
		if (showFullJobDetails)
		{
			foreach (SmithExperience experience in experienceList)
			{
				SmithJobClass smithJobClass = gameData.getSmithJobClass(experience.getSmithJobClassRefId());
				if (smithJobClass.getSmithJobRefId() == job.getSmithJobRefId())
				{
					empty += "[FFD84A]";
				}
				empty += smithJobClass.getSmithJobName();
				empty = empty + " " + CommonAPI.getGameData().getTextByRefId("smithStatsShort01") + ": ";
				if (experience.getSmithJobClassLevel() < smithJobClass.getMaxLevel())
				{
					string text = empty;
					empty = text + experience.getSmithJobClassLevel() + "/" + smithJobClass.getMaxLevel() + "\n";
				}
				else
				{
					empty = empty + gameData.getTextByRefId("playerStats08") + "\n";
				}
				if (smithJobClass.getSmithJobRefId() == job.getSmithJobRefId())
				{
					empty += "[-]";
				}
			}
			empty += "[s]                         [/s]\n";
		}
		return empty + "[C0C0C0][i]" + smithDesc + "[/i][-]";
	}

	public string getOutsourceSmithStandardInfoString()
	{
		GameData gameData = CommonAPI.getGameData();
		string empty = string.Empty;
		empty = empty + smithName + "\n";
		empty = empty + job.getSmithJobName() + "\n";
		empty += "[s]                         [/s]\n";
		return empty + "[C0C0C0][i]" + smithDesc + "[/i][-]";
	}

	public string getSmithId()
	{
		return smithId;
	}

	public void setSmithId(string aId)
	{
		smithId = aId;
	}

	public string getSmithRefId()
	{
		return smithRefId;
	}

	public string getSmithName()
	{
		return smithName;
	}

	public string getSmithDesc()
	{
		return smithDesc;
	}

	public int getSmithLevel()
	{
		return getExperienceByJobClass(job.getSmithJobRefId()).getSmithJobClassLevel();
	}

	public bool checkIsSmithMaxLevel()
	{
		if (getSmithLevel() >= job.getMaxLevel())
		{
			return true;
		}
		return false;
	}

	public int getSmithExp()
	{
		return expAmt;
	}

	public void setSmithExp(int aExp)
	{
		expAmt = aExp;
	}

	public SmithExperience getCurrentSmithExperience()
	{
		return getExperienceByJobClass(job.getSmithJobRefId());
	}

	public Dictionary<string, int> addSmithExp(int aExpAmt)
	{
		expAmt += aExpAmt;
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		dictionary.Add("permPowAdd", 0);
		dictionary.Add("permIntAdd", 0);
		dictionary.Add("permTecAdd", 0);
		dictionary.Add("permLucAdd", 0);
		dictionary.Add("permStaAdd", 0);
		dictionary.Add("lvlGain", 0);
		int num = 0;
		SmithExperience currentSmithExperience = getCurrentSmithExperience();
		int expToLevelUp = job.getExpToLevelUp(currentSmithExperience.getSmithJobClassLevel());
		while (!checkIsSmithMaxLevel() && expAmt >= expToLevelUp)
		{
			expAmt -= expToLevelUp;
			currentSmithExperience.addSmithJobClassLevel(1);
			num++;
			int smithJobClassLevel = currentSmithExperience.getSmithJobClassLevel();
			List<int> permBuffForLevel = job.getPermBuffForLevel(smithJobClassLevel);
			doPermPowBuff(permBuffForLevel[0]);
			doPermIntBuff(permBuffForLevel[1]);
			doPermTecBuff(permBuffForLevel[2]);
			doPermLucBuff(permBuffForLevel[3]);
			doPermStaminaBuff(permBuffForLevel[4]);
			dictionary["permPowAdd"] += permBuffForLevel[0];
			dictionary["permIntAdd"] += permBuffForLevel[1];
			dictionary["permTecAdd"] += permBuffForLevel[2];
			dictionary["permLucAdd"] += permBuffForLevel[3];
			dictionary["permStaAdd"] += permBuffForLevel[4];
			expToLevelUp = job.getExpToLevelUp(smithJobClassLevel);
		}
		if (checkIsSmithMaxLevel())
		{
			expAmt = 0;
		}
		dictionary["lvlGain"] += num;
		return dictionary;
	}

	public SmithJobClass getSmithJob()
	{
		return job;
	}

	public int getSmithHireCost()
	{
		return getSmithSalary() * 2 + hireCost;
	}

	public string getSmithPreferredAction()
	{
		return preferredAction;
	}

	public int getSmithPreferredActionChance()
	{
		return preferredActionChance;
	}

	public int calculateCurrentSmithSalary()
	{
		int num = 0;
		GameData gameData = CommonAPI.getGameData();
		foreach (SmithExperience experience in experienceList)
		{
			num += gameData.getSmithJobClass(experience.getSmithJobClassRefId()).getSalaryMult() * (experience.getSmithJobClassLevel() - 1);
		}
		int num2 = 15 * (CommonAPI.getExploreLevel(exploreExp) - 1) + 15 * (CommonAPI.getMerchantLevel(merchantExp) - 1);
		return (int)(Math.Pow((float)num, 1.2200000286102295) + Math.Pow((float)num2, 1.4500000476837158) + (double)(float)baseSalary);
	}

	public int getSmithSalary()
	{
		if (isOutsource)
		{
			return 0;
		}
		if (currentSalary == -1)
		{
			refreshSmithSalary();
		}
		return currentSalary;
	}

	public int refreshSmithSalary()
	{
		currentSalary = calculateCurrentSmithSalary();
		return currentSalary;
	}

	public int getCurrentSmithSalary()
	{
		if (isOutsource)
		{
			return 0;
		}
		return currentSalary;
	}

	public void setCurrentSmithSalary(int aSalary)
	{
		currentSalary = aSalary;
	}

	public int getNextMonthSalary()
	{
		return calculateCurrentSmithSalary();
	}

	public int getSmithPower()
	{
		int num = (int)(((double)((float)basePower + (float)buffPower) + (double)(float)growthPower * Math.Pow(getSmithLevel() - 1, growthType)) * (double)job.getPowMult());
		return num + getSmithAddedPower();
	}

	public int getSmithAddedPower()
	{
		int num = (int)(((double)((float)basePower + (float)buffPower) + (double)(float)growthPower * Math.Pow(getSmithLevel() - 1, growthType)) * (double)job.getPowMult());
		int num2 = 0;
		float num3 = 1f;
		for (int i = 0; i < smithEffectList.Count; i++)
		{
			switch (smithEffectList[i])
			{
			case StatEffect.StatEffectAbsPower:
				num2 += (int)smithEffectValueList[i];
				break;
			case StatEffect.StatEffectMultPower:
				num3 *= smithEffectValueList[i];
				break;
			}
		}
		return num2 + (int)((float)num * (num3 - 1f));
	}

	public int getSmithIntelligence()
	{
		int num = (int)(((double)((float)baseIntelligence + (float)buffIntelligence) + (double)(float)growthIntelligence * Math.Pow(getSmithLevel() - 1, growthType)) * (double)job.getIntMult());
		return num + getSmithAddedIntelligence();
	}

	public int getSmithAddedIntelligence()
	{
		int num = (int)(((double)((float)baseIntelligence + (float)buffIntelligence) + (double)(float)growthIntelligence * Math.Pow(getSmithLevel() - 1, growthType)) * (double)job.getIntMult());
		int num2 = 0;
		float num3 = 1f;
		for (int i = 0; i < smithEffectList.Count; i++)
		{
			switch (smithEffectList[i])
			{
			case StatEffect.StatEffectAbsIntelligence:
				num2 += (int)smithEffectValueList[i];
				break;
			case StatEffect.StatEffectMultIntelligence:
				num3 *= smithEffectValueList[i];
				break;
			}
		}
		return num2 + (int)((float)num * (num3 - 1f));
	}

	public int getSmithTechnique()
	{
		int num = (int)(((double)((float)baseTechnique + (float)buffTechnique) + (double)(float)growthTechnique * Math.Pow(getSmithLevel() - 1, growthType)) * (double)job.getTecMult());
		return num + getSmithAddedTechnique();
	}

	public int getSmithAddedTechnique()
	{
		int num = (int)(((double)((float)baseTechnique + (float)buffTechnique) + (double)(float)growthTechnique * Math.Pow(getSmithLevel() - 1, growthType)) * (double)job.getTecMult());
		int num2 = 0;
		float num3 = 1f;
		for (int i = 0; i < smithEffectList.Count; i++)
		{
			switch (smithEffectList[i])
			{
			case StatEffect.StatEffectAbsTechnique:
				num2 += (int)smithEffectValueList[i];
				break;
			case StatEffect.StatEffectMultTechnique:
				num3 *= smithEffectValueList[i];
				break;
			}
		}
		return num2 + (int)((float)num * (num3 - 1f));
	}

	public int getSmithLuck()
	{
		int num = (int)(((double)((float)baseLuck + (float)buffLuck) + (double)(float)growthLuck * Math.Pow(getSmithLevel() - 1, growthType)) * (double)job.getLucMult());
		return num + getSmithAddedLuck();
	}

	public int getSmithAddedLuck()
	{
		int num = (int)(((double)((float)baseLuck + (float)buffLuck) + (double)(float)growthLuck * Math.Pow(getSmithLevel() - 1, growthType)) * (double)job.getLucMult());
		int num2 = 0;
		float num3 = 1f;
		for (int i = 0; i < smithEffectList.Count; i++)
		{
			switch (smithEffectList[i])
			{
			case StatEffect.StatEffectAbsLuck:
				num2 += (int)smithEffectValueList[i];
				break;
			case StatEffect.StatEffectMultLuck:
				num3 *= smithEffectValueList[i];
				break;
			}
		}
		return num2 + (int)((float)num * (num3 - 1f));
	}

	public int getSmithTotalStats()
	{
		int num = 0;
		num += getSmithPower() - getSmithAddedPower();
		num += getSmithIntelligence() - getSmithAddedIntelligence();
		num += getSmithTechnique() - getSmithAddedTechnique();
		return num + (getSmithLuck() - getSmithAddedLuck());
	}

	public float getSmithMaxMood()
	{
		float num = baseMood + buffMood + growthMood * (float)Math.Pow((float)getSmithLevel() - 1f, growthType);
		return num + getSmithAddedMood();
	}

	public float getSmithAddedMood()
	{
		float num = baseMood + buffMood + growthMood * (float)Math.Pow((float)getSmithLevel() - 1f, growthType);
		float num2 = 0f;
		float num3 = 1f;
		for (int i = 0; i < smithEffectList.Count; i++)
		{
			switch (smithEffectList[i])
			{
			case StatEffect.StatEffectAbsStamina:
				num2 += smithEffectValueList[i];
				break;
			case StatEffect.StatEffectMultStamina:
				num3 *= smithEffectValueList[i];
				break;
			}
		}
		return num2 + num * (num3 - 1f);
	}

	public float getRemainingMood()
	{
		return mood;
	}

	public SmithMood getMoodState()
	{
		if (isOutsource)
		{
			return SmithMood.SmithMoodNormal;
		}
		float moodPercent = mood / getSmithMaxMood();
		return CommonAPI.getMoodState(moodPercent);
	}

	public void setRemainingMood(float aMood)
	{
		mood = aMood;
	}

	public float addSmithMood(float aAdd)
	{
		float smithMaxMood = getSmithMaxMood();
		mood += aAdd;
		if (mood > smithMaxMood)
		{
			mood = smithMaxMood;
		}
		return mood;
	}

	public float reduceSmithMood(float aReduce, bool hasMoodLimit)
	{
		mood -= aReduce;
		if (!hasMoodLimit)
		{
			if (mood < 0f)
			{
				mood = 0f;
			}
		}
		else if (mood < getSmithMaxMood() * 0.3f)
		{
			mood = getSmithMaxMood() * 0.3f;
		}
		return mood;
	}

	public bool decreaseActionDuration(int aElapsed)
	{
		if (smithActionDuration > -1)
		{
			smithActionElapsed += aElapsed;
			if (smithActionDuration <= smithActionElapsed)
			{
				smithActionElapsed = smithActionDuration;
				return true;
			}
		}
		return false;
	}

	public void setSmithAction(SmithAction action, int fixDuration)
	{
		smithAction = action;
		if (fixDuration < 0)
		{
			smithActionDuration = action.getDuration();
		}
		else
		{
			smithActionDuration = fixDuration;
		}
		smithActionElapsed = 0;
	}

	public bool checkSmithInShop()
	{
		SmithActionState actionState = smithAction.getActionState();
		if (actionState == SmithActionState.SmithActionStateAway || actionState == SmithActionState.SmithActionStateStandby || actionState == SmithActionState.SmithActionStateFired)
		{
			return false;
		}
		return true;
	}

	public bool checkSmithInShopOrStandby()
	{
		SmithActionState actionState = smithAction.getActionState();
		if (actionState == SmithActionState.SmithActionStateAway || actionState == SmithActionState.SmithActionStateFired)
		{
			return false;
		}
		return true;
	}

	public float calculateSmithActionProgressPercentage()
	{
		return Math.Min((float)smithActionElapsed / (float)smithActionDuration, 1f);
	}

	public int calculateSmithActionTimeLeft()
	{
		return smithActionDuration - smithActionElapsed;
	}

	public float calculateExploreProgressPercentage()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		foreach (int actionDuration in actionDurationList)
		{
			if (actionDuration > 0)
			{
				if (num3 < actionProgressIndex)
				{
					num += actionDuration;
				}
				else if (num3 == actionProgressIndex)
				{
					num += smithActionElapsed;
				}
				num2 += actionDuration;
			}
			num3++;
		}
		return Math.Min((float)num / (float)num2, 1f);
	}

	public string getSmithActionText()
	{
		string empty = string.Empty;
		empty += smithAction.getText();
		if (exploreArea.getAreaRefId() != string.Empty)
		{
			empty = empty.Replace("[areaName]", exploreArea.getAreaName());
			string text = empty;
			empty = text + ": " + (int)(calculateExploreProgressPercentage() * 100f) + "%";
			empty = empty + "\n(" + CommonAPI.convertSmithExploreStateToDisplayString(smithExploreState) + ")";
		}
		return empty;
	}

	public SmithAction getSmithAction()
	{
		return smithAction;
	}

	public int getSmithActionDuration()
	{
		return smithActionDuration;
	}

	public int getSmithActionElapsed()
	{
		return smithActionElapsed;
	}

	public void setSmithActionElapsed(int aActionElapsed)
	{
		smithActionElapsed = aActionElapsed;
	}

	public int getWorkProgress()
	{
		return workProgress;
	}

	public void useWorkProgress()
	{
		workProgress -= 3;
	}

	public void setWorkProgress(int aProgress)
	{
		workProgress = aProgress;
	}

	public void increaseWorkProgress(int aProgress)
	{
		workProgress += aProgress;
	}

	public void decreaseWorkProgress(int aProgress)
	{
		workProgress -= aProgress;
	}

	public void setSmithName(string aName)
	{
		smithName = aName;
	}

	public void setSmithDesc(string aDesc)
	{
		smithDesc = aDesc;
	}

	public SmithGender getSmithGender()
	{
		return smithGender;
	}

	public void setSmithJob(SmithJobClass aJob)
	{
		job = aJob;
	}

	public void resetExp()
	{
		expAmt = 0;
	}

	public List<StatEffect> getSmithEffectList()
	{
		return smithEffectList;
	}

	public void setSmithEffectList(List<StatEffect> aList)
	{
		smithEffectList = aList;
	}

	public List<float> getSmithEffectValueList()
	{
		return smithEffectValueList;
	}

	public void setSmithEffectValueList(List<float> aList)
	{
		smithEffectValueList = aList;
	}

	public List<int> getSmithEffectDurationList()
	{
		return smithEffectDurationList;
	}

	public void setSmithEffectDurationList(List<int> aList)
	{
		smithEffectDurationList = aList;
	}

	public List<string> getSmithEffectDecoList()
	{
		return smithEffectDecoList;
	}

	public void setSmithEffectDecoList(List<string> aList)
	{
		smithEffectDecoList = aList;
	}

	public List<string> getSmithEffectStatusList()
	{
		return smithStatusEffectList;
	}

	public void setSmithEffectStatusList(List<string> aList)
	{
		smithStatusEffectList = aList;
	}

	public float useForgeBoost()
	{
		float result = 1f;
		for (int i = 0; i < smithEffectList.Count; i++)
		{
			if (smithEffectList[i] == StatEffect.StatEffectForgeBoostNext)
			{
				result = smithEffectValueList[i];
				removeSmithEffectAt(i);
				break;
			}
		}
		return result;
	}

	public void passTimeOnEffects(int aElapsedHalfHours)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < smithEffectList.Count; i++)
		{
			if (smithEffectList[i] != StatEffect.StatEffectForgeBoostNext && smithEffectDecoList[i] == string.Empty)
			{
				smithEffectDurationList[i] -= aElapsedHalfHours;
				if (smithEffectDurationList[i] < 0)
				{
					list.Add(i);
				}
			}
		}
		list.Reverse();
		foreach (int item in list)
		{
			removeSmithEffectAt(item);
		}
	}

	public void addSmithEffect(StatEffect aEffect, float aValue, int aDuration, string aDecoRefId, string aStatusEffectRefId)
	{
		smithEffectList.Add(aEffect);
		smithEffectValueList.Add(aValue);
		smithEffectDurationList.Add(aDuration);
		smithEffectDecoList.Add(aDecoRefId);
		smithStatusEffectList.Add(aStatusEffectRefId);
	}

	public void clearSmithEffects()
	{
		smithEffectList.Clear();
		smithEffectValueList.Clear();
		smithEffectDurationList.Clear();
		smithEffectDecoList.Clear();
		smithStatusEffectList.Clear();
	}

	public void removeSmithEffectAt(int index)
	{
		smithEffectDurationList.RemoveAt(index);
		smithEffectValueList.RemoveAt(index);
		smithEffectDecoList.RemoveAt(index);
		smithEffectList.RemoveAt(index);
		smithStatusEffectList.RemoveAt(index);
	}

	public List<string> getSmithStatusEffectListNoRepeat()
	{
		List<string> list = new List<string>();
		foreach (string smithStatusEffect in smithStatusEffectList)
		{
			if (!list.Contains(smithStatusEffect) && smithStatusEffect != string.Empty)
			{
				list.Add(smithStatusEffect);
			}
		}
		return list;
	}

	public int getSmithStatusEffectDurationByEffectRefID(string effectRefID)
	{
		return smithEffectDurationList[smithStatusEffectList.IndexOf(effectRefID)];
	}

	public bool checkUnlock()
	{
		return isUnlocked;
	}

	public void setUnlock(bool aUnlock)
	{
		isUnlocked = aUnlock;
	}

	public void doUnlock()
	{
		isUnlocked = true;
	}

	public bool checkLegendary()
	{
		return isLegendary;
	}

	public UnlockCondition getUnlockCondition()
	{
		return unlockCondition;
	}

	public int getUnlockConditionValue()
	{
		return unlockConditionValue;
	}

	public string getCheckString()
	{
		return checkString;
	}

	public int getCheckNum()
	{
		return checkNum;
	}

	public bool checkPlayerOwned()
	{
		return isPlayerOwned;
	}

	public void setPlayerOwned(bool aOwned, bool isHire)
	{
		if (!isPlayerOwned && aOwned && isHire)
		{
			addTimesHired(1);
		}
		isPlayerOwned = aOwned;
	}

	public string getUnlockDialogueSetId()
	{
		return unlockDialogueSetId;
	}

	public int getTimesHired()
	{
		return timesHired;
	}

	public void setTimesHired(int aCount)
	{
		timesHired = aCount;
	}

	public void addTimesHired(int aCount)
	{
		timesHired += aCount;
	}

	public bool checkOutsource()
	{
		return isOutsource;
	}

	public void setStatBuffs(int aPow, int aInt, int aTec, int aLuc, float aSta)
	{
		buffPower = aPow;
		buffIntelligence = aInt;
		buffTechnique = aTec;
		buffLuck = aLuc;
		buffMood = aSta;
	}

	public int getPowBuff()
	{
		return buffPower;
	}

	public int getIntBuff()
	{
		return buffIntelligence;
	}

	public int getTecBuff()
	{
		return buffTechnique;
	}

	public int getLucBuff()
	{
		return buffLuck;
	}

	public float getStaBuff()
	{
		return buffMood;
	}

	public void doPermPowBuff(int aAdd)
	{
		buffPower += aAdd;
	}

	public void doPermIntBuff(int aAdd)
	{
		buffIntelligence += aAdd;
	}

	public void doPermTecBuff(int aAdd)
	{
		buffTechnique += aAdd;
	}

	public void doPermLucBuff(int aAdd)
	{
		buffLuck += aAdd;
	}

	public void doPermStaminaBuff(float aAdd)
	{
		buffMood += aAdd;
	}

	public string getImage()
	{
		return image;
	}

	public SmithStation getAssignedRole()
	{
		return assignedRole;
	}

	public void setAssignedRole(SmithStation aStation)
	{
		assignedRole = aStation;
	}

	public SmithStation getCurrentStation()
	{
		return currentStation;
	}

	public void setCurrentStation(SmithStation aStation)
	{
		currentStation = aStation;
	}

	public int getCurrentStationIndex()
	{
		return stationIndex;
	}

	public void setCurrentStationIndex(int aIndex)
	{
		stationIndex = aIndex;
	}

	public List<SmithExperience> getExperienceList()
	{
		return experienceList;
	}

	public void setExperienceList(List<SmithExperience> aList)
	{
		experienceList = aList;
		setRemainingMood(getSmithMaxMood());
	}

	public SmithExperience getExperienceByRefId(string aRefId)
	{
		foreach (SmithExperience experience in experienceList)
		{
			if (experience.getSmithExperienceRefId() == aRefId)
			{
				return experience;
			}
		}
		return new SmithExperience();
	}

	public SmithExperience getExperienceByJobClass(string aRefId)
	{
		foreach (SmithExperience experience in experienceList)
		{
			if (experience.getSmithJobClassRefId() == aRefId)
			{
				return experience;
			}
		}
		return new SmithExperience();
	}

	public int getMaxExp()
	{
		return job.getExpToLevelUp(getSmithLevel());
	}

	public string getCurrentMaxLevelTag()
	{
		SmithExperience currentSmithExperience = getCurrentSmithExperience();
		if (checkIsSmithMaxLevel() && !currentSmithExperience.checkTagGiven())
		{
			return currentSmithExperience.getMaxLevelTagRefId();
		}
		return string.Empty;
	}

	public int getExploreExp()
	{
		return exploreExp;
	}

	public void setExploreExp(int aExp)
	{
		exploreExp = aExp;
	}

	public bool addExploreExp(int aExp)
	{
		int exploreLevel = CommonAPI.getExploreLevel(exploreExp);
		exploreExp += aExp;
		int exploreLevel2 = CommonAPI.getExploreLevel(exploreExp);
		if (exploreLevel2 > exploreLevel)
		{
			return true;
		}
		return false;
	}

	public int getExploreLevel()
	{
		return CommonAPI.getExploreLevel(exploreExp);
	}

	public int getMerchantExp()
	{
		return merchantExp;
	}

	public void setMerchantExp(int aExp)
	{
		merchantExp = aExp;
	}

	public bool addMerchantExp(int aExp)
	{
		int merchantLevel = CommonAPI.getMerchantLevel(merchantExp);
		merchantExp += aExp;
		int merchantLevel2 = CommonAPI.getMerchantLevel(merchantExp);
		if (merchantLevel2 > merchantLevel)
		{
			return true;
		}
		return false;
	}

	public int getMerchantLevel()
	{
		return CommonAPI.getMerchantLevel(merchantExp);
	}

	public string tryAddStatusEffect(bool forceStatusEffect = false)
	{
		GameData gameData = CommonAPI.getGameData();
		AreaStatus randomAreaStatus = getRandomAreaStatus(forceStatusEffect);
		if (randomAreaStatus.getAreaRefID() == string.Empty || randomAreaStatus.getSmithEffectRefID() == "-1")
		{
			return string.Empty;
		}
		SmithStatusEffect smithStatusEffectByRefId = gameData.getSmithStatusEffectByRefId(randomAreaStatus.getSmithEffectRefID());
		removePreviousStatusEffect();
		addSmithEffect(smithStatusEffectByRefId.getEffect1Type(), smithStatusEffectByRefId.getEffect1Value(), smithStatusEffectByRefId.getEffectDuration(), string.Empty, smithStatusEffectByRefId.getEffectRefId());
		if (smithStatusEffectByRefId.getEffect2Type() != StatEffect.StatEffectNothing)
		{
			addSmithEffect(smithStatusEffectByRefId.getEffect2Type(), smithStatusEffectByRefId.getEffect2Value(), smithStatusEffectByRefId.getEffectDuration(), string.Empty, smithStatusEffectByRefId.getEffectRefId());
		}
		return smithStatusEffectByRefId.getEffectComment() + " [D484F5]" + smithStatusEffectByRefId.getEffectDesc() + "[-]";
	}

	public void removePreviousStatusEffect()
	{
		List<int> list = new List<int>();
		for (int i = 0; i < smithStatusEffectList.Count; i++)
		{
			if (smithStatusEffectList[i] != string.Empty)
			{
				list.Add(i);
			}
		}
		list.Reverse();
		foreach (int item in list)
		{
			removeSmithEffectAt(item);
		}
	}

	public List<SmithTag> getSmithTagList()
	{
		return smithTagList;
	}

	public List<string> getTagRefIdList()
	{
		return tagRefIdList;
	}

	public SmithTag getSmithTagByIndex(int aIndex)
	{
		return smithTagList[aIndex];
	}

	public bool replaceSmithTagByIndex(int aIndex, Tag aTag)
	{
		if (aIndex == -1)
		{
			string aSmithTagId = smithRefId + "0" + (smithTagList.Count + 1);
			smithTagList.Add(new SmithTag(aSmithTagId, aTag.getTagRefId(), smithRefId, aReplaceable: true));
			tagRefIdList.Add(aTag.getTagRefId());
			return true;
		}
		SmithTag smithTag = getSmithTagList()[aIndex];
		if (smithTagList[aIndex].getReplaceable())
		{
			smithTagList[aIndex].setTagRefId(aTag.getTagRefId());
			tagRefIdList[aIndex] = aTag.getTagRefId();
			return true;
		}
		return false;
	}

	public bool checkHasSmithTag(string aRefId)
	{
		foreach (SmithTag smithTag in smithTagList)
		{
			if (smithTag.getTagRefId() == aRefId)
			{
				return true;
			}
		}
		return false;
	}

	public SmithTag getSmithTagBySmithTagRefId(string aRefId)
	{
		foreach (SmithTag smithTag in smithTagList)
		{
			if (smithTag.getSmithTagId() == aRefId)
			{
				return smithTag;
			}
		}
		return new SmithTag();
	}

	public void setSmithTagList(List<SmithTag> aTagList)
	{
		smithTagList = aTagList;
		foreach (SmithTag aTag in aTagList)
		{
			tagRefIdList.Add(aTag.getTagRefId());
		}
	}

	public int getDlc()
	{
		return dlc;
	}

	public bool checkScenarioAllow(string aScenario)
	{
		if (scenarioLock.Length > 0)
		{
			string[] array = scenarioLock.Split('@');
			string[] array2 = array;
			foreach (string text in array2)
			{
				if (text == aScenario)
				{
					return false;
				}
			}
		}
		return true;
	}
}
