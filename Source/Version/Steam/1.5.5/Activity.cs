using System;

[Serializable]
public class Activity
{
	private string activityID;

	private ActivityType activityType;

	private ActivityState activityState;

	private string areaRefID;

	private string smithRefID;

	private int travelTime;

	private int progress;

	public Activity()
	{
		activityID = string.Empty;
		activityState = ActivityState.ActivityStateBlank;
		activityType = ActivityType.ActivityTypeBlank;
		areaRefID = string.Empty;
		smithRefID = string.Empty;
		travelTime = 0;
		progress = 0;
	}

	public Activity(string aActivityID, ActivityType aActType, string aAreaRefID, string aSmithRefID, int aTravelTime)
	{
		activityID = aActivityID;
		activityState = ActivityState.ActivityStateGoing;
		activityType = aActType;
		areaRefID = aAreaRefID;
		smithRefID = aSmithRefID;
		travelTime = aTravelTime;
		progress = 0;
	}

	public string getActivityID()
	{
		return activityID;
	}

	public ActivityState getActivityState()
	{
		return activityState;
	}

	public ActivityType getActivityType()
	{
		return activityType;
	}

	public string getAreaRefID()
	{
		return areaRefID;
	}

	public string getSmithRefID()
	{
		return smithRefID;
	}

	public int getTravelTime()
	{
		return travelTime;
	}

	public int getProgress()
	{
		return progress;
	}

	public void setActivityState(ActivityState aState)
	{
		activityState = aState;
	}

	public void addProgress(int units)
	{
		progress += units;
		if (progress > travelTime)
		{
			progress = travelTime;
		}
	}

	public void setProgress(int aProgress)
	{
		progress = aProgress;
	}
}
