using UnityEngine;

public class Laser : MonoBehaviour
{
    public float attackDamage = 25f;
    public ParticleSystem impactEffect;

    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        PlayerCombat player = hitInfo.GetComponent<PlayerCombat>();
        if (player != null)
        {
            player.TakeDamage(attackDamage);
        }
        ParticleSystem currentImpactEffect = Instantiate(impactEffect, transform.position, transform.rotation);
        currentImpactEffect.Play();

        Destroy(gameObject);
    }
}
