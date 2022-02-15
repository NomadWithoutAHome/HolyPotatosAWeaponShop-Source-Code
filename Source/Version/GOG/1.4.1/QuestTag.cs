using System;

[Serializable]
public class QuestTag
{
	private string questTagRefId;

	private string tagRefId;

	private string questRefId;

	private string rewardChestRefId;

	private int setNum;

	private bool isVisible;

	private bool isUnlocked;

	public QuestTag()
	{
		questTagRefId = string.Empty;
		tagRefId = string.Empty;
		questRefId = string.Empty;
		rewardChestRefId = string.Empty;
		setNum = 0;
		isVisible = false;
		isUnlocked = false;
	}

	public QuestTag(string aQuestTagRefId, string aTagRefId, string aQuestRefId, string aRewardChestRefId, int aSetNum, bool aVisible)
	{
		questTagRefId = aQuestTagRefId;
		tagRefId = aTagRefId;
		questRefId = aQuestRefId;
		rewardChestRefId = aRewardChestRefId;
		setNum = aSetNum;
		isVisible = aVisible;
		isUnlocked = false;
	}

	public string getQuestTagRefId()
	{
		return questTagRefId;
	}

	public string getTagRefId()
	{
		return tagRefId;
	}

	public string getQuestRefId()
	{
		return questRefId;
	}

	public bool checkTagVisible()
	{
		return isVisible;
	}

	public string getRewardChestRefId()
	{
		return rewardChestRefId;
	}

	public int getSetNum()
	{
		return setNum;
	}

	public void setTagUnlock(bool aUnlocked)
	{
		isUnlocked = aUnlocked;
	}

	public bool checkTagUnlocked()
	{
		return isUnlocked;
	}
}
