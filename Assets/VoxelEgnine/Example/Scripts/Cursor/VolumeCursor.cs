using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VolumeCursor : MonoBehaviour
{

	private MeshFilter meshFilter;
	private Vector3 _size = Vector3.zero;
	private Vector3 _pivot = Vector3.zero;
	private float _edge;

	public float edge;
	
	void Start()
	{
		meshFilter = GetComponent<MeshFilter>();
	}
	
	public void SetSize(Vector3 pivot, Vector3 size)
	{   
		if (_size == size && _pivot == pivot && edge == _edge)
			return;
		
		if (meshFilter != null)
			meshFilter.sharedMesh = NineScaleCube.GenerateMesh(pivot, size, edge);
		
		_size = size;
		_pivot = pivot;
		_edge = edge;
	}

	void OnGUI()
	{
		if (GUILayout.Button("Test"))
		{
			SetSize(new Vector3(5, 5, 5), new Vector3(10, 10, 10) );
		}
	}

}
