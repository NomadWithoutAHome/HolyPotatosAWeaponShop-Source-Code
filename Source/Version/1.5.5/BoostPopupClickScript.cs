using UnityEngine;

public class BoostPopupClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_Boost").GetComponent<GUIBoostController>().processClick(base.name);
	}
}
