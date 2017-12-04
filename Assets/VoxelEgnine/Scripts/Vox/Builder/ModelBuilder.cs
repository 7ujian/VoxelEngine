using UnityEngine;
using System.Collections.Generic;
using Vox.Render;


namespace Vox
{
    public class ModelBuilder
    {
        
        public virtual void Build(VolumeRenderer renderer, IVolume volume, Int3 position, byte blockId,
            VoxelEngineContext context)
        {
            var blockController = context.blockManager.GetController(blockId);
            var model = context.modelManager.GetModel(blockId);

            model.transform.parent = renderer.transform;
            model.transform.localPosition = (Vector3)(position - volume.position);
            model.transform.localRotation = Quaternion.identity;
            model.transform.localScale = Vector3.one;
        }
    }
}
