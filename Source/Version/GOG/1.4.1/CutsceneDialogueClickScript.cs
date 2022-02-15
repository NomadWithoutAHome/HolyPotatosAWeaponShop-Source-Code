using UnityEngine;

public class CutsceneDialogueClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("Panel_CutsceneDialogue").GetComponent<GUICutsceneDialogController>().showNext();
	}
}
