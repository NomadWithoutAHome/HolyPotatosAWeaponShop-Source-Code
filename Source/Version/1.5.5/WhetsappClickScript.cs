using UnityEngine;

public class WhetsappClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_Whetsapp").GetComponent<GUIWhetsappController>().processClick(base.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_Whetsapp").GetComponent<GUIWhetsappController>().processHover(isOver: true, base.name);
		}
		else
		{
			GameObject.Find("Panel_Whetsapp").GetComponent<GUIWhetsappController>().processHover(isOver: false, base.name);
		}
	}
}
