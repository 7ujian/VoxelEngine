using UnityEngine;

namespace Vox.Terrain
{
    public class TerrainGenerator : IVolumeGenerator
    {
        public static Texture2D debugTexture;
        
        public void Generate(IVolume volume)
        {
            
        }

        public void Generate(ChunkColumn chunkColumn)
        {
            // TODO: @jian 这里要整理参数
            var mapX = 256;
            var mapZ = 256;

            var dx = (float)chunkColumn.size.x / mapX;
            var dz = (float)chunkColumn.size.z / mapZ;

            var px = (float)chunkColumn.position.x / mapX;
            var pz = (float)chunkColumn.position.z / mapZ;

            var block = Block.Air;
            var position = new Int3();
            
            // TODO: @jian 下面还要优化
            for (var x = 0; x < chunkColumn.size.x; x++)
            {
                position.x = x + chunkColumn.position.x;
                for (var z = 0; z < chunkColumn.size.z; z++)
                {
                    position.z = z + chunkColumn.position.z;
                    var p = Mathf.PerlinNoise(px, pz);
                    var height = Mathf.FloorToInt(p * 16) + 16;
                    
                    for (var y = 0; y < chunkColumn.size.y; y++)
                    {
                        position.y = y;
                        if (y <= height)
                        {
                            block.id = 1;
                            chunkColumn.SetBlock(ref position, block);
                        }
                        else
                        {
                            chunkColumn.SetBlock(ref position, Block.Air);
                        }
                    }
                    
                    pz += dz;
                }
                pz = chunkColumn.position.z / mapZ;
                px += dx;
            }
            
            
       }
    }
}