using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : MonoBehaviour
{

	BasicPlayerInput obj_BasicPlayerInput;
	Vector2 input_Movement;
	Vector2 input_View;

	private void Awake()
	{
		obj_BasicPlayerInput = new BasicPlayerInput();

		obj_BasicPlayerInput.Movement.Movement.performed += x => input_Movement = x.ReadValue<Vector2>();
		obj_BasicPlayerInput.Movement.View.performed += x => input_View = x.ReadValue<Vector2>();
		obj_BasicPlayerInput.Actions.Jump.performed += x => Jump();

		// Move

	}

	private void Jump()
	{
		Debug.Log("I'm Jumping");
	}

	private void Update()
	{
		Debug.Log(input_View);
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
