using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] private GameObject barrel;
    private float force = 1100f;
    private float coneSize = 10f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var newRotation = CreateRandomRotation();

        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnBarrel();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Cleanup();
        }
    }


    Vector3 CreateRandomTorque()
    {
        var randomX = Random.Range(0, force);
        var randomY = Random.Range(0, force);
        var randomZ = Random.Range(0, force);
        var randomVector = new Vector3(randomX, randomY, randomZ);
        return randomVector;
    }

    public void SpawnBarrel()
    {
        FindObjectOfType<SoundManager>().PlayCannonSound(this.transform.position);

        var newRotation = CreateRandomRotation();
        var spawn = gameObject.transform.GetChild(0);
        var newBarrel = Instantiate(barrel, spawn.position, newRotation);
        var rb = newBarrel.GetComponent<Rigidbody>();
        ManageCollision(newBarrel);
        MoveBarrel(newBarrel, rb);
        CleanTrail(newBarrel);
    }

    void ManageCollision(GameObject barrel)
    {
        var ceiling = GameObject.FindGameObjectWithTag("Ceiling");
        var ceilingCollider = ceiling.GetComponent<BoxCollider>();
        var barrelCollider = barrel.GetComponent<MeshCollider>();
        Physics.IgnoreCollision(barrelCollider, ceilingCollider, true);
    }

    void MoveBarrel(GameObject barrel, Rigidbody rb)
    {
        var torqueVector = CreateRandomTorque();
        // Shoot the barrel upwards
        rb.AddForce(barrel.transform.up * force);

        // Make it spin in random direction as it falls
        rb.AddRelativeTorque(torqueVector, ForceMode.Impulse);
    }

    void Cleanup()
    {
        var barrelsList = GameObject.FindGameObjectsWithTag("Barrel").ToList();
        if (barrelsList.Count > 0)
        {
            foreach (var barrel in barrelsList)
            {
                Destroy(barrel);
            }
        }
    }

    void CleanTrail(GameObject barrel)
    {
        //Remove trail after 5 seconds
        var trail = barrel.GetComponent<TrailRenderer>();

        if (trail)
        {
            Destroy(trail, 4f);
        }
    }

    // Sourced from:
    // https://forum.unity.com/threads/cone-shaped-bullet-spread.414893/
    Quaternion CreateRandomRotation()
    {
        var curRotation = gameObject.transform.rotation;
        float randomX = Random.Range(-1, 1);
        float randomY = Random.Range(-1, 1);
        Vector3 spreadVector = new Vector3(randomX, randomY, 0f).normalized;
        spreadVector *= coneSize;
        Quaternion newRotation = Quaternion.Euler(spreadVector) * curRotation;
        return newRotation;
    }
}
