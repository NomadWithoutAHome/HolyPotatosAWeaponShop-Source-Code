using UnityEngine;

public class SmithJobChangeClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_SmithJobChange").GetComponent<GUISmithJobChangeController>().processClick(base.gameObject.name);
	}
}
