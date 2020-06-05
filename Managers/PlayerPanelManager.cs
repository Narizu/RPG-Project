using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelManager : MonoBehaviour
{
    public Text characterText;                                          // Personaje.
    public Text levelText;                                              // Nivel.
    public Text attackText;                                             // Ataque.
    public Text defenseText;                                            // Defensa.
    public Text speedText;                                              // Velocidad.
    public Text healthText;                                             // Salud.
    public Text magicText;                                              // Magia.
    public Text skill1Text;                                             // Habilidad 1.
    public Text skill2Text;                                             // Habilidad 2.
    public Text skill3Text;                                             // Habilidad 3.
    public Text skill4Text;                                             // Habilidad 4.
    public Text skill5Text;                                             // Habilidad 5.

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Jugador.
        PlayerStats playerStats = player.GetComponent<PlayerStats>();   // Estadísticas del jugador.

        characterText.text = PlayerStats.character;                     // Fijar el personaje.
        levelText.text = "" + PlayerExperience.level;                   // Fijar el nivel.
        attackText.text = "" + playerStats.attack;                      // Fijar el ataque.
        defenseText.text = "" + playerStats.defense;                    // Fijar la defensa.
        speedText.text = "" + playerStats.speed;                        // Fijar la velocidad.
        healthText.text = "" + playerStats.health;                      // Fijar la salud.
        magicText.text = "" + playerStats.magic;                        // Fijar la magia.
        skill1Text.text = "Basic attack";                               // Fijar la habilidad 1.
        skill2Text.text = "Recover health";                             // Fijar la habilidad 2.
        skill3Text.text = "Strong attack";                              // Fijar la habilidad 3.
        skill4Text.text = "Increase stats";                             // Fijar la habilidad 4.
        skill5Text.text = "Ultimate attack";                            // Fijar la habilidad 5.
    }

    public void showPlayerPanel()
    {
        gameObject.SetActive(!gameObject.activeSelf);                   // Activar o desactivar el panel del jugador.
    }
}