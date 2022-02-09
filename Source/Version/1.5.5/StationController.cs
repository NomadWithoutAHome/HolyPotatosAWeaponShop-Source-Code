using System.Collections.Generic;
using UnityEngine;

public class StationController : MonoBehaviour
{
	private Game game;

	private GUIGridController gridController;

	private CommonScreenObject commonScreenObject;

	private Station designStation;

	private Station craftStation;

	private Station polishStation;

	private Station enchantStation;

	private Station dogBedStation;

	private List<GameObject> designIndicator;

	private GameObject designBoostIndicator;

	private List<GameObject> craftIndicator;

	private GameObject craftBoostIndicator;

	private List<GameObject> polishIndicator;

	private GameObject polishBoostIndicator;

	private List<GameObject> enchantIndicator;

	private GameObject enchantBoostIndicator;

	private string stationPrefix;

	private string designPrefix;

	private string craftPrefix;

	private string polishPrefix;

	private string enchantPrefix;

	private string boostPrefix;

	private string dogBedPrefix;

	private GameObject panel_characters;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gridController = GameObject.Find("GUIGridController").GetComponent<GUIGridController>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		designStation = null;
		craftStation = null;
		polishStation = null;
		enchantStation = null;
		designIndicator = new List<GameObject>();
		designBoostIndicator = null;
		craftIndicator = new List<GameObject>();
		craftBoostIndicator = null;
		polishIndicator = new List<GameObject>();
		polishBoostIndicator = null;
		enchantIndicator = new List<GameObject>();
		enchantBoostIndicator = null;
		stationPrefix = "Station_";
		designPrefix = "Design";
		craftPrefix = "Craft";
		polishPrefix = "Polish";
		enchantPrefix = "Enchant";
		boostPrefix = "Boost";
		dogBedPrefix = "DogBed";
		panel_characters = GameObject.Find("Panel_Characters");
	}

	public void setStation()
	{
		resetGameObject();
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		ShopLevel shopLevel = player.getShopLevel();
		int shopLevelInt = player.getShopLevelInt();
		Vector3 aRotation = new Vector3(35.264f, 45f, 0f);
		Sprite sprite = commonScreenObject.loadSprite("Image/Smith/Spot/" + shopLevel.getSpotIndicatorImage());
		int furnitureLevel = player.getHighestPlayerFurnitureByType("601").getFurnitureLevel();
		designStation = gameData.getPhaseStation(SmithStation.SmithStationDesign, shopLevelInt, furnitureLevel);
		List<Vector2> stationPointList = designStation.getStationPointList();
		for (int i = 0; i < stationPointList.Count; i++)
		{
			Vector3 position = gridController.getPosition((int)stationPointList[i].x, (int)stationPointList[i].y);
			GameObject gameObject = commonScreenObject.createPrefab(panel_characters, designPrefix + stationPrefix + i, "Prefab/Station/Station", position, Vector3.one, aRotation);
			commonScreenObject.findChild(gameObject, "Spot").gameObject.AddComponent<BoxCollider>();
			commonScreenObject.findChild(gameObject, "SpotLv").GetComponent<SpriteRenderer>().sprite = sprite;
			designIndicator.Add(gameObject);
		}
		Vector3 position2 = gridController.getPosition((int)designStation.getDogStationPoint().x, (int)designStation.getDogStationPoint().y);
		designBoostIndicator = commonScreenObject.createPrefab(panel_characters, designPrefix + boostPrefix, "Prefab/Station/Station", position2, Vector3.one, aRotation);
		commonScreenObject.findChild(designBoostIndicator, "Spot").gameObject.AddComponent<BoxCollider>();
		commonScreenObject.findChild(designBoostIndicator, "SpotLv").GetComponent<SpriteRenderer>().sprite = sprite;
		int furnitureLevel2 = player.getHighestPlayerFurnitureByType("701").getFurnitureLevel();
		craftStation = gameData.getPhaseStation(SmithStation.SmithStationCraft, shopLevelInt, furnitureLevel2);
		List<Vector2> stationPointList2 = craftStation.getStationPointList();
		for (int j = 0; j < stationPointList2.Count; j++)
		{
			Vector3 position3 = gridController.getPosition((int)stationPointList2[j].x, (int)stationPointList2[j].y);
			GameObject gameObject2 = commonScreenObject.createPrefab(panel_characters, craftPrefix + stationPrefix + j, "Prefab/Station/Station", position3, Vector3.one, aRotation);
			commonScreenObject.findChild(gameObject2, "Spot").gameObject.AddComponent<BoxCollider>();
			commonScreenObject.findChild(gameObject2, "SpotLv").GetComponent<SpriteRenderer>().sprite = sprite;
			craftIndicator.Add(gameObject2);
		}
		Vector3 position4 = gridController.getPosition((int)craftStation.getDogStationPoint().x, (int)craftStation.getDogStationPoint().y);
		craftBoostIndicator = commonScreenObject.createPrefab(panel_characters, craftPrefix + boostPrefix, "Prefab/Station/Station", position4, Vector3.one, aRotation);
		commonScreenObject.findChild(craftBoostIndicator, "Spot").gameObject.AddComponent<BoxCollider>();
		commonScreenObject.findChild(craftBoostIndicator, "SpotLv").GetComponent<SpriteRenderer>().sprite = sprite;
		int furnitureLevel3 = player.getHighestPlayerFurnitureByType("801").getFurnitureLevel();
		polishStation = gameData.getPhaseStation(SmithStation.SmithStationPolish, shopLevelInt, furnitureLevel3);
		List<Vector2> stationPointList3 = polishStation.getStationPointList();
		for (int k = 0; k < stationPointList3.Count; k++)
		{
			Vector3 position5 = gridController.getPosition((int)stationPointList3[k].x, (int)stationPointList3[k].y);
			GameObject gameObject3 = commonScreenObject.createPrefab(panel_characters, polishPrefix + stationPrefix + k, "Prefab/Station/Station", position5, Vector3.one, aRotation);
			commonScreenObject.findChild(gameObject3, "Spot").gameObject.AddComponent<BoxCollider>();
			commonScreenObject.findChild(gameObject3, "SpotLv").GetComponent<SpriteRenderer>().sprite = sprite;
			polishIndicator.Add(gameObject3);
		}
		Vector3 position6 = gridController.getPosition((int)polishStation.getDogStationPoint().x, (int)polishStation.getDogStationPoint().y);
		polishBoostIndicator = commonScreenObject.createPrefab(panel_characters, polishPrefix + boostPrefix, "Prefab/Station/Station", position6, Vector3.one, aRotation);
		commonScreenObject.findChild(polishBoostIndicator, "Spot").gameObject.AddComponent<BoxCollider>();
		commonScreenObject.findChild(polishBoostIndicator, "SpotLv").GetComponent<SpriteRenderer>().sprite = sprite;
		int furnitureLevel4 = player.getHighestPlayerFurnitureByType("901").getFurnitureLevel();
		if (!player.getEnchantLocked())
		{
			enchantStation = gameData.getPhaseStation(SmithStation.SmithStationEnchant, shopLevelInt, furnitureLevel4);
			List<Vector2> stationPointList4 = enchantStation.getStationPointList();
			for (int l = 0; l < stationPointList4.Count; l++)
			{
				Vector3 position7 = gridController.getPosition((int)stationPointList4[l].x, (int)stationPointList4[l].y);
				GameObject gameObject4 = commonScreenObject.createPrefab(panel_characters, enchantPrefix + stationPrefix + l, "Prefab/Station/Station", position7, Vector3.one, aRotation);
				commonScreenObject.findChild(gameObject4, "Spot").gameObject.AddComponent<BoxCollider>();
				commonScreenObject.findChild(gameObject4, "SpotLv").GetComponent<SpriteRenderer>().sprite = sprite;
				enchantIndicator.Add(gameObject4);
			}
			Vector3 position8 = gridController.getPosition((int)enchantStation.getDogStationPoint().x, (int)enchantStation.getDogStationPoint().y);
			enchantBoostIndicator = commonScreenObject.createPrefab(panel_characters, enchantPrefix + boostPrefix, "Prefab/Station/Station", position8, Vector3.one, aRotation);
			commonScreenObject.findChild(enchantBoostIndicator, "Spot").gameObject.AddComponent<BoxCollider>();
			commonScreenObject.findChild(enchantBoostIndicator, "SpotLv").GetComponent<SpriteRenderer>().sprite = sprite;
		}
		dogBedStation = gameData.getPhaseStation(SmithStation.SmithStationDogHome, shopLevelInt, 1);
		disableAllCollider();
		disableBoostCollider();
		GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>().checkProjectStatus(hasTimePass: false, checkDog: false);
	}

	public Station getStationBySmithStation(SmithStation aStation)
	{
		return aStation switch
		{
			SmithStation.SmithStationDesign => designStation, 
			SmithStation.SmithStationCraft => craftStation, 
			SmithStation.SmithStationPolish => polishStation, 
			SmithStation.SmithStationEnchant => enchantStation, 
			_ => null, 
		};
	}

	public Station getDesignStation()
	{
		return designStation;
	}

	public Station getCraftStation()
	{
		return craftStation;
	}

	public Station getPolishStation()
	{
		return polishStation;
	}

	public Station getEnchantStation()
	{
		return enchantStation;
	}

	public Station getDogBedStation()
	{
		return dogBedStation;
	}

	public void assignSmithStations()
	{
		Player player = game.getPlayer();
		List<Smith> stationSmithArray = player.getStationSmithArray(SmithStation.SmithStationCraft);
		if (stationSmithArray.Count > 0)
		{
			craftStation.assignSmiths(stationSmithArray);
		}
		List<Smith> stationSmithArray2 = player.getStationSmithArray(SmithStation.SmithStationDesign);
		if (stationSmithArray2.Count > 0)
		{
			designStation.assignSmiths(stationSmithArray2);
		}
		List<Smith> stationSmithArray3 = player.getStationSmithArray(SmithStation.SmithStationEnchant);
		if (stationSmithArray3.Count > 0 && enchantStation != null)
		{
			enchantStation.assignSmiths(stationSmithArray3);
		}
		List<Smith> stationSmithArray4 = player.getStationSmithArray(SmithStation.SmithStationPolish);
		if (stationSmithArray4.Count > 0)
		{
			polishStation.assignSmiths(stationSmithArray4);
		}
	}

	public Vector2 getSmithStation(Smith aSmith)
	{
		switch (aSmith.getCurrentStation())
		{
		case SmithStation.SmithStationCraft:
			return craftStation.getSmithStationPoint(aSmith);
		case SmithStation.SmithStationDesign:
			return designStation.getSmithStationPoint(aSmith);
		case SmithStation.SmithStationEnchant:
			if (enchantStation != null)
			{
				return enchantStation.getSmithStationPoint(aSmith);
			}
			return new Vector2(-1f, -1f);
		case SmithStation.SmithStationPolish:
			return polishStation.getSmithStationPoint(aSmith);
		default:
			return new Vector2(-1f, -1f);
		}
	}

	public Vector2 getSmithStationByIndex(SmithStation currStation, int aIndex)
	{
		switch (currStation)
		{
		case SmithStation.SmithStationCraft:
			return craftStation.getSmithStationByIndex(aIndex);
		case SmithStation.SmithStationDesign:
			return designStation.getSmithStationByIndex(aIndex);
		case SmithStation.SmithStationEnchant:
			if (enchantStation != null)
			{
				return enchantStation.getSmithStationByIndex(aIndex);
			}
			return new Vector2(-1f, -1f);
		case SmithStation.SmithStationPolish:
			return polishStation.getSmithStationByIndex(aIndex);
		default:
			return new Vector2(-1f, -1f);
		}
	}

	public List<string> getObstacleRefID(SmithStation aStation)
	{
		switch (aStation)
		{
		case SmithStation.SmithStationCraft:
			return craftStation.getObstacleRefID();
		case SmithStation.SmithStationDesign:
			return designStation.getObstacleRefID();
		case SmithStation.SmithStationEnchant:
			if (enchantStation != null)
			{
				return enchantStation.getObstacleRefID();
			}
			return new List<string>();
		case SmithStation.SmithStationPolish:
			return polishStation.getObstacleRefID();
		default:
			return new List<string>();
		}
	}

	public Vector2 getDogStationPoint(SmithStation aStation)
	{
		switch (aStation)
		{
		case SmithStation.SmithStationCraft:
			return craftStation.getDogStationPoint();
		case SmithStation.SmithStationDesign:
			return designStation.getDogStationPoint();
		case SmithStation.SmithStationEnchant:
			if (enchantStation != null)
			{
				return enchantStation.getDogStationPoint();
			}
			return new Vector2(-1f, -1f);
		case SmithStation.SmithStationPolish:
			return polishStation.getDogStationPoint();
		case SmithStation.SmithStationDogHome:
		case SmithStation.SmithStationBlank:
			return getDogBedStationPoint();
		default:
			return new Vector2(-1f, -1f);
		}
	}

	public Vector2 getDogBedStationPoint()
	{
		return dogBedStation.getDogStationPoint();
	}

	public void enableIndicator(SmithStation aStation, int index)
	{
		switch (aStation)
		{
		case SmithStation.SmithStationCraft:
			craftIndicator[index].SetActive(value: true);
			break;
		case SmithStation.SmithStationDesign:
			designIndicator[index].SetActive(value: true);
			break;
		case SmithStation.SmithStationEnchant:
			if (enchantIndicator.Count > 0)
			{
				enchantIndicator[index].SetActive(value: true);
			}
			break;
		case SmithStation.SmithStationPolish:
			polishIndicator[index].SetActive(value: true);
			break;
		}
	}

	public void disableIndicator(Smith aSmith)
	{
		int num = -1;
		switch (aSmith.getCurrentStation())
		{
		case SmithStation.SmithStationCraft:
			num = craftStation.getSmithStationPointIndex(aSmith);
			if (num != -1 && craftIndicator[num].activeSelf)
			{
				craftIndicator[num].SetActive(value: false);
			}
			break;
		case SmithStation.SmithStationDesign:
			num = designStation.getSmithStationPointIndex(aSmith);
			if (num != -1 && designIndicator[num].activeSelf)
			{
				designIndicator[num].SetActive(value: false);
			}
			break;
		case SmithStation.SmithStationEnchant:
			if (enchantStation != null)
			{
				num = enchantStation.getSmithStationPointIndex(aSmith);
				if (num != -1 && enchantIndicator[num].activeSelf)
				{
					enchantIndicator[num].SetActive(value: false);
				}
			}
			break;
		case SmithStation.SmithStationPolish:
			num = polishStation.getSmithStationPointIndex(aSmith);
			if (num != -1 && polishIndicator[num].activeSelf)
			{
				polishIndicator[num].SetActive(value: false);
			}
			break;
		}
	}

	public void enableTypeIndicator(Smith aSmith)
	{
		int num = -1;
		switch (aSmith.getCurrentStation())
		{
		case SmithStation.SmithStationCraft:
			num = craftStation.getSmithStationPointIndex(aSmith);
			if (num != -1 && craftIndicator[num].activeSelf)
			{
				commonScreenObject.findChild(craftIndicator[num], "SpotType").GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Smith/Spot/spotdark_craft");
			}
			break;
		case SmithStation.SmithStationDesign:
			num = designStation.getSmithStationPointIndex(aSmith);
			if (num != -1 && designIndicator[num].activeSelf)
			{
				commonScreenObject.findChild(designIndicator[num], "SpotType").GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Smith/Spot/spotdark_design");
			}
			break;
		case SmithStation.SmithStationEnchant:
			if (enchantStation != null)
			{
				num = enchantStation.getSmithStationPointIndex(aSmith);
				if (num != -1 && enchantIndicator[num].activeSelf)
				{
					commonScreenObject.findChild(enchantIndicator[num], "SpotType").GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Smith/Spot/spotdark_enchant");
				}
			}
			break;
		case SmithStation.SmithStationPolish:
			num = polishStation.getSmithStationPointIndex(aSmith);
			if (num != -1 && polishIndicator[num].activeSelf)
			{
				commonScreenObject.findChild(polishIndicator[num], "SpotType").GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Smith/Spot/spotdark_polish");
			}
			break;
		}
	}

	public void disableTypeIndicator(SmithStation aStation, int index)
	{
		CommonAPI.debug("disableTypeIndicator: " + aStation.ToString() + " index: " + index);
		switch (aStation)
		{
		case SmithStation.SmithStationCraft:
			commonScreenObject.findChild(craftIndicator[index], "SpotType").GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Smith/Spot/None");
			break;
		case SmithStation.SmithStationDesign:
			commonScreenObject.findChild(designIndicator[index], "SpotType").GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Smith/Spot/None");
			break;
		case SmithStation.SmithStationEnchant:
			if (enchantIndicator.Count > 0)
			{
				commonScreenObject.findChild(enchantIndicator[index], "SpotType").GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Smith/Spot/None");
			}
			break;
		case SmithStation.SmithStationPolish:
			commonScreenObject.findChild(polishIndicator[index], "SpotType").GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Smith/Spot/None");
			break;
		}
	}

	public void activateCollider(bool exchangeable)
	{
		activateACollider(designIndicator, designStation, exchangeable);
		activateACollider(craftIndicator, craftStation, exchangeable);
		activateACollider(polishIndicator, polishStation, exchangeable);
		if (enchantStation != null)
		{
			activateACollider(enchantIndicator, enchantStation, exchangeable);
		}
	}

	public void activateDesignCollider(bool exchangeable)
	{
		activateACollider(designIndicator, designStation, exchangeable);
	}

	private void activateACollider(List<GameObject> indicatorList, Station aStation, bool exchangeable)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		switch (aStation.getSmithStation())
		{
		case SmithStation.SmithStationDesign:
			text = "spotbright_design";
			text2 = "spotdark_design";
			break;
		case SmithStation.SmithStationCraft:
			text = "spotbright_craft";
			text2 = "spotdark_craft";
			break;
		case SmithStation.SmithStationPolish:
			text = "spotbright_polish";
			text2 = "spotdark_polish";
			break;
		case SmithStation.SmithStationEnchant:
			text = "spotbright_enchant";
			text2 = "spotdark_enchant";
			break;
		}
		for (int i = 0; i < indicatorList.Count; i++)
		{
			if (aStation.checkEmptyStation(i))
			{
				commonScreenObject.findChild(indicatorList[i], "SpotType").GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Smith/Spot/" + text);
			}
			else
			{
				commonScreenObject.findChild(indicatorList[i], "SpotType").GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Smith/Spot/" + text2);
			}
			indicatorList[i].SetActive(value: true);
			if (!aStation.checkEmptyStation(i) && !exchangeable)
			{
				indicatorList[i].GetComponentInChildren<BoxCollider>().enabled = false;
			}
			else
			{
				indicatorList[i].GetComponentInChildren<BoxCollider>().enabled = true;
			}
		}
	}

	public void disableAllCollider()
	{
		disableACollider(designIndicator, designStation);
		disableACollider(craftIndicator, craftStation);
		disableACollider(polishIndicator, polishStation);
		if (enchantStation != null)
		{
			disableACollider(enchantIndicator, enchantStation);
		}
	}

	private void disableACollider(List<GameObject> indicatorList, Station aStation)
	{
		for (int i = 0; i < indicatorList.Count; i++)
		{
			if (indicatorList[i].activeSelf)
			{
				indicatorList[i].GetComponentInChildren<BoxCollider>().enabled = false;
				if (aStation.checkEmptyStation(i))
				{
					commonScreenObject.findChild(indicatorList[i], "SpotType").GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("none");
				}
			}
		}
	}

	public void activateBoostCollider(SmithStation exceptStation = SmithStation.SmithStationBlank)
	{
		if (exceptStation != SmithStation.SmithStationDesign)
		{
			designBoostIndicator.GetComponentInChildren<BoxCollider>().enabled = true;
		}
		if (exceptStation != SmithStation.SmithStationCraft)
		{
			craftBoostIndicator.GetComponentInChildren<BoxCollider>().enabled = true;
		}
		if (exceptStation != SmithStation.SmithStationPolish)
		{
			polishBoostIndicator.GetComponentInChildren<BoxCollider>().enabled = true;
		}
		if (enchantBoostIndicator != null && exceptStation != SmithStation.SmithStationEnchant)
		{
			enchantBoostIndicator.GetComponentInChildren<BoxCollider>().enabled = true;
		}
	}

	public void disableBoostCollider()
	{
		designBoostIndicator.GetComponentInChildren<BoxCollider>().enabled = false;
		craftBoostIndicator.GetComponentInChildren<BoxCollider>().enabled = false;
		polishBoostIndicator.GetComponentInChildren<BoxCollider>().enabled = false;
		if (enchantBoostIndicator != null)
		{
			enchantBoostIndicator.GetComponentInChildren<BoxCollider>().enabled = false;
		}
	}

	public void changeBoostLayer(bool bringUp, SmithStation exceptStation = SmithStation.SmithStationBlank)
	{
		string empty = string.Empty;
		empty = ((!bringUp) ? "Character" : "Blackmask");
		if (exceptStation != SmithStation.SmithStationDesign)
		{
			SpriteRenderer[] componentsInChildren = designBoostIndicator.GetComponentsInChildren<SpriteRenderer>();
			SpriteRenderer[] array = componentsInChildren;
			foreach (SpriteRenderer spriteRenderer in array)
			{
				spriteRenderer.sortingLayerName = empty;
			}
		}
		if (exceptStation != SmithStation.SmithStationCraft)
		{
			SpriteRenderer[] componentsInChildren2 = craftBoostIndicator.GetComponentsInChildren<SpriteRenderer>();
			SpriteRenderer[] array2 = componentsInChildren2;
			foreach (SpriteRenderer spriteRenderer2 in array2)
			{
				spriteRenderer2.sortingLayerName = empty;
			}
		}
		if (exceptStation != SmithStation.SmithStationPolish)
		{
			SpriteRenderer[] componentsInChildren3 = polishBoostIndicator.GetComponentsInChildren<SpriteRenderer>();
			SpriteRenderer[] array3 = componentsInChildren3;
			foreach (SpriteRenderer spriteRenderer3 in array3)
			{
				spriteRenderer3.sortingLayerName = empty;
			}
		}
		if (enchantBoostIndicator != null && exceptStation != SmithStation.SmithStationEnchant)
		{
			SpriteRenderer[] componentsInChildren4 = enchantBoostIndicator.GetComponentsInChildren<SpriteRenderer>();
			SpriteRenderer[] array4 = componentsInChildren4;
			foreach (SpriteRenderer spriteRenderer4 in array4)
			{
				spriteRenderer4.sortingLayerName = empty;
			}
		}
	}

	public void changeLayer(bool bringUp)
	{
		string empty = string.Empty;
		empty = ((!bringUp) ? "Character" : "Blackmask");
		for (int i = 0; i < designIndicator.Count; i++)
		{
			SpriteRenderer[] componentsInChildren = designIndicator[i].GetComponentsInChildren<SpriteRenderer>();
			SpriteRenderer[] array = componentsInChildren;
			foreach (SpriteRenderer spriteRenderer in array)
			{
				spriteRenderer.sortingLayerName = empty;
			}
		}
		for (int k = 0; k < craftIndicator.Count; k++)
		{
			SpriteRenderer[] componentsInChildren2 = craftIndicator[k].GetComponentsInChildren<SpriteRenderer>();
			SpriteRenderer[] array2 = componentsInChildren2;
			foreach (SpriteRenderer spriteRenderer2 in array2)
			{
				spriteRenderer2.sortingLayerName = empty;
			}
		}
		for (int m = 0; m < polishIndicator.Count; m++)
		{
			SpriteRenderer[] componentsInChildren3 = polishIndicator[m].GetComponentsInChildren<SpriteRenderer>();
			SpriteRenderer[] array3 = componentsInChildren3;
			foreach (SpriteRenderer spriteRenderer3 in array3)
			{
				spriteRenderer3.sortingLayerName = empty;
			}
		}
		for (int num = 0; num < enchantIndicator.Count; num++)
		{
			SpriteRenderer[] componentsInChildren4 = enchantIndicator[num].GetComponentsInChildren<SpriteRenderer>();
			SpriteRenderer[] array4 = componentsInChildren4;
			foreach (SpriteRenderer spriteRenderer4 in array4)
			{
				spriteRenderer4.sortingLayerName = empty;
			}
		}
	}

	public void changeLayerDesign(bool bringUp)
	{
		string empty = string.Empty;
		empty = ((!bringUp) ? "Character" : "Blackmask");
		for (int i = 0; i < designIndicator.Count; i++)
		{
			SpriteRenderer[] componentsInChildren = designIndicator[i].GetComponentsInChildren<SpriteRenderer>();
			SpriteRenderer[] array = componentsInChildren;
			foreach (SpriteRenderer spriteRenderer in array)
			{
				spriteRenderer.sortingLayerName = empty;
			}
		}
	}

	public void resetStationSmithList()
	{
		if (designStation != null)
		{
			designStation.clearSmithList();
		}
		if (craftStation != null)
		{
			craftStation.clearSmithList();
		}
		if (polishStation != null)
		{
			polishStation.clearSmithList();
		}
		if (enchantStation != null)
		{
			enchantStation.clearSmithList();
		}
	}

	private void resetGameObject()
	{
		foreach (GameObject item in designIndicator)
		{
			commonScreenObject.destroyPrefabImmediate(item);
		}
		designIndicator.Clear();
		commonScreenObject.destroyPrefabImmediate(designBoostIndicator);
		designBoostIndicator = null;
		foreach (GameObject item2 in craftIndicator)
		{
			commonScreenObject.destroyPrefabImmediate(item2);
		}
		craftIndicator.Clear();
		commonScreenObject.destroyPrefabImmediate(craftBoostIndicator);
		craftBoostIndicator = null;
		foreach (GameObject item3 in polishIndicator)
		{
			commonScreenObject.destroyPrefabImmediate(item3);
		}
		polishIndicator.Clear();
		commonScreenObject.destroyPrefabImmediate(polishBoostIndicator);
		polishBoostIndicator = null;
		foreach (GameObject item4 in enchantIndicator)
		{
			commonScreenObject.destroyPrefabImmediate(item4);
		}
		enchantIndicator.Clear();
		commonScreenObject.destroyPrefabImmediate(enchantBoostIndicator);
		enchantBoostIndicator = null;
	}

	public void removeSmith(Smith aSmith)
	{
		switch (aSmith.getCurrentStation())
		{
		case SmithStation.SmithStationCraft:
			craftStation.unassignASmith(aSmith);
			break;
		case SmithStation.SmithStationDesign:
			designStation.unassignASmith(aSmith);
			break;
		case SmithStation.SmithStationEnchant:
			if (enchantStation != null)
			{
				enchantStation.unassignASmith(aSmith);
			}
			break;
		case SmithStation.SmithStationPolish:
			polishStation.unassignASmith(aSmith);
			break;
		}
		aSmith.setAssignedRole(SmithStation.SmithStationBlank);
		aSmith.setCurrentStation(SmithStation.SmithStationBlank);
		aSmith.setCurrentStationIndex(-1);
	}

	public int getSmithStationIndex(Smith aSmith)
	{
		switch (aSmith.getCurrentStation())
		{
		case SmithStation.SmithStationCraft:
			return craftStation.getSmithStationPointIndex(aSmith);
		case SmithStation.SmithStationDesign:
			return designStation.getSmithStationPointIndex(aSmith);
		case SmithStation.SmithStationEnchant:
			if (enchantStation != null)
			{
				return enchantStation.getSmithStationPointIndex(aSmith);
			}
			return -1;
		case SmithStation.SmithStationPolish:
			return polishStation.getSmithStationPointIndex(aSmith);
		default:
			return -1;
		}
	}
}
