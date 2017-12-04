using System.Text;
using UnityEngine;

namespace Vox
{
    public class DirtyFlagTest : MonoBehaviour
    {
        void Start()
        {
            var masks =new DirtyFlagGenerator().Generate();

            var strBuilder = new StringBuilder();
            
            for (var i = 0; i < masks.Length; i++)
            {
                strBuilder.AppendFormat("0x{0:X8},\n", masks[i]);
            }
            
            Debug.Log(strBuilder);
        }        
            
    }
}