using System.Runtime.Serialization.Formatters;
using UniRx;
using UnityEngine;

namespace Vox.Render
{
    public class ChunkedVolumeRenderer : MonoBehaviour
    {
        public Material material;
        
        private ChunkedVolume _volume;

        public ChunkedVolume volume
        {
            get { return _volume; }
            set
            {
                if (_volume == value)
                    return;

                if (_volume != null)
                {
                    _volume.onCreateChunk -= OnCreateChunk;
                    _volume.onRemoveChunk -= OnRemoveChunk;
                }

                RemoveChildren();

                _volume = value;

                if (_volume != null)
                {
                    _volume.onCreateChunk += OnCreateChunk;
                    _volume.onRemoveChunk += OnRemoveChunk;
                }
                    
            }
        }

        private void OnCreateChunk(Chunk chunk)
        {
            UniRx.Observable.Start(() =>
            {

            }).ObserveOnMainThread().Subscribe(xs =>
            {
                // TODO: @jian 这里考虑以后读取Prefab            
                var go = new GameObject(chunk.ToString());
                go.AddComponent<MeshFilter>();
                go.AddComponent<MeshRenderer>().sharedMaterial = material;
                go.AddComponent<VolumeRenderer>().volume = chunk;
                // TODO: @jian 这里把Behaviour放在Chunk上，是否在LargeVolume上也需要？
                go.AddComponent<VolumeBehaviour>().volume = chunk;
                go.AddComponent<MeshCollider>();

                go.transform.parent = transform;
                go.transform.localPosition = chunk.position;
            });
        }

        private void OnRemoveChunk(Chunk chunk)
        {
            // TODO: @jian 处理Chunk移除
        }

        private void RemoveChildren()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

    }
}