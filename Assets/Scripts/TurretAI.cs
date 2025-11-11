using UnityEngine;

public class TurretAI2D : MonoBehaviour
{
    // Assign these in the Unity Inspector
    public Transform target;            // The player's transform
    public GameObject bulletPrefab;     // The bullet prefab
    public Transform firePoint;         // The position where bullets spawn
    public float rotationSpeed = 100f;  // How fast the turret turns (in degrees/sec)
    public float fireRate = 1f;         // Bullets per second (1.0f for 1 shot/sec)
    public float detectionRange = 10f;  // Range to start tracking/firing
    [SerializeField]
    private int health = 5;
    [SerializeField]
    private float bulletSpeed = 10f; // speed of the bullet

    private float nextFireTime;

    void Start()
    {
        // Find the player automatically if not assigned
        if (target == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                target = playerObject.transform;
            }
        }
        nextFireTime = Time.time;
    }

    void Update()
    {
        if (target != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, target.position);

            if (distanceToPlayer <= detectionRange)
            {
                // 1. Turn to face the player
                RotateTowardsTarget();

                // 2. Start firing with a cooldown
                if (Time.time >= nextFireTime)
                {
                    Shoot();
                    // Calculate the next time the turret can fire
                    nextFireTime = Time.time + 1f / fireRate;
                }
            }
        }
    }

    void RotateTowardsTarget()
    {
        // Calculate the direction to the target in 2D space
        Vector3 direction = target.position - transform.position;
        // Calculate the angle in degrees using Mathf.Atan2 (trigonometry)
        // Note: transform.right is typically the forward direction for 2D sprites
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Create the target rotation as a Quaternion around the Z-axis
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Smoothly rotate the turret head towards the target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        var bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * bulletSpeed;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        target.GetComponent<Turret>().money += 10;
        Debug.Log($"Player money increased by 10! Current player money: {target.GetComponent<Turret>().money}.");
        Debug.Log($"Turret took {damage} damage, remaining health: {health}");
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