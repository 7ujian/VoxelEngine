using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MessagePack;

namespace Vox {

	[MessagePackObject]
	public class LargeVolume : ChunkedVolume
	{
		[Key(1)] public Dictionary<int, Chunk> chunks;
		[Key(2)] public int bitWidthX;
		[Key(3)] public int bitWidthY;
		[Key(4)] public int bitWidthZ;

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

			AddChunkNeighbours(chunk);
		}

		public void RemoveChunk(Int3 position)
		{
			var key = GetChunkKey(ref position);

			Chunk chunk;
			if (!chunks.TryGetValue(key, out chunk))
			{
				throw new System.Exception("Missing Chunk " + position + " " + key);
			}
			
			chunks.Remove(key);
			RemoveChunkNeighbours(chunk);
		}

		public static Int3[] NeighbourOffsets =
		{
			new Int3(0, 0, Consts.ChunkSize),
			new Int3(Consts.ChunkSize, 0, 0),
			new Int3(0, 0, -Consts.ChunkSize),
			new Int3(-Consts.ChunkSize, 0, 0),
			new Int3(0, Consts.ChunkSize, 0),
			new Int3(0, -Consts.ChunkSize, 0),
		};

		
		
		private void AddChunkNeighbours(Chunk chunk)
		{
			for (var i = 0; i < 6; i++)
			{
				var neighbourPosition = chunk.position + NeighbourOffsets[i];
				var neighbour = GetChunk(ref neighbourPosition);
				if (neighbour != null)
				{
					chunk.neighbours[i] = neighbour;
					neighbour.neighbours[VoxelUtility.GetOppositeDirection(i)] = chunk.neighbours[i];
				}
			}
		}

		private void RemoveChunkNeighbours(Chunk chunk)
		{
			for (var i = 0; i < 6; i++)
			{
				var neighbour = chunk.neighbours[i];
				if (neighbour != null)
				{
					neighbour.neighbours[VoxelUtility.GetOppositeDirection(i)] = null;
					chunk.neighbours[i] = null;
				}				
			}
		}

		public override Chunk GetOrCreateChunk(ref Int3 position)
		{
			var key = GetChunkKey(ref position);

			if (key == cachedChunkKey)
				return cachedChunk;

			Chunk chunk = null;

			if (!chunks.TryGetValue(key, out chunk))
			{
				chunk = new Chunk();
				chunk.volume = this;
				chunk.position = new Int3(
					unchecked ((int)(position.x & Settings.ChunkPositionMask)),
					unchecked ((int)(position.y & Settings.ChunkPositionMask)), 
					unchecked ((int)(position.z & Settings.ChunkPositionMask)));
				
				chunks[key] = chunk;
				AddChunkNeighbours(chunk);

				cachedChunk = chunk;
				cachedChunkKey = key;

				if (onCreateChunk != null)
					onCreateChunk(chunk);				
			}
			else
			{
				cachedChunk = chunk;
				cachedChunkKey = key;
			}

			return chunk;
		}
            		
        public override Chunk GetChunk (ref Int3 position)
        {
	        var key = GetChunkKey(ref position);

	        if (key == cachedChunkKey)
		        return cachedChunk;

            Chunk chunk = null;

	        if (chunks.TryGetValue(key, out chunk))
	        {
		        cachedChunk = chunk;
		        cachedChunkKey = key;
		        return chunk;
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