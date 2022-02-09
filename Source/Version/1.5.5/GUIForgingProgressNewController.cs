using System.Collections.Generic;
using UnityEngine;

public class GUIForgingProgressNewController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private Project currProject;

	private UILabel questTitle;

	private GameObject heroFrame;

	private GameObject heroBubbleText;

	private UILabel heroBubbleLabel;

	private UITexture heroImg;

	private UILabel heroNameLabel;

	private GameObject weaponFrame;

	private UILabel nowForgingLabel;

	private UITexture weaponImg;

	private UITexture weaponSilhouette;

	private UILabel weaponNameLabel;

	private int currentPerc;

	private UILabel forgePercentageLabel;

	private GameObject contractBarBg;

	private UISlider contractProgressBar;

	private UILabel contractBarStatus;

	private UILabel contractQuestName;

	private GameObject contractFinishButton;

	private GameObject weaponStats;

	private UILabel currAtk;

	private UILabel reqAtk;

	private UILabel currAcc;

	private UILabel reqAcc;

	private UILabel currSpd;

	private UILabel reqSpd;

	private UILabel currMag;

	private UILabel reqMag;

	private UISprite weaponFrameBg;

	private GameObject questProgressBar;

	private GameObject questPercentageBubble;

	private UILabel questPercentageLabel;

	private GameObject claimRewardButton;

	private int tempAtk;

	private int tempAcc;

	private int tempSpd;

	private int tempMag;

	private GameObject atkPop;

	private GameObject accPop;

	private GameObject spdPop;

	private GameObject magPop;

	private int prevHeroCommentPercent;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		currProject = null;
		questTitle = commonScreenObject.findChild(base.gameObject, "QuestTitle").GetComponent<UILabel>();
		heroFrame = commonScreenObject.findChild(base.gameObject, "HeroFrame").gameObject;
		heroBubbleText = commonScreenObject.findChild(heroFrame, "HeroBubbleText").gameObject;
		heroBubbleLabel = commonScreenObject.findChild(heroBubbleText, "HeroBubbleLabel").GetComponent<UILabel>();
		heroImg = commonScreenObject.findChild(heroFrame, "HeroImg").GetComponent<UITexture>();
		heroNameLabel = commonScreenObject.findChild(heroFrame, "HeroNameLabel").GetComponent<UILabel>();
		weaponFrame = commonScreenObject.findChild(base.gameObject, "WeaponFrame").gameObject;
		nowForgingLabel = commonScreenObject.findChild(weaponFrame, "NowForgingLabel").GetComponent<UILabel>();
		weaponImg = commonScreenObject.findChild(weaponFrame, "WeaponImg").GetComponent<UITexture>();
		weaponSilhouette = commonScreenObject.findChild(weaponFrame, "WeaponSilhouette").GetComponent<UITexture>();
		weaponNameLabel = commonScreenObject.findChild(weaponFrame, "WeaponNameLabel").GetComponent<UILabel>();
		currentPerc = 0;
		forgePercentageLabel = commonScreenObject.findChild(weaponFrame, "ForgePercentageLabel").GetComponent<UILabel>();
		contractBarBg = commonScreenObject.findChild(base.gameObject, "ContractBarBg").gameObject;
		contractProgressBar = commonScreenObject.findChild(contractBarBg, "ContractProgressBar").GetComponent<UISlider>();
		contractBarStatus = commonScreenObject.findChild(contractBarBg, "ContractBarStatus").GetComponent<UILabel>();
		contractQuestName = commonScreenObject.findChild(contractBarBg, "ContractQuestName").GetComponent<UILabel>();
		contractFinishButton = commonScreenObject.findChild(base.gameObject, "ContractFinishButton").gameObject;
		weaponStats = commonScreenObject.findChild(base.gameObject, "WeaponStats").gameObject;
		currAtk = commonScreenObject.findChild(weaponStats, "Atk/CurrAtk").GetComponent<UILabel>();
		reqAtk = commonScreenObject.findChild(weaponStats, "Atk/ReqAtk").GetComponent<UILabel>();
		currAcc = commonScreenObject.findChild(weaponStats, "Acc/CurrAcc").GetComponent<UILabel>();
		reqAcc = commonScreenObject.findChild(weaponStats, "Acc/ReqAcc").GetComponent<UILabel>();
		currSpd = commonScreenObject.findChild(weaponStats, "Spd/CurrSpd").GetComponent<UILabel>();
		reqSpd = commonScreenObject.findChild(weaponStats, "Spd/ReqSpd").GetComponent<UILabel>();
		currMag = commonScreenObject.findChild(weaponStats, "Mag/CurrMag").GetComponent<UILabel>();
		reqMag = commonScreenObject.findChild(weaponStats, "Mag/ReqMag").GetComponent<UILabel>();
		weaponFrameBg = commonScreenObject.findChild(base.gameObject, "WeaponFrame/WeaponFrameBg").GetComponent<UISprite>();
		questProgressBar = commonScreenObject.findChild(base.gameObject, "QuestProgressBar").gameObject;
		questPercentageBubble = commonScreenObject.findChild(questProgressBar, "QuestProgressThumb/QuestPercentageBubble").gameObject;
		questPercentageLabel = commonScreenObject.findChild(questProgressBar, "QuestProgressThumb/QuestPercentageBubble/QuestPercentageLabel").GetComponent<UILabel>();
		claimRewardButton = commonScreenObject.findChild(base.gameObject, "ClaimRewardButton").gameObject;
		tempAtk = -1;
		tempAcc = -1;
		tempSpd = -1;
		tempMag = -1;
		atkPop = GameObject.Find("AtkPop");
		accPop = GameObject.Find("AccPop");
		spdPop = GameObject.Find("SpdPop");
		magPop = GameObject.Find("MagPop");
		prevHeroCommentPercent = 0;
	}

	public void processClick(string gameobjectName)
	{
		switch (gameobjectName)
		{
		case "ContractFinishButton":
			break;
		case "ClaimRewardButton":
			break;
		}
	}

	private void showBubbleText(string aText)
	{
		heroBubbleLabel.text = aText;
		commonScreenObject.tweenScale(heroBubbleText.GetComponent<TweenScale>(), Vector3.zero, Vector3.one, 2f, null, string.Empty);
	}

	private void setContractPercentage(int aValue)
	{
		contractProgressBar.GetComponent<UISlider>().value = (float)aValue / 100f;
	}

	private void setForgePercentage(int aValue)
	{
		forgePercentageLabel.text = aValue + "%";
		weaponImg.fillAmount = (float)aValue / 100f;
	}

	private void setQuestPercentage(int aValue)
	{
		questPercentageLabel.text = aValue + "%";
		questProgressBar.GetComponent<UISlider>().value = (float)aValue / 100f;
	}

	public void setReference()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		currProject = game.getPlayer().getCurrentProject();
		Weapon projectWeapon = currProject.getProjectWeapon();
		setForgePercentage(0);
		heroBubbleText.transform.localScale = Vector3.zero;
		switch (currProject.getProjectType())
		{
		case ProjectType.ProjectTypeWeapon:
			nowForgingLabel.text = gameData.getTextByRefId("projectStats11");
			weaponImg.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + projectWeapon.getImage());
			weaponSilhouette.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + projectWeapon.getImage());
			weaponNameLabel.text = currProject.getProjectName(includePrefix: true);
			contractBarBg.SetActive(value: false);
			break;
		case ProjectType.ProjectTypeContract:
		{
			heroFrame.SetActive(value: false);
			weaponFrame.SetActive(value: false);
			Contract projectContract = currProject.getProjectContract();
			contractQuestName.text = projectContract.getContractName();
			questTitle.text = projectContract.getContractName();
			break;
		}
		}
		reqAtk.text = CommonAPI.formatNumber(currProject.getAtkReq());
		reqAcc.text = CommonAPI.formatNumber(currProject.getAccReq());
		reqSpd.text = CommonAPI.formatNumber(currProject.getSpdReq());
		reqMag.text = CommonAPI.formatNumber(currProject.getMagReq());
		setQuestPercentage(0);
		weaponFrameBg.enabled = false;
		questProgressBar.SetActive(value: false);
		claimRewardButton.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("questHud02");
		claimRewardButton.SetActive(value: false);
		contractFinishButton.SetActive(value: false);
		prevHeroCommentPercent = 0;
		refreshStats();
	}

	public void setQuest()
	{
		weaponStats.SetActive(value: false);
		weaponFrameBg.enabled = true;
		forgePercentageLabel.text = string.Empty;
		questProgressBar.SetActive(value: true);
	}

	public void refreshQuestStats()
	{
	}

	public void refreshStats()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		switch (currProject.getProjectType())
		{
		case ProjectType.ProjectTypeContract:
		{
			int projectProgressPercent2 = player.getProjectProgressPercent();
			if (projectProgressPercent2 < 100)
			{
				long playerTimeLong = player.getPlayerTimeLong();
				long contractTimeLeft = player.getCurrentProject().getContractTimeLeft(playerTimeLong);
				string textByRefIdWithDynText = game.getGameData().getTextByRefIdWithDynText("projectStats07", "[timeLeft]", CommonAPI.convertHalfHoursToTimeString(contractTimeLeft));
				if (projectProgressPercent2 / 10 == prevHeroCommentPercent / 10 + 1)
				{
					showBubbleText(gameData.getTextByRefId(gameData.getRandomTextBySetRefId("forgingHeroText")));
					prevHeroCommentPercent = projectProgressPercent2;
				}
				contractBarStatus.text = projectProgressPercent2 + "% " + textByRefIdWithDynText;
			}
			else
			{
				contractBarBg.SetActive(value: false);
				contractFinishButton.SetActive(value: true);
			}
			setContractPercentage(projectProgressPercent2);
			break;
		}
		case ProjectType.ProjectTypeWeapon:
		{
			weaponNameLabel.text = player.getCurrentProject().getProjectName(includePrefix: true);
			int projectProgressPercent = player.getProjectProgressPercent();
			setForgePercentage(projectProgressPercent);
			if (projectProgressPercent / 10 == prevHeroCommentPercent / 10 + 1)
			{
				showBubbleText(gameData.getTextByRefId(gameData.getRandomTextBySetRefId("forgingHeroText")));
				prevHeroCommentPercent = projectProgressPercent;
			}
			break;
		}
		}
		List<int> currentProjectStats = player.getCurrentProjectStats();
		if (currentProjectStats[0] > tempAtk)
		{
			atkPop.GetComponent<UILabel>().text = "+" + (currentProjectStats[0] - tempAtk);
			TweenPosition component = atkPop.GetComponent<TweenPosition>();
			component.enabled = true;
			component.ResetToBeginning();
			component.PlayForward();
			TweenColor component2 = atkPop.GetComponent<TweenColor>();
			component2.enabled = true;
			component2.ResetToBeginning();
			component2.PlayForward();
		}
		tempAtk = currentProjectStats[0];
		string text = "FF4842";
		if (currentProjectStats[0] >= currProject.getAtkReq())
		{
			text = "56AE59";
		}
		currAtk.text = "[" + text + "]" + CommonAPI.formatNumber(currentProjectStats[0]) + "[-]";
		if (currentProjectStats[2] > tempAcc)
		{
			accPop.GetComponent<UILabel>().text = "+" + (currentProjectStats[2] - tempAcc);
			TweenPosition component = accPop.GetComponent<TweenPosition>();
			component.enabled = true;
			component.ResetToBeginning();
			component.PlayForward();
			TweenColor component2 = accPop.GetComponent<TweenColor>();
			component2.enabled = true;
			component2.ResetToBeginning();
			component2.PlayForward();
		}
		tempAcc = currentProjectStats[2];
		string text2 = "FF4842";
		if (currentProjectStats[2] >= currProject.getAccReq())
		{
			text2 = "56AE59";
		}
		currAcc.text = "[" + text2 + "]" + CommonAPI.formatNumber(currentProjectStats[2]) + "[-]";
		if (currentProjectStats[1] > tempSpd)
		{
			spdPop.GetComponent<UILabel>().text = "+" + (currentProjectStats[1] - tempSpd);
			TweenPosition component = spdPop.GetComponent<TweenPosition>();
			component.enabled = true;
			component.ResetToBeginning();
			component.PlayForward();
			TweenColor component2 = spdPop.GetComponent<TweenColor>();
			component2.enabled = true;
			component2.ResetToBeginning();
			component2.PlayForward();
		}
		tempSpd = currentProjectStats[1];
		string text3 = "FF4842";
		if (currentProjectStats[1] >= currProject.getSpdReq())
		{
			text3 = "56AE59";
		}
		currSpd.text = "[" + text3 + "]" + CommonAPI.formatNumber(currentProjectStats[1]) + "[-]";
		if (currentProjectStats[3] > tempMag)
		{
			magPop.GetComponent<UILabel>().text = "+" + (currentProjectStats[3] - tempMag);
			TweenPosition component = magPop.GetComponent<TweenPosition>();
			component.enabled = true;
			component.ResetToBeginning();
			component.PlayForward();
			TweenColor component2 = magPop.GetComponent<TweenColor>();
			component2.enabled = true;
			component2.ResetToBeginning();
			component2.PlayForward();
		}
		tempMag = currentProjectStats[3];
		string text4 = "FF4842";
		if (currentProjectStats[3] >= currProject.getMagReq())
		{
			text4 = "56AE59";
		}
		currMag.text = "[" + text4 + "]" + CommonAPI.formatNumber(currentProjectStats[3]) + "[-]";
	}

	public Project getProjectInfo()
	{
		return currProject;
	}
}
