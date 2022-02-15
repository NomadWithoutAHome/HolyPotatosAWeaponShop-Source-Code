using System;

[Serializable]
public class Tag
{
	private string tagRefId;

	private string tagName;

	private string tagDesc;

	private bool seenTag;

	private int tagUseCount;

	public Tag()
	{
		tagRefId = string.Empty;
		tagName = string.Empty;
		tagDesc = string.Empty;
		seenTag = false;
		tagUseCount = 0;
	}

	public Tag(string aRefId, string aName, string aDesc)
	{
		tagRefId = aRefId;
		tagName = aName;
		tagDesc = aDesc;
		seenTag = false;
		tagUseCount = 0;
	}

	public string getTagRefId()
	{
		return tagRefId;
	}

	public string getTagName()
	{
		return tagName;
	}

	public string getTagDesc()
	{
		return tagDesc;
	}

	public bool checkSeenTag()
	{
		return seenTag;
	}

	public void setSeenTag(bool aSeen)
	{
		seenTag = aSeen;
	}

	public int getUseCount()
	{
		return tagUseCount;
	}

	public void setUseCount(int aCount)
	{
		tagUseCount = aCount;
	}

	public void addUseCount(int aCount)
	{
		tagUseCount += aCount;
	}
}
