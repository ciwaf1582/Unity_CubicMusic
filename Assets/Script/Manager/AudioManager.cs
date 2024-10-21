using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
    }

    public Sound[] sfx; // 효과음
    public Sound[] bgm; // 배경 음악

    public AudioSource bgmPlayer; // 배경 음악을 재생할 오디오 소스
    public AudioSource[] sfxPlayer; // 효과음을 재생할 오디오 소스 배열

    public void PlayBGM(string bgmName)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (bgmName == bgm[i].name)
            {
                bgmPlayer.clip = bgm[i].clip;
                bgmPlayer.Play();
            }
        }
    }
    public void StopBGM()
    {
        bgmPlayer.Stop();
        Debug.Log("에러");
    }
    public void PlayerSFX(string sfxName)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (sfxName == sfx[i].name)
            {
                // 현재 효과음이 재생 중이지 않은 오디오 소스 찾기
                for (int j = 0; j < sfxPlayer.Length; j++) 
                {
                    if (!sfxPlayer[j].isPlaying)
                    {
                        sfxPlayer[j].clip = sfx[i].clip;
                        sfxPlayer[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 오디오 플레이어가 재생 중입니다.");
                return;
            }
        }
        Debug.Log($"{sfxName} 이름의 효과음이 없습니다.");
    }
}
