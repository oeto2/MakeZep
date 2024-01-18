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

    bool isWall; //�� ����
    Vector3 curpos; //���� ��ġ
   
    private void Awake()
    {
        //�г��� �ؽ�Ʈ
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName; 
        NickNameText.color = PV.IsMine ? Color.green : Color.red; //�÷��̾� : �ʷϻ�, Ÿ�� : ������
    }

    private void Update()
    {
        if(PV.IsMine)
        {
            //�÷��̾� �̵�
            float hAxis = Input.GetAxisRaw("Horizontal"); //�¿� �̵�
            float vAxis = Input.GetAxisRaw("Vertical"); //���� �̵�
            RB.velocity = new Vector2(4 * hAxis, 4 * vAxis); //�÷��̾� �ӵ�

            //�÷��̾� �̵���
            if (hAxis != 0 || vAxis != 0)
            {
                AN.SetBool("isWalk", true);
                PV.RPC("FlipXRPC", RpcTarget.AllBuffered, hAxis);
            }
            else AN.SetBool("isWalk", false);

            //�÷��̾� ������
            if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(PlayerJump());
        }
    }

    [PunRPC]
    void FlipXRPC(float hAxis) => SR.flipX = hAxis == -1; //Ű�Է°��� ���� ���� ��ȯ

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
