using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TalkBoxList : MonoBehaviour
{
    static public TalkBoxList instance { get; private set; }

    public DialogManager dialogManager;
    public GameObject[] example;
    public GameObject roomTitlePanel;   //��ȭ �� �̸� �ǳ�
    public Image talkBackImg;   // ��ȭ�� ���
    public Sprite[] talkBackSprite; //��ȭ�� ��� ����

    public GameObject kaiObj;

    public string questCheck;   //���û��׿��� ������ �亯
    public bool talkEnd;    //��ȭ �������� ���� - pause���� �˸�


    public GameObject joyStickParent;    //Ʃ�丮�� ���� ���̽�ƽ �θ�
    GameObject joyStick;    //Ʃ�丮�� ���� ���̽�ƽ




    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }


    void Start()
    {
        if(SceneManager.GetActiveScene().name.Equals("3.Lobby"))
        {
            if(PlayerPrefs.GetString("KS_StoryProgress").Equals("0-1"))
            {
                KaiModelingState(false);
                DialogState(true);
                Story0_1TalkStart();
            }
            else if(PlayerPrefs.GetString("KS_OpenMap").Equals("StoryEND") &&
                PlayerPrefs.GetString("KS_StoryEndingState").Equals("No"))
            {
                KaiModelingState(false);
                DialogState(true);
                Lobby_UIManager.instance.TalkBoxListShow(true); //��ũ�ڽ� Ȱ��ȭ
                StoryEndingTalkStart();
            }
            else
            {
                KaiModelingState(true);
                DialogState(false);
                Lobby_UIManager.instance.TalkBoxListShow(false);
            }
        }
        else //if(SceneManager.GetActiveScene().name.Equals("4.TutorialMap"))
        {
            StartCoroutine(JoypadParentsSetting());
        }
    }

    public void KaiModelingState(bool _state)
    {
        kaiObj.SetActive(_state);
    }

    public void DialogState(bool _state)
    {
        dialogManager.gameObject.SetActive(_state);
    }

    //���̽�ƽ ������Ʈ �θ������ �־��ش�.
    IEnumerator JoypadParentsSetting()
    {
        yield return new WaitForSeconds(0.05f);
        joyStick = GameObject.Find("JoyPad Dual_1(Clone)");
        joyStick.transform.SetParent(joyStickParent.transform);
    }

    //���̽�ƽ Ȱ��ȭ/��Ȱ��ȭ
    public void JoPadFind_and_State(bool _state)
    {
        joyStickParent.transform.GetChild(0).gameObject.SetActive(_state);
    }

    // Update is called once per frame
    void Update()
    {
        //é�� ���丮 �����ؼ� ���� �����ϴ� ����
        if(PlayerPrefs.GetString("KS_StoryProgress").Equals("0-1_2") &&
            UserDateManager.instance.storyOnePlay.Equals(true))
        {
            UserDateManager.instance.storyOnePlay = false;
            Story0_1_2TalkStart();
        }
        else if(PlayerPrefs.GetString("KS_StoryProgress").Equals("0-2") &&
            UserDateManager.instance.storyOnePlay.Equals(true))
        {
            UserDateManager.instance.storyOnePlay = false;
            Story0_2TalkStart();
        }
        else if (PlayerPrefs.GetString("KS_StoryProgress").Equals("0-3") &&
            UserDateManager.instance.storyOnePlay.Equals(true))
        {
            UserDateManager.instance.storyOnePlay = false;
            Story0_3TalkStart();
        }
        else if (PlayerPrefs.GetString("KS_StoryProgress").Equals("0-4") &&
            UserDateManager.instance.storyOnePlay.Equals(true))
        {
            UserDateManager.instance.storyOnePlay = false;
            Story0_4TalkStart();
        }
        else if (PlayerPrefs.GetString("KS_StoryProgress").Equals("0-5") &&
            UserDateManager.instance.storyOnePlay.Equals(true))
        {
            UserDateManager.instance.storyOnePlay = false;
            Story0_5TalkStart();
        }
        else if (PlayerPrefs.GetString("KS_StoryProgress").Equals("0-6") &&
            UserDateManager.instance.storyOnePlay.Equals(true))
        {
            UserDateManager.instance.storyOnePlay = false;
            Story0_6TalkStart();
        }
        else if(PlayerPrefs.GetString("KS_StoryProgress").Equals("1-4_2") &&
            UserDateManager.instance.storyOnePlay.Equals(true))
        {
            UserDateManager.instance.storyOnePlay = false;
            Story1_4_2TalkStart();
        }
        else if(PlayerPrefs.GetString("KS_StoryProgress").Equals("1-4_3") &&
            UserDateManager.instance.storyOnePlay.Equals(true))
        {
            UserDateManager.instance.storyOnePlay = false;
            Story1_4_3TalkStart();
        }
        else if(PlayerPrefs.GetString("KS_StoryProgress").Equals("2-1_2") &&
            UserDateManager.instance.storyOnePlay.Equals(true))
        {
            UserDateManager.instance.storyOnePlay = false;
            Story2_1_2TalkStart();
        }
        else if(PlayerPrefs.GetString("KS_StoryProgress").Equals("3-1_2") &&
            UserDateManager.instance.storyOnePlay.Equals(true))
        {
            UserDateManager.instance.storyOnePlay = false;
            Story3_1_2TalkStart();
        }
        else if(PlayerPrefs.GetString("KS_StoryProgress").Equals("3-1_3") &&
            UserDateManager.instance.storyOnePlay.Equals(true))
        {
            UserDateManager.instance.storyOnePlay = false;
            Story3_1_3TalkStart();
        }
        else if(PlayerPrefs.GetString("KS_StoryProgress").Equals("3-1_4") &&
            UserDateManager.instance.storyOnePlay.Equals(true))
        {
            UserDateManager.instance.storyOnePlay = false;
            Story3_1_4TalkStart();
        }
        else if(PlayerPrefs.GetString("KS_StoryProgress").Equals("3-1_5") &&
            UserDateManager.instance.storyOnePlay.Equals(true))
        {
            UserDateManager.instance.storyOnePlay = false;
            Story3_1_5TalkStart();
        }
    }

    //���丮 �¿��� �Լ�
    public void StoryPrgressONOFF()
    {
        //���丮 ����
        if(PlayerPrefs.GetString("KS_StoryOnOff").Equals("Off"))
        {
            if(SceneManager.GetActiveScene().name.Equals("4.TutorialMap"))
            {
                if(PlayerPrefs.GetString("KS_OpenMap").Equals("1-1"))
                {
                    //������ é�Ϳ� ���� ���丮�� ����Ǿ�� �ؼ� ������ é�͹�ȣ�� ����
                    UserDateManager.instance.SetStoryTalkProgress("1-1");
                    //���� 1-1é�͸� ���� ���� ���¶� ���丮�� �׽� ����
                    dialogManager.gameObject.SetActive(true);
                    Story1_1TalkStart();
                }
                else
                {
                    dialogManager.gameObject.SetActive(false);
                    roomTitlePanel.SetActive(false);    //��ȭâ�����ǳ� ��Ȱ��ȭ
                    RaceManager.Instance.CountDownStart();
                }
            }
            else if(SceneManager.GetActiveScene().name.Equals("4.Map1"))
            {
                if(PlayerPrefs.GetString("KS_OpenMap").Equals("1-2"))
                {
                    //������ é�Ϳ� ���� ���丮�� ����Ǿ�� �ؼ� ������ é�͹�ȣ�� ����
                    UserDateManager.instance.SetStoryTalkProgress("1-2");
                    dialogManager.gameObject.SetActive(true);
                    Story1_2TalkStart();
                }
                else if(PlayerPrefs.GetString("KS_OpenMap").Equals("1-3"))
                {
                    if(PlayerPrefs.GetString("KS_ChapterNumber").Equals("1-3"))
                    {
                        //������ é�Ϳ� ���� ���丮�� ����Ǿ�� �ؼ� ������ é�͹�ȣ�� ����
                        UserDateManager.instance.SetStoryTalkProgress("1-3");
                        dialogManager.gameObject.SetActive(true);
                        Story1_3TalkStart();
                    }
                    else
                    {
                        dialogManager.gameObject.SetActive(false);
                        roomTitlePanel.SetActive(false);    //��ȭâ�����ǳ� ��Ȱ��ȭ
                        RaceManager.Instance.CountDownStart();
                    }
                }
                else if(PlayerPrefs.GetString("KS_OpenMap").Equals("1-4"))
                {
                    if(PlayerPrefs.GetString("KS_ChapterNumber").Equals("1-4"))
                    {
                        //������ é�Ϳ� ���� ���丮�� ����Ǿ�� �ؼ� ������ é�͹�ȣ�� ����
                        UserDateManager.instance.SetStoryTalkProgress("1-4");
                        dialogManager.gameObject.SetActive(true);
                        Story1_4TalkStart();
                    }
                    else
                    {
                        dialogManager.gameObject.SetActive(false);
                        roomTitlePanel.SetActive(false);    //��ȭâ�����ǳ� ��Ȱ��ȭ
                        RaceManager.Instance.CountDownStart();
                    }
                }
                else
                {
                    dialogManager.gameObject.SetActive(false);
                    roomTitlePanel.SetActive(false);    //��ȭâ�����ǳ� ��Ȱ��ȭ
                    RaceManager.Instance.CountDownStart();
                }
            }
            else if(SceneManager.GetActiveScene().name.Equals("4.Map2"))
            {
                if(PlayerPrefs.GetString("KS_OpenMap").Equals("2-1"))
                {
                    //������ é�Ϳ� ���� ���丮�� ����Ǿ�� �ؼ� ������ é�͹�ȣ�� ����
                    UserDateManager.instance.SetStoryTalkProgress("2-1");
                    dialogManager.gameObject.SetActive(true);
                    Story2_1TalkStart();
                }
                else if(PlayerPrefs.GetString("KS_OpenMap").Equals("2-2"))
                {
                    if(PlayerPrefs.GetString("KS_ChapterNumber").Equals("2-2"))
                    {
                        //������ é�Ϳ� ���� ���丮�� ����Ǿ�� �ؼ� ������ é�͹�ȣ�� ����
                        UserDateManager.instance.SetStoryTalkProgress("2-2");
                        dialogManager.gameObject.SetActive(true);
                        Story2_2TalkStart();
                    }
                    else
                    {
                        dialogManager.gameObject.SetActive(false);
                        roomTitlePanel.SetActive(false);    //��ȭâ�����ǳ� ��Ȱ��ȭ
                        RaceManager.Instance.CountDownStart();
                    }
                }
                else if(PlayerPrefs.GetString("KS_OpenMap").Equals("2-3"))
                {
                    if(PlayerPrefs.GetString("KS_OpenMap").Equals("2-3"))
                    {
                        //������ é�Ϳ� ���� ���丮�� ����Ǿ�� �ؼ� ������ é�͹�ȣ�� ����
                        UserDateManager.instance.SetStoryTalkProgress("2-3");
                        dialogManager.gameObject.SetActive(true);
                        Story2_3TalkStart();
                    }
                    else
                    {
                        dialogManager.gameObject.SetActive(false);
                        roomTitlePanel.SetActive(false);    //��ȭâ�����ǳ� ��Ȱ��ȭ
                        RaceManager.Instance.CountDownStart();
                    }
                }
                else
                {
                    dialogManager.gameObject.SetActive(false);
                    roomTitlePanel.SetActive(false);    //��ȭâ�����ǳ� ��Ȱ��ȭ
                    RaceManager.Instance.CountDownStart();
                }
            }
            else if(SceneManager.GetActiveScene().name.Equals("4.Map3"))
            {
                if(PlayerPrefs.GetString("KS_OpenMap").Equals("3-1"))
                {
                    //������ é�Ϳ� ���� ���丮�� ����Ǿ�� �ؼ� ������ é�͹�ȣ�� ����
                    UserDateManager.instance.SetStoryTalkProgress("3-1");
                    dialogManager.gameObject.SetActive(true);
                    Story3_1TalkStart();
                }
                else
                {
                    dialogManager.gameObject.SetActive(false);
                    roomTitlePanel.SetActive(false);    //��ȭâ�����ǳ� ��Ȱ��ȭ
                    RaceManager.Instance.CountDownStart();
                }
            }
            //else if(SceneManager.GetActiveScene().name.Equals("3.Lobby"))
            //{
            //    if(PlayerPrefs.GetString("KS_OpenMap").Equals("StoryEND") &&
            //        PlayerPrefs.GetString("KS_StoryEndingState").Equals("No"))
            //    {
            //        dialogManager.gameObject.SetActive(true);
            //        Story
            //    }
            //}
        }
        else
        {
            if (SceneManager.GetActiveScene().name.Equals("4.TutorialMap"))
            {
                //������ é�Ϳ� ���� ���丮�� ����Ǿ�� �ؼ� ������ é�͹�ȣ�� ����
                UserDateManager.instance.SetStoryTalkProgress("1-1");
                dialogManager.gameObject.SetActive(true);
                Story1_1TalkStart();
            }
            else if (SceneManager.GetActiveScene().name.Equals("4.Map1"))
            {
                if(PlayerPrefs.GetString("KS_ChapterNumber").Equals("1-2"))
                {
                    //������ é�Ϳ� ���� ���丮�� ����Ǿ�� �ؼ� ������ é�͹�ȣ�� ����
                    UserDateManager.instance.SetStoryTalkProgress("1-2");
                    dialogManager.gameObject.SetActive(true);
                    Story1_2TalkStart();
                }
                else if(PlayerPrefs.GetString("KS_ChapterNumber").Equals("1-3"))
                {
                    //������ é�Ϳ� ���� ���丮�� ����Ǿ�� �ؼ� ������ é�͹�ȣ�� ����
                    UserDateManager.instance.SetStoryTalkProgress("1-3");
                    dialogManager.gameObject.SetActive(true);
                    Story1_3TalkStart();
                }
                else if(PlayerPrefs.GetString("KS_ChapterNumber").Equals("1-4"))
                {
                    //������ é�Ϳ� ���� ���丮�� ����Ǿ�� �ؼ� ������ é�͹�ȣ�� ����
                    UserDateManager.instance.SetStoryTalkProgress("1-4");
                    dialogManager.gameObject.SetActive(true);
                    Story1_4TalkStart();
                }
            }
            else if (SceneManager.GetActiveScene().name.Equals("4.Map2"))
            {
                if(PlayerPrefs.GetString("KS_ChapterNumber").Equals("2-1"))
                {
                    //������ é�Ϳ� ���� ���丮�� ����Ǿ�� �ؼ� ������ é�͹�ȣ�� ����
                    UserDateManager.instance.SetStoryTalkProgress("2-1");
                    dialogManager.gameObject.SetActive(true);
                    Story2_1TalkStart();
                }
                else if(PlayerPrefs.GetString("KS_ChapterNumber").Equals("2-2"))
                {
                    //������ é�Ϳ� ���� ���丮�� ����Ǿ�� �ؼ� ������ é�͹�ȣ�� ����
                    UserDateManager.instance.SetStoryTalkProgress("2-2");
                    dialogManager.gameObject.SetActive(true);
                    Story2_2TalkStart();
                }
                else if (PlayerPrefs.GetString("KS_ChapterNumber").Equals("2-3"))
                {
                    //������ é�Ϳ� ���� ���丮�� ����Ǿ�� �ؼ� ������ é�͹�ȣ�� ����
                    UserDateManager.instance.SetStoryTalkProgress("2-3");
                    dialogManager.gameObject.SetActive(true);
                    Story2_3TalkStart();
                }
            }
            else if(SceneManager.GetActiveScene().name.Equals("4.Map3"))
            {
                //������ é�Ϳ� ���� ���丮�� ����Ǿ�� �ؼ� ������ é�͹�ȣ�� ����
                UserDateManager.instance.SetStoryTalkProgress("3-1");
                dialogManager.gameObject.SetActive(true);
                Story3_1TalkStart();
            }
        }
    }


    void Show_StoryTalk(int _index)
    {
        example[_index].SetActive(true);
    }

    void Story0_1TalkStart()
    {
        StartCoroutine(_Story0_1TalkStart());
    }
    IEnumerator _Story0_1TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "â��";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);
        //JoPadFind_and_State(false);

        Story0_1Talk();
    }
    void Story0_1Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[0];

        dialogTexts.Add(new DialogData("/speed:0.1/�����...������ ã�ƿ���� �� ���ڷ簡 ���� ��� �����ٵ�...", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/������", "��"));
        dialogTexts.Add(new DialogData("/speed:0.1/��!! �̳��� �㰡 ��� ��Ÿ����?? ����! ���⿡ �� ������ �־���", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/��¦��¦", "��������"));
        dialogTexts.Add(new DialogData("/speed:0.1/�̰� ����? ���� �����ݾ�..�̰� ��� �ִ� ��Ÿ������?? �ϴ� ���踦 ì�ܺ��߰ھ�.", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/����� �ƺ��� �۾����̾��µ�..���谡 �������� �� �����̾�����..", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�� ����� â�� ������߰ھ�.", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/ö��! ������", "��"));

        dialogManager.Show(dialogTexts);
    }


    void Story0_1_2TalkStart()
    {
        StartCoroutine(_Story0_1_2TalkStart());
    }
    IEnumerator _Story0_1_2TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "�ƺ��� ������";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story0_1_2Talk();
    }
    void Story0_1_2Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[13];

        dialogTexts.Add(new DialogData("/speed:0.1/�ƺ��� ���ư��ð� ������ ó�� ������ �� ����.", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/��! ������ �ֳ�. ���� ���������.. ù ��° ������ ������߰ھ�.", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/������", "���� ĳ���"));
        dialogTexts.Add(new DialogData("/speed:0.1/�Ҷ��", "�ƺ��� ������Ʈ"));
        dialogTexts.Add(new DialogData("/speed:0.1/�ƺ��� ������Ʈ��..����...�Ƽ��ÿ� ����Ʈ..�б���...�� ���� �̷�..���� ����� �̾߱� ���̱���", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�� ��° �������� ���� ���������?", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/������", "���� ĳ���"));
        dialogTexts.Add(new DialogData("/speed:0.1/(���۸���)", "��������"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/��..�� ���� ��� ��. ���� � �� �ƺ��� �����ǿ� ���� ���ٰ� ���� �����̾�..", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷� ������ �������� ���� �ִ��� �� ������߰ھ�.", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/������", "���� ĳ���"));
        dialogTexts.Add(new DialogData("/speed:0.1/��¦��¦", "ĳ��ֿ���"));
        dialogTexts.Add(new DialogData("/speed:0.1/���. �� ���谡 �ֳ�..�̰� ĳ��� ���� ������..�̰ɷ� ĳ����� ���������..", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/ö��! ������", "���� ĳ���"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/�̾� ���� ȣ�������ΰ�..ģ������ Ÿ�� �ٴҶ��� �η����µ�..���� �� ȣ�����尡 ����°ǰ�?", "ī��"));

        dialogManager.Show(dialogTexts);
    }


    void Story0_2TalkStart()
    {
        StartCoroutine(_Story0_2TalkStart());
    }
    IEnumerator _Story0_2TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "�� ��";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story0_2Talk();
    }
    void Story0_2Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[1];
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/���� â�� ���� �ִ� �ƺ� �۾��ǿ��� ������ ���ô� ȣ�����带 ã�Ҿ��. �̰� ���� Ÿ�� �ǳ���?", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sad/�̰� ������ �ƺ��� ���ô� ȣ�����屸��. ���Ƽ� ����� ���������� �𸣴µ� �������� �ʰڴ�?", "����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/ģ���� ���� ���� Ÿ������ ���Ƽ� �������ſ���. ��� Ÿ�� ���� �˷��� �������� ���Կ� ���� ����Կ�.", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷� ī��. ������ �� �����̴� �����ϰ� Ÿ���� ���� �� ��ó���� �������� Ÿ��..", "����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/�� ���� �׷� �ٳ�ðԿ�.. ^^", "ī��"));

        dialogManager.Show(dialogTexts);
    }


    void Story0_3TalkStart()
    {
        StartCoroutine(_Story0_3TalkStart());
    }
    IEnumerator _Story0_3TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "�˷����� ����������";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story0_3Talk();
    }
    void Story0_3Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[2];

        dialogTexts.Add(new DialogData("/speed:0.1/ī��. �������̳�?", "�˷���"));
        dialogTexts.Add(new DialogData("/speed:0.1/������ �ȳ��ϼ���. �ƺ��� ���ô� ȣ�� ���带 ã�Ҿ��. �ٵ� �� ź�� �ʹ� �����Ǿ �۵��� ������ �� �� �� ���庸������..", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/��...�� �� ���ڲٳ�...�̰� ����ũ�ο� �����̱���. �׶�뽺������ ó�� ���� ȣ���������./emote:Sad/�������� ���ε��� Ÿ�� �Ŷ� ������ ���� �������� �ʾ�...", "�˷���"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/�̰� ���� �ٷ� �� �� �ֳ���?", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�� ���� ������...��...�ٸ� ��ǰ�� �������� �ö�� �ν��ʹ� ��ü�� �ؾ� �� �� ������..������ ���Ѱ� ����.", "�˷���"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/�ö�� �ν��͸� ���� ����� �� ���� �ʿ��ұ��?", "ī��"));
        //dialogTexts.Add(new DialogData("/speed:0.1/�� �ν��ʹ� 300ũ����Ż �Ѱ� �鿩�� �������߰� ���� �� 100ũ����Ż ������ ������ �͵� ��������" +
         //   "�ʿ��� �ʹ� ����� �ʴ�? ���� �̰� ������ �߰�� �ȸ� ��ڴ�..50ũ����Ż ������ �� �� �ִµ�..", "�˷���"));

        var questionText = new DialogData("/speed:0.1/�� �ν��ʹ� 300ũ����Ż �Ѱ� �鿩�� ����...�߰� ���� �� 100ũ����Ż ������ ������ �͵� ������..." +
            "�ʿ��� �ʹ� ����� �ʴ�? ���� �̰� ������ �߰�� �ȸ� ��ڴ�..50ũ����Ż ������ �� �� �ִµ�..", "�˷���");

        questionText.SelectList.Add("Yes", "1. �߰�� �Ǵ�.");
        questionText.SelectList.Add("No", "2. �ƺ��� ��ǰ�̶� ������ �ִ´�.");
        questionText.Callback = () => Alex_Question();

        dialogTexts.Add(questionText);

        dialogManager.Show(dialogTexts);
    }
    void Alex_Question()
    {
        if (dialogManager.Result == "Yes")
        {
            var dialogTexts = new List<DialogData>();
            dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/�׷� �� �����ߴ�..�� ���� �� �� �� ���캸�ڲٳ�..��.." +
                "/emote:Normal/�̰� ������ ���� �ʹ� ������...20ũ����Ż ���� �ۿ��� �� �� �� ����..", "�˷���"));
            dialogTexts.Add(new DialogData("/speed:0.1//emote:Sad/�׷��Ÿ� ���� ���� ��������. �ƺ� ��ǰ�ε�...", "ī��"));
            dialogManager.Show(dialogTexts);
        }
        else if (dialogManager.Result.Equals("No"))
        {
            var dialogTexts = new List<DialogData>();
            dialogTexts.Add(new DialogData("/speed:0.1/��¿������. ������ �ٲ�ŵ� ã�ƿ�����.", "�˷���"));
            dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/��.. �ȳ����輼��.", "ī��"));
            dialogManager.Show(dialogTexts);
        }
    }


    void Story0_4TalkStart()
    {
        StartCoroutine(_Story0_4TalkStart());
    }
    IEnumerator _Story0_4TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "������ ��";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story0_4Talk();
    }
    void Story0_4Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[3];

        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/100ũ����Ż�̸�..���� ���� �ϴ� �ɺθ� �Ƹ�����Ʈ�δ� ��� ���� ���̾�...�ƽ���.." +
            "/emote:Sad/�ƺ��� ��ư�̴ٸ� ���� �����ּ����ٵ�..", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/������ �������� �쿬�� ����µ� �� ���尡 �ƺ��� �����ֽŰŴ�?", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1/�� �׷��� �ѵ� �������� ��������?", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�������� ������ ���� ������ Ÿ�� ����� �ݰ����� �׸� �� �Ұ��� ���� �ʰ� �ƴ�ô�� �߱���. " +
            "�� Ŭ���̶�� �Ѵܴ�. �ݰ���..", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1/�� �ȳ��ϼ���, �� ī�̶�� �ؿ�...", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷� �ݰ���. �� ���带 ���� ���� ������ ���±���. " +
            "�������� ���ν����� ���� Ÿ�� �ٴϴ� �������.", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/��...", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷��� �װ� ������ �ִ� ���忡�� �ö�� �ν��Ͱ� ���� �� ������. " +
            "�׷����� �������� �����ٵ�...", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/�ȱ׷��� �� �� ������ �������� �Դµ� ��ǰ�� �ʹ� ��μ���...", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷�����...Ȥ�� �������� ������ �ִ� �ν��Ͱ� �ִµ� �װŶ� �ٱ�?", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1/������ ������ �𸣴ºп��� �׷� ��� ������ �׳� ������ �������� ȥ���ŵ��...�˼��մϴ�..", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�Ǹ��� ��Ӵϸ� �ξ�����..�׷��ٸ� �̰� ���? �������� �ϴ� �������״� ��� " +
            "���� ��� ���߿� ������ ���� �ʰڴ�?", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/������ ���� �ϴ� �ɺθ� �Ƹ�����Ʈ�δ� �� ���� ���Ⱑ �������..", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�װ� �׷���...������ ȣ������ ��ȸ�� ������ ����� ������ �ݹ� �׵� ������ �� �� ����...", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/�� ���� �ڰݵ� ���°ɿ�...", "ī��"));
        //dialogTexts.Add(new DialogData("/speed:0.1/�ʰ��� ���ο� �������� ���� ������ ����� ��Ⱑ �ִܴ�. Ȥ�� ������ �ִٸ� ���� ���� 9�ÿ� ��Ƽ���������� ����. ", "�����"));

        var questionText = new DialogData("/speed:0.1/�ʰ��� ���ο� �������� ���� ������ ����� ��Ⱑ �ִܴ�. " +
            "Ȥ�� ������ �ִٸ� ���� ���� 9�ÿ� ��Ƽ���������� ����. ", "�����");
        questionText.SelectList.Add("Yes", "1. ��� ��ҷ� ������.");
        questionText.SelectList.Add("No", "2. �׳� ������ ����.");
        questionText.Callback = () => CityPark_Question();

        dialogTexts.Add(questionText);
        dialogManager.Show(dialogTexts);
    }
    void CityPark_Question()
    {
        if (dialogManager.Result.Equals("Yes"))
        {
            var dialogTexts = new List<DialogData>();
            Debug.Log("1__������");
            questCheck = "CityPark_1Check";
            dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/��. ���������մϴ�.", "ī��"));
            dialogManager.Show(dialogTexts);
        }
        else if (dialogManager.Result.Equals("No"))
        {
            var dialogTexts = new List<DialogData>();
            questCheck = "CityPark_2Check";
            dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/��. ���������մϴ�.", "ī��"));
            Debug.Log("2__������");
            dialogManager.Show(dialogTexts);
        }
    }


    void Story0_5TalkStart()
    {
        StartCoroutine(_Story0_5TalkStart());
    }
    IEnumerator _Story0_5TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "��Ƽ���� ��";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story0_5Talk();
    }
    void Story0_5Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[4];

        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/���� ��� �� ���ٵ�..", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/ī�̱� �����ѰŴ�?", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/�� ������ ���� �󸶳� �� �� �� ������ ������ ���� �� ���� ���;��� �� ���Ƽ���..", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷�. �� �����ߴ�. �Ƹ� �� �Ǹ��� �ڽ����۰� �� �� �����ž�?", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/�ڽ��� ���۰� ����?", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�Ƹ��� �༺���� ������ ȣ������ �ְ��� ��ȸ�� �����ϴ� �������� �ڽ����۶�� �Ѵܴ�.", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷�����. ���� �׷� �� ���� �ڽ����۰� �ǰ� �;��..", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷� �� �׷��� �ɰž�. �׸��� ���� ���� ���� �ö�� �ν��ʹ�. " +
            "�� ó���̴� �������� ��ġ���ٲ�..", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1/�� �����մϴ�. ��� �ν��� ����� ���� �����Կ�.", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷�..�ʶ�� 100ũ����Ż ������ ���� �� �� �����ž�...", "�����"));

        dialogManager.Show(dialogTexts);
    }


    void Story0_6TalkStart()
    {
        StartCoroutine(_Story0_6TalkStart());
    }
    IEnumerator _Story0_6TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "������ ��";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story0_6Talk();
    }
    void Story0_6Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[5];

        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/���� �ٳ�Ծ��..", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sick/(�ݷ� �ݷ�) �׷� ī�̱���", "����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/���� �ֱ׷���? ���� ���Ŀ�?", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sick/������ ������ �ǻ� �������� �ప �ܻ��� �ʹ� �зȴٰ� ���� ���� �� �� ���ٰ� �ϴ±���..", "����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sad/(������) ���� ���ݸ� ���ƿ� ���� �� �� ��帱����..", "ī��"));

        dialogManager.Show(dialogTexts);
    }


    void Story1_1TalkStart()
    {
        StartCoroutine(_Story1_1TalkStart());
    }
    IEnumerator _Story1_1TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "��Ƽ���� ��";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);
        Story1_1Talk();
    }
    void Story1_1Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[4];

        dialogTexts.Add(new DialogData("/speed:0.1/ī��, ���⼭ ȣ������ Ÿ�� ���� ������ڱ���.", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1/��. ������! ������ ���������.", "ī��"));

        dialogManager.Show(dialogTexts);
    }


    void Story1_2TalkStart()
    {
        StartCoroutine(_Story1_2TalkStart());
    }
    IEnumerator _Story1_2TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "��Ƽ���� ��";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story1_2Talk();
    }
    void Story1_2Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[6];

        dialogTexts.Add(new DialogData("/speed:0.1/���� �� ȣ�����忡 ����� �ֱ���. �̷��� ���� �����ϴ� ���� �����ٴ�. ��밡 �Ǵ±���.", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/�� ������ �����մϴ�. ���� ��Ʊ� ������ ���ݾ� �ͼ������� �� ���ƿ�.", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/���� �� �����̴� �Ƹ��� ��Ⱑ ���۵ɰž�. �ʴ� ù �����̴� 3�� ��⿡ ������û�� �ϸ� �ɰž�.", "�����"));

        dialogManager.Show(dialogTexts);
    }


    void Story1_3TalkStart()
    {
        StartCoroutine(_Story1_3TalkStart());
    }
    IEnumerator _Story1_3TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "������ �׸��� ����� ��";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story1_3Talk();
    }
    void Story1_3Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[6];

        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/������ �����ֽ� �ν��� ���п� ����� ���� ��Ҿ��. ���� �ν��� ��� 100ũ����Ż �帱����..", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷� ī�̾�. �������� ���� ��ұ���..���� �����...", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1/�����մϴ�. ������.", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/���� �ٸ� ��ȹ�� �ִ�?", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/���� ���� ���� ���� �� ���ƿ�. �� �� ������ �ؼ� ���� ������� �ప���� ���� ���� �������� �ؿ�.", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�ƺ��� �Ȱ�ô�?", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/��.. 2������ ���ư��̾��..", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/2����?  ��...", "�����"));

        dialogManager.Show(dialogTexts);
    }


    void Story1_4TalkStart()
    {
        StartCoroutine(_Story1_4TalkStart());
    }
    IEnumerator _Story1_4TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "������ �׸��� ����� ��";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story1_4Talk();
    }
    void Story1_4Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[6];

        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/�� ������ ������ ������� �ప�� ����Ұž�..", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/���� �ҳ�!!", "��"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/�ʴ¡�.���� ���� �����ߴ���..", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷� �ݰ��� �� ���̶�� ����..�ٵ� �ҳ� �Ƿ��� �� ������...������", "��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�ʵ� ������ �Ѵ���..", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/���� ��..�� Ǯ�� ������...�̷� ������ ���ִ� ��...�״�...", "��"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/����???", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/�̷� �̷� �̷� �칰�� ������ �ҳ�...�̷� ����� ���ִ� �ڽ������̶�� �̸��� ���̱⵵ �β�����...������...", "��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷� �ʴ� �Ƹ��¿��� ������ ��ȸ�� �����ϴ°ž�..", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/(���� �� �긮��) �翬����...������", "��"));
        dialogTexts.Add(new DialogData("/speed:0.1/���� ������ ���� �������� �� �𿴱���..", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/������ �� �ְ� �� ������� �����ϴٰ�...", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Angry/����..�� ������ �ҳ�..", "��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷��� �ƹ����� ���� ��⿡ ���ؼ��� ���� �����.", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷� �׶�뽺���� ���� ��Ⱑ �����°ǰ���?", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷�. ���⼭ �� �ֱ������� �縷���ÿ��� ���� ��Ⱑ �����ܴ�.", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/�ű� ���� �� �� ���� ���� �� �� �������? ������ �� �� ���� ������ �����帱 �� �����ٵ�...", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/���� ����̴� �翬�� �׷�����. ������ ��⿡ �����Ϸ��� �������� ��������� �ʿ��ϴܴ�. �˾ƺ��ַ�??", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1/�� ������ �׷��� ��������...", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�˰ڴ�. �׷��� ���� �� �� �˾ƺ��� �����ϸ�..", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/�����մϴ�. ������.", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/�׷� ����...��� ��...������...", "��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷��� �ư���..", "�����"));

        dialogManager.Show(dialogTexts);
    }


    void Story1_4_2TalkStart()
    {
        StartCoroutine(_Story1_4_2TalkStart());
    }
    IEnumerator _Story1_4_2TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "������ ��";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story1_4_2Talk();
    }
    void Story1_4_2Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[5];

        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/���� �ܻ��� �� ���Ҿ��. �ǻ� �������� ���� �����ֽ� ���̿���.", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Angry/(����) �ƴ� �̷� ���� �� ��� ����?", "����"));
        dialogTexts.Add(new DialogData("/speed:0.1/�ƺ��� �����ֽ� ȣ������� ���ֿ� ������ ź ����̿���. " +
            "������ �ϳ��� ���ϰ� �� ���̴� ���� ���ϼŵ� �ǿ�..", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sad/���� ������ ������ Ȥ�ó� �츮 ī�̰� ��ġ�� ������ �����̱���..", "����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/���������� ����. �� �긯�� �Ƶ� ī���ݾƿ�.. ^^", "ī��"));

        dialogManager.Show(dialogTexts);
    }


    void Story1_4_3TalkStart()
    {
        StartCoroutine(_Story1_4_3TalkStart());
    }
    IEnumerator _Story1_4_3TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "�� ��";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story1_4_3Talk();
    }
    void Story1_4_3Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[7];

        dialogTexts.Add(new DialogData("/speed:0.1/ī��. ������ ����忡 ������ �縷���� �����ڰ� �־��� ����̾�. �ʿ� �� ģ���� ������ ���Ҵٴ±��� ", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/������? �����̳׿�.", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷���. �Դٰ� �̹� ������ ����ڴ� �Ƹ��¿��� ������ �ڽ����� ��ȸ�� �����ǰ� ���ּ� ž�±��� �λ����� �ɷ��ִٴ±���.", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/�ų���! �׷� ���� �Ƹ��¿� �� �� �ִ°ǰ���?", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�縷���� ����忡�� ����ϴ� ���� ���� ������ �ʰ����� �����Ұž�.�׸��� ī�̾�. " +
            "�縷���� ���ֿ��� ���� ����� ����Ǿ� �־ ���� ���� �����ϴ� �ʿ��Դ� ���� �Ҹ��Ұž�.", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1/� �����ΰ���?", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/��κ� ���� �̻����� ���� �������. ���� �̻����� �°� �Ǹ� ������ ������ ������ ���������� ���尡 ���� ���� �ʰ� ����", "�����"));

        dialogManager.Show(dialogTexts);
    }


    void Story2_1TalkStart()
    {
        StartCoroutine(_Story2_1TalkStart());
    }
    IEnumerator _Story2_1TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "�� ��";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story2_1Talk();
    }
    void Story2_1Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[7];

        dialogTexts.Add(new DialogData("/speed:0.1/���� ���� ���� �̻����� ����� �� �ְ� �Ǿ��׿�.", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/���Ӱ� ���� �������� ����̻��� �߻���ġ����." +
            "�̻����� ��� �߿� Ʈ������ ȹ���� �� ����. �Ϲ� �߻����� ���� �߻����� ������ �� Ȱ���ϸ� �ȴ�.", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/����!!! ������~~", "��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�� �� ���� ���⿡!!", "ī��"));

        dialogManager.Show(dialogTexts);
    }


    void Story2_1_2TalkStart()
    {
        StartCoroutine(_Story2_1_2TalkStart());
    }
    IEnumerator _Story2_1_2TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "�縷�� �� ����� ��";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story2_1_2Talk();
    }
    void Story2_1_2Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[8];

        dialogTexts.Add(new DialogData("/speed:0.1/�����ϰ� ������ ���ٲ�. �縷 ���� ���ִ� �������� �ذ��, ��� �� 3���� �¸��ؾ� �Ѵ�. " +
            "�켱 4�� �������� ��ġ�� ���⿡�� 1���� �ذ������ ��������.", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/�������� �� ����ؾ� �ϴ±���.", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷���. ���������� 1���� �ؼ� �ذ������ ���� 1:1��⸦ �ϰ� �ǰ� ���⿡�� " +
            "�̱� ������� ����� 1: 1 ��⸦ ��ģ��. �ڽ�����?", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1/��", "ī��"));

        dialogManager.Show(dialogTexts);
    }


    void Story2_2TalkStart()
    {
        StartCoroutine(_Story2_2TalkStart());
    }
    IEnumerator _Story2_2TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "�縷�� �� ���� ����";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story2_2Talk();
    }
    void Story2_2Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[9];

        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/(ȥ�ڸ�)'�������� �ٵ� �ʹ� ������ ��ģ �� ����'", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/���� �ҳ�! ������� ���ɵ� �Ա�...", "��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�� ��� �Ǿ���??", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/���� �ذ�� ��밡 �ٷ� ����!! ������ �ҳ� ������!!", "��"));

        dialogManager.Show(dialogTexts);
    }


    void Story2_3TalkStart()
    {
        StartCoroutine(_Story2_3TalkStart());
    }
    IEnumerator _Story2_3TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "�縷�� �� ���� ����";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story2_3Talk();
    }
    void Story2_3Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[9];

        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/ó�� ���� �༮�ε�..��±��� �ö�Ա�.", "�˶�"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/�� ������?", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/���� �� �Ұ��� ����. �� �׶�뽺 �ְ��� ȣ������ ���̼� �˶����̴�.", "�˶�"));
        dialogTexts.Add(new DialogData("/speed:0.1/�� ī�̶�� ��. ȣ�����带 ź �� �� ���� �ʾ� �ٸ� ģ������ �� ����.", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/����...�̷� �̶߱� ģ���� ����̶�...��.... " +
            "�ƹ�ư ����� ���ϱ� ��ġ�� ���� �� ���� �Ͷ�.. ������", "�˶�"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷� ������ ���� ��⸦ ���� ���ڰ�..", "ī��"));

        dialogManager.Show(dialogTexts);
    }


    void Story3_1TalkStart()
    {
        StartCoroutine(_Story3_1TalkStart());
    }
    IEnumerator _Story3_1TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "�縷�� �� ����� ��";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story3_1Talk();
    }
    void Story3_1Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[8];

        dialogTexts.Add(new DialogData("/speed:0.1/���� ī�� �� ����� ���̾�. ù �������� ����̶��!!", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/�������� �����ֽ� �����̿���.", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷� �ϴ� �� ���� �Ƹ������� ���� ���ּ��� 3�� �� ����̴� �׵��� ��ġ�� ���� �غ� ���ϰŶ�.", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/���� �̶߱� ģ�� ������.", "�˶�"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/�˶�! ��������� ��ſ� ��⿴��. /emote:Normal/���� �̰ܼ� �� �̾��ϳ�..", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/�� �� �� ������ �̶߱����� ��ȸ�� �������", "�˶�"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/.....", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sad/�̶߱� ģ�� �� �� �� ����ذ���...", "�˶�"));
        dialogTexts.Add(new DialogData("/speed:0.1/(�������)������", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/��·�� ����������� ������. �� �� �����ζ� ǥ�� �缭 �Ƹ������� ������ ��.", "�˶�"));
        dialogTexts.Add(new DialogData("/speed:0.1/�������� �־�?", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�ʹ� ���� ����. �Ƹ��¿� ���� ������ ������ �־ �� ��⿡ ����ϸ� �Ƹ����佺Ÿ�� ������ �� ����.", "�˶�"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷�����.", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sad/�̶߱� �༮ ���� ���� ���� �����Ŵ�..������. /emote:Coloration/�ƹ�ư 3�� �ڿ� �Ƹ������� ���� ���ּ����� ����. " +
            "/emote:Normal/�Ƹ����佺Ÿ ����� ���� ������ �����̴� ���� ���� �غ� �ϰ�...", "�˶�"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷� �˶�. �Ƹ��¿����� �ְ��� ��⸦ ��ġ�ڱ�..", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷� �ҳ�. 3�� �ڿ� ����...", "��"));
        dialogTexts.Add(new DialogData("/speed:0.1/��!! �� �� ����??", "ī��"));

        dialogManager.Show(dialogTexts);
    }


    void Story3_1_2TalkStart()
    {
        StartCoroutine(_Story3_1_2TalkStart());
    }
    IEnumerator _Story3_1_2TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "��Ƽ���� ��";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story3_1_2Talk();
    }
    void Story3_1_2Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[4];

        dialogTexts.Add(new DialogData("/speed:0.1/ī��!! ������ ���??", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/���� �����?", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�ڳװ� ���� ���ư��� �濡 ��� ���ߴٴ� �ҽ��� ��� �޷�����.", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/���� ���� �־����� �����...", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/������ ũ�� ��ģ���� ���� ���̴µ� �Ӹ��� ���� �ε��� ����̱���.", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/�Ӹ��� ���ϰ� �����...", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/�.ž�±ǡ�ž�±��� ���������!!", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/ž�� ��ܿ� ����� �ȵǾ��ִ�?", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sad/�׷��� �ֳ���?", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�̷��̷�. ž�°� ����� �ߴٸ� ž�±��� ��� ��� �غ��ڴµ�. " +
            "����� �ȵ� ž�±��̸� �ٸ� ���� �ᵵ ��� ã�� ����� ���ܴ�.", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sad/........", "ī��"));

        dialogManager.Show(dialogTexts);
    }


    void Story3_1_3TalkStart()
    {
        StartCoroutine(_Story3_1_3TalkStart());
    }
    IEnumerator _Story3_1_3TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "ī���� ��";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story3_1_3Talk();
    }
    void Story3_1_3Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[10];

        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sad/(������ ǥ������) �� ������...", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sad/������ ���̰� ���� ���������ٵ�...", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/ī�̾� ���� ã�� �Դµ�...", "����"));

        dialogManager.Show(dialogTexts);
    }


    void Story3_1_4TalkStart()
    {
        StartCoroutine(_Story3_1_4TalkStart());
    }
    IEnumerator _Story3_1_4TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "�� ��";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story3_1_4Talk();
    }
    void Story3_1_4Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[7];

        dialogTexts.Add(new DialogData("/speed:0.1/ǥ���� ���� ��������...", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sad/���� �������� �ʾƼ� ������ ��ȸ�� ���Ĺ��Ⱦ��...", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/...��....ī�̾�..�� ���ɽ������ �ϴٸ�...", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/���� ��Ź �����Ű���?", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/..�װ�..��Ź�� �ƴϰ�...�ϰ� �Ƹ������� ���� ž�±��� ���� �� �ִ� ����� ���� ���°� �ƴ϶�...", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1/������? ��� �ϸ� �ǳ���?", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/Ȥ�� ���ϵ��ö�� ���ô�?", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1/���� ���� �ӿ� ������ ���� ���̿���?", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷� �� ������ �������� ���� ���ڻ���� ����������� ��⸦ �����ϰ� �־�.", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1/���� ȣ������ ���ֺ��� �ξ� ��ĥ�� ���������� �׷� ſ�� �� �αⰡ �ִܴ�. �� ���� ���� ū �ǵ����� ������..", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1/���� �� ���� ���� ���� �� �� �ִ°ǰ���?", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�׷���. ī�̴� ù �����̰� ������� �� �𸣴ϱ� �� ���� ������ �ɸ��ž�. " +
            "�ű⿡�� �߸� �ϸ� ��Ʋ �ȿ� ž�±� �� �� �ִ� ������ ���� �� �� �����Ŵ�..", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1/...��..�� �� �غ�����...", "ī��"));

        dialogManager.Show(dialogTexts);
    }


    void Story3_1_5TalkStart()
    {
        StartCoroutine(_Story3_1_5TalkStart());
    }
    IEnumerator _Story3_1_5TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "���ϵ����� ����� ��";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story3_1_5Talk();
    }
    void Story3_1_5Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[11];

        dialogTexts.Add(new DialogData("/speed:0.1/���ڻ���� �����ϴ� ����̱⶧���� �����鵵 ������ ���� �ϰ� �߰����� ���õ� ������ ���̾�. �� �����ϰ� �����ؾ� �Ѵ�.", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1/��. ���� �� �س��� ���ſ���.", "ī��"));

        dialogManager.Show(dialogTexts);
    }


    void StoryEndingTalkStart()
    {
        StartCoroutine(_StoryEndingTalkStart());
    }
    IEnumerator _StoryEndingTalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "ž���� ��";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        StoryEndingTalk();
    }
    void StoryEndingTalk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[6];

        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/��������� ���� �Ƹ������� �� �� �־�.", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/�����Ѵ� ī�̾�. �Ƹ����佺Ÿ���� �� ����ؼ� ���� ����.", "�����"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/���� �ҳ�. Ȥ�� ���ö� ������ ��Գ�? ȦȦȦ", "��"));
        dialogTexts.Add(new DialogData("/speed:0.1/���� ��¦�̾�!!", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1/���� �̶߱�..��Ʋ���� ���µ� �� ���� ����������..", "�˶�"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/�׷� ��� �� �ߴ�..", "ī��"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/'���̶��� �λ絵 �� ������ �Ա���..���̾� ���� �� ��Ź��..'", "ī��"));

        dialogManager.Show(dialogTexts);
    }

}
