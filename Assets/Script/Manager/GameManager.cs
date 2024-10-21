using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void Awake()
    {
        instance = this;

        comboManager = FindObjectOfType<ComboManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        timeManager = FindObjectOfType<TimeManager>();
        statusManager = FindObjectOfType<StatusManager>();
        playerController = FindObjectOfType<PlayerController>();
        stageManager = FindObjectOfType<StageManager>();
    }
    public void GameStart()
    {
        for (int i = 0; i < gameUI.Length; i++)
        {
            gameUI[i].SetActive(true);
        }
        stageManager.RemoveStage();
        stageManager.SettingStage();

        comboManager.ResetCombo();
        scoreManager.Initialized();
        timeManager.Initialized();
        statusManager.Initialized();
        playerController.Initialized();

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
