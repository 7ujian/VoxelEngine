using UnityEngine;

namespace Vox
{
    public class BlockWorm:BlockController
    {
        
        public override bool isSolid {get { return false; }}
        public override Color32 color {get { return new Color32(255,255,0,255); }}
        public override bool updateEnabled {get { return true; }}

        public override void BeforePlace(IVolume volume, ref Int3 position, ref Block block)
        {            
        }

        public override void AfterPlace(IVolume volume, ref Int3 position, ref Block block)
        {                                    
        }

        public override void Update(IVolume volume, ref Int3 position, float deltaTime)
        {
            var block = volume.GetBlock(ref position);
            var property = block.property;
            var direction = property & 0x3;
            var distance = property >> 2;

            if (distance == 0)
            {                                
                direction = UnityEngine.Random.Range(0, 4);

                var nextPosition = VoxelUtility.DirectionToInt3(direction) + position;
                var nextBlockId = volume.GetBlockId(ref nextPosition);
                var nextDownPosition = nextPosition + new Int3(0, -1, 0);
                var nextDownBlockId = volume.GetBlockId(ref nextDownPosition);
                
                if (nextBlockId == (byte) BlockId.Air && nextDownBlockId != (byte)BlockId.Air)
                {
                    distance = UnityEngine.Random.Range(4, 8);
                    property = (byte)(distance << 2 | direction);
                    volume.SetBlockProperty(ref position, property);    
                }
                
            }
            else
            {
                var nextPosition = VoxelUtility.DirectionToInt3(direction) + position;
                var nextBlockId = volume.GetBlockId(ref nextPosition);
                var nextDownPosition = nextPosition + new Int3(0, -1, 0);
                var nextDownBlockId = volume.GetBlockId(ref nextDownPosition);
                
                if (nextBlockId == (byte) BlockId.Air)
                {
                    if (nextDownBlockId != (byte) BlockId.Air)
                    {
                        distance--;

                        property = (byte) (distance << 2 | direction);
                        block.property = property;


                        //VoxelUtility.Plus(ref nextPosition, ref position);

                        volume.SetBlock(ref nextPosition, block);
                        volume.SetBlock(ref position, Block.Air);    
                    }
                    else
                    {
                        volume.SetBlockProperty(ref position, 0);
                    }
                    
                }
                else
                {
                    volume.SetBlock(ref nextPosition, Block.Air);
                }
            }
        }
    }
}