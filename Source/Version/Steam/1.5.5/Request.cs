using System;
using System.Collections.Generic;

[Serializable]
public class Request
{
	private string requestRefId;

	private int requestLevel;

	private string requestReq1;

	private string requestReq2;

	private int requestDuration;

	private int requestBaseGold;

	private int requestBaseLoyalty;

	private int requestBaseFame;

	private string requestRewardSet;

	private int requestRewardQty;

	public Request()
	{
		requestRefId = string.Empty;
		requestLevel = 0;
		requestReq1 = string.Empty;
		requestReq2 = string.Empty;
		requestDuration = 0;
		requestBaseGold = 0;
		requestBaseLoyalty = 0;
		requestBaseFame = 0;
		requestRewardSet = string.Empty;
		requestRewardQty = 0;
	}

	public Request(string aRefId, int aLevel, string aReq1, string aReq2, int aDuration, int aBaseGold, int aBaseLoyalty, int aBaseFame, string aRewardSet, int aQty)
	{
		requestRefId = aRefId;
		requestLevel = aLevel;
		requestReq1 = aReq1;
		requestReq2 = aReq2;
		requestDuration = aDuration;
		requestBaseGold = aBaseGold;
		requestBaseLoyalty = aBaseLoyalty;
		requestBaseFame = aBaseFame;
		requestRewardSet = aRewardSet;
		requestRewardQty = aQty;
	}

	public string getRequestRefId()
	{
		return requestRefId;
	}

	public int getRequestLevel()
	{
		return requestLevel;
	}

	public bool checkLevelRange(int aMin, int aMax)
	{
		if (requestLevel >= aMin && requestLevel <= aMax)
		{
			return true;
		}
		return false;
	}

	public string getRequestReq1()
	{
		return requestReq1;
	}

	public string getRequestReq2()
	{
		return requestReq2;
	}

	public List<string> getRequestReqList()
	{
		List<string> list = new List<string>();
		list.Add(requestReq1);
		if (requestReq2 != string.Empty)
		{
			list.Add(requestReq2);
		}
		return list;
	}

	public int getRequestDuration()
	{
		return requestDuration;
	}

	public int getRequestBaseGold()
	{
		return requestBaseGold;
	}

	public int getRequestBaseLoyalty()
	{
		return requestBaseLoyalty;
	}

	public int getRequestBaseFame()
	{
		return requestBaseFame;
	}

	public string getRequestRewardSet()
	{
		return requestRewardSet;
	}

	public int getRequestRewardQty()
	{
		return requestRewardQty;
	}
}
