using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000194 RID: 404
public class GUICutsceneGridController : MonoBehaviour
{
	// Token: 0x06000A31 RID: 2609 RVA: 0x0004C1F8 File Offset: 0x0004A5F8
	private void Awake()
	{
		this.game = GameObject.Find("Game").GetComponent<Game>();
		this.commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		this.cutsceneGrid = GameObject.Find("CutsceneGrid");
		this.coordinates = new Vector3[this.widthNoOfSquare, this.heightNoOfSquare];
		this.createCutsceneWorld();
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x0004C25C File Offset: 0x0004A65C
	public void createCutsceneWorld()
	{
		base.StartCoroutine("createWorld");
	}

	// Token: 0x06000A33 RID: 2611 RVA: 0x0004C26C File Offset: 0x0004A66C
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

	// Token: 0x06000A34 RID: 2612 RVA: 0x0004C287 File Offset: 0x0004A687
	public int getWidthNoOfSquare()
	{
		return this.widthNoOfSquare;
	}

	// Token: 0x06000A35 RID: 2613 RVA: 0x0004C28F File Offset: 0x0004A68F
	public int getHeightNoOfSquare()
	{
		return this.heightNoOfSquare;
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x0004C297 File Offset: 0x0004A697
	public Vector3 getPosition(int x, int z)
	{
		return this.coordinates[x, z];
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x0004C2A8 File Offset: 0x0004A6A8
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

	// Token: 0x040009C7 RID: 2503
	private Game game;

	// Token: 0x040009C8 RID: 2504
	private CommonScreenObject commonScreenObject;

	// Token: 0x040009C9 RID: 2505
	private GameObject cutsceneGrid;

	// Token: 0x040009CA RID: 2506
	private int widthNoOfSquare = 12;

	// Token: 0x040009CB RID: 2507
	private int heightNoOfSquare = 14;

	// Token: 0x040009CC RID: 2508
	private float spawnSpeed;

	// Token: 0x040009CD RID: 2509
	private Vector3[,] coordinates;
}
