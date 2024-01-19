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

    //캐릭터 선택전용 값
    public Sprite[] PlayerSprite;
    public RuntimeAnimatorController[] PlayerAnimator;

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

            //플레이어 점프
            if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(PlayerJump());

            //플레이어 공격
            if (Input.GetMouseButton(1)) PV.RPC("PlayerAttack", RpcTarget.AllBuffered);

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
    [PunRPC]
    void PlayerAttack() => AN.SetBool("isAttack", true);

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

    
    //캐릭터 바꾸기
    [PunRPC]
    public void ChangeCharacterAnimation(int _selctNum)
    {
        switch((PlayerType)_selctNum)
        {
            //펭귄 선택
            case PlayerType.penguin:

                //이름 위치 변경
                NickNameText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0.179f, -0.591f, 0);
                //NickNameText.transform.position = new Vector3(0.179f, -0.591f, 0);

                //애니메이션 변경
                SR.sprite = PlayerSprite[0];
                AN.runtimeAnimatorController = PlayerAnimator[0];
                break;

            //전사 선택
            case PlayerType.warrior:

                //이름 위치 변경
                NickNameText.GetComponent<RectTransform>().anchoredPosition = new Vector3(-0.07f, -0.24f, 0);
                //NickNameText.transform.position = new Vector3(-0.07f, -0.24f, 0);

                //애니메이션 변경
                SR.sprite = PlayerSprite[1];
                AN.runtimeAnimatorController = PlayerAnimator[1];
                break;
        }
    }
}
