using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

namespace Vox.Render
{
    public class VolumeMeshBuilder
    {
        public void Build(VolumeBuildTask task)
        {
            BuildMeshData(task.meshData, task.volume, task.context);
        }

       
        private static void BuildMeshData(MeshData meshData, IVolume volume, VoxelEngineContext context)
        {           
            var size = volume.size;
            var blockManager = context.blockManager;

//            Profiler.BeginSample("Build MeshData");
            var position = new Int3();
            // TODO @jian 下面很猥琐，以后改为GetBlock接口传入的是localPosition
            var globalPosition = new Int3();
            for (var y = 0; y < size.y; y++)
            {
                position.y = y;
                globalPosition.y = y + volume.position.y;
                for (var x = 0; x < size.x; x++)
                {
                    position.x = x;
                    globalPosition.x = x + volume.position.x;
                    for (var z = 0; z < size.z; z++)
                    {
                        position.z = z;
                        globalPosition.z = z + volume.position.z;
                        
                        var blockId = volume.GetBlockId(ref globalPosition);
                        var blockController = blockManager.GetController(blockId);

                        blockController.Build(meshData, volume, position, blockId, context);

                    }
                }
            }

//            Profiler.EndSample();
            
        }
    }
}