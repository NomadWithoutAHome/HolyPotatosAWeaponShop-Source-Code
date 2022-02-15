using System.Globalization;
using UnityEngine;

public class GUIBetaCompleteController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
	}

	public void processClick(string gameobjectName)
	{
		if (gameobjectName != null && gameobjectName == "Review_button")
		{
			Application.OpenURL("http://www.day-lightstudios.com/games/holy-potatoes-weapon-shop/beta-complete/");
			shopMenuController.restartGame();
		}
	}

	public void setReference()
	{
		GameData gameData = game.getGameData();
		audioController.stopBGMAudio();
		audioController.playForgeCompleteAudio();
		UILabel[] componentsInChildren = base.gameObject.GetComponentsInChildren<UILabel>();
		foreach (UILabel uILabel in componentsInChildren)
		{
			switch (uILabel.gameObject.name)
			{
			case "Congrats_label":
				gameData.getTextByRefId("betaComplete01").ToUpper(CultureInfo.InvariantCulture);
				break;
			case "Version_label":
				gameData.getTextByRefId("betaComplete02");
				break;
			case "BetaText_label":
				gameData.getTextByRefId("betaComplete03");
				break;
			case "Review_label":
				gameData.getTextByRefId("betaComplete04").ToUpper(CultureInfo.InvariantCulture);
				break;
			}
		}
	}
}
