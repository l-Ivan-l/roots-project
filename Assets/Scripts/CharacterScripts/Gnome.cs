using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnome : Character,IDragable
{
    private Vector3 initPosition;
    private bool returning;
    public float returningSpeed = 7f;

    private GnomeSpawn spawn;

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

    public void SetSpawn(GnomeSpawn _spawn)
    {
        spawn = _spawn;
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
                Mandrake behavior = mandrake as Mandrake;
                behavior.CanMove = true;
                this.Damage();
            }
        }
        if(_collision.gameObject.CompareTag("BossMandrake"))
        {
            pastThreshold = false;
            StopMovement();
            BossMandrake boss = _collision.gameObject.GetComponent<BossMandrake>();
            if(CanDamageBoss(boss))
            {
                boss.Damage();
            }
            Die();
        }
    }

    void ReturnToInitPosition()
    {
        moveSpeed = returningSpeed;
        moveSpeed *= -1f;
        canMove = true;
        returning = true;
        number = 0;
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
        if(number * number == _mandrake.Number && number != 0)
        {
            return true;
        }
        return false;
    }

    bool CanDamageBoss(BossMandrake _boss)
    {
        if(number * number == _boss.number)
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

    protected override void Die()
    {
        StopMovement();
        if (spawn != null)
            spawn.DelaySpawn();
        gameObject.SetActive(false);
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

    public void OnDropAble()
    {
        //throw new System.NotImplementedException();
    }

    public void OnDropNotAble()
    {
        //throw new System.NotImplementedException();
    }
}
