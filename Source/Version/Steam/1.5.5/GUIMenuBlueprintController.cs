using System.Collections.Generic;
using UnityEngine;

public class GUIMenuBlueprintController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private UILabel jobClassName;

	private UILabel questName;

	private UILabel questDesc;

	private GameObject selectedJobclass;

	private GameObject selectedWeapon;

	private QuestNEW selectedBlueprint;

	private Weapon blueprintWeapon;

	private Hero blueprintJobclass;

	private string blueprintNamePrefix;

	private UILabel totalCostLabel;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		jobClassName = GameObject.Find("JobClassName").GetComponent<UILabel>();
		questName = GameObject.Find("QuestName").GetComponent<UILabel>();
		questDesc = GameObject.Find("QuestDesc").GetComponent<UILabel>();
		selectedJobclass = GameObject.Find("SelectedJobClass");
		selectedWeapon = GameObject.Find("SelectedWeapon");
		blueprintNamePrefix = "Blueprint_";
	}

	public void SetReference(QuestNEW aBlueprint)
	{
		GameData gameData = game.getGameData();
		game.getPlayer().setLastSelectQuest(aBlueprint);
		selectedBlueprint = aBlueprint;
		GameObject.Find("JobTitleLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("forgeMenu01");
		GameObject.Find("WeaponTitleLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("forgeMenu02");
		GameObject.Find("LegQuestLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("forgeMenu08");
		GameObject.Find("NextLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("forgeMenu09");
		GameObject gameObject = GameObject.Find("MenuForgeWeapon_Grid");
		GameObject gameObject2 = commonScreenObject.createPrefab(gameObject, blueprintNamePrefix + "weapon", "Prefab/ForgeMenu/Button_Blueprint", Vector3.zero, Vector3.one, Vector3.zero);
		blueprintWeapon = gameData.getWeaponByRefId(aBlueprint.getWeaponRefId());
		gameObject2.GetComponentInChildren<UILabel>().text = blueprintWeapon.getWeaponName();
		selectedWeapon.GetComponentInChildren<UILabel>().text = blueprintWeapon.getWeaponName();
		gameObject.GetComponent<UIGrid>().Reposition();
		gameObject.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		GameObject gameObject3 = GameObject.Find("MenuForgeJob_Grid");
		GameObject gameObject4 = commonScreenObject.createPrefab(gameObject3, blueprintNamePrefix + "job", "Prefab/ForgeMenu/Button_Blueprint", Vector3.zero, Vector3.one, Vector3.zero);
		blueprintJobclass = gameData.getJobClassByRefId(aBlueprint.getJobClassRefId());
		gameObject4.GetComponentInChildren<UILabel>().text = blueprintJobclass.getJobClassName();
		selectedJobclass.GetComponentInChildren<UILabel>().text = blueprintJobclass.getJobClassName();
		jobClassName.text = blueprintJobclass.getJobClassName();
		gameObject3.GetComponent<UIGrid>().Reposition();
		gameObject3.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		List<QuestNEW> questNEWListByType = game.getGameData().getQuestNEWListByType(QuestNEWType.QuestNEWTypeBlueprint, ignoreLock: false);
		questName.text = questNEWListByType[0].getQuestName();
		questDesc.text = questNEWListByType[0].getQuestDesc();
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName == "BlueprintCloseButton")
		{
			GameObject.Find("ViewController").GetComponent<ViewController>().closeMenuBlueprint(hide: false);
			GameObject.Find("ViewController").GetComponent<ViewController>().showMainMenu(MenuState.MenuStateForgeMain);
			GameObject.Find("ViewController").GetComponent<ViewController>().showTier2Menu(MenuState.MenuStateForgeBlueprint);
		}
		else if (gameObjectName == "NextButton")
		{
			GameObject.Find("ViewController").GetComponent<ViewController>().showMenuForgeDirection();
		}
	}

	public Weapon getBlueprintWeapon()
	{
		return blueprintWeapon;
	}

	public Hero getBlueprintJobclass()
	{
		return blueprintJobclass;
	}
}
