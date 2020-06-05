using UnityEngine;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour
{
    public AudioMixerSnapshot paused;                   // Referencia al audio pausado.
    public AudioMixerSnapshot unpaused;                 // Referencia al audio no pausado.
    public GameObject panel;                            // Referencia al panel del menú de pausa.
    public GameObject pauseButton;                      // Referencia al botón de pausa.
    public GameObject characterButton;                  // Referencia al botón del personaje.
    public GameObject mapButton;
    public PlayerPanelManager playerPanel;              // Referencia al panel del jugador.
    public GameObject teleportPanel;

    void Start()
    {
        pauseButton.SetActive(true);                    // Activar el botón de pausa.
        characterButton.SetActive(true);                // Activar el botón del personaje.
        mapButton.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))           // Si se pulsa la tecla... 
            Pause();                                    // ...pausar el juego.

        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.I))                // Si se pulsa la tecla...
            playerPanel.showPlayerPanel();              // ...abrir el panel del jugador.

        if (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.M))
            teleportPanel.SetActive(!teleportPanel.activeSelf);
    }

    public void Pause()
    {
        panel.SetActive(!panel.activeSelf);             // Activar o desactivar el panel del menú de pausa.
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;   // Pausar o reanudar el juego.
        Lowpass();                                      // Realizar una transición.
    }

    void Lowpass()
    {
        if (Time.timeScale == 0)                        // Si el juego está pausado...
            paused.TransitionTo(.01f);                  // ...realizar una transición a juego en pausa.

        else                                            // Si no...
            unpaused.TransitionTo(.01f);                // ...realizar una transición a juego funcionando.
    }

    public void Quit()
    {
        Pause();
        LoadingScreen.Instance.LoadScene("Menu");
    }
}