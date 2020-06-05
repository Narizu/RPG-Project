using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject item;                                                                                             // Objeto que va a ser creado.

    Vector3 spawnPosition;                                                                                              // Posición en la que aparecerá el objeto.
    Quaternion spawnRotation;                                                                                           // Rotación en la que aparecerá el objeto.
    float posRange;                                                                                                     // Rango de la posición.
    float rotRange;                                                                                                     // Rango de la rotación.
    int maxItems;                                                                                                       // Número máximo de objetos.

    void Start()
    {
        posRange = 40f;                                                                                                 // Fijar rango de la posición.
        rotRange = 180f;                                                                                                // Fijar rango de la rotación.
        maxItems = 10;                                                                                                  // Fijar número máximo de objetos.

        Spawn();                                                                                                        // Llamar a la función que creará los objetos.
    }

    void Spawn()
    {
        for (int i = 0; i < maxItems; i++)                                                                              // Para cada objeto...
        {
            spawnPosition = new Vector3(Random.Range(-posRange, posRange), 0.5f, Random.Range(-posRange, posRange));    // ...buscar una posición aleatoria.
            spawnRotation = Quaternion.Euler(0f, Random.Range(-rotRange, rotRange), 0f);                                // ...buscar una rotación aleatoria.

            Instantiate(item, spawnPosition, spawnRotation);                                                            // ...crear el objeto.
        }
    }
}