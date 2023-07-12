using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    //�� ��ũ��Ʈ�� ��׶��忡 ����� ����
    //��ġ�� ������ �κ��� ��׶����̱� ����

    //�����̴� ������ �����ϱ� ���ؼ� ������
    [SerializeField] private RectTransform rect_Background;
    [SerializeField] private RectTransform rect_Joystick;

    //��׶����� �������� ������ �����ų ����
    private float radius;

    //������ �ӵ�
    [SerializeField] private float moveSpeed;

    //��ġ�� ���۵��� �� �����̰Ŷ�
    private bool isTouch = false;
    //ĳ���� ȸ������ ��������� value�� ���������� ������
    private Vector2 value;



    public bool left, right;

    void Start()
    {
        //inspector�� �� rect Transform�� �����ϴ� �� ����
        //0.5�� ���ؼ� �������� ���ؼ� ���� �־���
        this.radius = rect_Background.rect.width * 0.5f;

    }

    //�̵� ����
    void Update()
    {
        if (this.isTouch)
        {
            Debug.Log("���̽�ƽ ������");
            //���̽�ƽ �������� ĳ���� ȸ��
            //if (this.value != null)
            {
                //Debug.Log("���� - : " + Mathf.Atan2(this.value.x, this.value.y) * Mathf.Rad2Deg);

                //����
                if((Mathf.Atan2(this.value.x, this.value.y) * Mathf.Rad2Deg) < 0)
                {
                    left = true;
                    right = false;
                    Debug.Log("����");
                    //LeftTurn.instance._on = true;
                    //RightTurn.instance._on = false;
                    //LeftTurn.instance.LeftTurn_Move();
                }
                //������
                else if((Mathf.Atan2(this.value.x, this.value.y) * Mathf.Rad2Deg) > 0)
                {
                    left = false;
                    right = true;
                    Debug.Log("������");
                    //RightTurn.instance._on = true;
                    //LeftTurn.instance._on = false;
                    //RightTurn.instance.RightTurn_Move();
                    
                }
            }
        }
    }



    //�������̽� ����

    //������ ��(��ġ�� ���۵��� ��)
    public void OnPointerDown(PointerEventData eventData)
    {
        this.isTouch = true;

    }

    //�� ���� ��
    public void OnPointerUp(PointerEventData eventData)
    {
        //�� ���� �� ����ġ�� ������
        rect_Joystick.localPosition = Vector3.zero;

        this.isTouch = false;
    }

    //�巡�� ������
    public void OnDrag(PointerEventData eventData)
    {
        //���콺 ������(x��, y�ุ �־ ����2)
        //���콺 ��ǥ���� ������ ��׶��� ��ǥ���� �� ����ŭ ���̽�ƽ(�� ���׶��)�� ������ ����
        this.value = eventData.position - (Vector2)rect_Background.position;

        //���α�
        //����2�� �ڱ��ڽ��� ����ŭ, �ִ� ��������ŭ ���Ѱ���
        value = Vector2.ClampMagnitude(value, radius);
        //(1,4)���� ������ (-3 ~ 5)���� ���α� ��

        //�θ�ü(��׶���) �������� ������ ������� ��ǥ���� �־���
        rect_Joystick.localPosition = value;

        //value�� ���Ⱚ�� ���ϱ�
        value = value.normalized;
    }
}
