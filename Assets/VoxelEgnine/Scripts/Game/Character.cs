using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vox.Terrain;

public class Character : MonoBehaviour
{

	public TerrainUser terrainUser;
	
	public void Update()
	{
		terrainUser.position = transform.position;
	}
}
