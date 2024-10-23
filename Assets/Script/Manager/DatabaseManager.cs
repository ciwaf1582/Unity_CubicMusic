using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;

public class DatabaseManager : MonoBehaviour
{
    public int[] score;

    //public void SaveScore()
    //{
    //    #region
    //    //// PlayerPrefs : 데이터를 자체 기기에 저장(string, int, float)
    //    //PlayerPrefs.SetInt("Score1", score[0]);
    //    //PlayerPrefs.SetInt("Score2", score[1]);
    //    //PlayerPrefs.SetInt("Score3", score[2]);
    //    #endregion 
    //    // 비동기 (백그라운드)
    //    BackendAsyncClass.BackendAsync(Backend.GameInfo.GetPrivateContents, "Score", UserDataBro =>
    //    {
    //        if (UserDataBro.isSuccess())
    //        {
    //            Param data = new Param();
    //            data.Add("Scores", score); // (키 값, 밸류 값)

    //            if (UserDataBro.GetReturnValuetoJSON()["rows"].Count > 0)
    //            {
    //                // 수정할 데이터의 식별값
    //                string t_Indate = UserDataBro.GetReturnValuetoJSON()["rows"][0]["InDate"]["S"].ToString();
    //                BackendAsyncClass.BackendAsync(Backend.GameInfo.Update, "Score", t_Indate, data, (t_callback) =>
    //                {
    //                    // 성공 시 메세지, 실패 시 해쉬 코드별 메세지 추가
    //                });
    //            }
    //            else
    //            {
    //                BackendAsyncClass.BackendAsync(Backend.GameInfo.Insert, "Score", data, (t_callback) =>
    //                {
    //                    // 성공 시 메세지, 실패 시 해쉬 코드별 메세지 추가
    //                });
    //            }
    //        }
    //    });
    //}
    //public void LoadScore()
    //{
    //    // 반드시 키가 있는지 먼저 체크
    //    if (PlayerPrefs.HasKey("Score1"))
    //    {
    //        #region
    //        score[0] = PlayerPrefs.GetInt("Score1");
    //        score[1] = PlayerPrefs.GetInt("Score2");
    //        score[2] = PlayerPrefs.GetInt("Score3");
    //        #endregion
    //        BackendAsyncClass.BackendAsync(Backend.GameInfo.GetPrivateContents, "Score", UserDataBro =>
    //        {
    //            // UserDataBro 이름으로 데이터를 가져온 후 생성
    //            JsonData t_data = UserDataBro.GetReturnValuetoJSON();
    //            // 이후 처리
    //            if (t_data.Count > 0)
    //            {
    //                // 그리드에서 스코어스를 리스트 형태로 호출
    //                JsonData t_List = t_data["rows"][0]["Scores"]["L"];
    //                for (int i = 0; i < t_List.Count; i++)
    //                {
    //                    var t_value = t_List[i]["N"];
    //                    score[i] = int.Parse(t_value.ToString());
    //                }
    //                Debug.Log("로드 완료");
    //            }
    //            else
    //            {
    //                Debug.Log("로드 실패");
    //            }
    //        });
    //    }
    //}
}
