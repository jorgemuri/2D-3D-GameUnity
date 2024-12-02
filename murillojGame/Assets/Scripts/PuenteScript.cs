using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuenteScript : MonoBehaviour
{
    private float speed = 2.0f;
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("tope"))
        {
            speed  = - speed;
        }
    }
    
    
}
