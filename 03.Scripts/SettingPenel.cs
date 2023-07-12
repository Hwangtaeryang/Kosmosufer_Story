using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingPenel : MonoBehaviour
{
    public TextMeshProUGUI userUID;	//내가한거
    public GameObject pwChangePanel;
    public TextMeshProUGUI errText;
    public InputField pw_Field;
    public InputField repw_Field;

    public Toggle pushToggle;
    public Toggle vibrationToggle;


    bool pwSame, pwNine, pwSpecial; //비번동일,9자리이상,특수문자


    private void Start()
    {
        userUID.text = PlayerPrefs.GetString("KS_UserUID");
    }

    //UID 복사
    public void ClipBoardCopy()
    {
        GUIUtility.systemCopyBuffer = userUID.text;

    }

    //고객센터 클릭 이벤트
    public void UserCenterButtonOn()
    {
        Application.OpenURL("http://bojamajafitness.com/inquiry");
    }

    //게임정보 클릭 이벤트
    public void PasswordChangeButtonOn()
    {
        //Application.OpenURL("http://bojamajafitness.com/");
        pwChangePanel.SetActive(true);
    }

    //회원탈퇴 클릭 이벤트
    public void UserDropOutButtonOn()
    {
        PlayerPrefs.DeleteAll();    //전부 삭제
        SceneManager.LoadScene("0.GameSetBoot");
    }

    //온오프 시 진동 여부
    public void VibrationOnOff()
    {
        if(vibrationToggle.isOn.Equals(true))
        {
            //Debug.Log("진동");
            UserDateManager.instance.SetSettingToggle(PlayerPrefs.GetString("KS_PushOnOff"), "On");
            Vibration.Vibrate((long)1000);  //2초동안 진동
        }
        else
        {
            UserDateManager.instance.SetSettingToggle(PlayerPrefs.GetString("KS_PushOnOff"), "Off");
        }
    }

    public void PushOnOff()
    {
        if(pushToggle.isOn.Equals(true))
        {
            UserDateManager.instance.SetSettingToggle("On", PlayerPrefs.GetString("KS_VibrationOnOff"));
        }
        else
        {
            UserDateManager.instance.SetSettingToggle("Off", PlayerPrefs.GetString("KS_VibrationOnOff"));
        }
    }


    //비밀번호 체크 함수
    public void PasswordCheck()
    {
        Check_Password(pw_Field.text);
        StartCoroutine(_PasswordCheck());
    }

    IEnumerator _PasswordCheck()
    {
        yield return null;

        //패스워드가 동일하지 않음
        if (pwSame.Equals(false))
        {
            //noticsPopup.SetActive(true);
            errText.text = "비밀번호가 동일하지 않습니다." + "\n" + "다시 입력해주세요.";
        }
        //비밀번호가 9자리 이상이지 않을 때
        else if (pwNine.Equals(false))
        {
            //noticsPopup.SetActive(true);
            errText.text = "비밀번호가 9자리 이상이 되지 않습니다." + "\n" + "다시 입력해주세요.";
        }
        //비밀번호가 특수문자가 섞여있지 않으면
        else if (pwSpecial.Equals(false))
        {
            //noticsPopup.SetActive(true);
            errText.text = "특수문자와 숫자, 영어를 함께 사용하시길 바랍니다.";
        }
        else if(pwSpecial.Equals(true) && pwSame.Equals(true) && pwNine.Equals(true))
        {
            UserDateManager.instance.SetUserInfo(PlayerPrefs.GetString("KS_UserID"),
                pw_Field.text,
                PlayerPrefs.GetString("KS_UserPWFindEail"),
                PlayerPrefs.GetString("KS_UserUID"),
                PlayerPrefs.GetString("KS_UserLoginState"));
            pwChangePanel.SetActive(false);
        }
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

    public void ClickSound()
    {
        AudioManager.Instance.PlaySFX("sfx_button_return1");
    }

    public void ToggleSound()
    {
        AudioManager.Instance.PlaySFX("Toggle");
    }


    public void EventLinkClick()
    {
        AudioManager.Instance.PlayMusic("Lobby");
        UserDateManager.instance.SetStoryEndingView("Yes");
        SceneManager.LoadScene("1.Login");
        Application.OpenURL("http://bojamajafitness.com/shop?num=1122");
    }

    public void EventNoClick()
    {
        AudioManager.Instance.PlayMusic("Lobby");
        UserDateManager.instance.SetStoryEndingView("Yes");
        SceneManager.LoadScene("1.Login");
    }

    //앱 종류
    public void ApplicationClose()
    {
        Application.Quit();
    }
}
