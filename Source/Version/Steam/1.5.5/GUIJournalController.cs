using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIJournalController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private UIButton weaponsTab;

	private UIButton awardsTab;

	private UIButton heroTab;

	private UIButton smithTab;

	private string journalTab;

	private GameObject subTabs;

	private GameObject journalSubTabsObj;

	private string currentSubTab;

	private UIButton pageLeftButton;

	private UIButton pageRightButton;

	private UILabel pageNumLeftLabel;

	private UILabel pageNumRightLabel;

	private UISprite pageNumLeftBg;

	private UISprite pageNumRightBg;

	private GameObject book;

	private int currentPage;

	private GameObject journalContentObj;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		GameObject gameObject = commonScreenObject.findChild(base.gameObject, "Book_bg/SectionTabs").gameObject;
		weaponsTab = commonScreenObject.findChild(gameObject.gameObject, "BestWeaponTab_bg").GetComponent<UIButton>();
		awardsTab = commonScreenObject.findChild(gameObject.gameObject, "AwardsTab_bg").GetComponent<UIButton>();
		heroTab = commonScreenObject.findChild(gameObject.gameObject, "HeroesTab_bg").GetComponent<UIButton>();
		smithTab = commonScreenObject.findChild(gameObject.gameObject, "SmithsTab_bg").GetComponent<UIButton>();
		subTabs = commonScreenObject.findChild(base.gameObject, "Book_bg/SubTabs").gameObject;
		currentSubTab = string.Empty;
		GameObject gameObject2 = commonScreenObject.findChild(base.gameObject, "Book_bg/PageNumbers").gameObject;
		pageLeftButton = commonScreenObject.findChild(gameObject2.gameObject, "PageNumLeft_arrow").GetComponent<UIButton>();
		pageRightButton = commonScreenObject.findChild(gameObject2.gameObject, "PageNumRight_arrow").GetComponent<UIButton>();
		pageNumLeftLabel = commonScreenObject.findChild(gameObject2.gameObject, "PageNumLeft_label").GetComponent<UILabel>();
		pageNumRightLabel = commonScreenObject.findChild(gameObject2.gameObject, "PageNumRight_label").GetComponent<UILabel>();
		pageNumLeftBg = commonScreenObject.findChild(gameObject2.gameObject, "PageNumLeft_bg").GetComponent<UISprite>();
		pageNumRightBg = commonScreenObject.findChild(gameObject2.gameObject, "PageNumRight_bg").GetComponent<UISprite>();
		book = commonScreenObject.findChild(base.gameObject, "Book_bg").gameObject;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "Close_button":
			viewController.closeJournal();
			return;
		case "BestWeaponTab_bg":
			showJournalSection("WEAPON");
			return;
		case "AwardsTab_bg":
			showJournalSection("AWARD");
			return;
		case "HeroesTab_bg":
			showJournalSection("HERO");
			return;
		case "SmithsTab_bg":
			showJournalSection("SMITH");
			return;
		case "Normal_subTab":
			currentSubTab = "NORMAL";
			if (journalTab == "SMITH")
			{
				showSmithSubSection();
			}
			else if (journalTab == "HERO")
			{
				showHeroSubSection();
			}
			return;
		case "Outsource_subTab":
			currentSubTab = "OUTSOURCE";
			showSmithSubSection();
			return;
		case "Legendary_subTab":
			currentSubTab = "LEGENDARY";
			if (journalTab == "SMITH")
			{
				showSmithSubSection();
			}
			else if (journalTab == "HERO")
			{
				showHeroSubSection();
			}
			return;
		case "PageNumLeft_arrow":
			if (journalTab == "AWARD")
			{
				flipPage(-1);
			}
			else
			{
				flipPage(-2);
			}
			return;
		case "PageNumRight_arrow":
			if (journalTab == "AWARD")
			{
				flipPage(1);
			}
			else
			{
				flipPage(2);
			}
			return;
		}
		string[] array = gameObjectName.Split('_');
		switch (array[0])
		{
		case "WeaponTypeSubTab":
			currentSubTab = array[1];
			showWeaponSubSection();
			break;
		case "LegendarySmithCutsceneButton":
			showSmithCutscene(array[1]);
			break;
		case "LegendaryHeroIntroButton":
			showHeroCutscene(array[1], "INTRO");
			break;
		case "LegendaryHeroCompleteButton":
			showHeroCutscene(array[1], "COMPLETE");
			break;
		case "LegendaryHeroRejectButton":
			showHeroCutscene(array[1], "REJECT");
			break;
		}
	}

	public void processHover(bool isOver, string hoverName)
	{
		if (isOver)
		{
			if (hoverName != null)
			{
			}
			string[] array = hoverName.Split('_');
			switch (array[0])
			{
			case "NormalHeroObj":
				if (array[2] != "0")
				{
					GameData gameData3 = game.getGameData();
					Hero heroByHeroRefID = gameData3.getHeroByHeroRefID(array[2]);
					string heroStandardInfoString = heroByHeroRefID.getHeroStandardInfoString();
					tooltipScript.showText(heroStandardInfoString);
				}
				break;
			case "NormalSmithsObj":
				if (array[2] != "0")
				{
					GameData gameData = game.getGameData();
					Smith smithByRefId = gameData.getSmithByRefId(array[2]);
					string smithStandardInfoString = smithByRefId.getSmithStandardInfoString(showFullJobDetails: false, showCurrentState: false);
					tooltipScript.showText(smithStandardInfoString);
				}
				break;
			case "OutsourceSmithsObj":
				if (array[2] != "0")
				{
					GameData gameData2 = game.getGameData();
					Smith outsourceSmithByRefId = gameData2.getOutsourceSmithByRefId(array[2]);
					string outsourceSmithStandardInfoString = outsourceSmithByRefId.getOutsourceSmithStandardInfoString();
					tooltipScript.showText(outsourceSmithStandardInfoString);
				}
				break;
			case "AwardWeapon":
				if (array[1] != "0")
				{
					Player player = game.getPlayer();
					Project projectById = player.getProjectById(array[1]);
					tooltipScript.showText(projectById.getProjectName(includePrefix: true));
				}
				break;
			}
		}
		else
		{
			tooltipScript.setInactive();
		}
	}

	public void setReference()
	{
		GameData gameData = game.getGameData();
		commonScreenObject.findChild(base.gameObject, "Journal_bg/JournalTitle_bg/JournalTitle_label").GetComponent<UILabel>().text = gameData.getTextByRefId("journal01").ToUpper(CultureInfo.InvariantCulture);
		commonScreenObject.findChild(book, "SectionTabs/BestWeaponTab_bg/BestWeaponTab_label").GetComponent<UILabel>().text = gameData.getTextByRefId("journal02");
		commonScreenObject.findChild(book, "SectionTabs/AwardsTab_bg/AwardsTab_label").GetComponent<UILabel>().text = gameData.getTextByRefId("journal03");
		commonScreenObject.findChild(book, "SectionTabs/HeroesTab_bg/HeroesTab_label").GetComponent<UILabel>().text = gameData.getTextByRefId("journal04");
		commonScreenObject.findChild(book, "SectionTabs/SmithsTab_bg/SmithsTab_label").GetComponent<UILabel>().text = gameData.getTextByRefId("journal05");
		showJournalSection("WEAPON");
	}

	private void showJournalSection(string aShowTab)
	{
		if (aShowTab != journalTab)
		{
			journalTab = aShowTab;
			clearPreviousSection();
			currentPage = 1;
			journalSubTabsObj = null;
			journalContentObj = null;
			updateJournalSection();
			updateJournalTabs();
		}
	}

	private void updateJournalSection()
	{
		switch (journalTab)
		{
		case "WEAPON":
			showWeaponsSection();
			break;
		case "AWARD":
			showAwardsSection();
			break;
		case "HERO":
			showHeroSection();
			break;
		case "SMITH":
			showSmithSection();
			break;
		}
	}

	private void clearPreviousSection()
	{
		if (journalSubTabsObj != null)
		{
			commonScreenObject.destroyPrefabImmediate(journalSubTabsObj);
		}
		if (journalContentObj != null)
		{
			commonScreenObject.destroyPrefabImmediate(journalContentObj);
		}
	}

	private void clearPreviousContent()
	{
		if (journalContentObj != null)
		{
			commonScreenObject.destroyPrefabImmediate(journalContentObj);
		}
	}

	private void showWeaponsSection()
	{
		GameData gameData = game.getGameData();
		journalSubTabsObj = commonScreenObject.createPrefab(subTabs, "WeaponsSubTabs", "Prefab/Journal/WeaponsSubTabs", new Vector3(10f, 175f, 0f), Vector3.one, Vector3.zero);
		GameObject gameObject = commonScreenObject.createPrefab(journalSubTabsObj, "WeaponTypeSubTab_100", "Prefab/Journal/WeaponTypeSubTab", Vector3.zero, Vector3.one, Vector3.zero);
		commonScreenObject.findChild(gameObject, "WeaponType_icon").GetComponent<UISprite>().alpha = 0f;
		gameObject.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("journal10").ToUpper(CultureInfo.InvariantCulture);
		List<WeaponType> weaponTypeList = gameData.getWeaponTypeList();
		foreach (WeaponType item in weaponTypeList)
		{
			if (checkWeaponTypeShow(item))
			{
				GameObject gameObject2 = commonScreenObject.createPrefab(journalSubTabsObj, "WeaponTypeSubTab_" + item.getWeaponTypeRefId(), "Prefab/Journal/WeaponTypeSubTab", Vector3.zero, Vector3.one, Vector3.zero);
				UISprite component = commonScreenObject.findChild(gameObject2, "WeaponType_icon").GetComponent<UISprite>();
				component.spriteName = "icon_" + item.getImage();
				component.alpha = 1f;
				gameObject2.GetComponentInChildren<UILabel>().text = string.Empty;
			}
		}
		journalSubTabsObj.GetComponent<UIGrid>().Reposition();
		currentSubTab = "100";
		showWeaponSubSection();
		hidePageNumbers();
	}

	private void showWeaponSubSection()
	{
		GameData gameData = game.getGameData();
		journalContentObj = commonScreenObject.createPrefab(book, "BestWeaponSectionObj", "Prefab/Journal/BestWeaponSectionObj", Vector3.zero, Vector3.one, Vector3.zero);
		UILabel component = commonScreenObject.findChild(journalContentObj, "BestWeaponLeft_grid/OverallTitle_bg/OverallTitle_label").GetComponent<UILabel>();
		UILabel component2 = commonScreenObject.findChild(journalContentObj, "BestWeaponRight_grid/AttackWeapon/AttackTitle_bg/AttackTitle_label").GetComponent<UILabel>();
		UILabel component3 = commonScreenObject.findChild(journalContentObj, "BestWeaponRight_grid/SpeedWeapon/SpeedTitle_bg/SpeedTitle_label").GetComponent<UILabel>();
		UILabel component4 = commonScreenObject.findChild(journalContentObj, "BestWeaponRight_grid/AccuracyWeapon/AccuracyTitle_bg/AccuracyTitle_label").GetComponent<UILabel>();
		UILabel component5 = commonScreenObject.findChild(journalContentObj, "BestWeaponRight_grid/MagicWeapon/MagicTitle_bg/MagicTitle_label").GetComponent<UILabel>();
		component.text = gameData.getTextByRefId("journal15").ToUpper(CultureInfo.InvariantCulture);
		component2.text = gameData.getTextByRefId("journal11").ToUpper(CultureInfo.InvariantCulture);
		component3.text = gameData.getTextByRefId("journal12").ToUpper(CultureInfo.InvariantCulture);
		component4.text = gameData.getTextByRefId("journal13").ToUpper(CultureInfo.InvariantCulture);
		component5.text = gameData.getTextByRefId("journal14").ToUpper(CultureInfo.InvariantCulture);
		updateWeaponSubSection();
		updateWeaponTypeSubTabs();
	}

	private void updateWeaponSubSection()
	{
		Player player = game.getPlayer();
		if (!(journalContentObj.name == "BestWeaponSectionObj"))
		{
			return;
		}
		List<Project> list = null;
		Project displayProject = null;
		Project displayProject2 = null;
		Project displayProject3 = null;
		Project displayProject4 = null;
		Project displayProject5 = null;
		list = ((!(currentSubTab == "100")) ? player.getCompletedProjectListByFilterType(ProjectType.ProjectTypeWeapon, CollectionFilterState.CollectionFilterStateAll, currentSubTab) : player.getCompletedProjectListByFilterType(ProjectType.ProjectTypeWeapon, CollectionFilterState.CollectionFilterStateAll, "00000"));
		if (list.Count > 0)
		{
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			int num4 = -1;
			int num5 = -1;
			foreach (Project item in list)
			{
				if (item.getTotalStat() >= num)
				{
					num = item.getTotalStat();
					displayProject = item;
				}
				if (item.getAtk() >= num2)
				{
					num2 = item.getAtk();
					displayProject2 = item;
				}
				if (item.getSpd() >= num3)
				{
					num3 = item.getSpd();
					displayProject3 = item;
				}
				if (item.getAcc() >= num4)
				{
					num4 = item.getAcc();
					displayProject4 = item;
				}
				if (item.getMag() >= num5)
				{
					num5 = item.getMag();
					displayProject5 = item;
				}
			}
		}
		GameObject displayRoot = commonScreenObject.findChild(journalContentObj, "BestWeaponLeft_grid").gameObject;
		displayProjectDetails(displayRoot, displayProject, "Overall");
		GameObject displayRoot2 = commonScreenObject.findChild(journalContentObj, "BestWeaponRight_grid/AttackWeapon").gameObject;
		displayProjectDetails(displayRoot2, displayProject2, "Attack");
		GameObject displayRoot3 = commonScreenObject.findChild(journalContentObj, "BestWeaponRight_grid/SpeedWeapon").gameObject;
		displayProjectDetails(displayRoot3, displayProject3, "Speed");
		GameObject displayRoot4 = commonScreenObject.findChild(journalContentObj, "BestWeaponRight_grid/AccuracyWeapon").gameObject;
		displayProjectDetails(displayRoot4, displayProject4, "Accuracy");
		GameObject displayRoot5 = commonScreenObject.findChild(journalContentObj, "BestWeaponRight_grid/MagicWeapon").gameObject;
		displayProjectDetails(displayRoot5, displayProject5, "Magic");
	}

	private void displayProjectDetails(GameObject displayRoot, Project displayProject, string displayType)
	{
		if (displayProject != null)
		{
			UITexture component = commonScreenObject.findChild(displayRoot, displayType + "_bg/" + displayType + "_texture").GetComponent<UITexture>();
			component.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + displayProject.getProjectWeapon().getImage());
			UILabel component2 = commonScreenObject.findChild(displayRoot, displayType + "Name_bg/" + displayType + "Name_label").GetComponent<UILabel>();
			component2.text = "[000000]" + displayProject.getProjectName(includePrefix: true) + "[-]";
			GameObject aObject = commonScreenObject.findChild(displayRoot, displayType + "WeaponStats").gameObject;
			UILabel component3 = commonScreenObject.findChild(aObject, "atk_sprite/atk_label").GetComponent<UILabel>();
			UILabel component4 = commonScreenObject.findChild(aObject, "spd_sprite/spd_label").GetComponent<UILabel>();
			UILabel component5 = commonScreenObject.findChild(aObject, "acc_sprite/acc_label").GetComponent<UILabel>();
			UILabel component6 = commonScreenObject.findChild(aObject, "mag_sprite/mag_label").GetComponent<UILabel>();
			UISprite component7 = commonScreenObject.findChild(aObject, "atk_sprite/atk_bg").GetComponent<UISprite>();
			UISprite component8 = commonScreenObject.findChild(aObject, "spd_sprite/spd_bg").GetComponent<UISprite>();
			UISprite component9 = commonScreenObject.findChild(aObject, "acc_sprite/acc_bg").GetComponent<UISprite>();
			UISprite component10 = commonScreenObject.findChild(aObject, "mag_sprite/mag_bg").GetComponent<UISprite>();
			component3.text = string.Empty + displayProject.getAtk();
			component5.text = string.Empty + displayProject.getAcc();
			component4.text = string.Empty + displayProject.getSpd();
			component6.text = string.Empty + displayProject.getMag();
			component3.effectStyle = UILabel.Effect.None;
			component5.effectStyle = UILabel.Effect.None;
			component4.effectStyle = UILabel.Effect.None;
			component6.effectStyle = UILabel.Effect.None;
			component3.fontSize = 14;
			component5.fontSize = 14;
			component4.fontSize = 14;
			component6.fontSize = 14;
			component7.color = new Color(0.0235f, 0.157f, 0.196f);
			component8.color = new Color(0.0235f, 0.157f, 0.196f);
			component9.color = new Color(0.0235f, 0.157f, 0.196f);
			component10.color = new Color(0.0235f, 0.157f, 0.196f);
			List<WeaponStat> priSecStat = displayProject.getPriSecStat();
			if (priSecStat.Count > 0)
			{
				switch (priSecStat[0])
				{
				case WeaponStat.WeaponStatAttack:
					component3.effectStyle = UILabel.Effect.Outline;
					component3.fontSize = 15;
					component7.color = new Color(0.0196f, 0.788f, 0.659f);
					break;
				case WeaponStat.WeaponStatSpeed:
					component4.effectStyle = UILabel.Effect.Outline;
					component4.fontSize = 15;
					component8.color = new Color(0.0196f, 0.788f, 0.659f);
					break;
				case WeaponStat.WeaponStatAccuracy:
					component5.effectStyle = UILabel.Effect.Outline;
					component5.fontSize = 15;
					component9.color = new Color(0.0196f, 0.788f, 0.659f);
					break;
				case WeaponStat.WeaponStatMagic:
					component6.effectStyle = UILabel.Effect.Outline;
					component6.fontSize = 15;
					component10.color = new Color(0.0196f, 0.788f, 0.659f);
					break;
				}
			}
			if (priSecStat.Count > 1)
			{
				switch (priSecStat[1])
				{
				case WeaponStat.WeaponStatAttack:
					component7.color = new Color(0.0235f, 0.522f, 0.439f);
					break;
				case WeaponStat.WeaponStatSpeed:
					component8.color = new Color(0.0235f, 0.522f, 0.439f);
					break;
				case WeaponStat.WeaponStatAccuracy:
					component9.color = new Color(0.0235f, 0.522f, 0.439f);
					break;
				case WeaponStat.WeaponStatMagic:
					component10.color = new Color(0.0235f, 0.522f, 0.439f);
					break;
				}
			}
		}
		else
		{
			UITexture component11 = commonScreenObject.findChild(displayRoot, displayType + "_bg/" + displayType + "_texture").GetComponent<UITexture>();
			component11.mainTexture = null;
			UILabel component12 = commonScreenObject.findChild(displayRoot, displayType + "Name_bg/" + displayType + "Name_label").GetComponent<UILabel>();
			component12.text = "[000000]???[-]";
			GameObject aObject2 = commonScreenObject.findChild(displayRoot, displayType + "WeaponStats").gameObject;
			UILabel component13 = commonScreenObject.findChild(aObject2, "atk_sprite/atk_label").GetComponent<UILabel>();
			UILabel component14 = commonScreenObject.findChild(aObject2, "spd_sprite/spd_label").GetComponent<UILabel>();
			UILabel component15 = commonScreenObject.findChild(aObject2, "acc_sprite/acc_label").GetComponent<UILabel>();
			UILabel component16 = commonScreenObject.findChild(aObject2, "mag_sprite/mag_label").GetComponent<UILabel>();
			UISprite component17 = commonScreenObject.findChild(aObject2, "atk_sprite/atk_bg").GetComponent<UISprite>();
			UISprite component18 = commonScreenObject.findChild(aObject2, "spd_sprite/spd_bg").GetComponent<UISprite>();
			UISprite component19 = commonScreenObject.findChild(aObject2, "acc_sprite/acc_bg").GetComponent<UISprite>();
			UISprite component20 = commonScreenObject.findChild(aObject2, "mag_sprite/mag_bg").GetComponent<UISprite>();
			component13.text = "???";
			component15.text = "???";
			component14.text = "???";
			component16.text = "???";
			component13.effectStyle = UILabel.Effect.None;
			component15.effectStyle = UILabel.Effect.None;
			component14.effectStyle = UILabel.Effect.None;
			component16.effectStyle = UILabel.Effect.None;
			component13.fontSize = 14;
			component15.fontSize = 14;
			component14.fontSize = 14;
			component16.fontSize = 14;
			component17.color = new Color(0.0235f, 0.157f, 0.196f);
			component18.color = new Color(0.0235f, 0.157f, 0.196f);
			component19.color = new Color(0.0235f, 0.157f, 0.196f);
			component20.color = new Color(0.0235f, 0.157f, 0.196f);
		}
	}

	private void updateWeaponTypeSubTabs()
	{
		if (!(journalSubTabsObj != null))
		{
			return;
		}
		foreach (Transform child in journalSubTabsObj.GetComponent<UIGrid>().GetChildList())
		{
			if (child.name.Split('_')[1] == currentSubTab)
			{
				child.GetComponent<UIButton>().isEnabled = false;
				child.GetComponentInChildren<UILabel>().color = new Color(0.94f, 0.11f, 0.32f);
			}
			else
			{
				child.GetComponent<UIButton>().isEnabled = true;
				child.GetComponentInChildren<UILabel>().color = Color.white;
			}
		}
	}

	private bool checkWeaponTypeShow(WeaponType checkType)
	{
		Player player = game.getPlayer();
		List<Project> completedProjectListByFilterType = player.getCompletedProjectListByFilterType(ProjectType.ProjectTypeWeapon, CollectionFilterState.CollectionFilterStateAll, checkType.getWeaponTypeRefId(), sortById: false);
		if (completedProjectListByFilterType.Count > 0)
		{
			return true;
		}
		return false;
	}

	private void showAwardsSection()
	{
		Player player = game.getPlayer();
		int showAwardsFromYear = player.getShowAwardsFromYear();
		int num = player.getNextGoldenHammerYear() - showAwardsFromYear;
		if (num > 0)
		{
			journalContentObj = commonScreenObject.createPrefab(book, "AwardsSectionObj", "Prefab/Journal/AwardsSectionObj", Vector3.zero, Vector3.one, Vector3.zero);
			showAwardsPage();
		}
		else
		{
			journalContentObj = commonScreenObject.createPrefab(book, "EmptyAwardsSectionObj", "Prefab/Journal/EmptyAwardsSectionObj", Vector3.zero, Vector3.one, Vector3.zero);
			journalContentObj.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("journal18");
			hidePageNumbers();
		}
	}

	private void showAwardsPage()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		int showAwardsFromYear = player.getShowAwardsFromYear();
		int nextGoldenHammerYear = player.getNextGoldenHammerYear();
		int max = nextGoldenHammerYear - showAwardsFromYear;
		int num = nextGoldenHammerYear - currentPage + 1;
		if (journalContentObj.name == "AwardsSectionObj")
		{
			UILabel component = commonScreenObject.findChild(journalContentObj, "AwardsLeft_grid/AwardOverall_bg/YearTitle_bg/YearTitle_label").GetComponent<UILabel>();
			component.text = gameData.getTextByRefIdWithDynText("yearLong", "[year]", num.ToString());
			Dictionary<ProjectAchievement, Project> awardedProjectList = player.getAwardedProjectList(num - 1);
			UISprite component2 = commonScreenObject.findChild(journalContentObj, "AwardsLeft_grid/AwardOverall_bg/OverallTrophy_sprite").GetComponent<UISprite>();
			UILabel component3 = commonScreenObject.findChild(journalContentObj, "AwardsLeft_grid/AwardOverall_bg/OverallTrophy_label").GetComponent<UILabel>();
			UILabel component4 = commonScreenObject.findChild(journalContentObj, "AwardsLeft_grid/AwardOverall_bg/OverallTitle_bg/OverallTitle_label").GetComponent<UILabel>();
			UILabel component5 = commonScreenObject.findChild(journalContentObj, "AwardsLeft_grid/OverallWeapon/OverallName_bg/OverallName_label").GetComponent<UILabel>();
			UITexture component6 = commonScreenObject.findChild(journalContentObj, "AwardsLeft_grid/OverallWeapon/OverallWeapon_bg/OverallWeapon_texture").GetComponent<UITexture>();
			GameObject aStatRoot = commonScreenObject.findChild(journalContentObj, "AwardsLeft_grid/OverallWeapon/OverallWeaponStats").gameObject;
			if (awardedProjectList.ContainsKey(ProjectAchievement.ProjectAchievementGoldenHammerOverall))
			{
				Project project = awardedProjectList[ProjectAchievement.ProjectAchievementGoldenHammerOverall];
				component2.alpha = 1f;
				component3.text = string.Empty;
				component5.text = "[000000]" + project.getProjectName(includePrefix: true) + "[-]";
				component4.text = gameData.getTextByRefId("goldenHammerText15");
				component6.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + project.getProjectWeapon().getImage());
				showProjectStats(project, aStatRoot);
			}
			else
			{
				component2.alpha = 0f;
				component3.text = gameData.getTextByRefId("journal17").ToUpper(CultureInfo.InvariantCulture);
				component5.text = string.Empty;
				component4.text = gameData.getTextByRefId("goldenHammerText15");
				component6.mainTexture = null;
				showProjectStats(null, aStatRoot);
			}
			int num2 = 1;
			List<ProjectAchievement> pastGoldenHammerAwardsByYear = player.getPastGoldenHammerAwardsByYear(num - 1);
			foreach (ProjectAchievement item in pastGoldenHammerAwardsByYear)
			{
				if (num2 > 2)
				{
					break;
				}
				GameObject aStatRoot2 = commonScreenObject.findChild(journalContentObj, "AwardsRight_grid/Award" + num2 + "_bg").gameObject;
				if (awardedProjectList.ContainsKey(item))
				{
					showAward(awardedProjectList[item], aStatRoot2, item);
				}
				else
				{
					showAward(null, aStatRoot2, item);
				}
				num2++;
			}
		}
		updatePageNumbersYear(num, currentPage, max);
	}

	private void showAward(Project aProject, GameObject aStatRoot, ProjectAchievement aAchievement)
	{
		GameData gameData = game.getGameData();
		UISprite component = commonScreenObject.findChild(aStatRoot, "AwardTrophy_sprite").GetComponent<UISprite>();
		UILabel component2 = commonScreenObject.findChild(aStatRoot, "AwardTrophy_label").GetComponent<UILabel>();
		UILabel component3 = commonScreenObject.findChild(aStatRoot, "AwardTitle_bg/AwardTitle_label").GetComponent<UILabel>();
		UITexture componentInChildren = commonScreenObject.findChild(aStatRoot, "AwardWinner_bg").GetComponentInChildren<UITexture>();
		UISprite component4 = commonScreenObject.findChild(aStatRoot, "AwardWinner_bg/stat_sprite").GetComponent<UISprite>();
		UILabel component5 = commonScreenObject.findChild(aStatRoot, "AwardWinner_bg/stat_sprite/stat_label").GetComponent<UILabel>();
		if (aProject != null)
		{
			componentInChildren.transform.parent.name = "AwardWeapon_" + aProject.getProjectId();
			switch (aAchievement)
			{
			case ProjectAchievement.ProjectAchievementGoldenHammerAttack:
				component.spriteName = "trophy-most powerful";
				component.alpha = 1f;
				component3.text = gameData.getTextByRefId("goldenHammerText11");
				component4.spriteName = "ico_atk";
				component5.text = CommonAPI.formatNumber(aProject.getAtk());
				break;
			case ProjectAchievement.ProjectAchievementGoldenHammerSpeed:
				component.spriteName = "trophy-the fastest";
				component.alpha = 1f;
				component3.text = gameData.getTextByRefId("goldenHammerText12");
				component4.spriteName = "ico_speed";
				component5.text = CommonAPI.formatNumber(aProject.getSpd());
				break;
			case ProjectAchievement.ProjectAchievementGoldenHammerAccuracy:
				component.spriteName = "trophy-most accurate";
				component.alpha = 1f;
				component3.text = gameData.getTextByRefId("goldenHammerText13");
				component4.spriteName = "ico_acc";
				component5.text = CommonAPI.formatNumber(aProject.getAcc());
				break;
			case ProjectAchievement.ProjectAchievementGoldenHammerMagic:
				component.spriteName = "trophy-most magical";
				component.alpha = 1f;
				component3.text = gameData.getTextByRefId("goldenHammerText14");
				component4.spriteName = "ico_enh";
				component5.text = CommonAPI.formatNumber(aProject.getMag());
				break;
			}
			component2.text = string.Empty;
			componentInChildren.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + aProject.getProjectWeapon().getImage());
		}
		else
		{
			componentInChildren.transform.parent.name = "AwardWeapon_0";
			switch (aAchievement)
			{
			case ProjectAchievement.ProjectAchievementGoldenHammerAttack:
				component3.text = gameData.getTextByRefId("goldenHammerText11");
				component4.spriteName = "ico_atk";
				break;
			case ProjectAchievement.ProjectAchievementGoldenHammerSpeed:
				component3.text = gameData.getTextByRefId("goldenHammerText12");
				component4.spriteName = "ico_speed";
				break;
			case ProjectAchievement.ProjectAchievementGoldenHammerAccuracy:
				component3.text = gameData.getTextByRefId("goldenHammerText13");
				component4.spriteName = "ico_acc";
				break;
			case ProjectAchievement.ProjectAchievementGoldenHammerMagic:
				component3.text = gameData.getTextByRefId("goldenHammerText14");
				component4.spriteName = "ico_enh";
				break;
			}
			component.alpha = 0f;
			component5.text = string.Empty;
			component2.text = gameData.getTextByRefId("journal17").ToUpper(CultureInfo.InvariantCulture);
			componentInChildren.mainTexture = null;
		}
	}

	private void showProjectStats(Project aProject, GameObject aStatRoot)
	{
		UILabel component = commonScreenObject.findChild(aStatRoot, "atk_sprite/atk_label").GetComponent<UILabel>();
		UILabel component2 = commonScreenObject.findChild(aStatRoot, "spd_sprite/spd_label").GetComponent<UILabel>();
		UILabel component3 = commonScreenObject.findChild(aStatRoot, "acc_sprite/acc_label").GetComponent<UILabel>();
		UILabel component4 = commonScreenObject.findChild(aStatRoot, "mag_sprite/mag_label").GetComponent<UILabel>();
		UISprite component5 = commonScreenObject.findChild(aStatRoot, "atk_sprite/atk_bg").GetComponent<UISprite>();
		UISprite component6 = commonScreenObject.findChild(aStatRoot, "spd_sprite/spd_bg").GetComponent<UISprite>();
		UISprite component7 = commonScreenObject.findChild(aStatRoot, "acc_sprite/acc_bg").GetComponent<UISprite>();
		UISprite component8 = commonScreenObject.findChild(aStatRoot, "mag_sprite/mag_bg").GetComponent<UISprite>();
		if (aProject != null)
		{
			component.text = string.Empty + aProject.getAtk();
			component2.text = string.Empty + aProject.getSpd();
			component3.text = string.Empty + aProject.getAcc();
			component4.text = string.Empty + aProject.getMag();
			component.effectStyle = UILabel.Effect.None;
			component2.effectStyle = UILabel.Effect.None;
			component3.effectStyle = UILabel.Effect.None;
			component4.effectStyle = UILabel.Effect.None;
			component.fontSize = 14;
			component2.fontSize = 14;
			component3.fontSize = 14;
			component4.fontSize = 14;
			component5.color = new Color(0.0235f, 0.157f, 0.196f);
			component6.color = new Color(0.0235f, 0.157f, 0.196f);
			component7.color = new Color(0.0235f, 0.157f, 0.196f);
			component8.color = new Color(0.0235f, 0.157f, 0.196f);
			List<WeaponStat> priSecStat = aProject.getPriSecStat();
			if (priSecStat.Count > 0)
			{
				switch (priSecStat[0])
				{
				case WeaponStat.WeaponStatAttack:
					component.effectStyle = UILabel.Effect.Outline;
					component.fontSize = 15;
					component5.color = new Color(0.0196f, 0.788f, 0.659f);
					break;
				case WeaponStat.WeaponStatSpeed:
					component2.effectStyle = UILabel.Effect.Outline;
					component2.fontSize = 15;
					component6.color = new Color(0.0196f, 0.788f, 0.659f);
					break;
				case WeaponStat.WeaponStatAccuracy:
					component3.effectStyle = UILabel.Effect.Outline;
					component3.fontSize = 15;
					component7.color = new Color(0.0196f, 0.788f, 0.659f);
					break;
				case WeaponStat.WeaponStatMagic:
					component4.effectStyle = UILabel.Effect.Outline;
					component4.fontSize = 15;
					component8.color = new Color(0.0196f, 0.788f, 0.659f);
					break;
				}
			}
			if (priSecStat.Count > 1)
			{
				switch (priSecStat[1])
				{
				case WeaponStat.WeaponStatAttack:
					component5.color = new Color(0.0235f, 0.522f, 0.439f);
					break;
				case WeaponStat.WeaponStatSpeed:
					component6.color = new Color(0.0235f, 0.522f, 0.439f);
					break;
				case WeaponStat.WeaponStatAccuracy:
					component7.color = new Color(0.0235f, 0.522f, 0.439f);
					break;
				case WeaponStat.WeaponStatMagic:
					component8.color = new Color(0.0235f, 0.522f, 0.439f);
					break;
				}
			}
		}
		else
		{
			component.text = string.Empty;
			component2.text = string.Empty;
			component3.text = string.Empty;
			component4.text = string.Empty;
			component5.color = new Color(0.0235f, 0.157f, 0.196f);
			component6.color = new Color(0.0235f, 0.157f, 0.196f);
			component7.color = new Color(0.0235f, 0.157f, 0.196f);
			component8.color = new Color(0.0235f, 0.157f, 0.196f);
		}
	}

	private void showHeroSection()
	{
		journalSubTabsObj = commonScreenObject.createPrefab(subTabs, "HeroSubTabs", "Prefab/Journal/HeroSubTabs", Vector3.zero, Vector3.one, Vector3.zero);
		UILabel[] componentsInChildren = journalSubTabsObj.GetComponentsInChildren<UILabel>();
		foreach (UILabel uILabel in componentsInChildren)
		{
			switch (uILabel.name)
			{
			case "Normal_label":
				uILabel.text = gameData.getTextByRefId("journal06");
				break;
			case "Legendary_label":
				uILabel.text = gameData.getTextByRefId("journal08");
				break;
			}
		}
		currentSubTab = "NORMAL";
		showHeroSubSection();
	}

	private void showHeroSubSection()
	{
		currentPage = 1;
		switch (currentSubTab)
		{
		case "NORMAL":
			showNormalHeroSection();
			break;
		case "LEGENDARY":
			showLegendaryHeroSection();
			break;
		}
		updateHeroSubTabs();
	}

	private void updateHeroSubTabs()
	{
		if (journalSubTabsObj != null)
		{
			UIButton component = commonScreenObject.findChild(journalSubTabsObj, "Normal_subTab").GetComponent<UIButton>();
			UIButton component2 = commonScreenObject.findChild(journalSubTabsObj, "Legendary_subTab").GetComponent<UIButton>();
			switch (currentSubTab)
			{
			case "NORMAL":
				component.isEnabled = false;
				component2.isEnabled = true;
				break;
			case "LEGENDARY":
				component.isEnabled = true;
				component2.isEnabled = false;
				break;
			}
			if (component.isEnabled)
			{
				component.GetComponentInChildren<UILabel>().color = Color.white;
			}
			else
			{
				component.GetComponentInChildren<UILabel>().color = new Color(0.94f, 0.11f, 0.32f);
			}
			if (component2.isEnabled)
			{
				component2.GetComponentInChildren<UILabel>().color = Color.white;
			}
			else
			{
				component2.GetComponentInChildren<UILabel>().color = new Color(0.94f, 0.11f, 0.32f);
			}
		}
	}

	private void showNormalHeroSection()
	{
		clearPreviousContent();
		journalContentObj = commonScreenObject.createPrefab(book, "NormalHeroSectionObj", "Prefab/Journal/NormalHeroSectionObj", Vector3.zero, Vector3.one, Vector3.zero);
		showNormalHeroPage();
	}

	private void showNormalHeroPage()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string itemLockSet = gameScenarioByRefId.getItemLockSet();
		clearPreviousNormalHeroPage();
		List<Hero> heroList = gameData.getHeroList(itemLockSet);
		int num = heroList.Count / 4;
		if (heroList.Count % 4 > 0)
		{
			num++;
		}
		if (currentPage > num)
		{
			currentPage = num;
		}
		if (currentPage % 2 == 0)
		{
			currentPage--;
		}
		int num2 = 100001;
		int num3 = 0;
		UIGrid component = commonScreenObject.findChild(journalContentObj, "NormalHeroLeft_grid").GetComponent<UIGrid>();
		UIGrid component2 = commonScreenObject.findChild(journalContentObj, "NormalHeroRight_grid").GetComponent<UIGrid>();
		foreach (Hero item in heroList)
		{
			if (num3 / 4 == currentPage - 1)
			{
				showNormalHeroObj(item, component.gameObject, "NormalHeroObj_" + num2);
				num2++;
			}
			else if (num3 / 4 == currentPage)
			{
				showNormalHeroObj(item, component2.gameObject, "NormalHeroObj_" + num2);
				num2++;
			}
			num3++;
		}
		component.Reposition();
		component2.Reposition();
		updatePageNumbers(currentPage, num);
	}

	private void clearPreviousNormalHeroPage()
	{
		if (!(journalContentObj != null) || !(journalContentObj.name == "NormalHeroSectionObj"))
		{
			return;
		}
		UIGrid[] componentsInChildren = journalContentObj.GetComponentsInChildren<UIGrid>();
		foreach (UIGrid uIGrid in componentsInChildren)
		{
			foreach (Transform child in uIGrid.GetChildList())
			{
				commonScreenObject.destroyPrefabImmediate(child.gameObject);
			}
		}
	}

	private void showLegendaryHeroSection()
	{
		clearPreviousContent();
		journalContentObj = commonScreenObject.createPrefab(book, "LegendaryHeroSectionObj", "Prefab/Journal/LegendaryHeroSectionObj", Vector3.zero, Vector3.one, Vector3.zero);
		showLegendaryHeroPage();
	}

	private void showLegendaryHeroPage()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		clearPreviousLegendaryHeroPage();
		List<LegendaryHero> legendaryHeroList = gameData.getLegendaryHeroList(checkDLC: true, gameScenarioByRefId.getItemLockSet());
		int count = legendaryHeroList.Count;
		if (currentPage > count)
		{
			currentPage = count;
		}
		if (currentPage % 2 == 0)
		{
			currentPage--;
		}
		int num = 100001;
		int num2 = 0;
		GameObject parent = commonScreenObject.findChild(journalContentObj, "LegendaryHeroLeft_grid").gameObject;
		GameObject parent2 = commonScreenObject.findChild(journalContentObj, "LegendaryHeroRight_grid").gameObject;
		foreach (LegendaryHero item in legendaryHeroList)
		{
			if (item.getRequestState() == RequestState.RequestStateCompleted)
			{
				if (num2 == currentPage - 1)
				{
					showLegendaryHeroObj(item, parent, "LegendaryHeroObj_" + num);
					num++;
				}
				else if (num2 == currentPage)
				{
					showLegendaryHeroObj(item, parent2, "LegendaryHeroObj_" + num);
					num++;
				}
				num2++;
			}
		}
		UILabel component = commonScreenObject.findChild(journalContentObj, "LegendaryHeroEmpty_label").GetComponent<UILabel>();
		if (num2 > 0)
		{
			component.text = string.Empty;
			updatePageNumbers(currentPage, num2);
		}
		else
		{
			component.text = gameData.getTextByRefId("journal25");
			hidePageNumbers();
		}
	}

	private void clearPreviousLegendaryHeroPage()
	{
		if (journalContentObj != null && journalContentObj.name == "LegendaryHeroSectionObj")
		{
			GameObject gameObject = commonScreenObject.findChild(journalContentObj, "LegendaryHeroLeft_grid").gameObject;
			GameObject gameObject2 = commonScreenObject.findChild(journalContentObj, "LegendaryHeroRight_grid").gameObject;
			if (gameObject.transform.childCount > 0)
			{
				commonScreenObject.destroyPrefabImmediate(gameObject.transform.GetChild(0).gameObject);
			}
			if (gameObject2.transform.childCount > 0)
			{
				commonScreenObject.destroyPrefabImmediate(gameObject2.transform.GetChild(0).gameObject);
			}
		}
	}

	private void showLegendaryHeroObj(LegendaryHero legendary, GameObject parent, string objNameString)
	{
		GameData gameData = game.getGameData();
		GameObject aObject = commonScreenObject.createPrefab(parent, objNameString + "_" + legendary.getLegendaryHeroRefId(), "Prefab/Journal/LegendaryHeroObj", Vector3.zero, Vector3.one, Vector3.zero);
		Weapon weaponByRefId = gameData.getWeaponByRefId(legendary.getWeaponRefId());
		string text = string.Empty;
		string aPath = string.Empty;
		if (legendary.getRewardItemQty() > 0)
		{
			switch (legendary.getRewardItemType())
			{
			case "DECORATION":
			{
				Decoration decorationByRefId = gameData.getDecorationByRefId(legendary.getRewardItemRefId());
				aPath = "Image/Decoration/" + decorationByRefId.getDecorationImage();
				text = decorationByRefId.getDecorationName();
				break;
			}
			case "ENCHANTMENT":
			{
				Item itemByRefId = gameData.getItemByRefId(legendary.getRewardItemRefId());
				aPath = "Image/Enchantment/" + itemByRefId.getImage();
				text = itemByRefId.getItemName();
				break;
			}
			}
		}
		UILabel component = commonScreenObject.findChild(aObject, "LegendaryHeroName_bg/LegendaryHeroName_label").GetComponent<UILabel>();
		UITexture component2 = commonScreenObject.findChild(aObject, "LegendaryImage_texture").GetComponent<UITexture>();
		UITexture component3 = commonScreenObject.findChild(aObject, "LegendaryWeapon_bg/LegendaryWeapon_texture").GetComponent<UITexture>();
		UILabel component4 = commonScreenObject.findChild(aObject, "CutsceneTitle_bg/CutsceneTitle_label").GetComponent<UILabel>();
		component4.text = gameData.getTextByRefId("journal19");
		UILabel component5 = commonScreenObject.findChild(aObject, "LegendaryReward_bg/LegendaryItemTitle_label").GetComponent<UILabel>();
		component5.text = gameData.getTextByRefId("journal24");
		UILabel component6 = commonScreenObject.findChild(aObject, "RequestedWeaponTitle_label").GetComponent<UILabel>();
		component6.text = gameData.getTextByRefId("journal23");
		UIButton component7 = commonScreenObject.findChild(aObject, "LegendaryHeroIntro_button").GetComponent<UIButton>();
		UIButton component8 = commonScreenObject.findChild(aObject, "LegendaryHeroComplete_button").GetComponent<UIButton>();
		UIButton component9 = commonScreenObject.findChild(aObject, "LegendaryHeroReject_button").GetComponent<UIButton>();
		component7.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("journal20");
		component8.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("journal21");
		component9.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("journal22");
		UILabel component10 = commonScreenObject.findChild(aObject, "LegendaryReward_bg/LegendaryItem_bg/LegendaryItem_label").GetComponent<UILabel>();
		UITexture component11 = commonScreenObject.findChild(aObject, "LegendaryReward_bg/LegendaryItem_texture").GetComponent<UITexture>();
		UILabel component12 = commonScreenObject.findChild(aObject, "RequestedWeaponTitle_label/WeaponName_bg/WeaponName_label").GetComponent<UILabel>();
		if (legendary.getRequestState() == RequestState.RequestStateCompleted)
		{
			component.text = legendary.getLegendaryHeroName();
			component2.mainTexture = commonScreenObject.loadTexture("Image/legendary heroes/standing/" + legendary.getImage() + "_request");
			component2.color = Color.white;
			component3.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + weaponByRefId.getImage());
			component3.color = Color.white;
			component10.text = text;
			component11.mainTexture = commonScreenObject.loadTexture(aPath);
			component11.color = Color.white;
			scaleTexture(component11, 40);
			component12.text = weaponByRefId.getWeaponName();
			component7.name = "LegendaryHeroIntroButton_" + legendary.getLegendaryHeroRefId();
			component7.isEnabled = true;
			component8.name = "LegendaryHeroCompleteButton_" + legendary.getLegendaryHeroRefId();
			component8.isEnabled = true;
			component9.name = "LegendaryHeroRejectButton_" + legendary.getLegendaryHeroRefId();
			component9.isEnabled = true;
		}
		else
		{
			component.text = "???";
			component2.mainTexture = commonScreenObject.loadTexture("Image/legendary heroes/standing/" + legendary.getImage() + "_request");
			component2.color = Color.black;
			component3.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + weaponByRefId.getImage());
			component3.color = Color.black;
			component10.text = "???";
			component11.mainTexture = commonScreenObject.loadTexture(aPath);
			component11.color = Color.black;
			scaleTexture(component11, 40);
			component12.text = "???";
			component7.isEnabled = false;
			component8.isEnabled = false;
			component9.isEnabled = false;
		}
	}

	private void scaleTexture(UITexture imageTexture, int maxHeight)
	{
		if (imageTexture.mainTexture != null)
		{
			int num = imageTexture.mainTexture.width;
			int num2 = imageTexture.mainTexture.height;
			if (num2 > maxHeight)
			{
				num = num * maxHeight / num2;
				num2 = maxHeight;
			}
			imageTexture.SetDimensions(num, num2);
		}
	}

	private void showNormalHeroObj(Hero hero, GameObject parent, string objNameString)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		Area areaByHeroRefId = gameData.getAreaByHeroRefId(hero.getHeroRefId());
		bool flag = areaByHeroRefId.checkIsUnlock();
		if (flag && player.getAreaRegion() < areaByHeroRefId.getRegion())
		{
			flag = false;
		}
		GameObject gameObject = commonScreenObject.createPrefab(parent, objNameString + "_" + hero.getHeroRefId(), "Prefab/Journal/NormalHeroObj", Vector3.zero, Vector3.one, Vector3.zero);
		UISprite component = commonScreenObject.findChild(gameObject, "Hero_bg/HeroMaxLvl_frame").GetComponent<UISprite>();
		UITexture componentInChildren = gameObject.GetComponentInChildren<UITexture>();
		UILabel component2 = commonScreenObject.findChild(gameObject, "HeroName_bg/HeroName_label").GetComponent<UILabel>();
		if (flag)
		{
			componentInChildren.mainTexture = commonScreenObject.loadTexture("Image/Hero/" + hero.getImage());
			componentInChildren.color = Color.white;
			component2.text = hero.getHeroName();
			if (hero.checkHeroMaxLevel())
			{
				component.alpha = 1f;
				component.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("smithStatsShort09");
			}
			else
			{
				component.alpha = 0f;
			}
		}
		else
		{
			gameObject.name = objNameString + "_0";
			componentInChildren.mainTexture = commonScreenObject.loadTexture("Image/Hero/" + hero.getImage());
			componentInChildren.color = Color.black;
			component2.text = "???";
			component.alpha = 0f;
		}
	}

	private void showHeroCutscene(string heroRefId, string cutsceneType)
	{
		GameData gameData = game.getGameData();
		LegendaryHero legendaryHeroByHeroRefId = gameData.getLegendaryHeroByHeroRefId(heroRefId);
		string aSetId = string.Empty;
		Dictionary<string, DialogueNEW> dictionary = new Dictionary<string, DialogueNEW>();
		switch (cutsceneType)
		{
		case "INTRO":
			aSetId = legendaryHeroByHeroRefId.getHeroVisitDialogueSetId();
			break;
		case "COMPLETE":
			aSetId = legendaryHeroByHeroRefId.getForgeSuccessDialogueRefId();
			break;
		case "REJECT":
			aSetId = legendaryHeroByHeroRefId.getForgeFailDialogueSetId();
			break;
		}
		dictionary = gameData.getDialogueBySetId(aSetId);
		if (dictionary.Count > 0)
		{
			viewController.showDialoguePopup(aSetId, dictionary, PopupType.PopupTypeJournal);
			base.transform.localPosition = new Vector3(0f, 2000f, 0f);
		}
	}

	public void showEndCutscene()
	{
		base.transform.localPosition = Vector3.zero;
	}

	private void showSmithSection()
	{
		journalSubTabsObj = commonScreenObject.createPrefab(subTabs, "SmithsSubTabs", "Prefab/Journal/SmithsSubTabs", Vector3.zero, Vector3.one, Vector3.zero);
		UILabel[] componentsInChildren = journalSubTabsObj.GetComponentsInChildren<UILabel>();
		foreach (UILabel uILabel in componentsInChildren)
		{
			switch (uILabel.name)
			{
			case "Normal_label":
				uILabel.text = gameData.getTextByRefId("journal06");
				break;
			case "Outsource_label":
				uILabel.text = gameData.getTextByRefId("journal07");
				break;
			case "Legendary_label":
				uILabel.text = gameData.getTextByRefId("journal08");
				break;
			}
		}
		currentSubTab = "NORMAL";
		showSmithSubSection();
	}

	private void showSmithSubSection()
	{
		currentPage = 1;
		switch (currentSubTab)
		{
		case "NORMAL":
			showNormalSmithSection();
			break;
		case "OUTSOURCE":
			showOutsourceSmithSection();
			break;
		case "LEGENDARY":
			showLegendarySmithSection();
			break;
		}
		updateSmithSubTabs();
	}

	private void updateSmithSubTabs()
	{
		if (journalSubTabsObj != null)
		{
			UIButton component = commonScreenObject.findChild(journalSubTabsObj, "Normal_subTab").GetComponent<UIButton>();
			UIButton component2 = commonScreenObject.findChild(journalSubTabsObj, "Outsource_subTab").GetComponent<UIButton>();
			UIButton component3 = commonScreenObject.findChild(journalSubTabsObj, "Legendary_subTab").GetComponent<UIButton>();
			switch (currentSubTab)
			{
			case "NORMAL":
				component.isEnabled = false;
				component2.isEnabled = true;
				component3.isEnabled = true;
				break;
			case "OUTSOURCE":
				component.isEnabled = true;
				component2.isEnabled = false;
				component3.isEnabled = true;
				break;
			case "LEGENDARY":
				component.isEnabled = true;
				component2.isEnabled = true;
				component3.isEnabled = false;
				break;
			}
			if (component.isEnabled)
			{
				component.GetComponentInChildren<UILabel>().color = Color.white;
			}
			else
			{
				component.GetComponentInChildren<UILabel>().color = new Color(0.94f, 0.11f, 0.32f);
			}
			if (component2.isEnabled)
			{
				component2.GetComponentInChildren<UILabel>().color = Color.white;
			}
			else
			{
				component2.GetComponentInChildren<UILabel>().color = new Color(0.94f, 0.11f, 0.32f);
			}
			if (component3.isEnabled)
			{
				component3.GetComponentInChildren<UILabel>().color = Color.white;
			}
			else
			{
				component3.GetComponentInChildren<UILabel>().color = new Color(0.94f, 0.11f, 0.32f);
			}
		}
	}

	private void showNormalSmithSection()
	{
		clearPreviousContent();
		journalContentObj = commonScreenObject.createPrefab(book, "NormalSmithsSectionObj", "Prefab/Journal/NormalSmithsSectionObj", Vector3.zero, Vector3.one, Vector3.zero);
		showNormalSmithPage();
	}

	private void showNormalSmithPage()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		clearPreviousNormalSmithPage();
		List<Smith> smithList = gameData.getSmithList(checkDLC: true, includeNormal: true, includeLegendary: false, gameScenarioByRefId.getItemLockSet());
		int num = smithList.Count / 4;
		if (smithList.Count % 4 > 0)
		{
			num++;
		}
		if (currentPage > num)
		{
			currentPage = num;
		}
		if (currentPage % 2 == 0)
		{
			currentPage--;
		}
		int num2 = 100001;
		int num3 = 0;
		UIGrid component = commonScreenObject.findChild(journalContentObj, "NormalSmithLeft_grid").GetComponent<UIGrid>();
		UIGrid component2 = commonScreenObject.findChild(journalContentObj, "NormalSmithRight_grid").GetComponent<UIGrid>();
		foreach (Smith item in smithList)
		{
			if (num3 / 4 == currentPage - 1)
			{
				showNormalSmithObj(item, component.gameObject, "NormalSmithsObj_" + num2);
				num2++;
			}
			else if (num3 / 4 == currentPage)
			{
				showNormalSmithObj(item, component2.gameObject, "NormalSmithsObj_" + num2);
				num2++;
			}
			num3++;
		}
		component.Reposition();
		component2.Reposition();
		updatePageNumbers(currentPage, num);
	}

	private void clearPreviousNormalSmithPage()
	{
		if (!(journalContentObj != null) || !(journalContentObj.name == "NormalSmithsSectionObj"))
		{
			return;
		}
		UIGrid[] componentsInChildren = journalContentObj.GetComponentsInChildren<UIGrid>();
		foreach (UIGrid uIGrid in componentsInChildren)
		{
			foreach (Transform child in uIGrid.GetChildList())
			{
				commonScreenObject.destroyPrefabImmediate(child.gameObject);
			}
		}
	}

	private void showOutsourceSmithSection()
	{
		clearPreviousContent();
		journalContentObj = commonScreenObject.createPrefab(book, "OutsourceSmithsSectionObj", "Prefab/Journal/NormalSmithsSectionObj", Vector3.zero, Vector3.one, Vector3.zero);
		showOutsourceSmithPage();
	}

	private void showOutsourceSmithPage()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		clearPreviousOutsourceSmithPage();
		List<Smith> allOutsourceSmithList = gameData.getAllOutsourceSmithList(checkDlc: true, gameScenarioByRefId.getItemLockSet());
		int num = allOutsourceSmithList.Count / 4;
		if (allOutsourceSmithList.Count % 4 > 0)
		{
			num++;
		}
		if (currentPage > num)
		{
			currentPage = num;
		}
		if (currentPage % 2 == 0)
		{
			currentPage--;
		}
		int num2 = 100001;
		int num3 = 0;
		UIGrid component = commonScreenObject.findChild(journalContentObj, "NormalSmithLeft_grid").GetComponent<UIGrid>();
		UIGrid component2 = commonScreenObject.findChild(journalContentObj, "NormalSmithRight_grid").GetComponent<UIGrid>();
		foreach (Smith item in allOutsourceSmithList)
		{
			if (num3 / 4 == currentPage - 1)
			{
				showNormalSmithObj(item, component.gameObject, "OutsourceSmithsObj_" + num2);
				num2++;
			}
			else if (num3 / 4 == currentPage)
			{
				showNormalSmithObj(item, component2.gameObject, "OutsourceSmithsObj_" + num2);
				num2++;
			}
			num3++;
		}
		component.Reposition();
		component2.Reposition();
		updatePageNumbers(currentPage, num);
	}

	private void clearPreviousOutsourceSmithPage()
	{
		if (!(journalContentObj != null) || !(journalContentObj.name == "OutsourceSmithsSectionObj"))
		{
			return;
		}
		UIGrid[] componentsInChildren = journalContentObj.GetComponentsInChildren<UIGrid>();
		foreach (UIGrid uIGrid in componentsInChildren)
		{
			foreach (Transform child in uIGrid.GetChildList())
			{
				commonScreenObject.destroyPrefabImmediate(child.gameObject);
			}
		}
	}

	private void showNormalSmithObj(Smith smith, GameObject parent, string objNameString)
	{
		GameObject gameObject = commonScreenObject.createPrefab(parent, objNameString + "_" + smith.getSmithRefId(), "Prefab/Journal/NormalSmithObj", Vector3.zero, Vector3.one, Vector3.zero);
		if (smith.getTimesHired() > 0)
		{
			gameObject.GetComponentInChildren<UILabel>().text = smith.getSmithName();
			UITexture componentInChildren = gameObject.GetComponentInChildren<UITexture>();
			componentInChildren.mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/" + smith.getImage());
			componentInChildren.color = Color.white;
		}
		else
		{
			gameObject.name = objNameString + "_0";
			gameObject.GetComponentInChildren<UILabel>().text = "???";
			UITexture componentInChildren2 = gameObject.GetComponentInChildren<UITexture>();
			componentInChildren2.mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/" + smith.getImage());
			componentInChildren2.color = Color.black;
		}
	}

	private void showLegendarySmithSection()
	{
		clearPreviousContent();
		journalContentObj = commonScreenObject.createPrefab(book, "LegendarySmithSectionObj", "Prefab/Journal/LegendarySmithSectionObj", Vector3.zero, Vector3.one, Vector3.zero);
		showLegendarySmithPage();
	}

	private void showLegendarySmithPage()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		clearPreviousLegendarySmithPage();
		List<Smith> smithList = gameData.getSmithList(checkDLC: true, includeNormal: false, includeLegendary: true, gameScenarioByRefId.getItemLockSet());
		int count = smithList.Count;
		if (currentPage > count)
		{
			currentPage = count;
		}
		if (currentPage % 2 == 0)
		{
			currentPage--;
		}
		int num = 100001;
		int num2 = 0;
		GameObject parent = commonScreenObject.findChild(journalContentObj, "LegendarySmithLeft_grid").gameObject;
		GameObject parent2 = commonScreenObject.findChild(journalContentObj, "LegendarySmithRight_grid").gameObject;
		foreach (Smith item in smithList)
		{
			if (item.getTimesHired() > 0)
			{
				if (num2 == currentPage - 1)
				{
					showLegendarySmithObj(item, parent, "LegendarySmithsObj_" + num);
					num++;
				}
				else if (num2 == currentPage)
				{
					showLegendarySmithObj(item, parent2, "LegendarySmithsObj_" + num);
					num++;
				}
				num2++;
			}
		}
		UILabel component = commonScreenObject.findChild(journalContentObj, "LegendarySmithEmpty_label").GetComponent<UILabel>();
		if (num2 > 0)
		{
			component.text = string.Empty;
			updatePageNumbers(currentPage, num2);
		}
		else
		{
			component.text = gameData.getTextByRefId("journal26");
			hidePageNumbers();
		}
	}

	private void clearPreviousLegendarySmithPage()
	{
		if (journalContentObj != null && journalContentObj.name == "LegendarySmithSectionObj")
		{
			GameObject gameObject = commonScreenObject.findChild(journalContentObj, "LegendarySmithLeft_grid").gameObject;
			GameObject gameObject2 = commonScreenObject.findChild(journalContentObj, "LegendarySmithRight_grid").gameObject;
			if (gameObject.transform.childCount > 0)
			{
				commonScreenObject.destroyPrefabImmediate(gameObject.transform.GetChild(0).gameObject);
			}
			if (gameObject2.transform.childCount > 0)
			{
				commonScreenObject.destroyPrefabImmediate(gameObject2.transform.GetChild(0).gameObject);
			}
		}
	}

	private void showLegendarySmithObj(Smith smith, GameObject parent, string objNameString)
	{
		GameData gameData = game.getGameData();
		GameObject aObject = commonScreenObject.createPrefab(parent, objNameString + "_" + smith.getSmithRefId(), "Prefab/Journal/LegendarySmithObj", Vector3.zero, Vector3.one, Vector3.zero);
		UILabel component = commonScreenObject.findChild(aObject, "LegendarySmithName_bg/LegendarySmithName_label").GetComponent<UILabel>();
		UITexture component2 = commonScreenObject.findChild(aObject, "LegendaryImage_texture").GetComponent<UITexture>();
		UILabel component3 = commonScreenObject.findChild(aObject, "LegendarySmithDesc_bg/LegendarySmithDesc_label").GetComponent<UILabel>();
		UIButton component4 = commonScreenObject.findChild(aObject, "LegendarySmithCutscene_button").GetComponent<UIButton>();
		component4.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("journal16");
		if (smith.getTimesHired() > 0)
		{
			component.text = smith.getSmithName();
			component2.mainTexture = commonScreenObject.loadTexture("Image/Smith/" + smith.getImage() + "_manage");
			component2.color = Color.white;
			component3.text = smith.getSmithDesc();
			if (smith.getUnlockDialogueSetId() != string.Empty)
			{
				component4.name = "LegendarySmithCutsceneButton_" + smith.getSmithRefId();
				component4.isEnabled = true;
			}
			else
			{
				component4.isEnabled = false;
			}
		}
		else
		{
			component.text = "???";
			component2.mainTexture = commonScreenObject.loadTexture("Image/Smith/" + smith.getImage() + "_manage");
			component2.color = Color.black;
			component3.text = "???";
			component4.isEnabled = false;
		}
	}

	private void showSmithCutscene(string smithRefId)
	{
		GameData gameData = game.getGameData();
		Smith smithByRefId = gameData.getSmithByRefId(smithRefId);
		string unlockDialogueSetId = smithByRefId.getUnlockDialogueSetId();
		Dictionary<string, DialogueNEW> dictionary = new Dictionary<string, DialogueNEW>();
		dictionary = gameData.getDialogueBySetId(unlockDialogueSetId);
		if (dictionary.Count > 0)
		{
			viewController.showDialoguePopup(unlockDialogueSetId, dictionary, PopupType.PopupTypeJournal);
			base.transform.localPosition = new Vector3(0f, 2000f, 0f);
		}
	}

	private void flipPage(int flipDir)
	{
		currentPage += flipDir;
		if (currentPage < 1)
		{
			currentPage = 1;
		}
		switch (journalTab)
		{
		case "AWARD":
			showAwardsPage();
			break;
		case "HERO":
			switch (currentSubTab)
			{
			case "NORMAL":
				showNormalHeroPage();
				break;
			case "LEGENDARY":
				showLegendaryHeroPage();
				break;
			}
			break;
		case "SMITH":
			switch (currentSubTab)
			{
			case "NORMAL":
				showNormalSmithPage();
				break;
			case "OUTSOURCE":
				showOutsourceSmithPage();
				break;
			case "LEGENDARY":
				showLegendarySmithPage();
				break;
			}
			break;
		}
	}

	private void updateJournalTabs()
	{
		switch (journalTab)
		{
		case "WEAPON":
			weaponsTab.isEnabled = false;
			awardsTab.isEnabled = true;
			heroTab.isEnabled = true;
			smithTab.isEnabled = true;
			break;
		case "AWARD":
			weaponsTab.isEnabled = true;
			awardsTab.isEnabled = false;
			heroTab.isEnabled = true;
			smithTab.isEnabled = true;
			break;
		case "HERO":
			weaponsTab.isEnabled = true;
			awardsTab.isEnabled = true;
			heroTab.isEnabled = false;
			smithTab.isEnabled = true;
			break;
		case "SMITH":
			weaponsTab.isEnabled = true;
			awardsTab.isEnabled = true;
			heroTab.isEnabled = true;
			smithTab.isEnabled = false;
			break;
		}
		if (weaponsTab.isEnabled)
		{
			weaponsTab.GetComponentInChildren<UILabel>().color = new Color(0.36f, 0.51f, 0.67f);
		}
		else
		{
			weaponsTab.GetComponentInChildren<UILabel>().color = new Color(0.94f, 0.11f, 0.32f);
		}
		if (awardsTab.isEnabled)
		{
			awardsTab.GetComponentInChildren<UILabel>().color = new Color(0.36f, 0.51f, 0.67f);
		}
		else
		{
			awardsTab.GetComponentInChildren<UILabel>().color = new Color(0.94f, 0.11f, 0.32f);
		}
		if (heroTab.isEnabled)
		{
			heroTab.GetComponentInChildren<UILabel>().color = new Color(0.36f, 0.51f, 0.67f);
		}
		else
		{
			heroTab.GetComponentInChildren<UILabel>().color = new Color(0.94f, 0.11f, 0.32f);
		}
		if (smithTab.isEnabled)
		{
			smithTab.GetComponentInChildren<UILabel>().color = new Color(0.36f, 0.51f, 0.67f);
		}
		else
		{
			smithTab.GetComponentInChildren<UILabel>().color = new Color(0.94f, 0.11f, 0.32f);
		}
	}

	private void updatePageNumbers(int current, int max)
	{
		GameData gameData = game.getGameData();
		int num = current;
		if (current % 2 == 0)
		{
			num = current - 1;
		}
		if (num <= 1)
		{
			pageLeftButton.isEnabled = false;
		}
		else
		{
			pageLeftButton.isEnabled = true;
		}
		if (num + 1 >= max)
		{
			pageRightButton.isEnabled = false;
		}
		else
		{
			pageRightButton.isEnabled = true;
		}
		List<string> list = new List<string>();
		list.Add("[current]");
		list.Add("[max]");
		List<string> list2 = new List<string>();
		list2.Add(num.ToString());
		list2.Add(max.ToString());
		pageNumLeftLabel.text = gameData.getTextByRefIdWithDynTextList("journal09", list, list2);
		pageNumLeftBg.alpha = 1f;
		if (num < max)
		{
			list2[0] = (num + 1).ToString();
			pageNumRightLabel.text = gameData.getTextByRefIdWithDynTextList("journal09", list, list2);
			pageNumRightBg.alpha = 1f;
		}
		else
		{
			pageNumRightLabel.text = string.Empty;
			pageNumRightBg.alpha = 0f;
		}
	}

	private void updatePageNumbersYear(int year, int current, int max)
	{
		GameData gameData = game.getGameData();
		if (current <= 1)
		{
			pageLeftButton.isEnabled = false;
		}
		else
		{
			pageLeftButton.isEnabled = true;
		}
		if (current >= max)
		{
			pageRightButton.isEnabled = false;
		}
		else
		{
			pageRightButton.isEnabled = true;
		}
		pageNumLeftLabel.text = gameData.getTextByRefIdWithDynText("yearLong", "[year]", year.ToString());
		pageNumLeftBg.alpha = 1f;
		pageNumRightLabel.text = gameData.getTextByRefIdWithDynText("yearLong", "[year]", year.ToString());
		pageNumRightBg.alpha = 1f;
	}

	private void hidePageNumbers()
	{
		pageLeftButton.isEnabled = false;
		pageRightButton.isEnabled = false;
		pageNumLeftLabel.text = string.Empty;
		pageNumRightLabel.text = string.Empty;
		pageNumLeftBg.alpha = 0f;
		pageNumRightBg.alpha = 0f;
	}
}
