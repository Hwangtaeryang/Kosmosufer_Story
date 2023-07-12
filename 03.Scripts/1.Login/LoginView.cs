using GooglePlayGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



//https://xd.adobe.com/view/c0be1706-3c2f-4cc5-85cd-6ea3bcae9834-95ed/
//https://docs.google.com/spreadsheets/d/1E9W2FiakjcoAss3Vt_c5p9L_RZMO8-ohzBxlH6OCT2U/edit#gid=0

public class LoginView : MonoBehaviour
{
    static public LoginView instance { get; private set; }

    public InputField id_Field; //아이디
    public InputField pw_Field; //비밀번호

    public GameObject progressPanel;
    public GameObject nickNamePanel;
    public GameObject pwFindPanel;
    public GameObject accountViewPanel;

    public GameObject noticsPopup;
    public Text noticsText;


    string loginState;  //로그인 진행 상태
    string player_id, player_uid, player_pw;



    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        //구글 플레이 로그를 확인하려면 활성화
        PlayGamesPlatform.DebugLogEnabled = true;
        //구글 플레이 활성화
        PlayGamesPlatform.Activate();

        //PlayerPrefs.SetString("KS_UserLoginState", "GatewaysCharacter");
        //AutoLogin();

        if(PlayerPrefs.GetString("KS_UserLoginState").Equals("Gateways"))
        {
            if (PlayerPrefs.GetString("KS_UserID") != "")
            {
                id_Field.text = PlayerPrefs.GetString("KS_UserID");
                pw_Field.text = PlayerPrefs.GetString("KS_UserPassWord");
            }
        }
    }

    //이메일 로그인 버튼 눌렀을 때 기존 정보가 있을면 인풋필더에 적기
    public void EmailButtonOn()
    {
        if (PlayerPrefs.GetString("KS_UserLoginState").Equals("Gateways"))
        {
            if (PlayerPrefs.GetString("KS_UserID") != "")
            {
                id_Field.text = PlayerPrefs.GetString("KS_UserID");
                pw_Field.text = PlayerPrefs.GetString("KS_UserPassWord");
            }
        }
    }


    void AutoLogin()
    {
        StartCoroutine(_AutoLogin());
    }

    IEnumerator _AutoLogin()
    {
        yield return null;

        UserDateManager.instance.SetUserInfo(PlayerPrefs.GetString("KS_UserID"),
            PlayerPrefs.GetString("KS_UserPassWord"),  PlayerPrefs.GetString("KS_UserPWFindEail"), PlayerPrefs.GetString("KS_UserUID"),
            PlayerPrefs.GetString("KS_UserLoginState"));

        player_id = PlayerPrefs.GetString("KS_UserID");
        player_pw = PlayerPrefs.GetString("KS_UserPassWord");
        player_uid = PlayerPrefs.GetString("KS_UserUID");
        loginState = PlayerPrefs.GetString("KS_UserLoginState");
        

        if (loginState.Equals("Gateways") || loginState.Equals("Google"))
        {
            StartCoroutine(GetUserInfo());  //유저 정보 전부 들고오기
            StartCoroutine(ProgressPanelOpen(true, false, false));  //게임시작판넬, 닉네임파넬, 캐릭터 판넬
            StartCoroutine(LoginProgressState(false, false)); //넥네임 판넬, 비번찾기판넬
        }
        else if(loginState.Equals("GatewaysNickName") || loginState.Equals("GoogleNickName"))
        {
            StartCoroutine(ProgressPanelOpen(false, true, false));  //게임시작판넬, 닉네임파넬, 캐릭터 판넬
            StartCoroutine(LoginProgressState(true, false)); //넥네임 판넬, 비번찾기판넬
        }
        else if(loginState.Equals("GatewaysCharacter") || loginState.Equals("GoogleCharacter"))
        {
            Debug.Log("여기안들어오나");
            //PlayerPrefs.SetString("KS_UserNickName", "");   //서버에서 닉네임 들고오기   
            StartCoroutine(ProgressPanelOpen(false, false, true));  //게임시작판넬, 닉네임파넬, 캐릭터 판넬
        }
    }

    //일반 로그인 버튼 클릭 이벤트
    public void BaseLoginButton()
    {
        StartCoroutine(_BaseLoginButton());
    }

    IEnumerator _BaseLoginButton()
    {
        yield return null;

        //서버연동 프리팹을 서버에서 아이디로
        if(id_Field.text.Equals(PlayerPrefs.GetString("KS_UserID")))
        {
            if(pw_Field.text.Equals(PlayerPrefs.GetString("KS_UserPassWord")))
            {
                //괄호안에 프리팹 서버에서 들고온거 넣어주기
                UserDateManager.instance.SetUserInfo(PlayerPrefs.GetString("KS_UserID"),
                    PlayerPrefs.GetString("KS_UserPassWord"), 
                    PlayerPrefs.GetString("KS_UserPWFindEail"),
                    PlayerPrefs.GetString("KS_UserUID"), 
                    PlayerPrefs.GetString("KS_UserLoginState"));

                loginState = PlayerPrefs.GetString("KS_UserLoginState");

                if(loginState.Equals("Gateways"))
                {
                    StartCoroutine(GetUserInfo());  //유저 정보 전부 들고오기
                    StartCoroutine(ProgressPanelOpen(true, false, false));  //게임시작판넬, 닉네임파넬, 캐릭터 판넬
                    StartCoroutine(LoginProgressState(false, false)); //게임시작판넬, 넥네임 판넬, 비번찾기판넬
                }
                else if(loginState.Equals("GatewaysNickName"))
                {
                    StartCoroutine(ProgressPanelOpen(false, true, false));  //게임시작판넬, 닉네임파넬, 캐릭터 판넬
                    StartCoroutine(LoginProgressState(true, false)); //게임시작판넬, 넥네임 판넬, 비번찾기판넬
                }
                else if(loginState.Equals("GatewaysCharater"))
                {
                    StartCoroutine(ProgressPanelOpen(false, false, true));  //게임시작판넬, 닉네임파넬, 캐릭터 판넬
                }
            }
            else
            {
                noticsPopup.SetActive(true);
                noticsText.text = "입력한 비밀번호가 맞지 않습니다.";
            }
        }
        else
        {
            noticsPopup.SetActive(true);
            noticsText.text = "입력한 아이다가 존재하지 않습니다.";
        }
    }

    //구글로그인 버튼 클릭
    public void GoogleLoginButton()
    {
        StartCoroutine(_GoogleLoginButton());
    }

    //public Text text;

    IEnumerator _GoogleLoginButton()
    {
        yield return null;

        Social.localUser.Authenticate((bool success) =>
        {
            if(success)
            {
                //text.text = "로그인 성공";
                StartCoroutine(GoogleSuccess());
            }
            else
            {
                //text.text = "로그인 실패";
                noticsPopup.SetActive(true);
                noticsText.text = "구글로그인 실패하였습니다.";
            }
        });
    }

    IEnumerator GoogleSuccess()
    {
        yield return new WaitForSeconds(1.5f);

        //아이디가 없으면 - 첫 로그인
        if(Social.localUser.id != "서버에 아이디 검색")
        {
            player_uid = UIDMake();
            //아이디, 비번, uid, 닉네임, 캐릭터, 상태
            UserDateManager.instance.SetUserInfo(Social.localUser.id, "", "",
                player_uid, "Google");

            //초기값 저장 첫시작
            UserDateManager.instance.SetUserAssets(0, "낡은 호버보드");
            UserDateManager.instance.SetOpenMap("1-1");
            UserDateManager.instance.SetStoryEndingView("No");
            UserDateManager.instance.SetStoryTalkProgress("0-1");
            UserDateManager.instance.SetStoryOnOff("On");
            UserDateManager.instance.SetSettingToggle("On", "On");
            UserDateManager.instance.MissionAcquisitionCrystal(0);
            UserDateManager.instance.SetDiaryOpenDay("", "", "", "");

            GameAdmission();
            //StartCoroutine(ProgressPanelOpen(true, false, false));  //게임시작판넬, 닉네임파넬, 캐릭터 판넬
            //StartCoroutine(LoginProgressState(false, false)); //게임시작판넬, 넥네임 판넬, 비번찾기판넬
            //StartCoroutine(ProgressPanelOpen(false, true, false));  //게임시작판넬, 닉네임파넬, 캐릭터 판넬
            //StartCoroutine(LoginProgressState(true, false)); //넥네임 판넬, 비번찾기판넬
        }
        else
        {
            //아이디, 비번, uid, 닉네임, 캐릭터, 상태 // 프리팹 전부 서버에서 들고오기
            UserDateManager.instance.SetUserInfo(Social.localUser.id,
                PlayerPrefs.GetString("KS_UserPassWord"),
                PlayerPrefs.GetString("KS_UserPWFindEail"),
                PlayerPrefs.GetString("KS_UserUID"),
                PlayerPrefs.GetString("KS_UserLoginState"));

            loginState = PlayerPrefs.GetString("KS_UserLoginState");

            if(loginState.Equals("Google"))
            {
                StartCoroutine(GetUserInfo());  //유저 정보 전부 들고오기
                GameAdmission();
                //StartCoroutine(ProgressPanelOpen(true, false, false));  //게임시작판넬, 닉네임파넬, 캐릭터 판넬
                //StartCoroutine(LoginProgressState(false, false)); //게임시작판넬, 넥네임 판넬, 비번찾기판넬
            }
            //else if(loginState.Equals("GoogleNickName"))
            //{
            //    StartCoroutine(ProgressPanelOpen(false, true, false));  //게임시작판넬, 닉네임파넬, 캐릭터 판넬
            //    StartCoroutine(LoginProgressState(true, false)); //게임시작판넬, 넥네임 판넬, 비번찾기판넬
            //}
            //else if(loginState.Equals("GoogleCharacter"))
            //{
            //    StartCoroutine(ProgressPanelOpen(false, false, true));  //게임시작판넬, 닉네임파넬, 캐릭터 판넬
            //}
        }
    }

    string UIDMake()
    {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        int currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
        int z1 = UnityEngine.Random.Range(0, 1000000);
        string number = String.Format("%07d", z1);
        string uid = currentEpochTime + "01" + z1;
        return uid;
    }

    //로그인 진행 상태 - 현재 보여지는 판넬
    IEnumerator LoginProgressState(bool _nickName, bool _pwFind)
    {
        yield return new WaitForSeconds(2);

        nickNamePanel.SetActive(_nickName);
        pwFindPanel.SetActive(_pwFind);
    }

    //현재 상태로 판넬 열리는데 대문역할인 판넬 오픈.(예)닉네임 화면으로 이동하라는 판넬)
    IEnumerator ProgressPanelOpen(bool _start, bool _nick, bool _charatger)
    {
        yield return new WaitForSeconds(2);

        progressPanel.transform.GetChild(0).gameObject.SetActive(_start);
        progressPanel.transform.GetChild(1).gameObject.SetActive(_nick);
        progressPanel.transform.GetChild(2).gameObject.SetActive(_charatger);
    }

    //버튼 클릭 시 InputField에 있는 텍스트 지우기
    public void DeleteInputField(InputField _inputfield)
    {
        _inputfield.text = "";
    }

    //캐릭터 씬 이동
    public void CharacterSceneMove()
    {
        SceneManager.LoadScene("2.CharacterChoiceBoot");
    }

    //게임 시작 하기
    public void GameAdmission()
    {
        AudioManager.Instance.PlayMusic("Lobby");
        SceneManager.LoadScene("3.Lobby");
    }

    //기존아이디를 가지고 있는지 없는지 판별 후 팝업 띄우기
    public void BaseIDExist_HaveNo()
    {
        if (PlayerPrefs.GetString("KS_UserID") != "")
        {
            noticsPopup.SetActive(true);
            noticsText.text = "기존에 사용중인  아이디가 있습니다.\n" +
                "새로운 계정을 만들 시 기존 데이터가 삭제되고,\n" +
                "복구하실 수 없습니다.\n" +
                "그래도 새로운 계정을 만드시겠습니까?";
        }
        else
        {
            accountViewPanel.SetActive(true);
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
        else if(openMapNum.Equals("1-4") || openMapNum.Equals("1-4_2") || openMapNum.Equals("1-4_3"))
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
