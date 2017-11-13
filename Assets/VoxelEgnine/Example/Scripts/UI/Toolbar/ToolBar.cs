using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ToolBar : MonoBehaviour
{
	public ToggleGroup toggleGroup;
	public ToolCell cellPrefab;
	public List<ToolCell> cells;

	public Item selectedItem;

	private Item[] _items;

	public Item[] items
	{
		get { return _items; }
		set
		{
			_items = value;
			UpdateCells();
		}
	}
	
	public void OnSelectItem(Item item)
	{
		this.selectedItem = item;
		
		// TODO: 将这部分代码移到 数据层
		FindObjectOfType<BlockMan>().itemInHand = item;
	}
	
	private void UpdateCells()
	{
		if (_items == null)
		{
			foreach (var cell in cells)
			{
				cell.gameObject.SetActive(false);
			}

			return;
		}

		if (_items.Length > cells.Count)
		{
			var count = _items.Length - cells.Count;
			for (var i = 0; i < count; i++)
			{
				var cell = Instantiate(cellPrefab, transform);				
				cells.Add(cell);
			}
		}

		for (var i = 0; i < cells.Count; i++)
		{
			if (i < _items.Length)
			{
				cells[i].gameObject.SetActive(true);
				cells[i].item = _items[i];
			}
			else
			{
				cells[i].gameObject.SetActive(false);
			}
		}		
	}
}
