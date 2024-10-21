using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public GameObject UI;

    [Header("# �ؽ�Ʈ UI #")]
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
        // Update���� Find�Լ��� ���°� �����ϰ� �ɸ��� �� ����
        FindObjectOfType<CenterFlame>().ResetMusic(); 
        AudioManager.instance.StopBGM();

        UI.SetActive(true);
        
        // �ʱ�ȭ
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
