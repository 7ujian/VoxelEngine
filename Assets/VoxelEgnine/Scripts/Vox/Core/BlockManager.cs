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
            var modelBuilder = new ModelBuilder();
            var waterBuilder = new WaterBuilder();
            
            
            controllers = new BlockController[256];
            
            controllers[(int)BlockId.Air] = new BlockAir();
            controllers[(int)BlockId.Stone] = new BlockStone { builder = cubeBuilder };
            controllers[(int)BlockId.Water] = new BlockWater { builder = waterBuilder };
            controllers[(int)BlockId.Worm] = new BlockWorm { builder = cubeBuilder };
            controllers[(int)BlockId.Snake] = new BlockSnake { builder = cubeBuilder };
            controllers[(int)BlockId.SnakeBody] = new BlockSnakeBody { builder = cubeBuilder };
            controllers[(int)BlockId.Food] = new BlockFood { builder = cubeBuilder };
            controllers[(int)BlockId.Elevator] = new BlockElevator { builder = cubeBuilder };
            controllers[(int)BlockId.Guard] = new BlockGaurd { builder = cubeBuilder};
            controllers[(int)BlockId.Joint] = new BlockJoint { builder = cubeBuilder};
            controllers[(int)BlockId.Tree] = new BlockTree { modelBuilder = modelBuilder};
            controllers[(int)BlockId.MagicWand] = new BlockMagicWand();
            controllers[(int)BlockId.Rotator] = new BlockRotator();

            var luaController = LuaBlockController.CreateTestController();
            luaController.builder = cubeBuilder;
            controllers[(int) BlockId.LuaBlock] = luaController;
    
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