using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    public Animator animNoteHit;
    string hit = "Hit";

    public Animator animJudgementHit;

    public Sprite[] judgementSprite;
    public Image judementImg;
    public void AnimJudgementHit(int p_num)
    {
        judementImg.sprite = judgementSprite[p_num];
        animJudgementHit.SetTrigger(hit);
    }
    public void AnimNoteHit()
    {
        animNoteHit.SetTrigger(hit);
    }
}
