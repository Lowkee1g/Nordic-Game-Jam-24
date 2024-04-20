using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//TMP
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject LevelSelectorScreen;
    public Button LevelSelectorButton;
    public GameObject MainMenuScreen;

    public void Start()
    {
        LevelSelectorScreen.SetActive(false);
        MainMenuScreen.SetActive(true);
    }

    public void OpenLevelSelector()
    {
        LevelSelectorScreen.SetActive(true);
        MainMenuScreen.SetActive(false);
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }   

    public void CloseLevelSelector()
    {
        LevelSelectorScreen.SetActive(false);
        MainMenuScreen.SetActive(true);
    }
    
}
