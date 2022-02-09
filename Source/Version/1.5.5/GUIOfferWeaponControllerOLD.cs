using System.Collections.Generic;
using UnityEngine;

public class GUIOfferWeaponControllerOLD : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private AudioController audioController;

	private Smith smith;

	private Area area;

	private UIGrid offerSelect_Grid;

	private UIScrollBar offerScrollbar;

	private UIGrid sellList_grid;

	private int sellListSelection;

	private UILabel weaponNameLabel;

	private UITexture weaponImage;

	private UILabel atkLabel;

	private UILabel spdLabel;

	private UILabel accLabel;

	private UILabel magLabel;

	private UIButton refreshButton;

	private UILabel refreshLabel;

	private UILabel goldLabel;

	private List<int> acceptedOfferList;

	private string offerPrefix;

	private int selectedIndex;

	private List<GameObject> offerObjectList;

	private int testOfferList;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		smith = null;
		area = null;
		offerSelect_Grid = commonScreenObject.findChild(base.gameObject, "OfferWeapon/OfferWeaponList_panel/Offer_Grid").GetComponent<UIGrid>();
		offerScrollbar = commonScreenObject.findChild(base.gameObject, "OfferWeapon/Offer_scrollbar").GetComponent<UIScrollBar>();
		sellList_grid = commonScreenObject.findChild(base.gameObject, "SellList_grid").GetComponent<UIGrid>();
		sellListSelection = 0;
		weaponNameLabel = commonScreenObject.findChild(base.gameObject, "SelectedWeapon_bg/SelectedWeapon_name").GetComponent<UILabel>();
		weaponImage = commonScreenObject.findChild(base.gameObject, "SelectedWeapon_bg/SelectedWeapon_imageBg/SelectedWeapon_image").GetComponent<UITexture>();
		atkLabel = commonScreenObject.findChild(base.gameObject, "SelectedWeapon_bg/atk_sprite/atk_label").GetComponent<UILabel>();
		spdLabel = commonScreenObject.findChild(base.gameObject, "SelectedWeapon_bg/spd_sprite/spd_label").GetComponent<UILabel>();
		accLabel = commonScreenObject.findChild(base.gameObject, "SelectedWeapon_bg/acc_sprite/acc_label").GetComponent<UILabel>();
		magLabel = commonScreenObject.findChild(base.gameObject, "SelectedWeapon_bg/mag_sprite/mag_label").GetComponent<UILabel>();
		refreshButton = commonScreenObject.findChild(base.gameObject, "RefreshButton").GetComponent<UIButton>();
		refreshLabel = commonScreenObject.findChild(base.gameObject, "RefreshButton/RefreshLabel").GetComponent<UILabel>();
		goldLabel = commonScreenObject.findChild(base.gameObject, "PlayerGold_label").GetComponent<UILabel>();
		offerObjectList = new List<GameObject>();
		acceptedOfferList = new List<int>();
		offerPrefix = "Offer_";
		selectedIndex = 0;
		testOfferList = 5;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "CloseButton":
			viewController.closeOfferWeapon(hide: true, resume: true);
			return;
		case "SellButton":
			tryCloseOffers();
			return;
		case "RefreshButton":
			game.getPlayer().reduceGold(area.getRefreshPrice(), allowNegative: false);
			audioController.playPurchaseAudio();
			checkRefreshButton();
			refreshList(sellListSelection);
			updateSellList();
			showOffer();
			return;
		}
		string[] array = gameObjectName.Split('_');
		switch (array[0])
		{
		case "SellListObj":
			sellListSelection = CommonAPI.parseInt(array[1]);
			updateSellList();
			showOffer();
			break;
		case "Offer":
			selectedIndex = CommonAPI.parseInt(array[1]);
			selectOffer(selectedIndex);
			updateSellList();
			break;
		}
	}

	public void tryCloseOffers()
	{
		if (acceptedOfferList.Contains(-1))
		{
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeYesAndNo, resume: false, "WARNING", "The weapons with no selected offers will not be sold. Are you sure you want to end the sale?", PopupType.PopupTypeSellWeapon, null, colorTag: false, null, map: false, string.Empty);
		}
		else
		{
			closeOffers();
		}
	}

	public void closeOffers()
	{
		viewController.closeOfferWeapon(hide: true, resume: true);
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		List<string> exploreTask = smith.getExploreTask();
		for (int i = 0; i < exploreTask.Count; i++)
		{
			Project completedProjectById = player.getCompletedProjectById(exploreTask[i]);
			int num = acceptedOfferList[i];
			if (num != -1)
			{
				Offer offer = completedProjectById.getOfferList()[num];
				completedProjectById.setBuyer(gameData.getJobClassByRefId(offer.getHeroRefId()));
				completedProjectById.setFinalPrice(offer.getPrice());
				completedProjectById.setFinalScore(offer.getWeaponScore());
			}
		}
		GameObject.Find("Panel_Explore").GetComponent<GUIExploreController>().refreshSmithExplorationList();
	}

	public void setOffer(Smith aSmith)
	{
		smith = aSmith;
		area = aSmith.getExploreArea();
		refreshLabel.text = "Refresh list for $" + CommonAPI.formatNumber(area.getRefreshPrice());
		checkRefreshButton();
		sellListSelection = 0;
		setWeaponOffers();
		updateSellList();
		showOffer();
	}

	private void checkRefreshButton()
	{
		Player player = game.getPlayer();
		goldLabel.text = "Gold Left: " + player.getPlayerGold();
		if (player.getPlayerGold() >= area.getRefreshPrice())
		{
			refreshButton.isEnabled = true;
		}
		else
		{
			refreshButton.isEnabled = false;
		}
	}

	private void showOffer()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		List<string> exploreTask = smith.getExploreTask();
		Project completedProjectById = player.getCompletedProjectById(exploreTask[sellListSelection]);
		setWeapon(completedProjectById);
		clearOfferObjectList();
		List<Offer> offerList = completedProjectById.getOfferList();
		for (int i = 0; i < offerList.Count; i++)
		{
			Offer offer = offerList[i];
			GameObject gameObject = commonScreenObject.createPrefab(offerSelect_Grid.gameObject, offerPrefix + i, "Prefab/OfferWeapon/Button_OfferWeapon", Vector3.zero, Vector3.one, Vector3.zero);
			Hero jobClassByRefId = gameData.getJobClassByRefId(offer.getHeroRefId());
			commonScreenObject.findChild(gameObject, "Name").GetComponent<UILabel>().text = jobClassByRefId.getHeroName();
			commonScreenObject.findChild(gameObject, "JobClass").GetComponent<UILabel>().text = jobClassByRefId.getJobClassName();
			commonScreenObject.findChild(gameObject, "Price").GetComponent<UILabel>().text = "$" + CommonAPI.formatNumber(offer.getPrice());
			offerObjectList.Add(gameObject);
		}
		selectOffer(acceptedOfferList[sellListSelection]);
		offerSelect_Grid.Reposition();
		offerScrollbar.value = 0f;
		offerSelect_Grid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		offerSelect_Grid.enabled = true;
	}

	private void clearOfferObjectList()
	{
		offerObjectList.Clear();
		while (offerSelect_Grid.transform.childCount > 0)
		{
			commonScreenObject.destroyPrefabImmediate(offerSelect_Grid.GetChild(0).gameObject);
		}
	}

	private void setWeapon(Project project)
	{
		weaponNameLabel.text = project.getProjectName(includePrefix: true);
		weaponImage.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + project.getProjectWeapon().getImage());
		atkLabel.text = project.getAtk().ToString();
		spdLabel.text = project.getSpd().ToString();
		accLabel.text = project.getAcc().ToString();
		magLabel.text = project.getMag().ToString();
	}

	private void selectOffer(int aIndex)
	{
		if (acceptedOfferList[sellListSelection] != aIndex)
		{
			acceptedOfferList[sellListSelection] = aIndex;
		}
		else
		{
			acceptedOfferList[sellListSelection] = -1;
		}
		int num = 0;
		foreach (GameObject offerObject in offerObjectList)
		{
			if (num == acceptedOfferList[sellListSelection])
			{
				offerObject.GetComponent<UISprite>().spriteName = "parent-active";
			}
			else
			{
				offerObject.GetComponent<UISprite>().spriteName = "parent-inactive";
			}
			num++;
		}
	}

	private void updateSellList()
	{
		Player player = game.getPlayer();
		List<string> exploreTask = smith.getExploreTask();
		for (int i = 0; i < exploreTask.Count; i++)
		{
			Project completedProjectById = player.getCompletedProjectById(exploreTask[i]);
			GameObject gameObject = commonScreenObject.createPrefab(sellList_grid.gameObject, "SellListObj_" + i, "Prefab/OfferWeapon/SellListObj", Vector3.zero, Vector3.one, Vector3.zero);
			gameObject.GetComponentInChildren<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + completedProjectById.getProjectWeapon().getImage());
			if (i == sellListSelection)
			{
				gameObject.GetComponent<UIButton>().isEnabled = false;
			}
			else
			{
				gameObject.GetComponent<UIButton>().isEnabled = true;
			}
			if (acceptedOfferList[i] != -1)
			{
				gameObject.GetComponent<UISprite>().spriteName = "bt_weapon_selected";
			}
			else
			{
				gameObject.GetComponent<UISprite>().spriteName = "bt_weapon";
			}
		}
		sellList_grid.Reposition();
		sellList_grid.enabled = true;
	}

	public void setWeaponOffers()
	{
		List<string> exploreTask = smith.getExploreTask();
		List<Project> completedProjectListById = game.getPlayer().getCompletedProjectListById(exploreTask);
		acceptedOfferList = new List<int>();
		Area exploreArea = smith.getExploreArea();
		for (int i = 0; i < completedProjectListById.Count; i++)
		{
			acceptedOfferList.Add(-1);
			refreshList(i);
		}
	}

	private void refreshList(int selection)
	{
		List<string> exploreTask = smith.getExploreTask();
		Project completedProjectById = game.getPlayer().getCompletedProjectById(exploreTask[selection]);
		acceptedOfferList[selection] = -1;
		GameData gameData = game.getGameData();
		int merchantLevel = CommonAPI.getMerchantLevel(smith.getMerchantExp());
		int b = Mathf.FloorToInt(3f + 0.3f * Mathf.Pow(merchantLevel - 1, 1.1f));
		b = Mathf.Min(6, b);
		List<Offer> list = new List<Offer>();
		List<string> rareHeroRefIdList = area.getRareHeroRefIdList();
		List<int> rareHeroChanceList = area.getRareHeroChanceList();
		List<string> heroRefIdList = area.getHeroRefIdList();
		List<int> heroChanceList = area.getHeroChanceList();
		for (int i = 0; i < b; i++)
		{
			bool flag = false;
			if (rareHeroChanceList.Count > 0)
			{
				int weightedRandomIndex = CommonAPI.getWeightedRandomIndex(rareHeroChanceList);
				if (rareHeroRefIdList[weightedRandomIndex] != string.Empty)
				{
					string text = rareHeroRefIdList[weightedRandomIndex];
					Hero jobClassByRefId = gameData.getJobClassByRefId(text);
					int num = 1;
					int num2 = jobClassByRefId.getWealth() * (int)Mathf.Pow((float)num / 2f + 0.5f, 1.5f) + (int)Mathf.Pow(jobClassByRefId.getWealth(), 0.3f);
					int aOfferPrice = (int)((float)num2 * Mathf.Pow(CommonAPI.getHeroLevel(jobClassByRefId.getExpPoints()), 0.5f));
					int aExpGrowth = Mathf.CeilToInt((float)num / 4f);
					list.Add(new Offer(i.ToString(), completedProjectById.getProjectId(), text, aOfferPrice, num, aExpGrowth, 1f, 1f));
					rareHeroRefIdList.RemoveAt(weightedRandomIndex);
					rareHeroChanceList.RemoveAt(weightedRandomIndex);
					flag = true;
				}
			}
			if (!flag && heroChanceList.Count > 0)
			{
				int weightedRandomIndex2 = CommonAPI.getWeightedRandomIndex(heroChanceList);
				string text2 = heroRefIdList[weightedRandomIndex2];
				Hero jobClassByRefId2 = gameData.getJobClassByRefId(text2);
				int num3 = 1;
				int num4 = jobClassByRefId2.getWealth() * (int)Mathf.Pow((float)num3 / 2f + 0.5f, 1.5f) + (int)Mathf.Pow(jobClassByRefId2.getWealth(), 0.3f);
				int aOfferPrice2 = (int)((float)num4 * Mathf.Pow(CommonAPI.getHeroLevel(jobClassByRefId2.getExpPoints()), 0.5f));
				int aExpGrowth2 = Mathf.CeilToInt((float)num3 / 4f);
				list.Add(new Offer(i.ToString(), completedProjectById.getProjectId(), text2, aOfferPrice2, num3, aExpGrowth2, 1f, 1f));
				heroRefIdList.RemoveAt(weightedRandomIndex2);
				heroChanceList.RemoveAt(weightedRandomIndex2);
				flag = true;
			}
		}
		completedProjectById.setOfferList(list);
	}
}
