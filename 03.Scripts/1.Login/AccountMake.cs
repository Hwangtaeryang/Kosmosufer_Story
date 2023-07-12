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
    public Toggle checkToggle;  //�������� ���� üũ
    public GameObject pwfindmailSaveObj;
    public TextMeshProUGUI emailEffectivenessText;

    string pw_Str;
    string id_Str;

    bool pwSame, pwNine, pwSpecial; //�������,9�ڸ��̻�,Ư������
    bool isEmailFormat; //�̸��� ����
    bool invalidEmailType;
    bool pwFindEmail;   //��� ã�� �̸���

    bool serverIDState;  //�ӽ� ������ �ճ����� �׽�Ʈ��
    

    void Start()
    {
        serverIDState = false;  //�׽�Ʈ�� ���Ŀ� ���ֵ���

        UserInfoInit(); //������ �ִ� ������ �ʱ�ȭ
    }

    
    void Update()
    {
        
    }

    void UserInfoInit()
    {
        noticsText.text = "";
        //���Ӽӿ����� ����ϴ� �����յ�
        UserDateManager.instance.InitData();
        UserDateManager.instance.RaceRecordInit();

        //���̵�, ���, uid, �г���, ĳ����, ����
        UserDateManager.instance.SetUserInfo("", "", "", "", "");
        //ũ����Ż, ����
        UserDateManager.instance.SetUserAssets(0, "");
        //���¸��̸�
        UserDateManager.instance.SetOpenMap("1-1");
        //������ ��� ����
        UserDateManager.instance.SetStoryEndingView("No");
        //���丮 �����
        UserDateManager.instance.SetStoryTalkProgress("0-1");
        //�̼ǿ� ȹ���� ũ����Ż ��
        UserDateManager.instance.MissionAcquisitionCrystal(0);
        //���丮��� OnOff
        UserDateManager.instance.SetStoryOnOff("On");
        //Map3 �÷��� Ƚ��
        UserDateManager.instance.SetDiaryOpenDay("", "", "", "");
    }

    public void Account_Make()
    {
        pw_Str = pw_Field.text;
        Check_Password(pw_Str); //��й�ȣ üũ
        id_Str = email_Field.text;
        Check_Id(id_Str);   //�̸��� ���̵� üũ

        StartCoroutine(_Account_Make());
    }

    IEnumerator _Account_Make()
    {
        yield return null;

        //�������� ���̵� �˻��ؼ� ���;���. ���̵� ������ ���
        if(serverIDState.Equals(true))
        {
            //noticsPopup.SetActive(true);
            noticsText.text = "�����ϴ� ���̵��Դϴ�." + "\n" + "�ٽ� �Է����ּ���.";
        }
        else
        {
            //�̸����� �ùٸ��� ����
            if(isEmailFormat.Equals(false))
            {
                //noticsPopup.SetActive(true);
                noticsText.text = "�ùٸ��� ���� �̸����Դϴ�." + "\n" + "�ٽ� �Է����ּ���.";
            }
            //�н����尡 �������� ����
            else if(pwSame.Equals(false))
            {
                //noticsPopup.SetActive(true);
                noticsText.text = "��й�ȣ�� �������� �ʽ��ϴ�." + "\n" + "�ٽ� �Է����ּ���.";
            }
            //��й�ȣ�� 9�ڸ� �̻����� ���� ��
            else if(pwNine.Equals(false))
            {
                //noticsPopup.SetActive(true);
                noticsText.text = "��й�ȣ�� 9�ڸ� �̻��� ���� �ʽ��ϴ�." + "\n" + "�ٽ� �Է����ּ���.";
            }
            //��й�ȣ�� Ư�����ڰ� �������� ������
            else if(pwSpecial.Equals(false))
            {
                //noticsPopup.SetActive(true);
                noticsText.text = "Ư�����ڿ� ����, ��� �Բ� ����Ͻñ� �ٶ��ϴ�.";
            }
            else if(isEmailFormat.Equals(true) && pwSame.Equals(true) && pwNine.Equals(true) && pwSpecial.Equals(true))
            {
                UserInfoSave(); //�⺻���� �������� ����
                StartCoroutine(LobbyMove());
                //StartCoroutine(FindPWEmailMove());
            }
        }
    }

    //�̸��� ��ȿ���˻�
    public void Email_Effectiveness()
    {
        Check_Id(pwEmail_Field.text);   //�̸��� ���̵� üũ
        StartCoroutine(_Email_Effectiveness());
    }

    IEnumerator _Email_Effectiveness()
    {
        yield return null;

        if(isEmailFormat.Equals(false))
            emailEffectivenessText.text = "�ùٸ��� ���� �̸����Դϴ�." + "\n" + "�ٽ� �Է����ּ���.";
        else
        {
            UserInfoSave(); //�⺻���� �������� ����
            StartCoroutine(LobbyMove());
        }

    }

    //�̸���ã�� ���� �ѹ� �� ����
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


    //�ùٸ� �̸������� üũ
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

        //Debug.Log("�̸��� üũ : " + isEmailFormat);
        return isEmailFormat;
    }

    //���������� ���� �Լ�
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

    string UIDMake()
    {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        int currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
        int z1 = UnityEngine.Random.Range(0, 1000000);
        string number = String.Format("%07d", z1);
        string uid = currentEpochTime + "01" + z1;
        return uid;
    }

    //�������� ���� �Լ�
    void UserInfoSave()
    {
        string userUID = UIDMake();

        //���̵�, ���, uid, �г���, ĳ����, ����
        UserDateManager.instance.SetUserInfo(email_Field.text, pw_Field.text, pwEmail_Field.text,
            userUID, "Gateways");
        UserDateManager.instance.SetUserAssets(0, "���� ȣ������");
        UserDateManager.instance.SetOpenMap("1-1");
        UserDateManager.instance.SetStoryEndingView("No");
        UserDateManager.instance.SetStoryTalkProgress("0-1");
        UserDateManager.instance.MissionAcquisitionCrystal(0);
        UserDateManager.instance.SetStoryOnOff("On");
        UserDateManager.instance.SetSettingToggle("On", "On");
        UserDateManager.instance.SetDiaryOpenDay("", "", "", "");
        //PlayerPrefs.SetString("KS_UserLoginState", "GatewaysNickName");  //������:�г��� ������ �����Ѵ�.
        //PlayerPrefs.SetString("KS_UserID", email_Field.text);
        //PlayerPrefs.SetString("KS_UserUID", userUID);
        //PlayerPrefs.SetString("KS_UserPassWord", pw_Field.text);
    }

    //�������� ���� ��üũ�� ����
    public void AgreementNoCheck()
    {
        checkToggle.isOn = false;
    }

    //��ư Ŭ�� �� InputField�� �ִ� �ؽ�Ʈ �����
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
