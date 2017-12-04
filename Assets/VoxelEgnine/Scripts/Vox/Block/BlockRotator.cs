using System;
using UnityEngine;
using Vox.Render;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Vox
{
    public class BlockRotator : BlockController
    {
        public override string name { get { return "Rotator"; }}
        public override bool canUseOnBlock {get { return true; }}
        
        public override void UseOnBlock(IVolume volume, ref Int3 blockPos)
        {
            var block = volume.GetBlock(ref blockPos);
            var blockController = context.blockManager.GetController(block.id);
            var direction = blockController.GetDirection(block.property);
            // TODO: @jian 这里做6向旋转，之后要分D6, R4 等
            direction = (direction + 1) % 6;
            block.property = blockController.SetDirection(block.property, direction);
            volume.SetBlockProperty(ref blockPos, block.property);
        }
    }
}