using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f; // Destroy after 3 seconds

    void Start()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bullet hit: " + collision.gameObject.name);
        // Destroy the bullet on impact
        Destroy(gameObject);
    }
}
