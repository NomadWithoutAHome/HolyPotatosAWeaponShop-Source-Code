using System;

[Serializable]
public class Vacation
{
	private string vacationRefId;

	private string vacationName;

	private string vacationDescription;

	private int vacationCost;

	private int vacationDuration;

	private float vacationMoodAdd;

	private int timesVisited;

	public Vacation()
	{
		vacationRefId = string.Empty;
		vacationName = string.Empty;
		vacationDescription = string.Empty;
		vacationCost = 0;
		vacationDuration = 0;
		vacationMoodAdd = 0f;
		timesVisited = 0;
	}

	public Vacation(string aRefId, string aName, string aDesc, int aCost, int aDuration, float aMood)
	{
		vacationRefId = aRefId;
		vacationName = aName;
		vacationDescription = aDesc;
		vacationCost = aCost;
		vacationDuration = aDuration;
		vacationMoodAdd = aMood;
		timesVisited = 0;
	}

	public string getVacationRefId()
	{
		return vacationRefId;
	}

	public string getVacationName()
	{
		return vacationName;
	}

	public string getVacationDesc()
	{
		return vacationDescription;
	}

	public int getVacationCost()
	{
		return vacationCost;
	}

	public int getVacationDuration()
	{
		return vacationDuration;
	}

	public float getVacationMoodAdd()
	{
		return vacationMoodAdd;
	}

	public int getTimesVisited()
	{
		return timesVisited;
	}

	public void addTimesVisited(int aVisited)
	{
		timesVisited += aVisited;
	}

	public void setTimesVisited(int aVisited)
	{
		timesVisited = aVisited;
	}
}
