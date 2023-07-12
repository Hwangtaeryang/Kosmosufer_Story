using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccountMake : MonoBehaviour
{
    //public GameObject nickNamePanel;
    public GameObject accountPanel;
    //public GameObject noticsPopup;
    public TextMeshProUGUI noticsText;
    public InputField email_Field;
    public InputField pw_Field;
    public InputField repw_Field;
    public InputField pwEmail_Field;
    public Toggle checkToggle;  //개인정보 동의 체크
    public GameObject pwfindmailSaveObj;
    public TextMeshProUGUI emailEffectivenessText;

    string pw_Str;
    string id_Str;

    bool pwSame, pwNine, pwSpecial; //비번동일,9자리이상,특수문자
    bool isEmailFormat; //이메일 포맷
    bool invalidEmailType;
    bool pwFindEmail;   //비번 찾기 이메일

    bool serverIDState;  //임시 서버에 잇나없나 테스트용
    

    void Start()
    {
        serverIDState = false;  //테스트용 추후에 없애도됨

        UserInfoInit(); //기존에 있던 정보들 초기화
    }

    
    void Update()
    {
        
    }

    void UserInfoInit()
    {
        noticsText.text = "";
        //게임속에서만 사용하는 프리팹들
        UserDateManager.instance.InitData();
        UserDateManager.instance.RaceRecordInit();

        //아이디, 비번, uid, 닉네임, 캐릭터, 상태
        UserDateManager.instance.SetUserInfo("", "", "", "", "");
        //크리스탈, 보드
        UserDateManager.instance.SetUserAssets(0, "");
        //오픈맵이름
        UserDateManager.instance.SetOpenMap("1-1");
        //엔딩맵 출력 여부
        UserDateManager.instance.SetStoryEndingView("No");
        //스토리 진행률
        UserDateManager.instance.SetStoryTalkProgress("0-1");
        //미션용 획득한 크리스탈 수
        UserDateManager.instance.MissionAcquisitionCrystal(0);
        //스토리모드 OnOff
        UserDateManager.instance.SetStoryOnOff("On");
        //Map3 플레이 횟수
        UserDateManager.instance.SetDiaryOpenDay("", "", "", "");
    }

    public void Account_Make()
    {
        pw_Str = pw_Field.text;
        Check_Password(pw_Str); //비밀번호 체크
        id_Str = email_Field.text;
        Check_Id(id_Str);   //이메일 아이디 체크

        StartCoroutine(_Account_Make());
    }

    IEnumerator _Account_Make()
    {
        yield return null;

        //서버에서 아이디 검사해서 들고와야함. 아이디가 존재할 경우
        if(serverIDState.Equals(true))
        {
            //noticsPopup.SetActive(true);
            noticsText.text = "존재하는 아이디입니다." + "\n" + "다시 입력해주세요.";
        }
        else
        {
            //이메일을 올바르지 않음
            if(isEmailFormat.Equals(false))
            {
                //noticsPopup.SetActive(true);
                noticsText.text = "올바르지 않은 이메일입니다." + "\n" + "다시 입력해주세요.";
            }
            //패스워드가 동일하지 않음
            else if(pwSame.Equals(false))
            {
                //noticsPopup.SetActive(true);
                noticsText.text = "비밀번호가 동일하지 않습니다." + "\n" + "다시 입력해주세요.";
            }
            //비밀번호가 9자리 이상이지 않을 때
            else if(pwNine.Equals(false))
            {
                //noticsPopup.SetActive(true);
                noticsText.text = "비밀번호가 9자리 이상이 되지 않습니다." + "\n" + "다시 입력해주세요.";
            }
            //비밀번호가 특수문자가 섞여있지 않으면
            else if(pwSpecial.Equals(false))
            {
                //noticsPopup.SetActive(true);
                noticsText.text = "특수문자와 숫자, 영어를 함께 사용하시길 바랍니다.";
            }
            else if(isEmailFormat.Equals(true) && pwSame.Equals(true) && pwNine.Equals(true) && pwSpecial.Equals(true))
            {
                UserInfoSave(); //기본적인 개인정보 저장
                StartCoroutine(LobbyMove());
                //StartCoroutine(FindPWEmailMove());
            }
        }
    }

    //이메일 유효성검사
    public void Email_Effectiveness()
    {
        Check_Id(pwEmail_Field.text);   //이메일 아이디 체크
        StartCoroutine(_Email_Effectiveness());
    }

    IEnumerator _Email_Effectiveness()
    {
        yield return null;

        if(isEmailFormat.Equals(false))
            emailEffectivenessText.text = "올바르지 않은 이메일입니다." + "\n" + "다시 입력해주세요.";
        else
        {
            UserInfoSave(); //기본적인 개인정보 저장
            StartCoroutine(LobbyMove());
        }

    }

    //이메일찾기 메일 한번 더 적기
    IEnumerator FindPWEmailMove()
    {
        yield return new WaitForSeconds(1f);
        pwfindmailSaveObj.SetActive(true);
    }

    IEnumerator LobbyMove()//NickNamePanelShow()
    {
        yield return new WaitForSeconds(1f);
        //accountPanel.SetActive(false);
        SceneManager.LoadScene("3.Lobby");
        //nickNamePanel.SetActive(true);
    }


    //올바른 이메일인지 체크
    public bool Check_Id(string _id)
    {
        if (string.IsNullOrEmpty(_id))
            isEmailFormat = false;

        _id = Regex.Replace(_id, @"(@)(.+)$", this.DomainMapper, RegexOptions.None);
        if (invalidEmailType)
            isEmailFormat = false;

        isEmailFormat = Regex.IsMatch(_id,
                  @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                  RegexOptions.IgnoreCase);

        //Debug.Log("이메일 체크 : " + isEmailFormat);
        return isEmailFormat;
    }

    //도메인으로 변경 함수
    private string DomainMapper(Match match)
    {
        // IdnMapping class with default property values.
        IdnMapping idn = new IdnMapping();

        string domainName = match.Groups[2].Value;
        try
        {
            domainName = idn.GetAscii(domainName);
        }
        catch (ArgumentException)
        {
            invalidEmailType = true;
        }
        return match.Groups[1].Value + domainName;
    }

    //비밀번호 확인하는 함수
    public bool Check_Password(string _pw)
    {
        //비밀번호가 동일하지 않을 때
        if (pw_Field.text != repw_Field.text)
        {
            pwSame = false;
            return false;
        }
        else if (pw_Field.text == repw_Field.text)
        {
            pwSame = true;
        }

        //비밀번호가 9자리가 넘지 않을 경우
        if (_pw.Length < 9)
        {
            pwNine = false;
            return false;
        }
        else if (_pw.Length >= 9)
        {
            pwNine = true;
        }

        //특수문자가 섞여잇는지
        Regex rxPassword =
            new Regex(@"^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{9,}$",
            RegexOptions.IgnorePatternWhitespace);

        pwSpecial = rxPassword.IsMatch(_pw);
        return rxPassword.IsMatch(_pw);
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

    //개인정보 저장 함수
    void UserInfoSave()
    {
        string userUID = UIDMake();

        //아이디, 비번, uid, 닉네임, 캐릭터, 상태
        UserDateManager.instance.SetUserInfo(email_Field.text, pw_Field.text, pwEmail_Field.text,
            userUID, "Gateways");
        UserDateManager.instance.SetUserAssets(0, "낡은 호버보드");
        UserDateManager.instance.SetOpenMap("1-1");
        UserDateManager.instance.SetStoryEndingView("No");
        UserDateManager.instance.SetStoryTalkProgress("0-1");
        UserDateManager.instance.MissionAcquisitionCrystal(0);
        UserDateManager.instance.SetStoryOnOff("On");
        UserDateManager.instance.SetSettingToggle("On", "On");
        UserDateManager.instance.SetDiaryOpenDay("", "", "", "");
        //PlayerPrefs.SetString("KS_UserLoginState", "GatewaysNickName");  //현상태:닉네임 만들기로 가야한다.
        //PlayerPrefs.SetString("KS_UserID", email_Field.text);
        //PlayerPrefs.SetString("KS_UserUID", userUID);
        //PlayerPrefs.SetString("KS_UserPassWord", pw_Field.text);
    }

    //개인정보 동의 노체크로 변경
    public void AgreementNoCheck()
    {
        checkToggle.isOn = false;
    }

    //버튼 클릭 시 InputField에 있는 텍스트 지우기
    public void DeleteInputField(InputField _inputfield)
    {
        _inputfield.text = "";
    }

    public void InputFieldInit()
    {
        email_Field.text = "";
        pw_Field.text = "";
        repw_Field.text = "";
        pwEmail_Field.text = "";
    }
}
