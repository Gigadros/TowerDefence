using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildplace : MonoBehaviour {

    // the tower that should be built
    public GameObject towerPrefab;
    bool isTowerPlaced = false;

    private void OnMouseUpAsButton()
    {
        if (!isTowerPlaced)
        {
            if (GameObject.FindObjectOfType<Score>().gold >= towerPrefab.GetComponent<Tower>().buildCost)
            {
                // build tower above buildplace
                GameObject g = (GameObject)Instantiate(towerPrefab);
                g.transform.position = transform.position + Vector3.up * 1.5f;
                isTowerPlaced = true;
                GameObject.FindObjectOfType<Score>().gold -= towerPrefab.GetComponent<Tower>().buildCost;
            }
        }
        
    }
}
