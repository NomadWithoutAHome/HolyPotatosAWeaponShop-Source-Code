using UnityEngine;

public class AreaClickScript : MonoBehaviour
{
	private void OnClick()
	{
		CommonAPI.debug("clicked: " + base.gameObject.name);
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("GUIMapController").GetComponent<GUIMapController>().processClick(base.gameObject.name);
	}
}
