using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIAddEnchantmentController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private ViewController viewController;

	private UILabel addEnchantTitleLabel;

	private UITexture weaponTexture;

	private ParticleSystem weaponParticles;

	private TweenScale weaponGlow;

	private TweenScale itemScale;

	private UITexture itemTexture;

	private UILabel itemLabel;

	private ParticleSystem itemParticles;

	private ParticleSystem.Particle[] itemParticleList;

	private TweenScale weaponStatScale;

	private UILabel statValue;

	private UISprite statSprite;

	private UILabel prefixValue;

	private GameObject enchantItemBg;

	private UIGrid starGrid;

	private string movePointObj;

	private string statStarObj;

	private GameObject starStart;

	private GameObject starEnd;

	private List<GameObject> starObjList;

	private List<GameObject> starGridList;

	private List<int> statsBefore;

	private List<int> statsAfter;

	private Boost boostEnchant;

	private Color statsAddColor;

	private int currentBoost;

	private string animPhase;

	private bool isSkipped;

	private bool isClosed;

	private float timer;

	private float convergeTime;

	private float speed;

	private float accel;

	private bool doItemParticles;

	private Weapon weapon;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		enchantItemBg = commonScreenObject.findChild(base.gameObject, "EnchantItem_bg").gameObject;
		addEnchantTitleLabel = commonScreenObject.findChild(base.gameObject, "EnchantItem_bg/EnchantItemTitle_bg/EnchantItemTitle_label").GetComponent<UILabel>();
		weaponTexture = commonScreenObject.findChild(base.gameObject, "EnchantItem_bg/EnchantWeapon").GetComponent<UITexture>();
		weaponParticles = commonScreenObject.findChild(weaponTexture.gameObject, "EnchantWeapon_particles").GetComponent<ParticleSystem>();
		weaponGlow = commonScreenObject.findChild(base.gameObject, "EnchantItem_bg/EnchantWeapon_glow").GetComponent<TweenScale>();
		itemScale = commonScreenObject.findChild(base.gameObject, "EnchantItem_bg/EnchantItem").GetComponent<TweenScale>();
		itemTexture = commonScreenObject.findChild(itemScale.gameObject, "EnchantItem_texture").GetComponent<UITexture>();
		itemLabel = commonScreenObject.findChild(itemScale.gameObject, "EnchantItem_label").GetComponent<UILabel>();
		itemParticles = commonScreenObject.findChild(base.gameObject, "EnchantItem_bg/EnchantItem_particles").GetComponent<ParticleSystem>();
		itemParticleList = new ParticleSystem.Particle[50];
		GameObject gameObject = commonScreenObject.findChild(base.gameObject, "EnchantItem_bg/WeaponStats").gameObject;
		weaponStatScale = gameObject.GetComponent<TweenScale>();
		statValue = commonScreenObject.findChild(gameObject, "stat_bg/stat_label").GetComponent<UILabel>();
		statSprite = commonScreenObject.findChild(gameObject, "stat_bg/stat_sprite").GetComponent<UISprite>();
		prefixValue = commonScreenObject.findChild(gameObject, "prefix_bg/prefix_label").GetComponent<UILabel>();
		starGrid = commonScreenObject.findChild(enchantItemBg, "StatStarGrid").GetComponent<UIGrid>();
		movePointObj = "Prefab/ForgeMenu/ForgeMenuNEW DEC2014/StatMovePoint";
		statStarObj = "Prefab/ForgeMenu/ForgeMenuNEW DEC2014/StatStarObj";
		currentBoost = 0;
		statsAddColor = new Color32(13, 123, 15, byte.MaxValue);
		doItemParticles = false;
		animPhase = string.Empty;
		isSkipped = false;
		isClosed = false;
		timer = 0f;
		convergeTime = 0.5f;
		speed = 3f;
		accel = 1f;
	}

	private void Update()
	{
		if (!doItemParticles)
		{
			return;
		}
		timer += Time.deltaTime;
		int num = itemParticleList.Length;
		for (int i = 0; i < num; i++)
		{
			float num2 = Vector3.Distance(itemParticleList[i].position, weaponTexture.transform.position);
			float num3 = speed + accel * (timer - convergeTime);
			if (num3 > 10f)
			{
				num3 = 10f;
			}
			float num4 = Time.deltaTime * num3;
			float t = num4 / num2;
			itemParticleList[i].position = Vector3.Lerp(itemParticleList[i].position, weaponTexture.transform.position, t);
		}
		itemParticles.SetParticles(itemParticleList, itemParticleList.Length);
	}

	public void processClick(string gameObjectName)
	{
		if (!(gameObjectName == "EnchantItem_bg"))
		{
			return;
		}
		if (isSkipped)
		{
			CommonAPI.debug("IS SKIPPED CLOSE");
			viewController.closeAddEnchantmentPopup();
			shopMenuController.showPopEvent(PopEventType.PopEventTypeNaming);
			isClosed = true;
			return;
		}
		switch (animPhase)
		{
		case "ITEM_PARTICLES_BURST":
			CommonAPI.debug("ITEM_PARTICLES_BURST SKIP");
			StartCoroutine(convergeParticles());
			game.getPlayer().addCurrentProjectStats(boostEnchant.getBoostAtk(), boostEnchant.getBoostSpd(), boostEnchant.getBoostAcc(), boostEnchant.getBoostMag());
			GameObject.Find("Panel_ForgeProgressNew").GetComponent<GUIForgeProgressNewController>().updateWeaponDisplay();
			commonScreenObject.tweenScale(weaponStatScale, Vector3.zero, Vector3.one, 0.4f, null, string.Empty);
			isSkipped = true;
			break;
		case "ITEM_PARTICLES_CONVERGE":
			CommonAPI.debug("ITEM_PARTICLES_CONVERGE SKIP");
			game.getPlayer().addCurrentProjectStats(boostEnchant.getBoostAtk(), boostEnchant.getBoostSpd(), boostEnchant.getBoostAcc(), boostEnchant.getBoostMag());
			GameObject.Find("Panel_ForgeProgressNew").GetComponent<GUIForgeProgressNewController>().updateWeaponDisplay();
			commonScreenObject.tweenScale(weaponStatScale, Vector3.zero, Vector3.one, 0.4f, null, string.Empty);
			isSkipped = true;
			break;
		case "STAT_POP":
			CommonAPI.debug("STAT_POP SKIP");
			doStarAnimSecondHalf();
			commonScreenObject.tweenScale(weaponStatScale, Vector3.zero, Vector3.one, 0.4f, null, string.Empty);
			isSkipped = true;
			break;
		case "STAR_ANIM_PT2":
			CommonAPI.debug("NO SKIP CLOSE");
			viewController.closeAddEnchantmentPopup();
			shopMenuController.showPopEvent(PopEventType.PopEventTypeNaming);
			isSkipped = true;
			isClosed = true;
			break;
		}
	}

	public void showPopup(List<int> projectBefore, List<int> projectAfter, Weapon displayWeapon, Boost aBoost, Item enchantItem)
	{
		GameData gameData = game.getGameData();
		weapon = displayWeapon;
		statsBefore = projectBefore;
		statsAfter = projectAfter;
		boostEnchant = aBoost;
		starStart = commonScreenObject.createPrefab(enchantItemBg, "starStart", movePointObj, Vector3.zero, Vector3.one, Vector3.zero);
		starEnd = commonScreenObject.createPrefab(GameObject.Find("Panel_ForgeProgressNew").gameObject, "starEnd", movePointObj, new Vector3(-140f, -195f, 0f), Vector3.one, Vector3.zero);
		starObjList = new List<GameObject>();
		starGridList = new List<GameObject>();
		addEnchantTitleLabel.text = gameData.getTextByRefId("enchantItem01").ToUpper(CultureInfo.InvariantCulture);
		GameObject aObject = commonScreenObject.findChild(base.gameObject, "EnchantItem_bg/WeaponStats").gameObject;
		commonScreenObject.findChild(aObject, "prefix_bg/prefix_title").GetComponent<UILabel>().text = gameData.getTextByRefId("enchantItem03").ToUpper(CultureInfo.InvariantCulture);
		commonScreenObject.findChild(aObject, "WeaponStats_title").GetComponent<UILabel>().text = gameData.getTextByRefId("enchantItem02").ToUpper(CultureInfo.InvariantCulture);
		if (enchantItem != null)
		{
			itemLabel.text = enchantItem.getItemName();
			itemTexture.mainTexture = commonScreenObject.loadTexture("Image/Enchantment/" + enchantItem.getImage());
		}
		else
		{
			itemLabel.text = string.Empty;
			itemTexture.mainTexture = null;
		}
		weaponTexture.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + displayWeapon.getImage());
		if (boostEnchant.getBoostAtk() > 0)
		{
			statSprite.spriteName = "ico_atk";
			statValue.text = "[56AE59]" + CommonAPI.formatNumber(boostEnchant.getBoostAtk()) + "[-]";
		}
		if (boostEnchant.getBoostSpd() > 0)
		{
			statSprite.spriteName = "ico_speed";
			statValue.text = "[56AE59]" + CommonAPI.formatNumber(boostEnchant.getBoostSpd()) + "[-]";
		}
		if (boostEnchant.getBoostAcc() > 0)
		{
			statSprite.spriteName = "ico_acc";
			statValue.text = "[56AE59]" + CommonAPI.formatNumber(boostEnchant.getBoostAcc()) + "[-]";
		}
		if (boostEnchant.getBoostMag() > 0)
		{
			statSprite.spriteName = "ico_enh";
			statValue.text = "[56AE59]" + CommonAPI.formatNumber(boostEnchant.getBoostMag()) + "[-]";
		}
		prefixValue.text = enchantItem.getItemEffectString();
		animPhase = "START";
		audioController.playForgeBoostEnchantAudio();
		StartCoroutine(showAddEnchantment());
	}

	private IEnumerator convergeParticles()
	{
		CommonAPI.debug("convergeParticles");
		commonScreenObject.tweenScale(itemScale, Vector3.one, Vector3.zero, 0.6f, null, string.Empty);
		int numParticles = itemParticles.GetParticles(itemParticleList);
		doItemParticles = true;
		yield return new WaitForSeconds(0.5f);
		if (!isClosed)
		{
			doItemParticles = false;
			addEnchantmentFireworks();
		}
	}

	private IEnumerator showAddEnchantment()
	{
		itemParticles.Emit(50);
		animPhase = "ITEM_PARTICLES_BURST";
		yield return new WaitForSeconds(1f);
		if (!isSkipped)
		{
			StartCoroutine(convergeParticles());
			animPhase = "ITEM_PARTICLES_CONVERGE";
			yield return new WaitForSeconds(0.5f);
		}
		if (!isSkipped)
		{
			generateBoostPop(boostEnchant);
			animPhase = "STAT_POP";
			yield return new WaitForSeconds(1.5f);
		}
		if (!isSkipped)
		{
			doStarAnimSecondHalf();
			commonScreenObject.tweenScale(weaponStatScale, Vector3.zero, Vector3.one, 0.4f, null, string.Empty);
			animPhase = "STAR_ANIM_PT2";
		}
		yield return new WaitForSeconds(2f);
		if (!isClosed)
		{
			viewController.closeAddEnchantmentPopup();
			shopMenuController.showPopEvent(PopEventType.PopEventTypeNaming);
		}
	}

	private void addEnchantmentFireworks()
	{
		commonScreenObject.tweenScale(weaponGlow, Vector3.zero, Vector3.one, 1f, null, string.Empty, isPlayForwards: false);
		weaponParticles.Play();
	}

	private void doStarAnimSecondHalf()
	{
		int num = 0;
		foreach (GameObject starObj in starObjList)
		{
			starObj.name += "OLD";
			commonScreenObject.tweenScale(starObj.GetComponent<TweenScale>(), Vector3.one, Vector3.zero, 0.5f, null, string.Empty);
			commonScreenObject.tweenPosition(starObj.GetComponent<TweenPosition>(), starObj.transform.localPosition, starEnd.transform.localPosition, 0.5f, null, string.Empty);
			commonScreenObject.destroyPrefabDelay(starObj, 1f);
			num++;
		}
		GameObject.Find("Panel_ForgeProgressNew").GetComponent<GUIForgeProgressNewController>().updateWeaponDisplay();
	}

	private void generateBoostPop(Boost currBoost)
	{
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
			child.SetParent(enchantItemBg.transform);
		}
		starEnd.transform.SetParent(enchantItemBg.transform);
		int num2 = 0;
		foreach (GameObject starObj in starObjList)
		{
			starObj.transform.SetParent(enchantItemBg.transform);
			commonScreenObject.tweenScale(starObj.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 0.5f, null, string.Empty);
			commonScreenObject.tweenPosition(starObj.GetComponent<TweenPosition>(), starStart.transform.localPosition, starGridList[num2].transform.localPosition, 0.5f, null, string.Empty);
			num2++;
		}
	}
}
