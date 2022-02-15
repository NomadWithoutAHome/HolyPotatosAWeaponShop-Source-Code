using UnityEngine;

public class OfferWeaponClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_OfferWeapon").GetComponent<GUIOfferWeaponController>().processClick(base.gameObject.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_OfferWeapon").GetComponent<GUIOfferWeaponController>().processHover(isOver: true, base.name);
		}
		else
		{
			GameObject.Find("Panel_OfferWeapon").GetComponent<GUIOfferWeaponController>().processHover(isOver: false, base.name);
		}
	}

	private void OnDrag()
	{
		GameObject.Find("Panel_OfferWeapon").GetComponent<GUIOfferWeaponController>().processHover(isOver: false, base.name);
	}
}
