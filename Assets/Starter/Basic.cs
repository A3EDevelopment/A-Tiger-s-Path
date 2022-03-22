using System.Collections;
using System.Collections.Generic;
using static Models;
using UnityEngine;

public class Basic : MonoBehaviour
{

	CharacterController characterController;
	BasicPlayerInput obj_BasicPlayerInput;
	public PlayerSettingsModel settings;
	[Space]
	public float flt_JumpingTimer;

    #region Inputs
    [Header("Player Inputs")]
	public Vector2 input_Movement;
	public Vector2 input_View;
	Vector3 playerMovement;
    #endregion

    #region Modes
    [Header("Modes")]
	public bool isWalking;
	public bool isSprinting;
	public bool isTargetMode;
	#endregion

	#region Speed Values
	[Header("Speed Values")]
	[Range(1,15)]
	public float verticalSpeed;
	private float targetVerticalSpeed;
	private float verticalSpeedVelocity;
	[Range(1, 15)]
	public float horizontalSpeed;
	private float targetHorizontalSpeed;
	private float horizontalSpeedVelocity;
	#endregion

	#region Camera
	[Header("Camera")]
	public Transform cameraTarget;
	public CameraController cameraController;
	public float movementSmoothdamp;
    #endregion

    #region Character Stats
    [Header("Character Stats")]
	public PlayerStatsModel playerStats;
	float playerSpeed;
	#endregion

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
		obj_BasicPlayerInput = new BasicPlayerInput();

		obj_BasicPlayerInput.Movement.Movement.performed += x => input_Movement = x.ReadValue<Vector2>();
		obj_BasicPlayerInput.Movement.View.performed += x => input_View = x.ReadValue<Vector2>();

		obj_BasicPlayerInput.Actions.Jump.performed += x => Jump();
		obj_BasicPlayerInput.Actions.SuperJump.performed += x => SuperJump();

		obj_BasicPlayerInput.Actions.WalkingToggle.performed += x => ToggleWalking();
		obj_BasicPlayerInput.Actions.Sprint.performed += x => Sprint();

		// Movements

	}

	private void Update()
	{
		JumpingTimer();

		Cursor.lockState = CursorLockMode.Locked;

		Movement();
		CalculateSprint();
	}

	#region Character Movement

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

			targetHorizontalSpeed = (isWalking ? settings.WalkingStrafindSpeed : settings.RunningStrafingSpeed);

		}
		else
		{
			var originalRotation = transform.rotation;  //Save original Rotation
			transform.LookAt(playerMovement + transform.position, Vector3.up);  //Transforms rotation
			var newRotation = transform.rotation;  //Transformed rotation is saved 
			transform.rotation = Quaternion.Lerp(originalRotation, newRotation, settings.CharacterRotationSmoothdamp);  //Transforms original rotation to new based on settings value found in Models script

			if (isSprinting)
			{
				playerSpeed = settings.RunningSpeed;
			}
			else
			{
				playerSpeed = (isWalking ? settings.WalkingSpeed : settings.RunningSpeed);
			}

			targetVerticalSpeed = playerSpeed;
			targetHorizontalSpeed = playerSpeed;
		}

		targetVerticalSpeed = targetVerticalSpeed * input_Movement.y * Time.deltaTime;
		targetHorizontalSpeed = targetHorizontalSpeed * input_Movement.x * Time.deltaTime;

		verticalSpeed = Mathf.SmoothDamp(verticalSpeed, targetVerticalSpeed, ref verticalSpeedVelocity, movementSmoothdamp);
		horizontalSpeed = Mathf.SmoothDamp(horizontalSpeed, targetHorizontalSpeed, ref horizontalSpeedVelocity, movementSmoothdamp);

		playerMovement = cameraController.transform.forward * verticalSpeed;
		playerMovement += cameraController.transform.right * horizontalSpeed;

		characterController.Move(playerMovement);
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

	private void ToggleWalking()
    {
		isWalking = !isWalking;
    }

	private void Sprint()
    {
		if (playerStats.Stamina > (playerStats.MaxStamina / 4))
        {
			isSprinting = true;
        }
    }

	private void CalculateSprint()
	{
		if (isSprinting)
		{
			if (playerStats.Stamina > 0)
			{
				playerStats.Stamina -= playerStats.StaminaDrain * Time.deltaTime;
			}
			else
			{
				isSprinting = false;
			}
		}
		else
		{
			if (playerStats.Stamina < playerStats.MaxStamina)
			{
				playerStats.Stamina += playerStats.StaminaRecovery * Time.deltaTime;
			}
			else
			{
				playerStats.Stamina = playerStats.MaxStamina;
			}
		}
	}

	#endregion

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
