using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;


public class calculadora : MonoBehaviour {

    public TextMeshProUGUI resultado;
    public TextMeshProUGUI lblTempo;
    public TextMeshProUGUI operacion;
    private string textoimprimir;
    string valor1;
    string valor2;
    string operacionActual;
    double rta;

    public void BorrarC()
    {

        resultado.text = "";
        lblTempo.text = "";
        operacion.text = "";
    }

   
   
    public void multiplicacion()
    {
        if (resultado.text != "")
        {
            operacion.text = "*";
            lblTempo.text = resultado.text;
            textoimprimir = "";
            resultado.text = textoimprimir;
        }
    }
    
  
   
   
  
          
  
   
 

    public void igual()
    {
        if (resultado.text != "")
        {
            valor2 = resultado.text;
            valor1 = lblTempo.text;
            operacionActual = operacion.text;
            resultado.text = operaciones(valor1, valor2, operacionActual);
            
        }
    }

    public string operaciones(string n1, string n2, string opeMath)
    {
        string respuesta = "";
        switch (opeMath)
        {
           
            case "*":
                respuesta = (double.Parse(n1) * double.Parse(n2)).ToString();
                break;
            
        }
        return respuesta;
    }
}
