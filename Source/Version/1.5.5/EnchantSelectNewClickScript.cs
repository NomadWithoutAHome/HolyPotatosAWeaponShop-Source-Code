using UnityEngine;

public class EnchantSelectNewClickScript : MonoBehaviour
{
	private void OnClick()
	{
		GameObject.Find("AudioController").GetComponent<AudioController>().playButtonAudio();
		GameObject.Find("Panel_EnchantmentSelectNEW").GetComponent<GUIEnchantmentSelectNewController>().processClick(base.gameObject.name);
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			GameObject.Find("Panel_EnchantmentSelectNEW").GetComponent<GUIEnchantmentSelectNewController>().processHover(isOver: true, base.name);
		}
		else
		{
			GameObject.Find("Panel_EnchantmentSelectNEW").GetComponent<GUIEnchantmentSelectNewController>().processHover(isOver: false, base.name);
		}
	}

	private void OnDrag()
	{
		GameObject.Find("Panel_EnchantmentSelectNEW").GetComponent<GUIEnchantmentSelectNewController>().processHover(isOver: false, base.name);
	}
}
