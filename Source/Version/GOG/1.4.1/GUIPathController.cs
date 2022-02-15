using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIPathController : MonoBehaviour
{
	private Game game;

	private GUIGridController gridController;

	private GUIObstacleController obstacleController;

	private CommonScreenObject commonScreenObject;

	private AudioController audioController;

	private StationController stationController;

	private ShopLevel shopLevel;

	private GameObject converter;

	private Smith smithInfo;

	private SmithStation prevStation;

	private int prevStationIndex;

	private SmithStation currStation;

	private CharacterPath currentPath;

	private List<Vector2> queuePath;

	private Vector2 stationPoint;

	private Vector2 doorCoor;

	private Vector3 targetPos;

	private int currentIndex;

	private float xDegree;

	private float yDegree;

	private string actionRefID;

	private bool faceBack;

	private bool isMoving;

	private bool isMovingOut;

	private float spdMultiplier;

	private GameObject spriteAnimator;

	private GameObject shadow;

	private GameObject beam;

	private string animationState;

	private bool isPopping;

	private TextMesh text;

	private Vector3 startPop;

	private Vector3 endPop;

	private Color currTextColor;

	private Color transparentColor;

	private TweenPosition bubblePositionTween;

	private TweenScale bubbleScaleTween;

	private GameObject bubbleObj;

	private TweenPosition speechBubblePositionTween;

	private TweenScale speechBubbleScaleTween;

	private GameObject speechBubbleObj;

	private TextMesh bubbleObjText;

	private GameObject floater;

	private int currentPopPriority;

	private Vector3 beamStart;

	private Vector3 beamEnd;

	private Vector3 beamOriginPos;

	private Vector3 beamNoScale;

	private Vector3 beamStartScale;

	private Vector3 beamFullScale;

	private bool researchWait;

	private bool researching;

	private string lastPopString;

	private string lastAction;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gridController = GameObject.Find("GUIGridController").GetComponent<GUIGridController>();
		obstacleController = GameObject.Find("GUIObstacleController").GetComponent<GUIObstacleController>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		stationController = GameObject.Find("StationController").GetComponent<StationController>();
		shopLevel = game.getPlayer().getShopLevel();
		converter = GameObject.Find("Panel_Converter");
		prevStationIndex = -1;
		currentIndex = 0;
		actionRefID = string.Empty;
		faceBack = false;
		isMoving = false;
		isMovingOut = false;
		spdMultiplier = 1f;
		spriteAnimator = GetComponentInChildren<Animator>().gameObject;
		shadow = commonScreenObject.findChild(base.gameObject, "shadow").gameObject;
		beam = commonScreenObject.findChild(base.gameObject, "beam").gameObject;
		animationState = string.Empty;
		isPopping = false;
		text = commonScreenObject.findChild(spriteAnimator.gameObject, "text").GetComponent<TextMesh>();
		startPop = new Vector3(0f, 0.25f, 0f);
		endPop = new Vector3(0f, 0.75f, 0f);
		transparentColor = Color.white;
		transparentColor.a = 0f;
		text.GetComponent<Renderer>().sortingLayerName = "Character";
		text.GetComponent<Renderer>().sortingOrder = 3;
		bubbleObj = commonScreenObject.findChild(spriteAnimator.gameObject, "bubble").gameObject;
		bubblePositionTween = bubbleObj.GetComponent<TweenPosition>();
		bubbleScaleTween = bubbleObj.GetComponent<TweenScale>();
		speechBubbleObj = commonScreenObject.findChild(spriteAnimator.gameObject, "speechBubble").gameObject;
		speechBubblePositionTween = speechBubbleObj.GetComponent<TweenPosition>();
		speechBubbleScaleTween = speechBubbleObj.GetComponent<TweenScale>();
		bubbleObjText = speechBubbleObj.GetComponentInChildren<TextMesh>();
		floater = commonScreenObject.findChild(spriteAnimator.gameObject, "floater").gameObject;
		currentPopPriority = 99;
		researchWait = false;
		researching = false;
		lastPopString = string.Empty;
		lastAction = string.Empty;
		beamOriginPos = new Vector3(0.03f, 6.35f, 0f);
		beamNoScale = new Vector3(0f, 1.5f, 1f);
		beamStartScale = new Vector3(0.1f, 1.5f, 1f);
		beamFullScale = new Vector3(1f, 1.5f, 1f);
	}

	private void Update()
	{
		if (isMoving && !isMovingOut)
		{
			float maxDistanceDelta = 4f * Time.deltaTime * spdMultiplier;
			base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, targetPos, maxDistanceDelta);
		}
	}

	private void setParameters(bool last = false)
	{
		if (last)
		{
			targetPos = gridController.getPosition((int)queuePath[queuePath.Count - 1].x, (int)queuePath[queuePath.Count - 1].y);
			targetPos.y = currentPath.getYValue();
			currentIndex = queuePath.Count - 1;
		}
		else
		{
			targetPos = gridController.getPosition((int)queuePath[currentIndex].x, (int)queuePath[currentIndex].y);
			targetPos.y = currentPath.getYValue();
		}
		isMoving = true;
		Vector2 coordinates = gridController.getCoordinates(base.transform.localPosition);
		Vector2 vector = queuePath[currentIndex];
		float num = coordinates.x - vector.x;
		float num2 = coordinates.y - vector.y;
		xDegree = 35.264f;
		yDegree = 45f;
		if (num2 < 0f || num < 0f)
		{
			faceBack = true;
			base.gameObject.GetComponentInChildren<Animator>().SetBool("faceBack", faceBack);
		}
		else if (num2 > 0f || num > 0f)
		{
			faceBack = false;
			base.gameObject.GetComponentInChildren<Animator>().SetBool("faceBack", faceBack);
		}
		if ((!faceBack && (num < 0f || num2 > 0f)) || (faceBack && (num > 0f || num2 < 0f)))
		{
			xDegree *= -1f;
			yDegree += 180f;
		}
		base.transform.localRotation = Quaternion.Euler(xDegree, yDegree, 0f);
		if (yDegree > 45f)
		{
			text.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			if (bubbleObj.transform.childCount > 0)
			{
				bubbleObj.transform.GetChild(0).localRotation = Quaternion.Euler(0f, 180f, 0f);
			}
			floater.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			speechBubbleObj.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
		}
		else
		{
			text.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			if (bubbleObj.transform.childCount > 0)
			{
				bubbleObj.transform.GetChild(0).localRotation = Quaternion.Euler(0f, 0f, 0f);
			}
			floater.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			speechBubbleObj.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		}
	}

	private IEnumerator startMoving()
	{
		while (isMoving)
		{
			yield return new WaitForSeconds(0.05f);
			if (Vector3.Distance(base.transform.localPosition, targetPos) == 0f)
			{
				currentIndex++;
				if (queuePath.Count >= currentIndex + 1)
				{
					setParameters();
					continue;
				}
				isMoving = false;
				reachDestination();
			}
		}
	}

	private IEnumerator goToHeaven()
	{
		base.transform.localRotation = Quaternion.Euler(currentPath.getXDegree(), currentPath.getYDegree(), 0f);
		faceBack = currentPath.getFaceBack();
		if (currentPath.getYDegree() > 45f)
		{
			text.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			if (bubbleObj.transform.childCount > 0)
			{
				bubbleObj.transform.GetChild(0).localRotation = Quaternion.Euler(0f, 180f, 0f);
			}
			floater.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			speechBubbleObj.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
		}
		else
		{
			text.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			if (bubbleObj.transform.childCount > 0)
			{
				bubbleObj.transform.GetChild(0).localRotation = Quaternion.Euler(0f, 0f, 0f);
			}
			floater.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			speechBubbleObj.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		}
		isMovingOut = true;
		beam.transform.SetParent(converter.transform);
		Vector3 worldPos = beam.transform.localPosition;
		worldPos.z -= 0.2f;
		beam.transform.localPosition = worldPos;
		beam.transform.SetParent(base.transform);
		beamStart = beam.transform.localPosition;
		beamEnd = beam.transform.localPosition;
		beamStart.y += 30f;
		audioController.playBeamInAudio();
		commonScreenObject.tweenPosition(beam.GetComponent<TweenPosition>(), beamStart, beamEnd, 0.5f, null, string.Empty);
		beam.transform.localScale = new Vector3(0.1f, 1f, 1f);
		yield return new WaitForSeconds(0.55f);
		commonScreenObject.tweenScale(beam.GetComponent<TweenScale>(), beamStartScale, beamFullScale, 0.5f, null, string.Empty);
		yield return new WaitForSeconds(0.5f);
		setAnimationState("stand", aValue: false);
		base.transform.localRotation = Quaternion.Euler(currentPath.getXDegree(), currentPath.getYDegree(), 0f);
		faceBack = currentPath.getFaceBack();
		if (currentPath.getYDegree() > 45f)
		{
			text.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			if (bubbleObj.transform.childCount > 0)
			{
				bubbleObj.transform.GetChild(0).localRotation = Quaternion.Euler(0f, 180f, 0f);
			}
			floater.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			speechBubbleObj.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
		}
		else
		{
			text.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			if (bubbleObj.transform.childCount > 0)
			{
				bubbleObj.transform.GetChild(0).localRotation = Quaternion.Euler(0f, 0f, 0f);
			}
			floater.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			speechBubbleObj.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		}
		commonScreenObject.tweenScale(shadow.GetComponent<TweenScale>(), shadow.transform.localScale, Vector3.zero, 1f, null, string.Empty);
		Vector3 targetUp = spriteAnimator.transform.localPosition;
		targetUp.y += 50f;
		commonScreenObject.tweenPosition(spriteAnimator.GetComponent<TweenPosition>(), spriteAnimator.transform.localPosition, targetUp, 1f, base.gameObject, "scaleBeamUp");
	}

	public void scaleBeamUp()
	{
		stationController.disableTypeIndicator(smithInfo.getCurrentStation(), smithInfo.getCurrentStationIndex());
		StartCoroutine("scaleBeamUpRoutine");
	}

	private IEnumerator scaleBeamUpRoutine()
	{
		commonScreenObject.tweenScale(beam.GetComponent<TweenScale>(), beamStartScale, beamFullScale, 0.5f, null, string.Empty, isPlayForwards: false);
		yield return new WaitForSeconds(0.55f);
		audioController.playBeamOutAudio();
		commonScreenObject.tweenPosition(beam.GetComponent<TweenPosition>(), beamEnd, beamStart, 0.5f, base.gameObject, "stopFire");
	}

	private IEnumerator downFrmHeaven()
	{
		stationController.enableTypeIndicator(smithInfo);
		setAnimationState("stand", aValue: true);
		Vector3 upPos = spriteAnimator.transform.localPosition;
		upPos.y += 50f;
		spriteAnimator.transform.localPosition = upPos;
		shadow.transform.localScale = Vector3.zero;
		base.transform.localRotation = Quaternion.Euler(currentPath.getXDegree(), currentPath.getYDegree(), 0f);
		faceBack = currentPath.getFaceBack();
		if (currentPath.getYDegree() > 45f)
		{
			text.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			if (bubbleObj.transform.childCount > 0)
			{
				bubbleObj.transform.GetChild(0).localRotation = Quaternion.Euler(0f, 180f, 0f);
			}
			floater.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			speechBubbleObj.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
		}
		else
		{
			text.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			if (bubbleObj.transform.childCount > 0)
			{
				bubbleObj.transform.GetChild(0).localRotation = Quaternion.Euler(0f, 0f, 0f);
			}
			floater.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			speechBubbleObj.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		}
		isMovingOut = true;
		beam.transform.SetParent(converter.transform);
		Vector3 worldPos = beam.transform.localPosition;
		worldPos.z -= 0.2f;
		beam.transform.localPosition = worldPos;
		beam.transform.SetParent(base.transform);
		beamStart = beam.transform.localPosition;
		beamEnd = beam.transform.localPosition;
		beamStart.y += 30f;
		audioController.playBeamInAudio();
		commonScreenObject.tweenPosition(beam.GetComponent<TweenPosition>(), beamStart, beamEnd, 0.5f, null, string.Empty);
		yield return new WaitForSeconds(0.55f);
		commonScreenObject.tweenScale(beam.GetComponent<TweenScale>(), beamStartScale, beamFullScale, 0.5f, null, string.Empty);
		yield return new WaitForSeconds(0.5f);
		commonScreenObject.tweenScale(shadow.GetComponent<TweenScale>(), Vector3.zero, new Vector3(0.55f, 0.555f, 0f), 1f, null, string.Empty);
		Vector3 targetDown = spriteAnimator.transform.localPosition;
		targetDown.y -= 50f;
		commonScreenObject.tweenPosition(spriteAnimator.GetComponent<TweenPosition>(), upPos, targetDown, 1f, base.gameObject, "scaleBeamUpDescend");
	}

	public void scaleBeamUpDescend()
	{
		StartCoroutine("resetBeamPos");
	}

	private IEnumerator resetBeamPos()
	{
		commonScreenObject.tweenScale(beam.GetComponent<TweenScale>(), beamStartScale, beamFullScale, 0.5f, null, string.Empty, isPlayForwards: false);
		yield return new WaitForSeconds(0.55f);
		audioController.playBeamOutAudio();
		commonScreenObject.tweenPosition(beam.GetComponent<TweenPosition>(), beamEnd, beamStart, 0.5f, base.gameObject, "checkNextState");
		yield return new WaitForSeconds(0.75f);
		beam.transform.localPosition = beamOriginPos;
		beam.transform.localScale = beamNoScale;
		isMovingOut = false;
		setAnimationState("stand", aValue: false);
		checkNextState();
	}

	private void reachDestination()
	{
		audioController.stopSmithFootstepAudio(smithInfo.getSmithRefId());
		StopCoroutine("startMoving");
		isMoving = false;
		base.transform.localRotation = Quaternion.Euler(currentPath.getXDegree(), currentPath.getYDegree(), 0f);
		faceBack = currentPath.getFaceBack();
		if (currentPath.getYDegree() > 45f)
		{
			text.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			if (bubbleObj.transform.childCount > 0)
			{
				bubbleObj.transform.GetChild(0).localRotation = Quaternion.Euler(0f, 180f, 0f);
			}
			floater.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			speechBubbleObj.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
		}
		else
		{
			text.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			if (bubbleObj.transform.childCount > 0)
			{
				bubbleObj.transform.GetChild(0).localRotation = Quaternion.Euler(0f, 0f, 0f);
			}
			floater.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			speechBubbleObj.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		}
		base.gameObject.GetComponentInChildren<Animator>().SetBool("faceBack", currentPath.getFaceBack());
		switch (actionRefID)
		{
		case "901":
		case "902":
		case "903":
		case "906":
		case "907":
		case "905":
		case "FIRED":
			StartCoroutine("goToHeaven");
			break;
		case "101":
			setAnimationState("stand", aValue: true);
			break;
		case "102":
			setAnimationState("isWorking", aValue: true);
			playStationAnimation();
			break;
		case "103":
			checkStationAnimation();
			stationController.disableTypeIndicator(prevStation, prevStationIndex);
			GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>().deleteCharacter(smithInfo.getSmithRefId());
			audioController.playSmithExitAudio();
			stationController.removeSmith(smithInfo);
			break;
		case "105":
		case "201":
			setAnimationState("daydream", aValue: true);
			break;
		case "202":
			audioController.playSmithCoffeeAudio();
			setAnimationState("coffee", aValue: true);
			break;
		case "203":
			audioController.playSmithChiAudio();
			setAnimationState("chi", aValue: true);
			if (game.getPlayer().getCurrentProject().getProjectId() != string.Empty)
			{
				playStationAnimation();
			}
			break;
		case "204":
			setAnimationState("sad", aValue: true);
			break;
		case "301":
		case "302":
		case "303":
			audioController.playSmithSickAudio(smithInfo.getSmithRefId());
			setAnimationState("sad", aValue: true);
			break;
		case "304":
			audioController.playSmithFrozenAudio(smithInfo.getSmithRefId());
			setAnimationState("shiver", aValue: true);
			break;
		case "904":
			if (researchWait)
			{
				stopFire();
				break;
			}
			checkStationAnimation();
			stationController.disableTypeIndicator(prevStation, prevStationIndex);
			stationController.removeSmith(smithInfo);
			StartCoroutine("animateBookshelf");
			break;
		default:
			CommonAPI.debug("default reach");
			setAnimationState("isWorking", aValue: true);
			break;
		}
	}

	private IEnumerator animateBookshelf()
	{
		GameData gameData = game.getGameData();
		int shopLevelInt = game.getPlayer().getShopLevelInt();
		base.transform.localRotation = Quaternion.Euler(currentPath.getXDegree(), currentPath.getYDegree(), 0f);
		faceBack = currentPath.getFaceBack();
		if (currentPath.getYDegree() > 45f)
		{
			text.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			if (bubbleObj.transform.childCount > 0)
			{
				bubbleObj.transform.GetChild(0).localRotation = Quaternion.Euler(0f, 180f, 0f);
			}
			floater.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			speechBubbleObj.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
		}
		else
		{
			text.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			if (bubbleObj.transform.childCount > 0)
			{
				bubbleObj.transform.GetChild(0).localRotation = Quaternion.Euler(0f, 0f, 0f);
			}
			floater.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			speechBubbleObj.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		}
		researchWait = true;
		isMovingOut = true;
		setAnimationState("stand", aValue: true);
		string researchObstacleRefID = string.Empty;
		switch (shopLevelInt)
		{
		case 1:
			researchObstacleRefID = "10010";
			break;
		case 2:
			researchObstacleRefID = "20013";
			break;
		case 3:
			researchObstacleRefID = "30024";
			break;
		case 4:
			researchObstacleRefID = "40025";
			break;
		}
		CommonAPI.debug("researchObstacleRefID: " + researchObstacleRefID);
		GameObject.Find("GUIObstacleController").GetComponent<GUIObstacleController>().playObstacleAnim(researchObstacleRefID, spriteNull: true, "Down");
		yield return new WaitForSeconds(4.5f);
		currentPath = gameData.getPathByRefIDStartEndShopLevel("-1", shopLevel.getResearchCoor(), shopLevel.getPortalCoor(), shopLevelInt);
		queuePath = currentPath.getPathList();
		currentIndex = 1;
		Vector3 newPos = base.transform.localPosition;
		newPos.y = currentPath.getYValue();
		base.transform.localPosition = newPos;
		setParameters();
		isMoving = true;
		isMovingOut = false;
		setAnimationState("stand", aValue: false);
		StartCoroutine("startMoving");
	}

	public void stopAction(string type)
	{
		base.transform.localRotation = Quaternion.Euler(currentPath.getXDegree(), currentPath.getYDegree(), 0f);
		if (currentPath.getYDegree() > 45f)
		{
			text.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			if (bubbleObj.transform.childCount > 0)
			{
				bubbleObj.transform.GetChild(0).localRotation = Quaternion.Euler(0f, 180f, 0f);
			}
			floater.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			speechBubbleObj.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
		}
		else
		{
			text.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			if (bubbleObj.transform.childCount > 0)
			{
				bubbleObj.transform.GetChild(0).localRotation = Quaternion.Euler(0f, 0f, 0f);
			}
			floater.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			speechBubbleObj.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		}
		setAnimationState(type, aValue: false);
		switch (type)
		{
		case "shiver":
			audioController.stopSmithFrozenAudio(smithInfo.getSmithRefId());
			break;
		case "sad":
			audioController.stopSmithSickAudio(smithInfo.getSmithRefId());
			break;
		}
		checkNextState();
	}

	public void stopFire()
	{
		if (smithInfo.checkSmithInShop())
		{
			isMovingOut = true;
			GetComponentInChildren<CapsuleCollider>().enabled = true;
			Vector3 localPosition = spriteAnimator.transform.localPosition;
			localPosition.y -= 50f;
			spriteAnimator.transform.localPosition = localPosition;
			beam.transform.localPosition = new Vector3(0f, 6.35f, 0f);
			beam.transform.localScale = new Vector3(0f, 1.5f, 1f);
			StartCoroutine("downFrmHeaven");
		}
		else
		{
			GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>().deleteCharacter(smithInfo.getSmithRefId(), check: false);
		}
	}

	public void unassignSmith()
	{
		checkStationAnimation();
		stationController.disableTypeIndicator(prevStation, prevStationIndex);
		stationController.removeSmith(smithInfo);
		GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>().checkProjectStatus(hasTimePass: false, checkDog: false);
	}

	public void checkState(Smith aSmith, bool frmHeaven = false)
	{
		Player player = game.getPlayer();
		smithInfo = aSmith;
		int indexOfSmith = game.getPlayer().getIndexOfSmith(smithInfo);
		stationPoint = stationController.getSmithStation(aSmith);
		Vector3 position = gridController.getPosition((int)stationPoint.x, (int)stationPoint.y);
		base.transform.localPosition = position;
		int shopLevelInt = player.getShopLevelInt();
		currentPath = game.getGameData().getPathByRefIDStartEndShopLevel("-1", stationPoint, stationPoint, shopLevelInt);
		queuePath = currentPath.getPathList();
		Vector3 localPosition = base.transform.localPosition;
		localPosition.y = currentPath.getYValue();
		base.transform.localPosition = localPosition;
		prevStation = smithInfo.getCurrentStation();
		prevStationIndex = stationController.getSmithStationIndex(smithInfo);
		setParameters(last: true);
		if (frmHeaven)
		{
			isMovingOut = true;
			StartCoroutine("downFrmHeaven");
		}
		else
		{
			checkNextState();
		}
	}

	private void removeSmithFromShop()
	{
		GetComponentInChildren<CapsuleCollider>().enabled = false;
		setAnimationState("stand", aValue: true);
		StartCoroutine("goToHeaven");
		stationController.removeSmith(smithInfo);
	}

	public void checkNextState()
	{
		if (!isMovingOut)
		{
			GameData gameData = game.getGameData();
			bool flag = false;
			actionRefID = smithInfo.getSmithAction().getRefId();
			CharacterPath characterPath = null;
			int shopLevelInt = game.getPlayer().getShopLevelInt();
			Vector2 coordinates = gridController.getCoordinates(base.transform.localPosition);
			bool flag2 = false;
			switch (actionRefID)
			{
			case "901":
				doBubblePop("EXPLORE", string.Empty);
				flag2 = true;
				stationController.disableTypeIndicator(smithInfo.getCurrentStation(), smithInfo.getCurrentStationIndex());
				removeSmithFromShop();
				break;
			case "902":
				doBubblePop("BUY", string.Empty);
				flag2 = true;
				stationController.disableTypeIndicator(smithInfo.getCurrentStation(), smithInfo.getCurrentStationIndex());
				removeSmithFromShop();
				break;
			case "903":
				doBubblePop("SELL", string.Empty);
				flag2 = true;
				stationController.disableTypeIndicator(smithInfo.getCurrentStation(), smithInfo.getCurrentStationIndex());
				removeSmithFromShop();
				break;
			case "906":
				doBubblePop("VACATION", string.Empty);
				flag2 = true;
				stationController.disableTypeIndicator(smithInfo.getCurrentStation(), smithInfo.getCurrentStationIndex());
				removeSmithFromShop();
				break;
			case "907":
				doBubblePop("TRAINING", string.Empty);
				flag2 = true;
				stationController.disableTypeIndicator(smithInfo.getCurrentStation(), smithInfo.getCurrentStationIndex());
				removeSmithFromShop();
				break;
			case "905":
				doBubblePop("STANDBY", string.Empty);
				flag2 = true;
				stationController.disableTypeIndicator(smithInfo.getCurrentStation(), smithInfo.getCurrentStationIndex());
				removeSmithFromShop();
				break;
			case "FIRED":
				flag2 = true;
				removeSmithFromShop();
				break;
			case "103":
				stationController.disableTypeIndicator(prevStation, prevStationIndex);
				GameObject.Find("GUICharacterAnimationController").GetComponent<GUICharacterAnimationController>().deleteCharacter(smithInfo.getSmithRefId());
				audioController.playSmithExitAudio();
				flag2 = true;
				flag = true;
				break;
			case "202":
			{
				Vector2 coffeeCoor = shopLevel.getCoffeeCoor();
				if (coordinates == coffeeCoor)
				{
					reachDestination();
					flag2 = true;
				}
				else
				{
					characterPath = gameData.getPathByRefIDStartEndShopLevel(smithInfo.getSmithAction().getRefId(), coordinates, coffeeCoor, shopLevelInt);
				}
				break;
			}
			case "904":
				researching = true;
				doBubblePop("RESEARCH", string.Empty);
				characterPath = ((Vector3.Distance(coordinates, shopLevel.getCoffeeCoor()) != 0f) ? gameData.getPathByRefIDStartEndShopLevel("-1", coordinates, shopLevel.getResearchCoor(), shopLevelInt) : gameData.getPathByRefIDStartEndShopLevel("202", coordinates, shopLevel.getResearchCoor(), shopLevelInt));
				GetComponentInChildren<CapsuleCollider>().enabled = false;
				break;
			default:
				stationPoint = stationController.getSmithStation(smithInfo);
				if (Vector2.Distance(coordinates, stationPoint) == 0f)
				{
					reachDestination();
					flag2 = true;
				}
				else if (stationPoint.x != -1f || stationPoint.y != -1f)
				{
					characterPath = ((!(coordinates == shopLevel.getCoffeeCoor())) ? gameData.getPathByRefIDStartEndShopLevel("-1", coordinates, stationPoint, shopLevelInt) : gameData.getPathByRefIDStartEndShopLevel("202", shopLevel.getCoffeeCoor(), stationPoint, shopLevelInt));
				}
				else
				{
					CommonAPI.debug("no station reached");
					reachDestination();
					flag2 = true;
				}
				break;
			}
			if (!flag)
			{
				checkStationAnimation();
			}
			if (!flag2)
			{
				currentIndex = 0;
				currentPath = characterPath;
				queuePath = currentPath.getPathList();
				Vector3 localPosition = base.transform.localPosition;
				localPosition.y = currentPath.getYValue();
				base.transform.localPosition = localPosition;
				setParameters();
				int speed = 1;
				if (spdMultiplier == 1.43f)
				{
					speed = 2;
				}
				else if (spdMultiplier == 3.33f)
				{
					speed = 3;
				}
				audioController.playSmithFootstepAudio(smithInfo.getSmithRefId(), speed);
				StartCoroutine("startMoving");
			}
		}
		else
		{
			setAnimationState("stand", aValue: true);
		}
	}

	private void startFromDoor()
	{
		Player player = game.getPlayer();
		int indexOfSmith = player.getIndexOfSmith(smithInfo);
		stationPoint = stationController.getSmithStation(smithInfo);
		doorCoor = player.getShopLevel().getStartingCoor();
		Vector3 position = gridController.getPosition((int)doorCoor.x, (int)doorCoor.y);
		base.transform.localPosition = position;
	}

	public void pauseCharacter()
	{
		if (researching)
		{
			return;
		}
		isMoving = false;
		StopCoroutine("startMoving");
		switch (actionRefID)
		{
		case "301":
		case "302":
		case "303":
			audioController.stopSmithSickAudio(smithInfo.getSmithRefId());
			break;
		case "304":
			audioController.stopSmithFrozenAudio(smithInfo.getSmithRefId());
			break;
		}
		base.gameObject.GetComponentInChildren<Animator>().speed = 0f;
		List<string> obstacleRefID = stationController.getObstacleRefID(currStation);
		foreach (string item in obstacleRefID)
		{
			obstacleController.pauseObstacleAnim(item);
		}
		audioController.pauseSmithFootstepAudio(smithInfo.getSmithRefId());
	}

	public void resumeCharacter()
	{
		Player player = game.getPlayer();
		base.gameObject.GetComponentInChildren<Animator>().speed = 1f;
		if (player.getCurrentProject().getProjectId() != string.Empty)
		{
			List<string> obstacleRefID = stationController.getObstacleRefID(currStation);
			foreach (string item in obstacleRefID)
			{
				obstacleController.resumeObstacleAnim(item);
			}
		}
		if (Vector3.Distance(base.transform.localPosition, targetPos) == 0f && queuePath.Count > currentIndex + 1)
		{
			audioController.resumeSmithFootstepAudio(smithInfo.getSmithRefId());
			isMoving = true;
			StartCoroutine("startMoving");
		}
		else if (Vector3.Distance(base.transform.localPosition, targetPos) > 0f)
		{
			audioController.resumeSmithFootstepAudio(smithInfo.getSmithRefId());
			isMoving = true;
			StartCoroutine("startMoving");
		}
		else if (!isMovingOut)
		{
			reachDestination();
		}
	}

	private void flip()
	{
		Vector3 localScale = base.transform.localScale;
		localScale.x *= -1f;
		base.transform.localScale = localScale;
	}

	public void refreshStationPoint()
	{
		Player player = game.getPlayer();
		shopLevel = player.getShopLevel();
		int indexOfSmith = player.getIndexOfSmith(smithInfo);
		stationPoint = stationController.getSmithStation(smithInfo);
		Vector3 position = gridController.getPosition((int)stationPoint.x, (int)stationPoint.y);
		position.y = currentPath.getYValue();
		base.transform.localPosition = position;
		targetPos = position;
		currentIndex = queuePath.Count - 1;
		checkNextState();
	}

	public void playStationAnimation()
	{
		checkStationAnimation();
		List<string> obstacleRefID = stationController.getObstacleRefID(currStation);
		foreach (string item in obstacleRefID)
		{
			obstacleController.playObstacleAnim(item, spriteNull: false, string.Empty);
		}
	}

	public void checkStationAnimation()
	{
		Player player = game.getPlayer();
		if (prevStationIndex != -1)
		{
			if (stationController.getStationBySmithStation(prevStation).checkEmptySmithList())
			{
				stationController.disableTypeIndicator(prevStation, prevStationIndex);
				List<string> obstacleRefID = stationController.getObstacleRefID(currStation);
				foreach (string item in obstacleRefID)
				{
					obstacleController.stopObstacleAnim(item);
				}
			}
			if (player.getCurrentProject().getProjectId() == string.Empty)
			{
				List<string> obstacleRefID2 = stationController.getObstacleRefID(currStation);
				foreach (string item2 in obstacleRefID2)
				{
					obstacleController.stopObstacleAnim(item2);
				}
			}
		}
		currStation = smithInfo.getCurrentStation();
		prevStation = currStation;
		prevStationIndex = stationController.getSmithStationIndex(smithInfo);
		stationController.enableTypeIndicator(smithInfo);
		if (player.getStationSmithActiveArray(currStation).Count < 1)
		{
			List<string> obstacleRefID3 = stationController.getObstacleRefID(currStation);
			foreach (string item3 in obstacleRefID3)
			{
				obstacleController.stopObstacleAnim(item3);
			}
		}
		if (!(player.getCurrentProject().getProjectId() == string.Empty))
		{
			return;
		}
		List<string> obstacleRefID4 = stationController.getObstacleRefID(currStation);
		foreach (string item4 in obstacleRefID4)
		{
			obstacleController.stopObstacleAnim(item4);
		}
	}

	public void doBubblePop(string action, string popString = "")
	{
		if ((action == lastAction && popString == lastPopString) || (popString != string.Empty && popString == lastPopString))
		{
			return;
		}
		lastAction = action;
		lastPopString = popString;
		int bubblePriority = CommonAPI.getBubblePriority(action);
		if (bubblePriority >= currentPopPriority && (bubblePriority > currentPopPriority || bubblePriority >= 6))
		{
			return;
		}
		switch (action)
		{
		case "HIRE":
			hidePopBubbles();
			popBubbleText("HIRE");
			return;
		case "FIRE":
			hidePopBubbles();
			popBubbleText("HIRE");
			return;
		case "EXPLORE":
			hidePopBubbles();
			popBubbleText(game.getGameData().getRandomTextBySetRefId("randomExploreText"));
			break;
		case "BUY":
			hidePopBubbles();
			popBubbleText(game.getGameData().getRandomTextBySetRefId("randomBuyMatsText"));
			break;
		case "SELL":
			hidePopBubbles();
			popBubbleText(game.getGameData().getRandomTextBySetRefId("randomSellWeaponText"));
			break;
		case "TRAINING":
			hidePopBubbles();
			popBubbleText(game.getGameData().getRandomTextBySetRefId("randomTrainingText"));
			break;
		case "VACATION":
			hidePopBubbles();
			popBubbleText(game.getGameData().getRandomTextBySetRefId("randomVacationText"));
			break;
		case "RESEARCH":
			hidePopBubbles();
			popBubbleText(game.getGameData().getRandomTextBySetRefId("randomResearchText"));
			break;
		case "STANDBY":
			hidePopBubbles();
			popBubbleText(game.getGameData().getRandomTextBySetRefId("randomStandbyText"));
			break;
		case "START_FORGE":
			hidePopBubbles();
			popBubble("smith_craft");
			break;
		case "CHANGE_STATION":
			if (popString != string.Empty)
			{
				hidePopBubbles();
				popBubble(popString);
				break;
			}
			return;
		case "STATUS":
			if (popString != string.Empty)
			{
				hidePopBubbles();
				popBubble(popString);
				break;
			}
			return;
		case "LEVEL_UP":
			popLvlUp();
			break;
		case "MOOD_CHANGE":
			hidePopBubbles();
			popMoodBubble(popString);
			break;
		case "RANDOM":
			hidePopBubbles();
			popBubbleText(popString);
			break;
		}
		currentPopPriority = bubblePriority;
	}

	public void hidePopBubbles()
	{
		speechBubblePositionTween.enabled = false;
		speechBubbleScaleTween.ResetToBeginning();
		speechBubbleScaleTween.enabled = false;
		speechBubbleObj.transform.localScale = Vector3.zero;
		bubblePositionTween.enabled = false;
		bubbleScaleTween.ResetToBeginning();
		bubbleScaleTween.enabled = false;
		bubbleObj.transform.localScale = Vector3.zero;
		Transform transform = commonScreenObject.findChild(floater, "popLvlUp" + smithInfo.getSmithRefId());
		if (transform != null)
		{
			commonScreenObject.destroyPrefabImmediate(transform.gameObject);
		}
	}

	public void popLvlUp()
	{
		hidePopBubbles();
		audioController.playSmithThoughtBubbleAudio();
		GameObject aObj = commonScreenObject.createPrefab(floater, "popLvlUp" + smithInfo.getSmithRefId(), "Image/Process bubble/levelup/lvlupAnimSprite", new Vector3(0f, 0.9f, 0f), new Vector3(0.7f, 0.7f, 0.7f), Vector3.zero);
		commonScreenObject.destroyPrefabDelay(aObj, 3f);
	}

	public void resetCurrentPopPriority()
	{
		currentPopPriority = 99;
	}

	public void popBubbleText(string bubbleText)
	{
		if (bubbleText != string.Empty)
		{
			audioController.playSmithDialogueAudio();
			int num = 1;
			switch (Constants.LANGUAGE)
			{
			case LanguageType.kLanguageTypeJap:
				num = 3;
				break;
			case LanguageType.kLanguageTypeRussia:
				num = 2;
				break;
			}
			string text = "bubble_l";
			if (bubbleText.Length <= 12 / num)
			{
				text = "bubble_s";
			}
			else if (bubbleText.Length <= 20 / num)
			{
				text = "bubble_m";
			}
			speechBubbleObj.GetComponent<SpriteRenderer>().sprite = commonScreenObject.loadSprite("Image/Process bubble/" + text);
			bubbleObjText.text = bubbleText;
			bubbleObjText.GetComponent<Renderer>().sortingLayerName = "Character";
			bubbleObjText.GetComponent<Renderer>().sortingOrder = 2;
			commonScreenObject.tweenPosition(speechBubblePositionTween, new Vector3(0f, 0.5f, 0f), new Vector3(0f, 0.8f, 0f), 1.5f, base.gameObject, "resetCurrentPopPriority");
			commonScreenObject.tweenScale(speechBubbleScaleTween, Vector3.zero, new Vector3(1.2f, 1.2f, 1.2f), 1.5f, null, string.Empty);
		}
	}

	public void popMoodBubble(string icon)
	{
		if (icon != string.Empty && icon != "image")
		{
			audioController.playSmithThoughtBubbleAudio();
			if (commonScreenObject.findChild(base.gameObject, "bubble/bubble_icon" + smithInfo.getSmithRefId()) != null)
			{
				commonScreenObject.destroyPrefabImmediate(commonScreenObject.findChild(base.gameObject, "bubble/bubble_icon" + smithInfo.getSmithRefId()).gameObject);
			}
			GameObject gameObject = commonScreenObject.createPrefab(bubbleObj, "bubble_icon" + smithInfo.getSmithRefId(), "Image/Mood/" + icon + "Obj", new Vector3(0f, 0.14f, 0f), Vector3.one, Vector3.zero);
			commonScreenObject.tweenPosition(bubblePositionTween, new Vector3(0.5f, 0.5f, 0f), new Vector3(0.5f, 0.8f, 0f), 1.5f, base.gameObject, "resetCurrentPopPriority");
			commonScreenObject.tweenScale(bubbleScaleTween, Vector3.zero, new Vector3(1.2f, 1.2f, 1.2f), 1.5f, null, string.Empty);
			if (currentPath.getYDegree() > 45f)
			{
				gameObject.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			}
			else
			{
				gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			}
		}
	}

	public void popBubble(string icon)
	{
		if (icon != string.Empty && icon != "image")
		{
			audioController.playSmithThoughtBubbleAudio();
			if (commonScreenObject.findChild(base.gameObject, "bubble/bubble_icon" + smithInfo.getSmithRefId()) != null)
			{
				commonScreenObject.destroyPrefabImmediate(commonScreenObject.findChild(base.gameObject, "bubble/bubble_icon" + smithInfo.getSmithRefId()).gameObject);
			}
			GameObject gameObject = commonScreenObject.createPrefab(bubbleObj, "bubble_icon" + smithInfo.getSmithRefId(), "Image/Process bubble/" + icon + "Obj", new Vector3(0f, 0.14f, 0f), Vector3.one, Vector3.zero);
			commonScreenObject.tweenPosition(bubblePositionTween, new Vector3(0.5f, 0.5f, 0f), new Vector3(0.5f, 0.8f, 0f), 1.5f, base.gameObject, "resetCurrentPopPriority");
			commonScreenObject.tweenScale(bubbleScaleTween, Vector3.zero, new Vector3(1.2f, 1.2f, 1.2f), 1.5f, null, string.Empty);
			if (currentPath.getYDegree() > 45f)
			{
				gameObject.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			}
			else
			{
				gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			}
		}
	}

	public void popStat(int value, string type)
	{
		StopCoroutine("startPopping");
		Color white = Color.white;
		white.a = 0f;
		text.text = "+" + value + type;
		currTextColor = white;
		text.transform.localPosition = startPop;
		isPopping = true;
		StartCoroutine("startPopping");
	}

	private IEnumerator startPopping()
	{
		while (isPopping)
		{
			yield return new WaitForSeconds(0.025f);
			if (Vector3.Distance(text.transform.localPosition, endPop) <= 0.05f)
			{
				isPopping = false;
				audioController.playForgeGrowthAudio();
				yield return new WaitForSeconds(0.8f);
				text.color = transparentColor;
			}
			else
			{
				Vector3 localPosition = text.transform.localPosition;
				localPosition.y += 0.025f;
				currTextColor.a += 0.05f;
				text.color = currTextColor;
				text.transform.localPosition = localPosition;
			}
		}
	}

	public bool isResearching()
	{
		return researching;
	}

	public Smith getSmithInfo()
	{
		return smithInfo;
	}

	public void setActionRefID(string aActionRefID)
	{
		actionRefID = aActionRefID;
	}

	public bool getIsMoving()
	{
		return isMoving;
	}

	public GameObject getBubbleObj()
	{
		return bubbleObj;
	}

	public void cancelCurrentAnimation()
	{
		if (queuePath.Count <= currentIndex + 1 && Vector3.Distance(base.transform.localPosition, targetPos) == 0f && animationState != string.Empty && !researchWait && !researching)
		{
			GetComponentInChildren<Animator>().SetBool(animationState, value: false);
			audioController.stopSmithFrozenAudio(smithInfo.getSmithRefId());
			audioController.stopSmithSickAudio(smithInfo.getSmithRefId());
			GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>().checkProjectStatus(hasTimePass: false, checkDog: false);
			checkNextState();
			GetComponentInChildren<Animator>().StopPlayback();
		}
	}

	private void setAnimationState(string aState, bool aValue)
	{
		GetComponentInChildren<Animator>().SetBool(aState, aValue);
		if (aValue)
		{
			animationState = aState;
		}
		else
		{
			animationState = string.Empty;
		}
	}

	public void setSpdMultiplier(float aMult)
	{
		spdMultiplier = aMult;
		audioController.stopSmithFootstepAudio(smithInfo.getSmithRefId());
		if (isMoving && !isMovingOut)
		{
			int speed = 1;
			if (spdMultiplier == 1.43f)
			{
				speed = 2;
			}
			else if (spdMultiplier == 3.33f)
			{
				speed = 3;
			}
			audioController.playSmithFootstepAudio(smithInfo.getSmithRefId(), speed);
		}
	}
}
