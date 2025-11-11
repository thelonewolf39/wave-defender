using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private Transform player; // the parent (boat), optional if it's already child
    [SerializeField]
    private Transform ammoEntryPoint; // where bullets are spawned from
    [SerializeField]
    private GameObject bulletPrefab; // bullet prefab to instantiate
    [SerializeField]
    private float rotationOffset = 0f; // adjust if your turret sprite points differently
    [SerializeField]
    private float turretRangeAtStart = 3f; // range within which the turret can target at the start of the game
    [SerializeField]
    private float turretRangeAtLevelTwo = 5f; // current range of the turret
    [SerializeField]
    private float turretFireCooldown = 2f; // shots per second
    [SerializeField]
    private float bulletSpeed = 10f; // speed of the bullet
    [SerializeField]
    private int health = 100;

    private float nextAvailableActionTime;

    [HideInInspector]
    public float money;

    private void Start()
    {
        nextAvailableActionTime = Time.time;
    }

    void Update()
    {

        // Get mouse position in world coordinates
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // ignore Z for 2D

        // Calculate direction from turret to mouse
        Vector3 direction = mousePos - transform.position;

        // Calculate angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply offset if needed
        angle += rotationOffset;

        // Rotate turret
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if(Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }

    }

    private void OnDrawGizmos()
    {
        // Draw turret range in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, turretRangeAtStart);
        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(transform.position, turretRangeAtLevelTwo);
    }

    private void Shoot()
    {
        if (Time.time >= nextAvailableActionTime)
        {
            nextAvailableActionTime = Time.time + turretFireCooldown;
            FireBullet();
        }
        else
        {
            Debug.LogWarning("Turret is cooling down!");
        }
    }

    private void FireBullet()
    {
        var bulletInstance = Instantiate(bulletPrefab, ammoEntryPoint.position, ammoEntryPoint.rotation);
        Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * -bulletSpeed; // or any specific direction
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"Player took {damage} damage, remaining health: {health}");
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDamage(1); // Assume each bullet does 1 damage
    }
}
