using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorBolas : MonoBehaviour
{ 
    public GameObject bolaPrefab;
    private float time = 0.0f;
    public float fuerzaBola = 5.0f;
    private GameObject bola;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 2.0f)
        {
            if (bola) //Borro la anterior
            {
                Destroy(bola);
            }
            bola = Instantiate(bolaPrefab, transform.position, Quaternion.identity);
            bola.GetComponent<Rigidbody>().AddForce(new Vector3(1.0f,0.0f,0.0f)* fuerzaBola);
            
            time = 0.0f;
        }
    }
}
