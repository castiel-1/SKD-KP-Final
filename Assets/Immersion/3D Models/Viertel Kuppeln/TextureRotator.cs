using UnityEngine;

public class TextureRotator : MonoBehaviour
{
    public float rotationSpeed = 2.0f;
    private Renderer rend;
    private float currentRotation = 0f;

    public Texture2D[] textureList;
    private int currentTextureIndex = 0;

    void Start()
    {
        rend = GetComponent<Renderer>();
        UpdateTexture();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            currentRotation += rotationSpeed * Time.deltaTime;
            rend.material.SetFloat("_Rotation", currentRotation);
        }
        if (Input.GetKey(KeyCode.E))
        {
            currentRotation -= rotationSpeed * Time.deltaTime;
            rend.material.SetFloat("_Rotation", currentRotation);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeTexture(1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeTexture(-1);
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
            rend.material.SetTexture("_MainTex", textureList[currentTextureIndex]);
        }
    }
}