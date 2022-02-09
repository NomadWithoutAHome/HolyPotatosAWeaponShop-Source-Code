using System;

[Serializable]
public class DialogueNEW
{
	private string dialogueRefId;

	private string dialogueSetId;

	private string dialogueName;

	private string dialogueText;

	private string dialogueNextRefId;

	private string dialogueChoice1;

	private string dialogueChoice1NextRefId;

	private string dialogueChoice2;

	private string dialogueChoice2NextRefId;

	private string dialoguePosition;

	private string backgroundTexture;

	private string soundEffect;

	private string backgroundMusic;

	private string dialogueImage;

	private bool dialogueFlip;

	public DialogueNEW()
	{
		dialogueRefId = string.Empty;
		dialogueSetId = string.Empty;
		dialogueName = string.Empty;
		dialogueText = string.Empty;
		dialogueNextRefId = string.Empty;
		dialogueChoice1 = string.Empty;
		dialogueChoice1NextRefId = string.Empty;
		dialogueChoice2 = string.Empty;
		dialogueChoice2NextRefId = string.Empty;
		dialoguePosition = string.Empty;
		backgroundTexture = string.Empty;
		soundEffect = string.Empty;
		backgroundMusic = string.Empty;
		dialogueImage = string.Empty;
		dialogueFlip = false;
	}

	public DialogueNEW(string aRefId, string aSetId, string aName, string aText, string aNextRefId, string aChoice1, string aChoice1Next, string aChoice2, string aChoice2Next, string aPosition, string aBackgroundTexture, string aSoundEffect, string aBgm, string aImage, bool aFlip)
	{
		dialogueRefId = aRefId;
		dialogueSetId = aSetId;
		dialogueName = aName;
		dialogueText = aText;
		dialogueNextRefId = aNextRefId;
		dialogueChoice1 = aChoice1;
		dialogueChoice1NextRefId = aChoice1Next;
		dialogueChoice2 = aChoice2;
		dialogueChoice2NextRefId = aChoice2Next;
		dialoguePosition = aPosition;
		backgroundTexture = aBackgroundTexture;
		soundEffect = aSoundEffect;
		backgroundMusic = aBgm;
		dialogueImage = aImage;
		dialogueFlip = aFlip;
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

	public string getDialogueNextRefId()
	{
		return dialogueNextRefId;
	}

	public string getDialogueChoice1()
	{
		return dialogueChoice1;
	}

	public string getDialogueChoice1NextRefId()
	{
		return dialogueChoice1NextRefId;
	}

	public string getDialogueChoice2()
	{
		return dialogueChoice2;
	}

	public string getDialogueChoice2NextRefId()
	{
		return dialogueChoice2NextRefId;
	}

	public string getDialoguePosition()
	{
		return dialoguePosition;
	}

	public string getBackgroundTexture()
	{
		return backgroundTexture;
	}

	public string getSoundEffect()
	{
		return soundEffect;
	}

	public string getBGM()
	{
		return backgroundMusic;
	}

	public string getDialogueImage()
	{
		return dialogueImage;
	}

	public bool getDialogueFlip()
	{
		return dialogueFlip;
	}
}
