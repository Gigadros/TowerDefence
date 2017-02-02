using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    // Make the object pooler easily accessible without keeping a reference to it
    public static ObjectPooler current;

    public GameObject pooledObject;
    public int pooledAmount = 20;
    public bool willGrow = true;

    List<GameObject> objectPool;

    void Awake()
    {
        current = this;
    }

	// Use this for initialization
	void Start () {
        // Build a pool of objects
		objectPool = new List<GameObject>();
        for(int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        // Search the object pool for an inactive object
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                return objectPool[i];
            }
        }
        // Create a new object if there are no inactive ones, and the pool is allowed to grow
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            objectPool.Add(obj);
            return obj;
        }
        // default in the event that the object pool cannot return an object to use
        return null;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
