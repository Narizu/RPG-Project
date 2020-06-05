using UnityEngine;
using UnityEngine.UI;

public class PlayerMagic : MonoBehaviour
{
    public Slider magicSlider;                                                          // Referencia a la barra de magia.
    public Image magicFill;                                                             // Color de la barra de magia.
    public Text magicText;                                                              // Texto de la magia.
    public float currentMagic;                                                          // Magia actual que tiene el jugador.

    PlayerStats playerStats;                                                            // Referencia a las estadísticas del jugador.
    bool firstTime;                                                                     // Cuando es la primera vez.
    float timer;                                                                        // Tiempo para contar cuando se hace la siguiente regeneración.
    float timeBetweenRegenerations;                                                     // Tiempo en segundos entre cada regeneración.
    float timerSpell;                                                                   // Tiempo para contar cuando se empieza a regenerar.
    float timeStartRegeneration;                                                        // Tiempo en segundos para empezar a regenerarse.
    float maxMagic;                                                                     // Magia máxima del jugador.
    float propMagic;                                                                    // Magia proporcional.
    int var;                                                                            // Variable.

    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();                                      // Componente de las estadísticas del jugador.
        currentMagic = 100f;                                                            // Fijar la magia inicial del jugador.
        firstTime = true;                                                               // Fijar la primera vez a verdadero.
        timeBetweenRegenerations = 0.1f;                                                // Fijar el tiempo entre regeneraciones.
        timeStartRegeneration = 3f;                                                     // Fijar el tiempo para empezar a regenerarse.
        var = 10;                                                                       // Fijar la variable.
        magicText.color = new Color(0, 0, 0, 1);
    }

    void Update()
    {
        maxMagic = playerStats.magic * var;                                             // Fijar la magia máxima del jugador.
        magicSlider.maxValue = maxMagic;                                                // Fijar el valor máximo de la barra de magia del jugador.

        if (firstTime)                                                                  // Si es la primera vez...
        {
            firstTime = false;                                                          // ...fijar la primera vez a falso.
            magicSlider.value = maxMagic;                                               // ...fijar el valor inicial de la barra de magia del jugador.
            currentMagic = maxMagic;                                                    // ...fijar la magia inicial del jugador.
        }

        timer += Time.deltaTime;                                                        // Añadir el tiempo desde la última vez que se llamó a esta función.
        timerSpell += Time.deltaTime;                                                   // Añadir el tiempo desde la última vez que se llamó a esta función.

        if (timerSpell >= timeStartRegeneration && timer >= timeBetweenRegenerations    // Si es tiempo para regenerarse y
            && currentMagic < maxMagic)                                                 // el jugador no tiene la magia al máximo...
            Regeneration();                                                             // ...regenerarse.

        magicText.text = (int)currentMagic + "/" + maxMagic;                            // Fijar texto de la magia.
    }

    public void TakeMagic(int amount)
    {
        timerSpell = 0f;                                                                // Reiniciar el tiempo.

        currentMagic -= amount;                                                         // Reducir la magia actual por la cantidad usada.

        if (currentMagic < 0)                                                           // Si el jugador tiene menos del mínimo de su magia...
            currentMagic = 0;                                                           // ...fijar la magia del jugador al mínimo.

        magicSlider.value = currentMagic;                                               // Fijar el valor de la barra de magia a la magia actual.

        ChangeColor();                                                                  // Cambiar el color de la barra de magia.
    }

    void Regeneration()
    {
        timer = 0f;                                                                     // Reiniciar el tiempo.

        currentMagic += (maxMagic * 0.005f);                                            // Aumentar la magia actual por la cantidad de regeneración.

        if (currentMagic > maxMagic)                                                    // Si el jugador tiene más del máximo de su magia...
            currentMagic = maxMagic;                                                    // ...fijar la magia del jugador al máximo.

        magicSlider.value = currentMagic;                                               // Fijar el valor de la barra de magia a la magia actual.

        ChangeColor();                                                                  // Cambiar el color de la barra de magia.
    }

    void ChangeColor()
    {
        propMagic = currentMagic / maxMagic;                                            // Calcular la magia proporcional.
        float x = 1 - (1 - propMagic) * 2;                                              // Calcular el color.

        if (currentMagic >= maxMagic / 2)                                               // Si la magia está por encima del 50%...
        {
            magicFill.color = new Color(2 * (1 - propMagic), 0, 1, 1);                  // ...cambiar el color de azul a violeta.
            //magicText.color = new Color(x, x, x, 1);                                    // ...cambiar el color del texto.
        }
        else                                                                            // Si no...
        {
            magicFill.color = new Color(1, 0, 2 * propMagic, 1);                        // ...cambiar el color de violeta a rojo.
            //magicText.color = new Color(0, 0, 0, 1);                                   // ...cambiar el color del texto.
        }
    }
}