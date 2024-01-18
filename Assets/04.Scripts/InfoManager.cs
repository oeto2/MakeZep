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

    //선택한 캐릭터
    public PlayerType selectPlayer = PlayerType.penguin;

    public GameObject Player1_SelectText;
    public GameObject Player2_SelectText;

    private void Awake()
    {
        //싱글톤
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else { if (instance != this) Destroy(this.gameObject); }
    }

    private void Start()
    {
        //플레이어 선택중 텍스트 띄우기
        ShowSelectPlayerText((int)selectPlayer);
    }


    //선택한 플레이어 텍스트 띄우기
    public void ShowSelectPlayerText(int _typeNum)
    {
        switch ((PlayerType)_typeNum)
        {
            //펭귄
            case PlayerType.penguin:
                selectPlayer = PlayerType.penguin;
                Player1_SelectText.SetActive(true);
                Player2_SelectText.SetActive(false);
                break;

            //여전사
            case PlayerType.warrior:
                selectPlayer = PlayerType.warrior;
                Player2_SelectText.SetActive(true);
                Player1_SelectText.SetActive(false);
                break;
        }
    }
}


