using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnome : Character,IDragable, EventListener<GameEvents>
{
    private Vector3 initPosition;
    private bool returning;
    public float returningSpeed = 7f;

    private GnomeSpawn spawn;
    public VisualNumber visualNumber;

    new void Awake()
    {
        character = CharacterType.GNOME;
        initPosition = transform.position;
        visualNumber.SetNumber(number);
        base.Awake();
    }

    void OnEnable()
    {
        this.EventStartListening<GameEvents>();
    }

    void OnDisable()
    {
        this.EventStopListening<GameEvents>();
    }

    void Update()
    {
        CheckIfReturned();
    }

    public void Activate(int _number)
    {
        number = _number;
        visualNumber.SetNumber(number);
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
            VFXPool.instance.SpawnImpactVFX(transform.position);
            SFXPool.instance.PlayImpactSound();
            StopMovement();
            Character mandrake = _collision.gameObject.GetComponent<Character>();
            if(CanDamage(mandrake))
            {
                screenShake.TriggerShake(0.15f, 0.15f);
                mandrake.Damage();
                ReturnToInitPosition();
            } 
            else 
            {
                screenShake.TriggerShake(0.1f, 0.25f);
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
                screenShake.TriggerShake(0.4f, 0.4f);
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
        visualNumber.SetNumber(number);
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
        if (spawn != null && !GameController.instance.gameOver)
            spawn.DelaySpawn();

        number = 0;
        visualNumber.SetNumber(number);
        VFXPool.instance.SpawnGnomeExplosionVFX(transform.position);
        gameObject.SetActive(false);
    }

    public void OnGEvent(GameEvents e)
    {
        switch(e.eventType)
        {
            case GameEventType.gameOver:
                Die();
            break;

            case GameEventType.pause:
                Debug.Log("The game is pause");
            break;

            case GameEventType.resume:
                Debug.Log("The game is resume");
            break;

            case GameEventType.win:
                Die();
            break;
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
        if(!returning)
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
