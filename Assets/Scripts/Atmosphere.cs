using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atmosphere : MonoBehaviour
{
    public float atmosphereRadius = 5f;
    // Start is called before the first frame update

    // Update is called once per frame
    public void FixedUpdate()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll((transform.position), atmosphereRadius))
        {
            // calculate direction from target to me
            //Vector3 forceDirection = transform.position - collider.transform.position;
            float distance = (transform.position - collider.transform.position).magnitude;

            // apply force on target towards me
            if (collider.GetComponent<Rigidbody2D>() != null)
            {
                float drag = (atmosphereRadius - distance - 0.2f) * 0.01f;
                drag = Mathf.Clamp(drag, 0f, 1f);
                collider.GetComponent<Rigidbody2D>().drag = drag;
                collider.GetComponent<Rigidbody2D>().angularDrag = drag;
            }
        }
    }
}
