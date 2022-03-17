using System.Collections;
using System.Collections.Generic;
using static Models;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Basic playerController;
    public  PlayerSettingsModel settings;

    private Vector3 targetRotation;



    private void Update()
    {

        FollowPlayerCameraTarget();
        CameraRotation();

    }

    private void CameraRotation()
    {
        var viewInput = playerController.input_View;

        targetRotation.x += (settings.InvertedY ? (viewInput.y * settings.SensitivityY) : -(viewInput.y * settings.SensitivityY)) * Time.deltaTime;
        targetRotation.y += (settings.InvertedX ? -(viewInput.x * settings.SensitivityX) : (viewInput.x * settings.SensitivityX)) * Time.deltaTime;

        targetRotation.x = Mathf.Clamp(targetRotation.x, settings.YClampMin, settings.YClampMax);

        //Looking Up and Down. 
        //Looking Left and Right.
        //Question mark is used like an if statement, first value is true

        transform.rotation = Quaternion.Euler(targetRotation);

        //Turns Vector into Quaternion for Rotation

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
        transform.position = playerController.cameraTarget.position;

    }


}
