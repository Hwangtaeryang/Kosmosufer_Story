using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonalInfoCheck : MonoBehaviour
{
    public Toggle personalToggle;
    public GameObject persoanlPanel;    //개인정보동의판넬
    public bool personalState;  //개인정보동의 여부


    private void Start()
    {
        persoanlPanel.SetActive(true);
    }

    //개인정보 동의 토글 체크 여부
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

    //개인정보 동의 확인 버튼 클릭
    public void PersoanlConfirmationButton()
    {
        //개인정보 동의 체크 했음
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
