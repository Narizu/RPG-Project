using UnityEngine;

public class FloorOfDeath : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(10000f);
    }
}