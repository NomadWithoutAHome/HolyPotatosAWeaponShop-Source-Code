using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class GUIMapBuyMatController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private AudioController audioController;

	private GUIMapController mapController;

	private TooltipTextScript tooltipScript;

	private Dictionary<string, ShopItem> buyItemList;

	private Dictionary<string, GameObject> itemObjList;

	private Dictionary<string, int> qtyList;

	private GameObject buyHeader;

	private UILabel buyHeaderTitle;

	private UILabel buyHeaderInfo;

	private GameObject buyMatExpandable;

	private UIGrid itemGrid;

	private UILabel totalValue;

	private string itemPrefix;

	private Vector3 openedPos;

	private Vector3 closedPos;

	private bool opened;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		mapController = GameObject.Find("GUIMapController").GetComponent<GUIMapController>();
		tooltipScript = GameObject.Find("MapTooltipInfo").GetComponent<TooltipTextScript>();
		buyItemList = new Dictionary<string, ShopItem>();
		itemObjList = new Dictionary<string, GameObject>();
		qtyList = new Dictionary<string, int>();
		buyHeader = commonScreenObject.findChild(base.gameObject, "Panel_BuyHeader/BuyHeader").gameObject;
		buyHeaderTitle = commonScreenObject.findChild(buyHeader, "BuyHeaderTitle").GetComponent<UILabel>();
		buyHeaderInfo = commonScreenObject.findChild(buyHeader, "BuyHeaderInfo").GetComponent<UILabel>();
		buyMatExpandable = commonScreenObject.findChild(base.gameObject, "BuyMatExpandable").gameObject;
		itemGrid = commonScreenObject.findChild(buyMatExpandable, "ItemGrid").GetComponent<UIGrid>();
		itemPrefix = "Item_";
		openedPos = new Vector3(0f, 0f, 0f);
		closedPos = new Vector3(0f, 0f, 0f);
		opened = false;
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName != null && gameObjectName == "BuyHeader")
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
		case "MinusButton":
			addSubtractItem(array[1], -1);
			break;
		case "PlusButton":
			addSubtractItem(array[1], 1);
			break;
		}
	}

	public void processHover(bool isOver, GameObject hoverObj)
	{
		string text = hoverObj.name;
		if (isOver)
		{
			if (text == "MatObjBg")
			{
				string[] array = hoverObj.transform.parent.name.Split('_');
				if (array[0] == "Item")
				{
					GameData gameData = game.getGameData();
					Item itemByRefId = gameData.getItemByRefId(array[2]);
					string itemStandardTooltipString = itemByRefId.getItemStandardTooltipString();
					tooltipScript.showText(itemStandardTooltipString);
				}
			}
		}
		else
		{
			tooltipScript.setInactive();
		}
	}

	public void setReference(Area area)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		buyHeaderTitle.text = gameData.getTextByRefId("mapText05").ToUpper(CultureInfo.InvariantCulture);
		buyHeaderInfo.text = gameData.getTextByRefId("mapText19") + " $ 0";
		base.transform.localPosition = openedPos;
		foreach (GameObject value in itemObjList.Values)
		{
			commonScreenObject.destroyPrefabImmediate(value);
		}
		itemObjList.Clear();
		qtyList.Clear();
		buyItemList = area.getShopItemList();
		int num = 0;
		foreach (KeyValuePair<string, ShopItem> item in buyItemList.OrderBy((KeyValuePair<string, ShopItem> i) => i.Value.getCost()))
		{
			string key = item.Key;
			Item itemByRefId = gameData.getItemByRefId(key);
			GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
			string itemLockSet = gameScenarioByRefId.getItemLockSet();
			if (itemByRefId.checkScenarioAllow(itemLockSet))
			{
				GameObject gameObject = commonScreenObject.createPrefab(itemGrid.gameObject, itemPrefix + num + "_" + key, "Prefab/Area/NEW/MapBuySelectable", Vector3.zero, Vector3.one, Vector3.zero);
				commonScreenObject.findChild(gameObject, "MatName").GetComponent<UILabel>().text = itemByRefId.getItemName();
				commonScreenObject.findChild(gameObject, "CostFrame/CostLabel").GetComponent<UILabel>().text = CommonAPI.formatNumber(item.Value.getCost()).ToString();
				commonScreenObject.findChild(gameObject, "YouHaveFrame/YouHaveLabel").GetComponent<UILabel>().text = itemByRefId.getItemNum().ToString();
				commonScreenObject.findChild(gameObject, "MatObjBg/MatImg").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/materials/" + itemByRefId.getImage());
				GameObject aObject = commonScreenObject.findChild(gameObject, "SelectableFrame").gameObject;
				commonScreenObject.findChild(aObject, "QtyLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("mapText15");
				commonScreenObject.findChild(aObject, "QtyValueFrame/QtyValue").GetComponent<UILabel>().text = "0";
				commonScreenObject.findChild(aObject, "MinusButton").gameObject.name = "MinusButton_" + key;
				commonScreenObject.findChild(aObject, "PlusButton").gameObject.name = "PlusButton_" + key;
				itemObjList.Add(key, gameObject);
				qtyList.Add(key, 0);
				updatePlusMinusButton(gameObject, 0);
				num++;
			}
		}
		enableContent();
		setOpened(aState: true, showTransition: false, scale: false, move: false, string.Empty, string.Empty);
	}

	private void addSubtractItem(string itemRefID, int addValue)
	{
		GameObject gameObject = itemObjList[itemRefID];
		ShopItem shopItem = buyItemList[itemRefID];
		int num = qtyList[itemRefID];
		int maxQty = shopItem.getMaxQty();
		audioController.playMapSelectItemAudio();
		num += addValue;
		num = Mathf.Max(0, num);
		num = Mathf.Min(maxQty, num);
		qtyList[itemRefID] = num;
		commonScreenObject.findChild(gameObject, "SelectableFrame/QtyValueFrame/QtyValue").GetComponent<UILabel>().text = num.ToString();
		updatePlusMinusButton(gameObject, num);
		updateTotal();
	}

	private void updatePlusMinusButton(GameObject buyListObj, int newQty)
	{
		string text = buyListObj.name.Split('_')[2];
		int maxQty = buyItemList[text].getMaxQty();
		if (newQty <= 0)
		{
			commonScreenObject.findChild(buyListObj, "MapBuySelectedFrame").GetComponent<UISprite>().enabled = false;
			commonScreenObject.findChild(buyListObj, "SelectableFrame/MinusButton_" + text).GetComponent<UIButton>().isEnabled = false;
		}
		else
		{
			commonScreenObject.findChild(buyListObj, "MapBuySelectedFrame").GetComponent<UISprite>().enabled = true;
			commonScreenObject.findChild(buyListObj, "SelectableFrame/MinusButton_" + text).GetComponent<UIButton>().isEnabled = true;
		}
		if (newQty >= maxQty)
		{
			commonScreenObject.findChild(buyListObj, "SelectableFrame/PlusButton_" + text).GetComponent<UIButton>().isEnabled = false;
		}
		else
		{
			commonScreenObject.findChild(buyListObj, "SelectableFrame/PlusButton_" + text).GetComponent<UIButton>().isEnabled = true;
		}
		mapController.setConfirmButton();
	}

	private void updateTotal()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		int num = 0;
		foreach (KeyValuePair<string, ShopItem> buyItem in buyItemList)
		{
			num += buyItem.Value.getCost() * qtyList[buyItem.Key];
		}
		if (num > player.getPlayerGold())
		{
			buyHeaderInfo.text = gameData.getTextByRefId("mapText19") + " [FF4842]$ " + CommonAPI.formatNumber(num) + "[-]";
		}
		else
		{
			buyHeaderInfo.text = gameData.getTextByRefId("mapText19") + " $ " + CommonAPI.formatNumber(num);
		}
	}

	public bool getOpened()
	{
		return opened;
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
					commonScreenObject.tweenScale(buyMatExpandable.GetComponent<TweenScale>(), Vector3.one, new Vector3(1f, 0f, 1f), 0.5f, base.gameObject, aCompletionHandler);
					break;
				case "DOWN":
					commonScreenObject.tweenScale(buyMatExpandable.GetComponent<TweenScale>(), new Vector3(1f, 0f, 1f), Vector3.one, 0.5f, base.gameObject, aCompletionHandler);
					break;
				}
			}
		}
		else if (aState)
		{
			base.transform.localPosition = openedPos;
			buyMatExpandable.transform.localScale = Vector3.one;
		}
		else
		{
			disableContent();
			base.transform.localPosition = closedPos;
			buyMatExpandable.transform.localScale = new Vector3(1f, 0f, 1f);
		}
	}

	public void enableContent()
	{
		itemGrid.gameObject.SetActive(value: true);
		itemGrid.Reposition();
	}

	public void disableContent()
	{
		itemGrid.gameObject.SetActive(value: false);
	}

	public bool checkSelectedItemList()
	{
		int num = 0;
		foreach (KeyValuePair<string, ShopItem> buyItem in buyItemList)
		{
			if (qtyList.ContainsKey(buyItem.Key))
			{
				num += buyItem.Value.getCost() * qtyList[buyItem.Key];
			}
		}
		if (num <= 0)
		{
			return false;
		}
		return true;
	}

	public Dictionary<string, ShopItem> getBuyItemList()
	{
		return buyItemList;
	}

	public Dictionary<string, int> getQtyList()
	{
		return qtyList;
	}

	public void setZeroScale()
	{
		opened = false;
		buyMatExpandable.transform.localScale = new Vector3(1f, 0f, 1f);
	}
}
