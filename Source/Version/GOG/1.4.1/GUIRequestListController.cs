using System.Collections.Generic;
using UnityEngine;

public class GUIRequestListController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private UIGrid requestGrid;

	private List<GameObject> requestObjList;

	private List<GameObject> legendaryObjList;

	private List<HeroRequest> currentList;

	private List<LegendaryHero> legendaryList;

	private int alertTime;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		requestGrid = commonScreenObject.findChild(base.gameObject, "Request_grid").GetComponent<UIGrid>();
		requestObjList = new List<GameObject>();
		legendaryObjList = new List<GameObject>();
		currentList = new List<HeroRequest>();
		legendaryList = new List<LegendaryHero>();
		alertTime = 48;
	}

	public void processClick(GameObject gameObjectClick)
	{
		string text = gameObjectClick.name;
		if (text != null && text == "Submit_button")
		{
			GameObject gameObject = gameObjectClick.transform.parent.transform.parent.transform.parent.gameObject;
			TweenPosition componentInChildren = gameObject.GetComponentInChildren<TweenPosition>();
			if (!componentInChildren.enabled && componentInChildren.transform.localPosition.x > 0f)
			{
				string[] array = gameObject.name.Split('_');
				if (array[0] == "RequestListObj")
				{
					int index = CommonAPI.parseInt(array[1]);
					HeroRequest heroRequest = currentList[index];
					viewController.showWeaponRequestSubmit(heroRequest);
				}
			}
		}
		else
		{
			string[] array2 = text.Split('_');
			if (array2[0] == "RequestListObj" || array2[0] == "LegendaryListObj")
			{
				toggleDrawer(gameObjectClick);
			}
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
			if (array[0] == "RequestListObj")
			{
				int index = CommonAPI.parseInt(array[1]);
				HeroRequest heroRequest = currentList[index];
				Hero heroByHeroRefID = game.getGameData().getHeroByHeroRefID(heroRequest.getRequestHero());
				tooltipScript.showText(heroByHeroRefID.getHeroStandardInfoString());
			}
			else if (array[0] == "LegendaryListObj")
			{
				int index2 = CommonAPI.parseInt(array[1]);
				LegendaryHero legendaryHero = legendaryList[index2];
				tooltipScript.showText(legendaryHero.getLegendaryHeroName() + "\n[i]" + legendaryHero.getLegendaryHeroDescription() + "[/i]");
			}
		}
		else
		{
			tooltipScript.setInactive();
		}
	}

	public void cleanupRequestDisplay()
	{
		foreach (GameObject requestObj in requestObjList)
		{
			commonScreenObject.destroyPrefabImmediate(requestObj);
		}
		foreach (GameObject legendaryObj in legendaryObjList)
		{
			commonScreenObject.destroyPrefabImmediate(legendaryObj);
		}
		requestObjList = new List<GameObject>();
		legendaryObjList = new List<GameObject>();
	}

	public void toggleDrawer(GameObject aRequestObj)
	{
		TweenPosition componentInChildren = aRequestObj.GetComponentInChildren<TweenPosition>();
		if (!componentInChildren.enabled && componentInChildren.transform.localPosition.x > 0f)
		{
			string[] array = aRequestObj.name.Split('_');
			if (array[0] == "RequestListObj")
			{
				componentInChildren.GetComponentInChildren<UIButton>().isEnabled = false;
			}
			commonScreenObject.tweenPosition(componentInChildren, componentInChildren.transform.localPosition, new Vector3(-70f, 0f, 0f), 0.4f, null, string.Empty);
		}
		else if (!componentInChildren.enabled && componentInChildren.transform.localPosition.x < 0f)
		{
			string[] array2 = aRequestObj.name.Split('_');
			if (array2[0] == "RequestListObj")
			{
				int index = CommonAPI.parseInt(array2[1]);
				HeroRequest displayRequest = currentList[index];
				showRequirementsInDrawer(aRequestObj, displayRequest);
				componentInChildren.GetComponentInChildren<UIButton>().isEnabled = true;
			}
			else if (array2[0] == "LegendaryListObj")
			{
				int index2 = CommonAPI.parseInt(array2[1]);
				LegendaryHero displayLegendary = legendaryList[index2];
				showLegendaryRequirementsInDrawer(aRequestObj, displayLegendary);
			}
			commonScreenObject.tweenPosition(componentInChildren, componentInChildren.transform.localPosition, new Vector3(60f, 0f, 0f), 0.4f, null, string.Empty);
		}
	}

	private void closeRequestObjDrawer(GameObject aRequestObj)
	{
		TweenPosition componentInChildren = aRequestObj.GetComponentInChildren<TweenPosition>();
		if (componentInChildren.transform.localPosition.x > 0f)
		{
			string[] array = aRequestObj.name.Split('_');
			if (array[0] == "RequestListObj")
			{
				componentInChildren.GetComponentInChildren<UIButton>().isEnabled = false;
			}
			commonScreenObject.tweenPosition(componentInChildren, componentInChildren.transform.localPosition, new Vector3(-70f, 0f, 0f), 0.4f, null, string.Empty);
		}
	}

	public bool checkRequestExpiry()
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		int num = 0;
		foreach (HeroRequest current in currentList)
		{
			if (current.getRequestTimeLeft(player.getPlayerTimeLong()) <= 0)
			{
				Hero heroByHeroRefID = gameData.getHeroByHeroRefID(current.getRequestHero());
				string randomTextBySetRefId = gameData.getRandomTextBySetRefId("whetsappRequestExpire");
				viewController.showGeneralDialoguePopup(GeneralPopupType.GeneralPopupTypeDialogueGeneral, resume: true, gameData.getTextByRefId("weaponRequest23"), gameData.getTextByRefId("weaponRequest24"), randomTextBySetRefId, "Image/Hero/" + heroByHeroRefID.getImage(), PopupType.PopupTypeRequestExpiry);
				string aName = heroByHeroRefID.getHeroName() + " " + gameData.getTextByRefIdWithDynText("heroStat08", "[level]", heroByHeroRefID.getHeroLevel().ToString());
				gameData.addNewWhetsappMsg(aName, randomTextBySetRefId, "Image/Hero/" + heroByHeroRefID.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeHero);
				tooltipScript.setInactive();
				current.expireRequest();
				player.removeDisplayRequest(current);
				return true;
			}
			num++;
		}
		return false;
	}

	private bool checkListChange()
	{
		Player player = game.getPlayer();
		bool result = false;
		if (legendaryList.Count != legendaryObjList.Count)
		{
			result = true;
		}
		if (currentList.Count != requestObjList.Count)
		{
			result = true;
		}
		return result;
	}

	public void updateRequests(bool hasTimeElapse)
	{
		Player player = game.getPlayer();
		bool flag = checkListChange();
		if (flag)
		{
		}
		legendaryList = player.getLegendaryHeroRequestList(acceptedOnly: true);
		int num = 0;
		foreach (GameObject legendaryObj in legendaryObjList)
		{
			if (legendaryList.Count > num)
			{
				LegendaryHero displayLegendary = legendaryList[num];
				updateLegendaryDisplayObj(legendaryObj, displayLegendary);
				if (flag)
				{
					closeRequestObjDrawer(legendaryObj);
				}
			}
			num++;
		}
		for (int i = num; i < legendaryList.Count; i++)
		{
			LegendaryHero displayLegendary2 = legendaryList[num];
			GameObject gameObject = commonScreenObject.createPrefab(requestGrid.gameObject, "LegendaryListObj_" + i, "Prefab/Request/LegendaryRequestListObj", Vector3.zero, Vector3.one, Vector3.zero);
			updateLegendaryDisplayObj(gameObject, displayLegendary2);
			legendaryObjList.Add(gameObject);
		}
		if (legendaryObjList.Count > legendaryList.Count)
		{
			int count = legendaryObjList.Count - legendaryList.Count;
			List<GameObject> range = legendaryObjList.GetRange(legendaryList.Count, count);
			legendaryObjList.RemoveRange(legendaryList.Count, count);
			foreach (GameObject item in range)
			{
				commonScreenObject.destroyPrefabImmediate(item);
			}
		}
		currentList = player.getDisplayRequestList();
		int num2 = 0;
		foreach (GameObject requestObj in requestObjList)
		{
			if (currentList.Count > num2)
			{
				HeroRequest displayRequest = currentList[num2];
				updateDisplayObj(requestObj, displayRequest);
				if (flag)
				{
					closeRequestObjDrawer(requestObj);
				}
			}
			num2++;
		}
		for (int j = num2; j < currentList.Count; j++)
		{
			HeroRequest displayRequest2 = currentList[num2];
			GameObject gameObject2 = commonScreenObject.createPrefab(requestGrid.gameObject, "RequestListObj_" + j, "Prefab/Request/RequestListObj", Vector3.zero, Vector3.one, Vector3.zero);
			updateDisplayObj(gameObject2, displayRequest2);
			requestObjList.Add(gameObject2);
		}
		if (requestObjList.Count > currentList.Count)
		{
			int count2 = requestObjList.Count - currentList.Count;
			List<GameObject> range2 = requestObjList.GetRange(currentList.Count, count2);
			requestObjList.RemoveRange(currentList.Count, count2);
			foreach (GameObject item2 in range2)
			{
				commonScreenObject.destroyPrefabImmediate(item2);
			}
		}
		requestGrid.Reposition();
	}

	private void updateLegendaryDisplayObj(GameObject displayObj, LegendaryHero displayLegendary)
	{
		GameData gameData = game.getGameData();
		UITexture component = commonScreenObject.findChild(displayObj, "HeroImage_texture").GetComponent<UITexture>();
		component.mainTexture = commonScreenObject.loadTexture("Image/legendary heroes/" + displayLegendary.getImage());
	}

	private void showLegendaryRequirementsInDrawer(GameObject displayObj, LegendaryHero displayLegendary)
	{
		GameData gameData = game.getGameData();
		GameObject aObject = commonScreenObject.findChild(displayObj, "Requirements_panel/RequirementDrawer").gameObject;
		UILabel component = commonScreenObject.findChild(aObject, "RequirementTitle_label").GetComponent<UILabel>();
		component.text = gameData.getTextByRefId("weaponRequest03");
		Weapon weaponByRefId = gameData.getWeaponByRefId(displayLegendary.getWeaponRefId());
		UILabel component2 = commonScreenObject.findChild(aObject, "LegendaryWeapon_label").GetComponent<UILabel>();
		component2.text = weaponByRefId.getWeaponName();
		List<WeaponStat> list = new List<WeaponStat>();
		List<int> list2 = new List<int>();
		if (displayLegendary.getReqAtk() > 0)
		{
			list.Add(WeaponStat.WeaponStatAttack);
			list2.Add(displayLegendary.getReqAtk());
		}
		if (displayLegendary.getReqSpd() > 0)
		{
			list.Add(WeaponStat.WeaponStatSpeed);
			list2.Add(displayLegendary.getReqSpd());
		}
		if (displayLegendary.getReqAcc() > 0)
		{
			list.Add(WeaponStat.WeaponStatAccuracy);
			list2.Add(displayLegendary.getReqAcc());
		}
		if (displayLegendary.getReqMag() > 0)
		{
			list.Add(WeaponStat.WeaponStatMagic);
			list2.Add(displayLegendary.getReqMag());
		}
		if (list.Count > 0)
		{
			UILabel component3 = commonScreenObject.findChild(aObject, "Stat1_sprite/Stat1_label").GetComponent<UILabel>();
			component3.text = list2[0].ToString();
			UISprite component4 = commonScreenObject.findChild(aObject, "Stat1_sprite").GetComponent<UISprite>();
			switch (list[0])
			{
			case WeaponStat.WeaponStatAttack:
				component4.spriteName = "ico_atk";
				break;
			case WeaponStat.WeaponStatSpeed:
				component4.spriteName = "ico_speed";
				break;
			case WeaponStat.WeaponStatAccuracy:
				component4.spriteName = "ico_acc";
				break;
			case WeaponStat.WeaponStatMagic:
				component4.spriteName = "ico_enh";
				break;
			}
		}
		if (list.Count > 1)
		{
			UILabel component5 = commonScreenObject.findChild(aObject, "Stat2_sprite/Stat2_label").GetComponent<UILabel>();
			component5.text = list2[1].ToString();
			UISprite component6 = commonScreenObject.findChild(aObject, "Stat2_sprite").GetComponent<UISprite>();
			switch (list[1])
			{
			case WeaponStat.WeaponStatAttack:
				component6.spriteName = "ico_atk";
				break;
			case WeaponStat.WeaponStatSpeed:
				component6.spriteName = "ico_speed";
				break;
			case WeaponStat.WeaponStatAccuracy:
				component6.spriteName = "ico_acc";
				break;
			case WeaponStat.WeaponStatMagic:
				component6.spriteName = "ico_enh";
				break;
			}
		}
	}

	private void updateDisplayObj(GameObject displayObj, HeroRequest displayRequest)
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		Hero heroByHeroRefID = gameData.getHeroByHeroRefID(displayRequest.getRequestHero());
		UITexture component = commonScreenObject.findChild(displayObj, "HeroImage_texture").GetComponent<UITexture>();
		component.mainTexture = commonScreenObject.loadTexture("Image/Hero/" + heroByHeroRefID.getImage());
		int requestTimeLeft = displayRequest.getRequestTimeLeft(player.getPlayerTimeLong());
		string text = CommonAPI.convertHalfHoursToTimeString(requestTimeLeft);
		commonScreenObject.findChild(displayObj, "TimeLeft_label").GetComponent<UILabel>().text = gameData.getTextByRefId("weaponRequest22");
		UILabel component2 = commonScreenObject.findChild(displayObj, "TimeLeft_label/TimeLeft_value").GetComponent<UILabel>();
		component2.text = text;
		if (requestTimeLeft < alertTime / 2)
		{
			component2.color = Color.red;
			TweenAlpha componentInChildren = displayObj.GetComponentInChildren<TweenAlpha>();
			componentInChildren.duration = 0.5f;
			return;
		}
		if (requestTimeLeft < alertTime)
		{
			component2.color = Color.red;
			TweenAlpha componentInChildren2 = displayObj.GetComponentInChildren<TweenAlpha>();
			if (!componentInChildren2.enabled)
			{
				commonScreenObject.tweenAlpha(componentInChildren2, 0f, 1f, 1f, null, string.Empty);
			}
			return;
		}
		component2.color = Color.white;
		TweenAlpha componentInChildren3 = displayObj.GetComponentInChildren<TweenAlpha>();
		if (componentInChildren3.enabled)
		{
			componentInChildren3.enabled = false;
			componentInChildren3.gameObject.GetComponent<UISprite>().alpha = 0f;
		}
	}

	private void showRequirementsInDrawer(GameObject displayObj, HeroRequest displayRequest)
	{
		GameData gameData = game.getGameData();
		UIGrid component = commonScreenObject.findChild(displayObj, "Requirements_panel/RequirementDrawer/Requirements_grid").GetComponent<UIGrid>();
		UILabel component2 = commonScreenObject.findChild(displayObj, "Requirements_panel/RequirementDrawer/RequirementTitle_label").GetComponent<UILabel>();
		component2.text = gameData.getTextByRefId("weaponRequest03");
		UILabel component3 = commonScreenObject.findChild(displayObj, "Requirements_panel/RequirementDrawer/Submit_button/Submit_label").GetComponent<UILabel>();
		component3.text = gameData.getTextByRefId("weaponRequest25");
		foreach (Transform child in component.GetChildList())
		{
			commonScreenObject.destroyPrefabImmediate(child.gameObject);
		}
		int num = 1;
		if (displayRequest.getRequestWeaponTypeRefIdReq() != string.Empty)
		{
			WeaponType weaponTypeByRefId = gameData.getWeaponTypeByRefId(displayRequest.getRequestWeaponTypeRefIdReq());
			GameObject aObject = commonScreenObject.createPrefab(component.gameObject, displayObj.name + "_req" + num, "Prefab/Request/RequestListReqObj", Vector3.zero, Vector3.one, Vector3.zero);
			UILabel component4 = commonScreenObject.findChild(aObject, "Requirement_label").GetComponent<UILabel>();
			component4.text = gameData.getTextByRefId("weaponRequest07") + " " + weaponTypeByRefId.getWeaponTypeName();
			component4.alpha = 1f;
			num++;
		}
		if (displayRequest.getRequestWeaponRefIdReq() != string.Empty)
		{
			Weapon weaponByRefId = gameData.getWeaponByRefId(displayRequest.getRequestWeaponRefIdReq());
			GameObject aObject2 = commonScreenObject.createPrefab(component.gameObject, displayObj.name + "_req" + num, "Prefab/Request/RequestListReqObj", Vector3.zero, Vector3.one, Vector3.zero);
			UILabel component5 = commonScreenObject.findChild(aObject2, "Requirement_label").GetComponent<UILabel>();
			component5.text = gameData.getTextByRefId("weaponRequest08") + " " + weaponByRefId.getWeaponName();
			component5.alpha = 1f;
			num++;
		}
		if (displayRequest.getRequestEnchantmentReq() != string.Empty)
		{
			Item itemByRefId = gameData.getItemByRefId(displayRequest.getRequestEnchantmentReq());
			GameObject aObject3 = commonScreenObject.createPrefab(component.gameObject, displayObj.name + "_req" + num, "Prefab/Request/RequestListReqObj", Vector3.zero, Vector3.one, Vector3.zero);
			UILabel component6 = commonScreenObject.findChild(aObject3, "Requirement_label").GetComponent<UILabel>();
			component6.text = gameData.getTextByRefId("weaponRequest06") + " " + itemByRefId.getItemEffectString();
			component6.alpha = 1f;
			num++;
		}
		if (displayRequest.checkHasStatReq())
		{
			GameObject aObject4 = commonScreenObject.createPrefab(component.gameObject, displayObj.name + "_req" + num, "Prefab/Request/RequestListReqObj", Vector3.zero, Vector3.one, Vector3.zero);
			List<WeaponStat> list = new List<WeaponStat>();
			List<int> list2 = new List<int>();
			if (displayRequest.getRequestAtkReq() > 0)
			{
				list.Add(WeaponStat.WeaponStatAttack);
				list2.Add(displayRequest.getRequestAtkReq());
			}
			if (displayRequest.getRequestSpdReq() > 0)
			{
				list.Add(WeaponStat.WeaponStatSpeed);
				list2.Add(displayRequest.getRequestSpdReq());
			}
			if (displayRequest.getRequestAccReq() > 0)
			{
				list.Add(WeaponStat.WeaponStatAccuracy);
				list2.Add(displayRequest.getRequestAccReq());
			}
			if (displayRequest.getRequestMagReq() > 0)
			{
				list.Add(WeaponStat.WeaponStatMagic);
				list2.Add(displayRequest.getRequestMagReq());
			}
			if (list.Count > 1)
			{
				UILabel component7 = commonScreenObject.findChild(aObject4, "Stat1_sprite/Stat1_label").GetComponent<UILabel>();
				component7.text = list2[0].ToString();
				UISprite component8 = commonScreenObject.findChild(aObject4, "Stat1_sprite").GetComponent<UISprite>();
				switch (list[0])
				{
				case WeaponStat.WeaponStatAttack:
					component8.spriteName = "ico_atk";
					break;
				case WeaponStat.WeaponStatSpeed:
					component8.spriteName = "ico_speed";
					break;
				case WeaponStat.WeaponStatAccuracy:
					component8.spriteName = "ico_acc";
					break;
				case WeaponStat.WeaponStatMagic:
					component8.spriteName = "ico_enh";
					break;
				}
				component8.alpha = 1f;
				UILabel component9 = commonScreenObject.findChild(aObject4, "Stat2_sprite/Stat2_label").GetComponent<UILabel>();
				component9.text = list2[1].ToString();
				UISprite component10 = commonScreenObject.findChild(aObject4, "Stat2_sprite").GetComponent<UISprite>();
				switch (list[1])
				{
				case WeaponStat.WeaponStatAttack:
					component10.spriteName = "ico_atk";
					break;
				case WeaponStat.WeaponStatSpeed:
					component10.spriteName = "ico_speed";
					break;
				case WeaponStat.WeaponStatAccuracy:
					component10.spriteName = "ico_acc";
					break;
				case WeaponStat.WeaponStatMagic:
					component10.spriteName = "ico_enh";
					break;
				}
				component10.alpha = 1f;
			}
			else if (list.Count > 0)
			{
				UILabel component11 = commonScreenObject.findChild(aObject4, "StatSingle_sprite/StatSingle_label").GetComponent<UILabel>();
				component11.text = list2[0].ToString();
				UISprite component12 = commonScreenObject.findChild(aObject4, "StatSingle_sprite").GetComponent<UISprite>();
				switch (list[0])
				{
				case WeaponStat.WeaponStatAttack:
					component12.spriteName = "ico_atk";
					break;
				case WeaponStat.WeaponStatSpeed:
					component12.spriteName = "ico_speed";
					break;
				case WeaponStat.WeaponStatAccuracy:
					component12.spriteName = "ico_acc";
					break;
				case WeaponStat.WeaponStatMagic:
					component12.spriteName = "ico_enh";
					break;
				}
				component12.alpha = 1f;
			}
			num++;
		}
		component.Reposition();
	}
}
