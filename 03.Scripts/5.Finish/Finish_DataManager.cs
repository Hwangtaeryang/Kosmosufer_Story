using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Finish_DataManager : MonoBehaviour
{
    public Transform[] pos;

    public Image rankingImg;
    public Image rankingPanel;  //랭킹 판넬
    public TextMeshProUGUI raceTimeText;
    public TextMeshProUGUI lapsText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI crystalText;

    public Sprite[] rankingSprite;  //순위이미지
    public Sprite[] rankingPanelSprtie; //랭킹 판넬 텍스쳐 0:1등, 1: 그위 순위, 2:미통과
    public GameObject[] paticals;   //파티클

    public Animator anim;


    string[] record;    //기록 쪼개기
    string myName, myRanking;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRankingPosInit();
        AudioManager.Instance.PlayMusic("One_of_Victory_Loop");
    }

    void PlayerRankingPosInit()
    {
        if(PlayerPrefs.GetString("KS_Ranking_No1") != "")
        {
            Ranking_Name(PlayerPrefs.GetString("KS_Ranking_No1"));
            Debug.Log("---- "+PlayerPrefs.GetString("KS_Ranking_No1") + "  " + myName);
            Instantiate(Resources.Load<GameObject>("Players/" + myName), pos[0]);
            pos[0].transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 0);

            pos[0].transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 180, 0);
            if (myName.Equals("Kai"))
            {
                pos[0].transform.GetChild(0).transform.localScale = new Vector3(80, 80, 80);
            }

            MyInfoData(pos[0]);
        }

        if (PlayerPrefs.GetString("KS_Ranking_No2") != "")
        {
            Ranking_Name(PlayerPrefs.GetString("KS_Ranking_No2"));
            Instantiate(Resources.Load<GameObject>("Players/" + myName), pos[1]);
            pos[1].transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 0);
            pos[1].transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 180, 0);

            if (myName.Equals("Kai"))
            {
                pos[1].transform.GetChild(0).transform.localScale = new Vector3(80, 80, 80);
            }

            MyInfoData(pos[1]);
        }

        if (PlayerPrefs.GetString("KS_Ranking_No3") != "")
        {
            Ranking_Name(PlayerPrefs.GetString("KS_Ranking_No3"));
            Instantiate(Resources.Load<GameObject>("Players/" + myName), pos[2]);
            pos[2].transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 0);
            pos[2].transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 180, 0);

            if (myName.Equals("Kai"))
            {
                pos[2].transform.GetChild(0).transform.localScale = new Vector3(80, 80, 80);
            }

            MyInfoData(pos[2]);
        }

        if (PlayerPrefs.GetString("KS_Ranking_No4") != "")
        {
            Ranking_Name(PlayerPrefs.GetString("KS_Ranking_No4"));

            MyInfoData(pos[4]);
        }
    }

    void MyInfoData(Transform _parents)
    {
        if (PlayerPrefs.GetString("KS_PassState").Equals("Yes"))
        {
            if (myName.Equals("Kai"))
            {
                //Instantiate(Resources.Load<GameObject>("Players/" + myName), pos[3]);
                //pos[3].transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 0);

                if (myRanking.Equals("1"))
                {
                    rankingPanel.sprite = rankingPanelSprtie[0];
                    _parents.transform.GetChild(0).GetComponent<Animator>().SetBool("Victory", true);
                    //anim.SetBool("Victory", true);
                }
                else
                {
                    rankingPanel.sprite = rankingPanelSprtie[1];
                    _parents.transform.GetChild(0).GetComponent<Animator>().SetBool("Fail", true);
                    //anim.SetBool("Fail", true);
                }
                    

                if (myRanking.Equals("1"))
                    rankingImg.sprite = rankingSprite[0];
                else if (myRanking.Equals("2"))
                    rankingImg.sprite = rankingSprite[1];
                else if (myRanking.Equals("3"))
                    rankingImg.sprite = rankingSprite[2];
                else if (myRanking.Equals("4"))
                    rankingImg.sprite = rankingSprite[3];
                else if (myRanking.Equals("5"))
                    rankingImg.sprite = rankingSprite[4];
                else if (myRanking.Equals("6"))
                    rankingImg.sprite = rankingSprite[5];
                else if (myRanking.Equals("7"))
                    rankingImg.sprite = rankingSprite[6];
                else if (myRanking.Equals("8"))
                    rankingImg.sprite = rankingSprite[7];

                raceTimeText.text = PlayerPrefs.GetString("KS_EndTime");
                lapsText.text = PlayerPrefs.GetString("KS_Laps");
                speedText.text = PlayerPrefs.GetInt("KS_MaxSpeed").ToString();
                crystalText.text = PlayerPrefs.GetInt("KS_TakeCrystal").ToString();
            }
            else
            {
                if (myRanking.Equals("1"))
                {
                    _parents.transform.GetChild(0).GetComponent<Animator>().SetBool("Victory", true);
                }
                else
                {
                    _parents.transform.GetChild(0).GetComponent<Animator>().SetBool("Fail", true);
                }
            }
        }
        else
        {
            for (int i = 0; i < paticals.Length; i++)
                paticals[i].SetActive(false);   //완주 못해서 파티클 비활성화

            rankingPanel.sprite = rankingPanelSprtie[2];
            rankingImg.gameObject.SetActive(false);

            raceTimeText.text = "";
            lapsText.text = "";
            speedText.text = "";
            crystalText.text = PlayerPrefs.GetInt("KS_TakeCrystal").ToString();
        }
    }

    void Ranking_Name(string _data)
    {
        if (_data != "")
        {
            string sourseData = _data;

            char sp = '+';
            record = sourseData.Split(sp);
        }
        myName = record[0];
        myRanking = record[1];
    }


    public void MainButtonOn()
    {
        if (PlayerPrefs.GetString("KS_StoryNumber").Equals("Story1"))
        {
            AudioManager.Instance.PlayMusic("Lobby");
            SceneManager.LoadScene("4_1_1.Story1");
        }
        else if(PlayerPrefs.GetString("KS_StoryNumber").Equals("Story2"))
        {
            AudioManager.Instance.PlayMusic("Lobby");
            SceneManager.LoadScene("4_1_1.Story2");
        }
        else if (PlayerPrefs.GetString("KS_StoryNumber").Equals("Story3"))
        {
            AudioManager.Instance.PlayMusic("Lobby");
            if(PlayerPrefs.GetString("KS_OpenMap").Equals("StoryEND") &&
                PlayerPrefs.GetString("KS_StoryEndingState").Equals("No"))
                SceneManager.LoadScene("3.Lobby");
            else
                SceneManager.LoadScene("4_1_1.Story3");
        }
    }
}
