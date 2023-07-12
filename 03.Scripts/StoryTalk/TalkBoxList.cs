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
    public GameObject roomTitlePanel;   //대화 방 이름 판넬
    public Image talkBackImg;   // 대화방 배경
    public Sprite[] talkBackSprite; //대화방 배경 사진

    public GameObject kaiObj;

    public string questCheck;   //선택사항에서 선택한 답변
    public bool talkEnd;    //대화 끝났는지 상태 - pause상태 알림


    public GameObject joyStickParent;    //튜토리얼에 들어가는 조이스틱 부모
    GameObject joyStick;    //튜토리얼에 들어가는 조이스틱




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
                Lobby_UIManager.instance.TalkBoxListShow(true); //토크박스 활성화
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

    //조이스틱 오브젝트 부모밑으로 넣어준다.
    IEnumerator JoypadParentsSetting()
    {
        yield return new WaitForSeconds(0.05f);
        joyStick = GameObject.Find("JoyPad Dual_1(Clone)");
        joyStick.transform.SetParent(joyStickParent.transform);
    }

    //조이스틱 활성화/비활성화
    public void JoPadFind_and_State(bool _state)
    {
        joyStickParent.transform.GetChild(0).gameObject.SetActive(_state);
    }

    // Update is called once per frame
    void Update()
    {
        //챕터 스토리 연속해서 가는 진행하는 구간
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

    //스토리 온오프 함수
    public void StoryPrgressONOFF()
    {
        //스토리 끄기
        if(PlayerPrefs.GetString("KS_StoryOnOff").Equals("Off"))
        {
            if(SceneManager.GetActiveScene().name.Equals("4.TutorialMap"))
            {
                if(PlayerPrefs.GetString("KS_OpenMap").Equals("1-1"))
                {
                    //선택한 챕터에 관한 스토리가 진행되어야 해서 선택한 챕터번호로 변경
                    UserDateManager.instance.SetStoryTalkProgress("1-1");
                    //아직 1-1챕터를 깨지 못한 상태라 스토리는 항시 나옴
                    dialogManager.gameObject.SetActive(true);
                    Story1_1TalkStart();
                }
                else
                {
                    dialogManager.gameObject.SetActive(false);
                    roomTitlePanel.SetActive(false);    //대화창제목판넬 비활성화
                    RaceManager.Instance.CountDownStart();
                }
            }
            else if(SceneManager.GetActiveScene().name.Equals("4.Map1"))
            {
                if(PlayerPrefs.GetString("KS_OpenMap").Equals("1-2"))
                {
                    //선택한 챕터에 관한 스토리가 진행되어야 해서 선택한 챕터번호로 변경
                    UserDateManager.instance.SetStoryTalkProgress("1-2");
                    dialogManager.gameObject.SetActive(true);
                    Story1_2TalkStart();
                }
                else if(PlayerPrefs.GetString("KS_OpenMap").Equals("1-3"))
                {
                    if(PlayerPrefs.GetString("KS_ChapterNumber").Equals("1-3"))
                    {
                        //선택한 챕터에 관한 스토리가 진행되어야 해서 선택한 챕터번호로 변경
                        UserDateManager.instance.SetStoryTalkProgress("1-3");
                        dialogManager.gameObject.SetActive(true);
                        Story1_3TalkStart();
                    }
                    else
                    {
                        dialogManager.gameObject.SetActive(false);
                        roomTitlePanel.SetActive(false);    //대화창제목판넬 비활성화
                        RaceManager.Instance.CountDownStart();
                    }
                }
                else if(PlayerPrefs.GetString("KS_OpenMap").Equals("1-4"))
                {
                    if(PlayerPrefs.GetString("KS_ChapterNumber").Equals("1-4"))
                    {
                        //선택한 챕터에 관한 스토리가 진행되어야 해서 선택한 챕터번호로 변경
                        UserDateManager.instance.SetStoryTalkProgress("1-4");
                        dialogManager.gameObject.SetActive(true);
                        Story1_4TalkStart();
                    }
                    else
                    {
                        dialogManager.gameObject.SetActive(false);
                        roomTitlePanel.SetActive(false);    //대화창제목판넬 비활성화
                        RaceManager.Instance.CountDownStart();
                    }
                }
                else
                {
                    dialogManager.gameObject.SetActive(false);
                    roomTitlePanel.SetActive(false);    //대화창제목판넬 비활성화
                    RaceManager.Instance.CountDownStart();
                }
            }
            else if(SceneManager.GetActiveScene().name.Equals("4.Map2"))
            {
                if(PlayerPrefs.GetString("KS_OpenMap").Equals("2-1"))
                {
                    //선택한 챕터에 관한 스토리가 진행되어야 해서 선택한 챕터번호로 변경
                    UserDateManager.instance.SetStoryTalkProgress("2-1");
                    dialogManager.gameObject.SetActive(true);
                    Story2_1TalkStart();
                }
                else if(PlayerPrefs.GetString("KS_OpenMap").Equals("2-2"))
                {
                    if(PlayerPrefs.GetString("KS_ChapterNumber").Equals("2-2"))
                    {
                        //선택한 챕터에 관한 스토리가 진행되어야 해서 선택한 챕터번호로 변경
                        UserDateManager.instance.SetStoryTalkProgress("2-2");
                        dialogManager.gameObject.SetActive(true);
                        Story2_2TalkStart();
                    }
                    else
                    {
                        dialogManager.gameObject.SetActive(false);
                        roomTitlePanel.SetActive(false);    //대화창제목판넬 비활성화
                        RaceManager.Instance.CountDownStart();
                    }
                }
                else if(PlayerPrefs.GetString("KS_OpenMap").Equals("2-3"))
                {
                    if(PlayerPrefs.GetString("KS_OpenMap").Equals("2-3"))
                    {
                        //선택한 챕터에 관한 스토리가 진행되어야 해서 선택한 챕터번호로 변경
                        UserDateManager.instance.SetStoryTalkProgress("2-3");
                        dialogManager.gameObject.SetActive(true);
                        Story2_3TalkStart();
                    }
                    else
                    {
                        dialogManager.gameObject.SetActive(false);
                        roomTitlePanel.SetActive(false);    //대화창제목판넬 비활성화
                        RaceManager.Instance.CountDownStart();
                    }
                }
                else
                {
                    dialogManager.gameObject.SetActive(false);
                    roomTitlePanel.SetActive(false);    //대화창제목판넬 비활성화
                    RaceManager.Instance.CountDownStart();
                }
            }
            else if(SceneManager.GetActiveScene().name.Equals("4.Map3"))
            {
                if(PlayerPrefs.GetString("KS_OpenMap").Equals("3-1"))
                {
                    //선택한 챕터에 관한 스토리가 진행되어야 해서 선택한 챕터번호로 변경
                    UserDateManager.instance.SetStoryTalkProgress("3-1");
                    dialogManager.gameObject.SetActive(true);
                    Story3_1TalkStart();
                }
                else
                {
                    dialogManager.gameObject.SetActive(false);
                    roomTitlePanel.SetActive(false);    //대화창제목판넬 비활성화
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
                //선택한 챕터에 관한 스토리가 진행되어야 해서 선택한 챕터번호로 변경
                UserDateManager.instance.SetStoryTalkProgress("1-1");
                dialogManager.gameObject.SetActive(true);
                Story1_1TalkStart();
            }
            else if (SceneManager.GetActiveScene().name.Equals("4.Map1"))
            {
                if(PlayerPrefs.GetString("KS_ChapterNumber").Equals("1-2"))
                {
                    //선택한 챕터에 관한 스토리가 진행되어야 해서 선택한 챕터번호로 변경
                    UserDateManager.instance.SetStoryTalkProgress("1-2");
                    dialogManager.gameObject.SetActive(true);
                    Story1_2TalkStart();
                }
                else if(PlayerPrefs.GetString("KS_ChapterNumber").Equals("1-3"))
                {
                    //선택한 챕터에 관한 스토리가 진행되어야 해서 선택한 챕터번호로 변경
                    UserDateManager.instance.SetStoryTalkProgress("1-3");
                    dialogManager.gameObject.SetActive(true);
                    Story1_3TalkStart();
                }
                else if(PlayerPrefs.GetString("KS_ChapterNumber").Equals("1-4"))
                {
                    //선택한 챕터에 관한 스토리가 진행되어야 해서 선택한 챕터번호로 변경
                    UserDateManager.instance.SetStoryTalkProgress("1-4");
                    dialogManager.gameObject.SetActive(true);
                    Story1_4TalkStart();
                }
            }
            else if (SceneManager.GetActiveScene().name.Equals("4.Map2"))
            {
                if(PlayerPrefs.GetString("KS_ChapterNumber").Equals("2-1"))
                {
                    //선택한 챕터에 관한 스토리가 진행되어야 해서 선택한 챕터번호로 변경
                    UserDateManager.instance.SetStoryTalkProgress("2-1");
                    dialogManager.gameObject.SetActive(true);
                    Story2_1TalkStart();
                }
                else if(PlayerPrefs.GetString("KS_ChapterNumber").Equals("2-2"))
                {
                    //선택한 챕터에 관한 스토리가 진행되어야 해서 선택한 챕터번호로 변경
                    UserDateManager.instance.SetStoryTalkProgress("2-2");
                    dialogManager.gameObject.SetActive(true);
                    Story2_2TalkStart();
                }
                else if (PlayerPrefs.GetString("KS_ChapterNumber").Equals("2-3"))
                {
                    //선택한 챕터에 관한 스토리가 진행되어야 해서 선택한 챕터번호로 변경
                    UserDateManager.instance.SetStoryTalkProgress("2-3");
                    dialogManager.gameObject.SetActive(true);
                    Story2_3TalkStart();
                }
            }
            else if(SceneManager.GetActiveScene().name.Equals("4.Map3"))
            {
                //선택한 챕터에 관한 스토리가 진행되어야 해서 선택한 챕터번호로 변경
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
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "창고";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);
        //JoPadFind_and_State(false);

        Story0_1Talk();
    }
    void Story0_1Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[0];

        dialogTexts.Add(new DialogData("/speed:0.1/어디보자...엄마가 찾아오라고 한 빗자루가 여기 어디 있을텐데...", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/직직찍", "쥐"));
        dialogTexts.Add(new DialogData("/speed:0.1/앗!! 이놈의 쥐가 어디서 나타났지?? 아하! 여기에 쥐 구멍이 있었네", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/반짝반짝", "작은열쇠"));
        dialogTexts.Add(new DialogData("/speed:0.1/이게 뭘까? 작은 열쇠잖아..이게 어디에 있다 나타난거지?? 일단 열쇠를 챙겨봐야겠어.", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/여기는 아빠의 작업실이었는데..열쇠가 없어진게 쥐 때문이었구나..", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/이 열쇠로 창고를 열어봐야겠어.", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/철컥! 끼이익", "문"));

        dialogManager.Show(dialogTexts);
    }


    void Story0_1_2TalkStart()
    {
        StartCoroutine(_Story0_1_2TalkStart());
    }
    IEnumerator _Story0_1_2TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "아빠의 연구실";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story0_1_2Talk();
    }
    void Story0_1_2Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[13];

        dialogTexts.Add(new DialogData("/speed:0.1/아빠가 돌아가시고 나서는 처음 들어오는 것 같아.", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/어! 서랍이 있네. 뭐가 들어있을까.. 첫 번째 서랍을 열어봐야겠어.", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/끼이익", "서랍 캐비닛"));
        dialogTexts.Add(new DialogData("/speed:0.1/촤라락", "아빠의 연구노트"));
        dialogTexts.Add(new DialogData("/speed:0.1/아빠의 연구노트군..보자...아센시오 이펙트..분광기...광 접속 이론..온통 어려운 이야기 뿐이구나", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/두 번째 서랍에는 뭐가 들어있을까?", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/끼이익", "서랍 캐비닛"));
        dialogTexts.Add(new DialogData("/speed:0.1/(몽글몽글)", "가족사진"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/아..이 사진 기억 나. 아주 어릴 때 아빠의 연구실에 구경 갔다가 찍은 사진이야..", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/그럼 마지막 서랍에는 뭐가 있는지 얼른 열어봐야겠어.", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/끼이익", "서랍 캐비닛"));
        dialogTexts.Add(new DialogData("/speed:0.1/반짝반짝", "캐비닛열쇠"));
        dialogTexts.Add(new DialogData("/speed:0.1/어랏. 또 열쇠가 있네..이건 캐비닛 열쇠 같은데..이걸로 캐비닛을 열어봐야지..", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/철컹! 끼이익", "서랍 캐비닛"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/이야 멋진 호버보드인걸..친구들이 타고 다닐때는 부러웠는데..이제 내 호버보드가 생기는건가?", "카이"));

        dialogManager.Show(dialogTexts);
    }


    void Story0_2TalkStart()
    {
        StartCoroutine(_Story0_2TalkStart());
    }
    IEnumerator _Story0_2TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "집 안";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story0_2Talk();
    }
    void Story0_2Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[1];
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/엄마 창고 옆에 있는 아빠 작업실에서 예전에 쓰시던 호버보드를 찾았어요. 이거 제가 타도 되나요?", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sad/이건 예전에 아빠가 쓰시던 호버보드구나. 낡아서 제대로 움직일지도 모르는데 위험하지 않겠니?", "엄마"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/친구들 보드 빌려 타본적이 많아서 괜찮을거예요. 대신 타기 전에 알렉스 아저씨네 가게에 가서 물어볼게요.", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/그래 카이. 엄마는 늘 걱정이니 위험하게 타지는 말고 집 근처에서 조심조심 타렴..", "엄마"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/네 엄마 그럼 다녀올게요.. ^^", "카이"));

        dialogManager.Show(dialogTexts);
    }


    void Story0_3TalkStart()
    {
        StartCoroutine(_Story0_3TalkStart());
    }
    IEnumerator _Story0_3TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "알렉스의 만물수리점";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story0_3Talk();
    }
    void Story0_3Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[2];

        dialogTexts.Add(new DialogData("/speed:0.1/카이. 무슨일이냐?", "알렉스"));
        dialogTexts.Add(new DialogData("/speed:0.1/아저씨 안녕하세요. 아빠가 쓰시던 호버 보드를 찾았어요. 근데 안 탄지 너무 오래되어서 작동이 가능할 지 한 번 여쭤보려구요..", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/음...한 번 보자꾸나...이건 윈드크로우 기종이구나. 테라노스에서는 처음 만든 호버보드란다./emote:Sad/예전에는 군인들이 타던 거라 지금은 거의 남아있질 않아...", "알렉스"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/이거 지금 바로 날 수 있나요?", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/좀 살펴 봐야지...음...다른 부품은 괜찮은데 플라즈마 부스터는 교체를 해야 할 것 같구나..수명이 다한것 같아.", "알렉스"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/플라즈마 부스터를 새로 살려면 얼마 정도 필요할까요?", "카이"));
        //dialogTexts.Add(new DialogData("/speed:0.1/새 부스터는 300크리스탈 넘게 들여야 하지…중고를 쓰면 한 100크리스탈 정도면 가능할 것도 같은데…" +
         //   "너에겐 너무 비싸지 않니? 차라리 이걸 나에게 중고로 팔면 어떻겠니..50크리스탈 정도는 줄 수 있는데..", "알렉스"));

        var questionText = new DialogData("/speed:0.1/새 부스터는 300크리스탈 넘게 들여야 하지...중고를 쓰면 한 100크리스탈 정도면 가능할 것도 같은데..." +
            "너에겐 너무 비싸지 않니? 차라리 이걸 나에게 중고로 팔면 어떻겠니..50크리스탈 정도는 줄 수 있는데..", "알렉스");

        questionText.SelectList.Add("Yes", "1. 중고로 판다.");
        questionText.SelectList.Add("No", "2. 아빠의 유품이라 가지고 있는다.");
        questionText.Callback = () => Alex_Question();

        dialogTexts.Add(questionText);

        dialogManager.Show(dialogTexts);
    }
    void Alex_Question()
    {
        if (dialogManager.Result == "Yes")
        {
            var dialogTexts = new List<DialogData>();
            dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/그래 잘 생각했다..그 전에 한 번 더 살펴보자꾸나..음.." +
                "/emote:Normal/이거 수리할 곳이 너무 많은데...20크리스탈 정도 밖에는 못 줄 것 같아..", "알렉스"));
            dialogTexts.Add(new DialogData("/speed:0.1//emote:Sad/그럴거면 차라리 제가 가질래요. 아빠 유품인데...", "카이"));
            dialogManager.Show(dialogTexts);
        }
        else if (dialogManager.Result.Equals("No"))
        {
            var dialogTexts = new List<DialogData>();
            dialogTexts.Add(new DialogData("/speed:0.1/어쩔수없지. 생각이 바뀌거든 찾아오더라.", "알렉스"));
            dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/네.. 안녕히계세요.", "카이"));
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
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "수리점 앞";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story0_4Talk();
    }
    void Story0_4Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[3];

        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/100크리스탈이면..내가 지금 하는 심부름 아르바이트로는 어림도 없는 돈이야...아쉬워.." +
            "/emote:Sad/아빠가 살아계셨다면 벌써 고쳐주셨을텐데..", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/꼬마야 지나가다 우연히 들었는데 이 보드가 아빠가 남겨주신거니?", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1/네 그렇긴 한데 아저씨는 누구세요?", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/아저씨도 예전에 군인 시절에 타던 보드라서 반가워서 그만 내 소개도 하지 않고 아는척을 했구나. " +
            "난 클락이라고 한단다. 반가워..", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1/네 안녕하세요, 전 카이라고 해요...", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/그래 반가워. 이 보드를 보니 예전 생각이 나는구나. " +
            "아저씨도 군인시절에 많이 타고 다니던 보드란다.", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/네...", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/그런데 네가 가지고 있는 보드에는 플라즈마 부스터가 없는 것 같구나. " +
            "그래서는 움직이지 않을텐데...", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/안그래도 그 것 때문에 수리점에 왔는데 부품이 너무 비싸서요...", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/그렇구나...혹시 아저씨가 가지고 있던 부스터가 있는데 그거라도 줄까?", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1/말씀은 고맙지만 모르는분에게 그런 비싼 물건을 그냥 받으면 엄마에게 혼나거든요...죄송합니다..", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/훌륭한 어머니를 두었구나..그렇다면 이건 어떠냐? 아저씨가 일단 빌려줄테니 대신 " +
            "돈을 벌어서 나중에 갚으면 되지 않겠니?", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/하지만 제가 하는 심부름 아르바이트로는 그 돈을 갚기가 어려워요..", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/그건 그렇지...하지만 호버보드 대회에 나가서 상금을 받으면 금방 그돈 정도는 벌 수 있지...", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/전 출전 자격도 없는걸요...", "카이"));
        //dialogTexts.Add(new DialogData("/speed:0.1/너같은 새로운 선수들을 위해 열리는 비공식 경기가 있단다. 혹시 생각이 있다면 오늘 저녁 9시에 시티공원앞으로 오렴. ", "언노운맨"));

        var questionText = new DialogData("/speed:0.1/너같은 새로운 선수들을 위해 열리는 비공식 경기가 있단다. " +
            "혹시 생각이 있다면 오늘 저녁 9시에 시티공원앞으로 오렴. ", "언노운맨");
        questionText.SelectList.Add("Yes", "1. 약속 장소로 나간다.");
        questionText.SelectList.Add("No", "2. 그냥 집에서 쉰다.");
        questionText.Callback = () => CityPark_Question();

        dialogTexts.Add(questionText);
        dialogManager.Show(dialogTexts);
    }
    void CityPark_Question()
    {
        if (dialogManager.Result.Equals("Yes"))
        {
            var dialogTexts = new List<DialogData>();
            Debug.Log("1__번선택");
            questCheck = "CityPark_1Check";
            dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/네. 말씀감사합니다.", "카이"));
            dialogManager.Show(dialogTexts);
        }
        else if (dialogManager.Result.Equals("No"))
        {
            var dialogTexts = new List<DialogData>();
            questCheck = "CityPark_2Check";
            dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/네. 말씀감사합니다.", "카이"));
            Debug.Log("2__번선택");
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
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "시티공원 앞";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story0_5Talk();
    }
    void Story0_5Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[4];

        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/여기 어디 쯤 될텐데..", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/카이군 결정한거니?", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/네 아직은 제가 얼마나 잘 할 수 있을지 모르지만 왠지 이 곳에 나와야할 것 같아서요..", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/그래. 잘 생각했다. 아마 넌 훌륭한 코스모서퍼가 될 수 있을거야?", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/코스모 서퍼가 뭐죠?", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/아리온 행성에서 열리는 호버보드 최고의 대회에 출전하는 선수들을 코스모서퍼라고 한단다.", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1/그렇군요. 저도 그럼 꼭 멋진 코스모서퍼가 되고 싶어요..", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/그래 꼭 그렇게 될거야. 그리고 여기 내가 말한 플라즈마 부스터다. " +
            "넌 처음이니 아저씨가 설치해줄께..", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1/네 감사합니다. 대신 부스터 비용은 빨리 갚을게요.", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/그럼..너라면 100크리스탈 정도는 쉽게 벌 수 있을거야...", "언노운맨"));

        dialogManager.Show(dialogTexts);
    }


    void Story0_6TalkStart()
    {
        StartCoroutine(_Story0_6TalkStart());
    }
    IEnumerator _Story0_6TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "엄마의 방";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story0_6Talk();
    }
    void Story0_6Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[5];

        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/엄마 다녀왔어요..", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sick/(콜록 콜록) 그래 카이구나", "엄마"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/엄마 왜그래요? 많이 아파요?", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sick/병원에 갔더니 의사 선생님이 약값 외상이 너무 밀렸다고 이제 약을 줄 수 없다고 하는구나..", "엄마"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sad/(속으로) 엄마 조금만 참아요 제가 약 꼭 사드릴께요..", "카이"));

        dialogManager.Show(dialogTexts);
    }


    void Story1_1TalkStart()
    {
        StartCoroutine(_Story1_1TalkStart());
    }
    IEnumerator _Story1_1TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "시티공원 앞";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);
        Story1_1Talk();
    }
    void Story1_1Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[4];

        dialogTexts.Add(new DialogData("/speed:0.1/카이, 여기서 호버보드 타는 법을 배워보자구나.", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1/네. 아저씨! 열심히 배워볼께요.", "카이"));

        dialogManager.Show(dialogTexts);
    }


    void Story1_2TalkStart()
    {
        StartCoroutine(_Story1_2TalkStart());
    }
    IEnumerator _Story1_2TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "시티공원 앞";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story1_2Talk();
    }
    void Story1_2Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[6];

        dialogTexts.Add(new DialogData("/speed:0.1/역시 넌 호버보드에 재능이 있구나. 이렇게 빨리 운전하는 법을 익히다니. 기대가 되는구나.", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/네 아저씨 감사합니다. 아직 어렵긴 하지만 조금씩 익숙해지는 것 같아요.", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/이제 곧 자정이니 아마도 경기가 시작될거야. 너는 첫 출전이니 3인 경기에 참가신청을 하면 될거야.", "언노운맨"));

        dialogManager.Show(dialogTexts);
    }


    void Story1_3TalkStart()
    {
        StartCoroutine(_Story1_3TalkStart());
    }
    IEnumerator _Story1_3TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "도시의 그림자 경기장 앞";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story1_3Talk();
    }
    void Story1_3Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[6];

        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/아저씨 빌려주신 부스터 덕분에 상금을 많이 모았어요. 여기 부스터 비용 100크리스탈 드릴께요..", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/그래 카이야. 생각보다 빨리 모았구나..역시 대단해...", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1/감사합니다. 아저씨.", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/이제 다른 계획은 있니?", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/이제 감을 조금 잡은 것 같아요. 좀 더 열심히 해서 엄마 병원비와 약값으로 생긴 빚도 갚으려고 해요.", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/아빠는 안계시니?", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/네.. 2년전에 돌아가셨어요..", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/2년전?  음...", "언노운맨"));

        dialogManager.Show(dialogTexts);
    }


    void Story1_4TalkStart()
    {
        StartCoroutine(_Story1_4TalkStart());
    }
    IEnumerator _Story1_4TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "도시의 그림자 경기장 앞";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story1_4Talk();
    }
    void Story1_4Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[6];

        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/이 정도면 엄마의 병원비랑 약값은 충분할거야..", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/어이 소년!!", "젠"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/너는….나랑 같이 경주했던…..", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/그래 반가워 난 젠이라고 하지..근데 소년 실력이 꽤 괜찮아...훌훌훌", "젠"));
        dialogTexts.Add(new DialogData("/speed:0.1/너도 보통은 넘던데..", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/나야 뭐..몸 풀기 정도지...이런 허접한 경주는 뭐...그닥...", "젠"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/허접???", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/이런 이런 이런 우물안 개구리 소년...이런 비공식 경주는 코스모서핑이라는 이름을 붙이기도 부끄럽지...훌훌훌...", "젠"));
        dialogTexts.Add(new DialogData("/speed:0.1/그럼 너는 아리온에서 열리는 대회에 참가하는거야..", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/(식은 땀 흘리며) 당연하지...훌훌훌", "젠"));
        dialogTexts.Add(new DialogData("/speed:0.1/여기 오늘의 출전 선수들이 다 모였구나..", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/아저씨 이 애가 이 경기장은 허접하다고...", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Angry/뭐냐..입 가벼운 소년..", "젠"));
        dialogTexts.Add(new DialogData("/speed:0.1/그렇지 아무래도 공식 경기에 비해서는 많이 어설프지.", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1/그럼 테라노스에도 공식 경기가 열리는건가요?", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/그럼. 여기서 좀 멀긴하지만 사막도시에서 정식 경기가 열린단다.", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/거기 가면 좀 더 많은 돈을 벌 수 있을까요? 엄마를 좀 더 좋은 병원에 보내드릴 수 있을텐데...", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/공식 경기이니 당연히 그렇겠지. 하지만 경기에 참가하려면 정식으로 선수등록이 필요하단다. 알아봐주련??", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1/네 아저씨 그러면 감사하죠...", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/알겠다. 그러면 내가 한 번 알아보고 연락하마..", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/감사합니다. 아저씨.", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/그럼 나도...어떻게 좀...훌훌훌...", "젠"));
        dialogTexts.Add(new DialogData("/speed:0.1/그래요 아가씨..", "언노운맨"));

        dialogManager.Show(dialogTexts);
    }


    void Story1_4_2TalkStart()
    {
        StartCoroutine(_Story1_4_2TalkStart());
    }
    IEnumerator _Story1_4_2TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "엄마의 방";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story1_4_2Talk();
    }
    void Story1_4_2Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[5];

        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/엄마 외상값을 다 갚았어요. 의사 선생님이 새로 지어주신 약이에요.", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Angry/(놀라며) 아니 이런 돈이 다 어디서 났니?", "엄마"));
        dialogTexts.Add(new DialogData("/speed:0.1/아빠가 물려주신 호버보드로 경주에 나가서 탄 상금이에요. " +
            "나쁜짓 하나도 안하고 번 돈이니 걱정 안하셔도 되요..", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sad/고맙긴 하지만 엄마는 혹시나 우리 카이가 다치지 않을까 걱정이구나..", "엄마"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/걱정마세요 엄마. 전 브릭의 아들 카이잖아요.. ^^", "카이"));

        dialogManager.Show(dialogTexts);
    }


    void Story1_4_3TalkStart()
    {
        StartCoroutine(_Story1_4_3TalkStart());
    }
    IEnumerator _Story1_4_3TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "집 앞";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story1_4_3Talk();
    }
    void Story1_4_3Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[7];

        dialogTexts.Add(new DialogData("/speed:0.1/카이. 지난번 경기장에 다행히 사막경주 관계자가 있었던 모양이야. 너와 네 친구를 눈여겨 보았다는구나 ", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/정말요? 다행이네요.", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/그렇지. 게다가 이번 시즌의 우승자는 아리온에서 열리는 코스모서핑 대회의 출전권과 우주선 탑승권이 부상으로 걸려있다는구나.", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/신난다! 그럼 저도 아리온에 갈 수 있는건가요?", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/사막도시 경기장에서 우승하는 일이 결코 쉽지는 않겠지만 가능할거야.그리고 카이야. " +
            "사막도시 경주에는 무기 사용이 허락되어 있어서 무기 없이 참가하는 너에게는 아주 불리할거야.", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1/어떤 무기인가요?", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/대부분 보드 미사일을 많이 사용하지. 보드 미사일을 맞게 되면 생명에는 지장이 없지만 순간적으로 보드가 말을 듣지 않게 되지", "언노운맨"));

        dialogManager.Show(dialogTexts);
    }


    void Story2_1TalkStart()
    {
        StartCoroutine(_Story2_1TalkStart());
    }
    IEnumerator _Story2_1TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "집 앞";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story2_1Talk();
    }
    void Story2_1Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[7];

        dialogTexts.Add(new DialogData("/speed:0.1/드디어 저도 보드 미사일을 사용할 수 있게 되었네요.", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/새롭게 얻은 이이템은 보드미사일 발사장치란다." +
            "미사일은 경기 중에 트랙에서 획득할 수 있지. 일반 발사형과 추적 발사형이 있으니 잘 활용하면 된다.", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/고고고!!! 훌훌훌~~", "젠"));
        dialogTexts.Add(new DialogData("/speed:0.1/헉 넌 언제 여기에!!", "카이"));

        dialogManager.Show(dialogTexts);
    }


    void Story2_1_2TalkStart()
    {
        StartCoroutine(_Story2_1_2TalkStart());
    }
    IEnumerator _Story2_1_2TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "사막의 끝 경기장 앞";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story2_1_2Talk();
    }
    void Story2_1_2Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[8];

        dialogTexts.Add(new DialogData("/speed:0.1/간단하게 설명을 해줄께. 사막 도시 경주는 예선전과 준결승, 결승 총 3번을 승리해야 한다. " +
            "우선 4명씩 예선전을 펼치고 여기에서 1등이 준결승으로 진출하지.", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/예선전을 꼭 통과해야 하는군요.", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/그렇지. 예선전에서 1등을 해서 준결승으로 가면 1:1경기를 하게 되고 여기에서 " +
            "이긴 사람들이 결승전 1: 1 경기를 펼친다. 자신있지?", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1/넵", "카이"));

        dialogManager.Show(dialogTexts);
    }


    void Story2_2TalkStart()
    {
        StartCoroutine(_Story2_2TalkStart());
    }
    IEnumerator _Story2_2TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "사막의 끝 선수 대기실";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story2_2Talk();
    }
    void Story2_2Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[9];

        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/(혼자말)'생각보다 다들 너무 빠르고 거친 것 같아'", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/어이 소년! 여기까지 용케도 왔군...", "젠"));
        dialogTexts.Add(new DialogData("/speed:0.1/넌 어떻게 되었어??", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/너의 준결승 상대가 바로 나라구!! 훌훌훌 소년 각오해!!", "젠"));

        dialogManager.Show(dialogTexts);
    }


    void Story2_3TalkStart()
    {
        StartCoroutine(_Story2_3TalkStart());
    }
    IEnumerator _Story2_3TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "사막의 끝 선수 대기실";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story2_3Talk();
    }
    void Story2_3Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[9];

        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/처음 보는 녀석인데..결승까지 올라왔군.", "알란"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/넌 누구냐?", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/흐흐 내 소개를 하지. 난 테라노스 최고의 호버보드 레이서 알란님이다.", "알란"));
        dialogTexts.Add(new DialogData("/speed:0.1/난 카이라고 해. 호버보드를 탄 지 얼마 되지 않아 다른 친구들은 잘 몰라.", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/어허...이런 촌뜨기 친구가 결승이라...음.... " +
            "아무튼 우승은 나니까 다치지 말고 잘 따라 와라.. ㅋㅋㅋ", "알란"));
        dialogTexts.Add(new DialogData("/speed:0.1/그래 끝까지 좋은 경기를 펼쳐 보자고..", "카이"));

        dialogManager.Show(dialogTexts);
    }


    void Story3_1TalkStart()
    {
        StartCoroutine(_Story3_1TalkStart());
    }
    IEnumerator _Story3_1TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "사막의 끝 경기장 앞";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story3_1Talk();
    }
    void Story3_1Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[8];

        dialogTexts.Add(new DialogData("/speed:0.1/역시 카이 넌 대단한 아이야. 첫 출전에서 우승이라니!!", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/아저씨가 도와주신 덕분이에요.", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/그래 일단 잘 쉬고 아리온으로 가는 우주선은 3일 뒤 출발이니 그동안 다치지 말고 준비 잘하거라.", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/어이 촌뜨기 친구 축하해.", "알란"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/알란! 힘들었지만 즐거운 경기였다. /emote:Normal/내가 이겨서 좀 미안하네..", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/뭐 한 번 정도야 촌뜨기한테 기회를 줘야지…", "알란"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/.....", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sad/촌뜨기 친구 기 한 번 살려준거지...", "알란"));
        dialogTexts.Add(new DialogData("/speed:0.1/(노려보며)흐흐흐", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/어쨌든 결승전에서는 졌지만. 난 내 돈으로라도 표를 사서 아리온으로 갈려고 해.", "알란"));
        dialogTexts.Add(new DialogData("/speed:0.1/출전권이 있어?", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/너무 걱정 말라구. 아리온에 가면 별도의 예선이 있어서 그 경기에 통과하면 아리오페스타에 참가할 수 있지.", "알란"));
        dialogTexts.Add(new DialogData("/speed:0.1/그렇구나.", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sad/촌뜨기 녀석 아직 한참 배울게 많을거다..흐흐흐. /emote:Coloration/아무튼 3일 뒤에 아리온으로 가는 우주선에서 보자. " +
            "/emote:Normal/아리오페스타 우승은 내가 차지할 예정이니 축하 해줄 준비 하고...", "알란"));
        dialogTexts.Add(new DialogData("/speed:0.1/그래 알란. 아리온에서도 최고의 경기를 펼치자구..", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/그래 소년. 3일 뒤에 보자...", "젠"));
        dialogTexts.Add(new DialogData("/speed:0.1/헉!! 넌 또 언제??", "카이"));

        dialogManager.Show(dialogTexts);
    }


    void Story3_1_2TalkStart()
    {
        StartCoroutine(_Story3_1_2TalkStart());
    }
    IEnumerator _Story3_1_2TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "시티공원 앞";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story3_1_2Talk();
    }
    void Story3_1_2Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[4];

        dialogTexts.Add(new DialogData("/speed:0.1/카이!! 정신이 들어??", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/여긴 어디죠?", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/자네가 집에 돌아가는 길에 사고를 당했다는 소식을 듣고 달려왔지.", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/무슨 일이 있었는지 기억이...", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/다행이 크게 다친데는 없어 보이는데 머리를 세게 부딪힌 모양이구나.", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/머리가 띵하게 울려요...", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/어….탑승권…탑승권이 없어졌어요!!", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/탑승 명단에 등록이 안되어있니?", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sad/그런게 있나요?", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/이런이런. 탑승객 등록을 했다면 탑승권이 없어도 어떻게 해보겠는데. " +
            "등록이 안된 탑승권이면 다른 누가 써도 어떻게 찾을 방법이 없단다.", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sad/........", "카이"));

        dialogManager.Show(dialogTexts);
    }


    void Story3_1_3TalkStart()
    {
        StartCoroutine(_Story3_1_3TalkStart());
    }
    IEnumerator _Story3_1_3TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "카이의 방";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story3_1_3Talk();
    }
    void Story3_1_3Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[10];

        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sad/(낙담한 표정으로) 다 끝났어...", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sad/엄마랑 주이가 정말 좋아했을텐데...", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/카이야 누가 찾아 왔는데...", "엄마"));

        dialogManager.Show(dialogTexts);
    }


    void Story3_1_4TalkStart()
    {
        StartCoroutine(_Story3_1_4TalkStart());
    }
    IEnumerator _Story3_1_4TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "집 앞";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story3_1_4Talk();
    }
    void Story3_1_4Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[7];

        dialogTexts.Add(new DialogData("/speed:0.1/표정이 많이 안좋구나...", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Sad/제가 조심하질 않아서 소중한 기회를 놓쳐버렸어요...", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/...음....카이야..좀 조심스럽기는 하다만...", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Worry/무슨 부탁 있으신가요?", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/..그게..부탁은 아니고...니가 아리온으로 가는 탑승권을 구할 수 있는 방법이 아주 없는건 아니라서...", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1/정말요? 어떻게 하면 되나요?", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/혹시 지하도시라고 들어봤니?", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1/지하 동굴 속에 지어진 도시 말이예요?", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/그래 그 곳에는 얼마전부터 전문 도박사들이 비공식적으로 경기를 개최하고 있어.", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1/정식 호버보드 경주보다 훨씬 거칠고 위험하지만 그런 탓에 꽤 인기가 있단다. 그 덕에 제법 큰 판돈들이 오고가지..", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1/저도 그 곳에 가서 돈을 벌 수 있는건가요?", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/그렇지. 카이는 첫 출전이고 사람들이 잘 모르니까 꽤 높은 배당금이 걸릴거야. " +
            "거기에서 잘만 하면 이틀 안에 탑승권 살 수 있는 정도의 돈을 벌 수 있을거다..", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1/...저..한 번 해볼께요...", "카이"));

        dialogManager.Show(dialogTexts);
    }


    void Story3_1_5TalkStart()
    {
        StartCoroutine(_Story3_1_5TalkStart());
    }
    IEnumerator _Story3_1_5TalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "지하도시의 경기장 앞";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        Story3_1_5Talk();
    }
    void Story3_1_5Talk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[11];

        dialogTexts.Add(new DialogData("/speed:0.1/도박사들이 개최하는 경기이기때문에 선수들도 참가비를 내야 하고 추가적인 배팅도 가능한 곳이야. 잘 생각하고 참가해야 한다.", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1/네. 저는 꼭 해내고 말거예요.", "카이"));

        dialogManager.Show(dialogTexts);
    }


    void StoryEndingTalkStart()
    {
        StartCoroutine(_StoryEndingTalkStart());
    }
    IEnumerator _StoryEndingTalkStart()
    {
        roomTitlePanel.SetActive(true);
        roomTitlePanel.transform.GetChild(0).GetComponent<Text>().text = "탑승장 앞";
        yield return new WaitForSeconds(1f);
        roomTitlePanel.SetActive(false);

        StoryEndingTalk();
    }
    void StoryEndingTalk()
    {
        var dialogTexts = new List<DialogData>();
        talkBackImg.sprite = talkBackSprite[6];

        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/힘들었지만 이제 아리온으로 갈 수 있어.", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/축하한다 카이야. 아리오페스타에서 꼭 우승해서 돌아 오렴.", "언노운맨"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/어이 소년. 혹시 도시락 같은거 사왔나? 홀홀홀", "젠"));
        dialogTexts.Add(new DialogData("/speed:0.1/아이 깜짝이야!!", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1/어이 촌뜨기..이틀만에 보는데 그 사이 못생겨졌군..", "알란"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Happy/그려 고생 좀 했다..", "카이"));
        dialogTexts.Add(new DialogData("/speed:0.1//emote:Coloration/'주이랑은 인사도 못 나누고 왔구나..주이야 엄마 잘 부탁해..'", "카이"));

        dialogManager.Show(dialogTexts);
    }

}
