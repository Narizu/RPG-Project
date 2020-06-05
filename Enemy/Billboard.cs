using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform);                                                            // Mirar a la cámara.
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);  // Corregir la orientación.
    }
}