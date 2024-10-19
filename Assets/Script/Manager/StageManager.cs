using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject stage;
    Transform[] stagePlates;

    public float offsetY;
    public float plateSpeed;

    int stepCount = 0;
    int totalPlateCount;
    private void Awake()
    {
        stagePlates = stage.GetComponent<Stage>().plates;
        Debug.Log(stagePlates.Length);
        totalPlateCount = stagePlates.Length;

        for (int i = 0; i < totalPlateCount; i++)
        {
            stagePlates[i].position = new Vector3(stagePlates[i].position.x,
                                                  stagePlates[i].position.y - offsetY,
                                                  stagePlates[i].position.z);
        }
    }
    public void ShowNextPlate()
    {
        if (stepCount < totalPlateCount)
        {
            StartCoroutine(MovePlateCo(stepCount++));
        }
    }
    IEnumerator MovePlateCo(int p_num)
    {
        stagePlates[p_num].gameObject.SetActive(true); // 활성화
        // 목적 위치
        Vector3 destPos = new Vector3(stagePlates[p_num].position.x,
                                      stagePlates[p_num].position.y + offsetY,
                                      stagePlates[p_num].position.z);

        while (Vector3.SqrMagnitude(stagePlates[p_num].position - destPos) >= 0.001f)
        {
            // ※ Lerp : 위치 값을 이용하여 부드럽게 위치
            // ※ MovePosition : rigid를 이용하여 충돌을 감지하며 위치

            // A(y - offset)와 B(목적 위치)의 C(plateSpeed의 비율)
            stagePlates[p_num].position = Vector3.Lerp(stagePlates[p_num].position,
                                                       destPos, plateSpeed * Time.deltaTime);
            yield return null;
        }
        stagePlates[p_num].position = destPos;
        Debug.Log(stagePlates[p_num].position);

    }
}
