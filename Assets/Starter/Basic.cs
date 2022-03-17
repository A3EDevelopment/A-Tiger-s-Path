using System.Collections;
using System.Collections.Generic;
using static Models;
using UnityEngine;

public class Basic : MonoBehaviour
{
	CharacterController characterController;
	BasicPlayerInput obj_BasicPlayerInput;
	public Vector2 input_Movement;
	public Vector2 input_View;

	Vector3 playerMovement;

	public PlayerSettingsModel settings;
	public bool isTargetMode;
	public float flt_JumpingTimer;

	[Header("Camera")]
	public Transform cameraTarget;
	public CameraController cameraController;

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
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

		Movement();
	}

	private void Movement()
    {
		playerMovement = cameraController.transform.forward * (settings.ForwardSpeed * input_Movement.y) * Time.deltaTime;
		playerMovement += cameraController.transform.right * (settings.ForwardSpeed * input_Movement.x) * Time.deltaTime;

		if (!isTargetMode)
        {
			var originalRotation = transform.rotation;  //Save original Rotation
			transform.LookAt(playerMovement + transform.position, Vector3.up);  //Transforms rotation
			var newRotation = transform.rotation;  //Transformed rotation is saved 
			transform.rotation = Quaternion.Lerp(originalRotation, newRotation, settings.CharacterRotationSmoothdamp);  //Transforms original rotation to new based on settings value found in Models script

        }

		characterController.Move(playerMovement);
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
