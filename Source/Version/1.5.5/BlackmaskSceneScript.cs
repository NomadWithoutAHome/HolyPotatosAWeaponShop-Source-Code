using UnityEngine;

public class BlackmaskSceneScript : MonoBehaviour
{
	private SpriteRenderer blackMask;

	private bool loadingFromBlack;

	private bool loadingToBlack;

	private void Awake()
	{
		blackMask = base.gameObject.GetComponent<SpriteRenderer>();
		loadingFromBlack = false;
		loadingToBlack = false;
	}

	private void Update()
	{
		if (loadingFromBlack)
		{
			if (blackMask.color.a > 0f)
			{
				Color color = blackMask.color;
				color.a -= 0.05f;
				blackMask.color = color;
			}
			else if (blackMask.color.a <= 0f)
			{
				loadingFromBlack = false;
			}
		}
		if (loadingToBlack)
		{
			if (blackMask.color.a < 0.7f)
			{
				Color color2 = blackMask.color;
				color2.a += 0.05f;
				blackMask.color = color2;
			}
			else if (blackMask.color.a >= 0.7f)
			{
				loadingToBlack = false;
			}
		}
	}

	public void startLoadingFromBlack()
	{
		loadingFromBlack = true;
		loadingToBlack = false;
	}

	public void startLoadingToBlack()
	{
		loadingToBlack = true;
		loadingFromBlack = false;
	}
}
