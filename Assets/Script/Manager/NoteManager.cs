using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    double currentTime = 0d;

    public Transform noteSpawn;
    //public GameObject prefab;

    TimeManager timeManager;
    EffectManager effectManager;
    private void Start()
    {
        timeManager = GetComponent<TimeManager>();
        effectManager = FindObjectOfType<EffectManager>();
    }
    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 60d / bpm)
        {
            GameObject note = ObjectPool.instance.noteQueue.Dequeue(); // 큐 반환
            // 반환 받은 큐 초기 값
            note.transform.position = noteSpawn.position;
            note.SetActive(true);
            //GameObject note = Instantiate(prefab, noteSpawn.position, Quaternion.identity);
            //note.transform.SetParent(this.transform);
            note.transform.localScale = Vector3.one;
            timeManager.noteList.Add(note);
            currentTime -= 60d / bpm;   
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            if (collision.GetComponent<Note>().GetNoteFlag())
            {
                effectManager.AnimJudgementHit(4);
            }
            timeManager.noteList.Remove(collision.gameObject);

            ObjectPool.instance.noteQueue.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false);

            //Destroy(collision.gameObject);
        }
    }
}
