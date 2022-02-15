using System;

[Serializable]
public class Tutorial
{
	private string tutorialRefId;

	private string tutorialSetRefId;

	private int tutorialOrderIndex;

	private string tutorialTitle;

	private string tutorialText;

	private string tutorialTexturePath;

	private float tutorialXPos;

	private float tutorialYPos;

	public Tutorial()
	{
		tutorialRefId = string.Empty;
		tutorialSetRefId = string.Empty;
		tutorialOrderIndex = 0;
		tutorialTitle = string.Empty;
		tutorialText = string.Empty;
		tutorialTexturePath = string.Empty;
		tutorialXPos = 0f;
		tutorialYPos = 0f;
	}

	public Tutorial(string aRefId, string aSetRefId, int aOrderIndex, string aTitle, string aText, string aTexturePath, float aXPos, float aYPos)
	{
		tutorialRefId = aRefId;
		tutorialSetRefId = aSetRefId;
		tutorialOrderIndex = aOrderIndex;
		tutorialTitle = aTitle;
		tutorialText = aText;
		tutorialTexturePath = aTexturePath;
		tutorialXPos = aXPos;
		tutorialYPos = aYPos;
	}

	public string getTutorialRefId()
	{
		return tutorialRefId;
	}

	public string getTutorialSetRefId()
	{
		return tutorialSetRefId;
	}

	public int getTutorialOrderIndex()
	{
		return tutorialOrderIndex;
	}

	public string getTutorialTitle()
	{
		return tutorialTitle;
	}

	public string getTutorialText()
	{
		return tutorialText;
	}

	public string getTutorialTexturePath()
	{
		return tutorialTexturePath;
	}

	public float getTutorialXPos()
	{
		return tutorialXPos;
	}

	public float getTutorialYPos()
	{
		return tutorialYPos;
	}
}
