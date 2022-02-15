using UnityEngine;

public class GUIAnimationClickController : MonoBehaviour
{
	private Game game;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private StationController stationController;

	private GUICharacterAnimationController charAnimCtr;

	private TooltipTextScript tooltipScript;

	private Smith selectedSmith;

	private UILabel assignNoteLabel;

	private bool avatarAssign;

	private bool dogAssign;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		stationController = GameObject.Find("StationController").GetComponent<StationController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		charAnimCtr = GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>();
		assignNoteLabel = GameObject.Find("AssignNoteLabel").GetComponent<UILabel>();
		selectedSmith = null;
		avatarAssign = false;
		dogAssign = false;
	}

	public void processClick(GameObject clickObj)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		string text = clickObj.name;
		string[] array = text.Split('_');
		switch (array[0])
		{
		case "Avatar":
			avatarAssign = true;
			viewController.showPatataDogAssignMenu(patata: true);
			break;
		case "Dog":
			if (!player.checkRandomDog())
			{
				dogAssign = true;
				viewController.showPatataDogAssignMenu(patata: false);
			}
			break;
		case "CraftBoost":
		{
			bool resumeFromPlayerPause4 = setAvatarDogStation(SmithStation.SmithStationCraft);
			resetAssignValues();
			viewController.closePatataDogAssignMenu(resumeFromPlayerPause4);
			break;
		}
		case "DesignBoost":
		{
			bool resumeFromPlayerPause8 = setAvatarDogStation(SmithStation.SmithStationDesign);
			resetAssignValues();
			viewController.closePatataDogAssignMenu(resumeFromPlayerPause8);
			break;
		}
		case "EnchantBoost":
		{
			bool resumeFromPlayerPause5 = setAvatarDogStation(SmithStation.SmithStationEnchant);
			resetAssignValues();
			viewController.closePatataDogAssignMenu(resumeFromPlayerPause5);
			break;
		}
		case "PolishBoost":
		{
			bool resumeFromPlayerPause6 = setAvatarDogStation(SmithStation.SmithStationPolish);
			resetAssignValues();
			viewController.closePatataDogAssignMenu(resumeFromPlayerPause6);
			break;
		}
		case "Smith":
		{
			selectedSmith = gameData.getSmithByRefId(array[1]);
			bool flag6 = false;
			if (array[1] == "10001")
			{
				GameObject gameObject5 = GameObject.Find("Panel_Tutorial");
				if (gameObject5 != null)
				{
					GUITutorialController component5 = gameObject5.GetComponent<GUITutorialController>();
					flag6 = component5.checkCurrentTutorial("10004");
					if (flag6)
					{
						component5.nextTutorial();
					}
				}
			}
			GUIPathController component6 = clickObj.GetComponent<GUIPathController>();
			if (component6 != null)
			{
				component6.hidePopBubbles();
			}
			if (flag6)
			{
				viewController.showAssignSmithMenu(selectedSmith, exchangeable: true, tutorial: true);
			}
			else
			{
				viewController.showAssignSmithMenu(selectedSmith, exchangeable: true);
			}
			break;
		}
		case "Obstacle":
		{
			Obstacle obstacleByRefID = gameData.getObstacleByRefID(array[1]);
			if (obstacleByRefID.getFurnitureRefID() != "-1")
			{
				Furniture furnitureByRefId = gameData.getFurnitureByRefId(obstacleByRefID.getFurnitureRefID());
				if (furnitureByRefId.getFurnitureType() == "301" && !player.checkRandomDog() && dogAssign && (player.getDogStation() != SmithStation.SmithStationDogHome || player.getDogStation() != SmithStation.SmithStationBlank))
				{
					setAvatarDogStation(SmithStation.SmithStationDogHome);
					resetAssignValues();
					viewController.closePatataDogAssignMenu(resumeFromPlayerPause: true);
				}
				else if (furnitureByRefId.getFurnitureType() == "301" && !player.checkRandomDog() && !dogAssign && (player.getDogStation() == SmithStation.SmithStationDogHome || player.getDogStation() == SmithStation.SmithStationBlank))
				{
					dogAssign = true;
					viewController.showPatataDogAssignMenu(patata: false, player.getPlayerStation());
				}
			}
			break;
		}
		case "DesignStation":
			if (selectedSmith.checkSmithInShopOrStandby())
			{
				bool resumeFromPlayerPause3 = false;
				if (shopMenuController == null && GameObject.Find("ShopMenuController") != null)
				{
					shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
				}
				Smith smithByStationIndex3 = stationController.getDesignStation().getSmithByStationIndex(CommonAPI.parseInt(array[1]));
				if (smithByStationIndex3 == null)
				{
					resumeFromPlayerPause3 = true;
					shopMenuController.doSmithReassign(selectedSmith, SmithStation.SmithStationDesign, CommonAPI.parseInt(array[1]));
				}
				else
				{
					if (selectedSmith.getSmithRefId() != smithByStationIndex3.getSmithRefId())
					{
						resumeFromPlayerPause3 = true;
					}
					shopMenuController.doSmithReassign(selectedSmith, SmithStation.SmithStationDesign, CommonAPI.parseInt(array[1]), smithByStationIndex3);
				}
				if (charAnimCtr.getSmithObject(selectedSmith) == null || !selectedSmith.checkSmithInShop())
				{
					resumeFromPlayerPause3 = true;
					returnSmithToShop();
				}
				assignNoteLabel.text = string.Empty;
				bool flag3 = false;
				bool flag4 = false;
				GameObject gameObject3 = GameObject.Find("Panel_Tutorial");
				if (gameObject3 != null)
				{
					GUITutorialController component3 = gameObject3.GetComponent<GUITutorialController>();
					flag3 = component3.checkCurrentTutorial("10007");
					flag4 = component3.checkCurrentTutorial("50003");
					if (flag3 || flag4)
					{
						component3.nextTutorial();
					}
				}
				closeSmithActionMenu(resume: true, resumeFromPlayerPause3, flag3);
			}
			else
			{
				assignNoteLabel.text = game.getGameData().getTextByRefId("assignSmith08");
			}
			break;
		case "CraftStation":
			if (selectedSmith.checkSmithInShopOrStandby())
			{
				bool resumeFromPlayerPause2 = false;
				if (shopMenuController == null && GameObject.Find("ShopMenuController") != null)
				{
					shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
				}
				Smith smithByStationIndex2 = stationController.getCraftStation().getSmithByStationIndex(CommonAPI.parseInt(array[1]));
				if (smithByStationIndex2 == null)
				{
					resumeFromPlayerPause2 = true;
					shopMenuController.doSmithReassign(selectedSmith, SmithStation.SmithStationCraft, CommonAPI.parseInt(array[1]));
				}
				else
				{
					if (selectedSmith.getSmithRefId() != smithByStationIndex2.getSmithRefId())
					{
						resumeFromPlayerPause2 = true;
					}
					shopMenuController.doSmithReassign(selectedSmith, SmithStation.SmithStationCraft, CommonAPI.parseInt(array[1]), smithByStationIndex2);
				}
				if (charAnimCtr.getSmithObject(selectedSmith) == null || !selectedSmith.checkSmithInShop())
				{
					resumeFromPlayerPause2 = true;
					returnSmithToShop();
				}
				assignNoteLabel.text = string.Empty;
				bool flag2 = false;
				GameObject gameObject2 = GameObject.Find("Panel_Tutorial");
				if (gameObject2 != null)
				{
					GUITutorialController component2 = gameObject2.GetComponent<GUITutorialController>();
					if (component2.checkCurrentTutorial("50003"))
					{
						component2.nextTutorial();
					}
				}
				closeSmithActionMenu(resume: true, resumeFromPlayerPause2);
			}
			else
			{
				assignNoteLabel.text = game.getGameData().getTextByRefId("assignSmith08");
			}
			break;
		case "PolishStation":
			if (selectedSmith.checkSmithInShopOrStandby())
			{
				bool resumeFromPlayerPause7 = false;
				if (shopMenuController == null && GameObject.Find("ShopMenuController") != null)
				{
					shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
				}
				Smith smithByStationIndex4 = stationController.getPolishStation().getSmithByStationIndex(CommonAPI.parseInt(array[1]));
				if (smithByStationIndex4 == null)
				{
					resumeFromPlayerPause7 = true;
					shopMenuController.doSmithReassign(selectedSmith, SmithStation.SmithStationPolish, CommonAPI.parseInt(array[1]));
				}
				else
				{
					if (selectedSmith.getSmithRefId() != smithByStationIndex4.getSmithRefId())
					{
						resumeFromPlayerPause7 = true;
					}
					shopMenuController.doSmithReassign(selectedSmith, SmithStation.SmithStationPolish, CommonAPI.parseInt(array[1]), smithByStationIndex4);
				}
				if (charAnimCtr.getSmithObject(selectedSmith) == null || !selectedSmith.checkSmithInShop())
				{
					resumeFromPlayerPause7 = true;
					returnSmithToShop();
				}
				assignNoteLabel.text = string.Empty;
				bool flag5 = false;
				GameObject gameObject4 = GameObject.Find("Panel_Tutorial");
				if (gameObject4 != null)
				{
					GUITutorialController component4 = gameObject4.GetComponent<GUITutorialController>();
					if (component4.checkCurrentTutorial("50003"))
					{
						component4.nextTutorial();
					}
				}
				closeSmithActionMenu(resume: true, resumeFromPlayerPause7);
			}
			else
			{
				assignNoteLabel.text = game.getGameData().getTextByRefId("assignSmith08");
			}
			break;
		case "EnchantStation":
			if (selectedSmith.checkSmithInShopOrStandby())
			{
				bool resumeFromPlayerPause = false;
				if (shopMenuController == null && GameObject.Find("ShopMenuController") != null)
				{
					shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
				}
				Smith smithByStationIndex = stationController.getEnchantStation().getSmithByStationIndex(CommonAPI.parseInt(array[1]));
				if (smithByStationIndex == null)
				{
					resumeFromPlayerPause = true;
					shopMenuController.doSmithReassign(selectedSmith, SmithStation.SmithStationEnchant, CommonAPI.parseInt(array[1]));
				}
				else
				{
					if (selectedSmith.getSmithRefId() != smithByStationIndex.getSmithRefId())
					{
						resumeFromPlayerPause = true;
					}
					shopMenuController.doSmithReassign(selectedSmith, SmithStation.SmithStationEnchant, CommonAPI.parseInt(array[1]), smithByStationIndex);
				}
				if (charAnimCtr.getSmithObject(selectedSmith) == null || !selectedSmith.checkSmithInShop())
				{
					resumeFromPlayerPause = true;
					returnSmithToShop();
				}
				assignNoteLabel.text = string.Empty;
				bool flag = false;
				GameObject gameObject = GameObject.Find("Panel_Tutorial");
				if (gameObject != null)
				{
					GUITutorialController component = gameObject.GetComponent<GUITutorialController>();
					if (component.checkCurrentTutorial("50003"))
					{
						component.nextTutorial();
					}
				}
				closeSmithActionMenu(resume: true, resumeFromPlayerPause);
			}
			else
			{
				assignNoteLabel.text = game.getGameData().getTextByRefId("assignSmith08");
			}
			break;
		case "StandbyButton":
		{
			SmithAction smithActionByRefId = game.getGameData().getSmithActionByRefId("905");
			selectedSmith.setSmithAction(smithActionByRefId, -1);
			assignNoteLabel.text = string.Empty;
			closeSmithActionMenu();
			break;
		}
		}
	}

	public void processStationHover(bool isOver, string gameObjectName)
	{
		if (isOver)
		{
			GameData gameData = game.getGameData();
			string[] array = gameObjectName.Split('_');
			string text = string.Empty;
			switch (array[0])
			{
			case "DesignStation":
				text = gameData.getTextByRefId("StationDesc01");
				break;
			case "CraftStation":
				text = gameData.getTextByRefId("StationDesc02");
				break;
			case "PolishStation":
				text = gameData.getTextByRefId("StationDesc03");
				break;
			case "EnchantStation":
				text = gameData.getTextByRefId("StationDesc04");
				break;
			}
			if (text != string.Empty)
			{
				tooltipScript.showText(text);
			}
		}
		else
		{
			tooltipScript.setInactive();
		}
	}

	public void closeSmithActionMenu(bool resume = true, bool resumeFromPlayerPause = true, bool tutorial = false)
	{
		viewController.closeAssignSmithMenu(selectedSmith, hide: true, resume, resumeFromPlayerPause, tutorial);
		selectedSmith = null;
	}

	public Smith getSelectedSmith()
	{
		return selectedSmith;
	}

	public void setSelectedSmith(Smith aSmith)
	{
		selectedSmith = aSmith;
	}

	public bool getAvatarAssign()
	{
		return avatarAssign;
	}

	public void setAvatarAssign(bool aBool)
	{
		avatarAssign = aBool;
	}

	public bool getDogAssign()
	{
		return dogAssign;
	}

	public void setDogAssign(bool aBool)
	{
		dogAssign = aBool;
	}

	public void resetAssignValues()
	{
		avatarAssign = false;
		dogAssign = false;
	}

	private void returnSmithToShop()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		bool flag = player.checkHasProject();
		selectedSmith.returnToShopStandby();
		SmithAction smithActionByRefId = gameData.getSmithActionByRefId("101");
		if (flag)
		{
			smithActionByRefId = gameData.getSmithActionByRefId("102");
		}
		selectedSmith.setSmithAction(smithActionByRefId, -1);
		player.updateSmithStations();
		stationController.assignSmithStations();
		if (charAnimCtr.getSmithObject(selectedSmith) == null)
		{
			charAnimCtr.spawnCharacterFrmheaven(selectedSmith.getSmithRefId());
		}
	}

	private bool setAvatarDogStation(SmithStation aStation)
	{
		Player player = game.getPlayer();
		SmithStation dogStation = player.getDogStation();
		SmithStation playerStation = player.getPlayerStation();
		bool flag = false;
		if (avatarAssign)
		{
			CommonAPI.debug(string.Concat("playerStation ", playerStation, " aStation ", aStation));
			if (playerStation != aStation)
			{
				flag = true;
			}
			if (dogStation == aStation)
			{
				player.setDogStation(player.getPlayerStation());
			}
			player.setPlayerStation(aStation);
		}
		else if (dogAssign)
		{
			CommonAPI.debug(string.Concat("dogStation ", dogStation, " aStation ", aStation));
			if (dogStation != aStation)
			{
				flag = true;
			}
			if (playerStation == aStation)
			{
				player.setPlayerStation(player.getDogStation());
			}
			player.setDogStation(aStation);
		}
		CommonAPI.debug("hasAssign " + flag);
		return flag;
	}
}
