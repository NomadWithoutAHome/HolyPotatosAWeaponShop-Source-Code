using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUICutscenePathController : MonoBehaviour
{
	private Game game;

	private GUICutsceneController cutsceneController;

	private GUICutsceneGridController gridController;

	private CommonScreenObject commonScreenObject;

	private AudioController audioController;

	private CutscenePath currentPath;

	private List<Vector2> queuePath;

	private Vector3 targetPos;

	private RuntimeAnimatorController originalController;

	private int currentIndex;

	private float xDegree;

	private float yDegree;

	private bool loop;

	private bool faceBack;

	private bool isMoving;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		cutsceneController = GameObject.Find("GUICutsceneController").GetComponent<GUICutsceneController>();
		gridController = GameObject.Find("GUICutsceneGridController").GetComponent<GUICutsceneGridController>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		originalController = null;
		loop = false;
		faceBack = false;
		isMoving = false;
	}

	private void Update()
	{
		if (isMoving)
		{
			float maxDistanceDelta = 4f * Time.deltaTime;
			base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, targetPos, maxDistanceDelta);
		}
	}

	public void setPosition(CutsceneDialogue aDialogue)
	{
		Vector2 spawnPoint = aDialogue.getSpawnPoint();
		Vector3 position = gridController.getPosition((int)spawnPoint.x, (int)spawnPoint.y);
		position.y = aDialogue.getYValue();
		base.transform.localPosition = position;
		base.transform.localRotation = Quaternion.Euler(aDialogue.getStartingRotation());
		faceBack = aDialogue.getFaceBack();
		GetComponentInChildren<Animator>().SetBool("faceBack", faceBack);
		GetComponentInChildren<Animator>().SetBool("stand", value: true);
	}

	public void setPath(CutsceneDialogue aDialogue)
	{
		currentPath = game.getGameData().getCutscenePathByRefID(aDialogue.getCutscenePathRefID());
		queuePath = currentPath.getPathList();
		currentIndex = 1;
		setParameters();
	}

	public void changeAction(string imageName, string actionName, bool aLoop = false)
	{
		RuntimeAnimatorController runtimeAnimatorController = GetComponentInChildren<Animator>().runtimeAnimatorController;
		AnimatorOverrideController animatorOverrideController = new AnimatorOverrideController();
		if (originalController == null)
		{
			originalController = runtimeAnimatorController;
		}
		animatorOverrideController.runtimeAnimatorController = originalController;
		animatorOverrideController["action"] = Resources.Load("Animation/Cutscene/" + imageName + "/" + actionName) as AnimationClip;
		GetComponentInChildren<Animator>().runtimeAnimatorController = animatorOverrideController;
		GetComponentInChildren<Animator>().SetBool("stand", value: false);
		GetComponentInChildren<Animator>().SetBool("action", value: true);
		loop = aLoop;
		if (loop)
		{
			cutsceneController.showNextDialogue();
		}
	}

	private void setParameters()
	{
		targetPos = gridController.getPosition((int)queuePath[currentIndex].x, (int)queuePath[currentIndex].y);
		targetPos.y = currentPath.getYValue();
		Vector2 coordinates = gridController.getCoordinates(base.transform.localPosition);
		Vector2 vector = queuePath[currentIndex];
		float num = coordinates.x - vector.x;
		float num2 = coordinates.y - vector.y;
		xDegree = 35.264f;
		yDegree = 45f;
		GetComponentInChildren<Animator>().SetBool("stand", value: false);
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
		isMoving = true;
		StartCoroutine("startMoving");
	}

	private IEnumerator startMoving()
	{
		while (isMoving)
		{
			yield return new WaitForSeconds(0.1f);
			if (Vector3.Distance(base.transform.localPosition, targetPos) == 0f)
			{
				isMoving = false;
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
		StopCoroutine("startMoving");
		isMoving = false;
		base.transform.localRotation = Quaternion.Euler(currentPath.getXDegree(), currentPath.getYDegree(), 0f);
		faceBack = currentPath.getFaceBack();
		base.gameObject.GetComponentInChildren<Animator>().SetBool("faceBack", currentPath.getFaceBack());
		base.gameObject.GetComponentInChildren<Animator>().SetBool("stand", value: true);
		cutsceneController.showNextDialogue();
	}

	public void stopAction()
	{
		if (!loop)
		{
			GetComponentInChildren<Animator>().SetBool("stand", value: true);
			GetComponentInChildren<Animator>().SetBool("action", value: false);
			cutsceneController.showNextDialogue();
		}
	}

	public void stopStand()
	{
		GetComponentInChildren<Animator>().SetBool("stand", value: false);
	}

	public void setLoop(bool aLoop)
	{
		loop = aLoop;
	}
}
