using UnityEngine;

public class MenuforgeClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_MenuForge").GetComponent<GUIMenuForgeController>().processClick(base.gameObject.name);
	}
}
