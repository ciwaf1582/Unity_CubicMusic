using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    [Header("# ü�� ���� ����Ʈ #")]
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
    public int shieldIncreaseCombo = 5; // �ǵ� �߰��� �޺� ����
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
    public void Initialized() // �ʱ�ȭ
    {
        currentHp = maxHp;
        currentShield = 0;
        currentShieldCombo = 0;
        shieldGauge.fillAmount = 0;
        isDead = false;
        SettingHpImg();
        SettingShieldImg();
    }
    public void increaseHp(int num) // ü�� ����
    {
        currentHp += num;
        if (currentHp >= maxHp) { currentHp = maxHp; }

        SettingHpImg();
    }
    public void DecreaseHp(int num) // ü�� ����
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
                if (currentHp <= 0) // ��� ��
                {
                    Debug.Log("GAME OVER");
                    result.ShowResult();
                    noteManager.RemoveNote();
                }
                else // ���� ��
                {
                    StartCoroutine(BlinkCo());
                }
                SettingHpImg();
            }
        }
    }
    void SettingHpImg() // ü�� �̹���
    {
        for (int i = 0; i < hpImg.Length; i++)
        {
            if (i < currentHp) // ���� ü�º��� ������ Ȱ��ȭ 
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

        // ���� �޺� ���� �ǵ� ���� ������ �޺����� ũ�ٸ�...
        if (currentShieldCombo >= shieldIncreaseCombo)
        {
            currentShieldCombo = 0;
            increaseShield();
        }
        shieldGauge.fillAmount = (float)currentShieldCombo / shieldIncreaseCombo;
    } // �ǵ尡 ���� �� ������ �޺� ī��Ʈ
    public void increaseShield() // �ǵ� �߰�
    {
        currentShield++;

        if (currentShield >= maxShield)
        {
            currentShield = maxShield;
        }
        SettingShieldImg();
    }
    public void DecreaseShield(int num) // �ǵ� ����
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
    } // �ǵ尡 ���� �� ������ �޺� �ʱ�ȭ
    void SettingShieldImg() // �ǵ� �̹���
    {
        for (int i = 0; i < shieldImg.Length; i++)
        {
            if (i < currentShield) // ���� ü�º��� ������ Ȱ��ȭ 
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
