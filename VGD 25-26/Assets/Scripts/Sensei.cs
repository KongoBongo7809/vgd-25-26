using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensei : MonoBehaviour
{
    //General Boss Battle Parameters
    public float attackDamage = 25f;
    public float waveTime = 30f;
    public float timeBetweenWaves = 10f;
    float currentWaveTime = 0f;

    //Wave One - Laser Spawn
    public LaserSpawn laserSpawn;
    public float attackRate = 0.5f;
    float nextAttackTime;

    private void Start()
    {
        StartCoroutine(WaveOne());
        //if on attack mode, run WaveOne();

        /*if (Time.time >= nextAttackTime)
        {
            FindFirstObjectByType<AudioManager>().Play("Enemy Attack");
            int randInt = Random.Range(0,3);
            Debug.Log(randInt);
            laserSpawn.SpawnLaser(randInt);
            nextAttackTime = Time.time + 1f / attackRate;
        }*/
    }

    /*private void Start()
    {
        StartCoroutine(StartWave());
        currentWaveTime = 0f;
    }

    public void WaveOne()
    {
        if(Time.time < waveTime)
        {
            if (Time.time >= nextAttackTime)
            {
                FindFirstObjectByType<AudioManager>().Play("Enemy Attack");
                laserSpawn.SpawnLaser(Random.Range(0, 2));
                nextAttackTime = Time.time + 1f / attackRate;
            }
            currentWaveTime += Time.deltaTime; 
        }
    }

    IEnumerator StartWave()
    {
        WaveOne();
        yield return new WaitForSeconds(waveTime);
        yield return new WaitForSeconds(timeBetweenWaves);
        WaveOne();
    }*/

    IEnumerator WaveOne()
    {
        //Debug.Log("Start Wave");
        while(currentWaveTime < waveTime)
        {
            if (Time.time >= nextAttackTime)
            {
                FindFirstObjectByType<AudioManager>().Play("Enemy Attack");
                laserSpawn.SpawnLaser(Random.Range(0, 3));
                nextAttackTime = Time.time + 1f / attackRate;
            }
            currentWaveTime += Time.deltaTime;
            Debug.Log(currentWaveTime);
        }
        //Debug.Log("End Wave");
        //currentWaveTime = 0f;
        yield return new WaitForSeconds(timeBetweenWaves);
    }
}
