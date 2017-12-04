using System.Runtime.Serialization.Formatters;
using UniRx;
using UnityEngine;

namespace Vox.Render
{
    [RequireComponent(typeof(VolumeAccessor))]
    public class ChunkedVolumeRenderer : MonoBehaviour
    {        
        private VolumeAccessor volumeAccessor;
        private ChunkedVolume _volume;


        void Awake()
        {
            volumeAccessor = GetComponent<VolumeAccessor>();
            volumeAccessor.OnChanged += OnVolumeChanged;
        }

        private void OnVolumeChanged()
        {
            if (_volume != null)
            {
                _volume.onCreateChunk -= OnCreateChunk;
                _volume.onRemoveChunk -= OnRemoveChunk;
            }
            
            RemoveChildren();

            _volume = volumeAccessor.volume as ChunkedVolume;

            if (_volume != null)
            {
                _volume.onCreateChunk += OnCreateChunk;
                _volume.onRemoveChunk += OnRemoveChunk;
                InitializeChildren();
            }                
        }

        private void InitializeChildren()
        {
            // TODO @jian 这里要抽象出来
            if (_volume is LargeVolume)
            {
                foreach (var chunk in (_volume as LargeVolume).chunks.Values)
                {
                    OnCreateChunk(chunk);
                }    
            }            
        }

        private void OnCreateChunk(Chunk chunk)
        {
            UniRx.Observable.Start(() =>
            {

            }).ObserveOnMainThread().Subscribe(xs =>
            {
                var volumeAccessor = VolumeFactory.Instance.CreateChunk(chunk);
                volumeAccessor.name = chunk.ToString();
                volumeAccessor.transform.parent = transform;
                volumeAccessor.transform.localPosition = (Vector3)chunk.position;
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