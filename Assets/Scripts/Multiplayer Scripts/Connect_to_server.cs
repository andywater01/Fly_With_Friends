using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class Connect_to_server : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnConnectToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    // Update is called once per frame
    public void Update()
    {
        SceneManager.LoadScene("Menu");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 100).ToString("000"); //*
    }
}
