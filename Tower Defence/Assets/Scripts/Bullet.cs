using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 15f;
    public Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
    void BulletHit()
    {
        Destroy(gameObject);
    }

	// Update is called once per frame
	void FixedUpdate () {
        Vector3 dir = target.position - this.transform.localPosition;
        float distThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distThisFrame)
        {
            // Reached the path node
            BulletHit();
        }
        else
        {
            // Move towards next path node
            transform.Translate(dir.normalized * distThisFrame, Space.World);
            // Turn to face the next path node
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 3);
        }
    }
}
