using System.Collections.Generic;
using UnityEngine;

public class GUIForgeCategoryController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private GUIForgeMenuNewController forgeMenuNewController;

	private List<GameObject> tier1ObjectList;

	private string categoryRefID;

	private GameObject currOpenedCatObj;

	private string objectRefID;

	private GameObject selectedCatObj;

	private List<GameObject> horizontalGridList;

	private List<GameObject> catObjectList;

	private string type;

	private UISprite categoryBg;

	private UIGrid categoryGrid;

	private UILabel categoryLabel;

	private UISprite categoryExpandBg;

	private UIGrid categoryObject_Grid;

	private string jobNamePrefix;

	private string weaponNamePrefix;

	private float openedButtonX;

	private string horizontalGridPrefix;

	private Color transparentColor;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		forgeMenuNewController = base.transform.parent.GetComponent<GUIForgeMenuNewController>();
		tier1ObjectList = new List<GameObject>();
		horizontalGridList = new List<GameObject>();
		catObjectList = new List<GameObject>();
		type = string.Empty;
		categoryBg = commonScreenObject.findChild(base.gameObject, "CategoryBg").GetComponent<UISprite>();
		categoryGrid = commonScreenObject.findChild(categoryBg.gameObject, "CategoryGrid").GetComponent<UIGrid>();
		categoryLabel = commonScreenObject.findChild(categoryBg.gameObject, "CategoryLabel").GetComponent<UILabel>();
		categoryExpandBg = commonScreenObject.findChild(base.gameObject, "CategoryExpandBg").GetComponent<UISprite>();
		categoryObject_Grid = commonScreenObject.findChild(base.gameObject, "Panel_CategoryObject/CategoryObject_Grid").GetComponent<UIGrid>();
		jobNamePrefix = "Job_";
		weaponNamePrefix = "Weapon_";
		openedButtonX = 17f;
		horizontalGridPrefix = "HoriGrid_";
		transparentColor = Color.white;
		transparentColor.a = 0f;
		hide();
	}

	public void processClick(string gameObjectName)
	{
		if (forgeMenuNewController.getIsAnimating())
		{
			return;
		}
		if (gameObjectName != null && gameObjectName == "SelectButton")
		{
			forgeMenuNewController.animateClose();
			return;
		}
		string[] array = gameObjectName.Split('_');
		string text = gameObjectName.Split('_')[0];
		if (array.Length > 2)
		{
			switch (text)
			{
			case "Job":
				selectJob(gameObjectName);
				break;
			case "Weapon":
				selectWeapon(gameObjectName);
				break;
			}
		}
		else
		{
			switch (text)
			{
			case "Job":
				expandTier1MenuJob(gameObjectName);
				break;
			case "Weapon":
				expandTier1MenuWeapon(gameObjectName);
				break;
			}
		}
	}

	public void hide()
	{
		categoryRefID = string.Empty;
		currOpenedCatObj = null;
		objectRefID = string.Empty;
		selectedCatObj = null;
		categoryLabel.text = string.Empty;
		type = string.Empty;
		categoryBg.enabled = false;
		categoryExpandBg.enabled = false;
		if (tier1ObjectList.Count > 0)
		{
			foreach (GameObject tier1Object in tier1ObjectList)
			{
				commonScreenObject.destroyPrefabImmediate(tier1Object);
			}
			tier1ObjectList.Clear();
		}
		if (horizontalGridList.Count <= 0)
		{
			return;
		}
		foreach (GameObject horizontalGrid in horizontalGridList)
		{
			commonScreenObject.destroyPrefabImmediate(horizontalGrid);
		}
		horizontalGridList.Clear();
	}

	public void setReference(string aType)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		categoryBg.enabled = true;
		categoryExpandBg.enabled = true;
		categoryLabel.text = gameData.getTextByRefId("questSelect13");
		type = aType;
		switch (aType)
		{
		case "Weapon":
		{
			List<WeaponType> unlockedWeaponTypeList = player.getUnlockedWeaponTypeList();
			if (tier1ObjectList.Count < 1)
			{
				for (int j = 0; j < unlockedWeaponTypeList.Count; j++)
				{
					GameObject gameObject2 = commonScreenObject.createPrefab(categoryGrid.gameObject, weaponNamePrefix + unlockedWeaponTypeList[j].getWeaponTypeRefId(), "Prefab/ForgeMenu/ForgeMenuNEW/CategoryButton", Vector3.zero, Vector3.one, Vector3.zero);
					commonScreenObject.findChild(gameObject2, "CategoryLabel").GetComponent<UILabel>().text = unlockedWeaponTypeList[j].getWeaponTypeName();
					tier1ObjectList.Add(gameObject2);
				}
			}
			break;
		}
		case "JobClass":
		{
			List<Hero> unlockedJobClassList = player.getUnlockedJobClassList();
			if (tier1ObjectList.Count >= 1)
			{
				break;
			}
			for (int i = 0; i < unlockedJobClassList.Count; i++)
			{
				if (unlockedJobClassList[i].getHeroRefId().Substring(3, 1) == "1")
				{
					GameObject gameObject = commonScreenObject.createPrefab(categoryGrid.gameObject, jobNamePrefix + unlockedJobClassList[i].getHeroRefId().Substring(0, 3), "Prefab/ForgeMenu/ForgeMenuNEW/CategoryButton", Vector3.zero, Vector3.one, Vector3.zero);
					commonScreenObject.findChild(gameObject, "CategoryLabel").GetComponent<UILabel>().text = unlockedJobClassList[i].getJobClassName();
					tier1ObjectList.Add(gameObject);
				}
			}
			break;
		}
		}
		categoryGrid.GetComponent<UIGrid>().Reposition();
	}

	private void expandTier1MenuJob(string gameObjectName)
	{
		if (!(categoryRefID != gameObjectName.Split('_')[1]))
		{
			return;
		}
		categoryRefID = gameObjectName.Split('_')[1];
		if (currOpenedCatObj != null)
		{
			Vector3 localPosition = currOpenedCatObj.transform.localPosition;
			Vector3 aEndPosition = localPosition;
			aEndPosition.x -= openedButtonX;
			currOpenedCatObj.GetComponent<UISprite>().spriteName = "bt_category";
			commonScreenObject.tweenPosition(currOpenedCatObj.GetComponent<TweenPosition>(), localPosition, aEndPosition, 0.1f, null, string.Empty);
		}
		currOpenedCatObj = GameObject.Find(gameObjectName);
		Vector3 localPosition2 = currOpenedCatObj.transform.localPosition;
		Vector3 aEndPosition2 = localPosition2;
		aEndPosition2.x += openedButtonX;
		currOpenedCatObj.GetComponent<UISprite>().spriteName = "bt_selectedcategory";
		commonScreenObject.tweenPosition(currOpenedCatObj.GetComponent<TweenPosition>(), localPosition2, aEndPosition2, 0.1f, null, string.Empty);
		if (catObjectList.Count > 0)
		{
			foreach (GameObject catObject in catObjectList)
			{
				commonScreenObject.destroyPrefabImmediate(catObject);
			}
			catObjectList.Clear();
		}
		List<Hero> jobClassListByCategory = game.getPlayer().getJobClassListByCategory(categoryRefID);
		int num = 0;
		GameObject gameObject = commonScreenObject.createPrefab(categoryObject_Grid.gameObject, horizontalGridPrefix + num, "Prefab/ForgeMenu/ForgeMenuNEW/CategoryGridHorizontal", Vector3.zero, Vector3.one, Vector3.zero);
		horizontalGridList.Add(gameObject);
		for (int i = 0; i < jobClassListByCategory.Count; i++)
		{
			GameObject gameObject2 = commonScreenObject.createPrefab(gameObject, gameObjectName + "_" + jobClassListByCategory[i].getHeroRefId(), "Prefab/ForgeMenu/ForgeMenuNEW/ForgeObject", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(gameObject2, "ObjectButton/ObjectImg").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Hero/" + jobClassListByCategory[i].getImage() + "_quest");
			commonScreenObject.findChild(gameObject2, "ObjectButton/ObjectPrice").GetComponent<UILabel>().text = CommonAPI.formatNumber(0);
			if (forgeMenuNewController.getSelectedJobClassRefID() == jobClassListByCategory[i].getHeroRefId())
			{
				commonScreenObject.findChild(gameObject2, "ObjectButton").GetComponent<UIButton>().isEnabled = false;
				commonScreenObject.findChild(gameObject2, "SelectButton").GetComponent<UISprite>().color = Color.white;
			}
			else
			{
				CommonAPI.debug("here");
				commonScreenObject.findChild(gameObject2, "SelectButton").GetComponent<UISprite>().color = transparentColor;
			}
			catObjectList.Add(gameObject2);
			if ((i + 1) % 4 == 0 && i + 1 != jobClassListByCategory.Count)
			{
				gameObject.GetComponent<UIGrid>().Reposition();
				num++;
				gameObject = commonScreenObject.createPrefab(categoryObject_Grid.gameObject, horizontalGridPrefix + num, "Prefab/ForgeMenu/ForgeMenuNEW/CategoryGridHorizontal", Vector3.zero, Vector3.one, Vector3.zero);
				horizontalGridList.Add(gameObject);
			}
		}
		gameObject.GetComponent<UIGrid>().Reposition();
		categoryObject_Grid.Reposition();
	}

	private void expandTier1MenuWeapon(string gameObjectName)
	{
		if (!(categoryRefID != gameObjectName.Split('_')[1]))
		{
			return;
		}
		categoryRefID = gameObjectName.Split('_')[1];
		if (currOpenedCatObj != null)
		{
			Vector3 localPosition = currOpenedCatObj.transform.localPosition;
			Vector3 aEndPosition = localPosition;
			aEndPosition.x -= openedButtonX;
			currOpenedCatObj.GetComponent<UISprite>().spriteName = "bt_category";
			commonScreenObject.tweenPosition(currOpenedCatObj.GetComponent<TweenPosition>(), localPosition, aEndPosition, 0.1f, null, string.Empty);
		}
		currOpenedCatObj = GameObject.Find(gameObjectName);
		Vector3 localPosition2 = currOpenedCatObj.transform.localPosition;
		Vector3 aEndPosition2 = localPosition2;
		aEndPosition2.x += openedButtonX;
		currOpenedCatObj.GetComponent<UISprite>().spriteName = "bt_selectedcategory";
		commonScreenObject.tweenPosition(currOpenedCatObj.GetComponent<TweenPosition>(), localPosition2, aEndPosition2, 0.1f, null, string.Empty);
		if (catObjectList.Count > 0)
		{
			foreach (GameObject catObject in catObjectList)
			{
				commonScreenObject.destroyPrefabImmediate(catObject);
			}
			catObjectList.Clear();
		}
		List<Weapon> unlockedWeaponListByType = game.getPlayer().getUnlockedWeaponListByType(categoryRefID);
		int num = 0;
		GameObject gameObject = commonScreenObject.createPrefab(categoryObject_Grid.gameObject, horizontalGridPrefix + num, "Prefab/ForgeMenu/ForgeMenuNEW/CategoryGridHorizontal", Vector3.zero, Vector3.one, Vector3.zero);
		horizontalGridList.Add(gameObject);
		CommonAPI.debug("weapon count: " + unlockedWeaponListByType.Count);
		for (int i = 0; i < unlockedWeaponListByType.Count; i++)
		{
			CommonAPI.debug("weapon name: " + unlockedWeaponListByType[i].getWeaponName());
			GameObject gameObject2 = commonScreenObject.createPrefab(gameObject, gameObjectName + "_" + unlockedWeaponListByType[i].getWeaponRefId(), "Prefab/ForgeMenu/ForgeMenuNEW/ForgeObject", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(gameObject2, "ObjectButton/ObjectImg").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + unlockedWeaponListByType[i].getImage());
			if (forgeMenuNewController.getSelectedWeaponRefID() == unlockedWeaponListByType[i].getWeaponRefId())
			{
				commonScreenObject.findChild(gameObject2, "ObjectButton").GetComponent<UIButton>().isEnabled = false;
				commonScreenObject.findChild(gameObject2, "SelectButton").GetComponent<UISprite>().color = Color.white;
			}
			else
			{
				commonScreenObject.findChild(gameObject2, "SelectButton").GetComponent<UISprite>().color = transparentColor;
			}
			catObjectList.Add(gameObject2);
			if ((i + 1) % 4 == 0 && i + 1 != unlockedWeaponListByType.Count)
			{
				gameObject.GetComponent<UIGrid>().Reposition();
				num++;
				gameObject = commonScreenObject.createPrefab(categoryObject_Grid.gameObject, horizontalGridPrefix + num, "Prefab/ForgeMenu/ForgeMenuNEW/CategoryGridHorizontal", Vector3.zero, Vector3.one, Vector3.zero);
				horizontalGridList.Add(gameObject);
			}
		}
		gameObject.GetComponent<UIGrid>().Reposition();
		categoryObject_Grid.Reposition();
	}

	private void selectJob(string gameObjectName)
	{
		if (objectRefID != gameObjectName.Split('_')[2])
		{
			objectRefID = gameObjectName.Split('_')[2];
			if (selectedCatObj != null)
			{
				commonScreenObject.findChild(selectedCatObj, "ObjectButton").GetComponent<UIButton>().isEnabled = true;
				commonScreenObject.findChild(selectedCatObj, "SelectButton").GetComponent<UISprite>().color = transparentColor;
			}
			selectedCatObj = GameObject.Find(gameObjectName);
			commonScreenObject.findChild(selectedCatObj, "ObjectButton").GetComponent<UIButton>().isEnabled = false;
			commonScreenObject.findChild(selectedCatObj, "SelectButton").GetComponent<UISprite>().color = Color.white;
			forgeMenuNewController.loadJobClassDetail(objectRefID);
		}
	}

	private void selectWeapon(string gameObjectName)
	{
		if (objectRefID != gameObjectName.Split('_')[2])
		{
			objectRefID = gameObjectName.Split('_')[2];
			if (selectedCatObj != null)
			{
				commonScreenObject.findChild(selectedCatObj, "ObjectButton").GetComponent<UIButton>().isEnabled = true;
				commonScreenObject.findChild(selectedCatObj, "SelectButton").GetComponent<UISprite>().color = transparentColor;
			}
			selectedCatObj = GameObject.Find(gameObjectName);
			commonScreenObject.findChild(selectedCatObj, "ObjectButton").GetComponent<UIButton>().isEnabled = false;
			commonScreenObject.findChild(selectedCatObj, "SelectButton").GetComponent<UISprite>().color = Color.white;
			forgeMenuNewController.loadWeaponDetail(objectRefID);
		}
	}
}
