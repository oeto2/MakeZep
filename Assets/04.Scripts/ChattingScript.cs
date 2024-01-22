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

    //채팅 입력값 넘겨주기
    public void SendButtonClicked()
    {
        //텍스트 입력값과 플레이어 이름 넘겨주고 RPC
        ChattingPhotonView.RPC("PlayerChatting", RpcTarget.All, "<color=#00FF18>" + PhotonNetwork.LocalPlayer.NickName + ": "
            + "</color>" + ChattingInputField.text);
    }

    [PunRPC]
    //넘겨 받은 텍스트 값으로 UI Text에 적용
    public void PlayerChatting(string _inputText)
    {
        GameObject Content = this.transform.Find("Scroll View/Viewport/Content").gameObject;

        //텍스트 오브젝트 Prefab 생성 후 Content 자식으로 할당
        GameObject TextObject = Instantiate(TextPrefab, Content.transform.position, Quaternion.identity);
        TextObject.transform.SetParent(Content.transform);
        TextObject.transform.localScale = new Vector3(1, 1, 1);

        //ChatText List에 TextObject 추가
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
