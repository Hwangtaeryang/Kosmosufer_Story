using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Mail;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using TMPro;

public class PassWordFind : MonoBehaviour
{
    public InputField email_Fidle;

    //public GameObject noticsPopup;
    public TextMeshProUGUI noticsText;

    string PASSWORD_CHARS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"; //������ ��й�ȣ �����ϱ� ���� ���ڵ�




    
    //��й�ȣ ã�� ��ư
    public void PassWordFindButton()
    {
        StartCoroutine(_PassWordFindButton());
    }

    IEnumerator _PassWordFindButton()
    {
        yield return null;

        string pw = GeneratePaseeword(7);   //������ ��й�ȣ

        if (email_Fidle.text.Equals(""))
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("gateways2021dev@gmail.com");   //�����ּ� �������� ������
            mail.To.Add(email_Fidle.text); ;//�޴»��
            mail.Subject = "������ ��й�ȣ�� �����帳�ϴ�." + "\n" + 
                "���ȣ('[]')�ȿ� �ִ� ��й�ȣ�� ���ӿ� �������ּ���." + "\n" +
                "���ӿ� �����Ͻ� �� ������������ ��й�ȣ�� �� �������ּ���." + "\n\n\n" +
                "[ " + pw + " ]"; //������ ��й�ȣ �ֱ�

            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential("gateways202dev@gmail.com", 
                "gw2021!!") as ICredentialsByHost;  //�����»��, ��й�ȣ
            smtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };

            smtpServer.Send(mail);

            //noticsPopup.SetActive(true);
            noticsText.text = "<color=#25a340>" +"������ ���������� �߼۵Ǿ����ϴ�." + "\n" + 
                "������ Ȯ�����ֽñ� �ٶ��ϴ�." + "</color>";
            email_Fidle.text = "";
        }
        else
        {
            //noticsPopup.SetActive(true);
            noticsText.text = "<color=#df1f1e>" + "������ ����(���̵�)�� �����ϴ�." + "\n" +
                "�ٽ� �ѹ� Ȯ�� �� �̿����ֽñ� �ٶ��ϴ�." + "</color>";
        }
    }

    //�ӽ� ��й�ȣ ���� �Լ�
    public string GeneratePaseeword(int _length)
    {
        var sb = new System.Text.StringBuilder(_length);
        var r = new System.Random();

        for (int i = 0; i < _length; i++)
        {
            int pos = r.Next(PASSWORD_CHARS.Length);
            char c = PASSWORD_CHARS[pos];
            sb.Append(c);
        }

        return sb.ToString();
    }

    //��ư Ŭ�� �� InputField�� �ִ� �ؽ�Ʈ �����
    public void DeleteInputField(InputField _inputfield)
    {
        _inputfield.text = "";
    }
}
