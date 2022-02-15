using UnityEngine;

public class ShopUpgradeClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_ShopUpgrade").GetComponent<GUIShopUpgradeHUDController>().processClick(base.name);
	}

	private void OnHover(bool isOver)
	{
		GameObject.Find("Panel_ShopUpgrade").GetComponent<GUIShopUpgradeHUDController>().processHover(isOver, base.name);
	}

	private void OnDrag()
	{
		GameObject.Find("Panel_ShopUpgrade").GetComponent<GUIShopUpgradeHUDController>().processHover(isOver: false, base.name);
	}
}
