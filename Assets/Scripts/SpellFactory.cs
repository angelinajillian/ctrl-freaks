using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class SpellFactory : MonoBehaviour
{
    // Animator class for handling animations (obviously)
    private Animator animator;
    private bool canFire = true;
    // private float fireCooldown = 0.6f; 
    private float mana;
    private float manaRegenerationRate = 2.5f;
    
    [SerializeField] private GameObject fistProjectile1;

    [SerializeField] private GameObject fistProjectile1Point;
    [SerializeField] private GameObject proj1CrossHair;
    [SerializeField] private GameObject AOEPrefab;
    [SerializeField] private GameObject HandGrenadePrefab;
    [SerializeField] private PlayerControllerExtended playerControllerExtended;

    // Kicking variables
    private float kickDamage = 0.0f; // no dam so far, maybe up it through upgrades in shop
    private float kickRange = 3.5f; // the max range kick reaches out to
    private bool canKick = true;

    //Grenade variables
    private bool isGrenadeHeld = false;
    private GameObject currentGrenade;
    private HandGrenadeController hgc;

    // Declaring a variable of type ManaBar
    public ManaBar manaBar;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(RegenerateMana());
    }

    void Update()
    {
        if (hgc == null && isGrenadeHeld)
        {
            Debug.Log("held grenade too long!");
            isGrenadeHeld = false;
            animator.SetTrigger("ExplodeTrigger");
        }

        if (isGrenadeHeld)
        {
            //var pos = fistProjectile1Point.transform.position;
            hgc.SetPosition(fistProjectile1Point);
        }

        mana = playerControllerExtended.currMana;

        Fire1Punch();
        Fire2Kick();
        AOESpell();
        HandGrenade();
    }

    private void Fire1Punch()
    {
        if (Input.GetButtonDown("Fire1") & canFire & playerControllerExtended.currMana >= 7f)
        {
            ReduceMana(7);
            // Debug.Log($"Mana: {playerControllerExtended.currMana}");
            canFire = false;
            animator.SetTrigger("Fire1Trigger");
            // Fire1Projectile();
        }
    }

    void Fire1Projectile()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.Normalize();

        GameObject fistProjectile = Instantiate(fistProjectile1, fistProjectile1Point.transform.position, Quaternion.LookRotation(cameraForward) * Quaternion.Euler(0, 180f, 0));
        
        FistProjectile1Controller projectileController = fistProjectile.GetComponent<FistProjectile1Controller>();

        // Set the target for the projectile
        projectileController.SetTarget(proj1CrossHair);
        
        canFire = true;
        // StartCoroutine(FireCooldown());
    }

    
    // private IEnumerator FireCooldown()
    // {
    //     yield return new WaitForSeconds(fireCooldown);
    //     canFire = true; 
    // }

    private void Fire2Kick()
    {
        if (Input.GetButtonDown("Fire2") && canKick)
        {
            // calls kickAttack in animator (in unity editor).
            animator.SetTrigger("PunchTrigger");
        }
    }

    public void kickAttack()
    {
        playerControllerExtended.setCanTakeDamage(false);

        // Debug.Log("punching!!");

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

            if (hit.collider.tag == "Barrel")
            {
                var barrel = hit.transform.gameObject;
                var rb = barrel.GetComponent<Rigidbody>();
                rb.AddForce(playerDirection * 1000f);
            }
        }

        playerControllerExtended.setCanTakeDamage(true);
        canKick = true;
    }

    public void AOESpell()
    {
        if (Input.GetButtonDown("k") & canFire & playerControllerExtended.currMana >= 40f)
        {
            ReduceMana(40f);
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
        // StartCoroutine(FireCooldown());
        canFire = true;
    }

    private void HandGrenade()
    {
        if (Input.GetKeyDown(KeyCode.G) & mana >= 15f & !isGrenadeHeld)
        {
            Debug.Log("holding grenade...");
            isGrenadeHeld = true;
            ReduceMana(15f);
            Debug.Log($"Mana: {mana}");
            animator.SetTrigger("GrenadeTrigger");
        }

        if (isGrenadeHeld && Input.GetKeyUp(KeyCode.G))
        {
            Debug.Log("launching grenade!!!");
            isGrenadeHeld = false;
            animator.SetTrigger("GrenadeThrowTrigger");
            hgc.Launch(fistProjectile1Point);
        }
    }

    public void ThrowGrenade()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.Normalize();

        currentGrenade = Instantiate(HandGrenadePrefab, fistProjectile1Point.transform.position, Quaternion.LookRotation(cameraForward) * Quaternion.Euler(0, 180f, 0));
        hgc = currentGrenade.GetComponent<HandGrenadeController>();
        hgc.Hold();
    }


    IEnumerator RegenerateMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f / manaRegenerationRate);
            IncreaseMana(1);
            playerControllerExtended.currMana = Mathf.Clamp(playerControllerExtended.currMana, 0f, 100f);
        }
    }

    void ReduceMana(float manaReduce)
    {
        // Deduct mana from playerMana
        playerControllerExtended.currMana -= manaReduce;
        // Update mana bar
        manaBar.SetMana(playerControllerExtended.currMana);
    }

    void IncreaseMana(float manaIncrease)
    {
        // Increase mana on playerMana
        playerControllerExtended.currMana += manaIncrease;
        // Update mana bar
        manaBar.SetMana(playerControllerExtended.currMana);
    }
}