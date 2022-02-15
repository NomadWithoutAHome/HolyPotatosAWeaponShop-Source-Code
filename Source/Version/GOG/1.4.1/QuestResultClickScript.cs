using UnityEngine;

public class QuestResultClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_QuestResult").GetComponent<GUIQuestResultController>().processClick(base.gameObject.name);
	}
}
