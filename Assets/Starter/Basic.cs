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

	public float movementSmoothdamp;
	public bool isWalking;

	public float verticalSpeed;
	private float targetVerticalSpeed;
	private float verticalSpeedVelocity;

	public float horizontalSpeed;
	private float targetHorizontalSpeed;
	private float horizontalSpeedVelocity;

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

		if (isTargetMode)
		{
			if (input_Movement.y > 0)
			{
				targetVerticalSpeed = (isWalking ? settings.WalkingSpeed : settings.RunningSpeed);
			}
			else
			{
				targetVerticalSpeed = (isWalking ? settings.WalkingBackwardSpeed : settings.RunningBackwardSpeed);
			}

			targetVerticalSpeed = targetVerticalSpeed * input_Movement.y * Time.deltaTime;
			targetHorizontalSpeed = (isWalking ? settings.WalkingStrafindSpeed : settings.RunningStrafingSpeed)  * input_Movement.x * Time.deltaTime;

		}
        else
        {
			var originalRotation = transform.rotation;  //Save original Rotation
			transform.LookAt(playerMovement + transform.position, Vector3.up);  //Transforms rotation
			var newRotation = transform.rotation;  //Transformed rotation is saved 
			transform.rotation = Quaternion.Lerp(originalRotation, newRotation, settings.CharacterRotationSmoothdamp);  //Transforms original rotation to new based on settings value found in Models script

			targetVerticalSpeed = (isWalking ? settings.WalkingSpeed : settings.RunningSpeed) * input_Movement.y * Time.deltaTime;
			targetHorizontalSpeed = (isWalking ? settings.WalkingSpeed : settings.RunningSpeed) * input_Movement.x * Time.deltaTime;
		}


		verticalSpeed = Mathf.SmoothDamp(verticalSpeed, targetVerticalSpeed, ref verticalSpeedVelocity, movementSmoothdamp);
		horizontalSpeed = Mathf.SmoothDamp(horizontalSpeed, targetHorizontalSpeed, ref horizontalSpeedVelocity, movementSmoothdamp);

		playerMovement = cameraController.transform.forward * verticalSpeed;
		playerMovement += cameraController.transform.right * horizontalSpeed;

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
