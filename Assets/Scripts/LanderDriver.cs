using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanderDriver : MonoBehaviour
{
    public delegate void winLevel();
    public static event winLevel OnLand;

    Rigidbody2D rb;
    TrailRenderer trail;
    Vector3 boundTopRight;
    float screenMargin = -2f;
    float cornerAngle;
    float rDist;
    bool counting = false;
    bool win = false;
    float countdown = 0f;
    float timer = 3f;
    bool engineIsOn = false;

    public GameObject boundImage;
    public GameObject resetText;
    public GameObject mainEngine;
    public GameObject[] rcs;
    public AudioSource engineSFX;
    public AudioSource hitSFX;
    public AudioSource winSFX;

    //public Collider2D Head;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        trail = this.GetComponent<TrailRenderer>();
        boundTopRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        boundTopRight += new Vector3(screenMargin, screenMargin, 0f);
        cornerAngle = Mathf.Atan(boundTopRight.y/ boundTopRight.x);
        rDist = new Vector2(boundTopRight.x, boundTopRight.y).magnitude;
        //* 0.87f
        //print(boundTopRight);
    }

    // Update is called once per frame
    private void Update()
    {
        //Check if lander is within bounds
        if (transform.position.x > -boundTopRight.x && transform.position.y > -boundTopRight.y
            && transform.position.x < boundTopRight.x && transform.position.y < boundTopRight.y)
        {
            //print("inbounds");
            boundImage.SetActive(false);
        }
        else
        {
            //print("out of bounds");
            boundImage.SetActive(true);
            float angle = Mathf.Atan2(transform.position.y, transform.position.x);
            //print(angle);
            if (Mathf.Abs(angle) < cornerAngle || Mathf.Abs(angle) > Mathf.PI - cornerAngle)
            {
                if (Mathf.Abs(angle) > Mathf.PI/2)
                {
                    boundImage.transform.position = new Vector3(-boundTopRight.x, rDist * Mathf.Sin(angle));
                }
                else
                {
                    boundImage.transform.position = new Vector3(boundTopRight.x, rDist * Mathf.Sin(angle));
                }
            }
            else
            {
                float ratio = boundTopRight.y / boundTopRight.x;
                if (angle > 0)
                {
                    boundImage.transform.position = new Vector3(rDist * Mathf.Cos(angle) * 1f, boundTopRight.y);
                }
                else
                {
                    boundImage.transform.position = new Vector3(rDist * Mathf.Cos(angle) * 1f, -boundTopRight.y);
                }
                
            }
        }
        resetText.SetActive(transform.position.magnitude > 20f);
    }

    //Lander Movement
    void FixedUpdate()
    {
        //trail.emitting = true;
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(rb.transform.up * 10f);
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
            rb.AddForce(-rb.transform.up * 10f);
            rcs[1].SetActive(true);
            rcs[2].SetActive(true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        hitSFX.Play();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            if (!counting)
            {
                counting = true;
                countdown = Time.time + timer;
                //print("counting down");
            }
            else if (!win && countdown < Time.time)
            {
                win = true;
                winSFX.Play();
                if (OnLand != null)
                {
                    OnLand();
                }
                //print("success!");
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            counting = false;
        }
    }
}
