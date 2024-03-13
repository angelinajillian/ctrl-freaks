using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandGrenadeController : MonoBehaviour
{
    [SerializeField] private GameObject explosion;

    private float throwSpeed = 19.5f;
    private float maxAngle = 95.0f;
    private float totalFuseTime = 2.75f;
    private float fuseTime = 2.75f;
    private float holdStartTime;
    private float force = 1100f; // for spinning the nade for visual flair
    private Rigidbody rb;

    // Player must hold and release grenade so have bool to track if thrown or not
    private bool thrown = false;

    private void Start()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        fuseTime -= Time.deltaTime;

        if (fuseTime <= 0)
            this.Explode();
    }

    public void SetPosition(GameObject pos)
    {
        // point the grenade forwards
        this.gameObject.transform.forward = -Camera.main.transform.forward;

        // set the position of the grenade to match the position of the hand
        // (this parent stuff is weird but it looks the best this way)
        this.gameObject.transform.parent = pos.transform; 
        this.gameObject.transform.localPosition = Vector3.zero; // ensure local position is zeroed out
    }

    public void Launch(GameObject pos)
    { 
        float holdDuration = Time.time - holdStartTime;

        // increase angle of throw the longer we hold the grenade
        float launchAngle = Mathf.Min(holdDuration / totalFuseTime, 1.0f) * maxAngle;

        this.gameObject.transform.parent = null; // stop tracking parent, now we are free
   
        // angle to launch our grenade, scales up with time as you hold
        Vector3 launchDirection = Quaternion.AngleAxis(launchAngle, transform.right) * -transform.forward;
        launchDirection.Normalize();

        // scale throwspeed up the longer we hold 
        throwSpeed += (throwSpeed * (holdDuration / totalFuseTime)); 
        rb.AddForce(launchDirection * throwSpeed, ForceMode.Impulse);
        
        // make it spin in random direction
        var torqueVector = CreateRandomTorque();
        rb.AddRelativeTorque(torqueVector, ForceMode.Impulse);
    }

    public void Hold()
    {
        holdStartTime = Time.time;
    }

    void Explode()
    {
        var position = gameObject.transform.position;
        var rotation = gameObject.transform.rotation;
        var newExplosion = Instantiate(explosion, position, rotation);
        var particleSystem = newExplosion.GetComponent<ParticleSystem>();
        var collider = newExplosion.transform.GetChild(2).gameObject;
        Destroy(collider, 0.15f);
        Destroy(newExplosion, particleSystem.main.duration);
        Destroy(gameObject);
    }

    Vector3 CreateRandomTorque()
    {
        var randomX = Random.Range(-force, force);
        var randomY = Random.Range(-force, force);
        var randomZ = Random.Range(-force, force);
        var randomVector = new Vector3(randomX, randomY, 0);
        return randomVector;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            this.Explode();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Explosion"))
        {
            this.Explode();
        }
    }

}