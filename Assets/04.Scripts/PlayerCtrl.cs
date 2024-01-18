using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerCtrl : MonoBehaviourPunCallbacks,IPunObservable
{
    public Rigidbody2D RB;
    public Animator AN;
    public SpriteRenderer SR;
    public PhotonView PV;
    public Text NickNameText;

    bool isWall; //벽 감지
    Vector3 curpos; //현재 위치
   
    private void Awake()
    {
        //닉네임 텍스트
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName; 
        NickNameText.color = PV.IsMine ? Color.green : Color.red; //플레이어 : 초록색, 타인 : 빨간색
    }

    private void Update()
    {
        if(PV.IsMine)
        {
            //플레이어 이동
            float hAxis = Input.GetAxisRaw("Horizontal"); //좌우 이동
            float vAxis = Input.GetAxisRaw("Vertical"); //상하 이동
            RB.velocity = new Vector2(4 * hAxis, 4 * vAxis); //플레이어 속도

            //플레이어 이동시
            if (hAxis != 0 || vAxis != 0)
            {
                AN.SetBool("isWalk", true);
                PV.RPC("FlipXRPC", RpcTarget.AllBuffered, hAxis);
            }
            else AN.SetBool("isWalk", false);

            //플레이어 점프시
            if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(PlayerJump());
        }
    }

    [PunRPC]
    void FlipXRPC(float hAxis) => SR.flipX = hAxis == -1; //키입력값에 따라 방향 전환

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
