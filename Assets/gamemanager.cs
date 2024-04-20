using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour
{
    public GameObject Player;

    public GameObject[] Enemies;
    public GameObject GameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameOverScreen.SetActive(false);
    }

    // Update Check if there are any enemies left
    void Update()
    {
        if (Enemies.Length == 0)
        {
            GameOverScreen.SetActive(true);
            Debug.Log("You Win!");

        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

}
