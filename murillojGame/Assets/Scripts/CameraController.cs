using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static int contadorBanana = 0;
    public int aux = 0;
    public GameObject player;
    public float distanciaPlayer = 10.0f;
    public AudioClip[] Clips;
    
    private static bool personajeMuerto = false;
    private AudioSource _audioSource;
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + distanciaPlayer, 2.0f, player.transform.position.z);
        aux = contadorBanana;
        if (personajeMuerto)
        {
            _audioSource.PlayOneShot(Clips[0],1.0f);
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
    
}
