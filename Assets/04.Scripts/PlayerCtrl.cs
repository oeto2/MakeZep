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

    //ĳ���� �������� ��
    public Sprite[] PlayerSprite;
    public RuntimeAnimatorController[] PlayerAnimator;

    bool isWall; //�� ����
    Vector3 curpos; //���� ��ġ

    private void Awake()
    {
        //�г��� �ؽ�Ʈ
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        NickNameText.color = PV.IsMine ? Color.green : Color.red; //�÷��̾� : �ʷϻ�, Ÿ�� : ������

        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (PV.IsMine)
        {
            MainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, MainCamera.transform.position.z);

            if (Input.mousePosition.x >= 950) PV.RPC("FlipXRPC", RpcTarget.AllBuffered, false);
            else PV.RPC("FlipXRPC", RpcTarget.AllBuffered, true);

            //�÷��̾� �̵�
            float hAxis = Input.GetAxisRaw("Horizontal"); //�¿� �̵�
            float vAxis = Input.GetAxisRaw("Vertical"); //���� �̵�
            RB.velocity = new Vector2(4 * hAxis, 4 * vAxis); //�÷��̾� �ӵ�

            //�÷��̾� �̵���
            if (hAxis != 0 || vAxis != 0) AN.SetBool("isWalk", true);
            else AN.SetBool("isWalk", false);

            //�÷��̾� ����
            if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(PlayerJump());

            //�÷��̾� ����
            if (Input.GetMouseButton(1)) PV.RPC("PlayerAttack", RpcTarget.AllBuffered);

        }
    }

    [PunRPC]
    void FlipXRPC(bool isFlip) => SR.flipX = isFlip; //���콺 ��ġ ���� ���� ��ȯ
    [PunRPC]
    void UpdateName() => NickNameText.text = PhotonNetwork.NickName; //�÷��̾� �̸� ���ΰ�ħ
    [PunRPC]
    void ReName(string _name) //�÷��̾� �̸� ����
    {
        PhotonNetwork.LocalPlayer.NickName = _name;  
        PV.RPC("UpdateName", RpcTarget.AllBuffered);
    }
    [PunRPC]
    void PlayerAttack() => AN.SetBool("isAttack", true);

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

    //�÷��̾� ����
    public IEnumerator PlayerJump()
    {
        AN.SetBool("isJump", true);
        yield return new WaitForSeconds(0.1f);
        AN.SetBool("isJump", false);
        yield return null;
    }

    
    //ĳ���� �ٲٱ�
    [PunRPC]
    public void ChangeCharacterAnimation(int _selctNum)
    {
        switch((PlayerType)_selctNum)
        {
            //��� ����
            case PlayerType.penguin:

                //�̸� ��ġ ����
                NickNameText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0.179f, -0.591f, 0);
                //NickNameText.transform.position = new Vector3(0.179f, -0.591f, 0);

                //�ִϸ��̼� ����
                SR.sprite = PlayerSprite[0];
                AN.runtimeAnimatorController = PlayerAnimator[0];
                break;

            //���� ����
            case PlayerType.warrior:

                //�̸� ��ġ ����
                NickNameText.GetComponent<RectTransform>().anchoredPosition = new Vector3(-0.07f, -0.24f, 0);
                //NickNameText.transform.position = new Vector3(-0.07f, -0.24f, 0);

                //�ִϸ��̼� ����
                SR.sprite = PlayerSprite[1];
                AN.runtimeAnimatorController = PlayerAnimator[1];
                break;
        }
    }
}
