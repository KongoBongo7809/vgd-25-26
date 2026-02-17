using UnityEngine;

public class ShirukenSpawn : MonoBehaviour
{
    public GameObject shirukenPrefab;
    public Transform attackPoint;
    public float shirukenForce = 20f;

    public void Attack()
    {
        //Create shiruken prefab
        GameObject shiruken = Instantiate(shirukenPrefab, attackPoint.position, attackPoint.rotation);
        Rigidbody2D rb = shiruken.GetComponent<Rigidbody2D>();

        //Rotate it towards the player and add force towards it
        Vector3 aimDirection = (PlayerManager.instance.player.transform.position - attackPoint.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        attackPoint.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        rb.AddForce(attackPoint.right * shirukenForce, ForceMode2D.Impulse);

        //Attach shiruken to specific enemy
        Shiruken newShiruken = shiruken.GetComponent<Shiruken>();
        newShiruken.setEnemy(transform.parent.gameObject.GetComponent<EnemyCombat>());
    }
}
