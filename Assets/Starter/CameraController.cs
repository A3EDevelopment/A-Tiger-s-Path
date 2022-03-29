using System.Collections;
using System.Collections.Generic;
using static Models;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Settings")]
    public Basic playerController;
    public CameraSettingsModel settings;
    private Vector3 targetRotation;
    public GameObject yGimbal;
    private Vector3 yGibalRotation;

    [Header("Position Settings")]
    public float movementSmoothTime = 1f;
    private Vector3 movementVelocity;

    private void Update()
    {

        FollowPlayerCameraTarget();
        CameraRotation();

    }

    private void CameraRotation()
    {
        var viewInput = playerController.input_View;

        targetRotation.y += (settings.InvertedX ? -(viewInput.x * settings.SensitivityX) : (viewInput.x * settings.SensitivityX)) * Time.deltaTime;
        transform.rotation = Quaternion.Euler(targetRotation);

        //Turns Vector into Quaternion for Rotation

        yGibalRotation.x += (settings.InvertedY ? (viewInput.y * settings.SensitivityY) : -(viewInput.y * settings.SensitivityY)) * Time.deltaTime;
        yGibalRotation.x = Mathf.Clamp(yGibalRotation.x, settings.YClampMin, settings.YClampMax);

        //Looking Up and Down. 
        //Looking Left and Right.
        //Question mark is used like an if statement, first value is true

        yGimbal.transform.localRotation = Quaternion.Euler(yGibalRotation);  
        
        //Only changes y axis, not x

        if (playerController.isTargetMode)
        {
            var currentRotation = playerController.transform.rotation;

            var newRotation = currentRotation.eulerAngles;
            newRotation.y = targetRotation.y;

            currentRotation = Quaternion.Lerp(currentRotation, Quaternion.Euler(newRotation), settings.CharacterRotationSpeedSmoothdamp);

            playerController.transform.rotation = currentRotation;
        }
    }

    private void FollowPlayerCameraTarget()
    {
        transform.position = Vector3.SmoothDamp(transform.position, playerController.cameraTarget.position, ref movementVelocity, movementSmoothTime);

    }


}
