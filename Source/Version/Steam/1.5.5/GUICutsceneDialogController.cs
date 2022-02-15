using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUICutsceneDialogController : MonoBehaviour
{
	private GUICutsceneController cutsceneController;

	private UILabel characterName;

	private UILabel dialogueLabel;

	private string fullDialogue;

	private List<char> charDialogue;

	private bool nextDialoguePresent;

	private BoxCollider clickDetect;

	private void Awake()
	{
		cutsceneController = GameObject.Find("GUICutsceneController").GetComponent<GUICutsceneController>();
		characterName = GameObject.Find("CharacterName").GetComponent<UILabel>();
		dialogueLabel = GameObject.Find("DialogueLabel").GetComponent<UILabel>();
		clickDetect = GameObject.Find("CutsceneFrame").GetComponent<BoxCollider>();
	}

	public void setDialogue(CutsceneDialogue aDialogue, bool isNextDialoguePresent)
	{
		characterName.text = aDialogue.getDialogueName();
		dialogueLabel.text = string.Empty;
		fullDialogue = aDialogue.getDialogueText();
		charDialogue = new List<char>(fullDialogue.ToCharArray());
		nextDialoguePresent = isNextDialoguePresent;
		StartCoroutine("renderDialogue");
	}

	private IEnumerator renderDialogue()
	{
		while (charDialogue.Count > 0)
		{
			yield return new WaitForSeconds(Constants.DialogueSpeed);
			dialogueLabel.text += charDialogue[0];
			charDialogue.RemoveAt(0);
		}
	}

	public void showNext()
	{
		if (charDialogue.Count > 0)
		{
			StopCoroutine("renderDialogue");
			dialogueLabel.text = fullDialogue;
			charDialogue.Clear();
			return;
		}
		if (!nextDialoguePresent)
		{
			GameObject.Find("ViewController").GetComponent<ViewController>().closeCutsceneDialog();
		}
		cutsceneController.showNextDialogue();
	}
}
