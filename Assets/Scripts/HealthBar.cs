using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public BossMandrake bossMandrake;
    public KingGnome kingGnome;

    private float maxHealth;
    private float currentHealth;

    public Image healthBar;

    void Awake()
    {
        if(bossMandrake != null)
        {
            maxHealth = bossMandrake.life;
        }
        else 
        {
            maxHealth = kingGnome.life;
        }
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(bossMandrake != null)
        {
            currentHealth = bossMandrake.life;
        }
        else 
        {
            currentHealth = kingGnome.life;
        }
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
