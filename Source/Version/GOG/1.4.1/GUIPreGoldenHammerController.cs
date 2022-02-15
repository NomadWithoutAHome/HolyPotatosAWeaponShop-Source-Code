using UnityEngine;

public class GUIPreGoldenHammerController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private UILabel preEventLabel;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		preEventLabel = commonScreenObject.findChild(base.gameObject, "PreEvent_text").GetComponent<UILabel>();
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName == "Close_button")
		{
			viewController.showGoldenHammerAwards();
			viewController.closeGoldenHammerPreEvent(resumeGame: false);
		}
	}

	public void setReference()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		commonScreenObject.findChild(base.gameObject, "PreEvent_text/Close_button").GetComponentInChildren<UILabel>().text = game.getGameData().getTextByRefId("weaponAwardsLetsGo");
		string text = gameData.getTextByRefId("specialEvent0101");
		if (player.getPlayerYear() == 0)
		{
			text = text + "\n" + gameData.getTextByRefId("specialEvent0102");
		}
		text = text + "\n" + gameData.getTextByRefId("specialEvent0103");
		preEventLabel.text = text;
	}
}
