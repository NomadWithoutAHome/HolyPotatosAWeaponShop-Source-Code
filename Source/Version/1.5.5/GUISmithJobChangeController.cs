using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUISmithJobChangeController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private ViewController viewController;

	private Smith smithInfo;

	private List<SmithJobClass> unlockedList;

	private List<SmithExperience> smithExperienceList;

	private string selectJob;

	private UILabel changeClassTitleLabel;

	private Dictionary<string, GameObject> jobObjList;

	private GameObject changeJobMenu;

	private UIButton closeButton;

	private UIButton changeJobButton;

	private UILabel changeJobCost;

	private UILabel changeJobConfirm;

	private UILabel changeJobAtk;

	private UILabel changeJobSpd;

	private UILabel changeJobAcc;

	private UILabel changeJobMag;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		changeClassTitleLabel = commonScreenObject.findChild(base.gameObject, "SmithJobChange_bg/JobChangeTitle_bg/JobChangeTitle_label").GetComponent<UILabel>();
		closeButton = commonScreenObject.findChild(base.gameObject, "SmithJobChange_bg/Close_button").GetComponent<UIButton>();
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "Close_button":
			viewController.closeSmithJobChange();
			break;
		case "ChangeMenu_button":
		{
			SmithJobClass smithJobClass = game.getGameData().getSmithJobClass(selectJob);
			shopMenuController.doSmithJobChange(smithInfo, smithJobClass);
			GameObject gameObject = GameObject.Find("Panel_AssignSmithHUD");
			if (gameObject != null)
			{
				gameObject.GetComponent<GUIAssignSmithHUDController>().refreshSmithStats();
			}
			selectJob = string.Empty;
			hideChangeMenu();
			updateTree();
			break;
		}
		default:
		{
			string[] array = gameObjectName.Split('_');
			if (array[0] == "JobClassObj")
			{
				selectJobClassObj(array[1]);
			}
			break;
		}
		}
	}

	private void Update()
	{
		if (viewController != null && viewController.getIsPaused() && viewController.getGameStarted())
		{
			handleInput();
		}
	}

	private void handleInput()
	{
		if (Input.GetMouseButtonDown(1) || (Input.GetKeyDown(gameData.getKeyCodeByRefID("100007")) && closeButton.isEnabled))
		{
			processClick("Close_button");
		}
	}

	public void setReference(Smith aSmith)
	{
		GameData gameData = game.getGameData();
		smithInfo = aSmith;
		selectJob = string.Empty;
		changeClassTitleLabel.text = gameData.getTextByRefId("menuChangeJob02").ToUpper(CultureInfo.InvariantCulture);
		jobObjList = new Dictionary<string, GameObject>();
		changeJobMenu = null;
		GameObject gameObject = commonScreenObject.findChild(base.gameObject, "SmithJobChange_bg/JobClassTree").gameObject;
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			GameObject gameObject2 = gameObject.transform.GetChild(i).gameObject;
			if (gameObject2.name == "ChangeMenu_bg")
			{
				changeJobMenu = gameObject2;
				continue;
			}
			jobObjList.Add(gameObject2.name.Split('_')[1], gameObject2);
		}
		changeJobButton = commonScreenObject.findChild(changeJobMenu.gameObject, "ChangeMenu_button").GetComponent<UIButton>();
		changeJobCost = commonScreenObject.findChild(changeJobButton.gameObject, "ChangeButton_starch").GetComponent<UILabel>();
		changeJobConfirm = commonScreenObject.findChild(changeJobButton.gameObject, "ChangeButton_confirm").GetComponent<UILabel>();
		changeJobAtk = commonScreenObject.findChild(changeJobMenu.gameObject, "JobFitStats/AtkFit_icon/AtkFit_label").GetComponent<UILabel>();
		changeJobSpd = commonScreenObject.findChild(changeJobMenu.gameObject, "JobFitStats/SpdFit_icon/SpdFit_label").GetComponent<UILabel>();
		changeJobAcc = commonScreenObject.findChild(changeJobMenu.gameObject, "JobFitStats/AccFit_icon/AccFit_label").GetComponent<UILabel>();
		changeJobMag = commonScreenObject.findChild(changeJobMenu.gameObject, "JobFitStats/MagFit_icon/MagFit_label").GetComponent<UILabel>();
		commonScreenObject.findChild(changeJobMenu.gameObject, "ChangeMenu_title").GetComponent<UILabel>().text = gameData.getTextByRefId("menuChangeJob05").ToUpper(CultureInfo.InvariantCulture);
		commonScreenObject.findChild(changeJobMenu.gameObject, "ChangeMenu_warning").GetComponent<UILabel>().text = gameData.getTextByRefId("menuChangeJob03").ToUpper(CultureInfo.InvariantCulture);
		updateTree();
		hideChangeMenu();
	}

	private void selectJobClassObj(string aRefId)
	{
		if (selectJob == aRefId)
		{
			selectJob = string.Empty;
			updateTree();
			hideChangeMenu();
		}
		else
		{
			selectJob = aRefId;
			updateTree();
			showChangeMenu();
		}
	}

	private void hideChangeMenu()
	{
		changeJobMenu.transform.localPosition = new Vector3(0f, 2000f, 0f);
	}

	private void showChangeMenu()
	{
		GameData gameData = game.getGameData();
		SmithJobClass smithJobClass = gameData.getSmithJobClass(selectJob);
		if (checkAbleToChange(smithJobClass))
		{
			changeJobButton.isEnabled = true;
			changeJobCost.text = CommonAPI.formatNumber(smithJobClass.getSmithJobChangeCost());
			changeJobConfirm.text = gameData.getTextByRefId("menuChangeJob07").ToUpper(CultureInfo.InvariantCulture);
		}
		else
		{
			changeJobButton.isEnabled = false;
		}
		int smithPower = smithInfo.getSmithPower();
		int num = smithInfo.fitSmithPower(smithJobClass);
		if (smithPower < num)
		{
			changeJobAtk.color = Color.green;
		}
		else if (smithPower > num)
		{
			changeJobAtk.color = Color.red;
		}
		else
		{
			changeJobAtk.color = Color.white;
		}
		changeJobAtk.text = num.ToString();
		int smithIntelligence = smithInfo.getSmithIntelligence();
		int num2 = smithInfo.fitSmithIntelligence(smithJobClass);
		if (smithIntelligence < num2)
		{
			changeJobSpd.color = Color.green;
		}
		else if (smithIntelligence > num2)
		{
			changeJobSpd.color = Color.red;
		}
		else
		{
			changeJobSpd.color = Color.white;
		}
		changeJobSpd.text = num2.ToString();
		int smithTechnique = smithInfo.getSmithTechnique();
		int num3 = smithInfo.fitSmithTechnique(smithJobClass);
		if (smithTechnique < num3)
		{
			changeJobAcc.color = Color.green;
		}
		else if (smithTechnique > num3)
		{
			changeJobAcc.color = Color.red;
		}
		else
		{
			changeJobAcc.color = Color.white;
		}
		changeJobAcc.text = num3.ToString();
		int smithLuck = smithInfo.getSmithLuck();
		int num4 = smithInfo.fitSmithLuck(smithJobClass);
		if (smithLuck < num4)
		{
			changeJobMag.color = Color.green;
		}
		else if (smithLuck > num4)
		{
			changeJobMag.color = Color.red;
		}
		else
		{
			changeJobMag.color = Color.white;
		}
		changeJobMag.text = num4.ToString();
		GameObject gameObject = jobObjList[selectJob];
		Vector3 localPosition = gameObject.transform.localPosition;
		localPosition.y += 100f;
		changeJobMenu.transform.localPosition = localPosition;
	}

	private void updateTree()
	{
		GameData gameData = game.getGameData();
		smithExperienceList = smithInfo.getExperienceList();
		unlockedList = gameData.getJobChangeList(smithInfo.getExperienceList(), smithInfo.getSmithJob().getSmithJobRefId());
		foreach (string key in jobObjList.Keys)
		{
			SmithJobClass smithJobClass = gameData.getSmithJobClass(key);
			SmithExperience experienceByJobClass = smithInfo.getExperienceByJobClass(key);
			GameObject gameObject = jobObjList[key];
			bool flag = false;
			if (smithInfo.getSmithJob().getSmithJobRefId() == key)
			{
				flag = true;
			}
			bool flag2 = false;
			if (experienceByJobClass.getSmithJobClassLevel() >= smithJobClass.getMaxLevel())
			{
				flag2 = true;
			}
			bool flag3 = checkAbleToChange(smithJobClass);
			UISprite component = gameObject.GetComponent<UISprite>();
			UIButton component2 = gameObject.GetComponent<UIButton>();
			if (experienceByJobClass.getSmithExperienceRefId() == string.Empty)
			{
				component.spriteName = "bg_jobdisabled";
				component2.normalSprite = "bg_jobdisabled";
				component2.isEnabled = false;
			}
			else if (flag2)
			{
				component.spriteName = "bg_jobmaxlvl";
				component2.normalSprite = "bg_jobmaxlvl";
				component2.isEnabled = true;
			}
			else if (flag3 || flag)
			{
				component.spriteName = "bg_jobnormal";
				component2.normalSprite = "bg_jobnormal";
				component2.isEnabled = true;
			}
			else
			{
				component.spriteName = "bg_jobdisabled";
				component2.normalSprite = "bg_jobdisabled";
				component2.isEnabled = false;
			}
			UISprite[] componentsInChildren = gameObject.GetComponentsInChildren<UISprite>();
			foreach (UISprite uISprite in componentsInChildren)
			{
				switch (uISprite.name)
				{
				case "CurrentJob_sprite":
					if (flag)
					{
						uISprite.alpha = 1f;
					}
					else
					{
						uISprite.alpha = 0f;
					}
					break;
				case "SelectJob_frame":
					if (selectJob == key)
					{
						uISprite.alpha = 1f;
					}
					else
					{
						uISprite.alpha = 0f;
					}
					break;
				case "JobClass_atk":
					if (smithJobClass.checkDesign())
					{
						uISprite.color = Color.white;
					}
					else
					{
						uISprite.color = new Color(0.2f, 0.2f, 0.2f);
					}
					break;
				case "JobClass_spd":
					if (smithJobClass.checkCraft())
					{
						uISprite.color = Color.white;
					}
					else
					{
						uISprite.color = new Color(0.2f, 0.2f, 0.2f);
					}
					break;
				case "JobClass_acc":
					if (smithJobClass.checkPolish())
					{
						uISprite.color = Color.white;
					}
					else
					{
						uISprite.color = new Color(0.2f, 0.2f, 0.2f);
					}
					break;
				case "JobClass_mag":
					if (smithJobClass.checkEnchant())
					{
						uISprite.color = Color.white;
					}
					else
					{
						uISprite.color = new Color(0.2f, 0.2f, 0.2f);
					}
					break;
				case "upperBranch_left":
					if (flag2)
					{
						uISprite.alpha = 1f;
					}
					else
					{
						uISprite.alpha = 0f;
					}
					break;
				case "upperBranch_right":
					if (flag2)
					{
						uISprite.alpha = 1f;
					}
					else
					{
						uISprite.alpha = 0f;
					}
					break;
				case "lowerBranch_glow":
					if (flag3 || flag)
					{
						uISprite.alpha = 1f;
					}
					else
					{
						uISprite.alpha = 0f;
					}
					break;
				}
			}
			UILabel[] componentsInChildren2 = gameObject.GetComponentsInChildren<UILabel>();
			foreach (UILabel uILabel in componentsInChildren2)
			{
				switch (uILabel.name)
				{
				case "JobClass_boost":
					uILabel.text = gameData.getTextByRefId("menuChangeJob04").ToUpper(CultureInfo.InvariantCulture);
					break;
				case "JobClass_level":
					if (flag3 || flag)
					{
						if (flag2)
						{
							uILabel.text = gameData.getTextByRefId("smithStatsShort09").ToUpper(CultureInfo.InvariantCulture);
							break;
						}
						List<string> list = new List<string>();
						list.Add("[level]");
						list.Add("[maxLevel]");
						List<string> list2 = new List<string>();
						list2.Add(experienceByJobClass.getSmithJobClassLevel().ToString());
						list2.Add(smithJobClass.getMaxLevel().ToString());
						uILabel.text = gameData.getTextByRefIdWithDynTextList("smithStatsShort10", list, list2);
					}
					else
					{
						uILabel.text = gameData.getTextByRefId("menuChangeJob01").ToUpper(CultureInfo.InvariantCulture);
					}
					break;
				case "JobClass_name":
					if (flag3 || flag)
					{
						uILabel.text = smithJobClass.getSmithJobName().ToUpper(CultureInfo.InvariantCulture);
					}
					else
					{
						uILabel.text = "???";
					}
					break;
				}
			}
		}
	}

	private bool checkAbleToChange(SmithJobClass aJobClass)
	{
		foreach (SmithJobClass unlocked in unlockedList)
		{
			if (unlocked.getSmithJobRefId() == aJobClass.getSmithJobRefId())
			{
				return true;
			}
		}
		return false;
	}
}
