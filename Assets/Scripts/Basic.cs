using System.Collections;
using System.Collections.Generic;
using static Models;
using UnityEngine;

public class Basic : MonoBehaviour
{
	public MeshRenderer torch;
	public GameObject fire;
	public bool holdingTorch;

	public GameObject[] WindParticles;

	CharacterController characterController;
	Animator characterAnimator;
	BasicPlayerInput obj_BasicPlayerInput;
	public PlayerSettingsModel settings;
	[Space]
	public bool jumpReady;
	public bool isOnGround;
	public bool GlideAfterJump = false;
	public float jumpCD = 3f;
	public float jumpCDCurrent = 0.0f;

	public LayerMask whatIsGround;
	public GameObject groundChecker;

	#region - Inputs -
	[Header("Player Inputs")]
	public Vector2 input_Movement;
	public Vector2 input_View;
	Vector3 playerMovement;
	#endregion

	#region - Modes -
	[Header("Modes")]
	public bool isWalking;
	public bool isSprinting;
	public bool isTargetMode;
	#endregion

	#region - Speed Values -
	[Header("Speed Values")]

	private float verticalSpeed;
	private float targetVerticalSpeed;
	private float verticalSpeedVelocity;

	private float horizontalSpeed;
	private float targetHorizontalSpeed;
	private float horizontalSpeedVelocity;

	public float movementSpeedOffset = 1f;

	public Vector3 relativePlayerVelocity;
	#endregion

	#region - Camera -
	[Header("Camera")]
	public Transform cameraTarget;
	public CameraController cameraController;
	public float movementSmoothdamp = 0.3f;
	#endregion

	#region - Character Stats -
	[Header("Character Stats")]
	public PlayerStatsModel playerStats;
	#endregion

	#region - Gravity Values -
	[Header("Gravity")]
	public float gravity;
	public float currentGravity;
	public float constantGravity;
	public float maxGravity;

	private Vector3 gravityDirection;
	private Vector3 gravityMovement;
	#endregion

	#region - Jumping / Falling / Climbing / Rolling-
	[Header("Jumping / Falling / Rolling")]
	public float fallingSpeed;
	public float fallingThreshold;
	public float fallingSpeedPeak;

	public bool jumpingTriggered;
	public bool fallingTriggered;

	public bool canClimb = false;
	public bool climbing = false;

	public Transform myEndPoint;
	public Transform playerTransform;
	public float speed;

	public bool isDodging;
	float dodgeTimer;
	[SerializeField] AnimationCurve dodgeCurve;

	#endregion

	public bool HardLandTriggered ;

	#region - sounds -

	public AudioSource Footsteps;

	#endregion

	
	void Start()
	{
		Keyframe dodge_lastFrame = dodgeCurve[dodgeCurve.length - 1];
		dodgeTimer = dodge_lastFrame.time;

		WindParticles[0].SetActive(false);
		WindParticles[1].SetActive(false);
		WindParticles[2].SetActive(false);
		
	}

	private void Update()
	{
		//JumpingTimer();


		//Movement();
		CalculateGravity();
		CalculateSprint();
		CanSprint();
		CalculateFalling();
		IsGliding();
		IsClimbing();

		Debug.Log(IsMoving());

		if (IsMoving() == true)
		{
			Footsteps.Play();
		}
		else 
		{
			Footsteps.Stop();
		}

		if (HardLandTriggered == false) Movement();

		if (Input.GetKeyDown(KeyCode.C))
		{
			if(IsMoving()) StartCoroutine(Dodge()); //Dodging is only allowed if the character is moving.
		}

		if (jumpCDCurrent >= jumpCD)
		{
			jumpReady = true;
		}
		else
		{
			jumpCDCurrent = jumpCDCurrent + Time.deltaTime;
			jumpReady = false;
		}

		if (jumpCDCurrent >= 1f)
        {
			GlideAfterJump = true;
        }
        else
        {
			GlideAfterJump = false;
		}

	}

	IEnumerator Dodge()
	{
		characterAnimator.SetTrigger("Dodge");
		isDodging = true;
		float timer = 0;
		if(timer < dodgeTimer)
		{
			float speed = dodgeCurve.Evaluate(timer);
			Vector3 dir = ((transform.forward * speed) + Vector3.up * verticalSpeed); //Direction and Gravity Added;
			characterController.Move(dir * Time.deltaTime);
			timer += Time.deltaTime;
			yield return null;
		}

		isDodging = false;
	}

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
		characterAnimator = GetComponent<Animator>();

		obj_BasicPlayerInput = new BasicPlayerInput();

		obj_BasicPlayerInput.Movement.Movement.performed += x => input_Movement = x.ReadValue<Vector2>();
		obj_BasicPlayerInput.Movement.View.performed += x => input_View = x.ReadValue<Vector2>();

		obj_BasicPlayerInput.Actions.Jump.performed += x => JumpAfterCD();

		obj_BasicPlayerInput.Actions.WalkingToggle.performed += x => ToggleWalking();
		obj_BasicPlayerInput.Actions.Sprint.performed += x => Sprint();

		gravityDirection = Vector3.down;

		// Movements
		Cursor.lockState = CursorLockMode.Locked;
	}

	#region - Jumping -

	private void JumpAfterCD()
	{
		Jump();
	}

	private void Jump()
	{
		if (jumpReady)
		{
			if (jumpingTriggered)
			{
				return;
			}

			if (IsMoving() && !isWalking)
			{
				characterAnimator.SetTrigger("RunningJump");
			}
			else
			{
				characterAnimator.SetTrigger("Jump");
			}

			jumpCDCurrent = 0.0f;
			jumpingTriggered = true;
			fallingTriggered = true;
		}
	}

	public void ApplyJumpForce()
	{
		currentGravity = settings.JumpingForce; //This will be an event in the animation which allows the jump to begin at the right time.
	}

	#endregion

	#region - Gravity -

	private bool IsGrounded()
	{
		return characterController.isGrounded;
	}

	private bool IsFalling()
	{
		if (fallingSpeed < fallingThreshold)
		{
			return true;
		}

		return false;
	}

	private void CalculateGravity()
	{
		if (IsGrounded() && !jumpingTriggered)
		{
			currentGravity = constantGravity;
		}
		else
		{
			if (currentGravity > maxGravity)
			{
				currentGravity -= gravity * Time.deltaTime;
			}
		}

		gravityMovement = gravityDirection * -currentGravity * Time.deltaTime;
	}

	private void CalculateFalling()
	{
		fallingSpeed = relativePlayerVelocity.y;

		if (IsFalling() && fallingSpeed < fallingSpeedPeak)
		{
			fallingSpeedPeak = fallingSpeed;
		}

		if (IsFalling() && !IsGrounded() && !jumpingTriggered && !fallingTriggered)
		{
			fallingTriggered = true;
			characterAnimator.SetTrigger("Falling");
		}

		if (fallingTriggered && IsGrounded() && fallingSpeed < -0.5f)
		{
			fallingTriggered = false;
			jumpingTriggered = false;

			if (fallingSpeedPeak < -7)
			{
				characterAnimator.SetTrigger("HardLand");
				StartCoroutine("DisableMovement");
			}
			else
			{
				characterAnimator.SetTrigger("Land");
			}
			fallingSpeedPeak = 0;
		}
	}

	IEnumerator DisableMovement()
	{
		HardLandTriggered = true;
		Debug.Log("YE");
		yield return new WaitForSeconds(1f);
		Debug.Log("NO");

		HardLandTriggered = false;
	}

	#endregion

	#region - Character Movement -

	private void ToggleWalking()
	{
		isWalking = !isWalking;
	}

	public bool IsMoving()
	{
		if (relativePlayerVelocity.x > 0.01f || relativePlayerVelocity.x < -0.01f)
		{
			return true;
		}

		if (relativePlayerVelocity.z > 0.01f || relativePlayerVelocity.z < -0.01f)
		{
			return true;
		}

		return false;
	}

	private void Sprint()
	{
		if (!CanSprint())
		{
			return;
		}

		if (playerStats.Stamina > (playerStats.MaxStamina / 4))
		{
			isSprinting = true;
		}
	}

	private bool CanSprint()
	{
		if (isTargetMode)
		{
			return false;
		}

		var sprintFalloff = 0.8f;

		if ((input_Movement.y < 0 ? input_Movement.y * -1 : input_Movement.y) < sprintFalloff && (input_Movement.x < 0 ? input_Movement.x * -1 : input_Movement.x) < sprintFalloff)  //Ensures input_Movement.y and x is always positive 
		{
			return false;
		}



		return true;
	}

	private void CalculateSprint()
	{
		if (!CanSprint())
		{
			isSprinting = false;
		}

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

			playerStats.StaminaCurrentDelay = playerStats.StaminaDelay;
		}
		else
		{
			if (playerStats.StaminaCurrentDelay <= 0)
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
			else
			{
				playerStats.StaminaCurrentDelay -= Time.deltaTime;
			}
		}
	}

	private void Movement()
	{
		characterAnimator.SetBool("isTargetMode", isTargetMode);

		relativePlayerVelocity = transform.InverseTransformDirection(characterController.velocity);

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

			float playerSpeed; //To change player speed, value must be changed in inspector.

			if (isSprinting)
			{
				playerSpeed = settings.SprintingSpeed;
			}
			else
			{
				playerSpeed = (isWalking ? settings.WalkingSpeed : settings.RunningSpeed);
			}

			targetVerticalSpeed = playerSpeed;
			targetHorizontalSpeed = playerSpeed;
		}

		targetVerticalSpeed = (targetVerticalSpeed * movementSpeedOffset) * input_Movement.y;  //Calculates speed by getting player input from WASD by speed and time. Forwards and Backwards
		targetHorizontalSpeed = (targetHorizontalSpeed * movementSpeedOffset) * input_Movement.x;  //Calculates speed by getting player input from WASD by speed and time. Left and Right

		verticalSpeed = Mathf.SmoothDamp(verticalSpeed, targetVerticalSpeed, ref verticalSpeedVelocity, movementSmoothdamp);
		horizontalSpeed = Mathf.SmoothDamp(horizontalSpeed, targetHorizontalSpeed, ref horizontalSpeedVelocity, movementSmoothdamp);

		if (isTargetMode)
		{
			characterAnimator.SetFloat("Vertical", verticalSpeed);
			characterAnimator.SetFloat("Horizontal", horizontalSpeed);
		}
		else
		{
			//Checks if this the input is negative and turns into positive.
			float verticalActualSpeed = verticalSpeed < 0 ? verticalSpeed * -1 : verticalSpeed;
			float horizontalActualSpeed = horizontalSpeed < 0 ? horizontalSpeed * -1 : horizontalSpeed;
			// Both camera modes affect the player speed differently. We need the actual speed.

			float animatorVertical = verticalActualSpeed > horizontalActualSpeed ? verticalActualSpeed : horizontalActualSpeed;

			characterAnimator.SetFloat("Vertical", animatorVertical);
		}






		playerMovement = cameraController.transform.forward * verticalSpeed * Time.deltaTime;
		playerMovement += cameraController.transform.right * horizontalSpeed * Time.deltaTime;

		characterController.Move(playerMovement + gravityMovement);
	}

	#endregion

	#region - Enable/Disable -


	private void OnEnable()
	{
		obj_BasicPlayerInput.Enable();
	}

	private void OnDisable()
	{
		obj_BasicPlayerInput.Disable();
	}
	#endregion

	#region - Climbing Trigger -

	private void OnTriggerEnter(Collider other)
	{ 
		if(other.gameObject.tag == "ClimbWall")
        {
			climbing = true;
			//gravity = 0f;
			//currentGravity = 0f;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "ClimbWall")
        {
			climbing = false;
			//gravity = 10;
			//canClimb = false;
		}
	}

	public void IsClimbing()
	{
		if (climbing == true && Input.GetKey(KeyCode.Q))
		{
			//transform.Translate(Vector3.up * 0.01f);
			gravityMovement = gravityDirection * currentGravity * 0.00025f;
			characterAnimator.SetBool("IsClimbing", climbing);
		}

		else
		{
			characterAnimator.SetBool("IsClimbing", false);
		}
	}

	#endregion

	#region - Gliding-

	public void IsGliding()
	{
		isOnGround = Physics.CheckSphere(groundChecker.transform.position, 0.2f, whatIsGround);


		if (isOnGround == false && Input.GetKey(KeyCode.F) && GlideAfterJump == true /*&& Input.GetKey(KeyCode.F)/*!jumpingTriggered*/)
		{
			//transform.Translate(Vector3.up * 0.1f);
			playerTransform.transform.forward = Vector3.MoveTowards(playerTransform.forward, myEndPoint.forward, speed * Time.deltaTime);
			gravity = 0.25f;


			WindParticles[0].SetActive(true);
			WindParticles[1].SetActive(true);
			WindParticles[2].SetActive(true);

			
			

			//gravityMovement = gravityDirection * currentGravity * 0.005f;
		}
		else
		{
			gravity = 10f;

			WindParticles[0].SetActive(false);
			WindParticles[1].SetActive(false);
			WindParticles[2].SetActive(false);
		}
	}

	#endregion

	public void GrabTorch(bool activate)
	{
		torch.enabled = activate;
		fire.SetActive(true);
	}

}
