using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score;                                // Puntuación del jugador.
    public static int items;                                // Objetos recogidos por el jugador.
    public static string questText;
    public static int questNumber;

    public QuestManager questManager;

    Text text;                                              // Referencia al texto.

    void Awake()
    {
        text = GetComponent<Text>();                        // Componente del texto.
        score = 0;                                          // Reiniciar la puntuación.
        items = 0;                                          // Reiniciar los objetos.
    }

    void Update()
    {
        if (questText == "Item")
            text.text = "Collected Items: " + items + "/" + questNumber;
        if (questText == "Enemy")
            text.text = "Defeated Enemies: " + score + "/" + questNumber;
        if (questText == "Level")
            text.text = "Level Required: " + PlayerExperience.level + "/" + questNumber;

        questManager.selectQuest();
    }
}