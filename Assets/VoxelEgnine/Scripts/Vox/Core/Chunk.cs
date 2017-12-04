using System;
using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using MessagePack;

namespace Vox
{
	[MessagePackObject]
	public partial class Chunk : Volume
	{
		[IgnoreMember]
		public ChunkedVolume volume { get; set; }

		[IgnoreMember] 
		public Chunk[] neighbours = new Chunk[6];
		
		public Chunk():base(new Int3(Settings.ChunkSize, Settings.ChunkSize, Settings.ChunkSize))
		{
		}

//		public override byte GetBlockId(ref Int3 position)
//		{
//			if (InRange(ref position))
//				return base.GetBlockId(ref position);
//			
//			if (volume != null)
//				return volume.GetBlockId(ref position);
//			
//			return Block.Void.id;
//		}
		
		public override byte GetBlockId(ref Int3 position)
		{
			// TODO: @jian 这里和Base重复Check InRange
			if (InRange(ref position))
			{
				return base.GetBlockIdNoCheck(ref position);
			}				
			
			if (volume != null)
				return volume.GetBlockId(ref position);
			
			return Block.Void.id;
		}

		public override byte GetBlockProperty(ref Int3 position)
		{
			if (InRange(ref position))
				return base.GetBlockPropertyNoCheck(ref position);
			
			if (volume != null)
				return volume.GetBlockProperty(ref position);
			
			return 0;
		}

		public override byte GetBlockLight(ref Int3 position)
		{
			if (InRange(ref position))
				return base.GetBlockLightNoCheck(ref position);
			
			if (volume != null)
				return volume.GetBlockLight(ref position);
			
			return 0;
		}

		public override Block GetBlock(ref Int3 position)
		{
			if (InRange(ref position))
				return base.GetBlockNoCheck(ref position);
			
			if (volume != null)
				return volume.GetBlock(ref position);
			
			return Block.Void;
		}

		public override void SetBlock(ref Int3 position, Block block)
		{
			if (InRange(ref position))
				base.SetBlockNoCheck(ref position, block);
			else if (volume != null)
				volume.SetBlock(ref position, block);
		}

		public override void SetBlockProperty(ref Int3 position, byte property)
		{
			if (InRange(ref position))
				base.SetBlockPropertyNoCheck(ref position, property);
			else if (volume != null)
				volume.SetBlockProperty(ref position, property);
		}

		public override void SetBlockLight(ref Int3 position, byte light)
		{
			if (InRange(ref position))
				base.SetBlockLightNoCheck(ref position, light);
			else if (volume != null)
				volume.SetBlockLight(ref position, light);		
		}
		
		protected override void SetDirtyFlag(ref Int3 position)
		{
			// TODO: @jian 下面可以优化
			// 1. 使用位运算
			// 2. 缓存Bounds信息，不要每次计算
			var w = position.x == this.position.x;
			var e = position.x == this.position.x + size.x - 1;
			var s = position.z == this.position.z;
			var n = position.z == this.position.z + size.z - 1;
			var d = position.y == this.position.y;
			var u = position.y == this.position.y + size.y - 1;

			if (n && neighbours[0] != null)
			{
				neighbours[0].SetDirty(true);
			}

			if (e && neighbours[1] != null)
			{
				neighbours[1].SetDirty(true);
			}
			
			if (s && neighbours[2] != null)
			{
				neighbours[2].SetDirty(true);
			}
			
			if (w && neighbours[3] != null)
			{
				neighbours[3].SetDirty(true);
			}
			
			if (u && neighbours[4] != null)
			{
				neighbours[4].SetDirty(true);
			}
			
			if (d && neighbours[5] != null)
			{
				neighbours[5].SetDirty(true);
			}				
		}

		protected override void GlobalToLocal(ref Int3 position)
		{
			position.x = unchecked((int)(position.x & Settings.BlockPositionMask));
			position.y = unchecked((int)(position.y & Settings.BlockPositionMask));
			position.z = unchecked((int)(position.z & Settings.BlockPositionMask));
			
		}

		protected override bool InRange(ref Int3 position)
		{			
//			var x = unchecked((int)(position.x & Settings.ChunkPositionMask));
//			var y = unchecked((int)(position.y & Settings.ChunkPositionMask));
//			var z = unchecked((int)(position.z & Settings.ChunkPositionMask));
			
			
			return unchecked ((int)(position.x & Settings.ChunkPositionMask)) == this.position.x &&
			       unchecked ((int)(position.y & Settings.ChunkPositionMask)) == this.position.y &&
			       unchecked ((int)(position.z & Settings.ChunkPositionMask)) == this.position.z;
		}

		protected override int GetIndexLocal(ref Int3 localPosition)
		{			
#if VOXEL_DEBUG
			Assert.IsTrue(InRange(localPosition));
#endif

			int index = 
				localPosition.y << Settings.BlockPositionBitWidthX2 | 
				localPosition.x << Settings.BlockPositionBitWidth | 
				localPosition.z;

			return index;
		}

		protected override int GetIndexGlobal(ref Int3 position)
		{
			var x = unchecked ((int)(position.x & Settings.BlockPositionMask));
			var y = unchecked ((int)(position.y & Settings.BlockPositionMask));
			var z = unchecked ((int)(position.z & Settings.BlockPositionMask));
			
#if VOXEL_DEBUG
			Assert.IsTrue(InRange(localPosition));
#endif

			int index = 
				y << Settings.BlockPositionBitWidthX2 | 
				x << Settings.BlockPositionBitWidth | 
				z;

			return index;
		}
		
		public Int3 ToChunkPosition(ref Int3 position)
		{
			Int3 chunkPosition;
			chunkPosition.x = unchecked ((int)(position.x & Settings.BlockPositionMask));
			chunkPosition.y = unchecked ((int)(position.y & Settings.BlockPositionMask));
			chunkPosition.z = unchecked ((int)(position.z & Settings.BlockPositionMask));
			
			return chunkPosition;
		}

		public override string ToString()
		{
			return "Chunk " + this.position;
		}

		// TODO: @jian
		
		public void Destory()
		{
			destroyed = true;
		}

		
	}
}