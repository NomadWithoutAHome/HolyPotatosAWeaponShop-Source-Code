using System;

[Serializable]
public class JobClassTag
{
	private string jobClassTagRefId;

	private string tagRefId;

	private string jobClassRefId;

	public JobClassTag()
	{
		jobClassTagRefId = string.Empty;
		tagRefId = string.Empty;
		jobClassRefId = string.Empty;
	}

	public JobClassTag(string aJobClassTagRefId, string aTagRefId, string aJobClassRefId)
	{
		jobClassTagRefId = aJobClassTagRefId;
		tagRefId = aTagRefId;
		jobClassRefId = aJobClassRefId;
	}

	public string getJobClassTagRefId()
	{
		return jobClassTagRefId;
	}

	public string getTagRefId()
	{
		return tagRefId;
	}

	public string getJobClassRefId()
	{
		return jobClassRefId;
	}
}
