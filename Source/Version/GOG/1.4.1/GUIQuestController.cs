using System.Collections.Generic;
using UnityEngine;

public class GUIQuestController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private AudioController audioController;

	private ShopMenuController shopMenuController;

	private UIPanel panel_QuestList;

	private UIGrid questGrid;

	private GameObject seasonQuestGiverObject;

	private UILabel seasonTimeLeft;

	private List<GameObject> normalAndBlueprintQuestObjectList;

	private Dictionary<string, List<QuestNEW>> sortedSeasonQuestList;

	private Dictionary<string, List<QuestNEW>> sortedBlueprintQuestList;

	private Dictionary<string, List<QuestNEW>> sortedNormalQuestList;

	private GameObject currOpenedQuestObject;

	private GameObject questDetailContainer;

	private UIGrid questDetailGrid;

	private UISprite questDetailCenter;

	private GameObject questDetailBottom;

	private const string seasonPrefix = "Season";

	private const string blueprintPrefix = "Blueprint";

	private const string normalPrefix = "Normal";

	private string progressPrefix;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		shopMenuController = null;
		panel_QuestList = GameObject.Find("Panel_QuestList").GetComponent<UIPanel>();
		questGrid = GameObject.Find("QuestGrid").GetComponent<UIGrid>();
		seasonQuestGiverObject = null;
		normalAndBlueprintQuestObjectList = new List<GameObject>();
		currOpenedQuestObject = null;
		questDetailContainer = null;
		questDetailGrid = null;
		questDetailCenter = null;
		questDetailBottom = null;
		progressPrefix = game.getGameData().getTextByRefId("questHud03");
	}

	public void processClick(string gameObjectName)
	{
		string[] array = gameObjectName.Split('_');
		CommonAPI.debug("splitName.Length: " + array.Length);
		if (currOpenedQuestObject != null && array.Length <= 3)
		{
			commonScreenObject.destroyPrefabImmediate(currOpenedQuestObject);
			viewController.hideBlackMask();
			panel_QuestList.depth = 1;
			viewController.resumeEverything();
		}
		else if (array.Length > 3)
		{
			panel_QuestList.depth = 1;
			startForging(gameObjectName);
		}
		else
		{
			viewController.pauseEverything(GameState.GameStatePopEvent);
			viewController.showBlackMask();
			panel_QuestList.depth = 11;
			selectQuestGiver(gameObjectName);
		}
	}

	public void checkQuest()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		int num = 0;
		reset();
		List<QuestNEW> questNEWListByType = gameData.getQuestNEWListByType(QuestNEWType.QuestNEWTypeSeason, ignoreLock: false);
		sortedSeasonQuestList = gameData.sortQuestListByQuestGiver(questNEWListByType);
		foreach (string key in sortedSeasonQuestList.Keys)
		{
			seasonQuestGiverObject = commonScreenObject.createPrefab(questGrid.gameObject, num + "_Season_" + key, "Prefab/Quest/QuestNEW/QuestSeasonObject", Vector3.zero, Vector3.one, Vector3.zero);
			int timeLeftInSeason = player.getTimeLeftInSeason();
			string replaceValue = ((timeLeftInSeason >= 1) ? timeLeftInSeason.ToString() : "< 1");
			seasonTimeLeft = commonScreenObject.findChild(seasonQuestGiverObject, "QuestTimeLeft/TimeLeft").GetComponent<UILabel>();
			seasonTimeLeft.text = gameData.getTextByRefIdWithDynText("questHud01", "[NoDays]", replaceValue);
			num++;
		}
		List<QuestNEW> questNEWListByType2 = gameData.getQuestNEWListByType(QuestNEWType.QuestNEWTypeBlueprint, ignoreLock: false);
		sortedBlueprintQuestList = gameData.sortQuestListByQuestGiver(questNEWListByType2);
		foreach (string key2 in sortedBlueprintQuestList.Keys)
		{
			GameObject item = commonScreenObject.createPrefab(questGrid.gameObject, num + "_Blueprint_" + key2, "Prefab/Quest/QuestNEW/QuestSeasonObject", Vector3.zero, Vector3.one, Vector3.zero);
			num++;
			normalAndBlueprintQuestObjectList.Add(item);
		}
		List<QuestNEW> questNEWListByType3 = gameData.getQuestNEWListByType(QuestNEWType.QuestNEWTypeNormal, ignoreLock: false, addChallengeQuest: true);
		sortedNormalQuestList = gameData.sortQuestListByQuestGiver(questNEWListByType3);
		foreach (string key3 in sortedNormalQuestList.Keys)
		{
			GameObject item2 = commonScreenObject.createPrefab(questGrid.gameObject, num + "_Normal_" + key3, "Prefab/Quest/QuestNEW/QuestObject", Vector3.zero, Vector3.one, Vector3.zero);
			num++;
			normalAndBlueprintQuestObjectList.Add(item2);
		}
		questGrid.Reposition();
	}

	private void reset()
	{
		if (seasonQuestGiverObject != null)
		{
			commonScreenObject.destroyPrefabImmediate(seasonQuestGiverObject);
			seasonTimeLeft = null;
		}
		if (normalAndBlueprintQuestObjectList.Count <= 0)
		{
			return;
		}
		foreach (GameObject normalAndBlueprintQuestObject in normalAndBlueprintQuestObjectList)
		{
			commonScreenObject.destroyPrefabImmediate(normalAndBlueprintQuestObject);
		}
		normalAndBlueprintQuestObjectList.Clear();
	}

	public void refreshSeasonTimeLeft()
	{
		if (seasonQuestGiverObject != null && seasonTimeLeft != null)
		{
			GameData gameData = game.getGameData();
			Player player = game.getPlayer();
			int timeLeftInSeason = player.getTimeLeftInSeason();
			string replaceValue = ((timeLeftInSeason >= 1) ? timeLeftInSeason.ToString() : "< 1");
			commonScreenObject.findChild(seasonQuestGiverObject, "QuestTimeLeft/TimeLeft").GetComponent<UILabel>().text = gameData.getTextByRefIdWithDynText("questHud01", "[NoDays]", replaceValue);
		}
	}

	private void selectQuestGiver(string gameObjectName)
	{
		Player player = game.getPlayer();
		GameObject aParent = GameObject.Find(gameObjectName);
		string[] array = gameObjectName.Split('_');
		int num = CommonAPI.parseInt(array[0]);
		string text = array[1];
		string key = array[2];
		if (text != null && text == "Season")
		{
			currOpenedQuestObject = commonScreenObject.createPrefab(aParent, "QuestObjectDetail", "Prefab/Quest/QuestNEW/SeasonQuestObjectDetail", Vector3.zero, Vector3.one, Vector3.zero);
			GameObject gameObject = commonScreenObject.findChild(currOpenedQuestObject, "QuestDetailContainer/SeasonalObj").gameObject;
		}
		else
		{
			currOpenedQuestObject = commonScreenObject.createPrefab(aParent, "QuestObjectDetail", "Prefab/Quest/QuestNEW/QuestObjectDetail", Vector3.zero, Vector3.one, Vector3.zero);
		}
		questDetailContainer = commonScreenObject.findChild(currOpenedQuestObject, "QuestDetailContainer").gameObject;
		questDetailGrid = commonScreenObject.findChild(questDetailContainer, "QuestDetailGrid").GetComponent<UIGrid>();
		questDetailCenter = commonScreenObject.findChild(questDetailContainer, "QuestDetailCenter").GetComponent<UISprite>();
		questDetailBottom = commonScreenObject.findChild(questDetailContainer, "QuestDetailBottom").gameObject;
		List<QuestNEW> list = new List<QuestNEW>();
		switch (text)
		{
		case "Season":
			list = sortedSeasonQuestList[key];
			break;
		case "Blueprint":
			list = sortedBlueprintQuestList[key];
			break;
		case "Normal":
			list = sortedNormalQuestList[key];
			break;
		}
		if (shopMenuController == null)
		{
			shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		}
		int num2 = 0;
		foreach (QuestNEW item in list)
		{
			GameObject aObject = commonScreenObject.createPrefab(questDetailGrid.gameObject, gameObjectName + "_" + num2, "Prefab/Quest/QuestNEW/QuestSelectObject", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(aObject, "QuestDifficultyLabel").GetComponent<UILabel>().text = game.getGameData().getTextByRefId("questSelect04");
			commonScreenObject.findChild(aObject, "QuestDetailTitle").GetComponent<UILabel>().text = item.getQuestName();
			commonScreenObject.findChild(aObject, "QuestDetailRewardBg/QuestDetailReward").GetComponent<UILabel>().text = item.getRewardGold().ToString();
			commonScreenObject.findChild(aObject, "QuestDifficulty").GetComponent<UILabel>().text = shopMenuController.getDifficultyString(ProjectType.ProjectTypeWeapon, item.getAtkReq(), item.getSpdReq(), item.getAccReq(), item.getMagReq(), item.getForgeTimeLimit());
			commonScreenObject.findChild(aObject, "QuestDifficulty").GetComponent<UILabel>().color = shopMenuController.getQuestDifficultyColorUI(ProjectType.ProjectTypeWeapon, item.getAtkReq(), item.getSpdReq(), item.getAccReq(), item.getMagReq(), item.getForgeTimeLimit());
			num2++;
		}
		questDetailGrid.Reposition();
		float num3 = (float)(list.Count - 1) * questDetailGrid.cellHeight;
		int height = questDetailCenter.height;
		height += (int)num3;
		questDetailCenter.height = height;
		Vector3 localPosition = questDetailBottom.transform.localPosition;
		localPosition.y -= num3;
		questDetailBottom.transform.localPosition = localPosition;
		if (list.Count > 1 && num > 1)
		{
			Vector3 localPosition2 = questDetailContainer.transform.localPosition;
			localPosition2.y += (float)(list.Count / 2) * questGrid.cellHeight;
			questDetailContainer.transform.localPosition = localPosition2;
		}
	}

	private void startForging(string gameObjectName)
	{
		string[] array = gameObjectName.Split('_');
		int num = CommonAPI.parseInt(array[0]);
		string text = array[1];
		string key = array[2];
		int index = CommonAPI.parseInt(array[3]);
		if (shopMenuController == null)
		{
			shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		}
		commonScreenObject.destroyPrefabImmediate(currOpenedQuestObject);
		QuestNEW lastSelectQuest = null;
		switch (text)
		{
		case "Season":
			lastSelectQuest = sortedSeasonQuestList[key][index];
			break;
		case "Blueprint":
			lastSelectQuest = sortedBlueprintQuestList[key][index];
			break;
		case "Normal":
			lastSelectQuest = sortedNormalQuestList[key][index];
			break;
		}
		game.getPlayer().setLastSelectQuest(lastSelectQuest);
		viewController.showForgeMenuNewPopup();
	}
}
