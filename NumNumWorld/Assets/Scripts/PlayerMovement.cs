using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // Zagan +x direction
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}
