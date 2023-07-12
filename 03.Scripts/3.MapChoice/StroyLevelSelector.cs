using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StroyLevelSelector : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject panelPrefab;
    [Space(10)]
    public Transform slideContent;

    GameObject objCopy;
    int i = 0;


    void Start()
    {
        // Adding level panels  레벨 패널 추가
        foreach (StroyLevel lev in MapStroy_UIManager.instance.levels)
        {
            //Instantiate(panelPrefab, slideContent).GetComponent<LevelPanel>().SetLevel(lev);
            objCopy = Instantiate(panelPrefab, slideContent);
            objCopy.GetComponent<LevelPanel>().SetLevel(lev);
            objCopy.name = "Story" + ++i;
            objCopy.GetComponent<Button>().interactable = false;
        }

        //스탭이 다 끝났으면 다음 스토리 오픈 조건식
        if (PlayerPrefs.GetString("KS_OpenMap").Equals("1-1") || PlayerPrefs.GetString("KS_OpenMap").Equals("1-2") ||
                PlayerPrefs.GetString("KS_OpenMap").Equals("1-3") || PlayerPrefs.GetString("KS_OpenMap").Equals("1-4") ||
                PlayerPrefs.GetString("KS_OpenMap").Equals("1-5"))
        {
            slideContent.transform.GetChild(0).gameObject.GetComponent<Button>().interactable = true;
            slideContent.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetString("KS_OpenMap").Equals("2-1") || PlayerPrefs.GetString("KS_OpenMap").Equals("2-2") ||
                PlayerPrefs.GetString("KS_OpenMap").Equals("2-3") || PlayerPrefs.GetString("KS_OpenMap").Equals("2-4") ||
                PlayerPrefs.GetString("KS_OpenMap").Equals("2-5"))
        {
            slideContent.transform.GetChild(0).gameObject.GetComponent<Button>().interactable = true;
            slideContent.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);

            slideContent.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = true;
            slideContent.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
        }
    }

}
