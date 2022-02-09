using System.Collections.Generic;
using System.Globalization;
using SmoothMoves;
using UnityEngine;

public class GUIOfferWeaponController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private Smith smith;

	private Area area;

	private List<Project> sellList;

	private int sellListSelection;

	private List<GameObject> offerObjectList;

	private List<int> acceptedOfferList;

	private List<string> acceptedHeroList;

	private string offerPrefix;

	private int selectedIndex;

	private UILabel locationLabel;

	private UILabel smithLabel;

	private UITexture smithTexture;

	private UISprite areaEventIcon;

	private GameObject heroListObj;

	private UISprite heroListBg;

	private UILabel weaponNameLabel;

	private UILabel weaponNum;

	private UITexture weaponImage;

	private ParticleSystem weaponParticles;

	private UILabel atkLabel;

	private UILabel spdLabel;

	private UILabel accLabel;

	private UILabel magLabel;

	private UISprite atkBg;

	private UISprite accBg;

	private UISprite spdBg;

	private UISprite magBg;

	private UILabel goldLabel;

	private UIButton sellButton;

	private UIButton notSellButton;

	private Shader animShader;

	private bool isTutorial;

	private ParticleSystem[] pauseParticles;

	private BoneAnimation[] pauseAnims;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		smith = null;
		area = null;
		sellList = new List<Project>();
		sellListSelection = 0;
		offerObjectList = new List<GameObject>();
		acceptedOfferList = new List<int>();
		acceptedHeroList = new List<string>();
		offerPrefix = "Offer_";
		selectedIndex = 0;
		locationLabel = commonScreenObject.findChild(base.gameObject, "Info_bg/LocationInfo_label").GetComponent<UILabel>();
		smithLabel = commonScreenObject.findChild(base.gameObject, "Info_bg/SmithInfo_label").GetComponent<UILabel>();
		smithTexture = commonScreenObject.findChild(base.gameObject, "Info_bg/SmithInfo_texture").GetComponent<UITexture>();
		areaEventIcon = commonScreenObject.findChild(base.gameObject, "Info_bg/AreaEventIcon_bg").GetComponent<UISprite>();
		heroListObj = commonScreenObject.findChild(base.gameObject, "OfferList_bg").gameObject;
		heroListBg = heroListObj.GetComponent<UISprite>();
		weaponNameLabel = commonScreenObject.findChild(base.gameObject, "WeaponDetails_bg/WeaponName_label").GetComponent<UILabel>();
		weaponNum = commonScreenObject.findChild(base.gameObject, "WeaponDetails_bg/WeaponNum_label").GetComponent<UILabel>();
		weaponImage = commonScreenObject.findChild(base.gameObject, "WeaponDetails_bg/WeaponImage/WeaponImage_texture").GetComponent<UITexture>();
		weaponParticles = commonScreenObject.findChild(base.gameObject, "WeaponDetails_bg/WeaponImage/WeaponParticles").GetComponent<ParticleSystem>();
		atkLabel = commonScreenObject.findChild(base.gameObject, "WeaponDetails_bg/WeaponStats_bg/atk_bg/atk_label").GetComponent<UILabel>();
		spdLabel = commonScreenObject.findChild(base.gameObject, "WeaponDetails_bg/WeaponStats_bg/spd_bg/spd_label").GetComponent<UILabel>();
		accLabel = commonScreenObject.findChild(base.gameObject, "WeaponDetails_bg/WeaponStats_bg/acc_bg/acc_label").GetComponent<UILabel>();
		magLabel = commonScreenObject.findChild(base.gameObject, "WeaponDetails_bg/WeaponStats_bg/mag_bg/mag_label").GetComponent<UILabel>();
		atkBg = commonScreenObject.findChild(base.gameObject, "WeaponDetails_bg/WeaponStats_bg/atk_bg").GetComponent<UISprite>();
		spdBg = commonScreenObject.findChild(base.gameObject, "WeaponDetails_bg/WeaponStats_bg/spd_bg").GetComponent<UISprite>();
		accBg = commonScreenObject.findChild(base.gameObject, "WeaponDetails_bg/WeaponStats_bg/acc_bg").GetComponent<UISprite>();
		magBg = commonScreenObject.findChild(base.gameObject, "WeaponDetails_bg/WeaponStats_bg/mag_bg").GetComponent<UISprite>();
		goldLabel = commonScreenObject.findChild(base.gameObject, "PlayerGold_bg/PlayerGold_label").GetComponent<UILabel>();
		sellButton = commonScreenObject.findChild(base.gameObject, "SellButton").GetComponent<UIButton>();
		notSellButton = commonScreenObject.findChild(base.gameObject, "NotSellButton").GetComponent<UIButton>();
		isTutorial = false;
	}

	private void Update()
	{
	}

	public void processClick(string gameObjectName)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		switch (gameObjectName)
		{
		case "NotSellButton":
		{
			hideAnims();
			string textByRefId = gameData.getTextByRefId("weaponOffers05");
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeYesAndNo, resume: false, gameData.getTextByRefId("weaponOffers10"), textByRefId, PopupType.PopupTypeOfferNoSell, null, colorTag: false, null, map: false, string.Empty);
			break;
		}
		case "SellButton":
		{
			if (isTutorial)
			{
				GameObject gameObject = GameObject.Find("Panel_Tutorial");
				if (gameObject != null)
				{
					GUITutorialController component = gameObject.GetComponent<GUITutorialController>();
					if (component.checkCurrentTutorial("40004"))
					{
						component.nextTutorial();
					}
				}
			}
			hideAnims();
			Project project = sellList[sellListSelection];
			Offer offer = project.getOfferList()[selectedIndex];
			Hero jobClassByRefId = gameData.getJobClassByRefId(offer.getHeroRefId());
			List<string> list = new List<string>();
			list.Add("[heroName]");
			list.Add("[price]");
			List<string> list2 = new List<string>();
			list2.Add(jobClassByRefId.getHeroName());
			list2.Add("$" + CommonAPI.formatNumber(offer.getPrice()));
			string textByRefIdWithDynTextList = gameData.getTextByRefIdWithDynTextList("weaponOffers06", list, list2);
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeYesAndNo, resume: false, gameData.getTextByRefId("weaponOffers08").ToUpper(CultureInfo.InvariantCulture), textByRefIdWithDynTextList, PopupType.PopupTypeOfferSell, null, colorTag: false, null, map: false, string.Empty);
			break;
		}
		default:
		{
			string[] array = gameObjectName.Split('_');
			if (array[0] == "Offer")
			{
				selectedIndex = CommonAPI.parseInt(array[1]);
				audioController.playHeroSelectAudio();
				selectOffer(selectedIndex);
			}
			break;
		}
		}
	}

	public void processHover(bool isOver, string hoverName)
	{
		if (isOver)
		{
			switch (hoverName)
			{
			case "SmithInfo_texture":
				tooltipScript.showText(smith.getSmithStandardInfoString(showFullJobDetails: false));
				return;
			case "AreaEventIcon_bg":
				if (area.getCurrentEventRefId() != string.Empty)
				{
					tooltipScript.showText(area.getCurrentEventTooltipInfo(game.getPlayer().getPlayerTimeLong()));
				}
				else
				{
					tooltipScript.showText(game.getGameData().getTextByRefId("weaponOffers09"));
				}
				return;
			}
			string[] array = hoverName.Split('_');
			if (array[0] == "Offer")
			{
				audioController.playHeroHoverAudio();
				int hoverIndex = CommonAPI.parseInt(array[1]);
				hoverOffer(hoverIndex);
			}
		}
		else
		{
			hoverOffer(-1);
			tooltipScript.setInactive();
		}
	}

	public void returnToPopup()
	{
		showAnims();
	}

	public void nextWeapon(bool doSell)
	{
		GameData gameData = game.getGameData();
		if (doSell)
		{
			Project project = sellList[sellListSelection];
			int num = selectedIndex;
			List<Offer> offerList = sellList[sellListSelection].getOfferList();
			if (num != -1)
			{
				Offer offer = project.getOfferList()[num];
				project.setBuyer(gameData.getJobClassByRefId(offer.getHeroRefId()));
				project.setFinalPrice(offer.getPrice());
				project.setFinalScore(offer.getWeaponScore());
				project.setSelectedOffer(offer);
				acceptedHeroList.Add(offer.getHeroRefId());
			}
		}
		sellListSelection++;
		if (sellListSelection < sellList.Count)
		{
			refreshList(sellListSelection);
			showOffer();
			return;
		}
		Player player = game.getPlayer();
		string randomTextBySetRefIdWithDynText = gameData.getRandomTextBySetRefIdWithDynText("whetsappSmithOffersConfirmed", "[areaName]", smith.getExploreArea().getAreaName());
		gameData.addNewWhetsappMsg(smith.getSmithName(), randomTextBySetRefIdWithDynText, "Image/Smith/Portraits/" + smith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
		if (shopMenuController.tryStartTutorial("OFFER_SELECTED"))
		{
			viewController.closeOfferWeapon(hide: true, resume: false);
		}
		else
		{
			viewController.closeOfferWeapon(hide: true, resume: true);
		}
	}

	public void setOffer(Smith aSmith)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		smith = aSmith;
		area = aSmith.getExploreArea();
		sellList = game.getPlayer().getCompletedProjectListById(smith.getExploreTask());
		sellListSelection = 0;
		commonScreenObject.findChild(base.gameObject, "Title_bg/Title_label").GetComponent<UILabel>().text = gameData.getTextByRefId("weaponOffers01");
		commonScreenObject.findChild(base.gameObject, "SellButton").GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("weaponOffers03");
		commonScreenObject.findChild(base.gameObject, "NotSellButton").GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("weaponOffers04");
		smithLabel.text = smith.getSmithName() + "\n" + gameData.getTextByRefId("smithStats17") + " " + CommonAPI.getMerchantLevel(smith.getMerchantExp());
		smithTexture.mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/" + smith.getImage());
		locationLabel.text = gameData.getTextByRefId("location") + ":\n" + area.getAreaName();
		if (area.getCurrentEventRefId() != string.Empty)
		{
			areaEventIcon.color = Color.white;
			commonScreenObject.findChild(areaEventIcon.gameObject, "AreaEventIcon_icon").GetComponent<UISprite>().color = Color.white;
		}
		else
		{
			areaEventIcon.color = Color.black;
			commonScreenObject.findChild(areaEventIcon.gameObject, "AreaEventIcon_icon").GetComponent<UISprite>().color = Color.black;
		}
		commonScreenObject.findChild(base.gameObject, "PlayerGold_bg/PlayerGold_title").GetComponent<UILabel>().text = gameData.getTextByRefId("playerStats12");
		goldLabel.text = CommonAPI.formatNumber(player.getPlayerGold());
		setWeaponOffers();
		refreshList(sellListSelection);
		showOffer();
	}

	private void showOffer()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		animShader = Resources.Load("Custom Shader/Alpha Blended - QuestSelect Hero") as Shader;
		Project project = sellList[sellListSelection];
		setWeapon(project);
		clearOfferObjectList();
		List<Offer> offerList = project.getOfferList();
		for (int i = 0; i < offerList.Count; i++)
		{
			Offer offer = offerList[i];
			Vector3 heroPosition = getHeroPosition(i);
			Vector3 heroScale = getHeroScale(i);
			GameObject gameObject = commonScreenObject.createPrefab(heroListObj, offerPrefix + i, "Prefab/OfferWeapon/OfferHeroListObj", heroPosition, Vector3.one, Vector3.zero);
			Hero jobClassByRefId = gameData.getJobClassByRefId(offer.getHeroRefId());
			GameObject gameObject2 = commonScreenObject.createPrefab(commonScreenObject.findChild(gameObject, "Hero_scaler").gameObject, "offerAnim_" + sellListSelection + "_" + jobClassByRefId.getImage(), "Animation/Hero/" + jobClassByRefId.getImage() + "/" + jobClassByRefId.getImage() + "_animObj", Vector3.zero, heroScale, Vector3.zero);
			BoneAnimation componentInChildren = gameObject2.GetComponentInChildren<BoneAnimation>();
			componentInChildren.mMaterials[0].shader = animShader;
			componentInChildren.GetComponent<Renderer>().sortingOrder = 1;
			commonScreenObject.findChild(gameObject, "OfferPrice_bg/OfferPrice_label").GetComponent<UILabel>().text = CommonAPI.formatNumber(offer.getPrice());
			GameObject aObject = commonScreenObject.findChild(gameObject, "ExpectedExp_bg").gameObject;
			commonScreenObject.findChild(aObject, "ExpectedExp_title").GetComponent<UILabel>().text = gameData.getTextByRefId("weaponOffers07").ToUpper(CultureInfo.InvariantCulture);
			UILabel component = commonScreenObject.findChild(aObject, "ExpectedExp_label").GetComponent<UILabel>();
			UISprite component2 = commonScreenObject.findChild(aObject, "ExpectedExpCurrent_bar").GetComponent<UISprite>();
			UISprite component3 = commonScreenObject.findChild(aObject, "ExpectedExpGrowth_bar").GetComponent<UISprite>();
			int expGrowth = offer.getExpGrowth();
			int expPoints = jobClassByRefId.getExpPoints();
			int heroLevel = jobClassByRefId.getHeroLevel();
			if (heroLevel >= jobClassByRefId.getHeroMaxLevel())
			{
				float num3 = (component3.fillAmount = (component2.fillAmount = 1f));
				commonScreenObject.findChild(aObject, "LvUp_bg").GetComponent<UISprite>().alpha = 0f;
				component.text = gameData.getTextByRefIdWithDynText("heroStat08", "[level]", jobClassByRefId.getHeroLevel() + " " + gameData.getTextByRefId("playerStats08"));
			}
			else
			{
				float expPercent = gameData.getExpPercent(expPoints);
				int heroLevelByExp = gameData.getHeroLevelByExp(expPoints + expGrowth);
				heroLevelByExp = Mathf.Min(heroLevelByExp, jobClassByRefId.getHeroMaxLevel());
				if (heroLevel == heroLevelByExp)
				{
					float expPercent2 = gameData.getExpPercent(expPoints + expGrowth);
					component2.fillAmount = expPercent;
					component3.fillAmount = expPercent2;
					commonScreenObject.findChild(aObject, "LvUp_bg").GetComponent<UISprite>().alpha = 0f;
				}
				else
				{
					component2.fillAmount = expPercent;
					component3.fillAmount = 1f;
					commonScreenObject.findChild(aObject, "LvUp_bg").GetComponent<UISprite>().alpha = 1f;
					if (heroLevelByExp == jobClassByRefId.getHeroMaxLevel())
					{
						commonScreenObject.findChild(aObject, "LvUp_bg/LvUp_label").GetComponent<UILabel>().text = gameData.getTextByRefIdWithDynText("heroStat08", "[level]", gameData.getTextByRefId("playerStats08"));
					}
					else
					{
						commonScreenObject.findChild(aObject, "LvUp_bg/LvUp_label").GetComponent<UILabel>().text = gameData.getTextByRefIdWithDynText("heroStat08", "[level]", "+" + (heroLevelByExp - heroLevel) + "!");
					}
				}
				component.text = gameData.getTextByRefIdWithDynText("heroStat08", "[level]", jobClassByRefId.getHeroLevel().ToString());
			}
			UILabel component4 = commonScreenObject.findChild(gameObject, "HeroInfo_label").GetComponent<UILabel>();
			component4.text = jobClassByRefId.getHeroStandardInfoString(smith.getExploreArea());
			component4.alpha = 0f;
			offerObjectList.Add(gameObject);
		}
		heroListBg.width = 624 + (offerList.Count - 1) / 2 * 220;
		weaponNum.text = sellListSelection + 1 + " OF " + sellList.Count;
		if (isTutorial)
		{
			notSellButton.isEnabled = false;
		}
		else
		{
			notSellButton.isEnabled = true;
		}
		selectOffer(acceptedOfferList[sellListSelection]);
	}

	private Vector3 getHeroPosition(int i)
	{
		Vector3 zero = Vector3.zero;
		if (i % 2 == 0)
		{
			zero.x = (float)(i / 2) * 120f + 220f;
			zero.y = (2f - (float)(i / 2)) * 30f - 30f;
			zero.z = (float)(i / 2) * -10f;
		}
		else
		{
			zero.x = -1f * ((float)(i / 2) * 120f + 220f);
			zero.y = (2f - (float)(i / 2)) * 30f - 30f;
			zero.z = (float)(i / 2) * -10f;
		}
		return zero;
	}

	private Vector3 getHeroScale(int i)
	{
		Vector3 one = Vector3.one;
		if (i % 2 == 0)
		{
			one.x = 1f;
		}
		else
		{
			one.x = -1f;
		}
		return one;
	}

	private void selectOffer(int aIndex)
	{
		if (acceptedOfferList[sellListSelection] != aIndex)
		{
			acceptedOfferList[sellListSelection] = aIndex;
		}
		int num = 0;
		foreach (GameObject offerObject in offerObjectList)
		{
			UISprite component = commonScreenObject.findChild(offerObject, "SelectedGlow_bg").GetComponent<UISprite>();
			if (num == acceptedOfferList[sellListSelection])
			{
				component.spriteName = "highlight_selected";
				component.alpha = 1f;
				Vector3 heroPosition = getHeroPosition(num);
				heroPosition.z = -50f;
				offerObject.transform.localPosition = heroPosition;
			}
			else
			{
				component.spriteName = "highlight_hover";
				component.alpha = 0f;
				Vector3 heroPosition2 = getHeroPosition(num);
				offerObject.transform.localPosition = heroPosition2;
			}
			num++;
		}
		if (acceptedOfferList[sellListSelection] != -1)
		{
			sellButton.isEnabled = true;
		}
		else
		{
			sellButton.isEnabled = false;
		}
	}

	private void hoverOffer(int hoverIndex)
	{
		GameData gameData = game.getGameData();
		int num = 0;
		foreach (GameObject offerObject in offerObjectList)
		{
			UISprite component = commonScreenObject.findChild(offerObject, "SelectedGlow_bg").GetComponent<UISprite>();
			GameObject gameObject = commonScreenObject.findChild(offerObject, "HeroInfo_label").gameObject;
			if (num == hoverIndex)
			{
				if (num != acceptedOfferList[sellListSelection])
				{
					component.spriteName = "highlight_hover";
					component.alpha = 1f;
				}
				commonScreenObject.playAnimationImmediate(offerObject.GetComponentInChildren<BoneAnimation>(), "inter");
				commonScreenObject.queueAnimation(offerObject.GetComponentInChildren<BoneAnimation>(), "idle");
				TweenAlpha component2 = gameObject.GetComponent<TweenAlpha>();
				commonScreenObject.tweenAlpha(component2, gameObject.GetComponent<UILabel>().alpha, 1f, 0.3f, null, string.Empty);
			}
			else
			{
				if (num != acceptedOfferList[sellListSelection])
				{
					component.spriteName = "highlight_hover";
					component.alpha = 0f;
				}
				commonScreenObject.playAnimationImmediate(offerObject.GetComponentInChildren<BoneAnimation>(), "idle");
				TweenAlpha component3 = gameObject.GetComponent<TweenAlpha>();
				commonScreenObject.tweenAlpha(component3, gameObject.GetComponent<UILabel>().alpha, 0f, 0.3f, null, string.Empty);
			}
			num++;
		}
	}

	private void clearOfferObjectList()
	{
		offerObjectList.Clear();
		while (heroListObj.transform.childCount > 0)
		{
			commonScreenObject.destroyPrefabImmediate(heroListObj.transform.GetChild(0).gameObject);
		}
	}

	private void setWeapon(Project project)
	{
		weaponNameLabel.text = project.getProjectName(includePrefix: true);
		weaponImage.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + project.getProjectWeapon().getImage());
		weaponParticles.Play();
		atkLabel.text = project.getAtk().ToString();
		spdLabel.text = project.getSpd().ToString();
		accLabel.text = project.getAcc().ToString();
		magLabel.text = project.getMag().ToString();
		atkLabel.fontSize = 14;
		spdLabel.fontSize = 14;
		accLabel.fontSize = 14;
		magLabel.fontSize = 14;
		atkBg.color = new Color(0.0235f, 0.157f, 0.196f);
		spdBg.color = new Color(0.0235f, 0.157f, 0.196f);
		accBg.color = new Color(0.0235f, 0.157f, 0.196f);
		magBg.color = new Color(0.0235f, 0.157f, 0.196f);
		List<WeaponStat> priSecStat = project.getPriSecStat();
		if (priSecStat.Count > 0)
		{
			switch (priSecStat[0])
			{
			case WeaponStat.WeaponStatAttack:
				atkLabel.effectStyle = UILabel.Effect.Outline;
				atkLabel.fontSize = 15;
				atkBg.color = new Color(0.0196f, 0.788f, 0.659f);
				break;
			case WeaponStat.WeaponStatSpeed:
				spdLabel.effectStyle = UILabel.Effect.Outline;
				spdLabel.fontSize = 15;
				spdBg.color = new Color(0.0196f, 0.788f, 0.659f);
				break;
			case WeaponStat.WeaponStatAccuracy:
				accLabel.effectStyle = UILabel.Effect.Outline;
				accLabel.fontSize = 15;
				accBg.color = new Color(0.0196f, 0.788f, 0.659f);
				break;
			case WeaponStat.WeaponStatMagic:
				magLabel.effectStyle = UILabel.Effect.Outline;
				magLabel.fontSize = 15;
				magBg.color = new Color(0.0196f, 0.788f, 0.659f);
				break;
			}
		}
		if (priSecStat.Count > 1)
		{
			switch (priSecStat[1])
			{
			case WeaponStat.WeaponStatAttack:
				atkBg.color = new Color(0.0235f, 0.522f, 0.439f);
				break;
			case WeaponStat.WeaponStatSpeed:
				spdBg.color = new Color(0.0235f, 0.522f, 0.439f);
				break;
			case WeaponStat.WeaponStatAccuracy:
				accBg.color = new Color(0.0235f, 0.522f, 0.439f);
				break;
			case WeaponStat.WeaponStatMagic:
				magBg.color = new Color(0.0235f, 0.522f, 0.439f);
				break;
			}
		}
	}

	public void setWeaponOffers()
	{
		List<string> exploreTask = smith.getExploreTask();
		List<Project> completedProjectListById = game.getPlayer().getCompletedProjectListById(exploreTask);
		acceptedOfferList = new List<int>();
		for (int i = 0; i < completedProjectListById.Count; i++)
		{
			acceptedOfferList.Add(-1);
		}
	}

	private void refreshList(int selection)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string formulaConstantsSet = gameScenarioByRefId.getFormulaConstantsSet();
		Project project = sellList[selection];
		acceptedOfferList[selection] = -1;
		int merchantLevel = CommonAPI.getMerchantLevel(smith.getMerchantExp());
		int num = 4;
		List<Offer> list = new List<Offer>();
		List<string> heroRefIdList = area.getHeroRefIdList();
		isTutorial = false;
		GameObject gameObject = GameObject.Find("Panel_Tutorial");
		if (gameObject != null)
		{
			GUITutorialController component = gameObject.GetComponent<GUITutorialController>();
			isTutorial = component.checkCurrentTutorial("40001");
			if (isTutorial)
			{
				component.nextTutorial();
			}
		}
		int num2 = 0;
		foreach (string item in heroRefIdList)
		{
			if ((!isTutorial || item == "10011" || item == "10022") && num2 < num && !acceptedHeroList.Contains(item))
			{
				Hero jobClassByRefId = gameData.getJobClassByRefId(item);
				float floatConstantByRefID = gameData.getFloatConstantByRefID(formulaConstantsSet + "_LTC_MULT");
				float floatConstantByRefID2 = gameData.getFloatConstantByRefID(formulaConstantsSet + "_LTC_CONST");
				float num3 = floatConstantByRefID * (float)jobClassByRefId.getHeroLevel() + floatConstantByRefID2;
				float floatConstantByRefID3 = gameData.getFloatConstantByRefID(formulaConstantsSet + "_HSP_MULT");
				float floatConstantByRefID4 = gameData.getFloatConstantByRefID(formulaConstantsSet + "_HSP_CONST");
				float floatConstantByRefID5 = gameData.getFloatConstantByRefID(formulaConstantsSet + "_HSP_BASE");
				float num4 = floatConstantByRefID4 + floatConstantByRefID3 * Mathf.Pow(floatConstantByRefID5, num3 - 1f) + 0.5f;
				Dictionary<string, int> dictionary = calculateHeroWeaponScore(jobClassByRefId, project);
				int aWeaponScore = dictionary["weaponScore"];
				int num5 = dictionary["weaponScoreNoCap"];
				int num6 = calculateHeroExpGain(jobClassByRefId, project, num5);
				string text = "_HSPLOW_POWER";
				string text2 = "_HSPLOW_SCOREPOWER";
				string text3 = "_HSPLOW_SCOREMULT";
				string text4 = "_HSPLOW_SCORECONST";
				if (num5 > 10)
				{
					text = "_HSPHIGH_POWER";
					text2 = "_HSPHIGH_SCOREPOWER";
					text3 = "_HSPHIGH_SCOREMULT";
					text4 = "_HSPHIGH_SCORECONST";
				}
				float floatConstantByRefID6 = gameData.getFloatConstantByRefID(formulaConstantsSet + text);
				float floatConstantByRefID7 = gameData.getFloatConstantByRefID(formulaConstantsSet + text2);
				float floatConstantByRefID8 = gameData.getFloatConstantByRefID(formulaConstantsSet + text3);
				float floatConstantByRefID9 = gameData.getFloatConstantByRefID(formulaConstantsSet + text4);
				int a = (int)(num4 * Mathf.Pow((float)num5 * floatConstantByRefID8 + floatConstantByRefID9, floatConstantByRefID7) + Mathf.Pow(num4, floatConstantByRefID6));
				a = Mathf.Max(a, 1);
				float currentEventExpMult = area.getCurrentEventExpMult(item, project.getProjectWeapon().getWeaponTypeRefId());
				float currentEventStarchMult = area.getCurrentEventStarchMult(item, project.getProjectWeapon().getWeaponTypeRefId());
				num6 = (int)((float)num6 * currentEventExpMult);
				a = (int)((float)a * currentEventStarchMult);
				list.Add(new Offer(project.getProjectId() + "0" + num2, project.getProjectId(), item, a, aWeaponScore, num6, currentEventStarchMult, currentEventExpMult));
				num2++;
			}
		}
		project.setOfferList(list);
	}

	public Dictionary<string, int> calculateHeroWeaponScore(Hero hero, Project sellProj)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string formulaConstantsSet = gameScenarioByRefId.getFormulaConstantsSet();
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		int atk = sellProj.getAtk();
		int spd = sellProj.getSpd();
		int acc = sellProj.getAcc();
		int mag = sellProj.getMag();
		float floatConstantByRefID = gameData.getFloatConstantByRefID(formulaConstantsSet + "_JCB_POWER");
		float floatConstantByRefID2 = gameData.getFloatConstantByRefID(formulaConstantsSet + "_JCB_BASE");
		float floatConstantByRefID3 = gameData.getFloatConstantByRefID(formulaConstantsSet + "_JCB_MULT");
		float floatConstantByRefID4 = gameData.getFloatConstantByRefID(formulaConstantsSet + "_JCB_OFFSET");
		int heroLevel = hero.getHeroLevel();
		float num = floatConstantByRefID2 * Mathf.Pow(0.043f * (float)heroLevel + 0.7f, floatConstantByRefID) + floatConstantByRefID3;
		float num2 = hero.getBaseAtk() * num + floatConstantByRefID4;
		float num3 = hero.getBaseSpd() * num + floatConstantByRefID4;
		float num4 = hero.getBaseAcc() * num + floatConstantByRefID4;
		float num5 = 0f;
		if (hero.getBaseMag() != 0f)
		{
			num5 = hero.getBaseMag() * num + floatConstantByRefID4;
		}
		float floatConstantByRefID5 = gameData.getFloatConstantByRefID(formulaConstantsSet + "_PRISTAT_MULT");
		float floatConstantByRefID6 = gameData.getFloatConstantByRefID(formulaConstantsSet + "_SECSTAT_MULT");
		float floatConstantByRefID7 = gameData.getFloatConstantByRefID(formulaConstantsSet + "_OVERALLSTAT_MULT");
		float num6 = 0f;
		switch (hero.getPriStat())
		{
		case WeaponStat.WeaponStatAttack:
			num6 = floatConstantByRefID5 * (float)atk / num2;
			break;
		case WeaponStat.WeaponStatSpeed:
			num6 = floatConstantByRefID5 * (float)spd / num3;
			break;
		case WeaponStat.WeaponStatAccuracy:
			num6 = floatConstantByRefID5 * (float)acc / num4;
			break;
		case WeaponStat.WeaponStatMagic:
			num6 = floatConstantByRefID5 * (float)mag / num5;
			break;
		}
		float num7 = 0f;
		switch (hero.getSecStat())
		{
		case WeaponStat.WeaponStatAttack:
			num7 = floatConstantByRefID6 * (float)atk / num2;
			break;
		case WeaponStat.WeaponStatSpeed:
			num7 = floatConstantByRefID6 * (float)spd / num3;
			break;
		case WeaponStat.WeaponStatAccuracy:
			num7 = floatConstantByRefID6 * (float)acc / num4;
			break;
		case WeaponStat.WeaponStatMagic:
			num7 = floatConstantByRefID6 * (float)mag / num5;
			break;
		}
		float num8 = floatConstantByRefID7 * (float)sellProj.getTotalStat() / num;
		int num9 = (int)(num6 + num7 + num8);
		int num10 = Mathf.Min(num9, 6);
		List<WeaponStat> preferredStats = hero.getPreferredStats();
		List<WeaponStat> priSecStat = sellProj.getPriSecStat();
		int num11 = 0;
		int num12 = 0;
		if (preferredStats[0] == priSecStat[0])
		{
			num11 = 1;
			if (preferredStats[1] == WeaponStat.WeaponStatNone)
			{
				num12 = 1;
			}
		}
		if (preferredStats[1] == priSecStat[1])
		{
			num12 = 1;
		}
		string weaponTypeRefId = sellProj.getProjectWeapon().getWeaponType().getWeaponTypeRefId();
		int affinity = hero.getAffinity(weaponTypeRefId);
		int num13 = 0;
		switch (affinity)
		{
		case 1:
			num13 = 0;
			break;
		case 3:
			num13 = 1;
			break;
		}
		int completedWeaponCountByWeaponTypeRefId = game.getPlayer().getCompletedWeaponCountByWeaponTypeRefId(weaponTypeRefId);
		float num14 = 1f / 30f * (float)(num10 + num11 + num12 + num13) + (0.675f + -2.4f * Mathf.Pow(completedWeaponCountByWeaponTypeRefId, -0.8f));
		if (num14 > 0.95f)
		{
			num14 = 0.95f;
		}
		if (num14 < 0f)
		{
			num14 = 0f;
		}
		int num15 = Mathf.FloorToInt(Random.Range(0f, 1f) + num14);
		int num16 = num10 + num11 + num12 + num13 + num15;
		int num17 = num9 + num11 + num12 + num13 + num15;
		if (affinity == 1)
		{
			num16 = (int)((float)num16 * (1f / 30f));
			num17 = (int)((float)num17 * (1f / 30f));
		}
		dictionary.Add("weaponScore", num16);
		dictionary.Add("weaponScoreNoCap", num17);
		return dictionary;
	}

	private int calculateHeroExpGain(Hero hero, Project project, int uncappedScore)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string formulaConstantsSet = gameScenarioByRefId.getFormulaConstantsSet();
		float floatConstantByRefID = gameData.getFloatConstantByRefID(formulaConstantsSet + "_SCORE_CONST");
		int heroLevel = hero.getHeroLevel();
		int num = 250;
		int num2 = 200;
		if (heroLevel > 1)
		{
			num = gameData.getTotalExpByHeroLevel(heroLevel);
			num2 = 0;
		}
		int heroTier = hero.getHeroTier();
		float floatConstantByRefID2 = gameData.getFloatConstantByRefID(formulaConstantsSet + "_TIERLOW_CONST");
		if (heroTier <= 2)
		{
			floatConstantByRefID2 = gameData.getFloatConstantByRefID(formulaConstantsSet + "_TIERHIGH_CONST");
		}
		return (int)((float)num * (floatConstantByRefID2 / (float)heroTier) * floatConstantByRefID * (float)uncappedScore) + num2;
	}

	private void hideAnims()
	{
		pauseParticles = base.gameObject.GetComponentsInChildren<ParticleSystem>();
		ParticleSystem[] array = pauseParticles;
		foreach (ParticleSystem particleSystem in array)
		{
			if (particleSystem.isPlaying)
			{
				particleSystem.Stop();
				particleSystem.Clear();
			}
		}
		pauseAnims = base.gameObject.GetComponentsInChildren<BoneAnimation>();
		BoneAnimation[] array2 = pauseAnims;
		foreach (BoneAnimation boneAnimation in array2)
		{
			boneAnimation.transform.localPosition = new Vector3(1000f, 1000f, 1000f);
		}
	}

	private void showAnims()
	{
		ParticleSystem[] array = pauseParticles;
		foreach (ParticleSystem particleSystem in array)
		{
			particleSystem.Play();
		}
		pauseAnims = base.gameObject.GetComponentsInChildren<BoneAnimation>();
		BoneAnimation[] array2 = pauseAnims;
		foreach (BoneAnimation boneAnimation in array2)
		{
			boneAnimation.transform.localPosition = Vector3.zero;
		}
	}
}
