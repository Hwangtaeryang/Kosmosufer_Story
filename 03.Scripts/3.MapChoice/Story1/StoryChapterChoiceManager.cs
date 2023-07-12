using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryChapterChoiceManager : MonoBehaviour
{
    static public StoryChapterChoiceManager instance { get; private set; }

    public GameObject[] chapterObj;
    public Button[] storyChapterBtn;
    public Button[] fristChapterBtn;    //제일 안쪽에 있는 버튼 (버튼 위에 이미지가 있어서 클릭 시 이벤트 발생을 안해서 하나 더 만들었음)
    //public StroyLevel[] levels;
    //[HideInInspector] public StroyLevel selectedLevel;

    public GameObject chapterParent;    //챕터 화면이 생성되는 부모
    


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    private void Start()
    {
        LockClear();
    }


    //챕터 메인 씬으로 이동함수 
    public void ChapterViewSetActive(string _str)
    {
        PlayerPrefs.SetString("KS_ChapterNumber", _str);

        if (PlayerPrefs.GetString("KS_ChapterNumber").Equals("1-1") || PlayerPrefs.GetString("KS_ChapterNumber").Equals("2-1") ||
            PlayerPrefs.GetString("KS_ChapterNumber").Equals("3-1"))
            StartCoroutine(ChapterHaveLook(0));//chapterParent.transform.GetChild(0).gameObject.SetActive(true);
        else if (PlayerPrefs.GetString("KS_ChapterNumber").Equals("1-2") || PlayerPrefs.GetString("KS_ChapterNumber").Equals("2-2") ||
            PlayerPrefs.GetString("KS_ChapterNumber").Equals("3-2"))
            StartCoroutine(ChapterHaveLook(1));//chapterParent.transform.GetChild(1).gameObject.SetActive(true);
        else if (PlayerPrefs.GetString("KS_ChapterNumber").Equals("1-3") || PlayerPrefs.GetString("KS_ChapterNumber").Equals("2-3") ||
            PlayerPrefs.GetString("KS_ChapterNumber").Equals("3-3"))
            StartCoroutine(ChapterHaveLook(2));//chapterParent.transform.GetChild(2).gameObject.SetActive(true);
        else if (PlayerPrefs.GetString("KS_ChapterNumber").Equals("1-4") || PlayerPrefs.GetString("KS_ChapterNumber").Equals("2-4") ||
            PlayerPrefs.GetString("KS_ChapterNumber").Equals("3-4"))
            StartCoroutine(ChapterHaveLook(3));//chapterParent.transform.GetChild(3).gameObject.SetActive(true);
        else if (PlayerPrefs.GetString("KS_ChapterNumber").Equals("1-5") || PlayerPrefs.GetString("KS_ChapterNumber").Equals("2-5") ||
            PlayerPrefs.GetString("KS_ChapterNumber").Equals("3-5"))
            StartCoroutine(ChapterHaveLook(4));//chapterParent.transform.GetChild(4).gameObject.SetActive(true);

        //SceneManager.LoadScene("4_1_1.StoryMapMain");
    }

    IEnumerator ChapterHaveLook(int _index)
    {
        yield return new WaitForSeconds(0.4f);

        chapterParent.transform.GetChild(_index).gameObject.SetActive(true);
    }

    public void LockClear()
    {
        if (PlayerPrefs.GetString("KS_StoryNumber").Equals("Story1"))
        {
            //오픈맵일때 그 전 맵들까지 전부 오픈
            if (PlayerPrefs.GetString("KS_OpenMap").Equals("1-2"))
            {
                storyChapterBtn[0].interactable = true;
                storyChapterBtn[0].transform.GetChild(0).gameObject.SetActive(false);
                fristChapterBtn[0].interactable = true;

                chapterObj[0].transform.GetChild(1).gameObject.SetActive(true); //챕터번호
                chapterObj[0].transform.GetChild(2).gameObject.SetActive(true); //챕터내용
            }
            else if (PlayerPrefs.GetString("KS_OpenMap").Equals("1-3"))
            {
                for (int i = 0; i < 2; i++)
                {
                    storyChapterBtn[i].interactable = true;
                    storyChapterBtn[i].transform.GetChild(0).gameObject.SetActive(false);
                    fristChapterBtn[i].interactable = true;

                    chapterObj[i].transform.GetChild(1).gameObject.SetActive(true); //챕터번호
                    chapterObj[i].transform.GetChild(2).gameObject.SetActive(true); //챕터내용
                }
            }
            else if (PlayerPrefs.GetString("KS_OpenMap").Equals("1-4"))
            {
                for (int i = 0; i < 3; i++)
                {
                    storyChapterBtn[i].interactable = true;
                    storyChapterBtn[i].transform.GetChild(0).gameObject.SetActive(false);
                    fristChapterBtn[i].interactable = true;

                    chapterObj[i].transform.GetChild(1).gameObject.SetActive(true); //챕터번호
                    chapterObj[i].transform.GetChild(2).gameObject.SetActive(true); //챕터내용
                }
            }
            else if (PlayerPrefs.GetString("KS_OpenMap").Equals("2-1") || PlayerPrefs.GetString("KS_OpenMap").Equals("2-2") ||
                PlayerPrefs.GetString("KS_OpenMap").Equals("2-3") || PlayerPrefs.GetString("KS_OpenMap").Equals("3-1") ||
                PlayerPrefs.GetString("KS_OpenMap").Equals("StoryEND"))
            {
                for (int i = 0; i < 3; i++)
                {
                    storyChapterBtn[i].interactable = true;
                    storyChapterBtn[i].transform.GetChild(0).gameObject.SetActive(false);
                    fristChapterBtn[i].interactable = true;

                    chapterObj[i].transform.GetChild(1).gameObject.SetActive(true); //챕터번호
                    chapterObj[i].transform.GetChild(2).gameObject.SetActive(true); //챕터내용
                }
            }
        }
        else if (PlayerPrefs.GetString("KS_StoryNumber").Equals("Story2"))
        {
            //오픈맵일때 그 전 맵들까지 전부 오픈
            if (PlayerPrefs.GetString("KS_OpenMap").Equals("2-2"))
            {
                storyChapterBtn[0].interactable = true;
                storyChapterBtn[0].transform.GetChild(0).gameObject.SetActive(false);
                fristChapterBtn[0].interactable = true;

                chapterObj[0].transform.GetChild(1).gameObject.SetActive(true); //챕터번호
                chapterObj[0].transform.GetChild(2).gameObject.SetActive(true); //챕터내용
            }
            else if (PlayerPrefs.GetString("KS_OpenMap").Equals("2-3"))
            {
                for (int i = 0; i < 2; i++)
                {
                    storyChapterBtn[i].interactable = true;
                    storyChapterBtn[i].transform.GetChild(0).gameObject.SetActive(false);
                    fristChapterBtn[i].interactable = true;

                    chapterObj[i].transform.GetChild(1).gameObject.SetActive(true); //챕터번호
                    chapterObj[i].transform.GetChild(2).gameObject.SetActive(true); //챕터내용
                }
            }
            else if (PlayerPrefs.GetString("KS_OpenMap").Equals("3-1") || PlayerPrefs.GetString("KS_OpenMap").Equals("StoryEND"))
            {
                for (int i = 0; i < 2; i++)
                {
                    storyChapterBtn[i].interactable = true;
                    storyChapterBtn[i].transform.GetChild(0).gameObject.SetActive(false);
                    fristChapterBtn[i].interactable = true;

                    chapterObj[i].transform.GetChild(1).gameObject.SetActive(true); //챕터번호
                    chapterObj[i].transform.GetChild(2).gameObject.SetActive(true); //챕터내용
                }
            }
            //else if (PlayerPrefs.GetString("KS_OpenMap").Equals("2-5"))
            //{
            //    for (int i = 0; i < 4; i++)
            //    {
            //        storyChapterBtn[i].interactable = true;
            //        storyChapterBtn[i].transform.GetChild(0).gameObject.SetActive(false);
            //    }
            //}
        }
        else if (PlayerPrefs.GetString("KS_StoryNumber").Equals("Story3"))
        {
            //오픈맵일때 그 전 맵들까지 전부 오픈
            if (PlayerPrefs.GetString("KS_OpenMap").Equals("StoryEND"))
            {
                storyChapterBtn[0].interactable = true;
                storyChapterBtn[0].transform.GetChild(0).gameObject.SetActive(false);
                fristChapterBtn[0].interactable = true;

                chapterObj[0].transform.GetChild(1).gameObject.SetActive(true); //챕터번호
                chapterObj[0].transform.GetChild(2).gameObject.SetActive(true); //챕터내용
            }
            else if (PlayerPrefs.GetString("KS_OpenMap").Equals("3-3"))
            {
                for (int i = 0; i < 2; i++)
                {
                    storyChapterBtn[i].interactable = true;
                    storyChapterBtn[i].transform.GetChild(0).gameObject.SetActive(false);
                    fristChapterBtn[i].interactable = true;

                    chapterObj[i].transform.GetChild(1).gameObject.SetActive(true); //챕터번호
                    chapterObj[i].transform.GetChild(2).gameObject.SetActive(true); //챕터내용
                }
            }
        }
    }

    

    public void BackButton()
    {
        SceneManager.LoadScene("4.MapThreeStory");
    }

}
