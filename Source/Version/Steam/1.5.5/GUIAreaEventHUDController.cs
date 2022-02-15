using System.Collections.Generic;
using UnityEngine;

public class GUIAreaEventHUDController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private GameObject eventGrid;

	private Dictionary<int, GameObject> eventObjList;

	private Dictionary<int, Area> eventAreaList;

	private int currentTooltip;

	private string prefabName;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		eventGrid = commonScreenObject.findChild(base.gameObject, "AreaEvent_grid").gameObject;
		eventObjList = new Dictionary<int, GameObject>();
		eventAreaList = new Dictionary<int, Area>();
		currentTooltip = -1;
		if (base.gameObject.name == "AreaEventMapHUD")
		{
			prefabName = "AreaEventMapObj";
		}
		else
		{
			prefabName = "AreaEventObj";
		}
	}

	public void processHover(bool isOver, string hoverName)
	{
		if (isOver)
		{
			if (hoverName != null)
			{
			}
			string[] array = hoverName.Split('_');
			if (array[0] == "AreaEventObj")
			{
				int spaceIndex = CommonAPI.parseInt(array[1]);
				showAreaEventTooltip(spaceIndex);
			}
		}
		else
		{
			currentTooltip = -1;
			tooltipScript.setInactive();
		}
	}

	public void cleanupAreaEventHUD()
	{
		foreach (int key in eventObjList.Keys)
		{
			GameObject aObj = eventObjList[key];
			commonScreenObject.destroyPrefabImmediate(aObj);
		}
		eventObjList.Clear();
		eventAreaList.Clear();
	}

	public void refreshAreaEventHUD()
	{
		setVariables();
		Player player = game.getPlayer();
		GameData gameData = game.getGameData();
		List<Area> list = new List<Area>();
		list.AddRange(gameData.getAreaListWithEvents(player.getAreaRegion()));
		int maxEventCount = gameData.getAreaRegionByRefID(player.getAreaRegion()).getMaxEventCount();
		List<int> list2 = new List<int>();
		for (int i = 0; i < maxEventCount; i++)
		{
			if (eventObjList.ContainsKey(i) && eventAreaList.ContainsKey(i))
			{
				if (list.Contains(eventAreaList[i]))
				{
					updateEvent(i);
					list.Remove(eventAreaList[i]);
				}
				else
				{
					clearEvent(i);
				}
			}
			else
			{
				list2.Add(i);
			}
		}
		foreach (int item in list2)
		{
			if (list.Count > 0)
			{
				addNewEvent(list[0], item);
				list.RemoveAt(0);
			}
		}
	}

	private void showAreaEventTooltip(int spaceIndex)
	{
		setVariables();
		if (eventAreaList.ContainsKey(spaceIndex))
		{
			Area area = eventAreaList[spaceIndex];
			tooltipScript.showText(area.getCurrentEventTooltipInfo(game.getPlayer().getPlayerTimeLong()));
		}
		currentTooltip = spaceIndex;
	}

	private void addNewEvent(Area eventArea, int spaceIndex)
	{
		Vector3 zero = Vector3.zero;
		zero.x = (float)spaceIndex * -60f;
		Vector3 aEndPosition = zero;
		zero.y = 70f;
		GameObject gameObject = commonScreenObject.createPrefab(eventGrid, "AreaEventObj_" + spaceIndex, "Prefab/AreaEvent/" + prefabName, zero, Vector3.one, Vector3.zero);
		eventAreaList.Add(spaceIndex, eventArea);
		eventObjList.Add(spaceIndex, gameObject);
		updateEvent(spaceIndex);
		commonScreenObject.tweenPosition(gameObject.GetComponent<TweenPosition>(), zero, aEndPosition, 0.4f, null, string.Empty);
	}

	private void updateEvent(int spaceIndex)
	{
		Player player = game.getPlayer();
		GameObject aObject = eventObjList[spaceIndex];
		Area area = eventAreaList[spaceIndex];
		long eventTimeLeft = area.getEventTimeLeft(player.getPlayerTimeLong());
		UILabel component = commonScreenObject.findChild(aObject, "AreaEventTimer_label").GetComponent<UILabel>();
		if (eventTimeLeft <= 48)
		{
			component.alpha = 1f;
			component.text = CommonAPI.convertHalfHoursToTimeString(eventTimeLeft);
		}
		else
		{
			component.alpha = 0f;
		}
	}

	private void clearEvent(int spaceIndex)
	{
		GameObject gameObject = eventObjList[spaceIndex];
		eventObjList.Remove(spaceIndex);
		eventAreaList.Remove(spaceIndex);
		if (currentTooltip == spaceIndex)
		{
			tooltipScript.setInactive();
			currentTooltip = -1;
		}
		Vector3 zero = Vector3.zero;
		zero.x = (float)spaceIndex * -60f;
		Vector3 aStartPosition = zero;
		zero.y = 70f;
		commonScreenObject.tweenPosition(gameObject.GetComponent<TweenPosition>(), aStartPosition, zero, 0.4f, null, string.Empty);
		commonScreenObject.destroyPrefabDelay(gameObject, 0.5f);
	}

	private void setVariables()
	{
		if (game == null)
		{
			game = GameObject.Find("Game").GetComponent<Game>();
		}
		if (commonScreenObject == null)
		{
			commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		}
		if (shopMenuController == null)
		{
			shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		}
		if (tooltipScript == null)
		{
			if (base.gameObject.name == "AreaEventMapHUD")
			{
				tooltipScript = GameObject.Find("MapTooltipInfo").GetComponent<TooltipTextScript>();
			}
			else
			{
				tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
			}
		}
	}

	public void slideOutPanel()
	{
		commonScreenObject.tweenPosition(base.gameObject.GetComponent<TweenPosition>(), Vector3.zero, new Vector3(0f, 200f, 0f), 0.4f, null, string.Empty);
	}

	public void slideInPanel(bool transition)
	{
		if (transition)
		{
			commonScreenObject.tweenPosition(base.gameObject.GetComponent<TweenPosition>(), new Vector3(0f, 200f, 0f), Vector3.zero, 0.4f, null, string.Empty);
		}
		else
		{
			base.gameObject.transform.localPosition = Vector3.zero;
		}
	}
}
