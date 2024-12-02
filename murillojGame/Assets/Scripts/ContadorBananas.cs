using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContadorBananas : MonoBehaviour
{
    private int contador;
    public Text contadorText;
    // Start is called before the first frame update
    void Start()
    {
        contadorText.text = "0";
    }

    public void aumentarContador()
    {
        contador++;
        contadorText.text = contador.ToString();
    }
}
