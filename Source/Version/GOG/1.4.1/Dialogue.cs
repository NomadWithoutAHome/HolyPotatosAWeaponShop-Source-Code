using System;

[Serializable]
public class Dialogue
{
	private string dialogueRefId;

	private string dialogueSetId;

	private string dialogueName;

	private string dialogueText;

	private string dialogueAction;

	private string dialoguePosition;

	private string image;

	public Dialogue()
	{
		dialogueRefId = string.Empty;
		dialogueSetId = string.Empty;
		dialogueName = string.Empty;
		dialogueText = string.Empty;
		dialogueAction = string.Empty;
		dialoguePosition = string.Empty;
		image = string.Empty;
	}

	public Dialogue(string aRefId, string aSetId, string aName, string aText, string aAction, string aPosition, string aImage)
	{
		dialogueRefId = aRefId;
		dialogueSetId = aSetId;
		dialogueName = aName;
		dialogueText = aText;
		dialogueAction = aAction;
		dialoguePosition = aPosition;
		image = aImage;
	}

	public string getDialogueRefId()
	{
		return dialogueRefId;
	}

	public string getDialogueSetId()
	{
		return dialogueSetId;
	}

	public string getDialogueName()
	{
		return dialogueName;
	}

	public string getDialogueText()
	{
		return dialogueText;
	}

	public string getDialogueAction()
	{
		return dialogueAction;
	}

	public string getDialoguePosition()
	{
		return dialoguePosition;
	}

	public string getImage()
	{
		return image;
	}
}
