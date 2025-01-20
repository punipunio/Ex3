using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class EyeTrackingChecker : MonoBehaviour
{
    void Start()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.EyeTracking, inputDevices);

        if (inputDevices.Count > 0)
        {
            Debug.Log("Eye tracking is supported and available.");
        }
        else
        {
            Debug.Log("Eye tracking is not supported or unavailable.");
        }
    }
}
