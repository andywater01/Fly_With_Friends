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
    public TextMeshProUGUI timerText;
    public ParticleSystem explosion;
    private bool hasCrashed = false;
    public AudioSource crash;
    public AudioSource engine;
    public AudioSource woosh;
    
    public Vector3 spawnRot;
    private float gameTimer = 0.0f;
    private bool isFinished = false;

    //public GameObject world;


   // public Vector3 offset = new Vector3();

    public GameObject Wind;

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

        //RaycastHit raydata;
        //if (Physics.Raycast(transform.position, -transform.up, out raydata))
        //{
        //    Debug.DrawLine(transform.position, raydata.point, Color.red);
        //    Debug.DrawRay(raydata.point, raydata.normal * 1000, Color.red);

        //    //offset = raydata.normal;
        //}
        //else
        //{
        //    //Debug.DrawRay(transform.position, -transform.up * 1000, Color.black);
        //}




        rotation = transform.localRotation.eulerAngles.x;
        if (hasCrashed == false)
        {
            Controls();
        }
        
        SpeedToText();

        if (speed > 13.0f && speed <= 16.0f)
        {
            Wind.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            Wind.SetActive(true);
        }
        else if (speed > 16.0f)
        {
            Wind.SetActive(true);
            Wind.transform.localScale = new Vector3(0.8f, 1.0f, 1.0f);
        }
        else if (speed < 11.0f)
        {
            Wind.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            Wind.SetActive(false);
        }
        else
        {
            Wind.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            Wind.SetActive(false);
        }


    }

    private void LateUpdate()
    {
        if (isFinished == false)
        {
            gameTimer += Time.deltaTime;

            float output = Mathf.Round(gameTimer * 10.0f) * 0.1f;
            timerText.text = output.ToString();
        }

    }


    public void SetTimerText(GameObject go)
    {
        TextMeshProUGUI[] joys = go.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI joy in joys)
        {
            if (joy.name == "Timer")
                timerText = joy;
        }

    }

    public void SpeedToText()
    {
        speedText.text = Mathf.RoundToInt(speed).ToString();
        
    }
    public void SetSpeedText(GameObject go)
    {
        TextMeshProUGUI[] joys = go.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI joy in joys)
        {
            if (joy.name == "SpeedText")
                speedText = joy;
        }
        
    }

    public void SetWind(GameObject go)
    {
        Image[] joys = go.GetComponentsInChildren<Image>();
        foreach (Image joy in joys)
        {
            if (joy.name == "WindPanel")
                Wind = joy.gameObject;
        }

    }
    


    public float GetSpeed()
    {
        return speed;
    }

    

    public void Controls()
    {
        transform.Translate((Vector3.forward)* Time.deltaTime * speed);

        //transform.localRotation = Quaternion.EulerRotation(-offset.x, 0.0f, 0.0f);
        //transform.RotateAround(world.transform.position, new Vector3(offset.x * 10, 0, 0),  Time.deltaTime);

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

    public void MobileControls()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i <= Input.touchCount; i++)
            {
                
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            }

            

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
        
        this.gameObject.transform.rotation = Quaternion.Euler(spawnRot);
        this.gameObject.transform.position = new Vector3(0.0f, 0.0f, 878.5f);

    }


    private IEnumerator OnCollisionEnter(Collision collision)
    {
        if (speed >= 8 || hasCrashed == true && collision.gameObject.tag != "boost" && collision.gameObject.tag != "FinishLine")
        {
            if (!explosion.isPlaying)
            {
                explosion.Play();
            }
            if (!crash.isPlaying)
            {
                crash.Play();
            }
            
            engine.Stop();

            rb.useGravity = enabled;
            speed = 0.0f;
            hasCrashed = true;

            yield return new WaitForSeconds(3);

            Respawn();

        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "boost")
        {
            speed = speed + 3.0f;
            woosh.Play();
        }

        else if (other.gameObject.tag == "FinishLine")
        {
            // Race finished
            isFinished = true;
            
        }
    }

}
