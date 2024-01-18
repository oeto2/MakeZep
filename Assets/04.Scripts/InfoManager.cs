using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    penguin,
    warrior
}

public class InfoManager : MonoBehaviour
{
    public static InfoManager instance = null;

    //������ ĳ����
    public PlayerType selectPlayer = PlayerType.penguin;

    public GameObject Player1_SelectText;
    public GameObject Player2_SelectText;

    private void Awake()
    {
        //�̱���
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else { if (instance != this) Destroy(this.gameObject); }
    }

    private void Start()
    {
        //�÷��̾� ������ �ؽ�Ʈ ����
        ShowSelectPlayerText((int)selectPlayer);
    }


    //������ �÷��̾� �ؽ�Ʈ ����
    public void ShowSelectPlayerText(int _typeNum)
    {
        switch ((PlayerType)_typeNum)
        {
            //���
            case PlayerType.penguin:
                selectPlayer = PlayerType.penguin;
                Player1_SelectText.SetActive(true);
                Player2_SelectText.SetActive(false);
                break;

            //������
            case PlayerType.warrior:
                selectPlayer = PlayerType.warrior;
                Player2_SelectText.SetActive(true);
                Player1_SelectText.SetActive(false);
                break;
        }
    }
}


