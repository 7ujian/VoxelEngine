using Vox.Render;

namespace Vox
{
    public class VoxelEngineContext
    {
        public BlockManager blockManager { get; private set; }
        public VolumeMeshBuilder meshBuilder { get; private set; }

        public VoxelEngineContext(BlockManager blockManager, VolumeMeshBuilder meshBuilder)
        {
            this.blockManager = blockManager;
            this.meshBuilder = meshBuilder;
        }

        private static VoxelEngineContext _Default;
        
        public static VoxelEngineContext Default
        {
            get
            {
                if (_Default == null)
                {
                    _Default = new VoxelEngineContext(
                        new BlockManager(), new VolumeMeshBuilder());
                    _Default.blockManager.Initialize();
                }

                return _Default;
            }
        }

    }
}