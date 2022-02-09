using System;

[Serializable]
public class SmithTag
{
	private string smithTagId;

	private string tagRefId;

	private string smithRefId;

	private bool replaceable;

	private int useCount;

	private int displayOrder;

	public SmithTag()
	{
		smithTagId = string.Empty;
		tagRefId = string.Empty;
		smithRefId = string.Empty;
		replaceable = false;
		useCount = 0;
		displayOrder = 0;
	}

	public SmithTag(string aSmithTagId, string aTagRefId, string aSmithRefId, bool aReplaceable)
	{
		smithTagId = aSmithTagId;
		tagRefId = aTagRefId;
		smithRefId = aSmithRefId;
		replaceable = aReplaceable;
		useCount = 0;
		displayOrder = 0;
	}

	public string getSmithTagId()
	{
		return smithTagId;
	}

	public string getTagRefId()
	{
		return tagRefId;
	}

	public void setTagRefId(string aTagRefId)
	{
		tagRefId = aTagRefId;
	}

	public string getSmithRefId()
	{
		return smithRefId;
	}

	public bool getReplaceable()
	{
		return replaceable;
	}

	public void setReplaceable(bool aReplaceable)
	{
		replaceable = aReplaceable;
	}

	public int getUseCount()
	{
		return useCount;
	}

	public void setUseCount(int aCount)
	{
		useCount = aCount;
	}

	public void addUseCount(int aCount)
	{
		useCount += aCount;
	}

	public int getDisplayOrder()
	{
		return displayOrder;
	}

	public void setDisplayOrder(int aOrder)
	{
		displayOrder = aOrder;
	}
}
