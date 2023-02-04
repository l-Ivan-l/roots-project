using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Threshold : MonoBehaviour
{
    public enum ThresholdType
    {
        MANDRAKE_THRESHOLD = 1,
        GNOME_THRESHOLD = 2
    }
    public ThresholdType thresholdType;

    private string enemyTag = "";
    public GameObject target;

    void Start()
    {
        if(thresholdType == ThresholdType.GNOME_THRESHOLD)
        {
            enemyTag = "Mandrake";
        }
        else if(thresholdType == ThresholdType.MANDRAKE_THRESHOLD)
        {
            enemyTag = "Gnome";
        }
    }

    void OnTriggerEnter(Collider _collider)
    {
        Debug.Log("TriggerEnter - go to target: " + target.name);
        if(_collider.gameObject.CompareTag(enemyTag))
        {
            Character enemy = _collider.gameObject.GetComponent<Character>();
            enemy.MoveTowardsEnemy(target.transform.position);
        }
    }
}
