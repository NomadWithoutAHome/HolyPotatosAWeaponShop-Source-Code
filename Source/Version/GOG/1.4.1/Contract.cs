using System;

[Serializable]
public class Contract
{
	private string contractRefId;

	private string contractName;

	private string contractDesc;

	private int rewardGold;

	private int timeLimit;

	private int atkReq;

	private int spdReq;

	private int accReq;

	private int magReq;

	private int monthStart;

	private int monthEnd;

	private int timesStarted;

	private int timesCompleted;

	public Contract()
	{
		contractRefId = string.Empty;
		contractName = string.Empty;
		contractDesc = string.Empty;
		rewardGold = 0;
		timeLimit = 0;
		atkReq = 0;
		spdReq = 0;
		accReq = 0;
		magReq = 0;
		monthStart = 0;
		monthEnd = 0;
		timesStarted = 0;
		timesCompleted = 0;
	}

	public Contract(string aContractRefId, string aName, string aDesc, int aGold, int aLimit, int aAtkReq, int aSpdReq, int aAccReq, int aMagReq, int aMonthStart, int aMonthEnd)
	{
		contractRefId = aContractRefId;
		contractName = aName;
		contractDesc = aDesc;
		rewardGold = aGold;
		timeLimit = aLimit;
		atkReq = aAtkReq;
		spdReq = aSpdReq;
		accReq = aAccReq;
		magReq = aMagReq;
		monthStart = aMonthStart;
		monthEnd = aMonthEnd;
		timesStarted = 0;
		timesCompleted = 0;
	}

	public void resetDynData()
	{
		timesStarted = 0;
		timesCompleted = 0;
	}

	public string getContractRefId()
	{
		return contractRefId;
	}

	public string getContractName()
	{
		return contractName;
	}

	public string getContractDesc()
	{
		return contractDesc;
	}

	public int getGold()
	{
		return rewardGold;
	}

	public int getTimeLimit()
	{
		return timeLimit;
	}

	public string getTimeLimitString()
	{
		return CommonAPI.convertHalfHoursToTimeString(timeLimit);
	}

	public string getReqString()
	{
		string text = " | ";
		if (atkReq > 0)
		{
			string text2 = text;
			text = text2 + "Atk " + atkReq + " | ";
		}
		if (spdReq > 0)
		{
			string text2 = text;
			text = text2 + "Spd " + spdReq + " | ";
		}
		if (accReq > 0)
		{
			string text2 = text;
			text = text2 + "Acc " + accReq + " | ";
		}
		if (magReq > 0)
		{
			string text2 = text;
			text = text2 + "Mag " + magReq + " | ";
		}
		return text;
	}

	public int getAtkReq()
	{
		return atkReq;
	}

	public int getSpdReq()
	{
		return spdReq;
	}

	public int getAccReq()
	{
		return accReq;
	}

	public int getMagReq()
	{
		return magReq;
	}

	public int getMonthStart()
	{
		return monthStart;
	}

	public bool checkMonthRequirement(int month)
	{
		if (month >= monthStart && (monthEnd == -1 || month < monthEnd))
		{
			return true;
		}
		return false;
	}

	public void setTimesStarted(int aStarted)
	{
		timesStarted = aStarted;
	}

	public void addTimesStarted(int aStarted)
	{
		timesStarted += aStarted;
	}

	public int getTimesStarted()
	{
		return timesStarted;
	}

	public void setTimesCompleted(int aCompleted)
	{
		timesCompleted = aCompleted;
	}

	public void addTimesCompleted(int aCompleted)
	{
		timesCompleted += aCompleted;
	}

	public int getTimesCompleted()
	{
		return timesCompleted;
	}
}
