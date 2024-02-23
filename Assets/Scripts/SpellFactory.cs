using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class SpellFactory : MonoBehaviour
{
    // Animator class for handling animations (obviously)
    private Animator animator;
    private bool canFire = true;
    private float fireCooldown = 0.6f; 
    private float mana = 100f;
    private float manaRegenerationRate = 2.5f;

    [SerializeField]
    private GameObject fistProjectile1;

    [SerializeField]
    private GameObject fistProjectile1Point;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(RegenerateMana());
    }

    void Update()
    {
        Fire1Punch();
    }

    private void Fire1Punch()
    {
        if (Input.GetButtonDown("Fire1") & canFire & mana >= 10f)
        {
            mana -= 10f;
            Debug.Log($"Mana: {mana}");
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

    IEnumerator RegenerateMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f / manaRegenerationRate);
            mana += 1;

            mana = Mathf.Clamp(mana, 0f, 100f);

            // yield return null;
        }
    }
}