using System.Collections.Generic;
using UnityEngine;

public class GUIScenarioTimerController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private string timerName;

	private long totalDuration;

	private long endTime;

	private List<string> itemOrderList;

	private Dictionary<string, Item> itemList;

	private Dictionary<string, bool> itemFoundList;

	private UIProgressBar bar;

	private UIGrid itemGrid;

	private Dictionary<string, GameObject> itemObjList;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
	}

	public void processClick(string gameObjectName)
	{
	}

	public void processHover(bool isOver, string hoverName)
	{
		if (isOver)
		{
			if (hoverName != null)
			{
			}
			string[] array = hoverName.Split('_');
			string text = array[0];
			if (text != null && text == "ScenarioItemObj" && array.Length > 1)
			{
				int index = CommonAPI.parseInt(array[1]);
				Player player = game.getPlayer();
				string key = itemOrderList[index];
				if (itemFoundList[key])
				{
					tooltipScript.showText(itemList[key].getItemName());
				}
				else
				{
					tooltipScript.showText("???");
				}
			}
		}
		else
		{
			tooltipScript.setInactive();
		}
	}

	public void setReference(string aTimerName)
	{
		timerName = aTimerName;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		totalDuration = long.Parse(gameData.getConstantByRefID(gameScenarioByRefId.getFormulaConstantsSet() + "_" + timerName + "_DURATION"));
		endTime = long.Parse(gameData.getScenarioVariableValue(gameScenarioByRefId.getFormulaConstantsSet(), timerName + "_ENDTIME").getVariableValueString());
		List<ScenarioVariable> scenarioVariableListByListName = gameData.getScenarioVariableListByListName(gameScenarioByRefId.getFormulaConstantsSet(), timerName + "ITEM");
		List<ScenarioVariable> scenarioVariableListByListName2 = gameData.getScenarioVariableListByListName(gameScenarioByRefId.getFormulaConstantsSet(), timerName + "ITEMFOUND");
		bar = commonScreenObject.findChild(base.gameObject, "ScenarioTimer_bar").GetComponent<UIProgressBar>();
		itemGrid = commonScreenObject.findChild(base.gameObject, "ScenarioObj_grid").GetComponent<UIGrid>();
		itemObjList = new Dictionary<string, GameObject>();
		itemOrderList = new List<string>();
		itemList = new Dictionary<string, Item>();
		itemFoundList = new Dictionary<string, bool>();
		for (int i = 0; i < scenarioVariableListByListName.Count; i++)
		{
			ScenarioVariable scenarioVariable = scenarioVariableListByListName[i];
			ScenarioVariable scenarioVariable2 = scenarioVariableListByListName2[i];
			string variableValueString = scenarioVariable.getVariableValueString();
			Item itemByRefId = gameData.getItemByRefId(variableValueString);
			itemList.Add(variableValueString, itemByRefId);
			itemFoundList.Add(variableValueString, scenarioVariable2.getVariableValueBool());
			itemOrderList.Add(variableValueString);
		}
		refreshBarDisplay();
		refreshItemDisplay();
	}

	public void updateItemOwnState(string itemRefId, bool aOwn)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		List<ScenarioVariable> scenarioVariableListByListName = gameData.getScenarioVariableListByListName(gameScenarioByRefId.getFormulaConstantsSet(), timerName + "ITEM");
		List<ScenarioVariable> scenarioVariableListByListName2 = gameData.getScenarioVariableListByListName(gameScenarioByRefId.getFormulaConstantsSet(), timerName + "ITEMFOUND");
		int num = 0;
		foreach (ScenarioVariable item in scenarioVariableListByListName)
		{
			if (item.getVariableValueString() == itemRefId)
			{
				scenarioVariableListByListName2[num].setVariableValueBool(aOwn);
			}
			num++;
		}
	}

	public void findItem(string itemRefId)
	{
		if (itemList.ContainsKey(itemRefId) && itemFoundList.ContainsKey(itemRefId) && !itemFoundList[itemRefId])
		{
			itemFoundList[itemRefId] = true;
			updateItemOwnState(itemRefId, aOwn: true);
			refreshItemDisplay();
		}
	}

	public void loseItem(string itemRefId)
	{
		if (itemList.ContainsKey(itemRefId) && itemFoundList.ContainsKey(itemRefId) && itemFoundList[itemRefId])
		{
			itemFoundList[itemRefId] = false;
			updateItemOwnState(itemRefId, aOwn: false);
			refreshItemDisplay();
		}
	}

	public void changeEndTime(long aEndTime)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		endTime = aEndTime;
		ScenarioVariable scenarioVariableValue = gameData.getScenarioVariableValue(gameScenarioByRefId.getFormulaConstantsSet(), timerName + "_ENDTIME");
		scenarioVariableValue.setVariableValueString(endTime.ToString());
		refreshBarDisplay();
	}

	public bool refreshBarDisplay()
	{
		bool result = false;
		if (totalDuration > 0)
		{
			Player player = game.getPlayer();
			long playerTimeLong = player.getPlayerTimeLong();
			float num = (float)(totalDuration - (endTime - playerTimeLong)) / (float)totalDuration;
			bar.value = num;
			if (num >= 1f)
			{
				result = doTimeEndHandling();
			}
		}
		return result;
	}

	public bool doTimeEndHandling()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		bool result = false;
		string text = gameScenarioByRefId.getFormulaConstantsSet() + "@TESTTIMER";
		if (text != null && text == "TEST@TESTTIMER")
		{
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: true, "Timer Test", "Timer ended.", PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
			viewController.closeScenarioTimer();
		}
		return result;
	}

	public void closeScenarioTimer()
	{
		viewController.closeScenarioTimer();
	}

	public void refreshItemDisplay()
	{
		if (totalDuration > 0 && itemObjList.Count < itemList.Count)
		{
			int num = 0;
			foreach (string itemOrder in itemOrderList)
			{
				Item item = itemList[itemOrder];
				GameObject gameObject = commonScreenObject.createPrefab(itemGrid.gameObject, "ScenarioItemObj_" + num, "Prefab/ScenarioTimer/ScenarioItemObj", Vector3.zero, Vector3.one, Vector3.zero);
				gameObject.GetComponentInChildren<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/scenarioItems/" + item.getImage());
				itemObjList.Add(item.getItemRefId(), gameObject);
				num++;
			}
			itemGrid.Reposition();
		}
		foreach (string key in itemList.Keys)
		{
			Item item2 = itemList[key];
			string itemRefId = item2.getItemRefId();
			if (itemFoundList[itemRefId])
			{
				itemObjList[itemRefId].GetComponentInChildren<UITexture>().alpha = 1f;
			}
			else
			{
				itemObjList[itemRefId].GetComponentInChildren<UITexture>().alpha = 0f;
			}
		}
	}
}
