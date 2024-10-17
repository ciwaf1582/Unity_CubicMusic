using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text txtScore;
    public int increaseScore = 10;
    int currentScore = 0;

    public float[] weight;

    Animator anim;
    string animScoreUp = "ScoreUp";

    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentScore = 0;
        txtScore.text = "0";
    }
    public void IncreaseScore(int JudementState)
    {
        int t_increaseScore = increaseScore;

        t_increaseScore = (int)(t_increaseScore * weight[JudementState]);
        currentScore += t_increaseScore;
        txtScore.text = string.Format("{0:#,##0}", currentScore);

        anim.SetTrigger(animScoreUp);
    }
}
