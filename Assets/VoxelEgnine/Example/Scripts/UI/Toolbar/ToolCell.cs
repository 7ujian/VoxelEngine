using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToolCell : MonoBehaviour
{
    [System.Serializable]
    public class ItemEvent : UnityEvent<Item>
    {
    }

    public Text nameText;

    private Item _item;

    public Item item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item != null)
            {
                nameText.text = _item.id.ToString();
            }
            else
            {
                nameText.text = "";
            }
        }
    }
    public ItemEvent onSelectItem;


    public void OnToggleChange(bool isOn)
    {
        if (isOn)
            onSelectItem.Invoke(item);
    }
}
