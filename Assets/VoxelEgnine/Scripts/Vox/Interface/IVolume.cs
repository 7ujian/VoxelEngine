using System;
using System.Runtime.CompilerServices;

namespace Vox
{
    public interface IVolume
    {

        Action<Int3, byte> OnBlockAdd { get; set; }
        Action<Int3, byte> OnBlockRemove { get; set; }
        
        byte GetBlockId(ref Int3 position);
        byte GetBlockProperty(ref Int3 position);
        byte GetBlockLight(ref Int3 position);
        Block GetBlock(ref Int3 position);
        
        void SetBlock(ref Int3 position, Block block);
        void SetBlockProperty(ref Int3 position, byte property);
        void SetBlockLight(ref Int3 position, byte light);

        void CopyTo(Int3 sourcePosition, Int3 size, IVolume volume, Int3 destPosition);
        
        Int3 size { get; }
        Int3 position { get; set; }

        // TODO: 增加 willRender
        bool destroyed { get; }
    }
}