using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartactorController : Photon.MonoBehaviour
{
    public float MoveX;
    public float MoveZ;
    public float RotX;
    public float RotZ;
    public bool Roar;
    public bool FireBall;
    public bool Jump;

    void Start()
    {

    }

    void Update()
    {
        if (PhotonNetwork.player.NickName == "Movey")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                photonView.RPC("MoveingZ", PhotonTargets.All, 5f);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                photonView.RPC("MoveingZ", PhotonTargets.All, -5f);
            }
            else
            {
                photonView.RPC("MoveingZ", PhotonTargets.All, 0f);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                photonView.RPC("MoveingX", PhotonTargets.All, 5f);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                photonView.RPC("MoveingX", PhotonTargets.All, -5f);
            }
            else
            {
                photonView.RPC("MoveingX", PhotonTargets.All, 0f);
            }
        }
        if (PhotonNetwork.player.NickName == "Balancy")
        {
            if (Input.GetKey(KeyCode.W))
            {
                photonView.RPC("RoteingZ", PhotonTargets.MasterClient, 5f);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                photonView.RPC("RoteingZ", PhotonTargets.MasterClient, -5f);
            }
            else
            {
                photonView.RPC("RoteingZ", PhotonTargets.MasterClient, 0f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                photonView.RPC("RoteingX", PhotonTargets.MasterClient, 5f);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                photonView.RPC("RoteingX", PhotonTargets.MasterClient, -5f);
            }
            else
            {
                photonView.RPC("RoteingX", PhotonTargets.MasterClient, 0f);
            }
        }
        if (PhotonNetwork.player.NickName == "Frecky")
        {
            if(Input.GetKey(KeyCode.H))
            {
                photonView.RPC("CallRoar", PhotonTargets.MasterClient, true);
            }
            else
            {
                photonView.RPC("CallRoar", PhotonTargets.MasterClient, false);
            }
            if (Input.GetKey(KeyCode.J))
            {
                photonView.RPC("CallFireBall", PhotonTargets.MasterClient, true);
            }
            else
            {
                photonView.RPC("CallFireBall", PhotonTargets.MasterClient, false);
            }
        }
    }

    [PunRPC]
    public void MoveingX(float x)
    {
        MoveX = x;
    }

    [PunRPC]
    public void MoveingZ(float z)
    {
        MoveZ = z;
    }

    [PunRPC]
    public void RoteingX(float x)
    {
        RotX = x;
    }

    [PunRPC]
    public void RoteingZ(float z)
    {
        RotZ = z;
    }

    [PunRPC]
    public void CallRoar(bool roar)
    {
        Roar = roar;
    }
    [PunRPC]
    public void CallFireBall(bool Fire)
    {
        FireBall = Fire;
    }
}
