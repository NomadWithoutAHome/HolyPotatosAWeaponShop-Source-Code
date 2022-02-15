using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIActivitySelectController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private UIButton buyMatsButton;

	private UIButton exploreButton;

	private UIButton sellWeaponButton;

	private UISprite sellWeaponAlert;

	private UILabel activityLabel;

	private UILabel buyLabel;

	private UILabel sellLabel;

	private UILabel exploreLabel;

	private UILabel upgradeLabel;

	private UILabel fameLabel;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		buyMatsButton = commonScreenObject.findChild(base.gameObject, "BuyMatsButton").GetComponent<UIButton>();
		exploreButton = commonScreenObject.findChild(base.gameObject, "ExploreButton").GetComponent<UIButton>();
		sellWeaponButton = commonScreenObject.findChild(base.gameObject, "SellWeaponButton").GetComponent<UIButton>();
		sellWeaponAlert = commonScreenObject.findChild(sellWeaponButton.gameObject, "SellWeaponAlert").GetComponent<UISprite>();
		activityLabel = commonScreenObject.findChild(base.gameObject, "WorldMapButton/activity_label").GetComponent<UILabel>();
		buyLabel = commonScreenObject.findChild(base.gameObject, "BuyMatsButton/BuyMatsLabel_label").GetComponent<UILabel>();
		sellLabel = commonScreenObject.findChild(base.gameObject, "SellWeaponButton/SellWeaponLabel_label").GetComponent<UILabel>();
		exploreLabel = commonScreenObject.findChild(base.gameObject, "ExploreButton/ExploreLabel_label").GetComponent<UILabel>();
		upgradeLabel = commonScreenObject.findChild(base.gameObject, "UpgradeButton/UpgradeLabel_label").GetComponent<UILabel>();
		fameLabel = commonScreenObject.findChild(base.gameObject, "FameButton/FameLabel").GetComponent<UILabel>();
		setReference();
	}

	public void processClick(string gameObjectName)
	{
		Player player = game.getPlayer();
		switch (gameObjectName)
		{
		case "BuyMatsButton":
			break;
		case "ExploreButton":
			break;
		case "SellWeaponButton":
			break;
		case "WorldMapButton":
			viewController.showWorldMap();
			break;
		case "UpgradeButton":
			player.setPlayerGold(player.getPlayerGold() + 100000);
			break;
		case "FameButton":
			player.setFame(player.getFame() + 2000);
			fameLabel.text = CommonAPI.formatNumber(player.getFame());
			break;
		}
	}

	public void processHover(bool isOver, string hoverName)
	{
		if (isOver)
		{
			showLabel(hoverName);
		}
		else
		{
			showLabel(string.Empty);
		}
	}

	private void showLabel(string show)
	{
		GameData gameData = game.getGameData();
		switch (show)
		{
		case "BuyMatsButton":
			buyLabel.text = gameData.getTextByRefId("mapText05").ToUpper(CultureInfo.InvariantCulture);
			buyLabel.alpha = 1f;
			exploreLabel.alpha = 0f;
			sellLabel.alpha = 0f;
			upgradeLabel.alpha = 0f;
			activityLabel.text = string.Empty;
			break;
		case "ExploreButton":
			exploreLabel.text = gameData.getTextByRefId("mapText03").ToUpper(CultureInfo.InvariantCulture);
			exploreLabel.alpha = 1f;
			buyLabel.alpha = 0f;
			sellLabel.alpha = 0f;
			upgradeLabel.alpha = 0f;
			activityLabel.text = string.Empty;
			break;
		case "SellWeaponButton":
			sellLabel.text = gameData.getTextByRefId("mapText04").ToUpper(CultureInfo.InvariantCulture);
			sellLabel.alpha = 1f;
			buyLabel.alpha = 0f;
			exploreLabel.alpha = 0f;
			upgradeLabel.alpha = 0f;
			activityLabel.text = string.Empty;
			break;
		case "UpgradeButton":
		{
			Player player = game.getPlayer();
			AreaRegion areaRegionByRefID = gameData.getAreaRegionByRefID(player.getAreaRegion() + 1);
			if (player.getFame() >= areaRegionByRefID.getFameRequired())
			{
				upgradeLabel.text = "UPGRADE";
			}
			else
			{
				upgradeLabel.text = "Require " + areaRegionByRefID.getFameRequired() + " fame";
			}
			upgradeLabel.alpha = 1f;
			buyLabel.alpha = 0f;
			exploreLabel.alpha = 0f;
			sellLabel.alpha = 0f;
			activityLabel.text = string.Empty;
			break;
		}
		default:
			activityLabel.text = string.Empty;
			buyLabel.alpha = 0f;
			exploreLabel.alpha = 0f;
			sellLabel.alpha = 0f;
			upgradeLabel.alpha = 0f;
			break;
		}
	}

	public void setReference()
	{
		refreshButtons();
		showLabel(string.Empty);
		fameLabel.text = CommonAPI.formatNumber(game.getPlayer().getFame());
	}

	public void refreshButtons()
	{
		List<Project> completedProjectListByType = game.getPlayer().getCompletedProjectListByType(ProjectType.ProjectTypeWeapon, includeSold: false, includeStock: true, includeSelling: false);
		if (completedProjectListByType.Count > 0)
		{
			sellWeaponAlert.alpha = 1f;
		}
		else
		{
			sellWeaponAlert.alpha = 0f;
		}
	}

	private void upgradeWorkshop()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		AreaRegion areaRegionByRefID = gameData.getAreaRegionByRefID(player.getAreaRegion() + 1);
		if (player.getFame() >= areaRegionByRefID.getFameRequired())
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			dictionary.Add("DESIGN", player.getHighestPlayerFurnitureByType("601").getFurnitureLevel());
			dictionary.Add("CRAFT", player.getHighestPlayerFurnitureByType("701").getFurnitureLevel());
			dictionary.Add("POLISH", player.getHighestPlayerFurnitureByType("801").getFurnitureLevel());
			dictionary.Add("ENCHANT", player.getHighestPlayerFurnitureByType("901").getFurnitureLevel());
			bool flag = true;
			string text = string.Empty;
			foreach (KeyValuePair<string, int> item in dictionary)
			{
				if (item.Value < areaRegionByRefID.getWorkstationLvl())
				{
					flag = false;
					text = ((!(text == string.Empty)) ? (text + ", " + item.Key) : (text + item.Key));
				}
			}
			if (!flag)
			{
				List<string> list = new List<string>();
				list.Add("[StationPhase]");
				list.Add("[StationLvl]");
				List<string> list2 = new List<string>();
				list2.Add(text);
				list2.Add(areaRegionByRefID.getWorkstationLvl().ToString());
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: true, string.Empty, gameData.getRandomTextBySetRefIdWithDynTextList("mapText50", list, list2), PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
			}
			else
			{
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeYesAndNo, resume: true, string.Empty, gameData.getTextByRefId("mapText51"), PopupType.PopupTypeUpgradeShop, null, colorTag: false, null, map: false, string.Empty);
			}
		}
		else
		{
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: true, string.Empty, gameData.getTextByRefId("mapText52"), PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
		}
	}

	public void upgradeShop()
	{
		StartCoroutine("startUpgradeShop");
	}

	private IEnumerator startUpgradeShop()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		ShopLevel currShopLevel = player.getShopLevel();
		ShopLevel nextShopLevel = gameData.getShopLevel(currShopLevel.getNextShopRefId());
		player.setShopLevel(nextShopLevel);
		player.setAreaRegion(player.getAreaRegion() + 1);
		GameObject.Find("GUIGridController").GetComponent<GUIGridController>().createWorld(refresh: true);
		yield return new WaitForSeconds(0.1f);
		viewController.closeGeneralPopup(toResume: true, hide: true, resumeFromPlayerPause: true);
	}
}
