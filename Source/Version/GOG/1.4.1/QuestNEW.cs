using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestNEW
{
	private string questRefId;

	private string questName;

	private string questDesc;

	private string questEndText;

	private string questGiverEndText;

	private string questUnlockCutscene;

	private string questEndCutscene;

	private QuestNEWType questType;

	private string objectiveRefId;

	private bool clearWithObjective;

	private UnlockCondition unlockCondition;

	private int unlockValue;

	private string[] lockQuests;

	private int questTimeLimit;

	private int forgeTimeLimit;

	private string jobClassRefId;

	private string weaponRefId;

	private int atkReq;

	private int spdReq;

	private int accReq;

	private int magReq;

	private int rewardPoint;

	private bool pointIsVariable;

	private int rewardGold;

	private int alignLaw;

	private int alignChaos;

	private string questGiverName;

	private string questGiverImage;

	private bool isUnlocked;

	private bool isLocked;

	private int expiryDay;

	private bool isExpired;

	private int completeCountLimit;

	private int completeCount;

	private bool isOngoing;

	private string terrainRefId;

	private int milestoneNum;

	private int questTime;

	private int minQuestGold;

	private List<QuestTag> questTagList;

	private List<string> tagRefIdList;

	private int questGoldTotal;

	private List<int> questGoldDivide;

	private int questProgress;

	private int milestonePassed;

	private int heroQuestGoldDrop;

	private string heroQuestString;

	private int challengeLastSet;

	public QuestNEW()
	{
		questRefId = string.Empty;
		questName = string.Empty;
		questDesc = string.Empty;
		questEndText = string.Empty;
		questGiverEndText = string.Empty;
		questUnlockCutscene = string.Empty;
		questEndCutscene = string.Empty;
		questType = QuestNEWType.QuestNEWTypeBlank;
		objectiveRefId = string.Empty;
		clearWithObjective = false;
		unlockCondition = UnlockCondition.UnlockConditionNone;
		unlockValue = 0;
		lockQuests = new string[0];
		questTimeLimit = 0;
		forgeTimeLimit = 0;
		jobClassRefId = string.Empty;
		weaponRefId = string.Empty;
		atkReq = 0;
		spdReq = 0;
		accReq = 0;
		magReq = 0;
		rewardPoint = 0;
		pointIsVariable = false;
		rewardGold = 0;
		alignLaw = 0;
		alignChaos = 0;
		questGiverName = string.Empty;
		questGiverImage = string.Empty;
		isUnlocked = false;
		isLocked = false;
		expiryDay = -1;
		isExpired = false;
		completeCountLimit = 0;
		completeCount = 0;
		isOngoing = false;
		terrainRefId = string.Empty;
		milestoneNum = 0;
		questTime = 0;
		minQuestGold = 0;
		questTagList = new List<QuestTag>();
		tagRefIdList = new List<string>();
		questGoldTotal = 0;
		questGoldDivide = new List<int>();
		questProgress = 0;
		milestonePassed = 0;
		heroQuestGoldDrop = 0;
		heroQuestString = string.Empty;
		challengeLastSet = 0;
	}

	public QuestNEW(string aRefId, string aName, string aDesc, string aEndText, string aGiverEndText, string aUnlockCutscene, string aEndCutscene, QuestNEWType aType, string aObjective, bool aObjectiveClear, UnlockCondition aUnlockCondition, int aUnlockValue, string[] aLock, int aQuestTimeLimit, int aForgeTimeLimit, string aJobClass, string aWeapon, int aAtkReq, int aSpdReq, int aAccReq, int aMagReq, int aPoint, bool aPointVar, int aGold, int aLaw, int aChaos, string aGiverName, string aGiverImage, int aCountLimit, string aTerrainRefId, int aMilestoneNum, int aQuestTime, int aMinQuestGold)
	{
		questRefId = aRefId;
		questName = aName;
		questDesc = aDesc;
		questEndText = aEndText;
		questGiverEndText = aGiverEndText;
		questUnlockCutscene = aUnlockCutscene;
		questEndCutscene = aEndCutscene;
		questType = aType;
		objectiveRefId = aObjective;
		clearWithObjective = aObjectiveClear;
		unlockCondition = aUnlockCondition;
		unlockValue = aUnlockValue;
		lockQuests = aLock;
		questTimeLimit = aQuestTimeLimit;
		forgeTimeLimit = aForgeTimeLimit;
		jobClassRefId = aJobClass;
		weaponRefId = aWeapon;
		atkReq = aAtkReq;
		spdReq = aSpdReq;
		accReq = aAccReq;
		magReq = aMagReq;
		rewardPoint = aPoint;
		pointIsVariable = aPointVar;
		rewardGold = aGold;
		alignLaw = aLaw;
		alignChaos = aChaos;
		questGiverName = aGiverName;
		questGiverImage = aGiverImage;
		isUnlocked = false;
		isLocked = false;
		expiryDay = -1;
		isExpired = false;
		completeCountLimit = aCountLimit;
		completeCount = 0;
		isOngoing = false;
		terrainRefId = aTerrainRefId;
		milestoneNum = aMilestoneNum;
		questTime = aQuestTime;
		minQuestGold = aMinQuestGold;
		questTagList = new List<QuestTag>();
		tagRefIdList = new List<string>();
		questGoldTotal = 0;
		questGoldDivide = new List<int>();
		questProgress = 0;
		milestonePassed = 0;
		heroQuestGoldDrop = 0;
		heroQuestString = string.Empty;
		challengeLastSet = 0;
		if (questType == QuestNEWType.QuestNEWTypeChallenge)
		{
			makeChallenge();
		}
	}

	public string getQuestRefId()
	{
		return questRefId;
	}

	public string getQuestName()
	{
		return questName;
	}

	public string getQuestDesc()
	{
		return questDesc;
	}

	public string getQuestEndText()
	{
		return questEndText;
	}

	public string getQuestGiverEndText()
	{
		return questGiverEndText;
	}

	public QuestNEWType getQuestType()
	{
		return questType;
	}

	public string getObjectiveRefId()
	{
		return objectiveRefId;
	}

	public bool checkClearWithObjective()
	{
		return clearWithObjective;
	}

	public UnlockCondition getUnlockCondition()
	{
		return unlockCondition;
	}

	public int getUnlockValue()
	{
		return unlockValue;
	}

	public string getJobClassRefId()
	{
		return jobClassRefId;
	}

	public string getWeaponRefId()
	{
		return weaponRefId;
	}

	public int getAtkReq()
	{
		return atkReq;
	}

	public int getSpdReq()
	{
		return spdReq;
	}

	public int getAccReq()
	{
		return accReq;
	}

	public int getMagReq()
	{
		return magReq;
	}

	public int getTotalReq()
	{
		return atkReq + spdReq + accReq + magReq;
	}

	public List<int> getReqList()
	{
		List<int> list = new List<int>();
		list.Add(atkReq);
		list.Add(spdReq);
		list.Add(accReq);
		list.Add(magReq);
		return list;
	}

	public string getReqString()
	{
		string text = " | ";
		if (atkReq > 0)
		{
			string text2 = text;
			text = text2 + "Atk " + atkReq + " | ";
		}
		if (spdReq > 0)
		{
			string text2 = text;
			text = text2 + "Spd " + spdReq + " | ";
		}
		if (accReq > 0)
		{
			string text2 = text;
			text = text2 + "Acc " + accReq + " | ";
		}
		if (magReq > 0)
		{
			string text2 = text;
			text = text2 + "Mag " + magReq + " | ";
		}
		return text;
	}

	public int getRewardPoint(float weaponStatRatio)
	{
		if (pointIsVariable)
		{
			return (int)((float)rewardPoint * (float)Math.Pow(weaponStatRatio, 0.800000011920929));
		}
		return rewardPoint;
	}

	public int getRewardGold()
	{
		return rewardGold;
	}

	public int getQuestLaw()
	{
		return alignLaw;
	}

	public int getQuestChaos()
	{
		return alignChaos;
	}

	public string getQuestGiverName()
	{
		return questGiverName;
	}

	public string getQuestGiverImage()
	{
		return questGiverImage;
	}

	public bool tryQuestUnlock(int playerDays)
	{
		if (!isUnlocked && !isExpired)
		{
			isUnlocked = true;
			if (questTimeLimit > 0)
			{
				expiryDay = playerDays + questTimeLimit;
			}
			if (questUnlockCutscene != string.Empty && questUnlockCutscene != "-1")
			{
				return true;
			}
		}
		return false;
	}

	public bool tryQuestExpire(int playerDays)
	{
		if (isUnlocked && playerDays > expiryDay && !isExpired)
		{
			isUnlocked = false;
			isExpired = true;
			return true;
		}
		return false;
	}

	public bool getUnlocked()
	{
		return isUnlocked;
	}

	public void setUnlocked(bool aUnlocked)
	{
		isUnlocked = aUnlocked;
	}

	public bool getLocked()
	{
		return isLocked;
	}

	public void setLocked(bool aLocked)
	{
		isLocked = aLocked;
	}

	public bool getOngoing()
	{
		return isOngoing;
	}

	public void setOngoing(bool aOngoing)
	{
		isOngoing = aOngoing;
	}

	public string[] getLockQuests()
	{
		return lockQuests;
	}

	public int getExpiryDay()
	{
		return expiryDay;
	}

	public void setExpiryDay(int aExpiryDay)
	{
		expiryDay = aExpiryDay;
	}

	public bool getExpired()
	{
		return isExpired;
	}

	public void setExpired(bool aExpired)
	{
		isExpired = aExpired;
	}

	public int getCompleteCount()
	{
		return completeCount;
	}

	public bool checkCompleteCount()
	{
		if (completeCount < completeCountLimit || completeCountLimit == -1)
		{
			return true;
		}
		return false;
	}

	public void setCompleteCount(int aCount)
	{
		completeCount = aCount;
		if (questType == QuestNEWType.QuestNEWTypeChallenge)
		{
			makeChallenge();
		}
	}

	public void addCompleteCount(int aCount)
	{
		completeCount += aCount;
		if (questType == QuestNEWType.QuestNEWTypeChallenge)
		{
			makeChallenge();
		}
	}

	public int getForgeTimeLimit()
	{
		return forgeTimeLimit;
	}

	public string getTimeLimitString()
	{
		return CommonAPI.convertHalfHoursToTimeString(forgeTimeLimit);
	}

	public string getTerrainRefId()
	{
		return terrainRefId;
	}

	public void setQuestGold(Project project)
	{
		float weaponStatRatio = project.getWeaponStatRatio();
		questGoldTotal = (int)((float)minQuestGold * (float)Math.Pow(weaponStatRatio, 0.800000011920929));
		questGoldDivide = new List<int>();
		for (int i = 0; i < 10; i++)
		{
			if (i < milestoneNum)
			{
				questGoldDivide.Add(1);
				continue;
			}
			int index = UnityEngine.Random.Range(0, milestoneNum);
			questGoldDivide[index]++;
		}
		int num = 0;
		for (int j = 0; j < questGoldDivide.Count; j++)
		{
			int num2 = 0;
			if (j == questGoldDivide.Count - 1)
			{
				num2 = questGoldTotal - num;
			}
			else
			{
				num2 = (int)((float)questGoldDivide[j] * (float)questGoldTotal / 10f);
				num += num2;
			}
			questGoldDivide[j] = num2;
		}
	}

	public int getMilestoneNum()
	{
		return milestoneNum;
	}

	public int getMinQuestGold()
	{
		return minQuestGold;
	}

	public float getQuestProgressPercentage()
	{
		return (float)questProgress / (float)questTime;
	}

	public int addProgress(int timePass)
	{
		int num = 0;
		for (int i = 0; i < timePass; i++)
		{
			questProgress++;
			if (questProgress > questTime)
			{
				questProgress = questTime;
			}
			if (milestonePassed < milestoneNum)
			{
				float num2 = (float)(milestonePassed + 1) / (float)(milestoneNum + 1);
				if (getQuestProgressPercentage() >= num2)
				{
					num += questGoldDivide[milestonePassed];
					milestonePassed++;
				}
			}
		}
		return num;
	}

	public int getQuestGoldTotal()
	{
		return questGoldTotal;
	}

	public List<int> getQuestGoldDivide()
	{
		return questGoldDivide;
	}

	public int getQuestTime()
	{
		return questTime;
	}

	public int getQuestProgress()
	{
		return questProgress;
	}

	public int getMilestonePassed()
	{
		return milestonePassed;
	}

	public void setQuestGoldTotal(int aTotal)
	{
		questGoldTotal = aTotal;
	}

	public void setQuestGoldDivide(List<int> aDivide)
	{
		questGoldDivide = aDivide;
	}

	public void setQuestProgress(int aProgress)
	{
		questProgress = aProgress;
	}

	public void setMilestonePassed(int aPassed)
	{
		milestonePassed = aPassed;
	}

	public int getHeroQuestGoldDrop()
	{
		return heroQuestGoldDrop;
	}

	public void setHeroQuestGoldDrop(int aDrop)
	{
		heroQuestGoldDrop = aDrop;
	}

	public string getHeroQuestString()
	{
		return heroQuestString;
	}

	public void setHeroQuestString(string aString)
	{
		heroQuestString = aString;
	}

	public void resetQuestDisplay()
	{
		heroQuestGoldDrop = 0;
		heroQuestString = string.Empty;
	}

	public List<QuestTag> getQuestTagList()
	{
		if (questType == QuestNEWType.QuestNEWTypeChallenge)
		{
			return getChallengeQuestTagList();
		}
		return questTagList;
	}

	public List<string> getTagRefIdList()
	{
		if (questType == QuestNEWType.QuestNEWTypeChallenge)
		{
			List<QuestTag> challengeQuestTagList = getChallengeQuestTagList();
			List<string> list = new List<string>();
			{
				foreach (QuestTag item in challengeQuestTagList)
				{
					list.Add(item.getTagRefId());
				}
				return list;
			}
		}
		return tagRefIdList;
	}

	public QuestTag getQuestTagByQuestTagRefId(string aRefId)
	{
		foreach (QuestTag questTag in questTagList)
		{
			if (questTag.getQuestTagRefId() == aRefId)
			{
				return questTag;
			}
		}
		return new QuestTag();
	}

	public void setQuestTagList(List<QuestTag> aTagList)
	{
		questTagList = aTagList;
		foreach (QuestTag aTag in aTagList)
		{
			tagRefIdList.Add(aTag.getTagRefId());
		}
	}

	public int getChallengeLastSet()
	{
		return challengeLastSet;
	}

	public void setChallenge(int aAtkReq, int aSpdReq, int aAccReq, int aMagReq, int aMilestoneNum, int aQuestTime, int aMinQuestGold, int aChallengeLastSet)
	{
		atkReq = aAtkReq;
		spdReq = aSpdReq;
		accReq = aAccReq;
		magReq = aMagReq;
		milestoneNum = aMilestoneNum;
		questTime = aQuestTime;
		minQuestGold = aMinQuestGold;
		challengeLastSet = aChallengeLastSet;
	}

	public void makeChallenge()
	{
		if (challengeLastSet != completeCount + 1)
		{
			atkReq = (int)((double)(float)atkReq + 20.0 * Math.Pow((float)(completeCount + 1), 1.2999999523162842));
			spdReq = (int)((double)(float)spdReq + 20.0 * Math.Pow((float)(completeCount + 1), 1.2999999523162842));
			accReq = (int)((double)(float)accReq + 20.0 * Math.Pow((float)(completeCount + 1), 1.2999999523162842));
			magReq = (int)((double)(float)magReq + 20.0 * Math.Pow((float)(completeCount + 1), 1.2999999523162842));
			rewardGold += (int)(300.0 * Math.Pow((float)(completeCount + 1), 0.550000011920929));
			challengeLastSet = completeCount + 1;
		}
	}

	public List<QuestTag> getChallengeQuestTagList()
	{
		List<QuestTag> list = new List<QuestTag>();
		foreach (QuestTag questTag in questTagList)
		{
			if (questTag.getSetNum() == completeCount % 4 + 1)
			{
				list.Add(questTag);
			}
		}
		return list;
	}
}
