using UnityEngine;

public class ParticleRenderQueue : MonoBehaviour
{
	public int renderQueue;

	public string sortingLayer;

	private void Start()
	{
		ParticleSystem component = base.gameObject.GetComponent<ParticleSystem>();
		component.GetComponent<Renderer>().sortingLayerName = sortingLayer;
		component.GetComponent<Renderer>().sortingOrder = renderQueue;
	}
}
