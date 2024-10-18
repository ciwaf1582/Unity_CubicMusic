using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float foolwSpeed = 15;

    Vector3 playerDistance = new Vector3();
    private void Awake()
    {
        playerDistance = transform.position - target.position;
    }
    private void Update()
    {
        
    }
}
