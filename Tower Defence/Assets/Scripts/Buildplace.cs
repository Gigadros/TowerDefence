using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Buildplace : MonoBehaviour {

    // the tower that should be built
    public GameObject towerPrefab1, towerPrefab2;
    bool isTowerPlaced = false;

    private void OnMouseUpAsButton()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (!isTowerPlaced)
            {
                if (GameObject.FindObjectOfType<Score>().gold >= towerPrefab1.GetComponent<Tower1>().buildCost)
                {
                    // build tower above buildplace
                    GameObject g = (GameObject)Instantiate(towerPrefab1);
                    g.transform.position = transform.position;
                    isTowerPlaced = true;
                    GameObject.FindObjectOfType<Score>().gold -= towerPrefab1.GetComponent<Tower1>().buildCost;
                }
            }
        } 
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonUp(1)){
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (!isTowerPlaced)
                {
                    if (GameObject.FindObjectOfType<Score>().gold >= towerPrefab2.GetComponent<Tower2>().buildCost)
                    {
                        // build tower above buildplace
                        GameObject g = (GameObject)Instantiate(towerPrefab2);
                        g.transform.position = transform.position;
                        isTowerPlaced = true;
                        GameObject.FindObjectOfType<Score>().gold -= towerPrefab2.GetComponent<Tower2>().buildCost;
                    }
                }
            }
        }
    }


}
