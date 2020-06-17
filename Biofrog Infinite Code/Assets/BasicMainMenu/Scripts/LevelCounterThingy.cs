using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCounterThingy : MonoBehaviour
{
    public int highestAchievedLevel = 0;
    Texture2D cursor;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += CheckIfLevelLarger;

    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= CheckIfLevelLarger;
    }

    void CheckIfLevelLarger(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.buildIndex);
        highestAchievedLevel = Mathf.Max(scene.buildIndex, highestAchievedLevel);
    }
}
