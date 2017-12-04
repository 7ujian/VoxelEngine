using LuaInterface;
using UnityEngine;

namespace Vox
{
    public class LuaBlockController:BlockController
    {
        private LuaState lua;

        private LuaFunction luaBeforePlace;
        private LuaFunction luaAfterPlace;
        private LuaFunction luaUpdate;
        
        public override void BeforePlace(IVolume volume, ref Int3 position, ref Block block)
        {
            if (luaBeforePlace != null)
                luaBeforePlace.Call(volume, position, block);
        }
        
        public override void AfterPlace(IVolume volume, ref Int3 position, ref Block block)
        {
            if (luaAfterPlace != null)
                luaAfterPlace.Call(volume, position, block);
        }

        public override void Update(IVolume volume, Int3 position, BlockUpdateTask task,  float deltaTime)
        {
            if (luaUpdate != null)
                luaUpdate.Call(volume, position, task, deltaTime);
        }

        public LuaBlockController(LuaState lua, VoxelEngineContext context = null):base(context)
        {
            LoadLua(lua);                        
        }

        private void LoadLua(LuaState lua)
        {
            this.lua = lua;

            
            Color c = lua.Invoke<Color>("GetColor", true);
            
            this.color = new Color32((byte)(c.r * 255), (byte)(c.g*255), (byte)(c.b*255), (byte)255);
            
            luaBeforePlace = lua.GetFunction("BeforePlace");
            luaAfterPlace = lua.GetFunction("AfterPlace");
            luaUpdate = lua.GetFunction("Update");

            if (luaUpdate != null)
                updateEnabled = true;
        }

        public static LuaBlockController CreateTestController()
        {
            var lua = new LuaState();
            LuaBinder.Bind(lua);
            lua.Start();
            lua.DoString(@"
local inspect = require 'inspect'
name = 'Cute';
color = Color(0.9, 0.3, 0.2, 1)

function AfterPlace(volume, position, block)
    local position2 = Vox.Int3.New(position.x, position.y+1, position.z)

    volume:SetBlock(position2, Vox.Block.New(1))
end

function Update(volume, position, task, deltaTime)
    local block = volume:GetBlock(position)
    local position2 = Vox.Int3.New(position.x, position.y+1, position.z)
    
    volume:SetBlock(position, Vox.Block.New(0))
    volume:SetBlock(position2, block)
end

function GetColor()
    return color
end

");
            var controller = new LuaBlockController(lua);
            return controller;
        }
    }
}