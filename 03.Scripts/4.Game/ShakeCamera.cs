using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    static public ShakeCamera instance { get; private set; }

    public float shakeAmount;   //ī�޶� ����
    float shakeTime;
    Vector3 initialPos;


    public bool onCollsion; //�浹����

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }


    void Start()
    {

    }

    public void VibrateForTime(float _time)
    {
        shakeTime = _time;
    }

    
    void Update()
    {
        if(onCollsion.Equals(true))
        {
            transform.position = Random.insideUnitSphere * shakeAmount + initialPos;
            shakeTime -= Time.deltaTime;
        }
        else
        {
            shakeTime = 0f;
        }
    }
}
