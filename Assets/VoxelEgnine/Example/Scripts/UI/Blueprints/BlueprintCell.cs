using UnityEngine;
using UnityEngine.UI;

public class BlueprintCell : CellView<string>
{
    public Text nameText;
    
    protected override void UpdateView()
    {
        nameText.text = item;
    }

    public void OnClick()
    {
        SendMessageUpwards("OnCellClick", item, SendMessageOptions.DontRequireReceiver);        
    }
}