using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaScript : MonoBehaviour
{
    public float speedOscilacion = 0.5f;
    private float posicionInicialY;
    public float distanciaSeparacion = 1.0f;
    void Start()
    {
        posicionInicialY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > posicionInicialY + distanciaSeparacion)
        {
            speedOscilacion = -speedOscilacion;
        }else if (transform.position.y < posicionInicialY - distanciaSeparacion)
        {
            speedOscilacion = -speedOscilacion;
        }
        transform.Translate(Vector3.forward * (speedOscilacion * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CameraController.aumentarContador();
            Destroy(gameObject);
        }
    }
}
