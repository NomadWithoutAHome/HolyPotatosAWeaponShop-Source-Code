using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIOpenTreasureController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private Dictionary<string, RewardChestItem> chestItemList;

	private List<UITexture> itemList;

	private UISprite chestImg;

	private UISprite coverImg;

	private UISprite glowImg;

	private UILabel treasureUnlockedLabel;

	private UIGrid treasureListGrid;

	private int currTreasureIndex;

	private string treasurePrefix;

	private bool isAnimating;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		itemList = new List<UITexture>();
		chestImg = commonScreenObject.findChild(base.gameObject, "ChestImg").GetComponent<UISprite>();
		coverImg = commonScreenObject.findChild(base.gameObject, "CoverImg").GetComponent<UISprite>();
		glowImg = commonScreenObject.findChild(base.gameObject, "GlowImg").GetComponent<UISprite>();
		treasureUnlockedLabel = commonScreenObject.findChild(base.gameObject, "Ribbon/TreasureUnlockedLabel").GetComponent<UILabel>();
		treasureListGrid = commonScreenObject.findChild(base.gameObject, "TreasureListGrid").GetComponent<UIGrid>();
		currTreasureIndex = 0;
		treasurePrefix = "treasure_";
		isAnimating = false;
	}

	public void setReference(Dictionary<string, RewardChestItem> aChestItemList)
	{
		GameData gameData = game.getGameData();
		chestItemList = aChestItemList;
		glowImg.enabled = false;
		treasureUnlockedLabel.text = gameData.getTextByRefId("questComplete03");
		int num = 0;
		foreach (string key in chestItemList.Keys)
		{
			GameObject aObject = commonScreenObject.createPrefab(treasureListGrid.gameObject, treasurePrefix + num, "Prefab/Quest/Treasure/TreasureObjectBg", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(aObject, "TagBg/TagValue").GetComponent<UILabel>().text = key;
			string itemRefId = chestItemList[key].getItemRefId();
			UITexture component = commonScreenObject.findChild(aObject, "TreasureObjectImg").GetComponent<UITexture>();
			if (itemRefId == "-1")
			{
				component.mainTexture = commonScreenObject.loadTexture("Image/Enchantment/amulet of strength");
			}
			else
			{
				Item itemByRefId = gameData.getItemByRefId(itemRefId);
				component.mainTexture = commonScreenObject.loadTexture("Image/Enchantment/amulet of strength");
			}
			component.enabled = false;
			itemList.Add(component);
		}
		treasureListGrid.Reposition();
		openChest();
	}

	private void openChest()
	{
		Vector3 localPosition = coverImg.transform.localPosition;
		Vector3 aEndPosition = localPosition;
		aEndPosition.y += 15f;
		commonScreenObject.tweenPosition(coverImg.GetComponent<TweenPosition>(), localPosition, aEndPosition, 0.5f, base.gameObject, "activateGlow");
	}

	private void activateGlow()
	{
		glowImg.enabled = true;
		isAnimating = true;
		StartCoroutine("animateItem");
	}

	private IEnumerator animateItem()
	{
		while (isAnimating)
		{
			CommonAPI.debug("itemList: " + itemList.Count);
			UITexture selectedTexture = itemList[currTreasureIndex];
			selectedTexture.enabled = true;
			TweenPosition tweenPos = selectedTexture.GetComponent<TweenPosition>();
			TweenRotation tweenRot = selectedTexture.GetComponent<TweenRotation>();
			selectedTexture.transform.position = chestImg.transform.position;
			Vector3 currLocalPos = selectedTexture.transform.localPosition;
			Vector3 newLocalPos = currLocalPos;
			newLocalPos.y += 75f;
			commonScreenObject.tweenPosition(tweenPos, currLocalPos, newLocalPos, 0.4f, null, string.Empty);
			yield return new WaitForSeconds(1f);
			Vector3 newRotatePos = Vector3.zero;
			newRotatePos.z += 360f;
			selectedTexture.depth = 10;
			commonScreenObject.tweenPosition(tweenPos, newLocalPos, Vector3.zero, 0.8f, null, string.Empty);
			commonScreenObject.tweenRotation(tweenRot, Vector3.zero, newRotatePos, 0.8f, null, string.Empty);
			yield return new WaitForSeconds(1.2f);
			if (currTreasureIndex < itemList.Count - 1)
			{
				currTreasureIndex++;
				continue;
			}
			isAnimating = false;
			StartCoroutine("closePopup");
		}
	}

	private IEnumerator closePopup()
	{
		yield return new WaitForSeconds(2f);
		viewController.closeOpenTreasure();
	}
}
