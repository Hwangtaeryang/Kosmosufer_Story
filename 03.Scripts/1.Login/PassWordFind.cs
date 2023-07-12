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

    string PASSWORD_CHARS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"; //임의의 비밀번호 생성하기 위한 문자들




    
    //비밀번호 찾기 버튼
    public void PassWordFindButton()
    {
        StartCoroutine(_PassWordFindButton());
    }

    IEnumerator _PassWordFindButton()
    {
        yield return null;

        string pw = GeneratePaseeword(7);   //임의의 비밀번호

        if (email_Fidle.text.Equals(""))
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("gateways2021dev@gmail.com");   //메일주소 서버에서 들고오기
            mail.To.Add(email_Fidle.text); ;//받는사람
            mail.Subject = "임의의 비밀번호를 보내드립니다." + "\n" + 
                "대괄호('[]')안에 있는 비밀번호로 게임에 접속해주세요." + "\n" +
                "게임에 접속하신 후 계정설정에서 비밀번호를 꼭 변경해주세요." + "\n\n\n" +
                "[ " + pw + " ]"; //임의의 비밀번호 넣기

            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential("gateways202dev@gmail.com", 
                "gw2021!!") as ICredentialsByHost;  //보내는사람, 비밀번호
            smtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };

            smtpServer.Send(mail);

            //noticsPopup.SetActive(true);
            noticsText.text = "<color=#25a340>" +"메일이 성공적으로 발송되었습니다." + "\n" + 
                "메일을 확인해주시길 바랍니다." + "</color>";
            email_Fidle.text = "";
        }
        else
        {
            //noticsPopup.SetActive(true);
            noticsText.text = "<color=#df1f1e>" + "동일한 메일(아이디)이 없습니다." + "\n" +
                "다시 한번 확인 후 이용해주시길 바랍니다." + "</color>";
        }
    }

    //임시 비밀번호 생성 함수
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

    //버튼 클릭 시 InputField에 있는 텍스트 지우기
    public void DeleteInputField(InputField _inputfield)
    {
        _inputfield.text = "";
    }
}
