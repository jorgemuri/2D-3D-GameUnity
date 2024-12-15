using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    private bool _sonado;
    
    public GameObject player;
    public float distanciaPlayer = 10.0f;
    public float altura = 2.0f; // Altura de la cámara respecto al jugador
    public float suavidad = 0.2f; // Cuánto tarda la cámara en llegar al objetivo
    public AudioClip[] Clips;
    public GameObject canva;
    
    private static bool personajeMuerto = false;
    private AudioSource _audioSource;
    private Vector3 _velocidadSuavizada; // Para SmoothDamp
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (personajeMuerto)
        {
            if (_sonado == false)
            {
                _audioSource.PlayOneShot(Clips[0],1.0f);
                canva.SetActive(true);
                _sonado = true;

            }
        }
        else
        {
            // Posición deseada de la cámara
            Vector3 posicionObjetivo = new Vector3(
                player.transform.position.x + distanciaPlayer, 
                altura, 
                player.transform.position.z
            );

            // Movimiento suave usando SmoothDamp
            transform.position = Vector3.SmoothDamp(transform.position, posicionObjetivo, ref _velocidadSuavizada, suavidad);
        }
    }
    public static void setPersonajeMuerto()
    {
        personajeMuerto = true;
    }
    public void ReloadCurrentScene()
    {
        // Obtiene el nombre de la escena actual
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Recarga la escena actual
        SceneManager.LoadScene(currentSceneName);
        canva.SetActive(false);
        personajeMuerto = false;
    }

    public void exitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
    
}
