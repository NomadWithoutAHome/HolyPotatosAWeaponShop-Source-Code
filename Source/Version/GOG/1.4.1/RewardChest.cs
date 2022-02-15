using System;
using System.Collections.Generic;

[Serializable]
public class RewardChest
{
	private string rewardChestRefId;

	private string chestName;

	private string chestDescription;

	private string image;

	private List<RewardChestItem> rewardChestItemList;

	public RewardChest()
	{
		rewardChestRefId = string.Empty;
		chestName = string.Empty;
		chestDescription = string.Empty;
		image = string.Empty;
		rewardChestItemList = new List<RewardChestItem>();
	}

	public RewardChest(string aChestRefId, string aChestName, string aChestDesc, string aImage)
	{
		rewardChestRefId = aChestRefId;
		chestName = aChestName;
		chestDescription = aChestDesc;
		image = aImage;
		rewardChestItemList = new List<RewardChestItem>();
	}

	public void addRewardChestItem(string aChestItemRefId, string aChestRefId, string aItemRefId, int aItemNum, int aProb)
	{
		rewardChestItemList.Add(new RewardChestItem(aChestItemRefId, aChestRefId, aItemRefId, aItemNum, aProb));
	}

	public void clearRewardChestItem()
	{
		rewardChestItemList.Clear();
	}

	public List<RewardChestItem> getRewardChestItemList()
	{
		return rewardChestItemList;
	}

	public RewardChestItem getRandomRewardFromChest()
	{
		List<int> list = new List<int>();
		foreach (RewardChestItem rewardChestItem in rewardChestItemList)
		{
			list.Add(rewardChestItem.getProbability());
		}
		int weightedRandomIndex = CommonAPI.getWeightedRandomIndex(list);
		if (weightedRandomIndex != -1 && weightedRandomIndex < rewardChestItemList.Count)
		{
			return rewardChestItemList[weightedRandomIndex];
		}
		return new RewardChestItem();
	}

	public string getRewardChestRefId()
	{
		return rewardChestRefId;
	}

	public string getChestName()
	{
		return chestName;
	}

	public string getChestDescription()
	{
		return chestDescription;
	}

	public string getImage()
	{
		return image;
	}
}
