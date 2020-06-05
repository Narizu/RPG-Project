using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public static GameObject player;

    public PauseManager pauseManager;                                       // Referencia al menú de pausa.
    public CameraFollow cameraFollow;                                       // Referencia a la cámara.
    public FloorFollow floorFollow;                                         // Referencia al suelo.
    public EnemyManager enemyManager;                                       // Referencia al creador de enemigos.
    public NPCManager npcManager;                                           // Referencia al creador de NPCs.
    public ItemManager itemManager;                                         // Referencia al creador de objetos.
    public GameObject minimap;                                              // Referencia al minimapa.
    public GameObject warrior;                                              // Guerrero.
    public GameObject guardian;                                             // Guardián.
    public GameObject hunter;                                               // Cazador.
    public GameObject barbarian;                                            // Bárbaro.
    public GameObject mage;                                                 // Mago.
    public Button warriorButton;
    public Button guardianButton;
    public Button hunterButton;
    public Button barbarianButton;
    public Button mageButton;
    public Button maleButton;
    public Button femaleButton;
    public Sprite[] sprites;

    Canvas canvas;                                                          // Referencia al menú de selección de personaje.
    Vector3 spawnPosition;                                                  // Posición en la que se creará el personaje.
    Quaternion spawnRotation;                                               // Rotación con la que se creará el personaje.
    bool gender;
    bool character;
    int charSelected;
    int genSelected;

    void Awake()
    {
        Time.timeScale = 0;                                                 // Pausar el juego.
        canvas = GetComponent<Canvas>();                                    // Componente del menú de selección de personaje.
        spawnPosition = new Vector3(0f, 0f, 0f);                            // Fijar la posición en la que se creará el personaje.
        spawnRotation = Quaternion.Euler(0f, 0f, 0f);                       // Fijar la rotación con la que se creará el personaje.
        gender = false;
        character = false;
        charSelected = 0;
        genSelected = 0;

        if (SceneManager.GetActiveScene().name != "ChooseYourCharacter")
            NewGame();
    }

    void Update()
    {
        switch (charSelected)
        {
            case 1:
                desactivateClassButtons();
                warriorButton.GetComponent<Image>().sprite = sprites[1];
                break;
            case 2:
                desactivateClassButtons();
                guardianButton.GetComponent<Image>().sprite = sprites[3];
                break;
            case 3:
                desactivateClassButtons();
                hunterButton.GetComponent<Image>().sprite = sprites[5];
                break;
            case 4:
                desactivateClassButtons();
                barbarianButton.GetComponent<Image>().sprite = sprites[7];
                break;
            case 5:
                desactivateClassButtons();
                mageButton.GetComponent<Image>().sprite = sprites[9];
                break;
            default:
                break;
        }

        switch (genSelected)
        {
            case 1:
                desactivateGenderButtons();
                maleButton.GetComponent<Image>().sprite = sprites[11];
                break;
            case 2:
                desactivateGenderButtons();
                femaleButton.GetComponent<Image>().sprite = sprites[13];
                break;
            default:
                break;
        }
    }

    public void SpawnMale()
    {
        PlayerStats.gender = "Male";                                        // Crear un chico.
        gender = true;
        genSelected = 1;
    }

    public void SpawnFemale()
    {
        PlayerStats.gender = "Female";                                      // Crear una chica.
        gender = true;
        genSelected = 2;
    }

    public void SpawnWarrior()
    {
        PlayerStats warriorStats = warrior.GetComponent<PlayerStats>();     // Componente de las estadísticas del Guerrero.
        PlayerStats.character = "Warrior";                                  // Personaje seleccionado.
        warriorStats.strength = 5;                                          // Fuerza del Guerrero.
        warriorStats.resistance = 1;                                        // Resistencia del Guerrero.
        warriorStats.dexterity = 4;                                         // Destreza del Guerrero.
        warriorStats.physique = 2;                                          // Constitución del Guerrero.
        warriorStats.energy = 3;                                            // Energía del Guerrero.

        player = warrior;
        character = true;
        charSelected = 1;
    }

    public void SpawnGuardian()
    {
        PlayerStats guardianStats = guardian.GetComponent<PlayerStats>();   // Componente de las estadísticas del Guardián.
        PlayerStats.character = "Guardian";                                 // Personaje seleccionado.
        guardianStats.strength = 2;                                         // Fuerza del Guardián.
        guardianStats.resistance = 5;                                       // Resistencia del Guardián.
        guardianStats.dexterity = 1;                                        // Destreza del Guardián.
        guardianStats.physique = 3;                                         // Constitución del Guardián.
        guardianStats.energy = 4;                                           // Energía del Guardián.

        player = guardian;
        character = true;
        charSelected = 2;
    }

    public void SpawnHunter()
    {
        PlayerStats hunterStats = hunter.GetComponent<PlayerStats>();       // Componente de las estadísticas del Cazador.
        PlayerStats.character = "Hunter";                                   // Personaje seleccionado.
        hunterStats.strength = 3;                                           // Fuerza del Cazador.
        hunterStats.resistance = 4;                                         // Resistencia del Cazador.
        hunterStats.dexterity = 5;                                          // Destreza del Cazador.
        hunterStats.physique = 1;                                           // Constitución del Cazador.
        hunterStats.energy = 2;                                             // Energía del Cazador.

        player = hunter;
        character = true;
        charSelected = 3;
    }

    public void SpawnBarbarian()
    {
        PlayerStats barbarianStats = barbarian.GetComponent<PlayerStats>(); // Componente de las estadísticas del Bárbaro.
        PlayerStats.character = "Barbarian";                                // Personaje seleccionado.
        barbarianStats.strength = 4;                                        // Fuerza del Bárbaro.
        barbarianStats.resistance = 2;                                      // Resistencia del Bárbaro.
        barbarianStats.dexterity = 3;                                       // Destreza del Bárbaro.
        barbarianStats.physique = 5;                                        // Constitución del Bárbaro.
        barbarianStats.energy = 1;                                          // Energía del Bárbaro.

        player = barbarian;
        character = true;
        charSelected = 4;
    }

    public void SpawnMage()
    {
        PlayerStats mageStats = mage.GetComponent<PlayerStats>();           // Componente de las estadísticas del Mago.
        PlayerStats.character = "Mage";                                     // Personaje seleccionado.
        mageStats.strength = 1;                                             // Fuerza del Mago.
        mageStats.resistance = 3;                                           // Resistencia del Mago.
        mageStats.dexterity = 2;                                            // Destreza del Mago.
        mageStats.physique = 4;                                             // Constitución del Mago.
        mageStats.energy = 5;                                               // Energía del Mago.

        player = mage;
        character = true;
        charSelected = 5;
    }

    public void CreateCharacter()
    {
        PlayerExperience.level = 1;
        PlayerExperience.currentExp = 0f;
        QuestManager.questsDone = 0;
        TeleportManager.maxSceneLevel = 1;

        if (gender && character)
            LoadingScreen.Instance.LoadScene("Level01Tutorial");            // Cargar la escena del Nivel 01.
    }

    void NewGame()
    {
        canvas.enabled = false;                                             // Desactivar el menú de selección de personaje.
        pauseManager.enabled = true;                                        // Activar el menú de pausa.
        cameraFollow.enabled = true;                                        // Activar la cámara.
        floorFollow.enabled = true;                                         // Activar el suelo.
        enemyManager.enabled = true;                                        // Activar el creador de enemigos.
        npcManager.enabled = true;                                          // Activar el creador de NPCs.
        itemManager.enabled = true;                                         // Activar el creador de objetos.
        minimap.SetActive(true);                                            // Activar el minimapa.
        ChangeCharacter();
        Instantiate(player, spawnPosition, spawnRotation);                  // Crear al personaje.
        Time.timeScale = 1;                                                 // Reanudar el juego.
    }

    void ChangeCharacter()
    {
        if (PlayerStats.character == "Warrior")
            player = warrior;

        if (PlayerStats.character == "Guardian")
            player = guardian;

        if (PlayerStats.character == "Hunter")
            player = hunter;

        if (PlayerStats.character == "Barbarian")
            player = barbarian;

        if (PlayerStats.character == "Mage")
            player = mage;
    }

    public void SetSpawnPosition(Vector3 sp)
    {
        spawnPosition = sp;
    }

    
    void desactivateClassButtons()
    {
        warriorButton.GetComponent<Image>().sprite = sprites[0];
        guardianButton.GetComponent<Image>().sprite = sprites[2];
        hunterButton.GetComponent<Image>().sprite = sprites[4];
        barbarianButton.GetComponent<Image>().sprite = sprites[6];
        mageButton.GetComponent<Image>().sprite = sprites[8];
    }

    void desactivateGenderButtons()
    {
        maleButton.GetComponent<Image>().sprite = sprites[10];
        femaleButton.GetComponent<Image>().sprite = sprites[12];
    }
    
}