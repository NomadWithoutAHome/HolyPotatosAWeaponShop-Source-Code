using System;

[Serializable]
public class DecorationEffect
{
	private string decorationEffectRefId;

	private string decorationRefId;

	private string decorationBoostType;

	private string decorationBoostRefId;

	private float decorationBoostMult;

	public DecorationEffect()
	{
		decorationEffectRefId = string.Empty;
		decorationRefId = string.Empty;
		decorationBoostType = string.Empty;
		decorationBoostRefId = string.Empty;
		decorationBoostMult = 0f;
	}

	public DecorationEffect(string aEffectRefId, string aRefId, string aBoostType, string aBoostRefId, float aBoostMult)
	{
		decorationEffectRefId = aEffectRefId;
		decorationRefId = aRefId;
		decorationBoostType = aBoostType;
		decorationBoostRefId = aBoostRefId;
		decorationBoostMult = aBoostMult;
	}

	public string getDecorationEffectRefId()
	{
		return decorationEffectRefId;
	}

	public string getDecorationRefId()
	{
		return decorationRefId;
	}

	public string getDecorationBoostType()
	{
		return decorationBoostType;
	}

	public string getDecorationBoostRefId()
	{
		return decorationBoostRefId;
	}

	public float getDecorationBoostMult()
	{
		return decorationBoostMult;
	}

	public string getDecorationStringBoost(string aText)
	{
		string empty = string.Empty;
		if (decorationBoostMult > 1f)
		{
			return "+ " + aText + " " + (int)((decorationBoostMult - 1f) * 100f) + "%";
		}
		return "- " + aText + " " + (int)((1f - decorationBoostMult) * 100f) + "%";
	}
}
