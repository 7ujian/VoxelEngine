using UnityEngine;

namespace Vox
{
    public class BlockSnake:BlockController
    {
        public override string name { get { return "Snake"; }}
        
        public override bool isSolid {get { return false; }}
        public override Color32 color {get { return new Color32(64,16,16,255); }}
        public override bool updateEnabled {get { return true; }}

        public static int GetPlayerId(byte property)
        {
            return property & 0x3;
        }

        // 0: n 1:e 2:s 3:w
        public static int GetDirection(byte property)
        {
            return (property >> 2) & 0x3;
        }

        // 0:tail 1:n 2:e 3:s 4:w
        public static int GetFollow(byte property)
        {
            return (property >> 4) & 0x7;
        }
                

        public static byte SetPlayerId(byte property, int playerId)
        {            
            return (byte)(property & 0xFC | playerId);
        }

        public static byte SetDirection(byte property, int direction)
        {
            return (byte) (property & 0xF3 | (direction << 2));
        }

        public static byte SetFollow(byte property, int follow)
        {
            return (byte) (property & 0x8F | (follow << 4));
        }

        public override void BeforePlace(IVolume volume, ref Int3 position, ref Block block)
        {
            
        }

        public override void AfterPlace(IVolume volume, ref Int3 position, ref Block block)
        {                                    
        }

        public override void Update(IVolume volume, Int3 position, BlockUpdateTask task, float deltaTime)
        {
            var block = volume.GetBlock(ref position);
            //var property = block.property;

            var direction = GetDirection(block.property);
            var turn = Random.Range(0, 10);
            if (turn == 0)
                direction = (direction + 3) % 4;
            else if (turn == 1)
                direction = (direction + 1) % 4;

            var nextPosition = VoxelUtility.DirectionToInt3(direction) + position;
            var nextBlockId = volume.GetBlockId(ref nextPosition);
            var nextDownPosition = nextPosition + new Int3(0, -1, 0);
            var nextDownBlockId = volume.GetBlockId(ref nextDownPosition);
            
            // Move
            if ((nextBlockId == (byte) BlockId.Air || nextBlockId == (byte) BlockId.Food ) && nextDownBlockId != (byte) BlockId.Air)
            {
                var follow = GetFollow(block.property);

                if (follow > 0 || nextBlockId == (byte) BlockId.Food)
                {                     
                    block.property = SetFollow(block.property, VoxelUtility.GetOppositeDirection(direction) + 1);
                }
                
                volume.SetBlock(ref nextPosition, block);
               

                var currentPosition = position;
                
                while (follow > 0)
                {
                    nextPosition = currentPosition;
                
                    currentPosition = VoxelUtility.DirectionToInt3(follow-1) + nextPosition;
                    block = volume.GetBlock(ref currentPosition);
                    
                    if (block.id != (byte) BlockId.SnakeBody)
                    {
                        Debug.LogError("Bad snakebody! id=" + block.id);
                        break;
                    }

                    var property = block.property;

                    // Has next body
                    if (GetFollow(block.property) > 0 || nextBlockId == (byte)BlockId.Food)
                        block.property = SetFollow(block.property, follow);
                    
                    volume.SetBlock(ref nextPosition, block);
                    
                    follow = GetFollow(property);
                }

                if (nextBlockId == (byte) BlockId.Food)
                {
                    volume.SetBlock(ref currentPosition, new Block((byte)BlockId.SnakeBody));
                }
                else
                {
                    volume.SetBlock(ref currentPosition, Block.Air);
                }
                
            }
            // Turn
            else 
            {
                direction = Random.Range(0, 4);
                var property = SetDirection(block.property, direction);
                volume.SetBlockProperty(ref position, property);
            }
           
        }
    }
}