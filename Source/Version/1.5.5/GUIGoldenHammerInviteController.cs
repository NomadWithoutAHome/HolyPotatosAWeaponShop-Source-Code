using System.Collections.Generic;
using UnityEngine;

public class GUIGoldenHammerInviteController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private List<string> awardValueList;

	private List<ProjectAchievement> awardList;

	private UILabel invitationLabel;

	private TweenPosition letterTween;

	private UIButton closeButton;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		invitationLabel = commonScreenObject.findChild(base.gameObject, "Letter_panel/Letter_content").GetComponent<UILabel>();
		letterTween = invitationLabel.gameObject.GetComponent<TweenPosition>();
		closeButton = commonScreenObject.findChild(base.gameObject, "Close_button").GetComponent<UIButton>();
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName == "Close_button")
		{
			viewController.closeGoldenHammerInvite();
		}
	}

	public void setReference()
	{
		closeButton.GetComponentInChildren<UILabel>().text = game.getGameData().getTextByRefId("menuGeneral04");
		setAwardList();
		showLetterContent();
		closeButton.isEnabled = false;
	}

	private void showLetterContent()
	{
		GameData gameData = game.getGameData();
		string empty = string.Empty;
		List<string> list = new List<string>();
		list.Add("[month]");
		list.Add("[day]");
		List<string> list2 = new List<string>();
		SpecialEvent specialEventByRefId = gameData.getSpecialEventByRefId("2001");
		list2.Add(specialEventByRefId.getDateMonth().ToString());
		list2.Add(((specialEventByRefId.getDateWeek() - 1) * 7 + specialEventByRefId.getDateDay()).ToString());
		empty = empty + gameData.getTextByRefIdWithDynTextList("goldenHammerInvite01", list, list2) + "\n\n";
		empty = empty + gameData.getTextByRefId("goldenHammerInvite02") + "\n";
		foreach (string awardValue in awardValueList)
		{
			empty = empty + "- " + awardValue + "\n";
		}
		empty = empty + "\n" + gameData.getTextByRefId("goldenHammerInvite03");
		invitationLabel.text = empty;
		float y = -152f - invitationLabel.printedSize.y;
		Vector3 aStartPosition = new Vector3(0f, y, 0f);
		Vector3 aEndPosition = new Vector3(0f, -100f, 0f);
		letterTween.delay = 0.4f;
		commonScreenObject.tweenPosition(letterTween, aStartPosition, aEndPosition, 0.5f, base.gameObject, "enableButton");
	}

	public void enableButton()
	{
		closeButton.isEnabled = true;
	}

	private void setAwardList()
	{
		GameData gameData = game.getGameData();
		Player player = game.getPlayer();
		awardValueList = new List<string>();
		awardList = new List<ProjectAchievement>();
		List<int> randomIntList = CommonAPI.getRandomIntList(4, 2);
		using (List<int>.Enumerator enumerator = randomIntList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				switch (enumerator.Current)
				{
				case 1:
					awardValueList.Add(gameData.getTextByRefId("goldenHammerText11"));
					awardList.Add(ProjectAchievement.ProjectAchievementGoldenHammerAttack);
					break;
				case 2:
					awardValueList.Add(gameData.getTextByRefId("goldenHammerText12"));
					awardList.Add(ProjectAchievement.ProjectAchievementGoldenHammerSpeed);
					break;
				case 3:
					awardValueList.Add(gameData.getTextByRefId("goldenHammerText13"));
					awardList.Add(ProjectAchievement.ProjectAchievementGoldenHammerAccuracy);
					break;
				default:
					awardValueList.Add(gameData.getTextByRefId("goldenHammerText14"));
					awardList.Add(ProjectAchievement.ProjectAchievementGoldenHammerMagic);
					break;
				}
			}
		}
		awardValueList.Add(gameData.getTextByRefId("goldenHammerText15"));
		awardList.Add(ProjectAchievement.ProjectAchievementGoldenHammerOverall);
		player.setNextGoldenHammerAwardList(awardList);
	}
}
