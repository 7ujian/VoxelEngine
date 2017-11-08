using MessagePack;

namespace Vox
{
    [MessagePackObject]
    public class VolumeData
    {
        [Key(0)] 
        public Int3 size;
        [Key(1)]
        public Int3 position;

        
        [Key(2)]
        public byte[] ids;
        [Key(3)]
        public byte[] properties;
        [Key(4)]
        public byte[] lights;
        [Key(5)]
        public byte[] skylights;

        public VolumeData()
        {            
        }

        public VolumeData(Int3 size)
        {
            this.size = size;
           
            this.position = Int3.Zero;

            var len = size.x * size.y * size.z;
            
            // TODO: 以后从对象池拿到
            ids = new byte[len];
            properties = new byte[len];
            lights = new byte[len];
        }

    }
}