using UnityEngine;

namespace Vox
{
    public class BlockFood:BlockController
    {
        public override string name { get { return "Food"; }}
        public override bool isSolid {get { return false; }}
        public override Color32 color {get { return new Color32(128,0,0,255); }}
    }
}