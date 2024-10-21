using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    public GameObject stageUI;

    public void BtnPlay()
    {
        stageUI.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
