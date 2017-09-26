using Vox;

namespace VoxelEgnine.Scripts.Vox.Generator
{
    public class RandomGenerator :IVolumeGenerator
    {
        public void Generate(IVolume volume)
        {
            var size = volume.size;
            var volumePosition = volume.position;
            var block = new Block();
            Int3 position;

            for (var x = 0; x < size.x; x++)
            {
                position.x = volumePosition.x + x; 
                for (var z = 0; z < size.z; z++)
                {
                    position.z = volumePosition.z + z;
                    for (var y = 0; y < size.y; y++)
                    {
                        position.y = volumePosition.y + y;
                        
                        block.id = (byte)UnityEngine.Random.Range(0, 2); 
                        volume.SetBlock(ref position, block);
                    }
                }
            }
        }
    }
}