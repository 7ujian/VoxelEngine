using UnityEngine;

namespace Vox
{
    public class BlockJoint:BlockController
    {
        public override string name { get { return "Joint"; }}
        public override bool isSolid {get { return false; }}
        public override Color32 color {get { return new Color32(0,64,200,255); }}
        public override bool updateEnabled {get { return false; }}

        public override void BeforePlace(IVolume volume, ref Int3 position, ref Block block)
        {            
        }

        public override void AfterPlace(IVolume volume, ref Int3 position, ref Block block)
        {                                    
        }

        public override void Update(IVolume volume, Int3 position, BlockUpdateTask task, float deltaTime)
        {            
        }
    }
}