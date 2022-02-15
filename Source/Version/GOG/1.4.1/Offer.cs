using System;

[Serializable]
public class Offer
{
	private string offerId;

	private string projectId;

	private string heroRefId;

	private int offerPrice;

	private int weaponScore;

	private int expGrowth;

	private float starchMult;

	private float expMult;

	public Offer()
	{
		offerId = string.Empty;
		projectId = string.Empty;
		heroRefId = string.Empty;
		offerPrice = 0;
		weaponScore = 0;
		expGrowth = 0;
		starchMult = 0f;
		expMult = 0f;
	}

	public Offer(string aOfferId, string aProjectId, string aHeroRefId, int aOfferPrice, int aWeaponScore, int aExpGrowth, float aStarchMult, float aExpMult)
	{
		offerId = aOfferId;
		projectId = aProjectId;
		heroRefId = aHeroRefId;
		offerPrice = aOfferPrice;
		weaponScore = aWeaponScore;
		expGrowth = aExpGrowth;
		starchMult = aStarchMult;
		expMult = aExpMult;
	}

	public string getOfferId()
	{
		return offerId;
	}

	public string getProjectId()
	{
		return projectId;
	}

	public string getHeroRefId()
	{
		return heroRefId;
	}

	public int getPrice()
	{
		return offerPrice;
	}

	public int getWeaponScore()
	{
		return weaponScore;
	}

	public int getExpGrowth()
	{
		return expGrowth;
	}

	public float getStarchMult()
	{
		return starchMult;
	}

	public float getExpMult()
	{
		return expMult;
	}
}
