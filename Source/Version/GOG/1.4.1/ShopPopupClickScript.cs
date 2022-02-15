using UnityEngine;

public class ShopPopupClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_Shop").GetComponent<GUIShopPopupController>().processClick(base.gameObject.name);
	}
}
