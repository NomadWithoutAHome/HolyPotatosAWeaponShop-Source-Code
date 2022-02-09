using System;
using UnityEngine;

[Serializable]
public class CutsceneDialogue : MonoBehaviour
{
	private string dialogueRefId;

	private string dialogueSetId;

	private int displayOrder;

	private CutsceneType dialogueType;

	private string cutscenePathRefID;

	private string cutsceneObstacleRefID;

	private string dialogueName;

	private string dialogueText;

	private string characterImage;

	private string action;

	private Vector2 spawnPoint;

	private float yValue;

	private Vector3 startingRotation;

	private bool faceBack;

	private string nextDialogueRefID;

	public CutsceneDialogue()
	{
		dialogueRefId = string.Empty;
		dialogueSetId = string.Empty;
		displayOrder = 0;
		dialogueType = CutsceneType.CutsceneBlank;
		cutscenePathRefID = string.Empty;
		cutsceneObstacleRefID = string.Empty;
		dialogueName = string.Empty;
		dialogueText = string.Empty;
		characterImage = string.Empty;
		action = string.Empty;
		spawnPoint = new Vector2(-1f, -1f);
		yValue = 0f;
		startingRotation = new Vector3(0f, 0f, 0f);
		faceBack = false;
		nextDialogueRefID = string.Empty;
	}

	public CutsceneDialogue(string aRefId, string aSetId, int aDisplayOrder, string aType, string aCutscenePathRefID, string aCutsceneObstacleRefID, string aName, string aText, string aCharacterImage, string aAction, string aSpawnPoint, float aYValue, string aStartingView, string aNextDialogueRefID)
	{
		dialogueRefId = aRefId;
		dialogueSetId = aSetId;
		displayOrder = aDisplayOrder;
		switch (aType)
		{
		case "BACKGROUND":
			dialogueType = CutsceneType.CutsceneBackground;
			break;
		case "SPAWN":
			dialogueType = CutsceneType.CutsceneSpawn;
			break;
		case "PATH":
			dialogueType = CutsceneType.CutscenePath;
			break;
		case "DIALOGUE":
			dialogueType = CutsceneType.CutsceneDialogue;
			break;
		case "ACTION":
			dialogueType = CutsceneType.CutsceneAction;
			break;
		case "DESTROY":
			dialogueType = CutsceneType.CutsceneDestroy;
			break;
		case "ACTIONLOOPSTART":
			dialogueType = CutsceneType.CutsceneActionLoopStart;
			break;
		case "ACTIONLOOPEND":
			dialogueType = CutsceneType.CutsceneActionLoopEnd;
			break;
		case "OBSSPAWN":
			dialogueType = CutsceneType.CutsceneObsSpawn;
			break;
		case "OBSDESTROY":
			dialogueType = CutsceneType.CutsceneObsDestroy;
			break;
		case "OBSANIMSPAWN":
			dialogueType = CutsceneType.CutsceneObsAnimSpawn;
			break;
		case "OBSANIMDESTROY":
			dialogueType = CutsceneType.CutsceneObsAnimDestroy;
			break;
		}
		cutscenePathRefID = aCutscenePathRefID;
		cutsceneObstacleRefID = aCutsceneObstacleRefID;
		dialogueName = aName;
		dialogueText = aText;
		characterImage = aCharacterImage;
		action = aAction;
		string[] array = aSpawnPoint.Split(',');
		if (array.Length > 1)
		{
			spawnPoint = new Vector2(CommonAPI.parseInt(array[0]), CommonAPI.parseInt(array[1]));
		}
		yValue = aYValue;
		switch (aStartingView)
		{
		case "LEFT_FRONT":
			startingRotation = new Vector3(35.264f, 45f, 0f);
			faceBack = false;
			break;
		case "LEFT_BACK":
			startingRotation = new Vector3(-35.264f, 225f, 0f);
			faceBack = true;
			break;
		case "RIGHT_FRONT":
			startingRotation = new Vector3(-35.264f, 225f, 0f);
			faceBack = false;
			break;
		case "RIGHT_BACK":
			startingRotation = new Vector3(35.264f, 45f, 0f);
			faceBack = true;
			break;
		}
		nextDialogueRefID = aNextDialogueRefID;
	}

	public string getDialogueRefId()
	{
		return dialogueRefId;
	}

	public string getDialogueSetId()
	{
		return dialogueSetId;
	}

	public int getDisplayOrder()
	{
		return displayOrder;
	}

	public CutsceneType getDialogueType()
	{
		return dialogueType;
	}

	public string getCutscenePathRefID()
	{
		return cutscenePathRefID;
	}

	public string getCutsceneObstacleRefID()
	{
		return cutsceneObstacleRefID;
	}

	public string getDialogueName()
	{
		return dialogueName;
	}

	public string getDialogueText()
	{
		return dialogueText;
	}

	public string getCharacterImage()
	{
		return characterImage;
	}

	public string getAction()
	{
		return action;
	}

	public Vector2 getSpawnPoint()
	{
		return spawnPoint;
	}

	public float getYValue()
	{
		return yValue;
	}

	public Vector3 getStartingRotation()
	{
		return startingRotation;
	}

	public bool getFaceBack()
	{
		return faceBack;
	}

	public string getNextDialogueRefID()
	{
		return nextDialogueRefID;
	}
}
