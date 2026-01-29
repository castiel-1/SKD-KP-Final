using UnityEngine;
using UnityEngine.XR;

public class TextureRotator : MonoBehaviour
{
    public float rotationSpeed = 2.0f;
    private Renderer rend;
    Material[] mats;
    private float currentRotation = 0f;

    public Texture2D[] textureList;
    private int currentTextureIndex = 0;

    InputDevice leftHand;
    InputDevice rightHand;
    private bool wasRightSecondaryPressedLastFrame = false;
    private bool wasLeftSecondaryPressedLastFrame = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        mats = rend.materials;

        leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        UpdateTexture();
    }

    void Update()
    {
        // get controllers again if they get disconnected or turned off while running
        if (!leftHand.isValid)
            leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        if (!rightHand.isValid)
            rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // rotation
        bool pressedLeft;
        if (leftHand.TryGetFeatureValue(CommonUsages.primaryButton, out pressedLeft) && pressedLeft)
        {
            currentRotation += rotationSpeed * Time.deltaTime;
            mats[1].SetFloat("_Rotation", currentRotation);
        }

        bool pressedRight;
        if (rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out pressedRight) && pressedRight)
        {
            currentRotation -= rotationSpeed * Time.deltaTime;
            mats[1].SetFloat("_Rotation", currentRotation);
        }

        bool pressedRightNow;
        bool pressedLeftNow;
        if (rightHand.TryGetFeatureValue(CommonUsages.secondaryButton, out pressedRightNow))
        {
            if (pressedRightNow && !wasRightSecondaryPressedLastFrame)
            {
                ChangeTexture(1);
            }
            wasRightSecondaryPressedLastFrame = pressedRightNow;
        }
        if (leftHand.TryGetFeatureValue(CommonUsages.secondaryButton, out pressedLeftNow))
        {
            if (pressedLeftNow && !wasLeftSecondaryPressedLastFrame)
            {
                ChangeTexture(-1);
            }
            wasLeftSecondaryPressedLastFrame = pressedLeftNow;
        }
    }

    void ChangeTexture(int step)
    {
        if (textureList.Length > 0)
        {
            currentTextureIndex += step;
            if (currentTextureIndex >= textureList.Length) currentTextureIndex = 0;
            if (currentTextureIndex < 0) currentTextureIndex = textureList.Length - 1;
            UpdateTexture();
        }
    }

    void UpdateTexture()
    {
        if (textureList.Length > 0 )
        {
            mats[1].SetTexture("_MainTex", textureList[currentTextureIndex]);
        }
    }
}