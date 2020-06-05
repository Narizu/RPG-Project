using UnityEngine;

public class SpellShotSkill : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.75f);                                             // Destruir el objeto.
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)                                                   // Si colisionamos con un objeto tangible...
        {
            if (other.tag == "Enemy" || other.tag == "EnemyBoss")               // ...si el objeto es un enemigo...
            {
                other.gameObject.GetComponent<EnemyHealth>().TakeDamage(1f);    // ...hacer daño al enemigo.
                Destroy(gameObject, 0.25f);                                     // ...destruir el objeto.
            }
        }
    }
}