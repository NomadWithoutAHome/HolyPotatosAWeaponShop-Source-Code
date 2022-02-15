using System.Collections.Generic;
using UnityEngine;

public class GUIExploreController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private GUICharacterAnimationController charAnimCtr;

	private UIGrid smithSelect_Grid;

	private UIScrollBar smithSelect_scroll;

	private List<Smith> smithList;

	private string smithPrefix;

	private ActivityType selectedActivityType;

	private List<GameObject> smithObjectList;

	private TweenPosition menuTweener;

	private UISprite alertSprite;

	private int selectedIndex;

	private Vector3 openPosition;

	private Vector3 outPosition;

	private bool isOpen;

	private bool isAnimating;

	private Smith smith;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		charAnimCtr = GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>();
		smithSelect_Grid = commonScreenObject.findChild(base.gameObject, "ExploreList_bg/ExploreList/Panel_SmithExplore/SmithExplore_Grid").GetComponent<UIGrid>();
		smithSelect_scroll = commonScreenObject.findChild(base.gameObject, "ExploreList_bg/ExploreList/ExploreScroll").GetComponent<UIScrollBar>();
		smithList = new List<Smith>();
		smithObjectList = new List<GameObject>();
		menuTweener = base.gameObject.GetComponent<TweenPosition>();
		alertSprite = commonScreenObject.findChild(base.gameObject, "button_explore/ExploreAlert_sprite").GetComponent<UISprite>();
		smithPrefix = "SmithExploration_";
		openPosition = new Vector3(-200f, 0f, 0f);
		outPosition = new Vector3(60f, 0f, 0f);
		isOpen = false;
		isAnimating = false;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "button_explore":
			if (isOpen)
			{
				closeExploreMenu();
			}
			else
			{
				openExploreMenu();
			}
			return;
		case "GoButton":
			return;
		}
		string[] array = gameObjectName.Split('_');
		string text = array[0];
		if (text == "SmithExploration")
		{
			selectedIndex = CommonAPI.parseInt(array[1]);
			Player player = game.getPlayer();
			smithList = player.getOutOfShopSmithList();
			Smith smith = smithList[selectedIndex];
			switch (smith.getExploreState())
			{
			case SmithExploreState.SmithExploreStateExploreTravelHome:
				finishExploration(smith);
				break;
			case SmithExploreState.SmithExploreStateBuyMaterialTravelHome:
				finishBuyMaterial(smith);
				break;
			case SmithExploreState.SmithExploreStateSellWeapon:
			{
				Area exploreArea = smith.getExploreArea();
				smith.nextExploreState();
				viewController.showOfferWeapon(smith);
				closeExploreMenu();
				break;
			}
			case SmithExploreState.SmithExploreStateSellWeaponTravelHome:
				finishSellWeapon(smith);
				break;
			case SmithExploreState.SmithExploreStateVacationTravelHome:
				finishVacation(smith);
				break;
			case SmithExploreState.SmithExploreStateTrainingTravelHome:
				finishTraining(smith);
				break;
			}
		}
	}

	private void finishSellWeapon(Smith aSmith)
	{
		smith = aSmith;
		Area exploreArea = aSmith.getExploreArea();
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(player.getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int completedTutorialIndex = player.getCompletedTutorialIndex();
		List<string> exploreTask = aSmith.getExploreTask();
		string empty = string.Empty;
		bool flag = false;
		for (int i = 0; i < exploreTask.Count; i++)
		{
			Project completedProjectById = player.getCompletedProjectById(exploreTask[i]);
			Hero buyer = completedProjectById.getBuyer();
			if (buyer.getHeroRefId() != string.Empty)
			{
				flag = true;
			}
			else
			{
				completedProjectById.setProjectSaleState(ProjectSaleState.ProjectSaleStateInStock);
			}
		}
		if (flag)
		{
			viewController.showSellResult(aSmith);
		}
		else
		{
			string text = aSmith.tryAddStatusEffect();
			if (text != string.Empty)
			{
				gameData.addNewWhetsappMsg(aSmith.getSmithName(), text, "Image/Smith/Portraits/" + aSmith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
			}
			bool hasMoodLimit = false;
			if (!gameData.checkFeatureIsUnlocked(gameLockSet, "MOODLIMIT", completedTutorialIndex))
			{
				hasMoodLimit = true;
			}
			float aReduce = aSmith.getExploreArea().getMoodFactor() * 3f;
			SmithMood moodState = aSmith.getMoodState();
			aSmith.reduceSmithMood(aReduce, hasMoodLimit);
			SmithMood moodState2 = aSmith.getMoodState();
			if (moodState != moodState2)
			{
				string whetsappMoodString = CommonAPI.getWhetsappMoodString(moodState2);
				if (whetsappMoodString != string.Empty)
				{
					gameData.addNewWhetsappMsg(aSmith.getSmithName(), whetsappMoodString, "Image/Smith/Portraits/" + aSmith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
				}
			}
			viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: true, gameData.getTextByRefId("smithStatusPopup01"), gameData.getTextByRefIdWithDynText("smithStatusPopup02", "[smithName]", aSmith.getSmithName()) + text, PopupType.PopuptypeFinishActivity, null, colorTag: false, null, map: false, string.Empty);
		}
		exploreArea.removeAreaSmithRefID(aSmith.getSmithRefId());
		GameObject.Find("Panel_ActivitySelect").GetComponent<GUIActivitySelectController>().refreshButtons();
	}

	private void finishBuyMaterial(Smith aSmith)
	{
		smith = aSmith;
		viewController.showBuyResult(aSmith);
	}

	private void finishVacation(Smith aSmith)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		smith = aSmith;
		Area exploreArea = aSmith.getExploreArea();
		Vacation vacation = aSmith.getVacation();
		string text = aSmith.tryAddStatusEffect();
		if (text != string.Empty)
		{
			gameData.addNewWhetsappMsg(aSmith.getSmithName(), text, "Image/Smith/Portraits/" + aSmith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
		}
		float vacationMoodAdd = vacation.getVacationMoodAdd();
		SmithMood moodState = aSmith.getMoodState();
		aSmith.addSmithMood(vacationMoodAdd);
		SmithMood moodState2 = aSmith.getMoodState();
		string whetsappMoodString = CommonAPI.getWhetsappMoodString(moodState2);
		if (whetsappMoodString != string.Empty)
		{
			gameData.addNewWhetsappMsg(aSmith.getSmithName(), whetsappMoodString, "Image/Smith/Portraits/" + aSmith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
		}
		string text2 = gameData.getTextByRefIdWithDynText("vacationText03", "[SmithName]", aSmith.getSmithName()) + "\n" + text;
		exploreArea.removeAreaSmithRefID(aSmith.getSmithRefId());
		viewController.showVacationResult(aSmith, whetsappMoodString);
	}

	private void finishTraining(Smith aSmith)
	{
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		smith = aSmith;
		Area exploreArea = aSmith.getExploreArea();
		SmithTraining training = aSmith.getTraining();
		string text = aSmith.tryAddStatusEffect();
		if (text != string.Empty)
		{
			gameData.addNewWhetsappMsg(aSmith.getSmithName(), text, "Image/Smith/Portraits/" + aSmith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
		}
		smith.addSmithExp(training.getSmithTrainingExp());
		SmithMood moodState = aSmith.getMoodState();
		float aAdd = 10f;
		aSmith.addSmithMood(aAdd);
		SmithMood moodState2 = aSmith.getMoodState();
		if (moodState != moodState2)
		{
			string whetsappMoodString = CommonAPI.getWhetsappMoodString(moodState2);
			if (whetsappMoodString != string.Empty)
			{
				gameData.addNewWhetsappMsg(aSmith.getSmithName(), whetsappMoodString, "Image/Smith/Portraits/" + aSmith.getImage(), player.getPlayerTimeLong(), WhetsappFilterType.WhetsappFilterTypeSmith);
			}
		}
		string displayText = aSmith.getSmithName() + " has returned from training!" + text;
		exploreArea.removeAreaSmithRefID(aSmith.getSmithRefId());
		viewController.showGeneralPopup(GeneralPopupType.GeneralPopupTypeGeneral, resume: true, "TRAINING COMPLETED", displayText, PopupType.PopuptypeFinishActivity, null, colorTag: false, null, map: false, string.Empty);
	}

	private void finishExploration(Smith aSmith)
	{
		smith = aSmith;
		viewController.showExploreResult(aSmith);
	}

	public void clearList()
	{
		foreach (GameObject smithObject in smithObjectList)
		{
			commonScreenObject.destroyPrefabImmediate(smithObject);
		}
		smithObjectList.Clear();
	}

	public void refreshSmithExplorationList()
	{
		clearList();
		smithList = game.getPlayer().getOutOfShopSmithList();
		CommonAPI.debug("refreshSmithExplorationList smithList " + smithList.Count);
		bool flag = false;
		if (smithList.Count > 0)
		{
			for (int i = 0; i < smithList.Count; i++)
			{
				Smith smith = smithList[i];
				GameObject gameObject = commonScreenObject.createPrefab(smithSelect_Grid.gameObject, smithPrefix + i, "Prefab/Explore/Button_Explore", Vector3.zero, Vector3.one, Vector3.zero);
				commonScreenObject.findChild(gameObject, "Name").GetComponent<UILabel>().text = smith.getSmithName();
				commonScreenObject.findChild(gameObject, "Status").GetComponent<UILabel>().text = CommonAPI.convertSmithExploreStateToDisplayString(smith.getExploreState());
				int num = smith.getSmithActionDuration();
				if ((smith.getExploreState() == SmithExploreState.SmithExploreStateExploreTravelHome || smith.getExploreState() == SmithExploreState.SmithExploreStateBuyMaterialTravelHome || smith.getExploreState() == SmithExploreState.SmithExploreStateSellWeaponTravelHome) && smith.getSmithActionDuration() <= 0)
				{
					commonScreenObject.enableClick(gameObject, aValue: true);
					num = 0;
					gameObject.GetComponent<UISprite>().spriteName = "parent-active";
					flag = true;
				}
				else
				{
					gameObject.GetComponent<UISprite>().spriteName = "parent-inactive";
				}
				commonScreenObject.findChild(gameObject, "Duration").GetComponent<UILabel>().text = string.Empty + num;
				smithObjectList.Add(gameObject);
			}
			smithSelect_Grid.Reposition();
			smithSelect_scroll.value = 0f;
			smithSelect_Grid.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
			smithSelect_Grid.enabled = true;
			CommonAPI.debug("hasAlert " + flag);
			if (flag)
			{
				alertSprite.alpha = 1f;
			}
			else
			{
				alertSprite.alpha = 0f;
			}
		}
		else
		{
			slideExploreButtonOut();
		}
	}

	public void updateDuration()
	{
		smithList = game.getPlayer().getOutOfShopSmithList();
		for (int i = 0; i < smithList.Count; i++)
		{
			Smith smith = smithList[i];
			GameObject aObject = smithObjectList[i];
			commonScreenObject.findChild(aObject, "Status").GetComponent<UILabel>().text = CommonAPI.convertSmithExploreStateToDisplayString(smith.getExploreState());
			int num = smith.getSmithActionDuration();
			if (smith.getSmithActionDuration() <= 0)
			{
				num = 0;
			}
			commonScreenObject.findChild(aObject, "Duration").GetComponent<UILabel>().text = string.Empty + num;
		}
	}

	public void enableClick(int aIndex)
	{
		smithList = game.getPlayer().getOutOfShopSmithList();
		Smith smith = smithList[aIndex];
		GameObject gameObject = smithObjectList[aIndex];
		commonScreenObject.enableClick(gameObject, aValue: true);
		alertSprite.alpha = 1f;
		gameObject.GetComponent<UISprite>().spriteName = "parent-active";
	}

	public void openExploreMenu()
	{
		isAnimating = true;
		isOpen = true;
		commonScreenObject.tweenPosition(menuTweener, menuTweener.gameObject.transform.localPosition, openPosition, 0.4f, base.gameObject, "endAnimation");
	}

	public void closeExploreMenu()
	{
		isAnimating = true;
		isOpen = false;
		commonScreenObject.tweenPosition(menuTweener, menuTweener.gameObject.transform.localPosition, Vector3.zero, 0.4f, base.gameObject, "endAnimation");
	}

	public void slideExploreButtonIn()
	{
		isAnimating = true;
		commonScreenObject.tweenPosition(menuTweener, menuTweener.gameObject.transform.localPosition, Vector3.zero, 0.4f, base.gameObject, "endAnimation");
	}

	public void slideExploreButtonOut()
	{
		isAnimating = true;
		commonScreenObject.tweenPosition(menuTweener, menuTweener.gameObject.transform.localPosition, outPosition, 0.4f, base.gameObject, "closeExplore");
	}

	public void endAnimation()
	{
		isAnimating = false;
	}

	public void closeExplore()
	{
	}
}
