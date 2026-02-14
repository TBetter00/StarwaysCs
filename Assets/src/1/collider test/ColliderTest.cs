using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    #region TesterScript
    [SerializeField] float colorChangeSpeed = 1f; // Control the rainbow speed
    private Color originalColor = Color.white;
    private Renderer rend;
    private bool isRainbowActive = false; // Toggle for when the trigger is hit

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null) originalColor = rend.material.color;
    }

    void Update()
    {
        // If the trigger was hit, run the rainbow logic
        if (isRainbowActive)
        {
            ApplyRainbowColor();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Reset();
        }
    }

    void ApplyRainbowColor()
    {
        float hue = (Time.time * colorChangeSpeed) % 1f;
        Color rainbowColor = Color.HSVToRGB(hue, 1f, 1f);

        ChangeColor(rainbowColor);
    }

    void Reset()
    {
        isRainbowActive = false; // Stop the rainbow
        ChangeColor(originalColor);
    }

    void ChangeColor(Color colorToSet)
    {
        if (rend != null)
        {
            rend.material.color = colorToSet;
        }
    }
    #endregion

    // Triggered when something enters the zone
    // void OnTriggerEnter(Collider other)
    // {
    //     isRainbowActive = true;
    // }

    // void OnTriggerExit(Collider other)
    // {
    //     Reset();
    // }

    void OnCollisionEnter(Collision collision)
    {
        isRainbowActive = true;
    }

    void OnCollisionExit(Collision collision)
    {
        Reset();
    }
}