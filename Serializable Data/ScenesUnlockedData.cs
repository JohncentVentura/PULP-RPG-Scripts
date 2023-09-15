using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ScenesUnlockedData
{   
    public string sceneName;
    public bool unlockStartMenuScene;
    public bool unlockChapter1Scene;
    public bool unlockChapter2Scene;
    public bool unlockChapter3Scene;
    public bool unlockChapter4Scene;
    public bool unlockChapter5Scene;
    public bool unlockChapter6Scene;
    public bool unlockChapterMenuScene;
    public bool unlockPlayerMenuScene;

    public ScenesUnlockedData()
    {   
        sceneName = "";
        unlockStartMenuScene = true;
        unlockChapter1Scene = true;
        unlockChapter2Scene = false;
        unlockChapter3Scene = false;
        unlockChapter4Scene = false;
        unlockChapter5Scene = false;
        unlockChapter6Scene = false;
        unlockChapterMenuScene = true;
        unlockPlayerMenuScene = true;
    }
}