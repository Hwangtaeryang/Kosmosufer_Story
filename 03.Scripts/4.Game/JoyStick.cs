using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    //이 스크립트는 백그라운드에 어사인 해줌
    //터치에 반응할 부분은 백그라운드이기 때문

    //움직이는 범위를 제한하기 위해서 선언함
    [SerializeField] private RectTransform rect_Background;
    [SerializeField] private RectTransform rect_Joystick;

    //백그라운드의 반지름의 범위를 저장시킬 변수
    private float radius;

    //움직일 속도
    [SerializeField] private float moveSpeed;

    //터치가 시작됐을 때 움직이거라
    private bool isTouch = false;
    //캐릭터 회전값을 만들기위해 value를 전역변수로 설정함
    private Vector2 value;



    public bool left, right;

    void Start()
    {
        //inspector에 그 rect Transform에 접근하는 거 맞음
        //0.5를 곱해서 반지름을 구해서 값을 넣어줌
        this.radius = rect_Background.rect.width * 0.5f;

    }

    //이동 구현
    void Update()
    {
        if (this.isTouch)
        {
            Debug.Log("조이스틱 움직임");
            //조이스틱 방향으로 캐릭터 회전
            //if (this.value != null)
            {
                //Debug.Log("방향 - : " + Mathf.Atan2(this.value.x, this.value.y) * Mathf.Rad2Deg);

                //왼쪽
                if((Mathf.Atan2(this.value.x, this.value.y) * Mathf.Rad2Deg) < 0)
                {
                    left = true;
                    right = false;
                    Debug.Log("왼쪽");
                    //LeftTurn.instance._on = true;
                    //RightTurn.instance._on = false;
                    //LeftTurn.instance.LeftTurn_Move();
                }
                //오른쪽
                else if((Mathf.Atan2(this.value.x, this.value.y) * Mathf.Rad2Deg) > 0)
                {
                    left = false;
                    right = true;
                    Debug.Log("오른쪽");
                    //RightTurn.instance._on = true;
                    //LeftTurn.instance._on = false;
                    //RightTurn.instance.RightTurn_Move();
                    
                }
            }
        }
    }



    //인터페이스 구현

    //눌렀을 때(터치가 시작됐을 때)
    public void OnPointerDown(PointerEventData eventData)
    {
        this.isTouch = true;

    }

    //손 땠을 때
    public void OnPointerUp(PointerEventData eventData)
    {
        //손 땠을 때 원위치로 돌리기
        rect_Joystick.localPosition = Vector3.zero;

        this.isTouch = false;
    }

    //드래그 했을때
    public void OnDrag(PointerEventData eventData)
    {
        //마우스 포지션(x축, y축만 있어서 벡터2)
        //마우스 좌표에서 검은색 백그라운드 좌표값을 뺀 값만큼 조이스틱(흰 동그라미)를 움직일 거임
        this.value = eventData.position - (Vector2)rect_Background.position;

        //가두기
        //벡터2인 자기자신의 값만큼, 최대 반지름만큼 가둘거임
        value = Vector2.ClampMagnitude(value, radius);
        //(1,4)값이 있으면 (-3 ~ 5)까지 가두기 함

        //부모객체(백그라운드) 기준으로 떨어질 상대적인 좌표값을 넣어줌
        rect_Joystick.localPosition = value;

        //value의 방향값만 구하기
        value = value.normalized;
    }
}
