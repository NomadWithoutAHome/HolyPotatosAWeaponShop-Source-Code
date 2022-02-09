using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIWeaponCompleteController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private Project project;

	private UILabel weaponCompleteTitle;

	private UITexture weaponImg;

	private ParticleSystem weaponParticles;

	private UILabel weaponName;

	private UISprite weaponType;

	private UILabel weaponDesc;

	private UILabel atk;

	private UILabel spd;

	private UILabel acc;

	private UILabel mag;

	private UISprite atkBg;

	private UISprite spdBg;

	private UISprite accBg;

	private UISprite magBg;

	private UILabel atkLabel;

	private UILabel spdLabel;

	private UILabel accLabel;

	private UILabel magLabel;

	private int atkDisplayValue;

	private int spdDisplayValue;

	private int accDisplayValue;

	private int magDisplayValue;

	private ParticleSystem[] fireworkList;

	private UIButton okButton;

	private UILabel okLabel;

	private bool isAnimating;

	private int displayStatSpeed;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		weaponCompleteTitle = commonScreenObject.findChild(base.gameObject, "WeaponCompleteTitle_bg/WeaponCompleteTitle_label").GetComponent<UILabel>();
		weaponImg = commonScreenObject.findChild(base.gameObject, "WeaponBg/WeaponImg").GetComponent<UITexture>();
		weaponName = commonScreenObject.findChild(base.gameObject, "WeaponNameBg/WeaponName").GetComponent<UILabel>();
		weaponType = commonScreenObject.findChild(base.gameObject, "WeaponNameBg/WeaponType").GetComponent<UISprite>();
		weaponDesc = commonScreenObject.findChild(base.gameObject, "WeaponDesc").GetComponent<UILabel>();
		weaponParticles = commonScreenObject.findChild(base.gameObject, "WeaponBg/WeaponParticles").GetComponent<ParticleSystem>();
		GameObject aObject = commonScreenObject.findChild(base.gameObject, "WeaponStats").gameObject;
		atk = commonScreenObject.findChild(aObject, "atk_sprite/atk_label").GetComponent<UILabel>();
		spd = commonScreenObject.findChild(aObject, "spd_sprite/spd_label").GetComponent<UILabel>();
		acc = commonScreenObject.findChild(aObject, "acc_sprite/acc_label").GetComponent<UILabel>();
		mag = commonScreenObject.findChild(aObject, "mag_sprite/mag_label").GetComponent<UILabel>();
		atkBg = commonScreenObject.findChild(aObject, "atk_sprite/atk_bg").GetComponent<UISprite>();
		spdBg = commonScreenObject.findChild(aObject, "spd_sprite/spd_bg").GetComponent<UISprite>();
		accBg = commonScreenObject.findChild(aObject, "acc_sprite/acc_bg").GetComponent<UISprite>();
		magBg = commonScreenObject.findChild(aObject, "mag_sprite/mag_bg").GetComponent<UISprite>();
		atkLabel = commonScreenObject.findChild(aObject, "atk_sprite/atkPriSec_label").GetComponent<UILabel>();
		spdLabel = commonScreenObject.findChild(aObject, "spd_sprite/spdPriSec_label").GetComponent<UILabel>();
		accLabel = commonScreenObject.findChild(aObject, "acc_sprite/accPriSec_label").GetComponent<UILabel>();
		magLabel = commonScreenObject.findChild(aObject, "mag_sprite/magPriSec_label").GetComponent<UILabel>();
		atkDisplayValue = 0;
		accDisplayValue = 0;
		spdDisplayValue = 0;
		magDisplayValue = 0;
		fireworkList = commonScreenObject.findChild(base.gameObject, "Firework").GetComponentsInChildren<ParticleSystem>();
		okButton = commonScreenObject.findChild(base.gameObject, "Ok_button").GetComponent<UIButton>();
		okLabel = commonScreenObject.findChild(base.gameObject, "Ok_button/Ok_label").GetComponent<UILabel>();
		isAnimating = true;
		displayStatSpeed = 1;
	}

	public void processClick(string gameobjectName)
	{
		switch (gameobjectName)
		{
		case "Ok_button":
			if (!isAnimating)
			{
				GameData gameData = game.getGameData();
				Player player = game.getPlayer();
				audioController.playBGMAudio(string.Empty);
				player.endCurrentProject(ProjectState.ProjectStateCompleted);
				GameObject.Find("Panel_ActivitySelect").GetComponent<GUIActivitySelectController>().refreshButtons();
				viewController.closeWeaponComplete(hide: true, resume: true);
				viewController.closeForgeProgress();
			}
			break;
		case "WeaponComplete_bg":
			forceAnimationEnd();
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
		if (!isAnimating && okButton.isEnabled && Input.GetKeyDown(gameData.getKeyCodeByRefID("100008")))
		{
			processClick("Ok_button");
		}
		else if (isAnimating && Input.GetKeyDown(gameData.getKeyCodeByRefID("100008")))
		{
			forceAnimationEnd();
		}
	}

	public void setStats()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		project = player.getCurrentProject();
		setStatDisplaySpeed();
		weaponCompleteTitle.text = gameData.getTextByRefId("menuForgeComplete24").ToUpper(CultureInfo.InvariantCulture);
		okLabel.text = string.Empty;
		okButton.disabledColor = Color.clear;
		okButton.isEnabled = false;
		weaponImg.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + player.getCurrentProjectWeapon().getImage());
		weaponName.text = project.getProjectName(includePrefix: true);
		weaponType.spriteName = "icon_" + project.getProjectWeapon().getWeaponType().getImage();
		weaponDesc.text = project.getProjectWeapon().getWeaponDesc();
		audioController.stopForgeBGLoopAudio();
		atk.text = "0";
		spd.text = "0";
		acc.text = "0";
		mag.text = "0";
		atk.effectStyle = UILabel.Effect.None;
		spd.effectStyle = UILabel.Effect.None;
		acc.effectStyle = UILabel.Effect.None;
		mag.effectStyle = UILabel.Effect.None;
		atkBg.color = new Color(0.0235f, 0.157f, 0.196f);
		spdBg.color = new Color(0.0235f, 0.157f, 0.196f);
		accBg.color = new Color(0.0235f, 0.157f, 0.196f);
		magBg.color = new Color(0.0235f, 0.157f, 0.196f);
		atkLabel.text = string.Empty;
		spdLabel.text = string.Empty;
		accLabel.text = string.Empty;
		magLabel.text = string.Empty;
		List<Smith> inShopSmithList = player.getInShopSmithList();
		if (inShopSmithList.Count > 0)
		{
			int index = Random.Range(0, inShopSmithList.Count);
			Smith smith = inShopSmithList[index];
			string randomTextBySetRefIdWithDynText = gameData.getRandomTextBySetRefIdWithDynText("whetsappWeaponComplete", "[weaponName]", project.getProjectName(includePrefix: true));
			gameData.addNewWhetsappMsg(smith.getSmithName(), randomTextBySetRefIdWithDynText, "Image/Smith/Portraits/" + smith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
		}
		audioController.playNumberUpAudio();
		StartCoroutine(displayResults());
	}

	public bool addWeaponStats(int addAmt)
	{
		bool result = true;
		atkDisplayValue += addAmt;
		if (atkDisplayValue >= project.getAtk())
		{
			atkDisplayValue = Mathf.Min(atkDisplayValue, project.getAtk());
		}
		else
		{
			result = false;
		}
		accDisplayValue += addAmt;
		if (accDisplayValue >= project.getAcc())
		{
			accDisplayValue = Mathf.Min(accDisplayValue, project.getAcc());
		}
		else
		{
			result = false;
		}
		spdDisplayValue += addAmt;
		if (spdDisplayValue >= project.getSpd())
		{
			spdDisplayValue = Mathf.Min(spdDisplayValue, project.getSpd());
		}
		else
		{
			result = false;
		}
		magDisplayValue += addAmt;
		if (magDisplayValue >= project.getMag())
		{
			magDisplayValue = Mathf.Min(magDisplayValue, project.getMag());
		}
		else
		{
			result = false;
		}
		atk.text = atkDisplayValue.ToString();
		spd.text = spdDisplayValue.ToString();
		acc.text = accDisplayValue.ToString();
		mag.text = magDisplayValue.ToString();
		return result;
	}

	public void displayStatsIn4Labels(int statValue, GameObject labelRoot, string color)
	{
		UILabel[] componentsInChildren = labelRoot.GetComponentsInChildren<UILabel>();
		char[] array = statValue.ToString().ToCharArray();
		UILabel[] array2 = componentsInChildren;
		foreach (UILabel uILabel in array2)
		{
			switch (uILabel.gameObject.name)
			{
			case "Stat1":
				if (array.Length > 3)
				{
					uILabel.text = "[" + color + "]" + array[array.Length - 4] + "[-]";
				}
				else
				{
					uILabel.text = string.Empty;
				}
				break;
			case "Stat2":
				if (array.Length > 2)
				{
					uILabel.text = "[" + color + "]" + array[array.Length - 3] + "[-]";
				}
				else
				{
					uILabel.text = string.Empty;
				}
				break;
			case "Stat3":
				if (array.Length > 1)
				{
					uILabel.text = "[" + color + "]" + array[array.Length - 2] + "[-]";
				}
				else
				{
					uILabel.text = string.Empty;
				}
				break;
			case "Stat4":
				uILabel.text = "[" + color + "]" + array[array.Length - 1] + "[-]";
				break;
			}
		}
	}

	public void setStatDisplaySpeed()
	{
		List<int> list = project.calculateHeroWeaponStats();
		int num = Mathf.Max(list.ToArray());
		displayStatSpeed = num / 30 + 1;
	}

	private void displayPriSecStat()
	{
		GameData gameData = game.getGameData();
		List<WeaponStat> priSecStat = project.getPriSecStat();
		if (priSecStat.Count > 0)
		{
			switch (priSecStat[0])
			{
			case WeaponStat.WeaponStatAttack:
				atkLabel.text = gameData.getTextByRefId("menuForgeComplete25").ToUpper(CultureInfo.InvariantCulture);
				atkLabel.color = new Color(1f, 0.827f, 0.478f);
				atk.effectStyle = UILabel.Effect.Outline;
				atk.fontSize = 18;
				atkBg.color = new Color(0.0196f, 0.788f, 0.659f);
				break;
			case WeaponStat.WeaponStatSpeed:
				spdLabel.text = gameData.getTextByRefId("menuForgeComplete25").ToUpper(CultureInfo.InvariantCulture);
				spdLabel.color = new Color(1f, 0.827f, 0.478f);
				spd.effectStyle = UILabel.Effect.Outline;
				spd.fontSize = 18;
				spdBg.color = new Color(0.0196f, 0.788f, 0.659f);
				break;
			case WeaponStat.WeaponStatAccuracy:
				accLabel.text = gameData.getTextByRefId("menuForgeComplete25").ToUpper(CultureInfo.InvariantCulture);
				accLabel.color = new Color(1f, 0.827f, 0.478f);
				acc.effectStyle = UILabel.Effect.Outline;
				acc.fontSize = 18;
				accBg.color = new Color(0.0196f, 0.788f, 0.659f);
				break;
			case WeaponStat.WeaponStatMagic:
				magLabel.text = gameData.getTextByRefId("menuForgeComplete25").ToUpper(CultureInfo.InvariantCulture);
				magLabel.color = new Color(1f, 0.827f, 0.478f);
				mag.effectStyle = UILabel.Effect.Outline;
				mag.fontSize = 18;
				magBg.color = new Color(0.0196f, 0.788f, 0.659f);
				break;
			}
		}
		if (priSecStat.Count > 1)
		{
			Player player = game.getPlayer();
			GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
			string gameLockSet = gameScenarioByRefId.getGameLockSet();
			int completedTutorialIndex = player.getCompletedTutorialIndex();
			string text = string.Empty;
			if (gameData.checkFeatureIsUnlocked(gameLockSet, "SECSTAT", completedTutorialIndex))
			{
				text = gameData.getTextByRefId("menuForgeComplete26").ToUpper(CultureInfo.InvariantCulture);
			}
			switch (priSecStat[1])
			{
			case WeaponStat.WeaponStatAttack:
				atkLabel.text = text;
				atkLabel.color = new Color(0.447f, 1f, 0.902f);
				atkBg.color = new Color(0.0235f, 0.522f, 0.439f);
				break;
			case WeaponStat.WeaponStatSpeed:
				spdLabel.text = text;
				spdLabel.color = new Color(0.447f, 1f, 0.902f);
				spdBg.color = new Color(0.0235f, 0.522f, 0.439f);
				break;
			case WeaponStat.WeaponStatAccuracy:
				accLabel.text = text;
				accLabel.color = new Color(0.447f, 1f, 0.902f);
				accBg.color = new Color(0.0235f, 0.522f, 0.439f);
				break;
			case WeaponStat.WeaponStatMagic:
				magLabel.text = text;
				magLabel.color = new Color(0.447f, 1f, 0.902f);
				magBg.color = new Color(0.0235f, 0.522f, 0.439f);
				break;
			}
		}
	}

	private void forceAnimationEnd()
	{
		addWeaponStats(10000000);
	}

	private IEnumerator displayResults()
	{
		while (isAnimating)
		{
			if (addWeaponStats(displayStatSpeed))
			{
				audioController.stopNumberUpAudio();
				GameData gameData = game.getGameData();
				displayPriSecStat();
				isAnimating = false;
				okLabel.text = gameData.getTextByRefId("menuGeneral04");
				ParticleSystem[] array = fireworkList;
				foreach (ParticleSystem particleSystem in array)
				{
					particleSystem.Play();
				}
				weaponParticles.Play();
				audioController.stopBGMAudio();
				audioController.playForgeCompleteAudio();
				okButton.isEnabled = true;
			}
			yield return new WaitForSeconds(0.05f);
		}
	}
}
