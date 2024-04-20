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
    void Awake()
    {
        Player = GameObject.Find("Player");
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameOverScreen.SetActive(false);
    }

    // Update Check if there are any enemies left
    void Update()
    {
        Count();
        if (Enemies.Length == 0)
        {
            GameOverScreen.SetActive(true);
            Debug.Log("You Win!");

        }
        PlayerDeath();
    }
    
    // check if the player is dead
    public void PlayerDeath()
    {
        if (Player == null)
        {
            GameOverScreen.SetActive(true);
            Debug.Log("You Lose!");
        }
    }

    //check how many enemies are left
    public void Count()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Player = GameObject.Find("Player");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

}
