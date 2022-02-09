using UnityEngine;

public class IslandClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("GUIMapController").GetComponent<GUIMapController>().processClick(base.name);
	}
}
