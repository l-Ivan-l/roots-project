using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public enum CharacterType 
    {
        GNOME = 1,
        MANDRAKE = 2
    }
    protected CharacterType character;
    private Vector3 movementDirection;
    public float initalSpeed = 5f;
    public float speedMultiplier = 2f;
    protected float moveSpeed;
    protected Rigidbody characterBody;
    protected int life = 1;
    [SerializeField]protected bool canMove;
    [SerializeField]protected int number = 0;
    protected bool pastThreshold;
    private Vector3 enemyDirection;
    private int direction = 1;
    protected ScreenShake screenShake;

    public int Life
    {
        get{return life;}
        set{life = value;}
    }

    public int Number 
    {
        get{return number;}
        set{number = value;}
    }

    public void Awake()
    {
        screenShake = Camera.main.GetComponent<ScreenShake>();
        characterBody = this.GetComponent<Rigidbody>();
        movementDirection = new Vector3(1,0,0);
        moveSpeed = initalSpeed;
        if(character == CharacterType.MANDRAKE)
        {
            direction = -1;
        }
        moveSpeed *= direction;
        canMove = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CharacterMovement();
    }

    void CharacterMovement()
    {
        if(canMove && !pastThreshold)
        {
            characterBody.velocity = new Vector3(movementDirection.x * moveSpeed, characterBody.velocity.y, characterBody.velocity.z);
        }
        else if(pastThreshold)
        {
            characterBody.velocity = new Vector3(enemyDirection.normalized.x * (moveSpeed * speedMultiplier), characterBody.velocity.y, 
            enemyDirection.normalized.z * (moveSpeed * speedMultiplier));
        }
    }

    public void MoveTowardsEnemy(Vector3 _enemyPosition)
    {
        StopMovement();
        enemyDirection = (_enemyPosition - this.transform.position) * direction;
        pastThreshold = true;
    }

    protected void StopMovement()
    {
        canMove = false;
        characterBody.velocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision _collision)
    {
        CharacterCollided(_collision);
    }

    protected virtual void Die()
    {
        StopMovement();
        VFXPool.instance.SpawnExplosionVFX(transform.position);
        gameObject.SetActive(false);
    }

    public abstract void Damage();

    public abstract void CharacterCollided(Collision collision);
}
