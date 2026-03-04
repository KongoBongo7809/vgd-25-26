using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensei : MonoBehaviour
{
    //General Boss Battle Parameters
    public float attackDamage = 25f;
    public float waveTime = 30f;
    public float timeBetweenWaves = 10f;
    float timer = 0f;
    public SenseiCutscenes cutscenes;
    int waveIndex = 0;

    //Wave One - Laser Spawn
    public LaserSpawn laserSpawn;
    public float attackRate = 0.5f;
    float nextAttackTime;

    //Wave Two - Enemies
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    private List<GameObject> spawnedEnemies;
    public float spawnRate = 5f;
    float nextEnemySpawn;

    //Wave Three - Platforms

    //Wave Four - Final Stage

    public void Start()
    {
        spawnedEnemies = new List<GameObject>();
    }

    public void LoadNextWave()
    {
        waveIndex++;
        StartCoroutine(selectedWave(waveIndex));
    }

    private IEnumerator selectedWave(int index)
    {
        switch(index)
        {
            case 1:
                return WaveOne();
                break;
            case 2:
                return WaveTwo();
                break;
            case 3:
                return WaveThree();
                break;
            case 4:
                return WaveFour();
                break;
            default:
                return WaveOne();
                break;
        }
    }

    IEnumerator WaveOne()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Start Wave");
        while(timer < waveTime)
        {
            if (Time.time >= nextAttackTime)
            {
                FindFirstObjectByType<AudioManager>().Play("Enemy Attack");
                laserSpawn.SpawnLaser(UnityEngine.Random.Range(0, 2));
                nextAttackTime = Time.time + 1f / attackRate;
            }
            yield return null;
            timer += Time.deltaTime;
        }
        timer = 0f;
        yield return new WaitForSeconds(1);
        cutscenes.TriggerCutscene();
        yield return new WaitForSeconds(timeBetweenWaves);
        Debug.Log("End Wave");
    }

    IEnumerator WaveTwo()
    {
        yield return new WaitForSeconds(1);
        while(timer < waveTime)
        {
            if (Time.time >= nextEnemySpawn)
            {
                foreach(Transform t in spawnPoints)
                {
                    GameObject enemy = Instantiate(enemyPrefab, t.position, t.rotation);
                    spawnedEnemies.Add(enemy);
                    nextEnemySpawn = Time.time + 1f / spawnRate;
                }
            }
        }
        timer = 0f;
        foreach(GameObject e in spawnedEnemies)
        {
            Destroy(e);
        }
        yield return new WaitForSeconds(1);
        cutscenes.TriggerCutscene();
        yield return new WaitForSeconds(timeBetweenWaves);
    }

    IEnumerator WaveThree()
    {
        yield return new WaitForSeconds(1);
    }

    IEnumerator WaveFour()
    {
        yield return new WaitForSeconds(1);
    }
}
