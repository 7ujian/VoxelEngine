using System.IO;
using MessagePack;
using MessagePack.Formatters;

namespace Vox
{
    public class LargetVolumeFormattor<T>: IMessagePackFormatter<LargeVolume>
    {
            public int Serialize(ref byte[] bytes, int offset, LargeVolume value, IFormatterResolver formatterResolver)
            {
                if (value == null)
                {                    
                    return MessagePackBinary.WriteNil(ref bytes, offset);
                }

                return 0;
            }

            public LargeVolume Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
            {
                if (MessagePackBinary.IsNil(bytes, offset))
                {
                    readSize = 1;
                    return null;
                }

                var path = MessagePackBinary.ReadString(bytes, offset, out readSize);
                return null;
            }
        
    }
}