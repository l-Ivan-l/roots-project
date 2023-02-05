using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public bool win;
    public bool gameOver;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Win()
    {
        win = true;
        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        gameOver = true;
        Time.timeScale = 0f;
    }
}
