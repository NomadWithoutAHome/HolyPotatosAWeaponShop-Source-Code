using UnityEngine;

public class StationClickScript : MonoBehaviour
{
	private CommonScreenObject commonScreenObject;

	private void Awake()
	{
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
	}

	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("GUIAnimationClickController").GetComponent<GUIAnimationClickController>().processClick(base.gameObject.transform.parent.gameObject);
	}

	private void OnHover(bool isOver)
	{
		GetComponent<SpriteRenderer>().enabled = isOver;
		GameObject.Find("GUIAnimationClickController").GetComponent<GUIAnimationClickController>().processStationHover(isOver, base.gameObject.transform.parent.name);
	}

	private void OnDrag()
	{
		GetComponent<SpriteRenderer>().enabled = false;
		GameObject.Find("GUIAnimationClickController").GetComponent<GUIAnimationClickController>().processStationHover(isOver: false, base.gameObject.transform.parent.name);
	}
}
