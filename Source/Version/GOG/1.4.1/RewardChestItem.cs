using System;

[Serializable]
public class RewardChestItem
{
	private string rewardChestItemRefId;

	private string rewardChestRefId;

	private string itemRefId;

	private int itemNum;

	private int probability;

	public RewardChestItem()
	{
		rewardChestItemRefId = string.Empty;
		rewardChestRefId = string.Empty;
		itemRefId = string.Empty;
		itemNum = 0;
		probability = 0;
	}

	public RewardChestItem(string aChestItemRefId, string aChestRefId, string aItemRefId, int aItemNum, int aProbability)
	{
		rewardChestItemRefId = aChestItemRefId;
		rewardChestRefId = aChestRefId;
		itemRefId = aItemRefId;
		itemNum = aItemNum;
		probability = aProbability;
	}

	public string getRewardChestItemRefId()
	{
		return rewardChestItemRefId;
	}

	public string getRewardChestRefId()
	{
		return rewardChestRefId;
	}

	public string getItemRefId()
	{
		return itemRefId;
	}

	public int getItemNum()
	{
		return itemNum;
	}

	public int getProbability()
	{
		return probability;
	}
}
