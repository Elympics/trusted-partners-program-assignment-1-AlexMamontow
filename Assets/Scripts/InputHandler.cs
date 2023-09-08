using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
	[SerializeField]
	private Camera mainCamera;
	[SerializeField]
	private LayerMask mouseRaycastMask;

	private GatheredInput gatheredInput;

	public void UpdateInput()
	{
		gatheredInput.mousePosition = GetWorldMousePosition();
		gatheredInput.movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		gatheredInput.attack = Input.GetKey(KeyCode.Mouse0) || gatheredInput.attack;
		//gatheredInput.block = Input.GetKey(KeyCode.Mouse1) || gatheredInput.block;
	}

	public GatheredInput GetInput()
	{
		GatheredInput returnedInput = gatheredInput;
		gatheredInput.movementInput = Vector2.zero;
		gatheredInput.mousePosition = transform.position + transform.forward;
		gatheredInput.attack = false;
		//gatheredInput.block = false;

		return returnedInput;
	}

	private Vector3 GetWorldMousePosition()
	{
		Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out var hit, 1000f, mouseRaycastMask))
		{
			return hit.point;
		}
		else
		{
			return transform.position + transform.forward;
		}
	}
}

public struct GatheredInput
{
	public Vector2 movementInput;
	public Vector3 mousePosition;
	public bool attack;
	//public bool block;
}