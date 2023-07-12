using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapChoice_UIManager : MonoBehaviour
{
    
    void Start()
    {
        Debug.Log("-----  " + PlayerPrefs.GetInt(GetInstanceID().ToString() + "stars"));
        Debug.Log("++++++++  " + PlayerPrefs.GetInt(GetInstanceID().ToString() + "lapRecord"));
    }

    
    void Update()
    {
        
    }

    public void BackButton()
    {
        SceneManager.LoadScene("4.MapThreeStory");
    }
}
