using UniRx;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Vox.Render
{
    [RequireComponent(typeof(MeshFilter))]
    public class VolumeRenderer : MonoBehaviour
    {
        private IVolume _volume;
        
        public IVolume volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                isDirty = true;
            }
        }
        
        public bool isDirty { get; private set; } 
        public bool isBuilding { get; private set; }

        private MeshFilter meshFilter;
        private MeshCollider meshCollider;
        private VoxelEngineContext context;
        
        private void Start()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshCollider = GetComponent<MeshCollider>();
            context = VoxelEngineContext.Default;
        }

        public void Update()
        {
            if (volume is IRenderable)
            {
                if (volume.destroyed)
                {
                    Destroy(gameObject);
                    return;
                }
                
                var renderable = volume as IRenderable;                
                if (!isBuilding && (renderable.isRenderDirty || isDirty))
                {
                    var task = new VolumeBuildTask();
                    task.SetVolume(volume, context);

                    UniRx.Observable.Start(() =>
                    {                        
                        context.meshBuilder.Build(task);
                        isBuilding = true;

                    }).ObserveOnMainThread().Subscribe(xs =>
                    {
//                        VolumeRendererManager.Instance.QueueTask(task, () =>
//                        {
                            Mesh mesh = task.ToMesh();
                            meshFilter.mesh = mesh;

                            if (meshCollider != null)
                            {
                                meshCollider.sharedMesh = mesh;
                            }
                            isBuilding = false;
                            isDirty = false;
                            renderable.Render();    
//                        });                        
                    });
                }
            }            
        }
    }
}