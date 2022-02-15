using System.Collections;
using UnityEngine;

public class ClickTimerController : MonoBehaviour
{
	private float holdCount;

	private bool holdState;

	private bool eventFired;

	private GameObject controllerObj;

	private float holdDuration;

	private void Awake()
	{
		holdCount = 0f;
		holdState = false;
		eventFired = false;
		controllerObj = null;
		holdDuration = 100f;
	}

	public void startClick(GameObject aControllerObj, float aDuration)
	{
		if (!holdState)
		{
			holdState = true;
			holdCount = 0f;
			eventFired = false;
			controllerObj = aControllerObj;
			holdDuration = aDuration;
			StartCoroutine("checkHold");
		}
	}

	public void endClick(bool isRelease)
	{
		holdState = false;
		if (controllerObj != null && holdCount > 0f)
		{
			StopCoroutine("checkHold");
			if (eventFired)
			{
				CommonAPI.debug(controllerObj.name + " END HOVER EVENT");
				controllerObj.BroadcastMessage("endHoverEvent");
			}
			else if (isRelease)
			{
				CommonAPI.debug(controllerObj.name + " DO CLICK EVENT");
				controllerObj.BroadcastMessage("doClickEvent");
			}
			holdCount = 0f;
			holdState = false;
			eventFired = false;
			controllerObj = null;
			holdDuration = 100f;
		}
	}

	private IEnumerator checkHold()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.02f);
			if (holdState)
			{
				holdCount += 0.02f;
				if (controllerObj != null && !eventFired && holdCount > holdDuration)
				{
					eventFired = true;
					CommonAPI.debug(controllerObj.name + " DO HOVER EVENT");
					controllerObj.BroadcastMessage("doHoverEvent");
				}
			}
		}
	}
}
