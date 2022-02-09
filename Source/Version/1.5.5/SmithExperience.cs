using System;

[Serializable]
public class SmithExperience
{
	private string smithExperienceRefId;

	private string smithRefId;

	private string smithJobClassRefId;

	private string maxLevelTagRefId;

	private int level;

	private bool tagGiven;

	public SmithExperience()
	{
		smithExperienceRefId = string.Empty;
		smithRefId = string.Empty;
		smithJobClassRefId = string.Empty;
		maxLevelTagRefId = string.Empty;
		level = 0;
		tagGiven = false;
	}

	public SmithExperience(string aRefId, string aSmithRefId, string aSmithJobClassRefId, string aTag, int aInitLevel)
	{
		smithExperienceRefId = aRefId;
		smithRefId = aSmithRefId;
		smithJobClassRefId = aSmithJobClassRefId;
		maxLevelTagRefId = aTag;
		level = aInitLevel;
		tagGiven = false;
	}

	public string getSmithExperienceRefId()
	{
		return smithExperienceRefId;
	}

	public string getSmithRefId()
	{
		return smithRefId;
	}

	public string getSmithJobClassRefId()
	{
		return smithJobClassRefId;
	}

	public string getMaxLevelTagRefId()
	{
		return maxLevelTagRefId;
	}

	public int getSmithJobClassLevel()
	{
		return level;
	}

	public void setSmithJobClassLevel(int aLevel)
	{
		level = aLevel;
	}

	public void addSmithJobClassLevel(int aLevel)
	{
		level += aLevel;
	}

	public bool checkTagGiven()
	{
		return tagGiven;
	}

	public void setTagGiven(bool aGiven)
	{
		tagGiven = aGiven;
	}
}
