using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class nana : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";

    public Text connectionInfoText;
    public Button joinButton;

    private void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

        joinButton.interactable = false;
        connectionInfoText.text = "connecting..";
    }

    public override void OnConnectedToMaster()
    {
        //base.OnConnectedToMaster();
        joinButton.interactable = true;
        connectionInfoText.text = "Clear!";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        //base.OnDisconnected(cause);
        joinButton.interactable = false;
        connectionInfoText.text = "Replay..";

        PhotonNetwork.ConnectUsingSettings();
    }

    public void Connect()
    {
        joinButton.interactable = false;

        if (PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "room coming..";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            connectionInfoText.text = "master Replay";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // base.OnJoinRandomFailed(returnCode, message);
        connectionInfoText.text = "no Room";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    public override void OnJoinedRoom()
    {
        // base.OnJoinedRoom();
        connectionInfoText.text = "Clear!!";
        PhotonNetwork.LoadLevel("B.Lobby");
    }
}
