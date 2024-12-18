using UnityEngine;


public class PauseGame : MonoBehaviour
{
    private bool isPaused = false; // Estado de pausa
    public GameObject canvaPausa;

    public void tocarPausa()
    {
        if (isPaused == false)
        {
            pausar();
        }
        else
        {
            resumeGame();
        }
    }

    private void pausar()
    {
        Time.timeScale = 0f; // Detener el tiempo
        canvaPausa.SetActive(true);
        isPaused = true;
    }

    private void resumeGame()
    {
        Time.timeScale = 1f; // Reanudar el tiempo
        canvaPausa.SetActive(false);
        isPaused = false;
    }
   
}
