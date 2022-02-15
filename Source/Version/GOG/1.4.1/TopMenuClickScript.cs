using UnityEngine;

public class TopMenuClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_TopLeftMenu").GetComponent<GUITopMenuNewController>().processClick(base.name);
	}
}
