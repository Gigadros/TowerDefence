using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    GameObject pathGO;
    Transform targetPathNode;
    int targetNodeIndex = 0;
    public float speed = 3f;
    bool isSlowed = false;
    float slowMultiplier, slowCooldown;
    //float rollRotation = -180f;
    public float maxHealth = 1f;
    public float currentHealth = 1f;
    public int reward = 2;
    // Use this for initialization
    void Start ()
    {
        pathGO = GameObject.Find("Path");
	}
	
    void GetNextPathNode()
    {
        try
        {
            targetPathNode = pathGO.transform.GetChild(targetNodeIndex);
            targetNodeIndex++;
        }
        catch
        {
            ReachedGoal();
        }
        
    }

    void ReachedGoal()
    {
        GameObject.FindObjectOfType<Score>().lives--;
        Die();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
            RewardPlayer();
        }
    }

    public void SlowDown(float slowDuration, float slowPower)
    {
        isSlowed = true;
        slowCooldown = slowDuration;
        slowMultiplier = slowPower;
    }


    public void Die()
    {
        // Reset health and pathing, then disable to return to object pool
        currentHealth = maxHealth;
        targetNodeIndex = 0;
        targetPathNode = pathGO.transform.GetChild(targetNodeIndex);
        isSlowed = false;
        gameObject.SetActive(false);
    }

    void RewardPlayer()
    {
        GameObject.FindObjectOfType<Score>().gold += reward;
        GameObject.FindObjectOfType<Score>().score += reward * 25;
    }

	// Update is called once per frame
	void Update ()
    {
        if (targetPathNode == null)
        {
            GetNextPathNode();
        }

        Vector3 dir = targetPathNode.position - this.transform.localPosition;
        float distThisFrame = speed * Time.deltaTime;
        if (isSlowed)
        {
            slowCooldown -= Time.deltaTime;
            if (slowCooldown > 0)
            {
                distThisFrame *= slowMultiplier;
            }
            else
            {
                isSlowed = false;
            }
        }

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
