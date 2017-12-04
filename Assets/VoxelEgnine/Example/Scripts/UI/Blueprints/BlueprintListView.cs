using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BlueprintListView : ListView<string>
{
    [Serializable]
    public class BlueprintListEvent:UnityEvent<string>{}
        
    [SerializeField]
    public BlueprintListEvent onCellClick = new BlueprintListEvent();
    
    public void OnCellClick(string filename)
    {        
        onCellClick.Invoke(filename);    
    }
}
