using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MessagePack;
using MessagePack.Resolvers;
using UnityEditor.VersionControl;
using UnityEngine;
using Vox;
using Vox.Render;
using FileMode = System.IO.FileMode;

public class BlockMan : MonoBehaviour
{
	public ToolBar toolBar;

	public VolumeRenderer volumeRenerer;
	public ChunkedVolumeRenderer chunkedVolumeRenderer;
	private LargeVolume volume;

	public bool isDig = false;
	public GameObject cursor;

	public Vector2 clickedPosition;
	public bool isClicked = false;

	private VoxelEngineContext context;
	public Item itemInHand;

	void Start ()
	{
		volume = new LargeVolume() {autoCreate = true};//new Int3(64, 64, 64));
		var position = new Int3(0, 0, 0);
		volume.SetBlock(ref position, Block.Stone );
		volumeRenerer.volume = volume;
		volumeRenerer.GetComponent<VolumeBehaviour>().volume = volume;
		if (chunkedVolumeRenderer != null)
			chunkedVolumeRenderer.volume = volume;
		
		// TODO: @jian 以后这些配到表里
		toolBar.cells[0].item = new Item {id = (int)BlockId.Air, canDig = true};
		toolBar.cells[1].item = new Item {id = (int)BlockId.Stone, canDig = false};
		toolBar.cells[2].item = new Item {id = (int)BlockId.Worm, canDig = false};

		context = VoxelEngineContext.Default;	
		
		MessagePack.Resolvers.CompositeResolver.RegisterAndSetAsDefault(
				// enable extension packages first
				MessagePack.Unity.UnityResolver.Instance,

				// finaly use standard(default) resolver
				StandardResolver.Instance);
	}
	
	void Update ()
	{
		// 移动选择
		Vector3 touchPosition;

		if (Input.touchCount <= 1)
			touchPosition = Input.mousePosition;
		else
			touchPosition = Input.touches[0].position;
		
		var ray = Camera.main.ScreenPointToRay(touchPosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit))
		{
			var localPos = hit.point - volumeRenerer.transform.position;
			if (!isDig)
				localPos += hit.normal * 0.5f;
			else 
				localPos -= hit.normal * 0.5f;
					            
			Int3 blockPos = new Int3(
				Mathf.RoundToInt(localPos.x),
				Mathf.RoundToInt(localPos.y),
				Mathf.RoundToInt(localPos.z)
			);
//			Debug.Log(hit.point + " " + hit.normal + " "  + volumeRenerer.transform.position + " " + blockPos);

			var blockId = volume.GetBlockId(ref blockPos);
			//var blockController = context.blockManager.GetController(blockId);
			
			//blockController.On

			if (cursor != null)
			{
				cursor.transform.position = blockPos + volumeRenerer.transform.position;				
			}
				

			if (isClicked)
			{
				if (!isDig)
				{
					// TODO: @jian 这里要给物品配置blockId，将物品与block分离
					if (itemInHand == null)
					{
						Debug.LogError("Item in Hand is Null");
					}

					var blockController = context.blockManager.GetController((byte)itemInHand.id);
					var block = new Block((byte) itemInHand.id);
					blockController.BeforePlace(volume, ref blockPos, ref block);
					volume.SetBlock(ref blockPos, block);					
					blockController.AfterPlace(volume, ref blockPos, ref block);
//					cursor.GetComponent<Cursor>().FadeOutAndIn();
				}
					
				else 
					volume.SetBlock(ref blockPos, Block.Air);
			}
		
		}

		if (isClicked)
			ClearClickEvent();

	}

	public void OnClicked(Vector2 position)
	{
		clickedPosition = position;
		isClicked = true;
	}

	public void ClearClickEvent()
	{
		isClicked = false;
	}

	
	public void Save()
	{
		Debug.Log("Saving " + Application.persistentDataPath + "/volume1.bin");
		var bytes = MessagePackSerializer.Serialize(volume);
		File.WriteAllBytes(Application.persistentDataPath + "/volume1.bin", bytes);
	}

	public void Load()
	{
		Debug.Log("Loading " + Application.persistentDataPath + "/volume1.bin");
		
		var fs = File.Open(Application.persistentDataPath + "/volume1.bin", FileMode.Open);
		volume = MessagePackSerializer.Deserialize<LargeVolume>(fs);
		fs.Close();

		chunkedVolumeRenderer.volume = volume;
		chunkedVolumeRenderer.GetComponent<VolumeBehaviour>().volume = volume;

	}
}
