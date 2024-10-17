using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    TimeManager timeManager;
    private void Start()
    {
        timeManager = FindObjectOfType<TimeManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeManager.CheckTiming();
        }
    }
}
