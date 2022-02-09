using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;

public class GUIQuestProgressController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private UILabel questHeaderLabel;

	private UILabel questTitleLabel;

	private UILabel jobClassLabel;

	private UITexture jobClassImg;

	private UILabel weaponLabel;

	private UITexture weaponImg;

	private UILabel questScenarioLabel;

	private Quest currentQuest;

	private Project currentProject;

	private Hero hero;

	private UISlider questBarPrevious;

	private UISlider questStatusBar;

	private int questPercentBefore;

	private int questPercentAfter;

	private float percentage;

	private UILabel questPercentageLabel;

	private List<GameObject> subquestObjectList;

	private Hashtable subquestPassList;

	private int trySubQuestIndex;

	private GameObject subquest0;

	private GameObject subquest1;

	private GameObject subquest2;

	private UILabel questRewardLabel;

	private int goldDrop;

	private float totalGold;

	private UILabel goldLabel;

	private UIButton okButton;

	private Color incompleteSubquestColor;

	private bool isAnimating;

	private bool battleDone;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		questHeaderLabel = GameObject.Find("QuestHeaderLabel").GetComponent<UILabel>();
		questTitleLabel = GameObject.Find("QuestTitleLabel").GetComponent<UILabel>();
		jobClassLabel = GameObject.Find("JobClassLabel").GetComponent<UILabel>();
		jobClassImg = GameObject.Find("JobClassImg").GetComponent<UITexture>();
		weaponLabel = GameObject.Find("WeaponLabel").GetComponent<UILabel>();
		weaponImg = GameObject.Find("WeaponImg").GetComponent<UITexture>();
		questScenarioLabel = GameObject.Find("QuestScenarioLabel").GetComponent<UILabel>();
		questBarPrevious = GameObject.Find("QuestBarPrevious").GetComponent<UISlider>();
		questStatusBar = GameObject.Find("QuestStatusBar").GetComponent<UISlider>();
		percentage = 0f;
		questPercentageLabel = GameObject.Find("QuestPercentageLabel").GetComponent<UILabel>();
		totalGold = 0f;
		okButton = GameObject.Find("OkButton").GetComponent<UIButton>();
		incompleteSubquestColor = new Color32(29, 68, 120, byte.MaxValue);
		isAnimating = false;
		battleDone = false;
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName == "OkButton")
		{
			ViewController component = GameObject.Find("ViewController").GetComponent<ViewController>();
			component.closeQuestProgressPopup();
		}
	}

	private IEnumerator wait(float timeToWait)
	{
		yield return new WaitForSeconds(timeToWait);
	}

	private IEnumerator fillUpBar()
	{
		int prevBattle = -1;
		while (isAnimating)
		{
			float percent25 = percentage % 25f;
			float waitTime = (percent25 * percent25 + 11.7551f) / 11755.1f;
			yield return new WaitForSeconds(waitTime);
			percentage += 0.25f;
			if (currentQuest.getQuestType() == QuestType.QuestTypeNormal)
			{
				setGoldDisplay(percentage);
			}
			setSliderValue(percentage);
			if ((int)percentage % 20 == 0 && (int)percentage != prevBattle)
			{
				doBattle();
				prevBattle = (int)percentage;
			}
			if (currentQuest.getQuestType() == QuestType.QuestTypeNormal && (percentage == 25f || percentage == 50f || percentage == 75f))
			{
				giveSubquestReward();
			}
			if (percentage == (float)questPercentAfter)
			{
				isAnimating = false;
				audioController.stopQuestBarLoopAudio();
				if (currentQuest.getQuestType() == QuestType.QuestTypeNormal)
				{
					setTotalGold(goldDrop);
				}
				if (!battleDone)
				{
					doBattle();
				}
				if (questPercentAfter == 100)
				{
					TweenScale component = GameObject.Find("QuestThumbFace").GetComponent<TweenScale>();
					component.ResetToBeginning();
					component.PlayForward();
					ParticleSystem component2 = GameObject.Find("CompleteFirework").GetComponent<ParticleSystem>();
					component2.Play();
					audioController.playSubquestCompleteAudio();
				}
				okButton.GetComponent<UIButton>().isEnabled = true;
			}
		}
	}

	private void setScenarioText(string aText)
	{
		questScenarioLabel.text += aText;
		string processedText = questScenarioLabel.processedText;
		int num = Regex.Matches(processedText, "\n", RegexOptions.IgnoreCase).Count + 1;
		questScenarioLabel.transform.parent.localPosition = new Vector3(0f, (num - 5) * 17, 0f);
	}

	private void setSliderValue(float aValue)
	{
		questStatusBar.value = aValue / 100f;
		questPercentageLabel.text = (int)aValue + "%";
	}

	private void setGoldDisplay(float aPercent)
	{
		totalGold = (float)goldDrop * (aPercent / 100f);
		goldLabel.text = CommonAPI.formatNumber((int)totalGold);
	}

	private void setTotalGold(float aValue)
	{
		totalGold = aValue;
		goldLabel.text = CommonAPI.formatNumber((int)totalGold);
	}

	public void sendHeroOnQuest(Project project, Quest quest)
	{
		GameData gameData = game.getGameData();
		currentQuest = quest;
		currentProject = project;
		setReference();
		okButton.isEnabled = false;
		okButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral04");
		questHeaderLabel.text = gameData.getTextByRefId("forgeMenu03").ToUpper(CultureInfo.InvariantCulture);
		questTitleLabel.text = quest.getQuestName();
		if (quest.getProgressPercent() >= 100)
		{
			quest.restartQuest();
		}
		questPercentBefore = quest.getProgressPercent();
		questBarPrevious.value = (float)questPercentBefore / 100f;
		percentage = questPercentBefore;
		setSliderValue(questPercentBefore);
		setQuestStart(quest);
		Weapon projectWeapon = project.getProjectWeapon();
		weaponLabel.text = projectWeapon.getWeaponName();
		weaponImg.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + projectWeapon.getImage());
		int num = 0;
		if (quest.getQuestType() == QuestType.QuestTypeInstant)
		{
			num = quest.getMaxTargetPoints();
		}
		else
		{
			WeaponStat weaponStat = WeaponStat.WeaponStatAttack;
			WeaponStat weaponStat2 = WeaponStat.WeaponStatSpeed;
			num = ((weaponStat != 0 && weaponStat2 != 0) ? (num + project.getAtk()) : (num + project.getAtk() * 2));
			num = ((weaponStat != WeaponStat.WeaponStatSpeed && weaponStat2 != WeaponStat.WeaponStatSpeed) ? (num + project.getSpd()) : (num + project.getSpd() * 2));
			num = ((weaponStat != WeaponStat.WeaponStatAccuracy && weaponStat2 != WeaponStat.WeaponStatAccuracy) ? (num + project.getAcc()) : (num + project.getAcc() * 2));
			num = ((weaponStat != WeaponStat.WeaponStatMagic && weaponStat2 != WeaponStat.WeaponStatMagic) ? (num + project.getMag()) : (num + project.getMag() * 2));
			num = (int)((float)num * Random.Range(1f, 1.2f));
			if (quest.getRecommendedWeaponTypeRefId() == projectWeapon.getWeaponTypeRefId())
			{
				num = (int)((float)num * 1.25f);
			}
			int maxTargetPoints = quest.getMaxTargetPoints();
			float num2 = (float)num / (float)maxTargetPoints;
			goldDrop = (int)(num2 * Mathf.Pow(maxTargetPoints, 0.5f) * 10f);
			CommonAPI.debug("goldDrop: " + goldDrop);
		}
		if (goldDrop > 0)
		{
			game.getPlayer().addGold(goldDrop);
			audioController.playGoldGainAudio();
		}
		subquestPassList = quest.doQuest(num);
		questPercentAfter = quest.getProgressPercent();
		List<string> list = new List<string>();
		list.Add("[heroName]");
		list.Add("[projectName]");
		List<string> list2 = new List<string>();
		list2.Add(hero.getHeroName());
		list2.Add(project.getProjectName(includePrefix: true));
		setScenarioText(gameData.getTextByRefIdWithDynTextList("menuQuest05", list, list2));
		isAnimating = true;
		StartCoroutine("fillUpBar");
		audioController.startQuestBarLoopAudio();
	}

	private void setReference()
	{
		if (currentQuest.getQuestType() == QuestType.QuestTypeNormal)
		{
			subquestObjectList = new List<GameObject>();
			trySubQuestIndex = 0;
			subquest0 = GameObject.Find("Subquest0");
			subquest1 = GameObject.Find("Subquest1");
			subquest2 = GameObject.Find("Subquest2");
			subquestObjectList.Add(subquest0);
			subquestObjectList.Add(subquest1);
			subquestObjectList.Add(subquest2);
			questRewardLabel = GameObject.Find("QuestRewardLabel").GetComponent<UILabel>();
			questRewardLabel.text = game.getGameData().getTextByRefId("questResult01");
			goldLabel = GameObject.Find("GoldLabel").GetComponent<UILabel>();
		}
	}

	private void doBattle()
	{
		GameData gameData = game.getGameData();
		battleDone = true;
		string text = "Hero Skill";
		if (text == "NONE" || text == string.Empty)
		{
			text = currentProject.getProjectWeapon().getWeaponType().getWeaponTypeSkill();
		}
		string randomQuestEnemy = CommonAPI.getRandomQuestEnemy();
		int num = Random.Range(1, 4);
		int num2 = 1;
		int num3 = (int)(Random.Range(0.95f, 1.05f) * (float)num2);
		List<string> list = new List<string>();
		list.Add("[heroName]");
		list.Add("[skill]");
		list.Add("[enemy]");
		list.Add("[hits]");
		list.Add("[dmg]");
		List<string> list2 = new List<string>();
		list2.Add(hero.getHeroName());
		list2.Add(text);
		list2.Add(randomQuestEnemy);
		list2.Add(num.ToString());
		list2.Add(num3.ToString());
		if (text == "NONE" || text == string.Empty)
		{
			setScenarioText("\n\n" + gameData.getTextByRefIdWithDynTextList("menuQuest16", list, list2));
		}
		else
		{
			setScenarioText("\n\n" + gameData.getTextByRefIdWithDynTextList("menuQuest17", list, list2));
		}
	}

	private void setQuestStart(Quest quest)
	{
		GameData gameData = game.getGameData();
		List<Subquest> subquestList = quest.getSubquestList();
		string text = string.Empty;
		string empty = string.Empty;
		if (currentQuest.getQuestType() != 0)
		{
			return;
		}
		if (questPercentBefore < 25)
		{
			trySubQuestIndex = 0;
		}
		else if (questPercentBefore < 50)
		{
			trySubQuestIndex = 1;
		}
		else if (questPercentBefore < 75)
		{
			trySubQuestIndex = 2;
		}
		for (int i = 0; i < subquestList.Count; i++)
		{
			switch (subquestList[i].getSubquestType())
			{
			case SubquestType.SubquestTypeElement:
				text = "ele";
				break;
			case SubquestType.SubquestTypeStat:
				text = "stat";
				break;
			case SubquestType.SubquestTypeWeapon:
				text = "weapon";
				break;
			}
			string subquestItemRefId = subquestList[i].getSubquestItemRefId();
			int subquestItemQuantity = subquestList[i].getSubquestItemQuantity();
			commonScreenObject.findChild(subquestObjectList[i], "SubquestHeader/SubquestTitlePercent").GetComponent<UILabel>().text = gameData.getTextByRefIdWithDynText("menuSubquest05", "[percentage]", ((i + 1) * 25).ToString());
			commonScreenObject.findChild(subquestObjectList[i], "SubquestTitle").GetComponent<UILabel>().text = subquestList[i].getSubquestTitle();
			commonScreenObject.findChild(subquestObjectList[i], "SubquestReward/RewardLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("reward");
			if (subquestList[i].checkSubquestUnlocked())
			{
				empty = "on";
				commonScreenObject.findChild(subquestObjectList[i], "SubquestReward").GetComponent<UISprite>().color = Color.white;
				commonScreenObject.findChild(subquestObjectList[i], "QuestCompletedFrame").GetComponent<UISprite>().spriteName = "subquest_completebg";
				commonScreenObject.findChild(subquestObjectList[i], "QuestCompletedFrame/QuestCompletedLabel").GetComponent<UILabel>().color = Color.white;
				commonScreenObject.findChild(subquestObjectList[i], "QuestCompletedFrame/QuestCompletedLabel").GetComponent<UILabel>().text = gameData.getTextByRefId("menuSubquest01");
				subquestObjectList[i].GetComponent<UISprite>().spriteName = "subquest_bglight";
			}
			else
			{
				empty = "off";
				commonScreenObject.findChild(subquestObjectList[i], "SubquestReward").GetComponent<UISprite>().color = Color.gray;
				commonScreenObject.findChild(subquestObjectList[i], "QuestCompletedFrame").GetComponent<UISprite>().spriteName = "subquest_incompletebg";
				commonScreenObject.findChild(subquestObjectList[i], "QuestCompletedFrame/QuestCompletedLabel").GetComponent<UILabel>().color = incompleteSubquestColor;
				commonScreenObject.findChild(subquestObjectList[i], "QuestCompletedFrame/QuestCompletedLabel").GetComponent<UILabel>().text = gameData.getTextByRefIdWithDynText("menuSubquest04", "[percentage]", ((i + 1) * 25).ToString());
				subquestObjectList[i].GetComponent<UISprite>().spriteName = "subquest_bgdark";
			}
			if (subquestItemRefId != string.Empty && subquestItemRefId != "-1" && subquestItemQuantity > 0)
			{
				Item itemByRefId = game.getGameData().getItemByRefId(subquestItemRefId);
				CommonAPI.debug("rewardItemRefID: " + subquestItemRefId + "   " + itemByRefId.getItemName());
				commonScreenObject.findChild(subquestObjectList[i], "SubquestReward/ItemName").GetComponent<UILabel>().text = itemByRefId.getItemName();
				commonScreenObject.findChild(subquestObjectList[i], "SubquestReward/RewardQty").GetComponent<UILabel>().text = subquestItemQuantity + "X";
			}
			else
			{
				int subquestGold = subquestList[i].getSubquestGold();
				commonScreenObject.findChild(subquestObjectList[i], "SubquestReward/GoldIcon").GetComponent<UISprite>().enabled = true;
				commonScreenObject.findChild(subquestObjectList[i], "SubquestReward/GoldSubquestLabel").GetComponent<UILabel>().text = CommonAPI.formatNumber(subquestGold);
			}
			commonScreenObject.findChild(subquestObjectList[i], "SubquestHeader/SubquestIcon").GetComponent<UISprite>().spriteName = text + "-" + empty;
		}
	}

	public void giveSubquestReward()
	{
	}
}
