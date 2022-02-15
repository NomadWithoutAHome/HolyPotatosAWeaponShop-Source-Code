using UnityEngine;

public class ContractClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_Contract").GetComponent<GUIContractController>().processClick(base.gameObject.name);
	}
}
