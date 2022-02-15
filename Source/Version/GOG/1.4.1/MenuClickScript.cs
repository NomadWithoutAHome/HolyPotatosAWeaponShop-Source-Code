using UnityEngine;

public class MenuClickScript : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ShopMenuController shopMenuController;

	private ViewController viewController;

	private TooltipTextScript tooltipScript;

	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_BottomMenu").GetComponent<BottomMenuController>().processClick(base.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_BottomMenu").GetComponent<BottomMenuController>().processHover(isOver: true, base.name);
		}
		else
		{
			GameObject.Find("Panel_BottomMenu").GetComponent<BottomMenuController>().processHover(isOver: false, base.name);
		}
	}

	private void OnDrag()
	{
		GameObject.Find("Panel_BottomMenu").GetComponent<BottomMenuController>().processHover(isOver: false, base.name);
	}
}
