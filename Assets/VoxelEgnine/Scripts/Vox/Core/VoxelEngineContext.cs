using Vox.Render;

namespace Vox
{
    public class VoxelEngineContext
    {
        public BlockManager blockManager { get; private set; }
        public VolumeBuilder volumeBuilder { get; private set; }        
        public ModelManager modelManager { get; private set; }

        public VoxelEngineContext(BlockManager blockManager, VolumeBuilder volumeBuilder, ModelManager modelManager)
        {
            this.blockManager = blockManager;
            this.volumeBuilder = volumeBuilder;
            this.modelManager = modelManager;
        }

        private static VoxelEngineContext _Default;
        
        public static VoxelEngineContext Default
        {
            get
            {
                if (_Default == null)
                {
                    _Default = new VoxelEngineContext(
                        new BlockManager(), new VolumeMeshBuilder(), new ModelManager());
                    _Default.blockManager.Initialize();
                }

                return _Default;
            }
        }

    }
}