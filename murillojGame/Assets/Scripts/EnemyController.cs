using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 5.0f;
    public float distanceMax = 3.0f;
    
    private Animator _animator;
    private bool _movingRight = true;

    private Vector3 _posicionincial;

    private float _tiempoFueraRango;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _posicionincial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < _posicionincial.z-distanceMax || transform.position.z > _posicionincial.z+distanceMax)
        {
            _tiempoFueraRango = _tiempoFueraRango+Time.deltaTime;
            if (_tiempoFueraRango > 1.0f)
            {
                transform.position = new Vector3(_posicionincial.x, _posicionincial.y, _posicionincial.z);
                _tiempoFueraRango = 0.0f;
            }
            Rotar();
        }
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));    
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
