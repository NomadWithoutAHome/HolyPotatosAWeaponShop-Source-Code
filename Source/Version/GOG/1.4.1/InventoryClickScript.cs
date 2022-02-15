using UnityEngine;

public class InventoryClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_Inventory").GetComponent<GUIInventoryController>().processClick(base.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_Inventory").GetComponent<GUIInventoryController>().processHover(isOver: true, base.name);
		}
		else
		{
			GameObject.Find("Panel_Inventory").GetComponent<GUIInventoryController>().processHover(isOver: false, base.name);
		}
	}

	private void OnDrag()
	{
		GameObject.Find("Panel_Inventory").GetComponent<GUIInventoryController>().processHover(isOver: false, base.name);
	}
}
