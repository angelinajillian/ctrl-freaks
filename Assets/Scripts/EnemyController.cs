using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class EnemyController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private int health = 3;
    [SerializeField] private float movementSpeed = 2.0f;
    [SerializeField] private GameObject xpOrbPrefab;
    public Color originalColor;
    public Color hitColor;
    private GameObject player;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        originalColor = GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            StartCoroutine(FlashWhite());
            health -= 1;
            
            if (health <= 0)
            {
                Instantiate(xpOrbPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;

            directionToPlayer.Normalize();

            rb.MovePosition(rb.position + directionToPlayer * movementSpeed * Time.deltaTime);

            transform.LookAt(player.transform);
        }
    }

    IEnumerator FlashWhite()
    {
        GetComponent<SpriteRenderer>().color = hitColor;

        yield return new WaitForSeconds(0.4f);

        GetComponent<SpriteRenderer>().color = originalColor;
    }
}