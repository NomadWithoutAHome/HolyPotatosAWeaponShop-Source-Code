using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Station
{
	private string refStationID;

	private List<Vector2> stationPointList;

	private List<Vector3> rotationList;

	private SmithStation smithStation;

	private int shopLevel;

	private int furnitureLevel;

	private List<string> obstacleRefID;

	private Vector2 dogStationPoint;

	private List<Smith> smithList;

	public Station()
	{
		refStationID = string.Empty;
		stationPointList = new List<Vector2>();
		rotationList = new List<Vector3>();
		smithList = new List<Smith>();
		smithStation = SmithStation.SmithStationBlank;
		shopLevel = 0;
		furnitureLevel = 0;
		obstacleRefID = new List<string>();
		dogStationPoint = new Vector2(-1f, -1f);
	}

	public Station(string aRefStationID, string aStationPoint, string aEndPoint, string aPhase, int aShopLevel, int aFurnitureLevel, string aObstacleRefID, string aDogStationCoor)
	{
		refStationID = aRefStationID;
		string[] array = aStationPoint.Split(';');
		stationPointList = new List<Vector2>();
		if (array.Length > 1)
		{
			string[] array2 = array;
			foreach (string text in array2)
			{
				string[] array3 = text.Split(',');
				stationPointList.Add(new Vector2(CommonAPI.parseInt(array3[0]), CommonAPI.parseInt(array3[1])));
			}
		}
		string[] array4 = aEndPoint.Split(';');
		rotationList = new List<Vector3>();
		if (array4.Length > 1)
		{
			string[] array5 = array4;
			for (int j = 0; j < array5.Length; j++)
			{
				switch (array5[j])
				{
				case "LEFT_FRONT":
					rotationList.Add(new Vector3(35.264f, 45f, 0f));
					break;
				case "LEFT_BACK":
					rotationList.Add(new Vector3(-35.264f, 225f, 0f));
					break;
				case "RIGHT_FRONT":
					rotationList.Add(new Vector3(-35.264f, 225f, 0f));
					break;
				case "RIGHT_BACK":
					rotationList.Add(new Vector3(35.264f, 45f, 0f));
					break;
				}
			}
		}
		smithList = new List<Smith>();
		for (int k = 0; k < array.Length; k++)
		{
			smithList.Add(null);
		}
		switch (aPhase)
		{
		case "DESIGN":
			smithStation = SmithStation.SmithStationDesign;
			break;
		case "CRAFT":
			smithStation = SmithStation.SmithStationCraft;
			break;
		case "POLISH":
			smithStation = SmithStation.SmithStationPolish;
			break;
		case "ENCHANT":
			smithStation = SmithStation.SmithStationEnchant;
			break;
		case "DOGBED":
			smithStation = SmithStation.SmithStationDogHome;
			break;
		}
		shopLevel = aShopLevel;
		furnitureLevel = aFurnitureLevel;
		string[] array6 = aObstacleRefID.Split(';');
		obstacleRefID = new List<string>();
		if (array6.Length > 0)
		{
			string[] array7 = array6;
			foreach (string item in array7)
			{
				obstacleRefID.Add(item);
			}
		}
		string[] array8 = aDogStationCoor.Split(',');
		dogStationPoint = new Vector2(CommonAPI.parseInt(array8[0]), CommonAPI.parseInt(array8[1]));
	}

	public string getRefStationID()
	{
		return refStationID;
	}

	public List<Vector2> getStationPointList()
	{
		return stationPointList;
	}

	public List<Vector3> getRotationList()
	{
		return rotationList;
	}

	public List<Smith> getSmithList()
	{
		return smithList;
	}

	public void setSmithList(List<Smith> aSmithList)
	{
		smithList = aSmithList;
	}

	public SmithStation getSmithStation()
	{
		return smithStation;
	}

	public int getShopLevel()
	{
		return shopLevel;
	}

	public int getFurnitureLevel()
	{
		return furnitureLevel;
	}

	public void assignSmiths(List<Smith> newSmithList)
	{
		if (checkEmptySmithList())
		{
			foreach (Smith newSmith in newSmithList)
			{
				assignASmith(newSmith);
			}
			return;
		}
		for (int i = 0; i < smithList.Count; i++)
		{
			if (smithList[i] != null)
			{
				checkSmithInside(i, newSmithList);
			}
		}
		foreach (Smith newSmith2 in newSmithList)
		{
			checkSmith(newSmith2);
		}
	}

	public void assignASmith(Smith aSmith)
	{
		bool flag = false;
		smithList[aSmith.getCurrentStationIndex()] = aSmith;
	}

	public void unassignASmith(Smith aSmith)
	{
		for (int i = 0; i < smithList.Count; i++)
		{
			if (smithList[i] != null && smithList[i].getSmithRefId() == aSmith.getSmithRefId())
			{
				smithList[i] = null;
			}
		}
	}

	public void swapASmith(Smith smithToBeSearched, Smith smithToReplace)
	{
		for (int i = 0; i < smithList.Count; i++)
		{
			if (smithList[i] != null && smithList[i].getSmithRefId() == smithToBeSearched.getSmithRefId())
			{
				smithList[i] = smithToReplace;
			}
		}
	}

	public bool checkEmptyStation(int index)
	{
		if (smithList[index] != null)
		{
			return false;
		}
		return true;
	}

	public bool checkEmptySmithList()
	{
		foreach (Smith smith in smithList)
		{
			if (smith != null)
			{
				return false;
			}
		}
		return true;
	}

	public void checkSmithInside(int smithIndex, List<Smith> newSmithList)
	{
		bool flag = false;
		foreach (Smith newSmith in newSmithList)
		{
			if (smithList[smithIndex] != null && smithList[smithIndex].getSmithRefId() == newSmith.getSmithRefId())
			{
				flag = true;
			}
		}
		if (!flag)
		{
			smithList[smithIndex] = null;
		}
	}

	public void checkSmith(Smith aSmith)
	{
		bool flag = false;
		foreach (Smith smith in smithList)
		{
			if (smith != null && smith.getSmithRefId() == aSmith.getSmithRefId())
			{
				flag = true;
			}
		}
		if (!flag)
		{
			assignASmith(aSmith);
		}
	}

	public Vector2 getSmithStationPoint(Smith aSmith)
	{
		int num = -1;
		for (int i = 0; i < smithList.Count; i++)
		{
			if (smithList[i] != null && aSmith.getSmithRefId() == smithList[i].getSmithRefId())
			{
				num = i;
			}
		}
		if (num != -1)
		{
			return stationPointList[num];
		}
		return new Vector2(-1f, -1f);
	}

	public int getSmithStationPointIndex(Smith aSmith)
	{
		int result = -1;
		for (int i = 0; i < smithList.Count; i++)
		{
			if (smithList[i] != null && aSmith.getSmithRefId() == smithList[i].getSmithRefId())
			{
				result = i;
			}
		}
		return result;
	}

	public void clearSmithList()
	{
		smithList = new List<Smith>();
		for (int i = 0; i < stationPointList.Count; i++)
		{
			smithList.Add(null);
		}
	}

	public Vector2 getSmithStationByIndex(int aIndex)
	{
		return stationPointList[aIndex];
	}

	public Smith getSmithByStationIndex(int aIndex)
	{
		return smithList[aIndex];
	}

	public List<string> getObstacleRefID()
	{
		return obstacleRefID;
	}

	public Vector2 getDogStationPoint()
	{
		return dogStationPoint;
	}
}
