using System.Collections.Generic;
using UnityEngine;

public class GUIForgeCompleteController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private AudioController audioController;

	private Project project;

	private UILabel finishedLabel;

	private UITexture weaponImg;

	private UILabel weaponName;

	private UILabel weaponDesc;

	private UILabel atkStats;

	private UILabel accStats;

	private UILabel spdStats;

	private UILabel magStats;

	private UILabel okLabel;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		finishedLabel = commonScreenObject.findChild(base.gameObject, "FinishedFrame/FinishedLabel").GetComponent<UILabel>();
		weaponImg = commonScreenObject.findChild(base.gameObject, "WeaponFrame/WeaponImg").GetComponent<UITexture>();
		weaponName = commonScreenObject.findChild(base.gameObject, "WeaponStats/WeaponName").GetComponent<UILabel>();
		weaponDesc = commonScreenObject.findChild(base.gameObject, "WeaponStats/WeaponDesc").GetComponent<UILabel>();
		atkStats = commonScreenObject.findChild(base.gameObject, "WeaponStats/AtkStats").GetComponent<UILabel>();
		accStats = commonScreenObject.findChild(base.gameObject, "WeaponStats/AccStats").GetComponent<UILabel>();
		spdStats = commonScreenObject.findChild(base.gameObject, "WeaponStats/SpdStats").GetComponent<UILabel>();
		magStats = commonScreenObject.findChild(base.gameObject, "WeaponStats/MagStats").GetComponent<UILabel>();
		okLabel = commonScreenObject.findChild(base.gameObject, "OkButton/OkLabel").GetComponent<UILabel>();
	}

	public void processClick(string gameobjectName)
	{
		if (gameobjectName != null && gameobjectName == "OkButton")
		{
			audioController.playBGMAudio(string.Empty);
			GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>().showAppraisalPopup();
		}
	}

	public void setStats()
	{
		Player player = game.getPlayer();
		finishedLabel.text = string.Empty;
		finishedLabel.gameObject.GetComponent<TextStampScript>().setTextStampAnim(game.getGameData().getTextByRefId("menuForgeComplete24"), 30f, 1f, 0f, 0f, 0.08f, 0.1f, Color.white, new Color(0.7f, 0.7f, 0.7f), 3);
		weaponImg.mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + player.getCurrentProjectWeapon().getImage());
		weaponName.text = player.getProjectName(includePrefix: true);
		weaponDesc.text = player.getCurrentProjectWeapon().getWeaponDesc();
		List<int> currentProjectStats = player.getCurrentProjectStats();
		CommonAPI.debug("atk: " + currentProjectStats[0] + " acc: " + currentProjectStats[2] + " spd: " + currentProjectStats[1] + " mag: " + currentProjectStats[3]);
		atkStats.text = CommonAPI.formatNumber(currentProjectStats[0]);
		accStats.text = CommonAPI.formatNumber(currentProjectStats[2]);
		spdStats.text = CommonAPI.formatNumber(currentProjectStats[1]);
		magStats.text = CommonAPI.formatNumber(currentProjectStats[3]);
		okLabel.text = game.getGameData().getTextByRefId("menuGeneral04");
		audioController.stopBGMAudio();
		audioController.stopForgeBGLoopAudio();
		audioController.playForgeCompleteAudio();
	}
}
