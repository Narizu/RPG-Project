using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int level;                                       // Nivel.
    public float strength;                                  // Fuerza.
    public float resistance;                                // Resistencia.
    public float dexterity;                                 // Destreza.
    public float physique;                                  // Constitución.
    public float attack;                                    // Ataque.
    public float defense;                                   // Defensa.
    public float speed;                                     // Velocidad.
    public float health;                                    // Salud.

    int var;                                                // Variable

    void Awake()
    {
        var = 10;                                           // Fijar la variable.
        health = physique * var * level;                    // Calcular la salud.
    }

    void Update()
    {
        attack = strength * var * level;                    // Calcular el ataque.
        defense = resistance * var * level;                 // Calcular la defensa.
        speed = dexterity * var * level;                    // Calcular la velocidad.
        health = physique * var * level;                    // Calcular la salud.
    }
}