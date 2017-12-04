using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vox
{
    [RequireComponent(typeof(VolumeAccessor))]
    public class VolumeSimulator : MonoBehaviour
    {
    private VolumeAccessor accessor;
        private VoxelEngineContext context;
        private float tickCd = 0;
        private float tickCdMax = 0.1f;

        private List<BlockUpdateTask> tasks = new List<BlockUpdateTask>();

        public IVolume volume
        {
            get { return accessor!=null?accessor.volume:null; }
        }



        void Start()
        {
            accessor = GetComponent<VolumeAccessor>();
            context = VoxelEngineContext.Default;            
            accessor.volume.OnBlockAdd += OnBlockAdd;
            accessor.volume.OnBlockRemove += OnBlockRemove;

            if (volume != null)
                InitializeVolumeUpdates();
        }

        private void InitializeVolumeUpdates()
        {
            var position = volume.position;
            for (var x = volume.position.x; x < volume.position.x + volume.size.x; x++)
            {
                position.x = x;
                
                for (var z = volume.position.z; z < volume.position.z + volume.size.z; z++)
                {
                    position.z = z;
                    
                    for (var y = volume.position.y; y < volume.position.y + volume.size.y; y++)
                    {
                        position.y = y;

                        var blockId = volume.GetBlockId(ref position);
                        var blockController = context.blockManager.GetController(blockId);
                        if (blockController.updateEnabled)
                        {
                            tasks.Add(new BlockUpdateTask
                            {
                                blockId = blockId,
                                position = position

                            });
                        }
                    }
                }
            }
//            if (volume!=null)
//                volume.
        }

        private void OnBlockAdd(Int3 position, byte blockId)
        {
            var blockController = context.blockManager.GetController(blockId);
            if (blockController.updateEnabled)
            {
                tasks.Add(new BlockUpdateTask
                {
                    blockId = blockId,
                    position = position

                });
            }

        }

        private void OnBlockRemove(Int3 position, byte blockId)
        {
            var blockController = context.blockManager.GetController(blockId);
            if (blockController.updateEnabled)
            {
                for (var i = 0; i < tasks.Count; i++)
                {
                    if (tasks[i].position == position)
                    {
                        tasks.RemoveAt(i);
                        break;
                    }
                }
            }
        }

    void Update()
        {
            if (volume == null)
                return;
            
            tickCd -= Time.deltaTime;
            
            if (tickCd > 0)
                return;

            tickCd = tickCdMax;
            


            foreach (var task in tasks.ToArray())
            {
                var blockController = context.blockManager.GetController(task.blockId);
                blockController.Update(volume, task.position, task, 0.1f);                    
            }
        }
    }
}