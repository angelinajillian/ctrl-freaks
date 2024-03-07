using UnityEngine;

public class AOEFistController : MonoBehaviour
{
    private float movementSpeed = 2.5f; 
    private float destroyTime = 5.0f;
    private Rigidbody rb;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime * -1);
    }
}