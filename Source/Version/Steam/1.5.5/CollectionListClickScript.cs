using UnityEngine;

public class CollectionListClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_CollectionNEW").GetComponent<GUIWeaponCollectionController>().processClick(base.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_CollectionNEW").GetComponent<GUIWeaponCollectionController>().processHover(isOver: true, base.name);
		}
		else
		{
			GameObject.Find("Panel_CollectionNEW").GetComponent<GUIWeaponCollectionController>().processHover(isOver: false, base.name);
		}
	}

	private void OnDrag()
	{
		GameObject.Find("Panel_CollectionNEW").GetComponent<GUIWeaponCollectionController>().processHover(isOver: false, base.name);
	}
}
