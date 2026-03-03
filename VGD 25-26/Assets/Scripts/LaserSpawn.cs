using UnityEngine;

public class LaserSpawn : MonoBehaviour
{
    //public GameObject redLaser, greenLaser, blueLaser;
    public float laserForce;

    public Transform attackPoint;
    public GameObject[] lasers;

    private void Start()
    {
        //lasers = new[] { redLaser, greenLaser, blueLaser };
    }

    public void SpawnLaser(int randPrefab)
    {
        GameObject prefab = lasers[randPrefab];
        /*if(prefab != null)
        {
            Debug.Log("Prefab not found");
            return;
        }*/

        //Create laser prefab
        GameObject laser = Instantiate(prefab, attackPoint.position, attackPoint.rotation);
        Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();

        //Rotate it towards player and add force towards it
        Vector3 aimDirection = (PlayerManager.instance.player.transform.position - attackPoint.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        attackPoint.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        rb.AddForce(attackPoint.right * laserForce, ForceMode2D.Impulse);
    }
}
