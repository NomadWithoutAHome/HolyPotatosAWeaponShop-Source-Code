using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUILegendaryCompleteController : MonoBehaviour
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

	private UILabel acc;

	private UILabel spd;

	private UILabel mag;

	private int atkDisplayValue;

	private int accDisplayValue;

	private int spdDisplayValue;

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
		acc = commonScreenObject.findChild(aObject, "acc_sprite/acc_label").GetComponent<UILabel>();
		spd = commonScreenObject.findChild(aObject, "spd_sprite/spd_label").GetComponent<UILabel>();
		mag = commonScreenObject.findChild(aObject, "mag_sprite/mag_label").GetComponent<UILabel>();
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
		{
			if (isAnimating)
			{
				break;
			}
			Player player = game.getPlayer();
			audioController.playBGMAudio(string.Empty);
			if (project.getProjectType() == ProjectType.ProjectTypeUnique)
			{
				player.endCurrentProject(ProjectState.ProjectStateCompleted);
				viewController.closeLegendaryComplete(hide: false, resume: true);
				viewController.closeForgeProgress();
				LegendaryHero ongoingLegendaryHeroByWeapon = player.getOngoingLegendaryHeroByWeapon(project.getProjectWeapon().getWeaponRefId());
				if (ongoingLegendaryHeroByWeapon.getLegendaryHeroRefId() != string.Empty)
				{
					GameData gameData = game.getGameData();
					if (ongoingLegendaryHeroByWeapon.trySubmitWeapon(project))
					{
						string forgeSuccessDialogueRefId = ongoingLegendaryHeroByWeapon.getForgeSuccessDialogueRefId();
						CommonAPI.debug("showDialoguePopup PopupTypeLegendaryRequestSuccess");
						viewController.showDialoguePopup(forgeSuccessDialogueRefId, gameData.getDialogueBySetId(forgeSuccessDialogueRefId), PopupType.PopupTypeLegendaryRequestSuccess);
						project.setProjectSaleState(ProjectSaleState.ProjectSaleStateDelivered);
					}
					else
					{
						string forgeFailDialogueSetId = ongoingLegendaryHeroByWeapon.getForgeFailDialogueSetId();
						viewController.showDialoguePopup(forgeFailDialogueSetId, gameData.getDialogueBySetId(forgeFailDialogueSetId), PopupType.PopupTypeLegendaryRequestFail);
						gameData.addNewWhetsappMsg(ongoingLegendaryHeroByWeapon.getLegendaryHeroName(), ongoingLegendaryHeroByWeapon.getFailComment(), "Image/legendary heroes/" + ongoingLegendaryHeroByWeapon.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeHero);
						project.setProjectSaleState(ProjectSaleState.ProjectSaleStateRejected);
					}
				}
			}
			else
			{
				player.endCurrentProject(ProjectState.ProjectStateCompleted);
				GameObject.Find("Panel_ActivitySelect").GetComponent<GUIActivitySelectController>().refreshButtons();
				viewController.closeLegendaryComplete(hide: false, resume: true);
				viewController.closeForgeProgress();
			}
			break;
		}
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
		weaponName.text = project.getProjectName(includePrefix: false);
		weaponType.spriteName = "icon_" + project.getProjectWeapon().getWeaponType().getImage();
		weaponDesc.text = project.getProjectWeapon().getWeaponDesc();
		List<int> currentProjectStats = player.getCurrentProjectStats();
		audioController.stopForgeBGLoopAudio();
		atk.text = "0";
		spd.text = "0";
		acc.text = "0";
		mag.text = "0";
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

	private void forceAnimationEnd()
	{
		addWeaponStats(10000000);
	}

	public void setStatDisplaySpeed()
	{
		List<int> list = project.calculateHeroWeaponStats();
		int num = Mathf.Max(list.ToArray());
		displayStatSpeed = num / 30 + 1;
	}

	private IEnumerator displayResults()
	{
		while (isAnimating)
		{
			yield return new WaitForSeconds(0.05f);
			if (addWeaponStats(displayStatSpeed))
			{
				audioController.stopNumberUpAudio();
				GameData gameData = game.getGameData();
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
		}
	}
}
