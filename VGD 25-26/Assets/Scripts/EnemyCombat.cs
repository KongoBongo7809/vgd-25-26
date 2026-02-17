using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    //Animation
    public Animator animator;
    public ParticleSystem deathEffect;

    //Enemy stats
    public int maxHealth = 100;
    public int currentHealth;

    //Awareness
    public float awarenessRange = 5f;
    private bool flip = true;

    //Attack stats
    public Transform attackPoint;
    public LayerMask playerLayer;
    private ShirukenSpawn newShiruken;
    public int attackDamage = 15;

    public float attackRate = 1f;
    float nextAttackTime;

    //Extra
    bool playerInRangeLastFrame = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        //Flip the enemy to face the player
        Vector3 scale = transform.localScale;

        if (PlayerManager.instance.player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        }

        transform.localScale = scale;

        //Check if player can be attacked
        if (Time.time >= nextAttackTime)
        {
            //If so, attack player
            if (playerInRange())
            {
                animator.SetTrigger("Attack");
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        /*if (Input.GetKeyDown(KeyCode.B))
        {
            //Trigger attack animation
            animator.SetTrigger("Attack");
        }*/
    }

    bool playerInRange()
    {
        return Physics2D.OverlapCircle(transform.position, awarenessRange, playerLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, awarenessRange);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Play hurt animation
        //animator.SetTrigger("Hurt");
        //FindObjectOfType<AudioManager>().Play("Enemy Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        ParticleSystem currentDeathEffect = Instantiate(deathEffect, transform.position, transform.rotation);
        currentDeathEffect.Play();
        Destroy(this.gameObject);
    }

/*private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (playerInRange())
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        //Player in range audio
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (playerInRange() && !playerInRangeLastFrame)
        {
            audioManager.Play("Enemy Growl");
        }
        playerInRangeLastFrame = playerInRange();
    }

    bool playerInRange()
    {
        return Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Play hurt animation
        animator.SetTrigger("Hurt");
        FindObjectOfType<AudioManager>().Play("Enemy Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }*/
}

    