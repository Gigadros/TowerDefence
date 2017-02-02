using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    GameObject pathGO;
    Transform targetPathNode;
    int targetNodeIndex = 0;
    float speed = 3f;
    //float rollRotation = -180f;
    public float health = 1f;
	// Use this for initialization
	void Start ()
    {
        pathGO = GameObject.Find("Path");
	}
	
    void GetNextPathNode()
    {
        targetPathNode = pathGO.transform.GetChild(targetNodeIndex);
        targetNodeIndex++;
    }

    void ReachedGoal()
    {
        // TODO subtract from player life
        Die();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // gameObject.SetActive(false);
        Destroy(gameObject);
    }

	// Update is called once per frame
	void Update ()
    {
        if (targetPathNode == null)
        {
            GetNextPathNode();
            if (targetPathNode == null)
            {
                // Run out of path
                ReachedGoal();
                return;
            }
        }

        Vector3 dir = targetPathNode.position - this.transform.localPosition;
        float distThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distThisFrame)
        {
            // Reached the path node
            targetPathNode = null;
        }
        else
        {
            // Move towards next path node
            transform.Translate(dir.normalized * distThisFrame, Space.World);
            // Turn to face the next path node
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 3);
            // Make the ball roll
            // rollRotation = rollRotation < 180 ? (rollRotation + Time.deltaTime) : -180f;
            // this.transform.Rotate(Vector3.right * rollRotation, Space.Self);
        }
	}
}
