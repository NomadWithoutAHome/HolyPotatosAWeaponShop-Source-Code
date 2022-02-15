using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIWeaponCollectionController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private string selectedProjectId;

	private GameObject selectionBg;

	private UITexture weaponImg;

	private UILabel weaponName;

	private UILabel weaponDesc;

	private UILabel titleLbl;

	private UILabel selectWeaponLbl;

	private UILabel atkWeapon;

	private UILabel accWeapon;

	private UILabel spdWeapon;

	private UILabel magWeapon;

	private UISprite atkBg;

	private UISprite accBg;

	private UISprite spdBg;

	private UISprite magBg;

	private UIButton sellButton;

	private UIButton allButton;

	private UIButton stockButton;

	private UIButton soldButton;

	private UISprite sellBg;

	private UILabel sellLbl;

	private UILabel filterTitle;

	private UILabel allLbl;

	private UILabel stockLbl;

	private UILabel soldLbl;

	private UILabel warningLbl;

	private UISprite ratingSprite;

	private UISprite weaponBuyerBg;

	private UISprite weaponCostBg;

	private UILabel weaponCostLbl;

	private UILabel weaponTitle;

	private UITexture weaponHero;

	private UILabel weaponHeroName;

	private UILabel weaponHeroJob;

	private UIGrid weaponListGrid;

	private UIScrollBar collectionScrollbar;

	private UIGrid weaponTypeList;

	private CollectionFilterState filterState;

	private string filterWeaponType;

	private UILabel sortTitle;

	private UILabel sortLabel;

	private UISprite sortDirSprite;

	private UIButton sortDirButton;

	private WeaponStat sortStat;

	private bool isAsc;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		selectedProjectId = string.Empty;
		selectionBg = commonScreenObject.findChild(base.gameObject, "Selection_bg").gameObject;
		weaponImg = commonScreenObject.findChild(selectionBg, "WeaponBg/WeaponImg").GetComponent<UITexture>();
		weaponName = commonScreenObject.findChild(selectionBg, "WeaponTitleBg/WeaponTitle").GetComponent<UILabel>();
		weaponDesc = commonScreenObject.findChild(selectionBg, "WeaponTitleBg/WeaponDesc").GetComponent<UILabel>();
		titleLbl = commonScreenObject.findChild(base.gameObject, "Lists_bg/ForgingTitle_bg/ForgingTitle_label").GetComponent<UILabel>();
		selectWeaponLbl = commonScreenObject.findChild(selectionBg, "WeaponBg/SelectWeaponLabel").GetComponent<UILabel>();
		atkWeapon = commonScreenObject.findChild(selectionBg, "WeaponStatBg/Atk_sprite/Atk_lbl").GetComponent<UILabel>();
		spdWeapon = commonScreenObject.findChild(selectionBg, "WeaponStatBg/Spd_sprite/Spd_lbl").GetComponent<UILabel>();
		accWeapon = commonScreenObject.findChild(selectionBg, "WeaponStatBg/Acc_sprite/Acc_lbl").GetComponent<UILabel>();
		magWeapon = commonScreenObject.findChild(selectionBg, "WeaponStatBg/Mag_sprite/Mag_lbl").GetComponent<UILabel>();
		atkBg = commonScreenObject.findChild(selectionBg, "WeaponStatBg/Atk_sprite/Atk_bg").GetComponent<UISprite>();
		spdBg = commonScreenObject.findChild(selectionBg, "WeaponStatBg/Spd_sprite/Spd_bg").GetComponent<UISprite>();
		accBg = commonScreenObject.findChild(selectionBg, "WeaponStatBg/Acc_sprite/Acc_bg").GetComponent<UISprite>();
		magBg = commonScreenObject.findChild(selectionBg, "WeaponStatBg/Mag_sprite/Mag_bg").GetComponent<UISprite>();
		sellButton = commonScreenObject.findChild(selectionBg, "Sell_button").GetComponent<UIButton>();
		allButton = commonScreenObject.findChild(base.gameObject, "Lists_bg/FilterType_bg/All_button").GetComponent<UIButton>();
		stockButton = commonScreenObject.findChild(base.gameObject, "Lists_bg/FilterType_bg/Stock_button").GetComponent<UIButton>();
		soldButton = commonScreenObject.findChild(base.gameObject, "Lists_bg/FilterType_bg/Sold_button").GetComponent<UIButton>();
		sellBg = commonScreenObject.findChild(selectionBg, "Sell_button").GetComponent<UISprite>();
		sellLbl = commonScreenObject.findChild(selectionBg, "Sell_button/SellLabel").GetComponent<UILabel>();
		filterTitle = commonScreenObject.findChild(base.gameObject, "Lists_bg/FilterType_bg/FilterTitle_label").GetComponent<UILabel>();
		allLbl = commonScreenObject.findChild(base.gameObject, "Lists_bg/FilterType_bg/All_button").GetComponent<UILabel>();
		stockLbl = commonScreenObject.findChild(base.gameObject, "Lists_bg/FilterType_bg/Sold_button").GetComponent<UILabel>();
		soldLbl = commonScreenObject.findChild(base.gameObject, "Lists_bg/FilterType_bg/Stock_button").GetComponent<UILabel>();
		warningLbl = commonScreenObject.findChild(base.gameObject, "Lists_bg/WeaponList_bg/WeaponList_warning").GetComponent<UILabel>();
		ratingSprite = commonScreenObject.findChild(selectionBg, "WeaponBg/SelectWeaponGrade").GetComponent<UISprite>();
		weaponBuyerBg = commonScreenObject.findChild(selectionBg, "WeaponBuyer").GetComponent<UISprite>();
		weaponCostBg = commonScreenObject.findChild(selectionBg, "WeaponBuyer/WeaponBuyerCostBg").GetComponent<UISprite>();
		weaponCostLbl = commonScreenObject.findChild(selectionBg, "WeaponBuyer/WeaponBuyerCostLbl").GetComponent<UILabel>();
		weaponTitle = commonScreenObject.findChild(selectionBg, "WeaponBuyer/WeaponBuyerTitle").GetComponent<UILabel>();
		weaponHero = commonScreenObject.findChild(selectionBg, "WeaponBuyer/WeaponBuyerHero").GetComponent<UITexture>();
		weaponHeroName = commonScreenObject.findChild(selectionBg, "WeaponBuyer/WeaponBuyerName").GetComponent<UILabel>();
		weaponHeroJob = commonScreenObject.findChild(selectionBg, "WeaponBuyer/WeaponBuyerJob").GetComponent<UILabel>();
		weaponListGrid = commonScreenObject.findChild(base.gameObject, "Lists_bg/WeaponList_bg/Panel_WeaponList/WeaponList_grid").GetComponent<UIGrid>();
		collectionScrollbar = commonScreenObject.findChild(base.gameObject, "Lists_bg/WeaponList_bg/WeaponList_scrollbar").GetComponent<UIScrollBar>();
		weaponTypeList = commonScreenObject.findChild(base.gameObject, "Lists_bg/WeaponType_bg/WeaponType_grid").GetComponent<UIGrid>();
		sortLabel = commonScreenObject.findChild(base.gameObject, "Lists_bg/SortType_bg/Sort_button/Sort_label").GetComponent<UILabel>();
		sortTitle = commonScreenObject.findChild(base.gameObject, "Lists_bg/SortType_bg/SortTitle_label").GetComponent<UILabel>();
		sortDirSprite = commonScreenObject.findChild(base.gameObject, "Lists_bg/SortType_bg/AscDesc_button").GetComponent<UISprite>();
		sortDirButton = sortDirSprite.GetComponent<UIButton>();
		sortStat = WeaponStat.WeaponStatNone;
		isAsc = false;
		filterState = CollectionFilterState.CollectionFilterStateStock;
		filterWeaponType = "00000";
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "Close_button":
			viewController.closeCollectionPopup();
			return;
		case "Sell_button":
			if (game.getPlayer().getInShopSmithList().Count > 1)
			{
				viewController.closeCollectionPopup();
				viewController.showWorldMap(ActivityType.ActivityTypeSellWeapon);
			}
			else
			{
				viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: true, string.Empty, game.getGameData().getTextByRefId("errorCommon03"), PopupType.PopupTypeNothing, null, colorTag: false, null, map: false, string.Empty);
			}
			return;
		case "All_button":
			filterState = CollectionFilterState.CollectionFilterStateAll;
			filterWeaponType = "00000";
			showCollectionList();
			selectWeaponTypeListDisplay();
			return;
		case "Stock_button":
			filterState = CollectionFilterState.CollectionFilterStateStock;
			filterWeaponType = "00000";
			showCollectionList();
			selectWeaponTypeListDisplay();
			return;
		case "Sold_button":
			filterState = CollectionFilterState.CollectionFilterStateSold;
			filterWeaponType = "00000";
			showCollectionList();
			selectWeaponTypeListDisplay();
			return;
		case "Sort_button":
			changeSortType();
			return;
		case "AscDesc_button":
			changeSortDir();
			return;
		}
		string[] array = gameObjectName.Split('_');
		switch (array[0])
		{
		case "CollectionListObj":
			selectWeaponInList(gameObjectName);
			loadWeaponDetail(array[2]);
			break;
		case "WeaponTypeListObj":
			filterWeaponType = array[1];
			showCollectionList();
			selectWeaponTypeListDisplay();
			break;
		}
	}

	public void processHover(bool isOver, string hoverName)
	{
		if (isOver)
		{
			if (hoverName != null && hoverName == "WeaponBuyer")
			{
				if (selectedProjectId != string.Empty)
				{
					Player player = game.getPlayer();
					Project completedProjectById = player.getCompletedProjectById(selectedProjectId);
					if (completedProjectById.getProjectType() != ProjectType.ProjectTypeUnique)
					{
						Hero buyer = completedProjectById.getBuyer();
						tooltipScript.showText(buyer.getHeroStandardInfoString());
					}
				}
			}
			else
			{
				string[] array = hoverName.Split('_');
				if (array[0] == "CollectionListObj" && !sellButton.isEnabled)
				{
					Player player2 = game.getPlayer();
					Project completedProjectById2 = player2.getCompletedProjectById(array[2]);
					Weapon projectWeapon = completedProjectById2.getProjectWeapon();
					tooltipScript.showText(completedProjectById2.getProjectName(includePrefix: true));
				}
			}
		}
		else
		{
			tooltipScript.setInactive();
		}
	}

	private void Update()
	{
		handleInput();
	}

	private void handleInput()
	{
		if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(gameData.getKeyCodeByRefID("100007")))
		{
			processClick("Close_button");
		}
	}

	private void changeSortType()
	{
		switch (sortStat)
		{
		case WeaponStat.WeaponStatNone:
			sortStat = WeaponStat.WeaponStatAttack;
			break;
		case WeaponStat.WeaponStatAttack:
			sortStat = WeaponStat.WeaponStatSpeed;
			break;
		case WeaponStat.WeaponStatSpeed:
			sortStat = WeaponStat.WeaponStatAccuracy;
			break;
		case WeaponStat.WeaponStatAccuracy:
			sortStat = WeaponStat.WeaponStatMagic;
			break;
		case WeaponStat.WeaponStatMagic:
			sortStat = WeaponStat.WeaponStatElement;
			break;
		default:
			sortStat = WeaponStat.WeaponStatNone;
			break;
		}
		showCollectionList();
		selectWeaponTypeListDisplay();
		updateSortButtons();
	}

	private void changeSortDir()
	{
		if (isAsc)
		{
			isAsc = false;
		}
		else
		{
			isAsc = true;
		}
		showCollectionList();
		selectWeaponTypeListDisplay();
		updateSortButtons();
	}

	private void updateSortButtons()
	{
		GameData gameData = game.getGameData();
		if (isAsc)
		{
			sortDirButton.normalSprite = "bt_up";
			sortDirSprite.spriteName = "bt_up";
		}
		else
		{
			sortDirButton.normalSprite = "bt_down";
			sortDirSprite.spriteName = "bt_down";
		}
		switch (sortStat)
		{
		case WeaponStat.WeaponStatNone:
			sortLabel.text = gameData.getTextByRefId("menuCollection16").ToUpper(CultureInfo.InvariantCulture);
			break;
		case WeaponStat.WeaponStatAttack:
			sortLabel.text = gameData.getTextByRefId("weaponStats06").ToUpper(CultureInfo.InvariantCulture);
			break;
		case WeaponStat.WeaponStatSpeed:
			sortLabel.text = gameData.getTextByRefId("weaponStats07").ToUpper(CultureInfo.InvariantCulture);
			break;
		case WeaponStat.WeaponStatAccuracy:
			sortLabel.text = gameData.getTextByRefId("weaponStats08").ToUpper(CultureInfo.InvariantCulture);
			break;
		case WeaponStat.WeaponStatMagic:
			sortLabel.text = gameData.getTextByRefId("weaponStats09").ToUpper(CultureInfo.InvariantCulture);
			break;
		default:
			sortLabel.text = gameData.getTextByRefId("menuCollection17").ToUpper(CultureInfo.InvariantCulture);
			break;
		}
	}

	private void clearSelection()
	{
		GameData gameData = game.getGameData();
		weaponName.text = "????";
		weaponDesc.text = "????";
		weaponImg.mainTexture = commonScreenObject.loadTexture(string.Empty);
		atkWeapon.text = "??";
		spdWeapon.text = "??";
		accWeapon.text = "??";
		magWeapon.text = "??";
		atkBg.color = new Color(0.0235f, 0.157f, 0.196f);
		spdBg.color = new Color(0.0235f, 0.157f, 0.196f);
		accBg.color = new Color(0.0235f, 0.157f, 0.196f);
		magBg.color = new Color(0.0235f, 0.157f, 0.196f);
		activateSellButton();
		sellButton.enabled = false;
		sellBg.alpha = 0f;
		sellLbl.alpha = 0f;
		ratingSprite.alpha = 0f;
		weaponBuyerBg.alpha = 0f;
		weaponCostBg.alpha = 0f;
		weaponCostLbl.alpha = 0f;
		weaponTitle.alpha = 0f;
		weaponHero.alpha = 0f;
		weaponHeroName.alpha = 0f;
		weaponHeroJob.alpha = 0f;
	}

	public void setReference()
	{
		GameData gameData = game.getGameData();
		clearSelection();
		warningLbl.text = gameData.getTextByRefId("menuCollection06");
		titleLbl.text = gameData.getTextByRefId("menuCollection01");
		selectWeaponLbl.text = gameData.getTextByRefId("questSelect06");
		weaponTitle.text = gameData.getTextByRefId("menuCollection07");
		sellLbl.text = gameData.getTextByRefId("menuShop18");
		filterTitle.text = gameData.getTextByRefId("menuCollection14").ToUpper(CultureInfo.InvariantCulture);
		allLbl.text = gameData.getTextByRefId("menuCollection03");
		stockLbl.text = gameData.getTextByRefId("menuCollection05");
		soldLbl.text = gameData.getTextByRefId("menuCollection04");
		sortTitle.text = gameData.getTextByRefId("menuCollection15").ToUpper(CultureInfo.InvariantCulture);
		UILabel component = commonScreenObject.findChild(selectionBg, "WeaponStatBg/WeaponStats_label").GetComponent<UILabel>();
		component.text = gameData.getTextByRefId("weaponStats11");
		filterState = CollectionFilterState.CollectionFilterStateStock;
		filterWeaponType = "00000";
		showCollectionList();
		selectWeaponTypeListDisplay();
		updateSortButtons();
		commonScreenObject.findChild(base.gameObject, "Lists_bg/ForgingTitle_bg/ForgingTitle_label").GetComponent<UILabel>().text = game.getGameData().getTextByRefId("menuCollection01");
		Player player = game.getPlayer();
	}

	public void showCollectionList()
	{
		CollectionFilterState aState = filterState;
		string aWeaponTypeRefId = filterWeaponType;
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		clearSelection();
		clearListGrid();
		refreshWeaponTypeList();
		updateFilterButtons();
		List<Project> list = player.getCompletedProjectListByFilterType(ProjectType.ProjectTypeWeapon, aState, aWeaponTypeRefId);
		list.AddRange(player.getCompletedProjectListByFilterType(ProjectType.ProjectTypeUnique, aState, aWeaponTypeRefId));
		list.Reverse();
		if (sortStat == WeaponStat.WeaponStatAttack || sortStat == WeaponStat.WeaponStatSpeed || sortStat == WeaponStat.WeaponStatAccuracy || sortStat == WeaponStat.WeaponStatMagic || sortStat == WeaponStat.WeaponStatElement)
		{
			list = player.sortProjectListByStat(list, sortStat, isAsc);
		}
		else if (sortStat == WeaponStat.WeaponStatNone && isAsc)
		{
			list.Reverse();
		}
		if (list.Count > 0)
		{
			warningLbl.alpha = 0f;
		}
		else
		{
			warningLbl.alpha = 1f;
		}
		int num = 10000001;
		foreach (Project item in list)
		{
			Hero buyer = item.getBuyer();
			GameObject gameObject = commonScreenObject.createPrefab(weaponListGrid.gameObject, "CollectionListObj_" + num + "_" + item.getProjectId(), "Prefab/Collection/CollectionListObj", Vector3.zero, Vector3.one, Vector3.zero);
			gameObject.GetComponentInChildren<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + item.getProjectWeapon().getImage());
			if (item.getProjectSaleState() == ProjectSaleState.ProjectSaleStateDelivered)
			{
				gameObject.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuCollection12");
			}
			else if (item.getProjectSaleState() == ProjectSaleState.ProjectSaleStateSold)
			{
				gameObject.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuCollection11");
			}
			else
			{
				gameObject.GetComponentInChildren<UILabel>().text = string.Empty;
			}
			num++;
		}
		weaponListGrid.Reposition();
		collectionScrollbar.value = 0f;
		if (list.Count > 0)
		{
			weaponListGrid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		}
		weaponListGrid.enabled = true;
		collectionScrollbar.enabled = true;
	}

	private void updateFilterButtons()
	{
		switch (filterState)
		{
		case CollectionFilterState.CollectionFilterStateAll:
			allButton.isEnabled = false;
			stockButton.isEnabled = true;
			soldButton.isEnabled = true;
			break;
		case CollectionFilterState.CollectionFilterStateStock:
			allButton.isEnabled = true;
			stockButton.isEnabled = false;
			soldButton.isEnabled = true;
			break;
		case CollectionFilterState.CollectionFilterStateSold:
			allButton.isEnabled = true;
			stockButton.isEnabled = true;
			soldButton.isEnabled = false;
			break;
		}
	}

	private void clearListGrid()
	{
		while (weaponListGrid.transform.childCount > 0)
		{
			commonScreenObject.destroyPrefabImmediate(weaponListGrid.transform.GetChild(0).gameObject);
		}
	}

	public void loadWeaponDetail(string aProjId)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		selectedProjectId = aProjId;
		Project completedProjectById = player.getCompletedProjectById(aProjId);
		Weapon projectWeapon = completedProjectById.getProjectWeapon();
		Hero buyer = completedProjectById.getBuyer();
		selectWeaponLbl.text = string.Empty;
		weaponName.text = completedProjectById.getProjectName(includePrefix: true);
		weaponDesc.text = projectWeapon.getWeaponDesc();
		weaponImg.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + projectWeapon.getImage());
		switch (completedProjectById.getProjectSaleState())
		{
		case ProjectSaleState.ProjectSaleStateSold:
			sellButton.enabled = false;
			sellBg.alpha = 0f;
			sellLbl.alpha = 0f;
			ratingSprite.alpha = 1f;
			weaponBuyerBg.alpha = 1f;
			weaponCostBg.alpha = 1f;
			weaponCostLbl.alpha = 1f;
			weaponTitle.text = gameData.getTextByRefId("menuCollection07");
			weaponTitle.alpha = 1f;
			weaponHero.alpha = 1f;
			weaponHeroName.alpha = 1f;
			weaponHeroJob.alpha = 1f;
			weaponCostLbl.text = string.Empty + completedProjectById.getFinalPrice();
			weaponHero.mainTexture = commonScreenObject.loadTexture("Image/Hero/" + buyer.getImage());
			weaponHeroName.text = buyer.getHeroName();
			weaponHeroJob.text = buyer.getJobClassName();
			switch (CommonAPI.convertWeaponScoreToRating(completedProjectById.getFinalScore()))
			{
			case "S":
				ratingSprite.spriteName = "ranking_s";
				break;
			case "A":
				ratingSprite.spriteName = "ranking_a";
				break;
			case "B":
				ratingSprite.spriteName = "ranking_b";
				break;
			case "C":
				ratingSprite.spriteName = "ranking_c";
				break;
			case "D":
				ratingSprite.spriteName = "ranking_d";
				break;
			case "F":
				ratingSprite.spriteName = "ranking_f";
				break;
			}
			break;
		case ProjectSaleState.ProjectSaleStateDelivered:
			sellButton.enabled = false;
			sellBg.alpha = 0f;
			sellLbl.alpha = 0f;
			ratingSprite.alpha = 0f;
			weaponBuyerBg.alpha = 1f;
			weaponCostBg.alpha = 0f;
			weaponCostLbl.alpha = 0f;
			weaponTitle.text = gameData.getTextByRefId("menuCollection10");
			weaponTitle.alpha = 1f;
			weaponHero.alpha = 1f;
			weaponHeroName.alpha = 1f;
			weaponHeroJob.alpha = 1f;
			if (completedProjectById.getProjectType() == ProjectType.ProjectTypeUnique)
			{
				LegendaryHero legendaryHeroByWeaponRefId = gameData.getLegendaryHeroByWeaponRefId(completedProjectById.getProjectWeapon().getWeaponRefId());
				weaponHero.mainTexture = commonScreenObject.loadTexture("Image/legendary heroes/" + legendaryHeroByWeaponRefId.getImage());
				weaponHeroName.text = legendaryHeroByWeaponRefId.getLegendaryHeroName();
				weaponHeroJob.text = gameData.getTextByRefId("menuCollection13");
			}
			else
			{
				weaponHero.mainTexture = commonScreenObject.loadTexture("Image/Hero/" + buyer.getImage());
				weaponHeroName.text = buyer.getHeroName();
				weaponHeroJob.text = buyer.getJobClassName();
			}
			break;
		default:
			sellButton.enabled = true;
			sellBg.alpha = 1f;
			sellLbl.alpha = 1f;
			ratingSprite.alpha = 0f;
			weaponBuyerBg.alpha = 0f;
			weaponCostBg.alpha = 0f;
			weaponCostLbl.alpha = 0f;
			weaponTitle.alpha = 0f;
			weaponHero.alpha = 0f;
			weaponHeroName.alpha = 0f;
			weaponHeroJob.alpha = 0f;
			break;
		}
		atkWeapon.text = string.Empty + completedProjectById.getAtk();
		accWeapon.text = string.Empty + completedProjectById.getAcc();
		spdWeapon.text = string.Empty + completedProjectById.getSpd();
		magWeapon.text = string.Empty + completedProjectById.getMag();
		atkWeapon.effectStyle = UILabel.Effect.None;
		accWeapon.effectStyle = UILabel.Effect.None;
		spdWeapon.effectStyle = UILabel.Effect.None;
		magWeapon.effectStyle = UILabel.Effect.None;
		atkWeapon.fontSize = 14;
		accWeapon.fontSize = 14;
		spdWeapon.fontSize = 14;
		magWeapon.fontSize = 14;
		atkBg.color = new Color(0.0235f, 0.157f, 0.196f);
		spdBg.color = new Color(0.0235f, 0.157f, 0.196f);
		accBg.color = new Color(0.0235f, 0.157f, 0.196f);
		magBg.color = new Color(0.0235f, 0.157f, 0.196f);
		displayPriSecStat(completedProjectById);
		player.setLastSelectProject(completedProjectById);
		activateSellButton();
	}

	private void displayPriSecStat(Project project)
	{
		List<WeaponStat> priSecStat = project.getPriSecStat();
		if (priSecStat.Count > 0)
		{
			switch (priSecStat[0])
			{
			case WeaponStat.WeaponStatAttack:
				atkWeapon.effectStyle = UILabel.Effect.Outline;
				atkWeapon.fontSize = 15;
				atkBg.color = new Color(0.0196f, 0.788f, 0.659f);
				break;
			case WeaponStat.WeaponStatSpeed:
				spdWeapon.effectStyle = UILabel.Effect.Outline;
				spdWeapon.fontSize = 15;
				spdBg.color = new Color(0.0196f, 0.788f, 0.659f);
				break;
			case WeaponStat.WeaponStatAccuracy:
				accWeapon.effectStyle = UILabel.Effect.Outline;
				accWeapon.fontSize = 15;
				accBg.color = new Color(0.0196f, 0.788f, 0.659f);
				break;
			case WeaponStat.WeaponStatMagic:
				magWeapon.effectStyle = UILabel.Effect.Outline;
				magWeapon.fontSize = 15;
				magBg.color = new Color(0.0196f, 0.788f, 0.659f);
				break;
			}
		}
		if (priSecStat.Count > 1)
		{
			switch (priSecStat[1])
			{
			case WeaponStat.WeaponStatAttack:
				atkBg.color = new Color(0.0235f, 0.522f, 0.439f);
				break;
			case WeaponStat.WeaponStatSpeed:
				spdBg.color = new Color(0.0235f, 0.522f, 0.439f);
				break;
			case WeaponStat.WeaponStatAccuracy:
				accBg.color = new Color(0.0235f, 0.522f, 0.439f);
				break;
			case WeaponStat.WeaponStatMagic:
				magBg.color = new Color(0.0235f, 0.522f, 0.439f);
				break;
			}
		}
	}

	private void refreshWeaponTypeList()
	{
		CollectionFilterState aState = filterState;
		clearListWeaponType();
		List<WeaponType> list = new List<WeaponType>(game.getPlayer().getCollectionWeaponTypeList(aState));
		list.Insert(0, new WeaponType("00000", game.getGameData().getTextByRefId("menuCollection09"), string.Empty, string.Empty, 0f, 0f, 0f, 0f, string.Empty));
		foreach (WeaponType item in list)
		{
			GameObject gameObject = commonScreenObject.createPrefab(weaponTypeList.gameObject, "WeaponTypeListObj_" + item.getWeaponTypeRefId(), "Prefab/Collection/CollectionWeaponTypeListObj", Vector3.zero, Vector3.one, Vector3.zero);
			gameObject.GetComponentInChildren<UILabel>().text = item.getWeaponTypeName();
			UISprite component = commonScreenObject.findChild(gameObject, "WeaponTypeListObj_bg/Category_sprite").GetComponent<UISprite>();
			if (item.getWeaponTypeRefId() != "00000")
			{
				component.spriteName = "icon_" + item.getImage();
				continue;
			}
			component.spriteName = "circle";
			component.SetDimensions(15, 15);
			component.color = new Color(0.9843137f, 0.831372559f, 0.380392164f);
		}
		weaponTypeList.Reposition();
		weaponTypeList.enabled = true;
	}

	private void clearListWeaponType()
	{
		while (weaponTypeList.transform.childCount > 0)
		{
			commonScreenObject.destroyPrefabImmediate(weaponTypeList.transform.GetChild(0).gameObject);
		}
	}

	private void selectWeaponTypeListDisplay()
	{
		string text = filterWeaponType;
		foreach (Transform child in weaponTypeList.GetChildList())
		{
			GameObject gameObject = commonScreenObject.findChild(child.gameObject, "WeaponTypeListObj_bg").gameObject;
			if (child.gameObject.name == "WeaponTypeListObj_" + text)
			{
				gameObject.transform.localPosition = new Vector3(10f, 0f, 0f);
				child.GetComponent<UIButton>().isEnabled = false;
			}
			else
			{
				gameObject.transform.localPosition = Vector3.zero;
				child.GetComponent<UIButton>().isEnabled = true;
			}
		}
	}

	private void selectWeaponInList(string gameObjectName)
	{
		foreach (Transform child in weaponListGrid.GetChildList())
		{
			if (child.gameObject.name == gameObjectName)
			{
				child.gameObject.GetComponentInChildren<UISprite>().spriteName = "bg_weaponselected";
			}
			else
			{
				child.gameObject.GetComponentInChildren<UISprite>().spriteName = "bg_weapon";
			}
		}
	}

	private void activateSellButton()
	{
		Project lastSelectProject = game.getPlayer().getLastSelectProject();
		if (lastSelectProject.getProjectId() != string.Empty && lastSelectProject.getProjectSaleState() == ProjectSaleState.ProjectSaleStateInStock)
		{
			sellLbl.text = game.getGameData().getTextByRefId("menuShop18").ToUpper(CultureInfo.InvariantCulture);
			sellButton.isEnabled = true;
			return;
		}
		switch (lastSelectProject.getProjectSaleState())
		{
		case ProjectSaleState.ProjectSaleStateSelling:
			sellLbl.text = game.getGameData().getTextByRefId("menuCollection08").ToUpper(CultureInfo.InvariantCulture);
			break;
		case ProjectSaleState.ProjectSaleStateSold:
			sellLbl.text = game.getGameData().getTextByRefId("menuCollection05").ToUpper(CultureInfo.InvariantCulture);
			break;
		case ProjectSaleState.ProjectSaleStateDelivered:
			sellLbl.text = game.getGameData().getTextByRefId("menuCollection05").ToUpper(CultureInfo.InvariantCulture);
			break;
		case ProjectSaleState.ProjectSaleStateRejected:
			sellLbl.text = game.getGameData().getTextByRefId("menuCollection05").ToUpper(CultureInfo.InvariantCulture);
			break;
		}
		sellButton.isEnabled = false;
	}
}
