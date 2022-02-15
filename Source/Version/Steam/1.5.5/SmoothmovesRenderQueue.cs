using SmoothMoves;
using UnityEngine;

public class SmoothmovesRenderQueue : MonoBehaviour
{
	public int renderQueue;

	public string sortingLayer;

	private void Start()
	{
		BoneAnimation componentInChildren = base.gameObject.GetComponentInChildren<BoneAnimation>();
		if (componentInChildren != null)
		{
			componentInChildren.GetComponent<Renderer>().sortingLayerName = sortingLayer;
			componentInChildren.GetComponent<Renderer>().sortingOrder = renderQueue;
		}
	}
}
