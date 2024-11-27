using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaMenu : MonoBehaviour
{
    // Velocidad de rotación en grados por segundo
    public float rotationSpeed = 10f;

    void Update()
    {
        // Calcula la rotación para este frame
        float rotationAmount = rotationSpeed * Time.deltaTime;

        // Aplica la rotación en el eje Z
        transform.Rotate(0.0f, 0f, rotationAmount);
        if (transform.position.y < -100.0f)
        {
            Destroy(gameObject);
        }
    }
}
