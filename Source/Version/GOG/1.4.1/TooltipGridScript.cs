using System.Collections.Generic;
using UnityEngine;

public class TooltipGridScript : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ViewController viewController;

	private ShopMenuController shopMenuController;

	private Camera uiCamera;

	private UILabel text;

	private UISprite background;

	private UIScrollView draggablePanel;

	private UIGrid grid;

	private List<GameObject> objList;

	private float appearSpeed;

	private bool scalingTransitions;

	protected Transform mTrans;

	protected float mTarget;

	protected float mCurrent;

	protected Vector3 mPos;

	protected Vector3 mSize = Vector3.zero;

	protected UIWidget[] mWidgets;

	protected bool menuActive;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		uiCamera = GameObject.Find("NGUICameraMapGUI").GetComponent<Camera>();
		text = commonScreenObject.findChild(base.gameObject, "TitleLabel").GetComponent<UILabel>();
		background = commonScreenObject.findChild(base.gameObject, "Bg_sprite").GetComponent<UISprite>();
		draggablePanel = commonScreenObject.findChild(base.gameObject, "Panel_IslandTooltipDraggable").GetComponent<UIScrollView>();
		grid = commonScreenObject.findChild(base.gameObject, "Panel_IslandTooltipDraggable/IslandTooltipGrid").GetComponent<UIGrid>();
		objList = new List<GameObject>();
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

	public void showText(string textString, Dictionary<string, ExploreItem> exploreItemList)
	{
		if (menuActive)
		{
			return;
		}
		GameData gameData = game.getGameData();
		this.text.text = textString;
		int num = 0;
		foreach (KeyValuePair<string, ExploreItem> exploreItem in exploreItemList)
		{
			Item itemByRefId = gameData.getItemByRefId(exploreItem.Key);
			GameObject gameObject = commonScreenObject.createPrefab(grid.gameObject, "ItemTooltip_" + num, "Prefab/Tooltip/TooltipListObj", Vector3.zero, Vector3.one, Vector3.zero);
			string text = "Image/";
			switch (itemByRefId.getItemType())
			{
			case ItemType.ItemTypeEnhancement:
				text += "Enchantment/";
				break;
			case ItemType.ItemTypeMaterial:
				text += "materials/";
				break;
			case ItemType.ItemTypeRelic:
				text += "relics/";
				break;
			}
			if (exploreItem.Value.getFound())
			{
				commonScreenObject.findChild(gameObject, "Image_texture").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture(text + itemByRefId.getImage());
			}
			else
			{
				commonScreenObject.findChild(gameObject, "Image_texture").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Area/question_box");
			}
			objList.Add(gameObject);
			num++;
		}
		grid.Reposition();
		draggablePanel.UpdateScrollbars();
		SetTooltip();
	}

	public void showText(string textString, Dictionary<string, ShopItem> shopItemList)
	{
		if (menuActive)
		{
			return;
		}
		GameData gameData = game.getGameData();
		this.text.text = textString;
		int num = 0;
		foreach (KeyValuePair<string, ShopItem> shopItem in shopItemList)
		{
			string key = shopItem.Key;
			Item itemByRefId = gameData.getItemByRefId(key);
			GameObject gameObject = commonScreenObject.createPrefab(grid.gameObject, "ItemTooltip_" + num, "Prefab/Tooltip/TooltipListObj", Vector3.zero, Vector3.one, Vector3.zero);
			string text = "Image/";
			switch (itemByRefId.getItemType())
			{
			case ItemType.ItemTypeEnhancement:
				text += "Enchantment/";
				break;
			case ItemType.ItemTypeMaterial:
				text += "materials/";
				break;
			case ItemType.ItemTypeRelic:
				text += "relics/";
				break;
			}
			commonScreenObject.findChild(gameObject, "Image_texture").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture(text + itemByRefId.getImage());
			objList.Add(gameObject);
			num++;
		}
		grid.Reposition();
		draggablePanel.UpdateScrollbars();
		SetTooltip();
	}

	public void showText(string textString, Dictionary<string, int> heroList)
	{
		if (menuActive)
		{
			return;
		}
		GameData gameData = game.getGameData();
		text.text = textString;
		int num = 0;
		foreach (KeyValuePair<string, int> hero in heroList)
		{
			Hero heroByHeroRefID = gameData.getHeroByHeroRefID(hero.Key);
			GameObject gameObject = commonScreenObject.createPrefab(grid.gameObject, "ItemTooltip_" + num, "Prefab/Tooltip/TooltipListObj", Vector3.zero, Vector3.one, Vector3.zero);
			commonScreenObject.findChild(gameObject, "Image_texture").GetComponent<UITexture>().mainTexture = commonScreenObject.loadTexture("Image/Hero/" + heroByHeroRefID.getImage());
			objList.Add(gameObject);
			num++;
		}
		grid.Reposition();
		draggablePanel.UpdateScrollbars();
		SetTooltip();
	}

	public virtual void SetTooltip()
	{
		if (!menuActive)
		{
			mTarget = 1f;
			mPos = Input.mousePosition;
			Transform transform = background.transform;
			Vector3 localPosition = background.transform.localPosition;
			Vector3 localScale = background.transform.localScale;
			mSize = background.localSize;
			mSize.x *= localScale.x;
			mSize.y *= localScale.y;
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

	public bool getMenuActive()
	{
		return menuActive;
	}

	public void setInactive()
	{
		menuActive = false;
		SetAlpha(0f);
		foreach (GameObject obj in objList)
		{
			commonScreenObject.destroyPrefabImmediate(obj);
		}
		objList.Clear();
	}
}
