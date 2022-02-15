using System.Collections.Generic;
using UnityEngine;

public class GUITier2MenuController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private UILabel titleLabel;

	private List<QuestNEW> playerBlueprintList;

	private MenuState currMenuState;

	private string tier2Prefix;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		titleLabel = commonScreenObject.findChild(base.gameObject, "HeaderFrame/TitleLabel").GetComponent<UILabel>();
		tier2Prefix = "Tier2_";
	}

	public void processClick(string gameObjectName)
	{
		string[] array = gameObjectName.Split('_');
		int num = CommonAPI.parseInt(array[1]);
		switch (currMenuState)
		{
		case MenuState.MenuStateForgeBlueprint:
			viewController.closeMainMenu(resume: false);
			viewController.closeTier2Menu(hide: false);
			viewController.showMenuBlueprint(playerBlueprintList[num]);
			break;
		case MenuState.MenuStateShopBuyMenu:
			switch (num)
			{
			case 0:
				shopMenuController.showFurnitureShop(100);
				break;
			case 1:
				shopMenuController.showEnchantmentShop(100);
				break;
			}
			break;
		}
	}

	public void setReference(MenuState aMenuState)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		currMenuState = aMenuState;
		GameObject gameObject = commonScreenObject.findChild(base.gameObject, "Panel_MainMenuDraggablePanel/MainMenu_Grid").gameObject;
		switch (currMenuState)
		{
		case MenuState.MenuStateForgeBlueprint:
			titleLabel.text = gameData.getTextByRefId("menuBlueprint07");
			gameObject.GetComponent<UIGrid>().cellHeight = 70f;
			gameObject.transform.parent.localPosition -= new Vector3(0f, 10f, 0f);
			playerBlueprintList = gameData.getQuestNEWListByType(QuestNEWType.QuestNEWTypeBlueprint, ignoreLock: false);
			if (playerBlueprintList.Count > 0)
			{
				for (int j = 0; j < playerBlueprintList.Count; j++)
				{
					Weapon weaponByRefId = gameData.getWeaponByRefId(playerBlueprintList[j].getWeaponRefId());
					GameObject aObject2 = commonScreenObject.createPrefab(gameObject, tier2Prefix + j, "Prefab/Menu/Button_Tier2Menu", Vector3.zero, Vector3.one, Vector3.zero);
					commonScreenObject.findChild(aObject2, "Text").GetComponent<UILabel>().text = weaponByRefId.getWeaponName();
				}
			}
			break;
		case MenuState.MenuStateSmithHire:
		{
			titleLabel.text = gameData.getTextByRefId("menuSmith02");
			gameObject.GetComponent<UIGrid>().cellHeight = 90f;
			List<RecruitmentType> recruitmentTypeList = gameData.getRecruitmentTypeList(player.getShopLevelInt(), player.getPlayerMonths());
			for (int k = 0; k < recruitmentTypeList.Count; k++)
			{
				GameObject aObject3 = commonScreenObject.createPrefab(gameObject, tier2Prefix + k, "Prefab/Menu/Button_HireMenu", Vector3.zero, Vector3.one, Vector3.zero);
				commonScreenObject.findChild(aObject3, "Text").GetComponent<UILabel>().text = recruitmentTypeList[k].getRecruitmentName();
				commonScreenObject.findChild(aObject3, "CostFrame/CostLabel").GetComponent<UILabel>().text = CommonAPI.formatNumber(recruitmentTypeList[k].getRecruitmentCost());
			}
			break;
		}
		case MenuState.MenuStateShopBuyMenu:
		{
			titleLabel.text = gameData.getTextByRefId("menuShop05");
			gameObject.GetComponent<UIGrid>().cellHeight = 70f;
			gameObject.transform.parent.localPosition -= new Vector3(0f, 10f, 0f);
			List<string> list = new List<string>();
			list.Add(gameData.getTextByRefId("menuShop01"));
			list.Add(gameData.getTextByRefId("menuShop07"));
			for (int i = 0; i < list.Count; i++)
			{
				GameObject aObject = commonScreenObject.createPrefab(gameObject, tier2Prefix + i, "Prefab/Menu/Button_Tier2Menu", Vector3.zero, Vector3.one, Vector3.zero);
				commonScreenObject.findChild(aObject, "Text").GetComponent<UILabel>().text = list[i];
			}
			break;
		}
		}
		gameObject.GetComponent<UIGrid>().Reposition();
		gameObject.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
	}
}
