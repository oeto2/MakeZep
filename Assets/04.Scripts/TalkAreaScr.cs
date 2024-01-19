using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkAreaScr : MonoBehaviour
{
    public GameObject StartTalkNPCPanel; //NPC ��ȭ ���� â
    public Text NPCTalkPanelNameText;
    public Text NPCNameText;

    public GameObject TalkNPCPanel; //NPC ��ȭ â
    public Text TalkSentence; //��ȭ ����

    public string talkSentenceOfNPC;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            NPCTalkPanelNameText.text = NPCNameText.text;
            ShowStartTalkNPCPanel();
            TalkSentence.text = talkSentenceOfNPC;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EndTalkNPC();
        }
    }

    //��ȭ ����â �����ֱ�
    public void ShowStartTalkNPCPanel() => StartTalkNPCPanel.SetActive(true); // ��ȭ ���� â ���̱�

    ////NPC ��ȭ ����
    //public void StartTalkToNPC()
    //{
    //    TalkNPCPanel.SetActive(true);
    //    TalkSentence.text = talkSentenceOfNPC;
    //}

    //��ȭ ����
    public void EndTalkNPC() => StartTalkNPCPanel.SetActive(false); // ��ȭ ���� â ����
}
