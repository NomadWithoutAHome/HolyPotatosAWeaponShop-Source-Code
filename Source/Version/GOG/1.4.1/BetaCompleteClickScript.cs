using UnityEngine;

public class BetaCompleteClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_BetaComplete").GetComponent<GUIBetaCompleteController>().processClick(base.gameObject.name);
	}
}
