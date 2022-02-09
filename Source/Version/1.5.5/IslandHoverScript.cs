using UnityEngine;

public class IslandHoverScript : MonoBehaviour
{
	private string islandLockedInfo;

	private void Awake()
	{
		islandLockedInfo = null;
	}

	private void OnHover(bool isOver)
	{
		GameObject.Find("GUIMapController").GetComponent<GUIMapController>().processHover(isOver, base.gameObject.name, islandLockedInfo);
	}

	private void OnDrag()
	{
		GameObject.Find("GUIMapController").GetComponent<GUIMapController>().processHover(isOver: false, base.gameObject.name, islandLockedInfo);
	}

	public void setIslandLockedInfo(string aText)
	{
		islandLockedInfo = aText;
	}
}
