using UnityEngine;

public class AreaUnlockedPanelScript : MonoBehaviour
{
	public void destroySelf()
	{
		Object.DestroyImmediate(base.gameObject);
	}
}
