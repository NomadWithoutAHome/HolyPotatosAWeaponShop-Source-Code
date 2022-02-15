using UnityEngine;

public class GUICameraController : MonoBehaviour
{
	private GameObject nguiCameraScene;

	private GameObject nguiCameraGUI;

	private GameObject nguiCameraMap;

	private GameObject nguiCameraMapGUI;

	private GameObject activeSceneCamera;

	private void Awake()
	{
		nguiCameraScene = GameObject.Find("NGUICameraScene");
		nguiCameraGUI = GameObject.Find("NGUICameraGUI");
		nguiCameraMap = GameObject.Find("NGUICameraMap");
		nguiCameraMapGUI = GameObject.Find("NGUICameraMapGUI");
		activeSceneCamera = nguiCameraScene;
	}

	public void changeCutsceneCamera()
	{
		nguiCameraScene.SetActive(value: false);
		nguiCameraGUI.SetActive(value: false);
		nguiCameraMap.SetActive(value: false);
		nguiCameraMapGUI.SetActive(value: false);
	}

	public void changeSceneCamera()
	{
		nguiCameraScene.SetActive(value: true);
		nguiCameraGUI.SetActive(value: true);
		nguiCameraMap.SetActive(value: false);
		nguiCameraMapGUI.SetActive(value: false);
		nguiCameraScene.GetComponent<GUISceneCameraController>().setCameraEnabled(aCameraEnabled: true);
		nguiCameraMap.GetComponent<GUISceneCameraController>().setCameraEnabled(aCameraEnabled: false);
		activeSceneCamera = nguiCameraScene;
	}

	public void changeMapCamera()
	{
		nguiCameraMap.SetActive(value: true);
		nguiCameraMapGUI.SetActive(value: true);
		nguiCameraScene.SetActive(value: false);
		nguiCameraGUI.SetActive(value: false);
		nguiCameraScene.GetComponent<GUISceneCameraController>().setCameraEnabled(aCameraEnabled: false);
		nguiCameraMap.GetComponent<GUISceneCameraController>().setCameraEnabled(aCameraEnabled: true);
		activeSceneCamera = null;
	}
}
