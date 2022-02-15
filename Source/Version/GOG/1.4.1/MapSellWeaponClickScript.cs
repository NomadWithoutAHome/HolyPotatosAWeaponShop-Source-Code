using UnityEngine;

public class MapSellWeaponClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("MapSellWeaponHeader").GetComponent<GUIMapSellWeaponController>().processClick(base.gameObject.name);
	}

	private void OnHover(bool isOver)
	{
		GameObject.Find("MapSellWeaponHeader").GetComponent<GUIMapSellWeaponController>().processHover(isOver, base.name);
	}

	private void OnDrag()
	{
		GameObject.Find("MapSellWeaponHeader").GetComponent<GUIMapSellWeaponController>().processHover(isOver: false, base.name);
	}
}
