using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnome : Character
{
    private Vector3 initPosition;
    private bool returning;
    public float returningSpeed = 7f;

    new void Awake()
    {
        character = CharacterType.GNOME;
        initPosition = transform.position;
        base.Awake();
    }

    void Update()
    {
        CheckIfReturned();
    }
    public override void CharacterCollided(Collision _collision)
    {
        if(_collision.gameObject.CompareTag("Mandrake"))
        {
            Debug.Log("Collisioned with Mandrake");
            StopMovement();
            Character mandrake = _collision.gameObject.GetComponent<Character>();
            if(CanDamage(mandrake))
            {
                mandrake.Damage();
                ReturnToInitPosition();
            } 
            else 
            {
                this.Damage();
            }
        }
    }

    void ReturnToInitPosition()
    {
        moveSpeed = returningSpeed;
        moveSpeed *= -1f;
        canMove = true;
        returning = true;
    }

    void CheckIfReturned()
    {
        if(returning && transform.position.x <= initPosition.x)
        {
            StopMovement();
            returning = false;
            moveSpeed = initalSpeed;
        }
    }

    bool CanDamage(Character _mandrake)
    {
        if(number * number == _mandrake.Number)
        {
            return true;
        }
        return false;
    }

    public override void Damage()
    {
        life -= 1;
        if(life <= 0)
        {
            Die();
        }
    }
}
