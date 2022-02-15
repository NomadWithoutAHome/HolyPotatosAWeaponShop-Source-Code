using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUITutorialController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private StationController stationController;

	private Transform tutorialTransform;

	private UILabel titleLabel;

	private UILabel textLabel;

	private BoxCollider textBoxCollider;

	private UITexture imageTexture;

	private UIButton skipButton;

	private UIButton nextButton;

	private bool showNextButton;

	private bool hasNext;

	private bool useTutorialMask;

	private bool doPause;

	private string currentSetRefId;

	private Dictionary<int, Tutorial> currentSet;

	private Tutorial currentTutorial;

	private int currentListIndex;

	private bool lineIsAnimating;

	private string lineText;

	private string displayLineText;

	private int lineDisplayChar;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		stationController = GameObject.Find("StationController").GetComponent<StationController>();
		tutorialTransform = commonScreenObject.findChild(base.gameObject, "TutorialPos").transform;
		textLabel = commonScreenObject.findChild(tutorialTransform.gameObject, "TutorialText_label").GetComponent<UILabel>();
		textBoxCollider = commonScreenObject.findChild(textLabel.gameObject, "TutorialText_bg").GetComponent<BoxCollider>();
		titleLabel = commonScreenObject.findChild(textLabel.gameObject, "TutorialTitle_label").GetComponent<UILabel>();
		imageTexture = commonScreenObject.findChild(tutorialTransform.gameObject, "TutorialImage_texture").GetComponent<UITexture>();
		imageTexture.alpha = 0f;
		skipButton = commonScreenObject.findChild(tutorialTransform.gameObject, "Skip_button").GetComponent<UIButton>();
		nextButton = commonScreenObject.findChild(tutorialTransform.gameObject, "Next_button").GetComponent<UIButton>();
		showNextButton = true;
		hasNext = true;
		useTutorialMask = false;
		doPause = false;
		currentSetRefId = string.Empty;
		currentSet = new Dictionary<int, Tutorial>();
		currentTutorial = new Tutorial();
		currentListIndex = 1;
		lineIsAnimating = false;
		lineText = string.Empty;
		displayLineText = string.Empty;
		lineDisplayChar = 0;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "Next_button":
			nextTutorial();
			break;
		case "TutorialText_bg":
			if (lineIsAnimating)
			{
				forceTypewriterEnd();
			}
			break;
		case "Skip_button":
			audioController.stopTextTypeAudio();
			finishTutorial();
			break;
		}
	}

	public void nextTutorial()
	{
		currentListIndex++;
		StartCoroutine(showCurrentTutorial());
	}

	public void setReference(string aSetRefId)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		currentSetRefId = aSetRefId;
		currentSet = gameData.getTutorialListBySetRefId(aSetRefId);
		currentListIndex = 1;
		skipButton.isEnabled = false;
		StartCoroutine(showCurrentTutorial());
	}

	public IEnumerator showCurrentTutorial()
	{
		if (currentSet != null && currentSet.ContainsKey(currentListIndex))
		{
			currentTutorial = currentSet[currentListIndex];
			if (currentSet.ContainsKey(currentListIndex + 1))
			{
				hasNext = true;
			}
			doPause = false;
			float delay = doTutorialHandling(currentTutorial.getTutorialRefId());
			if (delay > 0f)
			{
				viewController.showTutorialMaskPopUp();
				textLabel.alpha = 0f;
				nextButton.isEnabled = false;
				tutorialTransform.localPosition = new Vector3(5000f, 5000f, 0f);
				yield return new WaitForSeconds(delay);
				textLabel.text = " ";
				textLabel.alpha = 1f;
			}
			if (useTutorialMask)
			{
				viewController.showTutorialMaskPopUp();
			}
			else
			{
				viewController.hideTutorialMaskPopUp();
			}
			if (doPause)
			{
				viewController.pauseEverything(GameState.GameStatePopEvent);
			}
			titleLabel.text = currentTutorial.getTutorialTitle();
			string text = currentTutorial.getTutorialText();
			lineText = replaceText(text);
			displayLineText = string.Empty;
			lineDisplayChar = 0;
			lineIsAnimating = true;
			textBoxCollider.enabled = true;
			string image = currentTutorial.getTutorialTexturePath();
			if (image != string.Empty)
			{
				imageTexture.alpha = 1f;
				imageTexture.mainTexture = commonScreenObject.loadTexture(image);
			}
			else
			{
				imageTexture.alpha = 0f;
			}
			float tutXPos = currentTutorial.getTutorialXPos();
			float tutYPos = currentTutorial.getTutorialYPos();
			tutorialTransform.localPosition = new Vector3(tutXPos, tutYPos, 0f);
			updateButton();
			if (lineText != string.Empty)
			{
				audioController.playTextTypeAudio();
				StartCoroutine(typewriterText());
			}
			else
			{
				forceTypewriterEnd();
			}
		}
		else
		{
			finishTutorial();
		}
	}

	private void finishTutorial()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		player.setCompletedTutorialIndex(gameData.getTutorialSetOrderIndex(currentSetRefId));
		if (currentSetRefId == "SELL_RESULT")
		{
			GameObject.Find("Panel_TopLeftMenu").GetComponent<GUITopMenuNewController>().setTopMenuTutorialState();
		}
		if (currentSetRefId == "SELL1" || currentSetRefId == "SELL2" || currentSetRefId == "JOB_CLASS" || currentSetRefId == "TRAINING")
		{
			viewController.closeTutorialPopup(toResume: false);
		}
		else
		{
			if (currentSetRefId == "SELL3")
			{
				GameObject gameObject = GameObject.Find("Panel_Speed");
				if (gameObject != null)
				{
					commonScreenObject.tweenPosition(gameObject.GetComponent<TweenPosition>(), gameObject.transform.localPosition, Vector3.zero, 0.75f, null, string.Empty);
				}
			}
			viewController.closeTutorialPopup(toResume: true);
		}
		List<GameLock> gameLockListByTypeAndRefId = gameData.getGameLockListByTypeAndRefId("TUTORIAL", currentSetRefId);
		foreach (GameLock item in gameLockListByTypeAndRefId)
		{
			shopMenuController.unlockFeature(item.getLockFeature());
		}
	}

	private string replaceText(string aText)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		aText = aText.Replace("[playerName]", player.getPlayerName());
		aText = aText.Replace("[dogName]", player.getDogName());
		aText = aText.Replace("[initialGold]", 500.ToString());
		aText = aText.Replace("[highlightStart]", "[D484F5]");
		aText = aText.Replace("[highlightEnd]", "[-]");
		aText = aText.Replace("[redStart]", "[FF4842]");
		aText = aText.Replace("[redEnd]", "[-]");
		aText = aText.Replace("[greenStart]", "[56AE59]");
		aText = aText.Replace("[greenEnd]", "[-]");
		aText = aText.Replace("[blueStart]", "[00AAC7]");
		aText = aText.Replace("[blueEnd]", "[-]");
		aText = aText.Replace("[yellowStart]", "[FFD84A]");
		aText = aText.Replace("[yellowEnd]", "[-]");
		return aText;
	}

	public float doTutorialHandling(string aRefId)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		float result = 0f;
		useTutorialMask = false;
		switch (aRefId)
		{
		case "10001":
		{
			viewController.pauseEverything(GameState.GameStatePopEvent);
			GameObject gameObject16 = GameObject.Find("Panel_TopLeftMenu");
			if (gameObject16 != null)
			{
				audioController.playSlideEnterAudio();
				viewController.moveHUD(gameObject16, MoveDirection.Right, moveIn: true, 0.75f, null, string.Empty);
			}
			showNextButton = true;
			break;
		}
		case "10003":
		{
			viewController.resumeEverything();
			GameObject gameObject9 = GameObject.Find("Calendar");
			if (gameObject9 != null)
			{
				audioController.playSlideEnterAudio();
				viewController.moveHUD(gameObject9, MoveDirection.Left, moveIn: true, 0.75f, null, string.Empty);
			}
			showNextButton = true;
			break;
		}
		case "10004":
		{
			viewController.pauseEverything(GameState.GameStatePopEvent);
			GUICharacterAnimationController component2 = GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>();
			component2.spawnCharacterFrmheaven("10001");
			result = 2.5f;
			showNextButton = false;
			break;
		}
		case "10007":
			stationController.changeLayerDesign(bringUp: true);
			stationController.activateDesignCollider(exchangeable: true);
			showNextButton = false;
			break;
		case "10008":
		{
			Smith smithByRefID = player.getSmithByRefID("10002");
			smithByRefID.setAssignedRole(SmithStation.SmithStationCraft);
			player.updateSmithStations();
			stationController.assignSmithStations();
			GUICharacterAnimationController component = GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>();
			component.spawnCharacterFrmheaven("10002");
			component.spawnCharacterFrmheaven("10003");
			result = 2.5f;
			showNextButton = true;
			useTutorialMask = true;
			doPause = true;
			break;
		}
		case "10009":
		{
			GameObject gameObject4 = GameObject.Find("Panel_SmithList");
			if (gameObject4 != null)
			{
				audioController.playSlideEnterAudio();
				viewController.moveHUD(gameObject4, MoveDirection.Right, moveIn: true, 0.75f, null, string.Empty);
			}
			showNextButton = true;
			useTutorialMask = true;
			break;
		}
		case "10011":
		{
			GameObject gameObject5 = GameObject.Find("Objective");
			if (gameObject5 != null)
			{
				viewController.moveHUD(gameObject5, MoveDirection.Left, moveIn: true, 0.75f, null, string.Empty);
			}
			GameObject gameObject6 = GameObject.Find("Panel_BottomMenu");
			if (gameObject6 != null)
			{
				viewController.moveHUD(gameObject6, MoveDirection.Up, moveIn: true, 0.75f, null, string.Empty);
				gameObject6.GetComponent<UIPanel>().depth = 32;
				gameObject6.GetComponent<BottomMenuController>().setTutorialState("INTRO_FORGE");
			}
			showNextButton = false;
			useTutorialMask = true;
			break;
		}
		case "10012":
		{
			GameObject gameObject17 = GameObject.Find("Panel_BottomMenu");
			if (gameObject17 != null)
			{
				gameObject17.GetComponent<UIPanel>().depth = 1;
				gameObject17.GetComponent<BottomMenuController>().setTutorialState(string.Empty);
			}
			showNextButton = false;
			useTutorialMask = false;
			break;
		}
		case "20001":
			showNextButton = true;
			useTutorialMask = true;
			break;
		case "30001":
		{
			GameObject gameObject13 = GameObject.Find("Panel_BottomMenu");
			if (gameObject13 != null)
			{
				viewController.moveHUD(gameObject13, MoveDirection.Up, moveIn: true, 0.75f, null, string.Empty);
				gameObject13.GetComponent<UIPanel>().depth = 32;
				gameObject13.GetComponent<BottomMenuController>().setTutorialState("INTRO_MAP");
			}
			showNextButton = false;
			useTutorialMask = true;
			doPause = true;
			break;
		}
		case "30002":
			GameObject.Find("Panel_MapActivitySelect").GetComponent<GUIMapActivitySelectController>().setTutorialState("SELL");
			showNextButton = false;
			useTutorialMask = false;
			break;
		case "30003":
			showNextButton = false;
			useTutorialMask = false;
			break;
		case "30004":
			showNextButton = false;
			useTutorialMask = false;
			break;
		case "30005":
		{
			GameObject gameObject10 = GameObject.Find("Panel_BottomMenu");
			if (gameObject10 != null)
			{
				gameObject10.GetComponent<UIPanel>().depth = 1;
				gameObject10.GetComponent<BottomMenuController>().setTutorialState(string.Empty);
			}
			result = 2.5f;
			showNextButton = true;
			useTutorialMask = true;
			doPause = true;
			break;
		}
		case "30006":
		{
			GameObject gameObject3 = GameObject.Find("Panel_Whetsapp");
			if (gameObject3 != null)
			{
				audioController.playSlideEnterAudio();
				viewController.moveHUD(gameObject3, MoveDirection.Up, moveIn: true, 0.75f, null, string.Empty);
			}
			showNextButton = true;
			useTutorialMask = true;
			break;
		}
		case "30007":
			showNextButton = true;
			useTutorialMask = true;
			break;
		case "30008":
			showNextButton = true;
			useTutorialMask = true;
			break;
		case "40001":
		{
			GameObject gameObject14 = GameObject.Find("Panel_SmithList");
			if (gameObject14 != null)
			{
				gameObject14.GetComponent<UIPanel>().depth = 32;
				gameObject14.GetComponent<GUISmithListMenuController>().setOfferWeaponTutorialState();
			}
			GameObject gameObject15 = GameObject.Find("Panel_SmithInfo");
			if (gameObject15 != null)
			{
				gameObject15.GetComponent<UIPanel>().depth = 35;
			}
			showNextButton = false;
			useTutorialMask = true;
			break;
		}
		case "40002":
		{
			GameObject gameObject11 = GameObject.Find("Panel_SmithList");
			if (gameObject11 != null)
			{
				gameObject11.GetComponent<UIPanel>().depth = 1;
				gameObject11.GetComponent<GUISmithListMenuController>().revertSmithListAfterTutorialState();
			}
			GameObject gameObject12 = GameObject.Find("Panel_SmithInfo");
			if (gameObject12 != null)
			{
				gameObject12.GetComponent<UIPanel>().depth = 20;
			}
			showNextButton = true;
			useTutorialMask = true;
			break;
		}
		case "40003":
			showNextButton = true;
			useTutorialMask = true;
			break;
		case "40004":
			showNextButton = false;
			useTutorialMask = false;
			break;
		case "40005":
			showNextButton = true;
			useTutorialMask = true;
			doPause = true;
			break;
		case "50001":
		{
			CommonAPI.debug("50001");
			GameObject gameObject7 = GameObject.Find("Panel_SmithList");
			if (gameObject7 != null)
			{
				gameObject7.GetComponent<UIPanel>().depth = 32;
				gameObject7.GetComponent<GUISmithListMenuController>().setSellResultTutorialState();
			}
			GameObject gameObject8 = GameObject.Find("Panel_SmithInfo");
			if (gameObject8 != null)
			{
				gameObject8.GetComponent<UIPanel>().depth = 35;
			}
			showNextButton = false;
			useTutorialMask = true;
			break;
		}
		case "50002":
		{
			CommonAPI.debug("50002");
			GameObject gameObject = GameObject.Find("Panel_SmithList");
			if (gameObject != null)
			{
				gameObject.GetComponent<UIPanel>().depth = 1;
				gameObject.GetComponent<GUISmithListMenuController>().revertSmithListAfterTutorialState();
			}
			GameObject gameObject2 = GameObject.Find("Panel_SmithInfo");
			if (gameObject2 != null)
			{
				gameObject2.GetComponent<UIPanel>().depth = 20;
			}
			showNextButton = false;
			useTutorialMask = false;
			break;
		}
		case "50003":
			stationController.changeLayer(bringUp: true);
			stationController.activateCollider(exchangeable: false);
			showNextButton = false;
			useTutorialMask = false;
			break;
		case "50004":
			result = 2.5f;
			showNextButton = true;
			useTutorialMask = true;
			doPause = true;
			break;
		case "60001":
			result = 0.5f;
			showNextButton = true;
			useTutorialMask = true;
			doPause = true;
			break;
		case "70001":
			showNextButton = true;
			useTutorialMask = true;
			break;
		case "70002":
			showNextButton = true;
			useTutorialMask = true;
			break;
		case "70003":
			showNextButton = true;
			useTutorialMask = true;
			break;
		case "80001":
			showNextButton = true;
			useTutorialMask = true;
			break;
		case "90001":
			showNextButton = true;
			useTutorialMask = true;
			doPause = true;
			break;
		case "90002":
			showNextButton = true;
			useTutorialMask = true;
			break;
		case "90003":
			showNextButton = true;
			useTutorialMask = true;
			break;
		case "90004":
			showNextButton = true;
			useTutorialMask = true;
			break;
		case "90005":
			showNextButton = true;
			useTutorialMask = true;
			break;
		case "90006":
			showNextButton = true;
			useTutorialMask = true;
			break;
		default:
			showNextButton = true;
			break;
		}
		return result;
	}

	private void doTutorialPositioning(string aRefId)
	{
		switch (aRefId)
		{
		case "40002":
		case "40003":
		case "40004":
			tutorialTransform.localPosition = new Vector3(240f, -268f, 0f);
			break;
		case "50002":
			tutorialTransform.localPosition = new Vector3(270f, -275f, 0f);
			tutorialTransform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
			break;
		}
	}

	private IEnumerator typewriterText()
	{
		while (lineIsAnimating)
		{
			if (lineText[lineDisplayChar] == '[')
			{
				int num = lineText.IndexOf(']', lineDisplayChar);
				string text = lineText.Substring(lineDisplayChar, num - lineDisplayChar + 1);
				displayLineText += text;
				lineDisplayChar += text.Length;
			}
			else
			{
				displayLineText += lineText[lineDisplayChar];
				lineDisplayChar++;
			}
			textLabel.text = displayLineText;
			if (lineDisplayChar >= lineText.Length)
			{
				lineIsAnimating = false;
				textBoxCollider.enabled = false;
				audioController.stopTextTypeAudio();
				updateButton();
			}
			yield return new WaitForSeconds(0.02f);
		}
	}

	private void forceTypewriterEnd()
	{
		displayLineText = lineText;
		lineDisplayChar = lineText.Length;
		textLabel.text = lineText;
		lineIsAnimating = false;
		textBoxCollider.enabled = false;
		audioController.stopTextTypeAudio();
		updateButton();
	}

	private void updateButton()
	{
		if (lineIsAnimating || !showNextButton)
		{
			nextButton.isEnabled = false;
			return;
		}
		GameData gameData = game.getGameData();
		if (hasNext)
		{
			nextButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral13");
		}
		else
		{
			nextButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral04");
		}
		nextButton.isEnabled = true;
	}

	public bool checkCurrentTutorial(string aCheckRefId)
	{
		if (currentTutorial.getTutorialRefId() == aCheckRefId)
		{
			return true;
		}
		return false;
	}
}
