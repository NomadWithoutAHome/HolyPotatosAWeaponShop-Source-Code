using System;
using UnityEngine;

[Serializable]
public class DecorationPosition
{
	private string decorationPositionRefId;

	private string decorationRefId;

	private Vector3 decoPosition;

	private float yValue;

	private int sortOrder;

	private float xDegree;

	private float yDegree;

	private string sortLayer;

	private string decorationImage;

	private int shopLevel;

	public DecorationPosition()
	{
		decorationPositionRefId = string.Empty;
		decorationRefId = string.Empty;
		decoPosition = new Vector3(-1f, -1f);
		yValue = 0f;
		sortOrder = 0;
		xDegree = 0f;
		yDegree = 0f;
		sortLayer = string.Empty;
		decorationImage = string.Empty;
		shopLevel = 0;
	}

	public DecorationPosition(string aPosRefId, string aRefId, string aDecoPosition, float aYValue, int aSortOrder, string aFlip, string aSortLayer, string aImage, int aShopLevel)
	{
		decorationPositionRefId = aPosRefId;
		decorationRefId = aRefId;
		string[] array = aDecoPosition.Split(',');
		if (array.Length > 1)
		{
			decoPosition = new Vector3(CommonAPI.parseFloat(array[0]), aYValue, CommonAPI.parseFloat(array[1]));
		}
		yValue = aYValue;
		sortOrder = aSortOrder;
		switch (aFlip)
		{
		case "TRUE":
			xDegree = -35.264f;
			yDegree = 225f;
			break;
		case "FALSE":
			xDegree = 35.264f;
			yDegree = 45f;
			break;
		}
		sortLayer = aSortLayer;
		decorationImage = aImage;
		shopLevel = aShopLevel;
	}

	public string getDecorationPositionRefId()
	{
		return decorationPositionRefId;
	}

	public string getDecorationRefId()
	{
		return decorationRefId;
	}

	public Vector3 getDecorationPosition()
	{
		return decoPosition;
	}

	public float getYValue()
	{
		return yValue;
	}

	public int getSortOrder()
	{
		return sortOrder;
	}

	public float getXDegree()
	{
		return xDegree;
	}

	public float getYDegree()
	{
		return yDegree;
	}

	public string getSortLayer()
	{
		return sortLayer;
	}

	public string getDecorationImage()
	{
		return decorationImage;
	}

	public int getShopLevel()
	{
		return shopLevel;
	}
}
