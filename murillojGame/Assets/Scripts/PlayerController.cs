
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
    private bool _facingRight = true; // Variable para saber si está mirando a la derecha
    private bool _isRunning = false;
    private bool _superSalto = true;
    
    private Animator _animatorWings;
    private Transform _transformWings;
    private AudioSource _audioSource;
    
    private float speed;
    
    
    public float velocidad = 5.0f;
    public float velocidadCorriendo = 10.0f;
    public float fuerzaSalto = 10.0f;
    public float velocidadGiro = 10.0f; // Velocidad de rotación para que sea suave
    public GameObject wings;
    public AudioClip[] Clips;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = velocidad;
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        
        _animatorWings = wings.GetComponent<Animator>();
        _transformWings = wings.GetComponent<Transform>();
        _transformWings.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        _z = (Input.GetAxis("Horizontal")); //Quiero que se mueva para atrás

        // Verificar si el personaje debe voltearse
        if (_z > 0 && !_facingRight)
        {
            Flip();
        }
        else if (_z < 0 && _facingRight)
        {
            Flip();
        }

        //SI SE ESTÁ MOVIENDO
        if (_z != 0)
        {
            _animator.SetBool("isMoving", true);
            
            if (Input.GetKeyDown(KeyCode.LeftShift) && _isGrounded) //Si está en el suelo y corriendo
            {
                speed = velocidadCorriendo;
                _animator.SetFloat("runningSpeed",2f);
                _isRunning = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) && _isRunning) //Si he levantado el control después de correr
            {
                speed = velocidad;
                _animator.SetFloat("runningSpeed",1f);
                _isRunning = false;
            }
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }
        //Compruebo si está corriendo
       
        // Mover al personaje en el eje X (izquierda o derecha)
        transform.Translate(transform.forward * (_z * speed * Time.deltaTime));
        
        // Salto
        if (Input.GetButtonDown("Jump"))
        {
            //salto normal
            if (_isGrounded)
            {
                _rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
                _animator.SetTrigger("jump");
                
                PlaySound(0,0.1f); //Salto
            }
            else if (_superSalto) //salto con las alas
            {
                _rb.AddForce(Vector3.up * (fuerzaSalto + 2.0f), ForceMode.Impulse);
                _transformWings.localScale = new Vector3(0.5f,0.5f,0.5f);
                _animatorWings.SetBool("aletear",true);
                _superSalto = false;
                PlaySound(1,1f); //Aleteo
            }
        }

        if (transform.position.y < -10)
        {
            matarPersonaje();
        }
    }

    private void Flip()
    {
        // Cambiar la dirección en que está mirando
        _facingRight = !_facingRight;

        transform.rotation = Quaternion.Euler(0, _facingRight ? 0 : 180, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            if (_animatorWings.GetBool("aletear"))
            {
                _animatorWings.SetBool("aletear",false);
                _transformWings.localScale = Vector3.zero;
                _superSalto = true;
            }
        }

        if (collision.gameObject.CompareTag("Ghost"))
        {
            BoxCollider box = collision.collider as BoxCollider;
            if (box != null)
            {
                // Obtener la posición del contacto
                foreach (ContactPoint contact in collision.contacts)
                {
                    // Obtener las coordenadas superiores del BoxCollider
                    Vector3 boxTopCenter = box.bounds.center + new Vector3(0, box.bounds.extents.y, 0);

                    // Verificar si el contacto está cerca de la parte superior
                    if (Mathf.Abs(contact.point.y - boxTopCenter.y) < 0.1f)
                    {
                        PlaySound(2, 0.5f);
                        _rb.AddForce(Vector3.up * fuerzaSalto * 0.5f, ForceMode.Impulse);
                        enemigoDead(collision.gameObject);
                    }
                    else
                    {
                       matarPersonaje();
                    }
                }
            }
        }

        if (collision.gameObject.CompareTag("banana"))
        {
            PlaySound(3,0.5f);
            CameraController.aumentarContador();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("champinion"))
        {
            _rb.AddForce(Vector3.up * fuerzaSalto * 2.0f, ForceMode.Impulse);
        }
        
        if (collision.gameObject.CompareTag("end"))
        {
            //Hacer que sale
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }

    private void matarPersonaje()
    {
        CameraController.setPersonajeMuerto();
        Destroy(gameObject);
    }
    
    private void PlaySound(int soundIndex, float volume)
    {
        if (_audioSource != null && Clips != null && soundIndex >= 0 && soundIndex < Clips.Length)
        {
            // Asegurarse de que el volumen esté dentro del rango válido
            volume = Mathf.Clamp(volume, 0f, 1f);
            if (gameObject)
            {
                _audioSource.PlayOneShot(Clips[soundIndex],volume);
            }
            
        }
    }

    private void enemigoDead(GameObject enemigo)
    {
        enemigo.transform.rotation = Quaternion.Euler(0,90,0);
        Animator animatorEnemy = enemigo.GetComponent<Animator>();
        animatorEnemy.SetTrigger("death");
        // Destruir el objeto después de la duración de la animación
        float deathAnimationLength = animatorEnemy.GetCurrentAnimatorStateInfo(0).length;
        Destroy(enemigo, deathAnimationLength);
    }
}
