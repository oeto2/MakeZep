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
    public Text[] ChatText;

    public void InputChattingRPC() => ChattingPhotonView.RPC("InputEnter", RpcTarget.AllBuffered);

    [PunRPC]
    public void InputEnter()
    {
        bool isFull = true;

        for (int i = 0; i < ChatText.Length; i++)
        {
            if (ChatText[i].text == "")
            {
                isFull = false;
                ChatText[i].text = ChattingInputField.text;
                break;
            }
        }

        if (isFull)
        {
            for (int i = 1; i < ChatText.Length; i++)
            {
                ChatText[i - 1].text = ChatText[i].text;

                if (i == ChatText.Length - 1)
                {
                    ChatText[i].text = ChattingInputField.text;
                }
            }
        }

        ChattingInputField.text = "";
    }
}
