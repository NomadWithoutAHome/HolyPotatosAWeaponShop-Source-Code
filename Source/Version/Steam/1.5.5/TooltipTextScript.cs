using UnityEngine;

public class TooltipTextScript : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private Smith smithInfo;

	private Camera uiCamera;

	private UILabel text;

	private UISprite sprite;

	private UISprite background;

	private float appearSpeed;

	private bool scalingTransitions;

	protected Transform mTrans;

	protected float mTarget;

	protected float mCurrent;

	protected Vector3 mPos;

	protected Vector3 mSize = Vector3.zero;

	protected UIWidget[] mWidgets;

	protected bool menuActive;

	private int testInt;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		if (base.gameObject.name == "MapTooltipInfo" || base.gameObject.name == "HeroTooltipInfo")
		{
			uiCamera = GameObject.Find("NGUICameraMapGUI").GetComponent<Camera>();
		}
		else
		{
			uiCamera = GameObject.Find("NGUICameraGUI").GetComponent<Camera>();
		}
		text = commonScreenObject.findChild(base.gameObject, "Label").GetComponent<UILabel>();
		background = commonScreenObject.findChild(base.gameObject, "Bg_sprite").GetComponent<UISprite>();
		appearSpeed = 10f;
		scalingTransitions = true;
		mTrans = base.transform;
		mPos = mTrans.localPosition;
		mWidgets = GetComponentsInChildren<UIWidget>();
		menuActive = false;
	}

	private void Start()
	{
		SetAlpha(0f);
	}

	protected virtual void Update()
	{
		if (mCurrent != mTarget)
		{
			mCurrent = Mathf.Lerp(mCurrent, mTarget, RealTime.deltaTime * appearSpeed);
			if (Mathf.Abs(mCurrent - mTarget) < 0.001f)
			{
				mCurrent = mTarget;
			}
			SetAlpha(mCurrent * mCurrent);
			if (scalingTransitions)
			{
				Vector3 vector = mSize * 0.25f;
				vector.y = 0f - vector.y;
				Vector3 localScale = Vector3.one * (0.8f + mCurrent * 0.2f);
				Vector3 localPosition = Vector3.Lerp(mPos - vector, mPos, mCurrent);
				mTrans.localPosition = localPosition;
				mTrans.localScale = localScale;
			}
		}
	}

	protected virtual void SetAlpha(float val)
	{
		if (val == 0f)
		{
			menuActive = false;
			mCurrent = 0f;
			mTarget = 0f;
		}
		if (val == 1f)
		{
			menuActive = true;
		}
		int i = 0;
		for (int num = mWidgets.Length; i < num; i++)
		{
			UIWidget uIWidget = mWidgets[i];
			Color color = uIWidget.color;
			color.a = val;
			uIWidget.color = color;
		}
	}

	public void showText(string textString)
	{
		text.text = textString;
		SetText();
	}

	public virtual void SetText()
	{
		if (!menuActive)
		{
			mTarget = 1f;
			mPos = Input.mousePosition;
			Transform transform = text.transform;
			Vector3 localPosition = transform.localPosition;
			Vector3 localScale = transform.localScale;
			mSize = text.localSize;
			mSize.x = mSize.x * localScale.x + 40f;
			mSize.y = mSize.y * localScale.y + 40f;
			mPos.x = Mathf.Clamp01(mPos.x / (float)Screen.width);
			mPos.y = Mathf.Clamp01(mPos.y / (float)Screen.height);
			float num = uiCamera.orthographicSize / mTrans.parent.lossyScale.y;
			float num2 = (float)Screen.height * 0.5f / num;
			Vector2 vector = new Vector2(num2 * mSize.x / (float)Screen.width, num2 * mSize.y / (float)Screen.height);
			mPos.x = Mathf.Min(mPos.x, 1f - vector.x);
			mPos.y = Mathf.Max(mPos.y, vector.y);
			mTrans.position = uiCamera.ViewportToWorldPoint(mPos);
			mPos = mTrans.localPosition;
			mPos.x = Mathf.Round(mPos.x);
			mPos.y = Mathf.Round(mPos.y);
			mTrans.localPosition = mPos;
		}
	}

	public void showSprite()
	{
	}

	public virtual void SetSprite()
	{
		if (!menuActive)
		{
			mTarget = 1f;
			mPos = Input.mousePosition;
			Transform transform = sprite.transform;
			Vector3 localPosition = transform.localPosition;
			Vector3 localScale = transform.localScale;
			mSize.x *= localScale.x;
			mSize.y *= localScale.y;
			mPos.x = Mathf.Clamp01(mPos.x / (float)Screen.width);
			mPos.y = Mathf.Clamp01(mPos.y / (float)Screen.height);
			float num = uiCamera.orthographicSize / mTrans.parent.lossyScale.y;
			float num2 = (float)Screen.height * 0.5f / num;
			Vector2 vector = new Vector2(num2 * mSize.x / (float)Screen.width, num2 * mSize.y / (float)Screen.height);
			mPos.x = Mathf.Min(mPos.x, (1f - vector.x) * 0.85f);
			mPos.y = Mathf.Max(mPos.y, vector.y);
			mTrans.position = uiCamera.ViewportToWorldPoint(mPos);
			mPos = mTrans.localPosition;
			mPos.x = Mathf.Round(mPos.x);
			mPos.y = Mathf.Round(mPos.y);
			mTrans.localPosition = mPos;
		}
	}

	public void refreshText()
	{
		if (smithInfo != null)
		{
			smithInfo = game.getPlayer().getSmithByRefID(smithInfo.getSmithRefId());
			text.text = smithInfo.getSmithName() + ": " + smithInfo.getRemainingMood();
		}
	}

	public Smith getSmithInfo()
	{
		return smithInfo;
	}

	public bool getMenuActive()
	{
		return menuActive;
	}

	public void setInactive()
	{
		menuActive = false;
		SetAlpha(0f);
	}
}
