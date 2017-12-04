using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

namespace Vox.Render
{
    public class VolumeMeshBuilder : VolumeBuilder
    {
        public override void BuildMeshData(VolumeBuildTask task)
        {
            BuildMeshData(task.meshData, task.volume, task.context);
        }

        public void BuildMeshData(MeshData meshData, IVolume volume, VoxelEngineContext context)
        {
            var size = volume.size;
            var blockManager = context.blockManager;

//            Profiler.BeginSample("Build MeshData");
            var position = new Int3();

            for (var y = volume.position.y; y < volume.position.y + size.y; y++)
            {
                position.y = y;

                for (var x = volume.position.x; x < volume.position.x + size.x; x++)
                {
                    position.x = x;

                    for (var z = volume.position.z; z < volume.position.z + size.z; z++)
                    {
                        position.z = z;

                        var blockId = volume.GetBlockId(ref position);
                        var blockController = blockManager.GetController(blockId);

                        blockController.Build(meshData, volume, position, blockId, context);
                    }
                }
            }

//            Profiler.EndSample();
        }
    }
}