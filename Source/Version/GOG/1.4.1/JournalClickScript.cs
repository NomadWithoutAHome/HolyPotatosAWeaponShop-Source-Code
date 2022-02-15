using UnityEngine;

public class JournalClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_Journal").GetComponent<GUIJournalController>().processClick(base.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_Journal").GetComponent<GUIJournalController>().processHover(isOver: true, base.name);
		}
		else
		{
			GameObject.Find("Panel_Journal").GetComponent<GUIJournalController>().processHover(isOver: false, base.name);
		}
	}
}
