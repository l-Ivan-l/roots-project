using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

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
   
}
