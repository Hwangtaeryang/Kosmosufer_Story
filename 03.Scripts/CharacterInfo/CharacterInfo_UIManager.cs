using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CharacterInfo_UIManager : MonoBehaviour
{
    List<Dictionary<string, object>> data;

    public TextMeshProUGUI crystalText;
    public Image infoPanel; //�����ǳ� - ���� �Ķ��� ��
    public Image nameImg;   //�̸��̹���
    public TextMeshProUGUI ageText; //����
    public TextMeshProUGUI birthdayText;    //����
    public TextMeshProUGUI nationalityText; //����
    public TextMeshProUGUI infoText;    //����
    public Image characterImg;  //ĳ�����̹���

    public Sprite[] nameSprite;
    public Sprite[] characterSprtie;

    public Sprite[] infoPanelsprite;
    public Sprite[] lockCharacterSprite;

    public Button leftBtn;
    public Button rightBtn;

    int characterIndex; //ĳ���� �ε�����


    public GameObject settingCanvas;    //�����˾� ĵ����
    public GameObject setupPenel;   //�¾��ξ�
    public GameObject appClosePopup;    //�� ���� �˾�


    private void Start()
    {
        RewardTextInit();
        data = CSVReader.Read("HoverCharacterInfo");
        //AudioManager.Instance.PlayMusic("Lobby");

        leftBtn.interactable = false;
        characterIndex = 0;
        nameImg.sprite = nameSprite[0]; //ī��
        characterImg.sprite = characterSprtie[0];   //ī��
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i]["�̸�"].ToString().Equals("ī��"))
            {
                ageText.text = data[i]["����"].ToString();
                birthdayText.text = data[i]["����"].ToString();
                nationalityText.text = data[i]["����"].ToString();
                infoText.text = data[i]["����"].ToString();
            }
        }
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
        Color color;
        if (characterIndex > 0)
        {
            characterIndex -= 1;
            leftBtn.interactable = true;

            if (characterIndex != 7)
                rightBtn.interactable = true;

            if (characterIndex.Equals(0))
                leftBtn.interactable = false;
        }
        else
        {
            leftBtn.interactable = false;
            rightBtn.interactable = true;
        }

        for(int i = 0; i < data.Count; i++)
        {
            if(data[i]["����"].ToString().Equals(characterIndex.ToString()))
            {
                //���� ����
                if(PlayerPrefs.GetString("KS_OpenMap").Equals("StoryEND"))
                {
                    infoPanel.sprite = infoPanelsprite[0];
                    nameImg.gameObject.SetActive(true);
                    ageText.gameObject.SetActive(true);
                    birthdayText.gameObject.SetActive(true);
                    nationalityText.gameObject.SetActive(true);
                    infoText.gameObject.SetActive(true);

                    nameImg.sprite = nameSprite[characterIndex];
                    ageText.text = data[i]["����"].ToString();
                    birthdayText.text = data[i]["����"].ToString();
                    nationalityText.text = data[i]["����"].ToString();
                    infoText.text = data[i]["����"].ToString();
                    characterImg.sprite = characterSprtie[characterIndex];
                }
                //�Ƹ��� �̿���
                else if(PlayerPrefs.GetString("KS_OpenMap").Equals("3-1") ||
                    PlayerPrefs.GetString("KS_OpenMap").Equals("2-3"))
                {
                    if(characterIndex.Equals(7))
                    {
                        infoPanel.sprite = infoPanelsprite[1]; //���̹��� ����
                        nameImg.gameObject.SetActive(false);
                        ageText.gameObject.SetActive(false);
                        birthdayText.gameObject.SetActive(false);
                        nationalityText.gameObject.SetActive(false);
                        infoText.gameObject.SetActive(false);
                        characterImg.sprite = lockCharacterSprite[2];
                    }
                    else
                    {
                        infoPanel.sprite = infoPanelsprite[0];
                        nameImg.gameObject.SetActive(true);
                        ageText.gameObject.SetActive(true);
                        birthdayText.gameObject.SetActive(true);
                        nationalityText.gameObject.SetActive(true);
                        infoText.gameObject.SetActive(true);

                        nameImg.sprite = nameSprite[characterIndex];
                        ageText.text = data[i]["����"].ToString();
                        birthdayText.text = data[i]["����"].ToString();
                        nationalityText.text = data[i]["����"].ToString();
                        infoText.text = data[i]["����"].ToString();
                        characterImg.sprite = characterSprtie[characterIndex];
                    }
                    
                }
                //�Ƹ���, �˶� �̿���
                else if(PlayerPrefs.GetString("KS_OpenMap").Equals("2-2") ||
                    PlayerPrefs.GetString("KS_OpenMap").Equals("2-1") ||
                    PlayerPrefs.GetString("KS_OpenMap").Equals("1-4"))
                {
                    if (characterIndex.Equals(7) || characterIndex.Equals(6))
                    {
                        infoPanel.sprite = infoPanelsprite[1]; //���̹��� ����
                        nameImg.gameObject.SetActive(false);
                        ageText.gameObject.SetActive(false);
                        birthdayText.gameObject.SetActive(false);
                        nationalityText.gameObject.SetActive(false);
                        infoText.gameObject.SetActive(false);

                        if(characterIndex.Equals(7))
                            characterImg.sprite = lockCharacterSprite[2];
                        else
                            characterImg.sprite = lockCharacterSprite[1];
                    }
                    else
                    {
                        infoPanel.sprite = infoPanelsprite[0];
                        nameImg.gameObject.SetActive(true);
                        ageText.gameObject.SetActive(true);
                        birthdayText.gameObject.SetActive(true);
                        nationalityText.gameObject.SetActive(true);
                        infoText.gameObject.SetActive(true);

                        nameImg.sprite = nameSprite[characterIndex];
                        ageText.text = data[i]["����"].ToString();
                        birthdayText.text = data[i]["����"].ToString();
                        nationalityText.text = data[i]["����"].ToString();
                        infoText.text = data[i]["����"].ToString();
                        characterImg.sprite = characterSprtie[characterIndex];
                    }
                }
                //�Ƹ���, �˶�, �� �̿���
                else if (PlayerPrefs.GetString("KS_OpenMap").Equals("1-1") ||
                    PlayerPrefs.GetString("KS_OpenMap").Equals("1-2") ||
                    PlayerPrefs.GetString("KS_OpenMap").Equals("1-3"))
                {
                    if (characterIndex.Equals(7) || characterIndex.Equals(6) || characterIndex.Equals(5))
                    {
                        infoPanel.sprite = infoPanelsprite[1]; //���̹��� ����
                        nameImg.gameObject.SetActive(false);
                        ageText.gameObject.SetActive(false);
                        birthdayText.gameObject.SetActive(false);
                        nationalityText.gameObject.SetActive(false);
                        infoText.gameObject.SetActive(false);

                        if (characterIndex.Equals(7))
                            characterImg.sprite = lockCharacterSprite[2];
                        else if(characterIndex.Equals(6))
                            characterImg.sprite = lockCharacterSprite[1];
                        else
                            characterImg.sprite = lockCharacterSprite[0];
                    }
                    else
                    {
                        infoPanel.sprite = infoPanelsprite[0];
                        nameImg.gameObject.SetActive(true);
                        ageText.gameObject.SetActive(true);
                        birthdayText.gameObject.SetActive(true);
                        nationalityText.gameObject.SetActive(true);
                        infoText.gameObject.SetActive(true);

                        nameImg.sprite = nameSprite[characterIndex];
                        ageText.text = data[i]["����"].ToString();
                        birthdayText.text = data[i]["����"].ToString();
                        nationalityText.text = data[i]["����"].ToString();
                        infoText.text = data[i]["����"].ToString();
                        characterImg.sprite = characterSprtie[characterIndex];
                    }
                }
            }
        }
    }

    public void RightButtonOn()
    {
        Color color;

        if (characterIndex < 7)
        {
            characterIndex += 1;
            rightBtn.interactable = true;

            if (characterIndex != 0)
                leftBtn.interactable = true;

            if(characterIndex.Equals(7))
                rightBtn.interactable = false;
        }
        else
        {
            rightBtn.interactable = false;
            leftBtn.interactable = true;
        }

        for (int i = 0; i < data.Count; i++)
        {
            if (data[i]["����"].ToString().Equals(characterIndex.ToString()))
            {
                //���� ����
                if (PlayerPrefs.GetString("KS_OpenMap").Equals("StoryEND"))
                {
                    infoPanel.sprite = infoPanelsprite[0];
                    nameImg.gameObject.SetActive(true);
                    ageText.gameObject.SetActive(true);
                    birthdayText.gameObject.SetActive(true);
                    nationalityText.gameObject.SetActive(true);
                    infoText.gameObject.SetActive(true);

                    nameImg.sprite = nameSprite[characterIndex];
                    ageText.text = data[i]["����"].ToString();
                    birthdayText.text = data[i]["����"].ToString();
                    nationalityText.text = data[i]["����"].ToString();
                    infoText.text = data[i]["����"].ToString();
                    characterImg.sprite = characterSprtie[characterIndex];
                }
                //�Ƹ��� �̿���
                else if (PlayerPrefs.GetString("KS_OpenMap").Equals("3-1") ||
                    PlayerPrefs.GetString("KS_OpenMap").Equals("2-3"))
                {
                    if (characterIndex.Equals(7))
                    {
                        infoPanel.sprite = infoPanelsprite[1]; //���̹��� ����
                        nameImg.gameObject.SetActive(false);
                        ageText.gameObject.SetActive(false);
                        birthdayText.gameObject.SetActive(false);
                        nationalityText.gameObject.SetActive(false);
                        infoText.gameObject.SetActive(false);

                        if (characterIndex.Equals(7))
                            characterImg.sprite = lockCharacterSprite[2];
                    }
                    else
                    {
                        infoPanel.sprite = infoPanelsprite[0];
                        nameImg.gameObject.SetActive(true);
                        ageText.gameObject.SetActive(true);
                        birthdayText.gameObject.SetActive(true);
                        nationalityText.gameObject.SetActive(true);
                        infoText.gameObject.SetActive(true);

                        nameImg.sprite = nameSprite[characterIndex];
                        ageText.text = data[i]["����"].ToString();
                        birthdayText.text = data[i]["����"].ToString();
                        nationalityText.text = data[i]["����"].ToString();
                        infoText.text = data[i]["����"].ToString();
                        characterImg.sprite = characterSprtie[characterIndex];
                    }
                }
                //�Ƹ���, �˶� �̿���
                else if (PlayerPrefs.GetString("KS_OpenMap").Equals("2-2") ||
                    PlayerPrefs.GetString("KS_OpenMap").Equals("2-1") ||
                    PlayerPrefs.GetString("KS_OpenMap").Equals("1-4"))
                {
                    if (characterIndex.Equals(7) || characterIndex.Equals(6))
                    {
                        infoPanel.sprite = infoPanelsprite[1]; //���̹��� ����
                        nameImg.gameObject.SetActive(false);
                        ageText.gameObject.SetActive(false);
                        birthdayText.gameObject.SetActive(false);
                        nationalityText.gameObject.SetActive(false);
                        infoText.gameObject.SetActive(false);

                        if (characterIndex.Equals(7))
                            characterImg.sprite = lockCharacterSprite[2];
                        else if (characterIndex.Equals(6))
                            characterImg.sprite = lockCharacterSprite[1];
                    }
                    else
                    {
                        infoPanel.sprite = infoPanelsprite[0];
                        nameImg.gameObject.SetActive(true);
                        ageText.gameObject.SetActive(true);
                        birthdayText.gameObject.SetActive(true);
                        nationalityText.gameObject.SetActive(true);
                        infoText.gameObject.SetActive(true);

                        nameImg.sprite = nameSprite[characterIndex];
                        ageText.text = data[i]["����"].ToString();
                        birthdayText.text = data[i]["����"].ToString();
                        nationalityText.text = data[i]["����"].ToString();
                        infoText.text = data[i]["����"].ToString();
                        characterImg.sprite = characterSprtie[characterIndex];
                    }
                }
                //�Ƹ���, �˶�, �� �̿���
                else if (PlayerPrefs.GetString("KS_OpenMap").Equals("1-1") ||
                    PlayerPrefs.GetString("KS_OpenMap").Equals("1-2") ||
                    PlayerPrefs.GetString("KS_OpenMap").Equals("1-3"))
                {
                    if (characterIndex.Equals(7) || characterIndex.Equals(6) || characterIndex.Equals(5))
                    {
                        infoPanel.sprite = infoPanelsprite[1]; //���̹��� ����
                        nameImg.gameObject.SetActive(false);
                        ageText.gameObject.SetActive(false);
                        birthdayText.gameObject.SetActive(false);
                        nationalityText.gameObject.SetActive(false);
                        infoText.gameObject.SetActive(false);

                        if (characterIndex.Equals(7))
                            characterImg.sprite = lockCharacterSprite[2];
                        else if (characterIndex.Equals(6))
                            characterImg.sprite = lockCharacterSprite[1];
                        else
                            characterImg.sprite = lockCharacterSprite[0];
                    }
                    else
                    {
                        infoPanel.sprite = infoPanelsprite[0];
                        nameImg.gameObject.SetActive(true);
                        ageText.gameObject.SetActive(true);
                        birthdayText.gameObject.SetActive(true);
                        nationalityText.gameObject.SetActive(true);
                        infoText.gameObject.SetActive(true);

                        nameImg.sprite = nameSprite[characterIndex];
                        ageText.text = data[i]["����"].ToString();
                        birthdayText.text = data[i]["����"].ToString();
                        nationalityText.text = data[i]["����"].ToString();
                        infoText.text = data[i]["����"].ToString();
                        characterImg.sprite = characterSprtie[characterIndex];
                    }
                }
            }
        }
    }
    



    public void BackButtonOn()
    {
        SceneManager.LoadScene("3.Lobby");
    }
}
