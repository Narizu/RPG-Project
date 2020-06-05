using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    NavMeshAgent nav;                                                                                               // Referencia al agente de navegación.
    Vector3 randomPosition;                                                                                         // Posición aleatoria.
    Animator anim;                                                                                                  // Referencia a la animación.
    NPCManager npcManager;                                                                                          // Referencia al creador de NPCs.
    LayerMask mask;                                                                                                 // Capa del suelo.
    float radius;                                                                                                   // Radio del NPC.
    float timer;                                                                                                    // Tiempo para contar cuando se cambia de destino.
    float timeBetweenMoves;                                                                                         // Tiempo entre movimientos.
    float xMin;                                                                                                     // Rango de la posición en la que puede moverse el NPC.
    float xMax;                                                                                                     // Rango de la posición en la que puede moverse el NPC.
    float zMin;                                                                                                     // Rango de la posición en la que puede moverse el NPC.
    float zMax;                                                                                                     // Rango de la posición en la que puede moverse el NPC.

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();                                                                         // Componente del agente de navegación.
        anim = GetComponent<Animator>();                                                                            // Componente de la animación.
        npcManager = GameObject.Find("NPCManager").GetComponent<NPCManager>();                                      // Componente del creador de enemigos.
        mask = npcManager.mask;                                                                                     // Capa del suelo.
        radius = GetComponent<Collider>().bounds.extents.y;                                                         // Calcular el radio del NPC.
        timeBetweenMoves = Random.Range(10f, 20f);                                                                  // Fijar el tiempo entre movimientos.

        if (!nav.isOnNavMesh)                                                                                       // Si el agente no está bien colocado...
            Destroy(gameObject);                                                                                    // ...destruir el NPC.

        SetMoveRange();                                                                                             // Fijar rango de la posición de los NPCs.
    }

    void Update()
    {
        timer += Time.deltaTime;                                                                                    // Añadir el tiempo desde la última vez que se llamó a esta función.
        nav.speed = 3f;                                                                                             // Calcular la velocidad con la que el NPC se moverá.
        anim.SetFloat("AnimationSpeed", 0.6f);                                                                      // Fijar la velocidad de la animación a la animación.
        anim.SetFloat("Velocity", nav.velocity.magnitude);
        
        if (timer >= timeBetweenMoves)                                                                              // Si es tiempo para moverse...
        {
            randomPosition = new Vector3(Random.Range(xMin, xMax), 0f, Random.Range(zMin, zMax));                   // ...buscar una posición aleatoria.

            RaycastHit hit;                                                                                         // Crear un golpe de rayo.
            Ray ray = new Ray(randomPosition + Vector3.up * 100, Vector3.down);                                     // Crear un rayo que vaya desde arriba de la posición del NPC hacia abajo.

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))                                                // Si el rayo golpea la capa del suelo...
                if (hit.collider != null)                                                                           // ...si el objeto que golpea tiene colisión...
                    randomPosition = new Vector3(randomPosition.x, hit.point.y + radius, randomPosition.z);         // ...cambiar la posición de destino del NPC a una posición por encima del suelo.

            nav.SetDestination(randomPosition);                                                                     // ...fijar el destino del agente de navegación a una posición aleatoria.
            timer = 0f;                                                                                             // ...reiniciar el tiempo.
            timeBetweenMoves = Random.Range(10f, 20f);
        }
    }

    void SetMoveRange()
    {
        xMin = npcManager.xMin;                                                                                     // Fijar rango de la posición de los NPCs.
        xMax = npcManager.xMax;                                                                                     // Fijar rango de la posición de los NPCs.
        zMin = npcManager.zMin;                                                                                     // Fijar rango de la posición de los NPCs.
        zMax = npcManager.zMax;                                                                                     // Fijar rango de la posición de los NPCs.
    }
}