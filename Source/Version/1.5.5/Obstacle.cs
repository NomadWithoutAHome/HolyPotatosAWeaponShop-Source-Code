using System;
using UnityEngine;

[Serializable]
public class Obstacle
{
	private string refObstaclesID;

	private Vector2 obstaclePoint;

	private float yValue;

	private int width;

	private int height;

	private string endView;

	private float xDegree;

	private float yDegree;

	private int sortOrder;

	private string imageLocked;

	private string imageUnlocked;

	private int shopLevel;

	private int furnitureLevel;

	private string furnitureRefID;

	public Obstacle()
	{
		refObstaclesID = string.Empty;
		yValue = 0f;
		obstaclePoint = new Vector2(-1f, -1f);
		width = 0;
		height = 0;
		xDegree = 0f;
		yDegree = 0f;
		sortOrder = 0;
		imageLocked = string.Empty;
		imageUnlocked = string.Empty;
		shopLevel = 0;
		furnitureLevel = 0;
		furnitureRefID = string.Empty;
	}

	public Obstacle(string aRefObstaclesID, string aObstaclePoint, float aYValue, int aWidth, int aHeight, string aEndView, int aSortOrder, string aImageLocked, string aImageUnlocked, int aShopLevel, int aFurnitureLevel, string aFurnitureRefID)
	{
		refObstaclesID = aRefObstaclesID;
		yValue = aYValue;
		string[] array = aObstaclePoint.Split(',');
		obstaclePoint = new Vector2(CommonAPI.parseInt(array[0]), CommonAPI.parseInt(array[1]));
		width = aWidth;
		height = aHeight;
		if (aEndView == null || !(aEndView == "FLIP"))
		{
			xDegree = 35.264f;
			yDegree = 45f;
		}
		else
		{
			xDegree = -35.264f;
			yDegree = 225f;
		}
		sortOrder = aSortOrder;
		imageLocked = aImageLocked;
		imageUnlocked = aImageUnlocked;
		shopLevel = aShopLevel;
		furnitureLevel = aFurnitureLevel;
		furnitureRefID = aFurnitureRefID;
	}

	public string getRefObstacleID()
	{
		return refObstaclesID;
	}

	public Vector2 getObstaclePoint()
	{
		return obstaclePoint;
	}

	public float getYValue()
	{
		return yValue;
	}

	public int getWidth()
	{
		return width;
	}

	public int getHeight()
	{
		return height;
	}

	public float getXDegree()
	{
		return xDegree;
	}

	public float getYDegree()
	{
		return yDegree;
	}

	public int getSortOrder()
	{
		return sortOrder;
	}

	public string getImageLocked()
	{
		return imageLocked;
	}

	public string getImageUnlocked()
	{
		return imageUnlocked;
	}

	public int getShopLevel()
	{
		return shopLevel;
	}

	public int getFurnitureLevel()
	{
		return furnitureLevel;
	}

	public string getFurnitureRefID()
	{
		return furnitureRefID;
	}
}
