using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mandrake : Character, EventListener<GameEvents>
{
    public enum MandrakeType
    {
        EASY,
        MEDIUM,
        HARD
    }

    private float knockbackForce = 100f;
    private Vector3 knockbackDirection = new Vector3(15f, 6f, 0f);
    private bool knockback;
    private float fallSpeed = 50f;
    public float stunnedTime = 3f;
    public MandrakeType type;

    public VisualNumber visualNumber;
    public Renderer mandrakeRenderer;

    public bool CanMove
    {
        get{return canMove;}
        set{canMove = value;}
    }

    [HideInInspector] public GenerarEnemigos spawner;
    public Material[] mandrakeMat;

    public Animator mandrakeAnimator;
    private bool stunned;
    
    new void Awake()
    {
        character = CharacterType.MANDRAKE;
        base.Awake();
    }

    void OnEnable()
    {
        this.EventStartListening<GameEvents>();
        
        switch(type)
        {
            case MandrakeType.EASY:
                life = 1;
                mandrakeRenderer.material = mandrakeMat[0];
            break;

            case MandrakeType.MEDIUM:
                life = 2;
                mandrakeRenderer.material = mandrakeMat[1];
            break;

            case MandrakeType.HARD:
                life = 3;
                mandrakeRenderer.material = mandrakeMat[2];
            break;
        }
        visualNumber.SetNumber(number);
    }

    void OnDisable()
    {
        this.EventStopListening<GameEvents>();
        if(spawner != null)
            spawner.hasActiveMandrake = false;
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
            //StopMovement();
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
            screenShake.TriggerShake(0.5f, 0.5f);
            pastThreshold = false;
            StopMovement();
            KingGnome kingGnome = _collision.gameObject.GetComponent<KingGnome>();
            kingGnome.Damage();
            Die();
        }
    }

    IEnumerator Stunned()
    {
        stunned = true;
        mandrakeAnimator.SetBool("stunned", stunned);
        yield return new WaitForSeconds(stunnedTime);
        canMove = true;
        stunned = false;
        mandrakeAnimator.SetBool("stunned", stunned);
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
            number = spawner.GetNewNumber(type);
            visualNumber.SetNumber(number);
            Knockback();
        } 
        else 
        {
            spawner.IncreaseRowTime();
            Die();
        }
    }

    public void OnGEvent(GameEvents e)
    {
        switch(e.eventType)
        {
            case GameEventType.gameOver:
                StopMovement();
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
}
