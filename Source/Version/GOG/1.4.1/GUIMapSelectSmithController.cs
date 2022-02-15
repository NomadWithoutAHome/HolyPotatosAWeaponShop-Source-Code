using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIMapSelectSmithController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private GUIMapController mapController;

	private TooltipTextScript heroTooltipScript;

	private UILabel selectSmithTitle;

	private UILabel selectSmithInfo;

	private List<Smith> smithList;

	private List<Smith> selectedSmithList;

	private List<string> selectableSmithRefIdList;

	private int maxSlots;

	private int smithsInArea;

	private int allowSelect;

	private GameObject smithExpandable;

	private UILabel selectedSmithLabel;

	private GameObject selectedSmithImg1;

	private UITexture smithImg1;

	private UISprite noSmithImg1;

	private UISprite minusSign1;

	private GameObject selectedSmithImg2;

	private UITexture smithImg2;

	private UISprite noSmithImg2;

	private UISprite minusSign2;

	private GameObject selectedSmithImg3;

	private UITexture smithImg3;

	private UISprite noSmithImg3;

	private UISprite minusSign3;

	private UIGrid smithGrid;

	private UIScrollBar smithScroll;

	private Dictionary<string, GameObject> smithObjectList;

	private string smithPrefix;

	private bool opened;

	private Vector3 headerOrigPos;

	private Vector3 openedPosSpecial;

	private Vector3 closedPosSpecial;

	private ActivityType selectedActivity;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		mapController = GameObject.Find("GUIMapController").GetComponent<GUIMapController>();
		heroTooltipScript = GameObject.Find("HeroTooltipInfo").GetComponent<TooltipTextScript>();
		selectSmithTitle = commonScreenObject.findChild(base.gameObject, "Panel_Header/SelectSmithTitle").GetComponent<UILabel>();
		selectSmithInfo = commonScreenObject.findChild(base.gameObject, "Panel_Header/SelectSmithInfo").GetComponent<UILabel>();
		smithList = new List<Smith>();
		selectedSmithList = new List<Smith>();
		selectableSmithRefIdList = new List<string>();
		maxSlots = 0;
		allowSelect = 0;
		smithExpandable = commonScreenObject.findChild(base.gameObject, "SmithExpandable").gameObject;
		selectedSmithLabel = commonScreenObject.findChild(smithExpandable, "Panel_SelectedSmith/SelectedSmithLabel").GetComponent<UILabel>();
		smithGrid = commonScreenObject.findChild(smithExpandable, "Panel_SmithDraggable/SmithGrid").GetComponent<UIGrid>();
		smithScroll = commonScreenObject.findChild(smithExpandable, "SmithScroll").GetComponent<UIScrollBar>();
		smithObjectList = new Dictionary<string, GameObject>();
		smithPrefix = "MapSmith_";
		headerOrigPos = new Vector3(0f, -75f, 0f);
		openedPosSpecial = new Vector3(0f, 0f, 0f);
		closedPosSpecial = new Vector3(0f, -565f, 0f);
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName != null && gameObjectName == "SelectSmithHeader")
		{
			audioController.playMapSlideAudio();
			if (opened)
			{
				return;
			}
			switch (mapController.getSelectedActivity())
			{
			case ActivityType.ActivityTypeSellWeapon:
			{
				GUIMapSellWeaponController component2 = GameObject.Find("MapSellWeaponHeader").GetComponent<GUIMapSellWeaponController>();
				if (component2.getOpened())
				{
					component2.setOpened(aState: false, showTransition: true, scale: true, move: false, "UP", string.Empty);
					setOpened(aState: true, showTransition: true, scale: true, move: true, "UP", "UP");
				}
				break;
			}
			case ActivityType.ActivityTypeBuyMats:
			{
				GUIMapBuyMatController component = GameObject.Find("MapBuyHeader").GetComponent<GUIMapBuyMatController>();
				if (component.getOpened())
				{
					component.setOpened(aState: false, showTransition: true, scale: true, move: false, "UP", string.Empty);
					setOpened(aState: true, showTransition: true, scale: true, move: true, "UP", "UP");
				}
				break;
			}
			}
		}
		else
		{
			string[] array = gameObjectName.Split('_');
			if (array[0] == "MapSmith")
			{
				audioController.playMapSelectItemAudio();
				selectMapSmith(gameObjectName);
			}
			else if (array[0] == "SelectedSmithImg")
			{
				audioController.playMapSelectItemAudio();
				unselectMapSmith(CommonAPI.parseInt(array[1]) - 1);
			}
		}
	}

	public void processHover(bool isOver, string gameObjectName)
	{
		if (isOver)
		{
			string[] array = gameObjectName.Split('_');
			switch (array[0])
			{
			case "MapSmith":
			{
				Smith smithByRefId2 = game.getGameData().getSmithByRefId(array[1]);
				if (smithByRefId2.getSmithRefId() != string.Empty)
				{
					heroTooltipScript.showText(smithByRefId2.getSmithStandardInfoString(showFullJobDetails: false));
				}
				break;
			}
			case "SmithMoodIcon":
			{
				Smith smithByRefId3 = game.getGameData().getSmithByRefId(array[1]);
				if (smithByRefId3.getSmithRefId() != string.Empty)
				{
					heroTooltipScript.showText(CommonAPI.getMoodString(smithByRefId3.getMoodState(), showDesc: true));
				}
				break;
			}
			case "SmithTooltip":
			{
				Smith smithByRefId = game.getGameData().getSmithByRefId(array[1]);
				if (smithByRefId.getSmithRefId() != string.Empty)
				{
					heroTooltipScript.showText(smithByRefId.getSmithStandardInfoString(showFullJobDetails: false));
				}
				break;
			}
			}
		}
		else
		{
			heroTooltipScript.setInactive();
		}
	}

	public void setReference(int aMaxSlots, int aSmithsInArea, Smith preselectSmith = null)
	{
		enableContent();
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		maxSlots = aMaxSlots;
		smithsInArea = aSmithsInArea;
		allowSelect = Mathf.Min(maxSlots, 3 - smithsInArea);
		selectedSmithLabel.text = gameData.getTextByRefId("mapText44");
		selectSmithTitle.text = gameData.getTextByRefId("mapText23").ToUpper(CultureInfo.InvariantCulture);
		selectSmithInfo.text = gameData.getTextByRefId("mapText56");
		smithList = player.getSmithList();
		selectedSmithList = new List<Smith>();
		if (preselectSmith != null && allowSelect > 0)
		{
			selectedSmithList.Add(preselectSmith);
		}
		selectableSmithRefIdList = new List<string>();
		foreach (GameObject value in smithObjectList.Values)
		{
			commonScreenObject.destroyPrefabImmediate(value);
		}
		smithObjectList.Clear();
		selectedActivity = mapController.getSelectedActivity();
		loadSmithDisplay(selectedActivity);
		for (int i = 0; i < smithList.Count; i++)
		{
			Smith smith = smithList[i];
			GameObject gameObject = commonScreenObject.createPrefab(smithGrid.gameObject, smithPrefix + smith.getSmithRefId(), "Prefab/Area/NEW/MapSelectSmithObject", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(gameObject, "SmithTooltip").gameObject.SetActive(value: false);
			commonScreenObject.findChild(gameObject, "SmithMoodIcon").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Mood/" + CommonAPI.getMoodIcon(smith.getMoodState()));
			commonScreenObject.findChild(gameObject, "SmithMoodIcon").gameObject.name = "SmithMoodIcon_" + smith.getSmithRefId();
			CommonAPI.debug("aobj: " + gameObject.name + " smith name: " + smith.getImage());
			commonScreenObject.findChild(gameObject, "SelectSmithImg").GetComponentInChildren<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/" + smith.getImage());
			commonScreenObject.findChild(gameObject, "SmithNameLabel").GetComponent<UILabel>().text = smith.getSmithName();
			switch (selectedActivity)
			{
			case ActivityType.ActivityTypeBuyMats:
			case ActivityType.ActivityTypeSellWeapon:
			{
				int merchantLevel = CommonAPI.getMerchantLevel(smith.getMerchantExp());
				commonScreenObject.findChild(gameObject, "SmithLvFrame/SmithLvLabel").GetComponent<UILabel>().text = "Lv " + merchantLevel + " " + gameData.getTextByRefId("SmithMerchantText");
				if (merchantLevel >= 15)
				{
					commonScreenObject.findChild(gameObject, "SmithLvFrame/SmithLevelBg").GetComponent<UISprite>().fillAmount = 1f;
					UILabel component2 = commonScreenObject.findChild(gameObject, "SmithLvFrame/SmithLvLabel").GetComponent<UILabel>();
					component2.text = component2.text + " (" + gameData.getTextByRefId("mapText18").ToUpper(CultureInfo.InvariantCulture) + ")";
				}
				else
				{
					commonScreenObject.findChild(gameObject, "SmithLvFrame/SmithLevelBg").GetComponent<UISprite>().fillAmount = (float)smith.getMerchantExp() / (float)CommonAPI.getExploreMerchantMaxExp(merchantLevel);
				}
				break;
			}
			case ActivityType.ActivityTypeExplore:
			{
				int exploreLevel = CommonAPI.getExploreLevel(smith.getExploreExp());
				commonScreenObject.findChild(gameObject, "SmithLvFrame/SmithLvLabel").GetComponent<UILabel>().text = "Lv " + exploreLevel + " " + gameData.getTextByRefId("SmithExplorerText");
				if (exploreLevel >= 15)
				{
					commonScreenObject.findChild(gameObject, "SmithLvFrame/SmithLevelBg").GetComponent<UISprite>().fillAmount = 1f;
					UILabel component = commonScreenObject.findChild(gameObject, "SmithLvFrame/SmithLvLabel").GetComponent<UILabel>();
					component.text = component.text + " (" + gameData.getTextByRefId("mapText18").ToUpper(CultureInfo.InvariantCulture) + ")";
				}
				else
				{
					commonScreenObject.findChild(gameObject, "SmithLvFrame/SmithLevelBg").GetComponent<UISprite>().fillAmount = (float)smith.getExploreExp() / (float)CommonAPI.getExploreMerchantMaxExp(exploreLevel);
				}
				break;
			}
			case ActivityType.ActivityTypeVacation:
			case ActivityType.ActivityTypeTraining:
				commonScreenObject.findChild(gameObject, "SmithLvFrame/SmithLvLabel").GetComponent<UILabel>().text = smith.getCurrentJobClassLevelString();
				commonScreenObject.findChild(gameObject, "SmithLvFrame/SmithLevelBg").GetComponent<UISprite>().fillAmount = (float)smith.getSmithExp() / (float)smith.getMaxExp();
				break;
			}
			if (!smith.checkSmithInShopOrStandby() || (selectedActivity == ActivityType.ActivityTypeTraining && smith.checkIsSmithMaxLevel()))
			{
				commonScreenObject.findChild(gameObject, "SelectedFrame").GetComponent<UISprite>().color = Color.gray;
				commonScreenObject.findChild(gameObject, "SelectSmithImg").GetComponentInChildren<UITexture>().color = Color.gray;
			}
			else
			{
				selectableSmithRefIdList.Add(smith.getSmithRefId());
			}
			smithObjectList.Add(smith.getSmithRefId(), gameObject);
		}
		smithGrid.Reposition();
		smithScroll.value = 0f;
		smithGrid.enabled = true;
		smithGrid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		smithGrid.transform.parent.GetComponent<UIScrollView>().verticalScrollBar.value = 0f;
		switch (selectedActivity)
		{
		case ActivityType.ActivityTypeSellWeapon:
			base.transform.localPosition = openedPosSpecial;
			setOpened(aState: false, showTransition: false, scale: false, move: false, string.Empty, string.Empty);
			break;
		case ActivityType.ActivityTypeBuyMats:
			base.transform.localPosition = openedPosSpecial;
			setOpened(aState: false, showTransition: false, scale: false, move: false, string.Empty, string.Empty);
			break;
		}
		updateSmithListDisplay();
		updateSelectedSmithDisplay();
	}

	private void selectMapSmith(string gameObjectName)
	{
		string[] array = gameObjectName.Split('_');
		string aRefID = array[1];
		Smith smithByRefID = game.getPlayer().getSmithByRefID(aRefID);
		if (!selectedSmithList.Contains(smithByRefID) && selectableSmithRefIdList.Contains(smithByRefID.getSmithRefId()))
		{
			if (selectedSmithList.Count < allowSelect)
			{
				selectedSmithList.Add(smithByRefID);
			}
		}
		else if (selectedSmithList.Contains(smithByRefID))
		{
			selectedSmithList.Remove(smithByRefID);
		}
		updateSmithListDisplay();
		updateSelectedSmithDisplay();
	}

	private void unselectMapSmith(int unselectIndex)
	{
		if (selectedSmithList.Count > unselectIndex)
		{
			Smith item = selectedSmithList[unselectIndex];
			selectedSmithList.Remove(item);
			updateSmithListDisplay();
			updateSelectedSmithDisplay();
		}
	}

	private void updateSmithListDisplay()
	{
		GameData gameData = game.getGameData();
		if (!smithGrid.gameObject.activeSelf)
		{
			return;
		}
		foreach (string key in smithObjectList.Keys)
		{
			bool flag = false;
			foreach (Smith selectedSmith in selectedSmithList)
			{
				if (selectedSmith.getSmithRefId() == key)
				{
					flag = true;
					break;
				}
			}
			GameObject aObject = smithObjectList[key];
			if (!selectableSmithRefIdList.Contains(key))
			{
				commonScreenObject.findChild(aObject, "Warning_label").GetComponent<UILabel>().alpha = 1f;
				commonScreenObject.findChild(aObject, "Warning_label").GetComponent<UILabel>().text = gameData.getTextByRefId("mapText93").ToUpper(CultureInfo.InvariantCulture);
				commonScreenObject.findChild(aObject, "Warning_bg").GetComponent<UISprite>().alpha = 0.4f;
				commonScreenObject.findChild(aObject, "SelectedFrame").GetComponent<UISprite>().color = Color.gray;
				commonScreenObject.findChild(aObject, "SelectSmithImg").GetComponentInChildren<UITexture>().color = Color.gray;
				continue;
			}
			if (flag)
			{
				commonScreenObject.findChild(aObject, "Warning_bg").GetComponent<UISprite>().alpha = 0f;
				commonScreenObject.findChild(aObject, "Warning_label").GetComponent<UILabel>().alpha = 0f;
				commonScreenObject.findChild(aObject, "SelectedFrame").GetComponent<UISprite>().spriteName = "bg_weaponselected";
				continue;
			}
			commonScreenObject.findChild(aObject, "SelectedFrame").GetComponent<UISprite>().spriteName = "bg_weapon";
			if (selectedSmithList.Count < allowSelect)
			{
				commonScreenObject.findChild(aObject, "Warning_bg").GetComponent<UISprite>().alpha = 0f;
				commonScreenObject.findChild(aObject, "Warning_label").GetComponent<UILabel>().alpha = 0f;
				commonScreenObject.findChild(aObject, "SelectedFrame").GetComponent<UISprite>().color = Color.white;
				commonScreenObject.findChild(aObject, "SelectSmithImg").GetComponentInChildren<UITexture>().color = Color.white;
				continue;
			}
			commonScreenObject.findChild(aObject, "Warning_label").GetComponent<UILabel>().alpha = 1f;
			if (selectedSmithList.Count >= maxSlots)
			{
				commonScreenObject.findChild(aObject, "Warning_label").GetComponent<UILabel>().text = gameData.getTextByRefId("mapText95").ToUpper(CultureInfo.InvariantCulture);
			}
			else
			{
				commonScreenObject.findChild(aObject, "Warning_label").GetComponent<UILabel>().text = gameData.getTextByRefId("mapText94").ToUpper(CultureInfo.InvariantCulture);
			}
			commonScreenObject.findChild(aObject, "Warning_bg").GetComponent<UISprite>().alpha = 0.4f;
			commonScreenObject.findChild(aObject, "SelectedFrame").GetComponent<UISprite>().color = Color.gray;
			commonScreenObject.findChild(aObject, "SelectSmithImg").GetComponentInChildren<UITexture>().color = Color.gray;
		}
	}

	private void updateSelectedSmithDisplay()
	{
		int num = 1;
		if (selectedActivity != ActivityType.ActivityTypeSellWeapon && selectedActivity != ActivityType.ActivityTypeBuyMats)
		{
			num = 3;
		}
		for (int i = 0; i < num; i++)
		{
			GameObject gameObject = selectedSmithImg1;
			UITexture uITexture = smithImg1;
			UISprite uISprite = noSmithImg1;
			UISprite uISprite2 = minusSign1;
			switch (i)
			{
			case 1:
				if (selectedSmithImg2 != null)
				{
					gameObject = selectedSmithImg2;
					uITexture = smithImg2;
					uISprite = noSmithImg2;
					uISprite2 = minusSign2;
				}
				break;
			case 2:
				if (selectedSmithImg3 != null)
				{
					gameObject = selectedSmithImg3;
					uITexture = smithImg3;
					uISprite = noSmithImg3;
					uISprite2 = minusSign3;
				}
				break;
			}
			if (i < maxSlots)
			{
				gameObject.GetComponent<UISprite>().alpha = 1f;
				gameObject.GetComponent<UISprite>().color = Color.white;
				uITexture.alpha = 1f;
				uISprite.alpha = 0f;
				uISprite2.alpha = 1f;
				if (i < allowSelect)
				{
					if (selectedSmithList.Count > i)
					{
						uITexture.mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/" + selectedSmithList[i].getImage());
						uISprite.alpha = 0f;
						uISprite2.alpha = 1f;
						gameObject.GetComponent<BoxCollider>().enabled = true;
					}
					else
					{
						uITexture.mainTexture = null;
						uISprite.alpha = 0f;
						uISprite2.alpha = 0f;
						gameObject.GetComponent<BoxCollider>().enabled = false;
					}
				}
				else
				{
					gameObject.GetComponent<UISprite>().color = Color.gray;
					uITexture.mainTexture = null;
					uISprite.alpha = 1f;
					uISprite2.alpha = 0f;
					gameObject.GetComponent<BoxCollider>().enabled = false;
				}
			}
			else
			{
				gameObject.GetComponent<UISprite>().alpha = 0f;
				uITexture.alpha = 0f;
				uISprite.alpha = 0f;
				uISprite2.alpha = 0f;
				gameObject.GetComponent<BoxCollider>().enabled = false;
			}
		}
		mapController.setConfirmButton();
	}

	public void setOpened(bool aState, bool showTransition)
	{
		opened = aState;
		if (showTransition)
		{
			if (aState)
			{
				commonScreenObject.tweenScale(smithExpandable.GetComponent<TweenScale>(), new Vector3(1f, 0f, 1f), Vector3.one, 0.5f, base.gameObject, string.Empty);
			}
			else
			{
				commonScreenObject.tweenScale(smithExpandable.GetComponent<TweenScale>(), Vector3.one, new Vector3(1f, 0f, 1f), 0.5f, base.gameObject, string.Empty);
			}
		}
		else if (aState)
		{
			smithExpandable.transform.localScale = Vector3.one;
		}
		else
		{
			smithExpandable.transform.localScale = new Vector3(1f, 0f, 1f);
		}
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
					commonScreenObject.tweenPosition(GetComponent<TweenPosition>(), closedPosSpecial, openedPosSpecial, 0.5f, base.gameObject, aCompletionHandler);
					break;
				case "DOWN":
					commonScreenObject.tweenPosition(GetComponent<TweenPosition>(), openedPosSpecial, closedPosSpecial, 0.5f, base.gameObject, aCompletionHandler);
					break;
				}
			}
			if (scale)
			{
				switch (scaleUpDown)
				{
				case "DOWN":
					commonScreenObject.tweenScale(smithExpandable.GetComponent<TweenScale>(), Vector3.one, new Vector3(1f, 0f, 1f), 0.5f, base.gameObject, aCompletionHandler);
					break;
				case "UP":
					commonScreenObject.tweenScale(smithExpandable.GetComponent<TweenScale>(), new Vector3(1f, 0f, 1f), Vector3.one, 0.5f, base.gameObject, aCompletionHandler);
					break;
				}
			}
		}
		else if (aState)
		{
			enableContent();
			base.transform.localPosition = openedPosSpecial;
			smithExpandable.transform.localScale = Vector3.one;
		}
		else
		{
			disableContent();
			base.transform.localPosition = closedPosSpecial;
			smithExpandable.transform.localScale = new Vector3(1f, 0f, 1f);
		}
	}

	public void enableContent()
	{
		smithGrid.gameObject.SetActive(value: true);
		smithScroll.gameObject.SetActive(value: true);
		smithGrid.Reposition();
		smithScroll.value = 0f;
		smithGrid.enabled = true;
		smithGrid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		smithGrid.transform.parent.GetComponent<UIScrollView>().verticalScrollBar.value = 0f;
		updateSmithListDisplay();
	}

	public void disableContent()
	{
		smithGrid.gameObject.SetActive(value: false);
		smithScroll.gameObject.SetActive(value: false);
	}

	private void loadSmithDisplay(ActivityType aType)
	{
		selectedSmithImg1 = commonScreenObject.findChild(smithExpandable, "Panel_SelectedSmith/SelectedSmithImg_1").gameObject;
		smithImg1 = commonScreenObject.findChild(selectedSmithImg1, "SmithImg").GetComponent<UITexture>();
		noSmithImg1 = commonScreenObject.findChild(selectedSmithImg1, "NoSmithImg").GetComponent<UISprite>();
		minusSign1 = commonScreenObject.findChild(selectedSmithImg1, "MinusSign").GetComponent<UISprite>();
		if (aType != ActivityType.ActivityTypeSellWeapon && aType != ActivityType.ActivityTypeBuyMats)
		{
			selectedSmithImg2 = commonScreenObject.findChild(smithExpandable, "Panel_SelectedSmith/SelectedSmithImg_2").gameObject;
			smithImg2 = commonScreenObject.findChild(selectedSmithImg2, "SmithImg").GetComponent<UITexture>();
			noSmithImg2 = commonScreenObject.findChild(selectedSmithImg2, "NoSmithImg").GetComponent<UISprite>();
			minusSign2 = commonScreenObject.findChild(selectedSmithImg2, "MinusSign").GetComponent<UISprite>();
			selectedSmithImg3 = commonScreenObject.findChild(smithExpandable, "Panel_SelectedSmith/SelectedSmithImg_3").gameObject;
			smithImg3 = commonScreenObject.findChild(selectedSmithImg3, "SmithImg").GetComponent<UITexture>();
			noSmithImg3 = commonScreenObject.findChild(selectedSmithImg3, "NoSmithImg").GetComponent<UISprite>();
			minusSign3 = commonScreenObject.findChild(selectedSmithImg3, "MinusSign").GetComponent<UISprite>();
		}
	}

	public bool getOpened()
	{
		return opened;
	}

	public bool checkSelectedSmith()
	{
		return selectedSmithList.Count > 0;
	}

	public List<Smith> getSelectedSmith()
	{
		return selectedSmithList;
	}

	public void resetMapSelectedSmiths()
	{
		selectedSmithList = new List<Smith>();
	}

	public void setZeroScale()
	{
		setOpened(aState: false, showTransition: false, scale: false, move: false, string.Empty, string.Empty);
	}
}
