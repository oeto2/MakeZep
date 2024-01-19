using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    public InputField NickNameInput;
    public GameObject ConnectPanel; //접속 창
    public GameObject UIPanel; //UI 창
    public InputField ReNameInPut;
    public Text RoomNameText;
    public Text JoinPlayerNumText;
    public Text[] JoinPlayerNameTexts;
    public GameObject Player;

    private void Awake()
    {
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }

    //포톤 온라인 서버 접속
    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 6 }, null); //총 인원수 6명
    }


    //방 접속시
    public override void OnJoinedRoom()
    {
        UIPanel.SetActive(true);
        ConnectPanel.SetActive(false); //접속창 비활성화
        Spawn();
        Player = GameObject.FindWithTag("Player").gameObject;
    }

    //Esc키 입력시 접속 끊기
    private void Update() { if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected) PhotonNetwork.Disconnect(); }

    //플레이어 생성
    public void Spawn()
    {
        switch (InfoManager.instance.selectPlayer)
        {
            //펭귄
            case PlayerType.penguin:
                PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
                break;

            //전사
            case PlayerType.warrior:
                PhotonNetwork.Instantiate("Player2", Vector3.zero, Quaternion.identity);
                break;
        }
    }

    //접속이 끊길 경우
    public override void OnDisconnected(DisconnectCause cause)
    {
        ConnectPanel.SetActive(true); //접속창 활성화
        UIPanel.SetActive(false); //UI창 비활성화
    }

    //플레이어 이름 재설정
    public void ReNamePlayer()
    {
        //GameObject player = GameObject.FindWithTag("Player").gameObject;
        Player.GetComponent<PlayerCtrl>().PV.RPC("ReName", RpcTarget.AllBuffered, ReNameInPut.text);
    }

    //방 정보 보여주기
    public void ShowRoomInfo()
    {
        ResetRoomInfo();

        RoomNameText.text += PhotonNetwork.CurrentRoom.Name;
        JoinPlayerNumText.text += $"{PhotonNetwork.CurrentRoom.PlayerCount}명";

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            JoinPlayerNameTexts[i].text = PhotonNetwork.PlayerList[i].NickName;
        }
    }

    //방 정보 리셋
    public void ResetRoomInfo()
    {
        RoomNameText.text = "방 이름: ";
        JoinPlayerNumText.text = "접속중: ";

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            JoinPlayerNameTexts[i].text = "";
        }
    }

    //플레이어 캐릭터 변경
    public void ChangePlayer(int _selectNum)
    {
        //GameObject player = GameObject.FindWithTag("Player").gameObject;
        Player.GetComponent<PlayerCtrl>().PV.RPC("ChangeCharacterAnimation", RpcTarget.AllBuffered, _selectNum);
    }
}
