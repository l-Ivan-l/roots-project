using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXPool : MonoBehaviour
{
    public static VFXPool instance;
    //Impact VFX
    [SerializeField] private GameObject impactVFXPrefab;
    [SerializeField] private Transform impactVFXContainer;
    private int impactVFXPoolLength = 2;
    private List<ParticleSystem> impactVFXPool = new List<ParticleSystem>();
    //Activate Explosion VFX
    [SerializeField] private GameObject GnomeExplosionVfxPrefab;
    [SerializeField] private Transform GnomeExplosionVfxContainer;
    private int GnomeExplosionVfxPoolLength = 3;
    private List<ParticleSystem> GnomeExplosionVfxPool = new List<ParticleSystem>();
    //Spawn VFX
    [SerializeField] private GameObject spawnVFXPrefab;
    [SerializeField] private Transform spawnVFXContainer;
    private int spawnVFXPoolLength = 10;
    private List<ParticleSystem> spawnVFXPool = new List<ParticleSystem>();
    //Explosion VFX
    [SerializeField] private GameObject explosionVFXPrefab;
    [SerializeField] private Transform explosionVFXContainer;
    private int explosionVFXPoolLength = 5;
    private List<ParticleSystem> explosionVFXPool = new List<ParticleSystem>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateImpactVFXPool();
        CreateGnomeExplosionVFXPool();
        CreateSpawnVFXPool();
        CreateEnemyExplosionVFXPool();
    }

    void CreateImpactVFXPool()
    {
        for (int i = 0; i < impactVFXPoolLength; i++)
        {
            ParticleSystem impact = Instantiate(impactVFXPrefab, Vector3.zero, Quaternion.identity, impactVFXContainer).GetComponent<ParticleSystem>();
            Quaternion impactRot = new Quaternion(0f, 0f, 0f, 0f);
            impactRot.eulerAngles = new Vector3(0f, 90f, 0f);
            impact.gameObject.transform.rotation = impactRot;
            impactVFXPool.Add(impact);
        }
    }

    void CreateGnomeExplosionVFXPool()
    {
        for (int i = 0; i < GnomeExplosionVfxPoolLength; i++)
        {
            ParticleSystem vegExp = Instantiate(GnomeExplosionVfxPrefab, Vector3.zero, Quaternion.identity, GnomeExplosionVfxContainer).GetComponent<ParticleSystem>();
            Quaternion vegRot = new Quaternion(0f, 0f, 0f, 0f);
            vegRot.eulerAngles = new Vector3(0f, 90f, 0f);
            vegExp.gameObject.transform.rotation = vegRot;
            GnomeExplosionVfxPool.Add(vegExp);
        }
    }

    void CreateSpawnVFXPool()
    {
        for (int i = 0; i < spawnVFXPoolLength; i++)
        {
            ParticleSystem spawn = Instantiate(spawnVFXPrefab, Vector3.zero, Quaternion.identity, spawnVFXContainer).GetComponent<ParticleSystem>();
            Quaternion spawnRot = new Quaternion(0f, 0f, 0f, 0f);
            spawnRot.eulerAngles = new Vector3(0f, 90f, 0f);
            spawn.gameObject.transform.rotation = spawnRot;
            spawnVFXPool.Add(spawn);
        }
    }

    void CreateEnemyExplosionVFXPool()
    {
        for(int i = 0; i < explosionVFXPoolLength; i++)
        {
            ParticleSystem explosion = Instantiate(explosionVFXPrefab, Vector3.zero, Quaternion.identity, explosionVFXContainer).GetComponent<ParticleSystem>();
            Quaternion expRot = new Quaternion(0f, 0f, 0f, 0f);
            expRot.eulerAngles = new Vector3(0f, 90f, 0f);
            explosion.gameObject.transform.rotation = expRot;
            explosionVFXPool.Add(explosion);
        }
    }

    //---------------------------------------------------------------------------
    public void SpawnImpactVFX(Vector3 _position)
    {
        for (int i = 0; i < impactVFXPool.Count; i++)
        {
            if (!impactVFXPool[i].isPlaying)
            {
                impactVFXPool[i].transform.position = _position;
                impactVFXPool[i].Play();
                break;
            }
        }
    }

    public void SpawnGnomeExplosionVFX(Vector3 _position, bool isKing = false)
    {
        for (int i = 0; i < GnomeExplosionVfxPool.Count; i++)
        {
            if (!GnomeExplosionVfxPool[i].isPlaying)
            {
                GnomeExplosionVfxPool[i].transform.position = _position;
                if(isKing) GnomeExplosionVfxPool[i].transform.localScale = new Vector3(2,2,2);
                GnomeExplosionVfxPool[i].Play();
                break;
            }
        }
    }

    public void SpawnInstantiateVFX(Vector3 _position)
    {
        for (int i = 0; i < spawnVFXPool.Count; i++)
        {
            if (!spawnVFXPool[i].isPlaying)
            {
                spawnVFXPool[i].transform.position = _position;
                spawnVFXPool[i].Play();
                break;
            }
        }
    }

    public void SpawnExplosionVFX(Vector3 _position, bool isBoss = false)
    {
        for (int i = 0; i < explosionVFXPool.Count; i++)
        {
            if (!explosionVFXPool[i].isPlaying)
            {
                explosionVFXPool[i].transform.position = _position;
                if(isBoss) explosionVFXPool[i].transform.localScale = new Vector3(2,2,2);
                explosionVFXPool[i].Play();
                break;
            }
        }
    }


}
