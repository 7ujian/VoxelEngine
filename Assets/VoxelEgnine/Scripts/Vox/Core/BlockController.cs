using UnityEngine;

namespace Vox
{
    public class BlockController
    {        
        public virtual bool isSolid { get; private set; }
        public virtual Color32 color { get; private set; }
        public virtual bool updateEnabled { get; private set; }
        
        public BlockBuilder builder { get; set; }
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

        public virtual void Update(IVolume volume, ref Int3 position, float deltaTime)
        {            
        }
        
        
    }
}