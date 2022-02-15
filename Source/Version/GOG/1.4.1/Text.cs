using System;

[Serializable]
public class Text
{
	private string textRefId;

	private string reference;

	private string text;

	public Text()
	{
		textRefId = string.Empty;
		reference = string.Empty;
		text = string.Empty;
	}

	public Text(string aTextRefId, string aReference, string aText)
	{
		textRefId = aTextRefId;
		reference = aReference;
		text = aText;
	}

	public string getTextRefId()
	{
		return textRefId;
	}

	public string getReference()
	{
		return reference;
	}

	public string getText()
	{
		return text;
	}
}
