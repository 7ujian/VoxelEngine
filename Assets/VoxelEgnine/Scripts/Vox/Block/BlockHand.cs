using System;
using UnityEngine;
using Vox.Render;

namespace Vox
{
    public class BlockMagicWand : BlockController
    {
        public override void UseOnScene(IVolume volume, ref Int3 blockPos)
        {
            var result = FloodFill.Flood(volume, blockPos);

            // TODO @jian 这里先用猥琐的方法解决
            var material = GameObject.FindObjectOfType<ChunkedVolumeRenderer>().material;
            
            var go = new GameObject();
            var renderer =go.AddComponent<ChunkedVolumeRenderer>();
            renderer.material = material;
            renderer.volume = result.volume as LargeVolume;
        }
    }
}