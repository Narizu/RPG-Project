using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Transform player;                                                                                               // Referencia a la posición del jugador.
    PlayerHealth playerHealth;                                                                                      // Referencia a la salud del jugador.
    EnemyHealth enemyHealth;                                                                                        // Referencia a la salud del enemigo.
    NavMeshAgent nav;                                                                                               // Referencia al agente de navegación.
    EnemyStats enemyStats;                                                                                          // Referencia a las estadísticas del enemigo.
    Vector3 randomPosition;                                                                                         // Posición aleatoria.
    Animator anim;                                                                                                  // Referencia a la animación.
    EnemyManager enemyManager;                                                                                      // Referencia al creador de enemigos.
    LayerMask mask;                                                                                                 // Capa del suelo.
    float radius;                                                                                                   // Radio del enemigo.
    float timer;                                                                                                    // Tiempo para contar cuando se cambia de destino.
    float timeBetweenMoves;                                                                                         // Tiempo entre movimientos.
    int distance;                                                                                                   // Distancia a la que el enemigo detectará al jugador.
    bool attacking;                                                                                                 // Cuando el enemigo está atacando.
    bool waiting;                                                                                                   // Cuando el enemigo tiene que esperar.
    float xMin;                                                                                                     // Rango de la posición en la que puede moverse el enemigo.
    float xMax;                                                                                                     // Rango de la posición en la que puede moverse el enemigo.
    float zMin;                                                                                                     // Rango de la posición en la que puede moverse el enemigo.
    float zMax;                                                                                                     // Rango de la posición en la que puede moverse el enemigo.
    float animSpeed;                                                                                                // Velocidad de la animación.

    void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();                                                                  // Componente de la salud del enemigo.
        nav = GetComponent<NavMeshAgent>();                                                                         // Componente del agente de navegación.
        enemyStats = GetComponent<EnemyStats>();                                                                    // Componente de las estadísticas del enemigo.
        anim = GetComponent<Animator>();                                                                            // Componente de la animación.
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();                                // Componente del creador de enemigos.
        mask = enemyManager.mask;                                                                                   // Capa del suelo.
        radius = GetComponent<Collider>().bounds.extents.y;                                                         // Calcular el radio del enemigo.
        timeBetweenMoves = Random.Range(10f, 20f);                                                                  // Fijar el tiempo entre movimientos.
        distance = 8;                                                                                               // Fijar la distancia para detectar al jugador.
        attacking = false;                                                                                          // Fijar atacando a falso.
        waiting = false;                                                                                            // Fijar esperando a falso.

        if (!nav.isOnNavMesh)                                                                                       // Si el agente no está bien colocado...
            Destroy(gameObject);                                                                                    // ...destruir el enemigo.

        SetMoveRange();                                                                                             // Fijar rango de la posición de los enemigos.
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;                                              // Componente de la posición del jugador.
        playerHealth = player.GetComponent<PlayerHealth>();                                                         // Componente de la salud del jugador.
    }

    void Update()
    {
        if (enemyHealth.currentHealth <= 0 || playerHealth.currentHealth <= 0)                                      // Si el enemigo o el jugador no tienen salud...
        {
            nav.enabled = false;                                                                                    // ...desactivar el agente de navegación.
            return;                                                                                                 // ...salir de la función.
        }
        
        timer += Time.deltaTime;                                                                                    // Añadir el tiempo desde la última vez que se llamó a esta función.
        nav.speed = 3f + (enemyStats.speed * 0.01f);                                                                // Calcular la velocidad con la que el enemigo se moverá.
        anim.SetFloat("Velocity", nav.velocity.magnitude);

        if (attacking)                                                                                              // Si el enemigo está atacando...
            nav.speed /= 2f;                                                                                        // ...reducir su velocidad a la mitad.

        SetAnimation();                                                                                             // Fijar la animación.

        if (waiting)                                                                                                // Si está esperando...
            return;                                                                                                 // ...salir de la función.

        if (PlayerInRange() || enemyHealth.currentHealth < enemyStats.health * 10)                                  // Si el jugador está en rango o el enemigo no tiene la vida al máximo...
            nav.SetDestination(player.position);                                                                    // ...fijar el destino del agente de navegación al jugador.
        else                                                                                                        // Si no...
            RandomMovement();                                                                                       // ...mover al enemigo aleatoriamente.
    }

    bool PlayerInRange()
    {
        return Vector3.Distance(transform.position, player.position) < distance;                                    // Cuando el jugador está en rango.
    }

    void RandomMovement()
    {
        if (timer >= timeBetweenMoves)                                                                              // Si es tiempo para moverse...
        {
            randomPosition = new Vector3(Random.Range(xMin, xMax), 0f, Random.Range(zMin, zMax));                   // ...buscar una posición aleatoria.

            RaycastHit hit;                                                                                         // Crear un golpe de rayo.
            Ray ray = new Ray(randomPosition + Vector3.up * 100, Vector3.down);                                     // Crear un rayo que vaya desde arriba de la posición del enemigo hacia abajo.

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))                                                // Si el rayo golpea la capa del suelo...
                if (hit.collider != null)                                                                           // ...si el objeto que golpea tiene colisión...
                    randomPosition = new Vector3(randomPosition.x, hit.point.y + radius, randomPosition.z);         // ...cambiar la posición de destino del enemigo a una posición por encima del suelo.

            nav.SetDestination(randomPosition);                                                                     // ...fijar el destino del agente de navegación a una posición aleatoria.
            timer = 0f;                                                                                             // ...reiniciar el tiempo.
            timeBetweenMoves = Random.Range(10f, 20f);                                                              // Fijar el tiempo entre movimientos.
        }
    }

    public void SetAttacking(bool attacking)
    {
        this.attacking = attacking;                                                                                 // Cuando el enemigo está atacando.
    }

    void SetAnimation()
    {
        animSpeed = 0.6f + (enemyStats.speed * 0.002f);                                                             // Calcular la velocidad de la animación.

        if (animSpeed > 8)                                                                                          // Si la velocidad de la animación supera el máximo...
            animSpeed = 8f;                                                                                         // ...fijar la velocidad de la animación al máximo.

        anim.SetFloat("AnimationSpeed", animSpeed);                                                                 // Fijar la velocidad de la animación a la animación.
    }

    void SetMoveRange()
    {
        xMin = enemyManager.xMin;                                                                                   // Fijar rango de la posición de los enemigos.
        xMax = enemyManager.xMax;                                                                                   // Fijar rango de la posición de los enemigos.
        zMin = enemyManager.zMin;                                                                                   // Fijar rango de la posición de los enemigos.
        zMax = enemyManager.zMax;                                                                                   // Fijar rango de la posición de los enemigos.
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NoPass")                                                                                  // Si el enemigo colisiona con el no pasar...
        {
            waiting = true;                                                                                         // ...fijar esperando a verdadero.
            timer = timeBetweenMoves;                                                                               // ...fijar el tiempo al tiempo entre movimientos.
            RandomMovement();                                                                                       // ...fijar un nuevo movimiento aleatorio.
            StartCoroutine(Wait());                                                                                 // ...esperar.
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);                                                                         // Esperar un tiempo.
        waiting = false;                                                                                            // Fijar esperando a falso.
    }
}