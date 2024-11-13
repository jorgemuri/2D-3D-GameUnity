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
            _animator.SetBool("isMoving", true);
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }
       transform.Translate( 0f, 0f, - (_z * velocidad * Time.deltaTime));

       // Llamamos a la función que realiza el Raycast
      
       
       if (_isGrounded  && Input.GetButtonDown("Jump"))
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
}
