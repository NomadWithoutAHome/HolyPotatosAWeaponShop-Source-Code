using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIBoostController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private ViewController viewController;

	private GameObject boostBg;

	private UISprite boostTitleBg;

	private UILabel boostTitleLabel;

	private UITexture smithImg;

	private GameObject boostAmtBg;

	private UILabel boostAmt;

	private TweenScale boostAmtTween;

	private ParticleSystem boostAmtFirework;

	private TweenScale weaponStatsScale;

	private UILabel statsTitle;

	private UILabel atkValue;

	private TweenScale atkValueTween;

	private UILabel spdValue;

	private TweenScale spdValueTween;

	private UILabel accValue;

	private TweenScale accValueTween;

	private UILabel magValue;

	private TweenScale magValueTween;

	private UISprite animSprite;

	private UISpriteAnimation anim;

	private ParticleSystem animParticles;

	private TweenScale enchantItemScale;

	private UITexture itemImage;

	private UILabel itemLabel;

	private TweenScale bubbleScale;

	private UILabel bubbleLabel;

	private UIGrid starGrid;

	private string movePointObj;

	private string statStarObj;

	private GameObject starStart;

	private GameObject starEnd;

	private List<GameObject> starObjList;

	private List<GameObject> starGridList;

	private ProcessPopupType currentType;

	private Smith smith;

	private string smithText;

	private List<int> statsBefore;

	private List<int> statsAfter;

	private List<Boost> boostList;

	private int noOfBoost;

	private Boost boostEnchant;

	private Item itemEnchant;

	private Color statsAddColor;

	private int currentBoost;

	private int percentage;

	private bool hasLevelUp;

	private bool isAnimating;

	private string animPhase;

	private bool isSkipped;

	private bool forceEnd;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		boostBg = commonScreenObject.findChild(base.gameObject, "Boost_bg").gameObject;
		boostTitleBg = commonScreenObject.findChild(boostBg, "BoostTitle_bg").GetComponent<UISprite>();
		boostTitleLabel = commonScreenObject.findChild(boostBg, "BoostTitle_bg/BoostTitle_label").GetComponent<UILabel>();
		smithImg = commonScreenObject.findChild(boostBg, "BoostAnim_bg/SmithImg").GetComponent<UITexture>();
		boostAmtBg = commonScreenObject.findChild(boostBg, "BoostAmtBg").gameObject;
		boostAmt = commonScreenObject.findChild(boostBg, "BoostAmtBg/BoostAmt").GetComponent<UILabel>();
		boostAmtTween = commonScreenObject.findChild(boostBg, "BoostAmtBg/BoostAmt").GetComponent<TweenScale>();
		boostAmtFirework = commonScreenObject.findChild(boostBg, "BoostAmtBg/BoostAmtFirework").GetComponent<ParticleSystem>();
		GameObject gameObject = commonScreenObject.findChild(boostBg, "WeaponStats").gameObject;
		weaponStatsScale = gameObject.GetComponent<TweenScale>();
		statsTitle = commonScreenObject.findChild(gameObject, "WeaponStats_title").GetComponent<UILabel>();
		atkValue = commonScreenObject.findChild(gameObject, "atk_sprite/atk_label").GetComponent<UILabel>();
		atkValueTween = commonScreenObject.findChild(gameObject, "atk_sprite/atk_label").GetComponent<TweenScale>();
		spdValue = commonScreenObject.findChild(gameObject, "spd_sprite/spd_label").GetComponent<UILabel>();
		spdValueTween = commonScreenObject.findChild(gameObject, "spd_sprite/spd_label").GetComponent<TweenScale>();
		accValue = commonScreenObject.findChild(gameObject, "acc_sprite/acc_label").GetComponent<UILabel>();
		accValueTween = commonScreenObject.findChild(gameObject, "acc_sprite/acc_label").GetComponent<TweenScale>();
		magValue = commonScreenObject.findChild(gameObject, "mag_sprite/mag_label").GetComponent<UILabel>();
		magValueTween = commonScreenObject.findChild(gameObject, "mag_sprite/mag_label").GetComponent<TweenScale>();
		animSprite = commonScreenObject.findChild(boostBg, "BoostAnim_bg/BoostAnim_scaler/BoostAnim_anim").GetComponent<UISprite>();
		anim = commonScreenObject.findChild(boostBg, "BoostAnim_bg/BoostAnim_scaler/BoostAnim_anim").GetComponent<UISpriteAnimation>();
		animParticles = commonScreenObject.findChild(boostBg, "BoostAnim_bg/BoostAnim_particles").GetComponent<ParticleSystem>();
		enchantItemScale = commonScreenObject.findChild(boostBg, "EnchantItem").GetComponent<TweenScale>();
		itemImage = commonScreenObject.findChild(boostBg, "EnchantItem/EnchantItem_texture").GetComponent<UITexture>();
		itemLabel = commonScreenObject.findChild(boostBg, "EnchantItem/EnchantItem_label").GetComponent<UILabel>();
		bubbleScale = commonScreenObject.findChild(boostBg, "BoostBubble_label").GetComponent<TweenScale>();
		bubbleLabel = commonScreenObject.findChild(boostBg, "BoostBubble_label").GetComponent<UILabel>();
		starGrid = commonScreenObject.findChild(boostBg, "StatStarGrid").GetComponent<UIGrid>();
		movePointObj = "Prefab/ForgeMenu/ForgeMenuNEW DEC2014/StatMovePoint";
		statStarObj = "Prefab/ForgeMenu/ForgeMenuNEW DEC2014/StatStarObj";
		currentBoost = 0;
		statsAddColor = new Color32(13, 123, 15, byte.MaxValue);
		percentage = 0;
		isAnimating = false;
		animPhase = string.Empty;
		isSkipped = false;
		forceEnd = false;
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName == "BoostAnim_bg")
		{
			if (!isSkipped && animPhase != "TALK" && animPhase != "FINISH")
			{
				StartCoroutine(skipStarAnims());
			}
			else if (animPhase == "FINISH")
			{
				forceEnd = true;
				closeBoostPopup();
			}
		}
	}

	public void showPopup(ProcessPopupType type, Smith selectedSmith, string afterBoostText, List<int> projectBefore, List<int> projectAfter, List<Boost> aBoostList, bool aHasLevelUp, Boost enchantBoost = null, Item enchantItem = null)
	{
		GameData gameData = game.getGameData();
		starStart = commonScreenObject.createPrefab(boostBg, "starStart", movePointObj, Vector3.zero, Vector3.one, Vector3.zero);
		starEnd = commonScreenObject.createPrefab(GameObject.Find("Panel_ForgeProgressNew").gameObject, "starEnd", movePointObj, new Vector3(-140f, -195f, 0f), Vector3.one, Vector3.zero);
		starObjList = new List<GameObject>();
		starGridList = new List<GameObject>();
		switch (type)
		{
		case ProcessPopupType.ProcessPopupTypeDesign:
			boostTitleBg.spriteName = "ribbon_red";
			boostTitleLabel.text = gameData.getTextByRefId("projectProgress02").ToUpper(CultureInfo.InvariantCulture);
			break;
		case ProcessPopupType.ProcessPopupTypeCraft:
			boostTitleBg.spriteName = "ribbon_green";
			boostTitleLabel.text = gameData.getTextByRefId("projectProgress03").ToUpper(CultureInfo.InvariantCulture);
			break;
		case ProcessPopupType.ProcessPopupTypePolish:
			boostTitleBg.spriteName = "ribbon_blue";
			boostTitleLabel.text = gameData.getTextByRefId("projectProgress04").ToUpper(CultureInfo.InvariantCulture);
			break;
		case ProcessPopupType.ProcessPopupTypeEnchant:
			boostTitleBg.spriteName = "ribbon_yellow";
			boostTitleLabel.text = gameData.getTextByRefId("projectProgress05").ToUpper(CultureInfo.InvariantCulture);
			break;
		}
		boostAmt.text = string.Empty;
		currentType = type;
		smith = selectedSmith;
		smithText = afterBoostText;
		statsBefore = projectBefore;
		statsAfter = projectAfter;
		boostList = aBoostList;
		noOfBoost = boostList.Count;
		hasLevelUp = aHasLevelUp;
		boostEnchant = enchantBoost;
		switch (currentType)
		{
		case ProcessPopupType.ProcessPopupTypeDesign:
			animSprite.spriteName = "Design_00";
			anim.namePrefix = "Design_";
			animParticles.transform.localPosition = new Vector3(-5f, -5f, 0f);
			break;
		case ProcessPopupType.ProcessPopupTypeCraft:
			animSprite.spriteName = "Craft_00";
			anim.namePrefix = "Craft_";
			animParticles.transform.localPosition = new Vector3(0f, -10f, 0f);
			break;
		case ProcessPopupType.ProcessPopupTypePolish:
			animSprite.spriteName = "Polish_00";
			anim.namePrefix = "Polish_";
			animParticles.transform.localPosition = Vector3.zero;
			break;
		case ProcessPopupType.ProcessPopupTypeEnchant:
			animSprite.spriteName = "Enchant_00";
			anim.namePrefix = "Enchant_";
			animParticles.transform.localPosition = new Vector3(24f, -30f, 0f);
			break;
		}
		smithImg.mainTexture = commonScreenObject.loadTexture("Image/Smith/" + smith.getImage() + "_manage");
		itemLabel.text = string.Empty;
		itemImage.mainTexture = null;
		bubbleLabel.text = CommonAPI.generateAfterBoostText(noOfBoost);
		statsTitle.text = gameData.getTextByRefId("enchantItem02");
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		foreach (Boost boost in boostList)
		{
			num += boost.getBoostAtk();
			num2 += boost.getBoostSpd();
			num3 += boost.getBoostAcc();
			num4 += boost.getBoostMag();
		}
		if (num > 0)
		{
			atkValue.text = "[56AE59]+" + CommonAPI.formatNumber(num) + "[-]";
		}
		else
		{
			atkValue.text = CommonAPI.formatNumber(num);
		}
		if (num2 > 0)
		{
			spdValue.text = "[56AE59]+" + CommonAPI.formatNumber(num2) + "[-]";
		}
		else
		{
			spdValue.text = CommonAPI.formatNumber(num2);
		}
		if (num3 > 0)
		{
			accValue.text = "[56AE59]+" + CommonAPI.formatNumber(num3) + "[-]";
		}
		else
		{
			accValue.text = CommonAPI.formatNumber(num3);
		}
		if (num4 > 0)
		{
			magValue.text = "[56AE59]+" + CommonAPI.formatNumber(num4) + "[-]";
		}
		else
		{
			magValue.text = CommonAPI.formatNumber(num4);
		}
		audioController.playForgeStartAudio();
		isAnimating = true;
		animPhase = "BOOST";
		StartCoroutine("showBoost");
	}

	private IEnumerator showBoost()
	{
		yield return new WaitForSeconds(0.5f);
		while (isAnimating && !isSkipped)
		{
			if (currentBoost < noOfBoost && !isSkipped)
			{
				if (currentBoost <= 3)
				{
					audioController.playForgeBoostIncreaseLowerAudio();
				}
				else
				{
					audioController.playForgeBoostIncreaseAudio();
				}
				anim.Reset();
				anim.enabled = true;
				anim.framesPerSecond = 15;
				animParticles.maxParticles = (currentBoost + 1) * 15;
				animParticles.Play();
				generateBoostPop(boostList[currentBoost]);
				currentBoost++;
				showBoostNumAnim();
				yield return new WaitForSeconds(1.5f);
			}
			if (animPhase == "BOOST" && currentBoost == noOfBoost && !isSkipped)
			{
				doStarAnimSecondHalf();
				GameObject.Find("Panel_ForgeProgressNew").GetComponent<GUIForgeProgressNewController>().updateWeaponDisplay();
				animPhase = "TALK";
				yield return new WaitForSeconds(1.5f);
			}
			else if (animPhase == "TALK" && !isSkipped)
			{
				animPhase = "FINISH";
				commonScreenObject.tweenScale(bubbleScale, Vector3.zero, Vector3.one, 0.4f, null, string.Empty);
				commonScreenObject.tweenScale(weaponStatsScale, Vector3.zero, Vector3.one, 0.4f, null, string.Empty);
				isAnimating = false;
				if (hasLevelUp)
				{
					popLvlUp();
				}
				yield return new WaitForSeconds(2.5f);
				if (!forceEnd)
				{
					closeBoostPopup();
				}
			}
		}
	}

	private void closeBoostPopup()
	{
		viewController.closeBoostPopup();
		GameObject.Find("Panel_ForgeProgressNew").GetComponent<GUIForgeProgressNewController>().updateBoostDrawer();
		commonScreenObject.clearTooltips();
		viewController.resumeEverything();
	}

	private IEnumerator skipStarAnims()
	{
		isSkipped = true;
		doStarAnimSecondHalf();
		int skipAtk = 0;
		int skipSpd = 0;
		int skipAcc = 0;
		int skipMag = 0;
		for (int i = currentBoost; i < noOfBoost; i++)
		{
			skipAtk += boostList[i].getBoostAtk();
			skipSpd += boostList[i].getBoostSpd();
			skipAcc += boostList[i].getBoostAcc();
			skipMag += boostList[i].getBoostMag();
		}
		currentBoost = noOfBoost;
		showBoostNumAnim();
		game.getPlayer().addCurrentProjectStats(skipAtk, skipSpd, skipAcc, skipMag);
		GameObject.Find("Panel_ForgeProgressNew").GetComponent<GUIForgeProgressNewController>().updateWeaponDisplay();
		animPhase = "FINISH";
		commonScreenObject.tweenScale(bubbleScale, Vector3.zero, Vector3.one, 0.4f, null, string.Empty);
		commonScreenObject.tweenScale(weaponStatsScale, Vector3.zero, Vector3.one, 0.4f, null, string.Empty);
		isAnimating = false;
		if (hasLevelUp)
		{
			popLvlUp();
		}
		yield return new WaitForSeconds(2.5f);
		if (!forceEnd)
		{
			closeBoostPopup();
		}
	}

	private void showBoostNumAnim()
	{
		boostAmt.text = currentBoost.ToString();
		float num = 1f + 0.1f * (float)currentBoost;
		boostAmtBg.transform.localScale = new Vector3(num, num, num);
		boostAmtTween.enabled = true;
		boostAmtTween.ResetToBeginning();
		boostAmtTween.PlayForward();
	}

	private void doStarAnimSecondHalf()
	{
		int num = 0;
		int count = starObjList.Count;
		foreach (GameObject starObj in starObjList)
		{
			starObj.name += "OLD";
			float delay = (float)(count - num - 1) * 0.1f;
			TweenScale component = starObj.GetComponent<TweenScale>();
			component.delay = delay;
			commonScreenObject.tweenScale(component, Vector3.one, Vector3.zero, 0.5f, null, string.Empty);
			TweenPosition component2 = starObj.GetComponent<TweenPosition>();
			component2.delay = delay;
			commonScreenObject.tweenPosition(component2, starObj.transform.localPosition, starEnd.transform.localPosition, 0.5f, null, string.Empty);
			commonScreenObject.destroyPrefabDelay(starObj, 1f);
			num++;
		}
	}

	private void generateBoostPop(Boost currBoost)
	{
		doStarAnimSecondHalf();
		foreach (GameObject starGrid in starGridList)
		{
			commonScreenObject.destroyPrefabImmediate(starGrid);
		}
		starObjList = new List<GameObject>();
		starGridList = new List<GameObject>();
		int num = 0;
		if (currBoost.getBoostAtk() > 0)
		{
			GameObject item = commonScreenObject.createPrefab(this.starGrid.gameObject, "starGrid_" + num, movePointObj, Vector3.zero, Vector3.one, Vector3.zero);
			GameObject gameObject = commonScreenObject.createPrefab(starStart, "starObj_" + num, statStarObj, Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(gameObject, "StatStar_icon").GetComponent<UISprite>().spriteName = "ico_atk";
			commonScreenObject.findChild(gameObject, "StatStar_label").GetComponent<UILabel>().text = "+" + currBoost.getBoostAtk();
			starObjList.Add(gameObject);
			starGridList.Add(item);
			num++;
		}
		if (currBoost.getBoostSpd() > 0)
		{
			GameObject item2 = commonScreenObject.createPrefab(this.starGrid.gameObject, "starGrid_" + num, movePointObj, Vector3.zero, Vector3.one, Vector3.zero);
			GameObject gameObject2 = commonScreenObject.createPrefab(starStart, "starObj_" + num, statStarObj, Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(gameObject2, "StatStar_icon").GetComponent<UISprite>().spriteName = "ico_speed";
			commonScreenObject.findChild(gameObject2, "StatStar_label").GetComponent<UILabel>().text = "+" + currBoost.getBoostSpd();
			starObjList.Add(gameObject2);
			starGridList.Add(item2);
			num++;
		}
		if (currBoost.getBoostAcc() > 0)
		{
			GameObject item3 = commonScreenObject.createPrefab(this.starGrid.gameObject, "starGrid_" + num, movePointObj, Vector3.zero, Vector3.one, Vector3.zero);
			GameObject gameObject3 = commonScreenObject.createPrefab(starStart, "starObj_" + num, statStarObj, Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(gameObject3, "StatStar_icon").GetComponent<UISprite>().spriteName = "ico_acc";
			commonScreenObject.findChild(gameObject3, "StatStar_label").GetComponent<UILabel>().text = "+" + currBoost.getBoostAcc();
			starObjList.Add(gameObject3);
			starGridList.Add(item3);
			num++;
		}
		if (currBoost.getBoostMag() > 0)
		{
			GameObject item4 = commonScreenObject.createPrefab(this.starGrid.gameObject, "starGrid_" + num, movePointObj, Vector3.zero, Vector3.one, Vector3.zero);
			GameObject gameObject4 = commonScreenObject.createPrefab(starStart, "starObj_" + num, statStarObj, Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(gameObject4, "StatStar_icon").GetComponent<UISprite>().spriteName = "ico_enh";
			commonScreenObject.findChild(gameObject4, "StatStar_label").GetComponent<UILabel>().text = "+" + currBoost.getBoostMag();
			starObjList.Add(gameObject4);
			starGridList.Add(item4);
			num++;
		}
		this.starGrid.Reposition();
		game.getPlayer().addCurrentProjectStats(currBoost.getBoostAtk(), currBoost.getBoostSpd(), currBoost.getBoostAcc(), currBoost.getBoostMag());
		foreach (Transform child in this.starGrid.GetChildList())
		{
			child.SetParent(boostBg.transform);
		}
		starEnd.transform.SetParent(boostBg.transform);
		int num2 = 0;
		foreach (GameObject starObj in starObjList)
		{
			starObj.transform.SetParent(boostBg.transform);
			float delay = (float)num2 * 0.1f;
			TweenScale component = starObj.GetComponent<TweenScale>();
			component.delay = delay;
			commonScreenObject.tweenScale(component, Vector3.zero, Vector3.one, 0.5f, null, string.Empty);
			TweenPosition component2 = starObj.GetComponent<TweenPosition>();
			component2.delay = delay;
			commonScreenObject.tweenPosition(component2, starStart.transform.localPosition, starGridList[num2].transform.localPosition, 0.5f, null, string.Empty);
			num2++;
		}
	}

	private void popLvlUp()
	{
		GameObject aObj = commonScreenObject.createPrefab(smithImg.gameObject, "popLvlUp" + smith.getSmithRefId(), "Image/Process bubble/levelup/lvlupAnimSprite", new Vector3(0f, 115f, 0f), new Vector3(-70f, 70f, 70f), Vector3.zero);
		commonScreenObject.destroyPrefabDelay(aObj, 3f);
	}
}
