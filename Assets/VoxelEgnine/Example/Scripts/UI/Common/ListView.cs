using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListView<T> : MonoBehaviour
{	
	public GameObject cellPrefab;
	private List<CellView<T>> cells = new List<CellView<T>>();
	private List<T> _items;
	
	public List<T> items
	{
		get { return _items; }
		set
		{
			_items = value;
			
			UpdateView();
		}
	}

	private void UpdateView()
	{
		if (_items == null)
		{
			foreach (var cell in cells)
			{
				cell.gameObject.SetActive(false);
			}

			return;
		}

		if (_items.Count > cells.Count)
		{
			var count = _items.Count - cells.Count;
			for (var i = 0; i < count; i++)
			{
				var cell = Instantiate(cellPrefab, transform).GetComponent<CellView<T>>();				
				cells.Add(cell);
			}
		}

		for (var i = 0; i < cells.Count; i++)
		{
			if (i < _items.Count)
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
