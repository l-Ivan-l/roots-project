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
        Application.targetFrameRate = 60;
    }

    public void Win()
    {
        win = true;
        GameEvents.Trigger(GameEventType.win);
        SFXPool.instance.PlayGnomeWinSound();
        SFXPool.instance.PlayMandrakeDieSound();
    }

    public void GameOver()
    {
        gameOver = true;
        GameEvents.Trigger(GameEventType.gameOver);
        SFXPool.instance.PlayMandrakeWinSound();
        SFXPool.instance.PlayGnomeDieSound();
        //Time.timeScale = 0f;
    }
}
