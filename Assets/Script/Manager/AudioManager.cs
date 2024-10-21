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

    public Sound[] sfx; // ȿ����
    public Sound[] bgm; // ��� ����

    public AudioSource bgmPlayer; // ��� ������ ����� ����� �ҽ�
    public AudioSource[] sfxPlayer; // ȿ������ ����� ����� �ҽ� �迭

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
        Debug.Log("����");
    }
    public void PlayerSFX(string sfxName)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (sfxName == sfx[i].name)
            {
                // ���� ȿ������ ��� ������ ���� ����� �ҽ� ã��
                for (int j = 0; j < sfxPlayer.Length; j++) 
                {
                    if (!sfxPlayer[j].isPlaying)
                    {
                        sfxPlayer[j].clip = sfx[i].clip;
                        sfxPlayer[j].Play();
                        return;
                    }
                }
                Debug.Log("��� ����� �÷��̾ ��� ���Դϴ�.");
                return;
            }
        }
        Debug.Log($"{sfxName} �̸��� ȿ������ �����ϴ�.");
    }
}
