using System.Collections.Generic;
using SmoothMoves;
using UnityEngine;

public class GUIWeaponRequestController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private GameObject heroAnimScaler;

	private UILabel heroNameLabel;

	private UILabel heroJobLabel;

	private UILabel requestTitle;

	private UILabel requestDesc;

	private UILabel fameRewardLabel;

	private UILabel fameRewardValue;

	private UILabel goldRewardLabel;

	private UILabel goldRewardValue;

	private UILabel itemRewardLabel;

	private UIGrid itemRewardGrid;

	private UILabel itemRewardValue;

	private UILabel timeLimitLabel;

	private UILabel timeLimitValue;

	private UILabel requirementsTitleLabel;

	private UILabel requirementsLabel;

	private UIGrid requirementsGrid;

	private HeroRequest heroRequest;

	private Shader animShader;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		heroAnimScaler = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/HeroAnim_panel/HeroAnim_scaler").gameObject;
		heroNameLabel = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/RequestDesc_bg/HeroName_bg/HeroName_label").GetComponent<UILabel>();
		heroJobLabel = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/RequestDesc_bg/HeroName_bg/HeroJob_label").GetComponent<UILabel>();
		requestTitle = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/RequestDesc_bg/RequestTitle\u0003_label").GetComponent<UILabel>();
		requestDesc = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/RequestDesc_bg/RequestDesc_label").GetComponent<UILabel>();
		fameRewardLabel = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/RequestDetails_bg/Fame_bg/FameReward_label").GetComponent<UILabel>();
		fameRewardValue = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/RequestDetails_bg/Fame_bg/FameReward_value").GetComponent<UILabel>();
		goldRewardLabel = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/RequestDetails_bg/GoldReward_bg/GoldReward_label").GetComponent<UILabel>();
		goldRewardValue = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/RequestDetails_bg/GoldReward_bg/GoldReward_value").GetComponent<UILabel>();
		itemRewardLabel = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/RequestDetails_bg/ItemReward_bg/ItemReward_label").GetComponent<UILabel>();
		itemRewardGrid = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/RequestDetails_bg/ItemReward_bg/RewardItemList_grid").GetComponent<UIGrid>();
		itemRewardValue = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/RequestDetails_bg/ItemReward_bg/ItemReward_value").GetComponent<UILabel>();
		timeLimitLabel = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/RequestDetails_bg/TimeLimit_bg/TimeLimit_label").GetComponent<UILabel>();
		timeLimitValue = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/RequestDetails_bg/TimeLimit_bg/TimeLimit_value").GetComponent<UILabel>();
		requirementsTitleLabel = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/Requirements_bg/RequirementsTitle_label").GetComponent<UILabel>();
		requirementsLabel = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/Requirements_bg/Requirements_label").GetComponent<UILabel>();
		requirementsGrid = commonScreenObject.findChild(base.gameObject, "WeaponRequest_bg/Requirements_bg/Requirements_grid").GetComponent<UIGrid>();
		heroRequest = null;
		animShader = Resources.Load("Custom Shader/Alpha Blended - QuestSelect Hero") as Shader;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "Accept_button":
		{
			Player player = game.getPlayer();
			player.addTotalRequestCount(1);
			player.addDisplayRequest(heroRequest);
			viewController.closeWeaponRequest();
			break;
		}
		case "Reject_button":
			viewController.closeWeaponRequest();
			break;
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
			if (array[0] == "RequestRewardItemObj")
			{
				GameData gameData = game.getGameData();
				Item itemByRefId = gameData.getItemByRefId(array[1]);
				int num = heroRequest.getRequestRewardItemList()[array[1]];
				tooltipScript.showText(itemByRefId.getItemName() + " x" + num);
			}
		}
		else
		{
			tooltipScript.setInactive();
		}
	}

	public void setReference(HeroRequest aRequest)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		heroRequest = aRequest;
		setLabels();
		Hero jobClassByRefId = gameData.getJobClassByRefId(heroRequest.getRequestHero());
		GameObject gameObject = commonScreenObject.createPrefab(heroAnimScaler, "RequestHero_" + jobClassByRefId.getImage(), "Animation/Hero/" + jobClassByRefId.getImage() + "/" + jobClassByRefId.getImage() + "_animObj", Vector3.zero, Vector3.one, Vector3.zero);
		BoneAnimation componentInChildren = gameObject.GetComponentInChildren<BoneAnimation>();
		componentInChildren.mMaterials[0].shader = animShader;
		componentInChildren.GetComponent<Renderer>().sortingOrder = 1;
		componentInChildren.Play("idle");
		heroNameLabel.text = jobClassByRefId.getHeroName();
		heroJobLabel.text = jobClassByRefId.getJobClassName();
		requestTitle.text = heroRequest.getRequestName();
		requestDesc.text = heroRequest.getRequestDesc();
		fameRewardValue.text = CommonAPI.formatNumber(heroRequest.getRequestRewardFame());
		goldRewardValue.text = CommonAPI.formatNumber(heroRequest.getRequestRewardGold());
		Dictionary<string, int> requestRewardItemList = heroRequest.getRequestRewardItemList();
		bool flag = false;
		foreach (string key in requestRewardItemList.Keys)
		{
			CommonAPI.debug("rewardRefId " + key);
			Item itemByRefId = gameData.getItemByRefId(key);
			GameObject aObject = commonScreenObject.createPrefab(itemRewardGrid.gameObject, "RequestRewardItemObj_" + itemByRefId.getItemRefId(), "Prefab/Request/RequestRewardItemObj", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(aObject, "WeaponMat_qtyBg/WeaponMat_qty").GetComponent<UILabel>().text = requestRewardItemList[key].ToString();
			switch (itemByRefId.getItemType())
			{
			case ItemType.ItemTypeMaterial:
				commonScreenObject.findChild(aObject, "WeaponMat_img").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/materials/" + itemByRefId.getImage());
				break;
			case ItemType.ItemTypeRelic:
				commonScreenObject.findChild(aObject, "WeaponMat_img").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/relics/" + itemByRefId.getImage());
				break;
			case ItemType.ItemTypeEnhancement:
				commonScreenObject.findChild(aObject, "WeaponMat_img").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Enchantment/" + itemByRefId.getImage());
				break;
			}
			flag = true;
		}
		if (flag)
		{
			itemRewardGrid.Reposition();
			itemRewardValue.text = string.Empty;
		}
		else
		{
			itemRewardValue.text = gameData.getTextByRefId("menuGeneral06");
		}
		requirementsTitleLabel.text = gameData.getTextByRefId("weaponRequest03");
		timeLimitValue.text = CommonAPI.convertHalfHoursToTimeString(heroRequest.getRequestTimeLeft(player.getPlayerTimeLong()));
		int num = 1;
		if (heroRequest.getRequestWeaponTypeRefIdReq() != string.Empty)
		{
			WeaponType weaponTypeByRefId = gameData.getWeaponTypeByRefId(heroRequest.getRequestWeaponTypeRefIdReq());
			GameObject aObject2 = commonScreenObject.createPrefab(requirementsGrid.gameObject, "ReqList_" + num, "Prefab/Request/WeaponTypeReqListObj", Vector3.zero, Vector3.one, Vector3.zero);
			UILabel component = commonScreenObject.findChild(aObject2, "WeaponTypeLabel_label").GetComponent<UILabel>();
			component.text = gameData.getTextByRefId("weaponRequest07");
			UILabel component2 = commonScreenObject.findChild(aObject2, "WeaponTypeName_label").GetComponent<UILabel>();
			component2.text = weaponTypeByRefId.getWeaponTypeName();
			UISprite component3 = commonScreenObject.findChild(aObject2, "WeaponType_sprite").GetComponent<UISprite>();
			component3.spriteName = "icon_" + weaponTypeByRefId.getImage();
			num++;
		}
		if (heroRequest.getRequestWeaponRefIdReq() != string.Empty)
		{
			Weapon weaponByRefId = gameData.getWeaponByRefId(heroRequest.getRequestWeaponRefIdReq());
			GameObject aObject3 = commonScreenObject.createPrefab(requirementsGrid.gameObject, "ReqList_" + num, "Prefab/Request/WeaponReqListObj", Vector3.zero, Vector3.one, Vector3.zero);
			UILabel component4 = commonScreenObject.findChild(aObject3, "WeaponLabel_label").GetComponent<UILabel>();
			component4.text = gameData.getTextByRefId("weaponRequest08");
			UILabel component5 = commonScreenObject.findChild(aObject3, "WeaponName_label").GetComponent<UILabel>();
			component5.text = weaponByRefId.getWeaponName();
			UITexture component6 = commonScreenObject.findChild(aObject3, "WeaponImage_bg/WeaponImage_texture").GetComponent<UITexture>();
			component6.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + weaponByRefId.getImage());
			num++;
		}
		if (heroRequest.getRequestEnchantmentReq() != string.Empty)
		{
			Item itemByRefId2 = gameData.getItemByRefId(heroRequest.getRequestEnchantmentReq());
			GameObject aObject4 = commonScreenObject.createPrefab(requirementsGrid.gameObject, "ReqList_" + num, "Prefab/Request/PrefixReqListObj", Vector3.zero, Vector3.one, Vector3.zero);
			UILabel component7 = commonScreenObject.findChild(aObject4, "PrefixLabel_label").GetComponent<UILabel>();
			component7.text = gameData.getTextByRefId("weaponRequest06");
			UILabel component8 = commonScreenObject.findChild(aObject4, "PrefixName_label").GetComponent<UILabel>();
			component8.text = itemByRefId2.getItemEffectString();
			num++;
		}
		if (heroRequest.checkHasStatReq())
		{
			GameObject aObject5 = commonScreenObject.createPrefab(requirementsGrid.gameObject, "ReqList_" + num, "Prefab/Request/StatReqListObj", Vector3.zero, Vector3.one, Vector3.zero);
			UILabel component9 = commonScreenObject.findChild(aObject5, "StatsReq_label").GetComponent<UILabel>();
			component9.text = gameData.getTextByRefId("weaponRequest09");
			List<WeaponStat> list = new List<WeaponStat>();
			List<int> list2 = new List<int>();
			if (heroRequest.getRequestAtkReq() > 0)
			{
				list.Add(WeaponStat.WeaponStatAttack);
				list2.Add(heroRequest.getRequestAtkReq());
			}
			if (heroRequest.getRequestSpdReq() > 0)
			{
				list.Add(WeaponStat.WeaponStatSpeed);
				list2.Add(heroRequest.getRequestSpdReq());
			}
			if (heroRequest.getRequestAccReq() > 0)
			{
				list.Add(WeaponStat.WeaponStatAccuracy);
				list2.Add(heroRequest.getRequestAccReq());
			}
			if (heroRequest.getRequestMagReq() > 0)
			{
				list.Add(WeaponStat.WeaponStatMagic);
				list2.Add(heroRequest.getRequestMagReq());
			}
			if (list.Count > 0)
			{
				UILabel component10 = commonScreenObject.findChild(aObject5, "StatsReq_bg/stat1_sprite/stat1_label").GetComponent<UILabel>();
				component10.text = list2[0].ToString();
				UISprite component11 = commonScreenObject.findChild(aObject5, "StatsReq_bg/stat1_sprite").GetComponent<UISprite>();
				switch (list[0])
				{
				case WeaponStat.WeaponStatAttack:
					component11.spriteName = "ico_atk";
					break;
				case WeaponStat.WeaponStatSpeed:
					component11.spriteName = "ico_speed";
					break;
				case WeaponStat.WeaponStatAccuracy:
					component11.spriteName = "ico_acc";
					break;
				case WeaponStat.WeaponStatMagic:
					component11.spriteName = "ico_enh";
					break;
				}
			}
			if (list.Count > 1)
			{
				UILabel component12 = commonScreenObject.findChild(aObject5, "StatsReq_bg/stat2_sprite/stat2_label").GetComponent<UILabel>();
				component12.text = list2[1].ToString();
				UISprite component13 = commonScreenObject.findChild(aObject5, "StatsReq_bg/stat2_sprite").GetComponent<UISprite>();
				switch (list[1])
				{
				case WeaponStat.WeaponStatAttack:
					component13.spriteName = "ico_atk";
					break;
				case WeaponStat.WeaponStatSpeed:
					component13.spriteName = "ico_speed";
					break;
				case WeaponStat.WeaponStatAccuracy:
					component13.spriteName = "ico_acc";
					break;
				case WeaponStat.WeaponStatMagic:
					component13.spriteName = "ico_enh";
					break;
				}
			}
			else
			{
				UISprite component14 = commonScreenObject.findChild(aObject5, "StatsReq_bg/stat2_sprite").GetComponent<UISprite>();
				component14.alpha = 0f;
				commonScreenObject.findChild(aObject5, "StatsReq_bg/stat1_sprite").transform.localPosition = new Vector3(-30f, 0f, 0f);
			}
			num++;
		}
		requirementsGrid.Reposition();
		List<Project> completedProjectListByType = player.getCompletedProjectListByType(ProjectType.ProjectTypeWeapon, includeSold: false, includeStock: true, includeSelling: false);
		bool flag2 = false;
		foreach (Project item in completedProjectListByType)
		{
			if (heroRequest.checkProjectDeliverable(item))
			{
				flag2 = true;
				break;
			}
		}
		if (flag2)
		{
			requirementsLabel.text = gameData.getTextByRefId("weaponRequest16");
		}
		else
		{
			requirementsLabel.text = gameData.getTextByRefId("weaponRequest17");
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
				uILabel.text = gameData.getTextByRefId("weaponRequest02");
				break;
			case "Accept_label":
				uILabel.text = gameData.getTextByRefId("weaponRequest14");
				break;
			case "Reject_label":
				uILabel.text = gameData.getTextByRefId("weaponRequest15");
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
			case "TimeLimit_label":
				uILabel.text = gameData.getTextByRefId("weaponRequest13");
				break;
			}
		}
	}
}
