using System.Collections.Generic;
using UnityEngine;

public class GUILegendaryRequestController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private UITexture heroImageTexture;

	private UILabel heroNameLabel;

	private UILabel requestTitle;

	private UILabel requestDesc;

	private UILabel fameRewardLabel;

	private UILabel fameRewardValue;

	private UILabel goldRewardLabel;

	private UILabel goldRewardValue;

	private UILabel itemRewardLabel;

	private UILabel itemRewardValue;

	private UITexture itemRewardImage;

	private UILabel itemRewardQty;

	private UILabel requirementsTitleLabel;

	private UILabel weaponReqTitle;

	private UILabel weaponReqLabel;

	private UITexture weaponReqTexture;

	private UILabel statReqTitle;

	private UISprite stat1Icon;

	private UILabel stat1Label;

	private UISprite stat2Icon;

	private UILabel stat2Label;

	private LegendaryHero legendaryHero;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		heroImageTexture = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/HeroImage_texture").GetComponent<UITexture>();
		heroNameLabel = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/RequestDesc_bg/HeroName_bg/HeroName_label").GetComponent<UILabel>();
		requestTitle = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/RequestDesc_bg/RequestTitle\u0003_label").GetComponent<UILabel>();
		requestDesc = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/RequestDesc_bg/RequestDesc_label").GetComponent<UILabel>();
		fameRewardLabel = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/RequestDetails_bg/Fame_bg/FameReward_label").GetComponent<UILabel>();
		fameRewardValue = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/RequestDetails_bg/Fame_bg/FameReward_value").GetComponent<UILabel>();
		goldRewardLabel = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/RequestDetails_bg/GoldReward_bg/GoldReward_label").GetComponent<UILabel>();
		goldRewardValue = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/RequestDetails_bg/GoldReward_bg/GoldReward_value").GetComponent<UILabel>();
		itemRewardLabel = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/RequestDetails_bg/ItemReward_bg/ItemReward_label").GetComponent<UILabel>();
		itemRewardValue = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/RequestDetails_bg/ItemReward_bg/ItemReward_value").GetComponent<UILabel>();
		itemRewardImage = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/RequestDetails_bg/ItemReward_bg/ItemReward_texture").GetComponent<UITexture>();
		itemRewardQty = commonScreenObject.findChild(itemRewardImage.gameObject, "LegendaryRewardQty_qtyBg/LegendaryRewardQty_qty").GetComponent<UILabel>();
		requirementsTitleLabel = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/Requirements_bg/RequirementsTitle_label").GetComponent<UILabel>();
		weaponReqTitle = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/Requirements_bg/WeaponReq_bg/WeaponReq_title").GetComponent<UILabel>();
		weaponReqLabel = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/Requirements_bg/WeaponReq_bg/WeaponReq_value").GetComponent<UILabel>();
		weaponReqTexture = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/Requirements_bg/WeaponReq_bg/Weapon_bg/Weapon_texture").GetComponent<UITexture>();
		statReqTitle = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/Requirements_bg/StatReq_bg/StatReq_title").GetComponent<UILabel>();
		stat1Icon = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/Requirements_bg/StatReq_bg/stat1_sprite").GetComponent<UISprite>();
		stat1Label = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/Requirements_bg/StatReq_bg/stat1_sprite/stat1_label").GetComponent<UILabel>();
		stat2Icon = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/Requirements_bg/StatReq_bg/stat2_sprite").GetComponent<UISprite>();
		stat2Label = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/Requirements_bg/StatReq_bg/stat2_sprite/stat2_label").GetComponent<UILabel>();
		legendaryHero = null;
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName != null && gameObjectName == "Accept_button")
		{
			bool resume = true;
			if (legendaryHero.getLegendaryHeroRefId() == "90012")
			{
				Dictionary<string, DialogueNEW> dialogueBySetId = game.getGameData().getDialogueBySetId("90102");
				viewController.showDialoguePopup("90102", dialogueBySetId);
				resume = false;
			}
			viewController.closeLegendaryRequest(resume);
		}
	}

	public void processHover(bool isOver, string hoverName)
	{
		if (isOver)
		{
			if (hoverName != null)
			{
			}
			string[] array = hoverName.Split('_');
		}
		else
		{
			tooltipScript.setInactive();
		}
	}

	public void setReference(LegendaryHero aLegendary)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		legendaryHero = aLegendary;
		setLabels();
		heroImageTexture.mainTexture = commonScreenObject.loadTexture("Image/legendary heroes/standing/" + legendaryHero.getImage() + "_request");
		heroNameLabel.text = legendaryHero.getLegendaryHeroName();
		requestTitle.text = legendaryHero.getLegendaryQuestName();
		requestDesc.text = legendaryHero.getLegendaryQuestDescription();
		fameRewardValue.text = CommonAPI.formatNumber(legendaryHero.getRewardFame());
		goldRewardValue.text = CommonAPI.formatNumber(legendaryHero.getRewardGold());
		if (legendaryHero.getRewardItemQty() > 0 && legendaryHero.getRewardItemRefId() != string.Empty)
		{
			itemRewardQty.text = legendaryHero.getRewardItemQty().ToString();
			itemRewardValue.text = string.Empty;
			switch (legendaryHero.getRewardItemType())
			{
			case "DECORATION":
			{
				Decoration decorationByRefId = gameData.getDecorationByRefId(legendaryHero.getRewardItemRefId());
				itemRewardImage.mainTexture = commonScreenObject.loadTexture("Image/Decoration/" + decorationByRefId.getDecorationImage());
				break;
			}
			case "ENCHANTMENT":
			{
				Item itemByRefId = gameData.getItemByRefId(legendaryHero.getRewardItemRefId());
				itemRewardImage.mainTexture = commonScreenObject.loadTexture("Image/Enchantment/" + itemByRefId.getImage());
				break;
			}
			case "FURNITURE":
			{
				Furniture furnitureByRefId = gameData.getFurnitureByRefId(legendaryHero.getRewardItemRefId());
				break;
			}
			}
			scaleTexture(itemRewardImage, 40);
		}
		else
		{
			itemRewardImage.alpha = 0f;
			itemRewardValue.text = gameData.getTextByRefId("menuGeneral06");
		}
		Weapon weaponByRefId = gameData.getWeaponByRefId(legendaryHero.getWeaponRefId());
		weaponReqLabel.text = weaponByRefId.getWeaponName();
		weaponReqTexture.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + weaponByRefId.getImage());
		List<WeaponStat> list = new List<WeaponStat>();
		List<int> list2 = new List<int>();
		if (legendaryHero.getReqAtk() > 0)
		{
			list.Add(WeaponStat.WeaponStatAttack);
			list2.Add(legendaryHero.getReqAtk());
		}
		if (legendaryHero.getReqSpd() > 0)
		{
			list.Add(WeaponStat.WeaponStatSpeed);
			list2.Add(legendaryHero.getReqSpd());
		}
		if (legendaryHero.getReqAcc() > 0)
		{
			list.Add(WeaponStat.WeaponStatAccuracy);
			list2.Add(legendaryHero.getReqAcc());
		}
		if (legendaryHero.getReqMag() > 0)
		{
			list.Add(WeaponStat.WeaponStatMagic);
			list2.Add(legendaryHero.getReqMag());
		}
		if (list.Count > 0)
		{
			stat1Label.text = list2[0].ToString();
			switch (list[0])
			{
			case WeaponStat.WeaponStatAttack:
				stat1Icon.spriteName = "ico_atk";
				break;
			case WeaponStat.WeaponStatSpeed:
				stat1Icon.spriteName = "ico_speed";
				break;
			case WeaponStat.WeaponStatAccuracy:
				stat1Icon.spriteName = "ico_acc";
				break;
			case WeaponStat.WeaponStatMagic:
				stat1Icon.spriteName = "ico_enh";
				break;
			}
		}
		else
		{
			stat1Icon.alpha = 0f;
		}
		if (list.Count > 1)
		{
			stat2Label.text = list2[1].ToString();
			switch (list[1])
			{
			case WeaponStat.WeaponStatAttack:
				stat2Icon.spriteName = "ico_atk";
				break;
			case WeaponStat.WeaponStatSpeed:
				stat2Icon.spriteName = "ico_speed";
				break;
			case WeaponStat.WeaponStatAccuracy:
				stat2Icon.spriteName = "ico_acc";
				break;
			case WeaponStat.WeaponStatMagic:
				stat2Icon.spriteName = "ico_enh";
				break;
			}
		}
		else
		{
			stat2Icon.alpha = 0f;
		}
		audioController.playLegendRequestAudio();
	}

	private void scaleTexture(UITexture imageTexture, int maxHeight)
	{
		if (imageTexture.mainTexture != null)
		{
			int num = imageTexture.mainTexture.width;
			int num2 = imageTexture.mainTexture.height;
			if (num2 > maxHeight)
			{
				num = num * maxHeight / num2;
				num2 = maxHeight;
			}
			imageTexture.SetDimensions(num, num2);
		}
	}

	private void setLabels()
	{
		GameData gameData = game.getGameData();
		UILabel[] componentsInChildren = base.gameObject.GetComponentsInChildren<UILabel>();
		foreach (UILabel uILabel in componentsInChildren)
		{
			switch (uILabel.gameObject.name)
			{
			case "RequestTitle_label":
				uILabel.text = gameData.getTextByRefId("weaponRequest01");
				break;
			case "Accept_label":
				uILabel.text = gameData.getTextByRefId("weaponRequest14");
				break;
			case "FameReward_label":
				uILabel.text = gameData.getTextByRefId("weaponRequest10");
				break;
			case "GoldReward_label":
				uILabel.text = gameData.getTextByRefId("weaponRequest11");
				break;
			case "ItemReward_label":
				uILabel.text = gameData.getTextByRefId("weaponRequest12");
				break;
			case "RequirementsTitle_label":
				uILabel.text = gameData.getTextByRefId("weaponRequest03");
				break;
			case "StatReq_title":
				uILabel.text = gameData.getTextByRefId("weaponRequest05");
				break;
			case "WeaponReq_title":
				uILabel.text = gameData.getTextByRefId("weaponRequest04");
				break;
			}
		}
	}
}
