using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _isGrounded;    
    private float _z;
    private Animator _animator;
    private bool _facingRight = true; // Variable para saber si está mirando a la derecha
    private bool _isRunning;
    private bool _superSalto = true;
    private bool _isOnBridge;
    private float diferencia;
    private GameObject _bridge;
    
    private Animator _animatorWings;
    private Transform _transformWings;
    private AudioSource _audioSource;
    
    private float speed;

    public GameObject contadorBanana;
    public float velocidad = 5.0f;
    public float velocidadCorriendo = 10.0f;
    public float fuerzaSalto = 10.0f;
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
                if (speed  < 0)
                {
                    speed = -velocidad;
                }
                else
                {
                    speed = velocidad;
                }
                _animator.SetFloat("runningSpeed",1f);
                _isRunning = false;
            }
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }
        
        //MIRO SI ESTÁ ENCIMA DEL PUENTE
        if (_isOnBridge && _z == 0)
        {
            Vector3 vector3 = transform.position;
            vector3.z = _bridge.transform.position.z - diferencia;
            transform.position = vector3;
        }else if (_isOnBridge && _z != 0)
        {
            diferencia = _bridge.transform.position.z - transform.position.z;
        }
        
        // Mover al personaje en el eje X (izquierda o derecha)
        
            // Calcula la dirección en la que te quieres mover
            Vector3 moveDirection = transform.forward * (_z * speed * Time.deltaTime);
    
            // Mueve el personaje utilizando Rigidbody
            _rb.MovePosition(_rb.position + moveDirection);

        //transform.Translate(transform.forward * (_z * speed * Time.deltaTime));
        
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
        speed = speed * -1;
        
        // Cambiar la dirección en que está mirando
        _facingRight = !_facingRight;
        

        transform.rotation = Quaternion.Euler(0, _facingRight ? 0 : 180, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                _isGrounded = true;
                comprobarSupersalto(); 
                break;
            case "Ghost":
                matarPersonaje();
                break;
            case "headEnemy":
                PlaySound(2, 0.5f);
                _rb.velocity = new Vector3(0.0f,0.0f,0.0f);
                _rb.AddForce(Vector3.up * fuerzaSalto * 2f, ForceMode.Impulse);
                Transform padre = collision.transform.parent;
                Transform fantasma = padre.transform.GetChild(0);
                enemigoDead(fantasma.gameObject);
                //pongo la posición del jugador en 0 en el eje x, para que no se vaya saliendo de la linea
                var vector3 = transform.position;
                vector3.x = 0;
                transform.position = vector3;
                break;
            case "champinion":
                _rb.AddForce(Vector3.up * fuerzaSalto * 2.0f, ForceMode.Impulse);
                PlaySound(4,1.0f);
                break;
            case "end":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case "puente":
                if (!_isOnBridge) //Si es la primera vez que choca antes de salir del puente
                {
                    diferencia = collision.transform.position.z - transform.position.z;
                }
                _isOnBridge = true;
                _isGrounded = true;
           
                comprobarSupersalto();
                _bridge = collision.gameObject;
                break;
            case "bola":
                _rb.AddForce(new Vector3(1.0f,0.0f,0.0f)* 300.0f, ForceMode.Impulse);
                break;
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("banana"))
        {
            PlaySound(3,0.5f);
            
            contadorBanana.GetComponent<ContadorBananas>().aumentarContador();
            
            Destroy(other.gameObject);
        }
    }

    private void comprobarSupersalto()
    {
        if (_animatorWings.GetBool("aletear"))
        {
            _animatorWings.SetBool("aletear",false);
            _transformWings.localScale = Vector3.zero;
            _superSalto = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
        else if (collision.gameObject.CompareTag("puente"))
        {
            _isGrounded = false;
            _isOnBridge = false;
        }
    }

    private void matarPersonaje()
    {
        CameraController.setPersonajeMuerto();
        Destroy(gameObject);
    }
    
    private void PlaySound(int soundIndex, float volume)
    {
        if (_audioSource && Clips != null && soundIndex >= 0 && soundIndex < Clips.Length)
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
