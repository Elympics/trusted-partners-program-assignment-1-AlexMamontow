using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
	[SerializeField]
	private Rigidbody playerRigidbody;
	[SerializeField]
	private Animator animator;
	[SerializeField]
	private float movementSpeed;

	public void HanfleMovement(Vector2 movementInput, Vector3 mousePosition)
	{
		movementInput.Normalize();
		movementInput *= movementSpeed;

		//option 1
		//var vertical = transform.forward * movementInput.y;
		//var horizontal = transform.right * movementInput.x;
		//playerRigidbody.velocity = horizontal + vertical;

		//option 2
		playerRigidbody.velocity = new Vector3(movementInput.x, 0f, movementInput.y);

		mousePosition.y = playerRigidbody.position.y;
		playerRigidbody.MoveRotation(Quaternion.LookRotation(mousePosition - playerRigidbody.position));

		animator.SetFloat("Movement", movementInput.magnitude);
	}
}
