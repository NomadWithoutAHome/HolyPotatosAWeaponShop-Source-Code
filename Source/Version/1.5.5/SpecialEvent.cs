using System;
using System.Collections.Generic;

[Serializable]
public class SpecialEvent
{
	private string specialEventRefId;

	private int specialEventMaxOccurrence;

	private string scenarioLock;

	private int dateYear;

	private int dateMonth;

	private int dateWeek;

	private int dateDay;

	private SpecialEventType eventType;

	private int startAfterMonths;

	private int occurrenceCount;

	public SpecialEvent()
	{
		specialEventRefId = string.Empty;
		specialEventMaxOccurrence = 0;
		dateYear = 0;
		dateMonth = 0;
		dateWeek = 0;
		dateDay = 0;
		eventType = SpecialEventType.SpecialEventTypeBlank;
		startAfterMonths = 0;
		occurrenceCount = 0;
	}

	public SpecialEvent(string aRefId, int aMax, string aLock, int aDateYear, int aDateMonth, int aDateWeek, int aDateDay, SpecialEventType aEventType, int aStartMonths)
	{
		specialEventRefId = aRefId;
		specialEventMaxOccurrence = aMax;
		scenarioLock = aLock;
		dateYear = aDateYear;
		dateMonth = aDateMonth;
		dateWeek = aDateWeek;
		dateDay = aDateDay;
		eventType = aEventType;
		startAfterMonths = aStartMonths;
		occurrenceCount = 0;
	}

	public bool checkEvent(List<int> date, int playerMonths, string aScenario)
	{
		if (!checkScenarioAllow(aScenario) || (specialEventMaxOccurrence != -1 && occurrenceCount > specialEventMaxOccurrence))
		{
			return false;
		}
		if (startAfterMonths > playerMonths)
		{
			return false;
		}
		if (dateYear > 0 && dateYear != date[0])
		{
			return false;
		}
		if (dateMonth > 0 && dateMonth != date[1])
		{
			return false;
		}
		if (dateWeek > 0 && dateWeek != date[2])
		{
			return false;
		}
		if (dateDay > 0 && dateDay != date[3])
		{
			return false;
		}
		return true;
	}

	public string getSpecialEventRefId()
	{
		return specialEventRefId;
	}

	public SpecialEventType getEventType()
	{
		return eventType;
	}

	public int getDateYear()
	{
		return dateYear;
	}

	public int getDateMonth()
	{
		return dateMonth;
	}

	public int getDateWeek()
	{
		return dateWeek;
	}

	public int getDateDay()
	{
		return dateDay;
	}

	public void addOccurrenceCount(int aNum)
	{
		occurrenceCount += aNum;
	}

	public void setOccurrenceCount(int aNum)
	{
		occurrenceCount = aNum;
	}

	public int getOccurrenceCount()
	{
		return occurrenceCount;
	}

	public bool checkScenarioAllow(string aScenario)
	{
		if (scenarioLock.Length > 0)
		{
			string[] array = scenarioLock.Split('@');
			string[] array2 = array;
			foreach (string text in array2)
			{
				if (text == aScenario)
				{
					return false;
				}
			}
		}
		return true;
	}
}
