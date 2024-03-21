using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandGrenadeController : MonoBehaviour
{
    [SerializeField] private GameObject explosion;

    private float throwSpeed = 18.0f;
    private float totalFuseTime = 2.75f;
    private float fuseTime = 2.75f;
    private float holdStartTime;
    private float force = 1100f; // for spinning the nade for visual flair
    private Rigidbody rb;

    // Player must hold and release grenade so have bool to track if thrown or not
    // private bool thrown = false;

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

        this.gameObject.transform.parent = null; // stop tracking parent, now we are free

        Vector3 launchDirection = -transform.forward;
        launchDirection.Normalize();

        // scale throwspeed up the longer we hold 
        throwSpeed += (throwSpeed * (holdDuration / totalFuseTime));
        rb.velocity = throwSpeed * launchDirection;

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
        FindObjectOfType<SoundManager>().PlayExplosionSound(this.transform.position);


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

        if (collision.gameObject.CompareTag("Ground"))
        {
            // don't use ground as parent cause it leads to ugly shit
            transform.position = collision.contacts[0].point; 
        }
        else
        {
            // this makes grenade stick and stay on moving target
            transform.parent = collision.transform;
        }

        rb.isKinematic = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Explosion") || other.CompareTag("ExplosionLarge"))
        {
            this.Explode();
        }

        if (other.CompareTag("KillPlane"))
        {
            this.Explode();
        }
    }
}