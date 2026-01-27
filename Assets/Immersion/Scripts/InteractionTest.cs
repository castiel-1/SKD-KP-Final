using UnityEngine;
using UnityEngine.XR;

public class InteractionTest : MonoBehaviour
{
    
    InputDevice leftHand;
    InputDevice rightHand;

    void Start()
    {

        leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    void Update()
    {
        // get controllers again if they get disconnected or turned off while running
        if (!leftHand.isValid)
            leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        if (!rightHand.isValid)
            rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // set image 0
        bool pressedLeft;
        if (leftHand.TryGetFeatureValue(CommonUsages.primaryButton, out pressedLeft) && pressedLeft)
        {
            MonitorManager.ChangeFrescoTo(0);
        }

        // set image 1
        bool pressedRight;
        if (rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out pressedRight) && pressedRight)
        {
            MonitorManager.ChangeFrescoTo(1);
        }
     
    }
}
