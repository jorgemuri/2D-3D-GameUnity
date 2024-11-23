using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    private static int contadorBanana = 0;
    private bool _sonado = false;
    
    public int aux = 0;
    public GameObject player;
    public float distanciaPlayer = 10.0f;
    public AudioClip[] Clips;
    public GameObject canva;
    
    private static bool personajeMuerto = false;
    private AudioSource _audioSource;
    
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
            transform.position = new Vector3(player.transform.position.x + distanciaPlayer, 2.0f, player.transform.position.z);
            aux = contadorBanana;
        }
    }

    public static void aumentarContador()
    {
        contadorBanana++;
    }

    public static int getContadorBanana()
    {
        return contadorBanana;
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
    
}
