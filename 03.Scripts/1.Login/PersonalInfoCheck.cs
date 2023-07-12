using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonalInfoCheck : MonoBehaviour
{
    public Toggle personalToggle;
    public GameObject persoanlPanel;    //�������������ǳ�
    public bool personalState;  //������������ ����


    private void Start()
    {
        persoanlPanel.SetActive(true);
    }

    //�������� ���� ��� üũ ����
    public void PersonalIsOnCheck()
    {
        if(personalToggle.isOn.Equals(true))
        {
            personalState = true;
        }
        else
        {
            personalState = false;
        }
    }

    //�������� ���� Ȯ�� ��ư Ŭ��
    public void PersoanlConfirmationButton()
    {
        //�������� ���� üũ ����
        if(personalState.Equals(true))
        {
            persoanlPanel.SetActive(false);
        }
        else
        {
            persoanlPanel.SetActive(true);
        }
    }
}
