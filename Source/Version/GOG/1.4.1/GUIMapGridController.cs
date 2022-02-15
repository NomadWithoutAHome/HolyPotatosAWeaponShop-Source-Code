using System.Collections;
using UnityEngine;

public class GUIMapGridController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private GameObject mapGrid;

	private int widthNoOfSquare = 11;

	private int heightNoOfSquare = 11;

	private float spawnSpeed;

	private Vector3[,] coordinates;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		mapGrid = GameObject.Find("MapGrid");
		coordinates = new Vector3[widthNoOfSquare, heightNoOfSquare];
		createMapWorld();
	}

	public void createMapWorld()
	{
		StartCoroutine("createWorld");
	}

	private IEnumerator createWorld()
	{
		yield return new WaitForSeconds(3f);
		float worldWidth = (float)this.widthNoOfSquare * 8.5f / 2f;
		float worldHeight = (float)this.heightNoOfSquare * 9f / 2f;
		int xArrayNo = 0;
		int zArrayNo = 0;
		for (float num = -worldHeight; num < worldHeight - 0.01f; num += 9f)
		{
			for (float num2 = -worldWidth; num2 < worldWidth - 0.01f; num2 += 8.5f)
			{
				this.coordinates[xArrayNo, zArrayNo] = new Vector3(num2, 0f, num);
				xArrayNo++;
			}
			zArrayNo++;
			xArrayNo = 0;
		}
		yield return null;
		yield break;
	}

	public int getWidthNoOfSquare()
	{
		return widthNoOfSquare;
	}

	public int getHeightNoOfSquare()
	{
		return heightNoOfSquare;
	}

	public Vector3 getPosition(int x, int z)
	{
		return coordinates[x, z];
	}

	public Vector3 getCentrePosition(Vector2 start, Vector2 end)
	{
		Vector3 vector = coordinates[(int)start.x, (int)start.y];
		return (coordinates[(int)end.x, (int)end.y] + vector) / 2f;
	}

	public float getRotation(Vector2 start, Vector2 end)
	{
		float num = -1f;
		if (end.x != start.x)
		{
			return 60f;
		}
		return 180f;
	}

	public Vector2 getCoordinates(Vector3 aPosition)
	{
		aPosition.y = 0f;
		for (int i = 0; i < widthNoOfSquare; i++)
		{
			for (int j = 0; j < heightNoOfSquare; j++)
			{
				if (Vector3.Distance(aPosition, coordinates[i, j]) <= 0.2f)
				{
					return new Vector2(i, j);
				}
			}
		}
		return new Vector2(-1f, -1f);
	}
}
