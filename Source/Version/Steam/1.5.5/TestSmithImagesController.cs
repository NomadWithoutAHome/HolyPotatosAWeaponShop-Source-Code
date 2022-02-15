using System.Collections.Generic;
using UnityEngine;

public class TestSmithImagesController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private TooltipTextScript tooltipScript;

	private List<Smith> smithList;

	private int displayIndex;

	private UILabel smithName;

	private UITexture portrait;

	private UITexture fullbody;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		tooltipScript = GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>();
		smithList = new List<Smith>();
		displayIndex = 0;
	}

	public void processClick(string gameObjectName)
	{
		switch (gameObjectName)
		{
		case "TestSmith_right":
			navigateSmithList(1);
			break;
		case "TestSmith_left":
			navigateSmithList(-1);
			break;
		case "Close_button":
			viewController.closeSmithImagesTest();
			break;
		}
	}

	public void processHover(bool isOver, GameObject hoverObj)
	{
		string text = hoverObj.name;
		CommonAPI.debug("processHover " + text);
		if (isOver)
		{
			switch (text)
			{
			case "TestSmith_manage":
				tooltipScript.showText(smithList[displayIndex].getSmithStandardInfoString(showFullJobDetails: false));
				break;
			case "TestSmith_portrait":
				tooltipScript.showText(smithList[displayIndex].getSmithStandardInfoString(showFullJobDetails: true));
				break;
			}
		}
		else
		{
			tooltipScript.setInactive();
		}
	}

	public void setReference()
	{
		smithList = game.getGameData().getSmithList(checkDLC: false, includeNormal: true, includeLegendary: true, string.Empty);
		displayIndex = 0;
		smithName = commonScreenObject.findChild(base.gameObject, "TestSmith_label").GetComponent<UILabel>();
		portrait = commonScreenObject.findChild(base.gameObject, "TestSmith_portrait").GetComponent<UITexture>();
		fullbody = commonScreenObject.findChild(base.gameObject, "TestSmith_manage").GetComponent<UITexture>();
		showSmith();
	}

	private void navigateSmithList(int aIndexAdd)
	{
		displayIndex += aIndexAdd;
		if (displayIndex < 0)
		{
			displayIndex = smithList.Count - 1;
		}
		if (displayIndex >= smithList.Count)
		{
			displayIndex = 0;
		}
		showSmith();
	}

	private void showSmith()
	{
		Smith smith = smithList[displayIndex];
		smithName.text = smith.getSmithName() + "\n" + smith.getImage() + "\n" + (displayIndex + 1) + "/" + smithList.Count;
		fullbody.mainTexture = commonScreenObject.loadTexture("Image/Smith/" + smith.getImage() + "_manage");
		portrait.mainTexture = commonScreenObject.loadTexture("Image/Smith/Portraits/" + smith.getImage());
	}
}
