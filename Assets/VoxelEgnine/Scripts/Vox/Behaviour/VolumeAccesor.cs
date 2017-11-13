using UnityEngine;

namespace Vox
{
    public class VolumeBehaviour : MonoBehaviour
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
    }
}