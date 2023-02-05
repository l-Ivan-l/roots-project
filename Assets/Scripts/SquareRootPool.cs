using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DificultyLevel
{
    Easy,
    Medium,
    Hard,
    Boss
}
public class SquareRootPool : MonoBehaviour
{
    [SerializeField] private int minNumberEasy = 1;
    [SerializeField] private int maxNumberEasy = 5;
    [SerializeField] private List<int> easyPool = new List<int>();

    [SerializeField] private int minNumberMedium = 6;
    [SerializeField] private int maxNumberMedium = 10;
    [SerializeField] private List<int> mediumPool = new List<int>();

    [SerializeField] private int minNumberHard = 11;
    [SerializeField] private int maxNumberHard = 15;
    [SerializeField] private List<int> hardPool = new List<int>();

    [SerializeField] private int minNumberBoss = 16;
    [SerializeField] private int maxNumberBoss = 20;
    [SerializeField] private List<int> bossPool = new List<int>();

    public static SquareRootPool instance;

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

    private void Start()
    {
        CreatePool(easyPool, minNumberEasy, maxNumberEasy);
        CreatePool(mediumPool, minNumberMedium, maxNumberMedium);
        CreatePool(hardPool, minNumberHard, maxNumberHard);
        CreatePool(bossPool, minNumberBoss, maxNumberBoss);

        ShufflePool(easyPool);
        ShufflePool(mediumPool);
        ShufflePool(hardPool);
        ShufflePool(bossPool);
    }

    
    private void CreatePool(List<int> pool, int min, int max)
    {
        for (int i = min; i <= max; i++)
        {
            int squere = i * i;
            pool.Add(squere);
        }
    }

    private void ShufflePool(List<int> pool)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            int temp = pool[i];
            int randomIndex = Random.Range(i, pool.Count);
            pool[i] = pool[randomIndex];
            pool[randomIndex] = temp;
        }
    }

    private int GetNumberList(List<int> pool)
    {
        int number = pool[0];
        pool.Remove(number);
        pool.Add(number);

        return number;
    }

    public int GetNumberFromPool(DificultyLevel level)
    {
        switch(level)
        {
            case DificultyLevel.Easy:
                return GetNumberList(easyPool);

            case DificultyLevel.Medium:
                return GetNumberList(mediumPool);

            case DificultyLevel.Hard:
                return GetNumberList(hardPool);

            case DificultyLevel.Boss:
                return GetNumberList(bossPool);
            default:
                return 0;
        }
    }
}
