using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellView<T> : MonoBehaviour
{

	private T _item;

	public T item
	{
		get
		{
			return _item;
		}
		set
		{
			_item = value;
			
			UpdateView();
		}
	}

	protected virtual void UpdateView()
	{		
	}
}
