using System.Globalization;
using UnityEngine;

public class GUIContractCompleteController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private Contract contract;

	private UILabel contractNameLabel;

	private UILabel contractDescriptionLabel;

	private UILabel rewardGoldLabel;

	private UILabel contractTitleLabel;

	private UILabel completeLabel;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		contractNameLabel = commonScreenObject.findChild(base.gameObject, "ContractDisplay/ContractName").GetComponent<UILabel>();
		contractDescriptionLabel = commonScreenObject.findChild(base.gameObject, "ContractDisplay/ContractDesc").GetComponent<UILabel>();
		rewardGoldLabel = commonScreenObject.findChild(base.gameObject, "ContractDisplay/ContractReward_bg/ContractReward_label").GetComponent<UILabel>();
		contractTitleLabel = commonScreenObject.findChild(base.gameObject, "ContractCompleteTitle_bg/ContractCompleteTitle_label").GetComponent<UILabel>();
		completeLabel = commonScreenObject.findChild(base.gameObject, "ContractDisplay/ContractComplete_labelBg/ContractComplete_label").GetComponent<UILabel>();
		contractTitleLabel.text = gameData.getTextByRefId("forgeBottomMenu02").ToUpper(CultureInfo.InvariantCulture);
		completeLabel.text = gameData.getTextByRefId("contractResult01").ToUpper(CultureInfo.InvariantCulture);
		commonScreenObject.findChild(base.gameObject, "Ok_button/Ok_label").GetComponent<UILabel>().text = gameData.getTextByRefId("menuGeneral04").ToUpper(CultureInfo.InvariantCulture);
	}

	public void processClick(string gameobjectName)
	{
		switch (gameobjectName)
		{
		case "Close_button":
			viewController.closeContractComplete();
			break;
		case "Ok_button":
			viewController.closeContractComplete();
			break;
		}
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
		if (Input.GetKeyDown(gameData.getKeyCodeByRefID("100008")))
		{
			processClick("Ok_button");
		}
	}

	public void setReference(Contract aContract)
	{
		contract = aContract;
		contractNameLabel.text = contract.getContractName();
		contractDescriptionLabel.text = contract.getContractDesc();
		rewardGoldLabel.text = "$" + CommonAPI.formatNumber(contract.getGold());
	}
}
