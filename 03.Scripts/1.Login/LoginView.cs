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

    public InputField id_Field; //���̵�
    public InputField pw_Field; //��й�ȣ

    public GameObject progressPanel;
    public GameObject nickNamePanel;
    public GameObject pwFindPanel;
    public GameObject accountViewPanel;

    public GameObject noticsPopup;
    public Text noticsText;


    string loginState;  //�α��� ���� ����
    string player_id, player_uid, player_pw;



    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        //���� �÷��� �α׸� Ȯ���Ϸ��� Ȱ��ȭ
        PlayGamesPlatform.DebugLogEnabled = true;
        //���� �÷��� Ȱ��ȭ
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

    //�̸��� �α��� ��ư ������ �� ���� ������ ������ ��ǲ�ʴ��� ����
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
            StartCoroutine(GetUserInfo());  //���� ���� ���� ������
            StartCoroutine(ProgressPanelOpen(true, false, false));  //���ӽ����ǳ�, �г����ĳ�, ĳ���� �ǳ�
            StartCoroutine(LoginProgressState(false, false)); //�س��� �ǳ�, ���ã���ǳ�
        }
        else if(loginState.Equals("GatewaysNickName") || loginState.Equals("GoogleNickName"))
        {
            StartCoroutine(ProgressPanelOpen(false, true, false));  //���ӽ����ǳ�, �г����ĳ�, ĳ���� �ǳ�
            StartCoroutine(LoginProgressState(true, false)); //�س��� �ǳ�, ���ã���ǳ�
        }
        else if(loginState.Equals("GatewaysCharacter") || loginState.Equals("GoogleCharacter"))
        {
            Debug.Log("����ȵ�����");
            //PlayerPrefs.SetString("KS_UserNickName", "");   //�������� �г��� ������   
            StartCoroutine(ProgressPanelOpen(false, false, true));  //���ӽ����ǳ�, �г����ĳ�, ĳ���� �ǳ�
        }
    }

    //�Ϲ� �α��� ��ư Ŭ�� �̺�Ʈ
    public void BaseLoginButton()
    {
        StartCoroutine(_BaseLoginButton());
    }

    IEnumerator _BaseLoginButton()
    {
        yield return null;

        //�������� �������� �������� ���̵��
        if(id_Field.text.Equals(PlayerPrefs.GetString("KS_UserID")))
        {
            if(pw_Field.text.Equals(PlayerPrefs.GetString("KS_UserPassWord")))
            {
                //��ȣ�ȿ� ������ �������� ���°� �־��ֱ�
                UserDateManager.instance.SetUserInfo(PlayerPrefs.GetString("KS_UserID"),
                    PlayerPrefs.GetString("KS_UserPassWord"), 
                    PlayerPrefs.GetString("KS_UserPWFindEail"),
                    PlayerPrefs.GetString("KS_UserUID"), 
                    PlayerPrefs.GetString("KS_UserLoginState"));

                loginState = PlayerPrefs.GetString("KS_UserLoginState");

                if(loginState.Equals("Gateways"))
                {
                    StartCoroutine(GetUserInfo());  //���� ���� ���� ������
                    StartCoroutine(ProgressPanelOpen(true, false, false));  //���ӽ����ǳ�, �г����ĳ�, ĳ���� �ǳ�
                    StartCoroutine(LoginProgressState(false, false)); //���ӽ����ǳ�, �س��� �ǳ�, ���ã���ǳ�
                }
                else if(loginState.Equals("GatewaysNickName"))
                {
                    StartCoroutine(ProgressPanelOpen(false, true, false));  //���ӽ����ǳ�, �г����ĳ�, ĳ���� �ǳ�
                    StartCoroutine(LoginProgressState(true, false)); //���ӽ����ǳ�, �س��� �ǳ�, ���ã���ǳ�
                }
                else if(loginState.Equals("GatewaysCharater"))
                {
                    StartCoroutine(ProgressPanelOpen(false, false, true));  //���ӽ����ǳ�, �г����ĳ�, ĳ���� �ǳ�
                }
            }
            else
            {
                noticsPopup.SetActive(true);
                noticsText.text = "�Է��� ��й�ȣ�� ���� �ʽ��ϴ�.";
            }
        }
        else
        {
            noticsPopup.SetActive(true);
            noticsText.text = "�Է��� ���̴ٰ� �������� �ʽ��ϴ�.";
        }
    }

    //���۷α��� ��ư Ŭ��
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
                //text.text = "�α��� ����";
                StartCoroutine(GoogleSuccess());
            }
            else
            {
                //text.text = "�α��� ����";
                noticsPopup.SetActive(true);
                noticsText.text = "���۷α��� �����Ͽ����ϴ�.";
            }
        });
    }

    IEnumerator GoogleSuccess()
    {
        yield return new WaitForSeconds(1.5f);

        //���̵� ������ - ù �α���
        if(Social.localUser.id != "������ ���̵� �˻�")
        {
            player_uid = UIDMake();
            //���̵�, ���, uid, �г���, ĳ����, ����
            UserDateManager.instance.SetUserInfo(Social.localUser.id, "", "",
                player_uid, "Google");

            //�ʱⰪ ���� ù����
            UserDateManager.instance.SetUserAssets(0, "���� ȣ������");
            UserDateManager.instance.SetOpenMap("1-1");
            UserDateManager.instance.SetStoryEndingView("No");
            UserDateManager.instance.SetStoryTalkProgress("0-1");
            UserDateManager.instance.SetStoryOnOff("On");
            UserDateManager.instance.SetSettingToggle("On", "On");
            UserDateManager.instance.MissionAcquisitionCrystal(0);
            UserDateManager.instance.SetDiaryOpenDay("", "", "", "");

            GameAdmission();
            //StartCoroutine(ProgressPanelOpen(true, false, false));  //���ӽ����ǳ�, �г����ĳ�, ĳ���� �ǳ�
            //StartCoroutine(LoginProgressState(false, false)); //���ӽ����ǳ�, �س��� �ǳ�, ���ã���ǳ�
            //StartCoroutine(ProgressPanelOpen(false, true, false));  //���ӽ����ǳ�, �г����ĳ�, ĳ���� �ǳ�
            //StartCoroutine(LoginProgressState(true, false)); //�س��� �ǳ�, ���ã���ǳ�
        }
        else
        {
            //���̵�, ���, uid, �г���, ĳ����, ���� // ������ ���� �������� ������
            UserDateManager.instance.SetUserInfo(Social.localUser.id,
                PlayerPrefs.GetString("KS_UserPassWord"),
                PlayerPrefs.GetString("KS_UserPWFindEail"),
                PlayerPrefs.GetString("KS_UserUID"),
                PlayerPrefs.GetString("KS_UserLoginState"));

            loginState = PlayerPrefs.GetString("KS_UserLoginState");

            if(loginState.Equals("Google"))
            {
                StartCoroutine(GetUserInfo());  //���� ���� ���� ������
                GameAdmission();
                //StartCoroutine(ProgressPanelOpen(true, false, false));  //���ӽ����ǳ�, �г����ĳ�, ĳ���� �ǳ�
                //StartCoroutine(LoginProgressState(false, false)); //���ӽ����ǳ�, �س��� �ǳ�, ���ã���ǳ�
            }
            //else if(loginState.Equals("GoogleNickName"))
            //{
            //    StartCoroutine(ProgressPanelOpen(false, true, false));  //���ӽ����ǳ�, �г����ĳ�, ĳ���� �ǳ�
            //    StartCoroutine(LoginProgressState(true, false)); //���ӽ����ǳ�, �س��� �ǳ�, ���ã���ǳ�
            //}
            //else if(loginState.Equals("GoogleCharacter"))
            //{
            //    StartCoroutine(ProgressPanelOpen(false, false, true));  //���ӽ����ǳ�, �г����ĳ�, ĳ���� �ǳ�
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

    //�α��� ���� ���� - ���� �������� �ǳ�
    IEnumerator LoginProgressState(bool _nickName, bool _pwFind)
    {
        yield return new WaitForSeconds(2);

        nickNamePanel.SetActive(_nickName);
        pwFindPanel.SetActive(_pwFind);
    }

    //���� ���·� �ǳ� �����µ� �빮������ �ǳ� ����.(��)�г��� ȭ������ �̵��϶�� �ǳ�)
    IEnumerator ProgressPanelOpen(bool _start, bool _nick, bool _charatger)
    {
        yield return new WaitForSeconds(2);

        progressPanel.transform.GetChild(0).gameObject.SetActive(_start);
        progressPanel.transform.GetChild(1).gameObject.SetActive(_nick);
        progressPanel.transform.GetChild(2).gameObject.SetActive(_charatger);
    }

    //��ư Ŭ�� �� InputField�� �ִ� �ؽ�Ʈ �����
    public void DeleteInputField(InputField _inputfield)
    {
        _inputfield.text = "";
    }

    //ĳ���� �� �̵�
    public void CharacterSceneMove()
    {
        SceneManager.LoadScene("2.CharacterChoiceBoot");
    }

    //���� ���� �ϱ�
    public void GameAdmission()
    {
        AudioManager.Instance.PlayMusic("Lobby");
        SceneManager.LoadScene("3.Lobby");
    }

    //�������̵� ������ �ִ��� ������ �Ǻ� �� �˾� ����
    public void BaseIDExist_HaveNo()
    {
        if (PlayerPrefs.GetString("KS_UserID") != "")
        {
            noticsPopup.SetActive(true);
            noticsText.text = "������ �������  ���̵� �ֽ��ϴ�.\n" +
                "���ο� ������ ���� �� ���� �����Ͱ� �����ǰ�,\n" +
                "�����Ͻ� �� �����ϴ�.\n" +
                "�׷��� ���ο� ������ ����ðڽ��ϱ�?";
        }
        else
        {
            accountViewPanel.SetActive(true);
        }
    }

    

    //�α��� �� ��� ���� ������ �Լ�
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
