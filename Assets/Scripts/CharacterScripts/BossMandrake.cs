using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMandrake : MonoBehaviour
{
    public int life = 5;
    public int number = 0; //Change this
    public VisualNumber visualNumber;

    void Start()
    {
        number = SquareRootPool.instance.GetNumberFromPool(DificultyLevel.Boss);
        visualNumber.SetNumber(number);
    }
    
    public void Damage()
    {
        life -= 1;
        if(life <= 0)
        {
            Die();
        }
        number = SquareRootPool.instance.GetNumberFromPool(DificultyLevel.Boss);
        visualNumber.SetNumber(number);
    }

    public void Die()
    {
        GameController.instance.Win();
    }
}
