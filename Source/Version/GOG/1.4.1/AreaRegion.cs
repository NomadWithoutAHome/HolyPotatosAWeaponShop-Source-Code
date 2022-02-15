using System;

[Serializable]
public class AreaRegion
{
	private int areaRegionRefID;

	private string regionName;

	private string regionDesc;

	private string scenarioLock;

	private int fameRequired;

	private int workstationLvl;

	private int regionTicketInterval;

	private int maxEventCount;

	private int grantAmount;

	private float cameraCentreX;

	private float cameraCentreY;

	private float zoomDefault;

	private float xPosLimitUpper;

	private float xPosLimitLower;

	private float yPosLimitUpper;

	private float yPosLimitLower;

	private float zoomLimitUpper;

	private float zoomLimitLower;

	private float targetZoom;

	private string bgImg;

	public AreaRegion()
	{
		areaRegionRefID = 0;
		regionName = string.Empty;
		regionDesc = string.Empty;
		scenarioLock = string.Empty;
		fameRequired = 0;
		workstationLvl = 0;
		regionTicketInterval = 0;
		maxEventCount = 0;
		grantAmount = 0;
		cameraCentreX = 0f;
		cameraCentreY = 0f;
		zoomDefault = 0f;
		xPosLimitUpper = 0f;
		xPosLimitLower = 0f;
		yPosLimitUpper = 0f;
		yPosLimitLower = 0f;
		zoomLimitUpper = 0f;
		zoomLimitLower = 0f;
		targetZoom = 0f;
		bgImg = string.Empty;
	}

	public AreaRegion(int aRefID, string aName, string aDesc, string aScenarioLock, int aFameRequired, int aWorkstationLvl, int aInterval, int aEventMax, int aGrantAmount, string aCameraCentre, float aZoomDefault, float aXPosLimitUpper, float aXPosLimitLower, float aYPosLimitUpper, float aYPosLimitLower, float aZoomLimitUpper, float aZoomLimitLower, float aTargetZoom, string aBgImg)
	{
		areaRegionRefID = aRefID;
		regionName = aName;
		regionDesc = aDesc;
		scenarioLock = aScenarioLock;
		fameRequired = aFameRequired;
		workstationLvl = aWorkstationLvl;
		regionTicketInterval = aInterval;
		maxEventCount = aEventMax;
		grantAmount = aGrantAmount;
		string[] array = aCameraCentre.Split(',');
		if (array.Length > 1)
		{
			cameraCentreX = CommonAPI.parseFloat(array[0]);
			cameraCentreY = CommonAPI.parseFloat(array[1]);
		}
		zoomDefault = aZoomDefault;
		xPosLimitUpper = aXPosLimitUpper;
		xPosLimitLower = aXPosLimitLower;
		yPosLimitUpper = aYPosLimitUpper;
		yPosLimitLower = aYPosLimitLower;
		zoomLimitUpper = aZoomLimitUpper;
		zoomLimitLower = aZoomLimitLower;
		targetZoom = aTargetZoom;
		bgImg = aBgImg;
	}

	public int getAreaRegionRefID()
	{
		return areaRegionRefID;
	}

	public string getRegionName()
	{
		return regionName;
	}

	public string getRegionDesc()
	{
		return regionDesc;
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

	public int getFameRequired()
	{
		return fameRequired;
	}

	public int getWorkstationLvl()
	{
		return workstationLvl;
	}

	public int getTicketInterval()
	{
		return regionTicketInterval;
	}

	public int getMaxEventCount()
	{
		return maxEventCount;
	}

	public int getGrantAmount()
	{
		return grantAmount;
	}

	public float getCameraCentreX()
	{
		return cameraCentreX;
	}

	public float getCameraCentreY()
	{
		return cameraCentreY;
	}

	public float getZoomDefault()
	{
		return zoomDefault;
	}

	public float getXPosLimitUpper()
	{
		return xPosLimitUpper;
	}

	public float getXPosLimitLower()
	{
		return xPosLimitLower;
	}

	public float getYPosLimitUpper()
	{
		return yPosLimitUpper;
	}

	public float getYPosLimitLower()
	{
		return yPosLimitLower;
	}

	public float getZoomLimitUpper()
	{
		return zoomLimitUpper;
	}

	public float getZoomLimitLower()
	{
		return zoomLimitLower;
	}

	public float getTargetZoom()
	{
		return targetZoom;
	}

	public string getBgImg()
	{
		return bgImg;
	}
}
