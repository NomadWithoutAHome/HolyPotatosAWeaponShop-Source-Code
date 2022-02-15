using UnityEngine;

public class AddEnchantmentClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_EnchantItem").GetComponent<GUIAddEnchantmentController>().processClick(base.name);
	}
}
