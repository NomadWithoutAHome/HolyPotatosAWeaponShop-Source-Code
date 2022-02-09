using UnityEngine;

public class TouchScript : MonoBehaviour
{
	private float holdCount;

	private bool holdState;

	private void Awake()
	{
		holdCount = 0f;
		holdState = false;
	}

	private void Update()
	{
		if (Input.touchCount <= 0 || Input.GetTouch(0).phase != 0)
		{
			return;
		}
		holdCount += Time.deltaTime;
		if (holdCount > 0.7f)
		{
			holdState = true;
			CommonAPI.debug("IS HOLD");
			if (Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				holdState = false;
				holdCount = 0f;
				CommonAPI.debug("HOLD ENDED");
			}
		}
		else if (Input.GetTouch(0).phase == TouchPhase.Ended && !holdState)
		{
			CommonAPI.debug("IS TAP");
			holdCount = 0f;
		}
	}
}
