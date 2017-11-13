using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HUD : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler	
{
	private Vector3 dragStartWorldPosition;

	public float pitch = 0;
	public float yaw = 0;
	public float distance = 20f;
	public Vector3 lookPoint = Vector3.zero;
	public Camera camera;
	public float yawSpeed = 1f;
	public float pitchSpeed = 1f;
	public bool disablePitch = true;

	public bool isDrag = false;
	

	void Start()
	{
		// TODO: @jian 统一的摄像机管理 CameraManager
		camera = Camera.main;
	}
	
	private void UpdateCamera()
	{
		var y = Mathf.Sin(pitch * Mathf.Deg2Rad);
		var r = Mathf.Cos(pitch * Mathf.Deg2Rad);
		
		var direction = new Vector3(Mathf.Cos(yaw * Mathf.Deg2Rad)*r, y, Mathf.Sin(yaw* Mathf.Deg2Rad)*r);
		var cameraPosition = lookPoint + direction * distance;
		camera.transform.position = cameraPosition;
		camera.transform.LookAt(lookPoint);
	}

	void Update()
	{
		// TODO: @jian 不需要每帧都计算
		UpdateCamera();
	}
	
	public void OnBeginDrag(PointerEventData eventData)
	{
		dragStartWorldPosition = Camera.main.ScreenToWorldPoint(eventData.position);
		isDrag = true;
	}
	
	public void OnEndDrag(PointerEventData eventData)
	{
		isDrag = false;
	}

	public void OnDrag(PointerEventData eventData)
	{		
		if (Input.GetMouseButton(0))
		{
			// 计算世界坐标系下摄像机的位移
			var worldPosition = Camera.main.ScreenToWorldPoint(eventData.position);
			var deltaPosition = worldPosition - dragStartWorldPosition;
			lookPoint -= deltaPosition;	
		}
		else if (Input.GetMouseButton(1))
		{
			yaw += eventData.delta.x * yawSpeed;
			if (!disablePitch)
				pitch += eventData.delta.y * pitchSpeed;
			// 计算世界坐标系下
		}
		
	}


	public void OnPointerClick(PointerEventData eventData)
	{
		if (!eventData.dragging)
			FindObjectOfType<BlockMan>().OnClicked(eventData.pressPosition);
	}
}
