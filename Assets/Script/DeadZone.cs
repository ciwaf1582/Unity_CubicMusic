using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // GetComponentInParent : �θ� ��ü�� Ư�� ������Ʈ�� ����
            other.GetComponentInParent<PlayerController>().ResetFalling();
        }
    }
}
