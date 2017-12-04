using JetBrains.Annotations;

namespace Vox
{
    public class BlockAir : BlockController
    {   
        public override string name { get { return "Air"; }}
        public override bool canDig {get { return true; }}        
    }
}