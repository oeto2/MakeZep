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

            //�÷��̾� ������
            if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(PlayerJump());
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
}
