using UnityEngine;

public class QuestCompleteClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_QuestComplete").GetComponent<GUIQuestCompleteController>().processClick(base.name);
	}
}
