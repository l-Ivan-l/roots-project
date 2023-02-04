using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleEventTrigger : MonoBehaviour
{

    public GameEventType type;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colliding");
        GameEvents.Trigger(type);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameEvents.Trigger(type);
    }
}
