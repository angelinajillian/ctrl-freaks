using UnityEngine;

public class FistProjectile1Controller : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 25.0f; 
    private float destroyTime = 1.5f;
    private Rigidbody rb;

    private Transform target;


    public void SetTarget(GameObject newTarget)
    {
        target = newTarget.transform;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (target != null)
        {
            // Calculate the direction towards the target
            Vector3 direction = (target.position - transform.position).normalized;

            // Set the velocity towards the target
            rb.velocity = direction * movementSpeed;
        }
        else
        {
            // If no target is specified, move forward based on the object's forward direction
            rb.velocity = -transform.forward * movementSpeed;
        }

        Destroy(gameObject, destroyTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}