using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    float range = 8f;
    public float fireCooldown = 0.5f;
    float fireCooldownLeft = 0f;
    public int buildCost = 60;
    public GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
		
	}
    
    // Shoot the Enemy
    void Shoot(Enemy e)
    {
        GameObject bulletGO = ObjectPooler.current.GetPooledObject();
        if (bulletGO == null) return;

        bulletGO.transform.position = this.transform.position;
        bulletGO.transform.rotation = this.transform.rotation;
        bulletGO.GetComponent<Bullet>().enemyGO = e.gameObject;
        bulletGO.SetActive(true);
    }

    void FixedUpdate()
    {
        // TODO optimize this!
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();

        Enemy nearestEnemy = null;
        float dist = Mathf.Infinity;

        // Find nearest enemy
        foreach (Enemy e in enemies)
        {
            float d = Vector3.Distance(this.transform.position, e.transform.position);
            if (nearestEnemy == null || d < dist)
            {
                nearestEnemy = e;
                dist = d;
            }
            if (nearestEnemy == null)
            {
                Debug.Log("No enemies?");
            }
            if (dist <= range * 1.5)
            {
                // Rotate tower to face enemy
                Vector3 dir = nearestEnemy.transform.position - this.transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                this.transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
                // TODO smooth out tower facing transition to new enemy
            }
        }
        fireCooldownLeft -= Time.deltaTime;
        if (fireCooldownLeft <= 0 && dist <= range)
        {
            fireCooldownLeft = fireCooldown;
            Shoot(nearestEnemy);
        }
    }
}
