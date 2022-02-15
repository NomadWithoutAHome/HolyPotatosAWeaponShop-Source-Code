using UnityEngine;

public class QuestClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("GUIQuestController").GetComponent<GUIQuestController>().processClick(base.gameObject.name);
	}
}
