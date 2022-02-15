using UnityEngine;

public class PayDayClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_PayDay").GetComponent<GUIPayDayController>().processClick(base.gameObject.name);
	}
}
