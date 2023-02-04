using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnome : Character,IDragable
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

    public void Activate(int _number)
    {
        number = _number;
        canMove = true;
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
        if(_collision.gameObject.CompareTag("BossMandrake"))
        {
            pastThreshold = false;
            StopMovement();
            Die();
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

    public void StartDrag(Transform refPos)
    {
        //throw new System.NotImplementedException();
    }

    public void EndDrag(Transform target)
    {
        //throw new System.NotImplementedException();
    }

    public void ActionDrop(Card _card)
    {
        Activate(_card.GetNumber());
    }
}
