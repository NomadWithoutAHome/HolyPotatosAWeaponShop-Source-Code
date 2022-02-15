using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIRandomScenarioController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private DayEndScenario scenario;

	private UISprite titleBg;

	private UISprite titleBgTop;

	private UILabel titleLabel;

	private UITexture patataTexture;

	private ParticleSystem successParticles;

	private ParticleSystem failParticles;

	private UISprite patataBg;

	private UISprite choiceBg;

	private UILabel scenarioText;

	private UILabel choice1Label;

	private UILabel choice2Label;

	private UISprite resultBg;

	private UIButton resultButton;

	private UILabel resultText;

	private UILabel effectLabel;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "ScenarioChoice1_bg":
			selectChoice(1);
			break;
		case "ScenarioChoice2_bg":
			selectChoice(2);
			break;
		case "Close_button":
			viewController.closeRandomScenarioPopup();
			break;
		}
	}

	private void Update()
	{
		if (viewController != null && viewController.getIsPaused() && viewController.getGameStarted())
		{
			handleInput();
		}
	}

	private void handleInput()
	{
		if (resultButton.isEnabled && Input.GetKeyDown(gameData.getKeyCodeByRefID("100008")))
		{
			processClick("Close_button");
		}
	}

	public void setScenario(DayEndScenario aScenario)
	{
		GameData gameData = game.getGameData();
		scenario = aScenario;
		titleBg = commonScreenObject.findChild(base.gameObject, "RandomScenario_bg/RandomScenarioTitle_bg").GetComponent<UISprite>();
		titleBgTop = commonScreenObject.findChild(titleBg.gameObject, "RandomScenarioTitle_bgTop").GetComponent<UISprite>();
		titleLabel = commonScreenObject.findChild(titleBg.gameObject, "RandomScenarioTitle_label").GetComponent<UILabel>();
		patataTexture = commonScreenObject.findChild(base.gameObject, "RandomScenario_bg/Patata_texture").GetComponent<UITexture>();
		patataBg = commonScreenObject.findChild(patataTexture.gameObject, "Patata_bg").GetComponent<UISprite>();
		successParticles = commonScreenObject.findChild(patataTexture.gameObject, "Success_particles").GetComponent<ParticleSystem>();
		failParticles = commonScreenObject.findChild(patataTexture.gameObject, "Fail_particles").GetComponent<ParticleSystem>();
		choiceBg = commonScreenObject.findChild(base.gameObject, "RandomScenario_bg/Choice_bg").GetComponent<UISprite>();
		scenarioText = commonScreenObject.findChild(choiceBg.gameObject, "RandomScenarioText_label").GetComponent<UILabel>();
		choice1Label = commonScreenObject.findChild(choiceBg.gameObject, "ScenarioChoice1_bg/ScenarioChoice1_label").GetComponent<UILabel>();
		choice2Label = commonScreenObject.findChild(choiceBg.gameObject, "ScenarioChoice2_bg/ScenarioChoice2_label").GetComponent<UILabel>();
		resultBg = commonScreenObject.findChild(base.gameObject, "RandomScenario_bg/Result_bg").GetComponent<UISprite>();
		resultButton = commonScreenObject.findChild(resultBg.gameObject, "Close_button").GetComponent<UIButton>();
		resultText = commonScreenObject.findChild(resultBg.gameObject, "ScenarioResult_label").GetComponent<UILabel>();
		effectLabel = commonScreenObject.findChild(resultBg.gameObject, "ScenarioEffect_bg/ScenarioEffect_label").GetComponent<UILabel>();
		commonScreenObject.findChild(resultBg.gameObject, "Close_button/Close_label").GetComponent<UILabel>().text = gameData.getTextByRefId("menuGeneral04");
		titleLabel.text = gameData.getTextByRefId("dayEndEvent01").ToUpper(CultureInfo.InvariantCulture);
		List<string> scenarioStringList = scenario.getScenarioStringList();
		scenarioText.text = replaceText(scenarioStringList[0]);
		choice1Label.text = replaceText(scenarioStringList[1]);
		choice2Label.text = replaceText(scenarioStringList[2]);
		patataTexture.mainTexture = commonScreenObject.loadTexture("Image/Dialogue/cutscene-patata-curious");
		audioController.playEventAppearAudio();
		showChoice();
	}

	private string replaceText(string aText)
	{
		Player player = game.getPlayer();
		return aText.Replace("[dogName]", player.getDogName());
	}

	private void selectChoice(int choice)
	{
		getDayEndEventResult(choice);
		showResult();
	}

	private void showChoice()
	{
		UIButton[] componentsInChildren = choiceBg.GetComponentsInChildren<UIButton>();
		foreach (UIButton uIButton in componentsInChildren)
		{
			uIButton.isEnabled = true;
		}
		choiceBg.alpha = 1f;
		titleBgTop.alpha = 1f;
		titleBg.spriteName = "bg_green";
		UIButton[] componentsInChildren2 = resultBg.GetComponentsInChildren<UIButton>();
		foreach (UIButton uIButton2 in componentsInChildren2)
		{
			uIButton2.isEnabled = false;
		}
		resultBg.alpha = 0f;
	}

	private void showResult()
	{
		UIButton[] componentsInChildren = choiceBg.GetComponentsInChildren<UIButton>();
		foreach (UIButton uIButton in componentsInChildren)
		{
			uIButton.isEnabled = false;
		}
		choiceBg.alpha = 0f;
		titleBgTop.alpha = 0f;
		UIButton[] componentsInChildren2 = resultBg.GetComponentsInChildren<UIButton>();
		foreach (UIButton uIButton2 in componentsInChildren2)
		{
			uIButton2.isEnabled = true;
		}
		resultBg.alpha = 1f;
	}

	private void getDayEndEventResult(int input)
	{
		string empty = string.Empty;
		string aText = string.Empty;
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		Hashtable hashtable = null;
		hashtable = ((input != 1) ? scenario.makeChoice2() : scenario.makeChoice1());
		switch (hashtable["result"].ToString())
		{
		case "SUCCESS":
			patataTexture.mainTexture = commonScreenObject.loadTexture("Image/Dialogue/cutscene-patata-cheerful");
			patataBg.spriteName = "yellow_bg";
			successParticles.Play();
			titleBg.spriteName = "bg_green";
			titleLabel.text = gameData.getTextByRefId("dayEndEvent05");
			audioController.playEventSuccessAudio();
			break;
		case "FAIL":
			patataTexture.mainTexture = commonScreenObject.loadTexture("Image/Dialogue/cutscene-patata-cry");
			patataBg.spriteName = "circle-purple";
			failParticles.Play();
			titleBg.spriteName = "bg_purple-1";
			titleLabel.text = gameData.getTextByRefId("dayEndEvent06");
			audioController.playEventFailAudio();
			break;
		}
		empty = hashtable["text"].ToString();
		switch (hashtable["effect"].ToString())
		{
		case "ScenarioEffectPowUpTempMult":
		{
			float aValue6 = (float)Convert.ToDouble(hashtable["value"]);
			Smith randomPlayerSmith9 = getRandomPlayerSmith();
			int smithPower = randomPlayerSmith9.getSmithPower();
			randomPlayerSmith9.addSmithEffect(StatEffect.StatEffectMultPower, aValue6, 144, string.Empty, string.Empty);
			int smithPower2 = randomPlayerSmith9.getSmithPower();
			string empty6 = string.Empty;
			empty6 = ((smithPower2 <= smithPower) ? (smithPower2 - smithPower).ToString() : ("+" + (smithPower2 - smithPower)));
			List<string> list9 = new List<string>();
			list9.Add("[smithName]");
			list9.Add("[stat]");
			list9.Add("[mult]");
			list9.Add("[amt]");
			List<string> list10 = new List<string>();
			list10.Add(randomPlayerSmith9.getSmithName());
			list10.Add(gameData.getTextByRefId("smithStats11"));
			list10.Add(aValue6.ToString());
			list10.Add(empty6);
			aText = gameData.getTextByRefIdWithDynTextList("dayEndEvent04", list9, list10);
			break;
		}
		case "ScenarioEffectIntUpTempMult":
		{
			float aValue = (float)Convert.ToDouble(hashtable["value"]);
			Smith randomPlayerSmith2 = getRandomPlayerSmith();
			int smithIntelligence = randomPlayerSmith2.getSmithIntelligence();
			randomPlayerSmith2.addSmithEffect(StatEffect.StatEffectMultIntelligence, aValue, 144, string.Empty, string.Empty);
			int smithIntelligence2 = randomPlayerSmith2.getSmithIntelligence();
			string empty2 = string.Empty;
			empty2 = ((smithIntelligence2 <= smithIntelligence) ? (smithIntelligence2 - smithIntelligence).ToString() : ("+" + (smithIntelligence2 - smithIntelligence)));
			List<string> list = new List<string>();
			list.Add("[smithName]");
			list.Add("[stat]");
			list.Add("[mult]");
			list.Add("[amt]");
			List<string> list2 = new List<string>();
			list2.Add(randomPlayerSmith2.getSmithName());
			list2.Add(gameData.getTextByRefId("smithStats12"));
			list2.Add(aValue.ToString());
			list2.Add(empty2);
			aText = gameData.getTextByRefIdWithDynTextList("dayEndEvent04", list, list2);
			break;
		}
		case "ScenarioEffectTecUpTempMult":
		{
			float aValue3 = (float)Convert.ToDouble(hashtable["value"]);
			Smith randomPlayerSmith4 = getRandomPlayerSmith();
			int smithTechnique = randomPlayerSmith4.getSmithTechnique();
			randomPlayerSmith4.addSmithEffect(StatEffect.StatEffectMultTechnique, aValue3, 144, string.Empty, string.Empty);
			int smithTechnique2 = randomPlayerSmith4.getSmithTechnique();
			string empty4 = string.Empty;
			empty4 = ((smithTechnique2 <= smithTechnique) ? (smithTechnique2 - smithTechnique).ToString() : ("+" + (smithTechnique2 - smithTechnique)));
			List<string> list5 = new List<string>();
			list5.Add("[smithName]");
			list5.Add("[stat]");
			list5.Add("[mult]");
			list5.Add("[amt]");
			List<string> list6 = new List<string>();
			list6.Add(randomPlayerSmith4.getSmithName());
			list6.Add(gameData.getTextByRefId("smithStats13"));
			list6.Add(aValue3.ToString());
			list6.Add(empty4);
			aText = gameData.getTextByRefIdWithDynTextList("dayEndEvent04", list5, list6);
			break;
		}
		case "ScenarioEffectLucUpTempMult":
		{
			float aValue2 = (float)Convert.ToDouble(hashtable["value"]);
			Smith randomPlayerSmith3 = getRandomPlayerSmith();
			int smithLuck = randomPlayerSmith3.getSmithLuck();
			randomPlayerSmith3.addSmithEffect(StatEffect.StatEffectMultLuck, aValue2, 144, string.Empty, string.Empty);
			int smithLuck2 = randomPlayerSmith3.getSmithLuck();
			string empty3 = string.Empty;
			empty3 = ((smithLuck2 <= smithLuck) ? (smithLuck2 - smithLuck).ToString() : ("+" + (smithLuck2 - smithLuck)));
			List<string> list3 = new List<string>();
			list3.Add("[smithName]");
			list3.Add("[stat]");
			list3.Add("[mult]");
			list3.Add("[amt]");
			List<string> list4 = new List<string>();
			list4.Add(randomPlayerSmith3.getSmithName());
			list4.Add(gameData.getTextByRefId("smithStats14"));
			list4.Add(aValue2.ToString());
			list4.Add(empty3);
			aText = gameData.getTextByRefIdWithDynTextList("dayEndEvent04", list3, list4);
			break;
		}
		case "ScenarioEffectStaUpTempMult":
		{
			float aValue5 = (float)Convert.ToDouble(hashtable["value"]);
			Smith randomPlayerSmith8 = getRandomPlayerSmith();
			float smithMaxMood = randomPlayerSmith8.getSmithMaxMood();
			randomPlayerSmith8.addSmithEffect(StatEffect.StatEffectMultStamina, aValue5, 144, string.Empty, string.Empty);
			float smithMaxMood2 = randomPlayerSmith8.getSmithMaxMood();
			string empty5 = string.Empty;
			empty5 = ((!(smithMaxMood2 > smithMaxMood)) ? (smithMaxMood2 - smithMaxMood).ToString() : ("+" + (smithMaxMood2 - smithMaxMood)));
			List<string> list7 = new List<string>();
			list7.Add("[smithName]");
			list7.Add("[stat]");
			list7.Add("[mult]");
			list7.Add("[amt]");
			List<string> list8 = new List<string>();
			list8.Add(randomPlayerSmith8.getSmithName());
			list8.Add(gameData.getTextByRefId("smithStats15"));
			list8.Add(aValue5.ToString());
			list8.Add(empty5);
			aText = gameData.getTextByRefIdWithDynTextList("dayEndEvent04", list7, list8);
			break;
		}
		case "ScenarioEffectPowUpPerm":
		{
			int num11 = (int)Convert.ToDouble(hashtable["value"]);
			Smith randomPlayerSmith10 = getRandomPlayerSmith();
			randomPlayerSmith10.doPermPowBuff(num11);
			aText = randomPlayerSmith10.getSmithName() + ": " + gameData.getTextByRefId("smithStats11") + " +" + num11 + "!";
			break;
		}
		case "ScenarioEffectIntUpPerm":
		{
			int num7 = (int)Convert.ToDouble(hashtable["value"]);
			Smith randomPlayerSmith5 = getRandomPlayerSmith();
			randomPlayerSmith5.doPermIntBuff(num7);
			aText = randomPlayerSmith5.getSmithName() + ": " + gameData.getTextByRefId("smithStats12") + " +" + num7 + "!";
			break;
		}
		case "ScenarioEffectTecUpPerm":
		{
			int num9 = (int)Convert.ToDouble(hashtable["value"]);
			Smith randomPlayerSmith6 = getRandomPlayerSmith();
			randomPlayerSmith6.doPermTecBuff(num9);
			aText = randomPlayerSmith6.getSmithName() + ": " + gameData.getTextByRefId("smithStats13") + " +" + num9 + "!";
			break;
		}
		case "ScenarioEffectLucUpPerm":
		{
			int num2 = (int)Convert.ToDouble(hashtable["value"]);
			Smith randomPlayerSmith = getRandomPlayerSmith();
			randomPlayerSmith.doPermLucBuff(num2);
			aText = randomPlayerSmith.getSmithName() + ": " + gameData.getTextByRefId("smithStats14") + " +" + num2 + "!";
			break;
		}
		case "ScenarioEffectStaUpPerm":
		{
			int num10 = (int)Convert.ToDouble(hashtable["value"]);
			Smith randomPlayerSmith7 = getRandomPlayerSmith();
			randomPlayerSmith7.doPermStaminaBuff(num10);
			aText = randomPlayerSmith7.getSmithName() + ": " + gameData.getTextByRefId("smithStats15") + " +" + num10 + "!";
			break;
		}
		case "ScenarioEffectGold":
		{
			int num4 = (int)Convert.ToDouble(hashtable["value"]);
			if (num4 > 0)
			{
				player.addGold(num4);
				audioController.playGoldGainAudio();
				player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeEarningMisc, string.Empty, num4);
				aText = gameData.getTextByRefIdWithDynText("playerStats01", "[gold]", "+" + CommonAPI.formatNumber(num4));
			}
			else
			{
				int aValue4 = -num4;
				aValue4 = player.reduceGold(aValue4, allowNegative: true);
				audioController.playPurchaseAudio();
				player.addShopMonthlyStarchByType(player.getPlayerMonths(), RecordType.RecordTypeExpenseMisc, string.Empty, -aValue4);
				aText = gameData.getTextByRefIdWithDynText("playerStats01", "[gold]", "-" + CommonAPI.formatNumber(num4));
			}
			break;
		}
		case "ScenarioEffectAtkAbs":
			if (player.checkHasForgingProject())
			{
				float num3 = (float)Convert.ToDouble(hashtable["value"]);
				List<int> currentProjectStats3 = player.getCurrentProjectStats();
				if (num3 > 1f)
				{
					num3 -= 1f;
					player.buffCurrentProjectStats(num3, 0f, 0f, 0f);
				}
				else
				{
					num3 -= 1f;
					player.debuffCurrentProjectStats(0f - num3, 0f, 0f, 0f);
				}
				List<int> currentProjectStats4 = player.getCurrentProjectStats();
				aText = gameData.getTextByRefId("projectStats10");
				aText = ((currentProjectStats4[0] <= currentProjectStats3[0]) ? ((currentProjectStats4[0] >= currentProjectStats3[0]) ? (aText + " " + gameData.getTextByRefId("menuGeneral06")) : (aText + " " + gameData.getTextByRefIdWithDynText("projectStats02", "[atk]", (currentProjectStats4[0] - currentProjectStats3[0]).ToString()))) : (aText + " " + gameData.getTextByRefIdWithDynText("projectStats02", "[atk]", "+" + (currentProjectStats4[0] - currentProjectStats3[0]))));
			}
			break;
		case "ScenarioEffectSpdAbs":
			if (player.checkHasForgingProject())
			{
				float num8 = (float)Convert.ToDouble(hashtable["value"]);
				List<int> currentProjectStats9 = player.getCurrentProjectStats();
				if (num8 > 1f)
				{
					num8 -= 1f;
					player.buffCurrentProjectStats(0f, num8, 0f, 0f);
				}
				else
				{
					num8 -= 1f;
					player.debuffCurrentProjectStats(0f, 0f - num8, 0f, 0f);
				}
				List<int> currentProjectStats10 = player.getCurrentProjectStats();
				aText = gameData.getTextByRefId("projectStats10");
				aText = ((currentProjectStats10[1] <= currentProjectStats9[1]) ? ((currentProjectStats10[1] >= currentProjectStats9[1]) ? (aText + " " + gameData.getTextByRefId("menuGeneral06")) : (aText + " " + gameData.getTextByRefIdWithDynText("projectStats03", "[spd]", (currentProjectStats10[1] - currentProjectStats9[1]).ToString()))) : (aText + " " + gameData.getTextByRefIdWithDynText("projectStats03", "[spd]", "+" + (currentProjectStats10[1] - currentProjectStats9[1]))));
			}
			break;
		case "ScenarioEffectAccAbs":
			if (player.checkHasForgingProject())
			{
				float num6 = (float)Convert.ToDouble(hashtable["value"]);
				List<int> currentProjectStats7 = player.getCurrentProjectStats();
				if (num6 > 1f)
				{
					num6 -= 1f;
					player.buffCurrentProjectStats(0f, 0f, num6, 0f);
				}
				else
				{
					num6 -= 1f;
					player.debuffCurrentProjectStats(0f, 0f, 0f - num6, 0f);
				}
				List<int> currentProjectStats8 = player.getCurrentProjectStats();
				aText = gameData.getTextByRefId("projectStats10");
				aText = ((currentProjectStats8[2] <= currentProjectStats7[2]) ? ((currentProjectStats8[2] >= currentProjectStats7[2]) ? (aText + " " + gameData.getTextByRefId("menuGeneral06")) : (aText + " " + gameData.getTextByRefIdWithDynText("projectStats04", "[acc]", (currentProjectStats8[2] - currentProjectStats7[2]).ToString()))) : (aText + " " + gameData.getTextByRefIdWithDynText("projectStats04", "[acc]", "+" + (currentProjectStats8[2] - currentProjectStats7[2]))));
			}
			break;
		case "ScenarioEffectMagAbs":
			if (player.checkHasForgingProject())
			{
				float num5 = (float)Convert.ToDouble(hashtable["value"]);
				List<int> currentProjectStats5 = player.getCurrentProjectStats();
				if (num5 > 1f)
				{
					num5 -= 1f;
					player.buffCurrentProjectStats(0f, 0f, 0f, num5);
				}
				else
				{
					num5 -= 1f;
					player.debuffCurrentProjectStats(0f, 0f, 0f, 0f - num5);
				}
				List<int> currentProjectStats6 = player.getCurrentProjectStats();
				aText = gameData.getTextByRefId("projectStats10");
				aText = ((currentProjectStats6[3] <= currentProjectStats5[3]) ? ((currentProjectStats6[3] >= currentProjectStats5[3]) ? (aText + " " + gameData.getTextByRefId("menuGeneral06")) : (aText + " " + gameData.getTextByRefIdWithDynText("projectStats05", "[mag]", (currentProjectStats6[3] - currentProjectStats5[3]).ToString()))) : (aText + " " + gameData.getTextByRefIdWithDynText("projectStats05", "[mag]", "+" + (currentProjectStats6[3] - currentProjectStats5[3]))));
			}
			break;
		case "ScenarioEffectWeaponStats":
			if (player.checkHasForgingProject())
			{
				float num = (float)Convert.ToDouble(hashtable["value"]);
				List<int> currentProjectStats = player.getCurrentProjectStats();
				if (num > 1f)
				{
					num -= 1f;
					player.buffCurrentProjectStats(num, num, num, num);
				}
				else
				{
					num -= 1f;
					player.debuffCurrentProjectStats(0f - num, 0f - num, 0f - num, 0f - num);
				}
				List<int> currentProjectStats2 = player.getCurrentProjectStats();
				aText = gameData.getTextByRefId("projectStats10");
				string text = string.Empty;
				if (currentProjectStats2[0] > currentProjectStats[0])
				{
					text = text + " " + gameData.getTextByRefIdWithDynText("projectStats02", "[atk]", "+" + (currentProjectStats2[0] - currentProjectStats[0]));
				}
				else if (currentProjectStats2[0] < currentProjectStats[0])
				{
					text = text + " " + gameData.getTextByRefIdWithDynText("projectStats02", "[atk]", (currentProjectStats2[0] - currentProjectStats[0]).ToString());
				}
				if (currentProjectStats2[1] > currentProjectStats[1])
				{
					text = text + " " + gameData.getTextByRefIdWithDynText("projectStats03", "[spd]", "+" + (currentProjectStats2[1] - currentProjectStats[1]));
				}
				else if (currentProjectStats2[1] < currentProjectStats[1])
				{
					text = text + " " + gameData.getTextByRefIdWithDynText("projectStats03", "[spd]", (currentProjectStats2[1] - currentProjectStats[1]).ToString());
				}
				if (currentProjectStats2[2] > currentProjectStats[2])
				{
					text = text + " " + gameData.getTextByRefIdWithDynText("projectStats04", "[acc]", "+" + (currentProjectStats2[2] - currentProjectStats[2]));
				}
				else if (currentProjectStats2[2] < currentProjectStats[2])
				{
					text = text + " " + gameData.getTextByRefIdWithDynText("projectStats04", "[acc]", (currentProjectStats2[2] - currentProjectStats[2]).ToString());
				}
				if (currentProjectStats2[3] > currentProjectStats[3])
				{
					text = text + " " + gameData.getTextByRefIdWithDynText("projectStats05", "[mag]", "+" + (currentProjectStats2[3] - currentProjectStats[3]));
				}
				else if (currentProjectStats2[3] < currentProjectStats[3])
				{
					text = text + " " + gameData.getTextByRefIdWithDynText("projectStats05", "[mag]", (currentProjectStats2[3] - currentProjectStats[3]).ToString());
				}
				if (text == string.Empty)
				{
					text = text + " " + gameData.getTextByRefId("menuGeneral06");
				}
				aText += text;
			}
			break;
		}
		commonScreenObject.getController("ShopViewController").GetComponent<ShopViewController>().showTimeStatus();
		resultText.text = replaceText(empty);
		effectLabel.text = replaceText(aText);
	}

	public Smith getRandomPlayerSmith()
	{
		List<Smith> smithList = game.getPlayer().getSmithList();
		int randomInt = CommonAPI.getRandomInt(smithList.Count);
		return smithList[randomInt];
	}
}
