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
	PlayerAnimation playerAnim;

	bool isGrounded;
	bool playerJumped;
	bool canDoubleJump;
	bool pressedSpace;
	bool gameStarted;

	private void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		playerAnim = GetComponent<PlayerAnimation>();
		Invoke("StartGame", 1);
	}

	private void Update()
	{
		if (!gameStarted) return;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			pressedSpace = true;
		}
	}

	private void FixedUpdate()
	{
		if (!gameStarted) return;

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

		if (isGrounded && playerJumped)
		{
			playerJumped = false;
			playerAnim.DidLand();
		}
	}

	void PlayerJumped()
	{
		if (pressedSpace && isGrounded)
		{
			rigidBody.AddForce(new Vector3(0, firstJumpSpeed, 0));
			playerJumped = true;
			canDoubleJump = true;
			pressedSpace = false;
			playerAnim.DidJump();
		}

		else if(pressedSpace && !isGrounded && canDoubleJump)
		{
			rigidBody.AddForce(new Vector3(0, secondJumpSpeed, 0));
			canDoubleJump = false;
			pressedSpace = false;
			playerAnim.DidJump();
		}
	}

	void StartGame()
	{
		gameStarted = true;
		playerAnim.PlayerRun();
	}
}
