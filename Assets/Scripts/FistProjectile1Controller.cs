using UnityEngine;

public class FistProjectile1Controller : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 30.0f; 
    [SerializeField] private float destroyDistance = 0.1f;

    public int damage = 2;
    private float destroyTime = 1.5f;
    private Rigidbody rb;

    private Transform target;


    public void SetTarget(GameObject newTarget)
    {
        target = newTarget.transform;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
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

    void Update()
    {
        if (target != null)
        {
            // Check if the projectile is close enough to the target to be destroyed
            if (Vector3.Distance(transform.position, target.position) <= destroyDistance)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}