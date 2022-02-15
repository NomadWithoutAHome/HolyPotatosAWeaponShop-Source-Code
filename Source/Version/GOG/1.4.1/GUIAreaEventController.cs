using System.Globalization;
using UnityEngine;

public class GUIAreaEventController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private UITexture islandTexture;

	private UILabel islandName;

	private UILabel eventName;

	private UILabel eventDesc;

	private UILabel eventDuration;

	private UILabel eventEffect;

	private Area area;

	private AreaEvent areaEvent;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
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
			viewController.closeAreaEventPopup();
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
		if (Input.GetKeyDown(gameData.getKeyCodeByRefID("100008")))
		{
			processClick("Close_button");
		}
	}

	public void setReference(Area aArea, AreaEvent aEvent)
	{
		area = aArea;
		areaEvent = aEvent;
		GameData gameData = game.getGameData();
		UILabel component = commonScreenObject.findChild(base.gameObject, "AreaEvent_bg/AreaEventTitle_bg/AreaEventTitle_label").GetComponent<UILabel>();
		component.text = gameData.getTextByRefId("areaEventEffect10").ToUpper(CultureInfo.InvariantCulture);
		UILabel component2 = commonScreenObject.findChild(base.gameObject, "AreaEvent_bg/Close_button/Close_label").GetComponent<UILabel>();
		component2.text = gameData.getTextByRefId("menuGeneral05").ToUpper(CultureInfo.InvariantCulture);
		GameObject gameObject = commonScreenObject.findChild(base.gameObject, "AreaEvent_bg/EventInfo_bg").gameObject;
		islandTexture = commonScreenObject.findChild(gameObject.gameObject, "Island_texture").GetComponent<UITexture>();
		islandName = commonScreenObject.findChild(gameObject.gameObject, "IslandName_bg/IslandName_label").GetComponent<UILabel>();
		eventName = commonScreenObject.findChild(gameObject.gameObject, "EventName_bg/EventName_label").GetComponent<UILabel>();
		eventDesc = commonScreenObject.findChild(gameObject.gameObject, "EventName_bg/EventDesc_label").GetComponent<UILabel>();
		eventDuration = commonScreenObject.findChild(gameObject.gameObject, "EffectDuration_bg/EffectDuration_label").GetComponent<UILabel>();
		eventEffect = commonScreenObject.findChild(gameObject.gameObject, "EffectDuration_bg/Effect_label").GetComponent<UILabel>();
		islandTexture.mainTexture = commonScreenObject.loadTexture("Image/Area/" + area.getAreaImage());
		islandName.text = area.getAreaName();
		eventName.text = area.getCurrentEventName();
		eventDesc.text = area.getCurrentEventDescription();
		eventDuration.text = gameData.getTextByRefIdWithDynText("areaEventEffect11", "[duration]", CommonAPI.convertHalfHoursToTimeString(areaEvent.getDurationInHalfHours(), showHalfHours: false));
		eventEffect.text = area.getCurrentEventEffectString();
		audioController.playEventAppearAudio();
	}
}
