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

    string specialStr;  //Ư������
    string nicknameStr;    //�г��Ӻ���

    bool specialState;  //Ư������ ��� ����
    bool overlapState;  //�ߺ����� ����
    bool overlapOnBtn;  //�ߺ�üũ ��ư Ŭ�� ����
    bool nullState; //�г��� �� üũ ����
    bool curseState;    //���� ����
    
    string loginState;  //�α��� ����


    //�׽�Ʈ�� ���� �س��� �ߺ� - ���߿� �����
    bool serverOverlap;

    void Start()
    {
        nicknameStr = "";
        specialState = true;    //�ʱⰪ true�� Ư������ ����ߴٴ°�
        curseState = true;  //�ʱⰪ true�� �� ����ߴٴ� ��
        nullState = true;
    }

    
    public void NickName_OverlapCheck()
    {
        StartCoroutine(_NickName_OverlapCheck());
    }

    IEnumerator _NickName_OverlapCheck()
    {
        yield return null;

        overlapOnBtn = true;    //�ߺ�üũ ����(üũ��)
        nicknameStr = nickname_field.text;
        nicknameStr = nicknameStr.Replace(" ", "");   //��������

        //�г����� ���� ��
        if(nicknameStr.Equals(""))
        {
            nullState = true;
        }
        else
        {
            nullState = false;
            specialState = Special_Character_Check(nicknameStr);    //Ư�����ڻ�뿩��
            curseState = HangeulCurseCheck(nicknameStr);    //�弳��뿩��

            //Ư������ �Ⱦ���, �弳�� ����
            if(specialState.Equals(false) && curseState.Equals(false))
            {
                //�������� Ȯ���ؾ��� - ������ ����
                if(serverOverlap.Equals(true))
                {
                    overlapState = true;    //�г��� �ߺ�
                }
                else
                {
                    overlapState = false;   //�г��� �ߺ� �ƴ�
                }
            }
        }

        if(nullState.Equals(false) && overlapState.Equals(false) && specialState.Equals(false) && curseState.Equals(false))
        {
            overlapText.text = "��밡���� �г����Դϴ�.";
        }
        //�г����� ���� �ڰ�
        else if(nullState.Equals(true))
        {
            overlapText.text = "�г����� �Է��ϼ���";
        }
        //�ߺ��г��� �϶�
        else if(overlapState.Equals(true))
        {
            overlapText.text = "�г��� �ߺ��Դϴ�.";
        }
        //Ư������ �������� ��
        else if(specialState.Equals(true))
        {
            overlapText.text = "Ư�����ڸ� ����߽��ϴ�. �ٽ� �Է����ּ���.";
        }
        //�弳 ������� ��
        else if(curseState.Equals(true))
        {
            overlapText.text = "��Ӿ ����߽��ϴ�. �ٽ� �Է����ּ���.";
        }
        else
        {
            overlapText.text = "�г����� �ٽ� �����Ͽ� �ֽʽÿ�";
        }
    }

    //�г��� Ư������ ��� ���� Ȯ�� �Լ�
    bool Special_Character_Check(string _nickname)
    {
        specialStr = @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]";

        Regex regex = new Regex(specialStr);

        //Debug.Log(regex.IsMatch(_nickname));
        //True�� ������ Ư�����ڸ� ����Ѵ�
        specialState = regex.IsMatch(_nickname);
        return regex.IsMatch(_nickname);
    }

    //�ѱۿ�˻�
    bool HangeulCurseCheck(string _nickname)
    {
        data = CSVReader.Read("Swearlist");

        bool isCheck = false;

        for (int i = 0; i < data.Count; i++)
        {
            //isCheck = data[i]["��"].ToString().Contains(_nickname);
            isCheck = _nickname.Contains(data[i]["��"].ToString());
            if (isCheck.Equals(true))
                return isCheck;
        }

        return isCheck;
    }

    //�г��� ����
    public void NickNameSavaButton()
    {
        //�˻��ߴ� �г����̶� ������ Ȯ��
        if(nicknameStr.Equals(nickname_field.text) && nicknameStr != "")
        {
            //�ߺ�üũ ��ư ������ ��
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

                    //���̵�, ���, uid, ����
                    UserDateManager.instance.SetUserInfo(PlayerPrefs.GetString("KS_UserID"),
                        PlayerPrefs.GetString("KS_UserPassWord"),  PlayerPrefs.GetString("KS_UserPWFindEail"), PlayerPrefs.GetString("KS_UserUID")
                        , "GatewaysCharacter");

                    //������ �г��� ���� ��Ű��


                    SceneManager.LoadScene("2.CharacterChoiceBoot");
                }
                else
                {
                    overlapOnBtn = false;
                    overlapText.text = "�г����� �ٽ� �����Ͽ� �ֽʽÿ�";
                }
            }
            else
            {
                overlapOnBtn = false;
                overlapText.text = "�г��� �ߺ� üũ�� ���ֽñ� �ٶ��ϴ�.";
            }
        }
        else if(nickname_field.text.Equals(""))
        {
            overlapOnBtn = false;    //�ߺ�üũ ����(üũ����)
            overlapText.text = "�г����� �����Ͽ� �ֽʽÿ�.";
        }
        else if (overlapOnBtn.Equals(false))
        {
            overlapOnBtn = false;    //�ߺ�üũ ����(üũ����)
            overlapText.text = "�г��� �ߺ� üũ�� ���ֽñ� �ٶ��ϴ�.";
        }
    }
}
