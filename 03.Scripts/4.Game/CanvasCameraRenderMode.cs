using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasCameraRenderMode : MonoBehaviour
{
    public Canvas thisCanvas;
    Camera camera;

    void Start()
    {
        StartCoroutine(CanvasSizeResolution());
    }

    IEnumerator CanvasSizeResolution()
    {
        yield return new WaitForSeconds(0.08f);
        if (SceneManager.GetActiveScene().name.Equals("4.TutorialMap"))
        {
            camera = GameObject.Find("TutorialCamera(Clone)").transform.GetChild(2).GetComponent<Camera>();
            TalkBoxList.instance.JoPadFind_and_State(false);
        }
        else if(SceneManager.GetActiveScene().name.Equals("4.Map1"))
        {
            camera = GameObject.Find("Story1Camera(Clone)").transform.GetChild(2).GetComponent<Camera>();
            TalkBoxList.instance.JoPadFind_and_State(false);
        }
        else if (SceneManager.GetActiveScene().name.Equals("4.Map2"))
        {
            camera = GameObject.Find("Story2_Camera(Clone)").transform.GetChild(2).GetComponent<Camera>();
            TalkBoxList.instance.JoPadFind_and_State(false);
        }
        else if(SceneManager.GetActiveScene().name.Equals("4.Map3"))
        {
            camera = GameObject.Find("Story3_Camera(Clone)").transform.GetChild(2).GetComponent<Camera>();
            TalkBoxList.instance.JoPadFind_and_State(false);
        }
            
        thisCanvas.worldCamera = camera;
    }

    
    void Update()
    {
        
    }
}
