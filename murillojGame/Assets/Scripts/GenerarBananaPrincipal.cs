using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerarBananaPrincipal : MonoBehaviour
{
    public GameObject prefabBanana;

    private Vector3 _posGenerador;

    private float _time;
    // Start is called before the first frame update
    void Start()
    {
        _posGenerador = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _time = _time+Time.deltaTime;

        if (_time > 4.0f)
        {
            int rand = Random.Range(0, 3);
            int rand2 = Random.Range(0, 3);

            if (rand == rand2)
            {
                Instantiate(prefabBanana, _posGenerador, Quaternion.identity);
            }
            _time = 0.0f;
        }
    }
}
