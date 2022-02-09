using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIContractController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ShopMenuController shopMenuController;

	private ViewController viewController;

	private UIGrid contractListGrid;

	private List<Contract> contractList;

	private List<GameObject> contractObjectList;

	private UILabel contractTitleLabel;

	private UILabel atkValue;

	private UILabel accValue;

	private UILabel spdValue;

	private UILabel magValue;

	private UILabel durationLabel;

	private UIButton goButton;

	private int selectedContractIndex;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		contractListGrid = GameObject.Find("ContractList_grid").GetComponent<UIGrid>();
		contractObjectList = new List<GameObject>();
		contractTitleLabel = GameObject.Find("ContractTitle_label").GetComponent<UILabel>();
		selectedContractIndex = -1;
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName == "Close_button")
		{
			viewController.closeContract(hide: true, resumeFromPlayerPause: false);
			return;
		}
		string[] array = gameObjectName.Split('_');
		int index = CommonAPI.parseInt(array[1]);
		game.getPlayer().setLastSelectContract(contractList[index]);
		ShopMenuController component = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		component.startContract();
	}

	private void Update()
	{
		if (viewController != null && viewController.getIsPaused() && viewController.getGameStarted())
		{
			handleInput();
		}
	}

	private void handleInput()
	{
		if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(gameData.getKeyCodeByRefID("100007")))
		{
			processClick("Close_button");
		}
	}

	public void setReference()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		contractTitleLabel.text = gameData.getTextByRefId("forgeBottomMenu02").ToUpper(CultureInfo.InvariantCulture);
		contractList = player.getPlayerContractList(gameData.getContractList(player.getPlayerMonths()));
		int num = 0;
		foreach (Contract contract in contractList)
		{
			GameObject gameObject = commonScreenObject.createPrefab(contractListGrid.gameObject, "Contract_" + num, "Prefab/Contract/ContractListObj", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(gameObject, "ContractName").GetComponent<UILabel>().text = contract.getContractName();
			commonScreenObject.findChild(gameObject, "ContractDesc").GetComponent<UILabel>().text = contract.getContractDesc();
			commonScreenObject.findChild(gameObject, "ContractReward/RewardLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("questSelect02");
			commonScreenObject.findChild(gameObject, "ContractReward/RewardValue").GetComponent<UILabel>().text = "$" + CommonAPI.formatNumber(contract.getGold());
			commonScreenObject.findChild(gameObject, "ContractStat/atk_sprite/atk_label").GetComponent<UILabel>().text = CommonAPI.formatNumber(contract.getAtkReq());
			commonScreenObject.findChild(gameObject, "ContractStat/spd_sprite/spd_label").GetComponent<UILabel>().text = CommonAPI.formatNumber(contract.getSpdReq());
			commonScreenObject.findChild(gameObject, "ContractStat/acc_sprite/acc_label").GetComponent<UILabel>().text = CommonAPI.formatNumber(contract.getAccReq());
			commonScreenObject.findChild(gameObject, "ContractStat/mag_sprite/mag_label").GetComponent<UILabel>().text = CommonAPI.formatNumber(contract.getMagReq());
			commonScreenObject.findChild(gameObject, "TimeLimit/TimeLimitLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("menuResearch10");
			commonScreenObject.findChild(gameObject, "TimeLimit/TimeLimitValue").GetComponent<UILabel>().text = contract.getTimeLimitString();
			commonScreenObject.findChild(gameObject, "Difficulty/DifficultyValue").GetComponent<UILabel>().text = shopMenuController.getDifficultyString(ProjectType.ProjectTypeContract, contract.getAtkReq(), contract.getSpdReq(), contract.getAccReq(), contract.getMagReq(), contract.getTimeLimit()).ToUpper(CultureInfo.InvariantCulture);
			commonScreenObject.findChild(gameObject, "Difficulty").GetComponent<UISprite>().color = shopMenuController.getQuestDifficultyColorUI(ProjectType.ProjectTypeContract, contract.getAtkReq(), contract.getSpdReq(), contract.getAccReq(), contract.getMagReq(), contract.getTimeLimit());
			commonScreenObject.findChild(gameObject, "Start_Button/StartLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("questSelect11").ToUpper(CultureInfo.InvariantCulture);
			contractObjectList.Add(gameObject);
			num++;
		}
		contractListGrid.Reposition();
	}
}
