using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TextureRotator : MonoBehaviour
{
    [Header("Rotation Settings (Hold A/X)")]
    public float rotationSpeed = 2.0f;
    public InputActionProperty leftPrimaryAction;  // Map to X
    public InputActionProperty rightPrimaryAction; // Map to A

    [Header("Texture Settings (Click B/Y)")]
    public InputActionProperty leftSecondaryAction;  // Map to Y
    public InputActionProperty rightSecondaryAction; // Map to B
    public Texture2D[] textureList;
    
    private Renderer rend;
    private Material[] mats;
    private float currentRotation = 0f;
    private int currentTextureIndex = 0;

    InputDevice leftHand;
    InputDevice rightHand;
    private bool wasRightSecondaryPressedLastFrame = false;
    private bool wasLeftSecondaryPressedLastFrame = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        mats = rend.materials;

        // Subscribe to Secondary buttons for the one-click texture swap
        leftSecondaryAction.action.performed += OnLeftSecondaryClick;
        rightSecondaryAction.action.performed += OnRightSecondaryClick;

        UpdateTexture();
    }

    void OnDestroy()
    {
        // Clean up subscriptions
        leftSecondaryAction.action.performed -= OnLeftSecondaryClick;
        rightSecondaryAction.action.performed -= OnRightSecondaryClick;
    }

    void Update()
    {
        HandleRotation();
    }

    private void HandleRotation()
    {
        // Continuous check: While button is held down
        if (leftPrimaryAction.action.IsPressed())
        {
            currentRotation += rotationSpeed * Time.deltaTime;
            mats[1].SetFloat("_Rotation", currentRotation);
        }

        if (rightPrimaryAction.action.IsPressed())
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

    // These trigger only ONCE per press
    private void OnLeftSecondaryClick(InputAction.CallbackContext context) => ChangeTexture(-1);
    private void OnRightSecondaryClick(InputAction.CallbackContext context) => ChangeTexture(1);

    void ChangeTexture(int step)
    {
        if (textureList.Length > 0)
        {
            currentTextureIndex = (currentTextureIndex + step + textureList.Length) % textureList.Length;
            UpdateTexture();
        }
    }

    void UpdateTexture()
    {
        if (textureList.Length > 0 && mats.Length > 1)
        {
            mats[1].SetTexture("_MainTex", textureList[currentTextureIndex]);
        }
    }
}
