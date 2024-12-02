using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 5.0f;
    
    private Animator _animator;
    private bool _movingRight = true;
    public GameObject cabeza;
    private float posicionInicialy;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        posicionInicialy = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        cabeza.transform.position = new Vector3(transform.position.x, posicionInicialy+4.0f, transform.position.z);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("tope"))
        {
            Rotar();
        }
    }
    private void Rotar()
    {
        if (_movingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _movingRight = false;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _movingRight = true;
        }
        
    }
    
}
