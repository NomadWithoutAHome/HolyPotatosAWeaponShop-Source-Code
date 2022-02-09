using System.Globalization;
using UnityEngine;

public class GUILegendarySuccessController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private UILabel titleLabel;

	private UILabel heroTextLabel;

	private UITexture heroImage;

	private UILabel fameLabel;

	private UILabel fameValue;

	private UILabel starchLabel;

	private UILabel starchValue;

	private UIButton okayButton;

	private TweenScale itemTweenScale;

	private UILabel itemTitle;

	private UITexture itemTexture;

	private UILabel itemLabel;

	private LegendaryHero legendaryHero;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		titleLabel = commonScreenObject.findChild(base.gameObject, "RequestResultTitle_bg/RequestResultTitle_label").GetComponent<UILabel>();
		heroTextLabel = commonScreenObject.findChild(base.gameObject, "HeroText_label").GetComponent<UILabel>();
		heroImage = commonScreenObject.findChild(base.gameObject, "HeroImage_texture").GetComponent<UITexture>();
		fameLabel = commonScreenObject.findChild(base.gameObject, "Details_bg/Fame_bg/Fame_label").GetComponent<UILabel>();
		fameValue = commonScreenObject.findChild(base.gameObject, "Details_bg/Fame_valueBg/Fame_value").GetComponent<UILabel>();
		starchLabel = commonScreenObject.findChild(base.gameObject, "Details_bg/StarchTitle_bg/StarchTitle_label").GetComponent<UILabel>();
		starchValue = commonScreenObject.findChild(base.gameObject, "Details_bg/StarchTitle_bg/StarchRewards_bg/StarchReward_label").GetComponent<UILabel>();
		okayButton = commonScreenObject.findChild(base.gameObject, "Close_button").GetComponent<UIButton>();
		itemTweenScale = commonScreenObject.findChild(base.gameObject, "LegendaryItem_bg").GetComponent<TweenScale>();
		itemTitle = commonScreenObject.findChild(base.gameObject, "LegendaryItem_bg/LegendaryItemTitle_bg/LegendaryItemTitle_label").GetComponent<UILabel>();
		itemTexture = commonScreenObject.findChild(base.gameObject, "LegendaryItem_bg/LegendaryItem_texture").GetComponent<UITexture>();
		itemLabel = commonScreenObject.findChild(base.gameObject, "LegendaryItem_bg/LegendaryItemName_label").GetComponent<UILabel>();
		legendaryHero = null;
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName != null && gameObjectName == "Close_button")
		{
			viewController.closeLegendarySuccess(hide: true, resume: true);
		}
	}

	public void setReference(LegendaryHero aLegendary)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		legendaryHero = aLegendary;
		titleLabel.text = gameData.getTextByRefId("weaponRequest18").ToUpper(CultureInfo.InvariantCulture);
		heroTextLabel.text = legendaryHero.getSuccessComment();
		heroImage.mainTexture = commonScreenObject.loadTexture("Image/Dialogue/" + legendaryHero.getImage() + "_requestComplete");
		okayButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("menuGeneral04");
		player.addFame(legendaryHero.getRewardFame());
		audioController.playFameGainAudio();
		player.addGold(legendaryHero.getRewardGold());
		audioController.playGoldGainAudio();
		player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeEarningLegendary, string.Empty, legendaryHero.getRewardGold());
		itemTitle.text = gameData.getTextByRefId("weaponRequest12");
		if (legendaryHero.getRewardItemQty() > 0 && legendaryHero.getRewardItemRefId() != string.Empty)
		{
			string aImage = string.Empty;
			string aText = string.Empty;
			switch (legendaryHero.getRewardItemType())
			{
			case "DECORATION":
			{
				Decoration decorationByRefId = gameData.getDecorationByRefId(legendaryHero.getRewardItemRefId());
				aImage = "Image/Decoration/" + decorationByRefId.getDecorationImage();
				aText = decorationByRefId.getDecorationName() + " x" + legendaryHero.getRewardItemQty();
				decorationByRefId.setIsPlayerOwned(aPlayerOwned: true);
				break;
			}
			case "ENCHANTMENT":
			{
				Item itemByRefId = gameData.getItemByRefId(legendaryHero.getRewardItemRefId());
				aImage = "Image/Enchantment/" + itemByRefId.getImage();
				aText = itemByRefId.getItemName() + " x" + legendaryHero.getRewardItemQty();
				itemByRefId.addItem(legendaryHero.getRewardItemQty());
				break;
			}
			case "FURNITURE":
			{
				Furniture furnitureByRefId = gameData.getFurnitureByRefId(legendaryHero.getRewardItemRefId());
				aImage = "Image/Obstacle/" + furnitureByRefId.getImage();
				aText = furnitureByRefId.getFurnitureName() + " x" + legendaryHero.getRewardItemQty();
				furnitureByRefId.setPlayerOwned(aOwned: true);
				break;
			}
			}
			viewController.queueItemGetPopup(gameData.getTextByRefId("featureUnlock23"), aImage, aText);
			gameData.addNewWhetsappMsg(legendaryHero.getLegendaryHeroName(), legendaryHero.getSuccessComment(), "Image/legendary heroes/" + legendaryHero.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeHero);
			okayButton.isEnabled = true;
		}
		fameLabel.text = gameData.getTextByRefId("weaponRequest10");
		fameValue.text = CommonAPI.formatNumber(legendaryHero.getRewardFame());
		starchLabel.text = gameData.getTextByRefId("weaponRequest11");
		starchValue.text = CommonAPI.formatNumber(legendaryHero.getRewardGold());
		legendaryHero.setRequestState(RequestState.RequestStateCompleted);
		player.completeLegendaryHero(legendaryHero);
	}

	public void finishItemAnim()
	{
		okayButton.isEnabled = true;
	}
}
