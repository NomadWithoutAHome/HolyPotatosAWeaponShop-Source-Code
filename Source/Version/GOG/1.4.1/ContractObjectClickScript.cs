using UnityEngine;

public class ContractObjectClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_Contract").GetComponent<GUIContractController>().processClick(base.transform.parent.name);
	}
}
