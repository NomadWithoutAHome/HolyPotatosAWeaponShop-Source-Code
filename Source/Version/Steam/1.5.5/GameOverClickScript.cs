using UnityEngine;

public class GameOverClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_GameOver").GetComponent<GUIGameOverController>().processClick(base.gameObject.name);
	}
}
