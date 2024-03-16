using UnityEngine;

public class FistProjectileEnemyController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 30.0f; 
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
            Vector3 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * movementSpeed;
        }
        else
        {
            rb.velocity = -transform.forward * movementSpeed;
        }

        Destroy(gameObject, destroyTime);
    }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     Destroy(gameObject);
    // }
}