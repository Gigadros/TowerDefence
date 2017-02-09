using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower1 : MonoBehaviour {

    float range = 8f;
    public float fireCooldown = 0.5f;
    float fireCooldownLeft = 0f;
    public int buildCost = 60;
    int bulletPoolID = 0;

    public AudioClip shotSound;
    AudioSource source;
    float volMin = 0.25f, volMax = 0.4f, pitchMin = 0.4f, pitchMax = 0.55f;


	void Awake () {
        source = GetComponent<AudioSource>();
	}
    
    // Shoot the Enemy
    void Shoot(Enemy e)
    {
        GameObject bulletGO = ObjectPooler.current.GetPooledObject(bulletPoolID);
        if (bulletGO == null) return;

        bulletGO.transform.position = this.transform.position + Vector3.up;
        bulletGO.transform.rotation = this.transform.rotation;
        bulletGO.GetComponent<Bullet>().enemyGO = e.gameObject;
        bulletGO.SetActive(true);

        //play shot sound
        source.pitch = Random.Range(pitchMin, pitchMax);
        float vol = Random.Range(volMin, volMax);
        source.PlayOneShot(shotSound, vol);
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
