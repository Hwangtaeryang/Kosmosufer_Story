using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLogin : MonoBehaviour
{
    public GameObject startBtn;
    string loginState;  //로그인 진행 상태
    string player_id, player_uid, player_pw;

    public GameObject settingCanvas;    //세팅팝업 캔버스
    public GameObject setupPenel;   //셋업팡업
    public GameObject appClosePopup;    //앱 종료 팝업

    void Start()
    {
        AutoLoginGo();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingCanvas.SetActive(true);
            setupPenel.SetActive(false);
            appClosePopup.SetActive(true);
        }
#endif
    }

    void AutoLoginGo()
    {
        StartCoroutine(_AutoLogin());
    }

    IEnumerator _AutoLogin()
    {
        yield return null;

        UserDateManager.instance.SetUserInfo(PlayerPrefs.GetString("KS_UserID"),
            PlayerPrefs.GetString("KS_UserPassWord"), PlayerPrefs.GetString("KS_UserPWFindEail"), PlayerPrefs.GetString("KS_UserUID"),
            PlayerPrefs.GetString("KS_UserLoginState"));

        player_id = PlayerPrefs.GetString("KS_UserID");
        player_pw = PlayerPrefs.GetString("KS_UserPassWord");
        player_uid = PlayerPrefs.GetString("KS_UserUID");
        loginState = PlayerPrefs.GetString("KS_UserLoginState");


        if (loginState.Equals("Gateways") || loginState.Equals("Google"))
        {
            startBtn.SetActive(true);
            StartCoroutine(GetUserInfo());  //유저 정보 전부 들고오기
        }
    }

    //로그인 시 모든 정보 들고오는 함수
    IEnumerator GetUserInfo()
    {
        yield return null;

        UserDateManager.instance.SetUserInfo(PlayerPrefs.GetString("KS_UserID"),
                    PlayerPrefs.GetString("KS_UserPassWord"),
                    PlayerPrefs.GetString("KS_UserPWFindEail"),
                    PlayerPrefs.GetString("KS_UserUID"),
                    PlayerPrefs.GetString("KS_UserLoginState"));

        UserDateManager.instance.SetUserAssets(PlayerPrefs.GetInt("KS_Crystal"),
            PlayerPrefs.GetString("KS_BoardName"));
        UserDateManager.instance.SetOpenMap(PlayerPrefs.GetString("KS_OpenMap"));
        UserDateManager.instance.SetStoryEndingView(PlayerPrefs.GetString("KS_StoryEndingState"));

        string openMapNum = PlayerPrefs.GetString("KS_StoryProgress");
        if (openMapNum.Equals("0-1") || openMapNum.Equals("0-1_2") || openMapNum.Equals("0-2") ||
            openMapNum.Equals("0-3") || openMapNum.Equals("0-4") || openMapNum.Equals("0-5") ||
            openMapNum.Equals("0-6"))
            UserDateManager.instance.SetStoryTalkProgress("0-1");
        else if (openMapNum.Equals("1-4") || openMapNum.Equals("1-4_2") || openMapNum.Equals("1-4_3"))
            UserDateManager.instance.SetStoryTalkProgress("1-4");
        else if (openMapNum.Equals("2-1") || openMapNum.Equals("2-1_2"))
            UserDateManager.instance.SetStoryTalkProgress("2-1");
        else if (openMapNum.Equals("3-1") || openMapNum.Equals("3-1_2") || openMapNum.Equals("3-1_3") ||
            openMapNum.Equals("3-1_4") || openMapNum.Equals("3-1_5"))
            UserDateManager.instance.SetStoryTalkProgress("3-1");
        else
            UserDateManager.instance.SetStoryTalkProgress(PlayerPrefs.GetString("KS_StoryProgress"));


        UserDateManager.instance.MissionAcquisitionCrystal(PlayerPrefs.GetInt("KS_AcquisitionCrystal"));

        UserDateManager.instance.SetStoryOnOff(PlayerPrefs.GetString("KS_StoryOnOff"));
        UserDateManager.instance.SetSettingToggle(PlayerPrefs.GetString("KS_PushOnOff"),
            PlayerPrefs.GetString("KS_VibrationOnOff"));

        UserDateManager.instance.SetDiaryOpenDay(PlayerPrefs.GetString("KS_DiaryOpenDay1"),
            PlayerPrefs.GetString("KS_DiaryOpenDay2"), PlayerPrefs.GetString("KS_DiaryOpenDay3"),
            PlayerPrefs.GetString("KS_DiaryOpenDay4"));
    }
}
