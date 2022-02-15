using UnityEngine;

public class GoldenHammerClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_GoldenHammerAwards").GetComponent<GUIGoldenHammerAwardsController>().processClick(base.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_GoldenHammerAwards").GetComponent<GUIGoldenHammerAwardsController>().processHover(isOver: true, base.name);
		}
		else
		{
			GameObject.Find("Panel_GoldenHammerAwards").GetComponent<GUIGoldenHammerAwardsController>().processHover(isOver: false, base.name);
		}
	}

	private void OnDrag()
	{
		GameObject.Find("Panel_GoldenHammerAwards").GetComponent<GUIGoldenHammerAwardsController>().processHover(isOver: false, base.name);
	}
}
