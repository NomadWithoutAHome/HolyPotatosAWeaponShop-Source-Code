using UnityEngine;

public class ShopStarchRecordClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_ShopStarchRecord").GetComponent<GUIShopStarchRecordController>().processClick(base.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_ShopStarchRecord").GetComponent<GUIShopStarchRecordController>().processHover(isOver: true, base.name);
		}
		else
		{
			GameObject.Find("Panel_ShopStarchRecord").GetComponent<GUIShopStarchRecordController>().processHover(isOver: false, base.name);
		}
	}
}
