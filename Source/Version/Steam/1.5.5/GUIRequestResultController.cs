using System.Collections.Generic;
using System.Globalization;
using SmoothMoves;
using UnityEngine;

public class GUIRequestResultController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private UILabel titleLabel;

	private UILabel heroTextLabel;

	private GameObject heroAnimScaler;

	private UILabel fameLabel;

	private UILabel fameValue;

	private UILabel itemTitle;

	private UILabel itemLabel;

	private UIGrid itemGrid;

	private UILabel starchLabel;

	private UILabel starchValue;

	private UIButton okayButton;

	private HeroRequest heroRequest;

	private Shader animShader;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		titleLabel = commonScreenObject.findChild(base.gameObject, "RequestResultTitle_bg/RequestResultTitle_label").GetComponent<UILabel>();
		heroTextLabel = commonScreenObject.findChild(base.gameObject, "HeroText_label").GetComponent<UILabel>();
		heroAnimScaler = commonScreenObject.findChild(base.gameObject, "HeroAnim_scaler").gameObject;
		fameLabel = commonScreenObject.findChild(base.gameObject, "Fame_bg/Fame_label").GetComponent<UILabel>();
		fameValue = commonScreenObject.findChild(base.gameObject, "Fame_bg/Fame_value").GetComponent<UILabel>();
		itemTitle = commonScreenObject.findChild(base.gameObject, "ItemTitle_bg/ItemTitle_label").GetComponent<UILabel>();
		itemLabel = commonScreenObject.findChild(base.gameObject, "ItemTitle_bg/ItemRewards_bg/ItemRewards_label").GetComponent<UILabel>();
		itemGrid = commonScreenObject.findChild(base.gameObject, "ItemTitle_bg/ItemRewards_bg/ItemRewards_grid").GetComponent<UIGrid>();
		starchLabel = commonScreenObject.findChild(base.gameObject, "StarchTitle_bg/StarchTitle_label").GetComponent<UILabel>();
		starchValue = commonScreenObject.findChild(base.gameObject, "StarchTitle_bg/StarchRewards_bg/StarchReward_label").GetComponent<UILabel>();
		okayButton = commonScreenObject.findChild(base.gameObject, "Close_button").GetComponent<UIButton>();
		heroRequest = null;
		animShader = Resources.Load("Custom Shader/Alpha Blended - QuestSelect Hero") as Shader;
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName != null && gameObjectName == "Close_button")
		{
			viewController.closeRequestResult();
		}
	}

	public void processHover(bool isOver, GameObject hoverObj)
	{
		string text = hoverObj.name;
		if (isOver)
		{
			if (text != null)
			{
			}
			string[] array = text.Split('_');
			if (array[0] == "RequestResultRewardItem")
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
		titleLabel.text = gameData.getTextByRefId("weaponRequest18").ToUpper(CultureInfo.InvariantCulture);
		Hero jobClassByRefId = gameData.getJobClassByRefId(heroRequest.getRequestHero());
		string randomTextBySetRefId = gameData.getRandomTextBySetRefId("whetsappRequestComplete");
		heroTextLabel.text = randomTextBySetRefId;
		GameObject gameObject = commonScreenObject.createPrefab(heroAnimScaler, "RequestResultHero_" + jobClassByRefId.getImage(), "Animation/Hero/" + jobClassByRefId.getImage() + "/" + jobClassByRefId.getImage() + "_animObj", Vector3.zero, Vector3.one, Vector3.zero);
		BoneAnimation componentInChildren = gameObject.GetComponentInChildren<BoneAnimation>();
		componentInChildren.mMaterials[0].shader = animShader;
		componentInChildren.GetComponent<Renderer>().sortingOrder = 1;
		componentInChildren.Play("idle");
		fameLabel.text = gameData.getTextByRefId("weaponRequest10");
		fameValue.text = CommonAPI.formatNumber(heroRequest.getRequestRewardFame());
		itemTitle.text = gameData.getTextByRefId("weaponRequest12");
		Dictionary<string, int> requestRewardItemList = heroRequest.getRequestRewardItemList();
		bool flag = false;
		foreach (string key in requestRewardItemList.Keys)
		{
			Item itemByRefId = gameData.getItemByRefId(key);
			GameObject aObject = commonScreenObject.createPrefab(itemGrid.gameObject, "RequestResultRewardItem_" + itemByRefId.getItemRefId(), "Prefab/Request/RequestResultRewardItemObj", Vector3.zero, Vector3.one, Vector3.zero);
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
			itemGrid.Reposition();
			itemLabel.text = string.Empty;
		}
		else
		{
			itemLabel.text = gameData.getTextByRefId("menuGeneral06");
		}
		starchLabel.text = gameData.getTextByRefId("weaponRequest11");
		starchValue.text = CommonAPI.formatNumber(heroRequest.getRequestRewardGold());
		okayButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral04");
		string aName = jobClassByRefId.getHeroName() + " " + gameData.getTextByRefIdWithDynText("heroStat08", "[level]", jobClassByRefId.getHeroLevel().ToString());
		gameData.addNewWhetsappMsg(aName, randomTextBySetRefId, "Image/Hero/" + jobClassByRefId.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeHero);
	}
}
