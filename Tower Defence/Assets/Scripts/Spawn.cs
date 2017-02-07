using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public float spawnCooldown = 1.6f;
    public float spawnCooldownLeft = 5f;
    public float waveCooldown = 10f;
    public int waveCount = 1;
    bool nextWave;
    int waveSpawns = 10;
    int enemyCount = 0;
    public GameObject enemyGO;
    int enemyPoolID = 1;

    void SpawnEnemy () {
        enemyGO = ObjectPooler.current.GetPooledObject(enemyPoolID);
        enemyGO.transform.position = this.transform.position;
        enemyGO.transform.rotation = this.transform.rotation;
        enemyGO.SetActive(true);
        enemyCount++;
	}

    void FixedUpdate()
    {
        spawnCooldownLeft -= Time.deltaTime;
        if (spawnCooldownLeft <= 0)
        {
            if(nextWave == true)
            {
                nextWave = false;
                waveCount++;
            }
            spawnCooldownLeft = spawnCooldown;
            SpawnEnemy();
            spawnCooldown *= 0.99f;
            if (enemyCount >= waveSpawns)
            {
                spawnCooldownLeft += waveCooldown;
                nextWave = true;
                waveSpawns += 2;
                spawnCooldown *= 0.96f;
                enemyCount = 0;
            }

        }
    } 
}
