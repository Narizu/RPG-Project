using UnityEngine;

public class ArrowShotUlti : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1f);                                                // Destruir el objeto.
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)                                                   // Si colisionamos con un objeto tangible...
        {
            if (other.tag == "Enemy" || other.tag == "EnemyBoss")               // ...si el objeto es un enemigo...
                other.gameObject.GetComponent<EnemyHealth>().TakeDamage(2f);    // ...hacer daño al enemigo.

            if (other.tag != "Player" && other.tag != "Floor")                  // ...si el objeto no es el jugador...
                Destroy(gameObject, 0.5f);                                      // ...destruir el objeto.
        }
    }
}