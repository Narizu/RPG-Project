using UnityEngine;

public class FloorFollow : MonoBehaviour
{
    Transform target;                                                                                   // Posición que el suelo seguirá.

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;                                  // Fijar el jugador como objetivo del suelo.
    }

    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y + 1, target.position.z);  // Actualizar la posición del suelo a la posición del jugador.
    }
}