using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MessagePack;

namespace Vox {
	
	[MessagePackObject]
	public class LargeVolume : ChunkedVolume
	{
		[Key(1)]
        public Dictionary<int, Chunk> chunks;
		[Key(2)]
		public int bitWidthX;
		[Key(3)]
		public int bitWidthY;
		[Key(4)]
		public int bitWidthZ;
				
		private Chunk cachedChunk;
		private int cachedChunkKey = int.MinValue;
		
		public LargeVolume()
		{
			bitWidthX = 7;
			bitWidthY = 4;
			bitWidthZ = 7;

			chunks = new Dictionary<int, Chunk>();
		}

		public void AddChunk(Chunk chunk)
		{
			var position = chunk.position;
			var key = GetChunkKey(ref position);

			//Debug.Log("Add Chunk " + position + " " + key);
			if (chunks.ContainsKey(key))
			{
				throw new System.Exception("Duplicated chunk in " + position + " " + key);
			}

			chunks[key] = chunk;
		}

		public void RemoveChunk(Int3 position)
		{
			var key = GetChunkKey(ref position);

			if (!chunks.ContainsKey(key))
			{
				throw new System.Exception("Missing Chunk " + position + " " + key);
			}

			chunks.Remove(key);
		}
            		
        public override Chunk GetChunk (ref Int3 position)
        {
	        var key = GetChunkKey(ref position);

	        if (key == cachedChunkKey)
		        return cachedChunk;

            Chunk chunk = null;

	        if (!chunks.TryGetValue(key, out chunk))
	        {
		        if (autoCreate)
		        {
			        chunk = new Chunk();
			        chunk.volume = this;
			        chunk.position = new Int3(
				        unchecked ((int)(position.x & Settings.ChunkPositionMask)),
				        unchecked ((int)(position.y & Settings.ChunkPositionMask)), 
				        unchecked ((int)(position.z & Settings.ChunkPositionMask)));
			        chunks[key] = chunk;

			        cachedChunk = chunk;
			        cachedChunkKey = key;

			        if (onCreateChunk != null)
				        onCreateChunk(chunk);
		        }
	        }
	        else
	        {
		        cachedChunk = chunk;
		        cachedChunkKey = key;
	        }

			return chunk;
		}            		

		private int GetChunkKey(ref Int3 position)
		{
			var x = (position.x >> Settings.BlockPositionBitWidth) + 64;
			var y = (position.y >> Settings.BlockPositionBitWidth) + 8;
			var z = (position.z >> Settings.BlockPositionBitWidth) + 64;

			return (y << bitWidthX | x ) << bitWidthZ | z;
		}

		// TODO: @jian 处理Destory
		public override void Destroy()
		{
			foreach (var pair in chunks)
			{
				pair.Value.Destory();
			}			
		}
	}
}