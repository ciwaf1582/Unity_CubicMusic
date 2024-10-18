using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    Animator anim;
    string animComboUp = "ComboUp";

    public GameObject imgCombo;
    public Text txtCombo;

    int currentCombo = 0;


    private void Start()
    {
        anim = GetComponent<Animator>();
        txtCombo.gameObject.SetActive(false);
        imgCombo.SetActive(false);
    }
    public void IncreaseCombo(int num = 1)
    {
        currentCombo += num;
        txtCombo.text = string.Format("{0:#,##0}", currentCombo);

        if (currentCombo > 2)
        {
            txtCombo.gameObject.SetActive(true);
            imgCombo.SetActive(true);

            anim.SetTrigger(animComboUp);
        }
    }
    public void ResetCombo()
    {
        currentCombo = 0;
        txtCombo.text = "0";
        txtCombo.gameObject.SetActive(false);
        imgCombo.SetActive(false);
    }
    public int GetCurrentCombo()
    {
        return currentCombo;
    }
}
