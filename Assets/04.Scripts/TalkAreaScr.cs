using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkAreaScr : MonoBehaviour
{
    public GameObject StartTalkNPCPanel; //NPC 대화 시작 창
    public Text NPCTalkPanelNameText;
    public Text NPCNameText;

    public GameObject TalkNPCPanel; //NPC 대화 창
    public Text TalkSentence; //대화 문장

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

    //대화 시작창 보여주기
    public void ShowStartTalkNPCPanel() => StartTalkNPCPanel.SetActive(true); // 대화 시작 창 보이기

    ////NPC 대화 시작
    //public void StartTalkToNPC()
    //{
    //    TalkNPCPanel.SetActive(true);
    //    TalkSentence.text = talkSentenceOfNPC;
    //}

    //대화 종료
    public void EndTalkNPC() => StartTalkNPCPanel.SetActive(false); // 대화 시작 창 종료
}
