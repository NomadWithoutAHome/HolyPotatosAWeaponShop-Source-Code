using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUICutsceneController : MonoBehaviour
{
	private Game game;

	private GUICameraController cameraController;

	private GUICutsceneGridController gridController;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private AudioController audioController;

	private SpriteRenderer cutsceneBg;

	private List<GameObject> charactersList;

	private List<GameObject> obstacleGameobjectList;

	private RuntimeAnimatorController originalController;

	private GameObject cutscene_characters;

	private List<CutsceneDialogue> cutsceneDialogueList;

	private int currentDialogueIndex;

	private GameObject loadingMask;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		cameraController = GameObject.Find("NGUICameraScene").GetComponent<GUICameraController>();
		gridController = GameObject.Find("GUICutsceneGridController").GetComponent<GUICutsceneGridController>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		cutsceneBg = GameObject.Find("CutsceneBg").GetComponent<SpriteRenderer>();
		charactersList = new List<GameObject>();
		obstacleGameobjectList = new List<GameObject>();
		originalController = null;
		cutscene_characters = GameObject.Find("Cutscene_characters");
		currentDialogueIndex = 0;
		loadingMask = GameObject.Find("LoadingMask");
	}

	private IEnumerator testCutscene()
	{
		yield return new WaitForSeconds(4f);
		loadingMask.GetComponent<LoadingScript>().setToTransparent();
		showCutscenes("900199");
	}

	public void showCutscenes(string dialogueSetID)
	{
		cameraController.changeCutsceneCamera();
		cutsceneDialogueList = game.getGameData().getCutsceneDialogueBySetIDAscending(dialogueSetID);
		currentDialogueIndex = 0;
		showDialogue();
	}

	public void showDialogue()
	{
		if (currentDialogueIndex <= cutsceneDialogueList.Count - 1)
		{
			CutsceneDialogue cutsceneDialogue = cutsceneDialogueList[currentDialogueIndex];
			CommonAPI.debug("currdialogue: " + cutsceneDialogue.getDialogueRefId());
			CutsceneType dialogueType = cutsceneDialogue.getDialogueType();
			string characterImage = cutsceneDialogue.getCharacterImage();
			string action = cutsceneDialogue.getAction();
			switch (dialogueType)
			{
			case CutsceneType.CutsceneBackground:
				cutsceneBg.sprite = commonScreenObject.loadSprite("Image/Cutscenes/Background/" + cutsceneDialogue.getCharacterImage());
				showNextDialogue();
				break;
			case CutsceneType.CutsceneSpawn:
				spawnCharacters(cutsceneDialogue);
				break;
			case CutsceneType.CutscenePath:
			{
				GameObject gameObject = GameObject.Find(characterImage);
				gameObject.GetComponent<GUICutscenePathController>().setPath(cutsceneDialogue);
				break;
			}
			case CutsceneType.CutsceneDialogue:
			{
				bool isNextDialoguePresent = false;
				if (currentDialogueIndex < cutsceneDialogueList.Count - 1)
				{
					CutsceneDialogue cutsceneDialogue2 = cutsceneDialogueList[currentDialogueIndex + 1];
					if (cutsceneDialogue2.getDialogueType() == CutsceneType.CutsceneDialogue)
					{
						isNextDialoguePresent = true;
					}
				}
				viewController.showCutsceneDialog(cutsceneDialogue, isNextDialoguePresent);
				break;
			}
			case CutsceneType.CutsceneAction:
			{
				GameObject gameObject = GameObject.Find(characterImage);
				gameObject.GetComponent<GUICutscenePathController>().changeAction(characterImage, action);
				break;
			}
			case CutsceneType.CutsceneDestroy:
				deleteCharacter(characterImage);
				break;
			case CutsceneType.CutsceneActionLoopStart:
			{
				GameObject gameObject = GameObject.Find(characterImage);
				gameObject.GetComponent<GUICutscenePathController>().changeAction(characterImage, action, aLoop: true);
				break;
			}
			case CutsceneType.CutsceneActionLoopEnd:
			{
				GameObject gameObject = GameObject.Find(characterImage);
				gameObject.GetComponent<GUICutscenePathController>().setLoop(aLoop: false);
				gameObject.GetComponent<GUICutscenePathController>().stopAction();
				break;
			}
			case CutsceneType.CutsceneObsSpawn:
				createObstacle(cutsceneDialogue.getCutsceneObstacleRefID());
				break;
			case CutsceneType.CutsceneObsDestroy:
				deleteObstacle(cutsceneDialogue.getCutsceneObstacleRefID());
				break;
			case CutsceneType.CutsceneObsAnimSpawn:
				createObstacle(cutsceneDialogue.getCutsceneObstacleRefID(), animate: true);
				break;
			case CutsceneType.CutsceneObsAnimDestroy:
				deleteObstacle(cutsceneDialogue.getCutsceneObstacleRefID());
				break;
			}
			return;
		}
		for (int i = 0; i < charactersList.Count; i++)
		{
			if (charactersList[i] != null)
			{
				deleteCharacter(charactersList[i].name, showNext: false);
			}
		}
		charactersList.Clear();
		for (int j = 0; j < obstacleGameobjectList.Count; j++)
		{
			if (obstacleGameobjectList[j] != null)
			{
				deleteObstacle(obstacleGameobjectList[j].name, showNext: false);
			}
		}
		obstacleGameobjectList.Clear();
		cameraController.changeSceneCamera();
	}

	public void spawnCharacters(CutsceneDialogue aDialogue)
	{
		GameObject gameObject = commonScreenObject.createPrefab(cutscene_characters, aDialogue.getCharacterImage(), "Prefab/Cutscene/cutsceneCharacter", Vector3.zero, Vector3.one, Vector3.zero);
		RuntimeAnimatorController runtimeAnimatorController = Resources.Load("Animation/Cutscene/" + aDialogue.getCharacterImage()) as RuntimeAnimatorController;
		gameObject.GetComponentInChildren<Animator>().runtimeAnimatorController = runtimeAnimatorController;
		commonScreenObject.findChild(gameObject, "spriteAnimator").gameObject.name = "Character_" + aDialogue.getCharacterImage();
		gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
		charactersList.Add(gameObject);
		gameObject.GetComponent<GUICutscenePathController>().setPosition(aDialogue);
		audioController.playSmithEnterAudio();
		showNextDialogue();
	}

	public void createObstacle(string aRefID, bool animate = false)
	{
		CutsceneObstacle cutsceneObstacleByRefID = game.getGameData().getCutsceneObstacleByRefID(aRefID);
		CommonAPI.debug("obstacle start x: " + cutsceneObstacleByRefID.getObstaclePoint().x + " y: " + cutsceneObstacleByRefID.getObstaclePoint().y);
		Vector3 position = gridController.getPosition((int)cutsceneObstacleByRefID.getObstaclePoint().x, (int)cutsceneObstacleByRefID.getObstaclePoint().y);
		float num = position.x;
		float num2 = position.z;
		if (cutsceneObstacleByRefID.getWidth() > 1)
		{
			CommonAPI.debug(cutsceneObstacleByRefID.getRefObstacleID() + "calculations: " + (float)(cutsceneObstacleByRefID.getWidth() - 1) / 2f);
			num += (float)(cutsceneObstacleByRefID.getWidth() - 1) / 2f * 0.73f;
		}
		if (cutsceneObstacleByRefID.getHeight() > 1)
		{
			num2 += (float)(cutsceneObstacleByRefID.getHeight() - 1) / 2f * 0.74f;
		}
		Vector3 aPosition = new Vector3(num, cutsceneObstacleByRefID.getYValue(), num2);
		GameObject gameObject = commonScreenObject.createPrefab(GameObject.Find("CutsceneWorld"), "CutObs_" + cutsceneObstacleByRefID.getRefObstacleID(), "Prefab/Cutscene/cutsceneObstacle", aPosition, Vector3.one, Vector3.zero);
		gameObject.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Obstacle/" + cutsceneObstacleByRefID.getImage());
		gameObject.transform.localRotation = Quaternion.Euler(35.264f, 45f, 0f);
		gameObject.transform.localScale = Vector3.one;
		gameObject.GetComponent<SpriteRenderer>().sortingOrder = cutsceneObstacleByRefID.getSortOrder();
		if (animate)
		{
			Animator animator = gameObject.AddComponent<Animator>();
			RuntimeAnimatorController runtimeAnimatorController2 = (animator.runtimeAnimatorController = Resources.Load("Animation/Cutscene/Obstacle/CutsceneObstacle") as RuntimeAnimatorController);
			AnimatorOverrideController animatorOverrideController = new AnimatorOverrideController();
			if (originalController == null)
			{
				originalController = runtimeAnimatorController2;
			}
			animatorOverrideController.runtimeAnimatorController = originalController;
			animatorOverrideController["Default"] = Resources.Load("Animation/Cutscene/Obstacles/" + cutsceneObstacleByRefID.getImage()) as AnimationClip;
			animator.runtimeAnimatorController = animatorOverrideController;
		}
		obstacleGameobjectList.Add(gameObject);
		showNextDialogue();
	}

	public void deleteCharacter(string gameObjectName, bool showNext = true)
	{
		GameObject gameObject = GameObject.Find(gameObjectName);
		charactersList.Remove(gameObject);
		commonScreenObject.destroyPrefab(gameObject);
		if (showNext)
		{
			showNextDialogue();
		}
	}

	public void deleteObstacle(string aRefID, bool showNext = true)
	{
		GameObject gameObject = GameObject.Find(aRefID);
		obstacleGameobjectList.Remove(gameObject);
		commonScreenObject.destroyPrefab(gameObject);
		if (showNext)
		{
			showNextDialogue();
		}
	}

	public void showNextDialogue()
	{
		currentDialogueIndex++;
		showDialogue();
	}
}
