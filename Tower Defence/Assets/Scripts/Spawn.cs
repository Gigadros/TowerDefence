using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public float spawnTimer = 1f;
    public GameObject enemyGO;

	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnEnemy", spawnTimer, spawnTimer);
	}
	
	// Update is called once per frame
	void SpawnEnemy () {
        Instantiate(enemyGO, this.transform.position, this.transform.rotation);
	}
}
