using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapStroy_UIManager : MonoBehaviour
{
    static public MapStroy_UIManager instance { get; private set; }

    public StroyLevel[] levels;
    [HideInInspector] public StroyLevel selectedLevel;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI crystalText;

    public GameObject[] storyObj;   //0: story2, 1: story3, 2: story1
    public Sprite[] clickSprite;    //클릭했을때 이미지
    public Sprite[] baseSprite; //기본 이미지


    public GameObject settingCanvas;    //세팅팝업 캔버스
    public GameObject setupPenel;   //셋업팡업
    public GameObject appClosePopup;    //앱 종료 팝업

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        RewardTextInit();
        LockStateInit();
        //AudioManager.Instance.PlayMusic("Lobby");
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
        goldText.text = UserDateManager.instance.CommaText(PlayerPrefs.GetInt("KS_Gold"));
        crystalText.text = UserDateManager.instance.CommaText(PlayerPrefs.GetInt("KS_Crystal"));
    }

    void LockStateInit()
    {
        //스탭이 다 끝났으면 다음 스토리 오픈 조건식
        if (PlayerPrefs.GetString("KS_OpenMap").Equals("1-1") || PlayerPrefs.GetString("KS_OpenMap").Equals("1-2") ||
                PlayerPrefs.GetString("KS_OpenMap").Equals("1-3") || PlayerPrefs.GetString("KS_OpenMap").Equals("1-4") ||
                PlayerPrefs.GetString("KS_OpenMap").Equals("1-5"))
        {
            
        }
        else if (PlayerPrefs.GetString("KS_OpenMap").Equals("2-1") || PlayerPrefs.GetString("KS_OpenMap").Equals("2-2") ||
                PlayerPrefs.GetString("KS_OpenMap").Equals("2-3") || PlayerPrefs.GetString("KS_OpenMap").Equals("2-4") ||
                PlayerPrefs.GetString("KS_OpenMap").Equals("2-5") || PlayerPrefs.GetString("KS_OpenMap").Equals("2-1_2"))
        {
            storyObj[0].gameObject.GetComponent<Button>().interactable = true;
            storyObj[0].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetString("KS_OpenMap").Equals("3-1") || PlayerPrefs.GetString("KS_OpenMap").Equals("3-2") ||
                PlayerPrefs.GetString("KS_OpenMap").Equals("3-3") || PlayerPrefs.GetString("KS_OpenMap").Equals("StoryEND"))
        {
            storyObj[0].gameObject.GetComponent<Button>().interactable = true;
            storyObj[0].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            storyObj[1].gameObject.GetComponent<Button>().interactable = true;
            storyObj[1].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void Story1_ButtonOn()
    {
        storyObj[2].gameObject.GetComponent<Image>().sprite = clickSprite[0];
        storyObj[0].gameObject.GetComponent<Image>().sprite = baseSprite[1];
        storyObj[1].gameObject.GetComponent<Image>().sprite = baseSprite[2];
    }

    public void Story2_ButtonOn()
    {
        storyObj[0].gameObject.GetComponent<Image>().sprite = clickSprite[1];
        storyObj[2].gameObject.GetComponent<Image>().sprite = baseSprite[0];
        storyObj[1].gameObject.GetComponent<Image>().sprite = baseSprite[2];
    }

    public void Story3_ButtonOn()
    {
        storyObj[1].gameObject.GetComponent<Image>().sprite = clickSprite[2];
        storyObj[2].gameObject.GetComponent<Image>().sprite = baseSprite[0];
        storyObj[0].gameObject.GetComponent<Image>().sprite = baseSprite[1];
    }

    //씬 이동
    public void StoryChapter(string _name)
    {
        SceneManager.LoadScene(_name);
    }
    //스토리 저장
    public void StoryNumberSave(string _storyName)
    {
        PlayerPrefs.SetString("KS_StoryNumber", _storyName);
    }

    public void BackButton()
    {
        SceneManager.LoadScene("3.Lobby");
    }
}
