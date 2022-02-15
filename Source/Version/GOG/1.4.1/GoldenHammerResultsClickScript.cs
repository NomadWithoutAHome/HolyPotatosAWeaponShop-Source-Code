using UnityEngine;

public class GoldenHammerResultsClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_GoldenHammerResults").GetComponent<GUIGoldenHammerResultsController>().processClick(base.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_GoldenHammerResults").GetComponent<GUIGoldenHammerResultsController>().processHover(isOver: true, base.name);
		}
		else
		{
			GameObject.Find("Panel_GoldenHammerResults").GetComponent<GUIGoldenHammerResultsController>().processHover(isOver: false, base.name);
		}
	}

	private void OnDrag()
	{
		GameObject.Find("Panel_GoldenHammerResults").GetComponent<GUIGoldenHammerResultsController>().processHover(isOver: false, base.name);
	}
}
