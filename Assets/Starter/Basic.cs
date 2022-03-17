using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : MonoBehaviour
{

	BasicPlayerInput obj_BasicPlayerInput;
	public Vector2 input_Movement;
	public Vector2 input_View;

	public Transform cameraTarget;

	public bool isTargetMode;

	public float flt_JumpingTimer;

	private void Awake()
	{
		obj_BasicPlayerInput = new BasicPlayerInput();

		obj_BasicPlayerInput.Movement.Movement.performed += x => input_Movement = x.ReadValue<Vector2>();
		obj_BasicPlayerInput.Movement.View.performed += x => input_View = x.ReadValue<Vector2>();

		obj_BasicPlayerInput.Actions.Jump.performed += x => Jump();
		obj_BasicPlayerInput.Actions.SuperJump.performed += x => SuperJump();

		// Move

	}

	private void JumpingTimer()
	{
		if (flt_JumpingTimer >= 0)
		{
			flt_JumpingTimer -= Time.deltaTime;
		}
	}

	private void Jump()
	{
		if (flt_JumpingTimer <= 0)
		{
			flt_JumpingTimer = 0.4f;
			return;
		}

		Debug.Log("I'm Jumping");
	}

		private void SuperJump()
	{
		Debug.Log("I'm Super Jumping");
	}

	private void Update()
	{
		JumpingTimer();

		Cursor.lockState = CursorLockMode.Locked;
	}

	#region Enable/Disable


	private void OnEnable()
	{
		obj_BasicPlayerInput.Enable(); 
	}

	private void OnDisable()
	{
		obj_BasicPlayerInput.Disable();
	}
	#endregion

}
