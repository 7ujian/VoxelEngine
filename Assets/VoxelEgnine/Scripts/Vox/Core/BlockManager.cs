using System;
using UnityEngine.Assertions;

namespace Vox
{
    public class BlockManager
    {
        private BlockController[] controllers;
        private bool initialized = false;

        public BlockManager()
        {

        }

        public void Initialize()
        {
            if (initialized)
                return;
            
            var cubeBuilder = new CubeBuilder();
            var waterBuilder = new WaterBuilder();
            
            controllers = new BlockController[256];
            
            controllers[(int)BlockId.Air] = new BlockAir();
            controllers[(int)BlockId.Stone] = new BlockStone()
            {
                builder = cubeBuilder
            };
            controllers[(int)BlockId.Water] = new BlockWater()
            {
                builder = waterBuilder
            };
            controllers[(int)BlockId.Void] = new BlockVoid();

            initialized = true;
        }
        
      
        public BlockController GetController(byte id)
        {
            var controller = controllers[id];
            if (controller == null)
            {
                UnityEngine.Debug.LogError(string.Format("Missing blockController {0}", id));
            }

            return controller;

        }


    }
}