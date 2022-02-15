using UnityEngine;

public class ForgeProgressNewClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_ForgeProgressNew").GetComponent<GUIForgeProgressNewController>().processClick(base.name);
	}
}
