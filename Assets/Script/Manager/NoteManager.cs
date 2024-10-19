using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    double currentTime = 0d;

    bool noteActive = true;

    public Transform noteSpawn;
    //public GameObject prefab;

    TimeManager timeManager;
    EffectManager effectManager;
    ComboManager comboManager;
    private void Start()
    {
        timeManager = GetComponent<TimeManager>();
        effectManager = FindObjectOfType<EffectManager>();
        comboManager = FindObjectOfType<ComboManager>();
    }
    private void Update()
    {
        if (noteActive)
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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            if (collision.GetComponent<Note>().GetNoteFlag()) // 이미지가 활성화 된 노트라면
            {
                comboManager.ResetCombo(); // 콤보 초기화
                effectManager.AnimJudgementHit(4); // 미스
            }
            timeManager.noteList.Remove(collision.gameObject);

            ObjectPool.instance.noteQueue.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false);

            //Destroy(collision.gameObject);
        }
    }
    public void RemoveNote()
    {
        noteActive = false; 
        for (int i = 0; i < timeManager.noteList.Count; i++)
        {
            timeManager.noteList[i].gameObject.SetActive(false);
            ObjectPool.instance.noteQueue.Enqueue(timeManager.noteList[i]);
        }
    }
}
