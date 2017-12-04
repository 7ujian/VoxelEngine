using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vox;
using Vox.Terrain;

public class Character : MonoBehaviour
{

	public TerrainUser terrainUser;
	
	public void Update()
	{
		terrainUser.position = (Int3)transform.position;
	}
}
