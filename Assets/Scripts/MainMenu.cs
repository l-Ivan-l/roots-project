using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    

    public void EscenaInstrucciones()
    {
        SceneManager.LoadScene("Instrucciones");
    }


    public void EscenaNivel()
    {
        SceneManager.LoadScene("Nivel");

    }



    public void EscenaMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
      


    }
    public void Cerrar()
    {
        Debug.Log("Cerrar");
        Application.Quit();
    }
   
}
