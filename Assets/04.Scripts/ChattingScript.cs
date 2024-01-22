using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ChattingScript : MonoBehaviourPunCallbacks
{
    public PhotonView ChattingPhotonView;

    public InputField ChattingInputField;
    //public Text[] ChatText;
    public List<Text> ChatText;

    public GameObject TextPrefab;

    //public void InputChattingRPC() => ChattingPhotonView.RPC("SendButtonClicked", RpcTarget.All);

    //ä�� �Է°� �Ѱ��ֱ�
    public void SendButtonClicked()
    {
        //�ؽ�Ʈ �Է°��� �÷��̾� �̸� �Ѱ��ְ� RPC
        ChattingPhotonView.RPC("PlayerChatting", RpcTarget.All, "<color=#00FF18>" + PhotonNetwork.LocalPlayer.NickName + ": "
            + "</color>" + ChattingInputField.text);
    }

    [PunRPC]
    //�Ѱ� ���� �ؽ�Ʈ ������ UI Text�� ����
    public void PlayerChatting(string _inputText)
    {
        GameObject Content = this.transform.Find("Scroll View/Viewport/Content").gameObject;

        //�ؽ�Ʈ ������Ʈ Prefab ���� �� Content �ڽ����� �Ҵ�
        GameObject TextObject = Instantiate(TextPrefab, Content.transform.position, Quaternion.identity);
        TextObject.transform.SetParent(Content.transform);
        TextObject.transform.localScale = new Vector3(1, 1, 1);

        //ChatText List�� TextObject �߰�
        ChatText.Add(TextObject.GetComponent<Text>());

        for (int i = 0; i < ChatText.Count; i++)
        {
            if (ChatText[i].text == "")
            {
                ChatText[i].text = _inputText;
                break;
            }
        }
        ChattingInputField.text = "";
    }
}
