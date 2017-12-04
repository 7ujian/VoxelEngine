using System;
using UnityEngine;

namespace Vox
{
    public class VolumeAccessor : MonoBehaviour
    {
        public Action OnChanged;
        
        private IVolume _volume;
        
        public IVolume volume
        {
            get { return _volume; }
            set
            {
                if (_volume == value)
                    return;
                
                _volume = value;
                isDirty = true;

                if (OnChanged != null)
                    OnChanged();
            }
        }
        
        public bool isDirty { get; set; } 
    }
}