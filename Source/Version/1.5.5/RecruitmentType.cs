using System;
using System.Collections.Generic;

[Serializable]
public class RecruitmentType
{
	private string recruitmentRefId;

	private string recruitmentName;

	private string recruitmentDesc;

	private int recruitmentCost;

	private int recruitmentMaxNum;

	private int recruitmentDuration;

	private int shopLevelReq;

	private int monthReq;

	private List<string> recruitmentSmithList;

	public RecruitmentType()
	{
		recruitmentRefId = string.Empty;
		recruitmentName = string.Empty;
		recruitmentDesc = string.Empty;
		recruitmentCost = 0;
		recruitmentMaxNum = 0;
		recruitmentDuration = 0;
		shopLevelReq = 0;
		monthReq = 0;
		recruitmentSmithList = new List<string>();
	}

	public RecruitmentType(string aRefId, string aName, string aDesc, int aCost, int aMax, int aDuration, int aLevel, int aMonth, List<string> aList)
	{
		recruitmentRefId = aRefId;
		recruitmentName = aName;
		recruitmentDesc = aDesc;
		recruitmentCost = aCost;
		recruitmentMaxNum = aMax;
		recruitmentDuration = aDuration;
		shopLevelReq = aLevel;
		monthReq = aMonth;
		recruitmentSmithList = aList;
	}

	public string getRecruitmentRefId()
	{
		return recruitmentRefId;
	}

	public string getRecruitmentName()
	{
		return recruitmentName;
	}

	public string getRecruitmentDesc()
	{
		return recruitmentDesc;
	}

	public int getRecruitmentCost()
	{
		return recruitmentCost;
	}

	public int getRecruitmentMaxNum()
	{
		return recruitmentMaxNum;
	}

	public int getRecruitmentDuration()
	{
		return recruitmentDuration;
	}

	public List<string> getRecruitmentSmithList()
	{
		return recruitmentSmithList;
	}

	public bool checkRequirements(int level, int month)
	{
		if (shopLevelReq > level || monthReq > month)
		{
			return false;
		}
		return true;
	}
}
