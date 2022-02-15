using System;

[Serializable]
public class ExploreItem
{
	private string areaExploreItemRefID;

	private string areaRefID;

	private string itemRefID;

	private int probability;

	private bool found;

	public ExploreItem()
	{
		areaExploreItemRefID = string.Empty;
		areaRefID = string.Empty;
		itemRefID = string.Empty;
		probability = 0;
		found = false;
	}

	public ExploreItem(string aRefId, string aAreaRefID, string aItemRefID, int aProbability)
	{
		areaExploreItemRefID = aRefId;
		areaRefID = aAreaRefID;
		itemRefID = aItemRefID;
		probability = aProbability;
		found = false;
	}

	public string getAreaExploreItemRefID()
	{
		return areaExploreItemRefID;
	}

	public string getAreaRefID()
	{
		return areaRefID;
	}

	public string getItemRefID()
	{
		return itemRefID;
	}

	public int getProbability()
	{
		return probability;
	}

	public bool getFound()
	{
		return found;
	}

	public void setFound(bool aFound)
	{
		found = aFound;
	}
}
