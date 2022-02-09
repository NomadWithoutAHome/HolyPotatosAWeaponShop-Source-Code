using System.Collections.Generic;
using UnityEngine;

public class GUIMenuForgeController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private List<GameObject> tier1JobObject;

	private List<GameObject> expandedJobObject;

	private List<GameObject> tier1WeaponObject;

	private List<GameObject> expandedWeaponObject;

	private List<GameObject> questObject;

	private List<GameObject> subquestObject;

	private float distanceBetweenButtonJob;

	private float distanceBetweenButtonWeapon;

	private float distanceBetweenButtonQuest;

	private List<Vector3> tempTier1JobPos;

	private List<Vector3> tempTier1WeaponPos;

	private List<Vector3> tempQuestPos;

	private List<Vector3> origQuestPos;

	private GameObject selectedJobClass;

	private GameObject selectedWeapon;

	private GameObject nextButton;

	private string currOpenedJob;

	private string currOpenedWeapon;

	private string currOpenedQuest;

	private GameObject currJobObject;

	private GameObject currWeaponObject;

	private string typeRefID;

	private string categoryRefID;

	private string selectedJobRefID;

	private string selectedWeaponRefID;

	private string selectedQuestRefID;

	private GameObject panel_QuestContent;

	private GameObject arrow;

	private string tempGameObjectName;

	private bool isAnimating;

	private string jobNamePrefix;

	private string weaponNamePrefix;

	private string questprefix;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		tier1JobObject = new List<GameObject>();
		expandedJobObject = new List<GameObject>();
		tier1WeaponObject = new List<GameObject>();
		expandedWeaponObject = new List<GameObject>();
		questObject = new List<GameObject>();
		subquestObject = new List<GameObject>();
		tempTier1JobPos = new List<Vector3>();
		tempTier1WeaponPos = new List<Vector3>();
		tempQuestPos = new List<Vector3>();
		origQuestPos = new List<Vector3>();
		selectedJobClass = GameObject.Find("SelectedJobClass");
		selectedWeapon = GameObject.Find("SelectedWeapon");
		nextButton = GameObject.Find("NextButton");
		currOpenedJob = string.Empty;
		currOpenedWeapon = string.Empty;
		currOpenedQuest = string.Empty;
		currJobObject = null;
		currWeaponObject = null;
		typeRefID = string.Empty;
		categoryRefID = string.Empty;
		selectedJobRefID = string.Empty;
		selectedWeaponRefID = string.Empty;
		selectedQuestRefID = string.Empty;
		panel_QuestContent = GameObject.Find("Panel_QuestContent");
		arrow = GameObject.Find("Arrow");
		isAnimating = false;
		jobNamePrefix = "Job_";
		weaponNamePrefix = "Weapon_";
		questprefix = "Quest_";
	}

	public void spawnTreeView()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		panel_QuestContent.SetActive(value: false);
		arrow.SetActive(value: false);
		GameObject.Find("JobTitleLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("forgeMenu01");
		GameObject.Find("WeaponTitleLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("forgeMenu02");
		GameObject.Find("QuestTitle").GetComponent<UILabel>().text = gameData.getTextByRefId("forgeMenu10");
		selectedJobClass.SetActive(value: false);
		selectedWeapon.SetActive(value: false);
		nextButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("forgeMenu09");
		nextButton.GetComponent<UIButton>().isEnabled = false;
		List<Hero> unlockedJobClassList = player.getUnlockedJobClassList();
		GameObject gameObject = GameObject.Find("MenuForgeJob_Grid");
		distanceBetweenButtonJob = gameObject.GetComponent<UIGrid>().cellHeight;
		if (tier1JobObject.Count < 1)
		{
			for (int i = 0; i < unlockedJobClassList.Count; i++)
			{
				if (unlockedJobClassList[i].getHeroRefId().Substring(3, 1) == "1")
				{
					GameObject gameObject2 = commonScreenObject.createPrefab(gameObject, jobNamePrefix + unlockedJobClassList[i].getHeroRefId().Substring(0, 3), "Prefab/ForgeMenu/Button_MenuForge", Vector3.zero, Vector3.one, Vector3.zero);
					commonScreenObject.findChild(gameObject2, "Text").GetComponent<UILabel>().text = unlockedJobClassList[i].getJobClassName();
					int count = game.getPlayer().getJobClassListByCategory(unlockedJobClassList[i].getHeroRefId().Substring(0, 3)).Count;
					commonScreenObject.findChild(gameObject2, "NumberFrame").GetComponent<UISprite>().enabled = true;
					commonScreenObject.findChild(gameObject2, "NumberFrame/NumberLabel").GetComponent<UILabel>().text = count.ToString();
					tier1JobObject.Add(gameObject2);
				}
			}
		}
		gameObject.GetComponent<UIGrid>().Reposition();
		List<WeaponType> unlockedWeaponTypeList = player.getUnlockedWeaponTypeList();
		GameObject gameObject3 = GameObject.Find("MenuForgeWeapon_Grid");
		distanceBetweenButtonWeapon = gameObject3.GetComponent<UIGrid>().cellHeight;
		if (tier1WeaponObject.Count < 1)
		{
			for (int j = 0; j < unlockedWeaponTypeList.Count; j++)
			{
				GameObject gameObject4 = commonScreenObject.createPrefab(gameObject3, weaponNamePrefix + unlockedWeaponTypeList[j].getWeaponTypeRefId(), "Prefab/ForgeMenu/Button_MenuForge", Vector3.zero, Vector3.one, Vector3.zero);
				commonScreenObject.findChild(gameObject4, "Text").GetComponent<UILabel>().text = unlockedWeaponTypeList[j].getWeaponTypeName();
				int count2 = player.getUnlockedWeaponListByType(unlockedWeaponTypeList[j].getWeaponTypeRefId()).Count;
				commonScreenObject.findChild(gameObject4, "NumberFrame").GetComponent<UISprite>().enabled = true;
				commonScreenObject.findChild(gameObject4, "NumberFrame/NumberLabel").GetComponent<UILabel>().text = count2.ToString();
				tier1WeaponObject.Add(gameObject4);
			}
		}
		gameObject3.GetComponent<UIGrid>().Reposition();
	}

	public void processClick(string gameObjectName)
	{
		if (isAnimating)
		{
			return;
		}
		if (gameObjectName == "forgeCloseButton")
		{
			GameObject.Find("ViewController").GetComponent<ViewController>().showMainMenu(MenuState.MenuStateForgeMain);
			return;
		}
		if (gameObjectName == "NextButton")
		{
			GameObject.Find("ViewController").GetComponent<ViewController>().showMenuForgeDirection();
			return;
		}
		string[] array = gameObjectName.Split('_');
		string text = gameObjectName.Split('_')[0];
		if (array.Length > 2)
		{
			switch (text)
			{
			case "Job":
				if (GameObject.Find("Panel_QuestContent") == null)
				{
					startAnimateQuest(gameObjectName);
				}
				else
				{
					setJobclassInfo(gameObjectName);
				}
				break;
			case "Weapon":
				GameObject.Find(gameObjectName).GetComponent<UISprite>().spriteName = "sub-active";
				setWeaponInfo(gameObjectName);
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
			case "Quest":
				expandTier1Quest(gameObjectName);
				break;
			}
		}
	}

	private void expandTier1MenuJob(string gameObjectName)
	{
		if (!(categoryRefID != gameObjectName.Split('_')[1]))
		{
			return;
		}
		categoryRefID = gameObjectName.Split('_')[1];
		List<Hero> jobClassListByCategory = game.getPlayer().getJobClassListByCategory(categoryRefID);
		if (currOpenedJob != string.Empty)
		{
			GameObject.Find(currOpenedJob).GetComponent<UISprite>().spriteName = "parent-inactive";
		}
		currOpenedJob = gameObjectName;
		GameObject.Find(gameObjectName).GetComponent<UISprite>().spriteName = "parent-active";
		if (expandedJobObject.Count > 0)
		{
			foreach (GameObject item in expandedJobObject)
			{
				commonScreenObject.destroyPrefabImmediate(item);
			}
			expandedJobObject.Clear();
		}
		int num = tier1JobObject.FindIndex((GameObject a) => a.name == gameObjectName);
		GameObject gameObject = GameObject.Find("MenuForgeJob_Grid");
		for (int i = 0; i < jobClassListByCategory.Count; i++)
		{
			GameObject gameObject2 = commonScreenObject.createPrefab(gameObject, gameObjectName + "_" + jobClassListByCategory[i].getHeroRefId(), "Prefab/ForgeMenu/Button_MenuForge", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(gameObject2, "Text").GetComponent<UILabel>().text = jobClassListByCategory[i].getJobClassName();
			if (selectedJobRefID == jobClassListByCategory[i].getHeroRefId())
			{
				gameObject2.GetComponent<UISprite>().spriteName = "sub-active";
			}
			else
			{
				gameObject2.GetComponent<UISprite>().spriteName = "sub-inactive";
			}
			gameObject2.GetComponent<UISprite>().depth = 0;
			commonScreenObject.findChild(gameObject2, "Text").GetComponent<UILabel>().depth = 1;
			if (i == 0)
			{
				if (GameObject.Find("Panel_QuestContent") == null)
				{
					startAnimateQuest(gameObject2.name);
				}
				else
				{
					setJobclassInfo(gameObject2.name);
				}
			}
			expandedJobObject.Add(gameObject2);
		}
		if (game.getGameData().getJobClassListByCategoryCount(categoryRefID) != jobClassListByCategory.Count)
		{
			GameObject gameObject3 = commonScreenObject.createPrefab(gameObject, gameObjectName + "_99999", "Prefab/ForgeMenu/Button_MenuForge", Vector3.zero, Vector3.one, Vector3.zero);
			Object.DestroyImmediate(gameObject3.GetComponent<MenuforgeClickScript>());
			commonScreenObject.findChild(gameObject3, "Text").GetComponent<UILabel>().text = "?????";
			gameObject3.GetComponent<UISprite>().spriteName = "sub-inactive";
			gameObject3.GetComponent<UISprite>().depth = 0;
			commonScreenObject.findChild(gameObject3, "Text").GetComponent<UILabel>().depth = 1;
			expandedJobObject.Add(gameObject3);
		}
		tempTier1JobPos.Clear();
		for (int j = 0; j < tier1JobObject.Count; j++)
		{
			tempTier1JobPos.Add(GameObject.Find(tier1JobObject[j].name).transform.localPosition);
		}
		gameObject.GetComponent<UIGrid>().Reposition();
		gameObject.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		if (num != tier1JobObject.Count - 1)
		{
			for (int num2 = num; num2 > 0; num2--)
			{
				GameObject gameObject4 = tier1JobObject[num2];
				commonScreenObject.tweenPosition(gameObject4.GetComponent<TweenPosition>(), tempTier1JobPos[num2], new Vector3(0f, (float)(-num2) * distanceBetweenButtonJob, 0f), 0.5f, null, string.Empty);
			}
			for (int k = num + 1; k < tier1JobObject.Count; k++)
			{
				GameObject gameObject5 = tier1JobObject[k];
				commonScreenObject.tweenPosition(gameObject5.GetComponent<TweenPosition>(), tempTier1JobPos[k], GameObject.Find(gameObject5.name).transform.localPosition, 0.5f, null, string.Empty);
			}
		}
		else
		{
			commonScreenObject.tweenPosition(tier1JobObject[num].GetComponent<TweenPosition>(), tempTier1JobPos[num], GameObject.Find(tier1JobObject[num].name).transform.localPosition, 0.5f, null, string.Empty);
		}
		Vector3 localPosition = GameObject.Find(gameObjectName).transform.localPosition;
		foreach (GameObject item2 in expandedJobObject)
		{
			commonScreenObject.tweenPosition(item2.GetComponent<TweenPosition>(), localPosition, item2.transform.localPosition, 0.5f, null, string.Empty);
		}
	}

	private void expandTier1MenuWeapon(string gameObjectName)
	{
		if (!(typeRefID != gameObjectName.Split('_')[1]))
		{
			return;
		}
		typeRefID = gameObjectName.Split('_')[1];
		List<Weapon> unlockedWeaponListByType = game.getPlayer().getUnlockedWeaponListByType(typeRefID);
		if (currOpenedWeapon != string.Empty)
		{
			GameObject.Find(currOpenedWeapon).GetComponent<UISprite>().spriteName = "parent-inactive";
		}
		currOpenedWeapon = gameObjectName;
		GameObject.Find(gameObjectName).GetComponent<UISprite>().spriteName = "parent-active";
		if (expandedWeaponObject.Count > 0)
		{
			foreach (GameObject item in expandedWeaponObject)
			{
				commonScreenObject.destroyPrefabImmediate(item);
			}
			expandedWeaponObject.Clear();
		}
		int num = tier1WeaponObject.FindIndex((GameObject a) => a.name == gameObjectName);
		GameObject gameObject = GameObject.Find("MenuForgeWeapon_Grid");
		for (int i = 0; i < unlockedWeaponListByType.Count; i++)
		{
			GameObject gameObject2 = commonScreenObject.createPrefab(gameObject, gameObjectName + "_" + unlockedWeaponListByType[i].getWeaponRefId(), "Prefab/ForgeMenu/Button_MenuForge", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(gameObject2, "Text").GetComponent<UILabel>().text = unlockedWeaponListByType[i].getWeaponName();
			if (selectedWeaponRefID == unlockedWeaponListByType[i].getWeaponRefId())
			{
				currWeaponObject = gameObject2;
				gameObject2.GetComponent<UISprite>().spriteName = "sub-active";
			}
			else
			{
				gameObject2.GetComponent<UISprite>().spriteName = "sub-inactive";
			}
			gameObject2.GetComponent<UISprite>().depth = 0;
			commonScreenObject.findChild(gameObject2, "Text").GetComponent<UILabel>().depth = 1;
			expandedWeaponObject.Add(gameObject2);
		}
		if (game.getGameData().getJobClassListByCategoryCount(typeRefID) != unlockedWeaponListByType.Count)
		{
			GameObject gameObject3 = commonScreenObject.createPrefab(gameObject, gameObjectName + "_99999", "Prefab/ForgeMenu/Button_MenuForge", Vector3.zero, Vector3.one, Vector3.zero);
			Object.DestroyImmediate(gameObject3.GetComponent<MenuforgeClickScript>());
			commonScreenObject.findChild(gameObject3, "Text").GetComponent<UILabel>().text = "?????";
			gameObject3.GetComponent<UISprite>().spriteName = "sub-inactive";
			gameObject3.GetComponent<UISprite>().depth = 0;
			commonScreenObject.findChild(gameObject3, "Text").GetComponent<UILabel>().depth = 1;
			expandedWeaponObject.Add(gameObject3);
		}
		tempTier1WeaponPos.Clear();
		for (int j = 0; j < tier1WeaponObject.Count; j++)
		{
			tempTier1WeaponPos.Add(GameObject.Find(tier1WeaponObject[j].name).transform.localPosition);
		}
		gameObject.GetComponent<UIGrid>().Reposition();
		gameObject.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		if (num != tier1WeaponObject.Count - 1)
		{
			for (int num2 = num; num2 > 0; num2--)
			{
				GameObject gameObject4 = tier1WeaponObject[num2];
				commonScreenObject.tweenPosition(gameObject4.GetComponent<TweenPosition>(), tempTier1WeaponPos[num2], new Vector3(0f, (float)(-num2) * distanceBetweenButtonJob, 0f), 0.5f, null, string.Empty);
			}
			for (int k = num + 1; k < tier1WeaponObject.Count; k++)
			{
				GameObject gameObject5 = tier1WeaponObject[k];
				commonScreenObject.tweenPosition(gameObject5.GetComponent<TweenPosition>(), tempTier1WeaponPos[k], GameObject.Find(gameObject5.name).transform.localPosition, 0.5f, null, string.Empty);
			}
		}
		else
		{
			commonScreenObject.tweenPosition(tier1WeaponObject[num].GetComponent<TweenPosition>(), tempTier1WeaponPos[num], GameObject.Find(tier1WeaponObject[num].name).transform.localPosition, 0.5f, null, string.Empty);
		}
		Vector3 localPosition = GameObject.Find(gameObjectName).transform.localPosition;
		foreach (GameObject item2 in expandedWeaponObject)
		{
			commonScreenObject.tweenPosition(item2.GetComponent<TweenPosition>(), localPosition, item2.transform.localPosition, 0.5f, null, string.Empty);
		}
	}

	private void expandTier1Quest(string gameObjectName)
	{
	}

	public void clearSubquestPos()
	{
		foreach (GameObject item in subquestObject)
		{
			commonScreenObject.destroyPrefabImmediate(item);
		}
		subquestObject.Clear();
	}

	private void setJobclassInfo(string gameObjectName)
	{
		selectedQuestRefID = string.Empty;
		currOpenedQuest = string.Empty;
		if (currJobObject != null && currJobObject.name != gameObjectName)
		{
			currJobObject.GetComponent<UISprite>().spriteName = "sub-inactive";
		}
		currJobObject = GameObject.Find(gameObjectName);
		currJobObject.GetComponent<UISprite>().spriteName = "sub-active";
		selectedJobRefID = gameObjectName.Split('_')[2];
		Hero jobClassByRefId = game.getGameData().getJobClassByRefId(selectedJobRefID);
		game.getPlayer().setLastSelectHero(jobClassByRefId);
		selectedJobClass.SetActive(value: true);
		selectedJobClass.GetComponentInChildren<UILabel>().text = jobClassByRefId.getJobClassName();
		GameObject gameObject = GameObject.Find("Quest_Grid");
		distanceBetweenButtonQuest = gameObject.GetComponent<UIGrid>().cellHeight;
		if (questObject.Count > 0)
		{
			foreach (GameObject item in questObject)
			{
				commonScreenObject.destroyPrefabImmediate(item);
			}
			questObject.Clear();
		}
		if (subquestObject.Count <= 0)
		{
			return;
		}
		foreach (GameObject item2 in subquestObject)
		{
			commonScreenObject.destroyPrefabImmediate(item2);
		}
		subquestObject.Clear();
	}

	private void setWeaponInfo(string gameObjectName)
	{
		Player player = game.getPlayer();
		if (currWeaponObject != null && currWeaponObject.name != gameObjectName)
		{
			currWeaponObject.GetComponent<UISprite>().spriteName = "sub-inactive";
		}
		currWeaponObject = GameObject.Find(gameObjectName);
		currWeaponObject.GetComponent<UISprite>().spriteName = "sub-active";
		selectedWeaponRefID = gameObjectName.Split('_')[2];
		Weapon weaponByRefId = game.getGameData().getWeaponByRefId(selectedWeaponRefID);
		player.setLastSelectWeapon(weaponByRefId);
		player.setLastSelectWeaponType(weaponByRefId.getWeaponType());
		selectedWeapon.SetActive(value: true);
		selectedWeapon.GetComponentInChildren<UILabel>().text = weaponByRefId.getWeaponName();
		if (selectedJobRefID != string.Empty && GameObject.Find("Arrow") == null)
		{
			startAnimateLine();
		}
	}

	private void startAnimateQuest(string gameObjectName)
	{
		tempGameObjectName = gameObjectName;
		isAnimating = true;
		GameObject.Find("QuestHeaderTop").GetComponent<TweenPosition>().ResetToBeginning();
		GameObject.Find("QuestHeaderBottom").GetComponent<TweenPosition>().ResetToBeginning();
		GameObject.Find("QuestHeaderTop").GetComponent<TweenPosition>().PlayForward();
		GameObject.Find("QuestHeaderBottom").GetComponent<TweenPosition>().PlayForward();
	}

	public void endAnimateQuest()
	{
		isAnimating = false;
		panel_QuestContent.SetActive(value: true);
		setJobclassInfo(tempGameObjectName);
	}

	private void startAnimateLine()
	{
		isAnimating = true;
		GameObject.Find("Line").GetComponent<TweenScale>().ResetToBeginning();
		GameObject.Find("Line").GetComponent<TweenScale>().PlayForward();
	}

	public void endAnimateLine()
	{
		isAnimating = false;
		arrow.SetActive(value: true);
		nextButton.GetComponent<UIButton>().isEnabled = true;
	}

	public void startForging()
	{
		GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>().startForging();
	}

	public string getSelectedJobRefID()
	{
		return selectedJobRefID;
	}

	public string getSelectedWeaponRefID()
	{
		return selectedWeaponRefID;
	}
}
