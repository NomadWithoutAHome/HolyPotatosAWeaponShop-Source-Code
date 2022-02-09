using UnityEngine;

public class ResearchCompleteClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_ResearchCompleteNEW").GetComponent<GUIResearchCompleteController>().processClick(base.gameObject.name);
	}
}
