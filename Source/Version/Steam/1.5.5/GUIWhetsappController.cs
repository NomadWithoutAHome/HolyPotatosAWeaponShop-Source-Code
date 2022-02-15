using System.Collections.Generic;
using UnityEngine;

public class GUIWhetsappController : MonoBehaviour
{
	private Game game;

	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private TooltipTextScript tooltipScript;

	private UIButton toggleButton;

	private UISprite toggleSprite;

	private bool isOpen;

	private UILabel newMsgLabel;

	private TweenPosition listTween;

	private UIScrollBar listScroll;

	private GameObject listRoot;

	private UIButton allFilter;

	private UIButton noticesFilter;

	private UIButton smithFilter;

	private UIButton heroFilter;

	private WhetsappFilterType currentFilter;

	private List<Whetsapp> messageList;

	private int messageCount;

	private int newMessageCount;

	private bool isOverWhetsapp;

	public void setReference()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gameData = game.getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		toggleButton = commonScreenObject.findChild(base.gameObject, "WhetsappBar_bg/Whetsapp_toggle").GetComponent<UIButton>();
		toggleSprite = commonScreenObject.findChild(base.gameObject, "WhetsappBar_bg/Whetsapp_toggle").GetComponent<UISprite>();
		isOpen = true;
		newMsgLabel = commonScreenObject.findChild(base.gameObject, "WhetsappBar_bg/NewMsg_label").GetComponent<UILabel>();
		listTween = commonScreenObject.findChild(base.gameObject, "WhetsappPanel_clipPanel/WhetsappList").GetComponent<TweenPosition>();
		listScroll = commonScreenObject.findChild(base.gameObject, "WhetsappPanel_clipPanel/WhetsappList/WhetsappList_scrollbar").GetComponent<UIScrollBar>();
		listRoot = commonScreenObject.findChild(base.gameObject, "WhetsappPanel_clipPanel/WhetsappList/WhetsappList_clipPanel/WhetsappList_root").gameObject;
		allFilter = commonScreenObject.findChild(listTween.gameObject, "WhetsappTabs/Tab0All_bg").GetComponent<UIButton>();
		noticesFilter = commonScreenObject.findChild(listTween.gameObject, "WhetsappTabs/Tab1Notice_bg").GetComponent<UIButton>();
		smithFilter = commonScreenObject.findChild(listTween.gameObject, "WhetsappTabs/Tab2Smith_bg").GetComponent<UIButton>();
		heroFilter = commonScreenObject.findChild(listTween.gameObject, "WhetsappTabs/Tab3Hero_bg").GetComponent<UIButton>();
		allFilter.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("whetsappTab01");
		noticesFilter.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("whetsappTab02");
		smithFilter.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("whetsappTab03");
		heroFilter.GetComponentInChildren<UILabel>().text = gameData.getTextByRefId("whetsappTab04");
		currentFilter = WhetsappFilterType.WhetsappFilterTypeAll;
		messageList = new List<Whetsapp>();
		messageCount = 0;
		newMessageCount = 0;
		isOverWhetsapp = false;
		extendWhetsapp();
		updateWhetsappDisplay(forceScroll: true);
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "Whetsapp_toggle":
			toggleWhetsapp();
			break;
		case "Tab0All_bg":
			currentFilter = WhetsappFilterType.WhetsappFilterTypeAll;
			updateWhetsappDisplay(forceScroll: true);
			break;
		case "Tab1Notice_bg":
			currentFilter = WhetsappFilterType.WhetsappFilterTypeNotice;
			updateWhetsappDisplay(forceScroll: true);
			break;
		case "Tab2Smith_bg":
			currentFilter = WhetsappFilterType.WhetsappFilterTypeSmith;
			updateWhetsappDisplay(forceScroll: true);
			break;
		case "Tab3Hero_bg":
			currentFilter = WhetsappFilterType.WhetsappFilterTypeHero;
			updateWhetsappDisplay(forceScroll: true);
			break;
		}
	}

	public void processHover(bool isOver, string hoverName)
	{
		if (isOver)
		{
			isOverWhetsapp = true;
		}
		else
		{
			isOverWhetsapp = false;
		}
	}

	private void Update()
	{
		if (viewController != null && viewController.getGameStarted())
		{
			handleInput();
		}
	}

	private void handleInput()
	{
		if (Input.GetKeyDown(gameData.getKeyCodeByRefID("200010")) && !viewController.getHasPopup())
		{
			toggleWhetsapp();
		}
	}

	public void toggleWhetsapp()
	{
		if (isOpen)
		{
			hideWhetsapp();
		}
		else
		{
			extendWhetsapp();
		}
		updateWhetsappDisplay(forceScroll: true);
	}

	public void extendWhetsapp()
	{
		isOpen = true;
		toggleSprite.spriteName = "whetsapp_half";
		toggleButton.normalSprite = "whetsapp_half";
		audioController.playSlideEnterAudio();
		commonScreenObject.tweenPosition(listTween, listTween.transform.localPosition, new Vector3(-112f, 0f, 0f), 0.4f, null, string.Empty);
	}

	public void hideWhetsapp()
	{
		isOpen = false;
		toggleSprite.spriteName = "whetsapp_full";
		toggleButton.normalSprite = "whetsapp_full";
		audioController.playSlideExitAudio();
		commonScreenObject.tweenPosition(listTween, listTween.transform.localPosition, new Vector3(-112f, -260f, 0f), 0.4f, null, string.Empty);
	}

	public bool checkIsOverWhetsapp()
	{
		return isOverWhetsapp;
	}

	public void updateWhetsappDisplay(bool forceScroll)
	{
		if (isOpen)
		{
			updateMessages(forceScroll);
			updateTabs();
		}
		updateUnreadCount();
	}

	private void updateUnreadCount()
	{
		Player player = game.getPlayer();
		int num = gameData.countUnreadWhetsapp(player.getPlayerTimeLong());
		if (num > 0)
		{
			newMsgLabel.alpha = 1f;
			if (num > 10)
			{
				newMsgLabel.text = 10 + "+";
			}
			else
			{
				newMsgLabel.text = num.ToString();
			}
			if (newMessageCount != num)
			{
				if (num == 0)
				{
					newMessageCount = num;
					return;
				}
				newMessageCount = num;
				audioController.playWhetsappAudio();
			}
		}
		else
		{
			newMsgLabel.alpha = 0f;
		}
	}

	private void updateMessages(bool forceScroll)
	{
		Player player = game.getPlayer();
		long playerTimeLong = player.getPlayerTimeLong();
		messageList = gameData.getWhetsappDisplayList(playerTimeLong, currentFilter);
		if (messageList.Count != 0 && messageCount != messageList.Count)
		{
			messageCount = messageList.Count;
			audioController.playWhetsappAudio();
		}
		if (forceScroll)
		{
			listScroll.value = 1f;
			listScroll.ForceUpdate();
		}
		int num = 0;
		float num2 = 0f;
		foreach (Whetsapp message in messageList)
		{
			string text = "FFFFFF";
			string text2 = "FFFFFF";
			bool flag = true;
			switch (message.getFilterType())
			{
			case WhetsappFilterType.WhetsappFilterTypeNotice:
				text = "FFD84A";
				text2 = "FFD84A";
				flag = false;
				break;
			case WhetsappFilterType.WhetsappFilterTypeSmith:
				text = "56AE59";
				break;
			case WhetsappFilterType.WhetsappFilterTypeHero:
				text = "00AAC7";
				break;
			}
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			list.Add("[colorStart]");
			list2.Add("[D484F5]");
			list.Add("[colorEnd]");
			list2.Add("[-]");
			GameObject gameObject = commonScreenObject.createPrefab(listRoot, "WhetsappListObj_" + num, "Prefab/Whetsapp/WhetsappListObj", Vector3.zero, Vector3.one, Vector3.zero);
			UILabel component = commonScreenObject.findChild(gameObject, "Title").GetComponent<UILabel>();
			component.text = "[" + text + "]" + message.getSenderName() + "[-]";
			UILabel component2 = commonScreenObject.findChild(gameObject, "Time").GetComponent<UILabel>();
			long time = message.getTime();
			component2.text = "[i]" + CommonAPI.generateWhetsappDisplayDate(time, playerTimeLong) + "[-]";
			UILabel component3 = commonScreenObject.findChild(gameObject, "Text").GetComponent<UILabel>();
			string text3 = gameData.replaceTextInTextString(message.getMessageText(), list, list2);
			component3.text = "[" + text2 + "]" + text3 + "[-]";
			if (component3.printedSize.y < 22f)
			{
				component3.text += "\n";
			}
			if (flag)
			{
				commonScreenObject.findChild(gameObject, "Image_bg").GetComponent<UISprite>().spriteName = "bg_weapon";
			}
			else
			{
				commonScreenObject.findChild(gameObject, "Image_bg").GetComponent<UISprite>().spriteName = string.Empty;
			}
			commonScreenObject.findChild(gameObject, "Image_bg/Image_texture").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture(message.getImage());
			UISprite component4 = commonScreenObject.findChild(gameObject, "MessageSize").GetComponent<UISprite>();
			component4.UpdateAnchors();
			Vector2 localSize = component4.localSize;
			Vector3 localPosition = new Vector3(0f, num2 + localSize.y - 15f, 0f);
			gameObject.transform.localPosition = localPosition;
			gameObject.GetComponent<BoxCollider>().center = component4.transform.localPosition;
			gameObject.GetComponent<BoxCollider>().size = new Vector3(localSize.x, localSize.y, 1f);
			num2 += localSize.y;
			message.setRead(aRead: true);
			num++;
		}
		int childCount = listRoot.transform.childCount;
		if (childCount > messageList.Count)
		{
			for (int i = 1; i <= childCount - messageList.Count; i++)
			{
				Transform child = listRoot.transform.GetChild(childCount - i);
				commonScreenObject.destroyPrefabImmediate(child.gameObject);
			}
		}
		listRoot.transform.parent.GetComponent<UIScrollView>().UpdateScrollbars();
	}

	private void updateTabs()
	{
		if (isOpen)
		{
			switch (currentFilter)
			{
			case WhetsappFilterType.WhetsappFilterTypeNotice:
				allFilter.isEnabled = true;
				noticesFilter.isEnabled = false;
				smithFilter.isEnabled = true;
				heroFilter.isEnabled = true;
				break;
			case WhetsappFilterType.WhetsappFilterTypeSmith:
				allFilter.isEnabled = true;
				noticesFilter.isEnabled = true;
				smithFilter.isEnabled = false;
				heroFilter.isEnabled = true;
				break;
			case WhetsappFilterType.WhetsappFilterTypeHero:
				allFilter.isEnabled = true;
				noticesFilter.isEnabled = true;
				smithFilter.isEnabled = true;
				heroFilter.isEnabled = false;
				break;
			default:
				allFilter.isEnabled = false;
				noticesFilter.isEnabled = true;
				smithFilter.isEnabled = true;
				heroFilter.isEnabled = true;
				break;
			}
		}
		else
		{
			allFilter.isEnabled = false;
			noticesFilter.isEnabled = false;
			smithFilter.isEnabled = false;
			heroFilter.isEnabled = false;
		}
	}
}
