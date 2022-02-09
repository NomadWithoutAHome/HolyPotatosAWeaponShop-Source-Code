using UnityEngine;

public class GUISpeedClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_Speed").GetComponent<GUISpeedController>().processClick(base.gameObject.name);
	}
}
