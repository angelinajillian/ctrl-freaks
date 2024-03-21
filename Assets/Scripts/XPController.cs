using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPController : MonoBehaviour
{
    private PlayerControllerExtended playerControllerExtended;
    private Transform playerTransform;
    public float movementSpeed = 4f;
    public float activationDistance = 12.5f;
    public float pickUpRadius = 5f;
    public float xpValue = 20f;
    public TrailRenderer trailRenderer;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerControllerExtended = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerExtended>();
        trailRenderer.enabled = false;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Debug.Log($"{distanceToPlayer}");
        if (distanceToPlayer <= activationDistance)
        {
            Vector3 targetPosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            trailRenderer.enabled = true;
        }
        else
        {
            trailRenderer.enabled = false;
        }

        if (distanceToPlayer < pickUpRadius)
        {
            // Debug.Log("PLAYER");
            playerControllerExtended.UpdateXP(xpValue);
            Destroy(gameObject);
        }

        ApplyGravity();
    }

    void ApplyGravity()
    {
        if (transform.position.y > 0.1f)
        {
            transform.Translate(Vector3.down * 3.5f * Time.deltaTime);
        }
        else 
        {
            transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        }
    }
}
