using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIForgeAppraisalController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private UILabel finishedLabel;

	private List<UILabel> appraisalScoreList;

	private List<UILabel> appraisalLabelList;

	private UITexture weaponImg;

	private UILabel weaponName;

	private UILabel weaponDesc;

	private UILabel atkStats;

	private UILabel accStats;

	private UILabel spdStats;

	private UILabel magStats;

	private UIButton okButton;

	private UILabel okLabel;

	private float displayInterval;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		finishedLabel = commonScreenObject.findChild(base.gameObject, "FinishedFrame/FinishedLabel").GetComponent<UILabel>();
		appraisalScoreList = new List<UILabel>();
		for (int i = 0; i < 3; i++)
		{
			appraisalScoreList.Add(GameObject.Find("AppraisalScore" + i).GetComponent<UILabel>());
		}
		appraisalLabelList = new List<UILabel>();
		for (int j = 0; j < 3; j++)
		{
			appraisalLabelList.Add(GameObject.Find("AppraisalLabel" + j).GetComponent<UILabel>());
		}
		weaponImg = commonScreenObject.findChild(base.gameObject, "WeaponFrame/WeaponImg").GetComponent<UITexture>();
		weaponName = commonScreenObject.findChild(base.gameObject, "WeaponStats/WeaponName").GetComponent<UILabel>();
		weaponDesc = commonScreenObject.findChild(base.gameObject, "WeaponStats/WeaponDesc").GetComponent<UILabel>();
		atkStats = commonScreenObject.findChild(base.gameObject, "WeaponStats/AtkStats").GetComponent<UILabel>();
		accStats = commonScreenObject.findChild(base.gameObject, "WeaponStats/AccStats").GetComponent<UILabel>();
		spdStats = commonScreenObject.findChild(base.gameObject, "WeaponStats/SpdStats").GetComponent<UILabel>();
		magStats = commonScreenObject.findChild(base.gameObject, "WeaponStats/MagStats").GetComponent<UILabel>();
		okButton = commonScreenObject.findChild(base.gameObject, "OkButton").GetComponent<UIButton>();
		okLabel = commonScreenObject.findChild(base.gameObject, "OkButton/OkLabel").GetComponent<UILabel>();
		displayInterval = 0.5f;
	}

	public void processClick(string gameobjectName)
	{
		if (gameobjectName != null && gameobjectName == "OkButton")
		{
			GameObject.Find("ViewController").GetComponent<ViewController>().closeForgeAppraisal();
		}
	}

	public void setStats()
	{
	}

	private IEnumerator showAppraisalScore()
	{
		yield return new WaitForSeconds(displayInterval);
		foreach (UILabel aAppraisal in appraisalLabelList)
		{
			commonScreenObject.tweenAlpha(aAppraisal.GetComponent<TweenAlpha>(), 0f, 1f, displayInterval, null, string.Empty);
			commonScreenObject.tweenAlpha(appraisalScoreList[appraisalLabelList.IndexOf(aAppraisal)].GetComponent<TweenAlpha>(), 0f, 1f, displayInterval, null, string.Empty);
			yield return new WaitForSeconds(displayInterval);
		}
		okButton.isEnabled = true;
	}
}
