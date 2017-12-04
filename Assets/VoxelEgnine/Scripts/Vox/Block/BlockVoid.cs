namespace Vox
{
    public class BlockVoid : BlockController
    {
        public override string name { get { return "Void"; }}
        
        public override bool isSolid {get { return false; }}
    }
}