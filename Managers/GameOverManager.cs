using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;                                               // Referencia a la salud del jugador.
    public float restartDelay = 5f;                                                 // Tiempo de espera antes de reiniciar el nivel.

    Animator anim;                                                                  // Referencia a la animación.
    float restartTimer;                                                             // Tiempo para contar hasta reiniciar el nivel.

    void Awake()
    {
        anim = GetComponent<Animator>();                                            // Componente de la animación.
    }

    void Update()
    {
        if (playerHealth.currentHealth <= 0)                                        // Si el jugador se ha quedado sin salud...
        {
            anim.SetTrigger("GameOver");                                            // ...decir a la animación que el juego se ha terminado.

            restartTimer += Time.deltaTime;                                         // ...incrementar el tiempo para contar hasta reiniciar el nivel.

            if (restartTimer >= restartDelay)                                       // ...si alcanza el tiempo de reiniciar...
            {
                if (SceneManager.GetActiveScene().name.Contains("Dungeon"))
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);   // ...reiniciar el nivel actual.
                else
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);       // ...reiniciar el nivel actual.
            }
        }
    }
}