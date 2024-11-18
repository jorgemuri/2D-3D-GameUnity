using System.Collections;
using UnityEngine;

public class ScriptBola : MonoBehaviour
{
    public GameObject prefab;

    private int numeroBolas = 10;

    private float size = 1.0f;
    public float valorDecrementarBola = 0.01f;
    
    private ArrayList bolas = new ArrayList();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numeroBolas; i++)
        {
            //POSICIÓN
            Vector3 pos = new Vector3(
                Random.Range(-15.0f, 15.0f),
                Random.Range(0.0f, 10.0f),
                Random.Range(-15.0f, 15.0f));
            GameObject go = Instantiate(prefab, pos, Quaternion.identity);
            
            //TAMAÑO
            size = Random.Range(1.0f,10.0f);
            go.transform.localScale = new Vector3(size,size,size);
            
            //COLOR
            MeshRenderer mr = go.GetComponent<MeshRenderer>();
            mr.material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

            bolas.Add(go);
        }    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < bolas.Count; i++)
        {
            GameObject bola = (GameObject) bolas[i];
            if (bola.transform.localScale.x >= 1.0f)
            {
                bola.transform.localScale = new Vector3(bola.transform.localScale.x - valorDecrementarBola,bola.transform.localScale.y - valorDecrementarBola,bola.transform.localScale.z - valorDecrementarBola);
            }
            //Compruebo si ha bajado del nivel -10.0f
            if (bola.transform.position.y <= -10.0f)
            {
                bolas.Remove(bola);
                Destroy(bola);
                
            }
        }
    }
}
