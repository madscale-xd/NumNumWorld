using UnityEngine;

public class SimpleEnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    private bool isStopped = false;

    void Update()
    {
        if (!isStopped)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isStopped = true;
        }
    }
}
