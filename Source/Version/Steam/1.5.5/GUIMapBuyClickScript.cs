using UnityEngine;

public class GUIMapBuyClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("MapBuyHeader").GetComponent<GUIMapBuyMatController>().processClick(base.gameObject.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("MapBuyHeader").GetComponent<GUIMapBuyMatController>().processHover(isOver: true, base.gameObject);
		}
		else
		{
			GameObject.Find("MapBuyHeader").GetComponent<GUIMapBuyMatController>().processHover(isOver: false, base.gameObject);
		}
	}
}
