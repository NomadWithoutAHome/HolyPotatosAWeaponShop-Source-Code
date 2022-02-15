using UnityEngine;

public class SmithMapActionClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_SmithMapActionMenu").GetComponent<GUISmithMapActionController>().processClick(base.gameObject.name);
	}
}
