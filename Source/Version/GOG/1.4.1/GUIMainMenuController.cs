using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIMainMenuController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private UILabel titleLabel;

	private MenuState currMenuState;

	private int selectedMenuIndex;

	private string menuPrefix;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		titleLabel = commonScreenObject.findChild(base.gameObject, "HeaderFrame/TitleLabel").GetComponent<UILabel>();
		selectedMenuIndex = -1;
		menuPrefix = "Menu_";
	}

	public void processClick(string gameObjectName)
	{
		string[] array = gameObjectName.Split('_');
		int num = CommonAPI.parseInt(array[1]);
		if (num != selectedMenuIndex)
		{
			if (selectedMenuIndex != -1)
			{
				GameObject.Find(menuPrefix + selectedMenuIndex).GetComponent<UISprite>().spriteName = "button-unselected";
			}
			selectedMenuIndex = num;
			GameObject.Find(menuPrefix + selectedMenuIndex).GetComponent<UISprite>().spriteName = "button-selected";
		}
		switch (currMenuState)
		{
		case MenuState.MenuStateForgeMain:
			if (game.getPlayer().getCurrentProject().getProjectType() == ProjectType.ProjectTypeNothing)
			{
				switch (num)
				{
				case 0:
					viewController.closeTier2Menu(hide: false);
					shopMenuController.showForgeJobClassMenu(-1, 100);
					break;
				case 1:
					viewController.closeTier2Menu(hide: false);
					shopMenuController.showContractMenu();
					break;
				case 2:
					break;
				case 3:
					viewController.closeTier2Menu(hide: false);
					shopMenuController.showResearchMainMenu(100);
					break;
				}
			}
			else
			{
				GameData gameData = game.getGameData();
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: false, gameData.getTextByRefId("errorTitle01") + " " + gameData.getTextByRefId("menuForgeConfirm05"), null, PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
			}
			break;
		case MenuState.MenuStateSmithMain:
			switch (num)
			{
			case 0:
				viewController.closeMainMenu(resume: false);
				viewController.closeTier2Menu(hide: false);
				viewController.showSmithManage(PopupType.PopupTypeManageSmith);
				break;
			case 1:
				viewController.showTier2Menu(MenuState.MenuStateSmithHire);
				break;
			}
			break;
		case MenuState.MenuStateInventory:
			switch (num)
			{
			case 0:
				viewController.showTier2Menu(MenuState.MenuStateShopBuyMenu);
				break;
			case 1:
				shopMenuController.showSellMenu(100);
				break;
			}
			break;
		}
	}

	public void setReference(MenuState aMenuState)
	{
		GameData gameData = game.getGameData();
		GameObject gameObject = commonScreenObject.findChild(base.gameObject, "Panel_MainMenuDraggablePanel/MainMenu_Grid").gameObject;
		currMenuState = aMenuState;
		List<string> list = new List<string>();
		switch (currMenuState)
		{
		case MenuState.MenuStateForgeMain:
			titleLabel.text = gameData.getTextByRefId("menuMain01").ToUpper(CultureInfo.InvariantCulture);
			list.Add(gameData.getTextByRefId("menuForge01"));
			list.Add(gameData.getTextByRefId("menuForge02"));
			list.Add(gameData.getTextByRefId("menuForge03"));
			break;
		case MenuState.MenuStateSmithMain:
			titleLabel.text = gameData.getTextByRefId("menuMain02").ToUpper(CultureInfo.InvariantCulture);
			list.Add(gameData.getTextByRefId("menuSmith06"));
			list.Add(gameData.getTextByRefId("menuSmith02"));
			break;
		case MenuState.MenuStateInventory:
			titleLabel.text = gameData.getTextByRefId("menuMain04").ToUpper(CultureInfo.InvariantCulture);
			list.Add(gameData.getTextByRefId("menuShop05"));
			list.Add(gameData.getTextByRefId("menuShop06"));
			break;
		}
		for (int i = 0; i < list.Count; i++)
		{
			GameObject gameObject2 = commonScreenObject.createPrefab(gameObject, menuPrefix + i, "Prefab/Menu/Button_MainMenu", Vector3.zero, Vector3.one, Vector3.zero);
			gameObject2.GetComponentInChildren<UILabel>().text = list[i];
		}
		gameObject.GetComponent<UIGrid>().Reposition();
		gameObject.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
	}
}
