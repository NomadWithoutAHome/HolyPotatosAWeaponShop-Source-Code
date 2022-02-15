using System.Collections.Generic;
using UnityEngine;

public class GUIProjectProgressController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private AudioController audioController;

	private UILabel nowSmithingLabel;

	private UILabel currentWeaponName;

	private UILabel projectPercentageLabel;

	private UISlider projectStatusBar;

	private GameObject greenBoard;

	private GameObject currentWeaponStats;

	private UISprite atkIcon;

	private UISprite accIcon;

	private UISprite spdIcon;

	private UISprite magIcon;

	private UILabel atkStats;

	private UILabel accStats;

	private UILabel spdStats;

	private UILabel magStats;

	private int tempAtk;

	private int tempAcc;

	private int tempSpd;

	private int tempMag;

	private GameObject atkPop;

	private GameObject accPop;

	private GameObject spdPop;

	private GameObject magPop;

	private TweenAlpha[] dotsTween;

	private UILabel[] dotsLabel;

	private bool toResume;

	private string phaseString;

	private Vector3 defaultGreenBoardPos;

	private Color transparentColor;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		nowSmithingLabel = GameObject.Find("NowSmithingLabel").GetComponent<UILabel>();
		currentWeaponName = GameObject.Find("CurrentWeaponName").GetComponent<UILabel>();
		projectPercentageLabel = GameObject.Find("ProjectPercentageLabel").GetComponent<UILabel>();
		projectStatusBar = GameObject.Find("ProjectStatusBar").GetComponent<UISlider>();
		greenBoard = GameObject.Find("GreenBoard");
		currentWeaponStats = GameObject.Find("CurrentWeaponStats");
		atkIcon = GameObject.Find("AtkIcon").GetComponent<UISprite>();
		accIcon = GameObject.Find("AccIcon").GetComponent<UISprite>();
		spdIcon = GameObject.Find("SpdIcon").GetComponent<UISprite>();
		magIcon = GameObject.Find("MagIcon").GetComponent<UISprite>();
		atkStats = GameObject.Find("AtkStats").GetComponent<UILabel>();
		accStats = GameObject.Find("AccStats").GetComponent<UILabel>();
		spdStats = GameObject.Find("SpdStats").GetComponent<UILabel>();
		magStats = GameObject.Find("MagStats").GetComponent<UILabel>();
		tempAtk = 0;
		tempAcc = 0;
		tempSpd = 0;
		tempMag = 0;
		atkPop = GameObject.Find("AtkPop");
		accPop = GameObject.Find("AccPop");
		spdPop = GameObject.Find("SpdPop");
		magPop = GameObject.Find("MagPop");
		dotsTween = GameObject.Find("Dots").GetComponentsInChildren<TweenAlpha>();
		dotsLabel = GameObject.Find("Dots").GetComponentsInChildren<UILabel>();
		phaseString = string.Empty;
		defaultGreenBoardPos = new Vector3(0f, 70.25f, 0f);
		transparentColor = Color.white;
		transparentColor.a = 0f;
		disableStats();
	}

	public void disableStats()
	{
		nowSmithingLabel.enabled = false;
		currentWeaponName.enabled = false;
		projectPercentageLabel.enabled = false;
		projectStatusBar.value = 0f;
		greenBoard.transform.localPosition = defaultGreenBoardPos;
		greenBoard.transform.localRotation = Quaternion.Euler(-180f, 0f, 0f);
		phaseString = string.Empty;
		TweenAlpha[] array = dotsTween;
		foreach (TweenAlpha tweenAlpha in array)
		{
			tweenAlpha.enabled = false;
		}
		UILabel[] array2 = dotsLabel;
		foreach (UILabel uILabel in array2)
		{
			uILabel.color = transparentColor;
		}
		tempAtk = 0;
		tempAcc = 0;
		tempSpd = 0;
		tempMag = 0;
		atkPop.GetComponent<TweenPosition>().enabled = false;
		atkPop.GetComponent<TweenColor>().enabled = false;
		accPop.GetComponent<TweenPosition>().enabled = false;
		accPop.GetComponent<TweenColor>().enabled = false;
		spdPop.GetComponent<TweenPosition>().enabled = false;
		spdPop.GetComponent<TweenColor>().enabled = false;
		magPop.GetComponent<TweenPosition>().enabled = false;
		magPop.GetComponent<TweenColor>().enabled = false;
		currentWeaponStats.SetActive(value: false);
	}

	private void enableStats()
	{
		nowSmithingLabel.enabled = true;
		currentWeaponName.enabled = true;
		projectPercentageLabel.enabled = true;
		atkIcon.enabled = true;
		accIcon.enabled = true;
		spdIcon.enabled = true;
		magIcon.enabled = true;
		atkStats.color = Color.black;
		accStats.color = Color.black;
		spdStats.color = Color.black;
		magStats.color = Color.black;
		TweenAlpha[] array = dotsTween;
		foreach (TweenAlpha tweenAlpha in array)
		{
			tweenAlpha.enabled = true;
		}
		currentWeaponStats.SetActive(value: true);
	}

	public void startAnimate(bool resume = true, bool loadSuccess = false)
	{
		float aDuration = 0.4f;
		if (loadSuccess)
		{
			aDuration = 0.1f;
		}
		toResume = resume;
		commonScreenObject.tweenRotation(greenBoard.GetComponent<TweenRotation>(), new Vector3(180f, 0f, 0f), new Vector3(0f, 0f, 0f), aDuration, null, null);
		commonScreenObject.tweenPosition(greenBoard.GetComponent<TweenPosition>(), defaultGreenBoardPos, new Vector3(0f, 64.5f, 0f), aDuration, base.gameObject, "setStats");
		audioController.playForgeStartAudio();
		audioController.startForgeBGLoopAudio();
	}

	public void setStats()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		enableStats();
		switch (player.getCurrentProject().getProjectType())
		{
		case ProjectType.ProjectTypeWeapon:
			nowSmithingLabel.text = gameData.getTextByRefId("projectProgress01");
			currentWeaponName.text = player.getCurrentProjectWeapon().getWeaponName();
			switch (player.getCurrentProject().getProjectPhase())
			{
			case ProjectPhase.ProjectPhaseDesignDone:
				phaseString = gameData.getTextByRefId("projectProgress02") + "... ";
				break;
			case ProjectPhase.ProjectPhaseCraftDone:
				phaseString = gameData.getTextByRefId("projectProgress03") + "... ";
				break;
			case ProjectPhase.ProjectPhasePolishDone:
				phaseString = gameData.getTextByRefId("projectProgress04") + "... ";
				break;
			case ProjectPhase.ProjectPhaseEnchantDone:
				phaseString = gameData.getTextByRefId("projectProgress05") + "... ";
				break;
			}
			break;
		case ProjectType.ProjectTypeContract:
			nowSmithingLabel.text = gameData.getTextByRefId("projectProgress06");
			currentWeaponName.text = player.getCurrentProject().getProjectName(includePrefix: false);
			phaseString = string.Empty;
			if (player.getCurrentProject().getAtkReq() == 0)
			{
				atkIcon.enabled = false;
				atkStats.text = string.Empty;
			}
			if (player.getCurrentProject().getAccReq() == 0)
			{
				accIcon.enabled = false;
				accStats.text = string.Empty;
			}
			if (player.getCurrentProject().getSpdReq() == 0)
			{
				spdIcon.enabled = false;
				spdStats.text = string.Empty;
			}
			if (player.getCurrentProject().getMagReq() == 0)
			{
				magIcon.enabled = false;
				magStats.text = string.Empty;
			}
			break;
		}
		refreshStats();
		if (toResume)
		{
			GameObject.Find("ViewController").GetComponent<ViewController>().resumeEverything();
		}
	}

	public void refreshStats()
	{
		Player player = game.getPlayer();
		switch (player.getCurrentProject().getProjectType())
		{
		case ProjectType.ProjectTypeWeapon:
		{
			projectPercentageLabel.text = phaseString + player.getProjectProgressPercent() + "%";
			projectStatusBar.value = (float)player.getProjectProgressPercent() / 100f;
			List<int> currentProjectStats2 = player.getCurrentProjectStats();
			if (currentProjectStats2[0] > tempAtk)
			{
				atkPop.GetComponent<UILabel>().text = "+" + (currentProjectStats2[0] - tempAtk);
				TweenPosition component = atkPop.GetComponent<TweenPosition>();
				component.enabled = true;
				component.ResetToBeginning();
				component.PlayForward();
				TweenColor component2 = atkPop.GetComponent<TweenColor>();
				component2.enabled = true;
				component2.ResetToBeginning();
				component2.PlayForward();
			}
			tempAtk = currentProjectStats2[0];
			atkStats.text = CommonAPI.formatNumber(currentProjectStats2[0]);
			if (currentProjectStats2[2] > tempAcc)
			{
				accPop.GetComponent<UILabel>().text = "+" + (currentProjectStats2[2] - tempAcc);
				TweenPosition component = accPop.GetComponent<TweenPosition>();
				component.enabled = true;
				component.ResetToBeginning();
				component.PlayForward();
				TweenColor component2 = accPop.GetComponent<TweenColor>();
				component2.enabled = true;
				component2.ResetToBeginning();
				component2.PlayForward();
			}
			tempAcc = currentProjectStats2[2];
			accStats.text = CommonAPI.formatNumber(currentProjectStats2[2]);
			if (currentProjectStats2[1] > tempSpd)
			{
				spdPop.GetComponent<UILabel>().text = "+" + (currentProjectStats2[1] - tempSpd);
				TweenPosition component = spdPop.GetComponent<TweenPosition>();
				component.enabled = true;
				component.ResetToBeginning();
				component.PlayForward();
				TweenColor component2 = spdPop.GetComponent<TweenColor>();
				component2.enabled = true;
				component2.ResetToBeginning();
				component2.PlayForward();
			}
			tempSpd = currentProjectStats2[1];
			spdStats.text = CommonAPI.formatNumber(currentProjectStats2[1]);
			if (currentProjectStats2[3] > tempMag)
			{
				magPop.GetComponent<UILabel>().text = "+" + (currentProjectStats2[3] - tempMag);
				TweenPosition component = magPop.GetComponent<TweenPosition>();
				component.enabled = true;
				component.ResetToBeginning();
				component.PlayForward();
				TweenColor component2 = magPop.GetComponent<TweenColor>();
				component2.enabled = true;
				component2.ResetToBeginning();
				component2.PlayForward();
			}
			tempMag = currentProjectStats2[3];
			magStats.text = CommonAPI.formatNumber(currentProjectStats2[3]);
			break;
		}
		case ProjectType.ProjectTypeContract:
		{
			long playerTimeLong = player.getPlayerTimeLong();
			long contractTimeLeft = player.getCurrentProject().getContractTimeLeft(playerTimeLong);
			string textByRefIdWithDynText = game.getGameData().getTextByRefIdWithDynText("projectStats07", "[timeLeft]", CommonAPI.convertHalfHoursToTimeString(contractTimeLeft));
			projectPercentageLabel.text = phaseString + player.getProjectProgressPercent() + "% " + textByRefIdWithDynText;
			projectStatusBar.value = (float)player.getProjectProgressPercent() / 100f;
			List<int> currentProjectStats = player.getCurrentProjectStats();
			if (atkIcon.enabled)
			{
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
				atkStats.text = CommonAPI.formatNumber(currentProjectStats[0]);
			}
			if (accIcon.enabled)
			{
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
				accStats.text = CommonAPI.formatNumber(currentProjectStats[2]);
			}
			if (spdIcon.enabled)
			{
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
				spdStats.text = CommonAPI.formatNumber(currentProjectStats[1]);
			}
			if (magIcon.enabled)
			{
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
				magStats.text = CommonAPI.formatNumber(currentProjectStats[3]);
			}
			break;
		}
		}
	}
}
