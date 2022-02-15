using System;

[Serializable]
public class Objective
{
	private string objectiveRefId;

	private string objectiveName;

	private string objectiveDesc;

	private int timeLimit;

	private string startDialogueRefId;

	private string successDialogueRefId;

	private string successNextObjective;

	private string failDialogueRefId;

	private string failNextObjective;

	private UnlockCondition successCondition;

	private int reqCount;

	private string checkString;

	private int checkNum;

	private bool countFromObjectiveStart;

	private bool countAsObjective;

	private string objectiveSet;

	private long startTime;

	private int initCount;

	private bool isStarted;

	private bool isEnded;

	private bool isSuccess;

	public Objective()
	{
		objectiveRefId = string.Empty;
		objectiveName = string.Empty;
		objectiveDesc = string.Empty;
		timeLimit = 0;
		startDialogueRefId = string.Empty;
		successDialogueRefId = string.Empty;
		successNextObjective = string.Empty;
		failDialogueRefId = string.Empty;
		failNextObjective = string.Empty;
		successCondition = UnlockCondition.UnlockConditionNone;
		reqCount = 0;
		checkString = string.Empty;
		checkNum = 0;
		countFromObjectiveStart = false;
		countAsObjective = false;
		objectiveSet = string.Empty;
		resetDynData();
	}

	public Objective(string aRefId, string aName, string aDesc, int aLimit, string aStartDialogue, string aSuccessDialogue, string aSuccessNextRefId, string aFailDialogue, string aFailNextRefId, UnlockCondition aCondition, int aReqCount, string aCheckString, int aCheckNum, bool aFromObjectiveStart, bool aDoCount, string aObjectiveSet)
	{
		objectiveRefId = aRefId;
		objectiveName = aName;
		objectiveDesc = aDesc;
		timeLimit = aLimit;
		startDialogueRefId = aStartDialogue;
		successDialogueRefId = aSuccessDialogue;
		successNextObjective = aSuccessNextRefId;
		failDialogueRefId = aFailDialogue;
		failNextObjective = aFailNextRefId;
		successCondition = aCondition;
		reqCount = aReqCount;
		checkString = aCheckString;
		checkNum = aCheckNum;
		countFromObjectiveStart = aFromObjectiveStart;
		countAsObjective = aDoCount;
		objectiveSet = aObjectiveSet;
		resetDynData();
	}

	public void resetDynData()
	{
		startTime = 0L;
		initCount = 0;
		isStarted = false;
		isEnded = false;
		isSuccess = false;
	}

	public string getObjectiveRefId()
	{
		return objectiveRefId;
	}

	public string getObjectiveName()
	{
		return objectiveName;
	}

	public string getObjectiveDesc()
	{
		return objectiveDesc;
	}

	public int getTimeLimit()
	{
		return timeLimit;
	}

	public string getTimeLimitString()
	{
		return CommonAPI.convertHalfHoursToTimeString(timeLimit);
	}

	public string getStartDialogueRefId()
	{
		return startDialogueRefId;
	}

	public string getSuccessDialogueRefId()
	{
		return successDialogueRefId;
	}

	public string getSuccessNextObjective()
	{
		return successNextObjective;
	}

	public string getFailDialogueRefId()
	{
		return failDialogueRefId;
	}

	public string getFailNextObjective()
	{
		return failNextObjective;
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

	public bool checkCountFromObjectiveStart()
	{
		return countFromObjectiveStart;
	}

	public bool checkCountAsObjective()
	{
		return countAsObjective;
	}

	public string getObjectiveSet()
	{
		return objectiveSet;
	}

	public long getStartTime()
	{
		return startTime;
	}

	public void setStartTime(long aStart)
	{
		startTime = aStart;
	}

	public int getTimeLeft(long currentTime)
	{
		int num = (int)(startTime + timeLimit - currentTime);
		if (num < 0)
		{
			num = 0;
		}
		return num;
	}

	public int getInitCount()
	{
		return initCount;
	}

	public void setInitCount(int aCount)
	{
		initCount = aCount;
	}

	public float getProgressPercent(int currentCount)
	{
		return (float)(currentCount - initCount) / (float)reqCount;
	}

	public bool checkObjectiveStarted()
	{
		return isStarted;
	}

	public void setObjectiveStarted(bool aStarted)
	{
		isStarted = aStarted;
	}

	public bool checkObjectiveEnded()
	{
		return isEnded;
	}

	public void setObjectiveEnded(bool aEnded)
	{
		isEnded = aEnded;
	}

	public bool checkObjectiveSuccess()
	{
		return isSuccess;
	}

	public void setObjectiveSuccess(bool aSuccess)
	{
		isSuccess = aSuccess;
	}
}
