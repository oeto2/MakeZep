using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    public GameObject ReNamePanel;
    public GameObject SelectChPanel;

    public void ShowReNamePanel() { if (!SelectChPanel.activeSelf) ReNamePanel.SetActive(true); }
    public void ShowSelectChPanel() { if (!ReNamePanel.activeSelf) SelectChPanel.SetActive(true); }

}
