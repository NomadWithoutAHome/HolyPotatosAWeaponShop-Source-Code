using UnityEngine;

public class GUICursorController : MonoBehaviour
{
	public Camera uiCamera;

	private Transform mTrans;

	private UISprite mSprite;

	private SpriteRenderer spriteRend;

	private UIAtlas mAtlas;

	private string mSpriteName;

	private void Awake()
	{
		mTrans = base.transform;
		mSprite = GetComponentInChildren<UISprite>();
		spriteRend = GetComponentInChildren<SpriteRenderer>();
		if (uiCamera == null)
		{
			uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		if (mSprite != null)
		{
			mAtlas = mSprite.atlas;
			mSpriteName = mSprite.spriteName;
			if (mSprite.depth < 100)
			{
				mSprite.depth = 100;
			}
		}
	}

	private void Update()
	{
		Vector3 mousePosition = Input.mousePosition;
		if (uiCamera != null)
		{
			mousePosition.x = Mathf.Clamp01(mousePosition.x / (float)Screen.width);
			mousePosition.y = Mathf.Clamp01(mousePosition.y / (float)Screen.height);
			mousePosition.z = 100f;
			mTrans.position = uiCamera.ViewportToWorldPoint(mousePosition);
			if (uiCamera.orthographic)
			{
				Vector3 localPosition = mTrans.localPosition;
				localPosition.x = Mathf.Round(localPosition.x);
				localPosition.y = Mathf.Round(localPosition.y);
				localPosition.z = 100f;
				mTrans.localPosition = localPosition;
			}
		}
		else
		{
			mousePosition.x -= (float)Screen.width * 0.5f;
			mousePosition.y -= (float)Screen.height * 0.5f;
			mousePosition.x = Mathf.Round(mousePosition.x);
			mousePosition.y = Mathf.Round(mousePosition.y);
			mousePosition.z = 100f;
			mTrans.localPosition = mousePosition;
		}
	}

	public void Clear()
	{
	}

	public void Set(UIAtlas atlas, string sprite)
	{
	}

	public void SetSprite(Sprite sprite)
	{
		spriteRend.sprite = sprite;
		Update();
	}
}
