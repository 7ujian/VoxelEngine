using UnityEngine;
using System.Collections;
using Vox;
using Vox.Render;
using VoxelEgnine.Scripts.Vox.Generator;
using Vox.Terrain;

public class VoxDemo : MonoBehaviour {

	private Engine engine;
	public Material defaultVolumeMaterial;

	private Chunk chunk;
	private Vox.Terrain.Terrain terrain;

	// Use this for initialization
	void Start () {
		InitializeVox();		
	}
	

	void InitializeVox()
	{
		
		
		var volume = new LargeVolume();
		var chunkPos = new Int3(0, 0, 0);
		chunk = volume.GetChunk(ref chunkPos);
		chunk.position = chunkPos;

		var generator = new RandomGenerator();
		generator.Generate(chunk);
	}

	public void CreateTerrain()
	{
		terrain = gameObject.AddComponent<Vox.Terrain.Terrain>();
		AddPlayer();
	}

	public void AddPlayer()
	{
		if (terrain != null)
		{
			var user = new TerrainUser(terrain);
			user.position = new Int3(0,0,0);
			terrain.AddUser(user);
			
			var player = new GameObject("Player");
			var character = player.AddComponent<Character>();
			character.terrainUser = user;
		}
	}

	void Update()
	{
		if(terrain != null)
			terrain.UpdateUserProximities();		
	}

	public void Testx1()
	{
		TestBuild(1);
	}

	public void Testx10()
	{
		TestBuild(10);
	}

	public void Testx100()
	{
		TestBuild(100);
	}

	public void TestBuild(int times)
	{
		var sw = new System.Diagnostics.Stopwatch();
		sw.Start();
           
		for (var i = 0; i < times; i++)
		{
			BuildVolume();
		}
		
		sw.Stop();
		Debug.Log("Build Mesh " + sw.ElapsedMilliseconds);
	}
	
	public void BuildVolume()
	{
		var chunkGO = new GameObject("chunkGO");
		chunkGO.AddComponent<MeshFilter>();
		var meshRenderer =chunkGO.AddComponent<MeshRenderer>();
		var volumeRenderer = chunkGO.AddComponent<VolumeRenderer>();
		
		chunkGO.AddComponent<VolumeAccessor>().volume = chunk;

		if (defaultVolumeMaterial != null)
			meshRenderer.material = defaultVolumeMaterial;
		else
		{
			var material = new Material(Shader.Find("Standard"));
			meshRenderer.material = material;			
		}				
		
		volumeRenderer.Update();
	}

	
}
