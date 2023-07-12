using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryChapter_UIManager : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI crystalText;
    public Toggle storyModeToggle;  //스토리모드 선택 토글
    public Image[] chapterBtnImg;
    public Sprite clickSprite;
    public Sprite baseSprite;


    public GameObject settingCanvas;    //세팅팝업 캔버스
    public GameObject setupPenel;   //셋업팡업
    public GameObject appClosePopup;    //앱 종료 팝업


    void Start()
    {
        RewardTextInit();
        //AudioManager.Instance.PlayMusic("Lobby");

        //프리팹에 따른 토글 On/Off상태 설정
        if (PlayerPrefs.GetString("KS_StoryOnOff").Equals("On"))
            storyModeToggle.isOn = true;
        else
            storyModeToggle.isOn = false;
    }

    void RewardTextInit()
    {
        goldText.text = UserDateManager.instance.CommaText(PlayerPrefs.GetInt("KS_Gold"));
        crystalText.text = UserDateManager.instance.CommaText(PlayerPrefs.GetInt("KS_Crystal"));
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingCanvas.SetActive(true);
            setupPenel.SetActive(false);
            appClosePopup.SetActive(true);
        }
#endif
    }

    public void StoryModeToggle()
    {
        if (storyModeToggle.isOn.Equals(true))
        {
            Debug.Log("스토리 On");
            UserDateManager.instance.SetStoryOnOff("On");
        }
        else
        {
            Debug.Log("스토리 Off");
            UserDateManager.instance.SetStoryOnOff("Off");
        }
    }

    public void Chapter1_1ClickOn()
    {
        if(SceneManager.GetActiveScene().name.Equals("4_1_1.Story1"))
        {
            chapterBtnImg[0].sprite = clickSprite;
            chapterBtnImg[1].sprite = baseSprite;
            chapterBtnImg[2].sprite = baseSprite;
            chapterBtnImg[3].sprite = baseSprite;
        }
        else if(SceneManager.GetActiveScene().name.Equals("4_1_1.Story2"))
        {
            chapterBtnImg[0].sprite = clickSprite;
            chapterBtnImg[1].sprite = baseSprite;
            chapterBtnImg[2].sprite = baseSprite;
        }
        else if(SceneManager.GetActiveScene().name.Equals("4_1_1.Story3"))
        {
            chapterBtnImg[0].sprite = clickSprite;
        }
    }

    public void Chapter1_2ClickOn()
    {
        if (SceneManager.GetActiveScene().name.Equals("4_1_1.Story1"))
        {
            chapterBtnImg[1].sprite = clickSprite;
            chapterBtnImg[0].sprite = baseSprite;
            chapterBtnImg[2].sprite = baseSprite;
            chapterBtnImg[3].sprite = baseSprite;
        }
        else if (SceneManager.GetActiveScene().name.Equals("4_1_1.Story2"))
        {
            chapterBtnImg[1].sprite = clickSprite;
            chapterBtnImg[0].sprite = baseSprite;
            chapterBtnImg[2].sprite = baseSprite;
        }
    }

    public void Chapter1_3ClickOn()
    {
        if (SceneManager.GetActiveScene().name.Equals("4_1_1.Story1"))
        {
            chapterBtnImg[2].sprite = clickSprite;
            chapterBtnImg[0].sprite = baseSprite;
            chapterBtnImg[1].sprite = baseSprite;
            chapterBtnImg[3].sprite = baseSprite;
        }
        else if (SceneManager.GetActiveScene().name.Equals("4_1_1.Story2"))
        {
            chapterBtnImg[2].sprite = clickSprite;
            chapterBtnImg[0].sprite = baseSprite;
            chapterBtnImg[1].sprite = baseSprite;
        }
    }

    public void Chapter1_4ClickOn()
    {
        if (SceneManager.GetActiveScene().name.Equals("4_1_1.Story1"))
        {
            chapterBtnImg[3].sprite = clickSprite;
            chapterBtnImg[0].sprite = baseSprite;
            chapterBtnImg[1].sprite = baseSprite;
            chapterBtnImg[2].sprite = baseSprite;
        }
        else if (SceneManager.GetActiveScene().name.Equals("4_1_1.Story2"))
        {

        }
    }
}
