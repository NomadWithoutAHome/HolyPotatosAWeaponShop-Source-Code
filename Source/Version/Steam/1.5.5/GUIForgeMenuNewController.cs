using System.Collections;
using System.Collections.Generic;
using SmoothMoves;
using UnityEngine;

public class GUIForgeMenuNewController : MonoBehaviour
{
	private QuestNEW questInfo;

	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private GUIForgeCategoryController forgeCatController;

	private GameObject questBg;

	private UILabel questTitleLabel;

	private GameObject difficultyIndicator;

	private UILabel difficultyLabel;

	private UILabel questTitle;

	private UILabel questDetail;

	private UILabel rewardLabel;

	private UILabel rewardValue;

	private UILabel statReqLabel;

	private UILabel atkValue;

	private UILabel accValue;

	private UILabel spdValue;

	private UILabel magValue;

	private UILabel tagValue;

	private GameObject detailBg;

	private UILabel totalLabel;

	private UILabel totalCost;

	private UIButton startButton;

	private UILabel startLabel;

	private string selectedJobClassRefID;

	private BoxCollider heroBg;

	private UILabel selectHeroLabel;

	private UIButton heroChangeButton;

	private UILabel heroChangeLabel;

	private UILabel heroCost;

	private UISprite heroCostCircle;

	private GameObject heroImg;

	private UILabel heroTitle;

	private UILabel heroBoostLabel;

	private UILabel atkHero;

	private UILabel accHero;

	private UILabel spdHero;

	private UILabel magHero;

	private List<GameObject> heroTagList;

	private string selectedWeaponRefID;

	private BoxCollider weaponBg;

	private UILabel selectWeaponLabel;

	private UIButton weaponChangeButton;

	private UILabel weaponChangeLabel;

	private UILabel weaponCost;

	private UISprite weaponCostCircle;

	private UITexture weaponImg;

	private UILabel weaponTitle;

	private UILabel weaponGrowthLabel;

	private UISprite atkWeapon;

	private UISprite accWeapon;

	private UISprite spdWeapon;

	private UISprite magWeapon;

	private List<GameObject> weaponTagList;

	private Vector3 questClosePos;

	private Vector3 questOpenPos;

	private Vector3 detailClosePos;

	private Vector3 detailOpenPos;

	private bool isOpen;

	private bool isAnimating;

	private string category;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		forgeCatController = commonScreenObject.findChild(base.gameObject, "CategorySelection").GetComponent<GUIForgeCategoryController>();
		questBg = commonScreenObject.findChild(base.gameObject, "QuestBg").gameObject;
		questTitleLabel = commonScreenObject.findChild(questBg, "QuestTitleLabel").GetComponent<UILabel>();
		difficultyIndicator = commonScreenObject.findChild(questBg, "DifficultyBg/DifficultyIndicator").gameObject;
		difficultyLabel = commonScreenObject.findChild(questBg, "DifficultyBg/DifficultyLabel").GetComponent<UILabel>();
		questTitle = commonScreenObject.findChild(questBg, "QuestDetailBg/QuestTitle").GetComponent<UILabel>();
		questDetail = commonScreenObject.findChild(questBg, "QuestDetailBg/QuestDetail").GetComponent<UILabel>();
		rewardLabel = commonScreenObject.findChild(questBg, "RewardBg/RewardLabel").GetComponent<UILabel>();
		rewardValue = commonScreenObject.findChild(questBg, "RewardBg/RewardValue").GetComponent<UILabel>();
		statReqLabel = commonScreenObject.findChild(questBg, "QuestRequirementBg/StatReqLabel").GetComponent<UILabel>();
		atkValue = commonScreenObject.findChild(questBg, "QuestRequirementBg/AtkReq/AtkValue").GetComponent<UILabel>();
		accValue = commonScreenObject.findChild(questBg, "QuestRequirementBg/AccReq/AccValue").GetComponent<UILabel>();
		spdValue = commonScreenObject.findChild(questBg, "QuestRequirementBg/SpdReq/SpdValue").GetComponent<UILabel>();
		magValue = commonScreenObject.findChild(questBg, "QuestRequirementBg/MagReq/MagValue").GetComponent<UILabel>();
		tagValue = commonScreenObject.findChild(questBg, "QuestRequirementBg/TagReq/TagValue").GetComponent<UILabel>();
		detailBg = commonScreenObject.findChild(base.gameObject, "DetailBg").gameObject;
		totalLabel = commonScreenObject.findChild(detailBg, "TotalLabel").GetComponent<UILabel>();
		totalCost = commonScreenObject.findChild(detailBg, "TotalCost").GetComponent<UILabel>();
		startButton = commonScreenObject.findChild(detailBg, "StartButton").GetComponent<UIButton>();
		startLabel = commonScreenObject.findChild(startButton.gameObject, "StartLabel").GetComponent<UILabel>();
		selectedJobClassRefID = string.Empty;
		GameObject aObject = commonScreenObject.findChild(detailBg, "Hero").gameObject;
		heroBg = commonScreenObject.findChild(aObject, "HeroBg").GetComponent<BoxCollider>();
		selectHeroLabel = commonScreenObject.findChild(heroBg.gameObject, "SelectHeroLabel").GetComponent<UILabel>();
		heroChangeButton = commonScreenObject.findChild(heroBg.gameObject, "HeroChangeButton").GetComponent<UIButton>();
		heroChangeLabel = commonScreenObject.findChild(heroChangeButton.gameObject, "HeroChangeLabel").GetComponent<UILabel>();
		heroCost = commonScreenObject.findChild(heroBg.gameObject, "HeroCost").GetComponent<UILabel>();
		heroCostCircle = commonScreenObject.findChild(heroBg.gameObject, "HeroCostCircle").GetComponent<UISprite>();
		heroImg = commonScreenObject.findChild(heroBg.gameObject, "HeroImg").gameObject;
		heroTitle = commonScreenObject.findChild(aObject, "HeroTitleBg/HeroTitle").GetComponent<UILabel>();
		heroBoostLabel = commonScreenObject.findChild(aObject, "HeroStatBg/HeroBoostLabel").GetComponent<UILabel>();
		atkHero = commonScreenObject.findChild(aObject, "HeroStatBg/AtkHero").GetComponent<UILabel>();
		accHero = commonScreenObject.findChild(aObject, "HeroStatBg/AccHero").GetComponent<UILabel>();
		spdHero = commonScreenObject.findChild(aObject, "HeroStatBg/SpdHero").GetComponent<UILabel>();
		magHero = commonScreenObject.findChild(aObject, "HeroStatBg/MagHero").GetComponent<UILabel>();
		heroTagList = new List<GameObject>();
		heroTagList.Add(commonScreenObject.findChild(aObject, "HeroTag1").gameObject);
		heroTagList.Add(commonScreenObject.findChild(aObject, "HeroTag2").gameObject);
		heroTagList.Add(commonScreenObject.findChild(aObject, "HeroTag3").gameObject);
		selectedWeaponRefID = string.Empty;
		GameObject aObject2 = commonScreenObject.findChild(detailBg, "Weapon").gameObject;
		weaponBg = commonScreenObject.findChild(aObject2, "WeaponBg").GetComponent<BoxCollider>();
		selectWeaponLabel = commonScreenObject.findChild(weaponBg.gameObject, "SelectWeaponLabel").GetComponent<UILabel>();
		weaponChangeButton = commonScreenObject.findChild(weaponBg.gameObject, "WeaponChangeButton").GetComponent<UIButton>();
		weaponChangeLabel = commonScreenObject.findChild(weaponChangeButton.gameObject, "WeaponChangeLabel").GetComponent<UILabel>();
		weaponCost = commonScreenObject.findChild(weaponBg.gameObject, "WeaponCost").GetComponent<UILabel>();
		weaponCostCircle = commonScreenObject.findChild(weaponBg.gameObject, "WeaponCostCircle").GetComponent<UISprite>();
		weaponImg = commonScreenObject.findChild(weaponBg.gameObject, "WeaponImg").GetComponent<UITexture>();
		weaponTitle = commonScreenObject.findChild(aObject2, "WeaponTitleBg/WeaponTitle").GetComponent<UILabel>();
		weaponGrowthLabel = commonScreenObject.findChild(aObject2, "WeaponStatBg/WeaponGrowthLabel").GetComponent<UILabel>();
		atkWeapon = commonScreenObject.findChild(aObject2, "WeaponStatBg/AtkWeapon").GetComponent<UISprite>();
		accWeapon = commonScreenObject.findChild(aObject2, "WeaponStatBg/AccWeapon").GetComponent<UISprite>();
		spdWeapon = commonScreenObject.findChild(aObject2, "WeaponStatBg/SpdWeapon").GetComponent<UISprite>();
		magWeapon = commonScreenObject.findChild(aObject2, "WeaponStatBg/MagWeapon").GetComponent<UISprite>();
		weaponTagList = new List<GameObject>();
		weaponTagList.Add(commonScreenObject.findChild(aObject2, "WeaponTag1").gameObject);
		weaponTagList.Add(commonScreenObject.findChild(aObject2, "WeaponTag2").gameObject);
		weaponTagList.Add(commonScreenObject.findChild(aObject2, "WeaponTag3").gameObject);
		questClosePos = new Vector3(0f, 72f, 0f);
		questOpenPos = new Vector3(0f, 175f, 0f);
		detailClosePos = new Vector3(0f, -96f, 0f);
		detailOpenPos = new Vector3(0f, -186f, 0f);
		isOpen = false;
		isAnimating = false;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "StartButton":
		{
			GameData gameData = game.getGameData();
			Player player = game.getPlayer();
			player.setLastSelectWeapon(gameData.getWeaponByRefId(selectedWeaponRefID));
			player.setLastSelectHero(gameData.getJobClassByRefId(selectedJobClassRefID));
			shopMenuController.startForging();
			break;
		}
		case "QuestCloseButton":
			viewController.closeForgeMenuNewPopup(resumeFromPlayerPause: false);
			break;
		case "HeroBg":
		case "HeroChangeButton":
			if (!isAnimating)
			{
				if (!isOpen)
				{
					category = "JobClass";
					animateOpen();
				}
				else if (category != "JobClass")
				{
					animateSwitch("JobClass");
				}
			}
			break;
		case "WeaponBg":
		case "WeaponChangeButton":
			if (!isAnimating)
			{
				if (!isOpen)
				{
					category = "Weapon";
					animateOpen();
				}
				else if (category != "Weapon")
				{
					animateSwitch("Weapon");
				}
			}
			break;
		}
	}

	private void reset()
	{
		GameData gameData = game.getGameData();
		heroCostCircle.enabled = false;
		weaponCostCircle.enabled = false;
		heroChangeButton.isEnabled = false;
		weaponChangeButton.isEnabled = false;
		startButton.isEnabled = false;
		questTitleLabel.text = gameData.getTextByRefId("questSelect01");
		rewardLabel.text = gameData.getTextByRefId("questSelect02");
		statReqLabel.text = gameData.getTextByRefId("questSelect03");
		difficultyLabel.text = gameData.getTextByRefId("questSelect04");
		selectHeroLabel.text = gameData.getTextByRefId("questSelect05");
		selectWeaponLabel.text = gameData.getTextByRefId("questSelect06");
		heroBoostLabel.text = gameData.getTextByRefId("questSelect07");
		weaponGrowthLabel.text = gameData.getTextByRefId("questSelect08");
		heroChangeLabel.text = gameData.getTextByRefId("questSelect09");
		weaponChangeLabel.text = gameData.getTextByRefId("questSelect09");
		totalLabel.text = gameData.getTextByRefId("questSelect10");
		startLabel.text = gameData.getTextByRefId("questSelect11");
		heroTitle.text = "????";
		weaponTitle.text = "????";
		totalCost.text = "$ ---";
		resetJobclassTag();
		resetWeaponTag();
	}

	private void resetJobclassTag()
	{
		foreach (GameObject heroTag in heroTagList)
		{
			heroTag.SetActive(value: false);
		}
	}

	private void resetWeaponTag()
	{
		foreach (GameObject weaponTag in weaponTagList)
		{
			weaponTag.SetActive(value: false);
		}
	}

	public void setReference()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		reset();
		questInfo = player.getLastSelectQuest();
		questTitle.text = questInfo.getQuestName();
		questDetail.text = questInfo.getQuestDesc();
		rewardValue.text = CommonAPI.formatNumber(questInfo.getRewardGold());
		difficultyIndicator.transform.localPosition = shopMenuController.getQuestDifficultyPosUI(questInfo);
		atkValue.text = CommonAPI.formatNumber(questInfo.getAtkReq());
		accValue.text = CommonAPI.formatNumber(questInfo.getAccReq());
		spdValue.text = CommonAPI.formatNumber(questInfo.getSpdReq());
		magValue.text = CommonAPI.formatNumber(questInfo.getMagReq());
		List<QuestTag> questTagList = questInfo.getQuestTagList();
		tagValue.text = gameData.getTextByRefIdWithDynText("questSelect12", "[TagAmount]", questTagList.Count.ToString());
	}

	public void animateOpen()
	{
		CommonAPI.debug("animateOpen");
		forgeCatController.hide();
		checkRefID();
		commonScreenObject.tweenPosition(questBg.GetComponent<TweenPosition>(), questClosePos, questOpenPos, 0.5f, null, string.Empty);
		commonScreenObject.tweenPosition(detailBg.GetComponent<TweenPosition>(), detailClosePos, detailOpenPos, 0.5f, base.gameObject, "finishAnimateOpen");
		isOpen = true;
		isAnimating = true;
	}

	public void animateSwitch(string switchToCategory)
	{
		CommonAPI.debug("animateSwitch");
		commonScreenObject.tweenPosition(questBg.GetComponent<TweenPosition>(), questOpenPos, questClosePos, 0.5f, null, string.Empty);
		commonScreenObject.tweenPosition(detailBg.GetComponent<TweenPosition>(), detailOpenPos, detailClosePos, 0.5f, base.gameObject, "animateOpen");
		isOpen = false;
		isAnimating = true;
		category = switchToCategory;
	}

	public void animateClose()
	{
		CommonAPI.debug("animateClose");
		commonScreenObject.tweenPosition(questBg.GetComponent<TweenPosition>(), questOpenPos, questClosePos, 0.5f, null, string.Empty);
		commonScreenObject.tweenPosition(detailBg.GetComponent<TweenPosition>(), detailOpenPos, detailClosePos, 0.5f, base.gameObject, "finishAnimateClose");
		isOpen = false;
		isAnimating = true;
	}

	public void finishAnimateOpen()
	{
		CommonAPI.debug("finishAnimateOpen " + category);
		isAnimating = false;
		forgeCatController.setReference(category);
	}

	public void finishAnimateClose()
	{
		CommonAPI.debug("finishAnimateClose " + category);
		isAnimating = false;
		forgeCatController.hide();
		checkRefID();
	}

	public void checkRefID()
	{
	}

	public IEnumerator animateHero(BoneAnimation heroAnim)
	{
		bool heroExists = true;
		while (heroExists)
		{
			float randomWait = Random.Range(2f, 6f);
			yield return new WaitForSeconds(randomWait);
			if (heroAnim != null)
			{
				heroAnim.CrossFadeQueued("inter", 0.3f, QueueMode.PlayNow, PlayMode.StopAll);
				heroAnim.CrossFadeQueued("idle", 0.3f, QueueMode.CompleteOthers, PlayMode.StopAll);
			}
			else
			{
				heroExists = false;
			}
		}
	}

	public void loadJobClassDetail(string aJobClassRefID)
	{
		selectedJobClassRefID = aJobClassRefID;
		Hero jobClassByRefId = game.getGameData().getJobClassByRefId(selectedJobClassRefID);
		selectHeroLabel.text = string.Empty;
		while (heroImg.transform.childCount > 0)
		{
			commonScreenObject.destroyPrefabImmediate(heroImg.transform.GetChild(0).gameObject);
		}
		heroCost.text = CommonAPI.formatNumber(0);
		heroCostCircle.enabled = true;
		string image = jobClassByRefId.getImage();
		GameObject gameObject = commonScreenObject.createPrefab(heroImg, image + "_animObj", "Animation/Hero/" + image + "/" + image + "_animObj", Vector3.zero, Vector3.one, Vector3.zero);
		BoneAnimation component = gameObject.GetComponent<BoneAnimation>();
		component.mMaterials[0].shader = Resources.Load("Custom Shader/Alpha Blended - QuestSelect Hero") as Shader;
		component.Play("idle");
		StartCoroutine(animateHero(component));
		heroTitle.text = jobClassByRefId.getJobClassName();
		checkRefID();
	}

	public void loadWeaponDetail(string aWeaponRefID)
	{
		selectedWeaponRefID = aWeaponRefID;
		Weapon weaponByRefId = game.getGameData().getWeaponByRefId(selectedWeaponRefID);
		selectWeaponLabel.text = string.Empty;
		weaponCostCircle.enabled = true;
		weaponImg.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + weaponByRefId.getImage());
		weaponTitle.text = weaponByRefId.getWeaponName();
		atkWeapon.fillAmount = weaponByRefId.getAtkMult() - 1f;
		accWeapon.fillAmount = weaponByRefId.getAccMult() - 1f;
		spdWeapon.fillAmount = weaponByRefId.getSpdMult() - 1f;
		magWeapon.fillAmount = weaponByRefId.getMagMult() - 1f;
		resetWeaponTag();
		checkRefID();
	}

	public string getSelectedJobClassRefID()
	{
		return selectedJobClassRefID;
	}

	public string getSelectedWeaponRefID()
	{
		return selectedWeaponRefID;
	}

	public bool getIsAnimating()
	{
		return isAnimating;
	}
}
