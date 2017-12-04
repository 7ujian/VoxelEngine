using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MessagePack;

namespace Vox {
	
	public abstract class ChunkedVolume : IVolume
	{
		
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
		
		public virtual Chunk GetOrCreateChunk (ref Int3 position)
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

		// TODO: @jian 这里考虑用 ref Block block
        public void SetBlock (ref Int3 position, Block block)
        {
            var chunk = GetOrCreateChunk(ref position);

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
			var chunk = GetOrCreateChunk(ref position);

			if (chunk != null)
			{				
				chunk.SetBlockLightNoCheck(ref position, light);
			}			
		}
		
		public void SetBlockProperty(ref Int3 position, byte property)
		{
			var chunk = GetOrCreateChunk(ref position);

			if (chunk != null)
			{
				chunk.SetBlockPropertyNoCheck(ref position, property);
			}			
		}

		public virtual void Destroy()
		{
			destroyed = true;
		}
		
		// -------------------------
		// CopyTo
		// -------------------------
		// TODO: @jian 下面可以分Chunk Copy来优化
		public void CopyTo(Int3 sourcePosition, Int3 size, IVolume volume, Int3 destPosition)
		{
			// TODO: @jian use unsafe to disable bounds check
			var from = sourcePosition;
			var to = destPosition;
            
			for (var y = 0; y < size.y; y++)
			{
				for (var x = 0; x < size.x; x++)
				{
					for (var z = 0; z < size.z; z++)
					{
						from.x = sourcePosition.x + x;
						from.y = sourcePosition.y + y;
						from.z = sourcePosition.z + z;
                        
						to.x = destPosition.x + x;
						to.y = destPosition.y + y;
						to.z = destPosition.z + z;
                        
						volume.SetBlock(ref to, GetBlock(ref from));
					}
				}
			}
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