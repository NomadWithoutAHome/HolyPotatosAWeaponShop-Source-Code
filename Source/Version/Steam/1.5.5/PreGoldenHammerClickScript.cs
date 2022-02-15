using UnityEngine;

public class PreGoldenHammerClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_GoldenHammerPreEvent").GetComponent<GUIPreGoldenHammerController>().processClick(base.name);
	}
}
