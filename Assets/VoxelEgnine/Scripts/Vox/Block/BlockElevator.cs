using System;
using UnityEngine;

namespace Vox
{
    public class BlockElevator:BlockController
    {
        
        
        public override string name { get { return "Elevator"; }}
        
        public override bool isSolid {get { return false; }}
        public override Color32 color {get { return new Color32(0,0,0,255); }}
        public override bool updateEnabled {get { return true; }}
        public override bool canUseOnScene { get { return true; } }

        public static int GetDistance(byte property)
        {            
            return property & 0x7;
        }
        
        public static byte SetDistance(byte property, int distance)
        {
            Debug.Assert(distance >= 0 && distance < 8);
            return (byte) (property & 0xF8 | distance);
        }

        public static bool GetIsRunning(byte property)
        {
            return (property & 0x8) != 0;
        }

        public static byte SetIsRunning(byte property, bool running)
        {
            return (byte) (running ? (property | 0x8) : (property & 0xF7));
        }

        public override int GetDirection(byte property)
        {
            return (property >> 4) & 0x7;
        }

        public override byte SetDirection(byte property, int direction)
        {
            return (byte) (property & 0x8F | (direction << 4));
        }
        
        public override void UseOnScene(IVolume volume, ref Int3 position)
        {
            var property = volume.GetBlockProperty(ref position);            
            var isRunning = GetIsRunning(property);
            if (isRunning)
                return;

            property = SetIsRunning(property, true);
            volume.SetBlockProperty(ref position, property);
        }
        
        public override void Update(IVolume volume, Int3 position, BlockUpdateTask task, float deltaTime)
        {            
            var property = volume.GetBlockProperty(ref position);
            
            var isRunning = GetIsRunning(property);
            if (!isRunning)
                return;
            
            var direction = GetDirection(property);
            var distance = GetDistance(property);
            
            if (distance < 7)
            {
                if (PushBlock(volume, position, direction, out position, 4))
                {
                   // task.position = position;
                    distance++;
                    property = SetDistance(property, distance);
                    if (distance == 7)
                    {
                        property = SetDistance(property, 0);
                        property = SetDirection(property, VoxelUtility.GetOppositeDirection(direction));
                        property = SetIsRunning(property, false);
                    }                        
                }                        
                else
                    property = SetIsRunning(property, false);
            }
            else if (distance == 7)
            {
                property = SetIsRunning(property, false);
            }
            
            volume.SetBlockProperty(ref position, property);
        }
    }
}