using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIRenamePopupController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private UIButton okButton;

	private UILabel renameText;

	private UILabel inputNameInputLbl;

	private UIInput inputNameInput;

	private PopupType currentPopupType;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		okButton = commonScreenObject.findChild(base.gameObject, "OkButton").GetComponent<UIButton>();
		renameText = GameObject.Find("RenameText").GetComponent<UILabel>();
		GameObject gameObject = GameObject.Find("InputNameInputLbl");
		inputNameInputLbl = gameObject.GetComponent<UILabel>();
		inputNameInput = gameObject.GetComponent<UIInput>();
		currentPopupType = PopupType.PopupTypeNothing;
	}

	private void Update()
	{
		if (Input.GetKeyDown(gameData.getKeyCodeByRefID("100008")))
		{
			processClick("OkButton");
		}
	}

	public void processClick(string gameObjectName)
	{
		if (!(gameObjectName == "OkButton"))
		{
			return;
		}
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		string text = inputNameInputLbl.text;
		text = text.Replace("\n", string.Empty);
		if (!(text != string.Empty))
		{
			return;
		}
		switch (currentPopupType)
		{
		case PopupType.PopupTypeForgeWeaponComplete:
			viewController.closeRenamePopup(hide: false);
			player.setProjectName(text);
			viewController.showWeaponComplete();
			break;
		case PopupType.PopupTypeIntroName:
			viewController.closeRenamePopup(hide: false);
			shopMenuController.enterPlayerName(text);
			break;
		case PopupType.PopupTypeIntroShopName:
			if (player.getGameScenario() == "10001")
			{
				viewController.closeRenamePopup(hide: true);
				shopMenuController.enterShopName(text);
				if (PlayerPrefs.GetInt("lastSeenTutorialIndex", -1) >= 7)
				{
					CommonAPI.debug("lastSeenTutorialIndex " + PlayerPrefs.GetInt("lastSeenTutorialIndex", -1));
					viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeYesAndNo, resume: true, gameData.getTextByRefId("tutorialSkip01").ToUpper(CultureInfo.InvariantCulture), gameData.getTextByRefId("tutorialSkip02"), PopupType.PopupTypeSkipTutorial, null, colorTag: false, null, map: false, string.Empty);
				}
				else
				{
					shopMenuController.startGame();
				}
				break;
			}
			if (player.getGameScenario() == "30001")
			{
				viewController.closeRenamePopup(hide: false);
				shopMenuController.enterShopName(text);
				break;
			}
			viewController.closeRenamePopup(hide: true);
			shopMenuController.enterShopName(text);
			if (player.getGameScenario() == "10002")
			{
				long num = long.Parse(gameData.getConstantByRefID("TEST_TESTTIMER_DURATION"));
				long num2 = player.getPlayerTimeLong() + num;
				gameData.getScenarioVariableValue("TEST", "TESTTIMER_ENDTIME").setVariableValueString(num2.ToString());
				List<ScenarioVariable> scenarioVariableListByListName = gameData.getScenarioVariableListByListName("TEST", "TESTTIMERITEMFOUND");
				foreach (ScenarioVariable item in scenarioVariableListByListName)
				{
					item.resetVariable();
				}
				viewController.showScenarioTimer("TESTTIMER");
			}
			player.setSkipTutorials(aBool: true);
			viewController.setGameStarted(started: true);
			shopMenuController.startGame();
			break;
		case PopupType.PopupTypeDogNaming:
			viewController.closeRenamePopup(hide: true);
			player.nameDog(text);
			if (player.getGameScenario() == "10001")
			{
				viewController.showDialoguePopup("90405", game.getGameData().getDialogueBySetId("90405"));
			}
			else if (player.getGameScenario() == "30001")
			{
				player.setSkipTutorials(aBool: true);
				viewController.setGameStarted(started: true);
				shopMenuController.startGame();
			}
			break;
		}
	}

	public void checkInput()
	{
		if (inputNameInputLbl.text.Length < 1)
		{
			okButton.isEnabled = false;
		}
		else
		{
			okButton.isEnabled = true;
		}
	}

	public void setReference(PopupType aPopupType)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		currentPopupType = aPopupType;
		commonScreenObject.findChild(base.gameObject, "OkButton/okLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("menuGeneral04");
		switch (currentPopupType)
		{
		case PopupType.PopupTypeForgeWeaponComplete:
			renameText.text = gameData.getTextByRefId("menuForgeComplete05");
			inputNameInput.defaultText = player.getCurrentProject().getProjectName(includePrefix: false);
			break;
		case PopupType.PopupTypeIntroName:
			base.gameObject.GetComponent<UIPanel>().depth = 101;
			renameText.text = gameData.getTextByRefId("intro01") + "\n" + gameData.getTextByRefId("intro02");
			inputNameInput.defaultText = gameData.getTextByRefId("PATATA");
			break;
		case PopupType.PopupTypeIntroShopName:
			base.gameObject.GetComponent<UIPanel>().depth = 101;
			renameText.text = gameData.getTextByRefIdWithDynText("intro03", "[playerName]", player.getPlayerName()) + "\n" + gameData.getTextByRefId("intro04");
			inputNameInput.defaultText = gameData.getTextByRefId("SHOPNAME");
			break;
		case PopupType.PopupTypeDogNaming:
			renameText.text = gameData.getTextByRefId("dogEvent06");
			inputNameInput.defaultText = player.getDogName();
			break;
		}
		checkInput();
	}
}
