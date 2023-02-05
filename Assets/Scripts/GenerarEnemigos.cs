using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GenerarEnemigos : MonoBehaviour
{

    public  enum Road
    {
        left, right,center
    }

    [SerializeField] List<GameObject> easyEnemiList = new List<GameObject>();
    [SerializeField] List<GameObject> mediumEnemiList = new List<GameObject>();
    [SerializeField] List<GameObject> hardEnemiList = new List<GameObject>();
    [SerializeField] public GameObject enemigos;
    [SerializeField] int EnemieNum = 5;
    [SerializeField]  float TiempoMax =10;
    [SerializeField] float TiempoActual;
    [SerializeField] private Road road;

    [SerializeField] Transform parent;

    public bool hasActiveMandrake;
    public BossMandrake bossMandrake;
    // Start is called before the first frame update
    void Start()
    {

       CreatePool();

        if(road == Road.center)
        {
            TiempoActual = 4f;
        } 
        else
        {
            TiempoActual = Random.Range(7, 20);
        }
    }

    void CreatePool()
    {
        for(int i =0; i<EnemieNum; i++)
        {
            GameObject newEnemie = Instantiate(enemigos,transform. position, Quaternion.identity, parent);
            Mandrake _mandrake = newEnemie.GetComponent<Mandrake>();
            _mandrake.type = Mandrake.MandrakeType.EASY;
            newEnemie.SetActive(false);
            easyEnemiList.Add(newEnemie);
        }
        for(int i =0; i<EnemieNum; i++)
        {
            GameObject newEnemie = Instantiate(enemigos,transform. position, Quaternion.identity, parent);
            Mandrake _mandrake = newEnemie.GetComponent<Mandrake>();
            int _type = Random.Range(0, 2);
            if(_type == 0) _mandrake.type = Mandrake.MandrakeType.EASY;
            if(_type == 1) _mandrake.type = Mandrake.MandrakeType.MEDIUM;
            newEnemie.SetActive(false);
            mediumEnemiList.Add(newEnemie);
        }
        for(int i =0; i<EnemieNum; i++)
        {
            GameObject newEnemie = Instantiate(enemigos,transform. position, Quaternion.identity, parent);
            Mandrake _mandrake = newEnemie.GetComponent<Mandrake>();
            int _type = Random.Range(0, 3);
            if(_type == 0) _mandrake.type = Mandrake.MandrakeType.EASY;
            if(_type == 1) _mandrake.type = Mandrake.MandrakeType.MEDIUM;
            if(_type == 2) _mandrake.type = Mandrake.MandrakeType.HARD;
            newEnemie.SetActive(false);
            hardEnemiList.Add(newEnemie);
        }
    }
    

    GameObject GetNextInPool()
    {
        switch(bossMandrake.life)
        {
            case 5:
            case 4:
                for(int i=0; i< easyEnemiList.Count; i++)
                {
                    if (!easyEnemiList[i].activeSelf)
                    {
                        return easyEnemiList[i];
                    }
                }
            break;

            case 3:
            case 2:
                for(int i=0; i< mediumEnemiList.Count; i++)
                {
                    if (!mediumEnemiList[i].activeSelf)
                    {
                        return mediumEnemiList[i];
                    }
                }
            break;

            case 1:
                for(int i=0; i< hardEnemiList.Count; i++)
                {
                    if (!hardEnemiList[i].activeSelf)
                    {
                        return hardEnemiList[i];
                    }
                }
            break;
        }
        
        return null;
    }

    void SpawnEnemie()
    {
        GameObject enemie = GetNextInPool();

        enemie.transform.position = transform.position;

        Mandrake mandrakeBehavior = enemie.GetComponent<Mandrake>();
        mandrakeBehavior.spawner = this;
        switch(mandrakeBehavior.type)
        {
            case Mandrake.MandrakeType.EASY:
                mandrakeBehavior.Number = SquareRootPool.instance.GetNumberFromPool(DificultyLevel.Easy);
            break;
            case Mandrake.MandrakeType.MEDIUM:
                mandrakeBehavior.Number = SquareRootPool.instance.GetNumberFromPool(DificultyLevel.Medium);
            break;
            case Mandrake.MandrakeType.HARD:
                mandrakeBehavior.Number = SquareRootPool.instance.GetNumberFromPool(DificultyLevel.Hard);
            break;
        }
        VFXPool.instance.SpawnInstantiateVFX(enemie.transform.position);
        enemie.SetActive(true);
        mandrakeBehavior.CanMove = true;
        hasActiveMandrake = true;
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            SpawnEnemie();
        }

        if(hasActiveMandrake || GameController.instance.win || GameController.instance.gameOver) return;
        if (TiempoActual> 0)
        {
            TiempoActual -= Time.deltaTime;
        }
        else
        {
            SpawnEnemie();
            TiempoMax = Random.Range(6, 10);
            TiempoActual = TiempoMax;
        }

    }

    public void IncreaseRowTime()
    {
        TiempoActual += 7.5f;
    }

    public int GetNewNumber(Mandrake.MandrakeType type)
    {
        if(type == Mandrake.MandrakeType.EASY)
        {
            return SquareRootPool.instance.GetNumberFromPool(DificultyLevel.Easy);
        }
        if(type == Mandrake.MandrakeType.MEDIUM)
        {
            return SquareRootPool.instance.GetNumberFromPool(DificultyLevel.Medium);
        }
        if(type == Mandrake.MandrakeType.HARD)
        {
            return SquareRootPool.instance.GetNumberFromPool(DificultyLevel.Hard);
        }
        return 0;
    }
}
