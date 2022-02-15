using System;
using UnityEngine;

[Serializable]
public class CutsceneObstacle
{
	private string refObstaclesID;

	private Vector2 obstaclePoint;

	private float yValue;

	private int width;

	private int height;

	private string endView;

	private int sortOrder;

	private string image;

	public CutsceneObstacle()
	{
		refObstaclesID = string.Empty;
		yValue = 0f;
		obstaclePoint = new Vector2(-1f, -1f);
		width = 0;
		height = 0;
		endView = string.Empty;
		sortOrder = 0;
		image = string.Empty;
	}

	public CutsceneObstacle(string aRefObstaclesID, string aObstaclePoint, float aYValue, int aWidth, int aHeight, string aEndView, int aSortOrder, string aImage)
	{
		refObstaclesID = aRefObstaclesID;
		yValue = aYValue;
		string[] array = aObstaclePoint.Split(',');
		obstaclePoint = new Vector2(CommonAPI.parseInt(array[0]), CommonAPI.parseInt(array[1]));
		width = aWidth;
		height = aHeight;
		endView = aEndView;
		sortOrder = aSortOrder;
		image = aImage;
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

	public int getSortOrder()
	{
		return sortOrder;
	}

	public string getImage()
	{
		return image;
	}
}
