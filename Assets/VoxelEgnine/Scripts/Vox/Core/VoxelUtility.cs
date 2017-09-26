
namespace Vox
{
    public static class VoxelUtility
    {
        public static void NorthOf(ref Int3 position, ref Int3 outPosition)
        {
            outPosition.x = position.x;
            outPosition.y = position.y;
            outPosition.z = position.z + 1;
        }
        
        public static void EastOf(ref Int3 position, ref Int3 outPosition)
        {
            outPosition.x = position.x + 1;
            outPosition.y = position.y;
            outPosition.z = position.z;
        }
        
        public static void SouthOf(ref Int3 position, ref Int3 outPosition)
        {
            outPosition.x = position.x;
            outPosition.y = position.y;
            outPosition.z = position.z - 1;
        }
        
        public static void WestOf(ref Int3 position, ref Int3 outPosition)
        {
            outPosition.x = position.x - 1;
            outPosition.y = position.y;
            outPosition.z = position.z;
        }
        
        public static void UpOf(ref Int3 position, ref Int3 outPosition)
        {
            outPosition.x = position.x;
            outPosition.y = position.y + 1;
            outPosition.z = position.z;
        }
        
        public static void DownOf(ref Int3 position, ref Int3 outPosition)
        {
            outPosition.x = position.x;
            outPosition.y = position.y - 1;
            outPosition.z = position.z;
        }
        
    }
}