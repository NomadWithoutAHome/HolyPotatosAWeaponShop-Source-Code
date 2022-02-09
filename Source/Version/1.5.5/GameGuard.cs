using UnityEngine;

public class GameGuard : MonoBehaviour
{
	private void Start()
	{
		//StartCoroutine(SC.GameGuard(Application.dataPath));
	}

	private void OnApplicationQuit()
	{
		if (SC.HasGameGuard())
		{
			SC.StopGameGuard();
		}
	}
}
