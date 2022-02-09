using System.Collections.Generic;
using UnityEngine;

public class GUIForgeController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private AudioController audioController;

	private ShopMenuController shopMenuController;

	private string progressPrefix;

	private UIButton cutsceneButton;

	private List<string> dialogueSetList;

	private UILabel cutsceneLabel;

	private int cutsceneIndex;

	private UILabel textTestLabel;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		shopMenuController = null;
		progressPrefix = game.getGameData().getTextByRefId("questHud03");
		cutsceneIndex = -1;
		setLabels();
	}

	public void setLabels()
	{
		GameData gameData = game.getGameData();
		UILabel[] componentsInChildren = base.gameObject.GetComponentsInChildren<UILabel>();
		foreach (UILabel uILabel in componentsInChildren)
		{
			switch (uILabel.gameObject.name)
			{
			case "objectiveTest_label":
				uILabel.text = "Objective Test";
				break;
			case "heroTest_label":
				uILabel.text = "Hero Test";
				break;
			case "smithTest_label":
				uILabel.text = "Smith Test";
				break;
			case "awardsTest_label":
				uILabel.text = "Awards Test";
				break;
			case "DogTrainingTest_label":
				uILabel.text = "Dog Training";
				break;
			case "textTest_label":
				textTestLabel = uILabel;
				setTextTestButtonText();
				break;
			case "cutsceneTest_label":
				cutsceneLabel = uILabel;
				cutsceneLabel.text = "CUTSCENE TEST";
				break;
			}
		}
		cutsceneButton = GameObject.Find("cutsceneTest_button").GetComponent<UIButton>();
		cutsceneButton.isEnabled = false;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "forge_button":
			showForgeMenu();
			break;
		case "heroTest_button":
			viewController.showHeroAnimTest();
			break;
		case "smithTest_button":
			viewController.showSmithImagesTest();
			break;
		case "awardsTest_button":
			viewController.showGoldenHammerAwards();
			break;
		case "DogTrainingTest_button":
			viewController.showDogTraining();
			break;
		case "textTest_button":
			toggleTextTest();
			break;
		case "objectiveTest_button":
			if (shopMenuController == null)
			{
				shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
			}
			shopMenuController.checkObjectiveStatus(forceObjectiveSuccess: true);
			break;
		case "cutsceneTest_button":
			if (cutsceneIndex >= 0)
			{
				viewController.showDialoguePopup(dialogueSetList[cutsceneIndex], game.getGameData().getDialogueBySetId(dialogueSetList[cutsceneIndex]));
			}
			break;
		case "cutscenePrev_button":
			selectNextCutscene();
			break;
		case "cutsceneNext_button":
			selectPrevCutscene();
			break;
		}
	}

	private void showForgeMenu()
	{
		if (shopMenuController == null)
		{
			shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		}
		CommonAPI.debug("showForgeMenu");
		viewController.showForgeMenuNewPopup();
	}

	private void selectNextCutscene()
	{
		if (dialogueSetList == null)
		{
			dialogueSetList = game.getGameData().getDialogueSetIdList();
		}
		if (dialogueSetList.Count > 0)
		{
			cutsceneButton.isEnabled = true;
			cutsceneIndex++;
			if (cutsceneIndex >= dialogueSetList.Count)
			{
				cutsceneIndex = 0;
			}
			cutsceneLabel.text = dialogueSetList[cutsceneIndex];
		}
	}

	private void selectPrevCutscene()
	{
		if (dialogueSetList == null)
		{
			dialogueSetList = game.getGameData().getDialogueSetIdList();
		}
		if (dialogueSetList.Count > 0)
		{
			cutsceneButton.isEnabled = true;
			cutsceneIndex--;
			if (cutsceneIndex < 0)
			{
				cutsceneIndex = dialogueSetList.Count - 1;
			}
			cutsceneLabel.text = dialogueSetList[cutsceneIndex];
		}
	}

	private void toggleTextTest()
	{
		if (PlayerPrefs.GetInt("showText", 0) == 0)
		{
			PlayerPrefs.SetInt("showText", 1);
		}
		else
		{
			PlayerPrefs.SetInt("showText", 0);
		}
		setTextTestButtonText();
	}

	private void setTextTestButtonText()
	{
		if (textTestLabel != null)
		{
			if (PlayerPrefs.GetInt("showText", 0) == 0)
			{
				textTestLabel.text = "Text Test OFF";
			}
			else
			{
				textTestLabel.text = "Text Test ON";
			}
		}
	}
}
