using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleEventListener : MonoBehaviour, EventListener<GameEvents>
{
    // Start is called before the first frame update
    void OnEnable()
    {
        this.EventStartListening<GameEvents>();
    }

    void OnDisable()
    {
        this.EventStopListening<GameEvents>();
    }

    public void OnGEvent(GameEvents e)
    {
        switch(e.eventType)
        {
            case GameEventType.gameOver:
                Debug.Log("The game is over");
            break;

            case GameEventType.pause:
                Debug.Log("The game is pause");
            break;

            case GameEventType.resume:
                Debug.Log("The game is resume");
            break;
        }
    }
}
