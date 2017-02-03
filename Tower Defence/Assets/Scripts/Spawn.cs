using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public float spawnCooldown = 1.6f;
    public float spawnCooldownLeft = 0f;
    public float waveCooldown = 10f;
    int waveCount = 0;
    int waveSpawns = 10;
    int enemyCount = 0;
    public GameObject enemyGO;

	void SpawnEnemy () {
        Instantiate(enemyGO, this.transform.position, this.transform.rotation);
        enemyCount++;
	}

    void FixedUpdate()
    {
        spawnCooldownLeft -= Time.deltaTime;
        if (spawnCooldownLeft <= 0)
        {
            spawnCooldownLeft = spawnCooldown;
            SpawnEnemy();
            spawnCooldown *= 0.99f;
            if (enemyCount >= waveSpawns)
            {
                spawnCooldownLeft += waveCooldown;
                waveCount++;
                waveSpawns += 2;
                spawnCooldown *= 0.96f;
                enemyCount = 0;
            }

        }
    } 
}
