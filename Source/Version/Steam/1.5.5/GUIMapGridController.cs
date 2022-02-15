using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001C7 RID: 455
public class GUIMapGridController : MonoBehaviour
{
	// Token: 0x06000C88 RID: 3208 RVA: 0x00079F4C File Offset: 0x0007834C
	private void Awake()
	{
		this.game = GameObject.Find("Game").GetComponent<Game>();
		this.commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		this.mapGrid = GameObject.Find("MapGrid");
		this.coordinates = new Vector3[this.widthNoOfSquare, this.heightNoOfSquare];
		this.createMapWorld();
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x00079FB0 File Offset: 0x000783B0
	public void createMapWorld()
	{
		base.StartCoroutine("createWorld");
	}

	// Token: 0x06000C8A RID: 3210 RVA: 0x00079FC0 File Offset: 0x000783C0
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

	// Token: 0x06000C8B RID: 3211 RVA: 0x00079FDB File Offset: 0x000783DB
	public int getWidthNoOfSquare()
	{
		return this.widthNoOfSquare;
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x00079FE3 File Offset: 0x000783E3
	public int getHeightNoOfSquare()
	{
		return this.heightNoOfSquare;
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x00079FEB File Offset: 0x000783EB
	public Vector3 getPosition(int x, int z)
	{
		return this.coordinates[x, z];
	}

	// Token: 0x06000C8E RID: 3214 RVA: 0x00079FFC File Offset: 0x000783FC
	public Vector3 getCentrePosition(Vector2 start, Vector2 end)
	{
		Vector3 b = this.coordinates[(int)start.x, (int)start.y];
		return (this.coordinates[(int)end.x, (int)end.y] + b) / 2f;
	}

	// Token: 0x06000C8F RID: 3215 RVA: 0x0007A054 File Offset: 0x00078454
	public float getRotation(Vector2 start, Vector2 end)
	{
		float result;
		if (end.x != start.x)
		{
			result = 60f;
		}
		else
		{
			result = 180f;
		}
		return result;
	}

	// Token: 0x06000C90 RID: 3216 RVA: 0x0007A08C File Offset: 0x0007848C
	public Vector2 getCoordinates(Vector3 aPosition)
	{
		aPosition.y = 0f;
		for (int i = 0; i < this.widthNoOfSquare; i++)
		{
			for (int j = 0; j < this.heightNoOfSquare; j++)
			{
				if (Vector3.Distance(aPosition, this.coordinates[i, j]) <= 0.2f)
				{
					return new Vector2((float)i, (float)j);
				}
			}
		}
		return new Vector2(-1f, -1f);
	}

	// Token: 0x04000DC5 RID: 3525
	private Game game;

	// Token: 0x04000DC6 RID: 3526
	private CommonScreenObject commonScreenObject;

	// Token: 0x04000DC7 RID: 3527
	private GameObject mapGrid;

	// Token: 0x04000DC8 RID: 3528
	private int widthNoOfSquare = 11;

	// Token: 0x04000DC9 RID: 3529
	private int heightNoOfSquare = 11;

	// Token: 0x04000DCA RID: 3530
	private float spawnSpeed;

	// Token: 0x04000DCB RID: 3531
	private Vector3[,] coordinates;
}
