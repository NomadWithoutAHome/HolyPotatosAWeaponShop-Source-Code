using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000192 RID: 402
public class GUICutsceneController : MonoBehaviour
{
	// Token: 0x06000A22 RID: 2594 RVA: 0x0004B728 File Offset: 0x00049B28
	private void Awake()
	{
		this.game = GameObject.Find("Game").GetComponent<Game>();
		this.cameraController = GameObject.Find("NGUICameraScene").GetComponent<GUICameraController>();
		this.gridController = GameObject.Find("GUICutsceneGridController").GetComponent<GUICutsceneGridController>();
		this.commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		this.viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		this.audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		this.cutsceneBg = GameObject.Find("CutsceneBg").GetComponent<SpriteRenderer>();
		this.charactersList = new List<GameObject>();
		this.obstacleGameobjectList = new List<GameObject>();
		this.originalController = null;
		this.cutscene_characters = GameObject.Find("Cutscene_characters");
		this.currentDialogueIndex = 0;
		this.loadingMask = GameObject.Find("LoadingMask");
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x0004B80C File Offset: 0x00049C0C
	private IEnumerator testCutscene()
	{
		yield return new WaitForSeconds(4f);
		this.loadingMask.GetComponent<LoadingScript>().setToTransparent();
		this.showCutscenes("900199");
		yield break;
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x0004B827 File Offset: 0x00049C27
	public void showCutscenes(string dialogueSetID)
	{
		this.cameraController.changeCutsceneCamera();
		this.cutsceneDialogueList = this.game.getGameData().getCutsceneDialogueBySetIDAscending(dialogueSetID);
		this.currentDialogueIndex = 0;
		this.showDialogue();
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x0004B858 File Offset: 0x00049C58
	public void showDialogue()
	{
		if (this.currentDialogueIndex <= this.cutsceneDialogueList.Count - 1)
		{
			CutsceneDialogue cutsceneDialogue = this.cutsceneDialogueList[this.currentDialogueIndex];
			CommonAPI.debug("currdialogue: " + cutsceneDialogue.getDialogueRefId());
			CutsceneType dialogueType = cutsceneDialogue.getDialogueType();
			string characterImage = cutsceneDialogue.getCharacterImage();
			string action = cutsceneDialogue.getAction();
			switch (dialogueType)
			{
			case CutsceneType.CutsceneBackground:
				this.cutsceneBg.sprite = this.commonScreenObject.loadSprite("Image/Cutscenes/Background/" + cutsceneDialogue.getCharacterImage());
				this.showNextDialogue();
				break;
			case CutsceneType.CutsceneSpawn:
				this.spawnCharacters(cutsceneDialogue);
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
				if (this.currentDialogueIndex < this.cutsceneDialogueList.Count - 1)
				{
					CutsceneDialogue cutsceneDialogue2 = this.cutsceneDialogueList[this.currentDialogueIndex + 1];
					if (cutsceneDialogue2.getDialogueType() == CutsceneType.CutsceneDialogue)
					{
						isNextDialoguePresent = true;
					}
				}
				this.viewController.showCutsceneDialog(cutsceneDialogue, isNextDialoguePresent);
				break;
			}
			case CutsceneType.CutsceneAction:
			{
				GameObject gameObject = GameObject.Find(characterImage);
				gameObject.GetComponent<GUICutscenePathController>().changeAction(characterImage, action, false);
				break;
			}
			case CutsceneType.CutsceneDestroy:
				this.deleteCharacter(characterImage, true);
				break;
			case CutsceneType.CutsceneActionLoopStart:
			{
				GameObject gameObject = GameObject.Find(characterImage);
				gameObject.GetComponent<GUICutscenePathController>().changeAction(characterImage, action, true);
				break;
			}
			case CutsceneType.CutsceneActionLoopEnd:
			{
				GameObject gameObject = GameObject.Find(characterImage);
				gameObject.GetComponent<GUICutscenePathController>().setLoop(false);
				gameObject.GetComponent<GUICutscenePathController>().stopAction();
				break;
			}
			case CutsceneType.CutsceneObsSpawn:
				this.createObstacle(cutsceneDialogue.getCutsceneObstacleRefID(), false);
				break;
			case CutsceneType.CutsceneObsDestroy:
				this.deleteObstacle(cutsceneDialogue.getCutsceneObstacleRefID(), true);
				break;
			case CutsceneType.CutsceneObsAnimSpawn:
				this.createObstacle(cutsceneDialogue.getCutsceneObstacleRefID(), true);
				break;
			case CutsceneType.CutsceneObsAnimDestroy:
				this.deleteObstacle(cutsceneDialogue.getCutsceneObstacleRefID(), true);
				break;
			}
		}
		else
		{
			for (int i = 0; i < this.charactersList.Count; i++)
			{
				if (this.charactersList[i] != null)
				{
					this.deleteCharacter(this.charactersList[i].name, false);
				}
			}
			this.charactersList.Clear();
			for (int j = 0; j < this.obstacleGameobjectList.Count; j++)
			{
				if (this.obstacleGameobjectList[j] != null)
				{
					this.deleteObstacle(this.obstacleGameobjectList[j].name, false);
				}
			}
			this.obstacleGameobjectList.Clear();
			this.cameraController.changeSceneCamera();
		}
	}

	// Token: 0x06000A26 RID: 2598 RVA: 0x0004BB1C File Offset: 0x00049F1C
	public void spawnCharacters(CutsceneDialogue aDialogue)
	{
		GameObject gameObject = this.commonScreenObject.createPrefab(this.cutscene_characters, aDialogue.getCharacterImage(), "Prefab/Cutscene/cutsceneCharacter", Vector3.zero, Vector3.one, Vector3.zero);
		RuntimeAnimatorController runtimeAnimatorController = Resources.Load("Animation/Cutscene/" + aDialogue.getCharacterImage()) as RuntimeAnimatorController;
		gameObject.GetComponentInChildren<Animator>().runtimeAnimatorController = runtimeAnimatorController;
		this.commonScreenObject.findChild(gameObject, "spriteAnimator").gameObject.name = "Character_" + aDialogue.getCharacterImage();
		gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
		this.charactersList.Add(gameObject);
		gameObject.GetComponent<GUICutscenePathController>().setPosition(aDialogue);
		this.audioController.playSmithEnterAudio();
		this.showNextDialogue();
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x0004BBF0 File Offset: 0x00049FF0
	public void createObstacle(string aRefID, bool animate = false)
	{
		CutsceneObstacle cutsceneObstacleByRefID = this.game.getGameData().getCutsceneObstacleByRefID(aRefID);
		CommonAPI.debug(string.Concat(new object[]
		{
			"obstacle start x: ",
			cutsceneObstacleByRefID.getObstaclePoint().x,
			" y: ",
			cutsceneObstacleByRefID.getObstaclePoint().y
		}));
		Vector3 position = this.gridController.getPosition((int)cutsceneObstacleByRefID.getObstaclePoint().x, (int)cutsceneObstacleByRefID.getObstaclePoint().y);
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
		GameObject gameObject = this.commonScreenObject.createPrefab(GameObject.Find("CutsceneWorld"), "CutObs_" + cutsceneObstacleByRefID.getRefObstacleID(), "Prefab/Cutscene/cutsceneObstacle", aPosition, Vector3.one, Vector3.zero);
		gameObject.GetComponent<SpriteRenderer>().sprite = this.commonScreenObject.loadSprite("Image/Obstacle/" + cutsceneObstacleByRefID.getImage());
		gameObject.transform.localRotation = Quaternion.Euler(35.264f, 45f, 0f);
		gameObject.transform.localScale = Vector3.one;
		gameObject.GetComponent<SpriteRenderer>().sortingOrder = cutsceneObstacleByRefID.getSortOrder();
		if (animate)
		{
			Animator animator = gameObject.AddComponent<Animator>();
			RuntimeAnimatorController runtimeAnimatorController = Resources.Load("Animation/Cutscene/Obstacle/CutsceneObstacle") as RuntimeAnimatorController;
			animator.runtimeAnimatorController = runtimeAnimatorController;
			AnimatorOverrideController animatorOverrideController = new AnimatorOverrideController();
			if (this.originalController == null)
			{
				this.originalController = runtimeAnimatorController;
			}
			animatorOverrideController.runtimeAnimatorController = this.originalController;
			animatorOverrideController["Default"] = (Resources.Load("Animation/Cutscene/Obstacles/" + cutsceneObstacleByRefID.getImage()) as AnimationClip);
			animator.runtimeAnimatorController = animatorOverrideController;
		}
		this.obstacleGameobjectList.Add(gameObject);
		this.showNextDialogue();
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x0004BE5C File Offset: 0x0004A25C
	public void deleteCharacter(string gameObjectName, bool showNext = true)
	{
		GameObject gameObject = GameObject.Find(gameObjectName);
		this.charactersList.Remove(gameObject);
		this.commonScreenObject.destroyPrefab(gameObject);
		if (showNext)
		{
			this.showNextDialogue();
		}
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x0004BE98 File Offset: 0x0004A298
	public void deleteObstacle(string aRefID, bool showNext = true)
	{
		GameObject gameObject = GameObject.Find(aRefID);
		this.obstacleGameobjectList.Remove(gameObject);
		this.commonScreenObject.destroyPrefab(gameObject);
		if (showNext)
		{
			this.showNextDialogue();
		}
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x0004BED1 File Offset: 0x0004A2D1
	public void showNextDialogue()
	{
		this.currentDialogueIndex++;
		this.showDialogue();
	}

	// Token: 0x040009B2 RID: 2482
	private Game game;

	// Token: 0x040009B3 RID: 2483
	private GUICameraController cameraController;

	// Token: 0x040009B4 RID: 2484
	private GUICutsceneGridController gridController;

	// Token: 0x040009B5 RID: 2485
	private CommonScreenObject commonScreenObject;

	// Token: 0x040009B6 RID: 2486
	private ViewController viewController;

	// Token: 0x040009B7 RID: 2487
	private AudioController audioController;

	// Token: 0x040009B8 RID: 2488
	private SpriteRenderer cutsceneBg;

	// Token: 0x040009B9 RID: 2489
	private List<GameObject> charactersList;

	// Token: 0x040009BA RID: 2490
	private List<GameObject> obstacleGameobjectList;

	// Token: 0x040009BB RID: 2491
	private RuntimeAnimatorController originalController;

	// Token: 0x040009BC RID: 2492
	private GameObject cutscene_characters;

	// Token: 0x040009BD RID: 2493
	private List<CutsceneDialogue> cutsceneDialogueList;

	// Token: 0x040009BE RID: 2494
	private int currentDialogueIndex;

	// Token: 0x040009BF RID: 2495
	private GameObject loadingMask;
}
