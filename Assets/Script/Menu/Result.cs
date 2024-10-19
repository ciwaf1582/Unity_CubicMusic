using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public GameObject UI;

    [Header("# 텍스트 UI #")]
    public Text[] txtCount;
    public Text txtScore;
    public Text txtMaxCombo;
    public Text txtCoin;

    ScoreManager scoreManager;
    ComboManager comboManager;
    TimeManager timeManager;

    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        comboManager = FindObjectOfType<ComboManager>();
        timeManager = FindObjectOfType<TimeManager>();
    }
    public void ShowResult()
    {
        UI.SetActive(true);

        // 초기화
        for (int i = 0; i < txtCount.Length; i++)
        {
            txtCount[i].text = "0";
        }
        txtScore.text = "0";
        txtMaxCombo.text = "0";
        txtCoin.text = "0";
    }
}
