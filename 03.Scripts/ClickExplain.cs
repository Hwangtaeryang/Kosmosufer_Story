using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickExplain : MonoBehaviour
{
    public GameObject explainObj;   //���� �ǳ�

    int clickCount = 0;


    public void ClickExplainShow()
    {
        if (clickCount.Equals(0))
        {
            clickCount = 1;
            explainObj.SetActive(true);
        }
        else
        {
            clickCount = 0;
            explainObj.SetActive(false);
        }
    }
}
