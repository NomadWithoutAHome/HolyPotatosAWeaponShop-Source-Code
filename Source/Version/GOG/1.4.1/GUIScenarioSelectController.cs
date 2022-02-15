using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIScenarioSelectController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private List<GameScenario> scenarioList;

	private int currentScenarioIndex;

	private GameObject panel_Scenario;

	private UIGrid scenarioGrid;

	private UILabel titleLabel;

	private UIButton scenarioScroll_left;

	private UIButton scenarioScroll_right;

	private Dictionary<int, GameObject> scenarioObjList;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		scenarioList = new List<GameScenario>();
		currentScenarioIndex = 0;
		panel_Scenario = GameObject.Find("Panel_Scenario");
		scenarioGrid = commonScreenObject.findChild(panel_Scenario, "ScenarioGrid").GetComponent<UIGrid>();
		titleLabel = commonScreenObject.findChild(base.gameObject, "TitleBg/TitleLabel").GetComponent<UILabel>();
		scenarioScroll_left = commonScreenObject.findChild(base.gameObject, "ScenarioScroll_left").GetComponent<UIButton>();
		scenarioScroll_right = commonScreenObject.findChild(base.gameObject, "ScenarioScroll_right").GetComponent<UIButton>();
		scenarioObjList = new Dictionary<int, GameObject>();
	}

	public void processClick(string gameObjectName)
	{
		string[] array = gameObjectName.Split('_');
		switch (array[0])
		{
		case "ScenarioScroll":
			if (array[1] == "left")
			{
				currentScenarioIndex--;
			}
			else if (array[1] == "right")
			{
				currentScenarioIndex++;
			}
			navigateScenario();
			break;
		case "StartButton":
			startSelectedScenario(CommonAPI.parseInt(array[1]));
			break;
		case "LoadButton":
			GameObject.Find("GUIStartScreenController").GetComponent<GUIStartScreenController>().disableButtons();
			viewController.showSaveLoadPopup(save: false, scenarioList[CommonAPI.parseInt(array[1])].getGameScenarioRefId());
			break;
		}
	}

	public void setReference(bool firstTime = false)
	{
		GameData gameData = game.getGameData();
		scenarioList = gameData.getOtherGameScenario();
		titleLabel.text = gameData.getTextByRefId("scenarioSelect01");
		CommonAPI.debug("scenarioList : " + scenarioList.Count);
		if (scenarioList.Count < 1)
		{
			commonScreenObject.destroyPrefabImmediate(base.gameObject);
			return;
		}
		GameScenario gameScenario = scenarioList[currentScenarioIndex];
		updateArrow();
		foreach (KeyValuePair<int, GameObject> scenarioObj in scenarioObjList)
		{
			commonScreenObject.destroyPrefabImmediate(scenarioObj.Value);
		}
		scenarioObjList.Clear();
		if (firstTime)
		{
			scenarioScroll_left.isEnabled = false;
			scenarioScroll_right.isEnabled = false;
		}
		for (int i = 0; i < scenarioList.Count; i++)
		{
			spawnScenarioObj(scenarioList[i], i, firstTime);
		}
		scenarioGrid.Reposition();
	}

	public void disableButton()
	{
		scenarioScroll_left.isEnabled = false;
		scenarioScroll_right.isEnabled = false;
		foreach (KeyValuePair<int, GameObject> scenarioObj in scenarioObjList)
		{
			commonScreenObject.findChild(scenarioObj.Value, "StartButton_" + scenarioObj.Key).GetComponent<UIButton>().isEnabled = false;
			commonScreenObject.findChild(scenarioObj.Value, "LoadButton_" + scenarioObj.Key).GetComponent<UIButton>().isEnabled = false;
		}
	}

	public void enableButton()
	{
		updateArrow();
		foreach (KeyValuePair<int, GameObject> scenarioObj in scenarioObjList)
		{
			commonScreenObject.findChild(scenarioObj.Value, "StartButton_" + scenarioObj.Key).GetComponent<UIButton>().isEnabled = true;
			commonScreenObject.findChild(scenarioObj.Value, "LoadButton_" + scenarioObj.Key).GetComponent<UIButton>().isEnabled = true;
		}
	}

	private void spawnScenarioObj(GameScenario aScenario, int index, bool firstTime)
	{
		GameObject gameObject = commonScreenObject.createPrefab(scenarioGrid.gameObject, "Scenario" + index, "Prefab/StartScreen/ScenarioObj", Vector3.zero, Vector3.one, Vector3.zero);
		bool flag = bool.Parse(PlayerPrefs.GetString("Scenario_" + aScenario.getGameScenarioRefId() + "_Play", "FALSE"));
		bool flag2 = bool.Parse(PlayerPrefs.GetString("Scenario_" + aScenario.getGameScenarioRefId() + "_Complete", "FALSE"));
		if (flag)
		{
			commonScreenObject.findChild(gameObject, "Frame").GetComponent<UISprite>().color = Color.black;
			commonScreenObject.findChild(gameObject, "NewBg").gameObject.SetActive(value: false);
		}
		else
		{
			commonScreenObject.findChild(gameObject, "Frame").GetComponent<UISprite>().color = Color.black;
			commonScreenObject.findChild(gameObject, "NewBg").gameObject.SetActive(value: true);
		}
		if (flag2)
		{
			commonScreenObject.findChild(gameObject, "CompleteBg").gameObject.SetActive(value: false);
			commonScreenObject.findChild(gameObject, "ScenarioBg").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Scenario/" + aScenario.getCompleteImg());
			commonScreenObject.findChild(gameObject, "ScenarioBg/ScenarioImg").GetComponent<UITexture>().mainTexture = null;
		}
		else
		{
			commonScreenObject.findChild(gameObject, "ScenarioBg").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Scenario/" + aScenario.getBgImage());
			commonScreenObject.findChild(gameObject, "ScenarioBg/ScenarioImg").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Scenario/" + aScenario.getImage());
			commonScreenObject.findChild(gameObject, "CompleteBg").gameObject.SetActive(value: false);
		}
		commonScreenObject.findChild(gameObject, "ScenarioTitleBg/ScnearioTitleLabel").GetComponent<UILabel>().text = aScenario.getGameScenarioName().ToUpper(CultureInfo.InvariantCulture);
		commonScreenObject.findChild(gameObject, "ScenarioDetail/ScenarioDesc").GetComponent<UILabel>().text = aScenario.getGameScenarioDescription();
		commonScreenObject.findChild(gameObject, "Difficulty/DifficultyLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("scenarioSelect02").ToUpper(CultureInfo.InvariantCulture);
		GameObject aObject = commonScreenObject.findChild(gameObject, "Difficulty/Panel_Star").gameObject;
		for (int i = 1; i <= aScenario.getDifficultyStar(); i++)
		{
			commonScreenObject.findChild(aObject, "Star" + i).GetComponent<UISprite>().spriteName = "star_a";
		}
		commonScreenObject.findChild(gameObject, "StartButton").GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("questSelect11");
		if (firstTime)
		{
			commonScreenObject.findChild(gameObject, "StartButton").GetComponent<UIButton>().isEnabled = false;
		}
		commonScreenObject.findChild(gameObject, "StartButton").name = "StartButton_" + index;
		commonScreenObject.findChild(gameObject, "LoadButton").GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("saveLoadMenu06");
		if (firstTime)
		{
			commonScreenObject.findChild(gameObject, "LoadButton").GetComponent<UIButton>().isEnabled = false;
		}
		commonScreenObject.findChild(gameObject, "LoadButton").name = "LoadButton_" + index;
		scenarioObjList.Add(index, gameObject);
	}

	private void updateArrow()
	{
		if (scenarioList.Count == 1)
		{
			scenarioScroll_left.isEnabled = false;
			scenarioScroll_right.isEnabled = false;
		}
		else if (currentScenarioIndex == 0)
		{
			scenarioScroll_left.isEnabled = false;
			scenarioScroll_right.isEnabled = true;
		}
		else if (currentScenarioIndex == scenarioList.Count - 1)
		{
			scenarioScroll_left.isEnabled = true;
			scenarioScroll_right.isEnabled = false;
		}
		else
		{
			scenarioScroll_left.isEnabled = true;
			scenarioScroll_right.isEnabled = true;
		}
	}

	private void startSelectedScenario(int index)
	{
		game.getPlayer().setGameScenario(scenarioList[index].getGameScenarioRefId());
		PlayerPrefs.SetString("Scenario_" + scenarioList[index].getGameScenarioRefId() + "_Play", "TRUE");
		viewController.closeStartScreen();
		viewController.showPanelsAtStart();
		viewController.showShop();
	}

	private void navigateScenario()
	{
		Vector3 localPosition = scenarioObjList[currentScenarioIndex].transform.localPosition;
		SpringPanel.Begin(panel_Scenario, -localPosition, 8f);
		updateArrow();
	}
}
