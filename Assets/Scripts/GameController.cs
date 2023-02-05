using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public bool win;
    public bool gameOver;
    public MainMenu Menu;

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
        StartCoroutine(WaitToChangeScene());
    }

    public void GameOver()
    {
        gameOver = true;
        GameEvents.Trigger(GameEventType.gameOver);
        SFXPool.instance.PlayMandrakeWinSound();
        SFXPool.instance.PlayGnomeDieSound();
        //Time.timeScale = 0f;
        StartCoroutine(WaitToChangeScene());
    }

    IEnumerator WaitToChangeScene()
    {
        yield return new WaitForSeconds(3f);
        if(gameOver)
        {
            Menu.EscenaGameOver();
        }
        if(win)
        {
            Menu.EsceneWin();
        }
    }
}
