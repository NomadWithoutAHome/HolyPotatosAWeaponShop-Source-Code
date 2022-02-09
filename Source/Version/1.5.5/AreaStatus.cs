using System;

[Serializable]
public class AreaStatus
{
	private string areaStatusRefID;

	private string areaRefID;

	private string smithEffectRefID;

	private Season season;

	private int probability;

	public AreaStatus()
	{
		areaStatusRefID = string.Empty;
		areaRefID = string.Empty;
		smithEffectRefID = string.Empty;
		season = Season.SeasonBlank;
		probability = 0;
	}

	public AreaStatus(string aAreaStatusRefID, string aAreaRefID, string aSmithEffectRefID, string aSeason, int aProbability)
	{
		areaStatusRefID = aAreaStatusRefID;
		areaRefID = aAreaRefID;
		smithEffectRefID = aSmithEffectRefID;
		switch (aSeason)
		{
		case "SUMMER":
			season = Season.SeasonSummer;
			break;
		case "AUTUMN":
			season = Season.SeasonAutumn;
			break;
		case "SPRING":
			season = Season.SeasonSpring;
			break;
		case "WINTER":
			season = Season.SeasonWinter;
			break;
		}
		probability = aProbability;
	}

	public string getAreaStatusRefID()
	{
		return areaStatusRefID;
	}

	public string getAreaRefID()
	{
		return areaRefID;
	}

	public string getSmithEffectRefID()
	{
		return smithEffectRefID;
	}

	public Season getSeason()
	{
		return season;
	}

	public int getProbability()
	{
		return probability;
	}
}
