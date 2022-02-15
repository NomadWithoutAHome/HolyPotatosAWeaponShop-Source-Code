using UnityEngine;

public class GUISceneCameraController : MonoBehaviour
{
	private GameData gameData;

	private CommonScreenObject commonScreenObject;

	private bool cameraEnabled;

	private UIDraggableCamera dragCam;

	private BoxCollider cameraBound;

	private BoxCollider mapCameraBound;

	private Touch pinchFinger1;

	private Touch pinchFinger2;

	private string mouseHorizontalAxisName = "Mouse X";

	private string mouseVerticalAxisName = "Mouse Y";

	private string scrollAxisName = "Mouse ScrollWheel";

	private string horizontalKey = "Horizontal";

	private string verticalKey = "Vertical";

	private float xPosLimitUpper;

	private float xPosLimitLower;

	private float yPosLimitUpper;

	private float yPosLimitLower;

	private float zoomLimitUpper;

	private float zoomLimitLower;

	private float mapTargetZoom;

	private float cameraZ;

	private bool reachPosIn;

	private float cameraCentreX;

	private float cameraCentreY;

	private float zoomDefault;

	private Vector3 targetPos;

	private float targetZoom;

	private float speed;

	private float zoomSpeed;

	private bool zoomingIn;

	private bool zoomingOut;

	private bool panLimit;

	private void Awake()
	{
		gameData = GameObject.Find("Game").GetComponent<Game>().getGameData();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		cameraEnabled = true;
		dragCam = GetComponent<UIDraggableCamera>();
		cameraBound = GameObject.Find("CameraBound").GetComponent<BoxCollider>();
		mapCameraBound = GameObject.Find("MapCameraBound").GetComponent<BoxCollider>();
		switch (base.gameObject.name)
		{
		case "NGUICameraMap":
			xPosLimitUpper = 4000f;
			xPosLimitLower = -4000f;
			yPosLimitUpper = 2500f;
			yPosLimitLower = -2500f;
			zoomLimitLower = 6f;
			zoomLimitUpper = 15f;
			cameraZ = -30000f;
			dragCam.momentumAmount = 275f;
			base.transform.localPosition = new Vector3(0f, 0f, cameraZ);
			GetComponent<Camera>().orthographicSize = 13f;
			break;
		case "NGUICameraScene":
			xPosLimitUpper = 1500f;
			xPosLimitLower = -700f;
			yPosLimitUpper = 500f;
			yPosLimitLower = -900f;
			zoomLimitLower = 4f;
			zoomLimitUpper = 10f;
			cameraZ = -3000f;
			dragCam.momentumAmount = 150f;
			break;
		}
		reachPosIn = true;
		speed = 10000f;
		zoomSpeed = 0.5f;
		zoomingIn = false;
		zoomingOut = false;
		panLimit = true;
	}

	private void Update()
	{
		if (cameraEnabled)
		{
			if (Input.GetKey(gameData.getKeyCodeByRefID("100001")))
			{
				float y = 0.5f;
				base.transform.Translate(0f, y, 0f);
			}
			if (Input.GetKey(gameData.getKeyCodeByRefID("100003")))
			{
				float y2 = -0.5f;
				base.transform.Translate(0f, y2, 0f);
			}
			if (Input.GetKey(gameData.getKeyCodeByRefID("100002")))
			{
				float x = -0.5f;
				base.transform.Translate(x, 0f, 0f);
			}
			if (Input.GetKey(gameData.getKeyCodeByRefID("100004")))
			{
				float x2 = 0.5f;
				base.transform.Translate(x2, 0f, 0f);
			}
			if (Input.GetKey(gameData.getKeyCodeByRefID("100005")))
			{
				float num = 0.5f;
				GetComponent<Camera>().orthographicSize -= num;
				GetComponent<Camera>().orthographicSize = Mathf.Clamp(GetComponent<Camera>().orthographicSize, zoomLimitLower, zoomLimitUpper);
			}
			if (Input.GetKey(gameData.getKeyCodeByRefID("100006")))
			{
				float num2 = -0.5f;
				GetComponent<Camera>().orthographicSize -= num2;
				GetComponent<Camera>().orthographicSize = Mathf.Clamp(GetComponent<Camera>().orthographicSize, zoomLimitLower, zoomLimitUpper);
			}
			if (commonScreenObject.checkShopScaleScrollAllowed() && (Input.GetAxis(scrollAxisName) > 0f || Input.GetAxis(scrollAxisName) < 0f))
			{
				float num3 = Input.GetAxis(scrollAxisName) * 0.5f;
				GetComponent<Camera>().orthographicSize -= num3;
				GetComponent<Camera>().orthographicSize = Mathf.Clamp(GetComponent<Camera>().orthographicSize, zoomLimitLower, zoomLimitUpper);
			}
			if (panLimit)
			{
				base.transform.localPosition = new Vector3(Mathf.Clamp(base.transform.localPosition.x, xPosLimitLower, xPosLimitUpper), Mathf.Clamp(base.transform.localPosition.y, yPosLimitLower, yPosLimitUpper), base.transform.localPosition.z);
			}
		}
		if (!reachPosIn)
		{
			float num4 = Mathf.Abs(base.transform.position.x - targetPos.x);
			float num5 = Mathf.Abs(base.transform.position.y - targetPos.y);
			if (num4 != num5 && (num4 >= 1f || num5 >= 1f))
			{
				base.transform.position = Vector3.MoveTowards(base.transform.position, targetPos, speed * Time.deltaTime / 30f);
				base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, cameraZ);
			}
			else
			{
				reachPosIn = true;
				targetPos = Vector3.zero;
			}
		}
		if (zoomingIn)
		{
			if (GetComponent<Camera>().orthographicSize >= targetZoom)
			{
				GetComponent<Camera>().orthographicSize -= zoomSpeed;
			}
			else
			{
				zoomingIn = false;
				targetZoom = 0f;
			}
		}
		if (zoomingOut)
		{
			if (GetComponent<Camera>().orthographicSize <= targetZoom)
			{
				GetComponent<Camera>().orthographicSize += zoomSpeed;
				return;
			}
			zoomingOut = false;
			targetZoom = 0f;
		}
	}

	public bool getCameraEnabled()
	{
		return cameraEnabled;
	}

	public void setCameraEnabled(bool aCameraEnabled)
	{
		cameraEnabled = aCameraEnabled;
		if (base.gameObject.name == "NGUICameraMap")
		{
			mapCameraBound.enabled = aCameraEnabled;
		}
		else if (base.gameObject.name == "NGUICameraScene")
		{
			cameraBound.enabled = aCameraEnabled;
		}
	}

	public void zoomSceneCameraIn(bool transition = true)
	{
		if (transition)
		{
			targetZoom = 5f;
			zoomingOut = false;
			zoomingIn = true;
		}
		else
		{
			GetComponent<Camera>().orthographicSize = 5f;
		}
	}

	public void zoomSceneCameraOut(bool transition = true)
	{
		if (transition)
		{
			targetZoom = 10f;
			zoomingIn = false;
			zoomingOut = true;
		}
		else
		{
			GetComponent<Camera>().orthographicSize = 10f;
		}
	}

	public void focusMapCameraOn(GameObject aObject, bool zoomIn = true)
	{
		Vector3 position = aObject.transform.position;
		panLimit = false;
		reachPosIn = true;
		if (zoomIn)
		{
			targetPos = position;
			reachPosIn = false;
		}
		else
		{
			targetPos = base.transform.parent.InverseTransformPoint(position);
			targetPos.z = cameraZ;
			commonScreenObject.tweenPosition(GetComponent<TweenPosition>(), base.transform.localPosition, targetPos, 0.75f, null, string.Empty);
		}
	}

	public void focusMapCameraOut(bool transition)
	{
		reachPosIn = true;
		if (transition)
		{
			targetPos = new Vector3(cameraCentreX, cameraCentreY, cameraZ);
			CommonAPI.debug("targetPos: " + targetPos.ToString());
			commonScreenObject.tweenPosition(GetComponent<TweenPosition>(), base.transform.localPosition, targetPos, 0.75f, base.gameObject, "activatePanLimit");
		}
		else
		{
			base.transform.localPosition = new Vector3(cameraCentreX, cameraCentreY, cameraZ);
			activatePanLimit();
		}
	}

	public void activatePanLimit()
	{
		panLimit = true;
	}

	public bool getPanLimit()
	{
		return panLimit;
	}

	public void zoomMapCameraIn()
	{
		targetZoom = mapTargetZoom;
		zoomingOut = false;
		zoomingIn = true;
	}

	public void zoomMapCameraOut(bool transition)
	{
		if (transition)
		{
			targetZoom = zoomDefault;
			zoomingIn = false;
			zoomingOut = true;
		}
		else
		{
			GetComponent<Camera>().orthographicSize = zoomDefault;
		}
	}

	public void setCameraVariables(AreaRegion aRegion)
	{
		cameraCentreX = aRegion.getCameraCentreX();
		cameraCentreY = aRegion.getCameraCentreY();
		zoomDefault = aRegion.getZoomDefault();
		xPosLimitUpper = aRegion.getXPosLimitUpper();
		xPosLimitLower = aRegion.getXPosLimitLower();
		yPosLimitUpper = aRegion.getYPosLimitUpper();
		yPosLimitLower = aRegion.getYPosLimitLower();
		zoomLimitUpper = aRegion.getZoomLimitUpper();
		zoomLimitLower = aRegion.getZoomLimitLower();
		mapTargetZoom = aRegion.getTargetZoom();
	}
}
