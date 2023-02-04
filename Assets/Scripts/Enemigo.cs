using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float speed;
   // public Transform Target;
    // Start is called before the first frame update
    void Start()
    {
       // transform.LookAt(new Vector3(Target.position.x,transform.position.y, Target.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-speed * Time.deltaTime, 0,0));


    }


     void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        
    }
}
