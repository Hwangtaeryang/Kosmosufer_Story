using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Diary_UIManager : MonoBehaviour
{
    public TextMeshProUGUI crystalText;
    public GameObject[] page;   //�ϱ� ������
    public TextMeshProUGUI[] openDay;   //�ϱ� ���µ� ��¥
    public Button[] pageBtn;    //�¿� ��ư

    int pageIndex;  //������ ��

    public GameObject settingCanvas;    //�����˾� ĵ����
    public GameObject setupPenel;   //�¾��ξ�
    public GameObject appClosePopup;    //�� ���� �˾�


    void Start()
    {
        RewardTextInit();


        //�ν��� ������ 100
        if (PlayerPrefs.GetString("KS_OpenMap").Equals("1-3"))
        {
            page[0].SetActive(true);
            openDay[0].text = PlayerPrefs.GetString("KS_DiaryOpenDay1");
            pageBtn[0].interactable = false; pageBtn[1].interactable = false;
        }
        //������ ������ 1000
        else if (PlayerPrefs.GetString("KS_OpenMap").Equals("1-4"))
        {
            page[0].SetActive(true);
            openDay[0].text = PlayerPrefs.GetString("KS_DiaryOpenDay1");
            page[1].SetActive(true);
            openDay[1].text = PlayerPrefs.GetString("KS_DiaryOpenDay2");
            pageBtn[0].interactable = false; pageBtn[1].interactable = false;
        }
        //�̻��� Ȯ�� 500
        else if (PlayerPrefs.GetString("KS_OpenMap").Equals("2-1") || PlayerPrefs.GetString("KS_OpenMap").Equals("2-2") ||
            PlayerPrefs.GetString("KS_OpenMap").Equals("StoryEND") || PlayerPrefs.GetString("KS_OpenMap").Equals("2-3") ||
            PlayerPrefs.GetString("KS_OpenMap").Equals("3-1"))
        {
            page[0].SetActive(true);
            openDay[0].text = PlayerPrefs.GetString("KS_DiaryOpenDay1");
            page[1].SetActive(true);
            openDay[1].text = PlayerPrefs.GetString("KS_DiaryOpenDay2");
            pageBtn[0].interactable = false; pageBtn[1].interactable = true;
        }
        else
        {
            pageBtn[0].interactable = false; pageBtn[1].interactable = false;
        }

        pageIndex = 0;
        ////�Ƹ����佺Ÿ ���� Ƽ�� 50000
        //else if (PlayerPrefs.GetString("KS_OpenMap").Equals("StoryEND"))
        //{

        //}
    }

    private void Update()
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

    void RewardTextInit()
    {
        //goldText.text = UserDateManager.instance.CommaText(PlayerPrefs.GetInt("KS_Gold"));
        crystalText.text = UserDateManager.instance.CommaText(PlayerPrefs.GetInt("KS_Crystal"));
    }

    public void LeftButtonOn()
    {
        string openMap = PlayerPrefs.GetString("KS_OpenMap");

        if (pageIndex.Equals(1))
        {
            pageIndex -= 1;
        }
        else if(pageIndex.Equals(0))
        {
            pageIndex = 0;
        }

        if (pageIndex.Equals(0))
        {
            pageBtn[0].interactable = false;

            if (openMap.Equals("2-1") || openMap.Equals("2-2") || openMap.Equals("2-3") ||
                openMap.Equals("3-1") || openMap.Equals("StoryEND"))
                pageBtn[1].interactable = true;
            else
                pageBtn[1].interactable = false;

            //�ϱ��� ���� ���̱�
            if(openMap.Equals("1-3"))
            {
                for(int i = 0; i < 4; i++)
                    page[i].SetActive(false);

                page[0].SetActive(true);
                openDay[0].text = PlayerPrefs.GetString("KS_DiaryOpenDay1");
            }
            else if(openMap.Equals("1-4") || openMap.Equals("2-1") || openMap.Equals("2-2") || openMap.Equals("2-3") ||
                openMap.Equals("3-1") || openMap.Equals("StoryEND"))
            {
                for (int i = 0; i < 4; i++)
                    page[i].SetActive(false);

                page[0].SetActive(true); page[1].SetActive(true);
                openDay[0].text = PlayerPrefs.GetString("KS_DiaryOpenDay1"); 
                openDay[1].text = PlayerPrefs.GetString("KS_DiaryOpenDay2");
            }
            else
            {
                for (int i = 0; i < 4; i++)
                    page[i].SetActive(false);
            }
        }
        else if (pageIndex.Equals(1))
        {
            pageBtn[0].interactable = true; pageBtn[1].interactable = false;
        }
    }

    public void RightButtonOn()
    {
        string openMap = PlayerPrefs.GetString("KS_OpenMap");

        if (pageIndex.Equals(0))
        {
            pageIndex += 1;
        }
        else if(pageIndex.Equals(1))
        {
            pageIndex = 1;
        }

        if(pageIndex.Equals(0))
        {
            pageBtn[0].interactable = false;

            if (openMap.Equals("2-1") || openMap.Equals("2-2") || openMap.Equals("2-3") ||
                openMap.Equals("3-1") || openMap.Equals("StoryEND"))
                pageBtn[1].interactable = true;
            else
                pageBtn[1].interactable = false;

            //�ϱ��� ���� ���̱�
            if(openMap.Equals("1-3"))
            {
                for (int i = 1; i < 4; i++)
                    page[i].SetActive(false);

                page[0].SetActive(true);
                openDay[0].text = PlayerPrefs.GetString("KS_DiaryOpenDay1");
                pageBtn[0].interactable = false; pageBtn[1].interactable = false;
            }
            else if(openMap.Equals("1-4") || openMap.Equals("2-1") || openMap.Equals("2-2") || openMap.Equals("2-3") ||
                openMap.Equals("3-1") || openMap.Equals("StoryEND"))
            {
                for (int i = 0; i < 4; i++)
                    page[i].SetActive(false);

                page[0].SetActive(true);
                openDay[0].text = PlayerPrefs.GetString("KS_DiaryOpenDay1");

                page[1].SetActive(true);
                openDay[1].text = PlayerPrefs.GetString("KS_DiaryOpenDay2");

                pageBtn[0].interactable = false; pageBtn[1].interactable = true;
            }
            else
            {
                for (int i = 0; i < 4; i++)
                    page[i].SetActive(false);

                pageBtn[0].interactable = false; pageBtn[1].interactable = false;
            }
        }
        else if(pageIndex.Equals(1))
        {
            pageBtn[0].interactable = true;
            pageBtn[1].interactable = false;    //��Ȱ��ȭ

            if(openMap.Equals("2-1") || openMap.Equals("2-2") || openMap.Equals("2-3") ||
                openMap.Equals("3-1"))
            {
                for (int i = 0; i < 4; i++)
                    page[i].SetActive(false);

                page[2].SetActive(true);
                openDay[2].text = PlayerPrefs.GetString("KS_DiaryOpenDay2");
            }
            else if(openMap.Equals("StoryEND"))
            {
                for (int i = 0; i < 4; i++)
                    page[i].SetActive(false);

                page[2].SetActive(true);
                openDay[2].text = PlayerPrefs.GetString("KS_DiaryOpenDay2");

                page[3].SetActive(true);
                openDay[3].text = PlayerPrefs.GetString("KS_DiaryOpenDay3");
            }
        }
    }

    public void BackButtonOn()
    {
        SceneManager.LoadScene("3.Lobby");
    }
}
