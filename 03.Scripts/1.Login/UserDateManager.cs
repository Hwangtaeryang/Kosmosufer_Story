using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//https://xd.adobe.com/view/057bd145-2abf-446c-bb4b-74f2c375fea9-5028/
//https://xd.adobe.com/view/1e2ecc70-4acb-4394-bb31-b5061b876153-4b7d/screen/283a8bd9-5ff0-402c-a7f8-265d7ea41da9
//https://xd.adobe.com/view/c0be1706-3c2f-4cc5-85cd-6ea3bcae9834-95ed/
//https://docs.google.com/spreadsheets/d/1E9W2FiakjcoAss3Vt_c5p9L_RZMO8-ohzBxlH6OCT2U/edit#gid=0

public class UserInfo
{
    public string userID;
    public string userPw;
    public string userPwFindMail;
    public string userUID;
    public string userNickName;
    public string userCharater;
    public string userLoginState;


    public UserInfo() { }

    public UserInfo(string _id, string _pw, string _findmail, string _uid, string _loginState)
    {
        userID = _id;
        userPw = _pw;
        userPwFindMail = _findmail;
        userUID = _uid;
        userLoginState = _loginState;
    }
}

//보유자산
public class UserAssets
{
    public int crystal;
    public string boardName;

    public UserAssets() { }

    public UserAssets(int _crystal, string _boardName)
    {
        crystal = _crystal;
        boardName = _boardName;
    }
}

//오픈맵
public class MapLevel
{
    public string openMap;


    public MapLevel() { }

    public MapLevel(string _openMap)
    {
        openMap = _openMap;
    }
}

public class StoryModeOnOff
{
    public string storymodeOnOff;

    public StoryModeOnOff() { }

    public StoryModeOnOff(string _onoff)
    {
        storymodeOnOff = _onoff;
    }
}


public class UserDateManager : MonoBehaviour
{
    public static UserDateManager instance { get; private set; }

    public List<UserInfo> userInfoList = new List<UserInfo>();
    public List<UserAssets> userAssetsList = new List<UserAssets>();

    public string userID;
    public string userPw;
    public string userPwFindMail;
    public string userUID;
    public string userLoginState;

    public int crystal;
    public string boardName;

    public string openMap;
    public string storyEndingState;   //엔딩스토리 출력 여부
    public string storyProgress;    //스토리 진행률
    public int acquisitionCrystal;  //획득한 크리스탈 수

    public string pushOnOff;
    public string vibrationOnOff;

    public string tutorialState;    //튜토리얼 상태

    public bool storyOnePlay;   //스토리 진행할 때 필요한거

    public string diaryDay1;    //일기장 오픈 된 날짜
    public string diaryDay2;
    public string diaryDay3;
    public string diaryDay4;

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else instance = this;

        DontDestroyOnLoad(this.gameObject);

    }

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;  //항상 전원
    }

    
    void Update()
    {
    }

    public void InitData()
    {
        PlayerPrefs.SetString("KS_StoryNumber", "");    //클릭한 스토리 번호 ( Story1, Story2, Story3)
        PlayerPrefs.SetString("KS_ChapterNumber", "");  //클릭한 스토리챕터 번호(1-1, 1-1.....2-1,2-2....3-1,3-2...)
        
    }

    public void RaceRecordInit()
    {
        PlayerPrefs.SetString("KS_Ranking_No1", "");    //경기중 랭킹 저장 RaceManger에서 사용
        PlayerPrefs.SetString("KS_Ranking_No2", "");    //경기중 랭킹 저장 RaceManger에서 사용
        PlayerPrefs.SetString("KS_Ranking_No3", "");    //경기중 랭킹 저장 RaceManger에서 사용
        PlayerPrefs.SetString("KS_Ranking_No4", "");    //경기중 랭킹 저장 RaceManger에서 사용
        PlayerPrefs.SetString("KS_Ranking_No5", "");    //경기중 랭킹 저장 RaceManger에서 사용
        PlayerPrefs.SetString("KS_Ranking_No6", "");    //경기중 랭킹 저장 RaceManger에서 사용
        PlayerPrefs.SetInt("KS_TakeCrystal", 0); //획득한 크리스탈 저장RaceManger에서 사용
        PlayerPrefs.SetString("KS_EndTime", "");	//통과시간 저장
        PlayerPrefs.SetString("KS_PassState", "No");    //골인 했는지 여부
        PlayerPrefs.SetString("KS_Laps", "");   //바퀴수
        PlayerPrefs.SetInt("KS_MaxSpeed", 0);   //최고 스피드 저장 - PlayerShip에서 사용됨
    }

    public void SetUserInfo(string _id, string _pw, string _pwfindmail, string _uid, string _loginState)
    {
        userID = _id;
        userPw = _pw;
        userUID = _uid;
        userLoginState = _loginState;
        userPwFindMail = _pwfindmail;

        PlayerPrefs.SetString("KS_UserID", _id);
        PlayerPrefs.SetString("KS_UserPassWord", _pw);
        PlayerPrefs.SetString("KS_UserPWFindEail", _pwfindmail);
        PlayerPrefs.SetString("KS_UserUID", _uid);
        PlayerPrefs.SetString("KS_UserLoginState", _loginState);

        //userInfoList.Add(new UserInfo(_id, _pw, _uid, _nickname, _charater, _loginState));
    }

    public void SetUserAssets(int _crystal, string _boardName)
    {
        crystal = _crystal;
        boardName = _boardName;

        PlayerPrefs.SetInt("KS_Crystal", _crystal);
        PlayerPrefs.SetString("KS_BoardName", _boardName);

        //userAssetsList.Add(new UserAssets(_gold, _crystal, _boardName));
    }

    public void SetOpenMap(string _openMap)
    {
        openMap = _openMap;

        PlayerPrefs.SetString("KS_OpenMap", _openMap);
    }

    public void SetStoryEndingView(string _state)
    {
        storyEndingState = _state;
        PlayerPrefs.SetString("KS_StoryEndingState", _state);   // No/Yes 
    }

    //스토리 진행 상황
    public void SetStoryTalkProgress(string _talk)
    {
        storyProgress = _talk;
        PlayerPrefs.SetString("KS_StoryProgress", _talk);
    }

    //미션 통과 크리스탈 갯수
    public void MissionAcquisitionCrystal(int _crystalNumber)
    {
        acquisitionCrystal = _crystalNumber;
        PlayerPrefs.SetInt("KS_AcquisitionCrystal", _crystalNumber);
    }

    public void SetStoryOnOff(string _state)
    {
        tutorialState = _state;
        PlayerPrefs.SetString("KS_StoryOnOff", _state);
    }

    public void SetSettingToggle(string _push, string _vibration)
    {
        pushOnOff = _push;
        vibrationOnOff = _vibration;

        PlayerPrefs.SetString("KS_PushOnOff", _push);
        PlayerPrefs.SetString("KS_VibrationOnOff", _vibration);
    }

    public void SetDiaryOpenDay(string _day1, string _day2, string _day3, string _day4)
    {
        diaryDay1 = _day1;
        diaryDay2 = _day2;
        diaryDay3 = _day3;
        diaryDay4 = _day4;

        PlayerPrefs.SetString("KS_DiaryOpenDay1", _day1);
        PlayerPrefs.SetString("KS_DiaryOpenDay2", _day2);
        PlayerPrefs.SetString("KS_DiaryOpenDay3", _day3);
        PlayerPrefs.SetString("KS_DiaryOpenDay4", _day4);
    }



    //숫자 콤마 찍는 함수
    public string CommaText(int _data)
    {
        if (_data != 0)
            return string.Format("{0:#,###}", _data);
        else
            return "0";
    }
}
