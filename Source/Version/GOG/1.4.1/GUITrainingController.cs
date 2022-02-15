using System.Collections.Generic;
using UnityEngine;

public class GUITrainingController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private AudioController audioController;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private Smith trainSmith;

	private SmithTraining selectedTraining;

	private string selectedTrainingObjName;

	private UILabel smithNameLabel;

	private UITexture smithImage;

	private UIProgressBar smithJobBar;

	private UIProgressBar smithJobBarLower;

	private UILabel smithJobName;

	private UILabel smithJobLevel;

	private UIProgressBar smithStaminaBar;

	private UIProgressBar smithStaminaBarLower;

	private UILabel smithMood;

	private UILabel smithPow;

	private UILabel smithInt;

	private UILabel smithTec;

	private UILabel smithLuc;

	private UISprite trainingListBg;

	private UIGrid trainingListGrid;

	private UIScrollBar trainingListScrollBar;

	private UILabel playerGold;

	private UIButton trainSmithButton;

	private UIButton closeButton;

	private int levelBefore;

	private int levelAfter;

	private int atkBefore;

	private int spdBefore;

	private int accBefore;

	private int magBefore;

	private int atkAfter;

	private int spdAfter;

	private int accAfter;

	private int magAfter;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		selectedTrainingObjName = string.Empty;
		smithNameLabel = commonScreenObject.findChild(base.gameObject, "SmithStats/SmithName_bg/SmithName_label").GetComponent<UILabel>();
		smithImage = commonScreenObject.findChild(base.gameObject, "SmithStats/SmithStats_smithBg/SmithImg").GetComponent<UITexture>();
		smithJobBar = commonScreenObject.findChild(base.gameObject, "SmithStats/SmithJob_bar").GetComponent<UIProgressBar>();
		smithJobBarLower = commonScreenObject.findChild(base.gameObject, "SmithStats/SmithJob_barLower").GetComponent<UIProgressBar>();
		smithJobName = commonScreenObject.findChild(base.gameObject, "SmithStats/SmithJob_bar/SmithJob_name").GetComponent<UILabel>();
		smithJobLevel = commonScreenObject.findChild(base.gameObject, "SmithStats/SmithJob_bar/SmithLevel_star/SmithLevel_value").GetComponent<UILabel>();
		smithStaminaBar = commonScreenObject.findChild(base.gameObject, "SmithStats/SmithStamina_bar").GetComponent<UIProgressBar>();
		smithStaminaBarLower = commonScreenObject.findChild(base.gameObject, "SmithStats/SmithStamina_barLower").GetComponent<UIProgressBar>();
		smithMood = commonScreenObject.findChild(base.gameObject, "SmithStats/SmithMood_bg/SmithMood_label").GetComponent<UILabel>();
		smithPow = commonScreenObject.findChild(base.gameObject, "SmithStats/SmithStats_stats/StatAtk_bg/StatAtk_value").GetComponent<UILabel>();
		smithInt = commonScreenObject.findChild(base.gameObject, "SmithStats/SmithStats_stats/StatSpd_bg/StatSpd_value").GetComponent<UILabel>();
		smithTec = commonScreenObject.findChild(base.gameObject, "SmithStats/SmithStats_stats/StatAcc_bg/StatAcc_value").GetComponent<UILabel>();
		smithLuc = commonScreenObject.findChild(base.gameObject, "SmithStats/SmithStats_stats/StatMag_bg/StatMag_value").GetComponent<UILabel>();
		trainingListBg = commonScreenObject.findChild(base.gameObject, "TrainingList_bg").GetComponent<UISprite>();
		trainingListGrid = commonScreenObject.findChild(base.gameObject, "TrainingList_bg/TrainingList_scrollList/TrainingList_grid").GetComponent<UIGrid>();
		trainingListScrollBar = commonScreenObject.findChild(base.gameObject, "TrainingList_bg/TrainingList_scrollbar").GetComponent<UIScrollBar>();
		playerGold = commonScreenObject.findChild(base.gameObject, "PlayerGold_bg/PlayerGold_label").GetComponent<UILabel>();
		trainSmithButton = commonScreenObject.findChild(base.gameObject, "TrainSmith_button").GetComponent<UIButton>();
		closeButton = commonScreenObject.findChild(base.gameObject, "CloseButton").GetComponent<UIButton>();
		closeButton.isEnabled = true;
		trainSmithButton.isEnabled = false;
		levelBefore = 0;
		levelAfter = 0;
		atkBefore = 0;
		spdBefore = 0;
		accBefore = 0;
		magBefore = 0;
		atkAfter = 0;
		spdAfter = 0;
		accAfter = 0;
		magAfter = 0;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "CloseButton":
			audioController.playButtonAudio();
			viewController.closeSmithTrainingPopup(hide: true);
			viewController.resumeEverything();
			return;
		case "TrainSmith_button":
			audioController.playButtonAudio();
			startTraining();
			return;
		}
		string[] array = gameObjectName.Split('_');
		if (array[0] == "trainingType")
		{
			updateSelection(gameObjectName, CommonAPI.parseInt(array[1]));
		}
	}

	private void startTraining()
	{
		levelBefore = trainSmith.getSmithLevel();
		atkBefore = trainSmith.getSmithPower();
		spdBefore = trainSmith.getSmithIntelligence();
		accBefore = trainSmith.getSmithTechnique();
		magBefore = trainSmith.getSmithLuck();
		shopMenuController.doSmithTraining(trainSmith, selectedTraining);
		foreach (Transform child in trainingListGrid.GetChildList())
		{
			commonScreenObject.destroyPrefabImmediate(child.gameObject);
		}
		trainSmithButton.isEnabled = false;
		commonScreenObject.tweenColor(trainingListBg.GetComponent<TweenColor>(), Color.white, Color.grey, 0.3f, null, string.Empty);
		GameObject aObject = commonScreenObject.createPrefab(base.gameObject, "TrainingAnim", "Prefab/SmithManage/TrainingAnim", trainingListBg.transform.localPosition, Vector3.one, Vector3.zero);
		UITexture component = commonScreenObject.findChild(aObject, "SmithImg").GetComponent<UITexture>();
		UITexture component2 = commonScreenObject.findChild(aObject, "SmithImg/SmithImg_white").GetComponent<UITexture>();
		component.mainTexture = smithImage.mainTexture;
		component2.mainTexture = smithImage.mainTexture;
		TweenColor component3 = commonScreenObject.findChild(aObject, "SmithImg/SmithImg_white").GetComponent<TweenColor>();
		component3.eventReceiver = base.gameObject;
		component3.callWhenFinished = "endTraining";
		closeButton.isEnabled = false;
	}

	private void endTraining()
	{
		levelAfter = trainSmith.getSmithLevel();
		atkAfter = trainSmith.getSmithPower();
		spdAfter = trainSmith.getSmithIntelligence();
		accAfter = trainSmith.getSmithTechnique();
		magAfter = trainSmith.getSmithLuck();
		commonScreenObject.destroyPrefabImmediate(GameObject.Find("TrainingAnim"));
		commonScreenObject.tweenColor(trainingListBg.GetComponent<TweenColor>(), Color.grey, Color.white, 0.3f, null, string.Empty);
		updateSelection(string.Empty, -1);
		if (levelAfter > levelBefore)
		{
			popLvlUp();
		}
		if (atkAfter > atkBefore)
		{
			popStatUp(smithPow.transform.parent.gameObject, atkAfter - atkBefore);
		}
		if (spdAfter > spdBefore)
		{
			popStatUp(smithInt.transform.parent.gameObject, spdAfter - spdBefore);
		}
		if (accAfter > accBefore)
		{
			popStatUp(smithTec.transform.parent.gameObject, accAfter - accBefore);
		}
		if (magAfter > magBefore)
		{
			popStatUp(smithLuc.transform.parent.gameObject, magAfter - magBefore);
		}
		closeButton.isEnabled = true;
	}

	private void updateSelection(string aName, int index)
	{
		bool flag = false;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		if (index >= 0)
		{
			List<SmithTraining> allowedTrainingList = gameData.getAllowedTrainingList(player.getPlayerDays(), player.getShopLevelInt());
			SmithTraining smithTraining = allowedTrainingList[index];
			if (smithTraining.getSmithTrainingCost() > player.getPlayerGold())
			{
				return;
			}
			audioController.playButtonAudio();
			selectedTraining = smithTraining;
			selectedTrainingObjName = aName;
			for (int i = 0; i < trainingListGrid.transform.childCount; i++)
			{
				GameObject gameObject = trainingListGrid.transform.GetChild(i).gameObject;
				if (gameObject.name == selectedTrainingObjName)
				{
					commonScreenObject.findChild(gameObject, "TrainingListObj_bg").GetComponent<UISprite>().alpha = 1f;
					flag = true;
				}
				else if (commonScreenObject.findChild(gameObject, "TrainingListObj_bg").GetComponent<UISprite>().color != Color.grey)
				{
					commonScreenObject.findChild(gameObject, "TrainingListObj_bg").GetComponent<UISprite>().alpha = 0f;
				}
			}
			int num = player.getPlayerGold();
			int aNumber = num - selectedTraining.getSmithTrainingCost();
			playerGold.text = "[000000]$" + CommonAPI.formatNumber(num) + "[-] [FF4842]> $" + CommonAPI.formatNumber(aNumber) + "[-]";
			float remainingMood = trainSmith.getRemainingMood();
			float num2 = remainingMood - (float)selectedTraining.getSmithTrainingStamina();
			float smithMaxMood = trainSmith.getSmithMaxMood();
			smithStaminaBarLower.value = remainingMood / smithMaxMood;
			smithStaminaBar.value = num2 / smithMaxMood;
			int smithExp = trainSmith.getSmithExp();
			int num3 = smithExp + selectedTraining.getSmithTrainingExp();
			int maxExp = trainSmith.getMaxExp();
			smithJobBar.value = (float)smithExp / (float)maxExp;
			smithJobBarLower.value = (float)num3 / (float)maxExp;
			if (flag)
			{
				trainSmithButton.isEnabled = true;
			}
			else
			{
				trainSmithButton.isEnabled = false;
			}
		}
		else
		{
			selectedTraining = null;
			selectedTrainingObjName = string.Empty;
			setReference(trainSmith);
			trainSmithButton.isEnabled = false;
		}
	}

	public void setReference(Smith aSmith)
	{
		if (shopMenuController == null)
		{
			shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		}
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		trainSmith = aSmith;
		playerGold.text = "[000000]$" + CommonAPI.formatNumber(player.getPlayerGold()) + "[-]";
		smithNameLabel.text = trainSmith.getSmithName();
		smithImage.mainTexture = commonScreenObject.loadTexture("Image/Smith/" + trainSmith.getImage() + "_manage");
		smithJobBar.value = (float)trainSmith.getSmithExp() / (float)trainSmith.getMaxExp();
		smithJobBarLower.value = smithJobBar.value;
		smithJobName.text = trainSmith.getSmithJob().getSmithJobName();
		smithJobLevel.text = trainSmith.getSmithLevel().ToString();
		smithStaminaBar.value = trainSmith.getRemainingMood() / trainSmith.getSmithMaxMood();
		smithStaminaBarLower.value = smithStaminaBar.value;
		smithPow.text = trainSmith.getSmithPower().ToString();
		smithInt.text = trainSmith.getSmithIntelligence().ToString();
		smithTec.text = trainSmith.getSmithTechnique().ToString();
		smithLuc.text = trainSmith.getSmithLuck().ToString();
		smithMood.text = CommonAPI.getMoodString(trainSmith.getMoodState(), showDesc: false);
		int num = 0;
		List<SmithTraining> allowedTrainingList = gameData.getAllowedTrainingList(player.getPlayerDays(), player.getShopLevelInt());
		float num2 = player.checkDecoEffect("TRAIN_COST", string.Empty);
		foreach (SmithTraining item in allowedTrainingList)
		{
			GameObject aObject = commonScreenObject.createPrefab(trainingListGrid.gameObject, "trainingType_" + num, "Prefab/SmithManage/TrainingListObj", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(aObject, "TrainingName").GetComponent<UILabel>().text = item.getSmithTrainingName();
			commonScreenObject.findChild(aObject, "TrainingDescription").GetComponent<UILabel>().text = item.getSmithTrainingDesc();
			int smithTrainingCost = item.getSmithTrainingCost();
			string text = "[000000]$" + CommonAPI.formatNumber(smithTrainingCost) + "[-]";
			if (num2 < 1f)
			{
				smithTrainingCost = (int)((float)smithTrainingCost * num2);
				text = "[56AE59]$" + CommonAPI.formatNumber(smithTrainingCost) + "[-]";
			}
			else if (num2 > 1f)
			{
				smithTrainingCost = (int)((float)smithTrainingCost * num2);
				text = "[FF4842]$" + CommonAPI.formatNumber(smithTrainingCost) + "[-]";
			}
			commonScreenObject.findChild(aObject, "TrainingCost").GetComponent<UILabel>().text = text;
			if (item.getSmithTrainingCost() > player.getPlayerGold())
			{
				commonScreenObject.findChild(aObject, "TrainingListObj_bg").GetComponent<UISprite>().color = Color.grey;
				commonScreenObject.findChild(aObject, "TrainingWarning").GetComponent<UILabel>().text = "[FF4842]Not enough money[-]";
			}
			else
			{
				commonScreenObject.findChild(aObject, "TrainingListObj_bg").GetComponent<UISprite>().alpha = 0f;
				commonScreenObject.findChild(aObject, "TrainingWarning").GetComponent<UILabel>().text = string.Empty;
			}
			num++;
		}
		trainingListGrid.Reposition();
		trainingListScrollBar.value = 0f;
		trainingListGrid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
		trainingListGrid.enabled = true;
	}

	private void popLvlUp()
	{
		GameObject aObj = commonScreenObject.createPrefab(smithImage.gameObject, "popLvlUp" + trainSmith.getSmithRefId(), "Image/Process bubble/levelup/lvlupAnimSprite", new Vector3(0f, 180f, 0f), new Vector3(100f, 100f, 100f), new Vector3(0f, 180f, 0f));
		commonScreenObject.destroyPrefabDelay(aObj, 3f);
	}

	private void popStatUp(GameObject parent, int addAmt)
	{
		GameObject gameObject = commonScreenObject.createPrefab(parent, "popStatUp_" + parent.name, "Prefab/SmithManage/TrainingStatGrowthAnim", Vector3.zero, Vector3.one, Vector3.zero);
		gameObject.GetComponent<UILabel>().text = "+" + addAmt;
		commonScreenObject.destroyPrefabDelay(gameObject, 3f);
	}
}
