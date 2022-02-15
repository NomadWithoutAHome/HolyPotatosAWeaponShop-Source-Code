using UnityEngine;

public class DirectionForgeClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_MenuForgeDirection").GetComponent<GUIDirectionForgeController>().processClick(base.gameObject.name);
	}
}
