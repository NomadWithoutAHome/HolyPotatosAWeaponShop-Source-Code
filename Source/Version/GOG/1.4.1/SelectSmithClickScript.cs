using UnityEngine;

public class SelectSmithClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_SelectSmith").GetComponent<GUISelectSmithController>().processClick(base.gameObject.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_SelectSmith").GetComponent<GUISelectSmithController>().processHover(isOver: true, base.gameObject);
		}
		else
		{
			GameObject.Find("Panel_SelectSmith").GetComponent<GUISelectSmithController>().processHover(isOver: false, base.gameObject);
		}
	}

	private void OnDrag()
	{
		GameObject.Find("Panel_SelectSmith").GetComponent<GUISelectSmithController>().processHover(isOver: false, base.gameObject);
	}
}
