using UnityEngine;

public class ForgeAppraisalClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_ForgeAppraisal").GetComponent<GUIForgeAppraisalController>().processClick(base.gameObject.name);
	}
}
