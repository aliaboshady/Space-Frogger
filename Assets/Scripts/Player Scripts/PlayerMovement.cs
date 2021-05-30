using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float firstJumpSpeed = 10f;
    [SerializeField] float secondJumpSpeed = 10f;
    [SerializeField] float radius = 0.3f;
	[SerializeField] Transform groundCheckPosition;
	[SerializeField] LayerMask layerGround;

	Rigidbody rigidBody;
	bool isGrounded;
	bool canDoubleJump;
	bool pressedSpace;

	private void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			pressedSpace = true;
		}
		else if (Input.GetKeyUp(KeyCode.Space))
		{
			pressedSpace = false;
		}
	}

	private void FixedUpdate()
	{
		PlayerMove();
		PlayerGrounded();
		PlayerJumped();
	}

	void PlayerMove()
	{
		rigidBody.velocity = new Vector3(movementSpeed, rigidBody.velocity.y, 0);
	}

	void PlayerGrounded()
	{
		isGrounded = Physics.OverlapSphere(groundCheckPosition.position, radius, layerGround).Length > 0;
	}

	void PlayerJumped()
	{
		if (pressedSpace && isGrounded)
		{
			rigidBody.AddForce(new Vector3(0, firstJumpSpeed, 0));
		}
	}
}
