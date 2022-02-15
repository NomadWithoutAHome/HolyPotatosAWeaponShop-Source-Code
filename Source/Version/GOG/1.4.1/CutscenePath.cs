using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CutscenePath : MonoBehaviour
{
	private string cutscenePathRefID;

	private Vector2 startingPoint;

	private Vector2 endPoint;

	private List<Vector2> pathList;

	private float yValue;

	private float xDegree;

	private float yDegree;

	private bool faceBack;

	public CutscenePath()
	{
		cutscenePathRefID = string.Empty;
		yValue = 0f;
		startingPoint = new Vector2(-1f, -1f);
		endPoint = new Vector2(-1f, -1f);
		pathList = new List<Vector2>();
		xDegree = 0f;
		yDegree = 0f;
		faceBack = false;
	}

	public CutscenePath(string aCutscenePathRefID, string aStartingPoint, string aEndPoint, string aPath, float aYValue, string aEndView)
	{
		cutscenePathRefID = aCutscenePathRefID;
		yValue = aYValue;
		string[] array = aStartingPoint.Split(',');
		if (array.Length > 1)
		{
			startingPoint = new Vector2(CommonAPI.parseInt(array[0]), CommonAPI.parseInt(array[1]));
		}
		string[] array2 = aEndPoint.Split(',');
		if (array2.Length > 1)
		{
			endPoint = new Vector2(CommonAPI.parseInt(array2[0]), CommonAPI.parseInt(array2[1]));
		}
		string[] array3 = aPath.Split(';');
		pathList = new List<Vector2>();
		if (array3.Length > 0)
		{
			string[] array4 = array3;
			foreach (string text in array4)
			{
				string[] array5 = text.Split(',');
				pathList.Add(new Vector2(CommonAPI.parseInt(array5[0]), CommonAPI.parseInt(array5[1])));
			}
		}
		switch (aEndView)
		{
		case "LEFT_FRONT":
			xDegree = 35.264f;
			yDegree = 45f;
			faceBack = false;
			break;
		case "LEFT_BACK":
			xDegree = -35.264f;
			yDegree = 225f;
			faceBack = true;
			break;
		case "RIGHT_FRONT":
			xDegree = -35.264f;
			yDegree = 225f;
			faceBack = false;
			break;
		case "RIGHT_BACK":
			xDegree = 35.264f;
			yDegree = 45f;
			faceBack = true;
			break;
		}
	}

	public string getCutscenePathRefID()
	{
		return cutscenePathRefID;
	}

	public List<Vector2> getPathList()
	{
		return pathList;
	}

	public float getXDegree()
	{
		return xDegree;
	}

	public float getYDegree()
	{
		return yDegree;
	}

	public bool getFaceBack()
	{
		return faceBack;
	}

	public Vector2 getStartingPoint()
	{
		return startingPoint;
	}

	public Vector2 getEndPoint()
	{
		return endPoint;
	}

	public float getYValue()
	{
		return yValue;
	}
}
