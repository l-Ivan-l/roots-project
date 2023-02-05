using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisualNumber : MonoBehaviour
{
    [SerializeField] private int num = 0;
    [SerializeField] private TextMeshPro txt_Number;

    public void SetNumber(int _num)
    {
        num = _num;
        txt_Number.text = _num.ToString();
    }
}
