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
    public void Initialized() // �޺� �ʱ�ȭ
    {
        currentScore = 0;
        txtScore.text = "0";
    }
    public void IncreaseScore(int JudementState)
    {
        // �޺� ����
        comboManager.IncreaseCombo();

        // �޺� ���ʽ� ���� ���
        int currentCombo = comboManager.GetCurrentCombo();
        int bounsComboScore = (currentCombo / 10) * comboBonusScore;

        // ���� ����ġ ���
        int t_increaseScore = increaseScore + bounsComboScore;
        t_increaseScore = (int)(t_increaseScore * weight[JudementState]);

        // ���� �ݿ�
        currentScore += t_increaseScore;
        txtScore.text = string.Format("{0:#,##0}", currentScore);

        anim.SetTrigger(animScoreUp);
    }
    public int GetCurrentScore()
    {
        return currentScore;
    }
}
