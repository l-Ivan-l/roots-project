using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMandrake : MonoBehaviour
{
    public int life = 5;
    public int number = 0; //Change this

    void Start()
    {
        number = SquareRootPool.instance.GetNumberFromPool(DificultyLevel.Boss);
    }
    
    public void Damage()
    {
        life -= 1;
        number = SquareRootPool.instance.GetNumberFromPool(DificultyLevel.Boss);
        if(life <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameController.instance.Win();
    }
}
