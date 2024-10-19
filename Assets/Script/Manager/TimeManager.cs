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
    StageManager stageManager;
    PlayerController playerController;
    private void Start()
    {
        comboManager = FindObjectOfType<ComboManager>();
        effectManager = FindObjectOfType<EffectManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        stageManager = FindObjectOfType<StageManager>();
        playerController = FindObjectOfType<PlayerController>();
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
                    }
                    if (CheckCanNextPlate()) // true
                    {
                        scoreManager.IncreaseScore(j); // ���� ����
                        stageManager.ShowNextPlate(); // ���� �� ���� ���� Ȱ��ȭ
                        effectManager.AnimJudgementHit(j);
                    }
                    else
                    {
                        effectManager.AnimJudgementHit(5);
                    }
                    return true;
                }
            }
        }
        comboManager.ResetCombo(); // �޺� �ʱ�ȭ
        effectManager.AnimJudgementHit(timingBoxs.Length); // Miss �ִϸ��̼�
        return false;
    }
    bool CheckCanNextPlate()
    {
        if (Physics.Raycast(playerController.destPos, Vector3.down, out RaycastHit hit, 1.1f))
        {
            // ����ģ ��ġ�� �±� �ĺ�
            if (hit.transform.CompareTag("BasicPlate"))
            {
                // �浹�� ������Ʈ�� ��ġ ��ȯ
                BasicPlate plate = hit.transform.GetComponent<BasicPlate>();

                if (plate.flag)
                {
                    plate.flag = false; // ���� ���� false
                    return true;
                }
            }
        }
        return false;
    }
}
