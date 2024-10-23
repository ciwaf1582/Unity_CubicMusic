using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Song
{
    public string name;
    public string composer;
    public int bpm;
    public Sprite sprite;
}
public class StageMenu : MonoBehaviour
{
    public Song[] songList;
    public Text txtSongName;
    public Text txtSongComposer;
    public Text txtSongScore;
    public Image imgDisk;

    public GameObject titleMenu;

    DatabaseManager databaseManager;

    int currentSong;

    private void OnEnable()
    {
        if (databaseManager == null) databaseManager = FindObjectOfType<DatabaseManager>();

        SettingSong();
    }
    public void BtnNext()
    {
        AudioManager.instance.PlayerSFX("Touch");

        if (++currentSong > songList.Length - 1) 
            currentSong = 0;
        SettingSong();
    }
    public void BtnPrior()
    {
        AudioManager.instance.PlayerSFX("Touch");

        if (--currentSong < 0) 
            currentSong = songList.Length - 1;
        SettingSong();
    }
    void SettingSong()
    {
        txtSongName.text = songList[currentSong].name;
        txtSongComposer.text = songList[currentSong].composer;
        //txtSongScore.text = string.Format("{0:#,##0}", databaseManager.score[currentSong]);
        imgDisk.sprite = songList[currentSong].sprite;

        AudioManager.instance.PlayBGM("BGM_" + (currentSong + 1));
        Debug.Log("BGM_" + currentSong);
    }
    public void BtnBack()
    {
        titleMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void BtnPlay()
    {
        int t_bpm = songList[currentSong].bpm;

        GameManager.instance.GameStart(currentSong, t_bpm);
        this.gameObject.SetActive(false);
    }
}
