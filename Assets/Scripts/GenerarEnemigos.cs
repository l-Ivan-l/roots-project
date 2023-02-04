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

    [SerializeField] List<GameObject> EnemiList = new List<GameObject>();
    [SerializeField] public GameObject enemigos;
    [SerializeField] int EnemieNum = 5;
    [SerializeField]  float TiempoMax =10;
    [SerializeField] float TiempoActual;
    [SerializeField] private Road road;

    [SerializeField] Transform parent;
    // Start is called before the first frame update
    void Start()
    {

       CreatePool();

        TiempoActual = TiempoMax;


    }

    void CreatePool()
    {
        for(int i =0; i<EnemieNum; i++)
        {
            GameObject newEnemie = Instantiate(enemigos,transform. position, Quaternion.identity, parent);
            newEnemie.SetActive(false);
            EnemiList.Add(newEnemie);
        }
    }
    

    GameObject GetNextInPool()
    {
        for(int i=0; i< EnemiList.Count; i++)
        {
            if (!EnemiList[i].activeSelf)
            {
                return EnemiList[i];
            }
        }
        return null;
    }

    void SpawnEnemie()
    {
        GameObject enemie = GetNextInPool();

        enemie.transform.position = transform.position;
        enemie.SetActive(true);
    }


   void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            SpawnEnemie();
        }


        if (TiempoActual> 0)
        {
            TiempoActual -= Time.deltaTime;
        }
        else
        {
            SpawnEnemie();
            TiempoMax = Random.Range(3, 10);
            TiempoActual = TiempoMax;
        }

    }
}
