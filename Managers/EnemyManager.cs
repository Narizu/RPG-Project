using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject enemyBoss;                                                                                // Jefe enemigo que va a ser creado.
    public LayerMask mask;                                                                                      // Capa del suelo.
    public int maxEnemies;                                                                                      // Número máximo de enemigos.
    public int maxBosses;
    public int level;
    public float strength;
    public float resistance;
    public float physique;
    public float xMin;                                                                                          // Rango de la posición en la que puede aparecer el enemigo.
    public float xMax;                                                                                          // Rango de la posición en la que puede aparecer el enemigo.
    public float zMin;                                                                                          // Rango de la posición en la que puede aparecer el enemigo.
    public float zMax;                                                                                          // Rango de la posición en la que puede aparecer el enemigo.

    GameObject enemy;                                                                                           // Enemigo que va a ser creado.
    float spawnTime;                                                                                            // Tiempo entre cada creación.
    float spawnBoss;                                                                                            // Tiempo entre cada creación.
    GameObject player;                                                                                          // Referencia al jugador.
    PlayerHealth playerHealth;                                                                                  // Referencia a la salud del jugador.
    float rotRange;                                                                                             // Rango de la rotación en la que puede aparecer el enemigo.
    GameObject[] currentEnemies;                                                                                // Número actual de enemigos.
    Vector3 spawnPosition;                                                                                      // Posición en la que van a aparecer los enemigos.
    Quaternion spawnRotation;                                                                                   // Rotación en la que van a aparecer los enemigos.
    float radius;                                                                                               // Radio del enemigo.

    void Start()
    {
        spawnTime = Random.Range(3f, 6f);                                                                       // Fijar el tiempo entre cada creación.
        spawnBoss = Random.Range(30f, 60f);                                                                     // Fijar el tiempo entre cada creación.

        player = GameObject.FindGameObjectWithTag("Player");                                                    // Componente del jugador.
        playerHealth = player.GetComponent<PlayerHealth>();                                                     // Componente de la salud del jugador.

        rotRange = 180f;                                                                                        // Fijar rango de la rotación de los enemigos.

        InvokeRepeating("Spawn", 0f, spawnTime);                                                                // Llamar a la función después de un tiempo y continuar llamándola después del mismo tiempo.
        InvokeRepeating("SpawnBoss", 0f, spawnBoss);                                                            // Llamar a la función después de un tiempo y continuar llamándola después del mismo tiempo.
    }

    void Spawn()
    {
        currentEnemies = GameObject.FindGameObjectsWithTag("Enemy");                                            // Encontrar todos los enemigos que hay actualmente.

        if (playerHealth.currentHealth <= 0f || currentEnemies.Length >= maxEnemies)                            // Si el jugador no tiene salud o ya se ha alcanzado el máximo de enemigos...
            return;                                                                                             // ...salir de la función.

        enemy = enemies[Random.Range(0, enemies.Length)];
        radius = enemy.GetComponent<Collider>().bounds.extents.y;                                               // Calcular el radio del enemigo.

        SelectStats();                                                                                          // Seleccionar las estadísticas del enemigo.

        spawnPosition = new Vector3(Random.Range(xMin, xMax), 0f, Random.Range(zMin, zMax));                    // Buscar una posición aleatoria para que aparezca el enemigo.
        spawnRotation = Quaternion.Euler(0f, Random.Range(-rotRange, rotRange), 0f);                            // Buscar una rotación aleatoria para que aparezca el enemigo.

        RaycastHit hit;                                                                                         // Crear un golpe de rayo.
        Ray ray = new Ray(spawnPosition + Vector3.up * 100, Vector3.down);                                      // Crear un rayo que vaya desde arriba de la posición del enemigo hacia abajo.

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))                                                // Si el rayo golpea la capa del suelo...
            if (hit.collider != null)                                                                           // ...si el objeto que golpea tiene colisión...
                spawnPosition = new Vector3(spawnPosition.x, hit.point.y + radius, spawnPosition.z);            // ...cambiar la posición de aparición del enemigo a una posición por encima del suelo.

        Instantiate(enemy, spawnPosition, spawnRotation);                                                       // Crear el enemigo.
        spawnTime = Random.Range(3f, 6f);                                                                       // Fijar el tiempo entre cada creación.
    }

    void SelectStats()
    {
        EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();                                               // Componente de las estadísticas del enemigo.
        enemyStats.level = level;                                                                               // Fijar el nivel actual del enemigo.
        enemyStats.strength = strength;                                                                         // Fuerza del enemigo.
        enemyStats.resistance = resistance;                                                                     // Resistencia del enemigo.
        enemyStats.dexterity = 0.5f;                                                                            // Destreza del enemigo.
        enemyStats.physique = physique;                                                                         // Constitución del enemigo.
    }

    void SpawnBoss()
    {
        currentEnemies = GameObject.FindGameObjectsWithTag("EnemyBoss");                                        // Encontrar todos los jefes que hay actualmente.

        if (playerHealth.currentHealth <= 0f || currentEnemies.Length >= maxBosses)                             // Si el jugador no tiene salud o ya se ha alcanzado el máximo de jefes...
            return;                                                                                             // ...salir de la función.

        radius = enemyBoss.GetComponent<Collider>().bounds.extents.y;

        SelectStatsBoss();                                                                                      // Seleccionar las estadísticas del jefe.

        spawnPosition = new Vector3(Random.Range(xMin, xMax), 0f, Random.Range(zMin, zMax));                    // Buscar una posición aleatoria para que aparezca el jefe.
        spawnRotation = Quaternion.Euler(0f, Random.Range(-rotRange, rotRange), 0f);                            // Buscar una rotación aleatoria para que aparezca el jefe.

        RaycastHit hit;                                                                                         // Crear un golpe de rayo.
        Ray ray = new Ray(spawnPosition + Vector3.up * 100, Vector3.down);                                      // Crear un rayo que vaya desde arriba de la posición del enemigo hacia abajo.

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))                                                // Si el rayo golpea la capa del suelo...
            if (hit.collider != null)                                                                           // ...si el objeto que golpea tiene colisión...
                spawnPosition = new Vector3(spawnPosition.x, hit.point.y + radius, spawnPosition.z);            // ...cambiar la posición de aparición del enemigo a una posición por encima del suelo.

        Instantiate(enemyBoss, spawnPosition, spawnRotation);                                                   // Crear el jefe.
        spawnBoss = Random.Range(30f, 60f);                                                                     // Fijar el tiempo entre cada creación.
    }

    void SelectStatsBoss()
    {
        EnemyStats enemyStats = enemyBoss.GetComponent<EnemyStats>();                                           // Componente de las estadísticas del jefe.
        enemyStats.level = level;                                                                               // Fijar el nivel actual del enemigo.
        enemyStats.strength = strength * 2;                                                                     // Fuerza del jefe.
        enemyStats.resistance = resistance * 2;                                                                 // Resistencia del jefe.
        enemyStats.dexterity = 0.5f;                                                                            // Destreza del jefe.
        enemyStats.physique = physique * 2;                                                                     // Constitución del jefe.
    }
}