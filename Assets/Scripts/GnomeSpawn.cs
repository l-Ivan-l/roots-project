using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeSpawn : MonoBehaviour
{
    [SerializeField] Gnome gnome;
    [SerializeField] float timeToRespawn = 5;

    private void Start()
    {
        gnome.SetSpawn(this);
    }
    void Respawn()
    {
        gnome.gameObject.transform.position = transform.position;
        gnome.gameObject.transform.rotation = transform.rotation;
        gnome.Number = 0;
        gnome.gameObject.SetActive(true);
    }

    IEnumerator CoDelaySpawn()
    {
        yield return new WaitForSeconds(timeToRespawn);
        Respawn();
    }

    public void DelaySpawn()
    {
        StartCoroutine(CoDelaySpawn());
    }
}
