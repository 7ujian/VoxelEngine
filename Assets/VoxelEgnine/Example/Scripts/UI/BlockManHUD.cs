using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManHUD : MonoBehaviour
{
	public BlockMan blockMan;

	public void OnClickSaveButton()
	{
		if (blockMan != null)
		{
			blockMan.Save();
		}
	}

	public void OnClickLoadButton()
	{
		if (blockMan != null)
		{
			blockMan.Load();
		}
	}
}
