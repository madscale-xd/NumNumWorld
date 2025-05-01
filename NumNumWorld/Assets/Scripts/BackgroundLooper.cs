using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public bool loopBackground = true;

    private float backgroundWidth;
    private Transform[] backgrounds;

    void Start()
    {
        // Assume two children with identical sprites are attached to this GameObject
        backgrounds = new Transform[2];
        backgrounds[0] = transform.GetChild(0);
        backgrounds[1] = transform.GetChild(1);

        backgroundWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        if (!loopBackground) return;

        // Move both backgrounds leftward
        foreach (Transform bg in backgrounds)
        {
            bg.position += Vector3.left * scrollSpeed * Time.deltaTime;
        }

        // Check if any background has moved completely off screen
        if (backgrounds[0].position.x <= -backgroundWidth)
        {
            SwapAndReposition();
        }
    }

    void SwapAndReposition()
    {
        // Move the first background to the right of the second one
        Transform temp = backgrounds[0];
        backgrounds[0] = backgrounds[1];
        backgrounds[1] = temp;

        backgrounds[1].position = backgrounds[0].position + Vector3.right * backgroundWidth;
    }
}
