
using UniRx;

namespace Vox
{
    public static class VoxelUtility
    {
        public static int[] Directions =
        {
            0,
            1,
            2,
            3,
            4,
            5,
        };
	
        private static int[] OppositeDirections =
        {
            2,
            3,
            0,
            1,
            5,
            4,
        };
        
        public static int GetOppositeDirection(int direction)
        {
            return OppositeDirections[direction];
        }
        
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

        private static Int3[] _DirectionToInt3 = new[]
        {
            new Int3(0, 0, 1),
            new Int3(1, 0, 0),
            new Int3(0, 0, -1),
            new Int3(-1, 0, 0),
            new Int3(0, 1, 0),
            new Int3(0, -1, 0),
        };

        public static Int3 DirectionToInt3(int direction)
        {
            return _DirectionToInt3[direction];
        }

        public static void Plus(ref Int3 a, ref Int3 b)
        {
            a.x += b.x;
            a.y += b.y;
            a.z += b.z;
        }


    }
}