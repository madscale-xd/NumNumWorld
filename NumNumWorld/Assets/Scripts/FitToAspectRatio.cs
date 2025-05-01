using UnityEngine;

public class FitToAspectRatio : MonoBehaviour
{
    public Camera mainCamera;

    // Desired aspect ratio (width/height)
    private float targetAspect;

    void Start()
    {
        // Set the target aspect ratio (the ratio of width to height)
        targetAspect = (float)Screen.width / (float)Screen.height;

        // Adjust the camera based on the current screen aspect ratio
        AdjustCameraAspect();
    }

    void AdjustCameraAspect()
    {
        // Get the current aspect ratio of the screen
        float currentAspect = (float)Screen.width / (float)Screen.height;

        // Compare the current aspect ratio with the target aspect ratio
        float scaleHeight = currentAspect / targetAspect;

        // Adjust the camera size for 2D games (Orthographic)
        if (scaleHeight < 1.0f)
        {
            // Maintain the width and scale the camera height
            mainCamera.orthographicSize = 5 / scaleHeight;  // Adjust 5 based on your scene's desired view size
        }
        else
        {
            // If the screen's aspect ratio is taller than the target aspect, fit based on height
            mainCamera.orthographicSize = 5;  // Adjust 5 based on your scene's desired view size
        }
    }
}
