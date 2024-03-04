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
    
    [SerializeField] private GameObject fistProjectile1;

    [SerializeField] private GameObject fistProjectile1Point;
    [SerializeField] private GameObject AOEPrefab;

    [SerializeField] private PlayerControllerExtended playerControllerExtended;

    // Kicking variables
    private float kickDamage = 0.0f; // no dam so far, maybe up it through upgrades in shop
    private float kickRange = 3.0f; // the max range kick reaches out to
    private float kickCooldownTime = 0.0f;
    private bool canKick = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(RegenerateMana());
    }

    void Update()
    {
        Fire1Punch();
        Fire2Kick();
        AOESpell();
    }

    private void Fire1Punch()
    {
        if (Input.GetButtonDown("Fire1") & canFire & mana >= 7f)
        {
            mana -= 7f;
            Debug.Log($"Mana: {mana}");
            canFire = false;
            animator.SetTrigger("Fire1Trigger");
            // Fire1Projectile();
        }
    }

    void Fire1Projectile()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.Normalize();

        GameObject fist1 = Instantiate(fistProjectile1, fistProjectile1Point.transform.position, Quaternion.LookRotation(cameraForward) * Quaternion.Euler(0, 180f, 0));
        StartCoroutine(FireCooldown());
    }

    
    private IEnumerator FireCooldown()
    {
        yield return new WaitForSeconds(fireCooldown);
        canFire = true; 
    }

    private void Fire2Kick()
    {
        if (Input.GetButtonDown("Fire2") && canKick)
        {
            // calls kickAttack in animator (in unity editor).
            animator.SetTrigger("PunchTrigger");
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Kick is on CD!");
        }
    }

    public void kickAttack()
    {
        playerControllerExtended.setCanTakeDamage(false);

        Debug.Log("kicking!!");

        canKick = false;

        // TODO: play kicking noise, maybe find kick animation.

        // get the direction the player is facing
        Vector3 playerDirection = transform.forward;

        // detect enemies in front of the player
        RaycastHit[] hits = Physics.RaycastAll(transform.position, playerDirection, kickRange);

        // loop through all the hit things
        foreach (RaycastHit hit in hits)
        {
            // check that we hit an enemy
            EnemyController enemyController = hit.collider.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                // kick the enemy
                enemyController.Kicked(kickDamage, playerDirection, 5.0f);
            }
        }
        
        playerControllerExtended.setCanTakeDamage(true);
        StartCoroutine(KickCooldown());
    }

    private IEnumerator KickCooldown()
    {
        yield return new WaitForSeconds(kickCooldownTime);
        canKick = true; 
    }

    public void AOESpell()
    {
        if (Input.GetButtonDown("k") & canFire & mana >= 40f)
        {
            mana -= 40f;
            Debug.Log($"Mana: {mana}");
            canFire = false;
            animator.SetTrigger("AOETrigger");
            // AOEAttack();
        }
    }

    void AOEAttack()
    {
        Vector3 circlePosition = new Vector3(transform.position.x, 0.0f, transform.position.z);

        GameObject AOEspell = Instantiate(AOEPrefab, circlePosition, Quaternion.identity);
        StartCoroutine(FireCooldown());
    }

    IEnumerator RegenerateMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f / manaRegenerationRate);
            mana += 1;

            mana = Mathf.Clamp(mana, 0f, 100f);
        }
    }
}