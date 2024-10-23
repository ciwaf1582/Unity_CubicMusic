using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField id;
    public InputField pw;

    DatabaseManager databaseManager;
    void Start()
    {
        databaseManager = FindObjectOfType<DatabaseManager>();
        // Backend 초기화, InitializeCallback 메서드를 콜백으로 전달
        //Backend.Initialize(InitializeCallback);
    }

    // InitializeCallback 메서드 정의 (BackendReturnObject를 매개변수로 받음)
    void InitializeCallback()
    {
        if (Backend.IsInitialized)
        {
            // 초기화가 성공적으로 완료된 경우
            Debug.Log(Backend.Utils.GetServerTime());
            Debug.Log(Backend.Utils.GetGoogleHash());
        }
        else
        {
            // 초기화 실패 처리
            Debug.Log("초기화 실패");
        }
    }
    public void BtnRegist()
    {
        string t_id = id.text;
        string t_pw = pw.text;

        // CustomSignUp 함수를 호출하면 bro로 반환
        BackendReturnObject bro = Backend.BMember.CustomSignUp(t_id, t_pw, "Test");

        if (bro.IsSuccess())
        {
            Debug.Log("회원가입 완료");
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("회원가입 실패");
            // 에러 코드는 뒷끝서버에서 확인하고 코드별 코멘트 설정
            // bro.GetStatusCode() == "201";
        }
    }
    public void BtnLogin()
    {
        string t_id = id.text;
        string t_pw = pw.text;

        BackendReturnObject bro = Backend.BMember.CustomLogin(t_id, t_pw);

        if (bro.IsSuccess())
        {
            Debug.Log("로그인 완료");
            //databaseManager.LoadScore();
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("로그인 실패");
            // 에러 코드는 뒷끝서버에서 확인하고 코드별 코멘트 설정
            // bro.GetStatusCode() == "201";
        }
    }

}
