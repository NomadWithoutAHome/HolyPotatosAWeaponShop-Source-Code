using UnityEngine;

public class MapSelectSmithClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("MapSelectSmithHeader").GetComponent<GUIMapSelectSmithController>().processClick(base.gameObject.name);
	}

	private void OnHover(bool isOver)
	{
		GameObject.Find("MapSelectSmithHeader").GetComponent<GUIMapSelectSmithController>().processHover(isOver, base.name);
	}

	private void OnDrag()
	{
		GameObject.Find("MapSelectSmithHeader").GetComponent<GUIMapSelectSmithController>().processHover(isOver: false, base.name);
	}
}
