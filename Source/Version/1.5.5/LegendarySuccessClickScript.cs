using UnityEngine;

public class LegendarySuccessClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_LegendarySuccess").GetComponent<GUILegendarySuccessController>().processClick(base.gameObject.name);
	}
}
