using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public float pullRadius = 30;
    public float mass = 1f;
    public bool massAsDensity = true;

    private void Start()
    {
        if (massAsDensity)
        {
            mass *= this.transform.localScale.sqrMagnitude;
        }
    }

    public void FixedUpdate()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll((transform.position), pullRadius)) {
            // calculate direction from target to me
            Vector3 forceDirection = transform.position - collider.transform.position;
            float squareDistance = forceDirection.sqrMagnitude;

            // apply force on target towards me
            if (collider.GetComponent<Rigidbody2D>() != null)
            {
                collider.GetComponent<Rigidbody2D>().AddForce(mass / squareDistance * forceDirection.normalized);
            }
            
        }
    }
}
