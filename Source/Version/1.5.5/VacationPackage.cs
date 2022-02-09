using System;
using System.Collections.Generic;

[Serializable]
public class VacationPackage
{
	private string vacationPackageRefID;

	private string summer;

	private string autumn;

	private string spring;

	private string winter;

	public VacationPackage()
	{
		vacationPackageRefID = string.Empty;
		summer = string.Empty;
		autumn = string.Empty;
		spring = string.Empty;
		winter = string.Empty;
	}

	public VacationPackage(string aRefID, string aSummer, string aAutumn, string aSpring, string aWinter)
	{
		vacationPackageRefID = aRefID;
		summer = aSummer;
		autumn = aAutumn;
		spring = aSpring;
		winter = aWinter;
	}

	public string getVacationPackageRefID()
	{
		return vacationPackageRefID;
	}

	public string getSummerRefID()
	{
		return summer;
	}

	public string getAutumnRefID()
	{
		return autumn;
	}

	public string getSpringRefID()
	{
		return spring;
	}

	public string getWinterRefID()
	{
		return winter;
	}

	public List<string> getAllRefID()
	{
		List<string> list = new List<string>();
		list.Add(summer);
		list.Add(autumn);
		list.Add(spring);
		list.Add(winter);
		return list;
	}

	public Season checkSeason(string aRefID)
	{
		if (aRefID == summer)
		{
			return Season.SeasonSummer;
		}
		if (aRefID == autumn)
		{
			return Season.SeasonAutumn;
		}
		if (aRefID == spring)
		{
			return Season.SeasonSpring;
		}
		if (aRefID == winter)
		{
			return Season.SeasonWinter;
		}
		return Season.SeasonBlank;
	}

	public string getVacationRefIDBySeason(Season aSeason)
	{
		return aSeason switch
		{
			Season.SeasonSummer => summer, 
			Season.SeasonAutumn => autumn, 
			Season.SeasonSpring => spring, 
			Season.SeasonWinter => winter, 
			_ => string.Empty, 
		};
	}
}
