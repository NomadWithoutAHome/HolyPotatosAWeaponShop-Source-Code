using System;
using UnityEngine;

[Serializable]
public class ShopLevel
{
	private string shopRefId;

	private string shopName;

	private int shopCapacity;

	private int upgradeCost;

	private int monthReq;

	private float shopMoodReduceRate;

	private string nextShopRefId;

	private int width;

	private int height;

	private Vector2 startingCoor;

	private Vector2 coffeeCoor;

	private Vector2 researchCoor;

	private Vector2 portalCoor;

	private string spotIndicatorImage;

	private string floorImg;

	private string wallLeftImg;

	private string wallRightImg;

	private Vector3 floorPosition;

	private Vector3 wallLeftPosition;

	private Vector3 wallRightPosition;

	private Vector3 bgPosition;

	public ShopLevel()
	{
		shopRefId = string.Empty;
		shopName = string.Empty;
		shopCapacity = 0;
		upgradeCost = 0;
		monthReq = 0;
		shopMoodReduceRate = 0f;
		nextShopRefId = string.Empty;
		width = 0;
		height = 0;
		startingCoor = new Vector2(-1f, -1f);
		coffeeCoor = new Vector2(-1f, -1f);
		researchCoor = new Vector2(-1f, -1f);
		portalCoor = new Vector2(-1f, -1f);
		spotIndicatorImage = string.Empty;
		floorImg = string.Empty;
		wallLeftImg = string.Empty;
		wallRightImg = string.Empty;
		floorPosition = new Vector3(-1f, 0f, -1f);
		wallLeftPosition = new Vector3(-1f, 0f, -1f);
		wallRightPosition = new Vector3(-1f, 0f, -1f);
		bgPosition = new Vector3(-1f, -1f, -1f);
	}

	public ShopLevel(string aRefId, string aName, int aCapacity, int aCost, int aMonth, float aMoodReduceRate, string aNextRefId, int aWidth, int aHeight, string aStartingCoor, string aCoffeeCoor, string aResearchCoor, string aPortalCoor, string aSpotIndicatorImage, string aFloorImg, string aWallLeftImg, string aWallRightImg, string aFloorPosition, string aWallLeftPosition, string aWallRightPosition, string aBgPosition)
	{
		shopRefId = aRefId;
		shopName = aName;
		shopCapacity = aCapacity;
		upgradeCost = aCost;
		monthReq = aMonth;
		shopMoodReduceRate = aMoodReduceRate;
		nextShopRefId = aNextRefId;
		width = aWidth;
		height = aHeight;
		string[] array = aStartingCoor.Split(',');
		if (array.Length > 1)
		{
			startingCoor = new Vector2(CommonAPI.parseInt(array[0]), CommonAPI.parseInt(array[1]));
		}
		string[] array2 = aCoffeeCoor.Split(',');
		if (array2.Length > 1)
		{
			coffeeCoor = new Vector2(CommonAPI.parseInt(array2[0]), CommonAPI.parseInt(array2[1]));
		}
		string[] array3 = aResearchCoor.Split(',');
		if (array3.Length > 1)
		{
			researchCoor = new Vector2(CommonAPI.parseInt(array3[0]), CommonAPI.parseInt(array3[1]));
		}
		string[] array4 = aPortalCoor.Split(',');
		if (array4.Length > 1)
		{
			portalCoor = new Vector2(CommonAPI.parseInt(array4[0]), CommonAPI.parseInt(array4[1]));
		}
		spotIndicatorImage = aSpotIndicatorImage;
		floorImg = aFloorImg;
		wallLeftImg = aWallLeftImg;
		wallRightImg = aWallRightImg;
		string[] array5 = aFloorPosition.Split(',');
		if (array5.Length > 1)
		{
			floorPosition = new Vector3(CommonAPI.parseFloat(array5[0]), 0f, CommonAPI.parseFloat(array5[1]));
		}
		string[] array6 = aWallLeftPosition.Split(',');
		if (array6.Length > 1)
		{
			wallLeftPosition = new Vector3(CommonAPI.parseFloat(array6[0]), 0f, CommonAPI.parseFloat(array6[1]));
		}
		string[] array7 = aWallRightPosition.Split(',');
		if (array7.Length > 1)
		{
			wallRightPosition = new Vector3(CommonAPI.parseFloat(array7[0]), 0f, CommonAPI.parseFloat(array7[1]));
		}
		string[] array8 = aBgPosition.Split(',');
		if (array8.Length > 1)
		{
			bgPosition = new Vector3(CommonAPI.parseFloat(array8[0]), CommonAPI.parseFloat(array8[1]), CommonAPI.parseFloat(array8[2]));
		}
	}

	public string getShopName()
	{
		return shopName;
	}

	public string getShopRefId()
	{
		return shopRefId;
	}

	public int getShopMaxCapacity()
	{
		return shopCapacity;
	}

	public int getUpgradeCost()
	{
		return upgradeCost;
	}

	public int getMonthReq()
	{
		return monthReq;
	}

	public float getShopMoodReduceRate()
	{
		return shopMoodReduceRate;
	}

	public string getNextShopRefId()
	{
		return nextShopRefId;
	}

	public int getWidth()
	{
		return width;
	}

	public int getHeight()
	{
		return height;
	}

	public Vector2 getStartingCoor()
	{
		return startingCoor;
	}

	public Vector2 getCoffeeCoor()
	{
		return coffeeCoor;
	}

	public Vector2 getPortalCoor()
	{
		return portalCoor;
	}

	public Vector2 getResearchCoor()
	{
		return researchCoor;
	}

	public string getSpotIndicatorImage()
	{
		return spotIndicatorImage;
	}

	public string getFloorImg()
	{
		return floorImg;
	}

	public string getWallLeftImg()
	{
		return wallLeftImg;
	}

	public string getWallRightImg()
	{
		return wallRightImg;
	}

	public Vector3 getFloorPosition()
	{
		return floorPosition;
	}

	public Vector3 getWallLeftPosition()
	{
		return wallLeftPosition;
	}

	public Vector3 getWallRightPosition()
	{
		return wallRightPosition;
	}

	public Vector3 getBgPosition()
	{
		return bgPosition;
	}
}
