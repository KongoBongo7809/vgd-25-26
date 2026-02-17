using UnityEngine;

public class Shiruken : MonoBehaviour
{
    public GameObject sprite;
    public float rotationSpeed;
    private EnemyCombat enemy;
    public ParticleSystem impactEffect;

    void Update()
    {
        sprite.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        PlayerCombat player = hitInfo.GetComponent<PlayerCombat>();
        if (player != null)
        {
            player.TakeDamage(enemy.attackDamage);
        }
        ParticleSystem currentImpactEffect = Instantiate(impactEffect, transform.position, transform.rotation);
        currentImpactEffect.Play();
        
        Debug.Log(hitInfo.name);
        Destroy(gameObject);
    }

    public void setEnemy(EnemyCombat newEnemy)
    {
        enemy = newEnemy;
    }
}
