using System.Collections.Generic;
using UnityEngine;

public class GUIDirectionForgeController : MonoBehaviour
{
	private Game game;

	private GUIMenuForgeController menuForgeController;

	private GUIMenuBlueprintController menuBlueprintController;

	private UILabel atkStats;

	private UILabel accStats;

	private UILabel spdStats;

	private UILabel magStats;

	private UIButton atkPlus;

	private UIButton atkMinus;

	private UIButton accPlus;

	private UIButton accMinus;

	private UIButton spdPlus;

	private UIButton spdMinus;

	private UIButton magPlus;

	private UIButton magMinus;

	private UILabel statsLeft;

	private UIButton forgeButton;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		if (GameObject.Find("Panel_MenuForge") != null)
		{
			menuForgeController = GameObject.Find("Panel_MenuForge").GetComponent<GUIMenuForgeController>();
		}
		else
		{
			menuForgeController = null;
		}
		if (GameObject.Find("Panel_MenuBlueprint") != null)
		{
			menuBlueprintController = GameObject.Find("Panel_MenuBlueprint").GetComponent<GUIMenuBlueprintController>();
		}
		else
		{
			menuBlueprintController = null;
		}
		atkStats = GameObject.Find("AtkStats").GetComponent<UILabel>();
		accStats = GameObject.Find("AccStats").GetComponent<UILabel>();
		spdStats = GameObject.Find("SpdStats").GetComponent<UILabel>();
		magStats = GameObject.Find("MagStats").GetComponent<UILabel>();
		atkPlus = GameObject.Find("Atk_Plus").GetComponent<UIButton>();
		atkMinus = GameObject.Find("Atk_Minus").GetComponent<UIButton>();
		accPlus = GameObject.Find("Acc_Plus").GetComponent<UIButton>();
		accMinus = GameObject.Find("Acc_Minus").GetComponent<UIButton>();
		spdPlus = GameObject.Find("Spd_Plus").GetComponent<UIButton>();
		spdMinus = GameObject.Find("Spd_Minus").GetComponent<UIButton>();
		magPlus = GameObject.Find("Mag_Plus").GetComponent<UIButton>();
		magMinus = GameObject.Find("Mag_Minus").GetComponent<UIButton>();
		statsLeft = GameObject.Find("StatsPointsLeft").GetComponent<UILabel>();
		GameData gameData = game.getGameData();
		forgeButton = GameObject.Find("ForgeButton").GetComponent<UIButton>();
		forgeButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("forgeMenu07");
		GameObject.Find("StatsLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("forgeMenu06");
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName == "DirectionCloseButton")
		{
			GameObject.Find("ViewController").GetComponent<ViewController>().closeMenuForgeDirection();
		}
		else if (gameObjectName == "ForgeButton")
		{
			if (menuForgeController != null)
			{
				GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>().startForging();
			}
			else if (!(menuBlueprintController != null))
			{
			}
		}
		else
		{
			addPoints(gameObjectName);
		}
	}

	public void checkStats()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameObject.Find("DirectionText").GetComponent<UILabel>().text = gameData.getTextByRefId("menuForgeDirection01");
		List<int> lastDirectionBuild = player.getLastDirectionBuild();
		int remainingBuildPoints = player.getRemainingBuildPoints();
		statsLeft.text = remainingBuildPoints.ToString();
		atkStats.text = lastDirectionBuild[0].ToString();
		if (remainingBuildPoints < 1 && lastDirectionBuild[0] == 1)
		{
			atkPlus.isEnabled = false;
			atkMinus.isEnabled = false;
		}
		else if ((lastDirectionBuild[0] == 5 && remainingBuildPoints > 0) || remainingBuildPoints < 1)
		{
			atkPlus.isEnabled = false;
			atkMinus.isEnabled = true;
		}
		else if (lastDirectionBuild[0] == 1)
		{
			atkPlus.isEnabled = true;
			atkMinus.isEnabled = false;
		}
		else if (remainingBuildPoints > 0)
		{
			atkPlus.isEnabled = true;
			atkMinus.isEnabled = true;
		}
		accStats.text = lastDirectionBuild[2].ToString();
		if (remainingBuildPoints < 1 && lastDirectionBuild[2] == 1)
		{
			accPlus.isEnabled = false;
			accMinus.isEnabled = false;
		}
		else if ((lastDirectionBuild[2] == 5 && remainingBuildPoints > 0) || remainingBuildPoints < 1)
		{
			accPlus.isEnabled = false;
			accMinus.isEnabled = true;
		}
		else if (lastDirectionBuild[2] == 1)
		{
			accPlus.isEnabled = true;
			accMinus.isEnabled = false;
		}
		else if (remainingBuildPoints > 0)
		{
			accPlus.isEnabled = true;
			accMinus.isEnabled = true;
		}
		spdStats.text = lastDirectionBuild[1].ToString();
		if (remainingBuildPoints < 1 && lastDirectionBuild[1] == 1)
		{
			spdPlus.isEnabled = false;
			spdMinus.isEnabled = false;
		}
		else if ((lastDirectionBuild[1] == 5 && remainingBuildPoints > 0) || remainingBuildPoints < 1)
		{
			spdPlus.isEnabled = false;
			spdMinus.isEnabled = true;
		}
		else if (lastDirectionBuild[1] == 1)
		{
			spdPlus.isEnabled = true;
			spdMinus.isEnabled = false;
		}
		else if (remainingBuildPoints > 0)
		{
			spdPlus.isEnabled = true;
			spdMinus.isEnabled = true;
		}
		magStats.text = lastDirectionBuild[3].ToString();
		if (remainingBuildPoints < 1 && lastDirectionBuild[3] == 1)
		{
			magPlus.isEnabled = false;
			magMinus.isEnabled = false;
		}
		else if ((lastDirectionBuild[3] == 5 && remainingBuildPoints > 0) || remainingBuildPoints < 1)
		{
			magPlus.isEnabled = false;
			magMinus.isEnabled = true;
		}
		else if (lastDirectionBuild[3] == 1)
		{
			magPlus.isEnabled = true;
			magMinus.isEnabled = false;
		}
		else if (remainingBuildPoints > 0)
		{
			magPlus.isEnabled = true;
			magMinus.isEnabled = true;
		}
		if (menuForgeController != null)
		{
			Weapon weaponByRefId = gameData.getWeaponByRefId(menuForgeController.getSelectedWeaponRefID());
			Hero jobClassByRefId = gameData.getJobClassByRefId(menuForgeController.getSelectedJobRefID());
		}
		if (menuBlueprintController != null)
		{
		}
		if ((menuForgeController != null && remainingBuildPoints == 0 && menuForgeController.getSelectedWeaponRefID() != string.Empty && menuForgeController.getSelectedJobRefID() != string.Empty) || (menuBlueprintController != null && remainingBuildPoints == 0))
		{
			forgeButton.isEnabled = true;
		}
		else
		{
			forgeButton.isEnabled = false;
		}
	}

	public void addPoints(string gameObjectName)
	{
		string[] array = gameObjectName.Split('_');
		string text = array[0];
		string text2 = array[1];
		int modifyIndex = -1;
		switch (text)
		{
		case "Atk":
			modifyIndex = 0;
			break;
		case "Acc":
			modifyIndex = 2;
			break;
		case "Spd":
			modifyIndex = 1;
			break;
		case "Mag":
			modifyIndex = 3;
			break;
		}
		int addAmt = -1;
		if (text2 == "Plus")
		{
			addAmt = 1;
		}
		game.getPlayer().modifyDirectionBuild(modifyIndex, addAmt);
		checkStats();
	}
}
