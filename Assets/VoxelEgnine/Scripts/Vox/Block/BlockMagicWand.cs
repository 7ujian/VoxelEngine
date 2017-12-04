using System;
using UnityEngine;
using Vox.Render;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Vox
{
    public class BlockMagicWand : BlockController
    {
        public override string name { get { return "Magic"; }}
        public override bool canUseOnBlock {get { return true; }}
        
        public override void UseOnBlock(IVolume volume, ref Int3 blockPos)
        {
            var result = FloodFill.Flood(volume, blockPos);

//            var volumeRenderer = VolumeFactory.Instance.CreateSimpleVolume();
//            
//            volumeRenderer.volume = result.volume;
            
#if UNITY_EDITOR

            var path = EditorUtility.SaveFilePanel("Save Blueprint", Application.persistentDataPath + "/blueprints/", "1.blueprint", "blueprint");
            if (!string.IsNullOrEmpty(path))
                GameObject.FindObjectOfType<BlockMan>().SaveBlueprint(result.volume as Volume, System.IO.Path.GetFileNameWithoutExtension(path));
#endif            
        }
    }
}