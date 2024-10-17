using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public float noteSpeed = 400;

    Image noteImg;

    private void OnEnable()
    {
        if (noteImg == null) noteImg = GetComponent<Image>();

        noteImg.enabled = true;
    }
    void Update()
    {
        transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime;
    }
    public void HideNote()
    {
        noteImg.enabled = false;
    }
    public bool GetNoteFlag()
    {
        return noteImg.enabled;
    }
}
