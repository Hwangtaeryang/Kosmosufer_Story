using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditEvent : MonoBehaviour
{
    public GameObject creditObj;
    public GameObject eventObj;


    public void CreditPanelUnLook()
    {
        creditObj.SetActive(false);
        AudioManager.Instance.PlayMusic("Lobby");
        UserDateManager.instance.SetStoryEndingView("Yes");
        SceneManager.LoadScene("1.Login");
        //eventObj.SetActive(true);
    }
}
