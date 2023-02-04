using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingGnome : MonoBehaviour
{
    public int life = 3;
    
    public void Damage()
    {
        life -= 1;
        if(life <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //GameOver
    }
}
