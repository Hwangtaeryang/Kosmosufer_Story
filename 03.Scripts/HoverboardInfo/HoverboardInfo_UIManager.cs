using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoverboardInfo_UIManager : MonoBehaviour
{

    public GameObject boardObj;
    public TextMeshProUGUI crystalText;

    float rotSpeed = 50f;

    public GameObject settingCanvas;    //�����˾� ĵ����
    public GameObject setupPenel;   //�¾��ξ�
    public GameObject appClosePopup;    //�� ���� �˾�

    void Start()
    {
        //AudioManager.Instance.PlayMusic("Lobby");
        RewardTextInit();
    }


    void RewardTextInit()
    {
        //goldText.text = UserDateManager.instance.CommaText(PlayerPrefs.GetInt("KS_Gold"));
        crystalText.text = UserDateManager.instance.CommaText(PlayerPrefs.GetInt("KS_Crystal"));
    }

    void Update()
    {
        boardObj.transform.Rotate(new Vector3(0, 0, rotSpeed * Time.deltaTime));

#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingCanvas.SetActive(true);
            setupPenel.SetActive(false);
            appClosePopup.SetActive(true);
        }
#endif
    }

    public void BackButtonOn()
    {
        SceneManager.LoadScene("3.Lobby");
    }
}
