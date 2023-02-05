using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingGnome : MonoBehaviour
{
    public int life = 3;
    public GameObject gnomeMesh;
    
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
        gnomeMesh.SetActive(false);
        VFXPool.instance.SpawnGnomeExplosionVFX(transform.position, true);
        GameController.instance.GameOver();
    }
}
