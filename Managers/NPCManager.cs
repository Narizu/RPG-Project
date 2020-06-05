using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public GameObject npc;                                                                                              // NPC que va a ser creado.
    public LayerMask mask;                                                                                              // Capa del suelo.
    public int maxCharacters;                                                                                           // Número máximo de NPCs.
    public float xMin;                                                                                                  // Rango de la posición en la que puede aparecer el NPC.
    public float xMax;                                                                                                  // Rango de la posición en la que puede aparecer el NPC.
    public float zMin;                                                                                                  // Rango de la posición en la que puede aparecer el NPC.
    public float zMax;                                                                                                  // Rango de la posición en la que puede aparecer el NPC.

    Vector3 spawnPosition;                                                                                              // Posición en la que aparecerá el NPC.
    Quaternion spawnRotation;                                                                                           // Rotación en la que aparecerá el NPC.
    float rotRange;                                                                                                     // Rango de la rotación.
    float radius;                                                                                                       // Radio del enemigo.

    void Start()
    {
        rotRange = 180f;                                                                                                // Fijar rango de la rotación.

        radius = npc.GetComponent<Collider>().bounds.extents.y;                                                         // Calcular el radio del enemigo.

        Spawn();                                                                                                        // Llamar a la función que creará los NPCs.
    }

    void Spawn()
    {
        for (int i = 0; i < maxCharacters; i++)                                                                         // Para cada NPC...
        {
            spawnPosition = new Vector3(Random.Range(xMin, xMax), 0f, Random.Range(zMin, zMax));                        // ...buscar una posición aleatoria.
            spawnRotation = Quaternion.Euler(0f, Random.Range(-rotRange, rotRange), 0f);                                // ...buscar una rotación aleatoria.

            RaycastHit hit;                                                                                             // Crear un golpe de rayo.
            Ray ray = new Ray(spawnPosition + Vector3.up * 100, Vector3.down);                                          // Crear un rayo que vaya desde arriba de la posición del NPC hacia abajo.

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))                                                    // Si el rayo golpea la capa del suelo...
                if (hit.collider != null)                                                                               // ...si el objeto que golpea tiene colisión...
                    spawnPosition = new Vector3(spawnPosition.x, hit.point.y + radius, spawnPosition.z);                // ...cambiar la posición de aparición del NPC a una posición por encima del suelo.

            Instantiate(npc, spawnPosition, spawnRotation);                                                             // ...crear el NPC.
        }
    }
}