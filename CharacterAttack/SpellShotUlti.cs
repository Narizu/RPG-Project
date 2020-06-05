using UnityEngine;

public class SpellShotUlti : MonoBehaviour
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
            {
                other.gameObject.GetComponent<EnemyHealth>().TakeDamage(2f);    // ...hacer daño al enemigo.
                Destroy(gameObject, 0.5f);                                      // ...destruir el objeto.
            }
        }
    }
}