using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingPenel : MonoBehaviour
{
    public TextMeshProUGUI userUID;	//�����Ѱ�
    public GameObject pwChangePanel;
    public TextMeshProUGUI errText;
    public InputField pw_Field;
    public InputField repw_Field;

    public Toggle pushToggle;
    public Toggle vibrationToggle;


    bool pwSame, pwNine, pwSpecial; //�������,9�ڸ��̻�,Ư������


    private void Start()
    {
        userUID.text = PlayerPrefs.GetString("KS_UserUID");
    }

    //UID ����
    public void ClipBoardCopy()
    {
        GUIUtility.systemCopyBuffer = userUID.text;

    }

    //������ Ŭ�� �̺�Ʈ
    public void UserCenterButtonOn()
    {
        Application.OpenURL("http://bojamajafitness.com/inquiry");
    }

    //�������� Ŭ�� �̺�Ʈ
    public void PasswordChangeButtonOn()
    {
        //Application.OpenURL("http://bojamajafitness.com/");
        pwChangePanel.SetActive(true);
    }

    //ȸ��Ż�� Ŭ�� �̺�Ʈ
    public void UserDropOutButtonOn()
    {
        PlayerPrefs.DeleteAll();    //���� ����
        SceneManager.LoadScene("0.GameSetBoot");
    }

    //�¿��� �� ���� ����
    public void VibrationOnOff()
    {
        if(vibrationToggle.isOn.Equals(true))
        {
            //Debug.Log("����");
            UserDateManager.instance.SetSettingToggle(PlayerPrefs.GetString("KS_PushOnOff"), "On");
            Vibration.Vibrate((long)1000);  //2�ʵ��� ����
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


    //��й�ȣ üũ �Լ�
    public void PasswordCheck()
    {
        Check_Password(pw_Field.text);
        StartCoroutine(_PasswordCheck());
    }

    IEnumerator _PasswordCheck()
    {
        yield return null;

        //�н����尡 �������� ����
        if (pwSame.Equals(false))
        {
            //noticsPopup.SetActive(true);
            errText.text = "��й�ȣ�� �������� �ʽ��ϴ�." + "\n" + "�ٽ� �Է����ּ���.";
        }
        //��й�ȣ�� 9�ڸ� �̻����� ���� ��
        else if (pwNine.Equals(false))
        {
            //noticsPopup.SetActive(true);
            errText.text = "��й�ȣ�� 9�ڸ� �̻��� ���� �ʽ��ϴ�." + "\n" + "�ٽ� �Է����ּ���.";
        }
        //��й�ȣ�� Ư�����ڰ� �������� ������
        else if (pwSpecial.Equals(false))
        {
            //noticsPopup.SetActive(true);
            errText.text = "Ư�����ڿ� ����, ��� �Բ� ����Ͻñ� �ٶ��ϴ�.";
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

    //��й�ȣ Ȯ���ϴ� �Լ�
    public bool Check_Password(string _pw)
    {
        //��й�ȣ�� �������� ���� ��
        if (pw_Field.text != repw_Field.text)
        {
            pwSame = false;
            return false;
        }
        else if (pw_Field.text == repw_Field.text)
        {
            pwSame = true;
        }

        //��й�ȣ�� 9�ڸ��� ���� ���� ���
        if (_pw.Length < 9)
        {
            pwNine = false;
            return false;
        }
        else if (_pw.Length >= 9)
        {
            pwNine = true;
        }

        //Ư�����ڰ� �����մ���
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

    //�� ����
    public void ApplicationClose()
    {
        Application.Quit();
    }
}
