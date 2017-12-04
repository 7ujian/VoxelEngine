using System;
using UnityEngine;

namespace Vox
{
    public class BlockWater :BlockController
    {
        public override string name { get { return "Water"; }}
        
        public override bool isSolid {get { return false; }}
        public override Color32 color {get { return new Color32(0,0,255,255); }}
        public override bool updateEnabled {get { return true; }}

        public override void BeforePlace(IVolume volume, ref Int3 position, ref Block block)
        {
            block.property = (byte) 4;
        }

        public override void AfterPlace(IVolume volume, ref Int3 position, ref Block block)
        {
            // TODO: @jian 这里临时加载AfterPlace，其实应该放在OnAdded                        
        }

        public override void Update(IVolume volume, Int3 position, BlockUpdateTask task, float deltaTime)
        {
            var block = volume.GetBlock(ref position);
            var level = GetWaterLevel(block.property);

            Int3 neighbourPosition = position;
            
            VoxelUtility.DownOf(ref position, ref neighbourPosition);
            
            var blockIdDown = volume.GetBlockId(ref neighbourPosition);

            if (blockIdDown == (byte)BlockId.Air)
            {
                volume.SetBlock(ref neighbourPosition, block);
                volume.SetBlock(ref position, Block.Air);
            }
            // TODO: @jian 将上面的水强制删除，这里有BUG的
            else if (blockIdDown == (byte) BlockId.Water)
            {
                volume.SetBlock(ref position, Block.Air);
            }
            else if (level > 1)
            {
                var neighbourLevel = level - 1;
                VoxelUtility.NorthOf(ref position, ref neighbourPosition);
                FloodWater(volume, ref neighbourPosition, neighbourLevel);
                
                VoxelUtility.EastOf(ref position, ref neighbourPosition);
                FloodWater(volume, ref neighbourPosition, neighbourLevel);
                
                VoxelUtility.SouthOf(ref position, ref neighbourPosition);
                FloodWater(volume, ref neighbourPosition, neighbourLevel);
                
                VoxelUtility.WestOf(ref position, ref neighbourPosition);
                FloodWater(volume, ref neighbourPosition, neighbourLevel);            
            }
        }

        private static void FloodWater(IVolume volume, ref Int3 position, int level)
        {
            var blockN = volume.GetBlock(ref position);
            if (blockN.id == (byte)BlockId.Air)
            {
                volume.SetBlock(ref position, new Block((int) BlockId.Water)
                {
                    property = (byte)(level)
                });                    
            }
        }

        public static int GetWaterLevel(byte property)
        {
            return property & 0xF;
        }
    }
}