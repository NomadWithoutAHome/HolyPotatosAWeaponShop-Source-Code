using System;

[Serializable]
public class RandomText
{
	private string randomTextRefId;

	private string textRefId;

	private string setRefId;

	public RandomText()
	{
		randomTextRefId = string.Empty;
		textRefId = string.Empty;
		setRefId = string.Empty;
	}

	public RandomText(string aRandomTextRefId, string aTextRefId, string aSetRefId)
	{
		randomTextRefId = aRandomTextRefId;
		textRefId = aTextRefId;
		setRefId = aSetRefId;
	}

	public string getRandomTextRefId()
	{
		return randomTextRefId;
	}

	public string getTextRefId()
	{
		return textRefId;
	}

	public string getSetRefId()
	{
		return setRefId;
	}
}
