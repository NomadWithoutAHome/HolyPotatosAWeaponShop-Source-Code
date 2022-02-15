using UnityEngine;

public class ContractCompleteClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_ContractComplete").GetComponent<GUIContractCompleteController>().processClick(base.gameObject.name);
	}
}
