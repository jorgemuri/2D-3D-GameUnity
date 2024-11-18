using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float distanciaPlayer = 10.0f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + distanciaPlayer, 2.0f, player.transform.position.z);
    }
    
}
