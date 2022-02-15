using System.Collections;
using System.Collections.Generic;
using SmoothMoves;
using UnityEngine;

public class GUIQuestSelectController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private GameObject questListGridObj;

	private List<GameObject> questObjectList;

	private float questObjectHeight;

	private float subquestObjectHeight;

	private float questObjectYSpace;

	private List<Quest> questList;

	private float menuAnimSpeed;

	private UILabel questJobLabel;

	private UISprite questJobIcon;

	private UIButton goButton;

	private int selectedQuestIndex;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		questListGridObj = commonScreenObject.findChild(base.gameObject, "QuestSelectCentre/QuestSelectCentre_Scroll/QuestScroll_grid").gameObject;
		questObjectList = new List<GameObject>();
		questObjectHeight = 80f;
		subquestObjectHeight = 47f;
		questObjectYSpace = 2f;
		questList = new List<Quest>();
		menuAnimSpeed = 0.3f;
		questJobLabel = GameObject.Find("QuestJobLabel").GetComponent<UILabel>();
		questJobIcon = GameObject.Find("QuestJobIcon").GetComponent<UISprite>();
		goButton = GameObject.Find("GoButton").GetComponent<UIButton>();
		goButton.isEnabled = false;
		selectedQuestIndex = -1;
	}

	public void setStats()
	{
		setWeaponDisplay();
		setHeroDisplay();
		setQuestDisplay();
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName == "GoButton")
		{
			GameObject.Find("ViewController").GetComponent<ViewController>().closeSelectQuestPopup();
			ShopMenuController component = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
			component.insertStoredInput("quest", selectedQuestIndex + 1);
		}
		else
		{
			string[] array = gameObjectName.Split('_');
			int index = CommonAPI.parseInt(array[1]);
			selectQuest(index);
		}
	}

	public void setQuestDisplay()
	{
		GameData gameData = game.getGameData();
		Project currentProject = game.getPlayer().getCurrentProject();
		Weapon projectWeapon = currentProject.getProjectWeapon();
		GameObject.Find("AudioController").GetComponent<AudioController>().stopAuctionAudienceLoopAudio();
		repositionQuestObjList(-1);
	}

	public void repositionQuestObjList(int selectIndex)
	{
		GameData gameData = game.getGameData();
		float num = 0f;
		int num2 = 0;
		foreach (GameObject questObject in questObjectList)
		{
			float y = (float)num2 * (0f - (questObjectHeight + questObjectYSpace)) - num;
			if (selectIndex != -1 || selectedQuestIndex != -1)
			{
				Vector3 localPosition = questObject.transform.localPosition;
				Vector3 aEndPosition = new Vector3(0f, y, 0f);
				commonScreenObject.tweenPosition(questObject.GetComponent<TweenPosition>(), localPosition, aEndPosition, menuAnimSpeed, null, string.Empty);
			}
			else
			{
				questObject.transform.localPosition = new Vector3(0f, y, 0f);
			}
			GameObject gameObject = commonScreenObject.findChild(questObject, "Quest_subquest").gameObject;
			if (selectIndex == num2)
			{
				Quaternion localRotation = default(Quaternion);
				localRotation.eulerAngles = Vector3.zero;
				commonScreenObject.findChild(questObject, "Quest_arrow").transform.localRotation = localRotation;
				commonScreenObject.findChild(questObject, "Quest_select").GetComponent<UISprite>().enabled = true;
				Quest quest = questList[selectIndex];
				List<Subquest> subquestList = quest.getSubquestList();
				int num3 = 0;
				foreach (Subquest item in subquestList)
				{
					float y2 = 0f - (questObjectHeight + questObjectYSpace) - (float)num3 * (subquestObjectHeight + questObjectYSpace);
					GameObject gameObject2 = commonScreenObject.createPrefab(gameObject, "Subquest_" + selectIndex + "_" + num3, "Prefab/Quest/Subquest_generic", new Vector3(7f, 0f, 0f), Vector3.one, Vector3.zero);
					commonScreenObject.findChild(gameObject2, "Subquest_name").GetComponent<UILabel>().text = item.getSubquestTitle();
					bool flag = item.checkSubquestUnlocked();
					commonScreenObject.findChild(gameObject2, "Subquest_icon").GetComponent<UISprite>().spriteName = CommonAPI.getSubquestIconName(item.getSubquestType(), flag);
					if (!flag)
					{
						commonScreenObject.findChild(gameObject2, "Subquest_completed").GetComponent<UISprite>().alpha = 0f;
						commonScreenObject.findChild(gameObject2, "Subquest_completed/Subquest_completedLabel").GetComponent<UILabel>().alpha = 0f;
						commonScreenObject.findChild(gameObject2, "Subquest_unlock").GetComponent<UISprite>().alpha = 1f;
						commonScreenObject.findChild(gameObject2, "Subquest_unlock/Subquest_unlockLabel").GetComponent<UILabel>().text = gameData.getTextByRefIdWithDynText("menuSubquest04", "[percentage]", (25 * (num3 + 1)).ToString());
						commonScreenObject.findChild(gameObject2, "Subquest_unlock/Subquest_unlockLabel").GetComponent<UILabel>().alpha = 1f;
					}
					else
					{
						commonScreenObject.findChild(gameObject2, "Subquest_completed").GetComponent<UISprite>().alpha = 1f;
						commonScreenObject.findChild(gameObject2, "Subquest_completed/Subquest_completedLabel").GetComponent<UILabel>().alpha = 1f;
						commonScreenObject.findChild(gameObject2, "Subquest_unlock").GetComponent<UISprite>().alpha = 0f;
						commonScreenObject.findChild(gameObject2, "Subquest_unlock/Subquest_unlockLabel").GetComponent<UILabel>().alpha = 0f;
					}
					if (selectIndex != -1 || selectedQuestIndex != -1)
					{
						Vector3 localPosition2 = gameObject2.transform.localPosition;
						Vector3 aEndPosition2 = new Vector3(7f, y2, 0f);
						commonScreenObject.tweenPosition(gameObject2.GetComponent<TweenPosition>(), localPosition2, aEndPosition2, menuAnimSpeed, null, string.Empty);
					}
					else
					{
						gameObject2.transform.localPosition = new Vector3(7f, y2, 0f);
					}
					num3++;
				}
				num = (float)subquestList.Count * (subquestObjectHeight + questObjectYSpace);
			}
			else
			{
				for (int i = 0; i < gameObject.transform.childCount; i++)
				{
					GameObject gameObject3 = gameObject.transform.GetChild(i).gameObject;
					Vector3 localPosition3 = gameObject3.transform.localPosition;
					Vector3 aEndPosition3 = new Vector3(7f, 0f, 0f);
					commonScreenObject.tweenPosition(gameObject3.GetComponent<TweenPosition>(), localPosition3, aEndPosition3, menuAnimSpeed, null, string.Empty);
				}
				Quaternion localRotation2 = default(Quaternion);
				localRotation2.eulerAngles = new Vector3(0f, 0f, -90f);
				commonScreenObject.findChild(questObject, "Quest_arrow").transform.localRotation = localRotation2;
				commonScreenObject.findChild(questObject, "Quest_select").GetComponent<UISprite>().enabled = false;
			}
			num2++;
		}
	}

	public void setWeaponDisplay()
	{
		Project currentProject = game.getPlayer().getCurrentProject();
		Weapon projectWeapon = currentProject.getProjectWeapon();
		commonScreenObject.findChild(base.gameObject, "QuestSelectLeft_Weapon/Weapon_name").GetComponent<UILabel>().text = currentProject.getProjectName(includePrefix: true);
		commonScreenObject.findChild(base.gameObject, "QuestSelectLeft_Weapon/Weapon_stats/Stat_1atk/wpn_atk").GetComponent<UILabel>().text = CommonAPI.formatNumber(currentProject.getAtk());
		commonScreenObject.findChild(base.gameObject, "QuestSelectLeft_Weapon/Weapon_stats/Stat_2spd/wpn_spd").GetComponent<UILabel>().text = CommonAPI.formatNumber(currentProject.getSpd());
		commonScreenObject.findChild(base.gameObject, "QuestSelectLeft_Weapon/Weapon_stats/Stat_3acc/wpn_acc").GetComponent<UILabel>().text = CommonAPI.formatNumber(currentProject.getAcc());
		commonScreenObject.findChild(base.gameObject, "QuestSelectLeft_Weapon/Weapon_stats/Stat_4mag/wpn_mag").GetComponent<UILabel>().text = CommonAPI.formatNumber(currentProject.getMag());
		Texture mainTexture = commonScreenObject.loadTexture("Image/Weapons/" + projectWeapon.getImage());
		commonScreenObject.findChild(base.gameObject, "QuestSelectLeft_Weapon/Weapon_base/Weapon_image").GetComponent<UITexture>().mainTexture = mainTexture;
	}

	public void setHeroDisplay()
	{
		GameData gameData = game.getGameData();
		Project currentProject = game.getPlayer().getCurrentProject();
		commonScreenObject.findChild(base.gameObject, "QuestSelectTitle").GetComponent<UILabel>().text = gameData.getTextByRefId("menuQuest02");
	}

	public IEnumerator animateHero(BoneAnimation heroAnim)
	{
		while (true)
		{
			float randomWait = Random.Range(2f, 6f);
			yield return new WaitForSeconds(randomWait);
			heroAnim.CrossFadeQueued("inter", 0.3f, QueueMode.PlayNow, PlayMode.StopAll);
			heroAnim.CrossFadeQueued("idle", 0.3f, QueueMode.CompleteOthers, PlayMode.StopAll);
		}
	}

	private void selectQuest(int index)
	{
		if (index != selectedQuestIndex)
		{
			repositionQuestObjList(index);
			selectedQuestIndex = index;
		}
		else
		{
			repositionQuestObjList(-1);
			selectedQuestIndex = -1;
		}
		if (selectedQuestIndex != -1)
		{
			goButton.isEnabled = true;
		}
		else
		{
			goButton.isEnabled = false;
		}
	}
}
