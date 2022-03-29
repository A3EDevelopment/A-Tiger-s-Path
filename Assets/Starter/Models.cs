using System;
using UnityEngine;

public static class Models 
{

    #region - Player -

    [Serializable]

    public class CameraSettingsModel
    {
        [Header("Camera Settings")]
        public float SensitivityX;
        public bool InvertedX;

        public float SensitivityY;
        public bool InvertedY;

        public float YClampMin = -40f;
        public float YClampMax = 20f;

        [Header("Character")]
        public float CharacterRotationSpeedSmoothdamp = 1f;

    }

    [Serializable]
    public class PlayerSettingsModel
    {
        public float CharacterRotationSmoothdamp = 0.6f;

        [Header("Movement Speeds")]
        [Range(0, 10)]
        public float WalkingSpeed;
        [Range(0, 10)]
        public float RunningSpeed;

        [Range(0, 10)]
        public float WalkingBackwardSpeed;
        [Range(0, 10)]
        public float RunningBackwardSpeed;

        [Range(0, 10)]
        public float WalkingStrafindSpeed;
        [Range(0, 10)]
        public float RunningStrafingSpeed;


        public float SprintingSpeed;
    }

    [Serializable]
    
    public class PlayerStatsModel
    {
        public float Stamina;
        public float MaxStamina;
        public float StaminaDrain;
        public float StaminaRecovery;
        public float StaminaDelay; 
        public float StaminaCurrentDelay;
    }


    #endregion

}
