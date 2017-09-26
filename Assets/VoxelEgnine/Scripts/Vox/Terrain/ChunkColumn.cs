using System;
using UnityEngine.Assertions;

namespace Vox.Terrain
{
    public class ChunkColumn : ChunkedVolume
    {
        public int height = 16;

        public Chunk[] chunks;

        public ChunkColumn()
        {
            chunks = new Chunk[16];
            this.size = new Int3(16, 256, 16);
        }

        public override Chunk GetChunk(ref Int3 position)
        {
            var y = position.y >> Settings.BlockPositionBitWidth;
            
            Assert.IsTrue(y >= 0 && y <= height, "Out of range");
            
            
            var chunk = chunks[y];

            if (autoCreate && chunk == null)
            {
                // TODO: @jian 将这部分与LargeVolume整合统一接口
                chunk = new Chunk();
                //chunk.volume = this;
                chunk.position = new Int3(
                    this.position.x, 
                    unchecked ((int)(position.y & Settings.ChunkPositionMask)), 
                    this.position.z);
                chunks[y] = chunk;
                
                if (onCreateChunk != null)
                    onCreateChunk(chunk);
            }

            return chunk;
        }

        protected bool InRange(ref Int3 position)
        {
            // TODO: @jian 考虑Y位置
            return unchecked ((int)(position.x & Settings.ChunkPositionMask)) == this.position.x &&
                   //unchecked ((int)(position.y & Settings.ChunkPositionMask)) == this.position.y &&
                   unchecked ((int)(position.z & Settings.ChunkPositionMask)) == this.position.z;
        }

        public override void Destroy()
        {
            foreach (var chunk in chunks)
            {
                if (chunk != null)
                    chunk.Destory();
            }
        }
    }
}