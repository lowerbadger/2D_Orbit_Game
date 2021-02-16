using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanderVisualize : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject mainEngine;
    public GameObject[] rcs;
    public AudioSource engineSFX;
    bool engineIsOn = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Lander Movement
    void FixedUpdate()
    {
        //trail.emitting = true;
        if (Input.GetKey(KeyCode.W))
        {
            //rb.AddForce(rb.transform.up * 10f);
            mainEngine.SetActive(true);
            if (!engineSFX.isPlaying && !engineIsOn)
            {
                engineSFX.Play();
                engineIsOn = true;
            }
        }
        else
        {
            mainEngine.SetActive(false);
            engineIsOn = false;
            engineSFX.Stop();
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(1f);
            rcs[1].SetActive(true);
            rcs[3].SetActive(true);
        }
        else
        {
            rcs[1].SetActive(false);
            rcs[3].SetActive(false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(-1f);
            rcs[0].SetActive(true);
            rcs[2].SetActive(true);
        }
        else
        {
            rcs[0].SetActive(false);
            rcs[2].SetActive(false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //rb.AddForce(-rb.transform.up * 10f);
            rcs[1].SetActive(true);
            rcs[2].SetActive(true);
        }
    }
}
