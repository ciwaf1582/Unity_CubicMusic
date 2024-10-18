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
        // ���� ����
        timingBoxs = new Vector2[timingRect.Length];

        for (int i = 0; i < timingRect.Length; i++)
        {
            // �ּڰ� = �߽� - (�̹����� �ʺ� / 2)
            // �ִ� = �߽� + (�̹����� �ʺ� / 2)
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
                // ���� �ּ� �� <= ��Ʈ�� x�� <= ���� �ִ� ��
                if (timingBoxs[j].x <= notePosX && notePosX <= timingBoxs[j].y)
                {
                    // ��Ʈ ����
                    noteList[i].GetComponent<Note>().HideNote();
                    noteList.RemoveAt(i);

                    // �ִϸ��̼� ����(0 ~ 3����)
                    if (j < timingBoxs.Length - 1)
                    {
                        effectManager.AnimNoteHit();

                        // ���� ����(Miss����)
                        scoreManager.IncreaseScore(j);
                    }
                    effectManager.AnimJudgementHit(j);
                    return true;
                }
            }
        }
        comboManager.ResetCombo(); // �޺� �ʱ�ȭ
        effectManager.AnimJudgementHit(timingBoxs.Length); // Miss �ִϸ��̼�
        return false;
    }
}
