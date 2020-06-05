using UnityEngine;
using UnityEngine.UI;

public class EnemyPanelManager : MonoBehaviour
{
    public Canvas healthUI;                                             // Barra de salud.
    public Slider healthSlider;                                         // Valor de la barra de salud.
    public Image healthFill;                                            // Color de la barra de salud.
    public Text healthText;                                             // Texto de la barra de salud.
    public Text levelText;                                              // Texto del nivel.

    public void showEnemyPanel()
    {
        GetComponent<Image>().enabled = true;                           // Activar el panel del enemigo.
        healthUI.enabled = true;                                        // Activar la barra de salud.
        levelText.enabled = true;                                       // Activar el texto del nivel.
    }

    public void hideEnemyPanel()
    {
        GetComponent<Image>().enabled = false;                          // Desactivar el panel del enemigo.
        healthUI.enabled = false;                                       // Desactivar la barra de salud.
        levelText.enabled = false;                                      // Desactivar el texto del nivel.
    }

    public void updateEnemyPanel(GameObject enemy)
    {
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();    // Salud del enemigo.
        EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();       // Estadísticas del enemigo.

        healthSlider.maxValue = enemyHealth.healthSlider.maxValue;      // Fijar el valor máximo de la barra de salud.
        healthSlider.value = enemyHealth.healthSlider.value;            // Fijar el valor actual de la barra de salud.
        healthFill.color = enemyHealth.healthFill.color;                // Fijar el color de la barra de salud.
        healthText.text = enemyHealth.healthText.text;                  // Fijar el texto de la barra de salud.
        healthText.color = enemyHealth.healthText.color;                // Fijar el color del texto de la barra de salud.
        levelText.text = enemy.name.Split('(')[0] + " - Level: " + enemyStats.level;                  // Fijar el texto del nivel.
    }
}