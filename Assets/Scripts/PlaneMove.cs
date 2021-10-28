using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlaneMove : MonoBehaviour
{
    public float speed;
    public float vertSpeed;
    public float turnSpeed;
    public float rotateSpeed;
    public Rigidbody rb;
    public float timer = 0.0f;
    public float rotation;
    public TextMeshProUGUI speedText;
    public ParticleSystem explosion;
    private bool hasCrashed = false;
    public AudioSource crash;
    public AudioSource engine;
    public GameObject spawnLoc;
    public Vector3 spawnRot;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //mouseDelta.x = Mathf.Clamp(mouseDelta.x, -1, 1);
        //mouseDelta.y = Mathf.Clamp(mouseDelta.y, -1, 1);
        //scrollValue = Mathf.Clamp(scrollValue, -1, 1);
    }


    void Update()
    {
        rotation = transform.localRotation.eulerAngles.x;
        if (hasCrashed == false)
        {
            Controls();
        }
        
        GetSpeed();




    }

    public void GetSpeed()
    {
        speedText.text = Mathf.RoundToInt(speed).ToString();
        
    }

    

    public void Controls()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.A))
        {
            //transform.RotateAroundLocal(Vector3.forward, turnSpeed);
            transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //transform.RotateAroundLocal(Vector3.forward, -turnSpeed);
            transform.Rotate(Vector3.forward * Time.deltaTime * -rotateSpeed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(Vector3.right * Time.deltaTime * vertSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(Vector3.right * Time.deltaTime * -vertSpeed);
        }

        if (Input.GetMouseButton(0))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * -turnSpeed);
        }
        else if (Input.GetMouseButton(1))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed);
        }


        if (transform.localRotation.eulerAngles.x < 335 && transform.localRotation.eulerAngles.x > 270 && speed >= 8.0f)
        {


            speed -= Time.deltaTime;

        }
        else if (transform.localRotation.eulerAngles.x > 12 && transform.localRotation.eulerAngles.x < 100 && speed <= 18.0f)
        {


            speed += Time.deltaTime;

        }
        else
        {
            if (speed < 12)
            {
                speed += Time.deltaTime;
            }
            else if (speed > 12)
            {
                speed -= Time.deltaTime;
            }
            //timer = 0.0f;
        }
    }

    public void Respawn()
    {
        hasCrashed = false;
        engine.Play();
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        speed = 8;
        
        this.gameObject.transform.rotation = Quaternion.EulerAngles(spawnRot);
        this.gameObject.transform.position = spawnLoc.transform.position;

    }


    private IEnumerator OnCollisionEnter(Collision collision)
    {
        if (speed >= 8 || hasCrashed == true)
        {
            explosion.Play();
            crash.Play();
            engine.Stop();

            rb.useGravity = enabled;
            speed = 0.0f;
            hasCrashed = true;

            yield return new WaitForSeconds(3);

            Respawn();

        }
    }
}