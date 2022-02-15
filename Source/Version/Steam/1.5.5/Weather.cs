using System;

[Serializable]
public class Weather
{
	private string weatherRefId;

	private string weatherName;

	private string weatherText;

	private bool showWhetsapp;

	private Season season;

	private int weatherChance;

	private string scenarioLock;

	private int monthReq;

	private string image;

	public Weather()
	{
		weatherRefId = string.Empty;
		weatherName = string.Empty;
		weatherText = string.Empty;
		showWhetsapp = false;
		season = Season.SeasonBlank;
		weatherChance = 0;
		scenarioLock = string.Empty;
		monthReq = 0;
		image = string.Empty;
	}

	public Weather(string aRefId, string aName, string aText, bool aShow, Season aSeason, int aChance, string aLock, int aMonth, string aImage)
	{
		weatherRefId = aRefId;
		weatherName = aName;
		weatherText = aText;
		showWhetsapp = aShow;
		season = aSeason;
		weatherChance = aChance;
		scenarioLock = aLock;
		monthReq = aMonth;
		image = aImage;
	}

	public string getWeatherRefId()
	{
		return weatherRefId;
	}

	public string getWeatherName()
	{
		return weatherName;
	}

	public string getWeatherText()
	{
		return weatherText;
	}

	public bool getShowWhetsapp()
	{
		return showWhetsapp;
	}

	public int getWeatherChance()
	{
		return weatherChance;
	}

	public string getScenarioLock()
	{
		return scenarioLock;
	}

	public bool checkConditions(Season aSeason, int aMonth, string aScenario)
	{
		if (season == aSeason && monthReq <= aMonth && checkScenarioAllow(aScenario))
		{
			return true;
		}
		return false;
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

	public string getImage()
	{
		return image;
	}
}
