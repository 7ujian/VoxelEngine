using UnityEngine;
using Vox.Render;

namespace Vox
{
    public class VolumeFactory:MonoBehaviour
    {
        private static VolumeFactory __Instance;
        
        public static VolumeFactory Instance
        {
            get
            {
                if (__Instance == null)
                {
                    var go = new GameObject("VolumeFactory");
                    __Instance = go.AddComponent<VolumeFactory>();
                }

                return __Instance;
            }
        }

        void Awake()
        {
            if (__Instance != null)
                throw new System.Exception("Singleton Error!");

            __Instance = this;
        }

        public Material volumeMaterial;
        
        public VolumeAccessor CreateSimpleVolume()
        {
            var go = new GameObject();
            var accessor =  go.AddComponent<VolumeAccessor>();
            go.AddComponent<MeshCollider>();
            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>().material = volumeMaterial;            
            go.AddComponent<VolumeRenderer>();
            go.AddComponent<VolumeSimulator>();

            return accessor;
        }

        public VolumeAccessor CreateLargeVolume()
        {
            var go = new GameObject();
            
            var volumeAccessor = go.AddComponent<VolumeAccessor>();
            go.AddComponent<ChunkedVolumeRenderer>();
            go.AddComponent<VolumeSimulator>();

            return volumeAccessor;
        }

        public VolumeAccessor CreateChunk(IVolume volume = null)
        {
            var go = new GameObject();
            var volumeAccessor = go.AddComponent<VolumeAccessor>();
            volumeAccessor.volume = volume;
            
            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>().sharedMaterial = volumeMaterial;
            go.AddComponent<VolumeRenderer>();
            // TODO: @jian 这里把Behaviour放在Chunk上，是否在LargeVolume上也需要？
            go.AddComponent<VolumeSimulator>();
            go.AddComponent<MeshCollider>();

            return volumeAccessor;

        }
    }
}