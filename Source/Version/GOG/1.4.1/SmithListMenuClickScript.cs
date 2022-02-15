using UnityEngine;

public class SmithListMenuClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_SmithList").GetComponent<GUISmithListMenuController>().processClick(base.name);
	}
}
