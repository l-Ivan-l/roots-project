using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour, EventListener<CardEvents>
{
    [SerializeField] List<int> deckContent = new List<int>();

    private void Awake()
    {
        InitializeDeck();
        ShuffleDeck();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetNumberList();
        }
    }
    private void InitializeDeck()
    {
        for (int i = 1; i < 10; i++)
        {
            deckContent.Add(i);
        }
    }

    private void ShuffleDeck()
    {
        for (int i = 0; i < deckContent.Count; i++)
        {
            int temp = deckContent[i];
            int randomIndex = Random.Range(i, deckContent.Count);
            deckContent[i] = deckContent[randomIndex];
            deckContent[randomIndex] = temp;
        }
    }

    public int GetNumberList()
    {
        int number = deckContent[0];
        deckContent.Remove(number);
        deckContent.Add(number);

        return number;
    }

    public void OnGEvent(CardEvents e)
    {
        switch (e.cardEventType)
        {
            case CardEventType.GetNumber:
                e.card.SetNumber(GetNumberList());
            break;
        }
    }

    void OnEnable()
    {
        this.EventStartListening<CardEvents>();
    }

    void OnDisable()
    {
        this.EventStopListening<CardEvents>();
    }

}
