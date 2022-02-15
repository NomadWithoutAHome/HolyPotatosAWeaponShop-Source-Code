using System.Collections.Generic;
using UnityEngine;

public class GUIEventPopupController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private UIGrid eventGrid;

	private UILabel tapToContinueLabel;

	private int currentIndex;

	private string boxPrefix;

	private List<GameObject> dialogueBox;

	private List<Dialogue> currDialogue;

	private List<string> cutsceneKeyList;

	private List<string> cutsceneValueList;

	private PopupType currPopupType;

	private UITexture characterLeft;

	private UITexture characterRight;

	private string imageLeft;

	private string imageRight;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		eventGrid = GameObject.Find("EventGrid").GetComponent<UIGrid>();
		tapToContinueLabel = GameObject.Find("TapToContinueLabel").GetComponent<UILabel>();
		currentIndex = 0;
		boxPrefix = "box_";
		dialogueBox = new List<GameObject>();
		currDialogue = new List<Dialogue>();
		cutsceneKeyList = new List<string>();
		cutsceneValueList = new List<string>();
		characterLeft = GameObject.Find("CharacterLeft").GetComponent<UITexture>();
		characterRight = GameObject.Find("CharacterRight").GetComponent<UITexture>();
		imageLeft = string.Empty;
		imageRight = string.Empty;
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName == "EventFrame")
		{
			showNextLine();
		}
	}

	public void setReference(List<Dialogue> dialogueList, List<string> keyList, List<string> valueList, PopupType aPopupType)
	{
		currDialogue = dialogueList;
		cutsceneKeyList = keyList;
		cutsceneValueList = valueList;
		currPopupType = aPopupType;
		foreach (Dialogue item in currDialogue)
		{
			switch (item.getDialoguePosition())
			{
			case "LEFT":
				if (imageLeft == string.Empty)
				{
					imageLeft = item.getImage();
				}
				break;
			case "RIGHT":
				if (imageRight == string.Empty)
				{
					imageRight = item.getImage();
				}
				break;
			}
		}
		characterLeft.mainTexture = commonScreenObject.loadTexture("Image/Cutscenes/" + imageLeft);
		characterRight.mainTexture = commonScreenObject.loadTexture("Image/Cutscenes/" + imageRight);
		tapToContinueLabel.text = game.getGameData().getTextByRefId("TapToContinue");
	}

	private void showNextLine()
	{
		if (currDialogue.Count > 0)
		{
			Dialogue dialogue = currDialogue[0];
			currDialogue.RemoveAt(0);
			if (currentIndex > 3)
			{
				currentIndex = 0;
				foreach (GameObject item in dialogueBox)
				{
					Object.DestroyImmediate(item);
				}
				dialogueBox.Clear();
			}
			if (dialogue.getDialogueText() != string.Empty)
			{
				string text = dialogue.getDialogueText();
				for (int i = 0; i < cutsceneKeyList.Count; i++)
				{
					text = text.Replace(cutsceneKeyList[i], cutsceneValueList[i]);
				}
				GameObject gameObject = null;
				switch (dialogue.getDialoguePosition())
				{
				case "LEFT":
					gameObject = commonScreenObject.createPrefab(eventGrid.gameObject, boxPrefix + currentIndex, "Prefab/Event/leftBox", Vector3.zero, Vector3.one, Vector3.zero);
					break;
				case "RIGHT":
					gameObject = commonScreenObject.createPrefab(eventGrid.gameObject, boxPrefix + currentIndex, "Prefab/Event/rightBox", Vector3.zero, Vector3.one, Vector3.zero);
					break;
				}
				commonScreenObject.findChild(gameObject, "CharacterName").GetComponent<UILabel>().text = dialogue.getDialogueName();
				commonScreenObject.findChild(gameObject, "Text").GetComponent<UILabel>().text = text;
				dialogueBox.Add(gameObject);
			}
			GameObject gameObject2 = null;
			if (dialogue.getDialogueAction() != "NONE")
			{
				gameObject2 = dialogue.getDialoguePosition() switch
				{
					"LEFT" => commonScreenObject.createPrefab(eventGrid.gameObject, boxPrefix + currentIndex, "Prefab/Event/leftBox", Vector3.zero, Vector3.one, Vector3.zero), 
					"RIGHT" => commonScreenObject.createPrefab(eventGrid.gameObject, boxPrefix + currentIndex, "Prefab/Event/rightBox", Vector3.zero, Vector3.one, Vector3.zero), 
					_ => commonScreenObject.createPrefab(eventGrid.gameObject, boxPrefix + currentIndex, "Prefab/Event/leftBox", Vector3.zero, Vector3.one, Vector3.zero), 
				};
				switch (dialogue.getDialogueAction())
				{
				case "IN_POSITION":
					commonScreenObject.findChild(gameObject2, "CharacterName").GetComponent<UILabel>().text = dialogue.getDialogueName();
					commonScreenObject.findChild(gameObject2, "Text").GetComponent<UILabel>().text = " is here";
					dialogueBox.Add(gameObject2);
					break;
				case "ENTER":
					commonScreenObject.findChild(gameObject2, "CharacterName").GetComponent<UILabel>().text = dialogue.getDialogueName();
					commonScreenObject.findChild(gameObject2, "Text").GetComponent<UILabel>().text = " enters";
					dialogueBox.Add(gameObject2);
					break;
				case "SAD":
					commonScreenObject.findChild(gameObject2, "CharacterName").GetComponent<UILabel>().text = dialogue.getDialogueName();
					commonScreenObject.findChild(gameObject2, "Text").GetComponent<UILabel>().text = " looks sad";
					dialogueBox.Add(gameObject2);
					break;
				case "EXCITED":
					commonScreenObject.findChild(gameObject2, "CharacterName").GetComponent<UILabel>().text = dialogue.getDialogueName();
					commonScreenObject.findChild(gameObject2, "Text").GetComponent<UILabel>().text = " looks excited";
					dialogueBox.Add(gameObject2);
					break;
				}
			}
			currentIndex++;
			eventGrid.Reposition();
		}
		else
		{
			viewController.closeEventPopup();
		}
	}
}
