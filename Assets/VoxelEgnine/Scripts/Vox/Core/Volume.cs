using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MessagePack;
using UnityEngine.Assertions;
using Debug = UnityEngine.Debug;

namespace Vox
{

    [MessagePackObject]
    public partial class Volume : IVolume, IRenderable
    {
        [Key(0)]
        public VolumeData data { get; set; }
        
        [IgnoreMember]
        public bool destroyed { get; protected set; }
        
        [IgnoreMember]
        public Int3 size
        {
            get { return data.size; }            
        }

        [IgnoreMember]
        public Int3 position
        {
            get { return data.position; }
            set { data.position = value; }
        }

        [IgnoreMember] public int dirtyFlag { get; private set; }

        [IgnoreMember]
        public Action<Int3, byte> OnBlockAdd { get; set; }
        [IgnoreMember]
        public Action<Int3, byte> OnBlockRemove { get; set; }

        public Volume()
        {            
        }

        public Volume(Int3 size)
        {
            this.data = new VolumeData(size);
        }                

        public virtual byte GetBlockId(ref Int3 position)
        {
            if (!InRange(ref position))
                return 0;

            return GetBlockIdNoCheck(ref position);
        }

        public byte GetBlockIdNoCheck(ref Int3 position)
        {
            var index = GetIndexGlobal(ref position);
            
            return data.ids[index];
        }
        
        public virtual byte GetBlockProperty(ref Int3 position)
        {
            if (!InRange(ref position))
                return 0;

            return GetBlockPropertyNoCheck(ref position);
        }

        public byte GetBlockPropertyNoCheck(ref Int3 position)
        {
            var index = GetIndexGlobal(ref position);

            return data.properties[index];
        }

        public virtual byte GetBlockLight(ref Int3 position)
        {
            if (!InRange(ref position))
                return 0;

            return GetBlockLightNoCheck(ref position);
        }

        public byte GetBlockLightNoCheck(ref Int3 position)
        {
            var index = GetIndexGlobal(ref position);

            return data.lights[index];
        }

        public virtual Block GetBlock(ref Int3 position)
        {                        
            if (!InRange(ref position))
                return Block.Air;

            return GetBlockNoCheck(ref position);
        }

        public Block GetBlockNoCheck(ref Int3 position)
        {
            var index = GetIndexGlobal(ref position);

            var block = new Block
            {
                id = data.ids[index],
                property = data.properties[index],
                light = data.lights[index]
            };
            
            return block;
        }

        public virtual void SetBlock(ref Int3 position, Block block)
        {
            if (!InRange(ref position))
            {
                Debug.LogError("Out of Range " + position);
                return;
            }
            
            SetBlockNoCheck(ref position, block);
        }

        public void SetBlockNoCheck(ref Int3 position, Block block)
        {
            var index = GetIndexGlobal(ref position);

            if (OnBlockRemove != null)
            {
                var oldBlockId = data.ids[index];
                if (oldBlockId != (byte) BlockId.Air)
                {
                    OnBlockRemove(position, oldBlockId);
                }
            }
            
            data.ids[index] = block.id;
            data.properties[index] = block.property;
            data.lights[index] = block.light;

            SetDirtyFlag(ref position);
            SetDirty(true);
            
            if (OnBlockAdd != null)
            {
                OnBlockAdd(position, block.id);
            }
        }

        public virtual void SetBlockProperty(ref Int3 position, byte property)
        {
            if (!InRange(ref position))
            {
                Debug.LogError("Out of Range " + position );
                return;
            }
            
            SetBlockPropertyNoCheck(ref position, property);
        }

        public void SetBlockPropertyNoCheck(ref Int3 position, byte property)
        {
            var index = GetIndexGlobal(ref position);
            
            data.properties[index] = property;
            
            SetDirtyFlag(ref position);
            SetDirty(true);
        }

        public virtual void SetBlockLight(ref Int3 position, byte light)
        {
            if (!InRange(ref position))
            {
                Debug.LogError("Out of Range " + position);
                return;
            }

            SetBlockLightNoCheck(ref position, light);
        }

        public void SetBlockLightNoCheck(ref Int3 position, byte light)
        {
            var index = GetIndexGlobal(ref position);
            
            data.lights[index] = light;

            SetDirtyFlag(ref position);
            SetDirty(true);
        }

        protected virtual void SetDirtyFlag(ref Int3 position)
        {
            // TODO: @jian 下面可以优化
            // 1. 使用位运算
            // 2. 缓存Bounds信息，不要每次计算
            var w = position.x == this.position.x;
            var e = position.x == this.position.x + size.x - 1;
            var s = position.z == this.position.z;
            var n = position.z == this.position.z + size.z - 1;
            var d = position.y == this.position.y;
            var u = position.y == this.position.y + size.y - 1;

            dirtyFlag |= DirtyFlag.GetDirtyMask(n, e, s, w, u, d);
        }
        
        protected virtual void GlobalToLocal(ref Int3 position)
        {
            position.x -= this.position.x;
            position.y -= this.position.y;
            position.z -= this.position.z;
        }

        protected virtual bool InRange(ref Int3 position)
        {
            return position.x >= 0 &&
                   position.x < size.x &&
                   position.y >= 0 &&
                   position.y < size.y &&
                   position.z >= 0 &&
                   position.z < size.z;
        }
		
        protected virtual int GetIndexLocal(ref Int3 localPosition)
        {
#if VOXEL_DEBUG
            Assert.IsTrue(InRange(localPosition));
#endif            
            return  (localPosition.y * size.x + localPosition.x) * size.z + localPosition.z;
        }

        protected virtual int GetIndexGlobal(ref Int3 position)
        {
            var x = position.x - this.position.x;
            var y = position.y - this.position.y;
            var z = position.z - this.position.z;
            
#if VOXEL_DEBUG
            Assert.IsTrue(InRange(localPosition));
#endif            
            return  (y * size.x + x) * size.z + z;
        }
        
        [IgnoreMember]
        public bool isRenderDirty { get; private set; }

        public void Render()
        {
            isRenderDirty = false;
        }

        public void SetDirty(bool isDirty)
        {
            isRenderDirty = isDirty;
        }

        public void ClearDirtyFlag()
        {
            dirtyFlag = 0;
        }
        
        // -------------------------
        // CopyTo
        // -------------------------
        public void CopyTo(Int3 sourcePosition, Int3 size, IVolume volume, Int3 destPosition)
        {
            // TODO: @jian use unsafe to disable bounds check
            var from = sourcePosition;
            var to = destPosition;
            
            for (var y = 0; y < size.y; y++)
            {
                for (var x = 0; x < size.x; x++)
                {
                    for (var z = 0; z < size.z; z++)
                    {
                        from.x = sourcePosition.x + x;
                        from.y = sourcePosition.y + y;
                        from.z = sourcePosition.z + z;
                        
                        to.x = destPosition.x + x;
                        to.y = destPosition.y + y;
                        to.z = destPosition.z + z;
                        
                        volume.SetBlock(ref to, GetBlock(ref from));
                    }
                }
            }
        }
            
    }
}