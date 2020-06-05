 using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    public static string gender;                                // Género.
    public static string character;                             // Personaje.

    public GameObject warrior;
    public GameObject guardian;
    public GameObject hunter;
    public GameObject barbarian;
    public GameObject mage;
    public int strength;                                        // Fuerza.
    public int resistance;                                      // Resistencia.
    public int dexterity;                                       // Destreza.
    public int physique;                                        // Constitución.
    public int energy;                                          // Energía.
    public int attack;                                          // Ataque.
    public int defense;                                         // Defensa.
    public int speed;                                           // Velocidad.
    public int health;                                          // Salud.
    public int magic;                                           // Magia.

    int var;                                                    // Variable.

    void Start()
    {
        var = 10;                                               // Fijar la variable.
        health = physique * var * PlayerExperience.level;       // Calcular la salud.
        magic = energy * var * PlayerExperience.level;          // Calcular la magia.
    }

    void Update()
    {
        attack = strength * var * PlayerExperience.level;       // Calcular el ataque.
        defense = resistance * var * PlayerExperience.level;    // Calcular la defensa.
        speed = dexterity * var * PlayerExperience.level;       // Calcular la velocidad.
        health = physique * var * PlayerExperience.level;       // Calcular la salud.
        magic = energy * var * PlayerExperience.level;          // Calcular la magia.

        if (PlayerManager.player.name != character)
            ChangeCharacter();
    }

    void SpawnWarrior()
    {
        character = "Warrior";
        strength = 5;
        resistance = 1;
        dexterity = 4;
        physique = 2;
        energy = 3;
        PlayerManager.player = warrior;
    }

    void SpawnGuardian()
    {
        character = "Guardian";
        strength = 2;
        resistance = 5;
        dexterity = 1;
        physique = 3;
        energy = 4;
        PlayerManager.player = guardian;
    }

    void SpawnHunter()
    {
        character = "Hunter";
        strength = 3;
        resistance = 4;
        dexterity = 5;
        physique = 1;
        energy = 2;
        PlayerManager.player = hunter;
    }

    void SpawnBarbarian()
    {
        character = "Barbarian";
        strength = 4;
        resistance = 2;
        dexterity = 3;
        physique = 5;
        energy = 1;
        PlayerManager.player = barbarian;
    }

    void SpawnMage()
    {
        character = "Mage";
        strength = 1;
        resistance = 3;
        dexterity = 2;
        physique = 4;
        energy = 5;
        PlayerManager.player = mage;
    }

    void ChangeCharacter()
    {
        if (character == "Warrior")
            SpawnWarrior();
        if (character == "Guardian")
            SpawnGuardian();
        if (character == "Hunter")
            SpawnHunter();
        if (character == "Barbarian")
            SpawnBarbarian();
        if (character == "Mage")
            SpawnMage();
    }
}