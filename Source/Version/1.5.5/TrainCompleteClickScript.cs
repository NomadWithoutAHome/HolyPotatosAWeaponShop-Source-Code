using UnityEngine;

public class TrainCompleteClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_TrainComplete").GetComponent<GUITrainCompletePopupController>().processClick(base.gameObject.name);
	}
}
