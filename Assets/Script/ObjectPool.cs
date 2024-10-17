using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class ObjectInfo // ������ Note�� ����
{
    public GameObject prefab;
    public int count;
    public Transform poolParentPos;
}

public class ObjectPool : MonoBehaviour
{
    public ObjectInfo[] objectinfo;

    // Queue : ���Լ��� �ڷ��� (���� ���� �� �����Ͱ� ���� ���� ��������)
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
        // ���ο� ť�� �����ϰ� ������Ʈ�� ������ �� ������ ��ȯ
        Queue<GameObject> queue = new Queue<GameObject>();

        for (int i = 0; i < objectInfo.count; i++)
        {
            // objectInfo.prefab�� �������� ����
            GameObject clone = Instantiate(objectInfo.prefab, transform.position, Quaternion.identity);
            // �ʱ� ��
            clone.SetActive(false);
            // �θ� ��ü�� �ִٸ� ������ ����
            if (objectInfo.poolParentPos != null)
                clone.transform.SetParent(objectInfo.poolParentPos);
            // ���ٸ� �� ��ũ��Ʈ�� �پ��ִ� ��ü�� �θ�� ����
            else clone.transform.SetParent(this.transform);

            queue.Enqueue(clone);
        }
        return queue;
    }
}
