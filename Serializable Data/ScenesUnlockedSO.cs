using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScenesUnlockedSO", menuName = "ScriptableObjects/ScenesUnlockedScriptableObject", order = 2)]

public class ScenesUnlockedSO : ScriptableObject
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
}
