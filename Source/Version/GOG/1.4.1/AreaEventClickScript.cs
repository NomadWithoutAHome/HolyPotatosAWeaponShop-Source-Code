using UnityEngine;

public class AreaEventClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_AreaEvent").GetComponent<GUIAreaEventController>().processClick(base.name);
	}
}
