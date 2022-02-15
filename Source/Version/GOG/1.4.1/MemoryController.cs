using System;
using UnityEngine;

public class MemoryController : MonoBehaviour
{
	private void Update()
	{
		if (Time.frameCount % 600 == 0)
		{
			Resources.UnloadUnusedAssets();
			GC.Collect();
		}
	}
}
