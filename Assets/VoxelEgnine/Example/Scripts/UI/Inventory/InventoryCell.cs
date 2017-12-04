using UnityEngine.UI;
using Vox;

namespace Blockmon
{
    public class InventoryCell:CellView<BlockController>
    {
        public Text nameText;
        
        protected override void UpdateView()
        {
            nameText.text = item.name;
        }
    }
}