using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerCtrl : MonoBehaviourPunCallbacks, IPunObservable
{
    public Rigidbody2D RB;
    public Animator AN;
    public SpriteRenderer SR;
    public PhotonView PV;
    public Text NickNameText;
    public Camera MainCamera;

    bool isWall; //벽 감지
    Vector3 curpos; //현재 위치

    private void Awake()
    {
        //닉네임 텍스트
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        NickNameText.color = PV.IsMine ? Color.green : Color.red; //플레이어 : 초록색, 타인 : 빨간색

        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (PV.IsMine)
        {
            MainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, MainCamera.transform.position.z);

            if (Input.mousePosition.x >= 950) PV.RPC("FlipXRPC", RpcTarget.AllBuffered, false);
            else PV.RPC("FlipXRPC", RpcTarget.AllBuffered, true);

            //플레이어 이동
            float hAxis = Input.GetAxisRaw("Horizontal"); //좌우 이동
            float vAxis = Input.GetAxisRaw("Vertical"); //상하 이동
            RB.velocity = new Vector2(4 * hAxis, 4 * vAxis); //플레이어 속도

            //플레이어 이동시
            if (hAxis != 0 || vAxis != 0) AN.SetBool("isWalk", true);
            else AN.SetBool("isWalk", false);

            //플레이어 점프시
            if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(PlayerJump());
        }
    }

    [PunRPC]
    void FlipXRPC(bool isFlip) => SR.flipX = isFlip; //마우스 위치 따라 방향 전환
    [PunRPC]
    void UpdateName() => NickNameText.text = PhotonNetwork.NickName; //플레이어 이름 새로고침
    [PunRPC]
    void ReName(string _name) //플레이어 이름 변경
    {
        PhotonNetwork.LocalPlayer.NickName = _name;  
        PV.RPC("UpdateName", RpcTarget.AllBuffered);
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

    //플레이어 점프
    public IEnumerator PlayerJump()
    {
        AN.SetBool("isJump", true);
        yield return new WaitForSeconds(0.1f);
        AN.SetBool("isJump", false);
        yield return null;
    }
}
