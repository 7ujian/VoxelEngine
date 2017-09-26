using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MessagePack;

namespace Vox {
	
	public abstract class ChunkedVolume : IVolume
	{
		[Key(0)]
		public bool autoCreate = false;
		
		[IgnoreMember]
		public Action<Chunk> onCreateChunk;
		[IgnoreMember]
		public Action<Chunk> onRemoveChunk;
		[IgnoreMember]
		public Action<Int3, byte> OnBlockAdd { get; set; }
		[IgnoreMember]
		public Action<Int3, byte> OnBlockRemove { get; set; }
		
		[IgnoreMember]
        public Int3 size { get; protected set; }
		[IgnoreMember]
		public Int3 position { get; set; }
		[IgnoreMember]
		public bool destroyed { get; private set; }
				

		private Chunk cachedChunk;
		private int cachedChunkKey = int.MinValue;
		
		public ChunkedVolume()
		{
		}

//		private void _CreateChunk(ref Int3 position)
//		{
//			var chunk = CreateChunkInternal(ref position);
//
//			if (onCreateChunk != null)
//			{
//				onCreateChunk(chunk);
//			}
//		}
//
//		protected virtual Chunk CreateChunkInternal(ref Int3 position)
//		{
//			throw new NotImplementedException();
//		}
            		
        public virtual Chunk GetChunk (ref Int3 position)
        {
			throw new NotImplementedException();
		}
            		
		public Block GetBlock (ref Int3 position)
		{
			Chunk chunk = GetChunk(ref position);
			
			if (chunk != null)				
				return chunk.GetBlock(ref position);			
			else
				return Block.Air;
		}

		public byte GetBlockId(ref Int3 position)
		{
			Chunk chunk = GetChunk(ref position);
						
			if (chunk != null)							
				return chunk.GetBlockIdNoCheck(ref position);						
			else
				return Block.Air.id;
		}
		
		public byte GetBlockProperty(ref Int3 position)
		{
			Chunk chunk = GetChunk(ref position);
			
			if (chunk != null)
			{				
				return chunk.GetBlockPropertyNoCheck(ref position);
			}				
			else
				return 0;
		}
		
		public byte GetBlockLight(ref Int3 position)
		{
			Chunk chunk = GetChunk(ref position);
			
			if (chunk != null)
			{				
				return chunk.GetBlockLightNoCheck(ref position);
			}				
			else
				return 0;
		}

        public void SetBlock (ref Int3 position, Block block)
        {
            var chunk = GetChunk(ref position);

            if (chunk != null)
            {	            
                chunk.SetBlockNoCheck(ref position, block);
            }
            else
            {
                Debug.Log("Failed to find chunk when SetVoxel");
            }
        }
		

		public void SetBlockLight(ref Int3 position, byte light)
		{
			var chunk = GetChunk(ref position);

			if (chunk != null)
			{				
				chunk.SetBlockLightNoCheck(ref position, light);
			}			
		}
		
		public void SetBlockProperty(ref Int3 position, byte property)
		{
			var chunk = GetChunk(ref position);

			if (chunk != null)
			{
				chunk.SetBlockPropertyNoCheck(ref position, property);
			}			
		}

		public virtual void Destroy()
		{
			destroyed = true;
		}
		

		// TODO
//		private Int3 GlobalToLocal(Int3 globalPosition)
//		{
//			return new Int3(
//				globalPosition.x & Settings.BlockPositionMask,
//				globalPosition.y & Settings.BlockPositionMask,
//				globalPosition.z & Settings.BlockPositionMask
//			);
//		}

	}
}