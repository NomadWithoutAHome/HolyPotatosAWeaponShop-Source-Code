using UnityEngine;

public class WeaponCompleteClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_WeaponCompleteNEW").GetComponent<GUIWeaponCompleteController>().processClick(base.gameObject.name);
	}
}
