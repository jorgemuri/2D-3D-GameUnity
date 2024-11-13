using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _isGrounded;    
    private float _z;
    private Animator _animator;

    // Controla si el personaje está mirando hacia la derecha o izquierda
    private bool facingRight = true;

    public float velocidad = 5.0f;
    public float fuerzaSalto = 10.0f;
    public float raycastDistance = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _z = Input.GetAxis("Horizontal");

        if (_z != 0) //Se está moviendo
        {
            // Si se mueve hacia la izquierda y está mirando a la derecha, voltear
            if (_z < 0 && facingRight)
            {
                flip();
            }
            // Si se mueve hacia la derecha y está mirando a la izquierda, voltear
            else if (_z > 0 && !facingRight)
            {
                flip();
            }

            _animator.SetBool("isMoving", true);
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }

        // Mover al personaje en el eje Z (hacia adelante o hacia atrás)
        transform.Translate(0f, 0f, _z * velocidad * Time.deltaTime);

        // Salto
        if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            _rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            _animator.SetTrigger("jump");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }

    // Método para voltear al personaje
    private void flip()
    {
        facingRight = !facingRight;

        // Cambiar la escala en X a su valor negativo
        Vector3 scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;
    }
}
