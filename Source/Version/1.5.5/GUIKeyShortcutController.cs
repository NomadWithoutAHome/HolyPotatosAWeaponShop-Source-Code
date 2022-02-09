using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIKeyShortcutController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private TooltipTextScript tooltipScript;

	private UIButton saveButton;

	private UIButton closeButton;

	private GameObject resetButton;

	private UILabel title;

	private UILabel generalCtrlLabel;

	private UILabel shopCtrlLabel;

	private UIGrid generalKeyGrid;

	private UIScrollBar generalScrollBar;

	private UIGrid shopKeyGrid;

	private UIScrollBar shopScrollBar;

	private Dictionary<string, KeyCode> unchangeableKeyList;

	private Dictionary<string, KeyShortcut> keyList;

	private Dictionary<string, KeyCode> tempKeyList;

	private Dictionary<string, GameObject> keyObjList;

	private bool fromStart;

	private string modifiedKey;

	private Color32 unchangeableKeyColor;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		tooltipScript = GameObject.Find("KeyTooltipInfo").GetComponent<TooltipTextScript>();
		saveButton = commonScreenObject.findChild(base.gameObject, "SaveButton").GetComponent<UIButton>();
		closeButton = commonScreenObject.findChild(base.gameObject, "closeButton").GetComponent<UIButton>();
		resetButton = commonScreenObject.findChild(base.gameObject, "ResetButton").gameObject;
		title = commonScreenObject.findChild(base.gameObject, "Title_bg/Title_label").GetComponent<UILabel>();
		generalCtrlLabel = commonScreenObject.findChild(base.gameObject, "GeneralCtrlLabel").GetComponent<UILabel>();
		shopCtrlLabel = commonScreenObject.findChild(base.gameObject, "ShopCtrlLabel").GetComponent<UILabel>();
		generalKeyGrid = commonScreenObject.findChild(base.gameObject, "GeneralCtrlBg/Panel_GeneralKeyList/GeneralKeyListGrid").GetComponent<UIGrid>();
		generalScrollBar = commonScreenObject.findChild(base.gameObject, "GeneralCtrlBg/GeneralList_scrollbar").GetComponent<UIScrollBar>();
		shopKeyGrid = commonScreenObject.findChild(base.gameObject, "ShopCtrlBg/Panel_ShopKeyList/ShopKeyListGrid").GetComponent<UIGrid>();
		shopScrollBar = commonScreenObject.findChild(base.gameObject, "ShopCtrlBg/ShopList_scrollbar").GetComponent<UIScrollBar>();
		unchangeableKeyList = new Dictionary<string, KeyCode>();
		keyList = new Dictionary<string, KeyShortcut>();
		tempKeyList = new Dictionary<string, KeyCode>();
		keyObjList = new Dictionary<string, GameObject>();
		fromStart = false;
		modifiedKey = string.Empty;
		unchangeableKeyColor = new Color32(93, 114, 137, byte.MaxValue);
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "closeButton":
			viewController.closeKeyShortcutPopup();
			viewController.showSettingsMenu(fromStart);
			break;
		case "ResetButton":
			disableAllButtons();
			viewController.showKeyPopup(fromStart, game.getGameData().getTextByRefId("keyShortcutPopup04"), confirmReset: true);
			break;
		case "SaveButton":
			checkKeypadNone();
			break;
		}
	}

	public void processHover(bool isOver, string hoverName)
	{
		if (isOver)
		{
			if (unchangeableKeyList.ContainsKey(hoverName))
			{
				tooltipScript.showText(game.getGameData().getTextByRefId("keyShortcutPopup08"));
			}
		}
		else
		{
			tooltipScript.setInactive();
		}
	}

	private void OnGUI()
	{
		Event current = Event.current;
		if (modifiedKey != string.Empty && current.isKey && current.keyCode != 0)
		{
			checkDuplicateKey(current.keyCode);
		}
	}

	public void setReference(bool start)
	{
		GameData gameData = game.getGameData();
		saveButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("keyShortcutPopup07").ToUpper(CultureInfo.InvariantCulture);
		saveButton.gameObject.SetActive(value: false);
		resetButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("keyShortcutPopup05").ToUpper(CultureInfo.InvariantCulture);
		title.text = gameData.getTextByRefId("keyShortcutPopup01").ToUpper(CultureInfo.InvariantCulture);
		generalCtrlLabel.text = gameData.getTextByRefId("keyShortcutPopup02").ToUpper(CultureInfo.InvariantCulture);
		shopCtrlLabel.text = gameData.getTextByRefId("keyShortcutPopup03").ToUpper(CultureInfo.InvariantCulture);
		fromStart = start;
		List<KeyShortcut> keyShortcutList = gameData.getKeyShortcutList();
		foreach (KeyShortcut item in keyShortcutList)
		{
			GameObject aParent = generalKeyGrid.gameObject;
			if (item.getCategory() == "SHOP")
			{
				aParent = shopKeyGrid.gameObject;
			}
			GameObject aObject = commonScreenObject.createPrefab(aParent, "Key_" + item.getKeyShortcutRefID(), "Prefab/KeyboardShortcut/KeyboardList", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(aObject, "Function").GetComponent<UILabel>().text = item.getFunction();
			string aName = item.getAssignedKey().ToString();
			GameObject gameObject = commonScreenObject.findChild(aObject, "KeyBg/Key").gameObject;
			if (item.getAssignedKey() == KeyCode.None)
			{
				gameObject.transform.parent.GetComponent<UISprite>().spriteName = "keyboardBg_empty";
				gameObject.GetComponent<UILabel>().text = string.Empty;
			}
			else
			{
				gameObject.GetComponent<UILabel>().text = CommonAPI.formatKeycodeName(aName);
			}
			gameObject.name = item.getKeyShortcutRefID();
			if (Constants.LANGUAGE == LanguageType.kLanguageTypeJap)
			{
				commonScreenObject.findChild(aObject, "Function").GetComponent<UILabel>().fontSize = 10;
				gameObject.GetComponent<UILabel>().fontSize = 12;
			}
			keyList.Add(item.getKeyShortcutRefID(), item);
			tempKeyList.Add(item.getKeyShortcutRefID(), item.getAssignedKey());
			keyObjList.Add(item.getKeyShortcutRefID(), gameObject);
			if (!item.getCanBeChanged())
			{
				gameObject.GetComponent<UILabel>().color = unchangeableKeyColor;
				unchangeableKeyList.Add(item.getKeyShortcutRefID(), item.getAssignedKey());
			}
		}
		generalKeyGrid.Reposition();
		generalScrollBar.value = 0f;
		generalKeyGrid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		generalKeyGrid.enabled = true;
		shopKeyGrid.Reposition();
		shopKeyGrid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		shopScrollBar.value = 0f;
		shopKeyGrid.enabled = true;
	}

	public void setModifiedKey(string aKeyRefID)
	{
		if (keyList[aKeyRefID].getCanBeChanged())
		{
			checkModifiedKey();
			modifiedKey = aKeyRefID;
			keyObjList[aKeyRefID].transform.parent.GetComponent<UISprite>().spriteName = "keyboardBg_selected";
			keyObjList[aKeyRefID].GetComponent<UILabel>().color = Color.black;
		}
	}

	public void resetKeysToDefault()
	{
		checkModifiedKey();
		foreach (KeyValuePair<string, KeyShortcut> key in keyList)
		{
			KeyCode defaultKey = key.Value.getDefaultKey();
			if (tempKeyList[key.Key] != defaultKey)
			{
				keyObjList[key.Key].transform.parent.GetComponent<UISprite>().spriteName = "keyboardBg_changed";
				keyObjList[key.Key].GetComponent<UILabel>().color = Color.white;
			}
			tempKeyList[key.Key] = defaultKey;
			keyObjList[key.Key].GetComponent<UILabel>().text = CommonAPI.formatKeycodeName(defaultKey.ToString());
		}
		saveButton.gameObject.SetActive(value: true);
	}

	public void checkDuplicateKey(KeyCode aKeyCode)
	{
		if (!checkChangeableKey(aKeyCode))
		{
			return;
		}
		string text = string.Empty;
		foreach (KeyValuePair<string, KeyCode> tempKey in tempKeyList)
		{
			if (aKeyCode == tempKey.Value && tempKey.Key != modifiedKey)
			{
				text = tempKey.Key;
			}
		}
		if (text != string.Empty)
		{
			keyObjList[text].transform.parent.GetComponent<UISprite>().spriteName = "keyboardBg_empty";
			tempKeyList[text] = KeyCode.None;
			keyObjList[text].GetComponent<UILabel>().text = string.Empty;
		}
		tempKeyList[modifiedKey] = aKeyCode;
		keyObjList[modifiedKey].transform.parent.GetComponent<UISprite>().spriteName = "keyboardBg_changed";
		keyObjList[modifiedKey].GetComponent<UILabel>().color = Color.white;
		keyObjList[modifiedKey].GetComponent<UILabel>().text = CommonAPI.formatKeycodeName(aKeyCode.ToString());
		modifiedKey = string.Empty;
		saveButton.gameObject.SetActive(value: true);
	}

	public void enableAllButtons()
	{
		closeButton.isEnabled = true;
		foreach (KeyValuePair<string, GameObject> keyObj in keyObjList)
		{
			keyObj.Value.GetComponent<BoxCollider>().enabled = true;
		}
	}

	public void disableAllButtons()
	{
		closeButton.isEnabled = false;
		foreach (GameObject value in keyObjList.Values)
		{
			value.GetComponent<BoxCollider>().enabled = false;
		}
	}

	public void saveChanges()
	{
		foreach (KeyValuePair<string, KeyCode> tempKey in tempKeyList)
		{
			keyList[tempKey.Key].setAssignedKey(tempKey.Value);
		}
		foreach (GameObject value in keyObjList.Values)
		{
			commonScreenObject.destroyPrefabImmediate(value.transform.parent.transform.parent.gameObject);
		}
		unchangeableKeyList.Clear();
		keyList.Clear();
		tempKeyList.Clear();
		keyObjList.Clear();
		setReference(fromStart);
	}

	public void checkKeypadNone()
	{
		bool flag = false;
		foreach (KeyCode value in tempKeyList.Values)
		{
			if (value == KeyCode.None)
			{
				flag = true;
			}
		}
		if (flag)
		{
			viewController.showKeyPopup(fromStart, game.getGameData().getTextByRefId("keyShortcutPopup09"), confirmReset: false);
		}
		else
		{
			saveChanges();
		}
	}

	private void checkModifiedKey()
	{
		if (modifiedKey != string.Empty)
		{
			keyObjList[modifiedKey].GetComponent<UILabel>().color = Color.white;
			if (tempKeyList[modifiedKey] == KeyCode.None)
			{
				keyObjList[modifiedKey].transform.parent.GetComponent<UISprite>().spriteName = "keyboardBg_empty";
			}
			else if (tempKeyList[modifiedKey] != keyList[modifiedKey].getAssignedKey())
			{
				keyObjList[modifiedKey].transform.parent.GetComponent<UISprite>().spriteName = "keyboardBg_changed";
			}
			else
			{
				keyObjList[modifiedKey].transform.parent.GetComponent<UISprite>().spriteName = "keyboardBg_round";
			}
		}
	}

	private bool checkChangeableKey(KeyCode aKeyCode)
	{
		bool result = true;
		foreach (KeyCode value in unchangeableKeyList.Values)
		{
			if (aKeyCode == value)
			{
				result = false;
			}
		}
		return result;
	}
}
