using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

using UnityEngine.XR.Interaction.Toolkit;

//code from: https://discussions.unity.com/t/vr-get-controller-velocity/251820/2

public class ControllerVelocity : MonoBehaviour
{


    // Start is called before the first frame update
    InputDevice LeftControllerDevice;
    InputDevice RightControllerDevice;
    Vector3 LeftControllerVelocity;
    Vector3 RightControllerVelocity;

    void Start()
    {
        LeftControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        RightControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    void Update()
    {
        UpdateInput();
    }

    void UpdateInput()
    {
        LeftControllerDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out LeftControllerVelocity);
        RightControllerDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out RightControllerVelocity);
    }

    Vector3 getLeftControllerVel()
    {
        return LeftControllerVelocity;
    }
    Vector3 getRightControllerVel()
    {
        return RightControllerVelocity;
    }
}
