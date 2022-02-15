using AnimationOrTween;
using SmoothMoves;
using UnityEngine;

public class CommonScreenObject : MonoBehaviour
{
	public GameObject getPrefab(string prefabName, string objectName, string parentName, Vector3 startPosition, Vector3 startScale)
	{
		GameObject gameObject = GameObject.Find(objectName);
		if (gameObject == null)
		{
			gameObject = Object.Instantiate(Resources.Load("Prefab/Controller/" + prefabName)) as GameObject;
			if (parentName != string.Empty)
			{
				gameObject.transform.SetParent(GameObject.Find(parentName).transform);
			}
			gameObject.name = objectName;
			gameObject.transform.localPosition = startPosition;
			gameObject.transform.localScale = startScale;
		}
		return gameObject;
	}

	public GameObject getController(string controllerPrefab)
	{
		GameObject gameObject = GameObject.Find(controllerPrefab);
		if (gameObject == null)
		{
			gameObject = Object.Instantiate(Resources.Load("Prefab/Controller/" + controllerPrefab)) as GameObject;
			gameObject.transform.SetParent(GameObject.Find("ControllerList").transform);
			gameObject.name = controllerPrefab;
		}
		return gameObject;
	}

	public bool checkShopScaleScrollAllowed()
	{
		GameObject gameObject = GameObject.Find("Panel_Whetsapp");
		if (gameObject != null && gameObject.GetComponent<GUIWhetsappController>().checkIsOverWhetsapp())
		{
			return false;
		}
		if (GameObject.Find("Panel_ShopUpgrade") != null && GameObject.Find("Panel_ShopUpgrade").GetComponent<GUIShopUpgradeHUDController>().checkIsOverMenu())
		{
			return false;
		}
		return true;
	}

	public Texture2D loadTexture2D(string aPath)
	{
		return (Texture2D)Resources.Load(aPath, typeof(Texture2D));
	}

	public Texture loadTexture(string aPath)
	{
		return Resources.Load(aPath) as Texture;
	}

	public UnityEngine.Sprite loadSprite(string aPath)
	{
		return Resources.Load<UnityEngine.Sprite>(aPath);
	}

	public void showImage(GameObject aObj, bool aValue)
	{
		UIButton[] componentsInChildren = aObj.GetComponentsInChildren<UIButton>();
		foreach (UIButton uIButton in componentsInChildren)
		{
			uIButton.enabled = aValue;
		}
		if (aValue)
		{
			UITexture[] componentsInChildren2 = aObj.GetComponentsInChildren<UITexture>();
			foreach (UITexture uITexture in componentsInChildren2)
			{
				uITexture.alpha = 1f;
			}
			UISprite[] componentsInChildren3 = aObj.GetComponentsInChildren<UISprite>();
			foreach (UISprite uISprite in componentsInChildren3)
			{
				uISprite.alpha = 1f;
			}
			UILabel[] componentsInChildren4 = aObj.GetComponentsInChildren<UILabel>();
			foreach (UILabel uILabel in componentsInChildren4)
			{
				uILabel.alpha = 1f;
			}
		}
		else
		{
			UITexture[] componentsInChildren5 = aObj.GetComponentsInChildren<UITexture>();
			foreach (UITexture uITexture2 in componentsInChildren5)
			{
				uITexture2.alpha = 0f;
			}
			UISprite[] componentsInChildren6 = aObj.GetComponentsInChildren<UISprite>();
			foreach (UISprite uISprite2 in componentsInChildren6)
			{
				uISprite2.alpha = 0f;
			}
			UILabel[] componentsInChildren7 = aObj.GetComponentsInChildren<UILabel>();
			foreach (UILabel uILabel2 in componentsInChildren7)
			{
				uILabel2.alpha = 0f;
			}
		}
		BoxCollider[] componentsInChildren8 = aObj.GetComponentsInChildren<BoxCollider>();
		foreach (BoxCollider boxCollider in componentsInChildren8)
		{
			boxCollider.enabled = aValue;
			boxCollider.isTrigger = aValue;
		}
		TweenScale[] componentsInChildren9 = aObj.GetComponentsInChildren<TweenScale>();
		foreach (TweenScale tweenScale in componentsInChildren9)
		{
			tweenScale.enabled = aValue;
		}
		TweenAlpha[] componentsInChildren10 = aObj.GetComponentsInChildren<TweenAlpha>();
		foreach (TweenAlpha tweenAlpha in componentsInChildren10)
		{
			tweenAlpha.enabled = aValue;
		}
	}

	public void enableClick(GameObject aObj, bool aValue)
	{
		BoxCollider[] componentsInChildren = aObj.GetComponentsInChildren<BoxCollider>();
		foreach (BoxCollider boxCollider in componentsInChildren)
		{
			boxCollider.enabled = aValue;
			boxCollider.isTrigger = aValue;
		}
	}

	public void setLabelText(GameObject aObj, string aText)
	{
		UILabel component = aObj.GetComponent<UILabel>();
		component.text = aText;
	}

	public void changeSprite(GameObject aObj, string aName)
	{
		UISprite component = aObj.GetComponent<UISprite>();
		component.spriteName = aName;
	}

	public void setProgressBar(GameObject aObj, float aValue)
	{
		UIProgressBar component = aObj.GetComponent<UIProgressBar>();
		component.value = aValue;
	}

	public void changeTexture(GameObject aObj, string aName)
	{
		UITexture component = aObj.GetComponent<UITexture>();
		component.mainTexture = loadTexture(aName);
	}

	public void rotateObject(GameObject aObj, float xDegree, float yDegree, float zDegree)
	{
		aObj.transform.localEulerAngles = new Vector3(xDegree, yDegree, zDegree);
	}

	public void refreshGrid(GameObject aObj)
	{
		UIGrid component = aObj.GetComponent<UIGrid>();
		component.repositionNow = true;
	}

	public Transform findChild(GameObject aObject, string aPath)
	{
		return aObject.transform.Find(aPath);
	}

	public GameObject createPrefab(GameObject aParent, string aName, string aPath, Vector3 aPosition, Vector3 aScale, Vector3 aRotation)
	{
		GameObject gameObject = GameObject.Find(aName);
		if (gameObject == null)
		{
			gameObject = ((aPath == null) ? new GameObject() : (Object.Instantiate(Resources.Load(aPath)) as GameObject));
			gameObject.name = aName;
			gameObject.transform.SetParent(aParent.transform);
			gameObject.transform.localPosition = aPosition;
			gameObject.transform.localScale = aScale;
			gameObject.transform.localRotation = Quaternion.Euler(aRotation);
		}
		return gameObject;
	}

	public GameObject createAudioPrefabs(string aFilename)
	{
		GameObject gameObject = GameObject.Find(aFilename);
		if (gameObject == null)
		{
			gameObject = Object.Instantiate(Resources.Load("Prefab/Sound/" + aFilename)) as GameObject;
			gameObject.name = aFilename;
			gameObject.transform.SetParent(GameObject.Find("AudioController").transform);
		}
		return gameObject;
	}

	public GameObject createAudioPrefabsMultiple(string aFilename, string aObjName)
	{
		GameObject gameObject = GameObject.Find(aObjName);
		if (gameObject == null)
		{
			gameObject = Object.Instantiate(Resources.Load("Prefab/Sound/" + aFilename)) as GameObject;
			gameObject.name = aObjName;
			gameObject.transform.SetParent(GameObject.Find("AudioController").transform);
		}
		return gameObject;
	}

	public void destroyPrefab(GameObject aObj)
	{
		if (aObj != null)
		{
			Object.Destroy(aObj);
		}
	}

	public void destroyPrefabImmediate(GameObject aObj)
	{
		if (aObj != null)
		{
			Object.DestroyImmediate(aObj);
		}
	}

	public void destroyPrefabDelay(GameObject aObj, float aDelay)
	{
		if (aObj != null)
		{
			aObj.name = "Destroy_" + aObj.name;
			Object.Destroy(aObj, aDelay);
		}
	}

	public void clearTooltips()
	{
		GameObject gameObject = GameObject.Find("Anchor_centre");
		if (gameObject != null)
		{
			TooltipTextScript[] componentsInChildren = gameObject.GetComponentsInChildren<TooltipTextScript>();
			foreach (TooltipTextScript tooltipTextScript in componentsInChildren)
			{
				tooltipTextScript.setInactive();
			}
		}
		GameObject gameObject2 = GameObject.Find("Map_centre");
		if (gameObject2 != null)
		{
			TooltipTextScript[] componentsInChildren2 = gameObject2.GetComponentsInChildren<TooltipTextScript>();
			foreach (TooltipTextScript tooltipTextScript2 in componentsInChildren2)
			{
				tooltipTextScript2.setInactive();
			}
		}
		GameObject gameObject3 = GameObject.Find("Panel_BottomMenu");
		if (gameObject3 != null)
		{
			gameObject3.GetComponent<BottomMenuController>().clearTooltips();
		}
	}

	public void tweenAlpha(TweenAlpha aTween, float aStartAlpha, float aEndAlpha, float aDuration, GameObject aTarget, string aCompletionHandler)
	{
		aTween.duration = aDuration;
		aTween.from = aStartAlpha;
		aTween.to = aEndAlpha;
		aTween.eventReceiver = aTarget;
		aTween.callWhenFinished = aCompletionHandler;
		aTween.ResetToBeginning();
		aTween.PlayForward();
	}

	public void tweenColor(TweenColor aTween, Color aStartColor, Color aEndColor, float aDuration, GameObject aTarget, string aCompletionHandler)
	{
		aTween.duration = aDuration;
		aTween.from = new Color(aStartColor.r, aStartColor.g, aStartColor.b, aStartColor.a);
		aTween.to = new Color(aEndColor.r, aEndColor.g, aEndColor.b, aEndColor.a);
		aTween.eventReceiver = aTarget;
		aTween.callWhenFinished = aCompletionHandler;
		aTween.ResetToBeginning();
		aTween.PlayForward();
	}

	public void tweenColorAlpha(TweenColor aTween, float aStartAlpha, float aEndAlpha, float aDuration, GameObject aTarget, string aCompletionHandler)
	{
		aTween.duration = aDuration;
		Color value = aTween.value;
		aTween.from = new Color(value.r, value.g, value.b, aStartAlpha);
		aTween.to = new Color(value.r, value.g, value.b, aEndAlpha);
		aTween.eventReceiver = aTarget;
		aTween.callWhenFinished = aCompletionHandler;
		aTween.ResetToBeginning();
		aTween.PlayForward();
	}

	public void tweenPosition(TweenPosition aTween, Vector3 aStartPosition, Vector3 aEndPosition, float aDuration, GameObject aTarget, string aCompletionHandler, bool isPlayForwards = true)
	{
		aTween.duration = aDuration;
		aTween.from = aStartPosition;
		aTween.to = aEndPosition;
		aTween.eventReceiver = aTarget;
		aTween.callWhenFinished = aCompletionHandler;
		if (isPlayForwards)
		{
			if (aTween.direction == Direction.Reverse)
			{
				aTween.Toggle();
				return;
			}
			aTween.ResetToBeginning();
			aTween.PlayForward();
		}
		else if (aTween.direction == Direction.Forward)
		{
			aTween.Toggle();
		}
		else
		{
			aTween.ResetToBeginning();
			aTween.PlayReverse();
		}
	}

	public void tweenScale(TweenScale aTween, Vector3 aStartScale, Vector3 aEndScale, float aDuration, GameObject aTarget, string aCompletionHandler, bool isPlayForwards = true)
	{
		aTween.duration = aDuration;
		aTween.from = aStartScale;
		aTween.to = aEndScale;
		aTween.eventReceiver = aTarget;
		aTween.callWhenFinished = aCompletionHandler;
		if (isPlayForwards)
		{
			if (aTween.direction == Direction.Reverse)
			{
				aTween.Toggle();
				return;
			}
			aTween.ResetToBeginning();
			aTween.PlayForward();
		}
		else if (aTween.direction == Direction.Forward)
		{
			aTween.Toggle();
		}
		else
		{
			aTween.ResetToBeginning();
			aTween.PlayReverse();
		}
	}

	public void tweenRotation(TweenRotation aTween, Vector3 aStartRotation, Vector3 aEndRotation, float aDuration, GameObject aTarget, string aCompletionHandler)
	{
		aTween.duration = aDuration;
		aTween.from = aStartRotation;
		aTween.to = aEndRotation;
		aTween.eventReceiver = aTarget;
		aTween.callWhenFinished = aCompletionHandler;
		aTween.ResetToBeginning();
		aTween.PlayForward();
	}

	public void playAnimation(BoneAnimation aAnimation, string aClipName)
	{
		aAnimation.CrossFade(aClipName);
	}

	public void playAnimationImmediate(BoneAnimation aAnimation, string aClipName)
	{
		aAnimation.Play(aClipName);
	}

	public void queueAnimation(BoneAnimation aAnimation, string aClipName)
	{
		aAnimation.CrossFadeQueued(aClipName);
	}

	public void stopAnimation(BoneAnimation aAnimation)
	{
		aAnimation.Stop();
	}

	public void hideAnimation(BoneAnimation aAnimation)
	{
		aAnimation.updateColors = true;
		AnimationBone[] mBoneSource = aAnimation.mBoneSource;
		foreach (AnimationBone animationBone in mBoneSource)
		{
			aAnimation.HideBone(animationBone.boneName, hide: true);
		}
	}

	public void showAnimation(BoneAnimation aAnimation)
	{
		aAnimation.updateColors = true;
		AnimationBone[] mBoneSource = aAnimation.mBoneSource;
		foreach (AnimationBone animationBone in mBoneSource)
		{
			aAnimation.HideBone(animationBone.boneName, hide: false);
		}
	}

	public void flashAnimation(BoneAnimation aAnimation, Color aStartColor, Color aEndColor, float aDuration, int aFlashCount, bool aResetColor)
	{
		aAnimation.updateColors = true;
		AnimationBone[] mBoneSource = aAnimation.mBoneSource;
		foreach (AnimationBone animationBone in mBoneSource)
		{
			aAnimation.FlashBoneColor(animationBone.boneName, aStartColor, 1f, aEndColor, 1f, aDuration, aFlashCount, aResetColor);
		}
	}
}
