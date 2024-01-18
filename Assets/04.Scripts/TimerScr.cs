using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerScr : MonoBehaviour
{
    private Text timeText;

    private void Awake()
    {
        timeText = this.transform.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = DateTime.Now.ToString(("HH:MM"));
    }
}
