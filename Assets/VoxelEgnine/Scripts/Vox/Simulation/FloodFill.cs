using System.Collections.Generic;
using UnityEngine;

namespace Vox
{
    public class FloodFill
    {
        public static FloodFillResult Flood(IVolume volume,  Int3 position)
        {
            var result = new FloodFillResult();
            result.bounds.min = position;
            result.bounds.max = position;
            
            // TODO: @jian CacheVolume会产生很多GC的垃圾 
            var cacheVolume = new LargeVolume();

            var neighbourPosition = position;

            var queue = new Queue<Int3>();
            queue.Enqueue(position);
                                    
            while (queue.Count > 0)
            {
                position = queue.Dequeue();
                result.bounds.Intersect(position);
                
                var block = volume.GetBlock(ref position);
                if (block.id != (int) BlockId.Air && block.id != (int)BlockId.Joint)
                {
                    cacheVolume.SetBlock(ref position, block);

                    VoxelUtility.NorthOf(ref position, ref neighbourPosition);
                    if (cacheVolume.GetBlockId(ref neighbourPosition) == (int) BlockId.Air)
                        queue.Enqueue(neighbourPosition);

                    VoxelUtility.EastOf(ref position, ref neighbourPosition);
                    if (cacheVolume.GetBlockId(ref neighbourPosition) == (int) BlockId.Air)
                        queue.Enqueue(neighbourPosition);

                    VoxelUtility.SouthOf(ref position, ref neighbourPosition);
                    if (cacheVolume.GetBlockId(ref neighbourPosition) == (int) BlockId.Air)
                        queue.Enqueue(neighbourPosition);

                    VoxelUtility.WestOf(ref position, ref neighbourPosition);
                    if (cacheVolume.GetBlockId(ref neighbourPosition) == (int) BlockId.Air)
                        queue.Enqueue(neighbourPosition);

                    VoxelUtility.UpOf(ref position, ref neighbourPosition);
                    if (cacheVolume.GetBlockId(ref neighbourPosition) == (int) BlockId.Air)
                        queue.Enqueue(neighbourPosition);

                    VoxelUtility.DownOf(ref position, ref neighbourPosition);
                    if (cacheVolume.GetBlockId(ref neighbourPosition) == (int) BlockId.Air)
                        queue.Enqueue(neighbourPosition);
                }
            }
            
            var saveVolume = new Volume(result.bounds.size);
            cacheVolume.CopyTo(result.bounds.min, result.bounds.size, saveVolume, Int3.Zero);

            result.volume = saveVolume;

            return result;
        }
    }

    public class FloodFillResult
    {
        public List<Int3> blockPositions = new List<Int3>();
        public VoxelBounds bounds;
        public IVolume volume;
    }
}