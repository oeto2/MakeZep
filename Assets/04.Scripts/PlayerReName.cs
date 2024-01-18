using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerReName : MonoBehaviour
{
    public InputField ReNameInPut;


    public void ReNamePlayer()
    {
        Text playerName = GameObject.FindWithTag("Player").transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();

        Debug.Log(playerName);

        playerName.text = ReNameInPut.text;
    }
}
