using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkAreaScr : MonoBehaviour
{
    public GameObject NPCTalkPanel;
    public Text NPCTalkPanelNameText;
    public Text NPCNameText;

    public string talkSentenceOfNPC;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            NPCTalkPanelNameText.text = NPCNameText.text;
            StartTalkNPC();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EndTalkNPC();
        }
    }

    //대화 시작
    public void StartTalkNPC()
    {
        Debug.Log("StartTalk");
        NPCTalkPanel.SetActive(true); // 대화창 보이기
    }

    //대화 종료
    public void EndTalkNPC()
    {
        NPCTalkPanel.SetActive(false); // 대화창 종료
    }
}
