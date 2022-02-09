using System;

[Serializable]
public class Achievement
{
	private string achievementRefID;

	private string steamID;

	private UnlockCondition successCondition;

	private int reqCount;

	private string checkString;

	private int checkNum;

	public bool achieved;

	public Achievement()
	{
		achievementRefID = string.Empty;
		steamID = string.Empty;
		successCondition = UnlockCondition.UnlockConditionNone;
		reqCount = 0;
		checkString = string.Empty;
		checkNum = 0;
		achieved = false;
	}

	public Achievement(string aAchievementRefID, string aSteamID, UnlockCondition aCondition, int aReqCount, string aCheckString, int aCheckNum)
	{
		achievementRefID = aAchievementRefID;
		steamID = aSteamID;
		successCondition = aCondition;
		reqCount = aReqCount;
		checkString = aCheckString;
		checkNum = aCheckNum;
		achieved = false;
	}

	public string getAchivementRefID()
	{
		return achievementRefID;
	}

	public string getSteamID()
	{
		return steamID;
	}

	public UnlockCondition getSuccessCondition()
	{
		return successCondition;
	}

	public int getReqCount()
	{
		return reqCount;
	}

	public string getCheckString()
	{
		return checkString;
	}

	public int getCheckNum()
	{
		return checkNum;
	}

	public bool getAchieved()
	{
		return achieved;
	}

	public void setAchieved(bool aBool)
	{
		achieved = aBool;
	}
}
