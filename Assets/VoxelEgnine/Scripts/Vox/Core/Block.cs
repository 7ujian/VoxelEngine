namespace Vox
{
    public struct Block
    {
        public byte id;
        public byte property;
        public byte light;

        public Block(byte id)
        {
            this.id = id;
            this.property = 0;
            this.light = 0;
        }
        
        public static Block Air = new Block(0);
        public static Block Stone = new Block(1);
        public static Block Void = new Block(255);
    }
}