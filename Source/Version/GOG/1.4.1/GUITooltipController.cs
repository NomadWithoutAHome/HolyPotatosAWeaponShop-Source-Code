using System.Collections.Generic;
using UnityEngine;

public class GUITooltipController : MonoBehaviour
{
	private Dictionary<string, TooltipTextScript> tooltipList;

	private void Awake()
	{
		tooltipList = new Dictionary<string, TooltipTextScript>();
		tooltipList.Add("KeyTooltipInfo", GameObject.Find("KeyTooltipInfo").GetComponent<TooltipTextScript>());
		tooltipList.Add("ShortBubble", GameObject.Find("ShortBubble").GetComponent<TooltipTextScript>());
		tooltipList.Add("SmithInfoBubble", GameObject.Find("SmithInfoBubble").GetComponent<TooltipTextScript>());
		tooltipList.Add("HeroTooltipInfo", GameObject.Find("HeroTooltipInfo").GetComponent<TooltipTextScript>());
		tooltipList.Add("MapTooltipInfo", GameObject.Find("MapTooltipInfo").GetComponent<TooltipTextScript>());
	}

	private void Update()
	{
	}

	private void dismissAllActiveTooltip()
	{
		CommonAPI.debug("dismissing tooltip");
		foreach (TooltipTextScript value in tooltipList.Values)
		{
			if (value.gameObject.activeInHierarchy && value.GetComponentInChildren<UISprite>().color.a > 0f)
			{
				value.setInactive();
			}
		}
	}
}
