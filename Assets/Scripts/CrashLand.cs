using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrashLand : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject crashFab;
    public GameObject explosion;
    public TrailRenderer trail;
    public GameObject ghostTrailObject;
    //public LineRenderer ghostTrail;
    public Text textAttempts;
    int attempt = 1;
    LineRenderer ghostTrail;
    Vector3 respawnPos;
    Quaternion respawnRot;
    // Start is called before the first frame update
    void Start()
    {
        respawnPos = rb.transform.position;
        respawnRot = rb.transform.rotation;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CrashShip(collision.gameObject);
    }

    void CrashShip(GameObject parent)
    {
        GameObject crash = Instantiate(crashFab, rb.transform.position, rb.transform.rotation);
        Instantiate(explosion, rb.transform.position, rb.transform.rotation);
        crash.transform.SetParent(parent.transform);
        Respawn();
    }

    void Respawn()
    {
        addAttempt();

        rb.transform.position = respawnPos;
        rb.transform.rotation = respawnRot;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;

        turnOnTrail();
        trail.Clear();
    }

    void turnOnTrail()
    {
        //GameObject newTrail = Instantiate(trail, rb.transform.position, rb.transform.rotation);
        //newTrail.GetComponent<TrailRenderer>().autodestruct = true;
        Vector3[] positions = new Vector3[trail.positionCount];
        GameObject newTrail = Instantiate(ghostTrailObject, Vector3.zero, Quaternion.identity);
        LineRenderer ghostTrail = newTrail.GetComponent<LineRenderer>();
        trail.GetPositions(positions);
        ghostTrail.positionCount = trail.positionCount;
        ghostTrail.SetPositions(positions);
    }

    void addAttempt()
    {
        attempt++;
        textAttempts.text = "Attempt#: " + attempt.ToString();
    }
}
