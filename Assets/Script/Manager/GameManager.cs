using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] gameUI;
    public GameObject titleUI;

    public static GameManager instance;

    public bool isStartGame = false;

    ComboManager comboManager;
    ScoreManager scoreManager;
    TimeManager timeManager;
    StatusManager statusManager;
    PlayerController playerController;
    StageManager stageManager;
    NoteManager noteManager;
    Result result;

    public CenterFlame centerFlame;
    private void Awake()
    {
        instance = this;

        comboManager = FindObjectOfType<ComboManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        timeManager = FindObjectOfType<TimeManager>();
        statusManager = FindObjectOfType<StatusManager>();
        playerController = FindObjectOfType<PlayerController>();
        stageManager = FindObjectOfType<StageManager>();
        noteManager = FindObjectOfType<NoteManager>();
        result = FindObjectOfType<Result>();
    }
    public void GameStart(int songNum, int bpm)
    {
        for (int i = 0; i < gameUI.Length; i++)
        {
            gameUI[i].SetActive(true);
        }
        centerFlame.bgmName = "BGM_" + (songNum + 1);
        noteManager.bpm = bpm;
        stageManager.RemoveStage();
        stageManager.SettingStage(songNum);

        comboManager.ResetCombo();
        scoreManager.Initialized();
        timeManager.Initialized();
        statusManager.Initialized();
        playerController.Initialized();
        result.SetCurrentSong(songNum);

        AudioManager.instance.StopBGM();

        isStartGame = true;
    }
    public void MainMenu()
    {
        for (int i = 0; i < gameUI.Length; i++)
        {
            gameUI[i].SetActive(false);
        }
        titleUI.SetActive(true);
    }
}
