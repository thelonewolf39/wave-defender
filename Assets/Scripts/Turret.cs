using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private Transform player; // the parent (boat), optional if it's already child
    [SerializeField]
    private float rotationOffset = 0f; // adjust if your turret sprite points differently

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
    }
}
