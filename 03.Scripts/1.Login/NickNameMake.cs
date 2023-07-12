using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NickNameMake : MonoBehaviour
{
    List<Dictionary<string, object>> data;

    public InputField nickname_field;
    public Text overlapText;

    string specialStr;  //특수문자
    string nicknameStr;    //닉네임복사

    bool specialState;  //특수문자 사용 여부
    bool overlapState;  //중복상태 여부
    bool overlapOnBtn;  //중복체크 버튼 클릭 여부
    bool nullState; //닉네임 넬 체크 여부
    bool curseState;    //욕사용 여부
    
    string loginState;  //로그인 여부


    //테스트용 서버 넥네임 중복 - 나중에 지우기
    bool serverOverlap;

    void Start()
    {
        nicknameStr = "";
        specialState = true;    //초기값 true가 특수문자 사용했다는것
        curseState = true;  //초기값 true가 욕 사용했다는 것
        nullState = true;
    }

    
    public void NickName_OverlapCheck()
    {
        StartCoroutine(_NickName_OverlapCheck());
    }

    IEnumerator _NickName_OverlapCheck()
    {
        yield return null;

        overlapOnBtn = true;    //중복체크 여부(체크함)
        nicknameStr = nickname_field.text;
        nicknameStr = nicknameStr.Replace(" ", "");   //공백제거

        //닉네임이 없을 때
        if(nicknameStr.Equals(""))
        {
            nullState = true;
        }
        else
        {
            nullState = false;
            specialState = Special_Character_Check(nicknameStr);    //특수문자사용여부
            curseState = HangeulCurseCheck(nicknameStr);    //욕설사용여부

            //특수문자 안쓰고, 욕설도 없음
            if(specialState.Equals(false) && curseState.Equals(false))
            {
                //서버에서 확인해야함 - 성엽이 구간
                if(serverOverlap.Equals(true))
                {
                    overlapState = true;    //닉네임 중복
                }
                else
                {
                    overlapState = false;   //닉네임 중복 아님
                }
            }
        }

        if(nullState.Equals(false) && overlapState.Equals(false) && specialState.Equals(false) && curseState.Equals(false))
        {
            overlapText.text = "사용가능한 닉네임입니다.";
        }
        //닉네임이 없음 넬값
        else if(nullState.Equals(true))
        {
            overlapText.text = "닉네임을 입력하세요";
        }
        //중복닉네임 일때
        else if(overlapState.Equals(true))
        {
            overlapText.text = "닉네임 중복입니다.";
        }
        //특수문자 사용안했을 때
        else if(specialState.Equals(true))
        {
            overlapText.text = "특수문자를 사용했습니다. 다시 입력해주세요.";
        }
        //욕설 사용했을 때
        else if(curseState.Equals(true))
        {
            overlapText.text = "비속어를 사용했습니다. 다시 입력해주세요.";
        }
        else
        {
            overlapText.text = "닉네임을 다시 설정하여 주십시오";
        }
    }

    //닉네임 특수문자 사용 여부 확인 함수
    bool Special_Character_Check(string _nickname)
    {
        specialStr = @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]";

        Regex regex = new Regex(specialStr);

        //Debug.Log(regex.IsMatch(_nickname));
        //True가 나오면 특수문자를 사용한다
        specialState = regex.IsMatch(_nickname);
        return regex.IsMatch(_nickname);
    }

    //한글욕검사
    bool HangeulCurseCheck(string _nickname)
    {
        data = CSVReader.Read("Swearlist");

        bool isCheck = false;

        for (int i = 0; i < data.Count; i++)
        {
            //isCheck = data[i]["욕"].ToString().Contains(_nickname);
            isCheck = _nickname.Contains(data[i]["욕"].ToString());
            if (isCheck.Equals(true))
                return isCheck;
        }

        return isCheck;
    }

    //닉네임 저장
    public void NickNameSavaButton()
    {
        //검사했던 닉네임이랑 같은지 확인
        if(nicknameStr.Equals(nickname_field.text) && nicknameStr != "")
        {
            //중복체크 버튼 눌렀을 때
            if(overlapOnBtn.Equals(true))
            {
                if(nullState.Equals(false) && specialState.Equals(false) && curseState.Equals(false)&& overlapState.Equals(false))
                {
                    if (loginState == "GoogleNickName")
                        PlayerPrefs.SetString("KS_UserLoginState", "GoogleCharacter");
                    else if (loginState == "GatewaysNickName")
                        PlayerPrefs.SetString("KS_UserLoginState", "GatewaysCharacter");

                    PlayerPrefs.SetString("KS_UserNickName", nickname_field.text);
                    overlapOnBtn = false;

                    //아이디, 비번, uid, 상태
                    UserDateManager.instance.SetUserInfo(PlayerPrefs.GetString("KS_UserID"),
                        PlayerPrefs.GetString("KS_UserPassWord"),  PlayerPrefs.GetString("KS_UserPWFindEail"), PlayerPrefs.GetString("KS_UserUID")
                        , "GatewaysCharacter");

                    //서버에 닉네임 저장 시키기


                    SceneManager.LoadScene("2.CharacterChoiceBoot");
                }
                else
                {
                    overlapOnBtn = false;
                    overlapText.text = "닉네임을 다시 설정하여 주십시오";
                }
            }
            else
            {
                overlapOnBtn = false;
                overlapText.text = "닉네임 중복 체크를 해주시길 바랍니다.";
            }
        }
        else if(nickname_field.text.Equals(""))
        {
            overlapOnBtn = false;    //중복체크 여부(체크안함)
            overlapText.text = "닉네임을 설정하여 주십시오.";
        }
        else if (overlapOnBtn.Equals(false))
        {
            overlapOnBtn = false;    //중복체크 여부(체크안함)
            overlapText.text = "닉네임 중복 체크를 해주시길 바랍니다.";
        }
    }
}
