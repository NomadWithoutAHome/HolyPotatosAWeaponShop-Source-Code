using System.Collections;
using UnityEngine;

public class GUICutsceneGridController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private GameObject cutsceneGrid;

	private int widthNoOfSquare = 12;

	private int heightNoOfSquare = 14;

	private float spawnSpeed;

	private Vector3[,] coordinates;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		cutsceneGrid = GameObject.Find("CutsceneGrid");
		coordinates = new Vector3[widthNoOfSquare, heightNoOfSquare];
		createCutsceneWorld();
	}

	public void createCutsceneWorld()
	{
		StartCoroutine("createWorld");
	}

	private IEnumerator createWorld()
	{
		yield return new WaitForSeconds(2f);
		ShopLevel playerShopLvl = this.game.getPlayer().getShopLevel();
		float worldWidth = (float)this.widthNoOfSquare * 0.73f / 2f;
		float worldHeight = (float)this.heightNoOfSquare * 0.74f / 2f;
		int xArrayNo = 0;
		int zArrayNo = 0;
		for (float num = -worldHeight; num < worldHeight - 0.01f; num += 0.74f)
		{
			for (float num2 = -worldWidth; num2 < worldWidth - 0.01f; num2 += 0.73f)
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
