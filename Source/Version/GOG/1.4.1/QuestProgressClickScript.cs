using UnityEngine;

public class QuestProgressClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_QuestProgress").GetComponent<GUIQuestProgressController>().processClick(base.gameObject.name);
	}
}
