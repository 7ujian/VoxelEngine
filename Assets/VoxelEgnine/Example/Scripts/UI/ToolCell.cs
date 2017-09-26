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

    public Item item;
    public ItemEvent onSelectItem;
    

    public void OnToggleChange(bool isOn)
    {
        if (isOn)
            onSelectItem.Invoke(item);
    }
}
