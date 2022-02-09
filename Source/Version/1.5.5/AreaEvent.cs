using System;

[Serializable]
public class AreaEvent
{
	private string areaEventRefId;

	private string areaEventName;

	private string areaEventDescription;

	private int areaStartRegion;

	private int areaEndRegion;

	private string effectType;

	private float starchMult;

	private float expMult;

	private int duration;

	private int probability;

	public AreaEvent()
	{
		areaEventRefId = string.Empty;
		areaEventName = string.Empty;
		areaEventDescription = string.Empty;
		areaStartRegion = 0;
		areaEndRegion = 0;
		effectType = string.Empty;
		starchMult = 0f;
		expMult = 0f;
		duration = 0;
		probability = 0;
	}

	public AreaEvent(string aRefId, string aName, string aDescription, int aStartRegion, int aEndRegion, string aEffectType, float aStarchMult, float aExpMult, int aDuration, int aProbability)
	{
		areaEventRefId = aRefId;
		areaEventName = aName;
		areaEventDescription = aDescription;
		areaStartRegion = aStartRegion;
		areaEndRegion = aEndRegion;
		effectType = aEffectType;
		starchMult = aStarchMult;
		expMult = aExpMult;
		duration = aDuration;
		probability = aProbability;
	}

	public string getAreaEventRefId()
	{
		return areaEventRefId;
	}

	public string getAreaEventName()
	{
		return areaEventName;
	}

	public string getAreaEventDescription()
	{
		return areaEventDescription;
	}

	public int getAreaStartRegion()
	{
		return areaStartRegion;
	}

	public int getAreaEndRegion()
	{
		return areaEndRegion;
	}

	public string getEffectType()
	{
		return effectType;
	}

	public float getStarchMult()
	{
		return starchMult;
	}

	public float getExpMult()
	{
		return expMult;
	}

	public int getDurationInHalfHours()
	{
		return duration * 48;
	}

	public int getDuration()
	{
		return duration;
	}

	public int getProbability()
	{
		return probability;
	}

	public bool checkRegion(int aCheckRegion)
	{
		if (aCheckRegion < areaEndRegion && aCheckRegion >= areaStartRegion)
		{
			return true;
		}
		return false;
	}
}
