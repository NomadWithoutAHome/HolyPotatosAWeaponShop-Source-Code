using UnityEngine;

public class StartScreenCreditClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("GUIStartScreenController").GetComponent<GUIStartScreenController>().processClick(base.gameObject.name);
	}
}
