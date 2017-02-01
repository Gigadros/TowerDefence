using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    float range = 5f;
    float fireCooldown = 0.5f;
    float fireCooldownLeft = 0f;
    public GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
		
	}
    
    // Shoot the Enemy
    void Shoot(Enemy e)
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
        // TODO finish shoot function
    }

    // Update is called once per frame
    void Update()
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

            // Rotate tower to face enemy
            Vector3 dir = nearestEnemy.transform.position - this.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);

            fireCooldownLeft -= Time.deltaTime;
            if (fireCooldownLeft <= 0)
            {
                fireCooldownLeft = fireCooldown;
                Shoot(nearestEnemy);
            }
        }
    }
}
