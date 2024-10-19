using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalPlate : MonoBehaviour
{
    AudioSource coalAudio;
    NoteManager noteManager;
    Result result;

    private void Awake()
    {
        coalAudio = GetComponent<AudioSource>();
        noteManager = FindObjectOfType<NoteManager>();
        result = FindObjectOfType<Result>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            coalAudio.Play();
            PlayerController.canPressKey = false;
            noteManager.RemoveNote();
            result.ShowResult();
        }
    }
}
