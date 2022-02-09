using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIForgeIncidentController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private ForgeIncident forgeIncident;

	private UILabel titleLabel;

	private UITexture incidentTexture;

	private UILabel resultText;

	private UILabel effectLabel;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName != null && gameObjectName == "Close_button")
		{
			viewController.closeForgeIncidentPopup();
		}
	}

	public void setIncident(ForgeIncident aIncident)
	{
		GameData gameData = game.getGameData();
		forgeIncident = aIncident;
		titleLabel = commonScreenObject.findChild(base.gameObject, "ForgeIncident_bg/ForgeIncidentTitle_label").GetComponent<UILabel>();
		incidentTexture = commonScreenObject.findChild(base.gameObject, "ForgeIncident_bg/ForgeIncident_texture").GetComponent<UITexture>();
		GameObject aObject = commonScreenObject.findChild(base.gameObject, "ForgeIncident_bg/Result_bg").gameObject;
		resultText = commonScreenObject.findChild(aObject, "ForgeIncidentResult_label").GetComponent<UILabel>();
		effectLabel = commonScreenObject.findChild(aObject, "ForgeIncidentEffect_label").GetComponent<UILabel>();
		commonScreenObject.findChild(aObject, "Close_button/Close_label").GetComponent<UILabel>().text = gameData.getTextByRefId("menuGeneral04");
		showResult();
	}

	private void showResult()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		titleLabel.text = forgeIncident.getIncidentName().ToUpper(CultureInfo.InvariantCulture);
		string incidentDesc = forgeIncident.getIncidentDesc();
		string text = incidentDesc;
		resultText.text = incidentDesc;
		string text2 = string.Empty;
		incidentTexture.mainTexture = commonScreenObject.loadTexture("Image/forgeIncident/" + forgeIncident.getImage());
		IncidentType incidentType = forgeIncident.getIncidentType();
		float incidentMagnitude = forgeIncident.getIncidentMagnitude();
		switch (incidentType)
		{
		case IncidentType.IncidentTypeBuff:
		{
			float atkBuff = Random.Range(incidentMagnitude - 0.05f, incidentMagnitude + 0.05f);
			float spdBuff = Random.Range(incidentMagnitude - 0.05f, incidentMagnitude + 0.05f);
			float accBuff = Random.Range(incidentMagnitude - 0.05f, incidentMagnitude + 0.05f);
			float magBuff = Random.Range(incidentMagnitude - 0.05f, incidentMagnitude + 0.05f);
			if (!gameData.checkFeatureIsUnlocked(gameLockSet, "ENCHANT", completedTutorialIndex))
			{
				magBuff = 0f;
			}
			List<int> currentProjectStats = player.getCurrentProjectStats();
			player.buffCurrentProjectStats(atkBuff, spdBuff, accBuff, magBuff);
			List<int> currentProjectStats2 = player.getCurrentProjectStats();
			text2 = text2 + gameData.getTextByRefId("forgeIncident01") + "\n";
			text2 += CommonAPI.generateBoostString(currentProjectStats2[0] - currentProjectStats[0], currentProjectStats2[1] - currentProjectStats[1], currentProjectStats2[2] - currentProjectStats[2], currentProjectStats2[3] - currentProjectStats[3]);
			text = text + " " + gameData.getTextByRefId("forgeIncident01");
			text = text + " [D484F5]" + CommonAPI.generateBoostString(currentProjectStats2[0] - currentProjectStats[0], currentProjectStats2[1] - currentProjectStats[1], currentProjectStats2[2] - currentProjectStats[2], currentProjectStats2[3] - currentProjectStats[3]) + "[-]";
			GameObject gameObject2 = GameObject.Find("Panel_ForgeProgressNew");
			if (gameObject2 != null)
			{
				gameObject2.GetComponent<GUIForgeProgressNewController>().updateWeaponDisplay();
			}
			break;
		}
		case IncidentType.IncidentTypeDebuff:
		{
			float atkDebuff = Random.Range(incidentMagnitude - 0.05f, incidentMagnitude + 0.05f);
			float spdDebuff = Random.Range(incidentMagnitude - 0.05f, incidentMagnitude + 0.05f);
			float accDebuff = Random.Range(incidentMagnitude - 0.05f, incidentMagnitude + 0.05f);
			float magDebuff = Random.Range(incidentMagnitude - 0.05f, incidentMagnitude + 0.05f);
			if (!gameData.checkFeatureIsUnlocked(gameLockSet, "ENCHANT", completedTutorialIndex))
			{
				magDebuff = 0f;
			}
			List<int> currentProjectStats3 = player.getCurrentProjectStats();
			player.debuffCurrentProjectStats(atkDebuff, spdDebuff, accDebuff, magDebuff);
			List<int> currentProjectStats4 = player.getCurrentProjectStats();
			text2 = text2 + gameData.getTextByRefId("forgeIncident02") + "\n";
			text2 += CommonAPI.generateBoostString(currentProjectStats4[0] - currentProjectStats3[0], currentProjectStats4[1] - currentProjectStats3[1], currentProjectStats4[2] - currentProjectStats3[2], currentProjectStats4[3] - currentProjectStats3[3]);
			text = text + " " + gameData.getTextByRefId("forgeIncident02");
			text = text + " [D484F5]" + CommonAPI.generateBoostString(currentProjectStats4[0] - currentProjectStats3[0], currentProjectStats4[1] - currentProjectStats3[1], currentProjectStats4[2] - currentProjectStats3[2], currentProjectStats4[3] - currentProjectStats3[3]) + "[-]";
			GameObject gameObject3 = GameObject.Find("Panel_ForgeProgressNew");
			if (gameObject3 != null)
			{
				gameObject3.GetComponent<GUIForgeProgressNewController>().updateWeaponDisplay();
			}
			break;
		}
		case IncidentType.IncidentTypeGold:
		{
			int num2 = (int)incidentMagnitude;
			if (num2 < 0)
			{
				int num3 = -num2;
				player.reduceGold(num3, allowNegative: true);
				audioController.playPurchaseAudio();
				text2 = text2 + gameData.getTextByRefId("gold") + " -$" + CommonAPI.formatNumber(num3);
				player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseMisc, string.Empty, num2);
				string text3 = text;
				text = text3 + " [D484F5]" + gameData.getTextByRefId("gold") + " -$" + CommonAPI.formatNumber(num3) + "[-]";
			}
			else
			{
				player.addGold(num2);
				audioController.playGoldGainAudio();
				text2 = text2 + gameData.getTextByRefId("gold") + " +$" + CommonAPI.formatNumber(num2);
				player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeEarningMisc, string.Empty, num2);
				string text3 = text;
				text = text3 + " [D484F5]" + gameData.getTextByRefId("gold") + " +$" + CommonAPI.formatNumber(num2) + "[-]";
			}
			commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().showTimeStatus();
			break;
		}
		case IncidentType.IncidentTypeBoost:
		{
			int num = (int)incidentMagnitude;
			player.getCurrentProject().addMaxBoost(num);
			string text3 = text2;
			text2 = text3 + gameData.getTextByRefId("menuForgeBoost43") + " +" + num;
			text3 = text;
			text = text3 + " [D484F5]" + gameData.getTextByRefId("menuForgeBoost43") + " +" + num + "[-]";
			GameObject gameObject = GameObject.Find("Panel_ForgeProgressNew");
			if (gameObject != null)
			{
				gameObject.GetComponent<GUIForgeProgressNewController>().updateBoostDrawer();
			}
			break;
		}
		}
		effectLabel.text = text2;
		gameData.addNewWhetsappMsg(player.getShopName(), text, "Image/whetsapp/news2", player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeNotice);
	}
}
