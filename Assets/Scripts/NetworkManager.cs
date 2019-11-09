using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using Valve.VR.InteractionSystem;

public class NetworkManager : Photon.MonoBehaviour
{
    public static NetworkManager instance;
    public string roomName;
    public CloudRegionCode SelectRegion;
    public List<String> nameList;
    public string MyName;
    public bool isSetName;
    public ChartactorController Controller;

    public void Awake()
    {
        InstanceNetworkManager();
        //DontDestroyOnLoad(gameObject);
    }

    //玩家初始化設定
    public void Start()
    {
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.offlineMode = false;
        PhotonNetwork.ConnectToRegion(SelectRegion, "1.0");
        PhotonNetwork.sendRate = 30;
        PhotonNetwork.sendRateOnSerialize = 30;
    }

    private void Update()
    {

    }

    /// <summary>
    /// NetworkManager 單例檢查
    /// </summary>
    void InstanceNetworkManager()
    {
        if (instance != null)
        {
            DestroyImmediate(this);
        }
        else
        {
            instance = this;
        }
    }

    //與伺服器連接
    public void OnConnectedToMaster()
    {
        Debug.Log("As OnConnectedToMaster() got called, the PhotonServerSetting.AutoJoinLobby must be off. Joining lobby by calling PhotonNetwork.JoinLobby().");
        PhotonNetwork.JoinLobby();
    }

    public void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions() { IsVisible = true, MaxPlayers = 4, }, null);
    }

    //加入房間完畢
    public void OnJoinedRoom()
    {
        if(PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.player.NickName = "DrunkMan";
        }
        else
        {
            for(int i = 0;i<PhotonNetwork.playerList.Length;i++)
            {
                foreach(var name in nameList)
                {
                    if (PhotonNetwork.playerList[i].NickName != name && !isSetName)
                    {
                        PhotonNetwork.player.NickName = name;
                        isSetName = true;
                    }
                }
            }
        }
        MyName = PhotonNetwork.player.NickName;
        Controller.enabled = true;
    }

    public void StartGame() 
    {
        if(PhotonNetwork.isMasterClient)
        {
            photonView.RPC("SyncStartGame", PhotonTargets.All, true);
        }
    }

    [PunRPC]
    public void SyncStartGame(bool start)
    {
        GetComponent<IconManager>().enabled = true;
    }

    //創建房間失敗，房間名稱與已存在房間重複
    public void OnPhotonCreateRoomFailed()
    {
        Debug.Log("OnPhotonCreateRoomFailed got called. This can happen if the room exists (even if not visible). Try another room name.");
    }

    //加入房間失敗，房間不存在、房間已滿、房間已關閉
    public void OnPhotonJoinRoomFailed(object[] cause)
    {
        Debug.Log("OnPhotonJoinRoomFailed got called. This can happen if the room is not existing or full or closed.");
    }

    //隨機加入失敗，目前暫時沒有房間可以加入
    public void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonRandomJoinFailed got called. Happens if no room is available (or all full or invisible or closed). JoinrRandom filter-options can limit available rooms.");
    }

    //與伺服器斷線
    public void OnDisconnectedFromPhoton()
    {
        Debug.Log("Disconnected from Photon.");
    }

    //與伺服器連接失敗
    public void OnFailedToConnectToPhoton(object parameters)
    {
        Debug.Log("OnFailedToConnectToPhoton. StatusCode: " + parameters + " ServerAddress: " + PhotonNetwork.ServerAddress);
    }
}
