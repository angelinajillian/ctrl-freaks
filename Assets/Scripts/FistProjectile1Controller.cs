using UnityEngine;

public class FistProjectile1Controller : MonoBehaviour
{
    private float movementSpeed = 15.0f; // Adjust the speed as needed
    private float destroyTime = 1.5f; // Adjust the time before destroying the projectile

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = -transform.forward * movementSpeed;

        Destroy(gameObject, destroyTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}