using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIDogPathController : MonoBehaviour
{
	private Game game;

	private GUIGridController gridController;

	private GUIObstacleController obstacleController;

	private CommonScreenObject commonScreenObject;

	private AudioController audioController;

	private StationController stationController;

	private Smith smithInfo;

	private SmithStation currStation;

	private CharacterPath currentPath;

	private List<Vector2> queuePath;

	private Vector2 stationPoint;

	private Vector3 targetPos;

	private int currentIndex;

	private float xDegree;

	private float yDegree;

	private string actionRefID;

	private bool faceBack;

	private bool isMoving;

	private bool isBarking;

	private float spdMultiplier;

	private string dogPrefix;

	private string animationState;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		gridController = GameObject.Find("GUIGridController").GetComponent<GUIGridController>();
		obstacleController = GameObject.Find("GUIObstacleController").GetComponent<GUIObstacleController>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		stationController = GameObject.Find("StationController").GetComponent<StationController>();
		currentIndex = 0;
		actionRefID = string.Empty;
		faceBack = false;
		isMoving = false;
		isBarking = false;
		animationState = string.Empty;
		spdMultiplier = 1f;
		dogPrefix = "DOG";
	}

	private void Update()
	{
		if (isMoving)
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
					StartCoroutine("startMoving");
				}
				else
				{
					reachDestination();
				}
			}
		}
	}

	private void reachDestination()
	{
		audioController.stopSmithFootstepAudio(dogPrefix);
		StopCoroutine("startMoving");
		isMoving = false;
		base.transform.localRotation = Quaternion.Euler(currentPath.getXDegree(), currentPath.getYDegree(), 0f);
		faceBack = currentPath.getFaceBack();
		base.gameObject.GetComponentInChildren<Animator>().SetBool("faceBack", currentPath.getFaceBack());
		Vector2 coordinates = gridController.getCoordinates(base.transform.localPosition);
		if (Vector2.Distance(coordinates, stationController.getDogBedStationPoint()) == 0f)
		{
			SpriteRenderer[] componentsInChildren = GetComponentsInChildren<SpriteRenderer>();
			SpriteRenderer[] array = componentsInChildren;
			foreach (SpriteRenderer spriteRenderer in array)
			{
				spriteRenderer.enabled = false;
			}
			obstacleController.changeSprite(game.getPlayer().getDogBedRefID(), locked: true);
		}
		setAnimationState("stand", aValue: true);
	}

	public void stopAction(string type)
	{
		base.transform.localRotation = Quaternion.Euler(xDegree, yDegree, 0f);
		setAnimationState(type, aValue: false);
		if (type != null && type == "bark")
		{
			audioController.stopDogBarkAudio();
		}
		checkNextState();
	}

	public void refreshStationPoint()
	{
		Player player = game.getPlayer();
		currStation = player.getDogStation();
		stationPoint = stationController.getDogStationPoint(currStation);
		Vector3 position = gridController.getPosition((int)stationPoint.x, (int)stationPoint.y);
		position.y = currentPath.getYValue();
		base.transform.localPosition = position;
		targetPos = position;
	}

	public void checkState()
	{
		Player player = game.getPlayer();
		currStation = player.getDogStation();
		stationPoint = stationController.getDogStationPoint(currStation);
		Vector3 position = gridController.getPosition((int)stationPoint.x, (int)stationPoint.y);
		base.transform.localPosition = position;
		int shopLevelInt = player.getShopLevelInt();
		currentPath = game.getGameData().getPathByRefIDStartEndShopLevel("-1", stationPoint, stationPoint, shopLevelInt);
		queuePath = currentPath.getPathList();
		Vector3 localPosition = base.transform.localPosition;
		localPosition.y = currentPath.getYValue();
		base.transform.localPosition = localPosition;
		setAnimationState("stand", aValue: true);
		setParameters(last: true);
		checkNextState();
	}

	public void checkNextState()
	{
		Player player = game.getPlayer();
		if (currStation != player.getDogStation())
		{
			isMoving = true;
			setAnimationState("stand", aValue: false);
			GameData gameData = game.getGameData();
			int shopLevelInt = game.getPlayer().getShopLevelInt();
			Vector2 dogStationPoint = stationController.getDogStationPoint(player.getDogStation());
			Vector2 coordinates = gridController.getCoordinates(base.transform.localPosition);
			if (Vector2.Distance(coordinates, stationController.getDogBedStationPoint()) == 0f && stationPoint != dogStationPoint)
			{
				SpriteRenderer[] componentsInChildren = GetComponentsInChildren<SpriteRenderer>();
				SpriteRenderer[] array = componentsInChildren;
				foreach (SpriteRenderer spriteRenderer in array)
				{
					spriteRenderer.enabled = true;
				}
				obstacleController.changeSprite(game.getPlayer().getDogBedRefID(), locked: false);
			}
			CharacterPath pathByRefIDStartEndShopLevel = gameData.getPathByRefIDStartEndShopLevel("-1", stationPoint, dogStationPoint, shopLevelInt);
			currStation = player.getDogStation();
			stationPoint = dogStationPoint;
			currentPath = pathByRefIDStartEndShopLevel;
			queuePath = currentPath.getPathList();
			currentIndex = 0;
			Vector3 localPosition = base.transform.localPosition;
			localPosition.y = currentPath.getYValue();
			base.transform.localPosition = localPosition;
			setParameters();
			StartCoroutine("startMoving");
			int speed = 1;
			if (spdMultiplier == 1.43f)
			{
				speed = 2;
			}
			else if (spdMultiplier == 3.33f)
			{
				speed = 3;
			}
			audioController.playSmithFootstepAudio(dogPrefix, speed);
		}
		else
		{
			reachDestination();
		}
	}

	public void makeDogBark()
	{
		isBarking = true;
		GetComponentInChildren<Animator>().StopPlayback();
		audioController.playDogBarkAudio();
		setAnimationState("stand", aValue: false);
		setAnimationState("bark", aValue: true);
	}

	public void pauseCharacter()
	{
		isMoving = false;
		base.gameObject.GetComponentInChildren<Animator>().speed = 0f;
		audioController.pauseSmithFootstepAudio(dogPrefix);
	}

	public void resumeCharacter()
	{
		base.gameObject.GetComponentInChildren<Animator>().speed = 1f;
		if (Vector3.Distance(base.transform.localPosition, targetPos) > 0f)
		{
			audioController.resumeSmithFootstepAudio(dogPrefix);
			isMoving = true;
			StartCoroutine("startMoving");
		}
		else if (!isBarking)
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

	public bool getIsMoving()
	{
		return isMoving;
	}

	public bool getIsBarking()
	{
		return isBarking;
	}

	public void cancelCurrentAnimation()
	{
		if (Vector3.Distance(base.transform.localPosition, targetPos) == 0f && animationState != string.Empty)
		{
			GetComponentInChildren<Animator>().SetBool(animationState, value: false);
			checkNextState();
			audioController.stopDogBarkAudio();
			GetComponentInChildren<Animator>().StopPlayback();
		}
	}

	private void setAnimationState(string aState, bool aValue)
	{
		GetComponentInChildren<Animator>().SetBool(aState, aValue);
		if (aState == "bark")
		{
			isBarking = aValue;
		}
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
		audioController.stopSmithFootstepAudio(dogPrefix);
		if (isMoving)
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
			audioController.playSmithFootstepAudio(dogPrefix, speed);
		}
	}
}
