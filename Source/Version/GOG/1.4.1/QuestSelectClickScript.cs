using UnityEngine;

public class QuestSelectClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_QuestSelect").GetComponent<GUIQuestSelectController>().processClick(base.gameObject.name);
	}
}
