using System;
using UnityEngine;

namespace Vox
{
    public class BlockStone : BlockController
    {
        public override string name { get { return "Stone"; }}
        
        public override bool isSolid {get { return true; }}
        public override Color32 color {get {return new Color32(127,127,127,255);}}
    }
}