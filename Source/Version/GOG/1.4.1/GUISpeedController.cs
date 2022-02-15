using UnityEngine;

public class GUISpeedController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private GUICharacterAnimationController charAnimCtr;

	private ShopViewController shopViewController;

	private ViewController viewController;

	private CommonScreenObject commonScreenObject;

	private UISprite spd_0;

	private UISprite spd_1;

	private UISprite spd_2;

	private UISprite spd_3;

	private int lastSelectSpeed;

	private int currentSpeed;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		spd_0 = commonScreenObject.findChild(base.gameObject, "Spd_0").GetComponent<UISprite>();
		spd_1 = commonScreenObject.findChild(base.gameObject, "Spd_1").GetComponent<UISprite>();
		spd_2 = commonScreenObject.findChild(base.gameObject, "Spd_2").GetComponent<UISprite>();
		spd_3 = commonScreenObject.findChild(base.gameObject, "Spd_3").GetComponent<UISprite>();
		lastSelectSpeed = 1;
		currentSpeed = 1;
		showButtons(lastSelectSpeed);
	}

	private void Update()
	{
		if (viewController == null)
		{
			viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		}
		if (viewController != null && viewController.getGameStarted())
		{
			handleInput();
		}
	}

	private void handleInput()
	{
		if (Input.GetKeyDown(gameData.getKeyCodeByRefID("100009")))
		{
			if (!viewController.getHasPopup() && checkKeyboardControlsAllowed())
			{
				processClick("Key_1");
			}
		}
		else if (Input.GetKeyDown(gameData.getKeyCodeByRefID("100010")))
		{
			if (!viewController.getHasPopup() && checkKeyboardControlsAllowed())
			{
				processClick("Key_2");
			}
		}
		else if (Input.GetKeyDown(gameData.getKeyCodeByRefID("100011")))
		{
			if (!viewController.getHasPopup() && checkKeyboardControlsAllowed())
			{
				processClick("Key_3");
			}
		}
		else if (Input.GetKeyDown(gameData.getKeyCodeByRefID("100012")))
		{
			if (!viewController.getHasPopup() && checkKeyboardControlsAllowed())
			{
				processClick("Key_0");
			}
		}
		else if (Input.GetKeyDown(gameData.getKeyCodeByRefID("100013")) && !viewController.getHasPopup() && checkKeyboardControlsAllowed())
		{
			if (currentSpeed == 0)
			{
				processClick("Key_" + lastSelectSpeed);
			}
			else
			{
				processClick("Key_0");
			}
		}
	}

	private bool checkKeyboardControlsAllowed()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		int completedTutorialIndex = game.getPlayer().getCompletedTutorialIndex();
		if (completedTutorialIndex >= game.getGameData().getTutorialSetOrderIndex("SELL3"))
		{
			return true;
		}
		return false;
	}

	public void processClick(string gameObjectName)
	{
		string[] array = gameObjectName.Split('_');
		switch (array[1])
		{
		case "0":
			showButtons(0);
			doPause();
			break;
		case "1":
			lastSelectSpeed = 1;
			showButtons(1);
			setSpeed(1f, 1f);
			doUnpause();
			break;
		case "2":
			lastSelectSpeed = 2;
			showButtons(2);
			setSpeed(1.43f, 0.7f);
			doUnpause();
			break;
		case "3":
			lastSelectSpeed = 3;
			showButtons(3);
			setSpeed(3.33f, 0.3f);
			doUnpause();
			break;
		}
	}

	private void setVariables()
	{
		if (charAnimCtr == null)
		{
			charAnimCtr = GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>();
		}
		if (shopViewController == null)
		{
			shopViewController = GameObject.Find("ShopViewController").GetComponent<ShopViewController>();
		}
		if (viewController == null)
		{
			viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		}
	}

	private void setSpeed(float spdMultiplier, float timerSpd)
	{
		setVariables();
		charAnimCtr.setSpdMultiplier(spdMultiplier);
		shopViewController.setGameSpeed(timerSpd);
	}

	private void doPause()
	{
		setVariables();
		viewController.doPlayerPause();
	}

	private void doUnpause()
	{
		setVariables();
		viewController.doPlayerUnpause();
	}

	public void revertLastButtonState()
	{
		showButtons(lastSelectSpeed);
	}

	private void showButtons(int showState)
	{
		currentSpeed = showState;
		switch (showState)
		{
		case 0:
			spd_0.spriteName = "pause_selected";
			spd_1.spriteName = "x1_normal";
			spd_2.spriteName = "x2_normal";
			spd_3.spriteName = "x3_normal";
			break;
		case 1:
			spd_0.spriteName = "pause_normal";
			spd_1.spriteName = "x1_selected";
			spd_2.spriteName = "x2_normal";
			spd_3.spriteName = "x3_normal";
			break;
		case 2:
			spd_0.spriteName = "pause_normal";
			spd_1.spriteName = "x1_normal";
			spd_2.spriteName = "x2_selected";
			spd_3.spriteName = "x3_normal";
			break;
		case 3:
			spd_0.spriteName = "pause_normal";
			spd_1.spriteName = "x1_normal";
			spd_2.spriteName = "x2_normal";
			spd_3.spriteName = "x3_selected";
			break;
		}
	}
}
