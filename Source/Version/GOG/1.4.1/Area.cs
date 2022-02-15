using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[Serializable]
public class Area
{
	private string areaRefId;

	private string areaName;

	private string areaDesc;

	private Vector2 areaCoordinate;

	private Vector2 position;

	private float scale;

	private string areaImage;

	private Dictionary<string, int> heroList;

	private Dictionary<string, int> rareHeroList;

	private Dictionary<string, ExploreItem> exploreList;

	private Dictionary<string, ShopItem> shopItemList;

	private AreaType areaType;

	private bool canSell;

	private bool canBuy;

	private bool canExplore;

	private string trainingPackageRefId;

	private string vacationPackageRefId;

	private int travelTime;

	private float moodFactor;

	private int refreshPrice;

	private string statusEffectRefId;

	private int expGrowth;

	private List<string> unlockAreaList;

	private int unlockTickets;

	private int region;

	private List<string> areaSmithRefID;

	private bool isUnlocked;

	private int timesExplored;

	private int timesBuyItems;

	private int timesSell;

	private int timesTrain;

	private int timesVacation;

	private AreaEvent currentEvent;

	private long startTime;

	private string eventHeroRefId;

	private string eventTypeRefId;

	public Area()
	{
		areaRefId = string.Empty;
		areaName = string.Empty;
		areaDesc = string.Empty;
		areaCoordinate = default(Vector2);
		position = default(Vector2);
		scale = 0f;
		areaImage = string.Empty;
		heroList = new Dictionary<string, int>();
		rareHeroList = new Dictionary<string, int>();
		exploreList = new Dictionary<string, ExploreItem>();
		shopItemList = new Dictionary<string, ShopItem>();
		areaType = AreaType.AreaTypeBlank;
		canSell = false;
		canBuy = false;
		canExplore = false;
		trainingPackageRefId = string.Empty;
		vacationPackageRefId = string.Empty;
		travelTime = 0;
		moodFactor = 0f;
		refreshPrice = 0;
		statusEffectRefId = string.Empty;
		expGrowth = 0;
		unlockAreaList = new List<string>();
		unlockTickets = 0;
		region = 0;
		areaSmithRefID = new List<string>();
		isUnlocked = false;
		timesExplored = 0;
		timesBuyItems = 0;
		timesSell = 0;
		timesTrain = 0;
		timesVacation = 0;
		clearCurrentEvent();
	}

	public Area(string aRefId, string aName, string aDesc, string aCoordinate, string aPosition, float aScale, string aImage, Dictionary<string, int> aHeroList, Dictionary<string, int> aRareHeroList, Dictionary<string, ExploreItem> aExploreList, Dictionary<string, ShopItem> aShopItemList, AreaType aAreaType, bool aCanSell, bool aCanBuy, bool aCanExplore, string aTraining, string aVacation, int aTravelTime, float aMoodFactor, int aRefreshPrice, string aStatus, int aExpGrowth, List<string> aUnlockAreaList, int aUnlockTickets, int aRegion)
	{
		areaRefId = aRefId;
		areaName = aName;
		areaDesc = aDesc;
		string[] array = aCoordinate.Split(',');
		if (array.Length > 1)
		{
			areaCoordinate = new Vector2(CommonAPI.parseInt(array[0]), CommonAPI.parseInt(array[1]));
		}
		else
		{
			areaCoordinate = new Vector2(-1f, -1f);
		}
		string[] array2 = aPosition.Split(',');
		if (array2.Length > 1)
		{
			position = new Vector2(CommonAPI.parseFloat(array2[0]), CommonAPI.parseFloat(array2[1]));
		}
		else
		{
			position = new Vector2(-1f, -1f);
		}
		scale = aScale;
		areaImage = aImage;
		heroList = aHeroList;
		rareHeroList = aRareHeroList;
		exploreList = aExploreList;
		shopItemList = aShopItemList;
		areaType = aAreaType;
		canSell = aCanSell;
		canBuy = aCanBuy;
		canExplore = aCanExplore;
		trainingPackageRefId = aTraining;
		vacationPackageRefId = aVacation;
		travelTime = aTravelTime;
		moodFactor = aMoodFactor;
		refreshPrice = aRefreshPrice;
		statusEffectRefId = aStatus;
		expGrowth = aExpGrowth;
		unlockAreaList = aUnlockAreaList;
		unlockTickets = aUnlockTickets;
		region = aRegion;
		areaSmithRefID = new List<string>();
		if (unlockTickets < 1)
		{
			isUnlocked = true;
		}
		else
		{
			isUnlocked = false;
		}
		timesExplored = 0;
		timesBuyItems = 0;
		timesSell = 0;
		timesTrain = 0;
		timesVacation = 0;
		clearCurrentEvent();
	}

	public string getAreaRefId()
	{
		return areaRefId;
	}

	public string getAreaName()
	{
		return areaName;
	}

	public string getAreaDesc()
	{
		return areaDesc;
	}

	public Vector2 getAreaCoordinate()
	{
		return areaCoordinate;
	}

	public Vector2 getPosition()
	{
		return position;
	}

	public float getScale()
	{
		return scale;
	}

	public string getAreaImage()
	{
		return areaImage;
	}

	public Dictionary<string, int> getHeroList()
	{
		return heroList;
	}

	public List<string> getHeroRefIdList()
	{
		List<string> list = new List<string>();
		foreach (string key in heroList.Keys)
		{
			list.Add(key);
		}
		return list;
	}

	public List<int> getHeroChanceList()
	{
		List<int> list = new List<int>();
		foreach (string key in heroList.Keys)
		{
			list.Add(heroList[key]);
		}
		return list;
	}

	public Dictionary<string, int> getRareHeroList()
	{
		return rareHeroList;
	}

	public List<string> getRareHeroRefIdList()
	{
		List<string> list = new List<string>();
		foreach (string key in rareHeroList.Keys)
		{
			list.Add(key);
		}
		list.Add(string.Empty);
		return list;
	}

	public List<int> getRareHeroChanceList()
	{
		List<int> list = new List<int>();
		int num = 0;
		foreach (string key in rareHeroList.Keys)
		{
			list.Add(rareHeroList[key]);
			num += rareHeroList[key];
		}
		list.Add(1000 - num);
		return list;
	}

	public Dictionary<string, ExploreItem> getExploreItemList()
	{
		return exploreList;
	}

	public int getItemFoundAmount()
	{
		int num = 0;
		foreach (ExploreItem value in exploreList.Values)
		{
			if (value.getFound())
			{
				num++;
			}
		}
		return num;
	}

	public string getItemFoundString()
	{
		string text = string.Empty;
		bool flag = true;
		foreach (ExploreItem value in exploreList.Values)
		{
			if (value.getFound())
			{
				if (!flag)
				{
					text += ", ";
				}
				Item itemByRefId = CommonAPI.getGameData().getItemByRefId(value.getItemRefID());
				text += itemByRefId.getItemName();
				if (flag)
				{
					flag = false;
				}
			}
		}
		return text;
	}

	public void setExploreItemFound(string aRefID)
	{
		if (exploreList.ContainsKey(aRefID))
		{
			exploreList[aRefID].setFound(aFound: true);
		}
	}

	public Dictionary<string, ShopItem> getShopItemList()
	{
		return shopItemList;
	}

	public AreaType getAreaType()
	{
		return areaType;
	}

	public int getTravelTime()
	{
		return travelTime;
	}

	public int getExpGrowth()
	{
		return expGrowth;
	}

	public bool checkCanSell()
	{
		return canSell;
	}

	public bool checkCanBuy()
	{
		return canBuy;
	}

	public bool checkCanExplore()
	{
		return canExplore;
	}

	public string getTrainingPackageRefId()
	{
		return trainingPackageRefId;
	}

	public string getVacationPackageRefId()
	{
		return vacationPackageRefId;
	}

	public string getStatusEffect()
	{
		return statusEffectRefId;
	}

	public float getMoodFactor()
	{
		return moodFactor;
	}

	public int getRefreshPrice()
	{
		return refreshPrice;
	}

	public List<string> getUnlockAreaList()
	{
		return unlockAreaList;
	}

	public int getUnlockTickets()
	{
		return unlockTickets;
	}

	public int getRegion()
	{
		return region;
	}

	public bool trySetCurrentEvent(AreaEvent aEvent, long aStart, string aScenario)
	{
		if (areaType != AreaType.AreaTypeHome && isUnlocked && currentEvent.getAreaEventRefId() == string.Empty)
		{
			currentEvent = aEvent;
			startTime = aStart;
			eventHeroRefId = string.Empty;
			eventTypeRefId = string.Empty;
			switch (currentEvent.getEffectType())
			{
			case "HERO":
			{
				List<string> heroRefIdList = getHeroRefIdList();
				if (heroRefIdList.Count > 0)
				{
					int index2 = UnityEngine.Random.Range(0, heroRefIdList.Count);
					eventHeroRefId = heroRefIdList[index2];
					break;
				}
				clearCurrentEvent();
				return false;
			}
			case "WEAPON_TYPE":
			{
				List<string> preferredWeaponTypesInArea = CommonAPI.getGameData().getPreferredWeaponTypesInArea(areaRefId, aScenario);
				int index = UnityEngine.Random.Range(0, preferredWeaponTypesInArea.Count);
				if (preferredWeaponTypesInArea.Count > 0)
				{
					eventTypeRefId = preferredWeaponTypesInArea[index];
					break;
				}
				clearCurrentEvent();
				return false;
			}
			}
			return true;
		}
		return false;
	}

	public void clearCurrentEvent()
	{
		currentEvent = new AreaEvent();
		startTime = 0L;
		eventHeroRefId = string.Empty;
		eventTypeRefId = string.Empty;
	}

	public void checkEventExpiry(long aCurrentTime)
	{
		if (currentEvent.getAreaEventRefId() != string.Empty)
		{
		}
		if (currentEvent.getAreaEventRefId() != string.Empty && startTime + currentEvent.getDurationInHalfHours() < aCurrentTime)
		{
			clearCurrentEvent();
		}
	}

	public long getEventTimeLeft(long aCurrentTime)
	{
		if (currentEvent.getAreaEventRefId() != string.Empty)
		{
			return startTime + currentEvent.getDurationInHalfHours() - aCurrentTime;
		}
		return 0L;
	}

	public AreaEvent getCurrentEvent()
	{
		return currentEvent;
	}

	public float getCurrentEventExpMult(string checkHeroRefId, string checkWeaponTypeRefId)
	{
		switch (currentEvent.getEffectType())
		{
		case "ALL":
			return currentEvent.getExpMult();
		case "HERO":
			if (checkHeroRefId == eventHeroRefId)
			{
				return currentEvent.getExpMult();
			}
			break;
		case "WEAPON_TYPE":
			if (checkWeaponTypeRefId == eventTypeRefId)
			{
				return currentEvent.getExpMult();
			}
			break;
		}
		return 1f;
	}

	public float getCurrentEventStarchMult(string checkHeroRefId, string checkWeaponTypeRefId)
	{
		switch (currentEvent.getEffectType())
		{
		case "ALL":
			return currentEvent.getStarchMult();
		case "HERO":
			if (checkHeroRefId == eventHeroRefId)
			{
				return currentEvent.getStarchMult();
			}
			break;
		case "WEAPON_TYPE":
			if (checkWeaponTypeRefId == eventTypeRefId)
			{
				return currentEvent.getStarchMult();
			}
			break;
		}
		return 1f;
	}

	public string getCurrentEventName()
	{
		string text = currentEvent.getAreaEventName();
		switch (currentEvent.getEffectType())
		{
		case "HERO":
		{
			Hero heroByHeroRefID = CommonAPI.getGameData().getHeroByHeroRefID(eventHeroRefId);
			text = text.Replace("[heroName]", heroByHeroRefID.getHeroName());
			break;
		}
		case "WEAPON_TYPE":
		{
			WeaponType weaponTypeByRefId = CommonAPI.getGameData().getWeaponTypeByRefId(eventTypeRefId);
			text = text.Replace("[weaponType]", weaponTypeByRefId.getWeaponTypeName());
			break;
		}
		}
		return text;
	}

	public string getCurrentEventDescription()
	{
		string text = currentEvent.getAreaEventDescription();
		switch (currentEvent.getEffectType())
		{
		case "HERO":
		{
			Hero heroByHeroRefID = CommonAPI.getGameData().getHeroByHeroRefID(eventHeroRefId);
			text = text.Replace("[heroName]", heroByHeroRefID.getHeroName());
			break;
		}
		case "WEAPON_TYPE":
		{
			WeaponType weaponTypeByRefId = CommonAPI.getGameData().getWeaponTypeByRefId(eventTypeRefId);
			text = text.Replace("[weaponType]", weaponTypeByRefId.getWeaponTypeName());
			break;
		}
		}
		return text;
	}

	public string getCurrentEventEffectString()
	{
		GameData gameData = CommonAPI.getGameData();
		float starchMult = currentEvent.getStarchMult();
		float expMult = currentEvent.getExpMult();
		List<string> list = new List<string>();
		list.Add("[areaName]");
		list.Add("[starchMult]");
		list.Add("[expMult]");
		List<string> list2 = new List<string>();
		list2.Add(getAreaName());
		list2.Add(starchMult.ToString());
		list2.Add(expMult.ToString());
		string aRefId = string.Empty;
		switch (currentEvent.getEffectType())
		{
		case "ALL":
			if (starchMult != 1f && expMult != 1f)
			{
				aRefId = "areaEventEffect03";
			}
			else if (starchMult != 1f)
			{
				aRefId = "areaEventEffect01";
			}
			else if (expMult != 1f)
			{
				aRefId = "areaEventEffect02";
			}
			break;
		case "HERO":
		{
			Hero heroByHeroRefID = gameData.getHeroByHeroRefID(getEventHeroRefId());
			list.Add("[heroName]");
			list2.Add(heroByHeroRefID.getHeroName());
			if (starchMult != 1f && expMult != 1f)
			{
				aRefId = "areaEventEffect09";
			}
			else if (starchMult != 1f)
			{
				aRefId = "areaEventEffect07";
			}
			else if (expMult != 1f)
			{
				aRefId = "areaEventEffect08";
			}
			break;
		}
		case "WEAPON_TYPE":
		{
			WeaponType weaponTypeByRefId = gameData.getWeaponTypeByRefId(getEventTypeRefId());
			list.Add("[weaponType]");
			list2.Add(weaponTypeByRefId.getWeaponTypeName());
			if (starchMult != 1f && expMult != 1f)
			{
				aRefId = "areaEventEffect06";
			}
			else if (starchMult != 1f)
			{
				aRefId = "areaEventEffect04";
			}
			else if (expMult != 1f)
			{
				aRefId = "areaEventEffect05";
			}
			break;
		}
		}
		return gameData.getTextByRefIdWithDynTextList(aRefId, list, list2);
	}

	public string getCurrentEventTooltipInfo(long aCurrentTime)
	{
		string empty = string.Empty;
		empty = empty + "[56AE59]" + areaName.ToUpper(CultureInfo.InvariantCulture) + "\n";
		empty = empty + getCurrentEventName() + "[-]\n\n";
		empty = empty + getCurrentEventEffectString() + "\n\n";
		return empty + CommonAPI.getGameData().getTextByRefIdWithDynText("projectStats07", "[timeLeft]", CommonAPI.convertHalfHoursToTimeString(getEventTimeLeft(aCurrentTime)));
	}

	public string getCurrentEventRefId()
	{
		return currentEvent.getAreaEventRefId();
	}

	public long getStartTime()
	{
		return startTime;
	}

	public string getEventHeroRefId()
	{
		return eventHeroRefId;
	}

	public string getEventTypeRefId()
	{
		return eventTypeRefId;
	}

	public void setCurrentEvent(AreaEvent aEvent)
	{
		currentEvent = aEvent;
	}

	public void setStartTime(long aStartTime)
	{
		startTime = aStartTime;
	}

	public void setEventHeroRefId(string aHero)
	{
		eventHeroRefId = aHero;
	}

	public void setEventTypeRefId(string aType)
	{
		eventTypeRefId = aType;
	}

	public bool checkIsUnlock()
	{
		return isUnlocked;
	}

	public void setUnlock(bool aUnlock)
	{
		isUnlocked = aUnlock;
	}

	public int checkTimesExplored()
	{
		return timesExplored;
	}

	public void setTimesExplored(int aExploreCount)
	{
		timesExplored = aExploreCount;
	}

	public void addTimesExplored(int aExploreCount)
	{
		timesExplored += aExploreCount;
	}

	public int checkTimesBuyItems()
	{
		return timesBuyItems;
	}

	public void setTimesBuyItems(int aBuyCount)
	{
		timesBuyItems = aBuyCount;
	}

	public void addTimesBuyItems(int aBuyCount)
	{
		timesBuyItems += aBuyCount;
	}

	public int checkTimesSell()
	{
		return timesSell;
	}

	public void setTimesSell(int aSellCount)
	{
		timesSell = aSellCount;
	}

	public void addTimesSell(int aSellCount)
	{
		timesSell += aSellCount;
	}

	public int checkTimesTrain()
	{
		return timesTrain;
	}

	public void setTimesTrain(int aTrainCount)
	{
		timesTrain = aTrainCount;
	}

	public void addTimesTrain(int aTrainCount)
	{
		timesTrain += aTrainCount;
	}

	public int checkTimesVacation()
	{
		return timesVacation;
	}

	public void setTimesVacation(int aVacationCount)
	{
		timesVacation = aVacationCount;
	}

	public void addTimesVacation(int aVacationCount)
	{
		timesVacation += aVacationCount;
	}

	public List<string> getAreaSmithRefID(string exclude = "")
	{
		if (exclude != string.Empty)
		{
			List<string> list = new List<string>();
			{
				foreach (string item in areaSmithRefID)
				{
					if (item != exclude)
					{
						list.Add(item);
					}
				}
				return list;
			}
		}
		return areaSmithRefID;
	}

	public void addAreaSmithRefID(string aRefID)
	{
		areaSmithRefID.Add(aRefID);
	}

	public void setAreaSmtihRefIDList(List<string> aRefIDList)
	{
		areaSmithRefID = aRefIDList;
	}

	public void removeAreaSmithRefID(string aRefID)
	{
		if (areaSmithRefID.Contains(aRefID))
		{
			areaSmithRefID.Remove(aRefID);
		}
	}

	public bool checkAreaSmithExceed()
	{
		return areaSmithRefID.Count >= 3;
	}
}
