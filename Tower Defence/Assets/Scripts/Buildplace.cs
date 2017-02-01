using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildplace : MonoBehaviour {

    // the tower that should be built
    public GameObject towerPrefab;

    private void OnMouseUpAsButton()
    {
        // build tower above buildplace
        GameObject g = (GameObject)Instantiate(towerPrefab);
        g.transform.position = transform.position + Vector3.up;
    }
}
