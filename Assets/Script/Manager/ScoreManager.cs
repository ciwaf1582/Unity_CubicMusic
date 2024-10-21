using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    ComboManager comboManager;

    public Text txtScore;
    public int increaseScore = 10;
    int currentScore = 0;
    

    public float[] weight;
    public int comboBonusScore = 10;

    Animator anim;
    string animScoreUp = "ScoreUp";

    private void Awake()
    {
        comboManager = FindObjectOfType<ComboManager>();
        anim = GetComponent<Animator>();
        currentScore = 0;
        txtScore.text = "0";
    }
    public void Initialized() // 콤보 초기화
    {
        currentScore = 0;
        txtScore.text = "0";
    }
    public void IncreaseScore(int JudementState)
    {
        // 콤보 증가
        comboManager.IncreaseCombo();

        // 콤보 보너스 점수 계산
        int currentCombo = comboManager.GetCurrentCombo();
        int bounsComboScore = (currentCombo / 10) * comboBonusScore;

        // 판정 가중치 계산
        int t_increaseScore = increaseScore + bounsComboScore;
        t_increaseScore = (int)(t_increaseScore * weight[JudementState]);

        // 점수 반영
        currentScore += t_increaseScore;
        txtScore.text = string.Format("{0:#,##0}", currentScore);

        anim.SetTrigger(animScoreUp);
    }
    public int GetCurrentScore()
    {
        return currentScore;
    }
}
