using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ToolBar : MonoBehaviour
{
	public ToggleGroup toggleGroup;
	public List<ToolCell> cells;

	public Item selectedItem;
	
	public void OnSelectItem(Item item)
	{
		this.selectedItem = item;
		
		// TODO: 将这部分代码移到 数据层
		FindObjectOfType<BlockMan>().isDig = item != null && item.canDig;
		FindObjectOfType<BlockMan>().itemInHand = item;
	}
}
