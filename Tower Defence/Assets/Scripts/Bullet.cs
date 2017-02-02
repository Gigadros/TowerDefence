using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float power = 0.3f;
    public float speed = 15f;
    public GameObject enemyGO;

	// Use this for initialization
	void Start () {
		
	}
	
    void BulletHit()
    {
        enemyGO.GetComponent<Enemy>().TakeDamage(power);
        gameObject.SetActive(false);
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (enemyGO == null)
        {
            gameObject.SetActive(false);
            return;
        }
        Vector3 dir = enemyGO.transform.position - this.transform.localPosition;
        float distThisFrame = speed * Time.deltaTime;

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
