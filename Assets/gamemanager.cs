using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class gamemanager : MonoBehaviour
{
    public GameObject Player;

    public GameObject[] Enemies;
    public GameObject GameOverScreen;
    public TextMeshProUGUI endgame_msg;

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
    public void KillPlayer()
    {
        Destroy(Player);
    }
    public void KillPlayer(string msg) {
        Debug.Log(msg);
        endgame_msg.text = msg;
        Destroy(Player);
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
