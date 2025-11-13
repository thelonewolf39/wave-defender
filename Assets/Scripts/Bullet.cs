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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Deal damage to the enemy
            TurretAI2D enemy = collision.gameObject.GetComponent<TurretAI2D>();
            if (enemy != null)
            {
                enemy.TakeDamage(1); // Assuming 1 damage per bullet
            }
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Optionally handle obstacle collision
            Debug.Log("Bullet hit an obstacle.");
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Turret player = collision.gameObject.GetComponent<Turret>();
            player.TakeDamage(1); // Assuming 1 damage per bullet
        }
            // Destroy the bullet on impact
            Destroy(gameObject);
    }
}
