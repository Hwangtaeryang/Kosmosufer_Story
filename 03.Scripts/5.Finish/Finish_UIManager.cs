using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Finish_UIManager : MonoBehaviour
{
    
    public Image[] rankingImg;
    public Sprite[] rankingSprite;
    public TextMeshProUGUI lapsText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI crystalText;


    

    void Start()
    {
        Ranking_Name(PlayerPrefs.GetString("KS_Ranking_No1"), rankingImg[0]);
        Ranking_Name(PlayerPrefs.GetString("KS_Ranking_No2"), rankingImg[1]);
        Ranking_Name(PlayerPrefs.GetString("KS_Ranking_No3"), rankingImg[2]);
        speedText.text = PlayerPrefs.GetInt("KS_MaxSpeed").ToString();
        lapsText.text = PlayerPrefs.GetString("KS_Laps");
        crystalText.text = PlayerPrefs.GetInt("KS_TakeCrystal").ToString();
    }

    void Ranking_Name(string _data, Image _image)
    {
        string[] recordArr = new string[2];
        string myName = "";

        if(_data != "")
        {
            string sourseData = _data;

            char sp = '+';
            recordArr = sourseData.Split(sp);

            myName = recordArr[0];
        }
        Debug.Log("¿Ã∏ß : " + myName);
        if (myName.Equals("Kai"))
            _image.sprite = rankingSprite[0];
        else if (myName.Equals("Juy"))
            _image.sprite = rankingSprite[1];
        else if (myName.Equals("Alran"))
            _image.sprite = rankingSprite[2];
        else if (myName.Equals("Aria"))
            _image.sprite = rankingSprite[3];
        else if (myName.Equals("Miro"))
            _image.sprite = rankingSprite[4];
        else if (myName.Equals("Zen"))
            _image.sprite = rankingSprite[5];
        else if (myName.Equals("AIRobo1"))
            _image.sprite = rankingSprite[6];
        else if (myName.Equals("AIRobo2"))
            _image.sprite = rankingSprite[7];
        else if (myName.Equals("AIRobo3"))
            _image.sprite = rankingSprite[8];
        else
            _image.sprite = rankingSprite[9];
    }

    public void SceneMove()
    {
        if (PlayerPrefs.GetString("KS_StoryNumber").Equals("Story1"))
        {
            AudioManager.Instance.PlayMusic("Lobby");
            SceneManager.LoadScene("4_1_1.Story1");
        }
        else if (PlayerPrefs.GetString("KS_StoryNumber").Equals("Story2"))
        {
            AudioManager.Instance.PlayMusic("Lobby");
            SceneManager.LoadScene("4_1_1.Story2");
        }
        else if (PlayerPrefs.GetString("KS_StoryNumber").Equals("Story3"))
        {
            AudioManager.Instance.PlayMusic("Lobby");
            if (PlayerPrefs.GetString("KS_OpenMap").Equals("StoryEND") &&
                PlayerPrefs.GetString("KS_StoryEndingState").Equals("No"))
                SceneManager.LoadScene("3.Lobby");
            else
                SceneManager.LoadScene("4_1_1.Story3");
        }
    }
}
