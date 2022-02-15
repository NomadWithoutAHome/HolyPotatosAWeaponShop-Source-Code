using System;

[Serializable]
public class WeekendActivity
{
	private string weekendActivityRefId;

	private string activityName;

	private string activityResultText;

	private WeekendActivityType activityType;

	private int limit;

	private int chance;

	private int reqShopLevel;

	private Season reqSeason;

	private int reqPlayerMonths;

	private string reqFurniture;

	private int reqFurnitureLevel;

	private int reqAlignLaw;

	private int reqAlignChaos;

	private string rewardType;

	private string rewardRefId;

	private int rewardMagnitude;

	private int rewardQty;

	private int dogLove;

	private int doneCount;

	public WeekendActivity()
	{
		weekendActivityRefId = string.Empty;
		activityName = string.Empty;
		activityResultText = string.Empty;
		activityType = WeekendActivityType.WeekendActivityTypeBlank;
		limit = 0;
		chance = 0;
		reqShopLevel = 0;
		reqSeason = Season.SeasonBlank;
		reqPlayerMonths = 0;
		reqFurniture = string.Empty;
		reqAlignLaw = 0;
		reqAlignChaos = 0;
		rewardType = string.Empty;
		rewardRefId = string.Empty;
		rewardMagnitude = 0;
		rewardQty = 0;
		dogLove = 0;
		doneCount = 0;
	}

	public WeekendActivity(string aRefId, string aName, string aText, WeekendActivityType aType, int aLimit, int aChance, int aReqShopLevel, Season aReqSeason, int aReqMonths, string aReqFurniture, int aReqAlignLaw, int aReqAlignChaos, string aRewardType, string aRewardRefId, int aRewardMagnitude, int aRewardQty, int aDogLove)
	{
		weekendActivityRefId = aRefId;
		activityName = aName;
		activityResultText = aText;
		activityType = aType;
		limit = aLimit;
		chance = aChance;
		reqShopLevel = aReqShopLevel;
		reqSeason = aReqSeason;
		reqPlayerMonths = aReqMonths;
		reqFurniture = aReqFurniture;
		reqAlignLaw = aReqAlignLaw;
		reqAlignChaos = aReqAlignChaos;
		rewardType = aRewardType;
		rewardRefId = aRewardRefId;
		rewardMagnitude = aRewardMagnitude;
		rewardQty = aRewardQty;
		dogLove = aDogLove;
		doneCount = 0;
	}

	public string getWeekendActivityRefId()
	{
		return weekendActivityRefId;
	}

	public string getWeekendActivityName()
	{
		return activityName;
	}

	public string getWeekendActivityResultText()
	{
		return activityResultText;
	}

	public int getChance()
	{
		return chance;
	}

	public WeekendActivityType getWeekendActivityType()
	{
		return activityType;
	}

	public string getRequiredFurnitureRefId()
	{
		return reqFurniture;
	}

	public int getRequiredFurnitureLevel()
	{
		return reqFurnitureLevel;
	}

	public string getRewardType()
	{
		return rewardType;
	}

	public string getRewardRefId()
	{
		return rewardRefId;
	}

	public int getRewardMagnitude()
	{
		return rewardMagnitude;
	}

	public int getRewardQty()
	{
		return rewardQty;
	}

	public int getDogLove()
	{
		return dogLove;
	}

	public void addDoneCount(int count)
	{
		doneCount += count;
	}

	public void setDoneCount(int count)
	{
		doneCount = count;
	}

	public int getDoneCount()
	{
		return doneCount;
	}

	public bool checkReq(bool aHasDog, int aShopLevel, Season aSeason, int aMonths, int aAlignLaw, int aAlignChaos)
	{
		if (limit > 0 && doneCount >= limit)
		{
			return false;
		}
		if (activityType == WeekendActivityType.WeekendActivityTypeDog && !aHasDog)
		{
			return false;
		}
		if (aShopLevel < reqShopLevel)
		{
			return false;
		}
		if (reqSeason != Season.SeasonBlank && aSeason != reqSeason)
		{
			return false;
		}
		if (aMonths < reqPlayerMonths)
		{
			return false;
		}
		if (aAlignLaw < reqAlignLaw)
		{
			return false;
		}
		if (aAlignChaos < reqAlignChaos)
		{
			return false;
		}
		return true;
	}
}
