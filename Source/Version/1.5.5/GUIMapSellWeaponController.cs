using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIMapSellWeaponController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private GUIMapController mapController;

	private TooltipTextScript tooltipScript;

	private List<WeaponType> weaponTypeList;

	private int selectedWeaponTypeIndex;

	private GameObject sellHeader;

	private UILabel sellHeaderTitle;

	private UILabel sellHeaderInfo;

	private List<UISprite> selectedWeaponMinus;

	private List<BoxCollider> selectedWeaponCollider;

	private List<UITexture> selectedWeaponImgList;

	private GameObject weaponExpandable;

	private GameObject panel_SelectedWeapon;

	private UILabel selectedWeaponLabel;

	private GameObject panel_weaponFilter;

	private UIPopupList filterer;

	private GameObject panel_weaponDraggable;

	private UIGrid weaponGrid;

	private UIScrollBar weaponScroll;

	private List<Project> projectList;

	private List<Project> selectedProjectList;

	private Dictionary<string, GameObject> weaponObjectList;

	private UILabel noWeaponLabel;

	private string weaponPrefix;

	private int maxSlot;

	private Vector3 openedPos;

	private Vector3 closedPos;

	private bool opened;

	private string statPriSecString;

	private string statString;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		mapController = GameObject.Find("GUIMapController").GetComponent<GUIMapController>();
		tooltipScript = GameObject.Find("MapTooltipInfo").GetComponent<TooltipTextScript>();
		weaponTypeList = new List<WeaponType>();
		selectedWeaponTypeIndex = -1;
		sellHeader = commonScreenObject.findChild(base.gameObject, "Panel_SellHeader/SellHeader").gameObject;
		sellHeaderTitle = commonScreenObject.findChild(sellHeader, "SellHeaderTitle").GetComponent<UILabel>();
		sellHeaderInfo = commonScreenObject.findChild(sellHeader, "SellHeaderInfo").GetComponent<UILabel>();
		selectedWeaponMinus = new List<UISprite>();
		selectedWeaponCollider = new List<BoxCollider>();
		selectedWeaponImgList = new List<UITexture>();
		weaponExpandable = commonScreenObject.findChild(base.gameObject, "WeaponExpandable").gameObject;
		panel_SelectedWeapon = commonScreenObject.findChild(weaponExpandable, "Panel_SelectedWeapon").gameObject;
		selectedWeaponLabel = commonScreenObject.findChild(panel_SelectedWeapon, "SelectedWeaponLabel").GetComponent<UILabel>();
		panel_weaponFilter = commonScreenObject.findChild(weaponExpandable, "Panel_WeaponFilter").gameObject;
		filterer = commonScreenObject.findChild(panel_weaponFilter, "FilterButton").GetComponent<UIPopupList>();
		panel_weaponDraggable = commonScreenObject.findChild(weaponExpandable, "Panel_WeaponDraggable").gameObject;
		weaponGrid = commonScreenObject.findChild(panel_weaponDraggable, "WeaponGrid").GetComponent<UIGrid>();
		weaponScroll = commonScreenObject.findChild(weaponExpandable, "WeaponScroll").GetComponent<UIScrollBar>();
		projectList = new List<Project>();
		selectedProjectList = new List<Project>();
		weaponObjectList = new Dictionary<string, GameObject>();
		noWeaponLabel = commonScreenObject.findChild(weaponExpandable, "NoWeaponLabel").GetComponent<UILabel>();
		weaponPrefix = "Weapon_";
		openedPos = new Vector3(0f, 0f, 0f);
		closedPos = new Vector3(0f, 0f, 0f);
		maxSlot = 3;
		opened = false;
		statPriSecString = "stat_primary";
		statString = "stat";
		for (int i = 0; i < maxSlot; i++)
		{
			selectedWeaponMinus.Add(commonScreenObject.findChild(panel_SelectedWeapon, "SelectedWeaponImg_" + i + "/MinusSign").GetComponent<UISprite>());
			selectedWeaponCollider.Add(commonScreenObject.findChild(panel_SelectedWeapon, "SelectedWeaponImg_" + i).GetComponent<BoxCollider>());
			selectedWeaponImgList.Add(commonScreenObject.findChild(panel_SelectedWeapon, "SelectedWeaponImg_" + i + "/WeaponImg").GetComponent<UITexture>());
		}
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName != null && gameObjectName == "SellHeader")
		{
			audioController.playMapSlideAudio();
			if (!opened)
			{
				if (GameObject.Find("MapSelectSmithHeader").GetComponent<GUIMapSelectSmithController>().getOpened())
				{
					GameObject.Find("MapSelectSmithHeader").GetComponent<GUIMapSelectSmithController>().setOpened(aState: false, showTransition: true, scale: true, move: true, "DOWN", "DOWN");
				}
				setOpened(aState: true, showTransition: true, scale: true, move: false, "DOWN", string.Empty);
			}
			return;
		}
		string[] array = gameObjectName.Split('_');
		switch (array[0])
		{
		case "Weapon":
			audioController.playMapSelectItemAudio();
			selectProjectWeapon(array[1]);
			break;
		case "SelectedWeaponImg":
			audioController.playMapSelectItemAudio();
			unselectProjectWeapon(CommonAPI.parseInt(array[1]));
			break;
		}
	}

	public void processHover(bool isOver, string hoverName)
	{
		if (isOver)
		{
			string[] array = hoverName.Split('_');
			string text = array[0];
			if (text != null && text == "Weapon")
			{
				Project project = getProject(array[1]);
				Weapon projectWeapon = project.getProjectWeapon();
				if (projectWeapon.getWeaponRefId() != string.Empty)
				{
					tooltipScript.showText(projectWeapon.getWeaponDesc());
				}
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
		Player player = game.getPlayer();
		sellHeaderTitle.text = gameData.getTextByRefId("mapText24").ToUpper(CultureInfo.InvariantCulture);
		sellHeaderInfo.text = gameData.getTextByRefId("mapText57") + " (" + selectedProjectList.Count + "/" + maxSlot + ")";
		selectedWeaponLabel.text = gameData.getTextByRefId("mapText58").ToUpper(CultureInfo.InvariantCulture);
		weaponTypeList = gameData.getWeaponTypeList();
		List<string> list = new List<string>();
		foreach (WeaponType weaponType in weaponTypeList)
		{
			if (weaponType.getWeaponTypeRefId() != "901")
			{
				list.Add(weaponType.getWeaponTypeName());
			}
		}
		list.Insert(0, gameData.getTextByRefId("inventoryFilter01"));
		projectList = player.getCompletedProjectListByType(ProjectType.ProjectTypeWeapon, includeSold: false, includeStock: true, includeSelling: false);
		filterer.items = list;
		filterer.value = list[0];
		base.transform.localPosition = openedPos;
		setOpened(aState: true, showTransition: false, scale: false, move: false, string.Empty, string.Empty);
	}

	public void setNewFilter()
	{
		foreach (GameObject value in weaponObjectList.Values)
		{
			commonScreenObject.destroyPrefabImmediate(value);
		}
		weaponObjectList.Clear();
		for (int i = 0; i < weaponTypeList.Count; i++)
		{
			if (filterer.value == weaponTypeList[i].getWeaponTypeName())
			{
				selectedWeaponTypeIndex = i;
			}
		}
		if (filterer.value == "All")
		{
			selectedWeaponTypeIndex = -1;
		}
		updateProjectButton(string.Empty);
		for (int j = 0; j < projectList.Count; j++)
		{
			Project project = projectList[j];
			Weapon projectWeapon = project.getProjectWeapon();
			if (selectedWeaponTypeIndex == -1 || projectWeapon.getWeaponType().getWeaponTypeRefId() == weaponTypeList[selectedWeaponTypeIndex].getWeaponTypeRefId())
			{
				GameObject gameObject = commonScreenObject.createPrefab(weaponGrid.gameObject, weaponPrefix + project.getProjectId(), "Prefab/Area/NEW/MapSellWeaponObjectNEW", Vector3.zero, Vector3.one, Vector3.zero);
				updatePriSecStats(gameObject, project);
				weaponObjectList.Add(project.getProjectId(), gameObject);
				updateProjectButton(project.getProjectId());
			}
		}
		if (weaponObjectList.Count < 1)
		{
			noWeaponLabel.text = game.getGameData().getTextByRefId("mapText85");
		}
		else
		{
			noWeaponLabel.text = string.Empty;
		}
		weaponGrid.Reposition();
		weaponScroll.value = 0f;
		weaponGrid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
	}

	private void selectProjectWeapon(string projectID)
	{
		Project project = getProject(projectID);
		if (selectedProjectList.Count < maxSlot && !selectedProjectList.Contains(project))
		{
			selectedProjectList.Add(project);
		}
		else if (selectedProjectList.Contains(project))
		{
			selectedProjectList.Remove(project);
		}
		updateProjectButton(projectID);
		mapController.setConfirmButton();
	}

	private void unselectProjectWeapon(int index)
	{
		if (selectedWeaponImgList[index].mainTexture != null)
		{
			string projectId = selectedProjectList[index].getProjectId();
			selectedProjectList.RemoveAt(index);
			updateProjectButton(projectId);
			mapController.setConfirmButton();
		}
	}

	private void updateProjectButton(string projectID)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		if (weaponObjectList.ContainsKey(projectID))
		{
			GameObject aObject = weaponObjectList[projectID];
			Project completedProjectById = player.getCompletedProjectById(projectID);
			if (selectedProjectList.Contains(completedProjectById))
			{
				commonScreenObject.findChild(aObject, "ObjFrame").GetComponent<UISprite>().enabled = true;
			}
			else
			{
				commonScreenObject.findChild(aObject, "ObjFrame").GetComponent<UISprite>().enabled = false;
			}
		}
		foreach (UITexture selectedWeaponImg in selectedWeaponImgList)
		{
			selectedWeaponImg.mainTexture = null;
		}
		for (int i = 0; i < selectedProjectList.Count; i++)
		{
			selectedWeaponImgList[i].mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + selectedProjectList[i].getProjectWeapon().getImage());
		}
		for (int j = 0; j < selectedWeaponImgList.Count; j++)
		{
			if (selectedWeaponImgList[j].mainTexture != null)
			{
				selectedWeaponMinus[j].enabled = true;
				selectedWeaponCollider[j].enabled = true;
			}
			else
			{
				selectedWeaponMinus[j].enabled = false;
				selectedWeaponCollider[j].enabled = false;
			}
		}
		sellHeaderInfo.text = gameData.getTextByRefId("mapText57") + " (" + selectedProjectList.Count + "/" + maxSlot + ")";
	}

	public void setOpened(bool aState, bool showTransition, bool scale, bool move, string scaleUpDown = "", string moveUpDown = "")
	{
		opened = aState;
		if (showTransition)
		{
			string aCompletionHandler = string.Empty;
			if (aState)
			{
				aCompletionHandler = "enableContent";
			}
			else
			{
				disableContent();
			}
			if (move)
			{
				switch (moveUpDown)
				{
				case "UP":
					commonScreenObject.tweenPosition(GetComponent<TweenPosition>(), openedPos, closedPos, 0.5f, base.gameObject, aCompletionHandler);
					break;
				case "DOWN":
					commonScreenObject.tweenPosition(GetComponent<TweenPosition>(), closedPos, openedPos, 0.5f, base.gameObject, aCompletionHandler);
					break;
				}
			}
			if (scale)
			{
				switch (scaleUpDown)
				{
				case "UP":
					commonScreenObject.tweenScale(weaponExpandable.GetComponent<TweenScale>(), Vector3.one, new Vector3(1f, 0f, 1f), 0.5f, base.gameObject, aCompletionHandler);
					break;
				case "DOWN":
					commonScreenObject.tweenScale(weaponExpandable.GetComponent<TweenScale>(), new Vector3(1f, 0f, 1f), Vector3.one, 0.5f, base.gameObject, aCompletionHandler);
					break;
				}
			}
		}
		else if (aState)
		{
			enableContent();
			base.transform.localPosition = openedPos;
			weaponExpandable.transform.localScale = Vector3.one;
		}
		else
		{
			disableContent();
			base.transform.localPosition = closedPos;
			weaponExpandable.transform.localScale = new Vector3(1f, 0f, 1f);
		}
	}

	private void updatePriSecStats(GameObject aObj, Project aProject)
	{
		commonScreenObject.findChild(aObj, "WeaponObjBg/WeaponImg").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + aProject.getProjectWeapon().getImage());
		commonScreenObject.findChild(aObj, "WeaponName").GetComponent<UILabel>().text = aProject.getProjectName(includePrefix: true);
		UILabel component = commonScreenObject.findChild(aObj, "atk_sprite/atk_label").GetComponent<UILabel>();
		UILabel component2 = commonScreenObject.findChild(aObj, "spd_sprite/spd_label").GetComponent<UILabel>();
		UILabel component3 = commonScreenObject.findChild(aObj, "acc_sprite/acc_label").GetComponent<UILabel>();
		UILabel component4 = commonScreenObject.findChild(aObj, "mag_sprite/mag_label").GetComponent<UILabel>();
		UISprite component5 = commonScreenObject.findChild(aObj, "atk_sprite/atkFrame").GetComponent<UISprite>();
		UISprite component6 = commonScreenObject.findChild(aObj, "spd_sprite/spdFrame").GetComponent<UISprite>();
		UISprite component7 = commonScreenObject.findChild(aObj, "acc_sprite/accFrame").GetComponent<UISprite>();
		UISprite component8 = commonScreenObject.findChild(aObj, "mag_sprite/magFrame").GetComponent<UISprite>();
		component.text = aProject.getAtk().ToString();
		component2.text = aProject.getSpd().ToString();
		component3.text = aProject.getAcc().ToString();
		component4.text = aProject.getMag().ToString();
		List<WeaponStat> priSecStat = aProject.getPriSecStat();
		WeaponStat weaponStat = priSecStat[0];
		WeaponStat weaponStat2 = priSecStat[1];
		if (weaponStat == WeaponStat.WeaponStatAttack)
		{
			component.effectStyle = UILabel.Effect.Outline;
			component.fontSize = 12;
			component5.spriteName = statPriSecString;
		}
		else if (weaponStat2 == WeaponStat.WeaponStatAttack)
		{
			component.effectStyle = UILabel.Effect.None;
			component.fontSize = 10;
			component5.spriteName = statPriSecString;
		}
		else
		{
			component.effectStyle = UILabel.Effect.None;
			component.fontSize = 10;
			component5.spriteName = statString;
		}
		if (weaponStat == WeaponStat.WeaponStatSpeed)
		{
			component2.effectStyle = UILabel.Effect.Outline;
			component2.fontSize = 12;
			component6.spriteName = statPriSecString;
		}
		else if (weaponStat2 == WeaponStat.WeaponStatSpeed)
		{
			component2.effectStyle = UILabel.Effect.None;
			component2.fontSize = 10;
			component6.spriteName = statPriSecString;
		}
		else
		{
			component2.effectStyle = UILabel.Effect.None;
			component2.fontSize = 10;
			component6.spriteName = statString;
		}
		if (weaponStat == WeaponStat.WeaponStatAccuracy)
		{
			component3.effectStyle = UILabel.Effect.Outline;
			component3.fontSize = 12;
			component7.spriteName = statPriSecString;
		}
		else if (weaponStat2 == WeaponStat.WeaponStatAccuracy)
		{
			component3.effectStyle = UILabel.Effect.None;
			component3.fontSize = 10;
			component7.spriteName = statPriSecString;
		}
		else
		{
			component3.effectStyle = UILabel.Effect.None;
			component3.fontSize = 10;
			component7.spriteName = statString;
		}
		if (weaponStat == WeaponStat.WeaponStatMagic)
		{
			component4.effectStyle = UILabel.Effect.Outline;
			component4.fontSize = 12;
			component8.spriteName = statPriSecString;
		}
		else if (weaponStat2 == WeaponStat.WeaponStatMagic)
		{
			component4.effectStyle = UILabel.Effect.None;
			component4.fontSize = 10;
			component8.spriteName = statPriSecString;
		}
		else
		{
			component4.effectStyle = UILabel.Effect.None;
			component4.fontSize = 10;
			component8.spriteName = statString;
		}
		commonScreenObject.findChild(aObj, "ObjFrame").GetComponent<UISprite>().enabled = false;
	}

	public void enableContent()
	{
		panel_weaponFilter.SetActive(value: true);
		panel_SelectedWeapon.SetActive(value: true);
		panel_weaponDraggable.SetActive(value: true);
		weaponScroll.gameObject.SetActive(value: true);
		setNewFilter();
		weaponScroll.value = 0f;
		weaponGrid.Reposition();
		weaponGrid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
	}

	public void disableContent()
	{
		panel_weaponFilter.SetActive(value: false);
		panel_SelectedWeapon.SetActive(value: false);
		panel_weaponDraggable.SetActive(value: false);
		weaponScroll.gameObject.SetActive(value: false);
	}

	private Project getProject(string projectID)
	{
		Project result = null;
		foreach (Project project in projectList)
		{
			if (project.getProjectId() == projectID)
			{
				result = project;
			}
		}
		return result;
	}

	public void resetSellWeapon()
	{
		foreach (GameObject value in weaponObjectList.Values)
		{
			commonScreenObject.destroyPrefabImmediate(value);
		}
		selectedProjectList.Clear();
		weaponObjectList.Clear();
		updateProjectButton(string.Empty);
	}

	public bool getOpened()
	{
		return opened;
	}

	public bool checkSelectedProjectList()
	{
		return selectedProjectList.Count > 0;
	}

	public List<Project> getSelectedProjectList()
	{
		return selectedProjectList;
	}

	public void setZeroScale()
	{
		weaponExpandable.transform.localScale = new Vector3(1f, 0f, 1f);
	}
}
