using UnityEngine;

namespace Vox
{
    public class BlockSnakeBody:BlockController
    {
        public override string name { get { return "SnakeBody"; }}
        
        public override bool isSolid {get { return false; }}
        public override Color32 color {get { return new Color32(255,255,255,255); }}
                
    }
}