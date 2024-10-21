using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMenu : MonoBehaviour
{
    public GameObject titleMenu;

    public void BtnBack()
    {
        titleMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void BtnPlay()
    {
        GameManager.instance.GameStart();
        this.gameObject.SetActive(false);
    }
}
