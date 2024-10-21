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
        // Update문에 Find함수를 쓰는건 과부하가 걸리는 것 생각
        FindObjectOfType<CenterFlame>().ResetMusic(); 
        AudioManager.instance.StopBGM();

        UI.SetActive(true);
        
        // 초기화
        for (int i = 0; i < txtCount.Length; i++)
        {
            txtCount[i].text = "0";
        }
        txtScore.text = "0";
        txtMaxCombo.text = "0";
        txtCoin.text = "0";

        int[] jedement = timeManager.GetJudementRecord();
        int currentScore = scoreManager.GetCurrentScore();
        int maxCombo = comboManager.GetMaxCombo();
        int coin = (currentScore / 50);

        for (int i = 0;i < txtCount.Length; i++)
        {
            txtCount[i].text = string.Format("{0:#,##0}", jedement[i]);
        }
        txtScore.text = string.Format("{0:#,##0}", currentScore);
        txtMaxCombo.text = string.Format("{0:#,##0}", maxCombo);
        txtCoin.text = string.Format("{0:#,##0}", coin);
    }
    public void BtnMainMenu()
    {
        UI.SetActive(false);
        GameManager.instance.MainMenu();
        comboManager.ResetCombo();
    }
}
