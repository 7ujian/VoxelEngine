using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vox
{
    public class VolumeBehaviour : MonoBehaviour
    {
        class BlockUpdateTask
        {
            public byte blockId;
            public Int3 position;
        }

        public IVolume volume { get; set; }
        private VoxelEngineContext context;
        private float tickCd = 0;
        private float tickCdMax = 0.1f;

        private List<BlockUpdateTask> tasks = new List<BlockUpdateTask>();


        void Start()
        {
            context = VoxelEngineContext.Default;
            volume.OnBlockAdd += OnBlockAdd;
            volume.OnBlockRemove += OnBlockRemove;
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
            
            Int3 position;

            var size = volume.size;
            var blockManager = context.blockManager;

            foreach (var task in tasks.ToArray())
            {
                var blockController = context.blockManager.GetController(task.blockId);
                blockController.Update(volume, ref task.position, 0.1f);                    
            }
            return;
            
            // TODO @jian 下面很猥琐，以后改为GetBlock接口传入的是localPosition
            var globalPosition = new Int3();
            var volumePosition = volume.position;
            
            for (var y = 0; y < size.y; y++)
            {
                position.y = y;
                globalPosition.y = y + volumePosition.y;
                for (var x = 0; x < size.x; x++)
                {
                    position.x = x;
                    globalPosition.x = x + volumePosition.x;
                    for (var z = 0; z < size.z; z++)
                    {
                        position.z = z;
                        globalPosition.z = z + volumePosition.z;
                        
                        var blockId = volume.GetBlockId(ref globalPosition);
                        if (blockId != (byte)BlockId.Air && blockId != (byte)BlockId.Void)
                        {
                            var blockController = context.blockManager.GetController(blockId);
                        
                            // TODO @jian 这里需要考虑优化
                            // 如果在一帧Tick所有砖块，会卡，考虑将Tick分散到不同帧去，tick一定数量，yield出来，下一帧继续tick
                        
                            blockController.Update(volume, ref globalPosition, tickCdMax);    
                        }
                    }
                }
            }
        }
    }
}