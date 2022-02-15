using UnityEngine;

public class ForgeCompleteClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_ForgeComplete").GetComponent<GUIForgeCompleteController>().processClick(base.gameObject.name);
	}
}
