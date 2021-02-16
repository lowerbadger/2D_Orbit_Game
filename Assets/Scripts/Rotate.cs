using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotateSpeed = 15f;  //speed in dps

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(rotateSpeed * Vector3.forward * Time.fixedDeltaTime);
    }
}
