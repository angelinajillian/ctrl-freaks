using UnityEngine;

public class AOEFistController : MonoBehaviour
{
    private float movementSpeed = 1.5f; 
    private float destroyTime = 5.0f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0f, movementSpeed, 0f); 

        Destroy(gameObject, destroyTime);
    }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     Destroy(gameObject);
    // }
}