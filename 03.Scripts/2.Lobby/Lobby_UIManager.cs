using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Lobby_UIManager : MonoBehaviour
{
    static public Lobby_UIManager instance { get; private set; }

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI crystalText;
    public Transform playerPos;
    public GameObject talkboxListObj;
    //public GameManager dialogObj;

    public GameObject kaiObj;

    public GameObject settingCanvas;    //¼¼ÆÃÆË¾÷ Äµ¹ö½º
    public GameObject setupPenel;   //¼Â¾÷ÆÎ¾÷
    public GameObject appClosePopup;    //¾Û Á¾·á ÆË¾÷

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }


    void Start()
    {
        RewardTextInit();

        if (PlayerPrefs.GetString("KS_OpenMap").Equals("StoryEND") &&
                PlayerPrefs.GetString("KS_StoryEndingState").Equals("No"))
            AudioManager.Instance.PlayMusic("Ending");
        
        //AudioManager.Instance.PlayMusic("Lobby");
        //UserDateManager.instance.SetStoryEndingView("No");
        //UserDateManager.instance.SetOpenMap("3-1");
        //UserDateManager.instance.SetStoryTalkProgress("3-1");
        //UserDateManager.instance.SetTutorial("No");
        //UserDateManager.instance.SetOpenMap("StoryEND");
        //UserDateManager.instance.SetUserAssets(1000, PlayerPrefs.GetString("KS_BoardName"));
        //PlayerObject po = GameManager.Instance.GetSelectedPlayer();
        //player = Instantiate(po.gameplayPrefab, playerPos);
        //playerBody = player.GetComponent<PlayerShip>().shipBody;
    }

    private void Update()
    {

#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            kaiObj.SetActive(false);
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
    
    public void TalkBoxListShow(bool _state)
    {
        talkboxListObj.SetActive(_state);
    }

    //Ä«ÀÌ È°¼ºÈ­/ºñÈ°¼ºÈ­
    public void KaiOnOff(bool _state)
    {
        kaiObj.SetActive(_state);
    }


    public void GameMapChoiceButton()
    {
        SceneManager.LoadScene("4.MapThreeStory");// "4.MapChoiceBoot");
    }

    public void CharacterInfoButton()
    {
        SceneManager.LoadScene("3_1.CharacterInfo");
    }

    public void HoverboardInfoButton()
    {
        SceneManager.LoadScene("3_1.Hoverboard");
    }

    public void DiaryButton()
    {
        SceneManager.LoadScene("3_1.Diary");
    }
}
