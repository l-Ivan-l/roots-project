using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CardEventType
{
    GetNumber
}
public struct CardEvents 
{
    public CardEventType cardEventType;
    public Card card;

    public CardEvents(CardEventType _eventType, Card _card)
    {
        cardEventType = _eventType;
        card = _card;
    }

    static CardEvents e;

    public static void Trigger(CardEventType _eventType, Card _card)
    {
        e.cardEventType = _eventType;
        e.card = _card;

        GenericEventManager.TriggerEvent(e);
    }
}
