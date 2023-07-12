using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterChoice : MonoBehaviour
{
    public void CharacterChoiceButton()
    {
        if(PlayerPrefs.GetString("KS_UserLoginState").Equals("GatewaysCharacter"))
        {
            UserDateManager.instance.SetUserInfo(PlayerPrefs.GetString("KS_UserID"),
            PlayerPrefs.GetString("KS_UserPassWord"), PlayerPrefs.GetString("KS_UserPWFindEail"), PlayerPrefs.GetString("KS_UserUID"),
            "Gateways");
        }
        else
        {
            UserDateManager.instance.SetUserInfo(PlayerPrefs.GetString("KS_UserID"),
            PlayerPrefs.GetString("KS_UserPassWord"),  PlayerPrefs.GetString("KS_UserPWFindEail") , PlayerPrefs.GetString("KS_UserUID"),
            "Google");
        }

        SceneManager.LoadScene("3.Lobby");
    }

}
