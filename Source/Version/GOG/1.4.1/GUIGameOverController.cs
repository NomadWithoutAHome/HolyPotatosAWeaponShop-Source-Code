using System.Collections;
using UnityEngine;

public class GUIGameOverController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private UIButton loadButton;

	private UIButton menuButton;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		loadButton = commonScreenObject.findChild(base.gameObject, "GameOver_bg/GameOver_load").GetComponent<UIButton>();
		loadButton.isEnabled = false;
		menuButton = commonScreenObject.findChild(base.gameObject, "GameOver_bg/GameOver_mainMenu").GetComponent<UIButton>();
		menuButton.isEnabled = false;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "GameOver_load":
			viewController.showSaveLoadPopup(save: false, game.getPlayer().getGameScenario(), fromStart: false);
			break;
		case "GameOver_mainMenu":
			Application.LoadLevel("ALLNGUIMENU");
			break;
		}
	}

	public void setReference(string imagePath, string labelText)
	{
		GameData gameData = game.getGameData();
		audioController.changeBGM("cutscene_nostalgic");
		UILabel[] componentsInChildren = base.gameObject.GetComponentsInChildren<UILabel>();
		foreach (UILabel uILabel in componentsInChildren)
		{
			switch (uILabel.name)
			{
			case "GameOver_label":
				uILabel.text = labelText;
				break;
			case "GameOverLoad_label":
				uILabel.text = gameData.getTextByRefId("settings09");
				break;
			case "GameOverMainMenu_label":
				uILabel.text = gameData.getTextByRefId("gameOver01");
				break;
			}
		}
		UITexture component = commonScreenObject.findChild(base.gameObject, "GameOver_bg/GameOver_texture").GetComponent<UITexture>();
		component.mainTexture = commonScreenObject.loadTexture(imagePath);
		StartCoroutine(gameOverAnims());
	}

	public IEnumerator gameOverAnims()
	{
		yield return new WaitForSeconds(1.4f);
		loadButton.isEnabled = true;
		menuButton.isEnabled = true;
	}
}
