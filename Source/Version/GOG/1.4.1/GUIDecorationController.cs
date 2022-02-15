using System.Collections.Generic;
using UnityEngine;

public class GUIDecorationController : MonoBehaviour
{
	private Game game;

	private GUIGridController gridController;

	private CommonScreenObject commonScreenObject;

	private AudioController audioController;

	private List<Decoration> decorationList;

	private Dictionary<string, GameObject> decorationGameobjectList;

	private RuntimeAnimatorController originalController;

	private GameObject Panel_Obstacles;

	private string decorationPrefix;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gridController = GameObject.Find("GUIGridController").GetComponent<GUIGridController>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		decorationGameobjectList = new Dictionary<string, GameObject>();
		Panel_Obstacles = GameObject.Find("Panel_Obstacles");
		decorationPrefix = "Decoration_";
	}

	public void createDecorations()
	{
		GameData gameData = game.getGameData();
		foreach (GameObject value in decorationGameobjectList.Values)
		{
			commonScreenObject.destroyPrefabImmediate(value);
		}
		decorationGameobjectList.Clear();
		decorationList = gameData.getDecorationCurrentDisplay();
		foreach (Decoration decoration in decorationList)
		{
			generateDecoration(decoration);
		}
	}

	public void destroyDecoration(Decoration oneDecoration)
	{
		List<DecorationPosition> decoPosByRefIdAndShopLevel = game.getGameData().getDecoPosByRefIdAndShopLevel(oneDecoration.getDecorationRefId(), game.getPlayer().getShopLevelInt());
		foreach (DecorationPosition item in decoPosByRefIdAndShopLevel)
		{
			if (decorationGameobjectList.ContainsKey(decorationPrefix + item.getDecorationPositionRefId()))
			{
				commonScreenObject.destroyPrefabImmediate(decorationGameobjectList[decorationPrefix + item.getDecorationPositionRefId()]);
				decorationGameobjectList.Remove(decorationPrefix + item.getDecorationPositionRefId());
			}
		}
	}

	public void generateDecoration(Decoration oneDecoration)
	{
		List<DecorationPosition> decoPosByRefIdAndShopLevel = game.getGameData().getDecoPosByRefIdAndShopLevel(oneDecoration.getDecorationRefId(), game.getPlayer().getShopLevelInt());
		foreach (DecorationPosition item in decoPosByRefIdAndShopLevel)
		{
			if (!decorationGameobjectList.ContainsKey(decorationPrefix + item.getDecorationPositionRefId()))
			{
				Vector3 decorationPosition = item.getDecorationPosition();
				GameObject gameObject = commonScreenObject.createPrefab(Panel_Obstacles, decorationPrefix + item.getDecorationPositionRefId(), "Prefab/Decoration/Decoration", decorationPosition, Vector3.one, Vector3.zero);
				gameObject.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Decoration/" + item.getDecorationImage());
				gameObject.transform.localRotation = Quaternion.Euler(item.getXDegree(), item.getYDegree(), 0f);
				gameObject.transform.localScale = Vector3.one;
				gameObject.GetComponent<SpriteRenderer>().sortingOrder = item.getSortOrder();
				gameObject.GetComponent<SpriteRenderer>().sortingLayerName = item.getSortLayer();
				decorationGameobjectList.Add(gameObject.name, gameObject);
			}
		}
	}

	public GameObject getDecorationObject(string aRefID)
	{
		string key = decorationPrefix + aRefID;
		if (decorationGameobjectList.ContainsKey(key))
		{
			return decorationGameobjectList[key];
		}
		return null;
	}
}
