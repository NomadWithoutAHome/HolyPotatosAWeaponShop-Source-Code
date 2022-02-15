using System;

[Serializable]
public class MaxLoyaltyReward
{
	private string maxLoyaltyRewardRefId;

	private int heroTier;

	private int heroCount;

	private bool isSpecial;

	private string rewardType;

	private string rewardRefId;

	private int rewardNum;

	private bool isGiven;

	public MaxLoyaltyReward()
	{
		maxLoyaltyRewardRefId = string.Empty;
		heroTier = 0;
		heroCount = 0;
		isSpecial = false;
		rewardType = string.Empty;
		rewardRefId = string.Empty;
		rewardNum = 0;
		isGiven = false;
	}

	public MaxLoyaltyReward(string aRefId, int aTier, int aCount, bool aSpecial, string aRewardType, string aRewardRefId, int aRewardNum)
	{
		maxLoyaltyRewardRefId = aRefId;
		heroTier = aTier;
		heroCount = aCount;
		isSpecial = aSpecial;
		rewardType = aRewardType;
		rewardRefId = aRewardRefId;
		rewardNum = aRewardNum;
		isGiven = false;
	}

	public string getMaxLoyaltyRewardRefId()
	{
		return maxLoyaltyRewardRefId;
	}

	public int getHeroTier()
	{
		return heroTier;
	}

	public int getHeroCount()
	{
		return heroCount;
	}

	public bool getIsSpecial()
	{
		return isSpecial;
	}

	public string getRewardType()
	{
		return rewardType;
	}

	public string getRewardRefId()
	{
		return rewardRefId;
	}

	public int getRewardNum()
	{
		return rewardNum;
	}

	public bool checkIsGiven()
	{
		return isGiven;
	}

	public void setIsGiven(bool aGiven)
	{
		isGiven = aGiven;
	}
}
