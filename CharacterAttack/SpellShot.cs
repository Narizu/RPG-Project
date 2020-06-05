using UnityEngine;

public class SpellShot : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.5f);                                              // Destruir el objeto.
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)                                                   // Si colisionamos con un objeto tangible...
        {
            if (other.tag == "Enemy" || other.tag == "EnemyBoss")               // ...si el objeto es un enemigo...
            {
                other.gameObject.GetComponent<EnemyHealth>().TakeDamage(0.5f);  // ...hacer daño al enemigo.
                Destroy(gameObject, 0.1f);                                      // ...destruir el objeto.
            }
        }
    }
}