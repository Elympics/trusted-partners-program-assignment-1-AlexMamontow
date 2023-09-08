using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookOnCamera : MonoBehaviour
{
	[SerializeField]
	private Camera targetCamera;
	[SerializeField]
	private RectTransform targetTransform;

	private void Update()
	{
		targetTransform.LookAt(targetCamera.transform.position);
	}
}
