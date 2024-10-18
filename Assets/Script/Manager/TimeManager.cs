using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public List<GameObject> noteList = new List<GameObject>();

    public Transform Center = null;
    public RectTransform[] timingRect = null;
    Vector2[] timingBoxs = null;

    ComboManager comboManager;
    EffectManager effectManager;
    ScoreManager scoreManager;
    private void Start()
    {
        comboManager = FindObjectOfType<ComboManager>();
        effectManager = FindObjectOfType<EffectManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        // 판정 설정
        timingBoxs = new Vector2[timingRect.Length];

        for (int i = 0; i < timingRect.Length; i++)
        {
            // 최솟값 = 중심 - (이미지의 너비 / 2)
            // 최댓값 = 중심 + (이미지의 너비 / 2)
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                              Center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }
    public bool CheckTiming()
    {
        for (int i = 0; i < noteList.Count; i++)
        {
            float notePosX = noteList[i].transform.localPosition.x;

            for (int j = 0; j < timingBoxs.Length; j++)
            {
                // 판정 최솟 값 <= 노트의 x값 <= 판정 최댓 값
                if (timingBoxs[j].x <= notePosX && notePosX <= timingBoxs[j].y)
                {
                    // 노트 제거
                    noteList[i].GetComponent<Note>().HideNote();
                    noteList.RemoveAt(i);

                    // 애니메이션 동작(0 ~ 3까지)
                    if (j < timingBoxs.Length - 1)
                    {
                        effectManager.AnimNoteHit();

                        // 점수 증가(Miss제외)
                        scoreManager.IncreaseScore(j);
                    }
                    effectManager.AnimJudgementHit(j);
                    return true;
                }
            }
        }
        comboManager.ResetCombo(); // 콤보 초기화
        effectManager.AnimJudgementHit(timingBoxs.Length); // Miss 애니메이션
        return false;
    }
}
