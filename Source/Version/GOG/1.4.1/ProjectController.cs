using UnityEngine;

public class ProjectController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private void Start()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
	}
}
