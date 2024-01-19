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

    //��ȭ ����
    public void StartTalkNPC()
    {
        Debug.Log("StartTalk");
        NPCTalkPanel.SetActive(true); // ��ȭâ ���̱�
    }

    //��ȭ ����
    public void EndTalkNPC()
    {
        NPCTalkPanel.SetActive(false); // ��ȭâ ����
    }
}
