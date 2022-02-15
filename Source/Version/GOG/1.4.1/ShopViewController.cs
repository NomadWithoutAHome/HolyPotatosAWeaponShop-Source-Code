using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopViewController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private bool debugMode;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private GUIProgressHUDController progressHUDCtrl;

	private GameObject topMenuNew;

	private GameObject Panel_Whetsapp;

	private GameObject Panel_GeneralItemGet;

	private GameObject Panel_ObjectiveComplete;

	private GameObject tooltipObj;

	private GUISmithListMenuController smithListMenuCtrl;

	public GameState gameState;

	private GameState prevGameState;

	private int gameTime;

	private List<int> previousProjectStatsList;

	private float speed;

	private bool isAutosaveLoad;

	private void Start()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = commonScreenObject.getController("ViewController").GetComponent<ViewController>();
		shopMenuController = commonScreenObject.getController("ShopMenuController").GetComponent<ShopMenuController>();
		progressHUDCtrl = GameObject.Find("Grid_topRight").GetComponent<GUIProgressHUDController>();
		topMenuNew = GameObject.Find("Panel_TopLeftMenu");
		Panel_Whetsapp = GameObject.Find("Panel_Whetsapp");
		Panel_GeneralItemGet = GameObject.Find("Panel_GeneralItemGet");
		Panel_ObjectiveComplete = GameObject.Find("Panel_ObjectiveComplete");
		tooltipObj = GameObject.Find("SmithInfoBubble");
		smithListMenuCtrl = GameObject.Find("Panel_SmithList").GetComponent<GUISmithListMenuController>();
		debugMode = false;
		gameState = GameState.GameStatePopEvent;
		prevGameState = GameState.GameStateIdle;
		gameTime = 0;
		previousProjectStatsList = new List<int>();
		speed = 1f;
		isAutosaveLoad = false;
		shopMenuController.showStartMenu();
	}

	public void toggleDebug()
	{
		debugMode = !debugMode;
		showTimeStatus();
	}

	public void returnToShopView()
	{
		gameState = GameState.GameStateIdle;
	}

	public bool showTimeStatus(bool doChecks = false)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		switch (player.getCurrentProject().getProjectType())
		{
		case ProjectType.ProjectTypeContract:
			progressHUDCtrl.refreshStats();
			break;
		case ProjectType.ProjectTypeResearch:
			progressHUDCtrl.refreshStats();
			break;
		case ProjectType.ProjectTypeUnique:
		{
			GameObject gameObject = GameObject.Find("Panel_ProjectProgress");
			if (gameObject != null)
			{
				gameObject.GetComponent<GUIProjectProgressController>().refreshStats();
			}
			break;
		}
		case ProjectType.ProjectTypeWeapon:
			progressHUDCtrl.refreshStats();
			break;
		}
		topMenuNew.GetComponent<GUITopMenuNewController>().refreshPlayerStats();
		GameObject gameObject2 = GameObject.Find("AreaEventHUD");
		if (gameObject2 != null)
		{
			gameObject2.GetComponent<GUIAreaEventHUDController>().refreshAreaEventHUD();
		}
		if (doChecks)
		{
			GameObject gameObject3 = GameObject.Find("Objective");
			if (gameObject3 != null)
			{
				gameObject3.GetComponent<GUIObjectiveController>().refreshObjectiveVariables();
			}
			GameObject gameObject4 = GameObject.Find("Panel_RequestList");
			if (gameObject4 != null)
			{
				gameObject4.GetComponent<GUIRequestListController>().updateRequests(hasTimeElapse: true);
			}
			Panel_Whetsapp.GetComponent<GUIWhetsappController>().updateWhetsappDisplay(forceScroll: false);
			Panel_GeneralItemGet.GetComponent<GUIItemGetPopupController>().showItemGet();
			Panel_ObjectiveComplete.GetComponent<GUIObjectiveCompletePopupController>().showObjectiveComplete();
			TooltipTextScript component = tooltipObj.GetComponent<TooltipTextScript>();
			if (component.getMenuActive())
			{
				component.refreshText();
			}
			smithListMenuCtrl.refreshSmithActionProgress();
		}
		if (viewController.checkMaskEnabled())
		{
			return true;
		}
		return false;
	}

	public void resetPreviousProjectStatsList()
	{
		previousProjectStatsList = new List<int>();
	}

	public void showDisplayByState()
	{
		switch (gameState)
		{
		case GameState.GameStateMenu:
			shopMenuController.showMenuByState();
			break;
		case GameState.GameStatePopEvent:
			shopMenuController.showPopEvent(PopEventType.PopEventTypeNothing);
			break;
		}
	}

	public void replaceGameState(GameState aNewState)
	{
		if (gameState != aNewState)
		{
			prevGameState = gameState;
			gameState = aNewState;
		}
	}

	public void restorePrevGameState()
	{
		gameState = prevGameState;
		prevGameState = GameState.GameStateIdle;
	}

	public void setGameState(GameState aState)
	{
		gameState = aState;
	}

	public void debugSpeedMeter(float modSpeed)
	{
		speed += modSpeed;
		if (speed < 0.1f)
		{
			speed = 0.1f;
		}
	}

	public float getGameSpeed()
	{
		return speed;
	}

	public void setGameSpeed(float aSpeed)
	{
		speed = aSpeed;
	}

	public void startGameTimer()
	{
		CommonAPI.debug("startgametimer");
		StopCoroutine("gameTimer");
		StartCoroutine("gameTimer");
	}

	public void setIsAutosaveLoad()
	{
		isAutosaveLoad = true;
	}

	public IEnumerator gameTimer()
	{
		while (true)
		{
			if (gameState == GameState.GameStateIdle)
			{
				Player player = game.getPlayer();
				bool flag = player.addTimeLong(2);
				showTimeStatus(doChecks: true);
				gameTime++;
				if (flag || isAutosaveLoad)
				{
					shopMenuController.checkStartOfDayActionList(isAutosaveLoad);
					isAutosaveLoad = false;
				}
				bool flag2 = viewController.getHasPopup();
				if (!flag2)
				{
					flag2 = shopMenuController.checkEventTimers();
				}
				if (!flag2)
				{
					flag2 = shopMenuController.checkUnlocks();
				}
				if (!flag2 && !shopMenuController.checkTimedActionList())
				{
					shopMenuController.checkProjectStatus();
				}
			}
			yield return new WaitForSeconds(speed);
		}
	}
}
