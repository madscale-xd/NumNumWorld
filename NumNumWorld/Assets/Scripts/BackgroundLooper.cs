using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public float scrollSpeed = 2f;  // Speed at which the background moves
    public bool loopBackground = true; // Whether to loop the background
    private float width;            // Width of the background

    private Vector3 startPosition;  // Initial position of the background

    void Start()
    {
        // Store the initial position of the background
        startPosition = transform.position;

        // Get the width of the background from the sprite renderer
        width = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Move the background by the scroll speed in the X direction
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        // If the background moves past its starting position (or left side), reset it
        if (loopBackground && transform.position.x <= startPosition.x - width)
        {
            transform.position = startPosition;  // Reset position to the start for looping effect
        }
    }
}
