using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mandrake : Character
{
    private float knockbackForce = 50f;
    private Vector3 knockbackDirection = new Vector3(3f, 10f, 0f);
    private bool knockback;
    private float fallSpeed = 50f;
    private float stunnedTime = 1f;

    public bool CanMove
    {
        get{return canMove;}
        set{canMove = value;}
    }
    
    new void Awake()
    {
        character = CharacterType.MANDRAKE;
        base.Awake();
    }

    void Update()
    {
        FallPhysics();
    }

    public override void CharacterCollided(Collision _collision)
    {
        if(_collision.gameObject.CompareTag("Gnome"))
        {
            Debug.Log("Collisioned with Gnome");
            StopMovement();
        }
        if(_collision.gameObject.CompareTag("Floor"))
        {
            if(knockback) 
            {
                knockback = false;
                StopMovement();
                StartCoroutine(Stunned());
            }
        }
        if(_collision.gameObject.CompareTag("KingGnome"))
        {
            pastThreshold = false;
            StopMovement();
            Die();
        }
    }

    IEnumerator Stunned()
    {
        yield return new WaitForSeconds(stunnedTime);
        canMove = true;
    }

    void Knockback()
    {
        characterBody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        knockback = true;
    }

    void FallPhysics()
    {
        if(knockback)
        {
            characterBody.velocity += Vector3.up * Physics.gravity.y * fallSpeed * Time.deltaTime;
        }
    }

    public override void Damage()
    {
        life -= 1;
        if(life > 0)
        {
            Knockback();
        } 
        else 
        {
            Die();
        }
    }
}
