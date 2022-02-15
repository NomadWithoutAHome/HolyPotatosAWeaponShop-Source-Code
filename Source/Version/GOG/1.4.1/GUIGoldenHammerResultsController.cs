using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIGoldenHammerResultsController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private List<ProjectAchievement> winList;

	private List<int> winPrizeList;

	private List<GameObject> trophyList;

	private UIGrid trophyGrid;

	private UISprite bgSprite;

	private UIButton closeButton;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
	}

	public void processClick(string gameObjectName)
	{
		if (gameObjectName != null && gameObjectName == "Close_button")
		{
			viewController.closeGoldenHammerResults(resumeGame: true);
		}
	}

	public void processHover(bool isOver, string hoverName)
	{
		if (isOver)
		{
			if (hoverName == null)
			{
			}
		}
		else
		{
			tooltipScript.setInactive();
		}
	}

	public void setReference(List<ProjectAchievement> aWinList, List<int> aWinPrizeList)
	{
		GameData gameData = game.getGameData();
		winList = aWinList;
		winPrizeList = aWinPrizeList;
		trophyList = new List<GameObject>();
		trophyGrid = commonScreenObject.findChild(base.gameObject, "Trophies_grid").GetComponent<UIGrid>();
		bgSprite = commonScreenObject.findChild(base.gameObject, "Results_bg").GetComponent<UISprite>();
		UILabel component = commonScreenObject.findChild(base.gameObject, "ResultsTitle_label").GetComponent<UILabel>();
		component.text = gameData.getTextByRefId("menuGeneral07").ToUpper(CultureInfo.InvariantCulture);
		closeButton = commonScreenObject.findChild(base.gameObject, "Close_button").GetComponent<UIButton>();
		closeButton.isEnabled = false;
		UILabel component2 = commonScreenObject.findChild(closeButton.gameObject, "Close_label").GetComponent<UILabel>();
		component2.text = gameData.getTextByRefId("menuGeneral04");
		for (int i = 0; i < aWinList.Count; i++)
		{
			showTrophy(i);
		}
		trophyGrid.Reposition();
		float num = (float)component.GetComponentInChildren<UISprite>().width * component.transform.localScale.x;
		float a = (float)trophyList.Count * 160f + 50f;
		bgSprite.width = (int)Mathf.Max(a, num + 62f);
	}

	private void showTrophy(int index)
	{
		GameData gameData = game.getGameData();
		if (winList.Count >= index && winPrizeList.Count >= index)
		{
			GameObject gameObject = commonScreenObject.createPrefab(trophyGrid.gameObject, "ResultTrophyObj_" + index, "Prefab/GoldenHammer/ResultTrophyObj", Vector3.zero, Vector3.one, Vector3.zero);
			string spriteName = string.Empty;
			int width = 0;
			int height = 0;
			switch (winList[index])
			{
			case ProjectAchievement.ProjectAchievementGoldenHammerAttack:
				spriteName = "trophy-most powerful";
				width = 110;
				height = 99;
				break;
			case ProjectAchievement.ProjectAchievementGoldenHammerSpeed:
				spriteName = "trophy-the fastest";
				width = 110;
				height = 99;
				break;
			case ProjectAchievement.ProjectAchievementGoldenHammerAccuracy:
				spriteName = "trophy-most accurate";
				width = 110;
				height = 99;
				break;
			case ProjectAchievement.ProjectAchievementGoldenHammerMagic:
				spriteName = "trophy-most magical";
				width = 110;
				height = 99;
				break;
			case ProjectAchievement.ProjectAchievementGoldenHammerOverall:
				spriteName = "trophy-overall best";
				width = 110;
				height = 130;
				break;
			}
			UISprite component = commonScreenObject.findChild(gameObject, "Trophy_sprite").GetComponent<UISprite>();
			component.spriteName = spriteName;
			component.width = width;
			component.height = height;
			TweenScale component2 = component.GetComponent<TweenScale>();
			component2.delay = 0.2f * (float)index;
			commonScreenObject.tweenScale(component2, Vector3.zero, Vector3.one, 0.4f, base.gameObject, "startSparkle" + index);
			UILabel component3 = commonScreenObject.findChild(gameObject, "Received_label").GetComponent<UILabel>();
			component3.text = gameData.getTextByRefId("goldenHammerResult02");
			UILabel component4 = commonScreenObject.findChild(gameObject, "Prize_bg/Prize_label").GetComponent<UILabel>();
			component4.text = CommonAPI.formatNumber(winPrizeList[index]);
			trophyList.Add(gameObject);
		}
	}

	public void startSparkle0()
	{
		if (trophyList.Count > 0)
		{
			commonScreenObject.findChild(trophyList[0], "Trophy_sprite/Trophy_sparkle").GetComponent<ParticleSystem>().Play();
			if (trophyList.Count == 1)
			{
				closeButton.isEnabled = true;
			}
		}
	}

	public void startSparkle1()
	{
		if (trophyList.Count > 1)
		{
			commonScreenObject.findChild(trophyList[1], "Trophy_sprite/Trophy_sparkle").GetComponent<ParticleSystem>().Play();
			if (trophyList.Count == 2)
			{
				closeButton.isEnabled = true;
			}
		}
	}

	public void startSparkle2()
	{
		if (trophyList.Count > 2)
		{
			commonScreenObject.findChild(trophyList[2], "Trophy_sprite/Trophy_sparkle").GetComponent<ParticleSystem>().Play();
			if (trophyList.Count == 3)
			{
				closeButton.isEnabled = true;
			}
		}
	}
}
