using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using UnityEngine.UI;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject cameraPrefab; //*
    new GameObject camera;
    public GameObject c_VirtualCamera;

    public float minX, maxX;
    public float minZ, maxZ;

    void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 0f, Random.Range(minZ, maxZ));
        //PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        camera = Instantiate(cameraPrefab, randomPosition, Quaternion.identity);


        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        

        if (player.GetComponent<PhotonView>().IsMine)
        {
            
            //camera.GetComponent<CinemachineFreeLook>().setLookAt(player.transform.Find("SpeedText").GetComponent<GameObject>());
            player.GetComponent<PlaneMove>().SetSpeedText(camera);
            player.GetComponent<PlaneMove>().SetTimerText(camera);
            player.GetComponent<PlaneMove>().SetWind(camera);
            //player.GetComponentInChildren<CinemachineFreeLook>().SetLookAt(player);
           
            c_VirtualCamera = camera.GetComponentInChildren<CinemachineFreeLook>().GetCam();

            c_VirtualCamera.GetComponent<CinemachineFreeLook>().Follow = player.transform;
            c_VirtualCamera.GetComponent<CinemachineFreeLook>().LookAt = player.transform.Find("LookAt");
            //player.GetComponent<PlayerController>().SetJoysticks(Instantiate(cameraPrefab, randomPosition, Quaternion.identity));
            //player.GetComponent<mobileRotation>().SetJoystick(camera);
            //player.GetComponent<mobileControls>().SetJoystick(camera);
            //player.GetComponent<mobileControls>().SetCamera(camera.GetComponentInChildren<Camera>().gameObject);

            //camera.GetComponentInChildren<Camera>().gameObject.GetComponent<TransformFollower>().SetTarget(player);
        }
            
    }


}
