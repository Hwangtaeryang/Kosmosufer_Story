using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    static public Tutorial instance { get; private set; }

    public Image mainTutorialImg;
    public Sprite[] tutorialSprite;
    public GameObject endPopup;
    public RaceManager raceManager_S;

    public bool tutorialEnd;

    //GameObject joyStick;

    int clickCount = 0;

    

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        TalkBoxList.instance.JoPadFind_and_State(false);
        mainTutorialImg.sprite = tutorialSprite[0];
    }

    public void TutorialStart()
    {
        TalkBoxList.instance.JoPadFind_and_State(true);
    }


    public void TutorialButton()
    {
        if(clickCount.Equals(0))//두번째 이미지
        {
            mainTutorialImg.sprite = tutorialSprite[1];
            clickCount = 1;
        }
        else if(clickCount.Equals(1))   //세번째 이미지
        {
            mainTutorialImg.sprite = tutorialSprite[2];
            clickCount = 2;
        }
        else if(clickCount.Equals(2))   //네번째 이미지
        {
            mainTutorialImg.sprite = tutorialSprite[3];
            clickCount = 3;
        }
        else if(clickCount.Equals(3))   //5
        {
            mainTutorialImg.sprite = tutorialSprite[4];
            clickCount = 4;
        }
        else if (clickCount.Equals(4))   //6
        {
            mainTutorialImg.sprite = tutorialSprite[5];
            clickCount = 5;
        }
        else if (clickCount.Equals(5))   //7
        {
            mainTutorialImg.sprite = tutorialSprite[6];
            clickCount = 6;
        }
        else if (clickCount.Equals(6))   //8
        {
            mainTutorialImg.sprite = tutorialSprite[7];
            clickCount = 7;
        }
        else if(clickCount.Equals(7))   //다시 처음으로
        {
            mainTutorialImg.gameObject.SetActive(false);
            clickCount = 0;
            tutorialEnd = true;
            endPopup.SetActive(true);
        }
    }

    public void CountDwonStart()
    {
        //joyStick.SetActive(true);
        raceManager_S.CountDownStart();
        //UserDateManager.instance.SetTutorial("Yes");
    }
}
