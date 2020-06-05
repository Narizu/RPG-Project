using UnityEngine;
using UnityEngine.UI;

public class PlayerExperience : MonoBehaviour
{
    public static int level = 1;                                                    // Nivel del jugador.
    public static float currentExp = 0;

    public Slider expSlider;                                                    // Referencia a la barra de experiencia.
    public Image expFill;                                                       // Color de la barra de experiencia.
    public Text expText;                                                        // Texto de la experiencia.
    public Text levelText;                                                      // Texto del nivel del jugador.
    public AudioClip levelClip;                                                 // Clip de audio para cuando el jugador sube de nivel.
    //public float currentExp;                                                    // Experiencia actual que tiene el jugador.

    PlayerHealth playerHealth;                                                  // Referencia a la salud del jugador.
    PlayerSkin playerSkin;                                                      // Referencia a la apariencia del jugador.
    AudioSource playerAudio;                                                    // Referencia al audio.
    float propExp;                                                              // Experiencia proporcional.
    float maxExp;                                                               // Experiencia máxima.

    void Awake()
    {
        //level = 1;                                                              // Fijar el nivel inicial del jugador.
        //currentExp = 0f;                                                        // Fijar la experiencia inicial del jugador.

        playerHealth = GetComponent<PlayerHealth>();                            // Componente de la salud del jugador.
        playerSkin = GetComponent<PlayerSkin>();                                // Componente de la apariencia del jugador.
        playerAudio = GetComponent<AudioSource>();                              // Componente del audio.
        //maxExp = 100f;                                                          // Fijar la experiencia máxima.
        expText.color = new Color(0, 0, 0, 1);
    }

    void Start()
    {
        expSlider.value = currentExp;
    }

    void Update()
    {
        maxExp = 100 * Mathf.Pow(2f, level - 1);                                // Fijar la experiencia máxima.
        expSlider.maxValue = maxExp;                                            // Fijar el valor máximo de la barra de experiencia.

        levelText.text = "" + level;                                            // Fijar el texto para mostrar el nivel del jugador.
        expText.text = currentExp + "/" + maxExp;                               // Fijar texto de la experiencia.

        expSlider.value = currentExp;                                           // Fijar el valor de la barra de experiencia a la experiencia actual.
        ChangeColor();                                                          // Cambiar el color de la barra de experiencia.
    }

    public void TakeExp(float amount)
    {
        currentExp += amount;                                                   // Aumentar la experiencia actual por la cantidad ganada.

        if (currentExp >= maxExp)                                               // Si el jugador tiene más del máximo de su experiencia...
        {
            playerAudio.clip = levelClip;                                       // ...poner el clip de audio de subir de nivel.
            playerAudio.Play();                                                 // ...encender el audio.

            level++;                                                            // ...subir de nivel.
            currentExp = 0f;                                                    // ...fijar la experiencia del jugador al mínimo.
            playerHealth.CreateText("Level up!", 55, new Color(0, 1, 1, 1));    // ...crear texto del nivel subido.
            /*
            if (level <= 10)                                                    // ...si el nivel no es superior al nivel máximo...
                playerSkin.LevelUp(level - 1);                                  // ...cambiar la apariencia del jugador.*/
        }
        else
            playerHealth.CreateText("+" + amount, 50, new Color(0, 1, 1, 1));
    }

    void ChangeColor()
    {
        propExp = currentExp / maxExp;                                          // Calcular la experiencia proporcional.
        float x = 1 - (1 - propExp) * 2;                                        // Calcular el color.

        if (currentExp >= maxExp / 2)                                           // Si la experiencia está por encima del 50%...
        {
            expFill.color = new Color(0, 2 * (1 - propExp), 1, 1);              // ...cambiar el color de cian a azul.
            //expText.color = new Color(x, x, x, 1);                              // ...cambiar el color del texto.
        }
        else                                                                    // Si no...
        {
            expFill.color = new Color(0, 1, 2 * propExp, 1);                    // ...cambiar el color de verde a cian.
            //expText.color = new Color(0, 0, 0, 1);                              // ...cambiar el color del texto.
        }
    }
}