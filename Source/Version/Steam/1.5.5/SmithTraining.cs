using System;

[Serializable]
public class SmithTraining
{
	private string smithTrainingRefId;

	private string smithTrainingName;

	private string smithTrainingDescription;

	private int smithTrainingExp;

	private int smithTrainingCost;

	private int smithTrainingStamina;

	private int unlockTime;

	private int unlockShopLevel;

	private int useCount;

	public SmithTraining()
	{
		smithTrainingRefId = string.Empty;
		smithTrainingName = string.Empty;
		smithTrainingDescription = string.Empty;
		smithTrainingExp = 0;
		smithTrainingCost = 0;
		smithTrainingStamina = 0;
		unlockTime = 0;
		unlockShopLevel = 0;
		useCount = 0;
	}

	public SmithTraining(string aRefId, string aName, string aDesc, int aExp, int aCost, int aStamina, int aTime, int aShopLevel)
	{
		smithTrainingRefId = aRefId;
		smithTrainingName = aName;
		smithTrainingDescription = aDesc;
		smithTrainingExp = aExp;
		smithTrainingCost = aCost;
		smithTrainingStamina = aStamina;
		unlockTime = aTime;
		unlockShopLevel = aShopLevel;
		useCount = 0;
	}

	public string getSmithTrainingRefId()
	{
		return smithTrainingRefId;
	}

	public string getSmithTrainingName()
	{
		return smithTrainingName;
	}

	public string getSmithTrainingDesc()
	{
		return smithTrainingDescription;
	}

	public int getSmithTrainingExp()
	{
		return smithTrainingExp;
	}

	public int getSmithTrainingCost()
	{
		return smithTrainingCost;
	}

	public int getSmithTrainingStamina()
	{
		return smithTrainingStamina;
	}

	public bool checkUnlock(int playerDays, int shopLevel)
	{
		if (playerDays < unlockTime || shopLevel < unlockShopLevel)
		{
			return false;
		}
		return true;
	}

	public int getUseCount()
	{
		return useCount;
	}

	public void setUseCount(int aCount)
	{
		useCount = aCount;
	}

	public void addUseCount(int aCount)
	{
		useCount += aCount;
	}
}
