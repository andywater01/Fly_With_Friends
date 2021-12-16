using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateAround : MonoBehaviour
{
    public GameObject pivotObject;
    public float rotSpeed = 5f;

    public Vector3 Direction = new Vector3();
    public Vector3 offset = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Direction = (transform.localPosition - pivotObject.transform.localPosition).normalized;
        //transform.RotateAround(pivotObject.transform.position, new Vector3(1, 0, 0), rotSpeed);

        RaycastHit raydata;
        if (Physics.Raycast(transform.position, Direction, out raydata))
        {
            Debug.DrawLine(transform.position, raydata.point, Color.red);
            Debug.DrawRay(raydata.point, raydata.normal * 1000, Color.red);

            offset = raydata.normal;

            
        }
        else
        {
            Debug.DrawRay(transform.position, pivotObject.transform.position * 1000, Color.black);
        }

        transform.localRotation = Quaternion.Euler(offset);

    }
}
