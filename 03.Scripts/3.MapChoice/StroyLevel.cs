using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



[CreateAssetMenu (menuName = "StroyLevel")]
public class StroyLevel : ScriptableObject
{
    public Object scene;
    [HideInInspector] public string sceneNameForLoad;

    [HideInInspector] public string playMusic;
    [HideInInspector] public int selectedMusic;
    [HideInInspector] public string[] clipNames;


    public bool unlocked = true;
    public int openMapToUnlock;   //¸Ê ÇØÁ¦ÇÒ º° °¹¼ö

    [Header("LEVEL SELECT UI")]
    public string displayName;
    public Sprite displayImage;


    public int StarsCollected
    {
        get { return PlayerPrefs.GetInt(GetInstanceID().ToString() + "stars", 0); }
        set { PlayerPrefs.SetInt(GetInstanceID().ToString() + "stars", value); }

    }

    void Start()
    {
        
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (scene != null) sceneNameForLoad = scene.name;
    }


    public void SetPlayList(string[] playList)
    {
        clipNames = playList;
        if (selectedMusic >= clipNames.Length)
        {
            selectedMusic = clipNames.Length - 1;
            playMusic = clipNames[selectedMusic];
        }

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }

    public void ChangeMusic(int selected)
    {
        selectedMusic = selected;
        playMusic = clipNames[selected];

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
#endif

    public void UnlockLevel()
    {
        unlocked = true;
        PlayerPrefs.SetInt(GetInstanceID().ToString() + "unlocked", 1);
    }

    public void LoadData()
    {
        if (PlayerPrefs.GetInt(GetInstanceID().ToString() + "unlocked", 0) == 1) unlocked = true;
    }

    public void DeletePrefs()
    {
        PlayerPrefs.DeleteKey(GetInstanceID().ToString() + "unlocked");
        PlayerPrefs.DeleteKey(GetInstanceID().ToString() + "stars");
        PlayerPrefs.DeleteKey(GetInstanceID().ToString() + "lapRecord");
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(StroyLevel))]
public class StoryLevelEditor : Editor
{

    int selected;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        StroyLevel myScript = (StroyLevel)target;

        if (myScript.clipNames == null) return;

        GUILayout.Space(10);
        // Show popup for select
        int oldSelected = selected;
        selected = EditorGUILayout.Popup("PLAY MUSIC: ", myScript.selectedMusic, myScript.clipNames);
        // Update the selected choice in the underlying object
        if (oldSelected != selected) myScript.ChangeMusic(selected);
    }

}
#endif