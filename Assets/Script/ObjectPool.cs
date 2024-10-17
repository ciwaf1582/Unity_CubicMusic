using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class ObjectInfo // 프리팹 Note의 정보
{
    public GameObject prefab;
    public int count;
    public Transform poolParentPos;
}

public class ObjectPool : MonoBehaviour
{
    public ObjectInfo[] objectinfo;

    // Queue : 선입선출 자료형 (가장 먼저 들어간 데이터가 가장 먼저 빠져나옴)
    public Queue<GameObject> noteQueue = new Queue<GameObject>();

    public static ObjectPool instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        noteQueue = InsertQueue(objectinfo[0]);
    }
    Queue<GameObject> InsertQueue(ObjectInfo objectInfo)
    {
        // 새로운 큐를 생성하고 오브젝트를 저장한 후 끝나면 반환
        Queue<GameObject> queue = new Queue<GameObject>();

        for (int i = 0; i < objectInfo.count; i++)
        {
            // objectInfo.prefab를 기준으로 복제
            GameObject clone = Instantiate(objectInfo.prefab, transform.position, Quaternion.identity);
            // 초기 값
            clone.SetActive(false);
            // 부모 객체가 있다면 하위에 생성
            if (objectInfo.poolParentPos != null)
                clone.transform.SetParent(objectInfo.poolParentPos);
            // 없다면 이 스크립트가 붙어있는 객체가 부모로 설정
            else clone.transform.SetParent(this.transform);

            queue.Enqueue(clone);
        }
        return queue;
    }
}
