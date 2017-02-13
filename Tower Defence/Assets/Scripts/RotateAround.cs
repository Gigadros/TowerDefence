using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour {

    public int speed = 40;

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.LeftArrow)) // rotate clockwise
        {
            gameObject.transform.RotateAround(Vector3.zero, Vector3.up, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow)) // rotate counter-clockwise
        {
            gameObject.transform.RotateAround(Vector3.zero, Vector3.up, -speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow)) // zoom in
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, Vector3.zero, speed * Time.deltaTime);
            if (newPosition != Vector3.zero)
            {
                transform.position = newPosition;
            }
        }
        if (Input.GetKey(KeyCode.DownArrow)) // zoom out
        {
            transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, -speed * Time.deltaTime);
        }
    }
}
