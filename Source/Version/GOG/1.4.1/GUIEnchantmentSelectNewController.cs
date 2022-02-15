using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIEnchantmentSelectNewController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ShopMenuController shopMenuController;

	private TooltipTextScript tooltipScript;

	private UILabel enchantSelectTitleLabel;

	private UILabel selectEnchantText;

	private UIGrid enchantment_Grid;

	private UIScrollBar enchantListScrollbar;

	private GameObject skipButton;

	private GameObject selectButton;

	private List<Item> itemList;

	private int selectedItemIndex;

	private string horizontalGridPrefix;

	private string enchantmentPrefix;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		enchantSelectTitleLabel = commonScreenObject.findChild(base.gameObject, "EnchantSelectTitle_bg/EnchantSelectTitle_label").GetComponent<UILabel>();
		selectEnchantText = commonScreenObject.findChild(base.gameObject, "SelectEnchantText").GetComponent<UILabel>();
		enchantment_Grid = commonScreenObject.findChild(base.gameObject, "Panel_EnchantPanel/Enchantment_Grid").GetComponent<UIGrid>();
		enchantListScrollbar = commonScreenObject.findChild(base.gameObject, "EnchantList_scrollbar").GetComponent<UIScrollBar>();
		skipButton = commonScreenObject.findChild(base.gameObject, "SkipButton").gameObject;
		selectButton = commonScreenObject.findChild(base.gameObject, "SelectButton").gameObject;
		itemList = new List<Item>();
		selectedItemIndex = -1;
		horizontalGridPrefix = "HoriGrid_";
		enchantmentPrefix = "Enchant_";
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName == "SelectButton")
		{
			tooltipScript.setInactive();
			GameObject.Find("ViewController").GetComponent<ViewController>().closeEnchantmentSelectNew();
			if (selectedItemIndex != -1 && selectedItemIndex != itemList.Count)
			{
				shopMenuController.insertStoredInput("enchantItem", selectedItemIndex + 1);
			}
			shopMenuController.addEnchantItem(itemList[selectedItemIndex]);
		}
		else if (gameObjectName == "SkipButton")
		{
			tooltipScript.setInactive();
			GameObject.Find("ViewController").GetComponent<ViewController>().closeEnchantmentSelectNew();
			shopMenuController.showPopEvent(PopEventType.PopEventTypeNaming);
		}
		else
		{
			string[] array = gameObjectName.Split('_');
			int index = CommonAPI.parseInt(array[1]);
			selectEnchantment(index);
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
			if (array[0] == "Enchant")
			{
				tooltipScript.showText(itemList[CommonAPI.parseInt(array[1])].getItemName());
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
		if (selectButton.GetComponent<UIButton>().isEnabled && Input.GetKeyDown(gameData.getKeyCodeByRefID("100008")) && !GameObject.Find("blackmask_popup").GetComponent<BoxCollider>().enabled)
		{
			processClick("SelectButton");
		}
		else if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(gameData.getKeyCodeByRefID("100007"))) && !GameObject.Find("blackmask_popup").GetComponent<BoxCollider>().enabled)
		{
			processClick("SkipButton");
		}
	}

	public void setReference()
	{
		GameData gameData = game.getGameData();
		enchantSelectTitleLabel.text = gameData.getTextByRefId("menuForgeBoost28").ToUpper(CultureInfo.InvariantCulture);
		selectEnchantText.text = gameData.getTextByRefId("menuForgeBoost41");
		selectButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral13");
		selectButton.GetComponent<UIButton>().isEnabled = false;
		selectButton.GetComponentInChildren<UILabel>().color = Color.grey;
		skipButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral14");
		itemList = gameData.getSortedItemList(ownedOnly: true, includeSpecial: true, ItemType.ItemTypeEnhancement, string.Empty);
		int num = 0;
		GameObject gameObject = null;
		for (int i = 0; i < itemList.Count; i++)
		{
			Item item = itemList[i];
			string text = convertIndexToString(i);
			gameObject = commonScreenObject.createPrefab(enchantment_Grid.gameObject, enchantmentPrefix + text, "Prefab/ForgeMenu/ForgeMenuNEW/EnchantObject", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(gameObject, "ItemHolder/ObjectImg").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Enchantment/" + item.getImage());
			commonScreenObject.findChild(gameObject, "QuantityFrame/Quantity").GetComponent<UILabel>().text = CommonAPI.formatNumber(item.getItemNum());
			commonScreenObject.findChild(gameObject, "SelectedFrame").GetComponent<UISprite>().enabled = false;
			UISprite component = commonScreenObject.findChild(gameObject, "StatHolder/StatIcon").GetComponent<UISprite>();
			switch (item.getItemEffectStat())
			{
			case WeaponStat.WeaponStatAttack:
				component.spriteName = "ico_atk";
				break;
			case WeaponStat.WeaponStatSpeed:
				component.spriteName = "ico_speed";
				break;
			case WeaponStat.WeaponStatAccuracy:
				component.spriteName = "ico_acc";
				break;
			case WeaponStat.WeaponStatMagic:
				component.spriteName = "ico_enh";
				break;
			}
			commonScreenObject.findChild(gameObject, "StatHolder/StatValue").GetComponent<UILabel>().text = "+" + item.getItemEffectValue();
			commonScreenObject.findChild(gameObject, "Prefix_bg/Prefix_label").GetComponent<UILabel>().text = item.getItemEffectString();
		}
		enchantment_Grid.GetComponent<UIGrid>().Reposition();
		enchantment_Grid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		enchantListScrollbar.value = 0f;
	}

	private string convertIndexToString(int i)
	{
		string result = i.ToString();
		if (i < 10)
		{
			result = "0" + i;
		}
		return result;
	}

	private void selectEnchantment(int index)
	{
		if (selectedItemIndex != -1)
		{
			commonScreenObject.findChild(GameObject.Find(enchantmentPrefix + convertIndexToString(selectedItemIndex)), "SelectedFrame").GetComponent<UISprite>().enabled = false;
		}
		selectedItemIndex = index;
		commonScreenObject.findChild(GameObject.Find(enchantmentPrefix + convertIndexToString(selectedItemIndex)), "SelectedFrame").GetComponent<UISprite>().enabled = true;
		selectButton.GetComponent<UIButton>().isEnabled = true;
		selectButton.GetComponentInChildren<UILabel>().color = Color.white;
	}
}
