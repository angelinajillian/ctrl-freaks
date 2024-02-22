using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class SpellFactory : MonoBehaviour
{
    // Animator class for handling animations (obviously)
    private Animator animator;
    private bool canFire = true;
    private float fireCooldown = 0.3f; // Set the cooldown time in seconds

    [SerializeField]
    private GameObject fistProjectile1;

    [SerializeField]
    private GameObject fistProjectile1Point;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Fire1Punch();
    }

    private void Fire1Punch()
    {
        if (Input.GetButtonDown("Fire1") & canFire)
        {
            canFire = false;
            animator.SetTrigger("Fire1Trigger");
            Fire1Projectile();
        }
    }

    private IEnumerator FireCooldown()
    {
        yield return new WaitForSeconds(fireCooldown);
        canFire = true; 
    }

    void Fire1Projectile()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.Normalize();

        GameObject fist1 = Instantiate(fistProjectile1, fistProjectile1Point.transform.position, Quaternion.LookRotation(cameraForward) * Quaternion.Euler(0, 180f, 0));
        StartCoroutine(FireCooldown());
    }
}