using UnityEngine;

public class SmithSelectNewClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_SmithSelectNEW").GetComponent<GUISmithSelectNewController>().processClick(base.gameObject.name);
	}
}
