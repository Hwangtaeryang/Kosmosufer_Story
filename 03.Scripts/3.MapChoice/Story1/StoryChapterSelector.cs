using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryChapterSelector : MonoBehaviour
{
    List<Dictionary<string, object>> data;


    [Header("Prefabs")]
    public GameObject panelPrefab;
    [Space(10)]
    public Transform slideContent;


    GameObject objCopy;
    int j = 0;
    string chapterStr;  //챕터 이름 매칭시키기 위함
    

    void Start()
    {
        data = CSVReader.Read("HoverBoardStory");

        //
        //Debug.Log("11111111111   " + PlayerPrefs.GetString("KS_StoryNumber"));

        if (PlayerPrefs.GetString("KS_StoryNumber").Equals("Story1"))
        {
            // Adding level panels  레벨 패널 추가
            //foreach (StroyLevel lev in StoryChapterChoiceManager.instance.levels)
            foreach (Level lev in GameManager.Instance.storylevels_1)
            //for(int i = 3; i < GameManager.Instance.levels.Length; i++)
            {
                //Instantiate(panelPrefab, slideContent).GetComponent<LevelPanel>().SetLevel(lev);
                objCopy = Instantiate(panelPrefab, slideContent);
                objCopy.GetComponent<LevelPanel>().SetLevel(lev);
                objCopy.name = "Chapter" + ++j;

                if (objCopy.name.Equals("Chapter1"))
                    chapterStr = "1-1";
                else if (objCopy.name.Equals("Chapter2"))
                    chapterStr = "1-2";
                else if (objCopy.name.Equals("Chapter3"))
                    chapterStr = "1-3";
                else if (objCopy.name.Equals("Chapter4"))
                    chapterStr = "1-4";
                else if (objCopy.name.Equals("Chapter5"))
                    chapterStr = "1-5";


                for (int k = 0; k < data.Count; k++)
                {
                    if (data[k]["Number"].ToString().Equals(chapterStr))
                    {
                        if(chapterStr.Equals("1-1"))
                            objCopy.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = 
                                Resources.Load<Sprite>("ChapterImg/playing_map line1");
                        else
                            objCopy.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = 
                                Resources.Load<Sprite>("ChapterImg/playing_map line2");
                        objCopy.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("ChapterImg/story1_map_panal");
                        objCopy.transform.GetChild(1).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = 
                            data[k]["Chapter"].ToString();
                        objCopy.transform.GetChild(1).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text =
                            data[k]["ChapterInfo"].ToString();
                        objCopy.transform.GetChild(1).transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = 
                            data[k]["NPCNumber"].ToString();
                        objCopy.transform.GetChild(1).transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = 
                            data[k]["Mission"].ToString();
                        objCopy.transform.GetChild(1).transform.GetChild(7).GetComponent<TextMeshProUGUI>().text =
                            data[k]["EntryFee"].ToString();
                        objCopy.transform.GetChild(1).transform.GetChild(6).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                            UserDateManager.instance.CommaText(int.Parse(data[k]["AllCashPrize"].ToString()));
                        objCopy.transform.GetChild(1).transform.GetChild(6).transform.GetChild(2).gameObject.SetActive(true);   //보상알림판 활성화
                        objCopy.transform.GetChild(1).transform.GetChild(6).transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                            "1등 상금 : " + UserDateManager.instance.CommaText(int.Parse(data[k]["CashPrize1"].ToString())) + "\n" +
                            "2등 상금 : " + UserDateManager.instance.CommaText(int.Parse(data[k]["CashPrize2"].ToString())) + "\n" +
                            "3등 상금 : " + UserDateManager.instance.CommaText(int.Parse(data[k]["CashPrize3"].ToString()));
                        objCopy.transform.GetChild(1).transform.GetChild(6).transform.GetChild(2).gameObject.SetActive(false);   //보상알림판 비활성화
                        objCopy.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("ChapterImg/" + data[k]["ChapterImage"].ToString());
                        
                        if(chapterStr.Equals("1-1"))
                            objCopy.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = 
                            data[k]["PopupMission"].ToString();
                        else if(chapterStr.Equals("1-2"))
                            objCopy.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                            "<color=#FF0000>"+(100 - PlayerPrefs.GetInt("KS_AcquisitionCrystal")) + "개</color> " + data[k]["PopupMission"].ToString();
                        else if(chapterStr.Equals("1-3"))
                            objCopy.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                            "<color=#FF0000>" + (300 - PlayerPrefs.GetInt("KS_AcquisitionCrystal")) + "개</color> " + data[k]["PopupMission"].ToString();
                        else if (chapterStr.Equals("1-4"))
                            objCopy.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                            "<color=#FF0000>" + (500 - PlayerPrefs.GetInt("KS_AcquisitionCrystal")) + "개</color> " + data[k]["PopupMission"].ToString();
                    }
                }
                //objCopy.SetActive(false);
            }
            Debug.Log("---- " + PlayerPrefs.GetString("KS_OpenMap"));
            //미션해결햇기때문에 미션팝업 안나오게 함
            if(PlayerPrefs.GetString("KS_OpenMap").Equals("1-2"))
            {
                slideContent.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
            }
            else if(PlayerPrefs.GetString("KS_OpenMap").Equals("1-3"))
            {
                for(int i = 0; i < 2; i++)
                    slideContent.transform.GetChild(i).transform.GetChild(3).gameObject.SetActive(false);
            }
            else if (PlayerPrefs.GetString("KS_OpenMap").Equals("1-4") || PlayerPrefs.GetString("KS_OpenMap").Equals("1-4_2") ||
                PlayerPrefs.GetString("KS_OpenMap").Equals("1-4_3"))
            {
                for (int i = 0; i < 3; i++)
                    slideContent.transform.GetChild(i).transform.GetChild(3).gameObject.SetActive(false);
            }
            else if (PlayerPrefs.GetString("KS_OpenMap").Equals("2-1") || PlayerPrefs.GetString("KS_OpenMap").Equals("2-2") ||
                PlayerPrefs.GetString("KS_OpenMap").Equals("2-3") || PlayerPrefs.GetString("KS_OpenMap").Equals("3-1") ||
                PlayerPrefs.GetString("KS_OpenMap").Equals("StoryEND"))
            {
                for (int i = 0; i < 3; i++)
                    slideContent.transform.GetChild(i).transform.GetChild(3).gameObject.SetActive(false);
            }

            //맵 자세한 정보 팝업 전부 비활성화
            for (int i = 0; i < slideContent.childCount; i++)
                slideContent.transform.GetChild(i).gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetString("KS_StoryNumber").Equals("Story2"))
        {
            foreach (Level lev in GameManager.Instance.storylevels_2)
            {
                objCopy = Instantiate(panelPrefab, slideContent);
                objCopy.GetComponent<LevelPanel>().SetLevel(lev);
                objCopy.name = "Chapter" + ++j;

                if (objCopy.name.Equals("Chapter1"))
                    chapterStr = "2-1";
                else if (objCopy.name.Equals("Chapter2"))
                    chapterStr = "2-2";
                else if (objCopy.name.Equals("Chapter3"))
                    chapterStr = "2-3";
                else if (objCopy.name.Equals("Chapter4"))
                    chapterStr = "2-4";
                else if (objCopy.name.Equals("Chapter5"))
                    chapterStr = "2-5";


                for (int k = 0; k < data.Count; k++)
                {
                    if (data[k]["Number"].ToString().Equals(chapterStr))
                    {
                        objCopy.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = 
                            Resources.Load<Sprite>("ChapterImg/playing_map line3");
                        objCopy.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("ChapterImg/story2_map_panal");
                        objCopy.transform.GetChild(1).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
                            data[k]["Chapter"].ToString();
                        objCopy.transform.GetChild(1).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text =
                            data[k]["ChapterInfo"].ToString();
                        objCopy.transform.GetChild(1).transform.GetChild(4).GetComponent<TextMeshProUGUI>().text =
                            data[k]["NPCNumber"].ToString();
                        objCopy.transform.GetChild(1).transform.GetChild(5).GetComponent<TextMeshProUGUI>().text =
                            data[k]["Mission"].ToString();
                        objCopy.transform.GetChild(1).transform.GetChild(7).GetComponent<TextMeshProUGUI>().text =
                            data[k]["EntryFee"].ToString();
                        objCopy.transform.GetChild(1).transform.GetChild(6).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                            UserDateManager.instance.CommaText(int.Parse(data[k]["AllCashPrize"].ToString()));
                        objCopy.transform.GetChild(1).transform.GetChild(6).transform.GetChild(2).gameObject.SetActive(true);   //보상알림판 활성화
                        objCopy.transform.GetChild(1).transform.GetChild(6).transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                            "1등 상금 : " + UserDateManager.instance.CommaText(int.Parse(data[k]["CashPrize1"].ToString())) + "\n" +
                            "2등 상금 : " + UserDateManager.instance.CommaText(int.Parse(data[k]["CashPrize2"].ToString())) + "\n" +
                            "3등 상금 : " + UserDateManager.instance.CommaText(int.Parse(data[k]["CashPrize3"].ToString())) + "\n" +
                            "4등 상금 : " + UserDateManager.instance.CommaText(int.Parse(data[k]["CashPrize4"].ToString()));
                        objCopy.transform.GetChild(1).transform.GetChild(6).transform.GetChild(2).gameObject.SetActive(false);   //보상알림판 비활성화
                        objCopy.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("ChapterImg/" + data[k]["ChapterImage"].ToString());
                        objCopy.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                            data[k]["PopupMission"].ToString();
                    }
                }
                //objCopy.SetActive(false);
            }

            //미션해결햇기때문에 미션팝업 안나오게 함
            if (PlayerPrefs.GetString("KS_OpenMap").Equals("2-2"))
            {
                slideContent.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
            }
            else if (PlayerPrefs.GetString("KS_OpenMap").Equals("2-3"))
            {
                for (int i = 0; i < 2; i++)
                    slideContent.transform.GetChild(i).transform.GetChild(3).gameObject.SetActive(false);
            }
            //else if (PlayerPrefs.GetString("KS_OpenMap").Equals("2-4"))
            //{
            //    for (int i = 0; i < 3; i++)
            //        slideContent.transform.GetChild(i).transform.GetChild(3).gameObject.SetActive(false);
            //}
            //else if (PlayerPrefs.GetString("KS_OpenMap").Equals("2-5"))
            //{
            //    for (int i = 0; i < 4; i++)
            //        slideContent.transform.GetChild(i).transform.GetChild(3).gameObject.SetActive(false);
            //}
            else if(PlayerPrefs.GetString("KS_OpenMap").Equals("3-1") || PlayerPrefs.GetString("KS_OpenMap").Equals("StoryEND"))
            {
                for (int i = 0; i < 3; i++)
                    slideContent.transform.GetChild(i).transform.GetChild(3).gameObject.SetActive(false);
            }

            //맵 자세한 정보 팝업 전부 비활성화
            for (int i = 0; i < slideContent.childCount; i++)
                slideContent.transform.GetChild(i).gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetString("KS_StoryNumber").Equals("Story3"))
        {
            foreach (Level lev in GameManager.Instance.storylevels_3)
            {
                objCopy = Instantiate(panelPrefab, slideContent);
                objCopy.GetComponent<LevelPanel>().SetLevel(lev);
                objCopy.name = "Chapter" + ++j;

                if (objCopy.name.Equals("Chapter1"))
                    chapterStr = "3-1";
                else if (objCopy.name.Equals("Chapter2"))
                    chapterStr = "3-2";
                else if (objCopy.name.Equals("Chapter3"))
                    chapterStr = "3-3";
                else if (objCopy.name.Equals("Chapter4"))
                    chapterStr = "3-4";
                else if (objCopy.name.Equals("Chapter5"))
                    chapterStr = "3-5";


                for (int k = 0; k < data.Count; k++)
                {
                    if (data[k]["Number"].ToString().Equals(chapterStr))
                    {
                        objCopy.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = 
                            Resources.Load<Sprite>("ChapterImg/playing_map line4");
                        objCopy.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("ChapterImg/story3_map_panal");
                        objCopy.transform.GetChild(1).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
                            data[k]["Chapter"].ToString();
                        objCopy.transform.GetChild(1).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text =
                            data[k]["ChapterInfo"].ToString();
                        objCopy.transform.GetChild(1).transform.GetChild(4).GetComponent<TextMeshProUGUI>().text =
                            data[k]["NPCNumber"].ToString();
                        objCopy.transform.GetChild(1).transform.GetChild(5).GetComponent<TextMeshProUGUI>().text =
                            data[k]["Mission"].ToString();
                        objCopy.transform.GetChild(1).transform.GetChild(7).GetComponent<TextMeshProUGUI>().text =
                            data[k]["EntryFee"].ToString();
                        objCopy.transform.GetChild(1).transform.GetChild(6).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                            UserDateManager.instance.CommaText(int.Parse(data[k]["AllCashPrize"].ToString()));
                        objCopy.transform.GetChild(1).transform.GetChild(6).transform.GetChild(2).gameObject.SetActive(true);   //보상알림판 활성화
                        objCopy.transform.GetChild(1).transform.GetChild(6).transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                            "1등 상금 : " + UserDateManager.instance.CommaText(int.Parse(data[k]["CashPrize1"].ToString())) + "\n" +
                            "2등 상금 : " + UserDateManager.instance.CommaText(int.Parse(data[k]["CashPrize2"].ToString())) + "\n" +
                            "3등 상금 : " + UserDateManager.instance.CommaText(int.Parse(data[k]["CashPrize3"].ToString())) + "\n" +
                            "4등 상금 : " + UserDateManager.instance.CommaText(int.Parse(data[k]["CashPrize4"].ToString()));

                        objCopy.transform.GetChild(1).transform.GetChild(6).transform.GetChild(2).gameObject.SetActive(false);   //보상알림판 비활성화
                        objCopy.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("ChapterImg/" + data[k]["ChapterImage"].ToString());
                        objCopy.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                            "<color=#FF0000>" + (50000 - PlayerPrefs.GetInt("KS_AcquisitionCrystal")) + "개</color> " + data[k]["PopupMission"].ToString();
                    }
                }
                //objCopy.SetActive(false);
            }

            //미션해결햇기때문에 미션팝업 안나오게 함
            if (PlayerPrefs.GetString("KS_OpenMap").Equals("StoryEND"))
            {
                slideContent.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
            }
            //else if (PlayerPrefs.GetString("KS_OpenMap").Equals("3-2"))
            //{
            //    slideContent.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
            //}
            //else if (PlayerPrefs.GetString("KS_OpenMap").Equals("3-3"))
            //{
            //    for (int i = 0; i < 2; i++)
            //        slideContent.transform.GetChild(i).transform.GetChild(3).gameObject.SetActive(false);
            //}
            //else if (PlayerPrefs.GetString("KS_OpenMap").Equals("3-4"))
            //{
            //    for (int i = 0; i < 3; i++)
            //        slideContent.transform.GetChild(i).transform.GetChild(3).gameObject.SetActive(false);
            //}
            //else if (PlayerPrefs.GetString("KS_OpenMap").Equals("3-5"))
            //{
            //    for (int i = 0; i < 4; i++)
            //        slideContent.transform.GetChild(i).transform.GetChild(3).gameObject.SetActive(false);
            //}

            //맵 자세한 정보 팝업 전부 비활성화
            for (int i = 0; i < slideContent.childCount; i++)
                slideContent.transform.GetChild(i).gameObject.SetActive(false);
        }
    }


}
