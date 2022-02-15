using UnityEngine;

public class LegendaryCompleteClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_LegendaryComplete").GetComponent<GUILegendaryCompleteController>().processClick(base.gameObject.name);
	}
}
