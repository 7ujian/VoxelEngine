using System;
using UnityEngine;
using Vox.Render;

namespace Vox
{
    public class BlockController
    {
        public virtual string name { get; protected set; }
        public virtual bool canDig { get { return false; }}
        public virtual bool isSolid { get; private set; }
        public virtual Color32 color { get; protected set; }
        public virtual bool updateEnabled { get; protected set; }
        public virtual bool canUseOnScene { get { return false; }}
        public virtual bool canUseOnBlock { get { return false; }}
        
        public BlockBuilder builder { get; set; }
        public ModelBuilder modelBuilder { get; set; }
        
        public float height { get; private set; }
        public VoxelEngineContext context { get; private set; }

        public BlockController(VoxelEngineContext context = null)
        {
            if (context == null)
                context = VoxelEngineContext.Default;
            this.context = context;
            isSolid = false;
            updateEnabled = false;
            color = new Color32(255, 255, 255, 255);
            name = "Unknown";
        }

        public virtual void Build(MeshData meshData, IVolume volume, Int3 position, byte blockId, VoxelEngineContext context)
        {
            if (builder != null)
                builder.Build(meshData, volume, position, blockId, context);           
        }
        
        public virtual void BeforePlace(IVolume volume, ref Int3 position, ref Block block)
        {
        }

        public virtual void AfterPlace(IVolume volume, ref Int3 position, ref Block block)
        {            
        }

        public virtual void Update(IVolume volume, Int3 position, BlockUpdateTask task,  float deltaTime)
        {            
        }

        public virtual void UseOnScene(IVolume volume, ref Int3 blockPos)
        {            
        }

        public virtual void UseOnBlock(IVolume volume, ref Int3 blockPos)
        {
        }

        public virtual int GetDirection(byte property)
        {
            return 0;
        }

        public virtual byte SetDirection(byte property, int direction)
        {
            return property;
        }
        

        // 推动砖块
        public static bool PushBlock(IVolume volume, Int3 blockPos, int direction, out Int3 outPos, int n=0)
        {            
            var nextPosition = blockPos + VoxelUtility.DirectionToInt3(direction);
         
            var nextBlockId = volume.GetBlockId(ref nextPosition);
            if (nextBlockId != (byte) BlockId.Air)
            {
                if (n <= 0 || !PushBlock(volume, nextPosition, direction, out outPos, n - 1))
                {
                    outPos = blockPos;
                    return false;    
                }                
            }

            var block = volume.GetBlock(ref blockPos);
            volume.SetBlock(ref nextPosition, block);
            volume.SetBlock(ref blockPos, Block.Air);
            outPos = nextPosition;
            
            return true;
        }
        
    }
}