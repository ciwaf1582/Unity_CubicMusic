using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    [Header("# 체력 감소 이펙트 #")]
    public float blickSpeed = 0.1f;
    public int blinkCount = 10;
    int currentBlinkCount = 0;

    bool isBlink = false;
    bool isDead = false;

    int maxHp = 3;
    int currentHp = 3;

    int maxShield = 3;
    int currentShield = 0;

    public Image[] hpImg;
    public Image[] shieldImg;
    public int shieldIncreaseCombo = 5; // 실드 추가될 콤보 조건
    int currentShieldCombo;
    public Image shieldGauge;

    Result result;
    NoteManager noteManager;
    public MeshRenderer mesh;

    private void Awake()
    {
        result = FindObjectOfType<Result>();
        noteManager = FindObjectOfType<NoteManager>();
    }
    public void Initialized() // 초기화
    {
        currentHp = maxHp;
        currentShield = 0;
        currentShieldCombo = 0;
        shieldGauge.fillAmount = 0;
        isDead = false;
        SettingHpImg();
        SettingShieldImg();
    }
    public void increaseHp(int num) // 체력 증가
    {
        currentHp += num;
        if (currentHp >= maxHp) { currentHp = maxHp; }

        SettingHpImg();
    }
    public void DecreaseHp(int num) // 체력 감소
    {
        if (!isBlink)
        {
            if (currentShield > 0)
            {
                DecreaseShield(num);
            }
            else
            {
                currentHp -= num;
                if (currentHp <= 0) // 사망 시
                {
                    Debug.Log("GAME OVER");
                    result.ShowResult();
                    noteManager.RemoveNote();
                }
                else // 생존 시
                {
                    StartCoroutine(BlinkCo());
                }
                SettingHpImg();
            }
        }
    }
    void SettingHpImg() // 체력 이미지
    {
        for (int i = 0; i < hpImg.Length; i++)
        {
            if (i < currentHp) // 현재 체력보다 작으면 활성화 
            {
                hpImg[i].gameObject.SetActive(true);
            }
            else
            {
                hpImg[i].gameObject.SetActive(false);
            }
        }
    }
    public void CheckShield()
    {
        currentShieldCombo++;

        // 현재 콤보 값이 실드 생성 조건의 콤보보다 크다면...
        if (currentShieldCombo >= shieldIncreaseCombo)
        {
            currentShieldCombo = 0;
            increaseShield();
        }
        shieldGauge.fillAmount = (float)currentShieldCombo / shieldIncreaseCombo;
    } // 실드가 생성 될 조건의 콤보 카운트
    public void increaseShield() // 실드 추가
    {
        currentShield++;

        if (currentShield >= maxShield)
        {
            currentShield = maxShield;
        }
        SettingShieldImg();
    }
    public void DecreaseShield(int num) // 실드 감소
    {
        currentShield -= num;
        if (currentShield <= 0) 
        {
            currentShield = 0;
        }
        SettingShieldImg();
    }
    public void ResetShieldCombo()
    {
        currentShieldCombo = 0;
        shieldGauge.fillAmount = (float)currentShieldCombo / shieldIncreaseCombo;
    } // 실드가 생성 될 조건의 콤보 초기화
    void SettingShieldImg() // 실드 이미지
    {
        for (int i = 0; i < shieldImg.Length; i++)
        {
            if (i < currentShield) // 현재 체력보다 작으면 활성화 
            {
                shieldImg[i].gameObject.SetActive(true);
            }
            else
            {
                shieldImg[i].gameObject.SetActive(false);
            }
        }
    }
    public bool IsDead()
    {
        return isDead;
    }
    IEnumerator BlinkCo()
    {
        isBlink = true;
        while(currentBlinkCount <= blinkCount)
        {
            mesh.enabled = !mesh.enabled;
            yield return new WaitForSeconds(blickSpeed);
            currentBlinkCount++;
        }

        mesh.enabled = true;
        currentBlinkCount = 0;
        isBlink = false;
    }
}
