using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public GameObject enemyGO; // enemy to shoot at
    public Tower tower; // tower shot from
	
    void BulletHit()
    {
        enemyGO.GetComponent<Enemy>().TakeDamage(tower.power);
        gameObject.SetActive(false);
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (enemyGO == null || !enemyGO.activeSelf)
        {
            gameObject.SetActive(false);
            return;
        }
        Vector3 dir = enemyGO.transform.position - this.transform.localPosition;
        float distThisFrame = tower.speed * Time.deltaTime;

        if (dir.magnitude <= distThisFrame)
        {
            // Reached the target
            BulletHit();
        }
        else
        {
            // Move towards the target
            transform.Translate(dir.normalized * distThisFrame, Space.World);
            // Turn to face the target
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 3);
        }
    }
}
