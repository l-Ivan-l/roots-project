using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if(currentScene == "GameOver" || currentScene == "Win")
        {
            GameController.instance.gameOver = false;
            GameController.instance.win = false;
            StartCoroutine(ReturnToMenu());
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cerrar();
        }
    }

    public void EscenaInstrucciones()
    {
        StartCoroutine(LoadLevel("Instrucciones"));
    }


    public void EscenaNivel()
    {
        //SceneManager.LoadScene("Nivel");
        StartCoroutine(LoadLevel("Prototype"));
    }



    public void EscenaMainMenu()
    {
        //SceneManager.LoadScene("MainMenu");
      

        StartCoroutine(LoadLevel("MainMenu"));
    }

    public void EscenaGameOver()
    {
        StartCoroutine(LoadLevel("GameOver"));
    }

    public void EsceneWin()
    {
        StartCoroutine(LoadLevel("Win"));
    }

    public void Cerrar()
    {
        Debug.Log("Cerrar");
        Application.Quit();
    }

    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(5f);
        EscenaMainMenu();
    }
   
}
