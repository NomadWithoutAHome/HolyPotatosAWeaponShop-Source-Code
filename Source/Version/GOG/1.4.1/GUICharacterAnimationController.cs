using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUICharacterAnimationController : MonoBehaviour
{
	private Game game;

	private GUIGridController gridController;

	private TooltipTextScript smithInfoScript;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private AudioController audioController;

	private GUIAnimationClickController animClickCtr;

	private Dictionary<string, GameObject> charactersList;

	private GameObject avatarObject;

	private GameObject dogObject;

	private GameObject Panel_Characters;

	private bool isPaused;

	private bool isNewDay;

	private bool startGameTimer;

	private float spdMultiplier;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gridController = GameObject.Find("GUIGridController").GetComponent<GUIGridController>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		smithInfoScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		animClickCtr = GameObject.Find("GUIAnimationClickController").GetComponent<GUIAnimationClickController>();
		Panel_Characters = GameObject.Find("Panel_Characters");
		charactersList = new Dictionary<string, GameObject>();
		avatarObject = null;
		dogObject = null;
		isPaused = false;
		isNewDay = false;
		startGameTimer = false;
		spdMultiplier = 1f;
	}

	private void Update()
	{
		if (viewController != null && !viewController.getIsPaused() && viewController.getGameStarted())
		{
			handleInput();
		}
	}

	private void handleInput()
	{
		if (avatarObject != null && (Input.GetKey(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && Input.GetKeyDown(KeyCode.LeftBracket))
		{
			animClickCtr.processClick(avatarObject);
		}
		else if (dogObject != null && (Input.GetKey(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && Input.GetKeyDown(KeyCode.RightBracket))
		{
			animClickCtr.processClick(dogObject);
		}
	}

	public void spawnCharacters(bool startTimerAfter = false, bool isAutosaveLoad = false)
	{
		startGameTimer = startTimerAfter;
		List<Smith> smithList = game.getPlayer().getSmithList();
		if (charactersList.Count > 1)
		{
			Dictionary<string, GameObject>.ValueCollection values = charactersList.Values;
			foreach (GameObject item in values)
			{
				commonScreenObject.destroyPrefabImmediate(item);
			}
			charactersList.Clear();
		}
		StartCoroutine(startSpawn(smithList, isAutosaveLoad));
	}

	private IEnumerator startSpawn(List<Smith> smithList, bool isAutosaveLoad)
	{
		yield return new WaitForSeconds(1f);
		foreach (Smith smith in smithList)
		{
			if (smith.checkSmithInShop())
			{
				smithList.IndexOf(smith);
				GameObject gameObject = commonScreenObject.createPrefab(Panel_Characters, smith.getSmithName(), "Prefab/Character/character0", Vector3.zero, Vector3.one, Vector3.zero);
				RuntimeAnimatorController runtimeAnimatorController = Resources.Load("Animation/Smith/" + smith.getImage()) as RuntimeAnimatorController;
				gameObject.GetComponentInChildren<Animator>().runtimeAnimatorController = runtimeAnimatorController;
				commonScreenObject.findChild(gameObject, "spriteAnimator").gameObject.name = "Smith_" + smith.getSmithRefId();
				charactersList.Add(smith.getSmithRefId(), gameObject);
				gameObject.GetComponent<GUIPathController>().checkState(smith);
				gameObject.GetComponent<GUIPathController>().setSpdMultiplier(spdMultiplier);
				if (isPaused)
				{
					gameObject.GetComponent<GUIPathController>().pauseCharacter();
				}
				else
				{
					audioController.playSmithEnterAudio();
				}
			}
		}
		spawnAvatar();
		spawnDog();
		viewController.hideTransparentMask();
		if (isAutosaveLoad)
		{
			GameObject.Find("ShopViewController").GetComponent<ShopViewController>().setIsAutosaveLoad();
		}
		if (startGameTimer)
		{
			if (game.getPlayer().getCurrentObjectiveString() != string.Empty)
			{
				viewController.showProjectProgress();
			}
			GameObject.Find("ShopViewController").GetComponent<ShopViewController>().setGameState(GameState.GameStateIdle);
			GameObject.Find("ShopViewController").GetComponent<ShopViewController>().startGameTimer();
			GameObject.Find("NGUICameraScene").GetComponent<GUISceneCameraController>().setCameraEnabled(aCameraEnabled: true);
			viewController.setGameStarted(started: true);
			if (!viewController.getHasPopup())
			{
				viewController.resumeEverything();
			}
		}
	}

	public void spawnCharacter(string aRefID)
	{
		GameData gameData = game.getGameData();
		Smith smithByRefId = gameData.getSmithByRefId(aRefID);
		GameObject gameObject = commonScreenObject.createPrefab(Panel_Characters, smithByRefId.getSmithName(), "Prefab/Character/character0", Vector3.zero, Vector3.one, Vector3.zero);
		RuntimeAnimatorController runtimeAnimatorController = Resources.Load("Animation/Smith/" + smithByRefId.getImage()) as RuntimeAnimatorController;
		gameObject.GetComponentInChildren<Animator>().runtimeAnimatorController = runtimeAnimatorController;
		commonScreenObject.findChild(gameObject, "spriteAnimator").gameObject.name = "Smith_" + smithByRefId.getSmithRefId();
		charactersList.Add(smithByRefId.getSmithRefId(), gameObject);
		gameObject.GetComponent<GUIPathController>().checkState(smithByRefId);
		gameObject.GetComponent<GUIPathController>().setSpdMultiplier(spdMultiplier);
		if (isPaused)
		{
			gameObject.GetComponent<GUIPathController>().pauseCharacter();
		}
		else
		{
			audioController.playSmithEnterAudio();
		}
	}

	public void spawnCharacterFrmheaven(string aRefID)
	{
		GameData gameData = game.getGameData();
		Smith smithByRefId = gameData.getSmithByRefId(aRefID);
		GameObject gameObject = commonScreenObject.createPrefab(Panel_Characters, smithByRefId.getSmithName(), "Prefab/Character/character0", Vector3.zero, Vector3.one, Vector3.zero);
		RuntimeAnimatorController runtimeAnimatorController = Resources.Load("Animation/Smith/" + smithByRefId.getImage()) as RuntimeAnimatorController;
		gameObject.GetComponentInChildren<Animator>().runtimeAnimatorController = runtimeAnimatorController;
		commonScreenObject.findChild(gameObject, "spriteAnimator").gameObject.name = "Smith_" + smithByRefId.getSmithRefId();
		charactersList.Add(smithByRefId.getSmithRefId(), gameObject);
		gameObject.GetComponent<GUIPathController>().checkState(smithByRefId, frmHeaven: true);
		gameObject.GetComponent<GUIPathController>().setSpdMultiplier(spdMultiplier);
	}

	public void spawnDog()
	{
		if (game.getPlayer().checkHasDog())
		{
			dogObject = commonScreenObject.createPrefab(Panel_Characters, "Dog", "Prefab/Character/dog0", Vector3.zero, Vector3.one, Vector3.zero);
			dogObject.GetComponent<GUIDogPathController>().checkState();
			if (isPaused)
			{
				dogObject.GetComponent<GUIDogPathController>().pauseCharacter();
			}
			dogObject.GetComponent<GUIDogPathController>().setSpdMultiplier(spdMultiplier);
		}
	}

	public void makeDogBark()
	{
		dogObject.GetComponent<GUIDogPathController>().makeDogBark();
	}

	public void spawnAvatar()
	{
		if (!game.getPlayer().checkHasAvatar())
		{
			return;
		}
		avatarObject = commonScreenObject.createPrefab(Panel_Characters, "Avatar", "Prefab/Character/avatar0", Vector3.zero, Vector3.one, Vector3.zero);
		if (avatarObject != null)
		{
			avatarObject.GetComponent<GUIAvatarPathController>().checkState();
			avatarObject.GetComponent<GUIAvatarPathController>().setSpdMultiplier(spdMultiplier);
			if (isPaused)
			{
				avatarObject.GetComponent<GUIAvatarPathController>().pauseCharacter();
			}
		}
	}

	public void checkCharacter()
	{
		if (charactersList.Count == 0 && !isNewDay && game.getPlayer().checkDayEnd())
		{
			charactersList.Clear();
			isNewDay = true;
			StartCoroutine("startNewDay");
		}
	}

	private IEnumerator startNewDay()
	{
		if (!viewController.getIsPaused())
		{
			yield return new WaitForSeconds(1f);
			GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>().startNewDay();
			isNewDay = false;
		}
	}

	public void unassignDeleteCharacter(string aRefID)
	{
		if (charactersList.ContainsKey(aRefID))
		{
			GameObject gameObject = charactersList[aRefID];
			gameObject.GetComponentInChildren<GUIPathController>().unassignSmith();
			charactersList.Remove(aRefID);
			commonScreenObject.destroyPrefab(gameObject);
		}
	}

	public void deleteCharacter(string aRefID, bool check = true)
	{
		if (charactersList.ContainsKey(aRefID))
		{
			GameObject aObj = charactersList[aRefID];
			charactersList.Remove(aRefID);
			commonScreenObject.destroyPrefab(aObj);
		}
		if (check)
		{
			checkCharacter();
		}
	}

	public void resetCharacters()
	{
		foreach (GameObject value in charactersList.Values)
		{
			commonScreenObject.destroyPrefabImmediate(value);
		}
		charactersList.Clear();
		commonScreenObject.destroyPrefabImmediate(avatarObject);
		commonScreenObject.destroyPrefabImmediate(dogObject);
	}

	public void showUpdateBubbleDisplay(Smith aSmith)
	{
		if (charactersList.ContainsKey(aSmith.getSmithRefId()))
		{
			GameObject gameObject = charactersList[aSmith.getSmithRefId()];
			gameObject.GetComponentInChildren<GUIPathController>().popLvlUp();
		}
	}

	public void showStartForgeBubble(Smith aSmith)
	{
		if (charactersList.ContainsKey(aSmith.getSmithRefId()))
		{
			GameObject gameObject = charactersList[aSmith.getSmithRefId()];
			if (!gameObject.GetComponentInChildren<GUIPathController>().getIsMoving())
			{
				gameObject.GetComponentInChildren<GUIPathController>().doBubblePop("START_FORGE", string.Empty);
			}
		}
	}

	public void showLevelUpBubble(Smith aSmith)
	{
		if (charactersList.ContainsKey(aSmith.getSmithRefId()))
		{
			GameObject gameObject = charactersList[aSmith.getSmithRefId()];
			if (!gameObject.GetComponentInChildren<GUIPathController>().getIsMoving())
			{
				gameObject.GetComponentInChildren<GUIPathController>().doBubblePop("LEVEL_UP", string.Empty);
			}
		}
	}

	public void showRandomTextBubble(Smith aSmith)
	{
		if (!charactersList.ContainsKey(aSmith.getSmithRefId()))
		{
			return;
		}
		GameObject gameObject = charactersList[aSmith.getSmithRefId()];
		if (gameObject.GetComponentInChildren<GUIPathController>().getIsMoving())
		{
			return;
		}
		GameData gameData = game.getGameData();
		string text = string.Empty;
		List<string> list = new List<string>();
		if (game.getPlayer().getCurrentProject().getProjectId() != string.Empty)
		{
			switch (aSmith.getCurrentStation())
			{
			case SmithStation.SmithStationDesign:
				list.Add(gameData.getRandomTextBySetRefId("randomDesignText"));
				break;
			case SmithStation.SmithStationCraft:
				list.Add(gameData.getRandomTextBySetRefId("randomCraftText"));
				break;
			case SmithStation.SmithStationPolish:
				list.Add(gameData.getRandomTextBySetRefId("randomPolishText"));
				break;
			case SmithStation.SmithStationEnchant:
				list.Add(gameData.getRandomTextBySetRefId("randomEnchantText"));
				break;
			}
		}
		string randomTextBySetRefId = gameData.getRandomTextBySetRefId("smith" + aSmith.getSmithRefId());
		if (randomTextBySetRefId != null && randomTextBySetRefId != string.Empty)
		{
			list.Add(randomTextBySetRefId);
		}
		SmithMood moodState = aSmith.getMoodState();
		string text2 = string.Empty;
		switch (moodState)
		{
		case SmithMood.SmithMoodVeryHappy:
			text2 = gameData.getRandomTextBySetRefId("randomVeryHappyText");
			break;
		case SmithMood.SmithMoodHappy:
			text2 = gameData.getRandomTextBySetRefId("randomHappyText");
			break;
		case SmithMood.SmithMoodNormal:
			text2 = gameData.getRandomTextBySetRefId("randomNormalText");
			break;
		case SmithMood.SmithMoodSad:
			text2 = gameData.getRandomTextBySetRefId("randomSadText");
			break;
		case SmithMood.SmithMoodVerySad:
			text2 = gameData.getRandomTextBySetRefId("randomVerySadText");
			break;
		}
		if (text2 != null && text2 != string.Empty)
		{
			list.Add(text2);
		}
		list.Add("MOOD_ICON");
		if (list.Count > 0)
		{
			int index = Random.Range(0, list.Count);
			text = list[index];
			if (list[index] == "MOOD_ICON")
			{
				showMoodBubble(aSmith);
				text = string.Empty;
			}
			else
			{
				text = list[index];
			}
		}
		if (text != string.Empty)
		{
			gameObject.GetComponentInChildren<GUIPathController>().doBubblePop("RANDOM", text);
		}
	}

	public void showTextBubble(Smith aSmith, string text)
	{
		if (charactersList.ContainsKey(aSmith.getSmithRefId()))
		{
			GameObject gameObject = charactersList[aSmith.getSmithRefId()];
			if (!gameObject.GetComponentInChildren<GUIPathController>().getIsMoving())
			{
				gameObject.GetComponentInChildren<GUIPathController>().popBubbleText(text);
			}
		}
	}

	public void showActionBubble(Smith aSmith, string actionImage)
	{
		if (charactersList.ContainsKey(aSmith.getSmithRefId()))
		{
			GameObject gameObject = charactersList[aSmith.getSmithRefId()];
			if (!gameObject.GetComponentInChildren<GUIPathController>().getIsMoving())
			{
				gameObject.GetComponentInChildren<GUIPathController>().doBubblePop("STATUS", actionImage);
			}
		}
	}

	public void showMoodBubble(Smith aSmith)
	{
		if (!charactersList.ContainsKey(aSmith.getSmithRefId()))
		{
			return;
		}
		GameObject gameObject = charactersList[aSmith.getSmithRefId()];
		if (!gameObject.GetComponentInChildren<GUIPathController>().getIsMoving())
		{
			SmithMood moodState = aSmith.getMoodState();
			string text = string.Empty;
			switch (moodState)
			{
			case SmithMood.SmithMoodVeryHappy:
				text = "Mood-elated";
				break;
			case SmithMood.SmithMoodHappy:
				text = "Mood-happy";
				break;
			case SmithMood.SmithMoodNormal:
				text = "Mood-neutral";
				break;
			case SmithMood.SmithMoodSad:
				text = "Mood-stressed";
				break;
			case SmithMood.SmithMoodVerySad:
				text = "Mood-depressed";
				break;
			}
			if (text != string.Empty)
			{
				gameObject.GetComponentInChildren<GUIPathController>().doBubblePop("MOOD_CHANGE", text);
			}
		}
	}

	public void showProjectStats(Smith aSmith, int value, string type)
	{
		if (charactersList.ContainsKey(aSmith.getSmithRefId()))
		{
			charactersList[aSmith.getSmithRefId()].GetComponentInChildren<GUIPathController>().popStat(value, type);
		}
	}

	public void hireSmith(Smith aSmith)
	{
		GameObject gameObject = commonScreenObject.createPrefab(Panel_Characters, aSmith.getSmithName(), "Prefab/Character/character0", Vector3.zero, Vector3.one, Vector3.zero);
		RuntimeAnimatorController runtimeAnimatorController = Resources.Load("Animation/Smith/" + aSmith.getImage()) as RuntimeAnimatorController;
		gameObject.GetComponentInChildren<Animator>().runtimeAnimatorController = runtimeAnimatorController;
		commonScreenObject.findChild(gameObject, "spriteAnimator").gameObject.name = "Smith_" + aSmith.getSmithRefId();
		gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
		gameObject.GetComponent<GUIPathController>().checkState(aSmith);
		gameObject.GetComponent<GUIPathController>().setSpdMultiplier(spdMultiplier);
		charactersList.Add(aSmith.getSmithRefId(), gameObject);
	}

	public void fireSmith(Smith aSmith)
	{
		if (charactersList.ContainsKey(aSmith.getSmithRefId()))
		{
			charactersList[aSmith.getSmithRefId()].GetComponentInChildren<GUIPathController>().setActionRefID("FIRED");
		}
		refreshSmithStationPoint();
	}

	public void fireSmiths(List<Smith> aSmithList)
	{
		foreach (Smith aSmith in aSmithList)
		{
			if (charactersList.ContainsKey(aSmith.getSmithRefId()))
			{
				SmithAction action = new SmithAction("FIRED", string.Empty, SmithActionState.SmithActionStateFired, aWorkAllow: false, aIdleAllow: false, string.Empty, 0f, 0, 0, 0, StatEffect.StatEffectNothing, 0f, 0f, string.Empty, string.Empty, 0, string.Empty);
				aSmith.setSmithAction(action, -1);
			}
		}
	}

	public void refreshSmithStationPoint()
	{
		Dictionary<string, GameObject>.ValueCollection values = charactersList.Values;
		foreach (GameObject item in values)
		{
			item.GetComponentInChildren<GUIPathController>().refreshStationPoint();
		}
	}

	public GameObject getSmithObject(Smith aSmith)
	{
		if (charactersList.ContainsKey(aSmith.getSmithRefId()))
		{
			return charactersList[aSmith.getSmithRefId()];
		}
		return null;
	}

	public GameObject getAvatarObject()
	{
		return avatarObject;
	}

	public GUIAvatarPathController getAvatarController()
	{
		if (avatarObject != null)
		{
			return avatarObject.GetComponent<GUIAvatarPathController>();
		}
		return null;
	}

	public GameObject getDogObject()
	{
		return dogObject;
	}

	public Dictionary<string, GameObject> getCharactersList()
	{
		return charactersList;
	}

	public void enableCollider()
	{
		Dictionary<string, GameObject>.ValueCollection values = charactersList.Values;
		foreach (GameObject item in values)
		{
			if (item != null)
			{
				item.GetComponentInChildren<CapsuleCollider>().enabled = true;
			}
		}
		if (avatarObject != null)
		{
			avatarObject.GetComponentInChildren<BoxCollider>().enabled = true;
		}
		if (dogObject != null)
		{
			dogObject.GetComponentInChildren<BoxCollider>().enabled = true;
		}
	}

	public void disableCollider()
	{
		Dictionary<string, GameObject>.ValueCollection values = charactersList.Values;
		foreach (GameObject item in values)
		{
			if (item != null)
			{
				item.GetComponentInChildren<CapsuleCollider>().enabled = false;
			}
		}
		if (avatarObject != null)
		{
			avatarObject.GetComponentInChildren<BoxCollider>().enabled = false;
		}
		if (dogObject != null)
		{
			dogObject.GetComponentInChildren<BoxCollider>().enabled = false;
		}
	}

	public void changeLayer(Smith aSmith, bool bringUp, bool animatorEnabled = true)
	{
		foreach (KeyValuePair<string, GameObject> characters in charactersList)
		{
			if (characters.Value.GetComponent<GUIPathController>().isResearching())
			{
				continue;
			}
			SpriteRenderer[] componentsInChildren = characters.Value.GetComponentsInChildren<SpriteRenderer>();
			SpriteRenderer[] array = componentsInChildren;
			foreach (SpriteRenderer spriteRenderer in array)
			{
				if (bringUp)
				{
					spriteRenderer.sortingLayerName = "Blackmask";
				}
				else
				{
					spriteRenderer.sortingLayerName = "Character";
				}
				string[] array2 = spriteRenderer.gameObject.name.Split('_');
				if (array2.Length <= 1 || !(array2[0] == "Smith"))
				{
					continue;
				}
				if ((bringUp && characters.Key == aSmith.getSmithRefId()) || !bringUp)
				{
					spriteRenderer.color = Color.white;
					if (animatorEnabled)
					{
						spriteRenderer.GetComponent<Animator>().enabled = true;
					}
				}
				else
				{
					spriteRenderer.GetComponent<Animator>().enabled = false;
					spriteRenderer.color = Color.gray;
				}
			}
		}
	}

	public void changeAvatarDogLayer(bool bringUp, bool dog = false)
	{
		if (avatarObject != null)
		{
			SpriteRenderer component = commonScreenObject.findChild(avatarObject, "spriteAnimator").GetComponent<SpriteRenderer>();
			if (bringUp)
			{
				component.sortingLayerName = "Blackmask";
				if (dog)
				{
					component.GetComponent<Animator>().enabled = false;
					component.color = Color.gray;
				}
				else
				{
					component.color = Color.white;
					component.GetComponent<Animator>().enabled = true;
				}
			}
			else
			{
				component.sortingLayerName = "Character";
				component.color = Color.white;
				component.GetComponent<Animator>().enabled = true;
			}
		}
		if (!(dogObject != null))
		{
			return;
		}
		SpriteRenderer component2 = commonScreenObject.findChild(dogObject, "spriteAnimator").GetComponent<SpriteRenderer>();
		if (bringUp)
		{
			component2.sortingLayerName = "Blackmask";
			if (dog)
			{
				component2.color = Color.white;
				component2.GetComponent<Animator>().enabled = true;
			}
			else
			{
				component2.GetComponent<Animator>().enabled = false;
				component2.color = Color.gray;
			}
		}
		else
		{
			component2.sortingLayerName = "Character";
			component2.color = Color.white;
			component2.GetComponent<Animator>().enabled = true;
		}
	}

	public void pauseAllCharacters()
	{
		isPaused = true;
		if (charactersList != null && charactersList.Count > 0)
		{
			Dictionary<string, GameObject>.ValueCollection values = charactersList.Values;
			foreach (GameObject item in values)
			{
				if (item != null)
				{
					item.GetComponent<GUIPathController>().pauseCharacter();
				}
			}
		}
		if (avatarObject != null)
		{
			avatarObject.GetComponent<GUIAvatarPathController>().pauseCharacter();
		}
		if (dogObject != null)
		{
			dogObject.GetComponent<GUIDogPathController>().pauseCharacter();
		}
	}

	public void resumeAllCharacters()
	{
		isPaused = false;
		if (charactersList != null && charactersList.Count > 0)
		{
			Dictionary<string, GameObject>.ValueCollection values = charactersList.Values;
			foreach (GameObject item in values)
			{
				if (item != null)
				{
					item.GetComponentInChildren<Animator>().enabled = true;
					item.GetComponent<GUIPathController>().cancelCurrentAnimation();
					item.GetComponent<GUIPathController>().resumeCharacter();
				}
			}
		}
		if (avatarObject != null)
		{
			avatarObject.GetComponent<GUIAvatarPathController>().resumeCharacter();
			avatarObject.GetComponent<GUIAvatarPathController>().cancelCurrentAnimation();
		}
		if (dogObject != null)
		{
			dogObject.GetComponent<GUIDogPathController>().resumeCharacter();
			dogObject.GetComponent<GUIDogPathController>().cancelCurrentAnimation();
		}
	}

	public void refreshCharactersPos()
	{
		Dictionary<string, GameObject>.ValueCollection values = charactersList.Values;
		foreach (GameObject item in values)
		{
			if (item != null)
			{
				item.GetComponent<GUIPathController>().refreshStationPoint();
			}
		}
		if (avatarObject != null)
		{
			avatarObject.GetComponent<GUIAvatarPathController>().refreshStationPoint();
		}
		if (dogObject != null)
		{
			dogObject.GetComponent<GUIDogPathController>().refreshStationPoint();
		}
	}

	public void setSpdMultiplier(float aMult)
	{
		spdMultiplier = aMult;
		Dictionary<string, GameObject>.ValueCollection values = charactersList.Values;
		foreach (GameObject item in values)
		{
			if (item != null)
			{
				item.GetComponent<GUIPathController>().setSpdMultiplier(aMult);
			}
		}
		if (avatarObject != null)
		{
			avatarObject.GetComponent<GUIAvatarPathController>().setSpdMultiplier(aMult);
		}
		if (dogObject != null)
		{
			dogObject.GetComponent<GUIDogPathController>().setSpdMultiplier(aMult);
		}
	}
}
