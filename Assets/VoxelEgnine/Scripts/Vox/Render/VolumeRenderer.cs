using UniRx;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Vox.Render
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(VolumeAccessor))]
    public class VolumeRenderer : MonoBehaviour
    {       
        public bool isBuilding { get; private set; }

        public IVolume volume
        {
            get
            {
                if (volumeAccessor == null)
                    return null;
                return volumeAccessor.volume;
            }
        }

        private MeshFilter meshFilter;
        private MeshCollider meshCollider;
        private VolumeAccessor volumeAccessor;
        private VoxelEngineContext context;
        
        private void Start()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshCollider = GetComponent<MeshCollider>();
            volumeAccessor = GetComponent<VolumeAccessor>();
            context = VoxelEngineContext.Default;
        }

        public void Update()
        {
            if (volumeAccessor == null || volumeAccessor.volume == null)
                return;

            var volume = volumeAccessor.volume;
            
            if (volume is IRenderable)
            {
                if (volume.destroyed)
                {
                    Destroy(gameObject);
                    return;
                }
                
                var renderable = volume as IRenderable;                
                if (!isBuilding && (renderable.isRenderDirty || volumeAccessor.isDirty))
                {
                    var task = new VolumeBuildTask();
                    task.SetVolume(volume, context);
                    isBuilding = true;

                    UniRx.Observable.Start(() =>
                    {                        
                        context.volumeBuilder.BuildMeshData(task);                        

                    }).ObserveOnMainThread().Subscribe(xs =>
                    {
                        // Models
                        context.volumeBuilder.BuildModels(this, volume, context);
                         
                        // Volume Mesh
                        Mesh mesh = task.ToMesh();
                        meshFilter.mesh = mesh;

                        if (meshCollider != null)
                        {
                            meshCollider.sharedMesh = mesh;
                        }
                        isBuilding = false;
                        volumeAccessor.isDirty = false;
                        renderable.Render();
                    });
                }
            }            
        }
    }
}