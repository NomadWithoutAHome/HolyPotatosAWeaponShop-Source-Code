using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AreaPath
{
	private string areaPathRefID;

	private string startAreaRefID;

	private string endAreaRefID;

	private List<Vector2> pathList;

	public AreaPath()
	{
		areaPathRefID = string.Empty;
		startAreaRefID = string.Empty;
		endAreaRefID = string.Empty;
		pathList = new List<Vector2>();
	}

	public AreaPath(string aAreaPathRefID, string aStartAreaRefID, string aEndAreaRefID, string aPath)
	{
		areaPathRefID = aAreaPathRefID;
		startAreaRefID = aStartAreaRefID;
		endAreaRefID = aEndAreaRefID;
		string[] array = aPath.Split(';');
		pathList = new List<Vector2>();
		if (array.Length > 0)
		{
			string[] array2 = array;
			foreach (string text in array2)
			{
				string[] array3 = text.Split(',');
				pathList.Add(new Vector2(CommonAPI.parseInt(array3[0]), CommonAPI.parseInt(array3[1])));
			}
		}
	}

	public string getAreaPathRefID()
	{
		return areaPathRefID;
	}

	public List<Vector2> getPathList()
	{
		return pathList;
	}

	public string getStartAreaRefID()
	{
		return startAreaRefID;
	}

	public string getEndAreaRefID()
	{
		return endAreaRefID;
	}
}
