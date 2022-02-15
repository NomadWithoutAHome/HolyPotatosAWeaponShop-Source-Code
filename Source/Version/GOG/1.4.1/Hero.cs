using System;
using System.Collections.Generic;

[Serializable]
public class Hero
{
	private string heroRefId;

	private string heroName;

	private string heroDesc;

	private string jobClassName;

	private string jobClassDesc;

	private int heroTier;

	private int lvl;

	private int maxLvl;

	private string image;

	private float baseAtk;

	private float baseSpd;

	private float baseAcc;

	private float baseMag;

	private WeaponStat priStat;

	private WeaponStat secStat;

	private int wealth;

	private int sellExp;

	private int requestLevelMin;

	private int requestLevelMax;

	private string requestText;

	private Dictionary<string, int> weaponTypeAffinity;

	private string rewardSetId;

	private int expPoints;

	private int timesBought;

	private bool isUnlocked;

	private bool isLoyaltyRewardGiven;

	private string scenarioLock;

	private int dlc;

	public Hero()
	{
		heroRefId = string.Empty;
		heroName = string.Empty;
		heroDesc = string.Empty;
		jobClassName = string.Empty;
		jobClassDesc = string.Empty;
		heroTier = 0;
		lvl = -1;
		maxLvl = 0;
		image = string.Empty;
		baseAtk = 0f;
		baseSpd = 0f;
		baseAcc = 0f;
		baseMag = 0f;
		priStat = WeaponStat.WeaponStatNone;
		secStat = WeaponStat.WeaponStatNone;
		wealth = 0;
		sellExp = 0;
		requestLevelMin = 0;
		requestLevelMax = 0;
		requestText = string.Empty;
		weaponTypeAffinity = new Dictionary<string, int>();
		rewardSetId = string.Empty;
		expPoints = 0;
		dlc = 0;
		timesBought = 0;
		isUnlocked = false;
		isLoyaltyRewardGiven = false;
		scenarioLock = string.Empty;
	}

	public Hero(string aHeroRefId, string aHeroName, string aHeroDesc, string aJobClassName, string aJobClassDesc, int aHeroTier, string aImage, float aBaseAtk, float aBaseSpd, float aBaseAcc, float aBaseMag, WeaponStat aPriStat, WeaponStat aSecStat, int aWealth, int aSellExp, int aRequestLevelMin, int aRequestLevelMax, string aRequestText, Dictionary<string, int> aWeaponTypeAffinity, string aRewardSetId, int aInitExpPoints, string aScenarioLock, int aDlc)
	{
		heroRefId = aHeroRefId;
		heroName = aHeroName;
		heroDesc = aHeroDesc;
		jobClassName = aJobClassName;
		jobClassDesc = aJobClassDesc;
		heroTier = aHeroTier;
		maxLvl = CommonAPI.getHeroMaxLevel(aHeroTier);
		image = aImage;
		baseAtk = aBaseAtk;
		baseSpd = aBaseSpd;
		baseAcc = aBaseAcc;
		baseMag = aBaseMag;
		priStat = aPriStat;
		secStat = aSecStat;
		wealth = aWealth;
		sellExp = aSellExp;
		requestLevelMin = aRequestLevelMin;
		requestLevelMax = aRequestLevelMax;
		requestText = aRequestText;
		weaponTypeAffinity = aWeaponTypeAffinity;
		rewardSetId = aRewardSetId;
		expPoints = aInitExpPoints;
		dlc = aDlc;
		timesBought = 0;
		isUnlocked = false;
		isLoyaltyRewardGiven = false;
		scenarioLock = aScenarioLock;
		lvl = -1;
		getHeroLevel();
	}

	public string getHeroRefId()
	{
		return heroRefId;
	}

	public string getHeroName()
	{
		return heroName;
	}

	public string getHeroDesc()
	{
		return heroDesc;
	}

	public string getJobClassName()
	{
		return jobClassName;
	}

	public string getJobClassDesc()
	{
		return jobClassDesc;
	}

	public int getHeroTier()
	{
		return heroTier;
	}

	public string getImage()
	{
		return image;
	}

	public float getBaseAtk()
	{
		return baseAtk;
	}

	public float getBaseSpd()
	{
		return baseSpd;
	}

	public float getBaseAcc()
	{
		return baseAcc;
	}

	public float getBaseMag()
	{
		return baseMag;
	}

	public WeaponStat getPriStat()
	{
		return priStat;
	}

	public WeaponStat getSecStat()
	{
		return secStat;
	}

	public int getWealth()
	{
		return wealth;
	}

	public int getSellExp()
	{
		return sellExp;
	}

	public int getRequestLevelMin()
	{
		return requestLevelMin;
	}

	public int getRequestLevelMax()
	{
		return requestLevelMax;
	}

	public string getRequestText()
	{
		return requestText;
	}

	public int getAffinity(string aTypeRefId)
	{
		if (weaponTypeAffinity.ContainsKey(aTypeRefId))
		{
			return weaponTypeAffinity[aTypeRefId];
		}
		return 2;
	}

	public string getRewardSetId()
	{
		return rewardSetId;
	}

	public int getHeroLevel()
	{
		if (lvl == -1)
		{
			lvl = Math.Min(maxLvl, CommonAPI.getHeroLevel(expPoints));
		}
		return lvl;
	}

	public int getHeroMaxLevel()
	{
		return maxLvl;
	}

	public bool checkHeroMaxLevel()
	{
		if (getHeroLevel() >= maxLvl)
		{
			return true;
		}
		return false;
	}

	public List<string> getRequestWeaponTypeList()
	{
		List<string> list = new List<string>();
		foreach (string key in weaponTypeAffinity.Keys)
		{
			if (weaponTypeAffinity[key] >= 2)
			{
				list.Add(key);
			}
		}
		return list;
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

	public int getExpPoints()
	{
		return expPoints;
	}

	public void setExpPoints(int aExpPoints)
	{
		expPoints = aExpPoints;
		lvl = Math.Min(maxLvl, CommonAPI.getHeroLevel(expPoints));
	}

	public void addExpPoints(int aExpPoints)
	{
		expPoints += aExpPoints;
		int num = Math.Min(getHeroMaxLevel(), CommonAPI.getHeroLevel(expPoints));
		if (lvl != num)
		{
			lvl = num;
		}
	}

	public int getLevelExpPoints()
	{
		if (!checkHeroMaxLevel())
		{
			int heroLevelExp = CommonAPI.getHeroLevelExp(getHeroLevel());
			return getExpPoints() - heroLevelExp;
		}
		return -1;
	}

	public int getLevelMaxExpPoints()
	{
		if (!checkHeroMaxLevel())
		{
			int heroLevelExp = CommonAPI.getHeroLevelExp(getHeroLevel());
			int heroLevelExp2 = CommonAPI.getHeroLevelExp(getHeroLevel() + 1);
			return heroLevelExp2 - heroLevelExp;
		}
		return -1;
	}

	public int getTimesBought()
	{
		return timesBought;
	}

	public void setTimesBought(int aTimesBought)
	{
		timesBought = aTimesBought;
	}

	public void addTimesBought(int aTimesBought)
	{
		timesBought += aTimesBought;
	}

	public bool checkUnlocked()
	{
		return isUnlocked;
	}

	public void setUnlocked(bool aUnlock)
	{
		isUnlocked = aUnlock;
	}

	public bool checkLoyaltyRewardGiven()
	{
		return isLoyaltyRewardGiven;
	}

	public void setLoyaltyRewardGiven(bool aGiven)
	{
		isLoyaltyRewardGiven = aGiven;
	}

	public string getHeroStandardInfoString(Area aArea = null)
	{
		GameData gameData = CommonAPI.getGameData();
		string empty = string.Empty;
		empty = getHeroName() + "\n";
		empty = empty + getJobClassName() + " ";
		empty = empty + gameData.getTextByRefIdWithDynText("heroStat08", "[level]", getHeroLevel().ToString()) + " ";
		if (checkHeroMaxLevel())
		{
			empty = empty + gameData.getTextByRefId("heroStat10") + "\n";
		}
		else
		{
			List<string> list = new List<string>();
			list.Add("[exp]");
			list.Add("[max]");
			List<string> list2 = new List<string>();
			list2.Add(getLevelExpPoints().ToString());
			list2.Add(getLevelMaxExpPoints().ToString());
			empty = empty + gameData.getTextByRefIdWithDynTextList("heroStat09", list, list2) + "\n";
			empty = empty + gameData.getTextByRefIdWithDynText("heroStat11", "[maxLevel]", getHeroMaxLevel().ToString()) + "\n";
		}
		empty = empty + "[FF9000][i]" + gameData.getAreaByHeroRefId(heroRefId).getAreaName() + "[/i][-]\n";
		empty += "[s]                         [/s]\n";
		empty += getHeroUnlockableInfoString();
		empty += "[s]                         [/s]\n";
		return empty + "[C0C0C0][i]" + getHeroDesc() + "[/i][-]";
	}

	public float getAppearancePercentInArea(Area aArea)
	{
		float num = 0f;
		int num2 = 0;
		int num3 = 0;
		Dictionary<string, int> heroList = aArea.getHeroList();
		foreach (string key in heroList.Keys)
		{
			num2 += heroList[key];
			if (key == heroRefId)
			{
				num3 = heroList[key];
			}
		}
		return (float)num3 / (float)num2;
	}

	public string getHeroUnlockableInfoString()
	{
		string text = string.Empty;
		int heroLevel = getHeroLevel();
		GameData gameData = CommonAPI.getGameData();
		List<WeaponStat> preferredStats = getPreferredStats();
		if (preferredStats.Count > 0 && preferredStats[0] != WeaponStat.WeaponStatNone)
		{
			string replaceValue = "[" + CommonAPI.getStatColorHex(preferredStats[0]) + "]" + CommonAPI.convertWeaponStatToString(preferredStats[0]) + "[-]";
			text = text + gameData.getTextByRefIdWithDynText("heroStat12", "[stat]", replaceValue) + "\n";
		}
		if (preferredStats.Count > 1 && preferredStats[1] != WeaponStat.WeaponStatNone)
		{
			string replaceValue2 = "[" + CommonAPI.getStatColorHex(preferredStats[1]) + "]" + CommonAPI.convertWeaponStatToString(preferredStats[1]) + "[-]";
			text = text + gameData.getTextByRefIdWithDynText("heroStat13", "[stat]", replaceValue2) + "\n";
		}
		List<string> preferredWeaponTypeList = getPreferredWeaponTypeList();
		if (preferredWeaponTypeList.Count > 0)
		{
			string text2 = string.Empty;
			foreach (WeaponType weaponType in gameData.getWeaponTypeList())
			{
				if (preferredWeaponTypeList.Contains(weaponType.getWeaponTypeRefId()))
				{
					if (text2 != string.Empty)
					{
						text2 += ", ";
					}
					text2 += weaponType.getWeaponTypeName();
				}
			}
			string text3 = text;
			text = text3 + gameData.getTextByRefId("heroStat05") + ": [FF9000]" + text2 + "[-]\n";
		}
		List<string> dislikedWeaponTypeList = getDislikedWeaponTypeList();
		if (dislikedWeaponTypeList.Count > 0)
		{
			string text4 = string.Empty;
			foreach (WeaponType weaponType2 in gameData.getWeaponTypeList())
			{
				if (dislikedWeaponTypeList.Contains(weaponType2.getWeaponTypeRefId()))
				{
					if (text4 != string.Empty)
					{
						text4 += ", ";
					}
					text4 += weaponType2.getWeaponTypeName();
				}
			}
			string text3 = text;
			text = text3 + gameData.getTextByRefId("heroStat06") + ": [FF9000]" + text4 + "[-]\n";
		}
		return text;
	}

	public string getWealthLevelString()
	{
		GameData gameData = CommonAPI.getGameData();
		if (wealth < 103)
		{
			return gameData.getTextByRefId("heroWealth01");
		}
		if (wealth < 127)
		{
			return gameData.getTextByRefId("heroWealth02");
		}
		if (wealth < 231)
		{
			return gameData.getTextByRefId("heroWealth03");
		}
		if (wealth < 511)
		{
			return gameData.getTextByRefId("heroWealth04");
		}
		if (wealth < 1101)
		{
			return gameData.getTextByRefId("heroWealth05");
		}
		if (wealth < 2175)
		{
			return gameData.getTextByRefId("heroWealth06");
		}
		return gameData.getTextByRefId("heroWealth07");
	}

	public List<WeaponStat> getPreferredStats()
	{
		List<WeaponStat> list = new List<WeaponStat>();
		list.Add(priStat);
		list.Add(secStat);
		return list;
	}

	public List<string> getPreferredStatsString()
	{
		List<WeaponStat> preferredStats = getPreferredStats();
		List<string> list = new List<string>();
		foreach (WeaponStat item in preferredStats)
		{
			list.Add(CommonAPI.convertWeaponStatToString(item));
		}
		return list;
	}

	public List<string> getPreferredWeaponTypeList()
	{
		List<string> list = new List<string>();
		foreach (string key in weaponTypeAffinity.Keys)
		{
			if (weaponTypeAffinity[key] > 2)
			{
				list.Add(key);
			}
		}
		return list;
	}

	public List<string> getDislikedWeaponTypeList()
	{
		List<string> list = new List<string>();
		foreach (string key in weaponTypeAffinity.Keys)
		{
			if (weaponTypeAffinity[key] < 2)
			{
				list.Add(key);
			}
		}
		return list;
	}
}
