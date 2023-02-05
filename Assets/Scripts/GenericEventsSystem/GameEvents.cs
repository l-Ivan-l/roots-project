using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameEventType
{
    pause,
    resume,
    gameOver, 
    win
}
public struct GameEvents 
{
    public GameEventType eventType;

    public GameEvents(GameEventType _eventType)
    {
        eventType = _eventType;
    }

    static GameEvents e;

    public static void Trigger(GameEventType _eventType)
    {
        e.eventType = _eventType;
        GenericEventManager.TriggerEvent(e);
    }
}
