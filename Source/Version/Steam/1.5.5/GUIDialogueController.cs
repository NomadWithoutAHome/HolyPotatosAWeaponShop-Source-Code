using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIDialogueController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private UITexture bgTexture;

	private UISprite textBoxBg;

	private UISprite nameLBg;

	private UILabel nameLLabel;

	private UISprite nameRBg;

	private UILabel nameRLabel;

	private UILabel speechLabel;

	private UILabel actionTextLabel;

	private UISprite textBoxArrow;

	private UIButton choice1Button;

	private UILabel choice1Label;

	private UIButton choice2Button;

	private UILabel choice2Label;

	private GameObject characterLObj;

	private UITexture characterLObjTexture;

	private string currentLAnim;

	private bool currentLFlip;

	private GameObject characterRObj;

	private UITexture characterRObjTexture;

	private string currentRAnim;

	private bool currentRFlip;

	private UIButton skipButton;

	private string setId;

	private Dictionary<string, DialogueNEW> dialogueList;

	private string currentLineRefId;

	private PopupType popupType;

	private bool lineIsAnimating;

	private string lineText;

	private string displayLineText;

	private int lineDisplayChar;

	private DialogueNEW currentLine;

	private string linePos;

	private Shader animShader;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		bgTexture = commonScreenObject.findChild(base.gameObject, "Panel_DialogueBg/Dialogue_bg").GetComponent<UITexture>();
		textBoxBg = commonScreenObject.findChild(base.gameObject, "Panel_DialogueTextbox/TextBox_bg").GetComponent<UISprite>();
		nameLBg = commonScreenObject.findChild(textBoxBg.gameObject, "NameL_bg").GetComponent<UISprite>();
		nameLLabel = commonScreenObject.findChild(nameLBg.gameObject, "NameL_label").GetComponent<UILabel>();
		nameRBg = commonScreenObject.findChild(textBoxBg.gameObject, "NameR_bg").GetComponent<UISprite>();
		nameRLabel = commonScreenObject.findChild(nameRBg.gameObject, "NameR_label").GetComponent<UILabel>();
		speechLabel = commonScreenObject.findChild(textBoxBg.gameObject, "TextBox_textLeft").GetComponent<UILabel>();
		actionTextLabel = commonScreenObject.findChild(textBoxBg.gameObject, "TextBox_textCentre").GetComponent<UILabel>();
		textBoxArrow = commonScreenObject.findChild(textBoxBg.gameObject, "TextBox_arrow").GetComponent<UISprite>();
		choice1Button = commonScreenObject.findChild(textBoxBg.gameObject, "Choice1_bg").GetComponent<UIButton>();
		choice1Label = commonScreenObject.findChild(textBoxBg.gameObject, "Choice1_bg/Choice1_label").GetComponent<UILabel>();
		choice2Button = commonScreenObject.findChild(textBoxBg.gameObject, "Choice2_bg").GetComponent<UIButton>();
		choice2Label = commonScreenObject.findChild(textBoxBg.gameObject, "Choice2_bg/Choice2_label").GetComponent<UILabel>();
		characterLObj = commonScreenObject.findChild(base.gameObject, "Panel_DialogueAnims/CharacterL").gameObject;
		characterLObjTexture = characterLObj.GetComponent<UITexture>();
		currentLAnim = string.Empty;
		currentLFlip = false;
		characterRObj = commonScreenObject.findChild(base.gameObject, "Panel_DialogueAnims/CharacterR").gameObject;
		characterRObjTexture = characterRObj.GetComponent<UITexture>();
		currentRAnim = string.Empty;
		currentRFlip = false;
		skipButton = commonScreenObject.findChild(base.gameObject, "Panel_DialogueTextbox/TextBox_bg/Skip_button").GetComponent<UIButton>();
		setId = string.Empty;
		dialogueList = new Dictionary<string, DialogueNEW>();
		currentLineRefId = string.Empty;
		popupType = PopupType.PopupTypeNothing;
		animShader = Resources.Load("Custom Shader/Alpha Blended - QuestSelect Hero") as Shader;
		lineIsAnimating = false;
		lineText = string.Empty;
		displayLineText = string.Empty;
		lineDisplayChar = 0;
		currentLine = new DialogueNEW();
		linePos = string.Empty;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "Choice1_bg":
			currentLineRefId = dialogueList[currentLineRefId].getDialogueChoice1NextRefId();
			showLine();
			break;
		case "Choice2_bg":
			currentLineRefId = dialogueList[currentLineRefId].getDialogueChoice2NextRefId();
			showLine();
			break;
		case "Skip_button":
			currentLineRefId = string.Empty;
			showLine();
			break;
		case "TextBox_bg":
			if (lineIsAnimating)
			{
				forceTypewriterEnd();
				break;
			}
			currentLineRefId = dialogueList[currentLineRefId].getDialogueNextRefId();
			showLine();
			break;
		}
	}

	public void setReference(string aSetId, Dictionary<string, DialogueNEW> aDialogueList, PopupType aType = PopupType.PopupTypeNothing)
	{
		setId = aSetId;
		dialogueList = aDialogueList;
		popupType = aType;
		CommonAPI.debug("popupType " + popupType);
		if (false || game.getGameData().checkDialogueCanSkip(setId))
		{
			skipButton.isEnabled = true;
		}
		else
		{
			skipButton.isEnabled = false;
		}
		currentLineRefId = setId + "01";
		textBoxBg.alpha = 0f;
		showLine();
	}

	public void showLine()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		if (currentLineRefId != string.Empty && dialogueList.ContainsKey(currentLineRefId))
		{
			currentLine = dialogueList[currentLineRefId];
			string text = currentLine.getDialogueName();
			if (text == "PLAYER_NAME")
			{
				text = player.getPlayerName();
			}
			if (text == "DOG_NAME")
			{
				text = player.getDogName();
			}
			lineText = doTextReplace(currentLine.getDialogueText());
			displayLineText = string.Empty;
			lineDisplayChar = 0;
			lineIsAnimating = true;
			switch (currentLine.getDialoguePosition())
			{
			case "LEFT":
				nameLBg.alpha = 1f;
				nameLLabel.text = text;
				nameRBg.alpha = 0f;
				nameRLabel.text = string.Empty;
				speechLabel.text = displayLineText;
				actionTextLabel.text = string.Empty;
				textBoxBg.alpha = 1f;
				linePos = "LEFT";
				if (!(currentLine.getDialogueImage() != string.Empty))
				{
					break;
				}
				if (currentLAnim != currentLine.getDialogueImage() || currentLFlip != currentLine.getDialogueFlip())
				{
					currentLAnim = currentLine.getDialogueImage();
					currentLFlip = currentLine.getDialogueFlip();
					if (currentLAnim != "CLEAR")
					{
						characterLObjTexture.mainTexture = commonScreenObject.loadTexture("Image/Dialogue/" + currentLAnim);
						characterLObjTexture.alpha = 1f;
						int w2 = (int)((float)characterLObjTexture.mainTexture.width * 0.7f);
						int h2 = (int)((float)characterLObjTexture.mainTexture.height * 0.7f);
						characterLObjTexture.SetDimensions(w2, h2);
						if (currentLFlip)
						{
							characterLObjTexture.transform.localScale = new Vector3(-1f, 1f, 1f);
						}
						else
						{
							characterLObjTexture.transform.localScale = Vector3.one;
						}
					}
					else
					{
						characterLObjTexture.alpha = 0f;
					}
				}
				if (currentLAnim != string.Empty && currentLAnim != "CLEAR")
				{
					characterLObjTexture.color = Color.white;
				}
				if (currentRAnim != string.Empty && currentRAnim != "CLEAR")
				{
					characterRObjTexture.color = Color.grey;
				}
				break;
			case "RIGHT":
				nameRBg.alpha = 1f;
				nameRLabel.text = text;
				nameLBg.alpha = 0f;
				nameLLabel.text = string.Empty;
				speechLabel.text = displayLineText;
				actionTextLabel.text = string.Empty;
				textBoxBg.alpha = 1f;
				linePos = "LEFT";
				if (!(currentLine.getDialogueImage() != string.Empty))
				{
					break;
				}
				if (currentRAnim != currentLine.getDialogueImage() || currentRFlip != currentLine.getDialogueFlip())
				{
					currentRAnim = currentLine.getDialogueImage();
					currentRFlip = currentLine.getDialogueFlip();
					if (currentRAnim != "CLEAR")
					{
						characterRObjTexture.mainTexture = commonScreenObject.loadTexture("Image/Dialogue/" + currentRAnim);
						characterRObjTexture.alpha = 1f;
						int w = (int)((float)characterRObjTexture.mainTexture.width * 0.7f);
						int h = (int)((float)characterRObjTexture.mainTexture.height * 0.7f);
						characterRObjTexture.SetDimensions(w, h);
						if (currentLine.getDialogueFlip())
						{
							characterRObjTexture.transform.localScale = new Vector3(-1f, 1f, 1f);
						}
						else
						{
							characterRObjTexture.transform.localScale = Vector3.one;
						}
					}
					else
					{
						characterRObjTexture.alpha = 0f;
					}
				}
				if (currentRAnim != string.Empty && currentRAnim != "CLEAR")
				{
					characterRObjTexture.color = Color.white;
				}
				if (currentLAnim != string.Empty && currentLAnim != "CLEAR")
				{
					characterLObjTexture.color = Color.grey;
				}
				break;
			case "MIDDLE":
				nameRBg.alpha = 0f;
				nameRLabel.text = string.Empty;
				nameLBg.alpha = 0f;
				nameLLabel.text = string.Empty;
				speechLabel.text = string.Empty;
				actionTextLabel.text = displayLineText;
				textBoxBg.alpha = 1f;
				linePos = "CENTER";
				if (currentRAnim != string.Empty && currentRAnim != "CLEAR")
				{
					characterLObjTexture.color = Color.grey;
				}
				if (currentLAnim != string.Empty && currentLAnim != "CLEAR")
				{
					characterLObjTexture.color = Color.grey;
				}
				break;
			}
			if (currentLine.getBackgroundTexture() == "CLEAR")
			{
				bgTexture.alpha = 0f;
			}
			else if (currentLine.getBackgroundTexture() != string.Empty)
			{
				bgTexture.mainTexture = commonScreenObject.loadTexture("Image/DialogueBG/" + currentLine.getBackgroundTexture());
				bgTexture.alpha = 1f;
			}
			string soundEffect = currentLine.getSoundEffect();
			if (soundEffect != string.Empty)
			{
				audioController.playDialogueAudio(soundEffect);
			}
			string bGM = currentLine.getBGM();
			if (bGM != string.Empty)
			{
				switch (bGM)
				{
				case "MUTE":
					audioController.switchBGM("SILENT");
					break;
				case "FADEOUT":
					audioController.changeBGM("SILENT");
					break;
				case "SEASON":
				{
					Season seasonByMonth = CommonAPI.getSeasonByMonth(player.getSeasonIndex());
					audioController.changeBGM(CommonAPI.getSeasonBGM(seasonByMonth));
					break;
				}
				default:
					audioController.changeBGM(bGM);
					break;
				}
			}
			textBoxArrow.alpha = 0f;
			choice1Button.isEnabled = false;
			choice2Button.isEnabled = false;
			textBoxBg.GetComponent<BoxCollider>().enabled = true;
			if (lineText != string.Empty)
			{
				audioController.playTextTypeAudio();
				StartCoroutine(typewriterText());
			}
			else
			{
				forceTypewriterEnd();
			}
			return;
		}
		audioController.stopTextTypeAudio();
		bool hideMask = true;
		bool doResume = true;
		bool flag = true;
		string text2 = "SEASON";
		switch (popupType)
		{
		case PopupType.PopupTypeLegendaryRequestAccept:
			viewController.showLegendaryRequest(player.getLastLegendaryHeroRequest());
			hideMask = false;
			doResume = false;
			break;
		case PopupType.PopupTypeLegendaryRequestSuccess:
			CommonAPI.debug("PopupTypeLegendaryRequestSuccess " + player.getLastLegendaryHeroRequest().getLegendaryHeroRefId());
			if (player.getLastLegendaryHeroRequest().getLegendaryHeroRefId() != "91003")
			{
				viewController.showLegendarySuccess(player.getLastLegendaryHeroRequest());
			}
			else
			{
				LegendaryHero lastLegendaryHeroRequest2 = player.getLastLegendaryHeroRequest();
				lastLegendaryHeroRequest2.setRequestState(RequestState.RequestStateCompleted);
				player.completeLegendaryHero(lastLegendaryHeroRequest2);
				GameObject gameObject = GameObject.Find("SteamStatsAndAchievements");
				if (gameObject != null)
				{
					gameObject.GetComponent<SteamStatsAndAchievements>().checkAchievementStatus();
				}
			}
			hideMask = false;
			doResume = false;
			audioController.playEventSuccessAudio();
			break;
		case PopupType.PopupTypeLegendaryRequestFail:
			if (player.getLastLegendaryHeroRequest().getLegendaryHeroRefId() != "91003")
			{
				LegendaryHero lastLegendaryHeroRequest = player.getLastLegendaryHeroRequest();
				viewController.showGeneralDialoguePopup(GeneralPopupType.GeneralPopupTypeDialogueGeneral, resume: true, gameData.getTextByRefId("legendaryFail01"), gameData.getTextByRefId("legendaryFail02"), lastLegendaryHeroRequest.getFailComment(), "Image/legendary heroes/" + lastLegendaryHeroRequest.getImage());
				hideMask = true;
			}
			else
			{
				hideMask = false;
			}
			doResume = false;
			audioController.playEventFailAudio();
			break;
		case PopupType.PopupTypeNewObjective:
			GameObject.Find("Objective").GetComponent<GUIObjectiveController>().showNewObjectiveAnims();
			hideMask = true;
			doResume = true;
			break;
		case PopupType.PopupTypeGrantNotice:
		{
			GUIPayDayController component2 = GameObject.Find("Panel_PayDay").GetComponent<GUIPayDayController>();
			component2.showBankruptWarning(isFirstTime: true);
			component2.showPayDayPopup();
			hideMask = false;
			doResume = false;
			break;
		}
		case PopupType.PopupTypeJournal:
		{
			GUIJournalController component = GameObject.Find("Panel_Journal").GetComponent<GUIJournalController>();
			component.showEndCutscene();
			hideMask = false;
			doResume = false;
			flag = false;
			break;
		}
		case PopupType.PopupTypeGameOver:
			viewController.showGameOver("Image/gameover/gameover_patata", gameData.getTextByRefId("gameOver02"));
			hideMask = false;
			doResume = false;
			break;
		}
		if (flag)
		{
			switch (setId)
			{
			case "90401":
				shopMenuController.showIntro(isLoad: true);
				hideMask = false;
				doResume = false;
				break;
			case "90402":
			{
				bool flag2 = shopMenuController.tryStartTutorial("SELL1");
				hideMask = true;
				doResume = ((!flag2) ? true : false);
				break;
			}
			case "90404":
				viewController.showRenamePopup(PopupType.PopupTypeDogNaming);
				hideMask = false;
				doResume = false;
				text2 = string.Empty;
				break;
			case "90405":
				if (!player.checkHasDog())
				{
					shopMenuController.giveDogToPlayer();
					GameObject.Find("Panel_TopLeftMenu").GetComponent<GUITopMenuNewController>().updateDogBowl();
					viewController.queueItemGetPopup(gameData.getTextByRefId("featureUnlock05"), "Image/unlock/unlock_dog", gameData.getTextByRefId("featureUnlock06"));
				}
				hideMask = true;
				doResume = true;
				break;
			case "90406":
			{
				GUIObstacleController component3 = GameObject.Find("GUIObstacleController").GetComponent<GUIObstacleController>();
				component3.createObstacles();
				GameObject.Find("StationController").GetComponent<StationController>().setStation();
				GUICharacterAnimationController component4 = GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>();
				component4.spawnCharacterFrmheaven("10008");
				hideMask = true;
				doResume = true;
				break;
			}
			case "90425":
				viewController.showBetaComplete();
				hideMask = false;
				doResume = false;
				break;
			case "90428":
				hideMask = true;
				doResume = true;
				break;
			case "99901":
				viewController.showGameOver("Image/gameover/gameover_patata", gameData.getTextByRefId("gameOver02"));
				hideMask = false;
				doResume = false;
				text2 = string.Empty;
				break;
			case "90416":
			{
				List<Hashtable> list2 = new List<Hashtable>();
				for (int j = 1; j <= 16; j++)
				{
					string empty2 = string.Empty;
					empty2 = ((j >= 10) ? j.ToString() : ("0" + j));
					Hashtable hashtable3 = new Hashtable();
					string text3 = "Image/TextureSequence/ending-" + empty2;
					if (Constants.LANGUAGE == LanguageType.kLanguageTypeSpanish)
					{
						switch (empty2)
						{
						case "01":
						case "02":
						case "03":
						case "04":
						case "05":
						case "07":
							text3 += "-sp";
							break;
						}
					}
					hashtable3["texture"] = text3;
					hashtable3["anchor"] = "CENTER";
					hashtable3["posX"] = 0;
					hashtable3["posY"] = 0;
					hashtable3["sizeX"] = 1155;
					hashtable3["sizeY"] = 650;
					if (j > 5)
					{
						hashtable3["effect"] = "CROSSFADE";
					}
					if (j <= 5)
					{
						hashtable3["timer"] = 30;
						hashtable3["sound"] = "WHETSAPP_SFX";
					}
					switch (j)
					{
					case 1:
						hashtable3["bgm"] = "BGMFADE";
						break;
					case 6:
						hashtable3["bgm"] = "STARTFINALBGM";
						break;
					}
					list2.Add(hashtable3);
				}
				Hashtable hashtable4 = new Hashtable();
				hashtable4["special"] = "ENDING";
				list2.Add(hashtable4);
				viewController.showTextureSequencePopup(list2, isPureBlackMask: true, 0.8f);
				hideMask = false;
				doResume = false;
				break;
			}
			case "10001":
				shopMenuController.showIntro(isLoad: true);
				hideMask = false;
				doResume = false;
				break;
			case "11007":
			{
				LegendaryHero legendaryHeroByHeroRefId = gameData.getLegendaryHeroByHeroRefId("91002");
				legendaryHeroByHeroRefId.setRequestState(RequestState.RequestStateAccepted);
				player.addDisplayLegendaryHero(legendaryHeroByHeroRefId);
				viewController.showLegendaryRequest(legendaryHeroByHeroRefId);
				hideMask = false;
				doResume = false;
				break;
			}
			case "11009":
				viewController.queueItemGetPopup(gameData.getTextByRefId("featureUnlock29"), "Image/scenario_icon/gauntlet_blueprint_itemget", gameData.getTextByRefId("featureUnlock30"));
				break;
			case "11010":
				viewController.queueItemGetPopup(gameData.getTextByRefId("featureUnlock31"), "Image/Enchantment/hallowed_crest", gameData.getTextByRefId("featureUnlock32"));
				viewController.queueItemGetPopup(gameData.getTextByRefId("featureUnlock33"), "Image/scenario_icon/extraboost_itemget", gameData.getTextByRefId("featureUnlock34"));
				break;
			case "11012":
			{
				List<Hashtable> list = new List<Hashtable>();
				for (int i = 1; i <= 4; i++)
				{
					string empty = string.Empty;
					empty = ((i >= 10) ? i.ToString() : ("0" + i));
					Hashtable hashtable = new Hashtable();
					hashtable["texture"] = "Image/TextureSequence/olympus ending/endingScenes-" + empty;
					hashtable["anchor"] = "CENTER";
					hashtable["posX"] = 0;
					hashtable["posY"] = 0;
					hashtable["sizeX"] = 1155;
					hashtable["sizeY"] = 650;
					hashtable["effect"] = "CROSSFADE";
					list.Add(hashtable);
				}
				Hashtable hashtable2 = new Hashtable();
				hashtable2["special"] = "ENDING_NOCONTINUE";
				list.Add(hashtable2);
				viewController.showTextureSequencePopup(list, isPureBlackMask: true, 0.8f);
				hideMask = false;
				doResume = false;
				PlayerPrefs.SetString("Scenario_" + player.getGameScenario() + "_Complete", "TRUE");
				break;
			}
			case "11011":
				viewController.showGameOver("Image/gameover/gameOver_olympus", gameData.getTextByRefId("gameOver03"));
				hideMask = false;
				doResume = false;
				text2 = string.Empty;
				break;
			}
		}
		List<GameLock> gameLockListByTypeAndRefId = gameData.getGameLockListByTypeAndRefId("CUTSCENE", setId);
		foreach (GameLock item in gameLockListByTypeAndRefId)
		{
			shopMenuController.unlockFeature(item.getLockFeature());
		}
		viewController.closeDialoguePopup(hideMask, doResume);
		if (text2 != null)
		{
			if (text2 == "SEASON")
			{
				Season seasonByMonth2 = CommonAPI.getSeasonByMonth(player.getSeasonIndex());
				audioController.changeBGM(CommonAPI.getSeasonBGM(seasonByMonth2));
				return;
			}
			if (text2 == string.Empty)
			{
				return;
			}
		}
		audioController.changeBGM(text2);
	}

	private void showHideChoices()
	{
		if (currentLine.getDialogueNextRefId() != string.Empty)
		{
			textBoxArrow.alpha = 1f;
			choice1Button.isEnabled = false;
			choice2Button.isEnabled = false;
			textBoxBg.GetComponent<BoxCollider>().enabled = true;
			return;
		}
		bool flag = false;
		if (currentLine.getDialogueChoice1() != string.Empty)
		{
			choice1Label.text = doTextReplace(currentLine.getDialogueChoice1());
			choice1Button.isEnabled = true;
			textBoxBg.GetComponent<BoxCollider>().enabled = false;
			flag = true;
		}
		else
		{
			choice1Button.isEnabled = false;
		}
		if (currentLine.getDialogueChoice2() != string.Empty)
		{
			choice2Label.text = doTextReplace(currentLine.getDialogueChoice2());
			choice2Button.isEnabled = true;
			textBoxBg.GetComponent<BoxCollider>().enabled = false;
			flag = true;
		}
		else
		{
			choice2Button.isEnabled = false;
		}
		if (flag)
		{
			textBoxArrow.alpha = 0f;
		}
		else
		{
			textBoxArrow.alpha = 1f;
		}
	}

	private string doTextReplace(string aText)
	{
		Player player = game.getPlayer();
		aText = aText.Replace("[playerName]", player.getPlayerName());
		aText = aText.Replace("[dogName]", player.getDogName());
		return aText;
	}

	private void forceTypewriterEnd()
	{
		displayLineText = lineText;
		lineDisplayChar = lineText.Length;
		switch (currentLine.getDialoguePosition())
		{
		case "LEFT":
			speechLabel.text = lineText;
			break;
		case "RIGHT":
			speechLabel.text = lineText;
			break;
		case "MIDDLE":
			actionTextLabel.text = lineText;
			break;
		}
		lineIsAnimating = false;
		audioController.stopTextTypeAudio();
		showHideChoices();
	}

	private IEnumerator typewriterText()
	{
		while (lineIsAnimating)
		{
			displayLineText += lineText[lineDisplayChar];
			lineDisplayChar++;
			switch (currentLine.getDialoguePosition())
			{
			case "LEFT":
				speechLabel.text = displayLineText;
				break;
			case "RIGHT":
				speechLabel.text = displayLineText;
				break;
			case "MIDDLE":
				actionTextLabel.text = displayLineText;
				break;
			}
			if (lineDisplayChar >= lineText.Length)
			{
				lineIsAnimating = false;
				audioController.stopTextTypeAudio();
				showHideChoices();
			}
			yield return new WaitForSeconds(0.02f);
		}
	}
}
